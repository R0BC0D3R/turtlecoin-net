// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The BBSCoin Developers
// Copyright (c) 2018, The Karbo Developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


using Common;
using Crypto;
using CryptoNote;
using Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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
//ORIGINAL LINE: #define ENDL std::endl
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

namespace CryptoNote
{

public class PreparedTransaction
{
	public ITransaction transaction;
	public List<WalletTransfer> destinations = new List<WalletTransfer>();
	public ulong neededMoney;
	public ulong changeAmount;
}

//C++ TO C# CONVERTER TODO TASK: Multiple inheritance is not available in C#:
public abstract partial class WalletGreen : IWallet, ITransfersObserver, IBlockchainSynchronizerObserver, ITransfersSynchronizerObserver, IFusionManager
{
  public WalletGreen(System.Dispatcher dispatcher, Currency currency, INode node, Logging.ILogger logger, uint transactionSoftLockTime = 1)
  {
	  this.m_dispatcher = dispatcher;
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
	  this.m_currency = new CryptoNote.Currency(currency);
	  this.m_node = new CryptoNote.INode(node);
	  this.m_logger = new Logging.LoggerRef(logger, "WalletGreen/empty");
	  this.m_stopped = false;
	  this.m_blockchainSynchronizerStarted = false;
	  this.m_blockchainSynchronizer = new CryptoNote.BlockchainSynchronizer(node, logger, currency.genesisBlockHash());
	  this.m_synchronizer = new CryptoNote.TransfersSyncronizer(currency, logger, m_blockchainSynchronizer, node);
	  this.m_eventOccurred = m_dispatcher;
	  this.m_readyEvent = m_dispatcher;
	  this.m_state = new CryptoNote.WalletGreen.WalletState.NOT_INITIALIZED;
	  this.m_actualBalance = 0;
	  this.m_pendingBalance = 0;
	  this.m_transactionSoftLockTime = transactionSoftLockTime;
	m_readyEvent.set();
  }
  public override void Dispose()
  {
	if (m_state == WalletState.INITIALIZED)
	{
	  doShutdown();
	}

	m_dispatcher.yield(); //let remote spawns finish
	  base.Dispose();
  }

  public override void initialize(string path, string password)
  {
	Crypto.PublicKey viewPublicKey = new Crypto.PublicKey();
	Crypto.SecretKey viewSecretKey = new Crypto.SecretKey();
	Crypto.generate_keys(viewPublicKey, viewSecretKey);

	initWithKeys(path, password, viewPublicKey, viewSecretKey, 0, true);
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "New container initialized, public view key " << viewPublicKey;
  }
  public override void initializeWithViewKey(string path, string password, Crypto.SecretKey viewSecretKey, ulong scanHeight, bool newAddress)
  {
	Crypto.PublicKey viewPublicKey = new Crypto.PublicKey();
	if (!Crypto.secret_key_to_public_key(viewSecretKey, viewPublicKey))
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "initializeWithViewKey(" << viewSecretKey << ") Failed to convert secret key to public key";
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.KEY_GENERATION_ERROR));
	}

	initWithKeys(path, password, viewPublicKey, viewSecretKey, scanHeight, newAddress);
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Container initialized with view secret key, public view key " << viewPublicKey;
  }
  public override void load(string path, string password, string extra)
  {
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Loading container...";

	if (m_state != WalletState.NOT_INITIALIZED)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to load: already initialized. Current state: " << m_state;
	  throw std::system_error(GlobalMembers.make_error_code(error.WRONG_STATE));
	}

	throwIfStopped();

	stopBlockchainSynchronizer();

	Crypto.GlobalMembers.generate_chacha8_key(password, m_key);

	std::ifstream walletFileStream = new std::ifstream(path, std::ios_base.binary);
	int version = walletFileStream.peek();
	if (version == EOF)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to read wallet version";
	  throw std::system_error(GlobalMembers.make_error_code(error.WRONG_VERSION), "Failed to read wallet version");
	}

	if (version < WalletSerializerV2.MIN_VERSION || version > WalletSerializerV2.SERIALIZATION_VERSION)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Unsupported wallet version: " << version;
	  throw std::system_error(GlobalMembers.make_error_code(error.WRONG_VERSION), "Unsupported wallet version");
	}
	else
	{
	  walletFileStream.close();

	  loadContainerStorage(path);
	  subscribeWallets();

	  if (m_containerStorage.suffixSize() > 0)
	  {
		try
		{
		  HashSet<Crypto.PublicKey> addedSpendKeys = new HashSet<Crypto.PublicKey>();
		  HashSet<Crypto.PublicKey> deletedSpendKeys = new HashSet<Crypto.PublicKey>();
		  loadWalletCache(ref addedSpendKeys, ref deletedSpendKeys, extra);

		  if (addedSpendKeys.Count > 0)
		  {
			m_logger.functorMethod(WARNING, BRIGHT_YELLOW) << "Found addresses not saved in container cache. Resynchronize container";
			clearCaches(false, true);
			subscribeWallets();
		  }

		  if (deletedSpendKeys.Count > 0)
		  {
			m_logger.functorMethod(WARNING, BRIGHT_YELLOW) << "Found deleted addresses saved in container cache. Remove its transactions";
			deleteOrphanTransactions(deletedSpendKeys);
		  }

		  if (addedSpendKeys.Count > 0 || deletedSpendKeys.Count > 0)
		  {
			saveWalletCache(m_containerStorage, m_key, WalletSaveLevel.SAVE_ALL, extra);
		  }
		}
		catch (System.Exception e)
		{
		  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to load cache: " << e.Message << ", reset wallet data";
		  clearCaches(true, true);
		  subscribeWallets();
		}
	  }
	}

	// Read all output keys cache
	try
	{
	  List<AccountPublicAddress> subscriptionList = new List<AccountPublicAddress>();
	  m_synchronizer.getSubscriptions(subscriptionList);
	  foreach (var addr in subscriptionList)
	  {
		var sub = m_synchronizer.getSubscription(addr);
		if (sub != null)
		{
		   List<TransactionOutputInformation> allTransfers = new List<TransactionOutputInformation>();
		   ITransfersContainer container = sub.getContainer();
		   container.getOutputs(allTransfers, ITransfersContainer.IncludeAll);
		   m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Known Transfers " << allTransfers.Count;
		   foreach (var o in allTransfers)
		   {
			   if (o.type == TransactionTypes.OutputType.Key)
			   {
				  m_synchronizer.addPublicKeysSeen(addr, o.transactionHash, o.outputKey);
			   }
		   }
		}
	  }
	}
	catch (System.Exception e)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to read output keys!! Continue without output keys: " << e.Message;
	}

	m_blockchainSynchronizer.addObserver(this);

	initTransactionPool();

	Debug.Assert(m_blockchain.empty());
	if (m_walletsContainer.get<RandomAccessIndex>().size() != 0)
	{
	  m_synchronizer.subscribeConsumerNotifications(m_viewPublicKey, this);
	  initBlockchain(m_viewPublicKey);

	  startBlockchainSynchronizer();
	}
	else
	{
	  m_blockchain.push_back(m_currency.genesisBlockHash());
	  m_logger.functorMethod(DEBUGGING) << "Add genesis block hash to blockchain";
	}

	m_password = password;
	m_path = path;
	m_extra = extra;

	m_state = WalletState.INITIALIZED;
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Container loaded, view public key " << m_viewPublicKey << ", wallet count " << m_walletsContainer.size() << ", actual balance " << m_currency.formatAmount(m_actualBalance) << ", pending balance " << m_currency.formatAmount(m_pendingBalance);
  }
  public override void load(string path, string password)
  {
	string extra;
	load(path, password, extra);
  }
  public override void shutdown()
  {
	throwIfNotInitialized();
	doShutdown();

	m_dispatcher.yield(); //let remote spawns finish
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Container shut down";
	m_logger = new Logging.LoggerRef(m_logger.getLogger(), "WalletGreen/empty");
  }

  public override void changePassword(string oldPassword, string newPassword)
  {
	throwIfNotInitialized();
	throwIfStopped();

	if (m_password.CompareTo(oldPassword))
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to change password: the old password is wrong";
	  throw std::system_error(GlobalMembers.make_error_code(error.WRONG_PASSWORD));
	}

	if (oldPassword == newPassword)
	{
	  return;
	}

	Crypto.chacha8_key newKey = new Crypto.chacha8_key();
	Crypto.GlobalMembers.generate_chacha8_key(newPassword, newKey);

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: m_containerStorage.atomicUpdate([this, newKey](Common::FileMappedVector<EncryptedWalletRecord>& newStorage)
	m_containerStorage.atomicUpdate((Common.FileMappedVector<EncryptedWalletRecord> newStorage) =>
	{
	  copyContainerStoragePrefix(m_containerStorage, m_key, newStorage, newKey);
	  copyContainerStorageKeys(m_containerStorage, m_key, newStorage, newKey);

	  if (m_containerStorage.suffixSize() > 0)
	  {
		List<byte> containerData = new List<byte>();
		loadAndDecryptContainerData(m_containerStorage, m_key, containerData);
		encryptAndSaveContainerData(newStorage, newKey, containerData.data(), containerData.Count);
	  }
	});

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_key = newKey;
	m_key.CopyFrom(newKey);
	m_password = newPassword;

	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Container password changed";
  }
  public override void save(WalletSaveLevel saveLevel = WalletSaveLevel.SAVE_ALL, string extra = "")
  {
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Saving container...";

	throwIfNotInitialized();
	throwIfStopped();

	stopBlockchainSynchronizer();

	try
	{
	  saveWalletCache(m_containerStorage, m_key, saveLevel, extra);
	}
	catch (System.Exception e)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to save container: " << e.Message;
	  startBlockchainSynchronizer();
	  throw;
	}

	startBlockchainSynchronizer();
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Container saved";
  }
  public override void reset(ulong scanHeight)
  {
	  throwIfNotInitialized();
	  throwIfStopped();

	  /* Stop so things can't be added to the container as we're looping */
	  stop();

	  /* Grab the wallet encrypted prefix */
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  auto prefix = reinterpret_cast<ContainerStoragePrefix>(m_containerStorage.prefix());

	  ulong newTimestamp = scanHeightToTimestamp(scanHeight);

	  /* Reencrypt with the new creation timestamp so we rescan from here when we relaunch */
	  prefix.encryptedViewKeys = encryptKeyPair(m_viewPublicKey, m_viewSecretKey, newTimestamp);

	  /* As a reference so we can update it */
	  foreach (var encryptedSpendKeys in m_containerStorage)
	  {
		  Crypto.PublicKey publicKey = new Crypto.PublicKey();
		  Crypto.SecretKey secretKey = new Crypto.SecretKey();
		  ulong oldTimestamp;

		  /* Decrypt the key pair we're pointing to */
		  decryptKeyPair(encryptedSpendKeys, publicKey, secretKey, ref oldTimestamp);

		  /* Re-encrypt with the new timestamp */
		  encryptedSpendKeys = encryptKeyPair(publicKey, secretKey, newTimestamp);
	  }

	  /* Start again so we can save */
	  start();

	  /* Save just the keys + timestamp to file */
	  save(CryptoNote.WalletSaveLevel.SAVE_KEYS_ONLY);

	  /* Stop and shutdown */
	  stop();

	  /* Shutdown the wallet */
	  shutdown();

	  start();

	  /* Reopen from truncated storage */
	  load(m_path, m_password);
  }
  public override void exportWallet(string path, bool encrypt = true, WalletSaveLevel saveLevel = WalletSaveLevel.SAVE_ALL, string extra = "")
  {
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Exporting container...";

	throwIfNotInitialized();
	throwIfStopped();

	stopBlockchainSynchronizer();

	try
	{
	  bool storageCreated = false;
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: Tools::ScopeExit failExitHandler([path, &storageCreated]
	  Tools.ScopeExit failExitHandler(() =>
	  {
		// Don't delete file if it has existed
		if (storageCreated)
		{
		  boost::system.error_code ignore = new boost::system.error_code();
		  boost::filesystem.remove(path, ignore);
		}
	  });

	  Common.FileMappedVector<EncryptedWalletRecord> newStorage = new Common.FileMappedVector<EncryptedWalletRecord>(path, FileMappedVectorOpenMode.CREATE, m_containerStorage.prefixSize());
	  storageCreated = true;

	  chacha8_key newStorageKey = new chacha8_key();
	  if (encrypt)
	  {
		newStorageKey = m_key;
	  }
	  else
	  {
		Crypto.GlobalMembers.generate_chacha8_key("", newStorageKey);
	  }

	  copyContainerStoragePrefix(m_containerStorage, m_key, newStorage, newStorageKey);
	  copyContainerStorageKeys(m_containerStorage, m_key, newStorage, newStorageKey);
	  saveWalletCache(newStorage, newStorageKey, saveLevel, extra);

	  failExitHandler.cancel();

	  m_logger.functorMethod(DEBUGGING) << "Container export finished";
	}
	catch (System.Exception e)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to export container: " << e.Message;
	  startBlockchainSynchronizer();
	  throw;
	}

	startBlockchainSynchronizer();
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Container exported";
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getAddressCount() const override
  public override uint getAddressCount()
  {
	throwIfNotInitialized();
	throwIfStopped();

	return m_walletsContainer.get<RandomAccessIndex>().size();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual string getAddress(uint index) const override
  public override string getAddress(uint index)
  {
	throwIfNotInitialized();
	throwIfStopped();

	if (index >= m_walletsContainer.get<RandomAccessIndex>().size())
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to get address: invalid address index " << (int)index;
	  throw std::system_error(GlobalMembers.make_error_code(std::errc.invalid_argument));
	}

	WalletRecord wallet = m_walletsContainer.get<RandomAccessIndex>()[index];
	return m_currency.accountAddressAsString(new AccountBase(wallet.spendPublicKey, m_viewPublicKey));
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual KeyPair getAddressSpendKey(uint index) const override
  public override KeyPair getAddressSpendKey(uint index)
  {
	throwIfNotInitialized();
	throwIfStopped();

	if (index >= m_walletsContainer.get<RandomAccessIndex>().size())
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to get address spend key: invalid address index " << (int)index;
	  throw std::system_error(GlobalMembers.make_error_code(std::errc.invalid_argument));
	}

	WalletRecord wallet = m_walletsContainer.get<RandomAccessIndex>()[index];
	return new KeyPair(wallet.spendPublicKey, wallet.spendSecretKey);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual KeyPair getAddressSpendKey(const string& address) const override
  public override KeyPair getAddressSpendKey(string address)
  {
	throwIfNotInitialized();
	throwIfStopped();

	CryptoNote.AccountPublicAddress pubAddr = parseAddress(address);

	var it = m_walletsContainer.get<KeysIndex>().find(pubAddr.spendPublicKey);
	if (it == m_walletsContainer.get<KeysIndex>().end())
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to get address spend key: address not found " << address;
	  throw std::system_error(GlobalMembers.make_error_code(error.OBJECT_NOT_FOUND));
	}

	return new KeyPair(it.spendPublicKey, it.spendSecretKey);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual KeyPair getViewKey() const override
  public override KeyPair getViewKey()
  {
	throwIfNotInitialized();
	throwIfStopped();

	return new KeyPair(m_viewPublicKey, m_viewSecretKey);
  }

  public override string createAddress()
  {
	  KeyPair spendKey = new KeyPair();

	  Crypto.generate_keys(spendKey.publicKey, spendKey.secretKey);

	  return doCreateAddress(spendKey.publicKey, spendKey.secretKey, 0, true);
  }
  public override string createAddress(Crypto.SecretKey spendSecretKey, ulong scanHeight, bool newAddress)
  {
	  Crypto.PublicKey spendPublicKey = new Crypto.PublicKey();

	  if (!Crypto.secret_key_to_public_key(spendSecretKey, spendPublicKey))
	  {
		  m_logger.functorMethod(ERROR, BRIGHT_RED) << "createAddress(" << spendSecretKey << ") Failed to convert secret key to public key";
		  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.KEY_GENERATION_ERROR));
	  }

	  return doCreateAddress(spendPublicKey, spendSecretKey, scanHeight, newAddress);
  }
  public override string createAddress(Crypto.PublicKey spendPublicKey, ulong scanHeight, bool newAddress)
  {
	  if (!Crypto.check_key(spendPublicKey))
	  {
		  m_logger.functorMethod(ERROR, BRIGHT_RED) << "createAddress(" << spendPublicKey << ") Wrong public key format";
		  throw std::system_error(GlobalMembers.make_error_code(error.WRONG_PARAMETERS), "Wrong public key format");
	  }

	  return doCreateAddress(spendPublicKey, GlobalMembers.NULL_SECRET_KEY, scanHeight, newAddress);
  }

  public override List<string> createAddressList(List<Crypto.SecretKey> spendSecretKeys, ulong scanHeight, bool newAddress)
  {
	  List<NewAddressData> addressDataList = new List<NewAddressData>(spendSecretKeys.Count);

	  for (uint i = 0; i < spendSecretKeys.Count; ++i)
	  {
		  Crypto.PublicKey spendPublicKey = new Crypto.PublicKey();

		  if (!Crypto.secret_key_to_public_key(spendSecretKeys[i], spendPublicKey))
		  {
			  m_logger.functorMethod(ERROR, BRIGHT_RED) << "createAddressList(): failed to convert secret key to public key, secret key " << spendSecretKeys[i];
			  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.KEY_GENERATION_ERROR));
		  }

		  addressDataList[i].spendSecretKey = spendSecretKeys[i];
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: addressDataList[i].spendPublicKey = spendPublicKey;
		  addressDataList[i].spendPublicKey.CopyFrom(spendPublicKey);
	  }

	  return doCreateAddressList(addressDataList, scanHeight, newAddress);
  }

  public override void deleteAddress(string address)
  {
	throwIfNotInitialized();
	throwIfStopped();

	CryptoNote.AccountPublicAddress pubAddr = parseAddress(address);

	var it = m_walletsContainer.get<KeysIndex>().find(pubAddr.spendPublicKey);
	if (it == m_walletsContainer.get<KeysIndex>().end())
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to delete wallet: address not found " << address;
	  throw std::system_error(GlobalMembers.make_error_code(error.OBJECT_NOT_FOUND));
	}

	stopBlockchainSynchronizer();

	m_actualBalance -= it.actualBalance;
	m_pendingBalance -= it.pendingBalance;

	if (it.actualBalance != 0 || it.pendingBalance != 0)
	{
	  m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Container balance updated, actual " << m_currency.formatAmount(m_actualBalance) << ", pending " << m_currency.formatAmount(m_pendingBalance);
	}

	var addressIndex = std::distance(m_walletsContainer.get<RandomAccessIndex>().begin(), m_walletsContainer.project<RandomAccessIndex>(it));

#if !NDEBUG
	Crypto.PublicKey publicKey = new Crypto.PublicKey();
	Crypto.SecretKey secretKey = new Crypto.SecretKey();
	ulong creationTimestamp;
	decryptKeyPair(m_containerStorage[addressIndex], publicKey, secretKey, ref creationTimestamp);
	Debug.Assert(publicKey == it.spendPublicKey);
	Debug.Assert(secretKey == it.spendSecretKey);
	Debug.Assert(creationTimestamp == (ulong)it.creationTimestamp);
