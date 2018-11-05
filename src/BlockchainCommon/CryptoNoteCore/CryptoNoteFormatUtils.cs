﻿// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using Logging;
using Crypto;
using Common;
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
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);


namespace Logging
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class ILogger;
}

namespace CryptoNote
{

public class TransactionSourceEntry
{

  public List<Tuple<uint32_t, Crypto.PublicKey>> outputs = new List<Tuple<uint32_t, Crypto.PublicKey>>(); //index + key
  public size_t realOutput = new size_t(); //index in outputs vector of real output_entry
  public Crypto.PublicKey realTransactionPublicKey = new Crypto.PublicKey(); //incoming real tx public key
  public size_t realOutputIndexInTransaction = new size_t(); //index in transaction outputs vector
  public uint64_t amount = new uint64_t(); //money
}

public class TransactionDestinationEntry
{
  public uint64_t amount = new uint64_t(); //money
  public AccountPublicAddress addr = new AccountPublicAddress(); //destination address

  public TransactionDestinationEntry()
  {
	  this.amount = 0;
	  this.addr = boost::value_initialized<AccountPublicAddress>();
  }
  public TransactionDestinationEntry(uint64_t amount, AccountPublicAddress addr)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.amount = amount;
	  this.amount.CopyFrom(amount);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.addr = addr;
	  this.addr.CopyFrom(addr);
  }
}

}

