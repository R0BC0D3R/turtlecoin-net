// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

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


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl

namespace PaymentService
{

public class NodeFactory : System.IDisposable
{
  public static CryptoNote.INode createNode(string daemonAddress, ushort daemonPort, Logging.ILogger logger)
  {
	std::unique_ptr<CryptoNote.INode> node = new std::unique_ptr<CryptoNote.INode>(new CryptoNote.NodeRpcProxy(daemonAddress, daemonPort, logger));

	NodeInitObserver initObserver = new NodeInitObserver();
	node.init(std::bind(NodeInitObserver.initCompleted, initObserver, std::placeholders._1));
	initObserver.waitForInitEnd();

	return node.release();
  }
  public static CryptoNote.INode createNodeStub()
  {
	return new NodeRpcStub();
  }
  private NodeFactory()
  {
  }
  public void Dispose()
  {
  }

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  CryptoNote::INode getNode(string daemonAddress, ushort daemonPort);

  private static NodeFactory factory = new NodeFactory();
}

} //namespace PaymentService



namespace PaymentService
{

public class NodeRpcStub: CryptoNote.INode
{
  public override void Dispose()
  {
	  base.Dispose();
  }
  public override bool addObserver(CryptoNote.INodeObserver observer)
  {
	  return true;
  }
  public override bool removeObserver(CryptoNote.INodeObserver observer)
  {
	  return true;
  }

  public override void init(Callback callback)
  {
  }
  public override bool shutdown()
  {
	  return true;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getPeerCount() const override
  public override uint getPeerCount()
  {
	  return 0;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getLastLocalBlockHeight() const override
  public override uint getLastLocalBlockHeight()
  {
	  return 0;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getLastKnownBlockHeight() const override
  public override uint getLastKnownBlockHeight()
  {
	  return 0;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getLocalBlockCount() const override
  public override uint getLocalBlockCount()
  {
	  return 0;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getKnownBlockCount() const override
  public override uint getKnownBlockCount()
  {
	  return 0;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getLastLocalBlockTimestamp() const override
  public override ulong getLastLocalBlockTimestamp()
  {
	  return 0;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getNodeHeight() const override
  public override ulong getNodeHeight()
  {
	  return 0;
  }

  public override string getInfo()
  {
	  return string();
  }
  public override void getFeeInfo()
  {
  }

  public override void getBlockHashesByTimestamps(ulong timestampBegin, uint secondsCount, List<Crypto.Hash> blockHashes, Callback callback)
  {
	callback(std::error_code());
  }

  public override void getTransactionHashesByPaymentId(Crypto.Hash paymentId, List<Crypto.Hash> transactionHashes, Callback callback)
  {
	callback(std::error_code());
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual CryptoNote::BlockHeaderInfo getLastLocalBlockHeaderInfo() const override
  public override CryptoNote.BlockHeaderInfo getLastLocalBlockHeaderInfo()
  {
	  return new CryptoNote.BlockHeaderInfo();
  }

  public override void relayTransaction(CryptoNote.Transaction transaction, Callback callback)
  {
	  callback(std::error_code());
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public override void getRandomOutsByAmounts(List<ulong>&& amounts, ushort outsCount, List<CryptoNote.COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS.outs_for_amount> result, Callback callback)
  {
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public override void getNewBlocks(List<Crypto.Hash>&& knownBlockIds, List<CryptoNote.RawBlock> newBlocks, ref uint startHeight, Callback callback)
  {
	startHeight = 0;
	callback(std::error_code());
  }
  public override void getTransactionOutsGlobalIndices(Crypto.Hash transactionHash, List<uint> outsGlobalIndices, Callback callback)
  {
  }

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public override void queryBlocks(List<Crypto.Hash>&& knownBlockIds, ulong timestamp, List<CryptoNote.BlockShortEntry> newBlocks, ref uint startHeight, Callback callback)
  {
	startHeight = 0;
	callback(std::error_code());
  }

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public override void getPoolSymmetricDifference(List<Crypto.Hash>&& knownPoolTxIds, Crypto.Hash knownBlockId, ref bool isBcActual, List<std::unique_ptr<CryptoNote.ITransactionReader>> newTxs, List<Crypto.Hash> deletedTxIds, Callback callback)
  {
	isBcActual = true;
	callback(std::error_code());
  }

  public override void getBlocks(List<uint> blockHeights, List<List<CryptoNote.BlockDetails>> blocks, Callback callback)
  {
  }

  public override void getBlocks(List<Crypto.Hash> blockHashes, List<CryptoNote.BlockDetails> blocks, Callback callback)
  {
  }

  public override void getBlock(uint blockHeight, CryptoNote.BlockDetails block, Callback callback)
  {
  }

  public override void getTransactions(List<Crypto.Hash> transactionHashes, List<CryptoNote.TransactionDetails> transactions, Callback callback)
  {
  }

  public override void isSynchronized(ref bool syncStatus, Callback callback)
  {
  }
  public override string feeAddress()
  {
	  return string();
  }
  public override uint feeAmount()
  {
	  return 0;
  }
}


public class NodeInitObserver
{
  public NodeInitObserver()
  {
	initFuture = initPromise.get_future();
  }

  public void initCompleted(std::error_code result)
  {
	initPromise.set_value(result);
  }

  public void waitForInitEnd()
  {
	std::error_code ec = initFuture.get();
	if (ec != null)
	{
	  throw std::system_error(ec);
	}
	return;
  }

  private std::promise<std::error_code> initPromise = new std::promise<std::error_code>();
  private std::future<std::error_code> initFuture = new std::future<std::error_code>();
}

}
