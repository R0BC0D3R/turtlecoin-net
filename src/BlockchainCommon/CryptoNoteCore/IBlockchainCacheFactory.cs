// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class IBlockchainCache;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class Currency;

public abstract class IBlockchainCacheFactory : System.IDisposable
{
  public virtual void Dispose()
  {
  }

  public abstract std::unique_ptr<IBlockchainCache> createRootBlockchainCache(Currency currency);
  public abstract std::unique_ptr<IBlockchainCache> createBlockchainCache(Currency currency, IBlockchainCache parent, uint startIndex = 0);
}

} //namespace CryptoNote
