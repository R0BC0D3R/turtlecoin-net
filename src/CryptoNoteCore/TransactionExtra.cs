﻿//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
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
using Common;
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
  public List<uint8_t> nonce = new List<uint8_t>();
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

public class ExtraSerializerVisitor : boost::static_visitor<bool>
{
  public List<uint8_t> extra;

  public ExtraSerializerVisitor(List<uint8_t> tx_extra)
  {
	  this.extra = tx_extra;
  }

  public static bool functorMethod(TransactionExtraPadding t)
  {
	if (t.size > DefineConstants.TX_EXTRA_PADDING_MAX_COUNT)
	{
	  return false;
	}
//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'insert' method in C#:
	extra.insert(extra.end(), t.size, 0);
	return true;
  }

  public static bool functorMethod(TransactionExtraPublicKey t)
  {
	return CryptoNote.GlobalMembers.addTransactionPublicKeyToExtra(extra, t.publicKey);
  }

  public static bool functorMethod(TransactionExtraNonce t)
  {
	return CryptoNote.GlobalMembers.addExtraNonceToTransactionExtra(extra, t.nonce);
  }

  public static bool functorMethod(TransactionExtraMergeMiningTag t)
  {
	return CryptoNote.GlobalMembers.appendMergeMiningTagToExtra(extra, t);
  }
}


}
