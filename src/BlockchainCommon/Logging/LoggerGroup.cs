// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl

namespace Logging
{

public class LoggerGroup : CommonLogger
{
  public LoggerGroup(Level level = DEBUGGING) : base.functorMethod(level)
  {
  }

  public void addLogger(ILogger logger)
  {
	loggers.Add(logger);
  }
  public void removeLogger(ILogger logger)
  {
//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'erase' method in C#:
	loggers.erase(std::remove(loggers.GetEnumerator(), loggers.end(), logger), loggers.end());
  }
  public static override void functorMethod(string category, Level level, DateTime time, string body)
  {
	if (level <= ((int)logLevel) != 0 && disabledCategories.count(category) == 0)
	{
	  foreach (var logger in loggers)
	  {
		logger(category, level, time, body);
	  }
	}
  }

  protected List<ILogger> loggers = new List<ILogger>();
}

}


