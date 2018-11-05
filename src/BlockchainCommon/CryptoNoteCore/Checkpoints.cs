// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using Logging;
using System;
using System.Collections.Generic;

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
	public bool addCheckpoint(uint index, string hash_str)
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

		/* The block index (as a uint) */
		uint index = new uint();

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
			if (!addCheckpoint(new uint(index), hash))
			{
				return false;
			}
		}

		logger(INFO) << "Loaded " << points.Count << " checkpoints from " << filename;

		return true;
	}

	//---------------------------------------------------------------------------
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isInCheckpointZone(uint index) const
	public bool isInCheckpointZone(uint index)
	{
	  return points.Count > 0 && (index <= (--points.end()).first);
	}
	//---------------------------------------------------------------------------
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool checkBlock(uint index, const Crypto::Hash &h) const
	public bool checkBlock(uint index, Crypto.Hash h)
	{
	  bool ignored;
	  return checkBlock(new uint(index), h, ref ignored);
	}
	//---------------------------------------------------------------------------
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool checkBlock(uint index, const Crypto::Hash &h, bool& isCheckpoint) const
	public bool checkBlock(uint index, Crypto.Hash h, ref bool isCheckpoint)
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
//ORIGINAL LINE: bool isAlternativeBlockAllowed(uint blockchainSize, uint blockIndex) const
	public bool isAlternativeBlockAllowed(uint blockchainSize, uint blockIndex)
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
	  uint checkpointIndex = it.first;
	  return checkpointIndex < blockIndex;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<uint> getCheckpointHeights() const
	public List<uint> getCheckpointHeights()
	{
	  List<uint> checkpointHeights = new List<uint>();
	  checkpointHeights.Capacity = points.Count;
	  foreach (var it in points)
	  {
		checkpointHeights.Add(it.first);
	  }

	  return checkpointHeights;
	}
	private SortedDictionary<uint, Crypto.Hash> points = new SortedDictionary<uint, Crypto.Hash>();
	private Logging.LoggerRef logger = new Logging.LoggerRef();
  }
}

