// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


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

using Crypto;
using CryptoNote;
using System.Diagnostics;

namespace CryptoNote
{

public class CachedTransaction
{
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public CachedTransaction(Transaction && transaction)
  {
	  this.transaction = std::move(transaction);
  }
  public CachedTransaction(Transaction transaction)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.transaction = transaction;
	  this.transaction.CopyFrom(transaction);
  }
  public CachedTransaction(BinaryArray transactionBinaryArray)
  {
	  this.transactionBinaryArray = transactionBinaryArray;
	if (!CryptoNote.GlobalMembers.fromBinaryArray<Transaction>(ref transaction, this.transactionBinaryArray.get()))
	{
	  throw new System.Exception("CachedTransaction::CachedTransaction(BinaryArray&&), deserealization error.");
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Transaction& getTransaction() const
  public Transaction getTransaction()
  {
	return transaction;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Crypto::Hash& getTransactionHash() const
  public Crypto.Hash getTransactionHash()
  {
	if (!transactionHash.is_initialized())
	{
	  transactionHash = getBinaryArrayHash(getTransactionBinaryArray());
	}

	return transactionHash.get();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Crypto::Hash& getTransactionPrefixHash() const
  public Crypto.Hash getTransactionPrefixHash()
  {
	if (!transactionPrefixHash.is_initialized())
	{
	  transactionPrefixHash = CryptoNote.GlobalMembers.getObjectHash((TransactionPrefix)transaction);
	}

	return transactionPrefixHash.get();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const BinaryArray& getTransactionBinaryArray() const
  public BinaryArray getTransactionBinaryArray()
  {
	if (!transactionBinaryArray.is_initialized())
	{
	  transactionBinaryArray = CryptoNote.GlobalMembers.toBinaryArray(transaction);
	}

	return transactionBinaryArray.get();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong getTransactionFee() const
  public ulong getTransactionFee()
  {
	if (!transactionFee.is_initialized())
	{
	  ulong summaryInputAmount = 0;
	  ulong summaryOutputAmount = 0;
	  foreach (var @out in transaction.outputs)
	  {
		summaryOutputAmount += @out.amount;
	  }

	  foreach (var in in transaction.inputs)
	  {
//C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
		if (in.type() == typeid(KeyInput))
		{
		  summaryInputAmount += boost::get<KeyInput>(in).amount;
		}
//C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
		else if (in.type() == typeid(BaseInput))
		{
		  return 0;
		}
		else
		{
		  Debug.Assert(false && "Unknown out type");
		}
	  }

	  transactionFee = summaryInputAmount - summaryOutputAmount;
	}

	return transactionFee.get();
  }

  private Transaction transaction = new Transaction();
  private boost.optional<BinaryArray> transactionBinaryArray;
  private boost.optional<Crypto.Hash> transactionHash;
  private boost.optional<Crypto.Hash> transactionPrefixHash;
  private boost.optional<ulong> transactionFee;
}

}
