//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

using CryptoNote;
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

public class MemoryBlockchainStorage : BlockchainStorage.IBlockchainStorageInternal, System.IDisposable
{
  public MemoryBlockchainStorage(uint32_t reserveSize)
  {
	blocks.Capacity = reserveSize;
  }
  public virtual void Dispose()
  {
  }

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public override void pushBlock(RawBlock && rawBlock)
  {
	blocks.Add(rawBlock);
  }

  //Returns MemoryBlockchainStorage with elements from [splitIndex, blocks.size() - 1].
  //Original MemoryBlockchainStorage will contain elements from [0, splitIndex - 1].

  //Returns MemoryBlockchainStorage with elements from [splitIndex, blocks.size() - 1].
  //Original MemoryBlockchainStorage will contain elements from [0, splitIndex - 1].
  public override std::unique_ptr<BlockchainStorage.IBlockchainStorageInternal> splitStorage(uint32_t splitIndex)
  {
	Debug.Assert(splitIndex > 0);
	Debug.Assert(splitIndex < blocks.Count);
	std::unique_ptr<MemoryBlockchainStorage> newStorage = new std::unique_ptr<MemoryBlockchainStorage>(new MemoryBlockchainStorage(new uint32_t(splitIndex)));
	std::move(blocks.GetEnumerator() + splitIndex, blocks.end(), std::back_inserter(newStorage.blocks));
	blocks.Resize(splitIndex);
	blocks.shrink_to_fit();
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
	return (uint32_t)blocks.Count;
  }

  private List<RawBlock> blocks = new List<RawBlock>();
}

}
