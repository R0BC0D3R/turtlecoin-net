// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

namespace PaymentService
{

/* Forward declaration to avoid circular dependency from including "WalletService.h" */
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class WalletService;

public class RequestSerializationError: System.Exception
{

//C++ TO C# CONVERTER WARNING: Throw clauses are not available in C#:
//ORIGINAL LINE: virtual const char* what() const throw() override
  public override string what()
  {
	  return "Request error";
  }
}

public class Save
{
  public class Request
  {
	public void serialize(CryptoNote.ISerializer serializer)
	{
	}
  }

  public class Response
  {
	public void serialize(CryptoNote.ISerializer serializer)
	{
	}
  }
}

public class Export
{
  public class Request
  {
	public string fileName;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  if (serializer.functorMethod(fileName, "fileName") == null)
	  {
		throw new RequestSerializationError();
	  }
	}
  }

  public class Response
  {
	public void serialize(CryptoNote.ISerializer serializer)
	{
	}
  }
}

public class Reset
{
  public class Request
  {
	public string viewSecretKey;

	public ulong scanHeight = 0;

	public bool newAddress = false;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(scanHeight, "scanHeight");
	}
  }

  public class Response
  {
	public void serialize(CryptoNote.ISerializer serializer)
	{
	}
  }
}

public class GetViewKey
{
  public class Request
  {
	public void serialize(CryptoNote.ISerializer serializer)
	{
	}
  }

  public class Response
  {
	public string viewSecretKey;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(viewSecretKey, "viewSecretKey");
	}
  }
}

public class GetMnemonicSeed
{
  public class Request
  {
	public string address;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  if (serializer.functorMethod(address, "address") == null)
	  {
		throw new RequestSerializationError();
	  }
	}
  }

  public class Response
  {
	public string mnemonicSeed;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(mnemonicSeed, "mnemonicSeed");
	}
  }
}

public class GetStatus
{
  public class Request
  {
	public void serialize(CryptoNote.ISerializer serializer)
	{
	}
  }

  public class Response
  {
	public uint blockCount;
	public uint knownBlockCount;
	public ulong localDaemonBlockCount;
	public string lastBlockHash;
	public uint peerCount;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(blockCount, "blockCount");
	  serializer.functorMethod(knownBlockCount, "knownBlockCount");
	  serializer.functorMethod(localDaemonBlockCount, "localDaemonBlockCount");
	  serializer.functorMethod(lastBlockHash, "lastBlockHash");
	  serializer.functorMethod(peerCount, "peerCount");
	}
  }
}

public class GetAddresses
{
  public class Request
  {
	public void serialize(CryptoNote.ISerializer serializer)
	{
	}
  }

  public class Response
  {
	public List<string> addresses = new List<string>();

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(addresses, "addresses");
	}
  }
}

public class CreateAddress
{
  public class Request
  {
	public string spendSecretKey;
	public string spendPublicKey;

	public ulong scanHeight = 0;

	public bool newAddress = false;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  bool hasSecretKey = serializer.functorMethod(spendSecretKey, "spendSecretKey");
	  bool hasPublicKey = serializer.functorMethod(spendPublicKey, "spendPublicKey");

	  bool hasNewAddress = serializer.functorMethod(newAddress, "newAddress");
	  bool hasScanHeight = serializer.functorMethod(scanHeight, "scanHeight");

	  if (hasSecretKey && hasPublicKey)
	  {
		//TODO: replace it with error codes
		throw new RequestSerializationError();
	  }

	  /* Can't specify both that it is a new address, and a height to begin
	     scanning from */
	  if (hasNewAddress && hasScanHeight)
	  {
		throw new RequestSerializationError();
	  }
	}
  }

  public class Response
  {
	public string address;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(address, "address");
	}
  }
}

public class CreateAddressList
{
  public class Request
  {
	public List<string> spendSecretKeys = new List<string>();

	public ulong scanHeight = 0;

	public bool newAddress = false;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  if (serializer.functorMethod(spendSecretKeys, "spendSecretKeys") == null)
	  {
		//TODO: replace it with error codes
		throw new RequestSerializationError();
	  }

	  bool hasNewAddress = serializer.functorMethod(newAddress, "newAddress");
	  bool hasScanHeight = serializer.functorMethod(scanHeight, "scanHeight");

