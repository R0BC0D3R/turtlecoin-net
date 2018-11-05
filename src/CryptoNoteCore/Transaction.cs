using Crypto;

using CryptoNote;

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



namespace Crypto
{

public class Hash
{
  public uint8_t[] data = Arrays.InitializeWithDefaultInstances<uint8_t>(32);
}

public class PublicKey
{
  public uint8_t[] data = Arrays.InitializeWithDefaultInstances<uint8_t>(32);
}

public class SecretKey
{
  public uint8_t[] data = Arrays.InitializeWithDefaultInstances<uint8_t>(32);
}

public class KeyDerivation
{
  public uint8_t[] data = Arrays.InitializeWithDefaultInstances<uint8_t>(32);
}

public class KeyImage
{
  public uint8_t[] data = Arrays.InitializeWithDefaultInstances<uint8_t>(32);
}

public class Signature
{
  public uint8_t[] data = Arrays.InitializeWithDefaultInstances<uint8_t>(64);
}

}


namespace CryptoNote
{

public class BaseInput
{
  public uint32_t blockIndex = new uint32_t();
}

public class KeyInput
{
  public uint64_t amount = new uint64_t();
  public List<uint32_t> outputIndexes = new List<uint32_t>();
  public Crypto.KeyImage keyImage = new Crypto.KeyImage();
}

public class KeyOutput
{
  public Crypto.PublicKey key = new Crypto.PublicKey();
}



public class TransactionOutput
{
  public uint64_t amount = new uint64_t();
  public TransactionOutputTarget target = new TransactionOutputTarget();
}

public class TransactionPrefix
{
  public uint8_t version = new uint8_t();
  public uint64_t unlockTime = new uint64_t();
  public List<TransactionInput> inputs = new List<TransactionInput>();
  public List<TransactionOutput> outputs = new List<TransactionOutput>();
  public List<uint8_t> extra = new List<uint8_t>();
}

public class Transaction : TransactionPrefix
{
  public List<List<Crypto.Signature>> signatures = new List<List<Crypto.Signature>>();
}

public class BaseTransaction : TransactionPrefix
{
}

public class ParentBlock
{
  public uint8_t majorVersion = new uint8_t();
  public uint8_t minorVersion = new uint8_t();
  public Crypto.Hash previousBlockHash = new Crypto.Hash();
  public uint16_t transactionCount = new uint16_t();
  public List<Crypto.Hash> baseTransactionBranch = new List<Crypto.Hash>();
  public BaseTransaction baseTransaction = new BaseTransaction();
  public List<Crypto.Hash> blockchainBranch = new List<Crypto.Hash>();
}

public class BlockHeader
{
  public uint8_t majorVersion = new uint8_t();
  public uint8_t minorVersion = new uint8_t();
  public uint32_t nonce = new uint32_t();
  public uint64_t timestamp = new uint64_t();
  public Crypto.Hash previousBlockHash = new Crypto.Hash();
}

public class BlockTemplate : BlockHeader
{
  public ParentBlock parentBlock = new ParentBlock();
  public Transaction baseTransaction = new Transaction();
  public List<Crypto.Hash> transactionHashes = new List<Crypto.Hash>();
}

public class AccountPublicAddress
{
  public Crypto.PublicKey spendPublicKey = new Crypto.PublicKey();
  public Crypto.PublicKey viewPublicKey = new Crypto.PublicKey();
}

public class AccountKeys
{
  public AccountPublicAddress address = new AccountPublicAddress();
  public Crypto.SecretKey spendSecretKey = new Crypto.SecretKey();
  public Crypto.SecretKey viewSecretKey = new Crypto.SecretKey();
}

public class KeyPair
{
  public Crypto.PublicKey publicKey = new Crypto.PublicKey();
  public Crypto.SecretKey secretKey = new Crypto.SecretKey();
}


public class RawBlock
{
  public BinaryArray block = new BinaryArray(); //BlockTemplate
  public List<BinaryArray> transactions = new List<BinaryArray>();
}

}


