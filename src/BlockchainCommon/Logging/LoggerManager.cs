// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using JsonValue = Common.JsonValue;
using System.Collections.Generic;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl

namespace Logging
{

public class LoggerManager : LoggerGroup
{
  public LoggerManager()
  {
  }
  public void configure(JsonValue val)
  {
	std::unique_lock<object> @lock = new std::unique_lock<object>(reconfigureLock);
	loggers.Clear();
	base.loggers.Clear();
	Level globalLevel;
	if (val.contains("globalLevel"))
	{
	  var levelVal = val("globalLevel");
	  if (levelVal.isInteger())
	  {
		globalLevel = (Level)levelVal.getInteger();
	  }
	  else
	  {
		throw new System.Exception("parameter globalLevel has wrong type");
	  }
	}
	else
	{
	  globalLevel = Level.TRACE;
	}
	List<string> globalDisabledCategories = new List<string>();

	if (val.contains("globalDisabledCategories"))
	{
	  var globalDisabledCategoriesList = val("globalDisabledCategories");
	  if (globalDisabledCategoriesList.isArray())
	  {
		size_t countOfCategories = globalDisabledCategoriesList.size();
		for (size_t i = 0; i < countOfCategories; ++i)
		{
		  var categoryVal = globalDisabledCategoriesList[i];
		  if (categoryVal.isString())
		  {
			globalDisabledCategories.Add(categoryVal.getString());
		  }
		}
	  }
	  else
	  {
		throw new System.Exception("parameter globalDisabledCategories has wrong type");
	  }
	}

	if (val.contains("loggers"))
	{
	  var loggersList = val("loggers");
	  if (loggersList.isArray())
	  {
		size_t countOfLoggers = loggersList.size();
		for (size_t i = 0; i < countOfLoggers; ++i)
		{
		  var loggerConfiguration = loggersList[i];
		  if (!loggerConfiguration.isObject())
		  {
			throw new System.Exception("loggers element must be objects");
		  }

		  Level level = Level.INFO;
		  if (loggerConfiguration.contains("level"))
		  {
			level = (Level)(loggerConfiguration("level").getInteger());
		  }

		  string type = loggerConfiguration("type").getString();
		  std::unique_ptr<Logging.CommonLogger> logger = new std::unique_ptr<Logging.CommonLogger>();

		  if (type == "console")
		  {
			logger.reset(new ConsoleLogger(level));
		  }
		  else if (type == "file")
		  {
			string filename = loggerConfiguration("filename").getString();
			var fileLogger = new FileLogger(level);
			fileLogger.init(filename);
			logger.reset(fileLogger);
		  }
		  else
		  {
			throw new System.Exception("Unknown logger type: " + type);
		  }

		  if (loggerConfiguration.contains("pattern"))
		  {
			logger.setPattern(loggerConfiguration("pattern").getString());
		  }

		  List<string> disabledCategories = new List<string>();
		  if (loggerConfiguration.contains("disabledCategories"))
		  {
			var disabledCategoriesVal = loggerConfiguration("disabledCategories");
			size_t countOfCategories = disabledCategoriesVal.size();
			for (size_t i = 0; i < countOfCategories; ++i)
			{
			  var categoryVal = disabledCategoriesVal[i];
			  if (categoryVal.isString())
			  {
				logger.disableCategory(categoryVal.getString());
			  }
			}
		  }

		  loggers.emplace_back(std::move(logger));
		  addLogger(*loggers[loggers.Count - 1]);
		}
	  }
	  else
	  {
		throw new System.Exception("loggers parameter has wrong type");
	  }
	}
	else
	{
	  throw new System.Exception("loggers parameter missing");
	}
	setMaxLevel(globalLevel);
	foreach (var category in globalDisabledCategories)
	{
	  disableCategory(category);
	}
  }
  public static override void functorMethod(string category, Level level, boost::posix_time.ptime time, string body)
  {
	std::unique_lock<object> @lock = new std::unique_lock<object>(reconfigureLock);
	base  .functorMethod(category, level, time, body);
  }

  private List<std::unique_ptr<CommonLogger>> loggers = new List<std::unique_ptr<CommonLogger>>();
  private object reconfigureLock = new object();
}

}