	  /* Can't specify both that it is a new address, and a height to begin
	     scanning from */
	  if (hasNewAddress && hasScanHeight)
	  {
		throw new RequestSerializationError();
	  }
	}
  }

  public class Response
  {
	public List<string> addresses = new List<string>();

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(addresses, "addresses");
	}
  }
}

public class DeleteAddress
{
  public class Request
  {
	public string address;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  if (serializer.functorMethod(address, "address") == null)
	  {
		throw new RequestSerializationError();
	  }
	}
  }

  public class Response
  {
	public void serialize(CryptoNote.ISerializer serializer)
	{
	}
  }
}

public class GetSpendKeys
{
  public class Request
  {
	public string address;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  if (serializer.functorMethod(address, "address") == null)
	  {
		throw new RequestSerializationError();
	  }
	}
  }

  public class Response
  {
	public string spendSecretKey;
	public string spendPublicKey;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(spendSecretKey, "spendSecretKey");
	  serializer.functorMethod(spendPublicKey, "spendPublicKey");
	}
  }
}

public class GetBalance
{
  public class Request
  {
	public string address;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(address, "address");
	}
  }

  public class Response
  {
	public ulong availableBalance;
	public ulong lockedAmount;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(availableBalance, "availableBalance");
	  serializer.functorMethod(lockedAmount, "lockedAmount");
	}
  }
}

public class GetBlockHashes
{
  public class Request
  {
	public uint firstBlockIndex;
	public uint blockCount;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  bool r = serializer.functorMethod(firstBlockIndex, "firstBlockIndex");
	  r &= serializer.functorMethod(blockCount, "blockCount");

	  if (!r)
	  {
		throw new RequestSerializationError();
	  }
	}
  }

  public class Response
  {
	public List<string> blockHashes = new List<string>();

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(blockHashes, "blockHashes");
	}
  }
}

public class TransactionHashesInBlockRpcInfo
{
  public string blockHash;
  public List<string> transactionHashes = new List<string>();

  public void serialize(CryptoNote.ISerializer serializer)
  {
	serializer.functorMethod(blockHash, "blockHash");
	serializer.functorMethod(transactionHashes, "transactionHashes");
  }
}

public class GetTransactionHashes
{
  public class Request
  {
	public List<string> addresses = new List<string>();
	public string blockHash;
	public uint firstBlockIndex = uint.MaxValue;
	public uint blockCount;
	public string paymentId;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(addresses, "addresses");

	  if (serializer.functorMethod(blockHash, "blockHash") == serializer.functorMethod(firstBlockIndex, "firstBlockIndex"))
	  {
		throw new RequestSerializationError();
	  }

	  if (serializer.functorMethod(blockCount, "blockCount") == null)
	  {
		throw new RequestSerializationError();
	  }

	  serializer.functorMethod(paymentId, "paymentId");
	}
  }

  public class Response
  {
	public List<TransactionHashesInBlockRpcInfo> items = new List<TransactionHashesInBlockRpcInfo>();

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(items, "items");
	}
  }
}

public class TransferRpcInfo
{
  public byte type;
  public string address;
  public long amount;

  public void serialize(CryptoNote.ISerializer serializer)
  {
	serializer.functorMethod(type, "type");
	serializer.functorMethod(address, "address");
	serializer.functorMethod(amount, "amount");
  }
}

public class TransactionRpcInfo
{
  public byte state;
  public string transactionHash;
  public uint blockIndex;
  public ulong timestamp;
  public bool isBase;
  public ulong unlockTime;
  public long amount;
  public ulong fee;
  public List<TransferRpcInfo> transfers = new List<TransferRpcInfo>();
  public string extra;
  public string paymentId;

  public void serialize(CryptoNote.ISerializer serializer)
  {
	serializer.functorMethod(state, "state");
	serializer.functorMethod(transactionHash, "transactionHash");
	serializer.functorMethod(blockIndex, "blockIndex");
	serializer.functorMethod(timestamp, "timestamp");
	serializer.functorMethod(isBase, "isBase");
	serializer.functorMethod(unlockTime, "unlockTime");
	serializer.functorMethod(amount, "amount");
	serializer.functorMethod(fee, "fee");
	serializer.functorMethod(transfers, "transfers");
	serializer.functorMethod(extra, "extra");
	serializer.functorMethod(paymentId, "paymentId");
  }
}

