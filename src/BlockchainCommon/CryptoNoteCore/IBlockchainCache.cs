// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System;
using System.Collections.Generic;

namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class ISerializer;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//struct TransactionValidatorState;

public enum ExtractOutputKeysResult
{
  SUCCESS,
  INVALID_GLOBAL_INDEX,
  OUTPUT_LOCKED
}

//C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#:
//union PackedOutIndex

public class PushedBlockInfo
{
  public RawBlock rawBlock = new RawBlock();
  public TransactionValidatorState validatorState = new TransactionValidatorState();
  public uint blockSize;
  public ulong generatedCoins;
  public ulong blockDifficulty;
}

public class UseGenesis
{
  public UseGenesis(bool u)
  {
	  this.use = u;
  }
  // emulate boolean flag
  public static implicit operator bool(UseGenesis ImpliedObject)
  {
	return ImpliedObject.use;
  }

  private bool use = false;
}

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//struct CachedBlockInfo;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//struct CachedTransactionInfo;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class ITransactionPool;

public abstract class IBlockchainCache : System.IDisposable
{

  public virtual void Dispose()
  {
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual RawBlock getBlockByIndex(uint index) const = 0;
  public abstract RawBlock getBlockByIndex(uint index);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual BinaryArray getRawTransaction(uint blockIndex, uint transactionIndex) const = 0;
  public abstract BinaryArray getRawTransaction(uint blockIndex, uint transactionIndex);
  public abstract std::unique_ptr<IBlockchainCache> split(uint splitBlockIndex);
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public abstract void pushBlock(CachedBlock cachedBlock, List<CachedTransaction> cachedTransactions, TransactionValidatorState validatorState, uint blockSize, ulong generatedCoins, ulong blockDifficulty, RawBlock && rawBlock);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual PushedBlockInfo getPushedBlockInfo(uint index) const = 0;
  public abstract PushedBlockInfo getPushedBlockInfo(uint index);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool checkIfSpent(const Crypto::KeyImage& keyImage, uint blockIndex) const = 0;
  public abstract bool checkIfSpent(Crypto.KeyImage keyImage, uint blockIndex);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool checkIfSpent(const Crypto::KeyImage& keyImage) const = 0;
  public abstract bool checkIfSpent(Crypto.KeyImage keyImage);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool isTransactionSpendTimeUnlocked(ulong unlockTime) const = 0;
  public abstract bool isTransactionSpendTimeUnlocked(ulong unlockTime);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool isTransactionSpendTimeUnlocked(ulong unlockTime, uint blockIndex) const = 0;
  public abstract bool isTransactionSpendTimeUnlocked(ulong unlockTime, uint blockIndex);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ExtractOutputKeysResult extractKeyOutputKeys(ulong amount, Common::ArrayView<uint> globalIndexes, ClassicVector<Crypto::PublicKey>& publicKeys) const = 0;
  public abstract ExtractOutputKeysResult extractKeyOutputKeys(ulong amount, Common.ArrayView<uint> globalIndexes, List<Crypto.PublicKey> publicKeys);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ExtractOutputKeysResult extractKeyOutputKeys(ulong amount, uint blockIndex, Common::ArrayView<uint> globalIndexes, ClassicVector<Crypto::PublicKey>& publicKeys) const = 0;
  public abstract ExtractOutputKeysResult extractKeyOutputKeys(ulong amount, uint blockIndex, Common.ArrayView<uint> globalIndexes, List<Crypto.PublicKey> publicKeys);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ExtractOutputKeysResult extractKeyOtputIndexes(ulong amount, Common::ArrayView<uint> globalIndexes, ClassicVector<PackedOutIndex>& outIndexes) const = 0;
  public abstract ExtractOutputKeysResult extractKeyOtputIndexes(ulong amount, Common.ArrayView<uint> globalIndexes, List<PackedOutIndex> outIndexes);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ExtractOutputKeysResult extractKeyOtputReferences(ulong amount, Common::ArrayView<uint> globalIndexes, ClassicVector<System.Tuple<Crypto::Hash, uint>>& outputReferences) const = 0;
  public abstract ExtractOutputKeysResult extractKeyOtputReferences(ulong amount, Common.ArrayView<uint> globalIndexes, List<Tuple<Crypto.Hash, uint>> outputReferences);
  //TODO: get rid of pred in this method. return vector of KeyOutputInfo structures
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ExtractOutputKeysResult extractKeyOutputs(ulong amount, uint blockIndex, Common::ArrayView<uint> globalIndexes, System.Func<const CachedTransactionInfo& info, PackedOutIndex index, uint globalIndex, ExtractOutputKeysResult> pred) const = 0;
  public abstract ExtractOutputKeysResult extractKeyOutputs(ulong amount, uint blockIndex, Common.ArrayView<uint> globalIndexes, Func<CachedTransactionInfo info, PackedOutIndex index, uint globalIndex, ExtractOutputKeysResult> pred);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getTopBlockIndex() const = 0;
  public abstract uint getTopBlockIndex();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual const Crypto::Hash& getTopBlockHash() const = 0;
  public abstract Crypto.Hash getTopBlockHash();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getBlockCount() const = 0;
  public abstract uint getBlockCount();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool hasBlock(const Crypto::Hash& blockHash) const = 0;
  public abstract bool hasBlock(Crypto.Hash blockHash);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getBlockIndex(const Crypto::Hash& blockHash) const = 0;
  public abstract uint getBlockIndex(Crypto.Hash blockHash);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool hasTransaction(const Crypto::Hash& transactionHash) const = 0;
  public abstract bool hasTransaction(Crypto.Hash transactionHash);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<ulong> getLastTimestamps(uint count) const = 0;
  public abstract List<ulong> getLastTimestamps(uint count);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<ulong> getLastTimestamps(uint count, uint blockIndex, UseGenesis) const = 0;
  public abstract List<ulong> getLastTimestamps(uint count, uint blockIndex, UseGenesis UnnamedParameter);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<ulong> getLastBlocksSizes(uint count) const = 0;
  public abstract List<ulong> getLastBlocksSizes(uint count);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<ulong> getLastBlocksSizes(uint count, uint blockIndex, UseGenesis) const = 0;
  public abstract List<ulong> getLastBlocksSizes(uint count, uint blockIndex, UseGenesis UnnamedParameter);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<ulong> getLastCumulativeDifficulties(uint count, uint blockIndex, UseGenesis) const = 0;
  public abstract List<ulong> getLastCumulativeDifficulties(uint count, uint blockIndex, UseGenesis UnnamedParameter);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<ulong> getLastCumulativeDifficulties(uint count) const = 0;
  public abstract List<ulong> getLastCumulativeDifficulties(uint count);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getDifficultyForNextBlock() const = 0;
  public abstract ulong getDifficultyForNextBlock();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getDifficultyForNextBlock(uint blockIndex) const = 0;
  public abstract ulong getDifficultyForNextBlock(uint blockIndex);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getCurrentCumulativeDifficulty() const = 0;
  public abstract ulong getCurrentCumulativeDifficulty();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getCurrentCumulativeDifficulty(uint blockIndex) const = 0;
  public abstract ulong getCurrentCumulativeDifficulty(uint blockIndex);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getAlreadyGeneratedCoins() const = 0;
  public abstract ulong getAlreadyGeneratedCoins();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getAlreadyGeneratedCoins(uint blockIndex) const = 0;
  public abstract ulong getAlreadyGeneratedCoins(uint blockIndex);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getAlreadyGeneratedTransactions(uint blockIndex) const = 0;
  public abstract ulong getAlreadyGeneratedTransactions(uint blockIndex);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual Crypto::Hash getBlockHash(uint blockIndex) const = 0;
  public abstract Crypto.Hash getBlockHash(uint blockIndex);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getBlockHashes(uint startIndex, uint maxCount) const = 0;
  public abstract List<Crypto.Hash> getBlockHashes(uint startIndex, uint maxCount);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual IBlockchainCache* getParent() const = 0;
  public abstract IBlockchainCache getParent();
  public abstract void setParent(IBlockchainCache parent);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getStartBlockIndex() const = 0;
  public abstract uint getStartBlockIndex();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getKeyOutputsCountForAmount(ulong amount, uint blockIndex) const = 0;
  public abstract uint getKeyOutputsCountForAmount(ulong amount, uint blockIndex);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getTimestampLowerBoundBlockIndex(ulong timestamp) const = 0;
  public abstract uint getTimestampLowerBoundBlockIndex(ulong timestamp);

  //NOTE: shouldn't be recursive otherwise we'll get quadratic complexity
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual void getRawTransactions(const ClassicVector<Crypto::Hash>& transactions, ClassicVector<BinaryArray>& foundTransactions, ClassicVector<Crypto::Hash>& missedTransactions) const = 0;
  public abstract void getRawTransactions(List<Crypto.Hash> transactions, List<BinaryArray> foundTransactions, List<Crypto.Hash> missedTransactions);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<BinaryArray> getRawTransactions(const ClassicVector<Crypto::Hash> &transactions, ClassicVector<Crypto::Hash> &missedTransactions) const = 0;
  public abstract List<BinaryArray> getRawTransactions(List<Crypto.Hash> transactions, List<Crypto.Hash> missedTransactions);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<BinaryArray> getRawTransactions(const ClassicVector<Crypto::Hash> &transactions) const = 0;
  public abstract List<BinaryArray> getRawTransactions(List<Crypto.Hash> transactions);

  //NOTE: not recursive!
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getTransactionGlobalIndexes(const Crypto::Hash& transactionHash, ClassicVector<uint>& globalIndexes) const = 0;
  public abstract bool getTransactionGlobalIndexes(Crypto.Hash transactionHash, List<uint> globalIndexes);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getTransactionCount() const = 0;
  public abstract uint getTransactionCount();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getBlockIndexContainingTx(const Crypto::Hash& transactionHash) const = 0;
  public abstract uint getBlockIndexContainingTx(Crypto.Hash transactionHash);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getChildCount() const = 0;
  public abstract uint getChildCount();
  public abstract void addChild(IBlockchainCache UnnamedParameter);
  public abstract bool deleteChild(IBlockchainCache UnnamedParameter);

  public abstract void save();
  public abstract void load();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<ulong> getLastUnits(uint count, uint blockIndex, UseGenesis use, System.Func<const CachedBlockInfo&, ulong> pred) const = 0;
  public abstract List<ulong> getLastUnits(uint count, uint blockIndex, UseGenesis use, Func<CachedBlockInfo , ulong> pred);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getTransactionHashes() const = 0;
  public abstract List<Crypto.Hash> getTransactionHashes();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<uint> getRandomOutsByAmount(ulong amount, uint count, uint blockIndex) const = 0;
  public abstract List<uint> getRandomOutsByAmount(ulong amount, uint count, uint blockIndex);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getTransactionHashesByPaymentId(const Crypto::Hash& paymentId) const = 0;
  public abstract List<Crypto.Hash> getTransactionHashesByPaymentId(Crypto.Hash paymentId);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getBlockHashesByTimestamps(ulong timestampBegin, uint secondsCount) const = 0;
  public abstract List<Crypto.Hash> getBlockHashesByTimestamps(ulong timestampBegin, uint secondsCount);
}

}
