// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


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

using Crypto;
using CryptoNote;
using System.Collections.Generic;

namespace CryptoNote
{

public class CachedBlock
{
  public CachedBlock(BlockTemplate block)
  {
	  this.block = block;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const BlockTemplate& getBlock() const
  public BlockTemplate getBlock()
  {
	return block;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Crypto::Hash& getTransactionTreeHash() const
  public Crypto.Hash getTransactionTreeHash()
  {
	if (!transactionTreeHash.is_initialized())
	{
	  List<Crypto.Hash> transactionHashes = new List<Crypto.Hash>();
	  transactionHashes.Capacity = block.transactionHashes.size() + 1;
	  transactionHashes.Add(CryptoNote.GlobalMembers.getObjectHash(block.baseTransaction));
	  transactionHashes.AddRange(block.transactionHashes);
	  transactionTreeHash = Crypto.Hash();
	  Crypto.GlobalMembers.tree_hash(transactionHashes.data(), transactionHashes.Count, transactionTreeHash.get());
	}

	return transactionTreeHash.get();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Crypto::Hash& getBlockHash() const
  public Crypto.Hash getBlockHash()
  {
	if (!blockHash.is_initialized())
	{
	  BinaryArray blockBinaryArray = getBlockHashingBinaryArray();
	  if (BLOCK_MAJOR_VERSION_2 <= block.majorVersion)
	  {
		auto parentBlock = getParentBlockHashingBinaryArray(false);
		blockBinaryArray.insert(blockBinaryArray.end(), parentBlock.begin(), parentBlock.end());
	  }

	  blockHash = CryptoNote.GlobalMembers.getObjectHash(blockBinaryArray);
	}

	return blockHash.get();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Crypto::Hash& getBlockLongHash() const
  public Crypto.Hash getBlockLongHash()
  {
	if (!blockLongHash.is_initialized())
	{
	  if (block.majorVersion == BLOCK_MAJOR_VERSION_1)
	  {
		auto rawHashingBlock = getBlockHashingBinaryArray();
		blockLongHash = Hash();
		Crypto.GlobalMembers.cn_slow_hash_v0(rawHashingBlock.data(), rawHashingBlock.size(), blockLongHash.get());
	  }
	  else if ((block.majorVersion == BLOCK_MAJOR_VERSION_2) || (block.majorVersion == BLOCK_MAJOR_VERSION_3))
	  {
		auto rawHashingBlock = getParentBlockHashingBinaryArray(true);
		blockLongHash = Hash();
		Crypto.GlobalMembers.cn_slow_hash_v0(rawHashingBlock.data(), rawHashingBlock.size(), blockLongHash.get());
	  }
	  else if (block.majorVersion >= BLOCK_MAJOR_VERSION_4)
	  {
		auto rawHashingBlock = getParentBlockHashingBinaryArray(true);
		blockLongHash = Hash();
		Crypto.GlobalMembers.cn_lite_slow_hash_v1(rawHashingBlock.data(), rawHashingBlock.size(), blockLongHash.get());
	  }
	  else
	  {
		throw new System.Exception("Unknown block major version.");
	  }
	}

	return blockLongHash.get();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Crypto::Hash& getAuxiliaryBlockHeaderHash() const
  public Crypto.Hash getAuxiliaryBlockHeaderHash()
  {
	if (!auxiliaryBlockHeaderHash.is_initialized())
	{
	  auxiliaryBlockHeaderHash = CryptoNote.GlobalMembers.getObjectHash(getBlockHashingBinaryArray());
	}

	return auxiliaryBlockHeaderHash.get();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const BinaryArray& getBlockHashingBinaryArray() const
  public BinaryArray getBlockHashingBinaryArray()
  {
	if (!blockHashingBinaryArray.is_initialized())
	{
	  blockHashingBinaryArray = BinaryArray();
	  auto result = blockHashingBinaryArray.get();
	  if (!CryptoNote.GlobalMembers.toBinaryArray((BlockHeader)block, ref result))
	  {
		blockHashingBinaryArray.reset();
		throw new System.Exception("Can't serialize BlockHeader");
	  }

	  auto treeHash = getTransactionTreeHash();
	  result.insert(result.end(), treeHash.data, treeHash.data + 32);
	  var transactionCount = Common.asBinaryArray(Tools.get_varint_data(block.transactionHashes.size() + 1));
	  result.insert(result.end(), transactionCount.begin(), transactionCount.end());
	}

	return blockHashingBinaryArray.get();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const BinaryArray& getParentBlockBinaryArray(bool headerOnly) const
  public BinaryArray getParentBlockBinaryArray(bool headerOnly)
  {
	if (headerOnly)
	{
	  if (!parentBlockBinaryArrayHeaderOnly.is_initialized())
	  {
		var serializer = CryptoNote.GlobalMembers.makeParentBlockSerializer(block, false, true);
		parentBlockBinaryArrayHeaderOnly = BinaryArray();
		if (!CryptoNote.GlobalMembers.toBinaryArray(serializer, ref parentBlockBinaryArrayHeaderOnly.get()))
		{
		  parentBlockBinaryArrayHeaderOnly.reset();
		  throw new System.Exception("Can't serialize parent block header.");
		}
	  }

	  return parentBlockBinaryArrayHeaderOnly.get();
	}
	else
	{
	  if (!parentBlockBinaryArray.is_initialized())
	  {
		var serializer = CryptoNote.GlobalMembers.makeParentBlockSerializer(block, false, false);
		parentBlockBinaryArray = BinaryArray();
		if (!CryptoNote.GlobalMembers.toBinaryArray(serializer, ref parentBlockBinaryArray.get()))
		{
		  parentBlockBinaryArray.reset();
		  throw new System.Exception("Can't serialize parent block.");
		}
	  }

	  return parentBlockBinaryArray.get();
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const BinaryArray& getParentBlockHashingBinaryArray(bool headerOnly) const
  public BinaryArray getParentBlockHashingBinaryArray(bool headerOnly)
  {
	if (headerOnly)
	{
	  if (!parentBlockHashingBinaryArrayHeaderOnly.is_initialized())
	  {
		var serializer = CryptoNote.GlobalMembers.makeParentBlockSerializer(block, true, true);
		parentBlockHashingBinaryArrayHeaderOnly = BinaryArray();
		if (!CryptoNote.GlobalMembers.toBinaryArray(serializer, ref parentBlockHashingBinaryArrayHeaderOnly.get()))
		{
		  parentBlockHashingBinaryArrayHeaderOnly.reset();
		  throw new System.Exception("Can't serialize parent block header for hashing.");
		}
	  }

	  return parentBlockHashingBinaryArrayHeaderOnly.get();
	}
	else
	{
	  if (!parentBlockHashingBinaryArray.is_initialized())
	  {
		var serializer = CryptoNote.GlobalMembers.makeParentBlockSerializer(block, true, false);
		parentBlockHashingBinaryArray = BinaryArray();
		if (!CryptoNote.GlobalMembers.toBinaryArray(serializer, ref parentBlockHashingBinaryArray.get()))
		{
		  parentBlockHashingBinaryArray.reset();
		  throw new System.Exception("Can't serialize parent block for hashing.");
		}
	  }

	  return parentBlockHashingBinaryArray.get();
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint getBlockIndex() const
  public uint getBlockIndex()
  {
	if (!blockIndex.is_initialized())
	{
	  if (block.baseTransaction.inputs.size() != 1)
	  {
		blockIndex = 0;
	  }
	  else
	  {
		auto in = block.baseTransaction.inputs[0];
//C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
		if (in.type() != typeid(BaseInput))
		{
		  blockIndex = 0;
		}
		else
		{
		  blockIndex = boost::get<BaseInput>(in).blockIndex;
		}
	  }
	}

	return blockIndex.get();
  }

  private readonly BlockTemplate block;
  private boost.optional<BinaryArray> blockHashingBinaryArray;
  private boost.optional<BinaryArray> parentBlockBinaryArray;
  private boost.optional<BinaryArray> parentBlockHashingBinaryArray;
  private boost.optional<BinaryArray> parentBlockBinaryArrayHeaderOnly;
  private boost.optional<BinaryArray> parentBlockHashingBinaryArrayHeaderOnly;
  private boost.optional<uint> blockIndex;
  private boost.optional<Crypto.Hash> transactionTreeHash;
  private boost.optional<Crypto.Hash> blockHash;
  private boost.optional<Crypto.Hash> blockLongHash;
  private boost.optional<Crypto.Hash> auxiliaryBlockHeaderHash;
}

}
