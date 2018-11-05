// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


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
//ORIGINAL LINE: #define ENDL std::endl
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);


namespace CryptoNote
{

//C++ TO C# CONVERTER TODO TASK: Multiple inheritance is not available in C#:
public class Core : ICore, ICoreInformation
{
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public Core(Currency currency, Logging.ILogger logger, Checkpoints && checkpoints, System.Dispatcher dispatcher, std::unique_ptr<IBlockchainCacheFactory>&& blockchainCacheFactory, std::unique_ptr<IMainChainStorage>&& mainchainStorage)
  {
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
	  this.currency = new CryptoNote.Currency(currency);
	  this.dispatcher = new System.Dispatcher(dispatcher);
	  this.contextGroup = dispatcher;
	  this.logger = new Logging.LoggerRef(logger, "Core");
	  this.checkpoints = new CryptoNote.Checkpoints(std::move(checkpoints));
	  this.upgradeManager = new UpgradeManager();
	  this.blockchainCacheFactory = std::move(blockchainCacheFactory);
	  this.mainChainStorage = std::move(mainchainStorage);
	  this.initialized = false;

	upgradeManager.addMajorBlockVersion(BLOCK_MAJOR_VERSION_2, currency.upgradeHeight(BLOCK_MAJOR_VERSION_2));
	upgradeManager.addMajorBlockVersion(BLOCK_MAJOR_VERSION_3, currency.upgradeHeight(BLOCK_MAJOR_VERSION_3));
	upgradeManager.addMajorBlockVersion(BLOCK_MAJOR_VERSION_4, currency.upgradeHeight(BLOCK_MAJOR_VERSION_4));

	transactionPool = std::unique_ptr<ITransactionPoolCleanWrapper>(new TransactionPoolCleanWrapper(std::unique_ptr<ITransactionPool>(new TransactionPool(logger)), std::unique_ptr<ITimeProvider>(new RealTimeProvider()), logger, currency.mempoolTxLiveTime()));
  }
  public override void Dispose()
  {
	contextGroup.interrupt();
	contextGroup.wait();
	  base.Dispose();
  }

