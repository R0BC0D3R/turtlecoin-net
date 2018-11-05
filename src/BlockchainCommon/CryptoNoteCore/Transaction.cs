// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

namespace Crypto
{
    public class Hash
    {
        public ushort[] data = Arrays.InitializeWithDefaultInstances<ushort>(32);
    }

    public class PublicKey
    {
        public ushort[] data = Arrays.InitializeWithDefaultInstances<ushort>(32);
    }

    public class SecretKey
    {
        public ushort[] data = Arrays.InitializeWithDefaultInstances<ushort>(32);
    }

    public class KeyDerivation
    {
        public ushort[] data = Arrays.InitializeWithDefaultInstances<ushort>(32);
    }

    public class KeyImage
    {
        public ushort[] data = Arrays.InitializeWithDefaultInstances<ushort>(32);
    }

    public class Signature
    {
        public ushort[] data = Arrays.InitializeWithDefaultInstances<ushort>(64);
    }
}


namespace CryptoNote
{
    public class BaseInput
    {
        public uint blockIndex = new uint();
    }

    public class KeyInput
    {
        public ulong amount = new ulong();
        public List<uint> outputIndexes = new List<uint>();
        public Crypto.KeyImage keyImage = new Crypto.KeyImage();
    }

    public class KeyOutput
    {
        public Crypto.PublicKey key = new Crypto.PublicKey();
    }

    public class TransactionInput
    {
        // TODO: This is temporary so code compiles. Need to revisit this.
        public Dictionary<BaseInput, KeyInput> TransactionInputDictionary = new Dictionary<BaseInput, KeyInput>();
    }

    public class TransactionOutputTarget
    {
        // TODO: This is temporary so code compiles. Need to revisit this.
        public HashSet<KeyOutput> TransactionOutputTargetHashSet = new HashSet<KeyOutput>();
    }

    public class TransactionOutput
    {
        public ulong amount = new ulong();
        public TransactionOutputTarget target = new TransactionOutputTarget();
    }

    public class TransactionPrefix
    {
        public ushort version = new ushort();
        public ulong unlockTime = new ulong();
        public List<TransactionInput> inputs = new List<TransactionInput>();
        public List<TransactionOutput> outputs = new List<TransactionOutput>();
        public List<ushort> extra = new List<ushort>();
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
        public ushort majorVersion = new ushort();
        public ushort minorVersion = new ushort();
        public Crypto.Hash previousBlockHash = new Crypto.Hash();
        public ushort transactionCount = new ushort();
        public List<Crypto.Hash> baseTransactionBranch = new List<Crypto.Hash>();
        public BaseTransaction baseTransaction = new BaseTransaction();
        public List<Crypto.Hash> blockchainBranch = new List<Crypto.Hash>();
    }

    public class BlockHeader
    {
        public ushort majorVersion = new ushort();
        public ushort minorVersion = new ushort();
        public uint nonce = new uint();
        public ulong timestamp = new ulong();
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

    public class BinaryArray
    {
        // TODO: This is temporary so code compiles. Need to revisit this.
        public HashSet<ushort> BinaryArrayHashSet = new HashSet<ushort>();
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
        public enum InputType : ushort
        {
            Invalid,
            Key,
            Generating
        }
        public enum OutputType : ushort
        {
            Invalid,
            Key
        }

        public class GlobalOutput
        {
            public Crypto.PublicKey targetKey = new Crypto.PublicKey();
            public uint outputIndex = new uint();
        }


        public class OutputKeyInfo
        {
            public Crypto.PublicKey transactionPublicKey = new Crypto.PublicKey();
            public uint transactionIndex = new uint();
            public uint outputInTransaction = new uint();
        }

        public class GlobalOutputsContainer
        {
            // TODO: This is temporary so code compiles. Need to revisit this.
            public HashSet<GlobalOutput> GlobalOutputContainerHashSet = new HashSet<GlobalOutput>();
        }

        public class InputKeyInfo
        {
            public ulong amount = new ulong();
            public GlobalOutputsContainer outputs = new GlobalOutputsContainer();
            public OutputKeyInfo realOutput = new OutputKeyInfo();
        }
    }



    public abstract class ITransactionReader : System.IDisposable
    {
        public virtual void Dispose()
        { }

        public abstract Crypto.Hash GetTransactionHash();
        public abstract Crypto.Hash GetTransactionPrefixHash();
        public abstract Crypto.PublicKey GetTransactionPublicKey();
        public abstract bool GetTransactionSecretKey(Crypto.SecretKey key);
        public abstract ulong GetUnlockTime();

        // Extra
        public abstract bool GetPaymentId(Crypto.Hash paymentId);
        public abstract bool GetExtraNonce(BinaryArray nonce);
        public abstract BinaryArray GetExtra();

        // Inputs
        public abstract uint GetInputCount();
        public abstract ulong GetInputTotalAmount();
        public abstract TransactionTypes.InputType GetInputType(uint index);
        public abstract void GetInput(uint index, KeyInput input);

