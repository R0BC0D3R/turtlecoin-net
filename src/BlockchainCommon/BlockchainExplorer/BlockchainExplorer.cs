// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using Logging;
using Crypto;
using System;
using System.Collections.Generic;
using System.Diagnostics;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CRYPTO_MAKE_COMPARABLE(type) namespace Crypto { inline bool operator==(const type &_v1, const type &_v2) { return std::memcmp(&_v1, &_v2, sizeof(type)) == 0; } inline bool operator!=(const type &_v1, const type &_v2) { return std::memcmp(&_v1, &_v2, sizeof(type)) != 0; } }
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CRYPTO_MAKE_HASHABLE(type) CRYPTO_MAKE_COMPARABLE(type) namespace Crypto { static_assert(sizeof(size_t) <= sizeof(type), "Size of " #type " must be at least that of size_t"); inline size_t hash_value(const type &_v) { return reinterpret_cast<const size_t &>(_v); } } namespace std { template<> struct hash<Crypto::type> { size_t operator()(const Crypto::type &_v) const { return reinterpret_cast<const size_t &>(_v); } }; }
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_ITER (CN_SOFT_SHELL_MEMORY / 2)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_PAD_MULTIPLIER (CN_SOFT_SHELL_WINDOW / CN_SOFT_SHELL_MULTIPLIER)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_ITER_MULTIPLIER (CN_SOFT_SHELL_PAD_MULTIPLIER / 2)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl

namespace CryptoNote
{

public enum State
{
  NOT_INITIALIZED,
  INITIALIZED
}

//C++ TO C# CONVERTER TODO TASK: Multiple inheritance is not available in C#:
public class BlockchainExplorer : IBlockchainExplorer, INodeObserver
{
  public BlockchainExplorer(INode node, Logging.ILogger logger)
  {
	  this.node = new CryptoNote.INode(node);
	  this.logger = new Logging.LoggerRef(logger, "BlockchainExplorer");
	  this.state = State.NOT_INITIALIZED;
	  this.synchronized = false;
	  this.observersCounter = 0;
  }

//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  BlockchainExplorer(const BlockchainExplorer&) = delete;
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  BlockchainExplorer(BlockchainExplorer&&) = delete;

//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  BlockchainExplorer& operator =(const BlockchainExplorer&) = delete;
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  BlockchainExplorer& operator =(BlockchainExplorer&&) = delete;

  public override void Dispose()
  {
	  base.Dispose();
  }

  public override bool addObserver(IBlockchainObserver observer)
  {
	if (state.load() != State.INITIALIZED)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.NOT_INITIALIZED));
	}
	observersCounter.fetch_add(1);
	return observerManager.add(observer);
  }
  public override bool removeObserver(IBlockchainObserver observer)
  {
	if (state.load() != State.INITIALIZED)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.NOT_INITIALIZED));
	}
	if (observersCounter.load() != 0)
	{
	  observersCounter.fetch_sub(1);
	}
	return observerManager.remove(observer);
  }

  public override bool getBlocks(List<uint> blockIndexes, List<List<BlockDetails>> blocks)
  {
	return getBlocks(blockIndexes, blocks, true);
  }
  public override bool getBlocks(List<Hash> blockHashes, List<BlockDetails> blocks)
  {
	if (state.load() != State.INITIALIZED)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.NOT_INITIALIZED));
	}

	if (blockHashes.Count == 0)
	{
	  return true;
	}

	logger.functorMethod(DEBUGGING) << "Get blocks by hash request came.";
	NodeRequest new request((INode.Callback cb) =>
	{
		node.getBlocks(blockHashes, blocks, cb);
	});
	std::error_code ec = request.performBlocking();
	if (ec != null)
	{
	  logger.functorMethod(ERROR) << "Can't get blocks by hash: " << ec.message();
	  throw std::system_error(ec);
	}

	Debug.Assert(blocks.Count == blockHashes.Count);
	return true;
  }
  public override bool getBlocks(ulong timestampBegin, ulong timestampEnd, uint blocksNumberLimit, List<BlockDetails> blocks, ref uint blocksNumberWithinTimestamps)
  {
	if (state.load() != State.INITIALIZED)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.NOT_INITIALIZED));
	}

	if (timestampBegin > timestampEnd)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.REQUEST_ERROR), "timestampBegin must not be greater than timestampEnd");
	}

	logger.functorMethod(DEBUGGING) << "Get blocks by timestamp " << timestampBegin << " - " << timestampEnd << " request came.";

	List<Hash> blockHashes = new List<Hash>();
	NodeRequest new request((INode.Callback cb) =>
	{
		node.getBlockHashesByTimestamps(new ulong(timestampBegin), timestampEnd - timestampBegin + 1, blockHashes, cb);
	});
	var ec = request.performBlocking();
	if (ec != null)
	{
	  logger.functorMethod(ERROR) << "Can't get blocks hashes by timestamps: " << ec.message();
	  throw std::system_error(ec);
	}

	blocksNumberWithinTimestamps = (uint)blockHashes.Count;

	if (blocksNumberLimit < blocksNumberWithinTimestamps)
	{
//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'erase' method in C#:
	  blockHashes.erase(std::next(blockHashes.GetEnumerator(), blocksNumberLimit), blockHashes.end());
	}

	if (blockHashes.Count == 0)
	{
	  throw new System.Exception("block hashes not found");
	}

	return getBlocks(blockHashes, blocks);
  }

  public override bool getBlockchainTop(BlockDetails topBlock)
  {
	return getBlockchainTop(ref topBlock, true);
  }

  public override bool getTransactions(List<Hash> transactionHashes, List<TransactionDetails> transactions)
  {
	if (state.load() != State.INITIALIZED)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.NOT_INITIALIZED));
	}

	if (transactionHashes.Count == 0)
	{
	  return true;
	}

	logger.functorMethod(DEBUGGING) << "Get transactions by hash request came.";
	NodeRequest new request((INode.Callback cb) =>
	{
		return node.getTransactions(transactionHashes, transactions, cb);
	});
	std::error_code ec = request.performBlocking();
	if (ec != null)
	{
	  logger.functorMethod(ERROR) << "Can't get transactions by hash: " << ec.message();
	  throw std::system_error(ec);
	}
	return true;
  }
  public override bool getTransactionsByPaymentId(Hash paymentId, List<TransactionDetails> transactions)
  {
	if (state.load() != State.INITIALIZED)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.NOT_INITIALIZED));
	}

	logger.functorMethod(DEBUGGING) << "Get transactions by payment id " << paymentId << " request came.";

	List<Crypto.Hash> transactionHashes = new List<Crypto.Hash>();
	NodeRequest new request((INode.Callback cb) =>
	{
		return node.getTransactionHashesByPaymentId(paymentId, transactionHashes, cb);
	});

	var ec = request.performBlocking();
	if (ec != null)
	{
	  logger.functorMethod(ERROR) << "Can't get transaction hashes: " << ec.message();
	  throw std::system_error(ec);
	}

	if (transactionHashes.Count == 0)
	{
	  return false;
	}

	return getTransactions(transactionHashes, transactions);
  }
  public override bool getPoolState(List<Hash> knownPoolTransactionHashes, Hash knownBlockchainTopHash, ref bool isBlockchainActual, List<TransactionDetails> newTransactions, List<Hash> removedTransactions)
  {
	if (state.load() != State.INITIALIZED)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.NOT_INITIALIZED));
	}

	logger.functorMethod(DEBUGGING) << "Get pool state request came.";
	List<std::unique_ptr<ITransactionReader>> rawNewTransactions = new List<std::unique_ptr<ITransactionReader>>();

	NodeRequest new request((INode.Callback callback) =>
	{
		List<Hash> hashes = new List<Hash>();
		foreach (Hash hash in knownPoolTransactionHashes)
		{
		  hashes.Add(std::move(hash));
		}

		node.getPoolSymmetricDifference(std::move(hashes), reinterpret_cast<Hash&>(knownBlockchainTopHash), ref isBlockchainActual, rawNewTransactions, removedTransactions, callback);
	});
	std::error_code ec = request.performBlocking();
	if (ec != null)
	{
	  logger.functorMethod(ERROR) << "Can't get pool state: " << ec.message();
	  throw std::system_error(ec);
	}

	List<Hash> newTransactionsHashes = new List<Hash>();
	foreach (var rawTransaction in rawNewTransactions)
	{
	  Hash transactionHash = rawTransaction.getTransactionHash();
	  newTransactionsHashes.Add(std::move(transactionHash));
	}

	return getTransactions(newTransactionsHashes, newTransactions);
  }

  public override ulong getRewardBlocksWindow()
  {
	if (state.load() != State.INITIALIZED)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.NOT_INITIALIZED));
	}
	return parameters.CRYPTONOTE_REWARD_BLOCKS_WINDOW;
  }
  public override ulong getFullRewardMaxBlockSize(ushort majorVersion)
  {
	if (state.load() != State.INITIALIZED)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.NOT_INITIALIZED));
	}

	if (majorVersion >= BLOCK_MAJOR_VERSION_3)
	{
	  return parameters.CRYPTONOTE_BLOCK_GRANTED_FULL_REWARD_ZONE;
	}
	else if (majorVersion == BLOCK_MAJOR_VERSION_2)
	{
	  return parameters.CRYPTONOTE_BLOCK_GRANTED_FULL_REWARD_ZONE_V2;
	}
	else
	{
	  return parameters.CRYPTONOTE_BLOCK_GRANTED_FULL_REWARD_ZONE_V1;
	}
  }

  public override bool isSynchronized()
  {
	if (state.load() != State.INITIALIZED)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.NOT_INITIALIZED));
	}

	logger.functorMethod(DEBUGGING) << "Synchronization status request came.";
	bool syncStatus = false;
	NodeRequest new request((INode.Callback cb) =>
	{
		node.isSynchronized(ref syncStatus, cb);
	});
	std::error_code ec = request.performBlocking();
	if (ec != null)
	{
	  logger.functorMethod(ERROR) << "Can't get synchronization status: " << ec.message();
	  throw std::system_error(ec);
	}

	synchronized.store(syncStatus);
	return syncStatus;
  }

  public override void init()
  {
	if (state.load() != State.NOT_INITIALIZED)
	{
	  logger.functorMethod(ERROR) << "Init called on already initialized BlockchainExplorer.";
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.ALREADY_INITIALIZED));
	}

	if (!getBlockchainTop(ref knownBlockchainTop, false))
	{
	  logger.functorMethod(ERROR) << "Can't get blockchain top.";
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.INTERNAL_ERROR));
	}

	List<Crypto.Hash> knownPoolTransactionHashes = new List<Crypto.Hash>();
	bool isBlockchainActual;
	List<TransactionDetails> newTransactions = new List<TransactionDetails>();
	List<Crypto.Hash> removedTransactions = new List<Crypto.Hash>();
	StateRollback stateRollback = new StateRollback(state);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: if (!getPoolState(knownPoolTransactionHashes, knownBlockchainTop.hash, isBlockchainActual, newTransactions, removedTransactions))
	if (!getPoolState(knownPoolTransactionHashes, new Crypto.Hash(knownBlockchainTop.hash), ref isBlockchainActual, newTransactions, removedTransactions))
	{
	  logger.functorMethod(ERROR) << "Can't get pool state.";
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.INTERNAL_ERROR));
	}

	Debug.Assert(removedTransactions.Count == 0);

	if (node.addObserver(this))
	{
	  stateRollback.commit();
	}
	else
	{
	  logger.functorMethod(ERROR) << "Can't add observer to node.";
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.INTERNAL_ERROR));
	}
  }
  public override void shutdown()
  {
	if (state.load() != State.INITIALIZED)
	{
	  logger.functorMethod(ERROR) << "Shutdown called on not initialized BlockchainExplorer.";
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.NOT_INITIALIZED));
	}

	node.removeObserver(this);
	asyncContextCounter.waitAsyncContextsFinish();
	state.store(State.NOT_INITIALIZED);
  }

  public override void poolChanged()
  {
	logger.functorMethod(DEBUGGING) << "Got poolChanged notification.";

	if (!synchronized.load() || observersCounter.load() == 0)
	{
	  return;
	}

	if (!poolUpdateGuard.beginUpdate())
	{
	  return;
	}

	ScopeExitHandler poolUpdateEndGuard = new ScopeExitHandler(std::bind(this.poolUpdateEndHandler, this));

	std::unique_lock<object> @lock = new std::unique_lock<object>(mutex);

	var rawNewTransactionsPtr = new List<std::unique_ptr<ITransactionReader>>();
	var removedTransactionsPtr = new List<Hash>();
	var isBlockchainActualPtr = new bool(false);

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: NodeRequest request([this, rawNewTransactionsPtr, removedTransactionsPtr, isBlockchainActualPtr](const INode::Callback& callback)
	NodeRequest new request((INode.Callback callback) =>
	{
		List<Hash> hashes = new List<Hash>();
		hashes.Capacity = knownPoolState.Count;
		foreach (Tuple<Hash, TransactionDetails> kv in knownPoolState)
		{
		  hashes.Add(kv.Item1);
		}
		node.getPoolSymmetricDifference(std::move(hashes), reinterpret_cast<Hash&>(knownBlockchainTop.hash), ref * isBlockchainActualPtr, *rawNewTransactionsPtr, *removedTransactionsPtr, callback);
	});

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: request.performAsync(asyncContextCounter, [this, rawNewTransactionsPtr, removedTransactionsPtr, isBlockchainActualPtr](std::error_code ec)
	request.performAsync(asyncContextCounter, (std::error_code ec) =>
	{
		ScopeExitHandler poolUpdateEndGuard = new ScopeExitHandler(std::bind(this.poolUpdateEndHandler, this));

		if (ec != null)
		{
		  logger.functorMethod(ERROR) << "Can't send poolChanged notification because can't get pool symmetric difference: " << ec.message();
		  return;
		}

		std::unique_lock<object> @lock = new std::unique_lock<object>(mutex);

		List<Hash> newTransactionsHashesPtr = new List<Hash>();
		newTransactionsHashesPtr.Capacity = rawNewTransactionsPtr.Count;
		foreach (var rawTransaction in * rawNewTransactionsPtr)
		{
		  var hash = rawTransaction.getTransactionHash();
		  logger.functorMethod(DEBUGGING) << "Pool responded with new transaction: " << hash;
		  if (knownPoolState.count(hash) == 0)
		  {
			newTransactionsHashesPtr.Add(hash);
		  }
		}

		var removedTransactionsHashesPtr = new List<Tuple<Hash, TransactionRemoveReason>>();
		removedTransactionsHashesPtr.Capacity = removedTransactionsPtr.Count;
		foreach (Hash hash in * removedTransactionsPtr)
		{
		  logger.functorMethod(DEBUGGING) << "Pool responded with deleted transaction: " << hash;
		  var iter = knownPoolState.find(hash);
		  if (iter != knownPoolState.end())
		  {
			removedTransactionsHashesPtr.Add({hash, TransactionRemoveReason.INCLUDED_IN_BLOCK});
		  }
		}

		List<TransactionDetails> newTransactionsPtr = new List<TransactionDetails>();
		newTransactionsPtr.Capacity = newTransactionsHashesPtr.Count;
		NodeRequest new request((INode.Callback cb) =>
		{
		  node.getTransactions(newTransactionsHashesPtr, newTransactionsPtr, cb);
		});

		request.performAsync(asyncContextCounter, (std::error_code ec) =>
		{
			ScopeExitHandler poolUpdateEndGuard = new ScopeExitHandler(std::bind(this.poolUpdateEndHandler, this));

			if (ec != null)
			{
			  logger.functorMethod(ERROR) << "Can't send poolChanged notification because can't get transactions: " << ec.message();
			  return;
			}

			{
			  std::unique_lock<object> @lock = new std::unique_lock<object>(mutex);
			  foreach (TransactionDetails tx in * newTransactionsPtr)
			  {
				if (knownPoolState.count(tx.hash) == 0)
				{
				  knownPoolState.Add(tx.hash, tx);
				}
			  }

			  foreach (Tuple <Crypto in :Hash, TransactionRemoveReason> kv : *removedTransactionsHashesPtr)
			  {
				var iter = knownPoolState.find(kv.first);
				if (iter != knownPoolState.end())
				{
				  knownPoolState.Remove(iter);
				}
			  }
			}

			if (newTransactionsPtr.Count > 0 || removedTransactionsHashesPtr.Count > 0)
			{
			  observerManager.notify(IBlockchainObserver.poolUpdated, newTransactionsPtr, *removedTransactionsHashesPtr);
			  logger.functorMethod(DEBUGGING) << "poolUpdated notification was successfully sent.";
			}
		});

		poolUpdateEndGuard.reset();
	});

	poolUpdateEndGuard.reset();
  }
  public override void blockchainSynchronized(uint topIndex)
  {
	logger.functorMethod(DEBUGGING) << "Got blockchainSynchronized notification.";

	synchronized.store(true);

	if (observersCounter.load() == 0)
	{
	  return;
	}

	BlockDetails topBlock = new BlockDetails();
	{
	  std::unique_lock<object> @lock = new std::unique_lock<object>(mutex);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: topBlock = knownBlockchainTop;
	  topBlock.CopyFrom(knownBlockchainTop);
	}

	if (topBlock.index == topIndex)
	{
	  observerManager.notify(IBlockchainObserver.blockchainSynchronized, topBlock);
	  return;
	}

	List<uint> blockIndexesPtr = new List<uint>();
	List<List<BlockDetails>> blocksPtr = new List<List<BlockDetails>>();

	blockIndexesPtr.Add(topIndex);

//C++ TO C# CONVERTER NOTE: Data member pointers are not available in C#:
//	NodeRequest request(std::bind(static_cast< void(INode::*)(const ClassicVector<uint>&, ClassicVector<ClassicVector<BlockDetails>>&, const INode::Callback&) >(&INode::getBlocks), std::@ref(node), std::cref(*blockIndexesPtr), std::@ref(*blocksPtr), std::placeholders::_1));

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: request.performAsync(asyncContextCounter, [this, blockIndexesPtr, blocksPtr](std::error_code ec)
//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
	request.performAsync(asyncContextCounter, (std::error_code ec) =>
	{
		if (ec != null)
		{
		  logger.functorMethod(ERROR) << "Can't send blockchainSynchronized notification because can't get blocks by height: " << ec.message();
		  return;
		}
		Debug.Assert(blocksPtr.Count == blockIndexesPtr.Count && blocksPtr.Count == 1);

		var mainchainBlockIter = std::find_if_not(blocksPtr[0].cbegin(), blocksPtr[0].cend(), (BlockDetails block) =>
		{
			return block.isAlternative;
		});
		Debug.Assert(mainchainBlockIter != blocksPtr[0].cend());

		observerManager.notify(IBlockchainObserver.blockchainSynchronized, *mainchainBlockIter);
		logger.functorMethod(DEBUGGING) << "blockchainSynchronized notification was successfully sent.";
	});
  }
  public override void localBlockchainUpdated(uint index)
  {
	logger.functorMethod(DEBUGGING) << "Got localBlockchainUpdated notification.";

	std::unique_lock<object> @lock = new std::unique_lock<object>(mutex);
	Debug.Assert(index >= knownBlockchainTop.index);
	if (index == knownBlockchainTop.index)
	{
	  return;
	}

	var blockIndexesPtr = new List<uint>();
	var blocksPtr = new List<List<BlockDetails>>();

	for (uint i = knownBlockchainTop.index + 1; i <= index; ++i)
	{
	  blockIndexesPtr.Add(i);
	}

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: NodeRequest request([=](const INode::Callback& cb)
	NodeRequest new request((INode.Callback cb) =>
	{
		node.getBlocks(*blockIndexesPtr, *blocksPtr, cb);
	});

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: request.performAsync(asyncContextCounter, [this, blockIndexesPtr, blocksPtr](std::error_code ec)
	request.performAsync(asyncContextCounter, (std::error_code ec) =>
	{
		if (ec != null)
		{
		  logger.functorMethod(ERROR) << "Can't send blockchainUpdated notification because can't get blocks by height: " << ec.message();
		  return;
		}
		Debug.Assert(blocksPtr.Count == blockIndexesPtr.Count);
		handleBlockchainUpdatedNotification(*blocksPtr);
	});
  }
  public override void chainSwitched(uint newTopIndex, uint commonRoot, List<Crypto.Hash> hashes)
  {
	Debug.Assert(newTopIndex > commonRoot);
	List<uint> blockIndexesPtr = new List<uint>();
	List<List<BlockDetails>> blocksPtr = new List<List<BlockDetails>>();
	blockIndexesPtr.Capacity = newTopIndex - commonRoot;
	blocksPtr.Capacity = newTopIndex - commonRoot;

	for (uint i = commonRoot + 1; i <= newTopIndex; ++i)
	{
	  blockIndexesPtr.Add(i);
	}

//C++ TO C# CONVERTER NOTE: Data member pointers are not available in C#:
//	NodeRequest request(std::bind(static_cast< void(INode::*)(const ClassicVector<uint>&, ClassicVector<ClassicVector<BlockDetails>>&, const INode::Callback&) >(&INode::getBlocks), std::@ref(node), std::cref(*blockIndexesPtr), std::@ref(*blocksPtr), std::placeholders::_1));

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: request.performAsync(asyncContextCounter, [this, blockIndexesPtr, blocksPtr](std::error_code ec)
	request.performAsync(asyncContextCounter, (std::error_code ec) =>
	{
		if (ec != null)
		{
		  logger.functorMethod(ERROR) << "Can't send blockchainUpdated notification because can't get blocks by height: " << ec.message();
		  return;
		}
		Debug.Assert(blocksPtr.Count == blockIndexesPtr.Count);
		handleBlockchainUpdatedNotification(blocksPtr);
	});
  }


  private void poolUpdateEndHandler()
  {
	if (poolUpdateGuard.endUpdate())
	{
	  poolChanged();
	}
  }

  private class PoolUpdateGuard
  {
	public PoolUpdateGuard()
	{
		this.m_state = State.NONE;
	}

	public bool beginUpdate()
	{
	  var state = m_state.load();
	  for (;;)
	  {
		switch (state)
		{
		case State.NONE:
		  if (m_state.compare_exchange_weak(state, State.UPDATING))
		  {
			return true;
		  }
		  break;

		case State.UPDATING:
		  if (m_state.compare_exchange_weak(state, State.UPDATE_REQUIRED))
		  {
			return false;
		  }
		  break;

		case State.UPDATE_REQUIRED:
		  return false;

		default:
		  Debug.Assert(false);
		  return false;
		}
	  }
	}
	public bool endUpdate()
	{
	  var state = m_state.load();
	  for (;;)
	  {
		Debug.Assert(state != State.NONE);

		if (m_state.compare_exchange_weak(state, State.NONE))
		{
		  return state == State.UPDATE_REQUIRED;
		}
	  }
	}

	private enum State
	{
	  NONE,
	  UPDATING,
	  UPDATE_REQUIRED
	}

	private std::atomic<State> m_state = new std::atomic<State>();
  }

  private bool getBlockchainTop(ref BlockDetails topBlock, bool checkInitialization)
  {
	if (checkInitialization && state.load() != State.INITIALIZED)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.NOT_INITIALIZED));
	}

	logger.functorMethod(DEBUGGING) << "Get blockchain top request came.";
	uint lastIndex = node.getLastLocalBlockHeight();

	List<uint> indexes = new List<uint>();
	indexes.Add(std::move(lastIndex));

	List<List<BlockDetails>> blocks = new List<List<BlockDetails>>();
	if (!getBlocks(indexes, blocks, checkInitialization))
	{
	  logger.functorMethod(ERROR) << "Can't get blockchain top.";
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.INTERNAL_ERROR));
	}
	Debug.Assert(blocks.Count == indexes.Count && blocks.Count == 1);

	bool gotMainchainBlock = false;
	foreach (BlockDetails block in blocks[blocks.Count - 1])
	{
	  if (!block.isAlternative)
	  {
		topBlock = block;
		gotMainchainBlock = true;
		break;
	  }
	}

	if (!gotMainchainBlock)
	{
	  logger.functorMethod(ERROR) << "Can't get blockchain top: all blocks on index " << lastIndex << " are orphaned.";
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.INTERNAL_ERROR));
	}
	return true;
  }
  private bool getBlocks(List<uint> blockIndexes, List<List<BlockDetails>> blocks, bool checkInitialization)
  {
	if (checkInitialization && state.load() != State.INITIALIZED)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes.NOT_INITIALIZED));
	}

	if (blockIndexes.Count == 0)
	{
	  return true;
	}

	logger.functorMethod(DEBUGGING) << "Get blocks by index request came.";
	NodeRequest new request((INode.Callback cb) =>
	{
		node.getBlocks(blockIndexes, blocks, cb);
	});
	std::error_code ec = request.performBlocking();
	if (ec != null)
	{
	  logger.functorMethod(ERROR) << "Can't get blocks by index: " << ec.message();
	  throw std::system_error(ec);
	}
	Debug.Assert(blocks.Count == blockIndexes.Count);
	return true;
  }

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void rebuildIndexes();
  private void handleBlockchainUpdatedNotification(List<List<BlockDetails>> blocks)
  {
	List<BlockDetails> newBlocks = new List<BlockDetails>();
	List<BlockDetails> alternativeBlocks = new List<BlockDetails>();
	{
	  std::unique_lock<object> @lock = new std::unique_lock<object>(mutex);

	  BlockDetails topMainchainBlock = new BlockDetails();
	  bool gotTopMainchainBlock = false;
	  ulong topHeight = 0;

	  foreach (List<BlockDetails> sameHeightBlocks in blocks)
	  {
		foreach (BlockDetails block in sameHeightBlocks)
		{
		  if (topHeight < block.index)
		  {
			topHeight = block.index;
			gotTopMainchainBlock = false;
		  }

		  if (block.isAlternative)
		  {
			alternativeBlocks.Add(block);
		  }
		  else
		  {
			//assert(block.hash != knownBlockchainTop.hash);
			newBlocks.Add(block);
			if (!gotTopMainchainBlock)
			{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: topMainchainBlock = block;
			  topMainchainBlock.CopyFrom(block);
			  gotTopMainchainBlock = true;
			}
		  }
		}
	  }

	  Debug.Assert(gotTopMainchainBlock);

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: knownBlockchainTop = topMainchainBlock;
	  knownBlockchainTop.CopyFrom(topMainchainBlock);
	}

	observerManager.notify(IBlockchainObserver.blockchainUpdated, newBlocks, alternativeBlocks);
	logger.functorMethod(DEBUGGING) << "localBlockchainUpdated notification was successfully sent.";
  }

  private BlockDetails knownBlockchainTop = new BlockDetails();
  private Dictionary<Crypto.Hash, TransactionDetails> knownPoolState = new Dictionary<Crypto.Hash, TransactionDetails>();

  private std::atomic<State> state = new std::atomic<State>();
  private std::atomic<bool> synchronized = new std::atomic<bool>();
  private std::atomic<uint> observersCounter = new std::atomic<uint>();
  private Tools.ObserverManager<IBlockchainObserver> observerManager = new Tools.ObserverManager<IBlockchainObserver>();

  private object mutex = new object();

  private INode node;
  private Logging.LoggerRef logger = new Logging.LoggerRef();

  private WalletAsyncContextCounter asyncContextCounter = new WalletAsyncContextCounter();
  private PoolUpdateGuard poolUpdateGuard = new PoolUpdateGuard();
}
}

