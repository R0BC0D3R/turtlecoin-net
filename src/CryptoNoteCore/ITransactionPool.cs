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