namespace CryptoNote
{

namespace TransactionTypes
{

  public enum InputType : uint8_t
  {
	  Invalid,
	  Key,
	  Generating
  }
  public enum OutputType : uint8_t
  {
	  Invalid,
	  Key
  }

  public class GlobalOutput
  {
	public Crypto.PublicKey targetKey = new Crypto.PublicKey();
	public uint32_t outputIndex = new uint32_t();
  }


  public class OutputKeyInfo
  {
	public Crypto.PublicKey transactionPublicKey = new Crypto.PublicKey();
	public size_t transactionIndex = new size_t();
	public size_t outputInTransaction = new size_t();
  }

  public class InputKeyInfo
  {
	public uint64_t amount = new uint64_t();
	public GlobalOutputsContainer outputs = new GlobalOutputsContainer();
	public OutputKeyInfo realOutput = new OutputKeyInfo();
  }
}

//
// ITransactionReader
// 
public abstract class ITransactionReader : System.IDisposable
{
  public virtual void Dispose()
  {
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual Crypto::Hash getTransactionHash() const = 0;
  public abstract Crypto.Hash getTransactionHash();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual Crypto::Hash getTransactionPrefixHash() const = 0;
  public abstract Crypto.Hash getTransactionPrefixHash();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual Crypto::PublicKey getTransactionPublicKey() const = 0;
  public abstract Crypto.PublicKey getTransactionPublicKey();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getTransactionSecretKey(Crypto::SecretKey& key) const = 0;
  public abstract bool getTransactionSecretKey(Crypto.SecretKey key);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint64_t getUnlockTime() const = 0;
  public abstract uint64_t getUnlockTime();

  // extra
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getPaymentId(Crypto::Hash& paymentId) const = 0;
  public abstract bool getPaymentId(Crypto.Hash paymentId);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getExtraNonce(BinaryArray& nonce) const = 0;
  public abstract bool getExtraNonce(BinaryArray nonce);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual BinaryArray getExtra() const = 0;
  public abstract BinaryArray getExtra();

  // inputs
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual size_t getInputCount() const = 0;
  public abstract size_t getInputCount();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint64_t getInputTotalAmount() const = 0;
  public abstract uint64_t getInputTotalAmount();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual TransactionTypes::InputType getInputType(size_t index) const = 0;
  public abstract TransactionTypes.InputType getInputType(size_t index);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual void getInput(size_t index, KeyInput& input) const = 0;
  public abstract void getInput(size_t index, KeyInput input);

  // outputs
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual size_t getOutputCount() const = 0;
  public abstract size_t getOutputCount();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint64_t getOutputTotalAmount() const = 0;
  public abstract uint64_t getOutputTotalAmount();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual TransactionTypes::OutputType getOutputType(size_t index) const = 0;
  public abstract TransactionTypes.OutputType getOutputType(size_t index);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual void getOutput(size_t index, KeyOutput& output, uint64_t& amount) const = 0;
  public abstract void getOutput(size_t index, KeyOutput output, uint64_t amount);

  // signatures
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual size_t getRequiredSignaturesCount(size_t inputIndex) const = 0;
  public abstract size_t getRequiredSignaturesCount(size_t inputIndex);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool findOutputsToAccount(const AccountPublicAddress& addr, const Crypto::SecretKey& viewSecretKey, ClassicVector<uint32_t>& outs, uint64_t& outputAmount) const = 0;
  public abstract bool findOutputsToAccount(AccountPublicAddress addr, Crypto.SecretKey viewSecretKey, List<uint32_t> outs, uint64_t outputAmount);

  // various checks
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool validateInputs() const = 0;
  public abstract bool validateInputs();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool validateOutputs() const = 0;
  public abstract bool validateOutputs();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool validateSignatures() const = 0;
  public abstract bool validateSignatures();

  // serialized transaction
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual BinaryArray getTransactionData() const = 0;
  public abstract BinaryArray getTransactionData();
}

//
// ITransactionWriter
// 
public abstract class ITransactionWriter : System.IDisposable
{

