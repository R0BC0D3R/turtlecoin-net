// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

namespace CryptoNote
{

public abstract class ICoreInformation : System.IDisposable
{
  public virtual void Dispose()
  {
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getPoolTransactionCount() const = 0;
  public abstract uint getPoolTransactionCount();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getBlockchainTransactionCount() const = 0;
  public abstract uint getBlockchainTransactionCount();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getAlternativeBlockCount() const = 0;
  public abstract uint getAlternativeBlockCount();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getTotalGeneratedAmount() const = 0;
  public abstract ulong getTotalGeneratedAmount();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<BlockTemplate> getAlternativeBlocks() const = 0;
  public abstract List<BlockTemplate> getAlternativeBlocks();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Transaction> getPoolTransactions() const = 0;
  public abstract List<Transaction> getPoolTransactions();
}

}
