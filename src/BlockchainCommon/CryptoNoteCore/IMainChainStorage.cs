// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{

public abstract class IMainChainStorage : System.IDisposable
{
  public virtual void Dispose()
  {
  }

  public abstract void pushBlock(RawBlock rawBlock);
  public abstract void popBlock();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual RawBlock getBlockByIndex(uint index) const = 0;
  public abstract RawBlock getBlockByIndex(uint index);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getBlockCount() const = 0;
  public abstract uint getBlockCount();

  public abstract void clear();
}

}
