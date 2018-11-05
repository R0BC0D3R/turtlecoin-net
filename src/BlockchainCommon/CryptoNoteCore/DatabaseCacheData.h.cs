// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

namespace CryptoNote
{

public class KeyOutputInfo
{
  public Crypto.PublicKey publicKey = new Crypto.PublicKey();
  public Crypto.Hash transactionHash = new Crypto.Hash();
  public ulong unlockTime;
  public ushort outputIndex;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void serialize(CryptoNote::ISerializer s);
}

// inherit here to avoid breaking IBlockchainCache interface
public class ExtendedTransactionInfo : CachedTransactionInfo
{
  //CachedTransactionInfo tx;
  public SortedDictionary<IBlockchainCache.Amount, List<IBlockchainCache.GlobalOutputIndex>> amountToKeyIndexes = new SortedDictionary<IBlockchainCache.Amount, List<IBlockchainCache.GlobalOutputIndex>>(); //global key output indexes spawned in this transaction
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void serialize(ISerializer s);
}

}
