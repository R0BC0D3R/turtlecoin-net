using System;
using System.Collections.Generic;
using System.Diagnostics;

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

namespace Messages
{
// immutable messages
public class NewBlock
{
  public uint32_t blockIndex = new uint32_t();
  public Crypto.Hash blockHash = new Crypto.Hash();
}

public class NewAlternativeBlock
{
  public uint32_t blockIndex = new uint32_t();
  public Crypto.Hash blockHash = new Crypto.Hash();
}

public class ChainSwitch
{
  public uint32_t commonRootIndex = new uint32_t();
  public List<Crypto.Hash> blocksFromCommonRoot = new List<Crypto.Hash>();
}

public class AddTransaction
{
  public List<Crypto.Hash> hashes = new List<Crypto.Hash>();
}

public class DeleteTransaction
{
  public List<Crypto.Hash> hashes = new List<Crypto.Hash>();
  public enum Reason
  {
	InBlock,
	Outdated,
	NotActual
  }
  public Reason reason;
}
}

public class BlockchainMessage : System.IDisposable
{
  public enum Type
  {
	NewBlock,
	NewAlternativeBlock,
	ChainSwitch,
	AddTransaction,
	DeleteTransaction
  }


  public BlockchainMessage(NewBlock message)
  {
	  this.type = new CryptoNote.BlockchainMessage.Type.NewBlock;
	  this.newBlock = std::move(message);
  }
  public BlockchainMessage(NewAlternativeBlock message)
  {
	  this.type = new CryptoNote.BlockchainMessage.Type.NewAlternativeBlock;
	  this.newAlternativeBlock = message;
  }
  public BlockchainMessage(ChainSwitch message)
  {
	  this.type = new CryptoNote.BlockchainMessage.Type.ChainSwitch;
	  this.chainSwitch = new ChainSwitch(message);
  }
  public BlockchainMessage(AddTransaction message)
  {
	  this.type = new CryptoNote.BlockchainMessage.Type.AddTransaction;
	  this.addTransaction = new AddTransaction(message);
  }
  public BlockchainMessage(DeleteTransaction message)
  {
	  this.type = new CryptoNote.BlockchainMessage.Type.DeleteTransaction;
	  this.deleteTransaction = new DeleteTransaction(message);
  }

  public BlockchainMessage(BlockchainMessage other)
  {
	  this.type = new CryptoNote.BlockchainMessage.Type(other.type);
	switch (type)
	{
	  case Type.NewBlock:
		new(newBlock) new NewBlock(other.newBlock);
		break;
	  case Type.NewAlternativeBlock:
		new(newAlternativeBlock) new NewAlternativeBlock(other.newAlternativeBlock);
		break;
	  case Type.ChainSwitch:
		chainSwitch = new ChainSwitch(*other.chainSwitch);
		break;
	  case Type.AddTransaction:
		addTransaction = new AddTransaction(*other.addTransaction);
		break;
	  case Type.DeleteTransaction:
		deleteTransaction = new DeleteTransaction(*other.deleteTransaction);
		break;
	}
  }

  public void Dispose()
  {
	switch (type)
	{
	  case Type.NewBlock:
		newBlock.~new NewBlock();
		break;
	  case Type.NewAlternativeBlock:
		newAlternativeBlock.~new NewAlternativeBlock();
		break;
	  case Type.ChainSwitch:
		if (chainSwitch != null)
		{
			chainSwitch.Dispose();
		}
		break;
	  case Type.AddTransaction:
		if (addTransaction != null)
		{
			addTransaction.Dispose();
		}
		break;
	  case Type.DeleteTransaction:
		if (deleteTransaction != null)
		{
			deleteTransaction.Dispose();
		}
		break;
	}
  }

  // pattern matchin API
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void match(System.Action<const NewBlock&> newBlockVisitor, System.Action<const NewAlternativeBlock&> newAlternativeBlockVisitor, System.Action<const ChainSwitch&> chainSwitchMessageVisitor, System.Action<const AddTransaction&> addTxVisitor, System.Action<const DeleteTransaction&> delTxVisitor) const
  public void match(Action<NewBlock > newBlockVisitor, Action<NewAlternativeBlock > newAlternativeBlockVisitor, Action<ChainSwitch > chainSwitchMessageVisitor, Action<AddTransaction > addTxVisitor, Action<DeleteTransaction > delTxVisitor)
  {
	switch (getType())
	{
	  case Type.NewBlock:
		newBlockVisitor(newBlock);
		break;
	  case Type.NewAlternativeBlock:
		newAlternativeBlockVisitor(newAlternativeBlock);
		break;
	  case Type.ChainSwitch:
		chainSwitchMessageVisitor(*chainSwitch);
		break;
	  case Type.AddTransaction:
		addTxVisitor(*addTransaction);
		break;
	  case Type.DeleteTransaction:
		delTxVisitor(*deleteTransaction);
		break;
	}
  }

  // API with explicit type handling
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: BlockchainMessage::Type getType() const
  public BlockchainMessage.Type getType()
  {
	return type;
  }
//C++ TO C# CONVERTER TODO TASK: The return type of the following function could not be determined:
  public auto getNewBlock() const.const NewBlock &
  {
	Debug.Assert(getType() == Type.NewBlock);
	return newBlock;
  }
//C++ TO C# CONVERTER TODO TASK: The return type of the following function could not be determined:
  public auto getNewAlternativeBlock() const.const NewAlternativeBlock &
  {
	Debug.Assert(getType() == Type.NewAlternativeBlock);
	return newAlternativeBlock;
  }
//C++ TO C# CONVERTER TODO TASK: The return type of the following function could not be determined:
  public auto getChainSwitch() const.const ChainSwitch &
  {
	Debug.Assert(getType() == Type.ChainSwitch);
	return *chainSwitch;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Messages::AddTransaction& getAddTransaction() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  Messages::AddTransaction getAddTransaction();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Messages::DeleteTransaction& getDeleteTransaction() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  Messages::DeleteTransaction getDeleteTransaction();

  private readonly Type type;
//C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#:
//  union
//  {
//	Messages::NewBlock newBlock;
//	Messages::NewAlternativeBlock newAlternativeBlock;
//	Messages::ChainSwitch* chainSwitch;
//	Messages::AddTransaction* addTransaction;
//	Messages::DeleteTransaction* deleteTransaction;
//  };
}
}


