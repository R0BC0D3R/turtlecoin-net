// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{

public abstract class IUpgradeManager : System.IDisposable
{
  public virtual void Dispose()
  {
  }

  public abstract void addMajorBlockVersion(byte targetVersion, uint upgradeHeight);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual byte getBlockMajorVersion(uint blockIndex) const = 0;
  public abstract byte getBlockMajorVersion(uint blockIndex);
}

}