public class GetTransaction
{
  public class Request
  {
	public string transactionHash;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  if (serializer.functorMethod(transactionHash, "transactionHash") == null)
	  {
		throw new RequestSerializationError();
	  }
	}
  }

  public class Response
  {
	public TransactionRpcInfo transaction = new TransactionRpcInfo();

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(transaction, "transaction");
	}
  }
}

public class TransactionsInBlockRpcInfo
{
  public string blockHash;
  public List<TransactionRpcInfo> transactions = new List<TransactionRpcInfo>();

  public void serialize(CryptoNote.ISerializer serializer)
  {
	serializer.functorMethod(blockHash, "blockHash");
	serializer.functorMethod(transactions, "transactions");
  }
}

public class GetTransactions
{
  public class Request
  {
	public List<string> addresses = new List<string>();
	public string blockHash;
	public uint firstBlockIndex = uint.MaxValue;
	public uint blockCount;
	public string paymentId;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(addresses, "addresses");

	  if (serializer.functorMethod(blockHash, "blockHash") == serializer.functorMethod(firstBlockIndex, "firstBlockIndex"))
	  {
		throw new RequestSerializationError();
	  }

	  if (serializer.functorMethod(blockCount, "blockCount") == null)
	  {
		throw new RequestSerializationError();
	  }

	  serializer.functorMethod(paymentId, "paymentId");
	}
  }

  public class Response
  {
	public List<TransactionsInBlockRpcInfo> items = new List<TransactionsInBlockRpcInfo>();

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(items, "items");
	}
  }
}

public class GetUnconfirmedTransactionHashes
{
  public class Request
  {
	public List<string> addresses = new List<string>();

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(addresses, "addresses");
	}
  }

  public class Response
  {
	public List<string> transactionHashes = new List<string>();

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(transactionHashes, "transactionHashes");
	}
  }
}

public class WalletRpcOrder
{
  public string address;
  public ulong amount;

  public void serialize(CryptoNote.ISerializer serializer)
  {
	bool r = serializer.functorMethod(address, "address");
	r &= serializer.functorMethod(amount, "amount");

	if (!r)
	{
	  throw new RequestSerializationError();
	}
  }
}

public class SendTransaction
{
  public class Request
  {
	public List<string> sourceAddresses = new List<string>();
	public List<WalletRpcOrder> transfers = new List<WalletRpcOrder>();
	public string changeAddress;
	public ulong fee = 0;
	public ulong anonymity;
	public string extra;
	public string paymentId;
	public ulong unlockTime = 0;

	public void serialize(CryptoNote.ISerializer serializer, WalletService service)
	{
	  serializer.functorMethod(sourceAddresses, "addresses");

	  if (serializer.functorMethod(transfers, "transfers") == null)
	  {
		throw new RequestSerializationError();
	  }

	  serializer.functorMethod(changeAddress, "changeAddress");

	  if (serializer.functorMethod(fee, "fee") == null)
	  {
		throw new RequestSerializationError();
	  }

	  if (serializer.functorMethod(anonymity, "anonymity") == null)
	  {
		anonymity = service.getDefaultMixin();
	  }

	  bool hasExtra = serializer.functorMethod(extra, "extra");
	  bool hasPaymentId = serializer.functorMethod(paymentId, "paymentId");

	  if (hasExtra && hasPaymentId)
	  {
		throw new RequestSerializationError();
	  }

	  serializer.functorMethod(unlockTime, "unlockTime");
	}
  }

  public class Response
  {
	public string transactionHash;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(transactionHash, "transactionHash");
	}
  }
}

public class CreateDelayedTransaction
{
  public class Request
  {
	public List<string> addresses = new List<string>();
	public List<WalletRpcOrder> transfers = new List<WalletRpcOrder>();
	public string changeAddress;
	public ulong fee = 0;
	public ulong anonymity;
	public string extra;
	public string paymentId;
	public ulong unlockTime = 0;

