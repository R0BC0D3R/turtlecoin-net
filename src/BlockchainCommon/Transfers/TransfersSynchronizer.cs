//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);


using Common;
using Crypto;
using System;
using System.Collections.Generic;
using System.Diagnostics;

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The BBSCoin Developers
// Copyright (c) 2018, The Karbo Developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The BBSCoin Developers
// Copyright (c) 2018, The Karbo Developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.


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

namespace CryptoNote
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class Currency;
}

namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class TransfersConsumer;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class INode;

//C++ TO C# CONVERTER TODO TASK: Multiple inheritance is not available in C#:
public class TransfersSyncronizer : ITransfersSynchronizer, IBlockchainConsumerObserver
{
  public TransfersSyncronizer(CryptoNote.Currency currency, Logging.ILogger logger, IBlockchainSynchronizer sync, INode node)
  {
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
	  this.m_currency = new CryptoNote.Currency(currency);
	  this.m_logger = new Logging.LoggerRef(logger, "TransfersSyncronizer");
	  this.m_sync = new CryptoNote.IBlockchainSynchronizer(sync);
	  this.m_node = new CryptoNote.INode(node);
  }
  public override void Dispose()
  {
	m_sync.stop();
	foreach (var kv in m_consumers)
	{
	  m_sync.removeConsumer(kv.second.get());
	}
	  base.Dispose();
  }

  public void initTransactionPool(HashSet<Crypto.Hash> uncommitedTransactions)
  {
	for (var it = m_consumers.GetEnumerator(); it != m_consumers.end(); ++it)
	{
	  it.second.initTransactionPool(uncommitedTransactions);
	}
  }

  // ITransfersSynchronizer
  public override ITransfersSubscription addSubscription(AccountSubscription acc)
  {
	var it = m_consumers.find(acc.keys.address.viewPublicKey);

	if (it == m_consumers.end())
	{
	  std::unique_ptr<TransfersConsumer> consumer = new std::unique_ptr<TransfersConsumer>(new TransfersConsumer(m_currency, m_node, m_logger.getLogger(), acc.keys.viewSecretKey));

	  m_sync.addConsumer(consumer.get());
	  consumer.addObserver(this);
	  it = m_consumers.Add(acc.keys.address.viewPublicKey, std::move(consumer)).first;
	}

//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	return it.second.addSubscription(acc);
  }
  public override bool removeSubscription(AccountPublicAddress acc)
  {
	var it = m_consumers.find(acc.viewPublicKey);
	if (it == m_consumers.end())
	{
	  return false;
	}

//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	if (it.second.removeSubscription(acc))
	{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  m_sync.removeConsumer(it.second.get());
	  m_consumers.Remove(it);

	  m_subscribers.Remove(acc.viewPublicKey);
	}

	return true;
  }
  public override void getSubscriptions(List<AccountPublicAddress> subscriptions)
  {
	foreach (var kv in m_consumers)
	{
	  kv.second.getSubscriptions(subscriptions);
	}
  }
  public override ITransfersSubscription getSubscription(AccountPublicAddress acc)
  {
	var it = m_consumers.find(acc.viewPublicKey);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	return (it == m_consumers.end()) ? null : it.second.getSubscription(acc);
  }
  public override List<Crypto.Hash> getViewKeyKnownBlocks(Crypto.PublicKey publicViewKey)
  {
	var it = m_consumers.find(publicViewKey);
	if (it == m_consumers.end())
	{
	  throw new System.ArgumentException("Consumer not found");
	}

//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	return m_sync.getConsumerKnownBlocks(it.second);
  }

  public void subscribeConsumerNotifications(Crypto.PublicKey viewPublicKey, ITransfersSynchronizerObserver observer)
  {
	var it = m_subscribers.find(viewPublicKey);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	if (it != m_subscribers.end())
	{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  it.second.add(observer);
	  return;
	}

	var insertedIt = m_subscribers.Add(viewPublicKey, std::unique_ptr<SubscribersNotifier>(new SubscribersNotifier())).first;
	insertedIt.second.add(observer);
  }
  public void unsubscribeConsumerNotifications(Crypto.PublicKey viewPublicKey, ITransfersSynchronizerObserver observer)
  {
	m_subscribers[viewPublicKey].remove(observer);
  }

  public void addPublicKeysSeen(AccountPublicAddress acc, Crypto.Hash transactionHash, Crypto.PublicKey outputKey)
  {
	var it = m_consumers.find(acc.viewPublicKey);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	if (it != m_consumers.end())
	{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	   it.second.addPublicKeysSeen(transactionHash, outputKey);
	}
  }

