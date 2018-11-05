// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{

public abstract class IUpgradeDetector : System.IDisposable
{
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
  enum :
//C++ TO C# CONVERTER TODO TASK: The following method format was not recognized, possibly due to an unrecognized macro:
  uint
  {
	UNDEF_HEIGHT = (uint)-1
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual byte targetVersion() const = 0;
  public abstract byte targetVersion();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint upgradeIndex() const = 0;
  public abstract uint upgradeIndex();
  public virtual void Dispose()
  {
  }
}

}
