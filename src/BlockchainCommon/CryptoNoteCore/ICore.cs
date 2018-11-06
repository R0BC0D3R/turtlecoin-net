// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using BlockchainCommon.Common.CryptoNote;
using CryptoNote;
using CryptoNote.error;
using System.Collections.Generic;
using System.Diagnostics;

namespace CryptoNote
{
    public class ICoreObserver : System.IDisposable
    {
        public virtual void Dispose()
        {
        }
        public virtual void blockchainUpdated()
        {
        }
        public virtual void poolUpdated()
        {
        }
    }
}


namespace CryptoNote
{

    public class BlockFullInfo : RawBlock
    {
        public Crypto.Hash block_id = new Crypto.Hash();
    }

    public class TransactionPrefixInfo
    {
        public Crypto.Hash txHash = new Crypto.Hash();
        public TransactionPrefix txPrefix = new TransactionPrefix();
    }

    public class BlockShortInfo
    {
        public Crypto.Hash blockId = new Crypto.Hash();
        public BinaryArray block = new BinaryArray();
        public List<TransactionPrefixInfo> txPrefixes = new List<TransactionPrefixInfo>();
    }
}


namespace CryptoNote
{
    //public class IntrusiveLinkedList<Value>
    //{
    //    private Value head;
    //    private Value tail;

    //    public class Hook
    //    {
    //        private Value prev;
    //        private Value next;
    //        private bool used;

    //        public Hook()
    //        {
    //            //TODO: prev and next were originally converted to null
    //            prev = default(Value);
    //            next = default(Value);
    //            used = false;
    //        }

    //    }

    //    public class iterator : std::iterator<std::bidirectional_iterator_tag, Value>
    //    {
    //        public iterator(Value value)
    //        {
    //            this.currentElement = value;
    //        }

    //        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
    //        //ORIGINAL LINE: bool operator !=(const iterator& other) const;
    //        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //        //	bool operator !=(iterator other);
    //        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
    //        //ORIGINAL LINE: bool operator ==(const iterator& other) const;
    //        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //        //	bool operator ==(iterator other);
    //        public static IntrusiveLinkedList<Value>.iterator operator ++(iterator ImpliedObject)
    //        {
    //            Debug.Assert(ImpliedObject.currentElement != null);
    //            ImpliedObject.currentElement = ImpliedObject.currentElement.getHook().next;
    //            return *ImpliedObject;
    //        }
    //        public static IntrusiveLinkedList<Value>.iterator operator ++(int UnnamedParameter)
    //        {
    //            IntrusiveLinkedList<Value>.iterator copy = this;

    //            Debug.Assert(currentElement != null);
    //            currentElement = currentElement.getHook().next;
    //            return copy;
    //        }
    //        public static IntrusiveLinkedList<Value>.iterator operator --(iterator ImpliedObject)
    //        {
    //            Debug.Assert(ImpliedObject.currentElement != null);
    //            ImpliedObject.currentElement = ImpliedObject.currentElement.getHook().prev;
    //            return *ImpliedObject;
    //        }
    //        public static IntrusiveLinkedList<Value>.iterator operator --(int UnnamedParameter)
    //        {
    //            IntrusiveLinkedList<Value>.iterator copy = this;

    //            Debug.Assert(currentElement != null);
    //            currentElement = currentElement.getHook().prev;
    //            return copy;
    //        }

    //        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
    //        //ORIGINAL LINE: Value& operator *() const
    //        public Value Indirection()
    //        {
    //            Debug.Assert(currentElement != null);

    //            return currentElement;
    //        }
    //        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
    //        //ORIGINAL LINE: Value* operator ->() const
    //        public Value Dereference()
    //        {
    //            return currentElement;
    //        }

    //        private Value currentElement;
    //    }

    //    public IntrusiveLinkedList()
    //    {
    //        // TODO: This was originally null
    //        this.head = default(Value);
    //        this.tail = default(Value);
    //    }

    //    public bool insert(Value value)
    //    {
    //        if (!value.getHook().used)
    //        {
    //            if (head == null)
    //            {
    //                head = value;
    //                tail = head;
    //                value.getHook().prev = null;
    //            }
    //            else
    //            {
    //                tail.getHook().next = value;
    //                value.getHook().prev = tail;
    //                tail = value;
    //            }

