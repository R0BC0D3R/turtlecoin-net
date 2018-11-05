using Logging;
using System;
using System.Collections.Generic;

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

namespace CryptoNote
{
  public class Checkpoints
  {
	//---------------------------------------------------------------------------
	public Checkpoints(Logging.ILogger log)
	{
		this.logger = new Logging.LoggerRef(log, "checkpoints");
	}

	//---------------------------------------------------------------------------
	public bool addCheckpoint(uint32_t index, string hash_str)
	{
	  Crypto.Hash h = GlobalMembers.NULL_HASH;

	  if (!Common.GlobalMembers.podFromHex(hash_str, h))
	  {
		logger(ERROR, BRIGHT_RED) << "INVALID HASH IN CHECKPOINTS!";
		return false;
	  }

	  /* The return value lets us check if it was inserted or not. If it wasn't,
	     there is already a key (i.e., a height value) existing */
	  if (!points.insert({index, h}).second)
	  {
		logger(ERROR, BRIGHT_RED) << "CHECKPOINT ALREADY EXISTS!";
		return false;
	  }

	  return true;
	}
	public bool loadCheckpointsFromFile(string filename)
	{
		std::ifstream file = new std::ifstream(filename);

		if (file == null)
		{
			logger(ERROR, BRIGHT_RED) << "Could not load checkpoints file: " << filename;

			return false;
		}

		/* The block this checkpoint is for (as a string) */
		string indexString;

		/* The hash this block has (as a string) */
		string hash;

		/* The block index (as a uint32_t) */
		uint32_t index = new uint32_t();

		/* Checkpoints file has this format:
	
		   index,hash
		   index2,hash2
	
		   So, we do std::getline() on the file with the delimiter as ',' to take
		   the index, then we do std::getline() on the file again with the standard
		   delimiter of '\n', to get the hash. */
		while (getline(file, indexString, ','), getline(file, hash))
		{
			/* Try and parse the indexString as an int */
			try
			{
				index = Convert.ToInt32(indexString);
			}
			catch (System.ArgumentException)
			{
				logger(ERROR, BRIGHT_RED) << "Invalid checkpoint file format - " << "could not parse height as a number";

				return false;
			}

			/* Failed to parse hash, or checkpoint already exists */
			if (!addCheckpoint(new uint32_t(index), hash))
			{
				return false;
			}
		}

		logger(INFO) << "Loaded " << points.Count << " checkpoints from " << filename;

		return true;
	}

	//---------------------------------------------------------------------------
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isInCheckpointZone(uint32_t index) const
	public bool isInCheckpointZone(uint32_t index)
	{
	  return points.Count > 0 && (index <= (--points.end()).first);
	}
	//---------------------------------------------------------------------------
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool checkBlock(uint32_t index, const Crypto::Hash &h) const
	public bool checkBlock(uint32_t index, Crypto.Hash h)
	{
	  bool ignored;
	  return checkBlock(new uint32_t(index), h, ref ignored);
	}
	//---------------------------------------------------------------------------
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool checkBlock(uint32_t index, const Crypto::Hash &h, bool& isCheckpoint) const
	public bool checkBlock(uint32_t index, Crypto.Hash h, ref bool isCheckpoint)
	{
	  var it = points.find(index);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  isCheckpoint = it != points.end();
	  if (!isCheckpoint)
	  {
		return true;
	  }

//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  if (it.second == h)
	  {
		if (index % 100 == 0 != null)
		{
		  logger(Logging.INFO, BRIGHT_GREEN) << "CHECKPOINT PASSED FOR INDEX " << index << " " << h;
		}
		return true;
	  }
	  else
	  {
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
		logger(Logging.WARNING, BRIGHT_YELLOW) << "CHECKPOINT FAILED FOR HEIGHT " << index << ". EXPECTED HASH: " << it.second << ", FETCHED HASH: " << h;
		return false;
	  }
	}
	//---------------------------------------------------------------------------
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isAlternativeBlockAllowed(uint32_t blockchainSize, uint32_t blockIndex) const
	public bool isAlternativeBlockAllowed(uint32_t blockchainSize, uint32_t blockIndex)
	{
	  if (blockchainSize == 0)
	  {
		return false;
	  }

	  var it = points.upper_bound(blockchainSize);
	  // Is blockchainSize before the first checkpoint?
	  if (it == points.GetEnumerator())
	  {
		return true;
	  }

	  --it;
	  uint32_t checkpointIndex = it.first;
	  return checkpointIndex < blockIndex;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<uint32_t> getCheckpointHeights() const
	public List<uint32_t> getCheckpointHeights()
	{
	  List<uint32_t> checkpointHeights = new List<uint32_t>();
	  checkpointHeights.Capacity = points.Count;
	  foreach (var it in points)
	  {
		checkpointHeights.Add(it.first);
	  }

	  return checkpointHeights;
	}
	private SortedDictionary<uint32_t, Crypto.Hash> points = new SortedDictionary<uint32_t, Crypto.Hash>();
	private Logging.LoggerRef logger = new Logging.LoggerRef();
  }
}

