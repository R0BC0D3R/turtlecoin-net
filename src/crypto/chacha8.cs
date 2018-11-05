/*
chacha-merged.c version 20080118
D. J. Bernstein
Public domain.
*/


// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
// 
// Please see the included LICENSE file for more information.#pragma once



#if __cplusplus

//C++ TO C# CONVERTER TODO TASK: #define macros defined in multiple preprocessor conditionals can only be replaced within the scope of the preprocessor conditional:
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CRYPTO_MAKE_COMPARABLE(type) namespace Crypto { inline bool operator==(const type &_v1, const type &_v2) { return std::memcmp(&_v1, &_v2, sizeof(type)) == 0; } inline bool operator!=(const type &_v1, const type &_v2) { return std::memcmp(&_v1, &_v2, sizeof(type)) != 0; } }
#define CRYPTO_MAKE_COMPARABLE
//C++ TO C# CONVERTER TODO TASK: #define macros defined in multiple preprocessor conditionals can only be replaced within the scope of the preprocessor conditional:
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CRYPTO_MAKE_HASHABLE(type) CRYPTO_MAKE_COMPARABLE(type) namespace Crypto { static_assert(sizeof(size_t) <= sizeof(type), "Size of " #type " must be at least that of size_t"); inline size_t hash_value(const type &_v) { return reinterpret_cast<const size_t &>(_v); } } namespace std { template<> struct hash<Crypto::type> { size_t operator()(const Crypto::type &_v) const { return reinterpret_cast<const size_t &>(_v); } }; }
#define CRYPTO_MAKE_HASHABLE
#define CN_PAGE_SIZE
#define CN_SCRATCHPAD
#define CN_ITERATIONS
#define CN_LITE_PAGE_SIZE
#define CN_LITE_SCRATCHPAD
#define CN_LITE_ITERATIONS
#define CN_SOFT_SHELL_MEMORY
#define CN_SOFT_SHELL_WINDOW
#define CN_SOFT_SHELL_MULTIPLIER
//C++ TO C# CONVERTER TODO TASK: #define macros defined in multiple preprocessor conditionals can only be replaced within the scope of the preprocessor conditional:
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_ITER (CN_SOFT_SHELL_MEMORY / 2)
#define CN_SOFT_SHELL_ITER
//C++ TO C# CONVERTER TODO TASK: #define macros defined in multiple preprocessor conditionals can only be replaced within the scope of the preprocessor conditional:
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_PAD_MULTIPLIER (CN_SOFT_SHELL_WINDOW / CN_SOFT_SHELL_MULTIPLIER)
#define CN_SOFT_SHELL_PAD_MULTIPLIER
//C++ TO C# CONVERTER TODO TASK: #define macros defined in multiple preprocessor conditionals can only be replaced within the scope of the preprocessor conditional:
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_ITER_MULTIPLIER (CN_SOFT_SHELL_PAD_MULTIPLIER / 2)
#define CN_SOFT_SHELL_ITER_MULTIPLIER

namespace Crypto
{
#endif
#if __cplusplus

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
//#pragma pack(push, 1)
  public class chacha8_key
  {
	public uint8_t[] data = Arrays.InitializeWithDefaultInstances<uint8_t>(DefineConstants.CHACHA8_KEY_SIZE);
  }

  // MS VC 2012 doesn't interpret `class chacha8_iv` as POD in spite of [9.0.10], so it is a struct
  public class chacha8_iv
  {
	public uint8_t[] data = Arrays.InitializeWithDefaultInstances<uint8_t>(DefineConstants.CHACHA8_IV_SIZE);
  }
}

#endif