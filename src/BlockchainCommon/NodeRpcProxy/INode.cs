using CryptoNote;
using System;
using System.Collections.Generic;
using BlockchainCommon.Common.CryptoNote;

namespace BlockchainCommon.NodeRpcProxy
{
    public interface INode : IDisposable
    {
        //TODO: Removed Callback param from a lot of those. Not sure yet what they do but if it's method to be called when done, there are other ways to do that


        //public delegate void Callback(std::error_code UnnamedParameter);

        //public virtual void Dispose() { }
        bool AddObserver(INodeObserver observer);
        bool RemoveObserver(INodeObserver observer);

        //precondition: must be called in dispatcher's thread
        void Init(Action<string> callback);
        //precondition: must be called in dispatcher's thread
        bool Shutdown();

        //precondition: all of following methods must not be invoked in dispatcher's thread
        uint GetPeerCount();
        uint GetLastLocalBlockHeight();        
        uint GetLastKnownBlockHeight();
        uint GetLocalBlockCount();
        uint GetKnownBlockCount();        
        ulong GetLastLocalBlockTimestamp();        
        ulong GetNodeHeight();

        string GetInfo();
        void GetFeeInfo();

        void GetBlockHashesByTimestamps(ulong timestampBegin, uint secondsCount, List<Crypto.Hash> blockHashes);
        void GetTransactionHashesByPaymentId(Crypto.Hash paymentId, List<Crypto.Hash> transactionHashes);

        BlockHeaderInfo GetLastLocalBlockHeaderInfo();

        void RelayTransaction(Transaction transaction);
        void GetRandomOutsByAmounts(List<ulong> amounts, ushort outsCount, List<CryptoNote.COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount> result);
        void GetNewBlocks(List<Crypto.Hash> knownBlockIds, List<RawBlock> newBlocks, ref uint startHeight);
        void GetTransactionOutsGlobalIndices(Crypto.Hash transactionHash, List<uint> outsGlobalIndices);
        void QueryBlocks(List<Crypto.Hash> knownBlockIds, ulong timestamp, List<BlockShortEntry> newBlocks, ref uint startHeight);
        void GetPoolSymmetricDifference(List<Crypto.Hash> knownPoolTxIds, Crypto.Hash knownBlockId, ref bool isBcActual, List<HashSet<ITransactionReader>> newTxs, List<Crypto.Hash> deletedTxIds);

        void GetBlocks(List<uint> blockHeights, List<List<BlockDetails>> blocks);
        void GetBlocks(List<Crypto.Hash> blockHashes, List<BlockDetails> blocks);
        void GetBlock(uint blockHeight, BlockDetails block);
        void GetTransactions(List<Crypto.Hash> transactionHashes, List<TransactionDetails> transactions);
        void IsSynchronized(ref bool syncStatus);
        string FeeAddress();
        uint FeeAmount();
    }
}