﻿namespace myoddweb.classifier.core
{
  public class Options
  {
    private readonly Engine _engine;
    
    internal bool? _reCheckCategories;

    internal bool? _checkIfUnownCategory;

    internal bool? _reCheckIfCtrlIsDown;

    /// <summary>
    /// (re) Check all the categories all the time.
    /// This is on by default as we have the other default option "CheckIfUnownCategory" also set to on.
    /// The net effect of that would be to only check if we don't already know the value.
    /// </summary>
    public bool ReCheckCategories
    {
      get {
        return (bool) (_reCheckCategories ??
                       (_reCheckCategories = ("1" == _engine?.GetConfigWithDefault("Option.ReCheckCategories", "1"))));
      }
      set
      {
        _reCheckCategories = value;
         _engine?.SetConfig("Option.ReCheckCategories", (value ? "1" : "0") );
      }
    }

    /// <summary>
    /// (re) Check all the categories if the control key is pressed down.
    /// This is on by default as the control key has to be pressed.
    /// </summary>
    public bool ReCheckIfCtrlKeyIsDown
    {
      get
      {
        return (bool)(_reCheckIfCtrlIsDown ??
                       (_reCheckIfCtrlIsDown = ("1" == _engine?.GetConfigWithDefault("Option.ReCheckIfCtrlKeyIsDown", "1"))));
      }
      set
      {
        _reCheckIfCtrlIsDown = value;
        _engine?.SetConfig("Option.ReCheckIfCtrlKeyIsDown", (value ? "1" : "0"));
      }
    }

    /// <summary>
    /// Check the category only if we don't currently have a valid value.
    /// The default is to get the value if the value is not known.
    /// </summary>
    public bool CheckIfUnownCategory
    {
      get
      {
        return (bool)(_checkIfUnownCategory ??
                       (_checkIfUnownCategory = ("1" == _engine?.GetConfigWithDefault("Option.CheckIfUnownCategory", "1"))));
      }
      set
      {
        _checkIfUnownCategory = value;
        _engine?.SetConfig("Option.CheckIfUnownCategory", (value ? "1" : "0"));
      }
    }
    
    public Options(Engine engine)
    {
      _engine = engine;
    }
  }
}
