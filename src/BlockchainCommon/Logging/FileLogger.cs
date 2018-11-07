// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.IO;

namespace Logging
{
    public class FileLogger : StreamLogger
    {
        private FileStream fileStream;

        public FileLogger(Level level = Level.DEBUGGING) : base(level)
        {
        }

        public void init(string fileName)
        {
            fileStream = new FileStream(fileName, FileMode.OpenOrCreate);
            base.attachToStream(fileStream);
        }
    }
}