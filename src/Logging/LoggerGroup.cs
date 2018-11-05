using System.Collections.Generic;

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// This file is part of Bytecoin.
//
// Bytecoin is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Bytecoin is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// This file is part of Bytecoin.
//
// Bytecoin is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Bytecoin is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.


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
  public static override void functorMethod(string category, Level level, boost::posix_time.ptime time, string body)
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


