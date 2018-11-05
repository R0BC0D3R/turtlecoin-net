// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

namespace CryptoNote
{

public class TransactionValidatorState
{
  public HashSet<Crypto.KeyImage> spentKeyImages = new HashSet<Crypto.KeyImage>();
}

}