  // IStreamSerializable
  public override void save(std::ostream os)
  {
	m_sync.save(os);

	StdOutputStream stream = new StdOutputStream(os);
	CryptoNote.BinaryOutputStreamSerializer s = new CryptoNote.BinaryOutputStreamSerializer(stream);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
	s.functorMethod(const_cast<uint&>(GlobalMembers.TRANSFERS_STORAGE_ARCHIVE_VERSION), "version");

	ulong subscriptionCount = m_consumers.Count;

	s.beginArray(ref subscriptionCount, "consumers");

	foreach (var consumer in m_consumers)
	{
	  s.beginObject("");
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
	  s.functorMethod(const_cast<PublicKey&>(consumer.first), "view_key");

	  std::stringstream consumerState = new std::stringstream();
	  // synchronization state
	  m_sync.getConsumerState(consumer.second.get()).save(consumerState);

	  string blob = consumerState.str();
	  s.functorMethod(blob, "state");

	  List<AccountPublicAddress> subscriptions = new List<AccountPublicAddress>();
	  consumer.second.getSubscriptions(subscriptions);
	  ulong subCount = subscriptions.Count;

	  s.beginArray(ref subCount, "subscriptions");

	  foreach (var addr in subscriptions)
	  {
		var sub = consumer.second.getSubscription(addr);
		if (sub != null)
		{
		  s.beginObject("");

		  std::stringstream subState = new std::stringstream();
		  Debug.Assert(sub);
		  sub.getContainer().save(subState);
		  // store data block
		  string blob = subState.str();
		  s.functorMethod(addr, "address");
		  s.functorMethod(blob, "state");

		  s.endObject();
		}
	  }

	  s.endArray();
	  s.endObject();
	}
  }
  public override void load(std::istream @is)
  {
	m_sync.load(@is);

	StdInputStream inputStream = new StdInputStream(@is);
	CryptoNote.BinaryInputStreamSerializer s = new CryptoNote.BinaryInputStreamSerializer(inputStream);
	uint version = 0;

	s.functorMethod(version, "version");

	if (version > GlobalMembers.TRANSFERS_STORAGE_ARCHIVE_VERSION)
	{
	  throw new System.Exception("TransfersSyncronizer version mismatch");
	}


//C++ TO C# CONVERTER TODO TASK: C# does not allow declaring types within methods:
//	struct ConsumerState
//	{
//	  PublicKey viewKey;
//	  string state;
//	  ClassicVector<System.Tuple<AccountPublicAddress, string>> subscriptionStates;
//	};

	List<ConsumerState> updatedStates = new List<ConsumerState>();

	try
	{
	  ulong subscriptionCount = 0;
	  s.beginArray(ref subscriptionCount, "consumers");

	  while (subscriptionCount-- != 0)
	  {
		s.beginObject("");
		PublicKey viewKey = new PublicKey();
		s.functorMethod(viewKey, "view_key");

		string blob;
		s.functorMethod(blob, "state");

		var subIter = m_consumers.find(viewKey);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
		if (subIter != m_consumers.end())
		{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
		  var consumerState = m_sync.getConsumerState(subIter.second.get());
		  Debug.Assert(consumerState);

		  {
			// store previous state
			var prevConsumerState = GlobalMembers.getObjectState(*consumerState);
			// load consumer state
			GlobalMembers.setObjectState(*consumerState, blob);
			updatedStates.Add(ConsumerState({viewKey, std::move(prevConsumerState)}));
		  }

		  // load subscriptions
		  ulong subCount = 0;
		  s.beginArray(ref subCount, "subscriptions");

		  while (subCount-- != 0)
		  {
			s.beginObject("");

			AccountPublicAddress acc = new AccountPublicAddress();
			string state;

			s.functorMethod(acc, "address");
			s.functorMethod(state, "state");

//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
			var sub = subIter.second.getSubscription(acc);

			if (sub != null)
			{
			  var prevState = GlobalMembers.getObjectState(sub.getContainer());
			  GlobalMembers.setObjectState(sub.getContainer(), state);
			  updatedStates[updatedStates.Count - 1].subscriptionStates.push_back(Tuple.Create(acc, prevState));
			}
			else
			{
			  m_logger.functorMethod(Logging.Level.DEBUGGING) << "Subscription not found: " << m_currency.accountAddressAsString(acc);
			}

			s.endObject();
		  }

		  s.endArray();
		}
		else
		{
		  m_logger.functorMethod(Logging.Level.DEBUGGING) << "Consumer not found: " << viewKey;
		}

		s.endObject();
	  }

	  s.endArray();

	}
	catch
	{
	  // rollback state
	  foreach (var consumerState in updatedStates)
	  {
		var consumer = m_consumers[consumerState.viewKey].get();
		GlobalMembers.setObjectState(m_sync.getConsumerState(new Dictionary<Crypto.PublicKey, std::unique_ptr<TransfersConsumer>>.Enumerator(consumer)), consumerState.state);
		foreach (var sub in consumerState.subscriptionStates)
		{
		  GlobalMembers.setObjectState(consumer.getSubscription(sub.first).getContainer(), sub.second);
		}
	  }
	  throw;
	}

  }

