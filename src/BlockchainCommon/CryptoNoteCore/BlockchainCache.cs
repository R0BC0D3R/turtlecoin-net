using System;
using System.Collections.Generic;
using System.Diagnostics;

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
// 
// Please see the included LICENSE file for more information.

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

namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class ISerializer;

public class SpentKeyImage
{
  public uint32_t blockIndex = new uint32_t();
  public Crypto.KeyImage keyImage = new Crypto.KeyImage();

  public void serialize(ISerializer s)
  {
	s.functorMethod(blockIndex, "block_index");
	s.functorMethod(keyImage, "key_image");
  }
}

public class CachedTransactionInfo
{
  public uint32_t blockIndex = new uint32_t();
  public uint32_t transactionIndex = new uint32_t();
  public Crypto.Hash transactionHash = new Crypto.Hash();
  public uint64_t unlockTime = new uint64_t();
  public List<TransactionOutputTarget> outputs = new List<TransactionOutputTarget>();
  //needed for getTransactionGlobalIndexes query
  public List<uint32_t> globalIndexes = new List<uint32_t>();

  public void serialize(ISerializer s)
  {
	s.functorMethod(blockIndex, "block_index");
	s.functorMethod(transactionIndex, "transaction_index");
	s.functorMethod(transactionHash, "transaction_hash");
	s.functorMethod(unlockTime, "unlock_time");
	s.functorMethod(outputs, "outputs");
	s.functorMethod(globalIndexes, "global_indexes");
  }
}

public class CachedBlockInfo
{
  public Crypto.Hash blockHash = new Crypto.Hash();
  public uint64_t timestamp = new uint64_t();
  public uint64_t cumulativeDifficulty = new uint64_t();
  public uint64_t alreadyGeneratedCoins = new uint64_t();
  public uint64_t alreadyGeneratedTransactions = new uint64_t();
  public uint32_t blockSize = new uint32_t();

  public void serialize(ISerializer s)
  {
	s.functorMethod(blockHash, "block_hash");
	s.functorMethod(timestamp, "timestamp");
	s.functorMethod(blockSize, "block_size");
	s.functorMethod(cumulativeDifficulty, "cumulative_difficulty");
	s.functorMethod(alreadyGeneratedCoins, "already_generated_coins");
	s.functorMethod(alreadyGeneratedTransactions, "already_generated_transaction_count");
  }
}

public class OutputGlobalIndexesForAmount
{
  public uint32_t startIndex = 0;

  // 1. This container must be sorted by PackedOutIndex::blockIndex and PackedOutIndex::transactionIndex
  // 2. GlobalOutputIndex for particular output is calculated as following: startIndex + index in vector
  public List<PackedOutIndex> outputs = new List<PackedOutIndex>();

  public void serialize(ISerializer s)
  {
	s.functorMethod(startIndex, "start_index");
	s.functorMethod(outputs, "outputs");
  }
}

public class PaymentIdTransactionHashPair
{
  public Crypto.Hash paymentId = new Crypto.Hash();
  public Crypto.Hash transactionHash = new Crypto.Hash();

  public void serialize(ISerializer s)
  {
	s.functorMethod(paymentId, "payment_id");
	s.functorMethod(transactionHash, "transaction_hash");
  }
}

public class BlockchainCache : IBlockchainCache
{
  public BlockchainCache(string filename, Currency currency, Logging.ILogger logger_, IBlockchainCache parent, uint32_t splitBlockIndex = 0)
  {
	  this.filename = filename;
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
	  this.currency = new CryptoNote.Currency(currency);
	  this.logger = new Logging.LoggerRef(logger_, "BlockchainCache");
	  this.parent = parent;
	  this.storage = new BlockchainStorage(100);
	if (parent == null)
	{
	  startIndex = 0;

	  CachedBlock genesisBlock = new CachedBlock(currency.genesisBlock());

	  uint64_t minerReward = 0;
	  foreach (TransactionOutput output in genesisBlock.getBlock().baseTransaction.outputs)
	  {
		minerReward += output.amount;
	  }

	  Debug.Assert(minerReward > 0);

	  uint64_t coinbaseTransactionSize = CryptoNote.GlobalMembers.getObjectBinarySize(genesisBlock.getBlock().baseTransaction);
	  Debug.Assert(coinbaseTransactionSize < uint64_t.MaxValue);

	  List<CachedTransaction> transactions = new List<CachedTransaction>();
	  TransactionValidatorState validatorState = new TransactionValidatorState();
	  doPushBlock(genesisBlock, transactions, validatorState, new uint64_t(coinbaseTransactionSize), new uint64_t(minerReward), 1, new RawBlock(CryptoNote.GlobalMembers.toBinaryArray(genesisBlock.getBlock())));
	}
	else
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: startIndex = splitBlockIndex;
	  startIndex.CopyFrom(splitBlockIndex);
	}