        // Outputs
        public abstract uint GetOutputCount();
        public abstract ulong GetOutputTotalAmount();
        public abstract TransactionTypes.OutputType GetOutputType(uint index);
        public abstract void GetOutput(uint index, KeyOutput output, ulong amount);

        // Signatures
        public abstract uint GetRequiredSignaturesCount(uint inputIndex);
        public abstract bool FindOutputsToAccount(AccountPublicAddress addr, Crypto.SecretKey viewSecretKey, List<uint> outs, ulong outputAmount);

        // Various checks
        public abstract bool ValidateInputs();
        public abstract bool ValidateOutputs();
        public abstract bool ValidateSignatures();

        // Serialized transaction
        public abstract BinaryArray GetTransactionData();
    }



    public abstract class ITransactionWriter : System.IDisposable
    {
        public virtual void Dispose()
        { }

        // Transaction parameters
        public abstract void SetUnlockTime(ulong unlockTime);

        // Extra
        public abstract void SetPaymentId(Crypto.Hash paymentId);
        public abstract void SetExtraNonce(BinaryArray nonce);
        public abstract void AppendExtra(BinaryArray extraData);

        // Inputs/Outputs 
        public abstract uint AddInput(KeyInput input);
        public abstract uint AddInput(AccountKeys senderKeys, TransactionTypes.InputKeyInfo info, KeyPair ephKeys);

        public abstract uint AddOutput(ulong amount, AccountPublicAddress to);
        public abstract uint AddOutput(ulong amount, KeyOutput @out);

        // Transaction info
        public abstract void SetTransactionSecretKey(Crypto.SecretKey key);

        // Signing
        public abstract void SignInputKey(uint input, TransactionTypes.InputKeyInfo info, KeyPair ephKeys);
    }

    //C++ TO C# CONVERTER TODO TASK: Multiple inheritance is not available in C#:
    //TODO: Deal with this somehow
    public abstract class ITransaction : ITransactionReader, ITransactionWriter
    {
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}



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
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

namespace CryptoNote
{

  public class TransactionExtra
  {
	public TransactionExtra()
	{
	}
	public TransactionExtra(List<ushort> extra)
	{
	  parse(extra);
	}

