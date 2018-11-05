using Common;
using System;
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

namespace CryptoNote
{

public class SynchronizationState : IStreamSerializable
{

  public class CheckResult
  {
	public bool detachRequired;
	public uint detachHeight;
	public bool hasNewBlocks;
	public uint newBlockHeight;
  }


  public SynchronizationState(Crypto.Hash genesisBlockHash)
  {
	m_blockchain.Add(genesisBlockHash);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<Crypto::Hash> getShortHistory(uint localHeight) const
  public List<Crypto.Hash> getShortHistory(uint localHeight)
  {
	ShortHistory history = new ShortHistory();
	uint i = 0;
	uint current_multiplier = 1;
	uint sz = Math.Min((uint)m_blockchain.Count, localHeight + 1);

	if (sz == 0)
	{
	  return history;
	}

	uint current_back_offset = 1;
	bool genesis_included = false;

	while (current_back_offset < sz)
	{
	  history.push_back(m_blockchain[sz - current_back_offset]);
	  if (sz - current_back_offset == 0)
	  {
		genesis_included = true;
	  }
	  if (i < 10)
	  {
		++current_back_offset;
	  }
	  else
	  {
		current_back_offset += current_multiplier *= 2;
	  }
	  ++i;
	}

	if (!genesis_included)
	{
	  history.push_back(m_blockchain[0]);
	}

	return history;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: SynchronizationState::CheckResult checkInterval(const BlockchainInterval& interval) const
  public SynchronizationState.CheckResult checkInterval(BlockchainInterval interval)
  {
	Debug.Assert(interval.startHeight <= m_blockchain.Count);

	CheckResult result = new CheckResult(false, 0, false, 0);

	uint intervalEnd = interval.startHeight + (uint)interval.blocks.Count;
	uint iterationEnd = Math.Min((uint)m_blockchain.Count, intervalEnd);

	for (uint i = interval.startHeight; i < iterationEnd; ++i)
	{
	  if (m_blockchain[i] != interval.blocks[i - interval.startHeight])
	  {
		result.detachRequired = true;
		result.detachHeight = i;
		break;
	  }
	}

	if (result.detachRequired)
	{
	  result.hasNewBlocks = true;
	  result.newBlockHeight = result.detachHeight;
	  return result;
	}

	if (intervalEnd > m_blockchain.Count)
	{
	  result.hasNewBlocks = true;
	  result.newBlockHeight = (uint)m_blockchain.Count;
	}

	return result;
  }

  public void detach(uint height)
  {
	Debug.Assert(height < m_blockchain.Count);
	m_blockchain.Resize(height);
  }
  public void addBlocks(Crypto.Hash blockHashes, uint height, uint count)
  {
	Debug.Assert(blockHashes);
	var size = m_blockchain.Count;
	if (size)
	{
	}
	// Dummy fix for simplewallet or walletd when sync
	if (height == 0)
	{
	  height = 1;
	}
	Debug.Assert(size == height);
//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'insert' method in C#:
	m_blockchain.insert(m_blockchain.end(), blockHashes, blockHashes + count);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint getHeight() const
  public uint getHeight()
  {
	return (uint)m_blockchain.Count;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const ClassicVector<Crypto::Hash>& getKnownBlockHashes() const
  public List<Crypto.Hash> getKnownBlockHashes()
  {
	return m_blockchain;
  }

  // IStreamSerializable
  public void save(std::ostream os)
  {
	StdOutputStream stream = new StdOutputStream(os);
	CryptoNote.BinaryOutputStreamSerializer s = new CryptoNote.BinaryOutputStreamSerializer(stream);
	CryptoNote.GlobalMembers.serialize(s.functorMethod, "state");
  }
  public void load(std::istream in)
  {
	StdInputStream stream = new StdInputStream(in);
	CryptoNote.BinaryInputStreamSerializer s = new CryptoNote.BinaryInputStreamSerializer(stream);
	CryptoNote.GlobalMembers.serialize(s.functorMethod, "state");
  }

  // serialization
  public CryptoNote.ISerializer serialize(CryptoNote.ISerializer s, string name)
  {
	s.beginObject(name);
	s.functorMethod(m_blockchain, "blockchain");
	s.endObject();
	return s.functorMethod;
  }


  private List<Crypto.Hash> m_blockchain = new List<Crypto.Hash>();
}

}

