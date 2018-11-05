// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

using CryptoNote;
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

namespace CryptoNote
{

public class BlockchainWriteBatch : IWriteBatch, System.IDisposable
{
  public BlockchainWriteBatch()
  {

  }
  public void Dispose()
  {

  }

  public BlockchainWriteBatch insertSpentKeyImages(uint32_t blockIndex, HashSet<Crypto.KeyImage> spentKeyImages)
  {
	rawDataToInsert.Capacity = rawDataToInsert.Count + spentKeyImages.Count + 1;
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.BLOCK_INDEX_TO_KEY_IMAGE_PREFIX, blockIndex, spentKeyImages));
	foreach (Crypto  in :KeyImage & keyImage : spentKeyImages)
	{
	  rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.KEY_IMAGE_TO_BLOCK_INDEX_PREFIX, keyImage, blockIndex));
	}
	return this;
  }
  public BlockchainWriteBatch insertCachedTransaction(ExtendedTransactionInfo transaction, uint64_t totalTxsCount)
  {
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.TRANSACTION_HASH_TO_TRANSACTION_INFO_PREFIX, transaction.transactionHash, transaction));
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.TRANSACTION_HASH_TO_TRANSACTION_INFO_PREFIX, DB.GlobalMembers.TRANSACTIONS_COUNT_KEY, totalTxsCount));
	return this;
  }
  public BlockchainWriteBatch insertPaymentId(Crypto.Hash transactionHash, Crypto.Hash paymentId, uint32_t totalTxsCountForPaymentId)
  {
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.PAYMENT_ID_TO_TX_HASH_PREFIX, paymentId, totalTxsCountForPaymentId));
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.PAYMENT_ID_TO_TX_HASH_PREFIX, Tuple.Create(paymentId, totalTxsCountForPaymentId - 1), transactionHash));
	return this;
  }
  public BlockchainWriteBatch insertCachedBlock(CachedBlockInfo block, uint32_t blockIndex, List<Crypto.Hash> blockTxs)
  {
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.BLOCK_INDEX_TO_BLOCK_INFO_PREFIX, blockIndex, block));
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.BLOCK_INDEX_TO_TX_HASHES_PREFIX, blockIndex, blockTxs));
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.BLOCK_HASH_TO_BLOCK_INDEX_PREFIX, block.blockHash, blockIndex));
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.BLOCK_INDEX_TO_BLOCK_HASH_PREFIX, DB.GlobalMembers.LAST_BLOCK_INDEX_KEY, blockIndex));
	return this;
  }
  public BlockchainWriteBatch insertKeyOutputGlobalIndexes(uint64_t amount, List<PackedOutIndex> outputs, uint32_t totalOutputsCountForAmount)
  {
	Debug.Assert(totalOutputsCountForAmount >= outputs.Count);
	rawDataToInsert.Capacity = rawDataToInsert.Count + outputs.Count + 1;
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.KEY_OUTPUT_AMOUNT_PREFIX, amount, totalOutputsCountForAmount));
	uint32_t currentOutputId = totalOutputsCountForAmount - (uint32_t)outputs.Count;

	foreach (PackedOutIndex outIndex in outputs)
	{
	  rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.KEY_OUTPUT_AMOUNT_PREFIX, Tuple.Create(amount, currentOutputId++), outIndex));
	}

	return this;
  }
  public BlockchainWriteBatch insertRawBlock(uint32_t blockIndex, RawBlock block)
  {
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.BLOCK_INDEX_TO_RAW_BLOCK_PREFIX, blockIndex, block));
	return this;
  }
  public BlockchainWriteBatch insertClosestTimestampBlockIndex(uint64_t timestamp, uint32_t blockIndex)
  {
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.CLOSEST_TIMESTAMP_BLOCK_INDEX_PREFIX, timestamp, blockIndex));
	return this;
  }
  public BlockchainWriteBatch insertKeyOutputAmounts(SortedSet<uint64_t> amounts, uint32_t totalKeyOutputAmountsCount)
  {
	Debug.Assert(totalKeyOutputAmountsCount >= amounts.Count);
	rawDataToInsert.Capacity = rawDataToInsert.Count + amounts.Count + 1;
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.KEY_OUTPUT_AMOUNTS_COUNT_PREFIX, DB.GlobalMembers.KEY_OUTPUT_AMOUNTS_COUNT_KEY, totalKeyOutputAmountsCount));
	uint32_t currentAmountId = totalKeyOutputAmountsCount - (uint32_t)amounts.Count;

	foreach (uint64_t amount in amounts)
	{
	  rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.KEY_OUTPUT_AMOUNTS_COUNT_PREFIX, currentAmountId++, amount));
	}

	return this;
  }
  public BlockchainWriteBatch insertTimestamp(uint64_t timestamp, List<Crypto.Hash> blockHashes)
  {
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.TIMESTAMP_TO_BLOCKHASHES_PREFIX, timestamp, blockHashes));
	return this;
  }
  public BlockchainWriteBatch insertKeyOutputInfo(uint64_t amount, uint32_t globalIndex, KeyOutputInfo outputInfo)
  {
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.KEY_OUTPUT_KEY_PREFIX, Tuple.Create(amount, globalIndex), outputInfo));
	return this;
  }

  public BlockchainWriteBatch removeSpentKeyImages(uint32_t blockIndex, List<Crypto.KeyImage> spentKeyImages)
  {
	rawKeysToRemove.Capacity = rawKeysToRemove.Count + spentKeyImages.Count + 1;
	rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.BLOCK_INDEX_TO_KEY_IMAGE_PREFIX, blockIndex));

	foreach (Crypto  in :KeyImage & keyImage : spentKeyImages)
	{
	  rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.KEY_IMAGE_TO_BLOCK_INDEX_PREFIX, keyImage));
	}

	return this;
  }
  public BlockchainWriteBatch removeCachedTransaction(Crypto.Hash transactionHash, uint64_t totalTxsCount)
  {
	rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.TRANSACTION_HASH_TO_TRANSACTION_INFO_PREFIX, transactionHash));
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.TRANSACTION_HASH_TO_TRANSACTION_INFO_PREFIX, DB.GlobalMembers.TRANSACTIONS_COUNT_KEY, totalTxsCount));
	return this;
  }
  public BlockchainWriteBatch removePaymentId(Crypto.Hash paymentId, uint32_t totalTxsCountForPaymentId)
  {
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.PAYMENT_ID_TO_TX_HASH_PREFIX, paymentId, totalTxsCountForPaymentId));
	rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.PAYMENT_ID_TO_TX_HASH_PREFIX, Tuple.Create(paymentId, totalTxsCountForPaymentId)));
	return this;
  }
  public BlockchainWriteBatch removeCachedBlock(Crypto.Hash blockHash, uint32_t blockIndex)
  {
	rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.BLOCK_INDEX_TO_BLOCK_INFO_PREFIX, blockIndex));
	rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.BLOCK_INDEX_TO_TX_HASHES_PREFIX, blockIndex));
	rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.BLOCK_HASH_TO_BLOCK_INDEX_PREFIX, blockHash));
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.BLOCK_INDEX_TO_BLOCK_HASH_PREFIX, DB.GlobalMembers.LAST_BLOCK_INDEX_KEY, blockIndex - 1));
	return this;
  }
  public BlockchainWriteBatch removeKeyOutputGlobalIndexes(uint64_t amount, uint32_t outputsToRemoveCount, uint32_t totalOutputsCountForAmount)
  {
	rawKeysToRemove.Capacity = rawKeysToRemove.Count + outputsToRemoveCount;
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.KEY_OUTPUT_AMOUNT_PREFIX, amount, totalOutputsCountForAmount));
	for (uint32_t i = 0; i < outputsToRemoveCount; ++i)
	{
	  rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.KEY_OUTPUT_AMOUNT_PREFIX, Tuple.Create(amount, totalOutputsCountForAmount + i)));
	}
	return this;
  }
  public BlockchainWriteBatch removeRawBlock(uint32_t blockIndex)
  {
	rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.BLOCK_INDEX_TO_RAW_BLOCK_PREFIX, blockIndex));
	return this;
  }
  public BlockchainWriteBatch removeClosestTimestampBlockIndex(uint64_t timestamp)
  {
	rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.CLOSEST_TIMESTAMP_BLOCK_INDEX_PREFIX, timestamp));
	return this;
  }
  public BlockchainWriteBatch removeTimestamp(uint64_t timestamp)
  {
	rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.TIMESTAMP_TO_BLOCKHASHES_PREFIX, timestamp));
	return this;
  }
  public BlockchainWriteBatch removeKeyOutputAmounts(uint32_t keyOutputAmountsToRemoveCount, uint32_t totalKeyOutputAmountsCount)
  {
	rawKeysToRemove.Capacity = rawKeysToRemove.Count + keyOutputAmountsToRemoveCount;
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.KEY_OUTPUT_AMOUNTS_COUNT_PREFIX, DB.GlobalMembers.KEY_OUTPUT_AMOUNTS_COUNT_KEY, totalKeyOutputAmountsCount));
	for (uint32_t i = 0; i < keyOutputAmountsToRemoveCount; ++i)
	{
	  rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.KEY_OUTPUT_AMOUNTS_COUNT_PREFIX, totalKeyOutputAmountsCount + i));
	}

	return this;
  }
  public BlockchainWriteBatch removeKeyOutputInfo(uint64_t amount, uint32_t globalIndex)
  {
	rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.KEY_OUTPUT_KEY_PREFIX, Tuple.Create(amount, globalIndex)));
	return this;
  }

  public List<Tuple<string, string>> extractRawDataToInsert()
  {
	return std::move(rawDataToInsert);
  }
  public List<string> extractRawKeysToRemove()
  {
	return std::move(rawKeysToRemove);
  }
  private List<Tuple<string, string>> rawDataToInsert = new List<Tuple<string, string>>();
  private List<string> rawKeysToRemove = new List<string>();
}

}
