﻿// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;
using System.Diagnostics;

namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class P2pContext;

public class P2pContextOwner : System.IDisposable
{


//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  P2pContextOwner(P2pContext ctx, ClassicLinkedList<std::unique_ptr<P2pContext>> contextList);
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public P2pContextOwner(P2pContextOwner && other)
  {
	  this.contextList = other.contextList;
	  this.contextIterator = other.contextIterator;
	other.contextIterator = contextList.end();
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  P2pContextOwner(const P2pContextOwner& other) = delete;
  public void Dispose()
  {
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	if (contextIterator != contextList.end())
	{
//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL list 'erase' method in C#:
	  contextList.erase(contextIterator);
	}
  }

//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
  P2pContext & get();
  public P2pContext Dereference()
  {
	return get();
  }


  private LinkedList<std::unique_ptr<P2pContext>> contextList;
  private LinkedList<std::unique_ptr<P2pContext>>.Enumerator contextIterator;
}

}

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CRYPTO_MAKE_COMPARABLE(type) namespace Crypto { inline bool operator==(const type &_v1, const type &_v2) { return std::memcmp(&_v1, &_v2, sizeof(type)) == 0; } inline bool operator!=(const type &_v1, const type &_v2) { return std::memcmp(&_v1, &_v2, sizeof(type)) != 0; } }
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CRYPTO_MAKE_HASHABLE(type) CRYPTO_MAKE_COMPARABLE(type) namespace Crypto { static_assert(sizeof(uint) <= sizeof(type), "Size of " #type " must be at least that of uint"); inline uint hash_value(const type &_v) { return reinterpret_cast<const uint &>(_v); } } namespace std { template<> struct hash<Crypto::type> { uint operator()(const Crypto::type &_v) const { return reinterpret_cast<const uint &>(_v); } }; }
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_ITER (CN_SOFT_SHELL_MEMORY / 2)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_PAD_MULTIPLIER (CN_SOFT_SHELL_WINDOW / CN_SOFT_SHELL_MULTIPLIER)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_ITER_MULTIPLIER (CN_SOFT_SHELL_PAD_MULTIPLIER / 2)

