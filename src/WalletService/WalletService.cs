using System;
using System.Collections.Generic;
using System.Diagnostics;

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
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
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl


namespace CryptoNote
{
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class IFusionManager;
}

namespace PaymentService
{

public class WalletConfiguration
{
  public string walletFile;
  public string walletPassword;
  public bool syncFromZero;
  public string secretViewKey;
  public string secretSpendKey;
  public string mnemonicSeed;
  public ulong scanHeight;
}

public class WalletService : System.IDisposable
{
  public WalletService(CryptoNote.Currency currency, System.Dispatcher sys, CryptoNote.INode node, CryptoNote.IWallet wallet, CryptoNote.IFusionManager fusionManager, WalletConfiguration conf, Logging.ILogger logger)
  {
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
	  this.currency = new CryptoNote.Currency(currency);
	  this.wallet = new CryptoNote.IWallet(wallet);
	  this.fusionManager = new CryptoNote.IFusionManager(fusionManager);
	  this.node = new CryptoNote.INode(node);
	  this.config = new PaymentService.WalletConfiguration(conf);
	  this.inited = false;
	  this.logger = new Logging.LoggerRef(logger, "WalletService");
	  this.dispatcher = sys;
	  this.readyEvent = dispatcher;
	  this.refreshContext = dispatcher;
	readyEvent.set();
  }
  public virtual void Dispose()
  {
	if (inited)
	{
	  wallet.stop();
	  refreshContext.wait();
	  wallet.shutdown();
	}
  }

  public void init()
  {
	loadWallet();
	loadTransactionIdIndex();

	getNodeFee();
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: refreshContext.spawn([this]
	refreshContext.spawn(() =>
	{
		refresh();
	});

	inited = true;
  }
  public void saveWallet()
  {
	wallet.save();
	logger.functorMethod(Logging.Level.INFO, Logging.BRIGHT_WHITE) << "Wallet is saved";
  }

  public std::error_code saveWalletNoThrow()
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

	  logger.functorMethod(Logging.Level.INFO, Logging.BRIGHT_WHITE) << "Saving wallet...";

	  if (!inited)
	  {
		logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Save impossible: Wallet Service is not initialized";
		return GlobalMembers.make_error_code(CryptoNote.error.NOT_INITIALIZED);
	  }

	  saveWallet();
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while saving wallet: " << x.what();
	  return x.code();
	}
	catch (System.Exception x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while saving wallet: " << x.Message;
	  return GlobalMembers.make_error_code(CryptoNote.error.INTERNAL_WALLET_ERROR);
	}

	return std::error_code();
  }
  public std::error_code exportWallet(string fileName)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

	  if (!inited)
	  {
		logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Export impossible: Wallet Service is not initialized";
		return GlobalMembers.make_error_code(CryptoNote.error.NOT_INITIALIZED);
	  }

	  boost::filesystem.path walletPath = new boost::filesystem.path(config.walletFile);
	  boost::filesystem.path exportPath = walletPath.parent_path() / fileName;

