// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

namespace CryptoNote
{
    interface ICoreInformation : System.IDisposable
    {
        uint GetPoolTransactionCount();
        uint GetBlockchainTransactionCount();
        uint GetAlternativeBlockCount();
        ulong GetTotalGeneratedAmount();       
        List<BlockTemplate> GetAlternativeBlocks();
        List<Transaction> GetPoolTransactions();
    }
}