	public bool parse(List<ushort> extra)
	{
	  fields.Clear();
	  return GlobalMembers.parseTransactionExtra(extra, fields);
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
//ORIGINAL LINE: ClassicVector<ushort> serialize() const
	public List<ushort> serialize()
	{
	  List<ushort> extra = new List<ushort>();
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
	  transaction.extra = new List<ushort>(extra.serialize());

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
	public override Hash GetTransactionHash()
	{
	  if (!transactionHash.is_initialized())
	  {
		transactionHash = CryptoNote.GlobalMembers.getObjectHash(transaction);
	  }

	  return transactionHash.get();
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual Hash getTransactionPrefixHash() const override
	public override Hash GetTransactionPrefixHash()
	{
	  return CryptoNote.GlobalMembers.getObjectHash((TransactionPrefix)transaction);
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual PublicKey getTransactionPublicKey() const override
	public override PublicKey GetTransactionPublicKey()
	{
	  PublicKey pk = new PublicKey(GlobalMembers.NULL_PUBLIC_KEY);
	  extra.getPublicKey(ref pk);
	  return pk;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getUnlockTime() const override
	public override ulong GetUnlockTime()
	{
	  return transaction.unlockTime;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool getPaymentId(Hash& hash) const override
	public override bool getPaymentId(ref Hash hash)
	{
	  List<ushort> nonce = new List<ushort>();
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
//ORIGINAL LINE: virtual ClassicVector<ushort> getExtra() const override
	public override List<ushort> GetExtra()
	{
	  return transaction.extra;
	}

	// inputs
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getInputCount() const override
	public override uint GetInputCount()
	{
	  return transaction.inputs.Count;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getInputTotalAmount() const override
	public override ulong GetInputTotalAmount()
	{
	  return std::accumulate(transaction.inputs.GetEnumerator(), transaction.inputs.end(), 0UL, (ulong val, boost::variant<BaseInput, KeyInput> in) =>
	  {
		return val + getTransactionInputAmount(in);
	  });
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual CryptoNote::TransactionTypes::InputType getInputType(uint index) const override
	public override CryptoNote.TransactionTypes.InputType GetInputType(uint index)
	{
	  return getTransactionInputType(getInputChecked(transaction, index));
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual void getInput(uint index, KeyInput& input) const override
	public override void getInput(uint index, ref KeyInput input)
	{
	  input = boost::get<KeyInput>(getInputChecked(transaction, index, CryptoNote.TransactionTypes.InputType.Key));
	}

	// outputs
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getOutputCount() const override
	public override uint GetOutputCount()
	{
	  return transaction.outputs.Count;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getOutputTotalAmount() const override
	public override ulong GetOutputTotalAmount()
	{
	  return std::accumulate(transaction.outputs.GetEnumerator(), transaction.outputs.end(), 0UL, (ulong val, TransactionOutput @out) =>
	  {
		return val + @out.amount;
	  });
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual CryptoNote::TransactionTypes::OutputType getOutputType(uint index) const override
	public override CryptoNote.TransactionTypes.OutputType GetOutputType(uint index)
	{
	  return getTransactionOutputType(getOutputChecked(transaction, index).target);
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual void getOutput(uint index, KeyOutput& output, ulong& amount) const override
	public override void getOutput(uint index, ref KeyOutput output, ref ulong amount)
	{
	  auto @out = getOutputChecked(transaction, index, CryptoNote.TransactionTypes.OutputType.Key);
	  output = boost::get<KeyOutput>(@out.target);
	  amount = @out.amount;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getRequiredSignaturesCount(uint index) const override
	public override uint GetRequiredSignaturesCount(uint index)
	{
	  return global::getRequiredSignaturesCount(getInputChecked(transaction, index));
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool findOutputsToAccount(const AccountPublicAddress& addr, const SecretKey& viewSecretKey, ClassicVector<uint>& out, ulong& amount) const override
	public override bool findOutputsToAccount(AccountPublicAddress addr, SecretKey viewSecretKey, List<uint> @out, ulong amount)
	{
	  return global::CryptoNote.findOutputsToAccount(transaction, addr, viewSecretKey, @out, amount);
	}

	// various checks
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool validateInputs() const override
	public override bool ValidateInputs()
	{
	  return checkInputTypesSupported(transaction) && checkInputsOverflow(transaction) && checkInputsKeyimagesDiff(transaction);
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool validateOutputs() const override
	public override bool ValidateOutputs()
	{
	  return checkOutsValid(transaction) && checkOutsOverflow(transaction);
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool validateSignatures() const override
	public override bool ValidateSignatures()
	{
	  if (transaction.signatures.Count < transaction.inputs.Count)
	  {
		return false;
	  }

	  for (uint i = 0; i < transaction.inputs.Count; ++i)
	  {
		if (GetRequiredSignaturesCount(new uint(i)) > transaction.signatures[i].Count)
		{
		  return false;
		}
	  }

	  return true;
	}

	// get serialized transaction
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<ushort> getTransactionData() const override
	public override List<ushort> GetTransactionData()
	{
	  return CryptoNote.GlobalMembers.toBinaryArray(transaction);
	}

	// ITransactionWriter

	public override void setUnlockTime(ulong unlockTime)
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
	  List<ushort> paymentIdBlob = new List<ushort>();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  setPaymentIdToTransactionExtraNonce(paymentIdBlob, reinterpret_cast<const Hash&>(hash));
	  setExtraNonce(paymentIdBlob);
	}
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	override void setExtraNonce(BinaryArray nonce);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	override void appendExtra(BinaryArray extraData);

	// Inputs/Outputs 
	public override uint addInput(KeyInput input)
	{
	  checkIfSigning();
	  transaction.inputs.emplace_back(input);
	  invalidateHash();
	  return transaction.inputs.Count - 1;
	}
	public override uint addInput(AccountKeys senderKeys, CryptoNote.TransactionTypes.InputKeyInfo info, KeyPair ephKeys)
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

	public override uint addOutput(ulong amount, AccountPublicAddress to)
	{
	  checkIfSigning();

	  KeyOutput outKey = new KeyOutput();
	  GlobalMembers.derivePublicKey(to, txSecretKey(), transaction.outputs.Count, outKey.key);
	  TransactionOutput @out = new TransactionOutput(amount, outKey);
	  transaction.outputs.emplace_back(@out);
	  invalidateHash();

	  return transaction.outputs.Count - 1;
	}
	public override uint addOutput(ulong amount, KeyOutput @out)
	{
	  checkIfSigning();
	  uint outputIndex = transaction.outputs.Count;
	  TransactionOutput realOut = new TransactionOutput(amount, @out);
	  transaction.outputs.emplace_back(realOut);
	  invalidateHash();
	  return outputIndex;
	}

	public override void signInputKey(uint index, CryptoNote.TransactionTypes.InputKeyInfo info, KeyPair ephKeys)
	{
	  auto input = boost::get<KeyInput>(getInputChecked(transaction, index, CryptoNote.TransactionTypes.InputType.Key));
	  Hash prefixHash = GetTransactionPrefixHash();

	  List<Signature> signatures = new List<Signature>();
	  List<PublicKey> keysPtrs = new List<PublicKey>();

	  foreach (var o in info.outputs)
	  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		keysPtrs.Add(reinterpret_cast<const PublicKey>(o.targetKey));
	  }

	  signatures.Resize(keysPtrs.Count);

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  Crypto.GlobalMembers.generate_ring_signature(reinterpret_cast<const Hash&>(prefixHash), reinterpret_cast<const KeyImage&>(input.keyImage), keysPtrs, reinterpret_cast<const SecretKey&>(ephKeys.secretKey), new uint(info.realOutput.transactionIndex), signatures.data());

	  getSignatures(new uint(index)) = signatures;
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

	private List<Signature> getSignatures(uint input)
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
//ORIGINAL LINE: bool TransactionImpl::getExtraNonce(ClassicVector<ushort>& nonce) const
}
