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

namespace CryptoNote
{

public class SwappedBlockchainStorage : BlockchainStorage.IBlockchainStorageInternal, System.IDisposable
{
  public SwappedBlockchainStorage(string indexFileName, string dataFileName)
  {
	if (!blocks.open(dataFileName, indexFileName, 1024))
	{
	  throw new System.Exception("Can't open blockchain storage files.");
	}
  }
  public virtual void Dispose()
  {
	blocks.close();
  }

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public override void pushBlock(RawBlock && rawBlock)
  {
	blocks.push_back(rawBlock);
  }

  //Returns MemoryBlockchainStorage with elements from [splitIndex, blocks.size() - 1].
  //Original SwappedBlockchainStorage will contain elements from [0, splitIndex - 1].

  //Returns MemoryBlockchainStorage with elements from [splitIndex, blocks.size() - 1].
  //Original SwappedBlockchainStorage will contain elements from [0, splitIndex - 1].
  public override std::unique_ptr<BlockchainStorage.IBlockchainStorageInternal> splitStorage(uint32_t splitIndex)
  {
	Debug.Assert(splitIndex > 0);
	Debug.Assert(splitIndex < blocks.size());
	std::unique_ptr<MemoryBlockchainStorage> newStorage = new std::unique_ptr<MemoryBlockchainStorage>(new MemoryBlockchainStorage(new uint32_t(splitIndex)));

	uint64_t blocksCount = blocks.size();

	for (uint64_t i = splitIndex; i < blocksCount; ++i)
	{
	  newStorage.pushBlock(RawBlock(blocks[i]));
	}

	for (uint64_t i = 0; i < blocksCount - splitIndex; ++i)
	{
	  blocks.pop_back();
	}

	return std::move(newStorage);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual RawBlock getBlockByIndex(uint32_t index) const override
  public override RawBlock getBlockByIndex(uint32_t index)
  {
	Debug.Assert(index < getBlockCount());
	return blocks[index];
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint32_t getBlockCount() const override
  public override uint32_t getBlockCount()
  {
	return (uint32_t)blocks.size();
  }

  private SwappedVector<RawBlock> blocks = new SwappedVector<RawBlock>();
}

}



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

