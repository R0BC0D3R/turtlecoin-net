// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


using System;
using System.Collections.Generic;

namespace PaymentService
{

public class ConfigurationManager
{
  public ConfigurationManager()
  {
	rpcSecret = Crypto.Hash();
  }
  public bool init(int argc, string[] argv)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: serviceConfig = initConfiguration();
	serviceConfig.CopyFrom(PaymentService.GlobalMembers.initConfiguration());

	// Load in the initial CLI options
	PaymentService.GlobalMembers.handleSettings(argc, argv, serviceConfig);

	// If the user passed in the --config-file option, we need to handle that first
	if (!string.IsNullOrEmpty(serviceConfig.configFile))
	{
	  try
	  {
		PaymentService.GlobalMembers.handleSettings(serviceConfig.configFile, serviceConfig);
	  }
	  catch (System.Exception e)
	  {
		Console.Write("\n");
		Console.Write("There was an error parsing the specified configuration file. Please check the file and try again: ");
		Console.Write("\n");
		Console.Write(e.Message);
		Console.Write("\n");
		Environment.Exit(1);
	  }
	}

	// Load in the CLI specified parameters again to overwrite any given in the config file
	PaymentService.GlobalMembers.handleSettings(argc, argv, serviceConfig);

	if (serviceConfig.dumpConfig)
	{
	  Console.Write(CryptoNote.getProjectCLIHeader());
	  Console.Write(PaymentService.GlobalMembers.asString(serviceConfig));
	  Console.Write("\n");
	  Environment.Exit(0);
	}
	else if (!string.IsNullOrEmpty(serviceConfig.outputFile))
	{
	  try
	  {
		PaymentService.GlobalMembers.asFile(serviceConfig, serviceConfig.outputFile);
		Console.Write(CryptoNote.getProjectCLIHeader());
		Console.Write("Configuration saved to: ");
		Console.Write(serviceConfig.outputFile);
		Console.Write("\n");
		Environment.Exit(0);
	  }
	  catch (System.Exception e)
	  {
		Console.Write(CryptoNote.getProjectCLIHeader());
		Console.Write("Could not save configuration to: ");
		Console.Write(serviceConfig.outputFile);
		Console.Write("\n");
		Console.Write(e.Message);
		Console.Write("\n");
		Environment.Exit(1);
	  }
	}

	if (serviceConfig.registerService && serviceConfig.unregisterService)
	{
	  throw new System.Exception("It's impossible to use both --register-service and --unregister-service at the same time");
	}

	if (serviceConfig.logLevel > Logging.Level.TRACE)
	{
	  throw new System.Exception("log-level must be between " + Convert.ToString(Logging.Level.FATAL) + ".." + Convert.ToString(Logging.Level.TRACE));
	}

	if (string.IsNullOrEmpty(serviceConfig.containerFile))
	{
	  throw new System.Exception("You must specify a wallet file to open!");
	}

	if (!std::ifstream(serviceConfig.containerFile) && !serviceConfig.generateNewContainer)
	{
	  if (std::ifstream(serviceConfig.containerFile + ".wallet"))
	  {
		throw new System.Exception("The wallet file you specified does not exist. Did you mean: " + serviceConfig.containerFile + ".wallet?");
	  }
	  else
	  {
		throw new System.Exception("The wallet file you specified does not exist; please check your spelling and try again.");
	  }
	}

	if ((!string.IsNullOrEmpty(serviceConfig.secretViewKey) || !string.IsNullOrEmpty(serviceConfig.secretSpendKey)) && !serviceConfig.generateNewContainer)
	{
	  throw new System.Exception("--generate-container is required");
	}

	if (!string.IsNullOrEmpty(serviceConfig.mnemonicSeed) && !serviceConfig.generateNewContainer)
	{
	  throw new System.Exception("--generate-container is required");
	}

	if (!string.IsNullOrEmpty(serviceConfig.mnemonicSeed) && (!string.IsNullOrEmpty(serviceConfig.secretViewKey) || !string.IsNullOrEmpty(serviceConfig.secretSpendKey)))
	{
	  throw new System.Exception("You cannot specify import from both Mnemonic seed and private keys");
	}

	if ((serviceConfig.registerService || serviceConfig.unregisterService) && string.IsNullOrEmpty(serviceConfig.containerFile))
	{
	  throw new System.Exception("--container-file parameter is required");
	}

	// If we are generating a new container, we can skip additional checks
	if (serviceConfig.generateNewContainer)
	{
	  return true;
	}

	// Run authentication checks

	if (string.IsNullOrEmpty(serviceConfig.rpcPassword) && !serviceConfig.legacySecurity)
	{
	  throw new System.Exception("Please specify either an RPC password or use the --rpc-legacy-security flag");
	}

	if (!string.IsNullOrEmpty(serviceConfig.rpcPassword))
	{
	  List<byte> rawData = new List<byte>(serviceConfig.rpcPassword.GetEnumerator(), serviceConfig.rpcPassword.end());
	  Crypto.GlobalMembers.cn_slow_hash_v0(rawData.data(), rawData.Count, rpcSecret);
	  serviceConfig.rpcPassword = "";
	}

	return true;
  }

  public WalletServiceConfiguration serviceConfig = new WalletServiceConfiguration();

  public Crypto.Hash rpcSecret = new Crypto.Hash();
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
