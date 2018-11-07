// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System;
using System.Collections.Generic;

namespace Logging
{
    public class LoggerGroup : CommonLogger
    {
        protected List<ILogger> loggers = new List<ILogger>();

        public LoggerGroup(Level level = Level.DEBUGGING) : base(level)
        {
        }

        public void AddLogger(ILogger logger)
        {
            loggers.Add(logger);
        }

        public void RemoveLogger(ILogger logger)
        {
            loggers.Remove(logger);
        }

        public override void FunctorMethod(string category, Level level, DateTime time, string body)
        {
            //TODO: Redo this
            //if (level <= ((int)logLevel) != 0 && disabledCategories.count(category) == 0)
            //{
            //    foreach (var logger in loggers)
            //    {
            //        logger(category, level, time, body);
            //    }
            //}
        }
    }
}