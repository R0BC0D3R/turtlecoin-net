// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace Logging
{

public class FileLogger : StreamLogger
{
  public FileLogger(Level level = Level.DEBUGGING) : base(level)
  {
  }
  public void init(string fileName)
  {
	fileStream.open(fileName, std::ios.app);
	base.attachToStream(fileStream);
  }

  private std::ofstream fileStream = new std::ofstream();
}

}