	public void serialize(CryptoNote.ISerializer serializer, WalletService service)
	{
	  serializer.functorMethod(addresses, "addresses");

	  if (serializer.functorMethod(transfers, "transfers") == null)
	  {
		throw new RequestSerializationError();
	  }

	  serializer.functorMethod(changeAddress, "changeAddress");

	  if (serializer.functorMethod(fee, "fee") == null)
	  {
		throw new RequestSerializationError();
	  }

	  if (serializer.functorMethod(anonymity, "anonymity") == null)
	  {
		anonymity = service.getDefaultMixin();
	  }

	  bool hasExtra = serializer.functorMethod(extra, "extra");
	  bool hasPaymentId = serializer.functorMethod(paymentId, "paymentId");

	  if (hasExtra && hasPaymentId)
	  {
		throw new RequestSerializationError();
	  }

	  serializer.functorMethod(unlockTime, "unlockTime");
	}
  }

  public class Response
  {
	public string transactionHash;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(transactionHash, "transactionHash");
	}
  }
}

public class GetDelayedTransactionHashes
{
  public class Request
  {
	public void serialize(CryptoNote.ISerializer serializer)
	{
	}
  }

  public class Response
  {
	public List<string> transactionHashes = new List<string>();

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(transactionHashes, "transactionHashes");
	}
  }
}

public class DeleteDelayedTransaction
{
  public class Request
  {
	public string transactionHash;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  if (serializer.functorMethod(transactionHash, "transactionHash") == null)
	  {
		throw new RequestSerializationError();
	  }
	}
  }

  public class Response
  {
	public void serialize(CryptoNote.ISerializer serializer)
	{
	}
  }
}

public class SendDelayedTransaction
{
  public class Request
  {
	public string transactionHash;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  if (serializer.functorMethod(transactionHash, "transactionHash") == null)
	  {
		throw new RequestSerializationError();
	  }
	}
  }

  public class Response
  {
	public void serialize(CryptoNote.ISerializer serializer)
	{
	}
  }
}

public class SendFusionTransaction
{
  public class Request
  {
	public ulong threshold;
	public ulong anonymity;
	public List<string> addresses = new List<string>();
	public string destinationAddress;

	public void serialize(CryptoNote.ISerializer serializer, WalletService service)
	{
	  if (serializer.functorMethod(threshold, "threshold") == null)
	  {
		throw new RequestSerializationError();
	  }

	  if (serializer.functorMethod(anonymity, "anonymity") == null)
	  {
		anonymity = service.getDefaultMixin();
	  }

	  serializer.functorMethod(addresses, "addresses");
	  serializer.functorMethod(destinationAddress, "destinationAddress");
	}
  }

  public class Response
  {
	public string transactionHash;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(transactionHash, "transactionHash");
	}
  }
}

public class EstimateFusion
{
  public class Request
  {
	public ulong threshold;
	public List<string> addresses = new List<string>();

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  if (serializer.functorMethod(threshold, "threshold") == null)
	  {
		throw new RequestSerializationError();
	  }

	  serializer.functorMethod(addresses, "addresses");
	}
  }

  public class Response
  {
	public uint fusionReadyCount;
	public uint totalOutputCount;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(fusionReadyCount, "fusionReadyCount");
	  serializer.functorMethod(totalOutputCount, "totalOutputCount");
	}
  }
}

public class CreateIntegratedAddress
{
  public class Request
  {
	public string address;
	public string paymentId;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  if (serializer.functorMethod(address, "address") == null)
	  {
		throw new RequestSerializationError();
	  }

	  if (serializer.functorMethod(paymentId, "paymentId") == null)
	  {
		throw new RequestSerializationError();
	  }
	}
  }

  public class Response
  {
	public string integratedAddress;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(integratedAddress, "integratedAddress");
	}
  }
}

public class NodeFeeInfo
{
  public class Request
  {
	public void serialize(CryptoNote.ISerializer serializer)
	{
	}
  }

  public class Response
  {
	public string address;
	public uint amount;

	public void serialize(CryptoNote.ISerializer serializer)
	{
	  serializer.functorMethod(address, "address");
	  serializer.functorMethod(amount, "amount");
	}
  }
}

} //namespace PaymentService

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
//ORIGINAL LINE: #define ENDL std::endl

