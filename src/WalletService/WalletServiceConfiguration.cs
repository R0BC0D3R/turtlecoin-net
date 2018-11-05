// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.




using json = nlohmann.json;
using System;

namespace PaymentService
{
  public class WalletServiceConfiguration
  {
	public string daemonAddress;
	public string bindAddress;
	public string rpcPassword;
	public string containerFile;
	public string containerPassword;
	public string serverRoot;
	public string corsHeader;
	public string logFile;

	public int daemonPort;
	public int bindPort;
	public int logLevel;

	public bool legacySecurity;

	// Runtime online options
	public bool help;
	public bool version;
	public bool dumpConfig;
	public string configFile;
	public string outputFile;

	public string secretViewKey;
	public string secretSpendKey;
	public string mnemonicSeed;

	public bool generateNewContainer;
	public bool daemonize;
	public bool registerService;
	public bool unregisterService;
	public bool printAddresses;
	public bool syncFromZero;

	public ulong scanHeight;
  }
}