	  logger.functorMethod(Logging.Level.INFO, Logging.BRIGHT_WHITE) << "Exporting wallet to " << exportPath.string();
	  wallet.exportWallet(exportPath.string());
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while exporting wallet: " << x.what();
	  return x.code();
	}
	catch (System.Exception x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while exporting wallet: " << x.Message;
	  return GlobalMembers.make_error_code(CryptoNote.error.INTERNAL_WALLET_ERROR);
	}

	return std::error_code();
  }
  public std::error_code resetWallet(ulong scanHeight)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

	  logger.functorMethod(Logging.Level.INFO, Logging.BRIGHT_WHITE) << "Resetting wallet";

	  if (!inited)
	  {
		logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Reset impossible: Wallet Service is not initialized";
		return GlobalMembers.make_error_code(CryptoNote.error.NOT_INITIALIZED);
	  }

	  reset(scanHeight);
	  logger.functorMethod(Logging.Level.INFO, Logging.BRIGHT_WHITE) << "Wallet has been reset";
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while resetting wallet: " << x.what();
	  return x.code();
	}
	catch (System.Exception x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while resetting wallet: " << x.Message;
	  return GlobalMembers.make_error_code(CryptoNote.error.INTERNAL_WALLET_ERROR);
	}

	return std::error_code();
  }
  public std::error_code createAddress(string spendSecretKeyText, ulong scanHeight, bool newAddress, ref string address)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

	  logger.functorMethod(Logging.Level.DEBUGGING) << "Creating address";

	  Crypto.SecretKey secretKey = new Crypto.SecretKey();
	  if (!Common.GlobalMembers.podFromHex(spendSecretKeyText, secretKey))
	  {
		logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Wrong key format: " << spendSecretKeyText;
		return GlobalMembers.make_error_code(CryptoNote.error.WalletServiceErrorCode.WRONG_KEY_FORMAT);
	  }

	  address = wallet.createAddress(secretKey, scanHeight, newAddress);
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while creating address: " << x.what();
	  return x.code();
	}

	logger.functorMethod(Logging.Level.DEBUGGING) << "Created address " << address;

	return std::error_code();
  }
  public std::error_code createAddressList(List<string> spendSecretKeysText, ulong scanHeight, bool newAddress, ref List<string> addresses)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

	  logger.functorMethod(Logging.Level.DEBUGGING) << "Creating " << spendSecretKeysText.Count << " addresses...";

	  List<Crypto.SecretKey> secretKeys = new List<Crypto.SecretKey>();
	  HashSet<string> unique = new HashSet<string>();
	  secretKeys.Capacity = spendSecretKeysText.Count;
	  unique.reserve(spendSecretKeysText.Count);
	  foreach (var keyText in spendSecretKeysText)
	  {
		var insertResult = unique.Add(keyText);
		if (!insertResult.second)
		{
		  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Not unique key";
		  return GlobalMembers.make_error_code(CryptoNote.error.WalletServiceErrorCode.DUPLICATE_KEY);
		}

		Crypto.SecretKey key = new Crypto.SecretKey();
		if (!Common.GlobalMembers.podFromHex(keyText, key))
		{
		  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Wrong key format: " << keyText;
		  return GlobalMembers.make_error_code(CryptoNote.error.WalletServiceErrorCode.WRONG_KEY_FORMAT);
		}

		secretKeys.Add(std::move(key));
	  }

	  addresses = wallet.createAddressList(secretKeys, scanHeight, newAddress);
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while creating addresses: " << x.what();
	  return x.code();
	}

	logger.functorMethod(Logging.Level.DEBUGGING) << "Created " << addresses.Count << " addresses";

	return std::error_code();
  }
  public std::error_code createAddress(ref string address)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

	  logger.functorMethod(Logging.Level.DEBUGGING) << "Creating address";

	  address = wallet.createAddress();
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while creating address: " << x.what();
	  return x.code();
	}

	logger.functorMethod(Logging.Level.DEBUGGING) << "Created address " << address;

	return std::error_code();
  }
  public std::error_code createTrackingAddress(string spendPublicKeyText, ulong scanHeight, bool newAddress, ref string address)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

	  logger.functorMethod(Logging.Level.DEBUGGING) << "Creating tracking address";

	  Crypto.PublicKey publicKey = new Crypto.PublicKey();
	  if (!Common.GlobalMembers.podFromHex(spendPublicKeyText, publicKey))
	  {
		logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Wrong key format: " << spendPublicKeyText;
		return GlobalMembers.make_error_code(CryptoNote.error.WalletServiceErrorCode.WRONG_KEY_FORMAT);
	  }

	  address = wallet.createAddress(publicKey, scanHeight, true);
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while creating tracking address: " << x.what();
	  return x.code();
	}

	logger.functorMethod(Logging.Level.DEBUGGING) << "Created address " << address;
	return std::error_code();
  }
  public std::error_code deleteAddress(string address)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

	  logger.functorMethod(Logging.Level.DEBUGGING) << "Delete address request came";
	  wallet.deleteAddress(address);
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while deleting address: " << x.what();
	  return x.code();
	}

	logger.functorMethod(Logging.Level.DEBUGGING) << "Address " << address << " successfully deleted";
	return std::error_code();
  }
  public std::error_code getSpendkeys(string address, ref string publicSpendKeyText, ref string secretSpendKeyText)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

	  CryptoNote.KeyPair key = wallet.getAddressSpendKey(address);

	  publicSpendKeyText = Common.GlobalMembers.podToHex(key.publicKey);
	  secretSpendKeyText = Common.GlobalMembers.podToHex(key.secretKey);

	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting spend key: " << x.what();
	  return x.code();
	}

	return std::error_code();
  }
  public std::error_code getBalance(string address, ref ulong availableBalance, ref ulong lockedAmount)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);
	  logger.functorMethod(Logging.Level.DEBUGGING) << "Getting balance for address " << address;

	  availableBalance = wallet.getActualBalance(address);
	  lockedAmount = wallet.getPendingBalance(address);
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting balance: " << x.what();
	  return x.code();
	}

	logger.functorMethod(Logging.Level.DEBUGGING) << address << " actual balance: " << (int)availableBalance << ", pending: " << (int)lockedAmount;
	return std::error_code();
  }
  public std::error_code getBalance(ref ulong availableBalance, ref ulong lockedAmount)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);
	  logger.functorMethod(Logging.Level.DEBUGGING) << "Getting wallet balance";

	  availableBalance = wallet.getActualBalance();
	  lockedAmount = wallet.getPendingBalance();
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting balance: " << x.what();
	  return x.code();
	}

	logger.functorMethod(Logging.Level.DEBUGGING) << "Wallet actual balance: " << (int)availableBalance << ", pending: " << (int)lockedAmount;
	return std::error_code();
  }
  public std::error_code getBlockHashes(uint firstBlockIndex, uint blockCount, List<string> blockHashes)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);
	  List<Crypto.Hash> hashes = wallet.getBlockHashes(firstBlockIndex, blockCount);

	  blockHashes.Capacity = hashes.Count;
	  foreach (var hash in hashes)
	  {
		blockHashes.Add(Common.GlobalMembers.podToHex(hash));
	  }
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting block hashes: " << x.what();
	  return x.code();
	}

	return std::error_code();
  }
  public std::error_code getViewKey(ref string viewSecretKey)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);
	  CryptoNote.KeyPair viewKey = wallet.getViewKey();
	  viewSecretKey = Common.GlobalMembers.podToHex(viewKey.secretKey);
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting view key: " << x.what();
	  return x.code();
	}

	return std::error_code();
  }
  public std::error_code getMnemonicSeed(string address, ref string mnemonicSeed)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);
	  CryptoNote.KeyPair key = wallet.getAddressSpendKey(address);
	  CryptoNote.KeyPair viewKey = wallet.getViewKey();

	  Crypto.SecretKey deterministic_private_view_key = new Crypto.SecretKey();

	  CryptoNote.AccountBase.generateViewFromSpend(key.secretKey, deterministic_private_view_key);

	  bool deterministic_private_keys = deterministic_private_view_key == viewKey.secretKey;

	  if (deterministic_private_keys)
	  {
		mnemonicSeed = Mnemonics.PrivateKeyToMnemonic(key.secretKey);
	  }
	  else
	  {
		/* Have to be able to derive view key from spend key to create a mnemonic
		   seed, due to being able to generate multiple addresses we can't do
		   this in walletd as the default */
		logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Your private keys are not deterministic and so a mnemonic seed cannot be generated!";
		return GlobalMembers.make_error_code(CryptoNote.error.WalletServiceErrorCode.KEYS_NOT_DETERMINISTIC);
	  }
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting mnemonic seed: " << x.what();
	  return x.code();
	}

	return std::error_code();
  }
  public std::error_code getTransactionHashes(List<string> addresses, string blockHashString, uint blockCount, string paymentId, ref List<TransactionHashesInBlockRpcInfo> transactionHashes)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);
	  GlobalMembers.validateAddresses(addresses, currency, logger.functorMethod);

	  if (!string.IsNullOrEmpty(paymentId))
	  {
		GlobalMembers.validatePaymentId(paymentId, logger.functorMethod);
	  }

	  TransactionsInBlockInfoFilter transactionFilter = new TransactionsInBlockInfoFilter(addresses, paymentId);
	  Crypto.Hash blockHash = GlobalMembers.parseHash(blockHashString, logger.functorMethod);

	  transactionHashes = getRpcTransactionHashes(blockHash, blockCount, transactionFilter);
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting transactions: " << x.what();
	  return x.code();
	}
	catch (System.Exception x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting transactions: " << x.Message;
	  return GlobalMembers.make_error_code(CryptoNote.error.INTERNAL_WALLET_ERROR);
	}

	return std::error_code();
  }
  public std::error_code getTransactionHashes(List<string> addresses, uint firstBlockIndex, uint blockCount, string paymentId, ref List<TransactionHashesInBlockRpcInfo> transactionHashes)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);
	  GlobalMembers.validateAddresses(addresses, currency, logger.functorMethod);

	  if (!string.IsNullOrEmpty(paymentId))
	  {
		GlobalMembers.validatePaymentId(paymentId, logger.functorMethod);
	  }

	  TransactionsInBlockInfoFilter transactionFilter = new TransactionsInBlockInfoFilter(addresses, paymentId);
	  transactionHashes = getRpcTransactionHashes(firstBlockIndex, blockCount, transactionFilter);

	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting transactions: " << x.what();
	  return x.code();
	}
	catch (System.Exception x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting transactions: " << x.Message;
	  return GlobalMembers.make_error_code(CryptoNote.error.INTERNAL_WALLET_ERROR);
	}

	return std::error_code();
  }
  public std::error_code getTransactions(List<string> addresses, string blockHashString, uint blockCount, string paymentId, ref List<TransactionsInBlockRpcInfo> transactions)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);
	  GlobalMembers.validateAddresses(addresses, currency, logger.functorMethod);

	  if (!string.IsNullOrEmpty(paymentId))
	  {
		GlobalMembers.validatePaymentId(paymentId, logger.functorMethod);
	  }

	  TransactionsInBlockInfoFilter transactionFilter = new TransactionsInBlockInfoFilter(addresses, paymentId);

	  Crypto.Hash blockHash = GlobalMembers.parseHash(blockHashString, logger.functorMethod);

	  transactions = getRpcTransactions(blockHash, blockCount, transactionFilter);
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting transactions: " << x.what();
	  return x.code();
	}
	catch (System.Exception x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting transactions: " << x.Message;
	  return GlobalMembers.make_error_code(CryptoNote.error.INTERNAL_WALLET_ERROR);
	}

	return std::error_code();
  }
  public std::error_code getTransactions(List<string> addresses, uint firstBlockIndex, uint blockCount, string paymentId, ref List<TransactionsInBlockRpcInfo> transactions)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);
	  GlobalMembers.validateAddresses(addresses, currency, logger.functorMethod);

	  if (!string.IsNullOrEmpty(paymentId))
	  {
		GlobalMembers.validatePaymentId(paymentId, logger.functorMethod);
	  }

	  TransactionsInBlockInfoFilter transactionFilter = new TransactionsInBlockInfoFilter(addresses, paymentId);

	  transactions = getRpcTransactions(firstBlockIndex, blockCount, transactionFilter);
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting transactions: " << x.what();
	  return x.code();
	}
	catch (System.Exception x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting transactions: " << x.Message;
	  return GlobalMembers.make_error_code(CryptoNote.error.INTERNAL_WALLET_ERROR);
	}

	return std::error_code();
  }
  public std::error_code getTransaction(string transactionHash, ref TransactionRpcInfo transaction)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);
	  Crypto.Hash hash = GlobalMembers.parseHash(transactionHash, logger.functorMethod);

	  CryptoNote.WalletTransactionWithTransfers transactionWithTransfers = wallet.getTransaction(hash);

	  if (transactionWithTransfers.transaction.state == CryptoNote.WalletTransactionState.DELETED)
	  {
		logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Transaction " << transactionHash << " is deleted";
		return GlobalMembers.make_error_code(CryptoNote.error.OBJECT_NOT_FOUND);
	  }

	  transaction = GlobalMembers.convertTransactionWithTransfersToTransactionRpcInfo(transactionWithTransfers);
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting transaction: " << x.what();
	  return x.code();
	}
	catch (System.Exception x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting transaction: " << x.Message;
	  return GlobalMembers.make_error_code(CryptoNote.error.INTERNAL_WALLET_ERROR);
	}

	return std::error_code();
  }
  public std::error_code getAddresses(List<string> addresses)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

	  addresses.Clear();
	  addresses.Capacity = wallet.getAddressCount();

	  for (uint i = 0; i < wallet.getAddressCount(); ++i)
	  {
		addresses.Add(wallet.getAddress(i));
	  }
	}
	catch (System.Exception e)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Can't get addresses: " << e.Message;
	  return GlobalMembers.make_error_code(CryptoNote.error.INTERNAL_WALLET_ERROR);
	}

	return std::error_code();
  }
  public std::error_code sendTransaction(SendTransaction.Request request, ref string transactionHash)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

	  /* Integrated address payment ID's are uppercase - lets convert the input
	     payment ID to upper so we can compare with more ease */
	  std::transform(request.paymentId.GetEnumerator(), request.paymentId.end(), request.paymentId.GetEnumerator(), global::toupper);

	  List<string> paymentIDs = new List<string>();

	  foreach (var transfer in request.transfers)
	  {
		  string addr = transfer.address;

		  /* It's not a standard address. Is it an integrated address? */
		  if (!CryptoNote.validateAddress(addr, currency))
		  {
			  string address;
			  string paymentID;
			  address = GlobalMembers.decodeIntegratedAddress(addr, currency, logger.functorMethod).Item1;
			  paymentID = GlobalMembers.decodeIntegratedAddress(addr, currency, logger.functorMethod).Item2;

			  /* A payment ID was specified with the transaction, and it is not
			     the same as the decoded one -> we can't send a transaction
			     with two different payment ID's! */
			  if (request.paymentId != "" && request.paymentId != paymentID)
			  {
				  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.CONFLICTING_PAYMENT_IDS));
			  }

			  /* Replace the integrated transfer address with the actual
			     decoded address */
			  transfer.address = address;

			  paymentIDs.Add(paymentID);
		  }
	  }

	  /* Only one integrated address specified, set the payment ID to the
	     decoded value */
	  if (paymentIDs.Count == 1)
	  {
		  request.paymentId = paymentIDs[0];

	  }
	  else if (paymentIDs.Count > 1)
	  {
		  /* Are all the specified payment IDs equal? */
		  if (!std::equal(paymentIDs.GetEnumerator() + 1, paymentIDs.end(), paymentIDs.GetEnumerator()))
		  {
			  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.CONFLICTING_PAYMENT_IDS));
		  }

		  /* They are all equal, set the payment ID to the decoded value */
		  request.paymentId = paymentIDs[0];
	  }

	  GlobalMembers.validateAddresses(request.sourceAddresses, currency, logger.functorMethod);
	  GlobalMembers.validateAddresses(GlobalMembers.collectDestinationAddresses(request.transfers), currency, logger.functorMethod);
	  if (!string.IsNullOrEmpty(request.changeAddress))
	  {
		GlobalMembers.validateAddresses(new List<string>() {request.changeAddress}, currency, logger.functorMethod);
	  }

	  var (success, error, error_code) = CryptoNote.Mixins.validate(request.anonymity, node.getLastKnownBlockHeight());

	  if (!success)
	  {
		logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << error;
		throw std::system_error(error_code);
	  }

	  CryptoNote.TransactionParameters sendParams = new CryptoNote.TransactionParameters();
	  if (!string.IsNullOrEmpty(request.paymentId))
	  {
		GlobalMembers.addPaymentIdToExtra(request.paymentId, sendParams.extra);
	  }
	  else
	  {
		sendParams.extra = GlobalMembers.getValidatedTransactionExtraString(request.extra);
	  }

	  sendParams.sourceAddresses = new List<string>(request.sourceAddresses);
	  sendParams.destinations = new List<WalletOrder>(GlobalMembers.convertWalletRpcOrdersToWalletOrders(request.transfers, m_node_address, m_node_fee));
	  sendParams.fee = request.fee;
	  sendParams.mixIn = request.anonymity;
	  sendParams.unlockTimestamp = request.unlockTime;
	  sendParams.changeDestination = request.changeAddress;

	  uint transactionId = wallet.transfer(sendParams);
	  transactionHash = Common.GlobalMembers.podToHex(wallet.getTransaction(transactionId).hash);

	  logger.functorMethod(Logging.Level.DEBUGGING) << "Transaction " << transactionHash << " has been sent";
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while sending transaction: " << x.what();
	  return x.code();
	}
	catch (System.Exception x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while sending transaction: " << x.Message;
	  return GlobalMembers.make_error_code(CryptoNote.error.INTERNAL_WALLET_ERROR);
	}

	return std::error_code();
  }
  public std::error_code createDelayedTransaction(CreateDelayedTransaction.Request request, ref string transactionHash)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

	  /* Integrated address payment ID's are uppercase - lets convert the input
	     payment ID to upper so we can compare with more ease */
	  std::transform(request.paymentId.GetEnumerator(), request.paymentId.end(), request.paymentId.GetEnumerator(), global::toupper);

	  List<string> paymentIDs = new List<string>();

	  foreach (var transfer in request.transfers)
	  {
		  string addr = transfer.address;

		  /* It's not a standard address. Is it an integrated address? */
		  if (!CryptoNote.validateAddress(addr, currency))
		  {
			  string address;
			  string paymentID;
			  address = GlobalMembers.decodeIntegratedAddress(addr, currency, logger.functorMethod).Item1;
			  paymentID = GlobalMembers.decodeIntegratedAddress(addr, currency, logger.functorMethod).Item2;

			  /* A payment ID was specified with the transaction, and it is not
			     the same as the decoded one -> we can't send a transaction
			     with two different payment ID's! */
			  if (request.paymentId != "" && request.paymentId != paymentID)
			  {
				  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.CONFLICTING_PAYMENT_IDS));
			  }

			  /* Replace the integrated transfer address with the actual
			     decoded address */
			  transfer.address = address;

			  paymentIDs.Add(paymentID);
		  }
	  }

	  /* Only one integrated address specified, set the payment ID to the
	     decoded value */
	  if (paymentIDs.Count == 1)
	  {
		  request.paymentId = paymentIDs[0];
	  }
	  else if (paymentIDs.Count > 1)
	  {
		  /* Are all the specified payment IDs equal? */
		  if (!std::equal(paymentIDs.GetEnumerator() + 1, paymentIDs.end(), paymentIDs.GetEnumerator()))
		  {
			  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.CONFLICTING_PAYMENT_IDS));
		  }

		  /* They are all equal, set the payment ID to the decoded value */
		  request.paymentId = paymentIDs[0];
	  }


	  GlobalMembers.validateAddresses(request.addresses, currency, logger.functorMethod);
	  GlobalMembers.validateAddresses(GlobalMembers.collectDestinationAddresses(request.transfers), currency, logger.functorMethod);
	  if (!string.IsNullOrEmpty(request.changeAddress))
	  {
		GlobalMembers.validateAddresses(new List<string>() {request.changeAddress}, currency, logger.functorMethod);
	  }

	  CryptoNote.TransactionParameters sendParams = new CryptoNote.TransactionParameters();
	  if (!string.IsNullOrEmpty(request.paymentId))
	  {
		GlobalMembers.addPaymentIdToExtra(request.paymentId, sendParams.extra);
	  }
	  else
	  {
		sendParams.extra = Common.asString(Common.fromHex(request.extra));
	  }

	  sendParams.sourceAddresses = new List<string>(request.addresses);
	  sendParams.destinations = new List<WalletOrder>(GlobalMembers.convertWalletRpcOrdersToWalletOrders(request.transfers, m_node_address, m_node_fee));
	  sendParams.fee = request.fee;
	  sendParams.mixIn = request.anonymity;
	  sendParams.unlockTimestamp = request.unlockTime;
	  sendParams.changeDestination = request.changeAddress;

	  uint transactionId = wallet.makeTransaction(sendParams);
	  transactionHash = Common.GlobalMembers.podToHex(wallet.getTransaction(transactionId).hash);

	  logger.functorMethod(Logging.Level.DEBUGGING) << "Delayed transaction " << transactionHash << " has been created";
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while creating delayed transaction: " << x.what();
	  return x.code();
	}
	catch (System.Exception x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while creating delayed transaction: " << x.Message;
	  return GlobalMembers.make_error_code(CryptoNote.error.INTERNAL_WALLET_ERROR);
	}

	return std::error_code();
  }
  public std::error_code getDelayedTransactionHashes(List<string> transactionHashes)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

	  List<uint> transactionIds = wallet.getDelayedTransactionIds();
	  transactionHashes.Capacity = transactionIds.Count;

	  foreach (var id in transactionIds)
	  {
		transactionHashes.emplace_back(Common.GlobalMembers.podToHex(wallet.getTransaction(id).hash));
	  }

	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting delayed transaction hashes: " << x.what();
	  return x.code();
	}
	catch (System.Exception x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting delayed transaction hashes: " << x.Message;
	  return GlobalMembers.make_error_code(CryptoNote.error.INTERNAL_WALLET_ERROR);
	}

	return std::error_code();
  }
  public std::error_code deleteDelayedTransaction(string transactionHash)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

	  GlobalMembers.parseHash(transactionHash, logger.functorMethod); //validate transactionHash parameter

	  var idIt = transactionIdIndex.find(transactionHash);
	  if (idIt == transactionIdIndex.end())
	  {
		return GlobalMembers.make_error_code(CryptoNote.error.WalletServiceErrorCode.OBJECT_NOT_FOUND);
	  }

