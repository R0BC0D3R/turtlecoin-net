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
//ORIGINAL LINE: #define CRYPTO_MAKE_HASHABLE(type) CRYPTO_MAKE_COMPARABLE(type) namespace Crypto { static_assert(sizeof(uint) <= sizeof(type), "Size of " #type " must be at least that of uint"); inline uint hash_value(const type &_v) { return reinterpret_cast<const uint &>(_v); } } namespace std { template<> struct hash<Crypto::type> { uint operator()(const Crypto::type &_v) const { return reinterpret_cast<const uint &>(_v); } }; }
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

  public BlockchainWriteBatch insertSpentKeyImages(uint blockIndex, HashSet<Crypto.KeyImage> spentKeyImages)
  {
	rawDataToInsert.Capacity = rawDataToInsert.Count + spentKeyImages.Count + 1;
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.BLOCK_INDEX_TO_KEY_IMAGE_PREFIX, blockIndex, spentKeyImages));
	foreach (Crypto  in :KeyImage & keyImage : spentKeyImages)
	{
	  rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.KEY_IMAGE_TO_BLOCK_INDEX_PREFIX, keyImage, blockIndex));
	}
	return this;
  }
  public BlockchainWriteBatch insertCachedTransaction(ExtendedTransactionInfo transaction, ulong totalTxsCount)
  {
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.TRANSACTION_HASH_TO_TRANSACTION_INFO_PREFIX, transaction.transactionHash, transaction));
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.TRANSACTION_HASH_TO_TRANSACTION_INFO_PREFIX, DB.GlobalMembers.TRANSACTIONS_COUNT_KEY, totalTxsCount));
	return this;
  }
  public BlockchainWriteBatch insertPaymentId(Crypto.Hash transactionHash, Crypto.Hash paymentId, uint totalTxsCountForPaymentId)
  {
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.PAYMENT_ID_TO_TX_HASH_PREFIX, paymentId, totalTxsCountForPaymentId));
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.PAYMENT_ID_TO_TX_HASH_PREFIX, Tuple.Create(paymentId, totalTxsCountForPaymentId - 1), transactionHash));
	return this;
  }
  public BlockchainWriteBatch insertCachedBlock(CachedBlockInfo block, uint blockIndex, List<Crypto.Hash> blockTxs)
  {
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.BLOCK_INDEX_TO_BLOCK_INFO_PREFIX, blockIndex, block));
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.BLOCK_INDEX_TO_TX_HASHES_PREFIX, blockIndex, blockTxs));
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.BLOCK_HASH_TO_BLOCK_INDEX_PREFIX, block.blockHash, blockIndex));
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.BLOCK_INDEX_TO_BLOCK_HASH_PREFIX, DB.GlobalMembers.LAST_BLOCK_INDEX_KEY, blockIndex));
	return this;
  }
  public BlockchainWriteBatch insertKeyOutputGlobalIndexes(ulong amount, List<PackedOutIndex> outputs, uint totalOutputsCountForAmount)
  {
	Debug.Assert(totalOutputsCountForAmount >= outputs.Count);
	rawDataToInsert.Capacity = rawDataToInsert.Count + outputs.Count + 1;
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.KEY_OUTPUT_AMOUNT_PREFIX, amount, totalOutputsCountForAmount));
	uint currentOutputId = totalOutputsCountForAmount - (uint)outputs.Count;

	foreach (PackedOutIndex outIndex in outputs)
	{
	  rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.KEY_OUTPUT_AMOUNT_PREFIX, Tuple.Create(amount, currentOutputId++), outIndex));
	}

	return this;
  }
  public BlockchainWriteBatch insertRawBlock(uint blockIndex, RawBlock block)
  {
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.BLOCK_INDEX_TO_RAW_BLOCK_PREFIX, blockIndex, block));
	return this;
  }
  public BlockchainWriteBatch insertClosestTimestampBlockIndex(ulong timestamp, uint blockIndex)
  {
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.CLOSEST_TIMESTAMP_BLOCK_INDEX_PREFIX, timestamp, blockIndex));
	return this;
  }
  public BlockchainWriteBatch insertKeyOutputAmounts(SortedSet<ulong> amounts, uint totalKeyOutputAmountsCount)
  {
	Debug.Assert(totalKeyOutputAmountsCount >= amounts.Count);
	rawDataToInsert.Capacity = rawDataToInsert.Count + amounts.Count + 1;
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.KEY_OUTPUT_AMOUNTS_COUNT_PREFIX, DB.GlobalMembers.KEY_OUTPUT_AMOUNTS_COUNT_KEY, totalKeyOutputAmountsCount));
	uint currentAmountId = totalKeyOutputAmountsCount - (uint)amounts.Count;

	foreach (ulong amount in amounts)
	{
	  rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.KEY_OUTPUT_AMOUNTS_COUNT_PREFIX, currentAmountId++, amount));
	}

	return this;
  }
  public BlockchainWriteBatch insertTimestamp(ulong timestamp, List<Crypto.Hash> blockHashes)
  {
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.TIMESTAMP_TO_BLOCKHASHES_PREFIX, timestamp, blockHashes));
	return this;
  }
  public BlockchainWriteBatch insertKeyOutputInfo(ulong amount, uint globalIndex, KeyOutputInfo outputInfo)
  {
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.KEY_OUTPUT_KEY_PREFIX, Tuple.Create(amount, globalIndex), outputInfo));
	return this;
  }

  public BlockchainWriteBatch removeSpentKeyImages(uint blockIndex, List<Crypto.KeyImage> spentKeyImages)
  {
	rawKeysToRemove.Capacity = rawKeysToRemove.Count + spentKeyImages.Count + 1;
	rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.BLOCK_INDEX_TO_KEY_IMAGE_PREFIX, blockIndex));

	foreach (Crypto  in :KeyImage & keyImage : spentKeyImages)
	{
	  rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.KEY_IMAGE_TO_BLOCK_INDEX_PREFIX, keyImage));
	}

	return this;
  }
  public BlockchainWriteBatch removeCachedTransaction(Crypto.Hash transactionHash, ulong totalTxsCount)
  {
	rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.TRANSACTION_HASH_TO_TRANSACTION_INFO_PREFIX, transactionHash));
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.TRANSACTION_HASH_TO_TRANSACTION_INFO_PREFIX, DB.GlobalMembers.TRANSACTIONS_COUNT_KEY, totalTxsCount));
	return this;
  }
  public BlockchainWriteBatch removePaymentId(Crypto.Hash paymentId, uint totalTxsCountForPaymentId)
  {
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.PAYMENT_ID_TO_TX_HASH_PREFIX, paymentId, totalTxsCountForPaymentId));
	rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.PAYMENT_ID_TO_TX_HASH_PREFIX, Tuple.Create(paymentId, totalTxsCountForPaymentId)));
	return this;
  }
  public BlockchainWriteBatch removeCachedBlock(Crypto.Hash blockHash, uint blockIndex)
  {
	rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.BLOCK_INDEX_TO_BLOCK_INFO_PREFIX, blockIndex));
	rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.BLOCK_INDEX_TO_TX_HASHES_PREFIX, blockIndex));
	rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.BLOCK_HASH_TO_BLOCK_INDEX_PREFIX, blockHash));
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.BLOCK_INDEX_TO_BLOCK_HASH_PREFIX, DB.GlobalMembers.LAST_BLOCK_INDEX_KEY, blockIndex - 1));
	return this;
  }
  public BlockchainWriteBatch removeKeyOutputGlobalIndexes(ulong amount, uint outputsToRemoveCount, uint totalOutputsCountForAmount)
  {
	rawKeysToRemove.Capacity = rawKeysToRemove.Count + outputsToRemoveCount;
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.KEY_OUTPUT_AMOUNT_PREFIX, amount, totalOutputsCountForAmount));
	for (uint i = 0; i < outputsToRemoveCount; ++i)
	{
	  rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.KEY_OUTPUT_AMOUNT_PREFIX, Tuple.Create(amount, totalOutputsCountForAmount + i)));
	}
	return this;
  }
  public BlockchainWriteBatch removeRawBlock(uint blockIndex)
  {
	rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.BLOCK_INDEX_TO_RAW_BLOCK_PREFIX, blockIndex));
	return this;
  }
  public BlockchainWriteBatch removeClosestTimestampBlockIndex(ulong timestamp)
  {
	rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.CLOSEST_TIMESTAMP_BLOCK_INDEX_PREFIX, timestamp));
	return this;
  }
  public BlockchainWriteBatch removeTimestamp(ulong timestamp)
  {
	rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.TIMESTAMP_TO_BLOCKHASHES_PREFIX, timestamp));
	return this;
  }
  public BlockchainWriteBatch removeKeyOutputAmounts(uint keyOutputAmountsToRemoveCount, uint totalKeyOutputAmountsCount)
  {
	rawKeysToRemove.Capacity = rawKeysToRemove.Count + keyOutputAmountsToRemoveCount;
	rawDataToInsert.emplace_back(DB.GlobalMembers.serialize(DB.GlobalMembers.KEY_OUTPUT_AMOUNTS_COUNT_PREFIX, DB.GlobalMembers.KEY_OUTPUT_AMOUNTS_COUNT_KEY, totalKeyOutputAmountsCount));
	for (uint i = 0; i < keyOutputAmountsToRemoveCount; ++i)
	{
	  rawKeysToRemove.emplace_back(DB.GlobalMembers.serializeKey(DB.GlobalMembers.KEY_OUTPUT_AMOUNTS_COUNT_PREFIX, totalKeyOutputAmountsCount + i));
	}

	return this;
  }
  public BlockchainWriteBatch removeKeyOutputInfo(ulong amount, uint globalIndex)
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
