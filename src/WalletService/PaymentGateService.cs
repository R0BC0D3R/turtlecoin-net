// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


using PaymentService;
using System;
using System.Collections.Generic;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl

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

public class PaymentGateService
{
  public PaymentGateService()
  {
	  this.dispatcher = null;
	  this.stopEvent = null;
	  this.config = new PaymentService.ConfigurationManager();
	  this.service = null;
	  this.logger = new Logging.LoggerGroup();
	  this.currencyBuilder = new CryptoNote.CurrencyBuilder(logger.functorMethod);
	  this.fileLogger = new Logging.StreamLogger(Logging.Level.TRACE);
	  this.consoleLogger = new Logging.ConsoleLogger(Logging.Level.INFO);
	consoleLogger.setPattern("%D %T %L ");
	fileLogger.setPattern("%D %T %L ");
  }

  public bool init(int argc, string[] argv)
  {
	if (!config.init(argc, argv))
	{
	  return false;
	}

	logger.setMaxLevel((Logging.Level)config.serviceConfig.logLevel);
	logger.setPattern("%D %T %L ");
	logger.addLogger(consoleLogger);

	Logging.LoggerRef log = new Logging.LoggerRef(logger.functorMethod, "main");

	if (!string.IsNullOrEmpty(config.serviceConfig.serverRoot))
	{
	  GlobalMembers.changeDirectory(config.serviceConfig.serverRoot);
	  Math.Log(Logging.Level.INFO) << "Current working directory now is " << config.serviceConfig.serverRoot;
	}

	fileStream.open(config.serviceConfig.logFile, std::ofstream.app);

	if (fileStream == null)
	{
	  throw new System.Exception("Couldn't open log file");
	}

	fileLogger.attachToStream(fileStream);
	logger.addLogger(fileLogger);

	return true;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const PaymentService::ConfigurationManager& getConfig() const
  public PaymentService.ConfigurationManager getConfig()
  {
	  return config;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: WalletConfiguration getWalletConfig() const
  public WalletConfiguration getWalletConfig()
  {
	return WalletConfiguration{config.serviceConfig.containerFile, config.serviceConfig.containerPassword, config.serviceConfig.syncFromZero, config.serviceConfig.secretViewKey, config.serviceConfig.secretSpendKey, config.serviceConfig.mnemonicSeed, config.serviceConfig.scanHeight};
  }
  public CryptoNote.Currency getCurrency()
  {
	return currencyBuilder.currency();
  }

  public void run()
  {

	System.Dispatcher localDispatcher = new System.Dispatcher();
	System.Event localStopEvent = new System.Event(localDispatcher);

	this.dispatcher = localDispatcher;
	this.stopEvent = localStopEvent;

	Tools.SignalHandler.install(std::bind(GlobalMembers.stopSignalHandler, this));

	Logging.LoggerRef log = new Logging.LoggerRef(logger.functorMethod, "run");

	runRpcProxy(log.functorMethod);

	this.dispatcher = null;
	this.stopEvent = null;
  }
  public void stop()
  {
	Logging.LoggerRef log = new Logging.LoggerRef(logger.functorMethod, "stop");

	log.functorMethod(Logging.Level.INFO, Logging.BRIGHT_WHITE) << "Stop signal caught";

	if (dispatcher != null)
	{
	  dispatcher.remoteSpawn(() =>
	  {
		if (stopEvent != null)
		{
		  stopEvent.set();
		}
	  });
	}
  }

  public Logging.ILogger getLogger()
  {
	  return logger.functorMethod;
  }


//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void runInProcess(Logging::LoggerRef log);
  private void runRpcProxy(Logging.LoggerRef log)
  {
	Math.Log(Logging.Level.INFO) << "Starting Payment Gate with remote node";
	CryptoNote.Currency currency = currencyBuilder.currency();

	std::unique_ptr<CryptoNote.INode> node = new std::unique_ptr<CryptoNote.INode>(PaymentService.NodeFactory.createNode(config.serviceConfig.daemonAddress, config.serviceConfig.daemonPort, log.getLogger()));

	runWalletService(currency, *node);
  }

  private void runWalletService(CryptoNote.Currency currency, CryptoNote.INode node)
  {
	PaymentService.WalletConfiguration walletConfiguration = new PaymentService.WalletConfiguration(config.serviceConfig.containerFile, config.serviceConfig.containerPassword, config.serviceConfig.syncFromZero);

	std::unique_ptr<CryptoNote.WalletGreen> wallet = new std::unique_ptr<CryptoNote.WalletGreen>(new CryptoNote.WalletGreen(dispatcher, currency, node, logger.functorMethod));

	service = new PaymentService.WalletService(currency, dispatcher, node, *wallet, *wallet, walletConfiguration, logger.functorMethod);
	std::unique_ptr<PaymentService.WalletService> serviceGuard = new std::unique_ptr<PaymentService.WalletService>(service);
	try
	{
	  service.init();
	}
	catch (System.Exception e)
	{
	  Logging.LoggerRef(logger.functorMethod, "run")(Logging.Level.ERROR, Logging.BRIGHT_RED) << "Failed to init walletService reason: " << e.Message;
	  return;
	}

	if (config.serviceConfig.printAddresses)
	{
	  // print addresses and exit
	  List<string> addresses = new List<string>();
	  service.getAddresses(addresses);
	  foreach (var address in addresses)
	  {
		Console.Write("Address: ");
		Console.Write(address);
		Console.Write("\n");
	  }
	}
	else
	{
	  PaymentService.PaymentServiceJsonRpcServer rpcServer = new PaymentService.PaymentServiceJsonRpcServer(dispatcher, stopEvent, service, logger.functorMethod, config);
	  rpcServer.start(config.serviceConfig.bindAddress, config.serviceConfig.bindPort);

	  Logging.LoggerRef(logger.functorMethod, "PaymentGateService")(Logging.Level.INFO, Logging.BRIGHT_WHITE) << "JSON-RPC server stopped, stopping wallet service...";

	  try
	  {
		service.saveWallet();
	  }
	  catch (System.Exception ex)
	  {
		Logging.LoggerRef(logger.functorMethod, "saveWallet")(Logging.Level.WARNING, Logging.YELLOW) << "Couldn't save container: " << ex.Message;
	  }
	}
  }

  private System.Dispatcher dispatcher;
  private System.Event stopEvent;
  private PaymentService.ConfigurationManager config = new PaymentService.ConfigurationManager();
  private PaymentService.WalletService service;
  private CryptoNote.CurrencyBuilder currencyBuilder = new CryptoNote.CurrencyBuilder();

  private Logging.LoggerGroup logger = new Logging.LoggerGroup();
  private std::ofstream fileStream = new std::ofstream();
  private Logging.StreamLogger fileLogger = new Logging.StreamLogger();
  private Logging.ConsoleLogger consoleLogger = new Logging.ConsoleLogger();
}





#if ERROR
#undef ERROR
#endif

#if _WIN32
#else
#endif