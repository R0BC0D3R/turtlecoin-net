// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl

namespace Logging
{

public class LoggerRef
{
  public LoggerRef(ILogger logger, string category)
  {
	  this.logger = logger;
	  this.category = category;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: LoggerMessage operator ()(Level level, const string& color) const
  public static LoggerMessage functorMethod(Level level, string color)
  {
	return new LoggerMessage(logger, category, level, color);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ILogger& getLogger() const
  public ILogger getLogger()
  {
	return logger;
  }

  private ILogger logger;
  private string category;
}

}


