// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace Logging
{

    public class LoggerRef
    {
        public static ILogger logger;
        public static string category;

        public LoggerRef(ILogger logger, string category)
        {
            LoggerRef.logger = logger;
            LoggerRef.category = category;
        }

        public static LoggerMessage FunctorMethod(Level level, string color)
        {
            return new LoggerMessage(logger, category, level, color);
        }

        public ILogger GetLogger()
        {
            return logger;
        }
    }
}