// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


using System;
using System.Collections.Generic;
using System.Diagnostics;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);
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

public class TransactionExtraPadding
{
  public size_t size = new size_t();
}

public class TransactionExtraPublicKey
{
  public Crypto.PublicKey publicKey = new Crypto.PublicKey();
}

public class TransactionExtraNonce
{
  public List<ushort> nonce = new List<ushort>();
}

public class TransactionExtraMergeMiningTag
{
  public size_t depth = new size_t();
  public Crypto.Hash merkleRoot = new Crypto.Hash();
}

// tx_extra_field format, except tx_extra_padding and tx_extra_pub_key:
//   varint tag;
//   varint size;
//   varint data[];



//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename T>

}


namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

public class DbOutputConstIterator: boost::iterator_facade<DbOutputConstIterator, PackedOutIndex, boost::random_access_traversal_tag>
{
  public DbOutputConstIterator(Func<IBlockchainCache.Amount amount, uint globalOutputIndex, PackedOutIndex> retriever_, IBlockchainCache.Amount amount_, uint globalOutputIndex_)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.retriever = retriever_;
	  this.retriever.CopyFrom(retriever_);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.amount = amount_;
	  this.amount.CopyFrom(amount_);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.globalOutputIndex = globalOutputIndex_;
	  this.globalOutputIndex.CopyFrom(globalOutputIndex_);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const PackedOutIndex& dereference() const
  public PackedOutIndex dereference()
  {
	cachedValue = retriever(amount, globalOutputIndex);
	return cachedValue;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool equal(const DbOutputConstIterator& other) const
  public bool equal(DbOutputConstIterator other)
  {
	return globalOutputIndex == other.globalOutputIndex;
  }

  public void increment()
  {
	++globalOutputIndex;
  }

  public void decrement()
  {
	--globalOutputIndex;
  }

  public void advance(difference_type n)
  {
	Debug.Assert(n >= -(difference_type)globalOutputIndex);
	globalOutputIndex += (uint)n;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: difference_type distance_to(const DbOutputConstIterator& to) const
  public difference_type distance_to(DbOutputConstIterator to)
  {
	return (difference_type)to.globalOutputIndex - (difference_type)globalOutputIndex;
  }

  private Func<IBlockchainCache.Amount amount, uint globalOutputIndex, PackedOutIndex> retriever;
  private IBlockchainCache.Amount amount = new IBlockchainCache.Amount();
  private uint globalOutputIndex = new uint();
  private PackedOutIndex cachedValue = new PackedOutIndex();
}

public class DatabaseVersionReadBatch: IReadBatch, System.IDisposable
{
  public virtual void Dispose()
  {
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<string> getRawKeys() const override
  public override List<string> getRawKeys()
  {
	return new List<string>() {GlobalMembers.DB_VERSION_KEY};
  }

  public override void submitRawResult(List<string> values, List<bool> resultStates)
  {
	Debug.Assert(values.Count == 1);
	Debug.Assert(resultStates.Count == values.Count);

	if (!resultStates[0])
	{
	  return;
	}

	version = (uint)Convert.ToInt32(values[0]);
  }

  public boost.optional<uint> getDbSchemeVersion()
  {
	return version;
  }

  private boost.optional<uint> version;
}

public class DatabaseVersionWriteBatch: IWriteBatch, System.IDisposable
{
  public DatabaseVersionWriteBatch(uint version)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.schemeVersion = version;
	  this.schemeVersion.CopyFrom(version);
  }
  public virtual void Dispose()
  {
  }

  public override List<Tuple<string, string>> extractRawDataToInsert()
  {
	return new List<Tuple<string, string>>() {make_pair(GlobalMembers.DB_VERSION_KEY, Convert.ToString(schemeVersion))};
  }

  public override List<string> extractRawKeysToRemove()
  {
	return new List<string>();
  }

  private uint schemeVersion = new uint();
}


//C++ TO C# CONVERTER WARNING: The original type declaration contained unconverted modifiers:
//ORIGINAL LINE: struct DatabaseBlockchainCache::ExtendedPushedBlockInfo
public class ExtendedPushedBlockInfo
{
  public PushedBlockInfo pushedBlockInfo = new PushedBlockInfo();
  public ulong timestamp = new ulong();
}





/*
 * This methods splits cache, upper part (ie blocks with indexes greater or equal to splitBlockIndex)
 * is copied to new BlockchainCache
 */

//returns hash of pushed block
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:










//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint DatabaseBlockchainCache::updateKeyOutputCount(Amount amount, int diff) const



//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: PushedBlockInfo DatabaseBlockchainCache::getPushedBlockInfo(uint blockIndex) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool DatabaseBlockchainCache::checkIfSpent(const Crypto::KeyImage& keyImage, uint blockIndex) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool DatabaseBlockchainCache::checkIfSpent(const Crypto::KeyImage& keyImage) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool DatabaseBlockchainCache::isTransactionSpendTimeUnlocked(ulong unlockTime) const

// TODO: pass time
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool DatabaseBlockchainCache::isTransactionSpendTimeUnlocked(ulong unlockTime, uint blockIndex) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ExtractOutputKeysResult DatabaseBlockchainCache::extractKeyOutputKeys(ulong amount, Common::ArrayView<uint> globalIndexes, ClassicVector<Crypto::PublicKey>& publicKeys) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ExtractOutputKeysResult DatabaseBlockchainCache::extractKeyOutputKeys(ulong amount, uint blockIndex, Common::ArrayView<uint> globalIndexes, ClassicVector<Crypto::PublicKey>& publicKeys) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ExtractOutputKeysResult DatabaseBlockchainCache::extractKeyOtputIndexes(ulong amount, Common::ArrayView<uint> globalIndexes, ClassicVector<PackedOutIndex>& outIndexes) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ExtractOutputKeysResult DatabaseBlockchainCache::extractKeyOtputReferences(ulong amount, Common::ArrayView<uint> globalIndexes, ClassicVector<System.Tuple<Crypto::Hash, size_t>>& outputReferences) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint DatabaseBlockchainCache::getTopBlockIndex() const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ushort DatabaseBlockchainCache::getBlockMajorVersionForHeight(uint height) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong DatabaseBlockchainCache::getCachedTransactionsCount() const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Crypto::Hash& DatabaseBlockchainCache::getTopBlockHash() const
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint DatabaseBlockchainCache::getBlockCount() const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool DatabaseBlockchainCache::hasBlock(const Crypto::Hash& blockHash) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint DatabaseBlockchainCache::getBlockIndex(const Crypto::Hash& blockHash) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool DatabaseBlockchainCache::hasTransaction(const Crypto::Hash& transactionHash) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<ulong> DatabaseBlockchainCache::getLastTimestamps(size_t count) const
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<ulong> DatabaseBlockchainCache::getLastTimestamps(size_t count, uint blockIndex, UseGenesis useGenesis) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<ulong> DatabaseBlockchainCache::getLastBlocksSizes(size_t count) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<ulong> DatabaseBlockchainCache::getLastBlocksSizes(size_t count, uint blockIndex, UseGenesis useGenesis) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<ulong> DatabaseBlockchainCache::getLastCumulativeDifficulties(size_t count, uint blockIndex, UseGenesis useGenesis) const
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<ulong> DatabaseBlockchainCache::getLastCumulativeDifficulties(size_t count) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong DatabaseBlockchainCache::getDifficultyForNextBlock() const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong DatabaseBlockchainCache::getDifficultyForNextBlock(uint blockIndex) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong DatabaseBlockchainCache::getCurrentCumulativeDifficulty() const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong DatabaseBlockchainCache::getCurrentCumulativeDifficulty(uint blockIndex) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: CachedBlockInfo DatabaseBlockchainCache::getCachedBlockInfo(uint index) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong DatabaseBlockchainCache::getAlreadyGeneratedCoins() const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong DatabaseBlockchainCache::getAlreadyGeneratedCoins(uint blockIndex) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong DatabaseBlockchainCache::getAlreadyGeneratedTransactions(uint blockIndex) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<CachedBlockInfo> DatabaseBlockchainCache::getLastCachedUnits(uint blockIndex, size_t count, UseGenesis useGenesis) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<CachedBlockInfo> DatabaseBlockchainCache::getLastDbUnits(uint blockIndex, size_t count, UseGenesis useGenesis) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<ulong> DatabaseBlockchainCache::getLastUnits(size_t count, uint blockIndex, UseGenesis useGenesis, System.Func<const CachedBlockInfo&, ulong> pred) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Crypto::Hash DatabaseBlockchainCache::getBlockHash(uint blockIndex) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<Crypto::Hash> DatabaseBlockchainCache::getBlockHashes(uint startIndex, size_t maxCount) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: IBlockchainCache* DatabaseBlockchainCache::getParent() const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint DatabaseBlockchainCache::getStartBlockIndex() const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t DatabaseBlockchainCache::getKeyOutputsCountForAmount(ulong amount, uint blockIndex) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint DatabaseBlockchainCache::getTimestampLowerBoundBlockIndex(ulong timestamp) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool DatabaseBlockchainCache::getTransactionGlobalIndexes(const Crypto::Hash& transactionHash, ClassicVector<uint>& globalIndexes) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t DatabaseBlockchainCache::getTransactionCount() const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint DatabaseBlockchainCache::getBlockIndexContainingTx(const Crypto::Hash& transactionHash) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t DatabaseBlockchainCache::getChildCount() const



//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<ClassicVector<ushort>> DatabaseBlockchainCache::getRawTransactions(const ClassicVector<Crypto::Hash>& transactions, ClassicVector<Crypto::Hash>& missedTransactions) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<ClassicVector<ushort>> DatabaseBlockchainCache::getRawTransactions(const ClassicVector<Crypto::Hash>& transactions) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void DatabaseBlockchainCache::getRawTransactions(const ClassicVector<Crypto::Hash>& transactions, ClassicVector<ClassicVector<ushort>>& foundTransactions, ClassicVector<Crypto::Hash>& missedTransactions) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: RawBlock DatabaseBlockchainCache::getBlockByIndex(uint index) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<ushort> DatabaseBlockchainCache::getRawTransaction(uint blockIndex, uint transactionIndex) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<Crypto::Hash> DatabaseBlockchainCache::getTransactionHashes() const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<uint> DatabaseBlockchainCache::getRandomOutsByAmount(ulong amount, size_t count, uint blockIndex) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ExtractOutputKeysResult DatabaseBlockchainCache::extractKeyOutputs(ulong amount, uint blockIndex, Common::ArrayView<uint> globalIndexes, System.Func<const CachedTransactionInfo& info, PackedOutIndex index, uint globalIndex, ExtractOutputKeysResult> callback) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<Crypto::Hash> DatabaseBlockchainCache::getTransactionHashesByPaymentId(const Crypto::Hash& paymentId) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<Crypto::Hash> DatabaseBlockchainCache::getBlockHashesByTimestamps(ulong timestampBegin, size_t secondsCount) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: DatabaseBlockchainCache::ExtendedPushedBlockInfo DatabaseBlockchainCache::getExtendedPushedBlockInfo(uint blockIndex) const




//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: BlockchainReadResult DatabaseBlockchainCache::readDatabase(BlockchainReadBatch& batch) const

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:

}
