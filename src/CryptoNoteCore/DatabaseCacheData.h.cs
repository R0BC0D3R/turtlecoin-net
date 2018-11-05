using System.Collections.Generic;

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// This file is part of Bytecoin.
//
// Bytecoin is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Bytecoin is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.



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
