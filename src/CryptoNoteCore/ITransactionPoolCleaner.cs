using System.Collections.Generic;

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
// 
// Please see the included LICENSE file for more information.




namespace Crypto
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//struct Hash;
}

namespace CryptoNote
{

public abstract class ITransactionPoolCleanWrapper: ITransactionPool, System.IDisposable
{
  public virtual void Dispose()
  {
  }

  public abstract List<Crypto.Hash> clean(uint height);
}

} //namespace CryptoNote
