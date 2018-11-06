// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


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



using System;

namespace CryptoNote
{

public class WalletRecord
{
  public Crypto.PublicKey spendPublicKey = new Crypto.PublicKey();
  public Crypto.SecretKey spendSecretKey = new Crypto.SecretKey();
  public CryptoNote.ITransfersContainer container = null;
  public ulong pendingBalance = 0;
  public ulong actualBalance = 0;
  public DateTime creationTimestamp = new DateTime();
}

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
//#pragma pack(push, 1)
public class EncryptedWalletRecord
{
  public Crypto.chacha8_iv iv = new Crypto.chacha8_iv();
  // Secret key, public key and creation timestamp
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
  byte data[sizeof(Crypto.PublicKey) + sizeof(Crypto.SecretKey) + sizeof(ulong)];
}
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
//#pragma pack(pop)

public class RandomAccessIndex
{
}
public class KeysIndex
{
}
public class TransfersContainerIndex
{
}

public class WalletIndex
{
}
public class TransactionOutputIndex
{
}
public class BlockHeightIndex
{
}

public class TransactionHashIndex
{
}
public class TransactionIndex
{
}
public class BlockHashIndex
{
}

public class UnlockTransactionJob
{
  public uint blockHeight;
  public CryptoNote.ITransfersContainer container;
  public Crypto.Hash transactionHash = new Crypto.Hash();
}




}
