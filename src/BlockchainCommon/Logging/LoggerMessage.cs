// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System;

namespace Logging
{
    //TODO: Change this 
    public class LoggerMessage : System.IDisposable
    {
        private string message;
        private readonly string category;
        private Level logLevel;
        private ILogger logger;
        private DateTime timestamp = new DateTime();
        private bool gotText;
      
        public LoggerMessage(ILogger logger, string category, Level level, string color)
        {
            this.logger = logger;
            this.category = category;
            this.logLevel = level;
            this.message = color;
            this.timestamp = DateTime.Now;
            this.gotText = false;
        }
        public void Dispose()
        {
            //if (gotText)
            //{
            //    this << std::endl;
            //}
        }

        //TODO: Redo this
//#if !__linux__
//        //C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//        //C++ TO C# CONVERTER TODO TASK: The original C++ constructor header had calls to multiple base class constructors:
//        //ORIGINAL LINE: LoggerMessage(LoggerMessage&& other) : std::ostream(std::move(other)), std::streambuf(std::move(other)), category(other.category), logLevel(other.logLevel), logger(other.logger), message(other.message), timestamp(boost::posix_time::microsec_clock::local_time()), gotText(false)
//        public LoggerMessage(LoggerMessage && other)
//        {
//            this.category = other.category;
//            this.logLevel = new Logging.Level(other.logLevel);
//            this.logger = new Logging.ILogger(other.logger);
//            this.message = other.message;
//            this.timestamp = boost::posix_time.microsec_clock.local_time();
//            this.gotText = false;
//            this.set_rdbuf(this);
//        }
//#endif

        //private override int sync()
        //{
        //    logger(category, logLevel, timestamp, message);
        //    gotText = false;
        //    message = DEFAULT;
        //    return 0;
        //}
        //private override std::streamsize xsputn(string s, std::streamsize n)
        //{
        //    gotText = true;
        //    message.append(s, n);
        //    return n;
        //}
        //private override int overflow(int c)
        //{
        //    gotText = true;
        //    message += (char)c;
        //    return 0;
        //}
    }
}