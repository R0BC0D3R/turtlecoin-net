// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//struct TransactionValidatorState;

public interface ITransactionPool
{
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  bool pushTransaction(CachedTransaction && tx, TransactionValidatorState && transactionState);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual const CachedTransaction& getTransaction(const Crypto::Hash& hash) const = 0;
  CachedTransaction getTransaction(Crypto.Hash hash);
  bool removeTransaction(Crypto.Hash hash);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getTransactionCount() const = 0;
  uint getTransactionCount();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getTransactionHashes() const = 0;
  List<Crypto.Hash> getTransactionHashes();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool checkIfTransactionPresent(const Crypto::Hash& hash) const = 0;
  bool checkIfTransactionPresent(Crypto.Hash hash);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual const TransactionValidatorState& getPoolTransactionValidationState() const = 0;
  TransactionValidatorState getPoolTransactionValidationState();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<CachedTransaction> getPoolTransactions() const = 0;
  List<CachedTransaction> getPoolTransactions();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getTransactionReceiveTime(const Crypto::Hash& hash) const = 0;
  ulong getTransactionReceiveTime(Crypto.Hash hash);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getTransactionHashesByPaymentId(const Crypto::Hash& paymentId) const = 0;
  List<Crypto.Hash> getTransactionHashesByPaymentId(Crypto.Hash paymentId);
}

}
