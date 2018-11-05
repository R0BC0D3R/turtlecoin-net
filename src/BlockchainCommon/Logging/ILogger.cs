// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

namespace Logging
{

public enum Level
{
  FATAL = 0,
  ERROR = 1,
  WARNING = 2,
  INFO = 3,
  DEBUGGING = 4,
  TRACE = 5
}

public interface ILogger
{
  public readonly char COLOR_DELIMETER;

  public readonly List<string> LEVEL_NAMES = new List<string>();

  void operator ()(string category, Level level, boost::posix_time.ptime time, string body);
}

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl

}