    //            value.getHook().next = null;
    //            value.getHook().used = true;
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    public bool remove(Value value)
    //    {
    //        if (value.getHook().used && head != null)
    //        {
    //            Value toRemove = value;
    //            Value current = head;
    //            while (current.getHook().next != null)
    //            {
    //                if (toRemove == current)
    //                {
    //                    break;
    //                }

    //                current = current.getHook().next;
    //            }

    //            if (toRemove == current)
    //            {
    //                if (current.getHook().prev == null)
    //                {
    //                    Debug.Assert(current == head);
    //                    head = current.getHook().next;

    //                    if (head != null)
    //                    {
    //                        head.getHook().prev = null;
    //                    }
    //                    else
    //                    {
    //                        tail = null;
    //                    }
    //                }
    //                else
    //                {
    //                    current.getHook().prev.getHook().next = current.getHook().next;
    //                    if (current.getHook().next != null)
    //                    {
    //                        current.getHook().next.getHook().prev = current.getHook().prev;
    //                    }
    //                    else
    //                    {
    //                        tail = current.getHook().prev;
    //                    }
    //                }

    //                current.getHook().prev = null;
    //                current.getHook().next = null;
    //                current.getHook().used = false;
    //                return true;
    //            }
    //            else
    //            {
    //                return false;
    //            }
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
    //    //ORIGINAL LINE: bool empty() const
    //    //public bool empty()
    //    //{
    //    //    return head == null;
    //    //}

    //    //public IntrusiveLinkedList<Value>.iterator begin()
    //    //{
    //    //    return new iterator(head);
    //    //}
    //    //public IntrusiveLinkedList<Value>.iterator end()
    //    //{
    //    //    return new iterator(null);
    //    //}

    //}

    //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    //ORIGINAL LINE: template<class Value>

}



namespace CryptoNote
{
    public class MessageQueue<MessageType>
    {
        public Queue<MessageType> messageQueue;

        //public MessageQueue<MessageType>()
        //{

        //}

        public MessageType Front()
        {
            //wait();
            return messageQueue.Peek();
        }
        public void Pop()
        {
            //wait();
            messageQueue.Dequeue();
        }
        public void Push(MessageType message)
        {
            messageQueue.Enqueue(message);
        }
     }

//public void stop()
//{
//    stopped = true;
//    event.set();
//}

    //C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' class:
    //  friend class IntrusiveLinkedList<MessageQueue<MessageType>>;
    // private IntrusiveLinkedList<MessageQueue<MessageType>>.hook getHook()
    // {
    //return hook;
    // }
    // private void wait()
    // {
    //if (messageQueue.Count == 0)
    //{
    //  if (stopped)
    //  {
    //	throw new System.InterruptedException();
    //  }

    //  event.clear();
    //  while (!event.get())
    //  {
    //	event.wait();

    //	if (stopped)
    //	{
    //	  throw new System.InterruptedException();
    //	}
    //  }
    //}
    // }

    //private System.Dispatcher dispatcher;
    //private Queue<MessageType> messageQueue = new Queue<MessageType>();
    //private System.Event event;
    //private bool stopped;

    //private IntrusiveLinkedList<MessageQueue<MessageType>>.hook hook = new IntrusiveLinkedList<MessageQueue<MessageType>>.hook();
}

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <class MessageQueueContainer, class MessageType>
public class MesageQueueGuard<MessageQueueContainer, MessageType> : System.IDisposable
{
    private MessageQueueContainer container;
    private MessageQueue<MessageType> messageQueue;

    public MesageQueueGuard(MessageQueueContainer container, MessageQueue<MessageType> messageQueue)
    {
        this.container = container;

        //TODO: Convert to something
        //this.messageQueue = new CryptoNote.MessageQueue<MessageType>(messageQueue);
        //container.addMessageQueue(messageQueue);
    }

    public void Dispose()
    {
        //TODO: What needs to be diplosed?
        //container.removeMessageQueue(messageQueue);
    }
}


namespace CryptoNote
{

