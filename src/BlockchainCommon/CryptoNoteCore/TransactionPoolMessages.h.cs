// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{

public enum TransactionMessageType
{
  AddTransactionType,
  DeleteTransactionType
}

// immutable messages
public class AddTransaction
{
  public Crypto.Hash hash = new Crypto.Hash();
}

public class DeleteTransaction
{
  public Crypto.Hash hash = new Crypto.Hash();
}

public class TransactionPoolMessage
{
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  TransactionPoolMessage(AddTransaction at);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  TransactionPoolMessage(DeleteTransaction at);

  // pattern matchin API
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void match(System.Action<AddTransaction&>&&, System.Action<DeleteTransaction&>&&);

  // API with explicit type handling
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: TransactionMessageType getType() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  TransactionMessageType getType();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: AddTransaction getAddTransaction() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  AddTransaction getAddTransaction();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: DeleteTransaction getDeleteTransaction() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  DeleteTransaction getDeleteTransaction();

  private readonly TransactionMessageType type;
//C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#:
//  union
//  {
//	const AddTransaction addTransaction;
//	const DeleteTransaction deleteTransaction;
//  };
}
}