  public virtual void Dispose()
  {
  }

  // transaction parameters
  public abstract void setUnlockTime(uint64_t unlockTime);

  // extra
  public abstract void setPaymentId(Crypto.Hash paymentId);
  public abstract void setExtraNonce(BinaryArray nonce);
  public abstract void appendExtra(BinaryArray extraData);

  // Inputs/Outputs 
  public abstract size_t addInput(KeyInput input);
  public abstract size_t addInput(AccountKeys senderKeys, TransactionTypes.InputKeyInfo info, KeyPair ephKeys);

  public abstract size_t addOutput(uint64_t amount, AccountPublicAddress to);
  public abstract size_t addOutput(uint64_t amount, KeyOutput @out);

  // transaction info
  public abstract void setTransactionSecretKey(Crypto.SecretKey key);

  // signing
  public abstract void signInputKey(size_t input, TransactionTypes.InputKeyInfo info, KeyPair ephKeys);
}

//C++ TO C# CONVERTER TODO TASK: Multiple inheritance is not available in C#:
public abstract class ITransaction : ITransactionReader, ITransactionWriter
{
  public override void Dispose()
  {
	  base.Dispose();
  }

}

}

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

namespace CryptoNote
{

  public class TransactionExtra
  {
	public TransactionExtra()
	{
	}
	public TransactionExtra(List<uint8_t> extra)
	{
	  parse(extra);
	}

	public bool parse(List<uint8_t> extra)
	{
	  fields.Clear();
	  return CryptoNote.parseTransactionExtra(extra, fields);
	}

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool get(T& value) const
	public bool get<T>(ref T value)
	{
//C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
	  var it = find(typeid(T));
	  if (it == fields.end())
	  {
		return false;
	  }
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  value = boost::get<T>(it);
	  return true;
	}

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>
	public void set<T>(T value)
	{
//C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
	  var it = find(typeid(T));
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  if (it != fields.end())
	  {
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
		it = value;
	  }
	  else
	  {
		fields.Add(value);
	  }
	}

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>
	public void append<T>(T value)
	{
	  fields.Add(value);
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool getPublicKey(Crypto::PublicKey& pk) const
	public bool getPublicKey(ref Crypto.PublicKey pk)
	{
	  CryptoNote.TransactionExtraPublicKey extraPk = new CryptoNote.TransactionExtraPublicKey();
	  if (!get(ref extraPk))
	  {
		return false;
	  }
	  pk = extraPk.publicKey;
	  return true;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<uint8_t> serialize() const
	public List<uint8_t> serialize()
	{
	  List<uint8_t> extra = new List<uint8_t>();
	  writeTransactionExtra(extra, fields);
	  return extra;
	}


//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVectorIterator<boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag>> find(const System.Type& t) const
	private List<boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag>>.Enumerator find(System.Type t)
	{
	  return std::find_if(fields.GetEnumerator(), fields.end(), (boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag> f) =>
	  {
		  return t == f.type();
	  });
	}

	private List<boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag>>.Enumerator find(System.Type t)
	{
	  return std::find_if(fields.GetEnumerator(), fields.end(), (boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag> f) =>
	  {
		  return t == f.type();
	  });
	}

	private List<boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag>> fields = new List<boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag>>();
  }

}

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



//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace


namespace CryptoNote
{

  ////////////////////////////////////////////////////////////////////////
  // class Transaction declaration
  ////////////////////////////////////////////////////////////////////////