namespace CryptoNote
{

public class ContextCounterHolder : System.IDisposable
{
  public ContextCounterHolder(WalletAsyncContextCounter counter)
  {
	  this.counter = new CryptoNote.WalletAsyncContextCounter(counter);
  }
  public void Dispose()
  {
	  counter.delAsyncContext();
  }

  private WalletAsyncContextCounter counter;
}

public class NodeRequest
{

  public NodeRequest(Action<const INode.Callback >& request)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.requestFunc = request;
	  this.requestFunc.CopyFrom(request);
  }

  public std::error_code performBlocking()
  {
	std::promise<std::error_code> promise = new std::promise<std::error_code>();
	std::future<std::error_code> future = promise.get_future();
	requestFunc((std::error_code c) =>
	{
	  blockingCompleteionCallback(std::move(promise), new std::error_code(c));
	});
	return future.get();
  }

  public void performAsync(WalletAsyncContextCounter asyncContextCounter, INode.Callback callback)
  {
	asyncContextCounter.addAsyncContext();
	requestFunc(std::bind(NodeRequest.asyncCompleteionCallback, callback, std::@ref(asyncContextCounter), std::placeholders._1));
  }

  private void blockingCompleteionCallback(std::promise<std::error_code> promise, std::error_code ec)
  {
	promise.set_value(ec);
  }

  private static void asyncCompleteionCallback(INode.Callback callback, WalletAsyncContextCounter asyncContextCounter, std::error_code ec)
  {
	ContextCounterHolder counterHolder = new ContextCounterHolder(asyncContextCounter);
	try
	{
	  callback(ec);
	}
	catch
	{
	  return;
	}
  }

  private readonly Action<const INode.Callback> requestFunc;
}

public class ScopeExitHandler : System.IDisposable
{
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public ScopeExitHandler(Action && handler)
  {
	  this.m_handler = std::move(handler);
	  this.m_cancelled = false;
  }

  public void Dispose()
  {
	if (!m_cancelled)
	{
	  m_handler();
	}
  }

  public void reset()
  {
	m_cancelled = true;
  }

  private Action m_handler;
  private bool m_cancelled;
}

public class StateRollback : System.IDisposable
{
  public StateRollback(std::atomic<State> s)
  {
	  this.state = s;
	state.store(State.INITIALIZED);
  }
  public void commit()
  {
	  done = true;
  }
  public void Dispose()
  {
	if (!done)
	{
	  state.store(State.NOT_INITIALIZED);
	}
  }
  public bool done = false;
  public std::atomic<State> state;
}

}