#endif

	m_containerStorage.erase(std::next(m_containerStorage.begin(), addressIndex));

	m_synchronizer.removeSubscription(pubAddr);

	deleteContainerFromUnlockTransactionJobs(it.container);
	List<uint> deletedTransactions = new List<uint>();
	List<uint> updatedTransactions = deleteTransfersForAddress(address, deletedTransactions);
	deleteFromUncommitedTransactions(deletedTransactions);

	m_walletsContainer.get<KeysIndex>().erase(it);
	m_logger.functorMethod(DEBUGGING) << "Wallet count " << m_walletsContainer.size();

	if (m_walletsContainer.get<RandomAccessIndex>().size() != 0)
	{
	  startBlockchainSynchronizer();
	}
	else
	{
	  m_blockchain.clear();
	  m_blockchain.push_back(m_currency.genesisBlockHash());
	}

	foreach (var transactionId in updatedTransactions)
	{
	  pushEvent(GlobalMembers.makeTransactionUpdatedEvent(transactionId));
	}

	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Wallet deleted " << address;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getActualBalance() const override
  public override ulong getActualBalance()
  {
	throwIfNotInitialized();
	throwIfStopped();

	return m_actualBalance;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getActualBalance(const string& address) const override
  public override ulong getActualBalance(string address)
  {
	throwIfNotInitialized();
	throwIfStopped();

	auto wallet = getWalletRecord(address);
	return wallet.actualBalance;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getPendingBalance() const override
  public override ulong getPendingBalance()
  {
	throwIfNotInitialized();
	throwIfStopped();

	return m_pendingBalance;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ulong getPendingBalance(const string& address) const override
  public override ulong getPendingBalance(string address)
  {
	throwIfNotInitialized();
	throwIfStopped();

	auto wallet = getWalletRecord(address);
	return wallet.pendingBalance;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getTransactionCount() const override
  public override uint getTransactionCount()
  {
	throwIfNotInitialized();
	throwIfStopped();

	return m_transactions.get<RandomAccessIndex>().size();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual WalletTransaction getTransaction(uint transactionIndex) const override
  public override WalletTransaction getTransaction(uint transactionIndex)
  {
	throwIfNotInitialized();
	throwIfStopped();

	if (m_transactions.size() <= transactionIndex)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to get transaction: invalid index " << (int)transactionIndex << ". Number of transactions: " << m_transactions.size();
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.INDEX_OUT_OF_RANGE));
	}

	return m_transactions.get<RandomAccessIndex>()[transactionIndex];
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getTransactionTransferCount(uint transactionIndex) const override
  public override uint getTransactionTransferCount(uint transactionIndex)
  {
	throwIfNotInitialized();
	throwIfStopped();

	var bounds = getTransactionTransfersRange(transactionIndex);
	return (uint)std::distance(bounds.Item1, bounds.Item2);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual WalletTransfer getTransactionTransfer(uint transactionIndex, uint transferIndex) const override
  public override WalletTransfer getTransactionTransfer(uint transactionIndex, uint transferIndex)
  {
	throwIfNotInitialized();
	throwIfStopped();

	var bounds = getTransactionTransfersRange(transactionIndex);

	if (transferIndex >= (uint)std::distance(bounds.Item1, bounds.Item2))
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to get transfer: invalid transfer index " << (int)transferIndex << ". Transaction index " << (int)transactionIndex << " transfer count " << std::distance(bounds.Item1, bounds.Item2);
	  throw std::system_error(GlobalMembers.make_error_code(std::errc.invalid_argument));
	}

	return std::next(bounds.Item1, transferIndex).second;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual WalletTransactionWithTransfers getTransaction(const Crypto::Hash& transactionHash) const override
  public override WalletTransactionWithTransfers getTransaction(Crypto.Hash transactionHash)
  {
	throwIfNotInitialized();
	throwIfStopped();

	auto hashIndex = m_transactions.get<TransactionIndex>();
	var it = hashIndex.find(transactionHash);
	if (it == hashIndex.end())
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to get transaction: not found. Transaction hash " << transactionHash;
	  throw std::system_error(GlobalMembers.make_error_code(error.OBJECT_NOT_FOUND), "Transaction not found");
	}

	WalletTransactionWithTransfers walletTransaction = new WalletTransactionWithTransfers();
	walletTransaction.transaction = it;
	walletTransaction.transfers = new List<WalletTransfer>(getTransactionTransfers(*it));

	return walletTransaction;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<TransactionsInBlockInfo> getTransactions(const Crypto::Hash& blockHash, uint count) const override
  public override List<TransactionsInBlockInfo> getTransactions(Crypto.Hash blockHash, uint count)
  {
	throwIfNotInitialized();
	throwIfStopped();

	auto hashIndex = m_blockchain.get<BlockHashIndex>();
	var it = hashIndex.find(blockHash);
	if (it == hashIndex.end())
	{
	  return new List<TransactionsInBlockInfo>();
	}

	var heightIt = m_blockchain.project<BlockHeightIndex>(it);

	uint blockIndex = (uint)std::distance(m_blockchain.get<BlockHeightIndex>().begin(), heightIt);
	return getTransactionsInBlocks(blockIndex, count);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<TransactionsInBlockInfo> getTransactions(uint blockIndex, uint count) const override
  public override List<TransactionsInBlockInfo> getTransactions(uint blockIndex, uint count)
  {
	throwIfNotInitialized();
	throwIfStopped();

	return getTransactionsInBlocks(blockIndex, count);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<Crypto::Hash> getBlockHashes(uint blockIndex, uint count) const override
  public override List<Crypto.Hash> getBlockHashes(uint blockIndex, uint count)
  {
	throwIfNotInitialized();
	throwIfStopped();

	auto index = m_blockchain.get<BlockHeightIndex>();

	if (blockIndex >= index.size())
	{
	  return new List<Crypto.Hash>();
	}

	var start = std::next(index.begin(), blockIndex);
	var end = std::next(index.begin(), Math.Min(index.size(), blockIndex + count));
	return new List<Crypto.Hash>(start, end);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint getBlockCount() const override
  public override uint getBlockCount()
  {
	throwIfNotInitialized();
	throwIfStopped();

	uint blockCount = (uint)m_blockchain.size();
	Debug.Assert(blockCount != 0);

	return blockCount;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<WalletTransactionWithTransfers> getUnconfirmedTransactions() const override
  public override List<WalletTransactionWithTransfers> getUnconfirmedTransactions()
  {
	throwIfNotInitialized();
	throwIfStopped();

	List<WalletTransactionWithTransfers> result = new List<WalletTransactionWithTransfers>();
	var lowerBound = m_transactions.get<BlockHeightIndex>().lower_bound(GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT);
	for (var it = lowerBound; it != m_transactions.get<BlockHeightIndex>().end(); ++it)
	{
	  if (it.state != WalletTransactionState.SUCCEEDED)
	  {
		continue;
	  }

	  WalletTransactionWithTransfers transaction = new WalletTransactionWithTransfers();
	  transaction.transaction = it;
	  transaction.transfers = new List<WalletTransfer>(getTransactionTransfers(*it));

	  result.Add(transaction);
	}

	return result;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual ClassicVector<uint> getDelayedTransactionIds() const override
  public override List<uint> getDelayedTransactionIds()
  {
	throwIfNotInitialized();
	throwIfStopped();
	throwIfTrackingMode();

	List<uint> result = new List<uint>();
	result.Capacity = m_uncommitedTransactions.size();

	foreach (var kv in m_uncommitedTransactions)
	{
	  result.Add(kv.first);
	}

	return result;
  }

  public override uint transfer(TransactionParameters transactionParameters)
  {
	uint id = GlobalMembers.WALLET_INVALID_TRANSACTION_ID;
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: Tools::ScopeExit releaseContext([this, &id]
	Tools.ScopeExit releaseContext(() =>
	{
	  m_dispatcher.yield();

	  if (id != WALLET_INVALID_TRANSACTION_ID)
	  {
		auto tx = m_transactions[id];
		m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Transaction created and send, ID " << (int)id << ", hash " << m_transactions[id].hash << ", state " << tx.state << ", totalAmount " << m_currency.formatAmount(tx.totalAmount) << ", fee " << m_currency.formatAmount(tx.fee) << ", transfers: " << new TransferListFormatter(m_currency, getTransactionTransfersRange(id));
	  }
	});

	System.EventLock lk = new System.EventLock(m_readyEvent);

	throwIfNotInitialized();
	throwIfTrackingMode();
	throwIfStopped();

	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "transfer" << ", from " << Common.GlobalMembers.makeContainerFormatter(transactionParameters.sourceAddresses) << ", to " << new WalletOrderListFormatter(m_currency, transactionParameters.destinations) << ", change address '" << transactionParameters.changeDestination << '\'' << ", fee " << m_currency.formatAmount(transactionParameters.fee) << ", mixin " << transactionParameters.mixIn << ", unlockTimestamp " << (int)transactionParameters.unlockTimestamp;

	id = doTransfer(transactionParameters);
	return id;
  }

  public override uint makeTransaction(TransactionParameters sendingTransaction)
  {
	uint id = GlobalMembers.WALLET_INVALID_TRANSACTION_ID;
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: Tools::ScopeExit releaseContext([this, &id]
	Tools.ScopeExit releaseContext(() =>
	{
	  m_dispatcher.yield();

	  if (id != WALLET_INVALID_TRANSACTION_ID)
	  {
		auto tx = m_transactions[id];
		m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Delayed transaction created, ID " << (int)id << ", hash " << m_transactions[id].hash << ", state " << tx.state << ", totalAmount " << m_currency.formatAmount(tx.totalAmount) << ", fee " << m_currency.formatAmount(tx.fee) << ", transfers: " << new TransferListFormatter(m_currency, getTransactionTransfersRange(id));
	  }
	});

	System.EventLock lk = new System.EventLock(m_readyEvent);

	throwIfNotInitialized();
	throwIfTrackingMode();
	throwIfStopped();

	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "makeTransaction" << ", from " << Common.GlobalMembers.makeContainerFormatter(sendingTransaction.sourceAddresses) << ", to " << new WalletOrderListFormatter(m_currency, sendingTransaction.destinations) << ", change address '" << sendingTransaction.changeDestination << '\'' << ", fee " << m_currency.formatAmount(sendingTransaction.fee) << ", mixin " << sendingTransaction.mixIn << ", unlockTimestamp " << (int)sendingTransaction.unlockTimestamp;

	validateTransactionParameters(sendingTransaction);
	CryptoNote.AccountPublicAddress changeDestination = getChangeDestination(sendingTransaction.changeDestination, sendingTransaction.sourceAddresses);
	m_logger.functorMethod(DEBUGGING) << "Change address " << m_currency.accountAddressAsString(changeDestination);

	List<WalletOuts> wallets = new List<WalletOuts>();
	if (sendingTransaction.sourceAddresses.Count > 0)
	{
	  wallets = new List<WalletOuts>(pickWallets(sendingTransaction.sourceAddresses));
	}
	else
	{
	  wallets = new List<WalletOuts>(pickWalletsWithMoney());
	}

	PreparedTransaction preparedTransaction = new PreparedTransaction();
	prepareTransaction(std::move(wallets), sendingTransaction.destinations, sendingTransaction.fee, sendingTransaction.mixIn, sendingTransaction.extra, sendingTransaction.unlockTimestamp, sendingTransaction.donation, changeDestination, preparedTransaction);

	id = validateSaveAndSendTransaction(preparedTransaction.transaction, preparedTransaction.destinations, false, false);
	return id;
  }
  public override void commitTransaction(uint transactionId)
  {
	System.EventLock lk = new System.EventLock(m_readyEvent);

	throwIfNotInitialized();
	throwIfStopped();
	throwIfTrackingMode();

	if (transactionId >= m_transactions.size())
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to commit transaction: invalid index " << (int)transactionId << ". Number of transactions: " << m_transactions.size();
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.INDEX_OUT_OF_RANGE));
	}

	var txIt = std::next(m_transactions.get<RandomAccessIndex>().begin(), transactionId);
	if (m_uncommitedTransactions.count(transactionId) == 0 || txIt.state != WalletTransactionState.CREATED)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to commit transaction: bad transaction state. Transaction index " << (int)transactionId << ", state " << txIt.state;
	  throw std::system_error(GlobalMembers.make_error_code(error.TX_TRANSFER_IMPOSSIBLE));
	}

	System.Event completion = new System.Event(m_dispatcher);
	std::error_code ec = new std::error_code();

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: System::RemoteContext relayTransactionContext(m_dispatcher, [this, transactionId, &ec, &completion]()
	System.RemoteContext relayTransactionContext(m_dispatcher, () =>
	{
	  m_node.relayTransaction(m_uncommitedTransactions[transactionId], (std::error_code error) =>
	  {
		ec.CopyFrom(error);
		this.m_dispatcher.remoteSpawn(std::bind(asyncRequestCompletion, std::@ref(completion)));
	  });
	});
	relayTransactionContext.get();
	completion.wait();

	if (ec == null)
	{
	  updateTransactionStateAndPushEvent(transactionId, WalletTransactionState.SUCCEEDED);
	  m_uncommitedTransactions.erase(transactionId);
	}
	else
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to relay transaction: " << ec << ", " << ec.message() << ". Transaction index " << (int)transactionId;
	  throw std::system_error(ec);
	}

	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Delayed transaction sent, ID " << (int)transactionId << ", hash " << m_transactions[transactionId].hash;
  }
  public override void rollbackUncommitedTransaction(uint transactionId)
  {
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: Tools::ScopeExit releaseContext([this]
	Tools.ScopeExit releaseContext(() =>
	{
	  m_dispatcher.yield();
	});

	System.EventLock lk = new System.EventLock(m_readyEvent);

	throwIfNotInitialized();
	throwIfStopped();
	throwIfTrackingMode();

	if (transactionId >= m_transactions.size())
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to rollback transaction: invalid index " << (int)transactionId << ". Number of transactions: " << m_transactions.size();
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.INDEX_OUT_OF_RANGE));
	}

	var txIt = m_transactions.get<RandomAccessIndex>().begin();
	std::advance(txIt, transactionId);
	if (m_uncommitedTransactions.count(transactionId) == 0 || txIt.state != WalletTransactionState.CREATED)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to rollback transaction: bad transaction state. Transaction index " << (int)transactionId << ", state " << txIt.state;
	  throw std::system_error(GlobalMembers.make_error_code(error.TX_CANCEL_IMPOSSIBLE));
	}

	removeUnconfirmedTransaction(CryptoNote.GlobalMembers.getObjectHash(m_uncommitedTransactions[transactionId]));
	m_uncommitedTransactions.erase(transactionId);

	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Delayed transaction rolled back, ID " << (int)transactionId << ", hash " << m_transactions[transactionId].hash;
  }

  public uint transfer(PreparedTransaction preparedTransaction)
  {
	uint id = GlobalMembers.WALLET_INVALID_TRANSACTION_ID;
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: Tools::ScopeExit releaseContext([this, &id]
	Tools.ScopeExit releaseContext(() =>
	{
	  m_dispatcher.yield();

	  if (id != WALLET_INVALID_TRANSACTION_ID)
	  {
		auto tx = m_transactions[id];
		m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Transaction created and send, ID " << (int)id << ", hash " << tx.hash << ", state " << tx.state << ", totalAmount " << m_currency.formatAmount(tx.totalAmount) << ", fee " << m_currency.formatAmount(tx.fee) << ", transfers: " << new TransferListFormatter(m_currency, getTransactionTransfersRange(id));
	  }
	});

	System.EventLock lk = new System.EventLock(m_readyEvent);

	throwIfNotInitialized();
	throwIfTrackingMode();
	throwIfStopped();

	id = validateSaveAndSendTransaction(preparedTransaction.transaction, preparedTransaction.destinations, false, true);
	return id;
  }
  public bool txIsTooLarge(PreparedTransaction p)
  {
	return getTxSize(p) > getMaxTxSize();
  }
  public uint getTxSize(PreparedTransaction p)
  {
	return p.transaction.getTransactionData().size();
  }

  /* The formula for the block size is as follows. Calculate the
     maxBlockCumulativeSize. This is equal to:
     100,000 + ((height * 102,400) / 1,051,200)
     At a block height of 400k, this gives us a size of 138,964.
     The constants this calculation arise from can be seen below, or in
     src/CryptoNoteCore/Currency.cpp::maxBlockCumulativeSize(). Call this value
     x.
  
     Next, calculate the median size of the last 100 blocks. Take the max of
     this value, and 100,000. Multiply this value by 1.25. Call this value y.
  
     Finally, return the minimum of x and y.
  
     Or, in short: min(140k (slowly rising), 1.25 * max(100k, median(last 100 blocks size)))
     Block size will always be 125k or greater (Assuming non testnet)
  
     To get the max transaction size, remove 600 from this value, for the
     reserved miner transaction.
  
     We are going to ignore the median(last 100 blocks size), as it is possible
     for a transaction to be valid for inclusion in a block when it is submitted,
     but not when it actually comes to be mined, for example if the median
     block size suddenly decreases. This gives a bit of a lower cap of max
     tx sizes, but prevents anything getting stuck in the pool.
  
  */
  public uint getMaxTxSize()
  {
	  uint currentHeight = m_node.getLastKnownBlockHeight();

	  uint growth = (currentHeight * CryptoNote.parameters.MAX_BLOCK_SIZE_GROWTH_SPEED_NUMERATOR) / CryptoNote.parameters.MAX_BLOCK_SIZE_GROWTH_SPEED_DENOMINATOR;

	  uint x = CryptoNote.parameters.MAX_BLOCK_SIZE_INITIAL + growth;

	  uint y = 125000;

	  return Math.Min(x, y) - CryptoNote.parameters.CRYPTONOTE_COINBASE_BLOB_RESERVED_SIZE;
  }
  public PreparedTransaction formTransaction(TransactionParameters sendingTransaction)
  {
	System.EventLock lk = new System.EventLock(m_readyEvent);

	throwIfNotInitialized();
	throwIfTrackingMode();
	throwIfStopped();

	CryptoNote.AccountPublicAddress changeDestination = getChangeDestination(sendingTransaction.changeDestination, sendingTransaction.sourceAddresses);

	List<WalletOuts> wallets = new List<WalletOuts>();
	if (sendingTransaction.sourceAddresses.Count > 0)
	{
	  wallets = new List<WalletOuts>(pickWallets(sendingTransaction.sourceAddresses));
	}
	else
	{
	  wallets = new List<WalletOuts>(pickWalletsWithMoney());
	}

	PreparedTransaction preparedTransaction = new PreparedTransaction();
	prepareTransaction(std::move(wallets), sendingTransaction.destinations, sendingTransaction.fee, sendingTransaction.mixIn, sendingTransaction.extra, sendingTransaction.unlockTimestamp, sendingTransaction.donation, changeDestination, preparedTransaction);

	return preparedTransaction;
  }

  /* The blockchain events are sent to us from the blockchain synchronizer,
     but they appear to not get executed on the dispatcher until the synchronizer
     stops. After some investigation, it appears that we need to run this
     odd line of code to run other code on the dispatcher? */
  public void updateInternalCache()
  {
	  System.RemoteContext updateInternalBC(m_dispatcher, () =>
	  {
	  });
	  updateInternalBC.get();
  }
  public void clearCaches(bool clearTransactions, bool clearCachedData)
  {
	if (clearTransactions)
	{
	  m_transactions.clear();
	  m_transfers.clear();
	}

	if (clearCachedData)
	{
	  uint walletIndex = 0;
	  for (var it = m_walletsContainer.begin(); it != m_walletsContainer.end(); ++it)
	  {
		m_walletsContainer.modify(it, (WalletRecord wallet) =>
		{
		  wallet.actualBalance = 0;
		  wallet.pendingBalance = 0;
		  wallet.container = reinterpret_cast<CryptoNote.ITransfersContainer>(walletIndex++); //dirty hack. container field must be unique
		});
	  }

	  if (!clearTransactions)
	  {
		for (var it = m_transactions.begin(); it != m_transactions.end(); ++it)
		{
		  m_transactions.modify(it, (WalletTransaction tx) =>
		  {
			tx.state = WalletTransactionState.CANCELLED;
			tx.blockHeight = WALLET_UNCONFIRMED_TRANSACTION_HEIGHT;
		  });
		}
	  }

	  List<AccountPublicAddress> subscriptions = new List<AccountPublicAddress>();
	  m_synchronizer.getSubscriptions(subscriptions);
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: std::for_each(subscriptions.begin(), subscriptions.end(), [this](const AccountPublicAddress& address)
	  subscriptions.ForEach((AccountPublicAddress address) =>
	  {
		  m_synchronizer.removeSubscription(address);
	  });

	  m_uncommitedTransactions.clear();
	  m_unlockTransactionsJob.clear();
	  m_actualBalance = 0;
	  m_pendingBalance = 0;
	  m_fusionTxsCache.Clear();
	  m_blockchain.clear();
	}
  }
  public void clearCacheAndShutdown()
  {
	if (m_walletsContainer.size() != 0)
	{
	  m_synchronizer.unsubscribeConsumerNotifications(m_viewPublicKey, this);
	}

	stopBlockchainSynchronizer();
	m_blockchainSynchronizer.removeObserver(this);

	clearCaches(true, true);

	saveWalletCache(m_containerStorage, m_key, WalletSaveLevel.SAVE_ALL, "");

	m_walletsContainer.clear();

	shutdown();
  }
  public void createViewWallet(string path, string password, string address, Crypto.SecretKey viewSecretKey, ulong scanHeight, bool newAddress)
  {
	  CryptoNote.AccountPublicAddress publicKeys = new CryptoNote.AccountPublicAddress();
	  ulong prefix;

	  if (!CryptoNote.parseAccountAddressString(prefix, publicKeys, address))
	  {
		  throw new System.Exception("Failed to parse address!");
	  }

	  initializeWithViewKey(path, password, viewSecretKey, scanHeight, newAddress);

	  createAddress(publicKeys.spendPublicKey, scanHeight, newAddress);
  }
  public ulong getBalanceMinusDust(List<string> addresses)
  {
	  List<WalletOuts> wallets = pickWallets(addresses);
	  List<OutputToTransfer> unused = new List<OutputToTransfer>();

	  /* We want to get the full balance, so don't stop getting outputs early */
	  ulong needed = ulong.MaxValue;

	  return selectTransfers(needed, false, m_currency.defaultDustThreshold(m_node.getLastKnownBlockHeight()), std::move(wallets), unused);
  }

  public override void start()
  {
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Starting container";
	m_stopped = false;
  }
  public override void stop()
  {
	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Stopping container";
	m_stopped = true;
	m_eventOccurred.set();
  }
  public override WalletEvent getEvent()
  {
	throwIfNotInitialized();
	throwIfStopped();

	while (m_events.Count == 0)
	{
	  m_eventOccurred.wait();
	  m_eventOccurred.clear();
	  throwIfStopped();
	}

	WalletEvent event = std::move(m_events.Peek());
	m_events.Dequeue();

	return event;
  }

  public override uint createFusionTransaction(ulong threshold, ushort mixin, List<string> sourceAddresses = {}, string destinationAddress = "")
  {

	uint id = GlobalMembers.WALLET_INVALID_TRANSACTION_ID;
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: Tools::ScopeExit releaseContext([this, &id]
	Tools.ScopeExit releaseContext(() =>
	{
	  m_dispatcher.yield();

	  if (id != WALLET_INVALID_TRANSACTION_ID)
	  {
		auto tx = m_transactions[id];
		m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Fusion transaction created and sent, ID " << (int)id << ", hash " << m_transactions[id].hash << ", state " << tx.state << ", transfers: " << new TransferListFormatter(m_currency, getTransactionTransfersRange(id));
	  }
	});

	System.EventLock lk = new System.EventLock(m_readyEvent);

	m_logger.functorMethod(INFO, BRIGHT_WHITE) << "createFusionTransaction" << ", from " << Common.GlobalMembers.makeContainerFormatter(sourceAddresses) << ", to '" << destinationAddress << '\'' << ", threshold " << m_currency.formatAmount(threshold) << ", mixin " << mixin;

	throwIfNotInitialized();
	throwIfTrackingMode();
	throwIfStopped();

	validateSourceAddresses(sourceAddresses);
	validateChangeDestination(sourceAddresses, destinationAddress, true);

	const uint MAX_FUSION_OUTPUT_COUNT = 4;

	uint height = m_node.getLastKnownBlockHeight();

	if (threshold <= m_currency.defaultFusionDustThreshold(height))
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Fusion transaction threshold is too small. Threshold " << m_currency.formatAmount(threshold) << ", minimum threshold " << m_currency.formatAmount(m_currency.defaultFusionDustThreshold(height) + 1);
	  throw new System.Exception("Threshold must be greater than " + m_currency.formatAmount(m_currency.defaultFusionDustThreshold(height)));
	}

	if (m_walletsContainer.get<RandomAccessIndex>().size() == 0)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "The container doesn't have any wallets";
	  throw new System.Exception("You must have at least one address");
	}

	uint estimatedFusionInputsCount = m_currency.getApproximateMaximumInputCount(m_currency.fusionTxMaxSize(), MAX_FUSION_OUTPUT_COUNT, mixin);
	if (estimatedFusionInputsCount < m_currency.fusionTxMinInputCount())
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Fusion transaction mixin is too big " << mixin;
	  throw std::system_error(GlobalMembers.make_error_code(error.MIXIN_COUNT_TOO_BIG));
	}

	var fusionInputs = pickRandomFusionInputs(sourceAddresses, threshold, m_currency.fusionTxMinInputCount(), estimatedFusionInputsCount);
	if (fusionInputs.Count < m_currency.fusionTxMinInputCount())
	{
	  //nothing to optimize
	  m_logger.functorMethod(WARNING, BRIGHT_YELLOW) << "Fusion transaction not created: nothing to optimize, threshold " << m_currency.formatAmount(threshold);
	  return GlobalMembers.WALLET_INVALID_TRANSACTION_ID;
	}

  //C++ TO C# CONVERTER TODO TASK: The typedef 'outs_for_amount' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	List<outs_for_amount> mixinResult = new List<outs_for_amount>();
	if (mixin != 0)
	{
	  requestMixinOuts(fusionInputs, mixin, mixinResult);
	}

	List<InputInfo> keysInfo = new List<InputInfo>();
	prepareInputs(fusionInputs, mixinResult, mixin, keysInfo);

	AccountPublicAddress destination = getChangeDestination(destinationAddress, sourceAddresses);
	m_logger.functorMethod(DEBUGGING) << "Destination address " << m_currency.accountAddressAsString(destination);

	std::unique_ptr<ITransaction> fusionTransaction = new std::unique_ptr<ITransaction>();
	uint transactionSize;
	int round = 0;
	ulong transactionAmount = 0;
	if (transactionAmount != 0)
	{
	}
	do
	{
	  if (round != 0)
	  {
		fusionInputs.RemoveAt(fusionInputs.Count - 1);
		keysInfo.RemoveAt(keysInfo.Count - 1);
	  }

	  ulong inputsAmount = std::accumulate(fusionInputs.GetEnumerator(), fusionInputs.end(), (ulong)0, (ulong amount, OutputToTransfer input) =>
	  {
		return amount + input.@out.amount;
	  });

	  transactionAmount = inputsAmount;

	  ReceiverAmounts decomposedOutputs = decomposeFusionOutputs(destination, inputsAmount);
	  Debug.Assert(decomposedOutputs.amounts.Count <= MAX_FUSION_OUTPUT_COUNT);

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: fusionTransaction = makeTransaction(ClassicVector<ReceiverAmounts>({decomposedOutputs}), keysInfo, "", 0);
	  fusionTransaction.CopyFrom(makeTransaction(new List<ReceiverAmounts>() {decomposedOutputs}, keysInfo, "", 0));

	  transactionSize = GlobalMembers.getTransactionSize(*fusionTransaction);

	  ++round;
	} while (transactionSize > m_currency.fusionTxMaxSize() && fusionInputs.Count >= m_currency.fusionTxMinInputCount());

	if (fusionInputs.Count < m_currency.fusionTxMinInputCount())
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Unable to create fusion transaction";
	  throw new System.Exception("Unable to create fusion transaction");
	}

	id = validateSaveAndSendTransaction(*fusionTransaction, new List<WalletTransfer>(), true, true);
	return id;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool isFusionTransaction(uint transactionId) const override
  public override bool isFusionTransaction(uint transactionId)
  {
	throwIfNotInitialized();
	throwIfStopped();

	if (m_transactions.size() <= transactionId)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to check transaction: invalid index " << (int)transactionId << ". Number of transactions: " << m_transactions.size();
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.INDEX_OUT_OF_RANGE));
	}

	var isFusionIter = m_fusionTxsCache.find(transactionId);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	if (isFusionIter != m_fusionTxsCache.end())
	{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  return isFusionIter.second;
	}

	bool result = isFusionTransaction(m_transactions.get<RandomAccessIndex>()[transactionId]);
	m_fusionTxsCache.Add(transactionId, result);
	return result;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual IFusionManager::EstimateResult estimate(ulong threshold, const ClassicVector<string>& sourceAddresses = {}) const override
  public override IFusionManager.EstimateResult estimate(ulong threshold, List<string> sourceAddresses = {})
  {
	System.EventLock lk = new System.EventLock(m_readyEvent);

	throwIfNotInitialized();
	throwIfStopped();

	validateSourceAddresses(sourceAddresses);

	IFusionManager.EstimateResult result = new EstimateResult(0, 0);
	var walletOuts = sourceAddresses.Count == 0 ? pickWalletsWithMoney() : pickWallets(sourceAddresses);
	List<uint> bucketSizes = new List<uint>(numeric_limits<ulong>.digits10 + 1);
	bucketSizes.fill(0);
	for (uint walletIndex = 0; walletIndex < walletOuts.Count; ++walletIndex)
	{
	  foreach (var @out in walletOuts[walletIndex].outs)
	  {
		byte powerOfTen = 0;
		if (m_currency.isAmountApplicableInFusionTransactionInput(@out.amount, threshold, ref powerOfTen, m_node.getLastKnownBlockHeight()))
		{
		  Debug.Assert(powerOfTen < numeric_limits<ulong>.digits10 + 1);
		  bucketSizes[powerOfTen]++;
		}
	  }

	  result.totalOutputCount += walletOuts[walletIndex].outs.Count;
	}

	foreach (var bucketSize in bucketSizes)
	{
	  if (bucketSize >= m_currency.fusionTxMinInputCount())
	  {
		result.fusionReadyCount += bucketSize;
	  }
	}

	return result;
  }

  protected class NewAddressData
  {
	public Crypto.PublicKey spendPublicKey = new Crypto.PublicKey();
	public Crypto.SecretKey spendSecretKey = new Crypto.SecretKey();
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void throwIfNotInitialized() const
  protected void throwIfNotInitialized()
  {
	if (m_state != WalletState.INITIALIZED)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "WalletGreen is not initialized. Current state: " << m_state;
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.NOT_INITIALIZED));
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void throwIfStopped() const
  protected void throwIfStopped()
  {
	if (m_stopped)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "WalletGreen is already stopped";
	  throw std::system_error(GlobalMembers.make_error_code(error.OPERATION_CANCELLED));
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void throwIfTrackingMode() const
  protected void throwIfTrackingMode()
  {
	if (getTrackingMode() == WalletTrackingMode.TRACKING)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "WalletGreen is in tracking mode";
	  throw std::system_error(GlobalMembers.make_error_code(error.TRACKING_MODE));
	}
  }
  protected void doShutdown()
  {
	if (m_walletsContainer.size() != 0)
	{
	  m_synchronizer.unsubscribeConsumerNotifications(m_viewPublicKey, this);
	}

	stopBlockchainSynchronizer();
	m_blockchainSynchronizer.removeObserver(this);

	m_containerStorage.close();
	m_walletsContainer.clear();

	clearCaches(true, true);

	Queue<WalletEvent> noEvents = new Queue<WalletEvent>();
	std::swap(m_events, noEvents);

	m_state = WalletState.NOT_INITIALIZED;
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void convertAndLoadWalletFile(string path, std::ifstream&& walletFileStream);
  protected static void decryptKeyPair(EncryptedWalletRecord cipher, PublicKey publicKey, SecretKey secretKey, ref ulong creationTimestamp, Crypto.chacha8_key key)
  {

	List<char> buffer = new List<char>(sizeof(cipher.data));
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: chacha8(cipher.data, sizeof(cipher.data), key, cipher.iv, buffer.data());
	Crypto.GlobalMembers.chacha8(cipher.data, sizeof(cipher.data), new Crypto.chacha8_key(key), new Crypto.chacha8_iv(cipher.iv), ref buffer.data());

	MemoryInputStream stream = new MemoryInputStream(buffer.data(), buffer.Count);
	BinaryInputStreamSerializer serializer = new BinaryInputStreamSerializer(stream);

	serializer.functorMethod(publicKey, "publicKey");
	serializer.functorMethod(secretKey, "secretKey");
	serializer.binary(creationTimestamp, sizeof(ulong), "creationTimestamp");
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void decryptKeyPair(const EncryptedWalletRecord& cipher, PublicKey& publicKey, SecretKey& secretKey, ulong& creationTimestamp) const
  protected void decryptKeyPair(EncryptedWalletRecord cipher, PublicKey publicKey, SecretKey secretKey, ref ulong creationTimestamp)
  {
	decryptKeyPair(cipher, publicKey, secretKey, ref creationTimestamp, m_key);
  }
  protected static EncryptedWalletRecord encryptKeyPair(PublicKey publicKey, SecretKey secretKey, ulong creationTimestamp, Crypto.chacha8_key key, Crypto.chacha8_iv iv)
  {

	EncryptedWalletRecord result = new EncryptedWalletRecord();

	string serializedKeys;
	StringOutputStream outputStream = new StringOutputStream(serializedKeys);
	BinaryOutputStreamSerializer serializer = new BinaryOutputStreamSerializer(outputStream);

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
	serializer.functorMethod(const_cast<PublicKey&>(publicKey), "publicKey");
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
	serializer.functorMethod(const_cast<SecretKey&>(secretKey), "secretKey");
	serializer.binary(creationTimestamp, sizeof(ulong), "creationTimestamp");

	Debug.Assert(serializedKeys.Length == sizeof(result.data));

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: result.iv = iv;
	result.iv.CopyFrom(iv);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: chacha8(serializedKeys.data(), serializedKeys.size(), key, result.iv, reinterpret_cast<char*>(result.data));
	Crypto.GlobalMembers.chacha8(serializedKeys.data(), serializedKeys.Length, new Crypto.chacha8_key(key), new Crypto.chacha8_iv(result.iv), ref reinterpret_cast<char>(result.data));

	return result;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: EncryptedWalletRecord encryptKeyPair(const PublicKey& publicKey, const SecretKey& secretKey, ulong creationTimestamp) const
  protected EncryptedWalletRecord encryptKeyPair(PublicKey publicKey, SecretKey secretKey, ulong creationTimestamp)
  {
	return encryptKeyPair(publicKey, secretKey, creationTimestamp, m_key, getNextIv());
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Crypto::chacha8_iv getNextIv() const
  protected Crypto.chacha8_iv getNextIv()
  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	auto prefix = reinterpret_cast<const ContainerStoragePrefix>(m_containerStorage.prefix());
	return prefix.nextIv;
  }
  protected static void incIv(Crypto.chacha8_iv iv)
  {
  //C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
  //  static_assert(sizeof(ulong) == sizeof(Crypto::chacha8_iv), "Bad Crypto::chacha8_iv size");
//C++ TO C# CONVERTER TODO TASK: C# does not have an equivalent to pointers to value types:
//ORIGINAL LINE: ulong* i = reinterpret_cast<ulong*>(&iv);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	ulong i = reinterpret_cast<ulong>(iv);
	if (i < ulong.MaxValue)
	{
	  ++i;
	}
	else
	{
	  i = null;
	}
  }
  protected void incNextIv()
  {
  //C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
  //  static_assert(sizeof(ulong) == sizeof(Crypto::chacha8_iv), "Bad Crypto::chacha8_iv size");
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	auto prefix = reinterpret_cast<ContainerStoragePrefix>(m_containerStorage.prefix());
	incIv(prefix.nextIv);
  }
  protected void initWithKeys(string path, string password, Crypto.PublicKey viewPublicKey, Crypto.SecretKey viewSecretKey, ulong scanHeight, bool newAddress)
  {

	if (m_state != WalletState.NOT_INITIALIZED)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to initialize with keys: already initialized. Current state: " << m_state;
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.ALREADY_INITIALIZED));
	}

	throwIfStopped();

	Common.FileMappedVector<EncryptedWalletRecord> newStorage = new Common.FileMappedVector<EncryptedWalletRecord>(path, Common.FileMappedVectorOpenMode.CREATE, sizeof(ContainerStoragePrefix));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	ContainerStoragePrefix prefix = reinterpret_cast<ContainerStoragePrefix>(newStorage.prefix());
	prefix.version = (byte)WalletSerializerV2.SERIALIZATION_VERSION;
	prefix.nextIv = Crypto.GlobalMembers.rand<Crypto.chacha8_iv>();

	Crypto.GlobalMembers.generate_chacha8_key(password, m_key);

	ulong creationTimestamp;

	if (newAddress)
	{
	  creationTimestamp = getCurrentTimestampAdjusted();
	}
	else
	{
	  creationTimestamp = scanHeightToTimestamp(scanHeight);
	}

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: prefix->encryptedViewKeys = encryptKeyPair(viewPublicKey, viewSecretKey, creationTimestamp, m_key, prefix->nextIv);
	prefix.encryptedViewKeys.CopyFrom(encryptKeyPair(viewPublicKey, viewSecretKey, creationTimestamp, m_key, prefix.nextIv));

	newStorage.flush();
	m_containerStorage.swap(newStorage);
	incNextIv();

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_viewPublicKey = viewPublicKey;
	m_viewPublicKey.CopyFrom(viewPublicKey);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_viewSecretKey = viewSecretKey;
	m_viewSecretKey.CopyFrom(viewSecretKey);
	m_password = password;
	m_path = path;
	m_logger = new Logging.LoggerRef(m_logger.getLogger(), "WalletGreen/" + Common.GlobalMembers.podToHex(m_viewPublicKey).Substring(0, 5));

	Debug.Assert(m_blockchain.empty());
	m_blockchain.push_back(m_currency.genesisBlockHash());

	m_blockchainSynchronizer.addObserver(this);

	m_state = WalletState.INITIALIZED;
  }
  protected string doCreateAddress(Crypto.PublicKey spendPublicKey, Crypto.SecretKey spendSecretKey, ulong scanHeight, bool newAddress)
  {
	  List<NewAddressData> addressDataList = new List<NewAddressData>();

	  addressDataList.Add(new NewAddressData({spendPublicKey, spendSecretKey}));

	  List<string> addresses = doCreateAddressList(addressDataList, scanHeight, newAddress);

	  Debug.Assert(addresses.Count == 1);

	  return addresses[0];
  }
  protected List<string> doCreateAddressList(List<NewAddressData> addressDataList, ulong scanHeight, bool newAddress)
  {
	throwIfNotInitialized();
	throwIfStopped();

	stopBlockchainSynchronizer();

	List<string> addresses = new List<string>();

	bool resetRequired = false;

	auto walletsIndex = m_walletsContainer.get<RandomAccessIndex>();

	/* If there are already existing wallets, we need to check their creation
	   timestamps. If their creation timestamps are greater than the timestamp
	   of the wallet we are currently adding, we will have to rescan from this
	   lower height to get the blocks we need. */
	if (!walletsIndex.empty() && !newAddress)
	{
		ulong timestamp = scanHeightToTimestamp(scanHeight);

		DateTime minTimestamp = DateTime.MaxValue;

		foreach (WalletRecord wallet in walletsIndex)
		{
			if (wallet.creationTimestamp < minTimestamp)
			{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: minTimestamp = wallet.creationTimestamp;
				minTimestamp.CopyFrom(wallet.creationTimestamp);
			}
		}

		if (timestamp < (ulong)minTimestamp)
		{
			resetRequired = true;
		}
	}

	try
	{
	  {
		if (addressDataList.Count > 1)
		{
		  m_containerStorage.setAutoFlush(false);
		}

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: Tools::ScopeExit exitHandler([this]
		Tools.ScopeExit exitHandler(() =>
		{
		  if (!m_containerStorage.getAutoFlush())
		  {
			m_containerStorage.setAutoFlush(true);
			m_containerStorage.flush();
		  }
		});

		foreach (var addressData in addressDataList)
		{
		  string address = addWallet(addressData, scanHeight, newAddress);

		  m_logger.functorMethod(INFO, BRIGHT_WHITE) << "New wallet added " << address;

		  addresses.Add(std::move(address));
		}
	  }

	  m_containerStorage.setAutoFlush(true);

	  if (resetRequired)
	  {
		m_logger.functorMethod(DEBUGGING) << "A reset is required to scan from this new lower " << "block height" << std::endl;

		save(WalletSaveLevel.SAVE_KEYS_AND_TRANSACTIONS, m_extra);
		shutdown();
		load(m_path, m_password);
	  }
	}
	catch (System.Exception e)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to add wallets: " << e.Message;
	  startBlockchainSynchronizer();
	  throw;
	}

	startBlockchainSynchronizer();

	return addresses;
  }

  protected CryptoNote.BlockDetails getBlock(ulong blockHeight)
  {
	  CryptoNote.BlockDetails block = new CryptoNote.BlockDetails();

	  if (m_node.getLastKnownBlockHeight() == 0)
	  {
		  return block;
	  }

	  std::promise<std::error_code> errorPromise = new std::promise<std::error_code>();

	  var e = errorPromise.get_future();

//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
	  var callback = (std::error_code e) =>
	  {
		  errorPromise.set_value(e);
	  };

	  m_node.getBlock(blockHeight, block, callback);

	  e.get();

	  return block;
  }

  protected ulong scanHeightToTimestamp(ulong scanHeight)
  {
	  if (scanHeight == 0)
	  {
		  return 0;
	  }

	  /* Get the block timestamp from the node if the node has it */
	  ulong timestamp = (ulong)(getBlock(scanHeight).timestamp);

	  if (timestamp != 0)
	  {
		  return timestamp;
	  }

	  /* Get the amount of seconds since the blockchain launched */
	  ulong secondsSinceLaunch = scanHeight * CryptoNote.parameters.DIFFICULTY_TARGET;

	  /* Add a bit of a buffer in case of difficulty weirdness, blocks coming
	     out too fast */
	  secondsSinceLaunch *= 0.95;

	  /* Get the genesis block timestamp and add the time since launch */
	  timestamp = CryptoNote.parameters.GENESIS_BLOCK_TIMESTAMP + secondsSinceLaunch;

	  /* Timestamp in the future */
	  if (timestamp >= (ulong)std::time(null))
	  {
		  return getCurrentTimestampAdjusted();
	  }

	  return timestamp;
  }

  protected ulong getCurrentTimestampAdjusted()
  {
	  /* Get the current time as a unix timestamp */
	  std::DateTime time = std::time(null);

	  /* Take the amount of time a block can potentially be in the past/future */
	  std::initializer_list<ulong> limits = new std::initializer_list<ulong>(CryptoNote.parameters.CRYPTONOTE_BLOCK_FUTURE_TIME_LIMIT, CryptoNote.parameters.CRYPTONOTE_BLOCK_FUTURE_TIME_LIMIT_V3, CryptoNote.parameters.CRYPTONOTE_BLOCK_FUTURE_TIME_LIMIT_V4);

	  /* Get the largest adjustment possible */
	  ulong adjust = std::max(limits);

	  /* Take the earliest timestamp that will include all possible blocks */
	  return time - adjust;
  }

  protected class InputInfo
  {
	public TransactionTypes.InputKeyInfo keyInfo = new TransactionTypes.InputKeyInfo();
	public WalletRecord walletRecord = null;
	public KeyPair ephKeys = new KeyPair();
  }

  protected class OutputToTransfer
  {
	public TransactionOutputInformation @out = new TransactionOutputInformation();
	public WalletRecord wallet;
  }

  protected class ReceiverAmounts
  {
	public CryptoNote.AccountPublicAddress receiver = new CryptoNote.AccountPublicAddress();
	public List<ulong> amounts = new List<ulong>();
  }

  protected class WalletOuts
  {
	public WalletRecord wallet;
	public List<TransactionOutputInformation> outs = new List<TransactionOutputInformation>();
  }


  protected class AddressAmounts
  {
	public long input = 0;
	public long output = 0;
  }

  protected class ContainerAmounts
  {
	public ITransfersContainer container;
	public AddressAmounts amounts = new AddressAmounts();
  }

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
//#pragma pack(push, 1)
  protected class ContainerStoragePrefix
  {
	public byte version;
	public Crypto.chacha8_iv nextIv = new Crypto.chacha8_iv();
	public EncryptedWalletRecord encryptedViewKeys = new EncryptedWalletRecord();
  }
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
//#pragma pack(pop)


  protected override void onError(ITransfersSubscription @object, uint height, std::error_code ec)
  {
	m_logger.functorMethod(ERROR, BRIGHT_RED) << "Synchronization error: " << ec << ", " << ec.message() << ", height " << (int)height;
  }

  protected override void onTransactionUpdated(ITransfersSubscription @object, Crypto.Hash transactionHash)
  {
	// Deprecated, ignore it. New event handler is onTransactionUpdated(const Crypto::PublicKey&, const Crypto::Hash&, const std::vector<ITransfersContainer*>&)
  }
  protected override void onTransactionUpdated(Crypto.PublicKey viewPublicKey, Crypto.Hash transactionHash, List<ITransfersContainer> containers)
  {
	Debug.Assert(containers.Count > 0);

	TransactionInformation info = new TransactionInformation();
	List<ContainerAmounts> containerAmountsList = new List<ContainerAmounts>();
	containerAmountsList.Capacity = containers.Count;
	foreach (var container in containers)
	{
	  ulong inputsAmount;
	  // Don't move this code to the following remote spawn, because it guarantees that the container has the transaction
	  ulong outputsAmount;
	  bool found = container.getTransactionInformation(transactionHash, info, ref inputsAmount, ref outputsAmount);
	  if (found)
	  {
	  }
	  Debug.Assert(found);

	  ContainerAmounts containerAmounts = new ContainerAmounts();
	  containerAmounts.container = container;
	  containerAmounts.amounts.input = -(long)inputsAmount;
	  containerAmounts.amounts.output = (long)outputsAmount;
	  containerAmountsList.emplace_back(std::move(containerAmounts));
	}

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: m_dispatcher.remoteSpawn([this, info, containerAmountsList]
	m_dispatcher.remoteSpawn(() =>
	{
	  this.transactionUpdated(info, containerAmountsList);
	});
  }
  protected void transactionUpdated(TransactionInformation transactionInfo, List<ContainerAmounts> containerAmountsList)
  {
	System.EventLock lk = new System.EventLock(m_readyEvent);

	m_logger.functorMethod(DEBUGGING) << "transactionUpdated event, hash " << transactionInfo.transactionHash << ", block " << (int)transactionInfo.blockHeight << ", totalAmountIn " << m_currency.formatAmount(transactionInfo.totalAmountIn) << ", totalAmountOut " << m_currency.formatAmount(transactionInfo.totalAmountOut) << (transactionInfo.paymentId == GlobalMembers.NULL_HASH != null ? "" : ", paymentId " + Common.GlobalMembers.podToHex(transactionInfo.paymentId));

	if (m_state == WalletState.NOT_INITIALIZED)
	{
	  return;
	}

	bool updated = false;
	bool isNew = false;

	long totalAmount = std::accumulate(containerAmountsList.GetEnumerator(), containerAmountsList.end(), (long)0, (long sum, ContainerAmounts containerAmounts) =>
	{
		return sum + containerAmounts.amounts.input + containerAmounts.amounts.output;
	});

	uint transactionId;
	auto hashIndex = m_transactions.get<TransactionIndex>();
	var it = hashIndex.find(transactionInfo.transactionHash);
	if (it != hashIndex.end())
	{
	  transactionId = std::distance(m_transactions.get<RandomAccessIndex>().begin(), m_transactions.project<RandomAccessIndex>(it));
	  updated |= updateWalletTransactionInfo(transactionId, transactionInfo, totalAmount);
	}
	else
	{
	  isNew = true;
	  transactionId = insertBlockchainTransaction(transactionInfo, totalAmount);
	  m_fusionTxsCache.Add(transactionId, isFusionTransaction(*it));
	}

	if (transactionInfo.blockHeight != CryptoNote.GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT)
	{
	  // In some cases a transaction can be included to a block but not removed from m_uncommitedTransactions. Fix it
	  m_uncommitedTransactions.erase(transactionId);
	}

	// Update cached balance
	foreach (var containerAmounts in containerAmountsList)
	{
	  updateBalance(containerAmounts.container);

	  if (transactionInfo.blockHeight != CryptoNote.GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT)
	  {
		uint unlockHeight = Math.Max(transactionInfo.blockHeight + m_transactionSoftLockTime, (uint)transactionInfo.unlockTime);
		insertUnlockTransactionJob(transactionInfo.transactionHash, unlockHeight, containerAmounts.container);
	  }
	}

	bool transfersUpdated = updateTransactionTransfers(transactionId, containerAmountsList, -(long)transactionInfo.totalAmountIn, (long)transactionInfo.totalAmountOut);
	updated |= transfersUpdated;

	if (isNew)
	{
	  auto tx = m_transactions[transactionId];
	  m_logger.functorMethod(INFO, BRIGHT_WHITE) << "New transaction received, ID " << (int)transactionId << ", hash " << tx.hash << ", state " << tx.state << ", totalAmount " << m_currency.formatAmount(tx.totalAmount) << ", fee " << m_currency.formatAmount(tx.fee) << ", transfers: " << new TransferListFormatter(m_currency, getTransactionTransfersRange(transactionId));

	  pushEvent(GlobalMembers.makeTransactionCreatedEvent(transactionId));
	}
	else if (updated)
	{
	  if (transfersUpdated)
	  {
		m_logger.functorMethod(DEBUGGING) << "Transaction transfers updated, ID " << (int)transactionId << ", hash " << m_transactions[transactionId].hash << ", transfers: " << new TransferListFormatter(m_currency, getTransactionTransfersRange(transactionId));
	  }

	  pushEvent(GlobalMembers.makeTransactionUpdatedEvent(transactionId));
	}
  }

  protected override void onTransactionDeleted(ITransfersSubscription @object, Hash transactionHash)
  {
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: m_dispatcher.remoteSpawn([object, transactionHash, this]()
	m_dispatcher.remoteSpawn(() =>
	{
	  this.transactionDeleted(@object, transactionHash);
	});
  }
  protected void transactionDeleted(ITransfersSubscription @object, Hash transactionHash)
  {
	System.EventLock lk = new System.EventLock(m_readyEvent);

	m_logger.functorMethod(DEBUGGING) << "transactionDeleted event, hash " << transactionHash;

	if (m_state == WalletState.NOT_INITIALIZED)
	{
	  return;
	}

	var it = m_transactions.get<TransactionIndex>().find(transactionHash);
	if (it == m_transactions.get<TransactionIndex>().end())
	{
	  return;
	}

	CryptoNote.ITransfersContainer container = @object.getContainer();
	updateBalance(container);
	deleteUnlockTransactionJob(transactionHash);

	bool updated = false;
	m_transactions.get<TransactionIndex>().modify(it, (CryptoNote.WalletTransaction tx) =>
	{
	  if (tx.state == WalletTransactionState.CREATED || tx.state == WalletTransactionState.SUCCEEDED)
	  {
		tx.state = WalletTransactionState.CANCELLED;
		updated = true;
	  }

	  if (tx.blockHeight != WALLET_UNCONFIRMED_TRANSACTION_HEIGHT)
	  {
		tx.blockHeight = WALLET_UNCONFIRMED_TRANSACTION_HEIGHT;
		updated = true;
	  }
	});

	if (updated)
	{
	  var transactionId = getTransactionId(transactionHash);
	  var tx = m_transactions[transactionId];
	  m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Transaction deleted, ID " << (int)transactionId << ", hash " << transactionHash << ", state " << tx.state << ", block " << (int)tx.blockHeight << ", totalAmount " << m_currency.formatAmount(tx.totalAmount) << ", fee " << m_currency.formatAmount(tx.fee);
	  pushEvent(GlobalMembers.makeTransactionUpdatedEvent(transactionId));
	}
  }

  protected override void synchronizationProgressUpdated(uint processedBlockCount, uint totalBlockCount)
  {
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: m_dispatcher.remoteSpawn([processedBlockCount, totalBlockCount, this]()
	m_dispatcher.remoteSpawn(() =>
	{
		onSynchronizationProgressUpdated(processedBlockCount, totalBlockCount);
	});
  }
  protected override void synchronizationCompleted(std::error_code result)
  {
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: m_dispatcher.remoteSpawn([this]()
	m_dispatcher.remoteSpawn(() =>
	{
		onSynchronizationCompleted();
	});
  }

  protected void onSynchronizationProgressUpdated(uint processedBlockCount, uint totalBlockCount)
  {
	Debug.Assert(processedBlockCount > 0);

	System.EventLock lk = new System.EventLock(m_readyEvent);

	m_logger.functorMethod(TRACE) << "onSynchronizationProgressUpdated processedBlockCount " << (int)processedBlockCount << ", totalBlockCount " << (int)totalBlockCount;

	if (m_state == WalletState.NOT_INITIALIZED)
	{
	  return;
	}

	pushEvent(GlobalMembers.makeSyncProgressUpdatedEvent(processedBlockCount, totalBlockCount));

	uint currentHeight = processedBlockCount - 1;
	unlockBalances(currentHeight);
  }
  protected void onSynchronizationCompleted()
  {
	System.EventLock lk = new System.EventLock(m_readyEvent);

	m_logger.functorMethod(TRACE) << "onSynchronizationCompleted";

	if (m_state == WalletState.NOT_INITIALIZED)
	{
	  return;
	}

	pushEvent(GlobalMembers.makeSyncCompletedEvent());
  }

  protected override void onBlocksAdded(Crypto.PublicKey viewPublicKey, List<Crypto.Hash> blockHashes)
  {
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: m_dispatcher.remoteSpawn([this, blockHashes]()
	m_dispatcher.remoteSpawn(() =>
	{
		blocksAdded(blockHashes);
	});
  }
  protected void blocksAdded(List<Crypto.Hash> blockHashes)
  {
	System.EventLock lk = new System.EventLock(m_readyEvent);

	if (m_state == WalletState.NOT_INITIALIZED)
	{
	  return;
	}

	m_blockchain.insert(m_blockchain.end(), blockHashes.GetEnumerator(), blockHashes.end());
  }

  protected override void onBlockchainDetach(Crypto.PublicKey viewPublicKey, uint blockIndex)
  {
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: m_dispatcher.remoteSpawn([this, blockIndex]()
	m_dispatcher.remoteSpawn(() =>
	{
		blocksRollback(blockIndex);
	});
  }
  protected void blocksRollback(uint blockIndex)
  {
	System.EventLock lk = new System.EventLock(m_readyEvent);

	m_logger.functorMethod(TRACE) << "blocksRollback " << (int)blockIndex;

	if (m_state == WalletState.NOT_INITIALIZED)
	{
	  return;
	}

	auto blockHeightIndex = m_blockchain.get<BlockHeightIndex>();
	blockHeightIndex.erase(std::next(blockHeightIndex.begin(), blockIndex), blockHeightIndex.end());
  }

  protected override void onTransactionDeleteBegin(Crypto.PublicKey viewPublicKey, Crypto.Hash transactionHash)
  {
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: m_dispatcher.remoteSpawn([=]()
	m_dispatcher.remoteSpawn(() =>
	{
		transactionDeleteBegin(new Crypto.Hash(transactionHash));
	});
  }

  // TODO remove
  protected void transactionDeleteBegin(Crypto.Hash transactionHash)
  {
	m_logger.functorMethod(TRACE) << "transactionDeleteBegin " << transactionHash;
  }

  protected override void onTransactionDeleteEnd(Crypto.PublicKey viewPublicKey, Crypto.Hash transactionHash)
  {
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: m_dispatcher.remoteSpawn([=]()
	m_dispatcher.remoteSpawn(() =>
	{
		transactionDeleteEnd(new Crypto.Hash(transactionHash));
	});
  }

  // TODO remove
  protected void transactionDeleteEnd(Crypto.Hash transactionHash)
  {
	m_logger.functorMethod(TRACE) << "transactionDeleteEnd " << transactionHash;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<WalletGreen::WalletOuts> pickWalletsWithMoney() const
  protected List<WalletGreen.WalletOuts> pickWalletsWithMoney()
  {
	auto walletsIndex = m_walletsContainer.get<RandomAccessIndex>();

	List<WalletOuts> walletOuts = new List<WalletOuts>();
	foreach (var wallet in walletsIndex)
	{
	  if (wallet.actualBalance == 0)
	  {
		continue;
	  }

	  ITransfersContainer container = wallet.container;

	  WalletOuts outs = new WalletOuts();
	  container.getOutputs(outs.outs, ITransfersContainer.IncludeKeyUnlocked);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
	  outs.wallet = const_cast<WalletRecord>(wallet);

	  walletOuts.Add(std::move(outs));
	};

	return walletOuts;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: WalletGreen::WalletOuts pickWallet(const string& address) const
  protected WalletGreen.WalletOuts pickWallet(string address)
  {
	auto wallet = getWalletRecord(address);

	ITransfersContainer container = wallet.container;
	WalletOuts outs = new WalletOuts();
	container.getOutputs(outs.outs, ITransfersContainer.IncludeKeyUnlocked);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
	outs.wallet = const_cast<WalletRecord>(wallet);

	return outs;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<WalletGreen::WalletOuts> pickWallets(const ClassicVector<string>& addresses) const
  protected List<WalletGreen.WalletOuts> pickWallets(List<string> addresses)
  {
	List<WalletOuts> wallets = new List<WalletOuts>();
	wallets.Capacity = addresses.Count;

	foreach (var address in addresses)
	{
	  WalletOuts wallet = pickWallet(address);
	  if (wallet.outs.Count > 0)
	  {
		wallets.emplace_back(std::move(wallet));
	  }
	}

	return wallets;
  }

  protected void updateBalance(CryptoNote.ITransfersContainer container)
  {
	var it = m_walletsContainer.get<TransfersContainerIndex>().find(container);

	if (it == m_walletsContainer.get<TransfersContainerIndex>().end())
	{
	  return;
	}

	ulong actual = container.balance(ITransfersContainer.IncludeAllUnlocked);
	ulong pending = container.balance(ITransfersContainer.IncludeAllLocked);

	bool updated = false;

	if (it.actualBalance < actual)
	{
	  m_actualBalance += actual - it.actualBalance;
	  updated = true;
	}
	else if (it.actualBalance > actual)
	{
	  m_actualBalance -= it.actualBalance - actual;
	  updated = true;
	}

	if (it.pendingBalance < pending)
	{
	  m_pendingBalance += pending - it.pendingBalance;
	  updated = true;
	}
	else if (it.pendingBalance > pending)
	{
	  m_pendingBalance -= it.pendingBalance - pending;
	  updated = true;
	}

	if (updated)
	{
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: m_walletsContainer.get<TransfersContainerIndex>().modify(it, [actual, pending](WalletRecord& wallet)
	  m_walletsContainer.get<TransfersContainerIndex>().modify(it, (WalletRecord wallet) =>
	  {
		wallet.actualBalance = actual;
		wallet.pendingBalance = pending;
	  });

	  m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Wallet balance updated, address " << m_currency.accountAddressAsString(new AccountBase(it.spendPublicKey, m_viewPublicKey)) << ", actual " << m_currency.formatAmount(it.actualBalance) << ", pending " << m_currency.formatAmount(it.pendingBalance);
	  m_logger.functorMethod(INFO, BRIGHT_WHITE) << "Container balance updated, actual " << m_currency.formatAmount(m_actualBalance) << ", pending " << m_currency.formatAmount(m_pendingBalance);
	}
  }
  protected void unlockBalances(uint height)
  {
	auto index = m_unlockTransactionsJob.get<BlockHeightIndex>();
	var upper = index.upper_bound(height);

	if (index.begin() != upper)
	{
	  for (var it = index.begin(); it != upper; ++it)
	  {
		updateBalance(it.container);
	  }

	  index.erase(index.begin(), upper);
	  pushEvent(GlobalMembers.makeMoneyUnlockedEvent());
	}
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const WalletRecord& getWalletRecord(const PublicKey& key) const
  protected WalletRecord getWalletRecord(PublicKey key)
  {
	var it = m_walletsContainer.get<KeysIndex>().find(key);
	if (it == m_walletsContainer.get<KeysIndex>().end())
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to get wallet: not found. Spend public key " << key;
	  throw std::system_error(GlobalMembers.make_error_code(error.WALLET_NOT_FOUND));
	}

	return *it;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const WalletRecord& getWalletRecord(const string& address) const
  protected WalletRecord getWalletRecord(string address)
  {
	CryptoNote.AccountPublicAddress pubAddr = parseAddress(address);
	return getWalletRecord(pubAddr.spendPublicKey);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const WalletRecord& getWalletRecord(CryptoNote::ITransfersContainer* container) const
  protected WalletRecord getWalletRecord(CryptoNote.ITransfersContainer container)
  {
	var it = m_walletsContainer.get<TransfersContainerIndex>().find(container);
	if (it == m_walletsContainer.get<TransfersContainerIndex>().end())
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to get wallet by container: not found";
	  throw std::system_error(GlobalMembers.make_error_code(error.WALLET_NOT_FOUND));
	}

	return *it;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: CryptoNote::AccountPublicAddress parseAddress(const string& address) const
  protected CryptoNote.AccountPublicAddress parseAddress(string address)
  {
	CryptoNote.AccountPublicAddress pubAddr = new CryptoNote.AccountPublicAddress();

	if (!m_currency.parseAccountAddressString(address, pubAddr))
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to parse address: " << address;
	  throw std::system_error(GlobalMembers.make_error_code(error.BAD_ADDRESS));
	}

	return pubAddr;
  }
  protected string addWallet(NewAddressData addressData, ulong scanHeight, bool newAddress)
  {
	SecretKey spendSecretKey = new SecretKey(addressData.spendSecretKey);
	PublicKey spendPublicKey = new PublicKey(addressData.spendPublicKey);

	auto index = m_walletsContainer.get<KeysIndex>();

	var trackingMode = getTrackingMode();

	if ((trackingMode == WalletTrackingMode.TRACKING && spendSecretKey != GlobalMembers.NULL_SECRET_KEY) || (trackingMode == WalletTrackingMode.NOT_TRACKING && spendSecretKey == GlobalMembers.NULL_SECRET_KEY))
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to add wallet: incompatible tracking mode and spend secret key, tracking mode=" << trackingMode << ", spendSecretKey " << (spendSecretKey == GlobalMembers.NULL_SECRET_KEY != null ? "is null" : "is not null");
	  throw std::system_error(GlobalMembers.make_error_code(error.WRONG_PARAMETERS));
	}

	var insertIt = index.find(spendPublicKey);
	if (insertIt != index.end())
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to add wallet: address already exists, " << m_currency.accountAddressAsString(new AccountPublicAddress({spendPublicKey, m_viewPublicKey}));
	  throw std::system_error(GlobalMembers.make_error_code(error.ADDRESS_ALREADY_EXISTS));
	}

	try
	{
	  AccountSubscription sub = new AccountSubscription();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: sub.keys.address.viewPublicKey = m_viewPublicKey;
	  sub.keys.address.viewPublicKey.CopyFrom(m_viewPublicKey);
	  sub.keys.address.spendPublicKey = spendPublicKey;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: sub.keys.viewSecretKey = m_viewSecretKey;
	  sub.keys.viewSecretKey.CopyFrom(m_viewSecretKey);
	  sub.keys.spendSecretKey = spendSecretKey;
	  sub.transactionSpendableAge = m_transactionSoftLockTime;
	  sub.syncStart.height = scanHeight;

	  if (newAddress)
	  {
		  sub.syncStart.timestamp = getCurrentTimestampAdjusted();
	  }
	  else
	  {
		  sub.syncStart.timestamp = scanHeightToTimestamp(scanHeight);
	  }

	  m_containerStorage.push_back(encryptKeyPair(spendPublicKey, spendSecretKey, sub.syncStart.timestamp));
	  incNextIv();

	  auto trSubscription = m_synchronizer.addSubscription(sub);
	  ITransfersContainer container = trSubscription.getContainer();

	  WalletRecord wallet = new WalletRecord();
	  wallet.spendPublicKey = spendPublicKey;
	  wallet.spendSecretKey = spendSecretKey;
	  wallet.container = container;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: wallet.creationTimestamp = static_cast<DateTime>(sub.syncStart.timestamp);
	  wallet.creationTimestamp.CopyFrom((DateTime)sub.syncStart.timestamp);
	  trSubscription.addObserver(this);

	  index.insert(insertIt, std::move(wallet));
	  m_logger.functorMethod(DEBUGGING) << "Wallet count " << m_walletsContainer.size();

	  if (index.size() == 1)
	  {
		m_synchronizer.subscribeConsumerNotifications(m_viewPublicKey, this);
		initBlockchain(m_viewPublicKey);
	  }

	  var address = m_currency.accountAddressAsString(new AccountBase(spendPublicKey, m_viewPublicKey));
	  m_logger.functorMethod(DEBUGGING) << "Wallet added " << address << ", creation timestamp " << (int)sub.syncStart.timestamp;
	  return address;
	}
	catch (System.Exception e)
	{
	  m_logger.functorMethod(ERROR) << "Failed to add wallet: " << e.Message;

	  try
	  {
		m_containerStorage.pop_back();
	  }
	  catch
	  {
		m_logger.functorMethod(ERROR) << "Failed to rollback adding wallet to storage";
	  }

	  throw;
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: AccountKeys makeAccountKeys(const WalletRecord& wallet) const
  protected AccountKeys makeAccountKeys(WalletRecord wallet)
  {
	AccountKeys keys = new AccountKeys();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: keys.address.spendPublicKey = wallet.spendPublicKey;
	keys.address.spendPublicKey.CopyFrom(wallet.spendPublicKey);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: keys.address.viewPublicKey = m_viewPublicKey;
	keys.address.viewPublicKey.CopyFrom(m_viewPublicKey);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: keys.spendSecretKey = wallet.spendSecretKey;
	keys.spendSecretKey.CopyFrom(wallet.spendSecretKey);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: keys.viewSecretKey = m_viewSecretKey;
	keys.viewSecretKey.CopyFrom(m_viewSecretKey);

	return keys;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint getTransactionId(const Hash& transactionHash) const
  protected uint getTransactionId(Hash transactionHash)
  {
	var it = m_transactions.get<TransactionIndex>().find(transactionHash);

	if (it == m_transactions.get<TransactionIndex>().end())
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to get transaction ID: hash not found. Transaction hash " << transactionHash;
	  throw std::system_error(GlobalMembers.make_error_code(std::errc.invalid_argument));
	}

	var rndIt = m_transactions.project<RandomAccessIndex>(it);
	var txId = std::distance(m_transactions.get<RandomAccessIndex>().begin(), rndIt);

	return txId;
  }
  protected void pushEvent(const WalletEvent & event);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isFusionTransaction(const WalletTransaction& walletTx) const
  protected bool isFusionTransaction(WalletTransaction walletTx)
  {
	if (walletTx.fee != 0)
	{
	  return false;
	}

	ulong inputsSum = 0;
	ulong outputsSum = 0;
	List<ulong> outputsAmounts = new List<ulong>();
	List<ulong> inputsAmounts = new List<ulong>();
	TransactionInformation txInfo = new TransactionInformation();
	bool gotTx = false;
	auto walletsIndex = m_walletsContainer.get<RandomAccessIndex>();
	foreach (WalletRecord wallet in walletsIndex)
	{
	  foreach (TransactionOutputInformation output in wallet.container.getTransactionOutputs(walletTx.hash, ITransfersContainer.IncludeTypeKey | ITransfersContainer.IncludeStateAll))
	  {
		if (outputsAmounts.Count <= output.outputInTransaction)
		{
		  outputsAmounts.Resize(output.outputInTransaction + 1, 0);
		}

		Debug.Assert(output.amount != 0);
		Debug.Assert(outputsAmounts[output.outputInTransaction] == 0);
		outputsAmounts[output.outputInTransaction] = output.amount;
		outputsSum += output.amount;
	  }

	  foreach (TransactionOutputInformation input in wallet.container.getTransactionInputs(walletTx.hash, ITransfersContainer.IncludeTypeKey))
	  {
		inputsSum += input.amount;
		inputsAmounts.Add(input.amount);
	  }

	  if (!gotTx)
	  {
		gotTx = wallet.container.getTransactionInformation(walletTx.hash, txInfo);
	  }
	}

	if (!gotTx)
	{
	  return false;
	}

	if (outputsSum != inputsSum || outputsSum != txInfo.totalAmountOut || inputsSum != txInfo.totalAmountIn)
	{
	  return false;
	}
	else
	{
	  return m_currency.isFusionTransaction(inputsAmounts, outputsAmounts, 0, m_node.getLastKnownBlockHeight()); //size = 0 here because can't get real size of tx in wallet.
	}
  }


//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  protected void prepareTransaction(List<WalletOuts>&& wallets, List<WalletOrder> orders, ulong fee, ushort mixIn, string extra, ulong unlockTimestamp, DonationSettings donation, CryptoNote.AccountPublicAddress changeDestination, PreparedTransaction preparedTransaction)
  {

	preparedTransaction.destinations = new List<WalletTransfer>(convertOrdersToTransfers(orders));
	preparedTransaction.neededMoney = countNeededMoney(preparedTransaction.destinations, fee);

	List<OutputToTransfer> selectedTransfers = new List<OutputToTransfer>();
	ulong foundMoney = selectTransfers(preparedTransaction.neededMoney, mixIn == 0, m_currency.defaultDustThreshold(m_node.getLastKnownBlockHeight()), std::move(wallets), selectedTransfers);

	if (foundMoney < preparedTransaction.neededMoney)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to create transaction: not enough money. Needed " << m_currency.formatAmount(preparedTransaction.neededMoney) << ", found " << m_currency.formatAmount(foundMoney);
	  throw std::system_error(GlobalMembers.make_error_code(error.WRONG_AMOUNT), "Not enough money");
	}

	List<CryptoNote.COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS.outs_for_amount> mixinResult = new List<CryptoNote.COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS.outs_for_amount>();

	if (mixIn != 0)
	{
	  requestMixinOuts(selectedTransfers, mixIn, mixinResult);
	}

	List<InputInfo> keysInfo = new List<InputInfo>();
	prepareInputs(selectedTransfers, mixinResult, mixIn, keysInfo);

	ulong donationAmount = pushDonationTransferIfPossible(donation, foundMoney - preparedTransaction.neededMoney, m_currency.defaultDustThreshold(m_node.getLastKnownBlockHeight()), preparedTransaction.destinations);
	preparedTransaction.changeAmount = foundMoney - preparedTransaction.neededMoney - donationAmount;

	List<ReceiverAmounts> decomposedOutputs = splitDestinations(preparedTransaction.destinations, m_currency.defaultDustThreshold(m_node.getLastKnownBlockHeight()), m_currency);
	if (preparedTransaction.changeAmount != 0)
	{
	  WalletTransfer changeTransfer = new WalletTransfer();
	  changeTransfer.type = WalletTransferType.CHANGE;
	  changeTransfer.address = m_currency.accountAddressAsString(changeDestination);
	  changeTransfer.amount = (long)preparedTransaction.changeAmount;
	  preparedTransaction.destinations.emplace_back(std::move(changeTransfer));

	  var splittedChange = splitAmount(preparedTransaction.changeAmount, changeDestination, m_currency.defaultDustThreshold(m_node.getLastKnownBlockHeight()));
	  decomposedOutputs.emplace_back(std::move(splittedChange));
	}

	preparedTransaction.transaction = makeTransaction(decomposedOutputs, keysInfo, extra, unlockTimestamp);
  }

  protected uint doTransfer(TransactionParameters transactionParameters)
  {
	validateTransactionParameters(transactionParameters);
	CryptoNote.AccountPublicAddress changeDestination = getChangeDestination(transactionParameters.changeDestination, transactionParameters.sourceAddresses);
	m_logger.functorMethod(DEBUGGING) << "Change address " << m_currency.accountAddressAsString(changeDestination);

	List<WalletOuts> wallets = new List<WalletOuts>();
	if (transactionParameters.sourceAddresses.Count > 0)
	{
	  wallets = new List<WalletOuts>(pickWallets(transactionParameters.sourceAddresses));
	}
	else
	{
	  wallets = new List<WalletOuts>(pickWalletsWithMoney());
	}

	PreparedTransaction preparedTransaction = new PreparedTransaction();
	prepareTransaction(std::move(wallets), transactionParameters.destinations, transactionParameters.fee, transactionParameters.mixIn, transactionParameters.extra, transactionParameters.unlockTimestamp, transactionParameters.donation, changeDestination, preparedTransaction);

	return validateSaveAndSendTransaction(preparedTransaction.transaction, preparedTransaction.destinations, false, true);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void checkIfEnoughMixins(ClassicVector<CryptoNote::COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS::outs_for_amount>& mixinResult, ushort mixIn) const
  protected void checkIfEnoughMixins(List<CryptoNote.COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS.outs_for_amount> mixinResult, ushort mixIn)
  {
	Debug.Assert(mixIn != 0);

//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: auto notEnoughIt = std::find_if(mixinResult.begin(), mixinResult.end(), [mixIn](const CryptoNote::COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS::outs_for_amount& ofa)
	var notEnoughIt = std::find_if(mixinResult.GetEnumerator(), mixinResult.end(), (CryptoNote.COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS.outs_for_amount ofa) =>
	{
		return ofa.outs.size() < mixIn;
	});

	if (notEnoughIt != mixinResult.end())
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Mixin is too big: " << mixIn;
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.MIXIN_COUNT_TOO_BIG));
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<WalletTransfer> convertOrdersToTransfers(const ClassicVector<WalletOrder>& orders) const
  protected List<WalletTransfer> convertOrdersToTransfers(List<WalletOrder> orders)
  {
	List<WalletTransfer> transfers = new List<WalletTransfer>();
	transfers.Capacity = orders.Count;

	foreach (var order in orders)
	{
	  WalletTransfer transfer = new WalletTransfer();

	  if (order.amount > (ulong)(long.MaxValue))
	  {
		string message = "Order amount must not exceed " + m_currency.formatAmount(decltype(transfer.amount).MaxValue);
		m_logger.functorMethod(ERROR, BRIGHT_RED) << message;
		throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.WRONG_AMOUNT), message);
	  }

	  transfer.type = WalletTransferType.USUAL;
	  transfer.address = order.address;
	  transfer.amount = (long)order.amount;

	  transfers.emplace_back(std::move(transfer));
	}

	return transfers;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong countNeededMoney(const ClassicVector<CryptoNote::WalletTransfer>& destinations, ulong fee) const
  protected ulong countNeededMoney(List<CryptoNote.WalletTransfer> destinations, ulong fee)
  {
	ulong neededMoney = 0;
	foreach (var transfer in destinations)
	{
	  if (transfer.amount == 0)
	  {
		m_logger.functorMethod(ERROR, BRIGHT_RED) << "Bad destination: zero amount, address " << transfer.address;
		throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.ZERO_DESTINATION));
	  }
	  else if (transfer.amount < 0)
	  {
		m_logger.functorMethod(ERROR, BRIGHT_RED) << "Bad destination: negative amount, address " << transfer.address;
		throw std::system_error(GlobalMembers.make_error_code(std::errc.invalid_argument));
	  }

	  //to suppress warning
	  ulong uamount = (ulong)transfer.amount;
	  if (neededMoney <= ulong.MaxValue - uamount)
	  {
		neededMoney += uamount;
	  }
	  else
	  {
		m_logger.functorMethod(ERROR, BRIGHT_RED) << "Bad destinations: integer overflow";
		throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.SUM_OVERFLOW));
	  }
	}

	if (neededMoney <= ulong.MaxValue - fee)
	{
	  neededMoney += fee;
	}
	else
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Bad fee: integer overflow, fee=" << (int)fee;
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.SUM_OVERFLOW));
	}

	return neededMoney;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: CryptoNote::AccountPublicAddress parseAccountAddressString(const string& addressString) const
  protected CryptoNote.AccountPublicAddress parseAccountAddressString(string addressString)
  {
	CryptoNote.AccountPublicAddress address = new CryptoNote.AccountPublicAddress();

	if (!m_currency.parseAccountAddressString(addressString, address))
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Bad address: " << addressString;
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BAD_ADDRESS));
	}

	return address;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong pushDonationTransferIfPossible(const DonationSettings& donation, ulong freeAmount, ulong dustThreshold, ClassicVector<WalletTransfer>& destinations) const
  protected ulong pushDonationTransferIfPossible(DonationSettings donation, ulong freeAmount, ulong dustThreshold, List<WalletTransfer> destinations)
  {

	ulong donationAmount = 0;
	if (!string.IsNullOrEmpty(donation.address) && donation.threshold != 0)
	{
	  if (donation.threshold > (ulong)(long.MaxValue))
	  {
		string message = "Donation threshold must not exceed " + m_currency.formatAmount(long.MaxValue);
		m_logger.functorMethod(ERROR, BRIGHT_RED) << message;
		throw std::system_error(GlobalMembers.make_error_code(error.WRONG_AMOUNT), message);
	  }

	  donationAmount = GlobalMembers.calculateDonationAmount(freeAmount, donation.threshold, dustThreshold);
	  if (donationAmount != 0)
	  {
		destinations.emplace_back(new WalletTransfer({WalletTransferType.DONATION, donation.address, (long)donationAmount}));
		m_logger.functorMethod(DEBUGGING) << "Added donation: address " << donation.address << ", amount " << m_currency.formatAmount(donationAmount);
	  }
	}

	return donationAmount;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void validateAddresses(const ClassicVector<string>& addresses) const
  protected void validateAddresses(List<string> addresses)
  {
	foreach (var address in addresses)
	{
	  if (!CryptoNote.validateAddress(address, m_currency))
	  {
		m_logger.functorMethod(ERROR, BRIGHT_RED) << "Bad address: " << address;
		throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BAD_ADDRESS));
	  }
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void validateOrders(const ClassicVector<WalletOrder>& orders) const
  protected void validateOrders(List<WalletOrder> orders)
  {
	foreach (var order in orders)
	{
	  if (!CryptoNote.validateAddress(order.address, m_currency))
	  {
		m_logger.functorMethod(ERROR, BRIGHT_RED) << "Bad order address: " << order.address;
		throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BAD_ADDRESS));
	  }

	  if (order.amount >= (ulong)(long.MaxValue))
	  {
		string message = "Order amount must not exceed " + m_currency.formatAmount(long.MaxValue);
		m_logger.functorMethod(ERROR, BRIGHT_RED) << message;
		throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.WRONG_AMOUNT), message);
	  }
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void validateChangeDestination(const ClassicVector<string>& sourceAddresses, const string& changeDestination, bool isFusion) const
  protected void validateChangeDestination(List<string> sourceAddresses, string changeDestination, bool isFusion)
  {
	string message;
	if (string.IsNullOrEmpty(changeDestination))
	{
	  if (sourceAddresses.Count > 1 || (sourceAddresses.Count == 0 && m_walletsContainer.size() > 1))
	  {
		message = (string)(isFusion ? "Destination" : "Change destination") + " address is necessary";
		m_logger.functorMethod(ERROR, BRIGHT_RED) << message << ". Source addresses size=" << sourceAddresses.Count << ", wallets count=" << m_walletsContainer.size();
		throw std::system_error(GlobalMembers.make_error_code(isFusion ? error.DESTINATION_ADDRESS_REQUIRED : error.CHANGE_ADDRESS_REQUIRED), message);
	  }
	}
	else
	{
	  if (!CryptoNote.validateAddress(changeDestination, m_currency))
	  {
		message = "Bad " + (isFusion ? "destination" : "change destination") + " address: " + changeDestination;
		m_logger.functorMethod(ERROR, BRIGHT_RED) << message;
		throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BAD_ADDRESS), message);
	  }

	  if (!isMyAddress(changeDestination))
	  {
		message = (string)(isFusion ? "Destination" : "Change destination") + " address is not found in current container: " + changeDestination;
		m_logger.functorMethod(ERROR, BRIGHT_RED) << message;
		throw std::system_error(GlobalMembers.make_error_code(isFusion ? error.DESTINATION_ADDRESS_NOT_FOUND : error.CHANGE_ADDRESS_NOT_FOUND), message);
	  }
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void validateSourceAddresses(const ClassicVector<string>& sourceAddresses) const
  protected void validateSourceAddresses(List<string> sourceAddresses)
  {
	validateAddresses(sourceAddresses);

//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: auto badAddr = std::find_if(sourceAddresses.begin(), sourceAddresses.end(), [this](const string& addr)
	var badAddr = std::find_if(sourceAddresses.GetEnumerator(), sourceAddresses.end(), (string addr) =>
	{
	  return !isMyAddress(addr);
	});

	if (badAddr != sourceAddresses.end())
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Source address isn't belong to the container: " << *badAddr;
	  throw std::system_error(GlobalMembers.make_error_code(error.BAD_ADDRESS), "Source address must belong to current container: " + *badAddr);
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void validateTransactionParameters(const TransactionParameters& transactionParameters) const
  protected void validateTransactionParameters(TransactionParameters transactionParameters)
  {
	if (transactionParameters.destinations.Count == 0)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "No destinations";
	  throw std::system_error(GlobalMembers.make_error_code(error.ZERO_DESTINATION));
	}

	if (transactionParameters.fee < m_currency.minimumFee())
	{
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
	  string message = "Fee is too small. Fee " + m_currency.formatAmount(transactionParameters.fee) +
		", minimum fee " + m_currency.formatAmount(m_currency.minimumFee());
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << message;
	  throw std::system_error(GlobalMembers.make_error_code(error.FEE_TOO_SMALL), message);
	}

	if (string.IsNullOrEmpty(transactionParameters.donation.address) != (transactionParameters.donation.threshold == 0))
	{
	  string message = "DonationSettings must have both address and threshold parameters filled. Address '" + transactionParameters.donation.address + "'" +
		", threshold " + m_currency.formatAmount(transactionParameters.donation.threshold);
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << message;
	  throw std::system_error(GlobalMembers.make_error_code(error.WRONG_PARAMETERS), message);
	}

	validateSourceAddresses(transactionParameters.sourceAddresses);
	validateChangeDestination(transactionParameters.sourceAddresses, transactionParameters.changeDestination, false);
	validateOrders(transactionParameters.destinations);
  }

  protected void requestMixinOuts(List<OutputToTransfer> selectedTransfers, ushort mixIn, List<CryptoNote.COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS.outs_for_amount> mixinResult)
  {

	List<ulong> amounts = new List<ulong>();
	foreach (var @out in selectedTransfers)
	{
	  amounts.Add(@out.@out.amount);
	}

	System.Event requestFinished = new System.Event(m_dispatcher);
	std::error_code mixinError = new std::error_code();

	throwIfStopped();

	ushort requestMixinCount = mixIn + 1; //+1 to allow to skip real output

	m_logger.functorMethod(DEBUGGING) << "Requesting random outputs";
	System.RemoteContext getOutputsContext(m_dispatcher, [this, amounts, requestMixinCount, mixinResult, requestFinished, mixinError] mutable
	{
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: m_node.getRandomOutsByAmounts(std::move(amounts), requestMixinCount, mixinResult, [&requestFinished, &mixinError, this] (std::error_code ec) mutable
	  m_node.getRandomOutsByAmounts(std::move(amounts), requestMixinCount, mixinResult, (std::error_code ec) => mutable
	  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: mixinError = ec;
		mixinError.CopyFrom(ec);
		m_dispatcher.remoteSpawn(std::bind(GlobalMembers.asyncRequestCompletion, std::@ref(requestFinished)));
	  }
	 );
	}
   );
	getOutputsContext.get();
	requestFinished.wait();

	checkIfEnoughMixins(mixinResult, requestMixinCount);

	if (mixinError != null)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to get random outputs: " << mixinError << ", " << mixinError.message();
	  throw std::system_error(mixinError);
	}

	m_logger.functorMethod(DEBUGGING) << "Random outputs received";
  }

  protected void prepareInputs(List<OutputToTransfer> selectedTransfers, List<CryptoNote.COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS.outs_for_amount> mixinResult, ushort mixIn, List<InputInfo> keysInfo)
  {


	uint i = 0;
	foreach (var input in selectedTransfers)
	{
	  TransactionTypes.InputKeyInfo keyInfo = new TransactionTypes.InputKeyInfo();
	  keyInfo.amount = input.@out.amount;

	  if (mixinResult.Count != 0)
	  {
  //C++ TO C# CONVERTER TODO TASK: The typedef 'out_entry' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
		std::sort(mixinResult[i].outs.begin(), mixinResult[i].outs.end(), (out_entry a, out_entry b) =>
		{
			return a.global_amount_index < b.global_amount_index;
		});
		foreach (var fakeOut in mixinResult[i].outs)
		{

		  if (input.@out.globalOutputIndex == fakeOut.global_amount_index)
		  {
			continue;
		  }

		  TransactionTypes.GlobalOutput globalOutput = new TransactionTypes.GlobalOutput();
		  globalOutput.outputIndex = (uint)fakeOut.global_amount_index;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		  globalOutput.targetKey = reinterpret_cast<PublicKey&>(fakeOut.out_key);
		  keyInfo.outputs.push_back(std::move(globalOutput));
		  if (keyInfo.outputs.size() >= mixIn)
		  {
			break;
		  }
		}
	  }

	  //paste real transaction to the random index
//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
	  var insertIn = std::find_if(keyInfo.outputs.begin(), keyInfo.outputs.end(), (TransactionTypes.GlobalOutput a) =>
	  {
		return a.outputIndex >= input.@out.globalOutputIndex;
	  });

	  TransactionTypes.GlobalOutput realOutput = new TransactionTypes.GlobalOutput();
	  realOutput.outputIndex = input.@out.globalOutputIndex;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  realOutput.targetKey = reinterpret_cast<const PublicKey&>(input.@out.outputKey);

	  var insertedIn = keyInfo.outputs.insert(insertIn, realOutput);

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  keyInfo.realOutput.transactionPublicKey = reinterpret_cast<const PublicKey&>(input.@out.transactionPublicKey);
	  keyInfo.realOutput.transactionIndex = (uint)(insertedIn - keyInfo.outputs.begin());
	  keyInfo.realOutput.outputInTransaction = input.@out.outputInTransaction;

	  //Important! outputs in selectedTransfers and in keysInfo must have the same order!
	  InputInfo inputInfo = new InputInfo();
	  inputInfo.keyInfo = std::move(keyInfo);
	  inputInfo.walletRecord = input.wallet;
	  keysInfo.Add(std::move(inputInfo));
	  ++i;
	}
  }

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  protected ulong selectTransfers(ulong neededMoney, bool dust, ulong dustThreshold, List<WalletOuts>&& wallets, List<OutputToTransfer> selectedTransfers)
  {

	ulong foundMoney = 0;

	List<Tuple<WalletRecord, TransactionOutputInformation>> dustOutputs = new List<Tuple<WalletRecord, TransactionOutputInformation>>();
	List<Tuple<WalletRecord, TransactionOutputInformation>> walletOuts = new List<Tuple<WalletRecord, TransactionOutputInformation>>();
	for (var walletIt = wallets.begin(); walletIt != wallets.end(); ++walletIt)
	{
	  for (var outIt = walletIt.outs.begin(); outIt != walletIt.outs.end(); ++outIt)
	  {
		if (outIt.amount > dustThreshold)
		{
		  walletOuts.emplace_back(std::piecewise_construct, std::forward_as_tuple(walletIt.wallet), std::forward_as_tuple(*outIt));
		}
		else if (dust)
		{
		  dustOutputs.emplace_back(std::piecewise_construct, std::forward_as_tuple(walletIt.wallet), std::forward_as_tuple(*outIt));
		}
	  }
	}

	ShuffleGenerator<uint, Crypto.random_engine<uint>> indexGenerator = new ShuffleGenerator<uint, Crypto.random_engine<uint>>(walletOuts.Count);
	while (foundMoney < neededMoney && !indexGenerator.empty())
	{
	  auto @out = walletOuts[indexGenerator.functorMethod()];
	  foundMoney += @out.second.amount;
	  selectedTransfers.emplace_back(new OutputToTransfer({std::move(@out.second), std::move(@out.first)}));
	}

	if (dust && dustOutputs.Count > 0)
	{
	  ShuffleGenerator<uint, Crypto.random_engine<uint>> dustIndexGenerator = new ShuffleGenerator<uint, Crypto.random_engine<uint>>(dustOutputs.Count);
	  do
	  {
		auto @out = dustOutputs[dustIndexGenerator.functorMethod()];
		foundMoney += @out.second.amount;
		selectedTransfers.emplace_back(new OutputToTransfer({std::move(@out.second), std::move(@out.first)}));
	  } while (foundMoney < neededMoney && !dustIndexGenerator.empty());
	}

	return foundMoney;
  }

  protected List<CryptoNote.WalletGreen.ReceiverAmounts> splitDestinations(List<CryptoNote.WalletTransfer> destinations, ulong dustThreshold, CryptoNote.Currency currency)
  {

	List<ReceiverAmounts> decomposedOutputs = new List<ReceiverAmounts>();
	foreach (var destination in destinations)
	{
	  AccountPublicAddress address = parseAccountAddressString(destination.address);
	  decomposedOutputs.Add(splitAmount(destination.amount, address, dustThreshold));
	}

	return decomposedOutputs;
  }
  protected CryptoNote.WalletGreen.ReceiverAmounts splitAmount(ulong amount, AccountPublicAddress destination, ulong dustThreshold)
  {

	ReceiverAmounts receiverAmounts = new ReceiverAmounts();

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: receiverAmounts.receiver = destination;
	receiverAmounts.receiver.CopyFrom(destination);
	decomposeAmount(amount, dustThreshold, receiverAmounts.amounts);
	return receiverAmounts;
  }

  protected std::unique_ptr<CryptoNote.ITransaction> makeTransaction(List<ReceiverAmounts> decomposedOutputs, List<InputInfo> keysInfo, string extra, ulong unlockTimestamp)
  {

	std::unique_ptr<ITransaction> tx = createTransaction();

	List<Tuple<AccountPublicAddress, ulong>> amountsToAddresses = new List<Tuple<AccountPublicAddress, ulong>>();
	foreach (var output in decomposedOutputs)
	{
	  foreach (var amount in output.amounts)
	  {
		amountsToAddresses.emplace_back(AmountToAddress({output.receiver, amount}));
	  }
	}

	std::shuffle(amountsToAddresses.GetEnumerator(), amountsToAddresses.end(), std::default_random_engine({Crypto.GlobalMembers.rand<std::default_random_engine.result_type>()}));
//C++ TO C# CONVERTER TODO TASK: The 'Compare' parameter of std::sort produces a boolean value, while the .NET Comparison parameter produces a tri-state result:
//ORIGINAL LINE: std::sort(amountsToAddresses.begin(), amountsToAddresses.end(), [] (const System.Tuple<const AccountPublicAddress*, ulong>& left, const System.Tuple<const AccountPublicAddress*, ulong>& right)
	amountsToAddresses.Sort((Tuple<const AccountPublicAddress, ulong> left, Tuple<const AccountPublicAddress, ulong> right) =>
	{
	  return left.Item2 < right.Item2;
	});

	foreach (var amountToAddress in amountsToAddresses)
	{
	  tx.addOutput(amountToAddress.Item2, *amountToAddress.Item1);
	}

	tx.setUnlockTime(unlockTimestamp);
	tx.appendExtra(Common.asBinaryArray(extra));

	foreach (var input in keysInfo)
	{
	  tx.addInput(makeAccountKeys(input.walletRecord), input.keyInfo, input.ephKeys);
	}

	uint i = 0;
	foreach (var input in keysInfo)
	{
	  tx.signInputKey(i++, input.keyInfo, input.ephKeys);
	}

	m_logger.functorMethod(DEBUGGING) << "Transaction created, hash " << tx.getTransactionHash() << ", inputs " << m_currency.formatAmount(tx.getInputTotalAmount()) << ", outputs " << m_currency.formatAmount(tx.getOutputTotalAmount()) << ", fee " << m_currency.formatAmount(tx.getInputTotalAmount() - tx.getOutputTotalAmount());
	  return tx;
  }

  protected void sendTransaction(CryptoNote.Transaction cryptoNoteTransaction)
  {
	System.Event completion = new System.Event(m_dispatcher);
	std::error_code ec = new std::error_code();

	throwIfStopped();

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: System::RemoteContext relayTransactionContext(m_dispatcher, [this, &cryptoNoteTransaction, &ec, &completion]()
	System.RemoteContext relayTransactionContext(m_dispatcher, () =>
	{
	  m_node.relayTransaction(cryptoNoteTransaction, (std::error_code error) =>
	  {
		ec.CopyFrom(error);
		this.m_dispatcher.remoteSpawn(std::bind(asyncRequestCompletion, std::@ref(completion)));
	  });
	});
	relayTransactionContext.get();
	completion.wait();

	if (ec != null)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to relay transaction: " << ec << ", " << ec.message() << ". Transaction hash " << CryptoNote.GlobalMembers.getObjectHash(cryptoNoteTransaction);
	  throw std::system_error(ec);
	}
  }
  protected uint validateSaveAndSendTransaction(ITransactionReader transaction, List<WalletTransfer> destinations, bool isFusion, bool send)
  {
	List<byte> transactionData = transaction.getTransactionData();

	uint maxTxSize = getMaxTxSize();

	if (transactionData.Count > maxTxSize)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Transaction is too big. Transaction hash " << transaction.getTransactionHash() << ", size " << transactionData.Count << ", size limit " << (int)maxTxSize;
	  throw std::system_error(GlobalMembers.make_error_code(error.TRANSACTION_uintOO_BIG));
	}

	CryptoNote.Transaction cryptoNoteTransaction = new CryptoNote.Transaction();
	if (!CryptoNote.GlobalMembers.fromBinaryArray(ref cryptoNoteTransaction, transactionData))
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to deserialize created transaction. Transaction hash " << transaction.getTransactionHash();
	  throw std::system_error(GlobalMembers.make_error_code(error.INTERNAL_WALLET_ERROR), "Failed to deserialize created transaction");
	}

	ulong fee = transaction.getInputTotalAmount() - transaction.getOutputTotalAmount();
	uint transactionId = insertOutgoingTransactionAndPushEvent(transaction.getTransactionHash(), fee, transaction.getExtra(), transaction.getUnlockTime());
	m_logger.functorMethod(DEBUGGING) << "Transaction added to container, ID " << (int)transactionId << ", hash " << transaction.getTransactionHash() << ", block " << m_transactions[transactionId].blockHeight << ", state " << m_transactions[transactionId].state;
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: Tools::ScopeExit rollbackTransactionInsertion([this, transactionId]
	Tools.ScopeExit rollbackTransactionInsertion(() =>
	{
	  updateTransactionStateAndPushEvent(transactionId, WalletTransactionState.FAILED);
	});

	m_fusionTxsCache.Add(transactionId, isFusion);
	pushBackOutgoingTransfers(transactionId, destinations);

	addUnconfirmedTransaction(transaction);
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: Tools::ScopeExit rollbackAddingUnconfirmedTransaction([this, &transaction]
	Tools.ScopeExit rollbackAddingUnconfirmedTransaction(() =>
	{
	  try
	  {
		removeUnconfirmedTransaction(transaction.getTransactionHash());
	  }
	  catch
	  {
		// Ignore any exceptions. If rollback fails then the transaction is stored as unconfirmed and will be deleted after wallet relaunch
		// during transaction pool synchronization
		m_logger.functorMethod(ERROR, BRIGHT_RED) << "Unknown exception while removing unconfirmed transaction " << transaction.getTransactionHash();
	  }
	});

	if (send)
	{
	  sendTransaction(cryptoNoteTransaction);
	  m_logger.functorMethod(DEBUGGING) << "Transaction sent to node, ID " << (int)transactionId << ", hash " << transaction.getTransactionHash();
	  updateTransactionStateAndPushEvent(transactionId, WalletTransactionState.SUCCEEDED);
	}
	else
	{
	  Debug.Assert(m_uncommitedTransactions.count(transactionId) == 0);
	  m_uncommitedTransactions.emplace(transactionId, std::move(cryptoNoteTransaction));
	  m_logger.functorMethod(DEBUGGING) << "Transaction delayed, ID " << (int)transactionId << ", hash " << transaction.getTransactionHash();
	}

	rollbackAddingUnconfirmedTransaction.cancel();
	rollbackTransactionInsertion.cancel();

	return transactionId;
  }

  protected uint insertBlockchainTransaction(TransactionInformation info, long txBalance)
  {
	auto index = m_transactions.get<RandomAccessIndex>();

	WalletTransaction tx = new WalletTransaction();
	tx.state = WalletTransactionState.SUCCEEDED;
	tx.timestamp = info.timestamp;
	tx.blockHeight = info.blockHeight;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: tx.hash = info.transactionHash;
	tx.hash.CopyFrom(info.transactionHash);
	tx.isBase = info.totalAmountIn == 0;
	if (tx.isBase)
	{
	  tx.fee = 0;
	}
	else
	{
	  tx.fee = info.totalAmountIn - info.totalAmountOut;
	}

	tx.unlockTime = info.unlockTime;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	tx.extra.assign(reinterpret_cast<const char>(info.extra.data()), info.extra.Count);
	tx.totalAmount = txBalance;
	tx.creationTime = info.timestamp;

	uint txId = index.size();
	index.push_back(std::move(tx));

	m_logger.functorMethod(DEBUGGING) << "Transaction added, ID " << (int)txId << ", hash " << tx.hash << ", block " << (int)tx.blockHeight << ", state " << tx.state;

	return txId;
  }
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  uint insertOutgoingTransactionAndPushEvent(Crypto::Hash transactionHash, ulong fee, BinaryArray extra, ulong unlockTimestamp);
  protected void updateTransactionStateAndPushEvent(uint transactionId, WalletTransactionState state)
  {
	var it = std::next(m_transactions.get<RandomAccessIndex>().begin(), transactionId);

	if (it.state != state)
	{
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: m_transactions.get<RandomAccessIndex>().modify(it, [state](WalletTransaction& tx)
	  m_transactions.get<RandomAccessIndex>().modify(it, (WalletTransaction tx) =>
	  {
		tx.state = state;
	  });

	  pushEvent(GlobalMembers.makeTransactionUpdatedEvent(transactionId));
	  m_logger.functorMethod(DEBUGGING) << "Transaction state changed, ID " << (int)transactionId << ", hash " << it.hash << ", new state " << it.state;
	}
  }
  protected bool updateWalletTransactionInfo(uint transactionId, CryptoNote.TransactionInformation info, long totalAmount)
  {
	auto txIdIndex = m_transactions.get<RandomAccessIndex>();
	Debug.Assert(transactionId < txIdIndex.size());
	var it = std::next(txIdIndex.begin(), transactionId);

	bool updated = false;
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: bool r = txIdIndex.modify(it, [&info, totalAmount, &updated](WalletTransaction& transaction)
	bool r = txIdIndex.modify(it, (WalletTransaction transaction) =>
	{
	  if (transaction.blockHeight != info.blockHeight)
	  {
		transaction.blockHeight = info.blockHeight;
		updated = true;
	  }

	  if (transaction.timestamp != info.timestamp)
	  {
		transaction.timestamp = info.timestamp;
		updated = true;
	  }

	  bool isSucceeded = transaction.state == WalletTransactionState.SUCCEEDED;
	  // If transaction was sent to daemon, it can not have CREATED and FAILED states, its state can be SUCCEEDED, CANCELLED or DELETED
	  bool wasSent = transaction.state != WalletTransactionState.CREATED && transaction.state != WalletTransactionState.FAILED;
	  bool isConfirmed = transaction.blockHeight != WALLET_UNCONFIRMED_TRANSACTION_HEIGHT;
	  if (!isSucceeded && (wasSent || isConfirmed))
	  {
		//transaction may be deleted first then added again
		transaction.state = WalletTransactionState.SUCCEEDED;
		updated = true;
	  }

	  if (transaction.totalAmount != totalAmount)
	  {
		transaction.totalAmount = totalAmount;
		updated = true;
	  }

	  // Fix LegacyWallet error. Some old versions didn't fill extra field
	  if (string.IsNullOrEmpty(transaction.extra) && info.extra.Count > 0)
	  {
		transaction.extra = Common.asString(info.extra);
		updated = true;
	  }

	  bool isBase = info.totalAmountIn == 0;
	  if (transaction.isBase != isBase)
	  {
		transaction.isBase = isBase;
		updated = true;
	  }
	});

	if (r)
	{
	}
	Debug.Assert(r);

	if (updated)
	{
	  m_logger.functorMethod(DEBUGGING) << "Transaction updated, ID " << (int)transactionId << ", hash " << it.hash << ", block " << it.blockHeight << ", state " << it.state;
	}

	return updated;
  }
  protected bool updateTransactionTransfers(uint transactionId, List<ContainerAmounts> containerAmountsList, long allInputsAmount, long allOutputsAmount)
  {

	Debug.Assert(allInputsAmount <= 0);
	Debug.Assert(allOutputsAmount >= 0);

	bool updated = false;

	var transfersRange = getTransactionTransfersRange(transactionId);
	// Iterators can be invalidated, so the first transfer is addressed by its index
	uint firstTransferIdx = std::distance(m_transfers.cbegin(), transfersRange.Item1);

	TransfersMap initialTransfers = getKnownTransfersMap(transactionId, firstTransferIdx);

	HashSet<string> myInputAddresses = new HashSet<string>();
	HashSet<string> myOutputAddresses = new HashSet<string>();
	long myInputsAmount = 0;
	long myOutputsAmount = 0;
	foreach (var containerAmount in containerAmountsList)
	{
	  AccountPublicAddress address = new AccountPublicAddress(getWalletRecord(containerAmount.container).spendPublicKey, m_viewPublicKey);
	  string addressString = m_currency.accountAddressAsString(address);

	  updated |= updateAddressTransfers(transactionId, firstTransferIdx, addressString, initialTransfers[addressString].input, containerAmount.amounts.input);
	  updated |= updateAddressTransfers(transactionId, firstTransferIdx, addressString, initialTransfers[addressString].output, containerAmount.amounts.output);

	  myInputsAmount += containerAmount.amounts.input;
	  myOutputsAmount += containerAmount.amounts.output;

	  if (containerAmount.amounts.input != 0)
	  {
		myInputAddresses.emplace(addressString);
	  }

	  if (containerAmount.amounts.output != 0)
	  {
		myOutputAddresses.emplace(addressString);
	  }
	}

	Debug.Assert(myInputsAmount >= allInputsAmount);
	Debug.Assert(myOutputsAmount <= allOutputsAmount);

	long knownInputsAmount = 0;
	long knownOutputsAmount = 0;
	var updatedTransfers = getKnownTransfersMap(transactionId, firstTransferIdx);
	foreach (var pair in updatedTransfers)
	{
	  knownInputsAmount += pair.second.input;
	  knownOutputsAmount += pair.second.output;
	}

	Debug.Assert(myInputsAmount >= knownInputsAmount);
	Debug.Assert(myOutputsAmount <= knownOutputsAmount);

	updated |= updateUnknownTransfers(transactionId, firstTransferIdx, myInputAddresses, knownInputsAmount, myInputsAmount, allInputsAmount, false);
	updated |= updateUnknownTransfers(transactionId, firstTransferIdx, myOutputAddresses, knownOutputsAmount, myOutputsAmount, allOutputsAmount, true);

	return updated;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicUnorderedMap<string, AddressAmounts> getKnownTransfersMap(uint transactionId, uint firstTransferIdx) const
  protected Dictionary<string, AddressAmounts> getKnownTransfersMap(uint transactionId, uint firstTransferIdx)
  {
	TransfersMap result = new TransfersMap();

	for (var it = std::next(m_transfers.begin(), firstTransferIdx); it != m_transfers.end() && it.first == transactionId; ++it)
	{
	  auto address = it.second.address;

	  if (!address.empty())
	  {
		if (it.second.amount < 0)
		{
		  result[address].input += it.second.amount;
		}
		else
		{
		  Debug.Assert(it.second.amount > 0);
		  result[address].output += it.second.amount;
		}
	  }
	}

	return result;
  }
  protected bool updateAddressTransfers(uint transactionId, uint firstTransferIdx, string address, long knownAmount, long targetAmount)
  {
	Debug.Assert((knownAmount > 0 && targetAmount > 0) || (knownAmount < 0 && targetAmount < 0) || knownAmount == 0 || targetAmount == 0);

	bool updated = false;

	if (knownAmount != targetAmount)
	{
	  if (knownAmount == 0)
	  {
		appendTransfer(transactionId, firstTransferIdx, address, targetAmount);
		updated = true;
	  }
	  else if (targetAmount == 0)
	  {
		Debug.Assert(knownAmount != 0);
		updated |= eraseTransfersByAddress(transactionId, firstTransferIdx, address, knownAmount > 0);
	  }
	  else
	  {
		updated |= adjustTransfer(transactionId, firstTransferIdx, address, targetAmount);
	  }
	}

	return updated;
  }
  protected bool updateUnknownTransfers(uint transactionId, uint firstTransferIdx, HashSet<string> myAddresses, long knownAmount, long myAmount, long totalAmount, bool isOutput)
  {

	bool updated = false;

	if (Math.Abs(knownAmount) > Math.Abs(totalAmount))
	{
	  updated |= eraseForeignTransfers(transactionId, firstTransferIdx, myAddresses, isOutput);
	  if (totalAmount == myAmount)
	  {
		updated |= eraseTransfersByAddress(transactionId, firstTransferIdx, string(), isOutput);
	  }
	  else
	  {
		Debug.Assert(Math.Abs(totalAmount) > Math.Abs(myAmount));
		updated |= adjustTransfer(transactionId, firstTransferIdx, string(), totalAmount - myAmount);
	  }
	}
	else if (knownAmount == totalAmount)
	{
	  updated |= eraseTransfersByAddress(transactionId, firstTransferIdx, string(), isOutput);
	}
	else
	{
	  Debug.Assert(Math.Abs(totalAmount) > Math.Abs(knownAmount));
	  updated |= adjustTransfer(transactionId, firstTransferIdx, string(), totalAmount - knownAmount);
	}

	return updated;
  }
  protected void appendTransfer(uint transactionId, uint firstTransferIdx, string address, long amount)
  {
	var it = std::next(m_transfers.begin(), firstTransferIdx);
//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
	var insertIt = std::upper_bound(it, m_transfers.end(), transactionId, (uint transactionId, Tuple<ulong, CryptoNote.WalletTransfer> pair) =>
	{
	  return transactionId < pair.Item1;
	});

	WalletTransfer transfer = new WalletTransfer(WalletTransferType.USUAL, address, amount);
	m_transfers.emplace(insertIt, std::piecewise_construct, std::forward_as_tuple(transactionId), std::forward_as_tuple(transfer));
  }
  protected bool adjustTransfer(uint transactionId, uint firstTransferIdx, string address, long amount)
  {
	Debug.Assert(amount != 0);

	bool updated = false;
	bool updateOutputTransfers = amount > 0;
	bool firstAddressTransferFound = false;
	var it = std::next(m_transfers.begin(), firstTransferIdx);
	while (it != m_transfers.end() && it.first == transactionId)
	{
	  Debug.Assert(it.second.amount != 0);
	  bool transferIsOutput = it.second.amount > 0;
	  if (transferIsOutput == updateOutputTransfers && it.second.address == address)
	  {
		if (firstAddressTransferFound)
		{
		  it = m_transfers.erase(it);
		  updated = true;
		}
		else
		{
		  if (it.second.amount != amount)
		  {
			it.second.amount = amount;
			updated = true;
		  }

		  firstAddressTransferFound = true;
		  ++it;
		}
	  }
	  else
	  {
		++it;
	  }
	}

	if (!firstAddressTransferFound)
	{
	  WalletTransfer transfer = new WalletTransfer(WalletTransferType.USUAL, address, amount);
	  m_transfers.emplace(it, std::piecewise_construct, std::forward_as_tuple(transactionId), std::forward_as_tuple(transfer));
	  updated = true;
	}

	return updated;
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  protected bool eraseTransfers(uint transactionId, uint firstTransferIdx, Func<bool, string&, bool>&& predicate)
  {
	bool erased = false;
	var it = std::next(m_transfers.begin(), firstTransferIdx);
	while (it != m_transfers.end() && it.first == transactionId)
	{
	  bool transferIsOutput = it.second.amount > 0;
	  if (predicate(transferIsOutput, it.second.address))
	  {
		it = m_transfers.erase(it);
		erased = true;
	  }
	  else
	  {
		++it;
	  }
	}

	return erased;
  }
  protected bool eraseTransfersByAddress(uint transactionId, uint firstTransferIdx, string address, bool eraseOutputTransfers)
  {
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: return eraseTransfers(transactionId, firstTransferIdx, [&address, eraseOutputTransfers](bool isOutput, const string& transferAddress)
	return eraseTransfers(transactionId, firstTransferIdx, (bool isOutput, string transferAddress) =>
	{
	  return eraseOutputTransfers == isOutput && address == transferAddress;
	});
  }
  protected bool eraseForeignTransfers(uint transactionId, uint firstTransferIdx, HashSet<string> knownAddresses, bool eraseOutputTransfers)
  {

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: return eraseTransfers(transactionId, firstTransferIdx, [&knownAddresses, eraseOutputTransfers](bool isOutput, const string& transferAddress)
	return eraseTransfers(transactionId, firstTransferIdx, (bool isOutput, string transferAddress) =>
	{
	  return eraseOutputTransfers == isOutput && knownAddresses.count(transferAddress) == 0;
	});
  }
  protected void pushBackOutgoingTransfers(uint txId, List<WalletTransfer> destinations)
  {

	foreach (var dest in destinations)
	{
	  WalletTransfer d = new WalletTransfer();
	  d.type = dest.type;
	  d.address = dest.address;
	  d.amount = dest.amount;

	  m_transfers.emplace_back(txId, std::move(d));
	}
  }
  protected void insertUnlockTransactionJob(Hash transactionHash, uint blockHeight, CryptoNote.ITransfersContainer container)
  {
	auto index = m_unlockTransactionsJob.get<BlockHeightIndex>();
	index.insert({blockHeight, container, transactionHash});
  }
  protected void deleteUnlockTransactionJob(Hash transactionHash)
  {
	auto index = m_unlockTransactionsJob.get<TransactionHashIndex>();
	index.erase(transactionHash);
  }
  protected void startBlockchainSynchronizer()
  {
	if (!m_walletsContainer.empty() && !m_blockchainSynchronizerStarted)
	{
	  m_logger.functorMethod(DEBUGGING) << "Starting BlockchainSynchronizer";
	  m_blockchainSynchronizer.start();
	  m_blockchainSynchronizerStarted = true;
	}
  }
  protected void stopBlockchainSynchronizer()
  {
	if (m_blockchainSynchronizerStarted)
	{
	  m_logger.functorMethod(DEBUGGING) << "Stopping BlockchainSynchronizer";
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: System::RemoteContext stopContext(m_dispatcher, [this]()
	  System.RemoteContext stopContext(m_dispatcher, () =>
	  {
		m_blockchainSynchronizer.stop();
	  });
	  stopContext.get();

	  m_blockchainSynchronizerStarted = false;
	}
  }
  protected void addUnconfirmedTransaction(ITransactionReader transaction)
  {
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: System::RemoteContext<std::error_code> context(m_dispatcher, [this, &transaction]
	System.RemoteContext<std::error_code> context(m_dispatcher, () =>
	{
	  return m_blockchainSynchronizer.addUnconfirmedTransaction(transaction).get();
	});

	var ec = context.get();
	if (ec)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to add unconfirmed transaction: " << ec << ", " << ec.message();
	  throw std::system_error(ec, "Failed to add unconfirmed transaction");
	}

	m_logger.functorMethod(DEBUGGING) << "Unconfirmed transaction added to BlockchainSynchronizer, hash " << transaction.getTransactionHash();
  }
  protected void removeUnconfirmedTransaction(Crypto.Hash transactionHash)
  {
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: System::RemoteContext context(m_dispatcher, [this, &transactionHash]
	System.RemoteContext context(m_dispatcher, () =>
	{
	  m_blockchainSynchronizer.removeUnconfirmedTransaction(transactionHash).get();
	});

	context.get();
	m_logger.functorMethod(DEBUGGING) << "Unconfirmed transaction removed from BlockchainSynchronizer, hash " << transactionHash;
  }

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void copyContainerStorageKeys(ContainerStorage src, Crypto::chacha8_key srcKey, ContainerStorage dst, Crypto::chacha8_key dstKey);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  static void copyContainerStoragePrefix(ContainerStorage src, Crypto::chacha8_key srcKey, ContainerStorage dst, Crypto::chacha8_key dstKey);
  protected void deleteOrphanTransactions(HashSet<Crypto.PublicKey> deletedKeys)
  {
	foreach (var spendPublicKey in deletedKeys)
	{
	  AccountPublicAddress deletedAccountAddress = new AccountPublicAddress();
	  deletedAccountAddress.spendPublicKey = spendPublicKey;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: deletedAccountAddress.viewPublicKey = m_viewPublicKey;
	  deletedAccountAddress.viewPublicKey.CopyFrom(m_viewPublicKey);
	  var deletedAddressString = m_currency.accountAddressAsString(deletedAccountAddress);

	  List<uint> deletedTransactions = new List<uint>();
	  List<uint> updatedTransactions = deleteTransfersForAddress(deletedAddressString, deletedTransactions);
	  deleteFromUncommitedTransactions(deletedTransactions);
	}
  }
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  static void encryptAndSaveContainerData(ContainerStorage storage, Crypto::chacha8_key key, object containerData, uint containerDataSize);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  static void loadAndDecryptContainerData(ContainerStorage storage, Crypto::chacha8_key key, BinaryArray containerData);
  protected void initTransactionPool()
  {
	HashSet<Crypto.Hash> uncommitedTransactionsSet = new HashSet<Crypto.Hash>();
	std::transform(m_uncommitedTransactions.begin(), m_uncommitedTransactions.end(), std::inserter(uncommitedTransactionsSet, uncommitedTransactionsSet.end()), (SortedDictionary<ulong, CryptoNote.Transaction>.value_type pair) =>
	{
		return getObjectHash(pair.second);
	});
	m_synchronizer.initTransactionPool(uncommitedTransactionsSet);
  }
  protected void loadSpendKeys()
  {
	bool isTrackingMode;
	for (uint i = 0; i < m_containerStorage.size(); ++i)
	{
	  WalletRecord wallet = new WalletRecord();
	  ulong creationTimestamp;
	  decryptKeyPair(m_containerStorage[i], wallet.spendPublicKey, wallet.spendSecretKey, ref creationTimestamp);
	  wallet.creationTimestamp = creationTimestamp;

	  if (i == 0)
	  {
		isTrackingMode = wallet.spendSecretKey == GlobalMembers.NULL_SECRET_KEY;
	  }
	  else if ((isTrackingMode && wallet.spendSecretKey != GlobalMembers.NULL_SECRET_KEY) || (!isTrackingMode && wallet.spendSecretKey == GlobalMembers.NULL_SECRET_KEY))
	  {
		throw std::system_error(GlobalMembers.make_error_code(error.BAD_ADDRESS), "All addresses must be whether tracking or not");
	  }

	  if (wallet.spendSecretKey != GlobalMembers.NULL_SECRET_KEY)
	  {
		throwIfKeysMismatch(wallet.spendSecretKey, wallet.spendPublicKey, "Restored spend public key doesn't correspond to secret key");
	  }
	  else
	  {
		if (!Crypto.check_key(wallet.spendPublicKey))
		{
		  throw std::system_error(GlobalMembers.make_error_code(error.WRONG_PASSWORD), "Public spend key is incorrect");
		}
	  }

	  wallet.actualBalance = 0;
	  wallet.pendingBalance = 0;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  wallet.container = reinterpret_cast<CryptoNote.ITransfersContainer>(i); //dirty hack. container field must be unique

	  m_walletsContainer.push_back(std::move(wallet));
	}
  }
  protected void loadContainerStorage(string path)
  {
	try
	{
	  m_containerStorage.open(path, FileMappedVectorOpenMode.OPEN, sizeof(ContainerStoragePrefix));

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ContainerStoragePrefix prefix = reinterpret_cast<ContainerStoragePrefix>(m_containerStorage.prefix());
	  Debug.Assert(prefix.version >= WalletSerializerV2.MIN_VERSION);

	  ulong creationTimestamp;
	  decryptKeyPair(prefix.encryptedViewKeys, m_viewPublicKey, m_viewSecretKey, ref creationTimestamp);
	  throwIfKeysMismatch(m_viewSecretKey, m_viewPublicKey, "Restored view public key doesn't correspond to secret key");
	  m_logger = new Logging.LoggerRef(m_logger.getLogger(), "WalletGreen/" + Common.GlobalMembers.podToHex(m_viewPublicKey).Substring(0, 5));

	  loadSpendKeys();

	  m_logger.functorMethod(DEBUGGING) << "Container keys were successfully loaded";
	}
	catch (System.Exception e)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to load container keys: " << e.Message;

	  m_walletsContainer.clear();
	  m_containerStorage.close();

	  throw;
	}
  }
  protected void loadWalletCache(ref HashSet<Crypto.PublicKey> addedKeys, ref HashSet<Crypto.PublicKey> deletedKeys, string extra)
  {
	Debug.Assert(m_containerStorage.isOpened());

	List<byte> contanerData = new List<byte>();
	loadAndDecryptContainerData(m_containerStorage, m_key, contanerData);

	WalletSerializerV2 s = new WalletSerializerV2(this, m_viewPublicKey, m_viewSecretKey, ref m_actualBalance, ref m_pendingBalance, m_walletsContainer, m_synchronizer, m_unlockTransactionsJob, m_transactions, m_transfers, m_uncommitedTransactions, extra, m_transactionSoftLockTime);

	Common.MemoryInputStream containerStream = new Common.MemoryInputStream(contanerData.data(), contanerData.Count);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	s.load(containerStream, reinterpret_cast<const ContainerStoragePrefix>(m_containerStorage.prefix()).version);
	addedKeys = std::move(s.addedKeys());
	deletedKeys = std::move(s.deletedKeys());

	m_logger.functorMethod(DEBUGGING) << "Container cache loaded";
  }
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void saveWalletCache(ContainerStorage storage, Crypto::chacha8_key key, WalletSaveLevel saveLevel, string extra);
  protected void subscribeWallets()
  {
	m_logger.functorMethod(DEBUGGING) << "Subscribing wallets...";

	try
	{
	  auto index = m_walletsContainer.get<RandomAccessIndex>();

	  uint counter = 0;
	  for (var it = index.begin(); it != index.end(); ++it)
	  {
//C++ TO C# CONVERTER TODO TASK: C# does not have an equivalent to references to variables:
//ORIGINAL LINE: const auto& wallet = *it;
		auto wallet = it;

		AccountSubscription sub = new AccountSubscription();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: sub.keys.address.viewPublicKey = m_viewPublicKey;
		sub.keys.address.viewPublicKey.CopyFrom(m_viewPublicKey);
		sub.keys.address.spendPublicKey = wallet.spendPublicKey;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: sub.keys.viewSecretKey = m_viewSecretKey;
		sub.keys.viewSecretKey.CopyFrom(m_viewSecretKey);
		sub.keys.spendSecretKey = wallet.spendSecretKey;
		sub.transactionSpendableAge = m_transactionSoftLockTime;
		sub.syncStart.height = 0;
		sub.syncStart.timestamp = wallet.creationTimestamp;

		auto subscription = m_synchronizer.addSubscription(sub);
		bool r = index.modify(it, (WalletRecord rec) =>
		{
			rec.container = subscription.getContainer();
		});
		if (r)
		{
		}
		Debug.Assert(r);

		subscription.addObserver(this);

		++counter;
		if (counter % 100 == 0)
		{
		  m_logger.functorMethod(DEBUGGING) << "Subscribed " << (int)counter << " wallets of " << m_walletsContainer.size();
		}
	  }
	}
	catch (System.Exception e)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to subscribe wallets: " << e.Message;

	  List<AccountPublicAddress> subscriptionList = new List<AccountPublicAddress>();
	  m_synchronizer.getSubscriptions(subscriptionList);
	  foreach (var subscription in subscriptionList)
	  {
		m_synchronizer.removeSubscription(subscription);
	  }

	  throw;
	}
  }

  protected List<WalletGreen.OutputToTransfer> pickRandomFusionInputs(List<string> addresses, ulong threshold, uint minInputCount, uint maxInputCount)
  {

	List<WalletGreen.OutputToTransfer> allFusionReadyOuts = new List<WalletGreen.OutputToTransfer>();
	var walletOuts = addresses.Count == 0 ? pickWalletsWithMoney() : pickWallets(addresses);
	List<uint> bucketSizes = new List<uint>(numeric_limits<ulong>.digits10 + 1);
	bucketSizes.fill(0);
	for (uint walletIndex = 0; walletIndex < walletOuts.Count; ++walletIndex)
	{
	  foreach (var @out in walletOuts[walletIndex].outs)
	  {
		byte powerOfTen = 0;
		if (m_currency.isAmountApplicableInFusionTransactionInput(@out.amount, threshold, ref powerOfTen, m_node.getLastKnownBlockHeight()))
		{
		  allFusionReadyOuts.Add({std::move(@out), walletOuts[walletIndex].wallet});
		  Debug.Assert(powerOfTen < numeric_limits<ulong>.digits10 + 1);
		  bucketSizes[powerOfTen]++;
		}
	  }
	}

	//now, pick the bucket
	List<byte> bucketNumbers = new List<byte>(bucketSizes.Count);
	std::iota(bucketNumbers.GetEnumerator(), bucketNumbers.end(), 0);
	std::shuffle(bucketNumbers.GetEnumerator(), bucketNumbers.end(), std::default_random_engine({Crypto.GlobalMembers.rand<std::default_random_engine.result_type>()}));
	uint bucketNumberIndex = 0;
	for (; bucketNumberIndex < bucketNumbers.Count; ++bucketNumberIndex)
	{
	  if (bucketSizes[bucketNumbers[bucketNumberIndex]] >= minInputCount)
	  {
		break;
	  }
	}

	if (bucketNumberIndex == bucketNumbers.Count)
	{
	  return new List<WalletGreen.OutputToTransfer>();
	}

	uint selectedBucket = bucketNumbers[bucketNumberIndex];
	Debug.Assert(selectedBucket < numeric_limits<ulong>.digits10 + 1);
	Debug.Assert(bucketSizes[selectedBucket] >= minInputCount);
	ulong lowerBound = 1;
	for (uint i = 0; i < selectedBucket; ++i)
	{
	  lowerBound *= 10;
	}

	ulong upperBound = selectedBucket == numeric_limits<ulong>.digits10 ? UINT64_MAX : lowerBound * 10;
	List<WalletGreen.OutputToTransfer> selectedOuts = new List<WalletGreen.OutputToTransfer>();
	selectedOuts.Capacity = bucketSizes[selectedBucket];
	for (uint outIndex = 0; outIndex < allFusionReadyOuts.Count; ++outIndex)
	{
	  if (allFusionReadyOuts[outIndex].@out.amount >= lowerBound != 0 && allFusionReadyOuts[outIndex].@out.amount < upperBound)
	  {
		selectedOuts.Add(std::move(allFusionReadyOuts[outIndex]));
	  }
	}

	Debug.Assert(selectedOuts.Count >= minInputCount);

//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
	var outputsSortingFunction = (OutputToTransfer l, OutputToTransfer r) =>
	{
		return l.@out.amount < r.@out.amount;
	};
	if (selectedOuts.Count <= maxInputCount)
	{
//C++ TO C# CONVERTER TODO TASK: The 'Compare' parameter of std::sort produces a boolean value, while the .NET Comparison parameter produces a tri-state result:
//ORIGINAL LINE: std::sort(selectedOuts.begin(), selectedOuts.end(), outputsSortingFunction);
	  selectedOuts.Sort(outputsSortingFunction);
	  return selectedOuts;
	}

	ShuffleGenerator<uint, Crypto.random_engine<uint>> generator = new ShuffleGenerator<uint, Crypto.random_engine<uint>>(selectedOuts.Count);
	List<WalletGreen.OutputToTransfer> trimmedSelectedOuts = new List<WalletGreen.OutputToTransfer>();
	trimmedSelectedOuts.Capacity = maxInputCount;
	for (uint i = 0; i < maxInputCount; ++i)
	{
	  trimmedSelectedOuts.Add(std::move(selectedOuts[generator.functorMethod()]));
	}

//C++ TO C# CONVERTER TODO TASK: The 'Compare' parameter of std::sort produces a boolean value, while the .NET Comparison parameter produces a tri-state result:
//ORIGINAL LINE: std::sort(trimmedSelectedOuts.begin(), trimmedSelectedOuts.end(), outputsSortingFunction);
	trimmedSelectedOuts.Sort(outputsSortingFunction);
	return trimmedSelectedOuts;
  }
  protected static WalletGreen.ReceiverAmounts decomposeFusionOutputs(AccountPublicAddress address, ulong inputsAmount)
  {
	WalletGreen.ReceiverAmounts outputs = new WalletGreen.ReceiverAmounts();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: outputs.receiver = address;
	outputs.receiver.CopyFrom(address);

	decomposeAmount(inputsAmount, 0, outputs.amounts);
	outputs.amounts.Sort();

	return outputs;
  }

  protected enum WalletState
  {
	INITIALIZED,
	NOT_INITIALIZED
  }

  protected enum WalletTrackingMode
  {
	TRACKING,
	NOT_TRACKING,
	NO_ADDRESSES
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: WalletGreen::WalletTrackingMode getTrackingMode() const
  protected WalletGreen.WalletTrackingMode getTrackingMode()
  {
	if (m_walletsContainer.get<RandomAccessIndex>().empty())
	{
	  return WalletTrackingMode.NO_ADDRESSES;
	}

	return m_walletsContainer.get<RandomAccessIndex>().begin().spendSecretKey == GlobalMembers.NULL_SECRET_KEY != null ? WalletTrackingMode.TRACKING : WalletTrackingMode.NOT_TRACKING;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: System.Tuple<WalletTransfers::const_iterator, WalletTransfers::const_iterator> getTransactionTransfersRange(uint transactionIndex) const
  protected Tuple<WalletTransfers.const_iterator, WalletTransfers.const_iterator> getTransactionTransfersRange(uint transactionIndex)
  {
	var val = Tuple.Create(transactionIndex, new WalletTransfer());

//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
	var bounds = std::equal_range(m_transfers.begin(), m_transfers.end(), val, (Tuple<ulong, CryptoNote.WalletTransfer> a, Tuple<ulong, CryptoNote.WalletTransfer> b) =>
	{
	  return a.Item1 < b.Item1;
	});

	return bounds;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<TransactionsInBlockInfo> getTransactionsInBlocks(uint blockIndex, uint count) const
  protected List<TransactionsInBlockInfo> getTransactionsInBlocks(uint blockIndex, uint count)
  {
	if (count == 0)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Bad argument: block count must be greater than zero";
	  throw std::system_error(GlobalMembers.make_error_code(error.WRONG_PARAMETERS), "blocks count must be greater than zero");
	}

	if (blockIndex == 0)
	{
	  m_logger.functorMethod(ERROR, BRIGHT_RED) << "Bad argument: blockIndex must be greater than zero";
	  throw std::system_error(GlobalMembers.make_error_code(error.WRONG_PARAMETERS), "blockIndex must be greater than zero");
	}

	List<TransactionsInBlockInfo> result = new List<TransactionsInBlockInfo>();

	if (blockIndex >= m_blockchain.size())
	{
	  return result;
	}

	auto blockHeightIndex = m_transactions.get<BlockHeightIndex>();
	uint stopIndex = (uint)Math.Min(m_blockchain.size(), blockIndex + count);

	for (uint height = blockIndex; height < stopIndex; ++height)
	{
	  TransactionsInBlockInfo info = new TransactionsInBlockInfo();
	  info.blockHash = m_blockchain[height - 1];

	  var lowerBound = blockHeightIndex.lower_bound(height);
	  var upperBound = blockHeightIndex.upper_bound(height);
	  for (var it = lowerBound; it != upperBound; ++it)
	  {
		if (it.state != WalletTransactionState.SUCCEEDED)
		{
		  continue;
		}

		WalletTransactionWithTransfers transaction = new WalletTransactionWithTransfers();
		transaction.transaction = it;

		transaction.transfers = new List<WalletTransfer>(getTransactionTransfers(*it));

		info.transactions.emplace_back(std::move(transaction));
	  }

	  result.emplace_back(std::move(info));
	}

	return result;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Crypto::Hash getBlockHashByIndex(uint blockIndex) const
  protected Crypto.Hash getBlockHashByIndex(uint blockIndex)
  {
	Debug.Assert(blockIndex < m_blockchain.size());
	return m_blockchain.get<BlockHeightIndex>()[blockIndex];
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<WalletTransfer> getTransactionTransfers(const WalletTransaction& transaction) const
  protected List<WalletTransfer> getTransactionTransfers(WalletTransaction transaction)
  {
	auto transactionIdIndex = m_transactions.get<RandomAccessIndex>();

	var it = transactionIdIndex.iterator_to(transaction);
	Debug.Assert(it != transactionIdIndex.end());

	uint transactionId = std::distance(transactionIdIndex.begin(), it);
	var bounds = getTransactionTransfersRange(transactionId);

	List<WalletTransfer> result = new List<WalletTransfer>();
	result.Capacity = std::distance(bounds.Item1, bounds.Item2);

	for (var it = bounds.Item1; it != bounds.Item2; ++it)
	{
	  result.emplace_back(it.second);
	}

	return result;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void filterOutTransactions(WalletTransactions& transactions, WalletTransfers& transfers, System.Func<const WalletTransaction&, bool>&& pred) const;
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void filterOutTransactions(WalletTransactions transactions, WalletTransfers transfers, System.Func<WalletTransaction&, bool>&& pred);
  protected void initBlockchain(Crypto.PublicKey viewPublicKey)
  {
	List<Crypto.Hash> blockchain = m_synchronizer.getViewKeyKnownBlocks(m_viewPublicKey);
	m_blockchain.insert(m_blockchain.end(), blockchain.GetEnumerator(), blockchain.end());
  }

  ///pre: changeDestinationAddress belongs to current container
  ///pre: source address belongs to current container
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: CryptoNote::AccountPublicAddress getChangeDestination(const string& changeDestinationAddress, const ClassicVector<string>& sourceAddresses) const
  protected CryptoNote.AccountPublicAddress getChangeDestination(string changeDestinationAddress, List<string> sourceAddresses)
  {
	if (!string.IsNullOrEmpty(changeDestinationAddress))
	{
	  return parseAccountAddressString(changeDestinationAddress);
	}

	if (m_walletsContainer.size() == 1)
	{
	  return AccountPublicAddress {m_walletsContainer.get<RandomAccessIndex>()[0].spendPublicKey, m_viewPublicKey};
	}

	Debug.Assert(sourceAddresses.Count == 1 && isMyAddress(sourceAddresses[0]));
	return parseAccountAddressString(sourceAddresses[0]);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isMyAddress(const string& addressString) const
  protected bool isMyAddress(string addressString)
  {
	CryptoNote.AccountPublicAddress address = parseAccountAddressString(addressString);
	return m_viewPublicKey == address.viewPublicKey && m_walletsContainer.get<KeysIndex>().count(address.spendPublicKey) != 0;
  }

  protected void deleteContainerFromUnlockTransactionJobs(ITransfersContainer container)
  {
	for (var it = m_unlockTransactionsJob.begin(); it != m_unlockTransactionsJob.end();)
	{
	  if (it.container == container)
	  {
		it = m_unlockTransactionsJob.erase(it);
	  }
	  else
	  {
		++it;
	  }
	}
  }
  protected List<uint> deleteTransfersForAddress(string address, List<uint> deletedTransactions)
  {
	Debug.Assert(!string.IsNullOrEmpty(address));

	long deletedInputs = 0;
	long deletedOutputs = 0;

	long unknownInputs = 0;

	bool transfersLeft = false;
	uint firstTransactionTransfer = 0;

	List<uint> updatedTransactions = new List<uint>();

	for (uint i = 0; i < m_transfers.size(); ++i)
	{
	  WalletTransfer transfer = m_transfers[i].second;

	  if (transfer.address == address)
	  {
		if (transfer.amount >= 0)
		{
		  deletedOutputs += transfer.amount;
		}
		else
		{
		  deletedInputs += transfer.amount;
		  transfer.address = "";
		}
	  }
	  else if (string.IsNullOrEmpty(transfer.address))
	  {
		if (transfer.amount < 0)
		{
		  unknownInputs += transfer.amount;
		}
	  }
	  else if (isMyAddress(transfer.address))
	  {
		transfersLeft = true;
	  }

	  uint transactionId = m_transfers[i].first;
	  if ((i == m_transfers.size() - 1) || (transactionId != m_transfers[i + 1].first))
	  {
		//the last transfer for current transaction

		uint transfersBeforeMerge = m_transfers.size();
		if (deletedInputs != 0)
		{
		  adjustTransfer(transactionId, firstTransactionTransfer, "", deletedInputs + unknownInputs);
		}

		Debug.Assert(transfersBeforeMerge >= m_transfers.size());
		i -= transfersBeforeMerge - m_transfers.size();

		auto randomIndex = m_transactions.get<RandomAccessIndex>();

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: randomIndex.modify(std::next(randomIndex.begin(), transactionId), [this, transactionId, transfersLeft, deletedInputs, deletedOutputs] (WalletTransaction& transaction)
		randomIndex.modify(std::next(randomIndex.begin(), transactionId), (WalletTransaction transaction) =>
		{
		  transaction.totalAmount -= deletedInputs + deletedOutputs;

		  if (!transfersLeft)
		  {
			transaction.state = WalletTransactionState.DELETED;
			transaction.blockHeight = WALLET_UNCONFIRMED_TRANSACTION_HEIGHT;
			m_logger.functorMethod(DEBUGGING) << "Transaction state changed, ID " << (int)transactionId << ", hash " << transaction.hash << ", new state " << transaction.state;
		  }
		});

		if (!transfersLeft)
		{
		  deletedTransactions.Add(transactionId);
		}

		if (deletedInputs != 0 || deletedOutputs != 0)
		{
		  updatedTransactions.Add(transactionId);
		}

		//reset values for next transaction
		deletedInputs = 0;
		deletedOutputs = 0;
		unknownInputs = 0;
		transfersLeft = false;
		firstTransactionTransfer = i + 1;
	  }
	}

	return updatedTransactions;
  }
  protected void deleteFromUncommitedTransactions(List<uint> deletedTransactions)
  {
	foreach (var transactionId in deletedTransactions)
	{
	  m_uncommitedTransactions.erase(transactionId);
	}
  }

  protected System.Dispatcher m_dispatcher;
  protected readonly Currency m_currency;
  protected INode m_node;
  protected Logging.LoggerRef m_logger = new Logging.LoggerRef();
  protected bool m_stopped;

  protected WalletsContainer m_walletsContainer = new WalletsContainer();
  protected ContainerStorage m_containerStorage = new ContainerStorage();
  protected UnlockTransactionJobs m_unlockTransactionsJob = new UnlockTransactionJobs();
  protected WalletTransactions m_transactions = new WalletTransactions();
  protected WalletTransfers m_transfers = new WalletTransfers(); //sorted
  protected Dictionary<uint, bool> m_fusionTxsCache = new Dictionary<uint, bool>(); // txIndex -> isFusion
  protected UncommitedTransactions m_uncommitedTransactions = new UncommitedTransactions();

  protected bool m_blockchainSynchronizerStarted;
  protected BlockchainSynchronizer m_blockchainSynchronizer = new BlockchainSynchronizer();
  protected TransfersSyncronizer m_synchronizer = new TransfersSyncronizer();

  protected System.Event m_eventOccurred = new System.Event();
  protected Queue<WalletEvent> m_events = new Queue<WalletEvent>();
  protected System.Event m_readyEvent = new System.Event();

  protected WalletState m_state;

  protected string m_password;
  protected Crypto.chacha8_key m_key = new Crypto.chacha8_key();
  protected string m_path;
  protected string m_extra; // workaround for wallet reset

  protected Crypto.PublicKey m_viewPublicKey = new Crypto.PublicKey();
  protected Crypto.SecretKey m_viewSecretKey = new Crypto.SecretKey();

  protected ulong m_actualBalance;
  protected ulong m_pendingBalance;

  protected uint m_transactionSoftLockTime;

  protected BlockHashesContainer m_blockchain = new BlockHashesContainer();

//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' function:
//ORIGINAL LINE: friend std::ostream& operator <<(std::ostream& os, CryptoNote::WalletGreen::WalletState state);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::ostream operator <<(std::ostream os, CryptoNote::WalletGreen::WalletState state);
//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' function:
//ORIGINAL LINE: friend std::ostream& operator <<(std::ostream& os, CryptoNote::WalletGreen::WalletTrackingMode mode);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  std::ostream operator <<(std::ostream os, CryptoNote::WalletGreen::WalletTrackingMode mode);
//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' class:
//  friend class TransferListFormatter;
}

} //namespace CryptoNote

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace


namespace CryptoNote
{

//C++ TO C# CONVERTER WARNING: The original C++ declaration of the following method implementation was not found:
//ORIGINAL LINE: void WalletGreen::saveWalletCache(Common::FileMappedVector<EncryptedWalletRecord>& storage, const Crypto::chacha8_key& key, WalletSaveLevel saveLevel, const string& extra)

//C++ TO C# CONVERTER WARNING: The original C++ declaration of the following method implementation was not found:
//ORIGINAL LINE: void WalletGreen::copyContainerStorageKeys(Common::FileMappedVector<EncryptedWalletRecord>& src, const chacha8_key& srcKey, Common::FileMappedVector<EncryptedWalletRecord>& dst, const chacha8_key& dstKey)

//C++ TO C# CONVERTER WARNING: The original C++ declaration of the following method implementation was not found:
//ORIGINAL LINE: void WalletGreen::copyContainerStoragePrefix(Common::FileMappedVector<EncryptedWalletRecord>& src, const chacha8_key& srcKey, Common::FileMappedVector<EncryptedWalletRecord>& dst, const chacha8_key& dstKey)

//C++ TO C# CONVERTER WARNING: The original C++ declaration of the following method implementation was not found:
//ORIGINAL LINE: void WalletGreen::encryptAndSaveContainerData(Common::FileMappedVector<EncryptedWalletRecord>& storage, const Crypto::chacha8_key& key, const object* containerData, uint containerDataSize)

//C++ TO C# CONVERTER WARNING: The original C++ declaration of the following method implementation was not found:
//ORIGINAL LINE: void WalletGreen::loadAndDecryptContainerData(Common::FileMappedVector<EncryptedWalletRecord>& storage, const Crypto::chacha8_key& key, ClassicVector<byte>& containerData)

//C++ TO C# CONVERTER WARNING: The original C++ declaration of the following method implementation was not found:
//ORIGINAL LINE: uint WalletGreen::insertOutgoingTransactionAndPushEvent(const Hash& transactionHash, ulong fee, const ClassicVector<byte>& extra, ulong unlockTimestamp)

private void WalletGreen.pushEvent(const WalletEvent & event)
{
  m_events.push(event);
  m_eventOccurred.set();
}

//C++ TO C# CONVERTER WARNING: The original C++ declaration of the following method implementation was not found:
//ORIGINAL LINE: void WalletGreen::filterOutTransactions(boost::multi_index_container < CryptoNote::WalletTransaction, boost::multi_index::indexed_by < boost::multi_index::random_access < boost::multi_index::tag <RandomAccessIndex>>, boost::multi_index::hashed_unique < boost::multi_index::tag <TransactionIndex>, boost::multi_index::member<CryptoNote::WalletTransaction, Crypto::Hash, &CryptoNote::WalletTransaction::hash >>, boost::multi_index::ordered_non_unique < boost::multi_index::tag <BlockHeightIndex>, boost::multi_index::member<CryptoNote::WalletTransaction, uint, &CryptoNote::WalletTransaction::blockHeight >> >>& transactions, ClassicVector<System.Tuple<ulong, CryptoNote::WalletTransfer>>& transfers, System.Func<const WalletTransaction&, bool>&& pred) const
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:

} //namespace CryptoNote


namespace CryptoNote
{
public partial class WalletGreen
{
	public void saveWalletCache(Common.FileMappedVector<EncryptedWalletRecord> storage, Crypto.chacha8_key key, WalletSaveLevel saveLevel, string extra)
	{
	  m_logger(DEBUGGING) << "Saving cache...";
    
	  multi_index_container< CryptoNote.WalletTransaction, boost::multi_index.indexed_by < boost::multi_index.random_access < boost::multi_index.tag <RandomAccessIndex>>, boost::multi_index.hashed_unique < boost::multi_index.tag <TransactionIndex>, boost::multi_index.member<CryptoNote.WalletTransaction, Crypto.Hash, CryptoNote.WalletTransaction.hash >>, boost::multi_index.ordered_non_unique < boost::multi_index.tag <BlockHeightIndex>, boost::multi_index.member<CryptoNote.WalletTransaction, uint, CryptoNote.WalletTransaction.blockHeight >> >> transactions = new multi_index_container< CryptoNote.WalletTransaction, boost::multi_index.indexed_by < boost::multi_index.random_access < boost::multi_index.tag <RandomAccessIndex>>, boost::multi_index.hashed_unique < boost::multi_index.tag <TransactionIndex>, boost::multi_index.member<CryptoNote.WalletTransaction, Crypto.Hash, CryptoNote.WalletTransaction.hash >>, boost::multi_index.ordered_non_unique < boost::multi_index.tag <BlockHeightIndex>, boost::multi_index.member<CryptoNote.WalletTransaction, uint, CryptoNote.WalletTransaction.blockHeight >> >>();
	  List<Tuple<ulong, CryptoNote.WalletTransfer>> transfers = new List<Tuple<ulong, CryptoNote.WalletTransfer>>();
    
	  if (saveLevel == WalletSaveLevel.SAVE_KEYS_AND_TRANSACTIONS)
	  {
		filterOutTransactions(transactions, transfers, (WalletTransaction tx) =>
		{
		  return tx.state == WalletTransactionState.CREATED || tx.state == WalletTransactionState.DELETED;
		});
    
		for (var it = transactions.begin(); it != transactions.end(); ++it)
		{
		  transactions.modify(it, (WalletTransaction tx) =>
		  {
			tx.state = WalletTransactionState.CANCELLED;
			tx.blockHeight = WALLET_UNCONFIRMED_TRANSACTION_HEIGHT;
		  });
		}
	  }
	  else if (saveLevel == WalletSaveLevel.SAVE_ALL)
	  {
		filterOutTransactions(transactions, transfers, (WalletTransaction tx) =>
		{
		  return tx.state == WalletTransactionState.DELETED;
		});
	  }
    
	  string containerData;
	  Common.StringOutputStream containerStream = new Common.StringOutputStream(containerData);
    
	//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
	  WalletSerializerV2 s = new WalletSerializerV2(this, m_viewPublicKey, m_viewSecretKey, ref m_actualBalance, ref m_pendingBalance, m_walletsContainer, m_synchronizer, m_unlockTransactionsJob, transactions, transfers, m_uncommitedTransactions, const_cast<string&>(extra), m_transactionSoftLockTime);
    
	  s.save(containerStream, saveLevel);
    
	  encryptAndSaveContainerData(storage, key, containerData.data(), containerData.Length);
	  storage.flush();
    
	  m_extra = extra;
    
	  m_logger(DEBUGGING) << "Container saving finished";
	}
	public void copyContainerStorageKeys(Common.FileMappedVector<EncryptedWalletRecord> src, chacha8_key srcKey, Common.FileMappedVector<EncryptedWalletRecord> dst, chacha8_key dstKey)
	{
	  m_logger(DEBUGGING) << "Copying wallet keys...";
	  dst.reserve(src.size());
    
	  dst.setAutoFlush(false);
	  Tools.ScopeExit exitHandler(() =>
	  {
		dst.setAutoFlush(true);
		dst.flush();
	  });
    
	  uint counter = 0;
	  foreach (var encryptedSpendKeys in src)
	  {
		Crypto.PublicKey publicKey = new Crypto.PublicKey();
		Crypto.SecretKey secretKey = new Crypto.SecretKey();
		ulong creationTimestamp;
		decryptKeyPair(encryptedSpendKeys, publicKey, secretKey, creationTimestamp, srcKey);
    
		// push_back() can resize container, and dstPrefix address can be changed, so it is requested for each key pair
	//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		ContainerStoragePrefix dstPrefix = reinterpret_cast<ContainerStoragePrefix>(dst.prefix());
		Crypto.chacha8_iv keyPairIv = dstPrefix.nextIv;
		incIv(dstPrefix.nextIv);
    
		dst.push_back(encryptKeyPair(publicKey, secretKey, creationTimestamp, dstKey, keyPairIv));
    
		++counter;
		if (counter % 100 == 0)
		{
		  m_logger(DEBUGGING) << "Copied keys: " << (int)counter << " / " << (int)src.size();
		}
	  }
    
	  m_logger(DEBUGGING) << "Keys copied";
	}
	public void copyContainerStoragePrefix(Common.FileMappedVector<EncryptedWalletRecord> src, chacha8_key srcKey, Common.FileMappedVector<EncryptedWalletRecord> dst, chacha8_key dstKey)
	{
	//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ContainerStoragePrefix srcPrefix = reinterpret_cast<ContainerStoragePrefix>(src.prefix());
	//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ContainerStoragePrefix dstPrefix = reinterpret_cast<ContainerStoragePrefix>(dst.prefix());
	  dstPrefix.version = srcPrefix.version;
	  dstPrefix.nextIv = Crypto.GlobalMembers.rand<chacha8_iv>();
    
	  Crypto.PublicKey publicKey = new Crypto.PublicKey();
	  Crypto.SecretKey secretKey = new Crypto.SecretKey();
	  ulong creationTimestamp;
	  decryptKeyPair(srcPrefix.encryptedViewKeys, publicKey, secretKey, creationTimestamp, srcKey);
	  dstPrefix.encryptedViewKeys = encryptKeyPair(publicKey, secretKey, creationTimestamp, dstKey, dstPrefix.nextIv);
	  incIv(dstPrefix.nextIv);
	}
	public void encryptAndSaveContainerData(Common.FileMappedVector<EncryptedWalletRecord> storage, Crypto.chacha8_key key, object containerData, uint containerDataSize)
	{
	//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ContainerStoragePrefix prefix = reinterpret_cast<ContainerStoragePrefix>(storage.prefix());
    
	  Crypto.chacha8_iv suffixIv = prefix.nextIv;
	  incIv(prefix.nextIv);
    
	  List<byte> encryptedContainer = new List<byte>();
	  encryptedContainer.Resize(containerDataSize);
	//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
	//ORIGINAL LINE: chacha8(containerData, containerDataSize, key, suffixIv, reinterpret_cast<char*>(encryptedContainer.data()));
	  Crypto.GlobalMembers.chacha8(containerData, containerDataSize, new Crypto.chacha8_key(key), new Crypto.chacha8_iv(suffixIv), ref reinterpret_cast<char>(encryptedContainer.data()));
    
	  string suffix;
	  Common.StringOutputStream suffixStream = new Common.StringOutputStream(suffix);
	  BinaryOutputStreamSerializer suffixSerializer = new BinaryOutputStreamSerializer(suffixStream);
	  suffixSerializer.functorMethod(suffixIv, "suffixIv");
	  suffixSerializer.functorMethod(encryptedContainer, "encryptedContainer");
    
	  storage.resizeSuffix(suffix.Length);
	  std::copy(suffix.GetEnumerator(), suffix.end(), storage.suffix());
	}
	public void loadAndDecryptContainerData(Common.FileMappedVector<EncryptedWalletRecord> storage, Crypto.chacha8_key key, List<byte> containerData)
	{
	  Common.MemoryInputStream suffixStream = new Common.MemoryInputStream(storage.suffix(), storage.suffixSize());
	  BinaryInputStreamSerializer suffixSerializer = new BinaryInputStreamSerializer(suffixStream);
	  Crypto.chacha8_iv suffixIv = new Crypto.chacha8_iv();
	  List<byte> encryptedContainer = new List<byte>();
	  suffixSerializer.functorMethod(suffixIv, "suffixIv");
	  suffixSerializer.functorMethod(encryptedContainer, "encryptedContainer");
    
	  containerData.Resize(encryptedContainer.Count);
	//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
	//ORIGINAL LINE: chacha8(encryptedContainer.data(), encryptedContainer.size(), key, suffixIv, reinterpret_cast<char*>(containerData.data()));
	  Crypto.GlobalMembers.chacha8(encryptedContainer.data(), encryptedContainer.Count, new Crypto.chacha8_key(key), new Crypto.chacha8_iv(suffixIv), ref reinterpret_cast<char>(containerData.data()));
	}
	public uint insertOutgoingTransactionAndPushEvent(Hash transactionHash, ulong fee, List<byte> extra, ulong unlockTimestamp)
	{
	  WalletTransaction insertTx = new WalletTransaction();
	  insertTx.state = WalletTransactionState.CREATED;
	  insertTx.creationTime = (ulong)time(null);
	  insertTx.unlockTime = unlockTimestamp;
	  insertTx.blockHeight = CryptoNote.GlobalMembers.WALLET_UNCONFIRMED_TRANSACTION_HEIGHT;
	//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  insertTx.extra.assign(reinterpret_cast<const char>(extra.data()), extra.Count);
	  insertTx.fee = fee;
	//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
	//ORIGINAL LINE: insertTx.hash = transactionHash;
	  insertTx.hash.CopyFrom(transactionHash);
	  insertTx.totalAmount = 0; // 0 until transactionHandlingEnd() is called
	  insertTx.timestamp = 0; //0 until included in a block
	  insertTx.isBase = false;
    
	  uint txId = m_transactions.get<RandomAccessIndex>().size();
	  m_transactions.get<RandomAccessIndex>().push_back(std::move(insertTx));
    
	  pushEvent(GlobalMembers.makeTransactionCreatedEvent(txId));
    
	  return txId;
	}
	public void filterOutTransactions(boost::multi_index_container < CryptoNote.WalletTransaction, boost::multi_index.indexed_by < boost::multi_index.random_access < boost::multi_index.tag <RandomAccessIndex>>, boost::multi_index.hashed_unique < boost::multi_index.tag <TransactionIndex>, boost::multi_index.member<CryptoNote.WalletTransaction, Crypto.Hash, CryptoNote.WalletTransaction.hash >>, boost::multi_index.ordered_non_unique < boost::multi_index.tag <BlockHeightIndex>, boost::multi_index.member<CryptoNote.WalletTransaction, uint, CryptoNote.WalletTransaction.blockHeight >> >>& transactions, List<Tuple<ulong, CryptoNote.WalletTransfer>> transfers, Func<WalletTransaction&, bool>&& pred)
	{
	  uint cancelledTransactions = 0;
    
	  transactions.reserve(m_transactions.size());
	  transfers.Capacity = m_transfers.size();
    
	  auto index = m_transactions.get<RandomAccessIndex>();
	  uint transferIdx = 0;
	  for (uint i = 0; i < m_transactions.size(); ++i)
	  {
		WalletTransaction transaction = index[i];
    
		if (pred(transaction))
		{
		  ++cancelledTransactions;
    
		  while (transferIdx < m_transfers.size() && m_transfers[transferIdx].first == i)
		  {
			++transferIdx;
		  }
		}
		else
		{
		  transactions.push_back(transaction);
    
		  while (transferIdx < m_transfers.size() && m_transfers[transferIdx].first == i)
		  {
			transfers.emplace_back(i - cancelledTransactions, m_transfers[transferIdx].second);
			++transferIdx;
		  }
		}
	  }
	}
}
}