  public class TransactionImpl : ITransaction
  {
	public TransactionImpl()
	{
	  CryptoNote.KeyPair txKeys = new CryptoNote.KeyPair(CryptoNote.generateKeyPair());

	  TransactionExtraPublicKey pk = new TransactionExtraPublicKey(txKeys.publicKey);
	  extra.set(pk);

	  transaction.version = CURRENT_TRANSACTION_VERSION;
	  transaction.unlockTime = 0;
	  transaction.extra = new List<uint8_t>(extra.serialize());

	  secretKey = txKeys.secretKey;
	}
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	TransactionImpl(BinaryArray txblob);
	public TransactionImpl(CryptoNote.Transaction tx)
	{
		this.transaction = new CryptoNote.Transaction(tx);
	  extra.parse(transaction.extra);
	}

	// ITransactionReader
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual Hash getTransactionHash() const override
	public override Hash getTransactionHash()
	{
	  if (!transactionHash.is_initialized())
	  {
		transactionHash = CryptoNote.GlobalMembers.getObjectHash(transaction);
	  }

	  return transactionHash.get();
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual Hash getTransactionPrefixHash() const override
	public override Hash getTransactionPrefixHash()
	{
	  return CryptoNote.GlobalMembers.getObjectHash((TransactionPrefix)transaction);
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual PublicKey getTransactionPublicKey() const override
	public override PublicKey getTransactionPublicKey()
	{
	  PublicKey pk = new PublicKey(GlobalMembers.NULL_PUBLIC_KEY);
	  extra.getPublicKey(ref pk);
	  return pk;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint64_t getUnlockTime() const override
	public override uint64_t getUnlockTime()
	{
	  return transaction.unlockTime;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getPaymentId(Hash& hash) const override
	public override bool getPaymentId(ref Hash hash)
	{
	  List<uint8_t> nonce = new List<uint8_t>();
	  if (getExtraNonce(nonce))
	  {
		Hash paymentId = new Hash();
		if (getPaymentIdFromTransactionExtraNonce(nonce, paymentId))
		{
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		  hash = reinterpret_cast<const Hash&>(paymentId);
		  return true;
		}
	  }
	  return false;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getExtraNonce(BinaryArray& nonce) const override;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	override bool getExtraNonce(BinaryArray nonce);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<uint8_t> getExtra() const override
	public override List<uint8_t> getExtra()
	{
	  return transaction.extra;
	}

	// inputs
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual size_t getInputCount() const override
	public override size_t getInputCount()
	{
	  return transaction.inputs.Count;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint64_t getInputTotalAmount() const override
	public override uint64_t getInputTotalAmount()
	{
	  return std::accumulate(transaction.inputs.GetEnumerator(), transaction.inputs.end(), 0UL, (uint64_t val, boost::variant<BaseInput, KeyInput> in) =>
	  {
		return val + getTransactionInputAmount(in);
	  });
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual CryptoNote::TransactionTypes::InputType getInputType(size_t index) const override
	public override CryptoNote.TransactionTypes.InputType getInputType(size_t index)
	{
	  return getTransactionInputType(getInputChecked(transaction, index));
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual void getInput(size_t index, KeyInput& input) const override
	public override void getInput(size_t index, ref KeyInput input)
	{
	  input = boost::get<KeyInput>(getInputChecked(transaction, index, CryptoNote.TransactionTypes.InputType.Key));
	}

	// outputs
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual size_t getOutputCount() const override
	public override size_t getOutputCount()
	{
	  return transaction.outputs.Count;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint64_t getOutputTotalAmount() const override
	public override uint64_t getOutputTotalAmount()
	{
	  return std::accumulate(transaction.outputs.GetEnumerator(), transaction.outputs.end(), 0UL, (uint64_t val, TransactionOutput @out) =>
	  {
		return val + @out.amount;
	  });
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual CryptoNote::TransactionTypes::OutputType getOutputType(size_t index) const override
	public override CryptoNote.TransactionTypes.OutputType getOutputType(size_t index)
	{
	  return getTransactionOutputType(getOutputChecked(transaction, index).target);
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual void getOutput(size_t index, KeyOutput& output, uint64_t& amount) const override
	public override void getOutput(size_t index, ref KeyOutput output, ref uint64_t amount)
	{
	  auto @out = getOutputChecked(transaction, index, CryptoNote.TransactionTypes.OutputType.Key);
	  output = boost::get<KeyOutput>(@out.target);
	  amount = @out.amount;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual size_t getRequiredSignaturesCount(size_t index) const override
	public override size_t getRequiredSignaturesCount(size_t index)
	{
	  return global::getRequiredSignaturesCount(getInputChecked(transaction, index));
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool findOutputsToAccount(const AccountPublicAddress& addr, const SecretKey& viewSecretKey, ClassicVector<uint32_t>& out, uint64_t& amount) const override
	public override bool findOutputsToAccount(AccountPublicAddress addr, SecretKey viewSecretKey, List<uint32_t> @out, uint64_t amount)
	{
	  return global::CryptoNote.findOutputsToAccount(transaction, addr, viewSecretKey, @out, amount);
	}

	// various checks
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool validateInputs() const override
	public override bool validateInputs()
	{
	  return checkInputTypesSupported(transaction) && checkInputsOverflow(transaction) && checkInputsKeyimagesDiff(transaction);
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool validateOutputs() const override
	public override bool validateOutputs()
	{
	  return checkOutsValid(transaction) && checkOutsOverflow(transaction);
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool validateSignatures() const override
	public override bool validateSignatures()
	{
	  if (transaction.signatures.Count < transaction.inputs.Count)
	  {
		return false;
	  }

	  for (size_t i = 0; i < transaction.inputs.Count; ++i)
	  {
		if (getRequiredSignaturesCount(new size_t(i)) > transaction.signatures[i].Count)
		{
		  return false;
		}
	  }

	  return true;
	}

	// get serialized transaction
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<uint8_t> getTransactionData() const override
	public override List<uint8_t> getTransactionData()
	{
	  return CryptoNote.GlobalMembers.toBinaryArray(transaction);
	}

	// ITransactionWriter

	public override void setUnlockTime(uint64_t unlockTime)
	{
	  checkIfSigning();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: transaction.unlockTime = unlockTime;
	  transaction.unlockTime.CopyFrom(unlockTime);
	  invalidateHash();
	}
	public override void setPaymentId(Hash hash)
	{
	  checkIfSigning();
	  List<uint8_t> paymentIdBlob = new List<uint8_t>();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  setPaymentIdToTransactionExtraNonce(paymentIdBlob, reinterpret_cast<const Hash&>(hash));
	  setExtraNonce(paymentIdBlob);
	}
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	override void setExtraNonce(BinaryArray nonce);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	override void appendExtra(BinaryArray extraData);

	// Inputs/Outputs 
	public override size_t addInput(KeyInput input)
	{
	  checkIfSigning();
	  transaction.inputs.emplace_back(input);
	  invalidateHash();
	  return transaction.inputs.Count - 1;
	}
	public override size_t addInput(AccountKeys senderKeys, CryptoNote.TransactionTypes.InputKeyInfo info, KeyPair ephKeys)
	{
	  checkIfSigning();
	  KeyInput input = new KeyInput();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: input.amount = info.amount;
	  input.amount.CopyFrom(info.amount);

	  generate_key_image_helper(senderKeys, info.realOutput.transactionPublicKey, info.realOutput.outputInTransaction, ephKeys, input.keyImage);

	  // fill outputs array and use relative offsets
	  foreach (var @out in info.outputs)
	  {
		input.outputIndexes.Add(@out.outputIndex);
	  }

	  input.outputIndexes = absolute_output_offsets_to_relative(input.outputIndexes);
	  return addInput(input);
	}

	public override size_t addOutput(uint64_t amount, AccountPublicAddress to)
	{
	  checkIfSigning();

	  KeyOutput outKey = new KeyOutput();
	  GlobalMembers.derivePublicKey(to, txSecretKey(), transaction.outputs.Count, outKey.key);
	  TransactionOutput @out = new TransactionOutput(amount, outKey);
	  transaction.outputs.emplace_back(@out);
	  invalidateHash();

	  return transaction.outputs.Count - 1;
	}
	public override size_t addOutput(uint64_t amount, KeyOutput @out)
	{
	  checkIfSigning();
	  size_t outputIndex = transaction.outputs.Count;
	  TransactionOutput realOut = new TransactionOutput(amount, @out);
	  transaction.outputs.emplace_back(realOut);
	  invalidateHash();
	  return outputIndex;
	}

	public override void signInputKey(size_t index, CryptoNote.TransactionTypes.InputKeyInfo info, KeyPair ephKeys)
	{
	  auto input = boost::get<KeyInput>(getInputChecked(transaction, index, CryptoNote.TransactionTypes.InputType.Key));
	  Hash prefixHash = getTransactionPrefixHash();

	  List<Signature> signatures = new List<Signature>();
	  List<PublicKey> keysPtrs = new List<PublicKey>();

	  foreach (var o in info.outputs)
	  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		keysPtrs.Add(reinterpret_cast<const PublicKey>(o.targetKey));
	  }

	  signatures.Resize(keysPtrs.Count);

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  Crypto.GlobalMembers.generate_ring_signature(reinterpret_cast<const Hash&>(prefixHash), reinterpret_cast<const KeyImage&>(input.keyImage), keysPtrs, reinterpret_cast<const SecretKey&>(ephKeys.secretKey), new size_t(info.realOutput.transactionIndex), signatures.data());

	  getSignatures(new size_t(index)) = signatures;
	  invalidateHash();
	}

	// secret key
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getTransactionSecretKey(SecretKey& key) const override
	public override bool getTransactionSecretKey(ref SecretKey key)
	{
	  if (!secretKey)
	  {
		return false;
	  }
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  key = reinterpret_cast<const SecretKey&>(secretKey.get());
	  return true;
	}
	public override void setTransactionSecretKey(SecretKey key)
	{
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  auto sk = reinterpret_cast<const SecretKey&>(key);
	  PublicKey pk = new PublicKey();
	  PublicKey txPubKey = new PublicKey();

	  secret_key_to_public_key(sk, pk);
	  extra.getPublicKey(ref txPubKey);

	  if (txPubKey != pk)
	  {
		throw new System.Exception("Secret transaction key does not match public key");
	  }

	  secretKey = key;
	}


	private void invalidateHash()
	{
	  if (transactionHash.is_initialized())
	  {
		transactionHash = decltype(transactionHash)();
	  }
	}

	private List<Signature> getSignatures(size_t input)
	{
	  // update signatures container size if needed
	  if (transaction.signatures.Count < transaction.inputs.Count)
	  {
		transaction.signatures.Resize(transaction.inputs.Count);
	  }
	  // check range
	  if (input >= transaction.signatures.Count)
	  {
		throw new System.Exception("Invalid input index");
	  }

	  return transaction.signatures[input];
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const SecretKey& txSecretKey() const
	private SecretKey txSecretKey()
	{
	  if (!secretKey)
	  {
		throw new System.Exception("Operation requires transaction secret key");
	  }
	  return *secretKey;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void checkIfSigning() const
	private void checkIfSigning()
	{
	  if (transaction.signatures.Count > 0)
	  {
		throw new System.Exception("Cannot perform requested operation, since it will invalidate transaction signatures");
	  }
	}

	private CryptoNote.Transaction transaction = new CryptoNote.Transaction();
	private boost.optional<SecretKey> secretKey;
	private boost.optional<Hash> transactionHash;
	private TransactionExtra extra = new TransactionExtra();
  }




//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool TransactionImpl::getExtraNonce(ClassicVector<uint8_t>& nonce) const
}