//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  uint transactionId = idIt.second;
	  wallet.rollbackUncommitedTransaction(transactionId);

	  logger.functorMethod(Logging.Level.DEBUGGING) << "Delayed transaction " << transactionHash << " has been canceled";
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while deleting delayed transaction hashes: " << x.what();
	  return x.code();
	}
	catch (System.Exception x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while deleting delayed transaction hashes: " << x.Message;
	  return GlobalMembers.make_error_code(CryptoNote.error.INTERNAL_WALLET_ERROR);
	}

	return std::error_code();
  }
  public std::error_code sendDelayedTransaction(string transactionHash)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

	  GlobalMembers.parseHash(transactionHash, logger.functorMethod); //validate transactionHash parameter

	  var idIt = transactionIdIndex.find(transactionHash);
	  if (idIt == transactionIdIndex.end())
	  {
		return GlobalMembers.make_error_code(CryptoNote.error.WalletServiceErrorCode.OBJECT_NOT_FOUND);
	  }

//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  uint transactionId = idIt.second;
	  wallet.commitTransaction(transactionId);

	  logger.functorMethod(Logging.Level.DEBUGGING) << "Delayed transaction " << transactionHash << " has been sent";
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while sending delayed transaction hashes: " << x.what();
	  return x.code();
	}
	catch (System.Exception x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while sending delayed transaction hashes: " << x.Message;
	  return GlobalMembers.make_error_code(CryptoNote.error.INTERNAL_WALLET_ERROR);
	}

	return std::error_code();
  }
  public std::error_code getUnconfirmedTransactionHashes(List<string> addresses, List<string> transactionHashes)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

	  GlobalMembers.validateAddresses(addresses, currency, logger.functorMethod);

	  List<CryptoNote.WalletTransactionWithTransfers> transactions = wallet.getUnconfirmedTransactions();

	  TransactionsInBlockInfoFilter transactionFilter = new TransactionsInBlockInfoFilter(addresses, "");

	  foreach (var transaction in transactions)
	  {
		if (transactionFilter.checkTransaction(transaction))
		{
		  transactionHashes.emplace_back(Common.GlobalMembers.podToHex(transaction.transaction.hash));
		}
	  }
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting unconfirmed transaction hashes: " << x.what();
	  return x.code();
	}
	catch (System.Exception x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting unconfirmed transaction hashes: " << x.Message;
	  return GlobalMembers.make_error_code(CryptoNote.error.INTERNAL_WALLET_ERROR);
	}

	return std::error_code();
  }

  /* blockCount = the blocks the wallet has synced. knownBlockCount = the top block the daemon knows of. localDaemonBlockCount = the blocks the daemon has synced. */
  public std::error_code getStatus(ref uint blockCount, ref uint knownBlockCount, ref ulong localDaemonBlockCount, ref string lastBlockHash, ref uint peerCount)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: System::RemoteContext<System.Tuple<uint, ulong, uint>> remoteContext(dispatcher, [this]()
	  System.RemoteContext<Tuple<uint, ulong, uint>> remoteContext(dispatcher, () =>
	  {
		/* Daemon remote height, daemon local height, peer count */
		return Tuple.Create(node.getKnownBlockCount(), node.getNodeHeight(), (uint)node.getPeerCount());
	  });

	  std::tie(knownBlockCount, localDaemonBlockCount, peerCount) = remoteContext.get();

	  blockCount = wallet.getBlockCount();

	  var lastHashes = wallet.getBlockHashes(blockCount - 1, 1);
	  lastBlockHash = Common.GlobalMembers.podToHex(lastHashes[lastHashes.Count - 1]);
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting status: " << x.what();
	  return x.code();
	}
	catch (System.Exception x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while getting status: " << x.Message;
	  return GlobalMembers.make_error_code(CryptoNote.error.INTERNAL_WALLET_ERROR);
	}

	return std::error_code();
  }
  public std::error_code sendFusionTransaction(ulong threshold, uint anonymity, List<string> addresses, string destinationAddress, ref string transactionHash)
  {

	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

	  GlobalMembers.validateAddresses(addresses, currency, logger.functorMethod);
	  if (!string.IsNullOrEmpty(destinationAddress))
	  {
		GlobalMembers.validateAddresses(new List<string>() {destinationAddress}, currency, logger.functorMethod);
	  }

	  uint transactionId = fusionManager.createFusionTransaction(threshold, anonymity, addresses, destinationAddress);
	  transactionHash = Common.GlobalMembers.podToHex(wallet.getTransaction(transactionId).hash);

	  logger.functorMethod(Logging.Level.DEBUGGING) << "Fusion transaction " << transactionHash << " has been sent";
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while sending fusion transaction: " << x.what();
	  return x.code();
	}
	catch (System.Exception x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while sending fusion transaction: " << x.Message;
	  return GlobalMembers.make_error_code(CryptoNote.error.INTERNAL_WALLET_ERROR);
	}

	return std::error_code();
  }
  public std::error_code estimateFusion(ulong threshold, List<string> addresses, ref uint fusionReadyCount, ref uint totalOutputCount)
  {

	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

	  GlobalMembers.validateAddresses(addresses, currency, logger.functorMethod);

	  var estimateResult = fusionManager.estimate(threshold, addresses);
	  fusionReadyCount = (uint)estimateResult.fusionReadyCount;
	  totalOutputCount = (uint)estimateResult.totalOutputCount;
	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Failed to estimate number of fusion outputs: " << x.what();
	  return x.code();
	}
	catch (System.Exception x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Failed to estimate number of fusion outputs: " << x.Message;
	  return GlobalMembers.make_error_code(CryptoNote.error.INTERNAL_WALLET_ERROR);
	}

	return std::error_code();
  }
  public std::error_code createIntegratedAddress(string address, string paymentId, ref string integratedAddress)
  {
	try
	{
	  System.EventLock lk = new System.EventLock(readyEvent);

	  GlobalMembers.validateAddresses(new List<string>() {address}, currency, logger.functorMethod);
	  GlobalMembers.validatePaymentId(paymentId, logger.functorMethod);

	}
	catch (std::system_error x)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Error while creating integrated address: " << x.what();
	  return x.code();
	}

	ulong prefix;

	CryptoNote.AccountPublicAddress addr = new CryptoNote.AccountPublicAddress();

	/* Get the private + public key from the address */
	CryptoNote.parseAccountAddressString(prefix, addr, address);

	/* Pack as a binary array */
	List<byte> ba = new List<byte>();
	CryptoNote.GlobalMembers.toBinaryArray(addr, ref ba);
	string keys = Common.asString(ba);

	/* Encode prefix + paymentID + keys as an address */
	integratedAddress = Tools.Base58.encode_addr(CryptoNote.parameters.CRYPTONOTE_PUBLIC_ADDRESS_BASE58_PREFIX, paymentId + keys);

	return std::error_code();
  }
  public std::error_code getFeeInfo(ref string address, ref uint amount)
  {
	address = m_node_address;
	amount = m_node_fee;

	return std::error_code();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong getDefaultMixin() const
  public ulong getDefaultMixin()
  {
	  return CryptoNote.getDefaultMixinByHeight(node.getLastKnownBlockHeight());
  }


  private void refresh()
  {
	try
	{
	  logger.functorMethod(Logging.Level.DEBUGGING) << "Refresh is started";
	  for (;;)
	  {
		var event = wallet.getEvent();
		if (event.type == CryptoNote.WalletEventType.TRANSACTION_CREATED)
		{
		  uint transactionId = event.transactionCreated.transactionIndex;
		  transactionIdIndex.Add(Common.GlobalMembers.podToHex(wallet.getTransaction(transactionId).hash), transactionId);
		}
	  }
	}
	catch (std::system_error e)
	{
	  logger.functorMethod(Logging.Level.DEBUGGING) << "refresh is stopped: " << e.what();
	}
	catch (System.Exception e)
	{
	  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "exception thrown in refresh(): " << e.Message;
	}
  }
  private void reset(ulong scanHeight)
  {
	wallet.reset(scanHeight);
  }

  private void loadWallet()
  {
	logger.functorMethod(Logging.Level.INFO, Logging.BRIGHT_WHITE) << "Loading wallet";
	wallet.load(config.walletFile, config.walletPassword);
	logger.functorMethod(Logging.Level.INFO, Logging.BRIGHT_WHITE) << "Wallet loading is finished.";
  }
  private void loadTransactionIdIndex()
  {
	transactionIdIndex.Clear();

	for (uint i = 0; i < wallet.getTransactionCount(); ++i)
	{
	  transactionIdIndex.Add(Common.GlobalMembers.podToHex(wallet.getTransaction(i).hash), i);
	}
  }
  private void getNodeFee()
  {
	logger.functorMethod(Logging.Level.DEBUGGING) << "Trying to retrieve node fee information." << std::endl;

	m_node_address = node.feeAddress();
	m_node_fee = node.feeAmount();

	if (!string.IsNullOrEmpty(m_node_address) && m_node_fee != 0)
	{
	  // Partially borrowed from <zedwallet/Tools.h>
	  uint div = (uint)Math.Pow(10, CryptoNote.parameters.CRYPTONOTE_DISPLAY_DECIMAL_POINT);
	  uint coins = m_node_fee / div;
	  uint cents = m_node_fee % div;
	  std::stringstream stream = new std::stringstream();
	  stream << std::setfill('0') << std::setw(CryptoNote.parameters.CRYPTONOTE_DISPLAY_DECIMAL_POINT) << (int)cents;
	  string amount = Convert.ToString(coins) + "." + stream.str();

	  logger.functorMethod(Logging.Level.INFO, Logging.RED) << "You have connected to a node that charges " << "a fee to send transactions." << std::endl;

	  logger.functorMethod(Logging.Level.INFO, Logging.RED) << "The fee for sending transactions is: " << amount << " per transaction." << std::endl;

	  logger.functorMethod(Logging.Level.INFO, Logging.RED) << "If you don't want to pay the node fee, please " << "relaunch this program and specify a different " << "node or run your own." << std::endl;
	}
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<CryptoNote::TransactionsInBlockInfo> getTransactions(const Crypto::Hash& blockHash, uint blockCount) const
  private List<CryptoNote.TransactionsInBlockInfo> getTransactions(Crypto.Hash blockHash, uint blockCount)
  {
	List<CryptoNote.TransactionsInBlockInfo> result = wallet.getTransactions(blockHash, blockCount);
	if (result.Count == 0)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.WalletServiceErrorCode.OBJECT_NOT_FOUND));
	}

	return result;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<CryptoNote::TransactionsInBlockInfo> getTransactions(uint firstBlockIndex, uint blockCount) const
  private List<CryptoNote.TransactionsInBlockInfo> getTransactions(uint firstBlockIndex, uint blockCount)
  {
	List<CryptoNote.TransactionsInBlockInfo> result = wallet.getTransactions(firstBlockIndex, blockCount);
	if (result.Count == 0)
	{
	  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.WalletServiceErrorCode.OBJECT_NOT_FOUND));
	}

	return result;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<TransactionHashesInBlockRpcInfo> getRpcTransactionHashes(const Crypto::Hash& blockHash, uint blockCount, const TransactionsInBlockInfoFilter& filter) const
  private List<TransactionHashesInBlockRpcInfo> getRpcTransactionHashes(Crypto.Hash blockHash, uint blockCount, TransactionsInBlockInfoFilter filter)
  {
	List<CryptoNote.TransactionsInBlockInfo> allTransactions = getTransactions(blockHash, blockCount);
	List<CryptoNote.TransactionsInBlockInfo> filteredTransactions = GlobalMembers.filterTransactions(allTransactions, filter);
	return GlobalMembers.convertTransactionsInBlockInfoToTransactionHashesInBlockRpcInfo(filteredTransactions);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<TransactionHashesInBlockRpcInfo> getRpcTransactionHashes(uint firstBlockIndex, uint blockCount, const TransactionsInBlockInfoFilter& filter) const
  private List<TransactionHashesInBlockRpcInfo> getRpcTransactionHashes(uint firstBlockIndex, uint blockCount, TransactionsInBlockInfoFilter filter)
  {
	List<CryptoNote.TransactionsInBlockInfo> allTransactions = getTransactions(firstBlockIndex, blockCount);
	List<CryptoNote.TransactionsInBlockInfo> filteredTransactions = GlobalMembers.filterTransactions(allTransactions, filter);
	return GlobalMembers.convertTransactionsInBlockInfoToTransactionHashesInBlockRpcInfo(filteredTransactions);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<TransactionsInBlockRpcInfo> getRpcTransactions(const Crypto::Hash& blockHash, uint blockCount, const TransactionsInBlockInfoFilter& filter) const
  private List<TransactionsInBlockRpcInfo> getRpcTransactions(Crypto.Hash blockHash, uint blockCount, TransactionsInBlockInfoFilter filter)
  {
	List<CryptoNote.TransactionsInBlockInfo> allTransactions = getTransactions(blockHash, blockCount);
	List<CryptoNote.TransactionsInBlockInfo> filteredTransactions = GlobalMembers.filterTransactions(allTransactions, filter);
	return GlobalMembers.convertTransactionsInBlockInfoToTransactionsInBlockRpcInfo(filteredTransactions);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<TransactionsInBlockRpcInfo> getRpcTransactions(uint firstBlockIndex, uint blockCount, const TransactionsInBlockInfoFilter& filter) const
  private List<TransactionsInBlockRpcInfo> getRpcTransactions(uint firstBlockIndex, uint blockCount, TransactionsInBlockInfoFilter filter)
  {
	List<CryptoNote.TransactionsInBlockInfo> allTransactions = getTransactions(firstBlockIndex, blockCount);
	List<CryptoNote.TransactionsInBlockInfo> filteredTransactions = GlobalMembers.filterTransactions(allTransactions, filter);
	return GlobalMembers.convertTransactionsInBlockInfoToTransactionsInBlockRpcInfo(filteredTransactions);
  }

  private readonly CryptoNote.Currency currency;
  private CryptoNote.IWallet wallet;
  private CryptoNote.IFusionManager fusionManager;
  private CryptoNote.INode node;
  private readonly WalletConfiguration config;
  private bool inited;
  private Logging.LoggerRef logger = new Logging.LoggerRef();
  private System.Dispatcher dispatcher;
  private System.Event readyEvent = new System.Event();
  private System.ContextGroup refreshContext = new System.ContextGroup();
  private string m_node_address;
  private uint m_node_fee;

  private SortedDictionary<string, uint> transactionIdIndex = new SortedDictionary<string, uint>();
}

} //namespace PaymentService











namespace PaymentService
{

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace


public class TransactionsInBlockInfoFilter
{
  public TransactionsInBlockInfoFilter(List<string> addressesVec, string paymentIdStr)
  {
	addresses.insert(addressesVec.GetEnumerator(), addressesVec.end());

	if (!string.IsNullOrEmpty(paymentIdStr))
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: paymentId = parsePaymentId(paymentIdStr);
	  paymentId.CopyFrom(GlobalMembers.parsePaymentId(paymentIdStr));
	  havePaymentId = true;
	}
	else
	{
	  havePaymentId = false;
	}
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool checkTransaction(const CryptoNote::WalletTransactionWithTransfers& transaction) const
  public bool checkTransaction(CryptoNote.WalletTransactionWithTransfers transaction)
  {
	if (havePaymentId)
	{
	  Crypto.Hash transactionPaymentId = new Crypto.Hash();
	  if (!GlobalMembers.getPaymentIdFromExtra(transaction.transaction.extra, transactionPaymentId))
	  {
		return false;
	  }

	  if (paymentId != transactionPaymentId)
	  {
		return false;
	  }
	}

	if (addresses.Count == 0)
	{
	  return true;
	}

	bool haveAddress = false;
	foreach (CryptoNote  in :WalletTransfer & transfer: transaction.transfers)
	{
	  if (addresses.find(transfer.address) != addresses.end())
	  {
		haveAddress = true;
		break;
	  }
	}

	return haveAddress;
  }

  public HashSet<string> addresses = new HashSet<string>();
  public bool havePaymentId = false;
  public Crypto.Hash paymentId = new Crypto.Hash();
}

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace


} //namespace PaymentService