  public override bool addMessageQueue(MessageQueue<BlockchainMessage> messageQueue)
  {
	return queueList.insert(messageQueue);
  }
  public override bool removeMessageQueue(MessageQueue<BlockchainMessage> messageQueue)
  {
	return queueList.remove(messageQueue);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint32_t getTopBlockIndex() const override
  public override uint32_t getTopBlockIndex()
  {
	Debug.Assert(chainsStorage.Count > 0);
	Debug.Assert(chainsLeaves.Count > 0);
	throwIfNotInitialized();

	return chainsLeaves[0].getTopBlockIndex();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual Crypto::Hash getTopBlockHash() const override
  public override Crypto.Hash getTopBlockHash()
  {
	Debug.Assert(chainsStorage.Count > 0);
	Debug.Assert(chainsLeaves.Count > 0);

	throwIfNotInitialized();

	return chainsLeaves[0].getTopBlockHash();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual Crypto::Hash getBlockHashByIndex(uint32_t blockIndex) const override
  public override Crypto.Hash getBlockHashByIndex(uint32_t blockIndex)
  {
	Debug.Assert(chainsStorage.Count > 0);
	Debug.Assert(chainsLeaves.Count > 0);
	Debug.Assert(blockIndex <= getTopBlockIndex());

	throwIfNotInitialized();

	return chainsLeaves[0].getBlockHash(new uint32_t(blockIndex));
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint64_t getBlockTimestampByIndex(uint32_t blockIndex) const override
  public override uint64_t getBlockTimestampByIndex(uint32_t blockIndex)
  {
	Debug.Assert(chainsStorage.Count > 0);
	Debug.Assert(chainsLeaves.Count > 0);
	Debug.Assert(blockIndex <= getTopBlockIndex());

	throwIfNotInitialized();

//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: auto timestamps = chainsLeaves[0]->getLastTimestamps(1, blockIndex, addGenesisBlock);
	var timestamps = chainsLeaves[0].getLastTimestamps(1, new uint32_t(blockIndex), new CryptoNote.UseGenesis(GlobalMembers.addGenesisBlock));
	Debug.Assert(!(timestamps.Count == 1));

	return timestamps[0];
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool hasBlock(const Crypto::Hash& blockHash) const override
  public override bool hasBlock(Crypto.Hash blockHash)
  {
	throwIfNotInitialized();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: return findSegmentContainingBlock(blockHash) != null;
	return findSegmentContainingBlock(new Crypto.Hash(blockHash)) != null;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual BlockTemplate getBlockByIndex(uint32_t index) const override
  public override BlockTemplate getBlockByIndex(uint32_t index)
  {
	Debug.Assert(chainsStorage.Count > 0);
	Debug.Assert(chainsLeaves.Count > 0);
	Debug.Assert(index <= getTopBlockIndex());

	throwIfNotInitialized();
	IBlockchainCache segment = findMainChainSegmentContainingBlock(new uint32_t(index));
	Debug.Assert(segment != null);

	return restoreBlockTemplate(segment, new uint32_t(index));
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual BlockTemplate getBlockByHash(const Crypto::Hash& blockHash) const override
  public override BlockTemplate getBlockByHash(Crypto.Hash blockHash)
  {
	Debug.Assert(chainsStorage.Count > 0);
	Debug.Assert(chainsLeaves.Count > 0);

	throwIfNotInitialized();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: IBlockchainCache* segment = findMainChainSegmentContainingBlock(blockHash);
	IBlockchainCache segment = findMainChainSegmentContainingBlock(new Crypto.Hash(blockHash)); // TODO should it be requested from the main chain?
	if (segment == null)
	{
	  throw new System.Exception("Requested hash wasn't found in main blockchain");
	}

	uint32_t blockIndex = segment.getBlockIndex(blockHash);

	return restoreBlockTemplate(segment, new uint32_t(blockIndex));
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> buildSparseChain() const override
  public override List<Crypto.Hash> buildSparseChain()
  {
	throwIfNotInitialized();
	Crypto.Hash topBlockHash = chainsLeaves[0].getTopBlockHash();
	return doBuildSparseChain(topBlockHash);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> findBlockchainSupplement(const ClassicVector<Crypto::Hash>& remoteBlockIds, size_t maxCount, uint32_t& totalBlockCount, uint32_t& startBlockIndex) const override
  public override List<Crypto.Hash> findBlockchainSupplement(List<Crypto.Hash> remoteBlockIds, size_t maxCount, ref uint32_t totalBlockCount, ref uint32_t startBlockIndex)
  {
	Debug.Assert(remoteBlockIds.Count > 0);
	Debug.Assert(remoteBlockIds[remoteBlockIds.Count - 1] == getBlockHashByIndex(0));
	throwIfNotInitialized();

	totalBlockCount = getTopBlockIndex() + 1;
	startBlockIndex = findBlockchainSupplement(remoteBlockIds);

	return getBlockHashes(new uint32_t(startBlockIndex), (uint32_t)maxCount);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<RawBlock> getBlocks(uint32_t minIndex, uint32_t count) const override
  public override List<RawBlock> getBlocks(uint32_t minIndex, uint32_t count)
  {
	Debug.Assert(chainsStorage.Count > 0);
	Debug.Assert(chainsLeaves.Count > 0);

	throwIfNotInitialized();

	List<RawBlock> blocks = new List<RawBlock>();
	if (count > 0)
	{
	  var cache = chainsLeaves[0];
	  var maxIndex = Math.Min(minIndex + count - 1, cache.getTopBlockIndex());
	  blocks.Capacity = count;
	  while (cache != null)
	  {
		if (cache.getTopBlockIndex() >= maxIndex)
		{
		  var minChainIndex = Math.Max(minIndex, cache.getStartBlockIndex());
		  for (; minChainIndex <= maxIndex; --maxIndex)
		  {
			blocks.emplace_back(cache.getBlockByIndex(maxIndex));
			if (maxIndex == 0)
			{
			  break;
			}
		  }
		}

		if (blocks.Count == count)
		{
		  break;
		}

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: cache = cache->getParent();
		cache.CopyFrom(cache.getParent());
	  }
	}
	blocks.Reverse();

	return blocks;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual void getBlocks(const ClassicVector<Crypto::Hash>& blockHashes, ClassicVector<RawBlock>& blocks, ClassicVector<Crypto::Hash>& missedHashes) const override
  public override void getBlocks(List<Crypto.Hash> blockHashes, List<RawBlock> blocks, List<Crypto.Hash> missedHashes)
  {
	throwIfNotInitialized();

	foreach (var hash in blockHashes)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: IBlockchainCache* blockchainSegment = findSegmentContainingBlock(hash);
	  IBlockchainCache blockchainSegment = findSegmentContainingBlock(new Crypto.Hash(hash));
	  if (blockchainSegment == null)
	  {
		missedHashes.Add(hash);
	  }
	  else
	  {
		uint32_t blockIndex = blockchainSegment.getBlockIndex(hash);
		Debug.Assert(blockIndex <= blockchainSegment.getTopBlockIndex());

		blocks.Add(blockchainSegment.getBlockByIndex(new uint32_t(blockIndex)));
	  }
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool queryBlocks(const ClassicVector<Crypto::Hash>& blockHashes, uint64_t timestamp, uint32_t& startIndex, uint32_t& currentIndex, uint32_t& fullOffset, ClassicVector<BlockFullInfo>& entries) const override
  public override bool queryBlocks(List<Crypto.Hash> blockHashes, uint64_t timestamp, ref uint32_t startIndex, ref uint32_t currentIndex, ref uint32_t fullOffset, List<BlockFullInfo> entries)
  {
	Debug.Assert(entries.Count == 0);
	Debug.Assert(chainsLeaves.Count > 0);
	Debug.Assert(chainsStorage.Count > 0);
	throwIfNotInitialized();

	try
	{
	  IBlockchainCache mainChain = chainsLeaves[0];
	  currentIndex = mainChain.getTopBlockIndex();

	  startIndex = findBlockchainSupplement(blockHashes); // throws

	  fullOffset = mainChain.getTimestampLowerBoundBlockIndex(new uint64_t(timestamp));
	  if (fullOffset < startIndex)
	  {
		fullOffset = startIndex;
	  }

	  size_t hashesPushed = pushBlockHashes(new uint32_t(startIndex), new uint32_t(fullOffset), BLOCKS_IDS_SYNCHRONIZING_DEFAULT_COUNT, entries);

	  if (startIndex + hashesPushed != fullOffset != null)
	  {
		return true;
	  }

	  fillQueryBlockFullInfo(new uint32_t(fullOffset), new uint32_t(currentIndex), BLOCKS_SYNCHRONIZING_DEFAULT_COUNT, entries);

	  return true;
	}
	catch (System.Exception)
	{
	  // TODO log
	  return false;
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool queryBlocksLite(const ClassicVector<Crypto::Hash>& knownBlockHashes, uint64_t timestamp, uint32_t& startIndex, uint32_t& currentIndex, uint32_t& fullOffset, ClassicVector<BlockShortInfo>& entries) const override
  public override bool queryBlocksLite(List<Crypto.Hash> knownBlockHashes, uint64_t timestamp, ref uint32_t startIndex, ref uint32_t currentIndex, ref uint32_t fullOffset, List<BlockShortInfo> entries)
  {
	Debug.Assert(entries.Count == 0);
	Debug.Assert(chainsLeaves.Count > 0);
	Debug.Assert(chainsStorage.Count > 0);

	throwIfNotInitialized();

	try
	{
	  IBlockchainCache mainChain = chainsLeaves[0];
	  currentIndex = mainChain.getTopBlockIndex();

	  startIndex = findBlockchainSupplement(knownBlockHashes); // throws

	  // Stops bug where wallets fail to sync, because timestamps have been adjusted after syncronisation.
	  // check for a query of the blocks where the block index is non-zero, but the timestamp is zero
	  // indicating that the originator did not know the internal time of the block, but knew which block
	  // was wanted by index.  Fullfill this by getting the time of m_blocks[startIndex].timestamp.

	  if (startIndex > 0 && timestamp == 0)
	  {
		if (startIndex <= mainChain.getTopBlockIndex())
		{
		  RawBlock block = mainChain.getBlockByIndex(new uint32_t(startIndex));
		  var blockTemplate = GlobalMembers.extractBlockTemplate(block);
		  timestamp = blockTemplate.timestamp;
		}
	  }

	  fullOffset = mainChain.getTimestampLowerBoundBlockIndex(new uint64_t(timestamp));
	  if (fullOffset < startIndex)
	  {
		fullOffset = startIndex;
	  }

	  size_t hashesPushed = pushBlockHashes(new uint32_t(startIndex), new uint32_t(fullOffset), BLOCKS_IDS_SYNCHRONIZING_DEFAULT_COUNT, entries);

	  if (startIndex + (uint32_t)hashesPushed != fullOffset != null)
	  {
		return true;
	  }

	  fillQueryBlockShortInfo(new uint32_t(fullOffset), new uint32_t(currentIndex), BLOCKS_SYNCHRONIZING_DEFAULT_COUNT, entries);

	  return true;
	}
	catch (System.Exception e)
	{
	  logger.functorMethod(Logging.Level.ERROR) << "Failed to query blocks: " << e.Message;
	  return false;
	}
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool hasTransaction(const Crypto::Hash& transactionHash) const override
  public override bool hasTransaction(Crypto.Hash transactionHash)
  {
	throwIfNotInitialized();
	return findSegmentContainingTransaction(transactionHash) != null || transactionPool.checkIfTransactionPresent(transactionHash);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual void getTransactions(const ClassicVector<Crypto::Hash>& transactionHashes, ClassicVector<ClassicVector<uint8_t>>& transactions, ClassicVector<Crypto::Hash>& missedHashes) const override
  public override void getTransactions(List<Crypto.Hash> transactionHashes, List<List<uint8_t>> transactions, List<Crypto.Hash> missedHashes)
  {
	Debug.Assert(chainsLeaves.Count > 0);
	Debug.Assert(chainsStorage.Count > 0);
	throwIfNotInitialized();

	IBlockchainCache segment = chainsLeaves[0];
	Debug.Assert(segment != null);

	List<Crypto.Hash> leftTransactions = new List(transactionHashes);

	// find in main chain
	do
	{
	  List<Crypto.Hash> missedTransactions = new List<Crypto.Hash>();
	  segment.getRawTransactions(leftTransactions, transactions, missedTransactions);

	  leftTransactions = std::move(missedTransactions);
	  segment = segment.getParent();
	} while (segment != null && leftTransactions.Count > 0);

	if (leftTransactions.Count == 0)
	{
	  return;
	}

	// find in alternative chains
	for (size_t chain = 1; chain < chainsLeaves.Count; ++chain)
	{
	  segment = chainsLeaves[chain];

	  while (mainChainSet.count(segment) == 0 && leftTransactions.Count > 0)
	  {
		List<Crypto.Hash> missedTransactions = new List<Crypto.Hash>();
		segment.getRawTransactions(leftTransactions, transactions, missedTransactions);

		leftTransactions = std::move(missedTransactions);
		segment = segment.getParent();
	  }
	}

	missedHashes.AddRange(leftTransactions);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint64_t getBlockDifficulty(uint32_t blockIndex) const override
  public override uint64_t getBlockDifficulty(uint32_t blockIndex)
  {
	throwIfNotInitialized();
	IBlockchainCache mainChain = chainsLeaves[0];
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: auto difficulties = mainChain->getLastCumulativeDifficulties(2, blockIndex, addGenesisBlock);
	var difficulties = mainChain.getLastCumulativeDifficulties(2, new uint32_t(blockIndex), new CryptoNote.UseGenesis(GlobalMembers.addGenesisBlock));
	if (difficulties.Count == 2)
	{
	  return difficulties[1] - difficulties[0];
	}

	Debug.Assert(difficulties.Count == 1);
	return difficulties[0];
  }

  // TODO: just use mainChain->getDifficultyForNextBlock() ?
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint64_t getDifficultyForNextBlock() const override
  public override uint64_t getDifficultyForNextBlock()
  {
	throwIfNotInitialized();
	IBlockchainCache mainChain = chainsLeaves[0];

	uint32_t topBlockIndex = mainChain.getTopBlockIndex();

	uint8_t nextBlockMajorVersion = getBlockMajorVersionForHeight(new uint32_t(topBlockIndex));

	size_t blocksCount = Math.Min((size_t)topBlockIndex, currency.difficultyBlocksCountByBlockVersion(new uint8_t(nextBlockMajorVersion), new uint32_t(topBlockIndex)));

	var timestamps = mainChain.getLastTimestamps(new size_t(blocksCount));
	var difficulties = mainChain.getLastCumulativeDifficulties(new size_t(blocksCount));

	return currency.getNextDifficulty(new uint8_t(nextBlockMajorVersion), new uint32_t(topBlockIndex), new List<uint64_t>(timestamps), new List<uint64_t>(difficulties));
  }

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public override std::error_code addBlock(CachedBlock cachedBlock, RawBlock && rawBlock)
  {
	throwIfNotInitialized();
	uint32_t blockIndex = cachedBlock.getBlockIndex();
	Crypto.Hash blockHash = cachedBlock.getBlockHash();
	std::ostringstream os = new std::ostringstream();
	os << blockIndex << " (" << blockHash << ")";
	string blockStr = os.str();

	logger.functorMethod(Logging.Level.DEBUGGING) << "Request to add block " << blockStr;
	if (hasBlock(cachedBlock.getBlockHash()))
	{
	  logger.functorMethod(Logging.Level.DEBUGGING) << "Block " << blockStr << " already exists";
	  return error.AddBlockErrorCode.ALREADY_EXISTS;
	}

	auto blockTemplate = cachedBlock.getBlock();
	auto previousBlockHash = blockTemplate.previousBlockHash;

	Debug.Assert(rawBlock.transactions.size() == blockTemplate.transactionHashes.size());

	var cache = findSegmentContainingBlock(new auto(previousBlockHash));
	if (cache == null)
	{
	  logger.functorMethod(Logging.Level.DEBUGGING) << "Block " << blockStr << " rejected as orphaned";
	  return error.AddBlockErrorCode.REJECTED_AS_ORPHANED;
	}

	List<CachedTransaction> transactions = new List<CachedTransaction>();
	uint64_t cumulativeSize = 0;
	if (!extractTransactions(rawBlock.transactions, transactions, cumulativeSize))
	{
	  logger.functorMethod(Logging.Level.DEBUGGING) << "Couldn't deserialize raw block transactions in block " << blockStr;
	  return error.AddBlockErrorCode.DESERIALIZATION_FAILED;
	}

	var coinbaseTransactionSize = CryptoNote.GlobalMembers.getObjectBinarySize(blockTemplate.baseTransaction);
	Debug.Assert(coinbaseTransactionSize < decltype(coinbaseTransactionSize).MaxValue);
	var cumulativeBlockSize = coinbaseTransactionSize + cumulativeSize;
	TransactionValidatorState validatorState = new TransactionValidatorState();

	var previousBlockIndex = cache.getBlockIndex(previousBlockHash);

	bool addOnTop = cache.getTopBlockIndex() == previousBlockIndex;
	var maxBlockCumulativeSize = currency.maxBlockCumulativeSize(previousBlockIndex + 1);
	if (cumulativeBlockSize > maxBlockCumulativeSize)
	{
	  logger.functorMethod(Logging.Level.DEBUGGING) << "Block " << blockStr << " has too big cumulative size";
	  return error.BlockValidationError.CUMULATIVE_BLOCK_SIZE_TOO_BIG;
	}

	uint64_t minerReward = 0;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: auto blockValidationResult = validateBlock(cachedBlock, cache, minerReward);
	var blockValidationResult = validateBlock(cachedBlock, new CryptoNote.IBlockchainCache(cache), ref minerReward);
	if (blockValidationResult != null)
	{
	  logger.functorMethod(Logging.Level.DEBUGGING) << "Failed to validate block " << blockStr << ": " << blockValidationResult.message();
	  return blockValidationResult;
	}

	var currentDifficulty = cache.getDifficultyForNextBlock(new uint32_t(previousBlockIndex));
	if (currentDifficulty == 0)
	{
	  logger.functorMethod(Logging.Level.DEBUGGING) << "Block " << blockStr << " has difficulty overhead";
	  return error.BlockValidationError.DIFFICULTY_OVERHEAD;
	}

	// This allows us to accept blocks with transaction mixins for the mined money unlock window
	// that may be using older mixin rules on the network. This helps to clear out the transaction
	// pool during a network soft fork that requires a mixin lower or upper bound change
	uint32_t mixinChangeWindow = new uint32_t(blockIndex);
	if (mixinChangeWindow > CryptoNote.parameters.CRYPTONOTE_MINED_MONEY_UNLOCK_WINDOW)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: mixinChangeWindow = mixinChangeWindow - CryptoNote::parameters::CRYPTONOTE_MINED_MONEY_UNLOCK_WINDOW;
	  mixinChangeWindow.CopyFrom(mixinChangeWindow - CryptoNote.parameters.CRYPTONOTE_MINED_MONEY_UNLOCK_WINDOW);
	}

	var (success, error) = Mixins.validate(new List<CachedTransaction>(transactions), new uint32_t(blockIndex));

	if (!success)
	{
	  /* Warning, this shadows the above variables */
	  var (success, error) = Mixins.validate(new List<CachedTransaction>(transactions), new uint32_t(mixinChangeWindow));

	  if (!success)
	  {
		logger.functorMethod(Logging.Level.DEBUGGING) << error;
		return error.TransactionValidationError.INVALID_MIXIN;
	  }
	}

	uint64_t cumulativeFee = 0;

	foreach (var transaction in transactions)
	{
	  uint64_t fee = 0;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: auto transactionValidationResult = validateTransaction(transaction, validatorState, cache, fee, previousBlockIndex);
	  var transactionValidationResult = validateTransaction(transaction, validatorState, new CryptoNote.IBlockchainCache(cache), fee, new uint32_t(previousBlockIndex));
	  if (transactionValidationResult != null)
	  {
		logger.functorMethod(Logging.Level.DEBUGGING) << "Failed to validate transaction " << transaction.getTransactionHash() << ": " << transactionValidationResult.message();
		return transactionValidationResult;
	  }

	  cumulativeFee += fee;
	}

	uint64_t reward = 0;
	int64_t emissionChange = 0;
	var alreadyGeneratedCoins = cache.getAlreadyGeneratedCoins(new uint32_t(previousBlockIndex));
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: auto lastBlocksSizes = cache->getLastBlocksSizes(currency.rewardBlocksWindow(), previousBlockIndex, addGenesisBlock);
	var lastBlocksSizes = cache.getLastBlocksSizes(currency.rewardBlocksWindow(), new uint32_t(previousBlockIndex), new CryptoNote.UseGenesis(GlobalMembers.addGenesisBlock));
	var blocksSizeMedian = Common.GlobalMembers.medianValue(lastBlocksSizes);

	if (!currency.getBlockReward(new uint8_t(cachedBlock.getBlock().majorVersion), blocksSizeMedian, cumulativeBlockSize, new uint64_t(alreadyGeneratedCoins), new uint64_t(cumulativeFee), reward, emissionChange))
	{
	  logger.functorMethod(Logging.Level.DEBUGGING) << "Block " << blockStr << " has too big cumulative size";
	  return error.BlockValidationError.CUMULATIVE_BLOCK_SIZE_TOO_BIG;
	}

	if (minerReward != reward)
	{
	  logger.functorMethod(Logging.Level.DEBUGGING) << "Block reward mismatch for block " << blockStr << ". Expected reward: " << reward << ", got reward: " << minerReward;
	  return error.BlockValidationError.BLOCK_REWARD_MISMATCH;
	}

	if (checkpoints.isInCheckpointZone(cachedBlock.getBlockIndex()))
	{
	  if (!checkpoints.checkBlock(cachedBlock.getBlockIndex(), cachedBlock.getBlockHash()))
	  {
		logger.functorMethod(Logging.Level.WARNING) << "Checkpoint block hash mismatch for block " << blockStr;
		return error.BlockValidationError.CHECKPOINT_BLOCK_HASH_MISMATCH;
	  }
	}
	else if (!currency.checkProofOfWork(cachedBlock, new uint64_t(currentDifficulty)))
	{
	  logger.functorMethod(Logging.Level.WARNING) << "Proof of work too weak for block " << blockStr;
	  return error.BlockValidationError.PROOF_OF_WORK_TOO_WEAK;
	}

	var ret = error.AddBlockErrorCode.ADDED_TO_ALTERNATIVE;

	if (addOnTop)
	{
	  if (cache.getChildCount() == 0)
	  {
		// add block on top of leaf segment.
		var hashes = GlobalMembers.preallocateVector<Crypto.Hash>(transactions.Count);

		// TODO: exception safety
		if (cache == chainsLeaves[0])
		{
		  mainChainStorage.pushBlock(rawBlock);

		  cache.pushBlock(cachedBlock, transactions, validatorState, cumulativeBlockSize, new int64_t(emissionChange), new uint64_t(currentDifficulty), std::move(rawBlock));

		  updateBlockMedianSize();
		  actualizePoolTransactionsLite(validatorState);

		  ret = error.AddBlockErrorCode.ADDED_TO_MAIN;
		  logger.functorMethod(Logging.Level.DEBUGGING) << "Block " << blockStr << " added to main chain.";
		  if ((previousBlockIndex + 1) % 100 == 0 != null)
		  {
			logger.functorMethod(Logging.Level.INFO) << "Block " << blockStr << " added to main chain";
		  }

		  notifyObservers(makeDelTransactionMessage(std::move(hashes), Messages.DeleteTransaction.Reason.InBlock));
		}
		else
		{
		  cache.pushBlock(cachedBlock, transactions, validatorState, cumulativeBlockSize, new int64_t(emissionChange), new uint64_t(currentDifficulty), std::move(rawBlock));
		  logger.functorMethod(Logging.Level.DEBUGGING) << "Block " << blockStr << " added to alternative chain.";

		  var mainChainCache = chainsLeaves[0];
		  if (cache.getCurrentCumulativeDifficulty() > mainChainCache.getCurrentCumulativeDifficulty())
		  {
			size_t endpointIndex = std::distance(chainsLeaves.GetEnumerator(), std::find(chainsLeaves.GetEnumerator(), chainsLeaves.end(), cache));
			Debug.Assert(endpointIndex != chainsStorage.Count);
			Debug.Assert(endpointIndex != 0);
			std::swap(chainsLeaves[0], chainsLeaves[endpointIndex]);
			updateMainChainSet();

			updateBlockMedianSize();
			actualizePoolTransactions();
			copyTransactionsToPool(new List<IBlockchainCache>(chainsLeaves[endpointIndex]));

			switchMainChainStorage(chainsLeaves[0].getStartBlockIndex(), *chainsLeaves[0]);

			ret = error.AddBlockErrorCode.ADDED_TO_ALTERNATIVE_AND_SWITCHED;

			logger.functorMethod(Logging.Level.INFO) << "Resolved: " << blockStr << ", Previous: " << chainsLeaves[endpointIndex].getTopBlockIndex() << " (" << chainsLeaves[endpointIndex].getTopBlockHash() << ")";
		  }
		}
	  }
	  else
	  {
		//add block on top of segment which is not leaf! the case when we got more than one alternative block on the same height
		var newCache = blockchainCacheFactory.createBlockchainCache(currency, cache, previousBlockIndex + 1);
		cache.addChild(newCache.get());

		var newlyForkedChainPtr = newCache.get();
		chainsStorage.emplace_back(std::move(newCache));
		chainsLeaves.Add(newlyForkedChainPtr);

		logger.functorMethod(Logging.Level.DEBUGGING) << "Resolving: " << blockStr;

		newlyForkedChainPtr.pushBlock(cachedBlock, transactions, validatorState, cumulativeBlockSize, emissionChange, currentDifficulty, std::move(rawBlock));

		updateMainChainSet();
		updateBlockMedianSize();
	  }
	}
	else
	{
	  logger.functorMethod(Logging.Level.DEBUGGING) << "Resolving: " << blockStr;

	  var upperSegment = cache.split(previousBlockIndex + 1);
	  //[cache] is lower segment now

	  Debug.Assert(upperSegment.getBlockCount() > 0);
	  Debug.Assert(cache.getBlockCount() > 0);

	  if (upperSegment.getChildCount() == 0)
	  {
		//newly created segment is leaf node
		//[cache] used to be a leaf node. we have to replace it with upperSegment
		var found = std::find(chainsLeaves.GetEnumerator(), chainsLeaves.end(), cache);
		Debug.Assert(found != chainsLeaves.end());

		*found = upperSegment.get();
	  }

	  chainsStorage.emplace_back(std::move(upperSegment));

	  var newCache = blockchainCacheFactory.createBlockchainCache(currency, cache, previousBlockIndex + 1);
	  cache.addChild(newCache.get());

	  var newlyForkedChainPtr = newCache.get();
	  chainsStorage.emplace_back(std::move(newCache));
	  chainsLeaves.Add(newlyForkedChainPtr);

	  newlyForkedChainPtr.pushBlock(cachedBlock, transactions, validatorState, cumulativeBlockSize, emissionChange, currentDifficulty, std::move(rawBlock));

	  updateMainChainSet();
	}

	logger.functorMethod(Logging.Level.DEBUGGING) << "Block: " << blockStr << " successfully added";
	notifyOnSuccess(ret, new uint32_t(previousBlockIndex), cachedBlock, *cache);

	return ret;
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public override std::error_code addBlock(RawBlock && rawBlock)
  {
	throwIfNotInitialized();

	BlockTemplate blockTemplate = new BlockTemplate();
	bool result = CryptoNote.GlobalMembers.fromBinaryArray(ref blockTemplate, rawBlock.block);
	if (!result)
	{
	  return error.AddBlockErrorCode.DESERIALIZATION_FAILED;
	}

	CachedBlock cachedBlock = new CachedBlock(blockTemplate);
	return addBlock(cachedBlock, std::move(rawBlock));
  }

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override std::error_code submitBlock(BinaryArray&& rawBlockTemplate);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getTransactionGlobalIndexes(const Crypto::Hash& transactionHash, ClassicVector<uint32_t>& globalIndexes) const override
  public override bool getTransactionGlobalIndexes(Crypto.Hash transactionHash, List<uint32_t> globalIndexes)
  {
	throwIfNotInitialized();
	IBlockchainCache segment = chainsLeaves[0];

	bool found = false;
	while (segment != null && found == false)
	{
	  found = segment.getTransactionGlobalIndexes(transactionHash, globalIndexes);
	  segment = segment.getParent();
	}

	if (found)
	{
	  return true;
	}

	for (size_t i = 1; i < chainsLeaves.Count && found == false; ++i)
	{
	  segment = chainsLeaves[i];
	  while (found == false && mainChainSet.count(segment) == 0)
	  {
		found = segment.getTransactionGlobalIndexes(transactionHash, globalIndexes);
		segment = segment.getParent();
	  }
	}

	return found;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getRandomOutputs(uint64_t amount, uint16_t count, ClassicVector<uint32_t>& globalIndexes, ClassicVector<Crypto::PublicKey>& publicKeys) const override
  public override bool getRandomOutputs(uint64_t amount, uint16_t count, ref List<uint32_t> globalIndexes, List<Crypto.PublicKey> publicKeys)
  {
	throwIfNotInitialized();

	if (count == 0)
	{
	  return true;
	}

	var upperBlockLimit = getTopBlockIndex() - currency.minedMoneyUnlockWindow();
	if (upperBlockLimit < currency.minedMoneyUnlockWindow())
	{
	  logger.functorMethod(Logging.Level.DEBUGGING) << "Blockchain height is less than mined unlock window";
	  return false;
	}

	globalIndexes = chainsLeaves[0].getRandomOutsByAmount(new uint64_t(amount), new uint16_t(count), getTopBlockIndex());
	if (globalIndexes.Count == 0)
	{
	  return false;
	}

	globalIndexes.Sort();

	switch (chainsLeaves[0].extractKeyOutputKeys(new uint64_t(amount), getTopBlockIndex(), new Common.ArrayView<uint32_t>(globalIndexes.data(), globalIndexes.Count), publicKeys))
	{
	  case ExtractOutputKeysResult.SUCCESS:
		return true;
	  case ExtractOutputKeysResult.INVALID_GLOBAL_INDEX:
		logger.functorMethod(Logging.Level.DEBUGGING) << "Invalid global index is given";
		return false;
	  case ExtractOutputKeysResult.OUTPUT_LOCKED:
		logger.functorMethod(Logging.Level.DEBUGGING) << "Output is locked";
		return false;
	}

	return false;
  }

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool addTransactionToPool(BinaryArray transactionBinaryArray);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getPoolTransactionHashes() const override
  public override List<Crypto.Hash> getPoolTransactionHashes()
  {
	throwIfNotInitialized();

	return transactionPool.getTransactionHashes();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getPoolChanges(const Crypto::Hash& lastBlockHash, const ClassicVector<Crypto::Hash>& knownHashes, ClassicVector<ClassicVector<uint8_t>>& addedTransactions, ClassicVector<Crypto::Hash>& deletedTransactions) const override
  public override bool getPoolChanges(Crypto.Hash lastBlockHash, List<Crypto.Hash> knownHashes, List<List<uint8_t>> addedTransactions, List<Crypto.Hash> deletedTransactions)
  {
	throwIfNotInitialized();

	List<Crypto.Hash> newTransactions = new List<Crypto.Hash>();
	getTransactionPoolDifference(knownHashes, newTransactions, deletedTransactions);

	addedTransactions.Capacity = newTransactions.Count;
	foreach (var hash in newTransactions)
	{
	  addedTransactions.emplace_back(transactionPool.getTransaction(hash).getTransactionBinaryArray());
	}

	return getTopBlockHash() == lastBlockHash;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getPoolChangesLite(const Crypto::Hash& lastBlockHash, const ClassicVector<Crypto::Hash>& knownHashes, ClassicVector<TransactionPrefixInfo>& addedTransactions, ClassicVector<Crypto::Hash>& deletedTransactions) const override
  public override bool getPoolChangesLite(Crypto.Hash lastBlockHash, List<Crypto.Hash> knownHashes, List<TransactionPrefixInfo> addedTransactions, List<Crypto.Hash> deletedTransactions)
  {
	throwIfNotInitialized();

	List<Crypto.Hash> newTransactions = new List<Crypto.Hash>();
	getTransactionPoolDifference(knownHashes, newTransactions, deletedTransactions);

	addedTransactions.Capacity = newTransactions.Count;
	foreach (var hash in newTransactions)
	{
	  TransactionPrefixInfo transactionPrefixInfo = new TransactionPrefixInfo();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: transactionPrefixInfo.txHash = hash;
	  transactionPrefixInfo.txHash.CopyFrom(hash);
	  transactionPrefixInfo.txPrefix = (TransactionPrefix)(transactionPool.getTransaction(hash).getTransaction());
	  addedTransactions.emplace_back(std::move(transactionPrefixInfo));
	}

	return getTopBlockHash() == lastBlockHash;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getBlockTemplate(BlockTemplate& b, const AccountPublicAddress& adr, const BinaryArray& extraNonce, uint64_t& difficulty, uint32_t& height) const override;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override bool getBlockTemplate(BlockTemplate b, AccountPublicAddress adr, BinaryArray extraNonce, uint64_t difficulty, uint32_t height);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual CoreStatistics getCoreStatistics() const override
  public override CoreStatistics getCoreStatistics()
  {
	// TODO: implement it
	Debug.Assert(false);
	CoreStatistics result = new CoreStatistics();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	std::fill(reinterpret_cast<uint8_t>(result), reinterpret_cast<uint8_t>(result) + sizeof(CryptoNote.CoreStatistics), 0);
	return result;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual std::time_t getStartTime() const
  public virtual std::time_t getStartTime()
  {
	return start_time;
  }

  //ICoreInformation
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual size_t getPoolTransactionCount() const override
  public override size_t getPoolTransactionCount()
  {
	throwIfNotInitialized();
	return transactionPool.getTransactionCount();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual size_t getBlockchainTransactionCount() const override
  public override size_t getBlockchainTransactionCount()
  {
	throwIfNotInitialized();
	IBlockchainCache mainChain = chainsLeaves[0];
	return mainChain.getTransactionCount();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual size_t getAlternativeBlockCount() const override
  public override size_t getAlternativeBlockCount()
  {
	throwIfNotInitialized();

	return std::accumulate(chainsStorage.GetEnumerator(), chainsStorage.end(), size_t(0), (size_t sum, Ptr ptr) =>
	{
	  return mainChainSet.count(ptr.get()) == 0 ? sum + ptr.getBlockCount() : sum;
	});
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint64_t getTotalGeneratedAmount() const override
  public override uint64_t getTotalGeneratedAmount()
  {
	Debug.Assert(chainsLeaves.Count > 0);
	throwIfNotInitialized();

	return chainsLeaves[0].getAlreadyGeneratedCoins();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<BlockTemplate> getAlternativeBlocks() const override
  public override List<BlockTemplate> getAlternativeBlocks()
  {
	throwIfNotInitialized();

	List<BlockTemplate> alternativeBlocks = new List<BlockTemplate>();
	foreach (var cache in chainsStorage)
	{
	  if (mainChainSet.count(cache.get()))
	  {
		continue;
	  }
	  for (var index = cache.getStartBlockIndex(); index <= cache.getTopBlockIndex(); ++index)
	  {
		// TODO: optimize
		alternativeBlocks.Add(CryptoNote.GlobalMembers.fromBinaryArray<BlockTemplate>(cache.getBlockByIndex(index).block));
	  }
	}

	return alternativeBlocks;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Transaction> getPoolTransactions() const override
  public override List<Transaction> getPoolTransactions()
  {
	throwIfNotInitialized();

	List<Transaction> transactions = new List<Transaction>();
	var hashes = transactionPool.getPoolTransactions();
	std::transform(std::begin(hashes), std::end(hashes), std::back_inserter(transactions), (CachedTransaction tx) =>
	{
		return tx.getTransaction();
	});
	return transactions;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Currency& getCurrency() const
  public Currency getCurrency()
  {
	return currency;
  }

  public override void save()
  {
	throwIfNotInitialized();

	deleteAlternativeChains();
	mergeMainChainSegments();
	chainsLeaves[0].save();
  }
  public override void load()
  {
	initRootSegment();

	start_time = std::time(null);

	var dbBlocksCount = chainsLeaves[0].getTopBlockIndex() + 1;
	var storageBlocksCount = mainChainStorage.getBlockCount();

	logger.functorMethod(Logging.Level.DEBUGGING) << "Blockchain storage blocks count: " << storageBlocksCount << ", DB blocks count: " << dbBlocksCount;

	Debug.Assert(storageBlocksCount != 0); //we assume the storage has at least genesis block

	if (storageBlocksCount > dbBlocksCount)
	{
	  logger.functorMethod(Logging.Level.INFO) << "Importing blocks from blockchain storage";
	  importBlocksFromStorage();
	}
	else if (storageBlocksCount < dbBlocksCount)
	{
	  var cutFrom = GlobalMembers.findCommonRoot(*mainChainStorage, *chainsLeaves[0]) + 1;

	  logger.functorMethod(Logging.Level.INFO) << "DB has more blocks than blockchain storage, cutting from block index: " << cutFrom;
	  cutSegment(*chainsLeaves[0], cutFrom);

	  Debug.Assert(chainsLeaves[0].getTopBlockIndex() + 1 == mainChainStorage.getBlockCount());
	}
	else if (GlobalMembers.getBlockHash(mainChainStorage.getBlockByIndex(storageBlocksCount - 1)) != chainsLeaves[0].getTopBlockHash())
	{
	  logger.functorMethod(Logging.Level.INFO) << "Blockchain storage and root segment are on different chains. " << "Cutting root segment to common block index " << GlobalMembers.findCommonRoot(*mainChainStorage, *chainsLeaves[0]) << " and reimporting blocks";
	  importBlocksFromStorage();
	}
	else
	{
	  logger.functorMethod(Logging.Level.DEBUGGING) << "Blockchain storage and root segment are on the same height and chain";
	}

	initialized = true;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual BlockDetails getBlockDetails(const Crypto::Hash& blockHash) const override
  public override BlockDetails getBlockDetails(Crypto.Hash blockHash)
  {
	throwIfNotInitialized();

//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: IBlockchainCache* segment = findSegmentContainingBlock(blockHash);
	IBlockchainCache segment = findSegmentContainingBlock(new Crypto.Hash(blockHash));
	if (segment == null)
	{
	  throw new System.Exception("Requested hash wasn't found in blockchain.");
	}

	uint32_t blockIndex = segment.getBlockIndex(blockHash);
	BlockTemplate blockTemplate = restoreBlockTemplate(segment, new uint32_t(blockIndex));

	BlockDetails blockDetails = new BlockDetails();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockDetails.majorVersion = blockTemplate.majorVersion;
	blockDetails.majorVersion.CopyFrom(blockTemplate.majorVersion);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockDetails.minorVersion = blockTemplate.minorVersion;
	blockDetails.minorVersion.CopyFrom(blockTemplate.minorVersion);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockDetails.timestamp = blockTemplate.timestamp;
	blockDetails.timestamp.CopyFrom(blockTemplate.timestamp);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockDetails.prevBlockHash = blockTemplate.previousBlockHash;
	blockDetails.prevBlockHash.CopyFrom(blockTemplate.previousBlockHash);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockDetails.nonce = blockTemplate.nonce;
	blockDetails.nonce.CopyFrom(blockTemplate.nonce);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockDetails.hash = blockHash;
	blockDetails.hash.CopyFrom(blockHash);

	blockDetails.reward = 0;
	foreach (TransactionOutput @out in blockTemplate.baseTransaction.outputs)
	{
	  blockDetails.reward += @out.amount;
	}

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockDetails.index = blockIndex;
	blockDetails.index.CopyFrom(blockIndex);
	blockDetails.isAlternative = mainChainSet.count(segment) == 0;

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockDetails.difficulty = getBlockDifficulty(blockIndex);
	blockDetails.difficulty.CopyFrom(getBlockDifficulty(new uint32_t(blockIndex)));

//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: ClassicVector<uint64_t> sizes = segment->getLastBlocksSizes(1, blockDetails.index, addGenesisBlock);
	List<uint64_t> sizes = segment.getLastBlocksSizes(1, new uint32_t(blockDetails.index), new CryptoNote.UseGenesis(GlobalMembers.addGenesisBlock));
	Debug.Assert(sizes.Count == 1);
	blockDetails.transactionsCumulativeSize = sizes[0];

	uint64_t blockBlobSize = CryptoNote.GlobalMembers.getObjectBinarySize(blockTemplate);
	uint64_t coinbaseTransactionSize = CryptoNote.GlobalMembers.getObjectBinarySize(blockTemplate.baseTransaction);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockDetails.blockSize = blockBlobSize + blockDetails.transactionsCumulativeSize - coinbaseTransactionSize;
	blockDetails.blockSize.CopyFrom(blockBlobSize + blockDetails.transactionsCumulativeSize - coinbaseTransactionSize);

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockDetails.alreadyGeneratedCoins = segment->getAlreadyGeneratedCoins(blockDetails.index);
	blockDetails.alreadyGeneratedCoins.CopyFrom(segment.getAlreadyGeneratedCoins(new uint32_t(blockDetails.index)));
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockDetails.alreadyGeneratedTransactions = segment->getAlreadyGeneratedTransactions(blockDetails.index);
	blockDetails.alreadyGeneratedTransactions.CopyFrom(segment.getAlreadyGeneratedTransactions(new uint32_t(blockDetails.index)));

	uint64_t prevBlockGeneratedCoins = 0;
	blockDetails.sizeMedian = 0;
	if (blockDetails.index > 0)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: auto lastBlocksSizes = segment->getLastBlocksSizes(currency.rewardBlocksWindow(), blockDetails.index - 1, addGenesisBlock);
	  var lastBlocksSizes = segment.getLastBlocksSizes(currency.rewardBlocksWindow(), blockDetails.index - 1, new CryptoNote.UseGenesis(GlobalMembers.addGenesisBlock));
	  blockDetails.sizeMedian = Common.GlobalMembers.medianValue(lastBlocksSizes);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: prevBlockGeneratedCoins = segment->getAlreadyGeneratedCoins(blockDetails.index - 1);
	  prevBlockGeneratedCoins.CopyFrom(segment.getAlreadyGeneratedCoins(blockDetails.index - 1));
	}

	int64_t emissionChange = 0;
	bool result = currency.getBlockReward(new uint8_t(blockDetails.majorVersion), new uint64_t(blockDetails.sizeMedian), 0, new uint64_t(prevBlockGeneratedCoins), 0, blockDetails.baseReward, emissionChange);
	if (result)
	{
	}
	Debug.Assert(result);

	uint64_t currentReward = 0;
	result = currency.getBlockReward(new uint8_t(blockDetails.majorVersion), new uint64_t(blockDetails.sizeMedian), new uint64_t(blockDetails.transactionsCumulativeSize), new uint64_t(prevBlockGeneratedCoins), 0, currentReward, emissionChange);
	Debug.Assert(result);

	if (blockDetails.baseReward == 0 && currentReward == 0)
	{
	  blockDetails.penalty = (double)0;
	}
	else
	{
	  Debug.Assert(blockDetails.baseReward >= currentReward);
	  blockDetails.penalty = (double)(blockDetails.baseReward - currentReward) / (double)blockDetails.baseReward;
	}

	blockDetails.transactions.Capacity = blockTemplate.transactionHashes.Count + 1;
	CachedTransaction cachedBaseTx = new CachedTransaction(std::move(blockTemplate.baseTransaction));
	blockDetails.transactions.Add(getTransactionDetails(cachedBaseTx.getTransactionHash(), segment, false));

	blockDetails.totalFeeAmount = 0;
	foreach (Crypto  in :Hash & transactionHash : blockTemplate.transactionHashes)
	{
	  blockDetails.transactions.Add(getTransactionDetails(transactionHash, segment, false));
	  blockDetails.totalFeeAmount += blockDetails.transactions[blockDetails.transactions.Count - 1].fee;
	}

	return blockDetails;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: BlockDetails getBlockDetails(const uint32_t blockHeight) const
  public BlockDetails getBlockDetails(uint32_t blockHeight)
  {
	throwIfNotInitialized();

	IBlockchainCache segment = findSegmentContainingBlock(new uint32_t(blockHeight));
	if (segment == null)
	{
	  throw new System.Exception("Requested block height wasn't found in blockchain.");
	}

	return getBlockDetails(segment.getBlockHash(new uint32_t(blockHeight)));
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual TransactionDetails getTransactionDetails(const Crypto::Hash& transactionHash) const override
  public override TransactionDetails getTransactionDetails(Crypto.Hash transactionHash)
  {
	throwIfNotInitialized();

	IBlockchainCache segment = findSegmentContainingTransaction(transactionHash);
	bool foundInPool = transactionPool.checkIfTransactionPresent(transactionHash);
	if (segment == null && !foundInPool)
	{
	  throw new System.Exception("Requested transaction wasn't found.");
	}

	return getTransactionDetails(transactionHash, segment, foundInPool);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getAlternativeBlockHashesByIndex(uint32_t blockIndex) const override
  public override List<Crypto.Hash> getAlternativeBlockHashesByIndex(uint32_t blockIndex)
  {
	throwIfNotInitialized();

	List<Crypto.Hash> alternativeBlockHashes = new List<Crypto.Hash>();
	for (size_t chain = 1; chain < chainsLeaves.Count; ++chain)
	{
	  IBlockchainCache segment = chainsLeaves[chain];
	  if (segment.getTopBlockIndex() < blockIndex)
	  {
		continue;
	  }

	  do
	  {
		if (segment.getTopBlockIndex() - segment.getBlockCount() + 1 <= blockIndex != null)
		{
		  alternativeBlockHashes.Add(segment.getBlockHash(new uint32_t(blockIndex)));
		  break;
		}
		else if (segment.getTopBlockIndex() - segment.getBlockCount() - 1 > blockIndex)
		{
		  segment = segment.getParent();
		  Debug.Assert(segment != null);
		}
	  } while (mainChainSet.count(segment) == 0);
	}
	return alternativeBlockHashes;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getBlockHashesByTimestamps(uint64_t timestampBegin, size_t secondsCount) const override
  public override List<Crypto.Hash> getBlockHashesByTimestamps(uint64_t timestampBegin, size_t secondsCount)
  {
	throwIfNotInitialized();

	logger.functorMethod(Logging.Level.DEBUGGING) << "getBlockHashesByTimestamps request with timestamp " << timestampBegin << " and seconds count " << secondsCount;

	var mainChain = chainsLeaves[0];

	if (timestampBegin + (uint64_t)secondsCount < timestampBegin != null)
	{
	  logger.functorMethod(Logging.Level.WARNING) << "Timestamp overflow occured. Timestamp begin: " << timestampBegin << ", timestamp end: " << (timestampBegin + (uint64_t)secondsCount);

	  throw new System.Exception("Timestamp overflow");
	}

	return mainChain.getBlockHashesByTimestamps(new uint64_t(timestampBegin), new size_t(secondsCount));
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getTransactionHashesByPaymentId(const Hash& paymentId) const override
  public override List<Crypto.Hash> getTransactionHashesByPaymentId(Hash paymentId)
  {
	throwIfNotInitialized();

	logger.functorMethod(Logging.Level.DEBUGGING) << "getTransactionHashesByPaymentId request with paymentId " << paymentId;

	var mainChain = chainsLeaves[0];

	List<Crypto.Hash> hashes = mainChain.getTransactionHashesByPaymentId(paymentId);
	List<Crypto.Hash> poolHashes = transactionPool.getTransactionHashesByPaymentId(paymentId);

	hashes.Capacity = hashes.Count + poolHashes.Count;
	std::move(poolHashes.GetEnumerator(), poolHashes.end(), std::back_inserter(hashes));

	return hashes;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint64_t get_current_blockchain_height() const
  public virtual uint64_t get_current_blockchain_height()
  {
	// TODO: remove when GetCoreStatistics is implemented
	return mainChainStorage.getBlockCount();
  }

  private readonly Currency currency;
  private System.Dispatcher dispatcher;
  private System.ContextGroup contextGroup = new System.ContextGroup();
  private Logging.LoggerRef logger = new Logging.LoggerRef();
  private Checkpoints checkpoints = new Checkpoints();
  private std::unique_ptr<IUpgradeManager> upgradeManager = new std::unique_ptr<IUpgradeManager>();
  private List<std::unique_ptr<IBlockchainCache>> chainsStorage = new List<std::unique_ptr<IBlockchainCache>>();
  private List<IBlockchainCache> chainsLeaves = new List<IBlockchainCache>();
  private std::unique_ptr<ITransactionPoolCleanWrapper> transactionPool = new std::unique_ptr<ITransactionPoolCleanWrapper>();
  private HashSet<IBlockchainCache> mainChainSet = new HashSet<IBlockchainCache>();

  private string dataFolder;

  private IntrusiveLinkedList<MessageQueue<BlockchainMessage>> queueList = new IntrusiveLinkedList<MessageQueue<BlockchainMessage>>();
  private std::unique_ptr<IBlockchainCacheFactory> blockchainCacheFactory = new std::unique_ptr<IBlockchainCacheFactory>();
  private std::unique_ptr<IMainChainStorage> mainChainStorage = new std::unique_ptr<IMainChainStorage>();
  private bool initialized;

  private time_t start_time = new time_t();

  private size_t blockMedianSize = new size_t();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void throwIfNotInitialized() const
  private void throwIfNotInitialized()
  {
	if (!initialized)
	{
	  throw std::system_error(error.GlobalMembers.make_error_code(error.CoreErrorCode.NOT_INITIALIZED));
	}
  }
  private bool extractTransactions(List<List<uint8_t>> rawTransactions, List<CachedTransaction> transactions, uint64_t cumulativeSize)
  {
	try
	{
	  foreach (var rawTransaction in rawTransactions)
	  {
		if (rawTransaction.Count > currency.maxTxSize())
		{
		  logger.functorMethod(Logging.Level.INFO) << "Raw transaction size " << rawTransaction.Count << " is too big.";
		  return false;
		}

		cumulativeSize += rawTransaction.Count;
		transactions.emplace_back(rawTransaction);
	  }
	}
	catch (System.Exception e)
	{
	  logger.functorMethod(Logging.Level.INFO) << e.Message;
	  return false;
	}

	return true;
  }

  private std::error_code validateSemantic(Transaction transaction, ref uint64_t fee, uint32_t blockIndex)
  {
	if (transaction.inputs.Count == 0)
	{
	  return error.TransactionValidationError.EMPTY_INPUTS;
	}

	uint64_t summaryOutputAmount = 0;
	foreach (var output in transaction.outputs)
	{
	  if (output.amount == 0)
	  {
		return error.TransactionValidationError.OUTPUT_ZERO_AMOUNT;
	  }

//C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
	  if (output.target.type() == typeid(KeyOutput))
	  {
		if (!check_key(boost::get<KeyOutput>(output.target).key))
		{
		  return error.TransactionValidationError.OUTPUT_INVALID_KEY;
		}
	  }
	  else
	  {
		return error.TransactionValidationError.OUTPUT_UNKNOWN_TYPE;
	  }

	  if (uint64_t.MaxValue - output.amount < summaryOutputAmount)
	  {
		return error.TransactionValidationError.OUTPUTS_AMOUNT_OVERFLOW;
	  }

	  summaryOutputAmount += output.amount;
	}

	  // parameters used for the additional key_image check
	  Crypto.KeyImage Z = new Crypto.KeyImage({0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00});
	  if (Z == Z)
	  {
	  }
	  Crypto.KeyImage I = new Crypto.KeyImage({0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00});
	  Crypto.KeyImage L = new Crypto.KeyImage({0xed, 0xd3, 0xf5, 0x5c, 0x1a, 0x63, 0x12, 0x58, 0xd6, 0x9c, 0xf7, 0xa2, 0xde, 0xf9, 0xde, 0x14, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10});

	uint64_t summaryInputAmount = 0;
	HashSet<Crypto.KeyImage> ki = new HashSet<Crypto.KeyImage>();
	SortedSet<Tuple<uint64_t, uint32_t>> outputsUsage = new SortedSet<Tuple<uint64_t, uint32_t>>();
	foreach (var input in transaction.inputs)
	{
	  uint64_t amount = 0;
//C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
	  if (input.type() == typeid(KeyInput))
	  {
		KeyInput in = boost::get<KeyInput>(input);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: amount = in.amount;
		amount.CopyFrom(in.amount);
		if (!ki.Add(in.keyImage).second)
		{
		  return error.TransactionValidationError.INPUT_IDENTICAL_KEYIMAGES;
		}

		if (in.outputIndexes.Count == 0)
		{
		  return error.TransactionValidationError.INPUT_EMPTY_OUTPUT_USAGE;
		}

		// outputIndexes are packed here, first is absolute, others are offsets to previous,
		// so first can be zero, others can't
	// Fix discovered by Monero Lab and suggested by "fluffypony" (bitcointalk.org)
	if (!(scalarmultKey(in.keyImage, L) == I) && blockIndex > parameters.KEY_IMAGE_CHECKING_BLOCK_INDEX)
	{
	  return error.TransactionValidationError.INPUT_INVALID_DOMAIN_KEYIMAGES;
	}

		if (std::find(++in.outputIndexes.GetEnumerator(), in.outputIndexes.end(), 0) != in.outputIndexes.end())
		{
		  return error.TransactionValidationError.INPUT_IDENTICAL_OUTPUT_INDEXES;
		}
	  }
	  else
	  {
		return error.TransactionValidationError.INPUT_UNKNOWN_TYPE;
	  }

	  if (uint64_t.MaxValue - amount < summaryInputAmount)
	  {
		return error.TransactionValidationError.INPUTS_AMOUNT_OVERFLOW;
	  }

	  summaryInputAmount += amount;
	}

	if (summaryOutputAmount > summaryInputAmount)
	{
	  return error.TransactionValidationError.WRONG_AMOUNT;
	}

	Debug.Assert(transaction.signatures.Count == transaction.inputs.Count);
	fee = summaryInputAmount - summaryOutputAmount;
	return error.TransactionValidationError.VALIDATION_SUCCESS;
  }
  private std::error_code validateTransaction(CachedTransaction cachedTransaction, TransactionValidatorState state, IBlockchainCache cache, uint64_t fee, uint32_t blockIndex)
  {
	// TransactionValidatorState currentState;
	auto transaction = cachedTransaction.getTransaction();
  var error = validateSemantic(transaction, ref fee, new uint32_t(blockIndex));
	if (error != error.TransactionValidationError.VALIDATION_SUCCESS)
	{
	  return error;
	}

	size_t inputIndex = 0;
	foreach (var input in transaction.inputs)
	{
//C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
	  if (input.type() == typeid(KeyInput))
	  {
		KeyInput in = boost::get<KeyInput>(input);
		if (!state.spentKeyImages.Add(in.keyImage).second)
		{
		  return error.TransactionValidationError.INPUT_KEYIMAGE_ALREADY_SPENT;
		}

		if (!checkpoints.isInCheckpointZone(blockIndex + 1))
		{
		  if (cache.checkIfSpent(in.keyImage, new uint32_t(blockIndex)))
		  {
			return error.TransactionValidationError.INPUT_KEYIMAGE_ALREADY_SPENT;
		  }

		  List<PublicKey> outputKeys = new List<PublicKey>();
		  Debug.Assert(in.outputIndexes.Count > 0);

		  List<uint32_t> globalIndexes = new List<uint32_t>(in.outputIndexes.Count);
		  globalIndexes[0] = in.outputIndexes[0];
		  for (size_t i = 1; i < in.outputIndexes.Count; ++i)
		  {
			globalIndexes[i] = globalIndexes[i - 1] + in.outputIndexes[i];
		  }

		  var result = cache.extractKeyOutputKeys(new uint64_t(in.amount), new uint32_t(blockIndex), new Common.ArrayView<uint32_t>(globalIndexes.data(), globalIndexes.Count), outputKeys);
		  if (result == ExtractOutputKeysResult.INVALID_GLOBAL_INDEX)
		  {
			return error.TransactionValidationError.INPUT_INVALID_GLOBAL_INDEX;
		  }

		  if (result == ExtractOutputKeysResult.OUTPUT_LOCKED)
		  {
			return error.TransactionValidationError.INPUT_SPEND_LOCKED_OUT;
		  }

		  List<Crypto.PublicKey> outputKeyPointers = new List<Crypto.PublicKey>();
		  outputKeyPointers.Capacity = outputKeys.Count;
		  outputKeys.ForEach((Crypto.PublicKey key) =>
		  {
			  outputKeyPointers.Add(key);
		  });
		  if (!Crypto.check_ring_signature(cachedTransaction.getTransactionPrefixHash(), in.keyImage, outputKeyPointers.data(), outputKeyPointers.Count, transaction.signatures[inputIndex].data(), blockIndex > parameters.KEY_IMAGE_CHECKING_BLOCK_INDEX))
		  {
			return error.TransactionValidationError.INPUT_INVALID_SIGNATURES;
		  }
		}

	  }
	  else
	  {
		Debug.Assert(false);
		return error.TransactionValidationError.INPUT_UNKNOWN_TYPE;
	  }

	  inputIndex++;
	}

	return error.TransactionValidationError.VALIDATION_SUCCESS;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint32_t findBlockchainSupplement(const ClassicVector<Crypto::Hash>& remoteBlockIds) const
  private uint32_t findBlockchainSupplement(List<Crypto.Hash> remoteBlockIds)
  {
	// TODO: check for genesis blocks match
	foreach (var hash in remoteBlockIds)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: IBlockchainCache* blockchainSegment = findMainChainSegmentContainingBlock(hash);
	  IBlockchainCache blockchainSegment = findMainChainSegmentContainingBlock(new Crypto.Hash(hash));
	  if (blockchainSegment != null)
	  {
		return blockchainSegment.getBlockIndex(hash);
	  }
	}

	throw new System.Exception("Genesis block hash was not found.");
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<Crypto::Hash> getBlockHashes(uint32_t startBlockIndex, uint32_t maxCount) const
  private List<Crypto.Hash> getBlockHashes(uint32_t startBlockIndex, uint32_t maxCount)
  {
	return chainsLeaves[0].getBlockHashes(new uint32_t(startBlockIndex), new uint32_t(maxCount));
  }

  private std::error_code validateBlock(CachedBlock cachedBlock, IBlockchainCache cache, ref uint64_t minerReward)
  {
	auto block = cachedBlock.getBlock();
	var previousBlockIndex = cache.getBlockIndex(block.previousBlockHash);
	// assert(block.previousBlockHash == cache->getBlockHash(previousBlockIndex));

	minerReward = 0;

	if (upgradeManager.getBlockMajorVersion(cachedBlock.getBlockIndex()) != block.majorVersion)
	{
	  return error.BlockValidationError.WRONG_VERSION;
	}

	if (block.majorVersion >= BLOCK_MAJOR_VERSION_2)
	{
	  if (block.majorVersion == BLOCK_MAJOR_VERSION_2 && block.parentBlock.majorVersion > BLOCK_MAJOR_VERSION_1)
	  {
		logger.functorMethod(Logging.Level.ERROR, Logging.BRIGHT_RED) << "Parent block of block " << cachedBlock.getBlockHash() << " has wrong major version: " << (int)block.parentBlock.majorVersion << ", at index " << cachedBlock.getBlockIndex() << " expected version is " << (int)BLOCK_MAJOR_VERSION_1;
		return error.BlockValidationError.PARENT_BLOCK_WRONG_VERSION;
	  }

	  if (cachedBlock.getParentBlockBinaryArray(false).size() > 2048)
	  {
		return error.BlockValidationError.PARENT_BLOCK_SIZE_TOO_BIG;
	  }
	}

	if (block.timestamp > getAdjustedTime() + currency.blockFutureTimeLimit(previousBlockIndex + 1))
	{
	  return error.BlockValidationError.TIMESTAMP_TOO_FAR_IN_FUTURE;
	}

//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: auto timestamps = cache->getLastTimestamps(currency.timestampCheckWindow(previousBlockIndex+1), previousBlockIndex, addGenesisBlock);
	var timestamps = cache.getLastTimestamps(currency.timestampCheckWindow(previousBlockIndex + 1), new uint32_t(previousBlockIndex), new CryptoNote.UseGenesis(GlobalMembers.addGenesisBlock));
	if (timestamps.Count >= currency.timestampCheckWindow(previousBlockIndex + 1))
	{
	  var median_ts = Common.GlobalMembers.medianValue(timestamps);
	  if (block.timestamp < median_ts)
	  {
		return error.BlockValidationError.TIMESTAMP_TOO_FAR_IN_PAST;
	  }
	}

	if (block.baseTransaction.inputs.size() != 1)
	{
	  return error.TransactionValidationError.INPUT_WRONG_COUNT;
	}

//C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
	if (block.baseTransaction.inputs[0].type() != typeid(BaseInput))
	{
	  return error.TransactionValidationError.INPUT_UNEXPECTED_TYPE;
	}

	if (boost::get<BaseInput>(block.baseTransaction.inputs[0]).blockIndex != previousBlockIndex + 1)
	{
	  return error.TransactionValidationError.BASE_INPUT_WRONG_BLOCK_INDEX;
	}

	if (!(block.baseTransaction.unlockTime == previousBlockIndex + 1 + currency.minedMoneyUnlockWindow()))
	{
	  return error.TransactionValidationError.WRONG_TRANSACTION_UNLOCK_TIME;
	}

	foreach (var output in block.baseTransaction.outputs)
	{
	  if (output.amount == 0)
	  {
		return error.TransactionValidationError.OUTPUT_ZERO_AMOUNT;
	  }

//C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
	  if (output.target.type() == typeid(KeyOutput))
	  {
		if (!check_key(boost::get<KeyOutput>(output.target).key))
		{
		  return error.TransactionValidationError.OUTPUT_INVALID_KEY;
		}
	  }
	  else
	  {
		return error.TransactionValidationError.OUTPUT_UNKNOWN_TYPE;
	  }

	  if (uint64_t.MaxValue - output.amount < minerReward)
	  {
		return error.TransactionValidationError.OUTPUTS_AMOUNT_OVERFLOW;
	  }

	  minerReward += output.amount;
	}

	return error.BlockValidationError.VALIDATION_SUCCESS;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t getAdjustedTime() const
  private uint64_t getAdjustedTime()
  {
	return time(null);
  }
  private void updateMainChainSet()
  {
	mainChainSet.Clear();
	IBlockchainCache chainPtr = chainsLeaves[0];
	Debug.Assert(chainPtr != null);
	do
	{
	  mainChainSet.Add(chainPtr);
	  chainPtr = chainPtr.getParent();
	} while (chainPtr != null);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: IBlockchainCache* findSegmentContainingBlock(const Crypto::Hash& blockHash) const
  private IBlockchainCache findSegmentContainingBlock(Crypto.Hash blockHash)
  {
	Debug.Assert(chainsLeaves.Count > 0);

	// first search in main chain
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: auto blockSegment = findMainChainSegmentContainingBlock(blockHash);
	var blockSegment = findMainChainSegmentContainingBlock(new Crypto.Hash(blockHash));
	if (blockSegment != null)
	{
	  return blockSegment;
	}

	// than search in alternative chains
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: return findAlternativeSegmentContainingBlock(blockHash);
	return findAlternativeSegmentContainingBlock(new Crypto.Hash(blockHash));
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: IBlockchainCache* findSegmentContainingBlock(uint32_t blockHeight) const
  private IBlockchainCache findSegmentContainingBlock(uint32_t blockHeight)
  {
	Debug.Assert(chainsLeaves.Count > 0);

	// first search in main chain
	var blockSegment = findMainChainSegmentContainingBlock(new uint32_t(blockHeight));
	if (blockSegment != null)
	{
	  return blockSegment;
	}

	// than search in alternative chains
	return findAlternativeSegmentContainingBlock(new uint32_t(blockHeight));
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: IBlockchainCache* findMainChainSegmentContainingBlock(const Crypto::Hash& blockHash) const
  private IBlockchainCache findMainChainSegmentContainingBlock(Crypto.Hash blockHash)
  {
	return GlobalMembers.findIndexInChain(new List<IBlockchainCache>(chainsLeaves[0]), blockHash);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: IBlockchainCache* findAlternativeSegmentContainingBlock(const Crypto::Hash& blockHash) const
  private IBlockchainCache findAlternativeSegmentContainingBlock(Crypto.Hash blockHash)
  {
	IBlockchainCache cache = null;
	std::find_if(++chainsLeaves.GetEnumerator(), chainsLeaves.end(), (IBlockchainCache chain) =>
	{
		return cache = findIndexInChain(chain, blockHash);
	});
	return cache;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: IBlockchainCache* findMainChainSegmentContainingBlock(uint32_t blockIndex) const
  private IBlockchainCache findMainChainSegmentContainingBlock(uint32_t blockIndex)
  {
	return GlobalMembers.findIndexInChain(new List<IBlockchainCache>(chainsLeaves[0]), blockIndex);
  }

  // WTF?! this function returns first chain it is able to find..
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: IBlockchainCache* findAlternativeSegmentContainingBlock(uint32_t blockIndex) const
  private IBlockchainCache findAlternativeSegmentContainingBlock(uint32_t blockIndex)
  {
	IBlockchainCache cache = null;
	std::find_if(++chainsLeaves.GetEnumerator(), chainsLeaves.end(), (IBlockchainCache chain) =>
	{
		return cache = findIndexInChain(chain, blockIndex);
	});
	return null;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: IBlockchainCache* findSegmentContainingTransaction(const Crypto::Hash& transactionHash) const
  private IBlockchainCache findSegmentContainingTransaction(Crypto.Hash transactionHash)
  {
	Debug.Assert(chainsLeaves.Count > 0);
	Debug.Assert(chainsStorage.Count > 0);

	IBlockchainCache segment = chainsLeaves[0];
	Debug.Assert(segment != null);

	//find in main chain
	do
	{
	  if (segment.hasTransaction(transactionHash))
	  {
		return segment;
	  }

	  segment = segment.getParent();
	} while (segment != null);

	//find in alternative chains
	for (size_t chain = 1; chain < chainsLeaves.Count; ++chain)
	{
	  segment = chainsLeaves[chain];

	  while (mainChainSet.count(segment) == 0)
	  {
		if (segment.hasTransaction(transactionHash))
		{
		  return segment;
		}

		segment = segment.getParent();
	  }
	}

	return null;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: BlockTemplate restoreBlockTemplate(IBlockchainCache* blockchainCache, uint32_t blockIndex) const
  private BlockTemplate restoreBlockTemplate(IBlockchainCache blockchainCache, uint32_t blockIndex)
  {
	RawBlock rawBlock = blockchainCache.getBlockByIndex(new uint32_t(blockIndex));

	BlockTemplate block = new BlockTemplate();
	if (!CryptoNote.GlobalMembers.fromBinaryArray(ref block, rawBlock.block))
	{
	  throw new System.Exception("Coulnd't deserialize BlockTemplate");
	}

	return block;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<Crypto::Hash> doBuildSparseChain(const Crypto::Hash& blockHash) const
  private List<Crypto.Hash> doBuildSparseChain(Crypto.Hash blockHash)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: IBlockchainCache* chain = findSegmentContainingBlock(blockHash);
	IBlockchainCache chain = findSegmentContainingBlock(new Crypto.Hash(blockHash));

	uint32_t blockIndex = chain.getBlockIndex(blockHash);

	// TODO reserve ceil(log(blockIndex))
	List<Crypto.Hash> sparseChain = new List<Crypto.Hash>();
	sparseChain.Add(blockHash);

	for (uint32_t i = 1; i < blockIndex; i *= 2)
	{
	  sparseChain.Add(chain.getBlockHash(blockIndex - i));
	}

	var genesisBlockHash = chain.getBlockHash(0);
	if (sparseChain[0] != genesisBlockHash)
	{
	  sparseChain.Add(genesisBlockHash);
	}

	return sparseChain;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: RawBlock getRawBlock(IBlockchainCache* segment, uint32_t blockIndex) const
  private RawBlock getRawBlock(IBlockchainCache segment, uint32_t blockIndex)
  {
	Debug.Assert(blockIndex >= segment.getStartBlockIndex() != null && blockIndex <= segment.getTopBlockIndex());

	return segment.getBlockByIndex(new uint32_t(blockIndex));
  }


  //TODO: decompose these two methods
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t pushBlockHashes(uint32_t startIndex, uint32_t fullOffset, size_t maxItemsCount, ClassicVector<BlockShortInfo>& entries) const
  private size_t pushBlockHashes(uint32_t startIndex, uint32_t fullOffset, size_t maxItemsCount, List<BlockShortInfo> entries)
  {
	Debug.Assert(fullOffset >= startIndex);

	uint32_t itemsCount = Math.Min(fullOffset - startIndex, (uint32_t)maxItemsCount);
	if (itemsCount == 0)
	{
	  return 0;
	}

	List<Crypto.Hash> blockIds = getBlockHashes(new uint32_t(startIndex), new uint32_t(itemsCount));

	entries.Capacity = entries.Count + blockIds.Count;
	foreach (var blockHash in blockIds)
	{
	  BlockShortInfo entry = new BlockShortInfo();
	  entry.blockId = std::move(blockHash);
	  entries.emplace_back(std::move(entry));
	}

	return blockIds.Count;
  }

  //TODO: decompose these two methods
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t pushBlockHashes(uint32_t startIndex, uint32_t fullOffset, size_t maxItemsCount, ClassicVector<BlockFullInfo>& entries) const
  private size_t pushBlockHashes(uint32_t startIndex, uint32_t fullOffset, size_t maxItemsCount, List<BlockFullInfo> entries)
  {
	Debug.Assert(fullOffset >= startIndex);

	uint32_t itemsCount = Math.Min(fullOffset - startIndex, (uint32_t)maxItemsCount);
	if (itemsCount == 0)
	{
	  return 0;
	}

	List<Crypto.Hash> blockIds = getBlockHashes(new uint32_t(startIndex), new uint32_t(itemsCount));

	entries.Capacity = entries.Count + blockIds.Count;
	foreach (var blockHash in blockIds)
	{
	  BlockFullInfo entry = new BlockFullInfo();
	  entry.block_id = std::move(blockHash);
	  entries.emplace_back(std::move(entry));
	}

	return blockIds.Count;
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  private bool notifyObservers(BlockchainMessage && msg)
  {
	try
	{
	  foreach (var queue in queueList)
	  {
		queue.push(std::move(msg));
	  }
	  return true;
	}
	catch (System.Exception e)
	{
	  logger.functorMethod(Logging.Level.WARNING) << "failed to notify observers: " << e.Message;
	  return false;
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void fillQueryBlockFullInfo(uint32_t fullOffset, uint32_t currentIndex, size_t maxItemsCount, ClassicVector<BlockFullInfo>& entries) const
  private void fillQueryBlockFullInfo(uint32_t fullOffset, uint32_t currentIndex, size_t maxItemsCount, List<BlockFullInfo> entries)
  {
	Debug.Assert(currentIndex >= fullOffset);

	uint32_t fullBlocksCount = (uint32_t)Math.Min((uint32_t)maxItemsCount, currentIndex - fullOffset);
	entries.Capacity = entries.Count + fullBlocksCount;

	for (uint32_t blockIndex = fullOffset; blockIndex < fullOffset + fullBlocksCount; ++blockIndex)
	{
	  IBlockchainCache segment = findMainChainSegmentContainingBlock(new uint32_t(blockIndex));

	  BlockFullInfo blockFullInfo = new BlockFullInfo();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockFullInfo.block_id = segment->getBlockHash(blockIndex);
	  blockFullInfo.block_id.CopyFrom(segment.getBlockHash(new uint32_t(blockIndex)));
	  (RawBlock)blockFullInfo = getRawBlock(segment, new uint32_t(blockIndex));

	  entries.emplace_back(std::move(blockFullInfo));
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void fillQueryBlockShortInfo(uint32_t fullOffset, uint32_t currentIndex, size_t maxItemsCount, ClassicVector<BlockShortInfo>& entries) const
  private void fillQueryBlockShortInfo(uint32_t fullOffset, uint32_t currentIndex, size_t maxItemsCount, List<BlockShortInfo> entries)
  {
	Debug.Assert(currentIndex >= fullOffset);

	uint32_t fullBlocksCount = (uint32_t)Math.Min((uint32_t)maxItemsCount, currentIndex - fullOffset + 1);
	entries.Capacity = entries.Count + fullBlocksCount;

	for (uint32_t blockIndex = fullOffset; blockIndex < fullOffset + fullBlocksCount; ++blockIndex)
	{
	  IBlockchainCache segment = findMainChainSegmentContainingBlock(new uint32_t(blockIndex));
	  RawBlock rawBlock = getRawBlock(segment, new uint32_t(blockIndex));

	  BlockShortInfo blockShortInfo = new BlockShortInfo();
	  blockShortInfo.block = std::move(rawBlock.block);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockShortInfo.blockId = segment->getBlockHash(blockIndex);
	  blockShortInfo.blockId.CopyFrom(segment.getBlockHash(new uint32_t(blockIndex)));

	  blockShortInfo.txPrefixes.Capacity = rawBlock.transactions.Count;
	  foreach (var rawTransaction in rawBlock.transactions)
	  {
		TransactionPrefixInfo prefixInfo = new TransactionPrefixInfo();
		prefixInfo.txHash = getBinaryArrayHash(rawTransaction); // TODO: is there faster way to get hash without calculation?

		Transaction transaction = new Transaction();
		if (!CryptoNote.GlobalMembers.fromBinaryArray(ref transaction, rawTransaction))
		{
		  // TODO: log it
		  throw new System.Exception("Couldn't deserialize transaction");
		}

		prefixInfo.txPrefix = std::move((TransactionPrefix)transaction);
		blockShortInfo.txPrefixes.emplace_back(std::move(prefixInfo));
	  }

	  entries.emplace_back(std::move(blockShortInfo));
	}
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void getTransactionPoolDifference(const ClassicVector<Crypto::Hash>& knownHashes, ClassicVector<Crypto::Hash>& newTransactions, ClassicVector<Crypto::Hash>& deletedTransactions) const
  private void getTransactionPoolDifference(List<Crypto.Hash> knownHashes, List<Crypto.Hash> newTransactions, List<Crypto.Hash> deletedTransactions)
  {
	var t = transactionPool.getTransactionHashes();

	HashSet<Crypto.Hash> poolTransactions = new HashSet<Crypto.Hash>(t.begin(), t.end());
	HashSet<Crypto.Hash> knownTransactions = new HashSet<Crypto.Hash>(knownHashes.GetEnumerator(), knownHashes.end());

	for (var it = poolTransactions.GetEnumerator(), end = poolTransactions.end(); it != end;)
	{
	  var knownTransactionIt = knownTransactions.find(*it);
	  if (knownTransactionIt != knownTransactions.end())
	  {
		knownTransactions.erase(knownTransactionIt);
		it = poolTransactions.erase(it);
	  }
	  else
	  {
		++it;
	  }
	}

	newTransactions.assign(poolTransactions.GetEnumerator(), poolTransactions.end());
	deletedTransactions.assign(knownTransactions.GetEnumerator(), knownTransactions.end());
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint8_t getBlockMajorVersionForHeight(uint32_t height) const
  private uint8_t getBlockMajorVersionForHeight(uint32_t height)
  {
	return upgradeManager.getBlockMajorVersion(height);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t calculateCumulativeBlocksizeLimit(uint32_t height) const
  private size_t calculateCumulativeBlocksizeLimit(uint32_t height)
  {
	uint8_t nextBlockMajorVersion = getBlockMajorVersionForHeight(new uint32_t(height));
	size_t nextBlockGrantedFullRewardZone = currency.blockGrantedFullRewardZoneByBlockVersion(new uint8_t(nextBlockMajorVersion));

	Debug.Assert(chainsStorage.Count > 0);
	Debug.Assert(chainsLeaves.Count > 0);
	// FIXME: skip gensis here?
	var sizes = chainsLeaves[0].getLastBlocksSizes(currency.rewardBlocksWindow());
	uint64_t median = Common.GlobalMembers.medianValue(sizes);
	if (median <= nextBlockGrantedFullRewardZone)
	{
	  median = nextBlockGrantedFullRewardZone;
	}

	return median * 2;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void fillBlockTemplate(BlockTemplate& block, size_t medianSize, size_t maxCumulativeSize, size_t& transactionsSize, uint64_t& fee) const
  private void fillBlockTemplate(BlockTemplate block, size_t medianSize, size_t maxCumulativeSize, ref size_t transactionsSize, ref uint64_t fee)
  {
	transactionsSize = 0;
	fee = 0;

	size_t maxTotalSize = (125 * medianSize) / 100;
	maxTotalSize = Math.Min(maxTotalSize, maxCumulativeSize) - currency.minerTxBlobReservedSize();

	TransactionSpentInputsChecker spentInputsChecker = new TransactionSpentInputsChecker();

	List<CachedTransaction> poolTransactions = transactionPool.getPoolTransactions();
	for (var it = poolTransactions.rbegin(); it != poolTransactions.rend() && it.getTransactionFee() == 0; ++it)
	{
//C++ TO C# CONVERTER TODO TASK: C# does not have an equivalent to references to variables:
//ORIGINAL LINE: const CachedTransaction& transaction = *it;
	  CachedTransaction transaction = it;

	  var transactionBlobSize = transaction.getTransactionBinaryArray().size();
	  if (currency.fusionTxMaxSize() < transactionsSize + transactionBlobSize)
	  {
		continue;
	  }

	  if (!spentInputsChecker.haveSpentInputs(transaction.getTransaction()))
	  {
		block.transactionHashes.emplace_back(transaction.getTransactionHash());
		transactionsSize += transactionBlobSize;
		logger.functorMethod(Logging.Level.TRACE) << "Fusion transaction " << transaction.getTransactionHash() << " included to block template";
	  }
	}

	foreach (var cachedTransaction in poolTransactions)
	{
	  size_t blockSizeLimit = (cachedTransaction.getTransactionFee() == 0) ? medianSize : maxTotalSize;

	  if (blockSizeLimit < transactionsSize + cachedTransaction.getTransactionBinaryArray().size())
	  {
		continue;
	  }

	  if (!spentInputsChecker.haveSpentInputs(cachedTransaction.getTransaction()))
	  {
		transactionsSize += cachedTransaction.getTransactionBinaryArray().size();
		fee += cachedTransaction.getTransactionFee();
		block.transactionHashes.emplace_back(cachedTransaction.getTransactionHash());
		logger.functorMethod(Logging.Level.TRACE) << "Transaction " << cachedTransaction.getTransactionHash() << " included to block template";
	  }
	  else
	  {
		logger.functorMethod(Logging.Level.TRACE) << "Transaction " << cachedTransaction.getTransactionHash() << " is failed to include to block template";
	  }
	}
  }
  private void deleteAlternativeChains()
  {
	while (chainsLeaves.Count > 1)
	{
	  deleteLeaf(1);
	}
  }
  private void deleteLeaf(size_t leafIndex)
  {
	Debug.Assert(leafIndex < chainsLeaves.Count);

	IBlockchainCache leaf = chainsLeaves[leafIndex];

	IBlockchainCache parent = leaf.getParent();
	if (parent != null)
	{
	  bool r = parent.deleteChild(leaf);
	  if (r)
	  {
	  }
	  Debug.Assert(r);
	}

//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
	var segmentIt = std::find_if(chainsStorage.GetEnumerator(), chainsStorage.end(), (std::unique_ptr<IBlockchainCache> segment) =>
	{
		return segment.get() == leaf;
	});

	Debug.Assert(segmentIt != chainsStorage.end());

	if (leafIndex != 0)
	{
	  if (parent.getChildCount() == 0)
	  {
		chainsLeaves.Add(parent);
	  }

	  chainsLeaves.RemoveAt(leafIndex);
	}
	else
	{
	  if (parent != null)
	  {
		chainsLeaves[0] = parent;
	  }
	  else
	  {
//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'erase' method in C#:
		chainsLeaves.erase(chainsLeaves.GetEnumerator());
	  }
	}

//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'erase' method in C#:
	chainsStorage.erase(segmentIt);
  }
  private void mergeMainChainSegments()
  {
	Debug.Assert(chainsStorage.Count > 0);
	Debug.Assert(chainsLeaves.Count > 0);

	List<IBlockchainCache> chain = new List<IBlockchainCache>();
	IBlockchainCache segment = chainsLeaves[0];
	while (segment != null)
	{
	  chain.Add(segment);
	  segment = segment.getParent();
	}

	IBlockchainCache rootSegment = chain[chain.Count - 1];
	for (var it = ++chain.rbegin(); it != chain.rend(); ++it)
	{
	  mergeSegments(rootSegment, *it);
	}

//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
	var rootIt = std::find_if(chainsStorage.GetEnumerator(), chainsStorage.end(), (std::unique_ptr<IBlockchainCache> segment) =>
	{
		return segment.get() == rootSegment;
	});

	Debug.Assert(rootIt != chainsStorage.end());

	if (rootIt != chainsStorage.GetEnumerator())
	{
	  *chainsStorage.GetEnumerator() = std::move(*rootIt);
	}

//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'erase' method in C#:
	chainsStorage.erase(++chainsStorage.GetEnumerator(), chainsStorage.end());
	chainsLeaves.Clear();
	chainsLeaves.Add(chainsStorage.GetEnumerator().get());
  }
  private void mergeSegments(IBlockchainCache acceptingSegment, IBlockchainCache segment)
  {
	Debug.Assert(segment.getStartBlockIndex() == acceptingSegment.getStartBlockIndex() + acceptingSegment.getBlockCount());

	var startIndex = segment.getStartBlockIndex();
	var blockCount = segment.getBlockCount();
	for (var blockIndex = startIndex; blockIndex < startIndex + blockCount; ++blockIndex)
	{
	  PushedBlockInfo info = segment.getPushedBlockInfo(new auto(blockIndex));

	  BlockTemplate block = new BlockTemplate();
	  if (!CryptoNote.GlobalMembers.fromBinaryArray(ref block, info.rawBlock.block))
	  {
		logger.functorMethod(Logging.Level.WARNING) << "mergeSegments error: Couldn't deserialize block";
		throw new System.Exception("Couldn't deserialize block");
	  }

	  List<CachedTransaction> transactions = new List<CachedTransaction>();
	  if (!Utils.restoreCachedTransactions(info.rawBlock.transactions, transactions))
	  {
		logger.functorMethod(Logging.Level.WARNING) << "mergeSegments error: Couldn't deserialize transactions";
		throw new System.Exception("Couldn't deserialize transactions");
	  }

	  acceptingSegment.pushBlock(new CachedBlock(block), transactions, info.validatorState, new size_t(info.blockSize), new uint64_t(info.generatedCoins), new uint64_t(info.blockDifficulty), std::move(info.rawBlock));
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: TransactionDetails getTransactionDetails(const Crypto::Hash& transactionHash, IBlockchainCache* segment, bool foundInPool) const
  private TransactionDetails getTransactionDetails(Crypto.Hash transactionHash, IBlockchainCache segment, bool foundInPool)
  {
	Debug.Assert((segment != null) != foundInPool);
	if (segment == null)
	{
	  segment = chainsLeaves[0];
	}

	std::unique_ptr<ITransaction> transaction = new std::unique_ptr<ITransaction>();
	Transaction rawTransaction = new Transaction();
	TransactionDetails transactionDetails = new TransactionDetails();
	if (!foundInPool)
	{
	  List<Crypto.Hash> transactionsHashes = new List<Crypto.Hash>();
	  List<List<uint8_t>> rawTransactions = new List<List<uint8_t>>();
	  List<Crypto.Hash> missedTransactionsHashes = new List<Crypto.Hash>();
	  transactionsHashes.Add(transactionHash);

	  segment.getRawTransactions(transactionsHashes, rawTransactions, missedTransactionsHashes);
	  Debug.Assert(missedTransactionsHashes.Count == 0);
	  Debug.Assert(rawTransactions.Count == 1);

	  List<CachedTransaction> transactions = new List<CachedTransaction>();
	  Utils.restoreCachedTransactions(rawTransactions, transactions);
	  Debug.Assert(transactions.Count == 1);

	  transactionDetails.inBlockchain = true;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: transactionDetails.blockIndex = segment->getBlockIndexContainingTx(transactionHash);
	  transactionDetails.blockIndex.CopyFrom(segment.getBlockIndexContainingTx(transactionHash));
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: transactionDetails.blockHash = segment->getBlockHash(transactionDetails.blockIndex);
	  transactionDetails.blockHash.CopyFrom(segment.getBlockHash(new uint32_t(transactionDetails.blockIndex)));

//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: auto timestamps = segment->getLastTimestamps(1, transactionDetails.blockIndex, addGenesisBlock);
	  var timestamps = segment.getLastTimestamps(1, new uint32_t(transactionDetails.blockIndex), new CryptoNote.UseGenesis(GlobalMembers.addGenesisBlock));
	  Debug.Assert(timestamps.Count == 1);
	  transactionDetails.timestamp = timestamps[timestamps.Count - 1];

	  transactionDetails.size = transactions[transactions.Count - 1].getTransactionBinaryArray().size();
	  transactionDetails.fee = transactions[transactions.Count - 1].getTransactionFee();

	  rawTransaction = transactions[transactions.Count - 1].getTransaction();
	  transaction = createTransaction(rawTransaction);
	}
	else
	{
	  transactionDetails.inBlockchain = false;
	  transactionDetails.timestamp = transactionPool.getTransactionReceiveTime(transactionHash);

	  transactionDetails.size = transactionPool.getTransaction(transactionHash).getTransactionBinaryArray().size();
	  transactionDetails.fee = transactionPool.getTransaction(transactionHash).getTransactionFee();

	  rawTransaction = transactionPool.getTransaction(transactionHash).getTransaction();
	  transaction = createTransaction(rawTransaction);
	}

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: transactionDetails.hash = transactionHash;
	transactionDetails.hash.CopyFrom(transactionHash);
	transactionDetails.unlockTime = transaction.getUnlockTime();

	transactionDetails.totalOutputsAmount = transaction.getOutputTotalAmount();
	transactionDetails.totalInputsAmount = transaction.getInputTotalAmount();

	transactionDetails.mixin = 0;
	for (size_t i = 0; i < transaction.getInputCount(); ++i)
	{
	  if (transaction.getInputType(i) != TransactionTypes.InputType.Key)
	  {
		continue;
	  }

	  KeyInput input = new KeyInput();
	  transaction.getInput(i, input);
	  uint64_t currentMixin = input.outputIndexes.Count;
	  if (currentMixin > transactionDetails.mixin)
	  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: transactionDetails.mixin = currentMixin;
		transactionDetails.mixin.CopyFrom(currentMixin);
	  }
	}

	transactionDetails.paymentId = boost::value_initialized<Crypto.Hash>();
	if (transaction.getPaymentId(transactionDetails.paymentId))
	{
	  transactionDetails.hasPaymentId = true;
	}
	transactionDetails.extra.publicKey = transaction.getTransactionPublicKey();
	transaction.getExtraNonce(transactionDetails.extra.nonce);

	transactionDetails.signatures = new List<List<Crypto.Signature>>(rawTransaction.signatures);

	transactionDetails.inputs.Capacity = transaction.getInputCount();
	for (size_t i = 0; i < transaction.getInputCount(); ++i)
	{
	  boost::variant<BaseInputDetails, KeyInputDetails> txInDetails = new boost::variant<BaseInputDetails, KeyInputDetails>();

	  if (transaction.getInputType(i) == TransactionTypes.InputType.Generating)
	  {
		BaseInputDetails baseDetails = new BaseInputDetails();
		baseDetails.input = boost::get<BaseInput>(rawTransaction.inputs[i]);
		baseDetails.amount = transaction.getOutputTotalAmount();
		txInDetails = baseDetails;
	  }
	  else if (transaction.getInputType(i) == TransactionTypes.InputType.Key)
	  {
		KeyInputDetails txInToKeyDetails = new KeyInputDetails();
		txInToKeyDetails.input = boost::get<KeyInput>(rawTransaction.inputs[i]);
		List<Tuple<Crypto.Hash, size_t>> outputReferences = new List<Tuple<Crypto.Hash, size_t>>();
		outputReferences.Capacity = txInToKeyDetails.input.outputIndexes.Count;
		List<uint32_t> globalIndexes = relativeOutputOffsetsToAbsolute(txInToKeyDetails.input.outputIndexes);
		ExtractOutputKeysResult result = segment.extractKeyOtputReferences(new uint64_t(txInToKeyDetails.input.amount), new Common.ArrayView<uint32_t>(globalIndexes.data(), globalIndexes.Count), outputReferences);
		if (result == result)
		{
		}
		Debug.Assert(result == ExtractOutputKeysResult.SUCCESS);
		Debug.Assert(txInToKeyDetails.input.outputIndexes.Count == outputReferences.Count);

		txInToKeyDetails.mixin = txInToKeyDetails.input.outputIndexes.Count;
		txInToKeyDetails.output.number = outputReferences[outputReferences.Count - 1].Item2;
		txInToKeyDetails.output.transactionHash = outputReferences[outputReferences.Count - 1].Item1;
		txInDetails = txInToKeyDetails;
	  }

	  Debug.Assert(!txInDetails.empty());
	  transactionDetails.inputs.Add(std::move(txInDetails));
	}

	transactionDetails.outputs.Capacity = transaction.getOutputCount();
	List<uint32_t> globalIndexes = new List<uint32_t>();
	globalIndexes.Capacity = transaction.getOutputCount();
	if (!transactionDetails.inBlockchain || !getTransactionGlobalIndexes(transactionDetails.hash, globalIndexes))
	{
	  for (size_t i = 0; i < transaction.getOutputCount(); ++i)
	  {
		globalIndexes.Add(0);
	  }
	}

	Debug.Assert(transaction.getOutputCount() == globalIndexes.Count);
	for (size_t i = 0; i < transaction.getOutputCount(); ++i)
	{
	  TransactionOutputDetails txOutDetails = new TransactionOutputDetails();
	  txOutDetails.output = rawTransaction.outputs[i];
	  txOutDetails.globalIndex = globalIndexes[i];
	  transactionDetails.outputs.Add(std::move(txOutDetails));
	}

	return transactionDetails;
  }
  private void notifyOnSuccess(error.AddBlockErrorCode opResult, uint32_t previousBlockIndex, CachedBlock cachedBlock, IBlockchainCache cache)
  {
	switch (opResult)
	{
	  case error.AddBlockErrorCode.ADDED_TO_MAIN:
		notifyObservers(makeNewBlockMessage(previousBlockIndex + 1, cachedBlock.getBlockHash()));
		break;
	  case error.AddBlockErrorCode.ADDED_TO_ALTERNATIVE:
		notifyObservers(makeNewAlternativeBlockMessage(previousBlockIndex + 1, cachedBlock.getBlockHash()));
		break;
	  case error.AddBlockErrorCode.ADDED_TO_ALTERNATIVE_AND_SWITCHED:
	  {
		var parent = cache.getParent();
		var hashes = cache.getBlockHashes(cache.getStartBlockIndex(), cache.getBlockCount());
//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'insert' method in C#:
		hashes.insert(hashes.GetEnumerator(), parent.getTopBlockHash());
		notifyObservers(makeChainSwitchMessage(parent.getTopBlockIndex(), std::move(hashes)));
		break;
	  }
	  default:
		Debug.Assert(false);
		break;
	}
  }
  private void copyTransactionsToPool(IBlockchainCache alt)
  {
	Debug.Assert(alt != null);
	while (alt != null)
	{
	  if (mainChainSet.count(alt) != 0)
	  {
		break;
	  }
	  var transactions = alt.getRawTransactions(alt.getTransactionHashes());
	  foreach (var transaction in transactions)
	  {
		if (addTransactionToPool(std::move(transaction)))
		{
		  // TODO: send notification
		}
	  }
	  alt = alt.getParent();
	}
  }

  private void actualizePoolTransactions()
  {
	auto pool = transactionPool;
	var hashes = pool.getTransactionHashes();

	foreach (var hash in hashes)
	{
	  var tx = pool.getTransaction(hash);
	  pool.removeTransaction(hash);

	  if (!addTransactionToPool(std::move(tx)))
	  {
		notifyObservers(makeDelTransactionMessage({hash}, Messages.DeleteTransaction.Reason.NotActual));
	  }
	}
  }
  private void actualizePoolTransactionsLite(TransactionValidatorState validatorState)
  {
	auto pool = transactionPool;
	var hashes = pool.getTransactionHashes();

	foreach (var hash in hashes)
	{
	  var tx = pool.getTransaction(hash);

	  var txState = GlobalMembers.extractSpentOutputs(tx);

	  if (hasIntersections(validatorState, txState) || tx.getTransactionBinaryArray().size() > GlobalMembers.getMaximumTransactionAllowedSize(new size_t(blockMedianSize), currency))
	  {
		pool.removeTransaction(hash);
		notifyObservers(makeDelTransactionMessage({hash}, Messages.DeleteTransaction.Reason.NotActual));
	  }
	}
  }

  private void transactionPoolCleaningProcedure()
  {
	System.Timer timer = new System.Timer(dispatcher);

	try
	{
	  for (;;)
	  {
		timer.sleep(GlobalMembers.OUTDATED_TRANSACTION_POLLING_INTERVAL);

		var deletedTransactions = transactionPool.clean(getTopBlockIndex());
		notifyObservers(makeDelTransactionMessage(std::move(deletedTransactions), Messages.DeleteTransaction.Reason.Outdated));
	  }
	}
	catch (System.InterruptedException)
	{
	  logger.functorMethod(Logging.Level.DEBUGGING) << "transactionPoolCleaningProcedure has been interrupted";
	}
	catch (System.Exception e)
	{
	  logger.functorMethod(Logging.Level.ERROR) << "Error occurred while cleaning transactions pool: " << e.Message;
	}
  }
  private void updateBlockMedianSize()
  {
	var mainChain = chainsLeaves[0];

	size_t nextBlockGrantedFullRewardZone = currency.blockGrantedFullRewardZoneByBlockVersion(upgradeManager.getBlockMajorVersion(mainChain.getTopBlockIndex() + 1));

	var lastBlockSizes = mainChain.getLastBlocksSizes(currency.rewardBlocksWindow());

	blockMedianSize = Math.Max(Common.GlobalMembers.medianValue(lastBlockSizes), (uint64_t)nextBlockGrantedFullRewardZone);
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  private bool addTransactionToPool(CachedTransaction && cachedTransaction)
  {
	TransactionValidatorState validatorState = new TransactionValidatorState();

	if (!isTransactionValidForPool(cachedTransaction, validatorState))
	{
	  return false;
	}

	var transactionHash = cachedTransaction.getTransactionHash();

	if (!transactionPool.pushTransaction(std::move(cachedTransaction), std::move(validatorState)))
	{
	  logger.functorMethod(Logging.Level.DEBUGGING) << "Failed to push transaction " << transactionHash << " to pool, already exists";
	  return false;
	}

	logger.functorMethod(Logging.Level.DEBUGGING) << "Transaction " << transactionHash << " has been added to pool";
	return true;
  }
  private bool isTransactionValidForPool(CachedTransaction cachedTransaction, TransactionValidatorState validatorState)
  {
	var (success, err) = Mixins.validate(new uint32_t(cachedTransaction), getTopBlockIndex());

	if (!success)
	{
		return false;
	}

	uint64_t fee = new uint64_t();

	if (auto validationResult = validateTransaction(cachedTransaction, validatorState, new List<IBlockchainCache>(chainsLeaves[0]), fee, getTopBlockIndex()))
	{
	  logger.functorMethod(Logging.Level.DEBUGGING) << "Transaction " << cachedTransaction.getTransactionHash() << " is not valid. Reason: " << validationResult.message();
	  return false;
	}

	var maxTransactionSize = GlobalMembers.getMaximumTransactionAllowedSize(new size_t(blockMedianSize), currency);
	if (cachedTransaction.getTransactionBinaryArray().size() > maxTransactionSize)
	{
	  logger.functorMethod(Logging.Level.WARNING) << "Transaction " << cachedTransaction.getTransactionHash() << " is not valid. Reason: transaction is too big (" << cachedTransaction.getTransactionBinaryArray().size() << "). Maximum allowed size is " << maxTransactionSize;
	  return false;
	}

	bool isFusion = fee == 0 && currency.isFusionTransaction(cachedTransaction.getTransaction(), cachedTransaction.getTransactionBinaryArray().size(), getTopBlockIndex());

	if (!isFusion && fee < currency.minimumFee())
	{
	  logger.functorMethod(Logging.Level.WARNING) << "Transaction " << cachedTransaction.getTransactionHash() << " is not valid. Reason: fee is too small and it's not a fusion transaction";
	  return false;
	}

	return true;
  }

  private void initRootSegment()
  {
	std::unique_ptr<IBlockchainCache> cache = this.blockchainCacheFactory.createRootBlockchainCache(currency);

	mainChainSet.emplace(cache.get());

	chainsLeaves.Add(cache.get());
	chainsStorage.Add(std::move(cache));

	contextGroup.spawn(std::bind(this.transactionPoolCleaningProcedure, this));

	updateBlockMedianSize();

	chainsLeaves[0].load();
  }
  private void importBlocksFromStorage()
  {
	uint32_t commonIndex = GlobalMembers.findCommonRoot(*mainChainStorage, *chainsLeaves[0]);
	Debug.Assert(commonIndex <= mainChainStorage.getBlockCount());

	cutSegment(*chainsLeaves[0], commonIndex + 1);

	var previousBlockHash = GlobalMembers.getBlockHash(mainChainStorage.getBlockByIndex(commonIndex));
	var blockCount = mainChainStorage.getBlockCount();
	for (uint32_t i = commonIndex + 1; i < blockCount; ++i)
	{
	  RawBlock rawBlock = mainChainStorage.getBlockByIndex(i);
	  var blockTemplate = GlobalMembers.extractBlockTemplate(rawBlock);
	  CachedBlock cachedBlock = new CachedBlock(blockTemplate);

	  if (blockTemplate.previousBlockHash != previousBlockHash)
	  {
		logger.functorMethod(Logging.Level.ERROR) << "Corrupted blockchain. Block with index " << i << " and hash " << cachedBlock.getBlockHash() << " has previous block hash " << blockTemplate.previousBlockHash << ", but parent has hash " << previousBlockHash << ". Resynchronize your daemon please.";
		throw std::system_error(error.GlobalMembers.make_error_code(error.CoreErrorCode.CORRUPTED_BLOCKCHAIN));
	  }

	  previousBlockHash = cachedBlock.getBlockHash();

	  List<CachedTransaction> transactions = new List<CachedTransaction>();
	  uint64_t cumulativeSize = 0;
	  if (!extractTransactions(rawBlock.transactions, transactions, cumulativeSize))
	  {
		logger.functorMethod(Logging.Level.ERROR) << "Couldn't deserialize raw block transactions in block " << cachedBlock.getBlockHash();
		throw std::system_error(error.GlobalMembers.make_error_code(error.AddBlockErrorCode.DESERIALIZATION_FAILED));
	  }

	  cumulativeSize += CryptoNote.GlobalMembers.getObjectBinarySize(blockTemplate.baseTransaction);
	  TransactionValidatorState spentOutputs = GlobalMembers.extractSpentOutputs(transactions);
	  var currentDifficulty = chainsLeaves[0].getDifficultyForNextBlock(i - 1);

	  uint64_t cumulativeFee = std::accumulate(transactions.GetEnumerator(), transactions.end(), UINT64_C(0), (uint64_t fee, CachedTransaction transaction) =>
	  {
		return fee + transaction.getTransactionFee();
	  });

	  int64_t emissionChange = GlobalMembers.getEmissionChange(currency, *chainsLeaves[0], i - 1, cachedBlock, new uint64_t(cumulativeSize), new uint64_t(cumulativeFee));
	  chainsLeaves[0].pushBlock(cachedBlock, transactions, spentOutputs, new uint64_t(cumulativeSize), new int64_t(emissionChange), new uint64_t(currentDifficulty), std::move(rawBlock));

	  if (i % 1000 == 0 != null)
	  {
		logger.functorMethod(Logging.Level.INFO) << "Imported block with index " << i << " / " << (blockCount - 1);
	  }
	}
  }
  private void cutSegment(IBlockchainCache segment, uint32_t startIndex)
  {
	if (segment.getTopBlockIndex() < startIndex)
	{
	  return;
	}

	logger.functorMethod(Logging.Level.INFO) << "Cutting root segment from index " << startIndex;
	var childCache = segment.split(new uint32_t(startIndex));
	segment.deleteChild(childCache.get());
  }

  private void switchMainChainStorage(uint32_t splitBlockIndex, IBlockchainCache newChain)
  {
	Debug.Assert(mainChainStorage.getBlockCount() > splitBlockIndex);

	var blocksToPop = mainChainStorage.getBlockCount() - splitBlockIndex;
	for (size_t i = 0; i < blocksToPop; ++i)
	{
	  mainChainStorage.popBlock();
	}

	for (uint32_t index = splitBlockIndex; index <= newChain.getTopBlockIndex(); ++index)
	{
	  mainChainStorage.pushBlock(newChain.getBlockByIndex(new uint32_t(index)));
	}
  }
}

}

namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <class T>

public class TransactionSpentInputsChecker
{
  public bool haveSpentInputs(Transaction transaction)
  {
	foreach (var input in transaction.inputs)
	{
//C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
	  if (input.type() == typeid(KeyInput))
	  {
		var inserted = alreadSpentKeyImages.Add(boost::get<KeyInput>(input).keyImage);
		if (!inserted.second)
		{
		  return true;
		}
	  }
	}

	return false;
  }

  private HashSet<Crypto.KeyImage> alreadSpentKeyImages = new HashSet<Crypto.KeyImage>();
}


//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:


//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool Core::getBlockTemplate(BlockTemplate& b, const AccountPublicAddress& adr, const ClassicVector<uint8_t>& extraNonce, uint64_t& difficulty, uint32_t& height) const

}

