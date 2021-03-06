﻿using Classifier.Interfaces;
using myoddweb.classifier.interfaces;
using myoddweb.classifier.utils;
using System;
using System.IO;
using System.Reflection;
using System.Timers;

namespace myoddweb.classifier.core
{
  public class OutlookEngine : IEngine, IDisposable
  {
    /// <summary>
    /// All the folders.
    /// </summary>
    private IFolders _folders;

    /// <summary>
    /// The logger
    /// </summary>
    private ILogger _logger;

    /// <summary>
    /// the root folder.
    /// </summary>
    private readonly Microsoft.Office.Interop.Outlook.MAPIFolder _rootFolder;

    /// <summary>
    /// The timer we use to call the clean log function.
    /// </summary>
    private Timer LogTimer { get; set; }

    /// <summary>
    /// Name for logging in the event viewer,
    /// </summary>
    private const string EventViewSource = "myoddweb.classifier";

    /// <summary>
    /// The parent engine
    /// </summary>
    private readonly IEngine _parent;

    public ICategories Categories => _parent.Categories;

    public IClassify Classify => _parent.Classify;

    public IMagnets Magnets => _parent.Magnets;

    public IConfig Config => _parent.Config;

    public IOptions Options => _parent.Options;

    /// <summary>
    /// The logger.
    /// </summary>
    public ILogger Logger => _logger ?? (_logger = new OutlookLogger(EventViewSource, _parent.Logger ));

    /// <summary>
    /// Get all the folders.
    /// </summary>
    public IFolders Folders => _folders ?? (_folders = new OutlookFolders(GetRootFolder()));
    
    public Version GetEngineVersion()
    {
      return _parent.GetEngineVersion();
    }

    public OutlookEngine(Microsoft.Office.Interop.Outlook.MAPIFolder rootFolder) : this(new Engine(InitialiseEngine()))
    {
      _rootFolder = rootFolder;
    }

    /// <summary>
    /// The engine constructor.
    /// </summary>
    public OutlookEngine( IEngine parent )
    {
      //  this cannot be null!
      if (parent == null)
      {
        throw new ArgumentNullException(nameof(parent));
      }

      // the parent engine
      _parent = parent;

      // start the 'cleanup' timer.
      StartLogCleanupTimer();
    }

    ~OutlookEngine()
    {
      // release the engine
      Release();
    }

    public void Dispose()
    {
      Release();
    }

    private void Release()
    {
      // stop the time
      StopLogCleanupTimer();
    }

    // start the 'cleanup' timer.
    private void StartLogCleanupTimer()
    {
      //  stop the timer if need be.
      StopLogCleanupTimer();

      // start the new time
      LogTimer = new Timer();
      LogTimer.Elapsed += OnTimedLogEvent;
      LogTimer.Interval = 60 * 60 * 1000;  // one hour
      LogTimer.Enabled = true;
    }

    private void StopLogCleanupTimer()
    {
      LogTimer?.Stop();
      LogTimer?.Dispose();
      LogTimer = null;
    }

    private void OnTimedLogEvent(object source, ElapsedEventArgs e)
    {
      if (null == Classify)
      {
        return;
      }

      if (null == Options)
      {
        return;
      }

      // days of retention
      var daysRetention = Options.LogRetention;

      // the oldest log date
      var date = DateTime.UtcNow.AddDays(daysRetention * -1);

      // delete old entries.
      Logger.ClearLogEntries(Helpers.DateTimeToUnix(date));
    }


    /// <summary>
    /// Initialise the engine and load all the resources neeed.
    /// Will load the database and so on to get the plugin ready for use.
    /// </summary>
    /// <returns>boolean success or not.</returns>
    private static IClassify1 InitialiseEngine()
    {
      // the paths we will be using.
      var directoryName = AppDomain.CurrentDomain.BaseDirectory;

      //  the database path
      // %appdata%\MyOddWeb\Classifier
      var myAppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MyOddWeb\\Classifier");
      Directory.CreateDirectory(myAppData);
      var databasePath = Path.Combine(myAppData, "database.classifier");

      // initialise the engine.
      return InitialiseEngine(directoryName, databasePath);
    }

    /// <summary>
    /// Initialise the engine and load all the resources neeed.
    /// Will load the database and so on to get the plugin ready for use.
    /// </summary>
    /// <param name="directoryName">string the directory we are loading from.</param>
    /// <param name="databasePath">string the name/path of the database we will be loading.</param>
    /// <returns></returns>
    private static IClassify1 InitialiseEngine(string directoryName, string databasePath)
    {
      var dllInteropPath = Path.Combine(directoryName, "x86\\Classifier.Interop.dll");
      var dllEnginePath = Path.Combine(directoryName, "x86\\Classifier.Engine.dll");
      if (Environment.Is64BitProcess)
      {
        dllInteropPath = Path.Combine(directoryName, "x64\\Classifier.Interop.dll");
        dllEnginePath = Path.Combine(directoryName, "x64\\Classifier.Engine.dll");
      }

      // look for the 
      Assembly asm;
      try
      {
        asm = Assembly.LoadFrom(dllInteropPath);
      }
      catch (ArgumentException ex)
      {
        throw new Exception($"The interop file name/path does not appear to be valid. '{dllInteropPath}'.{Environment.NewLine}{Environment.NewLine}{ex.Message}");
      }
      catch (FileNotFoundException ex)
      {
        throw new Exception($"Unable to load the interop file. '{dllInteropPath}'.{Environment.NewLine}{Environment.NewLine}{ex.Message}");
      }

      // look for the interop interface
      var classifyEngine = TypeLoader.LoadTypeFromAssembly<IClassify1>(asm);
      if (null == classifyEngine)
      {
        // could not locate the interface.
        throw new Exception($"Unable to load the IClasify1 interface in the interop file. '{dllInteropPath}'.");
      }

      // initialise the engine itself.
      if (!classifyEngine.Initialise(EventViewSource, dllEnginePath, databasePath))
      {
        return null;
      }
      return classifyEngine;
    }

    private Microsoft.Office.Interop.Outlook.MAPIFolder GetRootFolder()
    {
      return _rootFolder;
    }
  }
}
