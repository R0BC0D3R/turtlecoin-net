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
        char COLOR_DELIMETER { get; set; }
        List<string> LEVEL_NAMES { get; set; }
    }
}