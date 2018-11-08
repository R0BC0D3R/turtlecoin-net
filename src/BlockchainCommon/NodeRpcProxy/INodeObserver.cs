using System;
using System.Collections.Generic;

namespace BlockchainCommon.NodeRpcProxy
{
    public interface INodeObserver : IDisposable
    {
        //public virtual void Dispose();
        void PeerCountUpdated(uint count);
        void LocalBlockchainUpdated(uint height);
        void LastKnownBlockHeightUpdated(uint height);
        void PoolChanged();
        void BlockchainSynchronized(uint topHeight);
        void ChainSwitched(uint newTopIndex, uint commonRoot, List<Crypto.Hash> hashes);        
    }
}