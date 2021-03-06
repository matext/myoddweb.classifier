#pragma once

#include <string>
#include <map>
#include <unordered_map>
#include "../common/engine.h"

using namespace System::Collections::Generic;

using namespace System;

class ClassifyEngine
{
public:
  ClassifyEngine();

public:
  //
  // Version
  //
  int GetEngineVersion();

  //
  //  Setup
  //
  bool Initialise(String^ eventViewSource, String^ configurationXml, String^ databasePath);
  void Release();

  //
  //  Configurations 
  //
  bool SetConfig(String^ configName, String^ configValue);
  bool GetConfig(String^ configName, String^% configValue);

  //
  //  Classification 
  //
  bool Train(String^ categoryName, String^ textToCategorise, String^ uniqueIdentifier, int weight );
  bool UnTrain( String^ uniqueIdentifier, String^ textToCategorise);
  int Categorize(String^ textToCategorise, unsigned int minPercentage);
  int Categorize(String^ textToCategorise, unsigned int minPercentage, List<Classifier::Interfaces::Helpers::WordCategory^> ^% wordsCategory, Dictionary<int, double> ^% categoryProbabilities );

  //
  //  Information / Manipulation
  //
  int GetCategoryFromUniqueId(String^ uniqueIdentifier);
  int GetCategory(String^ categoryName);
  int GetCategories(Dictionary<int, String^> ^% categories);
  bool RenameCategory(String^ oldName, String^ newName);
  bool DeleteCategory(String^ categoryName);

  //
  //  Magnets
  // 
  int CreateMagnet(String^ magnetName, int ruleType, int categoryTarget);
  bool UpdateMagnet(int id, String^ magnetName, int ruleType, int categoryTarget);
  bool DeleteMagnet(int id);
  int GetMagnets(List<Classifier::Interfaces::Helpers::Magnet^> ^% magnets );

  // Log
  int Log(String^ source, String^ entry);
  bool ClearLogEntries(long long olderThan);
  int GetLogEntries(List<Classifier::Interfaces::Helpers::LogEntry^> ^% entries, int max);

protected:

  typedef bool(__stdcall *f_Initialise)(const char*);
  typedef bool(__stdcall *f_SetConfig)(const char16_t*, const char16_t*);
  typedef bool(__stdcall *f_GetConfig)(const char16_t*, char16_t*, size_t);
  typedef bool(__stdcall *f_TrainEx)(const char16_t*, const char16_t*, const char16_t*, const int );
  typedef bool(__stdcall *f_UnTrainEx)(const char16_t*, const char16_t*);
  typedef bool(__stdcall *f_RenameCategory)(const char16_t*, const char16_t*);
  typedef bool(__stdcall *f_DeleteCategory)(const char16_t*);
  typedef bool(__stdcall *f_Release)();

  typedef int(__stdcall *f_GetCategoryInfo)(int number, int& id, int categoryNameLength, char16_t* categoryName);
  typedef int(__stdcall *f_Categorize)(const char16_t*, unsigned int);
  typedef int(__stdcall *f_GetCategoryFromUniqueId)(const char16_t*);
  typedef int(__stdcall *f_GetCategory)(const char16_t*);

  typedef int(__stdcall* f_GetMagnets)(MagnetInfo* magnets, int max);
  typedef int(__stdcall *f_CreateMagnet)(const char16_t*, int , int);
  typedef bool(__stdcall *f_UpdateMagnet)(int, const char16_t*, int, int);
  typedef bool(__stdcall *f_DeleteMagnet)(int);
    
  typedef int(__stdcall *f_CategorizeWithInfo)( const char16_t*, unsigned int, TextCategoryInfo*);
  
  typedef int(__stdcall *f_GetVersion)();

  typedef int(__stdcall *f_Log)(const char16_t*, const char16_t*);
  typedef bool(__stdcall *f_ClearLogEntries)(long long);

  // all the log entries, the id => LogEntry
  typedef int( __stdcall *f_GetLogEntries)(LogEntryInfo*, int);

  enum ProcType
  {
    procUnk = 0,
    procFirst = 1,
    procInitialise = procFirst,
    procSetConfig,
    procGetConfig,
    procTrainEx,
    procUnTrainEx,
    procCategorize,
    procCategorizeWithInfo,
    procGetCategoryFromUniqueId,
    procGetCategory,
    procGetCategoryInfo,
    procRenameCategory,
    procDeleteCategory,

    procCreateMagnet,
    procDeleteMagnet,
    procUpdateMagnet,
    procGetMagnets,
    procGetVersion,

    procLog,
    procClearLogEntries,
    procGetLogEntries,

    procRelease,

    procLast
  };

  HINSTANCE _hGetProcIDDLL;
  const FARPROC GetUnmanagedFunction(ProcType procType) const;

  typedef std::unordered_map<ProcType, FARPROC> ProcsFarProc;
  ProcsFarProc _farProcs;

protected:
  // the name of the event viewer source.
  std::string _eventViewSource;

  //  all the event loggins.
  void LogEventWarning(String^ sEvent);
  void LogEventInfo(String^ sEvent);
  void LogEventError(String^ sEvent);

private:
  bool _canUseEventLog;
  bool CanUseLog() const;
  void SetCanUseLog();

private:
  bool InitialiseUnmanagedHinstance(String^ enginePath);
  bool InitialiseUnmanagedFunctions();
  bool InitialiseUnmanagedFunction(HINSTANCE hInstance, ProcType procType);
  bool CallUnmanagedInitialise(String^ databasePath);

  void FreeLogEntries(LogEntryInfo* logEntries, int count );
  LogEntryInfo* CreateLogEntries( int count );

  void FreeMagnets(MagnetInfo* magnets, int count);
  void PrepareMagnetsName( MagnetInfo* magnets, int count );
  MagnetInfo* CreateMagnets( int count );

  WordCategoryAndProbability* CreateWordCategoryInfo( int size );
  void FreeWordCategoryInfo(WordCategoryAndProbability* words, int size);

  CategoryProbability* CreateCategoryProbability(int size);
  void FreeCategoryProbability(CategoryProbability* categories, int size);
};

