// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl

namespace Logging
{

//C++ TO C# CONVERTER TODO TASK: Multiple inheritance is not available in C#:
public class LoggerMessage : std::ostream, std::streambuf, System.IDisposable
{
//C++ TO C# CONVERTER TODO TASK: The original C++ constructor header had calls to multiple base class constructors:
//ORIGINAL LINE: LoggerMessage(ILogger& logger, const string& category, Level level, const string& color) : std::ostream(this), std::streambuf(), logger(logger), category(category), logLevel(level), message(color), timestamp(boost::posix_time::microsec_clock::local_time()), gotText(false)
  public LoggerMessage(ILogger logger, string category, Level level, string color)
  {
	  this.logger = new Logging.ILogger(logger);
	  this.category = category;
	  this.logLevel = new Logging.Level(level);
	  this.message = color;
	  this.timestamp = boost::posix_time.microsec_clock.local_time();
	  this.gotText = false;
  }
  public void Dispose()
  {
	if (gotText)
	{
	  this << std::endl;
	}
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  LoggerMessage(const LoggerMessage&) = delete;
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  LoggerMessage& operator =(const LoggerMessage&) = delete;
#if ! __linux__
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The original C++ constructor header had calls to multiple base class constructors:
//ORIGINAL LINE: LoggerMessage(LoggerMessage&& other) : std::ostream(std::move(other)), std::streambuf(std::move(other)), category(other.category), logLevel(other.logLevel), logger(other.logger), message(other.message), timestamp(boost::posix_time::microsec_clock::local_time()), gotText(false)
  public LoggerMessage(LoggerMessage && other)
  {
	  this.category = other.category;
	  this.logLevel = new Logging.Level(other.logLevel);
	  this.logger = new Logging.ILogger(other.logger);
	  this.message = other.message;
	  this.timestamp = boost::posix_time.microsec_clock.local_time();
	  this.gotText = false;
	this.set_rdbuf(this);
  }
#endif

  private override int sync()
  {
	logger(category, logLevel, timestamp, message);
	gotText = false;
	message = DEFAULT;
	return 0;
  }
  private override std::streamsize xsputn(string s, std::streamsize n)
  {
	gotText = true;
	message.append(s, n);
	return n;
  }
  private override int overflow(int c)
  {
	gotText = true;
	message += (char)c;
	return 0;
  }

  private string message;
  private readonly string category;
  private Level logLevel;
  private ILogger logger;
  private boost::posix_time.ptime timestamp = new boost::posix_time.ptime();
  private bool gotText;
}

}


namespace Logging
{

#if ! ! __linux__
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
#endif

}
