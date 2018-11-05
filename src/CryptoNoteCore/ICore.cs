using System.Collections.Generic;
using System.Diagnostics;

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


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);
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


//#include <Serialization/ISerializer.h>

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

//Value must have public method IntrusiveLinkedList<Value>::hook& getHook()
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class Value>
public class IntrusiveLinkedList <Value>
{
  public class hook
  {
//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' class:
//	friend class IntrusiveLinkedList<Value>;

	public hook()
	{
		this.prev = null;
		this.next = null;
		this.used = false;
	}
	private Value prev;
	private Value next;
	private bool used;
  }

  public class iterator : std::iterator<std::bidirectional_iterator_tag, Value>
  {
	public iterator(Value value)
	{
		this.currentElement = value;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator !=(const iterator& other) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool operator !=(iterator other);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator ==(const iterator& other) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool operator ==(iterator other);
	public static IntrusiveLinkedList<Value>.iterator operator ++(iterator ImpliedObject)
	{
	  Debug.Assert(ImpliedObject.currentElement != null);
	  ImpliedObject.currentElement = ImpliedObject.currentElement.getHook().next;
	  return *ImpliedObject;
	}
	public static IntrusiveLinkedList<Value>.iterator operator ++(int UnnamedParameter)
	{
	  IntrusiveLinkedList<Value>.iterator copy = this;

	  Debug.Assert(currentElement != null);
	  currentElement = currentElement.getHook().next;
	  return copy;
	}
	public static IntrusiveLinkedList<Value>.iterator operator --(iterator ImpliedObject)
	{
	  Debug.Assert(ImpliedObject.currentElement != null);
	  ImpliedObject.currentElement = ImpliedObject.currentElement.getHook().prev;
	  return *ImpliedObject;
	}
	public static IntrusiveLinkedList<Value>.iterator operator --(int UnnamedParameter)
	{
	  IntrusiveLinkedList<Value>.iterator copy = this;

	  Debug.Assert(currentElement != null);
	  currentElement = currentElement.getHook().prev;
	  return copy;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Value& operator *() const
	public Value Indirection()
	{
	  Debug.Assert(currentElement != null);

	  return currentElement;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Value* operator ->() const
	public Value Dereference()
	{
	  return currentElement;
	}

	private Value currentElement;
  }

  public IntrusiveLinkedList()
  {
	  this.head = null;
	  this.tail = null;
  }

  public bool insert(Value value)
  {
	if (!value.getHook().used)
	{
	  if (head == null)
	  {
		head = value;
		tail = head;
		value.getHook().prev = null;
	  }
	  else
	  {
		tail.getHook().next = value;
		value.getHook().prev = tail;
		tail = value;
	  }

	  value.getHook().next = null;
	  value.getHook().used = true;
	  return true;
	}
	else
	{
	  return false;
	}
  }
  public bool remove(Value value)
  {
	if (value.getHook().used && head != null)
	{
	  Value toRemove = value;
	  Value current = head;
	  while (current.getHook().next != null)
	  {
		if (toRemove == current)
		{
		  break;
		}

		current = current.getHook().next;
	  }

	  if (toRemove == current)
	  {
		if (current.getHook().prev == null)
		{
		  Debug.Assert(current == head);
		  head = current.getHook().next;

		  if (head != null)
		  {
			head.getHook().prev = null;
		  }
		  else
		  {
			tail = null;
		  }
		}
		else
		{
		  current.getHook().prev.getHook().next = current.getHook().next;
		  if (current.getHook().next != null)
		  {
			current.getHook().next.getHook().prev = current.getHook().prev;
		  }
		  else
		  {
			tail = current.getHook().prev;
		  }
		}

		current.getHook().prev = null;
		current.getHook().next = null;
		current.getHook().used = false;
		return true;
	  }
	  else
	  {
		return false;
	  }
	}
	else
	{
	  return false;
	}
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool empty() const
  public bool empty()
  {
	return head == null;
  }

  public IntrusiveLinkedList<Value>.iterator begin()
  {
	return new iterator(head);
  }
  public IntrusiveLinkedList<Value>.iterator end()
  {
	return new iterator(null);
  }
  private Value head;
  private Value tail;
}

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class Value>

}



namespace CryptoNote
{

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <class MessageType>
public class MessageQueue <MessageType>
{
  public MessageQueue(System.Dispatcher & dispatch) : dispatcher(dispatch), event(dispatch), stopped(false)
  {
  }

  public MessageType front()
  {
	wait();
	return messageQueue.Peek();
  }
  public void pop()
  {
	wait();
	messageQueue.Dequeue();
  }
  public void push(MessageType message)
  {
	dispatcher.remoteSpawn([=] mutable
	{
	  messageQueue.Enqueue(std::move(message));
	  event.set();
	}
   );
  }

  public void stop()
  {
	stopped = true;
	event.set();
  }

//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' class:
//  friend class IntrusiveLinkedList<MessageQueue<MessageType>>;
  private IntrusiveLinkedList<MessageQueue<MessageType>>.hook getHook()
  {
	return hook;
  }
  private void wait()
  {
	if (messageQueue.Count == 0)
	{
	  if (stopped)
	  {
		throw new System.InterruptedException();
	  }

	  event.clear();
	  while (!event.get())
	  {
		event.wait();

		if (stopped)
		{
		  throw new System.InterruptedException();
		}
	  }
	}
  }

  private System.Dispatcher dispatcher;
  private Queue<MessageType> messageQueue = new Queue<MessageType>();
  private System.Event event;
  private bool stopped;

  private IntrusiveLinkedList<MessageQueue<MessageType>>.hook hook = new IntrusiveLinkedList<MessageQueue<MessageType>>.hook();
}

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <class MessageQueueContainer, class MessageType>
public class MesageQueueGuard <MessageQueueContainer, MessageType> : System.IDisposable
{
  public MesageQueueGuard(MessageQueueContainer container, MessageQueue<MessageType> messageQueue)
  {
	  this.container = container;
	  this.messageQueue = new CryptoNote.MessageQueue<MessageType>(messageQueue);
	container.addMessageQueue(messageQueue);
  }

  public void Dispose()
  {
	container.removeMessageQueue(messageQueue);
  }

  private MessageQueueContainer container;
  private MessageQueue<MessageType> messageQueue;
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

  public abstract bool addMessageQueue(MessageQueue<BlockchainMessage> messageQueue);
  public abstract bool removeMessageQueue(MessageQueue<BlockchainMessage> messageQueue);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getTopBlockIndex() const = 0;
  public abstract uint getTopBlockIndex();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual Crypto::Hash getTopBlockHash() const = 0;
  public abstract Crypto.Hash getTopBlockHash();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual Crypto::Hash getBlockHashByIndex(uint blockIndex) const = 0;
  public abstract Crypto.Hash getBlockHashByIndex(uint blockIndex);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getBlockTimestampByIndex(uint blockIndex) const = 0;
  public abstract ulong getBlockTimestampByIndex(uint blockIndex);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool hasBlock(const Crypto::Hash& blockHash) const = 0;
  public abstract bool hasBlock(Crypto.Hash blockHash);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual BlockTemplate getBlockByIndex(uint index) const = 0;
  public abstract BlockTemplate getBlockByIndex(uint index);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual BlockTemplate getBlockByHash(const Crypto::Hash& blockHash) const = 0;
  public abstract BlockTemplate getBlockByHash(Crypto.Hash blockHash);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> buildSparseChain() const = 0;
  public abstract List<Crypto.Hash> buildSparseChain();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> findBlockchainSupplement(const ClassicVector<Crypto::Hash>& remoteBlockIds, uint maxCount, uint& totalBlockCount, uint& startBlockIndex) const = 0;
  public abstract List<Crypto.Hash> findBlockchainSupplement(List<Crypto.Hash> remoteBlockIds, uint maxCount, ref uint totalBlockCount, ref uint startBlockIndex);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<RawBlock> getBlocks(uint startIndex, uint count) const = 0;
  public abstract List<RawBlock> getBlocks(uint startIndex, uint count);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual void getBlocks(const ClassicVector<Crypto::Hash>& blockHashes, ClassicVector<RawBlock>& blocks, ClassicVector<Crypto::Hash>& missedHashes) const = 0;
  public abstract void getBlocks(List<Crypto.Hash> blockHashes, List<RawBlock> blocks, List<Crypto.Hash> missedHashes);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool queryBlocks(const ClassicVector<Crypto::Hash>& blockHashes, ulong timestamp, uint& startIndex, uint& currentIndex, uint& fullOffset, ClassicVector<BlockFullInfo>& entries) const = 0;
  public abstract bool queryBlocks(List<Crypto.Hash> blockHashes, ulong timestamp, ref uint startIndex, ref uint currentIndex, ref uint fullOffset, List<BlockFullInfo> entries);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool queryBlocksLite(const ClassicVector<Crypto::Hash>& knownBlockHashes, ulong timestamp, uint& startIndex, uint& currentIndex, uint& fullOffset, ClassicVector<BlockShortInfo>& entries) const = 0;
  public abstract bool queryBlocksLite(List<Crypto.Hash> knownBlockHashes, ulong timestamp, ref uint startIndex, ref uint currentIndex, ref uint fullOffset, List<BlockShortInfo> entries);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool hasTransaction(const Crypto::Hash& transactionHash) const = 0;
  public abstract bool hasTransaction(Crypto.Hash transactionHash);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual void getTransactions(const ClassicVector<Crypto::Hash>& transactionHashes, ClassicVector<BinaryArray>& transactions, ClassicVector<Crypto::Hash>& missedHashes) const = 0;
  public abstract void getTransactions(List<Crypto.Hash> transactionHashes, List<BinaryArray> transactions, List<Crypto.Hash> missedHashes);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getBlockDifficulty(uint blockIndex) const = 0;
  public abstract ulong getBlockDifficulty(uint blockIndex);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getDifficultyForNextBlock() const = 0;
  public abstract ulong getDifficultyForNextBlock();

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public abstract std::error_code addBlock(CachedBlock cachedBlock, RawBlock && rawBlock);
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public abstract std::error_code addBlock(RawBlock && rawBlock);

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public abstract std::error_code submitBlock(BinaryArray && rawBlockTemplate);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getTransactionGlobalIndexes(const Crypto::Hash& transactionHash, ClassicVector<uint>& globalIndexes) const = 0;
  public abstract bool getTransactionGlobalIndexes(Crypto.Hash transactionHash, List<uint> globalIndexes);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getRandomOutputs(ulong amount, ushort count, ClassicVector<uint>& globalIndexes, ClassicVector<Crypto::PublicKey>& publicKeys) const = 0;
  public abstract bool getRandomOutputs(ulong amount, ushort count, List<uint> globalIndexes, List<Crypto.PublicKey> publicKeys);

  public abstract bool addTransactionToPool(BinaryArray transactionBinaryArray);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getPoolTransactionHashes() const = 0;
  public abstract List<Crypto.Hash> getPoolTransactionHashes();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getPoolChanges(const Crypto::Hash& lastBlockHash, const ClassicVector<Crypto::Hash>& knownHashes, ClassicVector<BinaryArray>& addedTransactions, ClassicVector<Crypto::Hash>& deletedTransactions) const = 0;
  public abstract bool getPoolChanges(Crypto.Hash lastBlockHash, List<Crypto.Hash> knownHashes, List<BinaryArray> addedTransactions, List<Crypto.Hash> deletedTransactions);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getPoolChangesLite(const Crypto::Hash& lastBlockHash, const ClassicVector<Crypto::Hash>& knownHashes, ClassicVector<TransactionPrefixInfo>& addedTransactions, ClassicVector<Crypto::Hash>& deletedTransactions) const = 0;
  public abstract bool getPoolChangesLite(Crypto.Hash lastBlockHash, List<Crypto.Hash> knownHashes, List<TransactionPrefixInfo> addedTransactions, List<Crypto.Hash> deletedTransactions);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getBlockTemplate(BlockTemplate& b, const AccountPublicAddress& adr, const BinaryArray& extraNonce, ulong& difficulty, uint& height) const = 0;
  public abstract bool getBlockTemplate(BlockTemplate b, AccountPublicAddress adr, BinaryArray extraNonce, ref ulong difficulty, ref uint height);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual CoreStatistics getCoreStatistics() const = 0;
  public abstract CoreStatistics getCoreStatistics();

  public abstract void save();
  public abstract void load();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual BlockDetails getBlockDetails(const Crypto::Hash& blockHash) const = 0;
  public abstract BlockDetails getBlockDetails(Crypto.Hash blockHash);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual TransactionDetails getTransactionDetails(const Crypto::Hash& transactionHash) const = 0;
  public abstract TransactionDetails getTransactionDetails(Crypto.Hash transactionHash);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getAlternativeBlockHashesByIndex(uint blockIndex) const = 0;
  public abstract List<Crypto.Hash> getAlternativeBlockHashesByIndex(uint blockIndex);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getBlockHashesByTimestamps(ulong timestampBegin, uint secondsCount) const = 0;
  public abstract List<Crypto.Hash> getBlockHashesByTimestamps(ulong timestampBegin, uint secondsCount);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getTransactionHashesByPaymentId(const Crypto::Hash& paymentId) const = 0;
  public abstract List<Crypto.Hash> getTransactionHashesByPaymentId(Crypto.Hash paymentId);
}
}