	logger.functorMethod(Logging.Level.DEBUGGING) << "BlockchainCache with start block index: " << startIndex << " created";
  }

  //Returns upper part of segment. [this] remains lower part.
  //All of indexes on blockIndex == splitBlockIndex belong to upper part

  // Returns upper part of segment. [this] remains lower part.
  // All of indexes on blockIndex == splitBlockIndex belong to upper part
  // TODO: first move containers to new cache, then copy elements back. This can be much more effective, cause we usualy
  // split blockchain near its top.
  public override std::unique_ptr<IBlockchainCache> split(uint32_t splitBlockIndex)
  {
	logger.functorMethod(Logging.Level.DEBUGGING) << "Splitting at block index: " << splitBlockIndex << ", top block index: " << getTopBlockIndex();

	Debug.Assert(splitBlockIndex > startIndex);
	Debug.Assert(splitBlockIndex <= getTopBlockIndex());

	std::unique_ptr<BlockchainStorage> newStorage = storage.splitStorage(splitBlockIndex - startIndex);

	std::unique_ptr<BlockchainCache> newCache = new std::unique_ptr<BlockchainCache>(new BlockchainCache(filename, currency, logger.getLogger(), this, new uint32_t(splitBlockIndex)));

	newCache.storage = std::move(newStorage);

	splitSpentKeyImages(*newCache, new uint32_t(splitBlockIndex));
	splitTransactions(*newCache, new uint32_t(splitBlockIndex));
	splitBlocks(*newCache, new uint32_t(splitBlockIndex));
	splitKeyOutputsGlobalIndexes(*newCache, new uint32_t(splitBlockIndex));

	fixChildrenParent(newCache.get());
	newCache.children = children;
	children = new List<IBlockchainCache>() {newCache.get()};

	logger.functorMethod(Logging.Level.DEBUGGING) << "Split successfully completed";
	return std::move(newCache);
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public override void pushBlock(CachedBlock cachedBlock, List<CachedTransaction> cachedTransactions, TransactionValidatorState validatorState, size_t blockSize, uint64_t generatedCoins, uint64_t blockDifficulty, RawBlock && rawBlock)
  {
	//we have to call this function from constructor so it has to be non-virtual
	doPushBlock(cachedBlock, cachedTransactions, validatorState, new size_t(blockSize), new uint64_t(generatedCoins), new uint64_t(blockDifficulty), std::move(rawBlock));
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual PushedBlockInfo getPushedBlockInfo(uint32_t blockIndex) const override
  public override PushedBlockInfo getPushedBlockInfo(uint32_t blockIndex)
  {
	Debug.Assert(blockIndex >= startIndex);
	Debug.Assert(blockIndex < startIndex + getBlockCount());

	var localIndex = blockIndex - startIndex;
	auto cachedBlock = blockInfos.get<BlockIndexTag>()[localIndex];

	PushedBlockInfo pushedBlockInfo = new PushedBlockInfo();
	pushedBlockInfo.rawBlock = storage.getBlockByIndex(localIndex);
	pushedBlockInfo.blockSize = cachedBlock.blockSize;

	if (blockIndex > startIndex)
	{
	  auto previousBlock = blockInfos.get<BlockIndexTag>()[localIndex - 1];
	  pushedBlockInfo.blockDifficulty = cachedBlock.cumulativeDifficulty - previousBlock.cumulativeDifficulty;
	  pushedBlockInfo.generatedCoins = cachedBlock.alreadyGeneratedCoins - previousBlock.alreadyGeneratedCoins;
	}
	else
	{
	  if (parent == null)
	  {
		pushedBlockInfo.blockDifficulty = cachedBlock.cumulativeDifficulty;
		pushedBlockInfo.generatedCoins = cachedBlock.alreadyGeneratedCoins;
	  }
	  else
	  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: uint64_t cumulativeDifficulty = parent->getLastCumulativeDifficulties(1, startIndex - 1, addGenesisBlock)[0];
		uint64_t cumulativeDifficulty = parent.getLastCumulativeDifficulties(1, startIndex - 1, new CryptoNote.UseGenesis(GlobalMembers.addGenesisBlock))[0];
		uint64_t alreadyGeneratedCoins = parent.getAlreadyGeneratedCoins(startIndex - 1);

		pushedBlockInfo.blockDifficulty = cachedBlock.cumulativeDifficulty - cumulativeDifficulty;
		pushedBlockInfo.generatedCoins = cachedBlock.alreadyGeneratedCoins - alreadyGeneratedCoins;
	  }
	}

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: pushedBlockInfo.validatorState = fillOutputsSpentByBlock(blockIndex);
	pushedBlockInfo.validatorState.CopyFrom(fillOutputsSpentByBlock(new uint32_t(blockIndex)));

	return pushedBlockInfo;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool checkIfSpent(const Crypto::KeyImage& keyImage, uint32_t blockIndex) const override
  public override bool checkIfSpent(Crypto.KeyImage keyImage, uint32_t blockIndex)
  {
	if (blockIndex < startIndex)
	{
	  Debug.Assert(parent != null);
	  return parent.checkIfSpent(keyImage, new uint32_t(blockIndex));
	}

	var it = spentKeyImages.get<KeyImageTag>().find(keyImage);
	if (it == spentKeyImages.get<KeyImageTag>().end())
	{
	  return parent != null ? parent.checkIfSpent(keyImage, new uint32_t(blockIndex)) : false;
	}

	return it.blockIndex <= blockIndex;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool checkIfSpent(const Crypto::KeyImage& keyImage) const override
  public override bool checkIfSpent(Crypto.KeyImage keyImage)
  {
	if (spentKeyImages.get<KeyImageTag>().count(keyImage) != 0)
	{
	  return true;
	}

	return parent != null && parent.checkIfSpent(keyImage);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isTransactionSpendTimeUnlocked(uint64_t unlockTime) const override
  public override bool isTransactionSpendTimeUnlocked(uint64_t unlockTime)
  {
	return isTransactionSpendTimeUnlocked(new uint64_t(unlockTime), getTopBlockIndex());
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isTransactionSpendTimeUnlocked(uint64_t unlockTime, uint32_t blockIndex) const override
  public override bool isTransactionSpendTimeUnlocked(uint64_t unlockTime, uint32_t blockIndex)
  {
	if (unlockTime < currency.maxBlockHeight())
	{
	  // interpret as block index
	  return blockIndex + currency.lockedTxAllowedDeltaBlocks() >= unlockTime;
	}

	// interpret as time
	return (uint64_t)time(null) + currency.lockedTxAllowedDeltaSeconds() >= unlockTime;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ExtractOutputKeysResult extractKeyOutputKeys(uint64_t amount, Common::ArrayView<uint32_t> globalIndexes, ClassicVector<Crypto::PublicKey>& publicKeys) const override
  public override ExtractOutputKeysResult extractKeyOutputKeys(uint64_t amount, Common.ArrayView<uint32_t> globalIndexes, List<Crypto.PublicKey> publicKeys)
  {
	return extractKeyOutputKeys(new uint64_t(amount), getTopBlockIndex(), new Common.ArrayView<uint32_t>(globalIndexes), publicKeys);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ExtractOutputKeysResult extractKeyOutputKeys(uint64_t amount, uint32_t blockIndex, Common::ArrayView<uint32_t> globalIndexes, ClassicVector<Crypto::PublicKey>& publicKeys) const override
  public override ExtractOutputKeysResult extractKeyOutputKeys(uint64_t amount, uint32_t blockIndex, Common.ArrayView<uint32_t> globalIndexes, List<Crypto.PublicKey> publicKeys)
  {
	Debug.Assert(!globalIndexes.isEmpty());
	Debug.Assert(std::is_sorted(globalIndexes.begin(), globalIndexes.end())); // sorted
	Debug.Assert(std::adjacent_find(globalIndexes.begin(), globalIndexes.end()) == globalIndexes.end()); // unique

	return extractKeyOutputs(new uint64_t(amount), new uint32_t(blockIndex), new Common.ArrayView<uint32_t>(globalIndexes), (CachedTransactionInfo info, PackedOutIndex index, uint32_t globalIndex) =>
	{
	  if (!isTransactionSpendTimeUnlocked(new uint64_t(info.unlockTime), new uint32_t(blockIndex)))
	  {
		return ExtractOutputKeysResult.OUTPUT_LOCKED;
	  }

	  Debug.Assert(info.outputs[index.outputIndex].type() == typeid(KeyOutput));
	  publicKeys.Add(boost::get<KeyOutput>(info.outputs[index.outputIndex]).key);
	  return ExtractOutputKeysResult.SUCCESS;
	});
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ExtractOutputKeysResult extractKeyOtputIndexes(uint64_t amount, Common::ArrayView<uint32_t> globalIndexes, ClassicVector<PackedOutIndex>& outIndexes) const override
  public override ExtractOutputKeysResult extractKeyOtputIndexes(uint64_t amount, Common.ArrayView<uint32_t> globalIndexes, List<PackedOutIndex> outIndexes)
  {
	Debug.Assert(!globalIndexes.isEmpty());
	return extractKeyOutputs(new uint64_t(amount), getTopBlockIndex(), new Common.ArrayView<uint32_t>(globalIndexes), (CachedTransactionInfo info, PackedOutIndex index, uint32_t globalIndex) =>
	{
							   outIndexes.Add(index);
							   return ExtractOutputKeysResult.SUCCESS;
	});
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ExtractOutputKeysResult extractKeyOtputReferences(uint64_t amount, Common::ArrayView<uint32_t> globalIndexes, ClassicVector<System.Tuple<Crypto::Hash, size_t>>& outputReferences) const override
  public override ExtractOutputKeysResult extractKeyOtputReferences(uint64_t amount, Common.ArrayView<uint32_t> globalIndexes, List<Tuple<Crypto.Hash, size_t>> outputReferences)
  {
	Debug.Assert(!globalIndexes.isEmpty());
	Debug.Assert(std::is_sorted(globalIndexes.begin(), globalIndexes.end())); // sorted
	Debug.Assert(std::adjacent_find(globalIndexes.begin(), globalIndexes.end()) == globalIndexes.end()); // unique

	return extractKeyOutputs(new uint64_t(amount), getTopBlockIndex(), new Common.ArrayView<uint32_t>(globalIndexes), (CachedTransactionInfo info, PackedOutIndex index, uint32_t globalIndex) =>
	{
	  outputReferences.Add(Tuple.Create(info.transactionHash, index.outputIndex));
	  return ExtractOutputKeysResult.SUCCESS;
	});
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint32_t getTopBlockIndex() const override
  public override uint32_t getTopBlockIndex()
  {
	Debug.Assert(!blockInfos.empty());
	return startIndex + (uint32_t)blockInfos.size() - 1;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Crypto::Hash& getTopBlockHash() const override
  public override Crypto.Hash getTopBlockHash()
  {
	Debug.Assert(!blockInfos.empty());
	return blockInfos.get<BlockIndexTag>().back().blockHash;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint32_t getBlockCount() const override
  public override uint32_t getBlockCount()
  {
	return (uint32_t)blockInfos.size();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool hasBlock(const Crypto::Hash& blockHash) const override
  public override bool hasBlock(Crypto.Hash blockHash)
  {
	return blockInfos.get<BlockHashTag>().count(blockHash) != 0;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint32_t getBlockIndex(const Crypto::Hash& blockHash) const override
  public override uint32_t getBlockIndex(Crypto.Hash blockHash)
  {
  //  assert(blockInfos.get<BlockHashTag>().count(blockHash) > 0);
	var hashIt = blockInfos.get<BlockHashTag>().find(blockHash);
	if (hashIt == blockInfos.get<BlockHashTag>().end())
	{
	  throw new System.Exception("no such block");
	}

	var rndIt = blockInfos.project<BlockIndexTag>(hashIt);
	return (uint32_t)std::distance(blockInfos.get<BlockIndexTag>().begin(), rndIt) + startIndex;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool hasTransaction(const Crypto::Hash& transactionHash) const override
  public override bool hasTransaction(Crypto.Hash transactionHash)
  {
	auto index = transactions.get<TransactionHashTag>();
	var it = index.find(transactionHash);
	return it != index.end();
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<uint64_t> getLastTimestamps(size_t count) const override
  public override List<uint64_t> getLastTimestamps(size_t count)
  {
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
	return getLastTimestamps(count, getTopBlockIndex(), skipGenesisBlock);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<uint64_t> getLastTimestamps(size_t count, uint32_t blockIndex, UseGenesis useGenesis) const override
  public override List<uint64_t> getLastTimestamps(size_t count, uint32_t blockIndex, UseGenesis useGenesis)
  {
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
	return getLastUnits(count, blockIndex, useGenesis, [](const CachedBlockInfo & inf)
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<uint64_t> getLastBlocksSizes(size_t count) const override
  public override List<uint64_t> getLastBlocksSizes(size_t count)
  {
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
	return getLastBlocksSizes(count, getTopBlockIndex(), skipGenesisBlock);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<uint64_t> getLastBlocksSizes(size_t count, uint32_t blockIndex, UseGenesis useGenesis) const override
  public override List<uint64_t> getLastBlocksSizes(size_t count, uint32_t blockIndex, UseGenesis useGenesis)
  {
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
	return getLastUnits(count, blockIndex, useGenesis, [](const CachedBlockInfo & cb)
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<uint64_t> getLastCumulativeDifficulties(size_t count, uint32_t blockIndex, UseGenesis useGenesis) const override
  public override List<uint64_t> getLastCumulativeDifficulties(size_t count, uint32_t blockIndex, UseGenesis useGenesis)
  {
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
	return getLastUnits(count, blockIndex, useGenesis, [](const CachedBlockInfo & info)
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<uint64_t> getLastCumulativeDifficulties(size_t count) const override
  public override List<uint64_t> getLastCumulativeDifficulties(size_t count)
  {
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
	return getLastCumulativeDifficulties(count, getTopBlockIndex(), skipGenesisBlock);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t getDifficultyForNextBlock() const override
  public override uint64_t getDifficultyForNextBlock()
  {
	return getDifficultyForNextBlock(getTopBlockIndex());
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t getDifficultyForNextBlock(uint32_t blockIndex) const override
  public override uint64_t getDifficultyForNextBlock(uint32_t blockIndex)
  {
	Debug.Assert(blockIndex <= getTopBlockIndex());
	uint8_t nextBlockMajorVersion = getBlockMajorVersionForHeight(blockIndex + 1);
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
	auto timestamps = getLastTimestamps(currency.difficultyBlocksCountByBlockVersion(nextBlockMajorVersion, blockIndex), blockIndex, skipGenesisBlock);
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
	auto commulativeDifficulties = getLastCumulativeDifficulties(currency.difficultyBlocksCountByBlockVersion(nextBlockMajorVersion, blockIndex), blockIndex, skipGenesisBlock);
	return currency.getNextDifficulty(new uint8_t(nextBlockMajorVersion), new uint32_t(blockIndex), std::move(timestamps), std::move(commulativeDifficulties));
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint64_t getCurrentCumulativeDifficulty() const override
  public override uint64_t getCurrentCumulativeDifficulty()
  {
	Debug.Assert(!blockInfos.empty());
	return blockInfos.get<BlockIndexTag>().back().cumulativeDifficulty;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint64_t getCurrentCumulativeDifficulty(uint32_t blockIndex) const override
  public override uint64_t getCurrentCumulativeDifficulty(uint32_t blockIndex)
  {
	Debug.Assert(!blockInfos.empty());
	Debug.Assert(blockIndex <= getTopBlockIndex());
	return blockInfos.get<BlockIndexTag>().at(blockIndex - startIndex).cumulativeDifficulty;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t getAlreadyGeneratedCoins() const override
  public override uint64_t getAlreadyGeneratedCoins()
  {
	return getAlreadyGeneratedCoins(getTopBlockIndex());
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t getAlreadyGeneratedCoins(uint32_t blockIndex) const override
  public override uint64_t getAlreadyGeneratedCoins(uint32_t blockIndex)
  {
	if (blockIndex < startIndex)
	{
	  Debug.Assert(parent != null);
	  return parent.getAlreadyGeneratedCoins(new uint32_t(blockIndex));
	}

	return blockInfos.get<BlockIndexTag>().at(blockIndex - startIndex).alreadyGeneratedCoins;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t getAlreadyGeneratedTransactions(uint32_t blockIndex) const override
  public override uint64_t getAlreadyGeneratedTransactions(uint32_t blockIndex)
  {
	if (blockIndex < startIndex)
	{
	  Debug.Assert(parent != null);
	  return parent.getAlreadyGeneratedTransactions(new uint32_t(blockIndex));
	}

	return blockInfos.get<BlockIndexTag>().at(blockIndex - startIndex).alreadyGeneratedTransactions;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<uint64_t> getLastUnits(size_t count, uint32_t blockIndex, UseGenesis useGenesis, System.Func<const CachedBlockInfo&, uint64_t> pred) const override
  public override List<uint64_t> getLastUnits(size_t count, uint32_t blockIndex, UseGenesis useGenesis, Func<CachedBlockInfo , uint64_t> pred)
  {
	Debug.Assert(blockIndex <= getTopBlockIndex());

	size_t to = blockIndex < startIndex != null ? 0 : blockIndex - startIndex + 1;
	var realCount = Math.Min(count, to);
	var from = to - realCount;
	if (useGenesis == null && from == 0 && realCount != 0 && parent == null)
	{
	  from += 1;
	  realCount -= 1;
	}

	auto blocksIndex = blockInfos.get<BlockIndexTag>();

	List<uint64_t> result = new List<uint64_t>();
	if (realCount < count && parent != null)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: result = parent->getLastUnits(count - realCount, std::min(blockIndex, parent->getTopBlockIndex()), useGenesis, pred);
	  result = new List<uint64_t>(parent.getLastUnits(count - realCount, Math.Min(blockIndex, parent.getTopBlockIndex()), new CryptoNote.UseGenesis(useGenesis), pred));
	}

	std::transform(std::next(blocksIndex.begin(), from), std::next(blocksIndex.begin(), to), std::back_inserter(result), std::move(pred));
	return result;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Crypto::Hash getBlockHash(uint32_t blockIndex) const override
  public override Crypto.Hash getBlockHash(uint32_t blockIndex)
  {
	if (blockIndex < startIndex)
	{
	  Debug.Assert(parent != null);
	  return parent.getBlockHash(new uint32_t(blockIndex));
	}

	Debug.Assert(blockIndex - startIndex < blockInfos.size());
	return blockInfos.get<BlockIndexTag>()[blockIndex - startIndex].blockHash;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getBlockHashes(uint32_t startBlockIndex, size_t maxCount) const override
  public override List<Crypto.Hash> getBlockHashes(uint32_t startBlockIndex, size_t maxCount)
  {
	size_t blocksLeft = new size_t();
	size_t start = 0;
	List<Crypto.Hash> hashes = new List<Crypto.Hash>();

	if (startBlockIndex < startIndex)
	{
	  Debug.Assert(parent != null);
	  hashes = new List<Crypto.Hash>(parent.getBlockHashes(new uint32_t(startBlockIndex), new size_t(maxCount)));
	  blocksLeft = Math.Min(maxCount - hashes.Count, blockInfos.size());
	}
	else
	{
	  start = startBlockIndex - startIndex;
	  blocksLeft = Math.Min(blockInfos.size() - start, maxCount);
	}

	for (var i = start; i < start + blocksLeft; ++i)
	{
	  hashes.Add(blockInfos.get<BlockIndexTag>()[i].blockHash);
	}

	return hashes;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual IBlockchainCache* getParent() const override
  public override IBlockchainCache getParent()
  {
	return parent;
  }
  public override void setParent(IBlockchainCache p)
  {
	parent = p;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint32_t getStartBlockIndex() const override
  public override uint32_t getStartBlockIndex()
  {
	return startIndex;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual size_t getKeyOutputsCountForAmount(uint64_t amount, uint32_t blockIndex) const override
  public override size_t getKeyOutputsCountForAmount(uint64_t amount, uint32_t blockIndex)
  {
	var it = keyOutputsGlobalIndexes.find(amount);
	if (it == keyOutputsGlobalIndexes.end())
	{
	  if (parent == null)
	  {
		return 0;
	  }

	  return parent.getKeyOutputsCountForAmount(new uint64_t(amount), new uint32_t(blockIndex));
	}

//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	var lowerBound = std::lower_bound(it.second.outputs.begin(), it.second.outputs.end(), blockIndex, (PackedOutIndex output, uint32_t blockIndex) =>
	{
	  return output.blockIndex < blockIndex;
	});

//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	return it.second.startIndex + (size_t)std::distance(it.second.outputs.begin(), lowerBound);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint32_t getTimestampLowerBoundBlockIndex(uint64_t timestamp) const override
  public override uint32_t getTimestampLowerBoundBlockIndex(uint64_t timestamp)
  {
	Debug.Assert(!blockInfos.empty());

	auto index = blockInfos.get<BlockIndexTag>();
	if (index.back().timestamp < timestamp)
	{
	  // we don't have it
	  throw new System.Exception("no blocks for this timestamp, too large");
	}

	if (index.front().timestamp < timestamp)
	{
	  // we know the timestamp is in current segment for sure
//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
	  var bound = std::lower_bound(index.begin(), index.end(), timestamp, (CachedBlockInfo blockInfo, uint64_t value) =>
	  {
		  return blockInfo.timestamp < value;
	  });

	  return startIndex + (uint32_t)std::distance(index.begin(), bound);
	}

	// if index.front().timestamp >= timestamp we can't be sure the timestamp is in current segment
	// so we ask parent. If it doesn't have it then index.front() is the block being searched for.

	if (parent == null)
	{
	  // if given timestamp is less or equal genesis block timestamp
	  return 0;
	}

	try
	{
	  uint32_t blockIndex = parent.getTimestampLowerBoundBlockIndex(new uint64_t(timestamp));
	  return blockIndex != GlobalMembers.INVALID_BLOCK_INDEX != null ? blockIndex : startIndex;
	}
	catch (System.Exception)
	{
	  return startIndex;
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getTransactionGlobalIndexes(const Crypto::Hash& transactionHash, ClassicVector<uint32_t>& globalIndexes) const override
  public override bool getTransactionGlobalIndexes(Crypto.Hash transactionHash, ref List<uint32_t> globalIndexes)
  {
	var it = transactions.get<TransactionHashTag>().find(transactionHash);
	if (it == transactions.get<TransactionHashTag>().end())
	{
	  return false;
	}

	globalIndexes = it.globalIndexes;
	return true;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual size_t getTransactionCount() const override
  public override size_t getTransactionCount()
  {
	size_t count = 0;

	if (parent != null)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: count = parent->getTransactionCount();
	  count.CopyFrom(parent.getTransactionCount());
	}

	count += transactions.size();
	return count;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint32_t getBlockIndexContainingTx(const Crypto::Hash& transactionHash) const override
  public override uint32_t getBlockIndexContainingTx(Crypto.Hash transactionHash)
  {
	auto index = transactions.get<TransactionHashTag>();
	var it = index.find(transactionHash);
	Debug.Assert(it != index.end());
	return it.blockIndex;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual size_t getChildCount() const override
  public override size_t getChildCount()
  {
	return children.Count;
  }
  public override void addChild(IBlockchainCache child)
  {
	Debug.Assert(std::find(children.GetEnumerator(), children.end(), child) == children.end());
	children.Add(child);
  }
  public override bool deleteChild(IBlockchainCache child)
  {
	var it = std::find(children.GetEnumerator(), children.end(), child);
	if (it == children.end())
	{
	  return false;
	}

//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'erase' method in C#:
	children.erase(it);
	return true;
  }

  public override void save()
  {
	std::ofstream file = new std::ofstream(filename);
	Common.StdOutputStream stream = new Common.StdOutputStream(file);
	CryptoNote.BinaryOutputStreamSerializer s = new CryptoNote.BinaryOutputStreamSerializer(stream);

	serialize(s.functorMethod);
  }
  public override void load()
  {
	std::ifstream file = new std::ifstream(filename);
	Common.StdInputStream stream = new Common.StdInputStream(file);
	CryptoNote.BinaryInputStreamSerializer s = new CryptoNote.BinaryInputStreamSerializer(stream);

	serialize(s.functorMethod);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<BinaryArray> getRawTransactions(const ClassicVector<Crypto::Hash>& requestedTransactions, ClassicVector<Crypto::Hash>& missedTransactions) const override
  public override List<BinaryArray> getRawTransactions(List<Crypto.Hash> requestedTransactions, List<Crypto.Hash> missedTransactions)
  {
	List<BinaryArray> res = new List<BinaryArray>();
	getRawTransactions(requestedTransactions, res, missedTransactions);
	return res;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<BinaryArray> getRawTransactions(const ClassicVector<Crypto::Hash>& requestedTransactions) const override
  public override List<BinaryArray> getRawTransactions(List<Crypto.Hash> requestedTransactions)
  {
	List<Crypto.Hash> misses = new List<Crypto.Hash>();
	var ret = getRawTransactions(requestedTransactions, misses);
	Debug.Assert(misses.Count == 0);
	return ret;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void getRawTransactions(const ClassicVector<Crypto::Hash>& requestedTransactions, ClassicVector<BinaryArray>& foundTransactions, ClassicVector<Crypto::Hash>& missedTransactions) const override
  public override void getRawTransactions(List<Crypto.Hash> requestedTransactions, List<BinaryArray> foundTransactions, List<Crypto.Hash> missedTransactions)
  {
	auto index = transactions.get<TransactionHashTag>();
	foreach (var transactionHash in requestedTransactions)
	{
	  var it = index.find(transactionHash);
	  if (it == index.end())
	  {
		missedTransactions.Add(transactionHash);
		continue;
	  }

	  // assert(startIndex <= it->blockIndex);
	  foundTransactions.Add(getRawTransaction(it.blockIndex, it.transactionIndex));
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual RawBlock getBlockByIndex(uint32_t index) const override
  public override RawBlock getBlockByIndex(uint32_t index)
  {
	return index < startIndex != null ? parent.getBlockByIndex(new uint32_t(index)) : storage.getBlockByIndex(index - startIndex);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual BinaryArray getRawTransaction(uint32_t index, uint32_t transactionIndex) const override
  public override BinaryArray getRawTransaction(uint32_t index, uint32_t transactionIndex)
  {
	if (index < startIndex)
	{
	  return parent.getRawTransaction(new uint32_t(index), new uint32_t(transactionIndex));
	}
	else
	{
	  var rawBlock = storage.getBlockByIndex(index - startIndex);
	  if (transactionIndex == 0)
	  {
		var block = CryptoNote.GlobalMembers.fromBinaryArray<BlockTemplate>(rawBlock.block);
		return CryptoNote.GlobalMembers.toBinaryArray(block.baseTransaction);
	  }

	  Debug.Assert(rawBlock.transactions.size() >= transactionIndex - 1);
	  return rawBlock.transactions[transactionIndex - 1];
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getTransactionHashes() const override
  public override List<Crypto.Hash> getTransactionHashes()
  {
	auto txInfos = transactions.get<TransactionHashTag>();
	List<Crypto.Hash> hashes = new List<Crypto.Hash>();
	foreach (var tx in txInfos)
	{
	  // skip base transaction
	  if (tx.transactionIndex != 0)
	  {
		hashes.Add(tx.transactionHash);
	  }
	}
	return hashes;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<uint32_t> getRandomOutsByAmount(uint64_t amount, size_t count, uint32_t blockIndex) const override;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  override ClassicVector<uint32_t> getRandomOutsByAmount(uint64_t amount, size_t count, uint32_t blockIndex);

  // TODO: start from index
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ExtractOutputKeysResult extractKeyOutputs(uint64_t amount, uint32_t blockIndex, Common::ArrayView<uint32_t> globalIndexes, System.Func<const CachedTransactionInfo& info, PackedOutIndex index, uint32_t globalIndex, ExtractOutputKeysResult> pred) const override
  public override ExtractOutputKeysResult extractKeyOutputs(uint64_t amount, uint32_t blockIndex, Common.ArrayView<uint32_t> globalIndexes, Func<CachedTransactionInfo info, PackedOutIndex index, uint32_t globalIndex, ExtractOutputKeysResult> pred)
  {
	Debug.Assert(!globalIndexes.isEmpty());
	Debug.Assert(std::is_sorted(globalIndexes.begin(), globalIndexes.end())); // sorted
	Debug.Assert(std::adjacent_find(globalIndexes.begin(), globalIndexes.end()) == globalIndexes.end()); // unique

	var globalIndexesIterator = keyOutputsGlobalIndexes.find(amount);
	if (globalIndexesIterator == keyOutputsGlobalIndexes.end() || blockIndex < startIndex)
	{
	  return parent != null ? parent.extractKeyOutputs(new uint64_t(amount), new uint32_t(blockIndex), new Common.ArrayView<uint32_t>(globalIndexes), std::move(pred)) : ExtractOutputKeysResult.INVALID_GLOBAL_INDEX;
	}

//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	var startGlobalIndex = globalIndexesIterator.second.startIndex;
	var parentIndexesIterator = std::lower_bound(globalIndexes.begin(), globalIndexes.end(), startGlobalIndex);

	var offset = std::distance(globalIndexes.begin(), parentIndexesIterator);
	if (parentIndexesIterator != globalIndexes.begin())
	{
	  Debug.Assert(parent != null);
	  var result = parent.extractKeyOutputs(new uint64_t(amount), new uint32_t(blockIndex), globalIndexes.head(parentIndexesIterator - globalIndexes.begin()), pred);
	  if (result != ExtractOutputKeysResult.SUCCESS)
	  {
		return result;
	  }
	}

	var myGlobalIndexes = globalIndexes.unhead(offset);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	auto outputs = globalIndexesIterator.second.outputs;
	Debug.Assert(!outputs.empty());
	foreach (var globalIndex in myGlobalIndexes)
	{
	  if (globalIndex - startGlobalIndex >= outputs.size() != null)
	  {
		logger.functorMethod(Logging.Level.DEBUGGING) << "Couldn't extract key output for amount " << amount << " with global index " << globalIndex << " because global index is greater than the last available: " << (startGlobalIndex + outputs.size());
		return ExtractOutputKeysResult.INVALID_GLOBAL_INDEX;
	  }

	  var outputIndex = outputs[globalIndex - startGlobalIndex];

	  Debug.Assert(outputIndex.blockIndex >= startIndex);
	  Debug.Assert(outputIndex.blockIndex <= blockIndex);

	  var txIt = transactions.get<TransactionInBlockTag>().find(boost::make_tuple<uint32_t, uint32_t>(outputIndex.blockIndex, outputIndex.transactionIndex));
	  if (txIt == transactions.get<TransactionInBlockTag>().end())
	  {
		logger.functorMethod(Logging.Level.DEBUGGING) << "Couldn't extract key output for amount " << amount << " with global index " << globalIndex << " because containing transaction doesn't exist in index " << "(block index: " << outputIndex.blockIndex << ", transaction index: " << outputIndex.transactionIndex << ")";
		return ExtractOutputKeysResult.INVALID_GLOBAL_INDEX;
	  }

	  var ret = pred(*txIt, outputIndex, globalIndex);
	  if (ret != ExtractOutputKeysResult.SUCCESS)
	  {
		logger.functorMethod(Logging.Level.DEBUGGING) << "Couldn't extract key output for amount " << amount << " with global index " << globalIndex << " because callback returned fail status (block index: " << outputIndex.blockIndex << ", transaction index: " << outputIndex.transactionIndex << ")";
		return ret;
	  }
	}

	return ExtractOutputKeysResult.SUCCESS;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getTransactionHashesByPaymentId(const Crypto::Hash& paymentId) const override
  public override List<Crypto.Hash> getTransactionHashesByPaymentId(Crypto.Hash paymentId)
  {
	List<Crypto.Hash> transactionHashes = new List<Crypto.Hash>();

	if (parent != null)
	{
	  transactionHashes = new List<Crypto.Hash>(parent.getTransactionHashesByPaymentId(paymentId));
	}

	auto index = paymentIds.get<PaymentIdTag>();
	var range = index.equal_range(paymentId);

	transactionHashes.Capacity = transactionHashes.Count + std::distance(range.first, range.second);
	for (var it = range.first; it != range.second; ++it)
	{
	  transactionHashes.Add(it.transactionHash);
	}

	logger.functorMethod(Logging.Level.DEBUGGING) << "Found " << transactionHashes.Count << " transactions with payment id " << paymentId;
	return transactionHashes;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getBlockHashesByTimestamps(uint64_t timestampBegin, size_t secondsCount) const override
  public override List<Crypto.Hash> getBlockHashesByTimestamps(uint64_t timestampBegin, size_t secondsCount)
  {
	List<Crypto.Hash> blockHashes = new List<Crypto.Hash>();
	if (secondsCount == 0)
	{
	  return blockHashes;
	}

	if (parent != null)
	{
	  blockHashes = new List<Crypto.Hash>(parent.getBlockHashesByTimestamps(new uint64_t(timestampBegin), new size_t(secondsCount)));
	}

	auto index = blockInfos.get<TimestampTag>();
	var begin = index.lower_bound(timestampBegin);
	var end = index.upper_bound(timestampBegin + (uint64_t)secondsCount - 1);

	blockHashes.Capacity = blockHashes.Count + std::distance(begin, end);
	for (var it = begin; it != end; ++it)
	{
	  blockHashes.Add(it.blockHash);
	}

	logger.functorMethod(Logging.Level.DEBUGGING) << "Found " << blockHashes.Count << " within timestamp interval " << "[" << timestampBegin << ":" << (timestampBegin + secondsCount) << "]";
	return blockHashes;
  }


  private class BlockIndexTag
  {
  }
  private class BlockHashTag
  {
  }
  private class TransactionHashTag
  {
  }
  private class KeyImageTag
  {
  }
  private class TransactionInBlockTag
  {
  }
  private class PackedOutputTag
  {
  }
  private class TimestampTag
  {
  }
  private class PaymentIdTag
  {
  }

  private typedef boost::multi_index_container< SpentKeyImage, boost::multi_index.indexed_by< boost::multi_index.ordered_non_unique< boost::multi_index.tag<BlockIndexTag>, BOOST_MULTI_INDEX_MEMBER(SpentKeyImage, uint32_t, blockIndex) >, boost::multi_index.hashed_unique< boost::multi_index.tag<KeyImageTag>, BOOST_MULTI_INDEX_MEMBER(SpentKeyImage, Crypto.KeyImage, keyImage) >> > SpentKeyImagesContainer = new typedef();

  private typedef boost::multi_index_container< CachedTransactionInfo, boost::multi_index.indexed_by< boost::multi_index.hashed_unique< boost::multi_index.tag<TransactionInBlockTag>, boost::multi_index.composite_key< CachedTransactionInfo, BOOST_MULTI_INDEX_MEMBER(CachedTransactionInfo, uint32_t, blockIndex), BOOST_MULTI_INDEX_MEMBER(CachedTransactionInfo, uint32_t, transactionIndex) >>, boost::multi_index.ordered_non_unique< boost::multi_index.tag<BlockIndexTag>, BOOST_MULTI_INDEX_MEMBER(CachedTransactionInfo, uint32_t, blockIndex) >, boost::multi_index.hashed_unique< boost::multi_index.tag<TransactionHashTag>, BOOST_MULTI_INDEX_MEMBER(CachedTransactionInfo, Crypto.Hash, transactionHash) >> > TransactionsCacheContainer = new typedef();

  private typedef boost::multi_index_container< CachedBlockInfo, boost::multi_index.indexed_by< boost::multi_index.random_access< boost::multi_index.tag<BlockIndexTag>>, boost::multi_index.hashed_unique< boost::multi_index.tag<BlockHashTag>, BOOST_MULTI_INDEX_MEMBER(CachedBlockInfo, Crypto.Hash, blockHash) >, boost::multi_index.ordered_non_unique< boost::multi_index.tag<TimestampTag>, BOOST_MULTI_INDEX_MEMBER(CachedBlockInfo, uint64_t, timestamp) >> > BlockInfoContainer = new typedef();

  private typedef boost::multi_index_container< PaymentIdTransactionHashPair, boost::multi_index.indexed_by< boost::multi_index.hashed_non_unique< boost::multi_index.tag<PaymentIdTag>, BOOST_MULTI_INDEX_MEMBER(PaymentIdTransactionHashPair, Crypto.Hash, paymentId) >, boost::multi_index.hashed_unique< boost::multi_index.tag<TransactionHashTag>, BOOST_MULTI_INDEX_MEMBER(PaymentIdTransactionHashPair, Crypto.Hash, transactionHash) >> > PaymentIdContainer = new typedef();


  private readonly uint32_t CURRENT_SERIALIZATION_VERSION = 1;
  private string filename;
  private readonly Currency currency;
  private Logging.LoggerRef logger = new Logging.LoggerRef();
  private IBlockchainCache parent;
  // index of first block stored in this cache
  private uint32_t startIndex = new uint32_t();

  private TransactionsCacheContainer transactions = new TransactionsCacheContainer();
  private SpentKeyImagesContainer spentKeyImages = new SpentKeyImagesContainer();
  private BlockInfoContainer blockInfos = new BlockInfoContainer();
  private SortedDictionary<uint64_t, OutputGlobalIndexesForAmount> keyOutputsGlobalIndexes = new SortedDictionary<uint64_t, OutputGlobalIndexesForAmount>();
  private PaymentIdContainer paymentIds = new PaymentIdContainer();
  private std::unique_ptr<BlockchainStorage> storage = new std::unique_ptr<BlockchainStorage>();

  private List<IBlockchainCache> children = new List<IBlockchainCache>();

  private void serialize(ISerializer s)
  {
	Debug.Assert(s.type() == ISerializer.OUTPUT);

	uint32_t version = new uint32_t(CURRENT_SERIALIZATION_VERSION);

	s.functorMethod(version, "version");

	if (s.type() == ISerializer.OUTPUT)
	{
	  CryptoNote.GlobalMembers.writeSequence<CachedTransactionInfo>(transactions.begin(), transactions.end(), "transactions", s.functorMethod);
	  CryptoNote.GlobalMembers.writeSequence<SpentKeyImage>(spentKeyImages.begin(), spentKeyImages.end(), "spent_key_images", s.functorMethod);
	  CryptoNote.GlobalMembers.writeSequence<CachedBlockInfo>(blockInfos.begin(), blockInfos.end(), "block_hash_indexes", s.functorMethod);
	  CryptoNote.GlobalMembers.writeSequence<PaymentIdTransactionHashPair>(paymentIds.begin(), paymentIds.end(), "payment_id_indexes", s.functorMethod);

	  s.functorMethod(keyOutputsGlobalIndexes, "key_outputs_global_indexes");
	}
	else
	{
	  TransactionsCacheContainer restoredTransactions = new TransactionsCacheContainer();
	  SpentKeyImagesContainer restoredSpentKeyImages = new SpentKeyImagesContainer();
	  BlockInfoContainer restoredBlockHashIndex = new BlockInfoContainer();
	  OutputsGlobalIndexesContainer restoredKeyOutputsGlobalIndexes = new OutputsGlobalIndexesContainer();
	  PaymentIdContainer restoredPaymentIds = new PaymentIdContainer();

	  CryptoNote.GlobalMembers.readSequence<CachedTransactionInfo>(std::inserter(restoredTransactions, restoredTransactions.end()), "transactions", s.functorMethod);
	  CryptoNote.GlobalMembers.readSequence<SpentKeyImage>(std::inserter(restoredSpentKeyImages, restoredSpentKeyImages.end()), "spent_key_images", s.functorMethod);
	  CryptoNote.GlobalMembers.readSequence<CachedBlockInfo>(std::back_inserter(restoredBlockHashIndex), "block_hash_indexes", s.functorMethod);
	  CryptoNote.GlobalMembers.readSequence<PaymentIdTransactionHashPair>(std::inserter(restoredPaymentIds, restoredPaymentIds.end()), "payment_id_indexes", s.functorMethod);

	  s.functorMethod(restoredKeyOutputsGlobalIndexes, "key_outputs_global_indexes");

	  transactions = std::move(restoredTransactions);
	  spentKeyImages = std::move(restoredSpentKeyImages);
	  blockInfos = std::move(restoredBlockHashIndex);
	  keyOutputsGlobalIndexes = std::move(restoredKeyOutputsGlobalIndexes);
	  paymentIds = std::move(restoredPaymentIds);
	}
  }

  private void addSpentKeyImage(Crypto.KeyImage keyImage, uint32_t blockIndex)
  {
	Debug.Assert(!checkIfSpent(keyImage, blockIndex - 1)); //Changed from "assert(!checkIfSpent(keyImage, blockIndex));"
													 //to prevent fail when pushing block from DatabaseBlockchainCache.
													 //In case of pushing external block double spend within block
													 //should be checked by Core.
	spentKeyImages.get<BlockIndexTag>().insert(new SpentKeyImage({blockIndex, keyImage}));
  }
  private void pushTransaction(CachedTransaction cachedTransaction, uint32_t blockIndex, uint16_t transactionInBlockIndex)
  {
	logger.functorMethod(Logging.Level.DEBUGGING) << "Adding transaction " << cachedTransaction.getTransactionHash() << " at block " << blockIndex << ", index in block " << transactionInBlockIndex;

	auto tx = cachedTransaction.getTransaction();

	CachedTransactionInfo transactionCacheInfo = new CachedTransactionInfo();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: transactionCacheInfo.blockIndex = blockIndex;
	transactionCacheInfo.blockIndex.CopyFrom(blockIndex);
	transactionCacheInfo.transactionIndex = transactionInBlockIndex;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: transactionCacheInfo.transactionHash = cachedTransaction.getTransactionHash();
	transactionCacheInfo.transactionHash.CopyFrom(cachedTransaction.getTransactionHash());
	transactionCacheInfo.unlockTime = tx.unlockTime;

	Debug.Assert(tx.outputs.size() <= uint16_t.MaxValue);

	transactionCacheInfo.globalIndexes.Capacity = tx.outputs.size();
	transactionCacheInfo.outputs.Capacity = tx.outputs.size();

	logger.functorMethod(Logging.Level.DEBUGGING) << "Adding " << tx.outputs.size() << " transaction outputs";
	var outputCount = 0;
	foreach (var output in tx.outputs)
	{
	  transactionCacheInfo.outputs.Add(output.target);

	  PackedOutIndex poi = new PackedOutIndex();
	  poi.blockIndex = blockIndex;
	  poi.transactionIndex = transactionInBlockIndex;
	  poi.outputIndex = outputCount++;

//C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
	  if (output.target.type() == typeid(KeyOutput))
	  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: transactionCacheInfo.globalIndexes.push_back(insertKeyOutputToGlobalIndex(output.amount, poi, blockIndex));
		transactionCacheInfo.globalIndexes.Add(insertKeyOutputToGlobalIndex(output.amount, new PackedOutIndex(poi), new uint32_t(blockIndex)));
	  }
	}

	Debug.Assert(transactions.get<TransactionHashTag>().count(transactionCacheInfo.transactionHash) == 0);
	transactions.get<TransactionInBlockTag>().insert(std::move(transactionCacheInfo));

	PaymentIdTransactionHashPair paymentIdTransactionHash = new PaymentIdTransactionHashPair();
	if (!getPaymentIdFromTxExtra(tx.extra, paymentIdTransactionHash.paymentId))
	{
	  logger.functorMethod(Logging.Level.DEBUGGING) << "Transaction " << cachedTransaction.getTransactionHash() << " successfully added";
	  return;
	}

	logger.functorMethod(Logging.Level.DEBUGGING) << "Payment id found: " << paymentIdTransactionHash.paymentId;

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: paymentIdTransactionHash.transactionHash = cachedTransaction.getTransactionHash();
	paymentIdTransactionHash.transactionHash.CopyFrom(cachedTransaction.getTransactionHash());
	paymentIds.insert(std::move(paymentIdTransactionHash));
	logger.functorMethod(Logging.Level.DEBUGGING) << "Transaction " << cachedTransaction.getTransactionHash() << " successfully added";
  }

  private void splitSpentKeyImages(BlockchainCache newCache, uint32_t splitBlockIndex)
  {
	//Key images with blockIndex == splitBlockIndex remain in upper segment
	auto imagesIndex = spentKeyImages.get<BlockIndexTag>();
	var lowerBound = imagesIndex.lower_bound(splitBlockIndex);

	newCache.spentKeyImages.get<BlockIndexTag>().insert(lowerBound, imagesIndex.end());
	imagesIndex.erase(lowerBound, imagesIndex.end());

	logger.functorMethod(Logging.Level.DEBUGGING) << "Spent key images split completed";
  }
  private void splitTransactions(BlockchainCache newCache, uint32_t splitBlockIndex)
  {
	auto transactionsIndex = transactions.get<BlockIndexTag>();
	var lowerBound = transactionsIndex.lower_bound(splitBlockIndex);

	for (var it = lowerBound; it != transactionsIndex.end(); ++it)
	{
	  removePaymentId(it.transactionHash, newCache);
	}

	newCache.transactions.get<BlockIndexTag>().insert(lowerBound, transactionsIndex.end());
	transactionsIndex.erase(lowerBound, transactionsIndex.end());

	logger.functorMethod(Logging.Level.DEBUGGING) << "Transactions split completed";
  }
  private void splitBlocks(BlockchainCache newCache, uint32_t splitBlockIndex)
  {
	auto blocksIndex = blockInfos.get<BlockIndexTag>();
	var bound = std::next(blocksIndex.begin(), splitBlockIndex - startIndex);
	std::move(bound, blocksIndex.end(), std::back_inserter(newCache.blockInfos.get<BlockIndexTag>()));
	blocksIndex.erase(bound, blocksIndex.end());

	logger.functorMethod(Logging.Level.DEBUGGING) << "Blocks split completed";
  }
  private void splitKeyOutputsGlobalIndexes(BlockchainCache newCache, uint32_t splitBlockIndex)
  {
//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
	var lowerBoundFunction = (List<PackedOutIndex>.Enumerator begin, List<PackedOutIndex>.Enumerator end, uint32_t splitBlockIndex) =>
	{
	  return std::lower_bound(begin, end, splitBlockIndex, (PackedOutIndex outputIndex, uint32_t splitIndex) =>
	  {
		// all outputs in it->second.outputs are sorted according to blockIndex + transactionIndex
		return outputIndex.blockIndex < splitIndex;
	  });
	};

	GlobalMembers.splitGlobalIndexes(keyOutputsGlobalIndexes, newCache.keyOutputsGlobalIndexes, new uint32_t(splitBlockIndex), lowerBoundFunction);
	logger.functorMethod(Logging.Level.DEBUGGING) << "Key output global indexes split successfully completed";
  }
  private void removePaymentId(Crypto.Hash transactionHash, BlockchainCache newCache)
  {
	auto index = paymentIds.get<TransactionHashTag>();
	var it = index.find(transactionHash);

	if (it == index.end())
	{
	  return;
	}

	newCache.paymentIds.insert(*it);
	index.erase(it);
  }

  private uint32_t insertKeyOutputToGlobalIndex(uint64_t amount, PackedOutIndex output, uint32_t blockIndex)
  {
	var pair = keyOutputsGlobalIndexes.insert({amount, new OutputGlobalIndexesForAmount({})});
	auto indexEntry = pair.first.second;
	indexEntry.outputs.push_back(output);
	if (pair.second && parent != null)
	{
	  indexEntry.startIndex = (uint32_t)parent.getKeyOutputsCountForAmount(new uint64_t(amount), new uint32_t(blockIndex));
	  logger.functorMethod(Logging.Level.DEBUGGING) << "Key output count for amount " << amount << " requested from parent. Returned count: " << indexEntry.startIndex;
	}

	return indexEntry.startIndex + (uint32_t)indexEntry.outputs.size() - 1;
  }

  private enum OutputSearchResult : uint8_t
  {
	FOUND,
	NOT_FOUND,
	INVALID_ARGUMENT
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: TransactionValidatorState fillOutputsSpentByBlock(uint32_t blockIndex) const
  private TransactionValidatorState fillOutputsSpentByBlock(uint32_t blockIndex)
  {
	TransactionValidatorState spentOutputs = new TransactionValidatorState();
	auto keyImagesIndex = spentKeyImages.get<BlockIndexTag>();

	var range = keyImagesIndex.equal_range(blockIndex);
	for (var it = range.first; it != range.second; ++it)
	{
	  spentOutputs.spentKeyImages.Add(it.keyImage);
	}

	return spentOutputs;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint8_t getBlockMajorVersionForHeight(uint32_t height) const
private uint8_t getBlockMajorVersionForHeight(uint32_t height)
{
  UpgradeManager upgradeManager = new UpgradeManager();
  upgradeManager.addMajorBlockVersion(BLOCK_MAJOR_VERSION_2, currency.upgradeHeight(BLOCK_MAJOR_VERSION_2));
  upgradeManager.addMajorBlockVersion(BLOCK_MAJOR_VERSION_3, currency.upgradeHeight(BLOCK_MAJOR_VERSION_3));
  return upgradeManager.getBlockMajorVersion(new uint32_t(height));
}
  private void fixChildrenParent(IBlockchainCache p)
  {
	foreach (var child in children)
	{
	  child.setParent(p);
	}
  }

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  private void doPushBlock(CachedBlock cachedBlock, List<CachedTransaction> cachedTransactions, TransactionValidatorState validatorState, size_t blockSize, uint64_t generatedCoins, uint64_t blockDifficulty, RawBlock && rawBlock)
  {
	logger.functorMethod(Logging.Level.DEBUGGING) << "Pushing block " << cachedBlock.getBlockHash() << " at index " << cachedBlock.getBlockIndex();

	Debug.Assert(blockSize > 0);
	Debug.Assert(blockDifficulty > 0);

	uint64_t cumulativeDifficulty = 0;
	uint64_t alreadyGeneratedCoins = 0;
	uint64_t alreadyGeneratedTransactions = 0;

	if (getBlockCount() == 0)
	{
	  if (parent != null)
	  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: cumulativeDifficulty = parent->getCurrentCumulativeDifficulty(cachedBlock.getBlockIndex() - 1);
		cumulativeDifficulty.CopyFrom(parent.getCurrentCumulativeDifficulty(cachedBlock.getBlockIndex() - 1));
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: alreadyGeneratedCoins = parent->getAlreadyGeneratedCoins(cachedBlock.getBlockIndex() - 1);
		alreadyGeneratedCoins.CopyFrom(parent.getAlreadyGeneratedCoins(cachedBlock.getBlockIndex() - 1));
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: alreadyGeneratedTransactions = parent->getAlreadyGeneratedTransactions(cachedBlock.getBlockIndex() - 1);
		alreadyGeneratedTransactions.CopyFrom(parent.getAlreadyGeneratedTransactions(cachedBlock.getBlockIndex() - 1));
	  }

	  cumulativeDifficulty += blockDifficulty;
	  alreadyGeneratedCoins += generatedCoins;
	  alreadyGeneratedTransactions += cachedTransactions.Count + 1;
	}
	else
	{
	  auto lastBlockInfo = blockInfos.get<BlockIndexTag>().back();

	  cumulativeDifficulty = lastBlockInfo.cumulativeDifficulty + blockDifficulty;
	  alreadyGeneratedCoins = lastBlockInfo.alreadyGeneratedCoins + generatedCoins;
	  alreadyGeneratedTransactions = lastBlockInfo.alreadyGeneratedTransactions + cachedTransactions.Count + 1;
	}

	CachedBlockInfo blockInfo = new CachedBlockInfo();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockInfo.blockHash = cachedBlock.getBlockHash();
	blockInfo.blockHash.CopyFrom(cachedBlock.getBlockHash());
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockInfo.alreadyGeneratedCoins = alreadyGeneratedCoins;
	blockInfo.alreadyGeneratedCoins.CopyFrom(alreadyGeneratedCoins);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockInfo.alreadyGeneratedTransactions = alreadyGeneratedTransactions;
	blockInfo.alreadyGeneratedTransactions.CopyFrom(alreadyGeneratedTransactions);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockInfo.cumulativeDifficulty = cumulativeDifficulty;
	blockInfo.cumulativeDifficulty.CopyFrom(cumulativeDifficulty);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: blockInfo.blockSize = static_cast<uint32_t>(blockSize);
	blockInfo.blockSize.CopyFrom((uint32_t)blockSize);
	blockInfo.timestamp = cachedBlock.getBlock().timestamp;

	Debug.Assert(!hasBlock(blockInfo.blockHash));

	blockInfos.get<BlockIndexTag>().push_back(std::move(blockInfo));

	var blockIndex = cachedBlock.getBlockIndex();
	Debug.Assert(blockIndex == blockInfos.size() + startIndex - 1);

	foreach (var keyImage in validatorState.spentKeyImages)
	{
	  addSpentKeyImage(keyImage, new uint32_t(blockIndex));
	}

	logger.functorMethod(Logging.Level.DEBUGGING) << "Added " << validatorState.spentKeyImages.Count << " spent key images";

	Debug.Assert(cachedTransactions.Count <= uint16_t.MaxValue);

	var transactionBlockIndex = 0;
	var baseTransaction = cachedBlock.getBlock().baseTransaction;
	pushTransaction(new CachedTransaction(std::move(baseTransaction)), new uint32_t(blockIndex), transactionBlockIndex++);

	foreach (var cachedTransaction in cachedTransactions)
	{
	  pushTransaction(cachedTransaction, new uint32_t(blockIndex), transactionBlockIndex++);
	}

	storage.pushBlock(std::move(rawBlock));

	logger.functorMethod(Logging.Level.DEBUGGING) << "Block " << cachedBlock.getBlockHash() << " successfully pushed";
  }
}

}





//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);


namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<uint32_t> BlockchainCache::getRandomOutsByAmount(Amount amount, size_t count, uint32_t blockIndex) const

}