  private Logging.LoggerRef m_logger = new Logging.LoggerRef();

  // map { view public key -> consumer }
  private Dictionary<Crypto.PublicKey, std::unique_ptr<TransfersConsumer>> m_consumers = new Dictionary<Crypto.PublicKey, std::unique_ptr<TransfersConsumer>>();

  private Dictionary<Crypto.PublicKey, std::unique_ptr<Tools.ObserverManager<ITransfersSynchronizerObserver>>> m_subscribers = new Dictionary<Crypto.PublicKey, std::unique_ptr<Tools.ObserverManager<ITransfersSynchronizerObserver>>>();

  // std::unordered_map<AccountAddress, std::unique_ptr<TransfersConsumer>> m_subscriptions;
  private IBlockchainSynchronizer m_sync;
  private INode m_node;
  private readonly CryptoNote.Currency m_currency;

  private override void onBlocksAdded(IBlockchainConsumer consumer, List<Crypto.Hash> blockHashes)
  {
	var it = findSubscriberForConsumer(consumer);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	if (it != m_subscribers.end())
	{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  it.second.notify(ITransfersSynchronizerObserver.onBlocksAdded, it.first, blockHashes);
	}
  }
  private override void onBlockchainDetach(IBlockchainConsumer consumer, uint blockIndex)
  {
	var it = findSubscriberForConsumer(consumer);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	if (it != m_subscribers.end())
	{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  it.second.notify(ITransfersSynchronizerObserver.onBlockchainDetach, it.first, blockIndex);
	}
  }
  private override void onTransactionDeleteBegin(IBlockchainConsumer consumer, Crypto.Hash transactionHash)
  {
	var it = findSubscriberForConsumer(consumer);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	if (it != m_subscribers.end())
	{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  it.second.notify(ITransfersSynchronizerObserver.onTransactionDeleteBegin, it.first, transactionHash);
	}
  }
  private override void onTransactionDeleteEnd(IBlockchainConsumer consumer, Crypto.Hash transactionHash)
  {
	var it = findSubscriberForConsumer(consumer);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	if (it != m_subscribers.end())
	{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  it.second.notify(ITransfersSynchronizerObserver.onTransactionDeleteEnd, it.first, transactionHash);
	}
  }
  private override void onTransactionUpdated(IBlockchainConsumer consumer, Crypto.Hash transactionHash, List<ITransfersContainer> containers)
  {

	var it = findSubscriberForConsumer(consumer);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	if (it != m_subscribers.end())
	{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  it.second.notify(ITransfersSynchronizerObserver.onTransactionUpdated, it.first, transactionHash, containers);
	}
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool findViewKeyForConsumer(IBlockchainConsumer* consumer, Crypto::PublicKey& viewKey) const
  private bool findViewKeyForConsumer(IBlockchainConsumer consumer, ref Crypto.PublicKey viewKey)
  {
	//since we have only couple of consumers linear complexity is fine
//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: auto it = std::find_if(m_consumers.begin(), m_consumers.end(), [consumer] (const ConsumersContainer::value_type& subscription)
	var it = std::find_if(m_consumers.GetEnumerator(), m_consumers.end(), (ConsumersContainer.value_type subscription) =>
	{
	  return subscription.second.get() == consumer;
	});

	if (it == m_consumers.end())
	{
	  return false;
	}

	viewKey = it.first;
	return true;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicUnorderedMapIterator<Crypto::PublicKey, std::unique_ptr<Tools::ObserverManager<ITransfersSynchronizerObserver>>> findSubscriberForConsumer(IBlockchainConsumer* consumer) const
  private Dictionary<Crypto.PublicKey, std::unique_ptr<Tools.ObserverManager<ITransfersSynchronizerObserver>>>.Enumerator findSubscriberForConsumer(IBlockchainConsumer consumer)
  {
	Crypto.PublicKey viewKey = new Crypto.PublicKey();
	if (findViewKeyForConsumer(consumer, ref viewKey))
	{
	  var it = m_subscribers.find(viewKey);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  if (it != m_subscribers.end())
	  {
		return it;
	  }
	}

	return m_subscribers.end();
  }
}

}

namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace


}