    public enum CoreEvent
    {
        POOL_UPDATED,
        BLOCKHAIN_UPDATED
    }

    public abstract class ICore : System.IDisposable
    {
        public virtual void Dispose()
        {
        }

        public abstract bool AddMessageQueue(MessageQueue<BlockchainMessage> messageQueue);
        public abstract bool RemoveMessageQueue(MessageQueue<BlockchainMessage> messageQueue);


        public abstract uint GetTopBlockIndex();
        public abstract Crypto.Hash GetTopBlockHash();
        public abstract Crypto.Hash GetBlockHashByIndex(uint blockIndex);
        public abstract ulong GetBlockTimestampByIndex(uint blockIndex);


        public abstract bool HasBlock(Crypto.Hash blockHash);
        public abstract BlockTemplate GetBlockByIndex(uint index);
        public abstract BlockTemplate GetBlockByHash(Crypto.Hash blockHash);


        public abstract List<Crypto.Hash> BuildSparseChain();
        public abstract List<Crypto.Hash> FindBlockchainSupplement(List<Crypto.Hash> remoteBlockIds, uint maxCount, ref uint totalBlockCount, ref uint startBlockIndex);


        public abstract List<RawBlock> GetBlocks(uint startIndex, uint count);
        public abstract void GetBlocks(List<Crypto.Hash> blockHashes, List<RawBlock> blocks, List<Crypto.Hash> missedHashes);
        public abstract bool QueryBlocks(List<Crypto.Hash> blockHashes, ulong timestamp, ref uint startIndex, ref uint currentIndex, ref uint fullOffset, List<BlockFullInfo> entries);
        public abstract bool QueryBlocksLite(List<Crypto.Hash> knownBlockHashes, ulong timestamp, ref uint startIndex, ref uint currentIndex, ref uint fullOffset, List<BlockShortInfo> entries);


        public abstract bool HasTransaction(Crypto.Hash transactionHash);
        public abstract void GetTransactions(List<Crypto.Hash> transactionHashes, List<BinaryArray> transactions, List<Crypto.Hash> missedHashes);

        public abstract ulong GetBlockDifficulty(uint blockIndex);
        public abstract ulong GetDifficultyForNextBlock();


        public abstract AddBlockErrorCode AddBlock(CachedBlock cachedBlock, RawBlock rawBlock);
        public abstract AddBlockErrorCode AddBlock(RawBlock rawBlock);


        public abstract AddBlockErrorCode SubmitBlock(BinaryArray rawBlockTemplate);


        public abstract bool GetTransactionGlobalIndexes(Crypto.Hash transactionHash, List<uint> globalIndexes);

        public abstract bool GetRandomOutputs(ulong amount, ushort count, List<uint> globalIndexes, List<Crypto.PublicKey> publicKeys);

        public abstract bool AddTransactionToPool(BinaryArray transactionBinaryArray);
        public abstract List<Crypto.Hash> GetPoolTransactionHashes();
        public abstract bool GetPoolChanges(Crypto.Hash lastBlockHash, List<Crypto.Hash> knownHashes, List<BinaryArray> addedTransactions, List<Crypto.Hash> deletedTransactions);
        public abstract bool GetPoolChangesLite(Crypto.Hash lastBlockHash, List<Crypto.Hash> knownHashes, List<TransactionPrefixInfo> addedTransactions, List<Crypto.Hash> deletedTransactions);

        public abstract bool GetBlockTemplate(BlockTemplate b, AccountPublicAddress adr, BinaryArray extraNonce, ref ulong difficulty, ref uint height);

        public abstract CoreStatistics GetCoreStatistics();

        public abstract void Save();
        public abstract void Load();

        public abstract BlockDetails GetBlockDetails(Crypto.Hash blockHash);
        public abstract TransactionDetails GetTransactionDetails(Crypto.Hash transactionHash);
        public abstract List<Crypto.Hash> GetAlternativeBlockHashesByIndex(uint blockIndex);
        public abstract List<Crypto.Hash> GetBlockHashesByTimestamps(ulong timestampBegin, uint secondsCount);
        public abstract List<Crypto.Hash> GetTransactionHashesByPaymentId(Crypto.Hash paymentId);
    }
}