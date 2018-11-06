//#define SKEIN_USE_ASM
//#define SKEIN_LOOP

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Logging
{
    public static class GlobalMembers
    {
        public static string FormatPattern(string pattern, string category, Level level, DateTime time)
        {
            string returnString = string.Empty;

            // TODO: Not what it originally was but good enough for now
            if (pattern.Contains("%C"))
            {
                returnString += category;
            }
            else if (pattern.Contains("%D"))
            {
                returnString += time.Date.ToString();
            }
            else if (pattern.Contains("%T"))
            {
                returnString += time.TimeOfDay.ToString();
            }
            else if (pattern.Contains("%L"))
            {
                returnString += level;
            }
            else
            {
                returnString = pattern;
            }


            return returnString;
        }

        public static readonly string BLUE = @"\x1F""BLUE\x1F";
        public static readonly string GREEN = @"\x1F""GREEN\x1F";
        public static readonly string RED = @"\x1F""RED\x1F";
        public static readonly string YELLOW = @"\x1F""YELLOW\x1F";
        public static readonly string WHITE = @"\x1F""WHITE\x1F";
        public static readonly string CYAN = @"\x1F""CYAN\x1F";
        public static readonly string MAGENTA = @"\x1F""MAGENTA\x1F";
        public static readonly string BRIGHT_BLUE = @"\x1F""BRIGHT_BLUE\x1F";
        public static readonly string BRIGHT_GREEN = @"\x1F""BRIGHT_GREEN\x1F";
        public static readonly string BRIGHT_RED = @"\x1F""BRIGHT_RED\x1F";
        public static readonly string BRIGHT_YELLOW = @"\x1F""BRIGHT_YELLOW\x1F";
        public static readonly string BRIGHT_WHITE = @"\x1F""BRIGHT_WHITE\x1F";
        public static readonly string BRIGHT_CYAN = @"\x1F""BRIGHT_CYAN\x1F";
        public static readonly string BRIGHT_MAGENTA = @"\x1F""BRIGHT_MAGENTA\x1F";
        public static readonly string DEFAULT = @"\x1F""DEFAULT\x1F";

        //public static readonly char ILogger.COLOR_DELIMETER = '\x1F';

        //	public static readonly List<string> ILogger.LEVEL_NAMES =
        //	{
        //		{"FATAL", "ERROR", "WARNING", "INFO", "DEBUG", "TRACE"}
        //	};
    }
}

//public static class GlobalMembers
//{
    //	//public static std::error_code make_error_code(CryptoNote.error.BlockchainExplorerErrorCodes e)
    //	//{
    //	//  return std::error_code((int)e, CryptoNote.error.BlockchainExplorerErrorCategory.INSTANCE);
    //	//}



    //	//public static readonly char GENERIC_PATH_SEPARATOR = '/';

    //	//#if _WIN32
    //	//public static readonly char NATIVE_PATH_SEPARATOR = '\\';
    //	//#else
    //	//public static readonly char NATIVE_PATH_SEPARATOR = '/';
    //	//#endif


    //	//public static int findExtensionPosition(string filename)
    //	//{
    //	//  var pos = filename.LastIndexOf('.');

    //	//  if (pos != -1)
    //	//  {
    //	//	var slashPos = filename.LastIndexOf(GENERIC_PATH_SEPARATOR);
    //	//	if (slashPos != -1 && slashPos > pos)
    //	//	{
    //	//	  return -1;
    //	//	}
    //	//  }

    //	//  return pos;
    //	//}

    ////	  public static Action m_handler;
    //////C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
    ////  private static object handleSignal_m_mutex = new object();

    ////	  public static void handleSignal()
    ////	  {
    ////	//C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
    ////	//	static object m_mutex;
    ////		std::unique_lock<object> @lock = new std::unique_lock<object>(handleSignal_m_mutex, std::try_to_lock);
    ////		if (!@lock.owns_lock())
    ////		{
    ////		  return;
    ////		}
    ////		m_handler();
    ////	  }



    //    public static void posixHandler(int UnnamedParameter)
    //    {
    //        handleSignal();
    //    }














    //	public static std::ostream print256<T>(std::ostream o, T v)
    //	{
    //	  return o << Common.GlobalMembers.podToHex(v);
    //	}



    //	public static bool parse_hash256(string str_hash, Crypto.Hash hash)
    //	{
    //	  return Common.GlobalMembers.podFromHex(str_hash, hash);
    //	}

    //	public static ulong getSignaturesCount(TransactionInput input)
    //	{
    ////C++ TO C# CONVERTER TODO TASK: C# does not allow declaring types within methods:
    //	//  struct txin_signature_size_visitor : public boost::static_visitor < ulong >
    //	//  {
    //	//	ulong operator ()(const BaseInput& txin) const
    //	//	{
    //	//		return 0;
    //	//	}
    //	//	ulong operator ()(const KeyInput& txin) const
    //	//	{
    //	//		return txin.outputIndexes.size();
    //	//	}
    //	//  };

    //	  return boost::apply_visitor(txin_signature_size_visitor(), input);
    //	}

    //	public static void getVariantValue(CryptoNote.ISerializer serializer, ushort tag, ref CryptoNote.TransactionInput in)
    //	{
    //	  switch (tag)
    //	  {
    //	  case 0xff:
    //	  {
    //		CryptoNote.BaseInput v = new CryptoNote.BaseInput();
    //		serializer.functorMethod(v, "value");
    //		in = v;
    //		break;
    //	  }
    //	  case 0x2:
    //	  {
    //		CryptoNote.KeyInput v = new CryptoNote.KeyInput();
    //		serializer.functorMethod(v, "value");
    //		in = v;
    //		break;
    //	  }
    //	  default:
    //		throw new System.Exception("Unknown variant tag");
    //	  }
    //	}

    //	public static void getVariantValue(CryptoNote.ISerializer serializer, ushort tag, ref CryptoNote.TransactionOutputTarget @out)
    //	{
    //	  switch (tag)
    //	  {
    //	  case 0x2:
    //	  {
    //		CryptoNote.KeyOutput v = new CryptoNote.KeyOutput();
    //		serializer.functorMethod(v, "data");
    //		@out = v;
    //		break;
    //	  }
    //	  default:
    //		throw new System.Exception("Unknown variant tag");
    //	  }
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T>
    //	public static bool serializePod<T>(T v, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  return serializer.binary(v, sizeof(T), new Common.StringView(name));
    //	}

    //	public static bool serializeVarintVector(List<uint> vector, CryptoNote.ISerializer serializer, Common.StringView name)
    //	{
    //	  ulong size = vector.Count;

    //	  if (!serializer.beginArray(size, new Common.StringView(name)))
    //	  {
    //		vector.Clear();
    //		return false;
    //	  }

    //	  vector.Resize(size);

    //	  for (ulong i = 0; i < size; ++i)
    //	  {
    //		serializer.functorMethod(vector[i], "");
    //	  }

    //	  serializer.endArray();
    //	  return true;
    //	}
    //	  public static readonly ulong MEGABYTE = 1024 * 1024;


    //	public static std::error_code make_error_code(CryptoNote.error.DataBaseErrorCodes e)
    //	{
    //	  return std::error_code((int)e, CryptoNote.error.DataBaseErrorCategory.INSTANCE);
    //	}
    //	  public static readonly string RAW_BLOCK_NAME = "raw_block";
    //	  public static readonly string RAW_TXS_NAME = "raw_txs";

    //// LWMA-3 difficulty algorithm 
    //// Copyright (c) 2017-2018 Zawy, MIT License
    //// https://github.com/zawy12/difficulty-algorithms/issues/3
    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	// 
    //	// Please see the included LICENSE file for more information.


    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	// 
    //	// Please see the included LICENSE file for more information.


    //	public static ulong nextDifficultyV6(List<ulong> timestamps, List<ulong> cumulativeDifficulties)
    //	{
    //		ulong T = CryptoNote.parameters.DIFFICULTY_TARGET;
    //		ulong N = CryptoNote.parameters.DIFFICULTY_WINDOW_V3;
    //		ulong L = new ulong(0);
    //		ulong ST = new ulong();
    //		ulong sum_3_ST = new ulong(0);
    //		ulong next_D = new ulong();
    //		ulong prev_D = new ulong();
    //		ulong thisTimestamp = new ulong();
    //		ulong previousTimestamp = new ulong();

    //		/* If we are starting up, returning a difficulty guess. If you are a
    //		   new coin, you might want to set this to a decent estimate of your
    //		   hashrate */
    //		if (timestamps.Count <= 10)
    //		{
    //			return 10000;
    //		}

    //		/* Don't have the full amount of blocks yet, starting up */
    //		if (timestamps.Count < CryptoNote.parameters.DIFFICULTY_BLOCKS_COUNT_V3)
    //		{
    //			N = timestamps.Count - 1;
    //		}

    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: previousTimestamp = timestamps[0];
    //		previousTimestamp.CopyFrom(timestamps[0]);

    //		for (ulong i = 1; i <= N; i++)
    //		{
    //			if (timestamps[i] > previousTimestamp)
    //			{
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: thisTimestamp = timestamps[i];
    //				thisTimestamp.CopyFrom(timestamps[i]);
    //			}
    //			else
    //			{
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: thisTimestamp = previousTimestamp + 1;
    //				thisTimestamp.CopyFrom(previousTimestamp + 1);
    //			}

    //			ST = Math.Min(6 * T, thisTimestamp - previousTimestamp);

    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: previousTimestamp = thisTimestamp;
    //			previousTimestamp.CopyFrom(thisTimestamp);

    //			L += ST * i;

    //			if (i > N - 3)
    //			{
    //				sum_3_ST += ST;
    //			}
    //		}

    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: next_D = ((cumulativeDifficulties[N] - cumulativeDifficulties[0]) * T * (N+1) * 99) / (100 * 2 * L);
    //		next_D.CopyFrom(((cumulativeDifficulties[N] - cumulativeDifficulties[0]) * T * (N + 1) * 99) / (100 * 2 * L));

    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: prev_D = cumulativeDifficulties[N] - cumulativeDifficulties[N-1];
    //		prev_D.CopyFrom(cumulativeDifficulties[N] - cumulativeDifficulties[N - 1]);

    //		next_D = Math.Max((prev_D * 67) / 100, Math.Min(next_D, (prev_D * 150) / 100));

    //		if (sum_3_ST < (8 * T) / 10)
    //		{
    //			next_D = Math.Max(next_D, (prev_D * 108) / 100);
    //		}

    //		return next_D;
    //	}

    //// LWMA-2 difficulty algorithm 
    //// Copyright (c) 2017-2018 Zawy, MIT License
    //// https://github.com/zawy12/difficulty-algorithms/issues/3

    //	public static ulong nextDifficultyV5(List<ulong> timestamps, List<ulong> cumulativeDifficulties)
    //	{
    //		long T = CryptoNote.parameters.DIFFICULTY_TARGET;
    //		long N = CryptoNote.parameters.DIFFICULTY_WINDOW_V3;
    //		long L = new long(0);
    //		long ST = new long();
    //		long sum_3_ST = new long(0);
    //		long next_D = new long();
    //		long prev_D = new long();

    //		/* If we are starting up, returning a difficulty guess. If you are a
    //		   new coin, you might want to set this to a decent estimate of your
    //		   hashrate */
    //		if (timestamps.Count < (ulong)(N + 1))
    //		{
    //			return 10000;
    //		}

    //		for (long i = 1; i <= N; i++)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: ST = static_cast<long>(timestamps[i]) - static_cast<long>(timestamps[i-1]);
    //			ST.CopyFrom((long)timestamps[i] - (long)timestamps[i - 1]);

    //			ST = Math.Max(-4 * T, Math.Min(ST, 6 * T));

    //			L += ST * i;

    //			if (i > N - 3)
    //			{
    //				sum_3_ST += ST;
    //			}
    //		}

    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: next_D = (static_cast<long>(cumulativeDifficulties[N] - cumulativeDifficulties[0]) * T * (N+1) * 99) / (100 * 2 * L);
    //		next_D.CopyFrom(((long)(cumulativeDifficulties[N] - cumulativeDifficulties[0]) * T * (N + 1) * 99) / (100 * 2 * L));

    //		prev_D = cumulativeDifficulties[N] - cumulativeDifficulties[N - 1];

    //		next_D = Math.Max((prev_D * 67) / 100, Math.Min(next_D, (prev_D * 150) / 100));

    //		if (sum_3_ST < (8 * T) / 10)
    //		{
    //			next_D = Math.Max(next_D, (prev_D * 108) / 100);
    //		}

    //		return (ulong)next_D;
    //	}

    //// LWMA-2 difficulty algorithm 
    //// Copyright (c) 2017-2018 Zawy, MIT License
    //// https://github.com/zawy12/difficulty-algorithms/issues/3

    //	public static ulong nextDifficultyV4(List<ulong> timestamps, List<ulong> cumulativeDifficulties)
    //	{
    //		long T = CryptoNote.parameters.DIFFICULTY_TARGET;
    //		long N = CryptoNote.parameters.DIFFICULTY_WINDOW_V3;
    //		long L = new long(0);
    //		long ST = new long();
    //		long sum_3_ST = new long(0);
    //		long next_D = new long();
    //		long prev_D = new long();

    //		if (timestamps.Count <= (ulong)N)
    //		{
    //			return 1000;
    //		}

    //		for (long i = 1; i <= N; i++)
    //		{
    //			ST = clamp(-6 * T, (long)timestamps[i] - (long)timestamps[i - 1], 6 * T);

    //			L += ST * i;

    //			if (i > N - 3)
    //			{
    //				sum_3_ST += ST;
    //			}
    //		}

    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: next_D = (static_cast<long>(cumulativeDifficulties[N] - cumulativeDifficulties[0]) * T * (N+1) * 99) / (100 * 2 * L);
    //		next_D.CopyFrom(((long)(cumulativeDifficulties[N] - cumulativeDifficulties[0]) * T * (N + 1) * 99) / (100 * 2 * L));
    //		prev_D = cumulativeDifficulties[N] - cumulativeDifficulties[N - 1];

    //		/* Make sure we don't divide by zero if 50x attacker (thanks fireice) */
    //		next_D = Math.Max((prev_D * 67) / 100, Math.Min(next_D, (prev_D * 150) / 100));

    //		if (sum_3_ST < (8 * T) / 10)
    //		{
    //			next_D = Math.Max(next_D, (prev_D * 110) / 100);
    //		}

    //		return (ulong)next_D;
    //	}

    //// LWMA-2 difficulty algorithm 
    //// Copyright (c) 2017-2018 Zawy, MIT License
    //// https://github.com/zawy12/difficulty-algorithms/issues/3

    //	public static ulong nextDifficultyV3(List<ulong> timestamps, List<ulong> cumulativeDifficulties)
    //	{
    //		long T = CryptoNote.parameters.DIFFICULTY_TARGET;
    //		long N = CryptoNote.parameters.DIFFICULTY_WINDOW_V3;
    //		long FTL = CryptoNote.parameters.CRYPTONOTE_BLOCK_FUTURE_TIME_LIMIT_V3;
    //		long L = new long(0);
    //		long ST = new long();
    //		long sum_3_ST = new long(0);
    //		long next_D = new long();
    //		long prev_D = new long();

    //		if (timestamps.Count <= (ulong)N)
    //		{
    //			return 1000;
    //		}

    //		for (long i = 1; i <= N; i++)
    //		{
    //			ST = Math.Max(-FTL, Math.Min((long)timestamps[i] - (long)timestamps[i - 1], 6 * T));

    //			L += ST * i;

    //			if (i > N - 3)
    //			{
    //				sum_3_ST += ST;
    //			}
    //		}

    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: next_D = (static_cast<long>(cumulativeDifficulties[N] - cumulativeDifficulties[0]) * T * (N+1) * 99) / (100 * 2 * L);
    //		next_D.CopyFrom(((long)(cumulativeDifficulties[N] - cumulativeDifficulties[0]) * T * (N + 1) * 99) / (100 * 2 * L));
    //		prev_D = cumulativeDifficulties[N] - cumulativeDifficulties[N - 1];

    //		/* Make sure we don't divide by zero if 50x attacker (thanks fireice) */
    //		next_D = Math.Max((prev_D * 70) / 100, Math.Min(next_D, (prev_D * 107) / 100));

    //		if (sum_3_ST < (8 * T) / 10)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: next_D = (prev_D * 110) / 100;
    //			next_D.CopyFrom((prev_D * 110) / 100);
    //		}

    //		return (ulong)next_D;
    //	}

    //	/* TODO: This has been added in the stdlib in c++17 */
    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T>
    //	public static T clamp<T>(T n, T lower, T upper)
    //	{
    //		return Math.Max(lower, Math.Min(n, upper));
    //	}

    //	  public static readonly string DB_NAME = "DB";
    //	  public static readonly string TESTNET_DB_NAME = "testnet_DB";

    //	#if MSVC
    //	public static char suppressMSVCWarningLNK4221;
    //	#endif

    //	#if MSVC
    //	public static char suppressMSVCWarningLNK4221;
    //	#endif


    //	  public static void derivePublicKey(AccountPublicAddress to, SecretKey txKey, uint outputIndex, PublicKey ephemeralKey)
    //	  {
    //		KeyDerivation derivation = new KeyDerivation();
    //		generate_key_derivation(to.viewPublicKey, txKey, derivation);
    //		derive_public_key(derivation, outputIndex, to.spendPublicKey, ephemeralKey);
    //	  }

    //	public static readonly string INPUT_DATA = "0100fb8e8ac805899323371bb790db19218afd8db8e3755d8b90f39b3d5506a9abce4fa912244500000000ee8146d49fa93ee724deb57d12cbc6c6f3b924d946127c7a97418f9348828f0f02";

    //	public static readonly string CN_FAST_HASH = "b542df5b6e7f5f05275c98e7345884e2ac726aeeb07e03e44e0389eb86cd05f0";

    //	public static readonly string CN_SLOW_HASH_V0 = "1b606a3f4a07d6489a1bcd07697bd16696b61c8ae982f61a90160f4e52828a7f";
    //	public static readonly string CN_SLOW_HASH_V1 = "c9fae8425d8688dc236bcdbc42fdb42d376c6ec190501aa84b04a4b4cf1ee122";
    //	public static readonly string CN_SLOW_HASH_V2 = "871fcd6823f6a879bb3f33951c8e8e891d4043880b02dfa1bb3be498b50e7578";

    //	public static readonly string CN_LITE_SLOW_HASH_V0 = "28a22bad3f93d1408fca472eb5ad1cbe75f21d053c8ce5b3af105a57713e21dd";
    //	public static readonly string CN_LITE_SLOW_HASH_V1 = "87c4e570653eb4c2b42b7a0d546559452dfab573b82ec52f152b7ff98e79446f";
    //	public static readonly string CN_LITE_SLOW_HASH_V2 = "b7e78fab22eb19cb8c9c3afe034fb53390321511bab6ab4915cd538a630c3c62";

    //	public static readonly string[] CN_SOFT_SHELL_V0 = {"5e1891a15d5d85c09baf4a3bbe33675cfa3f77229c8ad66c01779e590528d6d3", "e1239347694df77cab780b7ec8920ec6f7e48ecef1d8c368e06708c08e1455f1", "118a03801c564d12f7e68972419303fe06f7a54ab8f44a8ce7deafbc6b1b5183", "8be48f7955eb3f9ac2275e445fe553f3ef359ea5c065cde98ff83011f407a0ec", "d33da3541960046e846530dcc9872b1914a62c09c7d732bff03bec481866ae48", "8be48f7955eb3f9ac2275e445fe553f3ef359ea5c065cde98ff83011f407a0ec", "118a03801c564d12f7e68972419303fe06f7a54ab8f44a8ce7deafbc6b1b5183", "e1239347694df77cab780b7ec8920ec6f7e48ecef1d8c368e06708c08e1455f1", "5e1891a15d5d85c09baf4a3bbe33675cfa3f77229c8ad66c01779e590528d6d3", "e1239347694df77cab780b7ec8920ec6f7e48ecef1d8c368e06708c08e1455f1", "118a03801c564d12f7e68972419303fe06f7a54ab8f44a8ce7deafbc6b1b5183", "8be48f7955eb3f9ac2275e445fe553f3ef359ea5c065cde98ff83011f407a0ec", "d33da3541960046e846530dcc9872b1914a62c09c7d732bff03bec481866ae48", "8be48f7955eb3f9ac2275e445fe553f3ef359ea5c065cde98ff83011f407a0ec", "118a03801c564d12f7e68972419303fe06f7a54ab8f44a8ce7deafbc6b1b5183", "e1239347694df77cab780b7ec8920ec6f7e48ecef1d8c368e06708c08e1455f1", "5e1891a15d5d85c09baf4a3bbe33675cfa3f77229c8ad66c01779e590528d6d3"};

    //	public static readonly string[] CN_SOFT_SHELL_V1 = {"ae7f864a7a2f2b07dcef253581e60a014972b9655a152341cb989164761c180a", "ce8687bdd08c49bd1da3a6a74bf28858670232c1a0173ceb2466655250f9c56d", "ddb6011d400ac8725995fb800af11646bb2fef0d8b6136b634368ad28272d7f4", "02576f9873dc9c8b1b0fc14962982734dfdd41630fc936137a3562b8841237e1", "d37e2785ab7b3d0a222940bf675248e7b96054de5c82c5f0b141014e136eadbc", "02576f9873dc9c8b1b0fc14962982734dfdd41630fc936137a3562b8841237e1", "ddb6011d400ac8725995fb800af11646bb2fef0d8b6136b634368ad28272d7f4", "ce8687bdd08c49bd1da3a6a74bf28858670232c1a0173ceb2466655250f9c56d", "ae7f864a7a2f2b07dcef253581e60a014972b9655a152341cb989164761c180a", "ce8687bdd08c49bd1da3a6a74bf28858670232c1a0173ceb2466655250f9c56d", "ddb6011d400ac8725995fb800af11646bb2fef0d8b6136b634368ad28272d7f4", "02576f9873dc9c8b1b0fc14962982734dfdd41630fc936137a3562b8841237e1", "d37e2785ab7b3d0a222940bf675248e7b96054de5c82c5f0b141014e136eadbc", "02576f9873dc9c8b1b0fc14962982734dfdd41630fc936137a3562b8841237e1", "ddb6011d400ac8725995fb800af11646bb2fef0d8b6136b634368ad28272d7f4", "ce8687bdd08c49bd1da3a6a74bf28858670232c1a0173ceb2466655250f9c56d", "ae7f864a7a2f2b07dcef253581e60a014972b9655a152341cb989164761c180a"};

    //	public static readonly string[] CN_SOFT_SHELL_V2 = {"b2172ec9466e1aee70ec8572a14c233ee354582bcb93f869d429744de5726a26", "b2623a2b041dc5ae3132b964b75e193558c7095e725d882a3946aae172179cf1", "141878a7b58b0f57d00b8fc2183cce3517d9d68becab6fee52abb3c1c7d0805b", "4646f9919791c28f0915bc0005ed619bee31d42359f7a8af5de5e1807e875364", "3fedc7ab0f8d14122fc26062de1af7a6165755fcecdf0f12fa3ccb3ff63629d0", "4646f9919791c28f0915bc0005ed619bee31d42359f7a8af5de5e1807e875364", "141878a7b58b0f57d00b8fc2183cce3517d9d68becab6fee52abb3c1c7d0805b", "b2623a2b041dc5ae3132b964b75e193558c7095e725d882a3946aae172179cf1", "b2172ec9466e1aee70ec8572a14c233ee354582bcb93f869d429744de5726a26", "b2623a2b041dc5ae3132b964b75e193558c7095e725d882a3946aae172179cf1", "141878a7b58b0f57d00b8fc2183cce3517d9d68becab6fee52abb3c1c7d0805b", "4646f9919791c28f0915bc0005ed619bee31d42359f7a8af5de5e1807e875364", "3fedc7ab0f8d14122fc26062de1af7a6165755fcecdf0f12fa3ccb3ff63629d0", "4646f9919791c28f0915bc0005ed619bee31d42359f7a8af5de5e1807e875364", "141878a7b58b0f57d00b8fc2183cce3517d9d68becab6fee52abb3c1c7d0805b", "b2623a2b041dc5ae3132b964b75e193558c7095e725d882a3946aae172179cf1", "b2172ec9466e1aee70ec8572a14c233ee354582bcb93f869d429744de5726a26"};

    //	internal static bool CompareHashes(Hash leftHash, string right)
    //	{
    //	  Hash rightHash = new Hash();
    //	  if (!Common.GlobalMembers.podFromHex(right, rightHash))
    //	  {
    //		return false;
    //	  }

    //	  return (leftHash == rightHash);
    //	}

    //	static void Main(int argc, string[] args)
    //	{
    //	  bool o_help;
    //	  bool o_version;
    //	  bool o_benchmark;
    //	  int o_iterations;

    //	  cxxopts.Options options = new cxxopts.Options(args[0], getProjectCLIHeader());

    //	  options.add_options("Core")("h,help", "Display this help message", cxxopts.value<bool>(o_help).implicit_value("true"))("v,version", "Output software version information", cxxopts.value<bool>(o_version).default_value("false").implicit_value("true"));

    //	  options.add_options("Performance Testing")("b,benchmark", "Run quick performance benchmark", cxxopts.value<bool>(o_benchmark).default_value("false").implicit_value("true"))("i,iterations", "The number of iterations for the benchmark test. Minimum of 1,000 iterations required.", cxxopts.value<int>(o_iterations).default_value(Convert.ToString(DefineConstants.PERFORMANCE_ITERATIONS)), "#");

    //	  try
    //	  {
    //		var result = options.parse(argc, args);
    //	  }
    //	  catch (cxxopts.OptionException e)
    //	  {
    //		Console.Write("Error: Unable to parse command line argument options: ");
    //		Console.Write(e.what());
    //		Console.Write("\n");
    //		Console.Write("\n");
    //		Console.Write(options.help({}));
    //		Console.Write("\n");
    //		Environment.Exit(1);
    //	  }

    //	  if (o_help) // Do we want to display the help message?
    //	  {
    //		Console.Write(options.help({}));
    //		Console.Write("\n");
    //		Environment.Exit(0);
    //	  }
    //	  else if (o_version) // Do we want to display the software version?
    //	  {
    //		Console.Write(getProjectCLIHeader());
    //		Console.Write("\n");
    //		Environment.Exit(0);
    //	  }

    //	  if (o_iterations < 1000 && o_benchmark)
    //	  {
    //		Console.Write("\n");
    //		Console.Write("Error: The number of --iterations should be at least 1,000 for reasonable accuracy");
    //		Console.Write("\n");
    //		Environment.Exit(1);
    //	  }

    //	  try
    //	  {
    //		BinaryArray rawData = Common.fromHex(INPUT_DATA);

    //		Console.Write(getProjectCLIHeader());
    //		Console.Write("\n");

    //		Console.Write("Input: ");
    //		Console.Write(INPUT_DATA);
    //		Console.Write("\n");
    //		Console.Write("\n");

    //		Hash hash = new Hash();

    //		Crypto.GlobalMembers.cn_fast_hash(rawData.data(), rawData.size(), hash);
    //		Console.Write("cn_fast_hash: ");
    //		Console.Write(Common.toHex(hash, sizeof(Hash)));
    //		Console.Write("\n");
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
    ////ORIGINAL LINE: System.Diagnostics.Debug.Assert(CompareHashes(hash, CN_FAST_HASH));
    //		Debug.Assert(CompareHashes(new Crypto.Hash(hash), CN_FAST_HASH));

    //		Console.Write("\n");

    //		Crypto.GlobalMembers.cn_slow_hash_v0(rawData.data(), rawData.size(), hash);
    //		Console.Write("cn_slow_hash_v0: ");
    //		Console.Write(Common.toHex(hash, sizeof(Hash)));
    //		Console.Write("\n");
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
    ////ORIGINAL LINE: System.Diagnostics.Debug.Assert(CompareHashes(hash, CN_SLOW_HASH_V0));
    //		Debug.Assert(CompareHashes(new Crypto.Hash(hash), CN_SLOW_HASH_V0));

    //		if (rawData.size() >= 43)
    //		{
    //		  Crypto.GlobalMembers.cn_slow_hash_v1(rawData.data(), rawData.size(), hash);
    //		  Console.Write("cn_slow_hash_v1: ");
    //		  Console.Write(Common.toHex(hash, sizeof(Hash)));
    //		  Console.Write("\n");
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
    ////ORIGINAL LINE: System.Diagnostics.Debug.Assert(CompareHashes(hash, CN_SLOW_HASH_V1));
    //		  Debug.Assert(CompareHashes(new Crypto.Hash(hash), CN_SLOW_HASH_V1));

    //		  Crypto.GlobalMembers.cn_slow_hash_v2(rawData.data(), rawData.size(), hash);
    //		  Console.Write("cn_slow_hash_v2: ");
    //		  Console.Write(Common.toHex(hash, sizeof(Hash)));
    //		  Console.Write("\n");
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
    ////ORIGINAL LINE: System.Diagnostics.Debug.Assert(CompareHashes(hash, CN_SLOW_HASH_V2));
    //		  Debug.Assert(CompareHashes(new Crypto.Hash(hash), CN_SLOW_HASH_V2));

    //		  Console.Write("\n");

    //		  Crypto.GlobalMembers.cn_lite_slow_hash_v0(rawData.data(), rawData.size(), hash);
    //		  Console.Write("cn_lite_slow_hash_v0: ");
    //		  Console.Write(Common.toHex(hash, sizeof(Hash)));
    //		  Console.Write("\n");
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
    ////ORIGINAL LINE: System.Diagnostics.Debug.Assert(CompareHashes(hash, CN_LITE_SLOW_HASH_V0));
    //		  Debug.Assert(CompareHashes(new Crypto.Hash(hash), CN_LITE_SLOW_HASH_V0));

    //		  Crypto.GlobalMembers.cn_lite_slow_hash_v1(rawData.data(), rawData.size(), hash);
    //		  Console.Write("cn_lite_slow_hash_v1: ");
    //		  Console.Write(Common.toHex(hash, sizeof(Hash)));
    //		  Console.Write("\n");
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
    ////ORIGINAL LINE: System.Diagnostics.Debug.Assert(CompareHashes(hash, CN_LITE_SLOW_HASH_V1));
    //		  Debug.Assert(CompareHashes(new Crypto.Hash(hash), CN_LITE_SLOW_HASH_V1));

    //		  Crypto.GlobalMembers.cn_lite_slow_hash_v2(rawData.data(), rawData.size(), hash);
    //		  Console.Write("cn_lite_slow_hash_v2: ");
    //		  Console.Write(Common.toHex(hash, sizeof(Hash)));
    //		  Console.Write("\n");
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
    ////ORIGINAL LINE: System.Diagnostics.Debug.Assert(CompareHashes(hash, CN_LITE_SLOW_HASH_V2));
    //		  Debug.Assert(CompareHashes(new Crypto.Hash(hash), CN_LITE_SLOW_HASH_V2));

    //		  Console.Write("\n");

    //		  for (uint height = 0; height <= 8192; height = height + 512)
    //		  {
    //			Crypto.GlobalMembers.cn_soft_shell_slow_hash_v0(rawData.data(), rawData.size(), hash, new uint(height));
    //			Console.Write("cn_soft_shell_slow_hash_v0 (");
    //			Console.Write(height);
    //			Console.Write("): ");
    //			Console.Write(Common.toHex(hash, sizeof(Hash)));
    //			Console.Write("\n");
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
    ////ORIGINAL LINE: System.Diagnostics.Debug.Assert(CompareHashes(hash, CN_SOFT_SHELL_V0[height / 512]));
    //			Debug.Assert(CompareHashes(new Crypto.Hash(hash), CN_SOFT_SHELL_V0[height / 512]));
    //		  }

    //		  Console.Write("\n");

    //		  for (uint height = 0; height <= 8192; height = height + 512)
    //		  {
    //			Crypto.GlobalMembers.cn_soft_shell_slow_hash_v1(rawData.data(), rawData.size(), hash, new uint(height));
    //			Console.Write("cn_soft_shell_slow_hash_v1 (");
    //			Console.Write(height);
    //			Console.Write("): ");
    //			Console.Write(Common.toHex(hash, sizeof(Hash)));
    //			Console.Write("\n");
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
    ////ORIGINAL LINE: System.Diagnostics.Debug.Assert(CompareHashes(hash, CN_SOFT_SHELL_V1[height / 512]));
    //			Debug.Assert(CompareHashes(new Crypto.Hash(hash), CN_SOFT_SHELL_V1[height / 512]));
    //		  }

    //		  Console.Write("\n");

    //		  for (uint height = 0; height <= 8192; height = height + 512)
    //		  {
    //			Crypto.GlobalMembers.cn_soft_shell_slow_hash_v2(rawData.data(), rawData.size(), hash, new uint(height));
    //			Console.Write("cn_soft_shell_slow_hash_v2 (");
    //			Console.Write(height);
    //			Console.Write("): ");
    //			Console.Write(Common.toHex(hash, sizeof(Hash)));
    //			Console.Write("\n");
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
    ////ORIGINAL LINE: System.Diagnostics.Debug.Assert(CompareHashes(hash, CN_SOFT_SHELL_V2[height / 512]));
    //			Debug.Assert(CompareHashes(new Crypto.Hash(hash), CN_SOFT_SHELL_V2[height / 512]));
    //		  }
    //		}

    //		if (o_benchmark)
    //		{
    //		  Console.Write("\nPerformance Tests: Please wait, this may take a while depending on your system...\n\n");

    //		  var startTimer = std::chrono.high_resolution_clock.now();
    //		  for (var i = 0; i < DefineConstants.PERFORMANCE_ITERATIONS; i++)
    //		  {
    //			Crypto.GlobalMembers.cn_slow_hash_v0(rawData.data(), rawData.size(), hash);
    //		  }
    //		  var elapsedTime = std::chrono.high_resolution_clock.now() - startTimer;
    //		  Console.Write("cn_slow_hash_v0: ");
    //		  Console.Write((DefineConstants.PERFORMANCE_ITERATIONS / std::chrono.duration_cast<std::chrono.seconds>(elapsedTime).count()));
    //		  Console.Write(" H/s\n");

    //		  if (rawData.size() >= 43)
    //		  {
    //			startTimer = std::chrono.high_resolution_clock.now();
    //			for (var i = 0; i < DefineConstants.PERFORMANCE_ITERATIONS; i++)
    //			{
    //			  Crypto.GlobalMembers.cn_slow_hash_v1(rawData.data(), rawData.size(), hash);
    //			}
    //			elapsedTime = std::chrono.high_resolution_clock.now() - startTimer;
    //			Console.Write("cn_slow_hash_v1: ");
    //			Console.Write((DefineConstants.PERFORMANCE_ITERATIONS / std::chrono.duration_cast<std::chrono.seconds>(elapsedTime).count()));
    //			Console.Write(" H/s\n");

    //			startTimer = std::chrono.high_resolution_clock.now();
    //			for (var i = 0; i < DefineConstants.PERFORMANCE_ITERATIONS; i++)
    //			{
    //			  Crypto.GlobalMembers.cn_slow_hash_v2(rawData.data(), rawData.size(), hash);
    //			}
    //			elapsedTime = std::chrono.high_resolution_clock.now() - startTimer;
    //			Console.Write("cn_slow_hash_v2: ");
    //			Console.Write((DefineConstants.PERFORMANCE_ITERATIONS / std::chrono.duration_cast<std::chrono.seconds>(elapsedTime).count()));
    //			Console.Write(" H/s\n");
    //		  }

    //		  startTimer = std::chrono.high_resolution_clock.now();
    //		  for (var i = 0; i < DefineConstants.PERFORMANCE_ITERATIONS; i++)
    //		  {
    //			Crypto.GlobalMembers.cn_lite_slow_hash_v0(rawData.data(), rawData.size(), hash);
    //		  }
    //		  elapsedTime = std::chrono.high_resolution_clock.now() - startTimer;
    //		  Console.Write("cn_lite_slow_hash_v0: ");
    //		  Console.Write((DefineConstants.PERFORMANCE_ITERATIONS / std::chrono.duration_cast<std::chrono.seconds>(elapsedTime).count()));
    //		  Console.Write(" H/s\n");

    //		  if (rawData.size() >= 43)
    //		  {
    //			startTimer = std::chrono.high_resolution_clock.now();
    //			for (var i = 0; i < DefineConstants.PERFORMANCE_ITERATIONS; i++)
    //			{
    //			  Crypto.GlobalMembers.cn_lite_slow_hash_v1(rawData.data(), rawData.size(), hash);
    //			}
    //			elapsedTime = std::chrono.high_resolution_clock.now() - startTimer;
    //			Console.Write("cn_lite_slow_hash_v1: ");
    //			Console.Write((DefineConstants.PERFORMANCE_ITERATIONS / std::chrono.duration_cast<std::chrono.seconds>(elapsedTime).count()));
    //			Console.Write(" H/s\n");

    //			startTimer = std::chrono.high_resolution_clock.now();
    //			for (var i = 0; i < DefineConstants.PERFORMANCE_ITERATIONS; i++)
    //			{
    //			  Crypto.GlobalMembers.cn_lite_slow_hash_v2(rawData.data(), rawData.size(), hash);
    //			}
    //			elapsedTime = std::chrono.high_resolution_clock.now() - startTimer;
    //			Console.Write("cn_lite_slow_hash_v2: ");
    //			Console.Write((DefineConstants.PERFORMANCE_ITERATIONS / std::chrono.duration_cast<std::chrono.seconds>(elapsedTime).count()));
    //			Console.Write(" H/s\n");
    //		  }
    //		}
    //	  }
    //	  catch (System.Exception e)
    //	  {
    //		Console.Write("Something went terribly wrong...");
    //		Console.Write("\n");
    //		Console.Write(e.Message);
    //		Console.Write("\n");
    //		Console.Write("\n");
    //	  }

    //	}

    //	  public static DaemonConfiguration initConfiguration()
    //	  {
    //		DaemonConfiguration config = new DaemonConfiguration();

    //		std::stringstream logfile = new std::stringstream();
    //		  logfile << CryptoNote.CRYPTONOTE_NAME << "d.log";

    //		config.dataDirectory = Tools.getDefaultDataDirectory();
    //		config.checkPoints = "default";
    //		config.logFile = logfile.str();
    //		config.logLevel = (int)Logging.Level.WARNING;
    //		config.dbMaxOpenFiles = CryptoNote.DATABASE_DEFAULT_MAX_OPEN_FILES;
    //		config.dbReadCacheSize = CryptoNote.DATABASE_READ_BUFFER_MB_DEFAULT_SIZE;
    //		config.dbThreads = CryptoNote.DATABASE_DEFAULT_BACKGROUND_THREADS_COUNT;
    //		config.dbWriteBufferSize = CryptoNote.DATABASE_WRITE_BUFFER_MB_DEFAULT_SIZE;
    //		config.p2pInterface = "0.0.0.0";
    //		config.p2pPort = CryptoNote.P2P_DEFAULT_PORT;
    //		config.p2pExternalPort = 0;
    //		config.rpcInterface = "127.0.0.1";
    //		config.rpcPort = CryptoNote.RPC_DEFAULT_PORT;
    //		config.noConsole = false;
    //		config.enableBlockExplorer = false;
    //		config.localIp = false;
    //		config.hideMyPort = false;
    //		config.help = false;
    //		config.version = false;
    //		config.osVersion = false;
    //		config.printGenesisTx = false;
    //		config.dumpConfig = false;

    //		return config;
    //	  }

    //	  public static DaemonConfiguration initConfiguration(string path)
    //	  {
    //		DaemonConfiguration config = initConfiguration();

    //		config.logFile = Common.ReplaceExtenstion(Common.NativePathToGeneric(path), ".log");

    //		return config;
    //	  }

    //	  public static void handleSettings(int argc, string[] argv, DaemonConfiguration config)
    //	  {
    //		cxxopts.Options options = new cxxopts.Options(argv[0], CryptoNote.getProjectCLIHeader());

    //		options.add_options("Core")("help", "Display this help message", cxxopts.value<bool>().implicit_value("true"))("os-version", "Output Operating System version information", cxxopts.value<bool>().default_value("false").implicit_value("true"))("version","Output daemon version information",cxxopts.value<bool>().default_value("false").implicit_value("true"));

    //		options.add_options("Genesis Block")("genesis-block-reward-address", "Specify the address for any premine genesis block rewards", cxxopts.value<List<string>>(), "<address>")("print-genesis-tx", "Print the genesis block transaction hex and exits", cxxopts.value<bool>().default_value("false").implicit_value("true"));

    //		options.add_options("Daemon")("c,config-file", "Specify the <path> to a configuration file", cxxopts.value<string>(), "<path>")("data-dir", "Specify the <path> to the Blockchain data directory", cxxopts.value<string>().default_value(config.dataDirectory), "<path>")("dump-config", "Prints the current configuration to the screen", cxxopts.value<bool>().default_value("false").implicit_value("true"))("load-checkpoints", "Specify a file <path> containing a CSV of Blockchain checkpoints for faster sync. A value of 'default' uses the built-in checkpoints.", cxxopts.value<string>().default_value(config.checkPoints), "<path>")("log-file", "Specify the <path> to the log file", cxxopts.value<string>().default_value(config.logFile), "<path>")("log-level", "Specify log level", cxxopts.value<int>().default_value(Convert.ToString(config.logLevel)), "#")("no-console", "Disable daemon console commands", cxxopts.value<bool>().default_value("false").implicit_value("true"))("save-config", "Save the configuration to the specified <file>", cxxopts.value<string>(), "<file>");

    //		options.add_options("RPC")("enable-blockexplorer", "Enable the Blockchain Explorer RPC", cxxopts.value<bool>().default_value("false").implicit_value("true"))("enable-cors", "Adds header 'Access-Control-Allow-Origin' to the RPC responses using the <domain>. Uses the value specified as the domain. Use * for all.", cxxopts.value<List<string>>(), "<domain>")("fee-address", "Sets the convenience charge <address> for light wallets that use the daemon", cxxopts.value<string>(), "<address>")("fee-amount", "Sets the convenience charge amount for light wallets that use the daemon", cxxopts.value<int>().default_value("0"), "#");

    //		options.add_options("Network")("allow-local-ip", "Allow the local IP to be added to the peer list", cxxopts.value<bool>().default_value("false").implicit_value("true"))("hide-my-port", "Do not announce yourself as a peerlist candidate", cxxopts.value<bool>().default_value("false").implicit_value("true"))("p2p-bind-ip", "Interface IP address for the P2P service", cxxopts.value<string>().default_value(config.p2pInterface), "<ip>")("p2p-bind-port", "TCP port for the P2P service", cxxopts.value<int>().default_value(Convert.ToString(config.p2pPort)), "#")("p2p-external-port", "External TCP port for the P2P service (NAT port forward)", cxxopts.value<int>().default_value("0"), "#")("rpc-bind-ip", "Interface IP address for the RPC service", cxxopts.value<string>().default_value(config.rpcInterface), "<ip>")("rpc-bind-port", "TCP port for the RPC service", cxxopts.value<int>().default_value(Convert.ToString(config.rpcPort)), "#");

    //		options.add_options("Peer")("add-exclusive-node", "Manually add a peer to the local peer list ONLY attempt connections to it. [ip:port]", cxxopts.value<List<string>>(), "<ip:port>")("add-peer", "Manually add a peer to the local peer list", cxxopts.value<List<string>>(), "<ip:port>")("add-priority-node", "Manually add a peer to the local peer list and attempt to maintain a connection to it [ip:port]", cxxopts.value<List<string>>(), "<ip:port>")("seed-node", "Connect to a node to retrieve the peer list and then disconnect", cxxopts.value<List<string>>(), "<ip:port>");

    //		options.add_options("Database")("db-max-open-files", "Number of files that can be used by the database at one time", cxxopts.value<int>().default_value(Convert.ToString(config.dbMaxOpenFiles)), "#")("db-read-buffer-size", "Size of the database read cache in megabytes (MB)", cxxopts.value<int>().default_value(Convert.ToString(config.dbReadCacheSize)), "#")("db-threads", "Number of background threads used for compaction and flush operations", cxxopts.value<int>().default_value(Convert.ToString(config.dbThreads)), "#")("db-write-buffer-size", "Size of the database write buffer in megabytes (MB)", cxxopts.value<int>().default_value(Convert.ToString(config.dbWriteBufferSize)), "#");

    //		try
    //		{
    //		  var cli = options.parse(argc, argv);

    //		  if (cli.count("config-file") > 0)
    //		  {
    //			config.configFile = cli["config-file"].@as<string>();
    //		  }

    //		  if (cli.count("save-config") > 0)
    //		  {
    //			config.outputFile = cli["save-config"].@as<string>();
    //		  }

    //		  if (cli.count("genesis-block-reward-address") > 0)
    //		  {
    //			config.genesisAwardAddresses = cli["genesis-block-reward-address"].@as<List<string>>();
    //		  }

    //		  if (cli.count("help") > 0)
    //		  {
    //			config.help = cli["help"].@as<bool>();
    //		  }

    //		  if (cli.count("version") > 0)
    //		  {
    //			config.version = cli["version"].@as<bool>();
    //		  }

    //		  if (cli.count("os-version") > 0)
    //		  {
    //			config.osVersion = cli["os-version"].@as<bool>();
    //		  }

    //		  if (cli.count("print-genesis-tx") > 0)
    //		  {
    //			config.printGenesisTx = cli["print-genesis-tx"].@as<bool>();
    //		  }

    //		  if (cli.count("dump-config") > 0)
    //		  {
    //			config.dumpConfig = cli["dump-config"].@as<bool>();
    //		  }

    //		  if (cli.count("data-dir") > 0)
    //		  {
    //			config.dataDirectory = cli["data-dir"].@as<string>();
    //		  }

    //		  if (cli.count("load-checkpoints") > 0)
    //		  {
    //			config.checkPoints = cli["load-checkpoints"].@as<string>();
    //		  }

    //		  if (cli.count("log-file") > 0)
    //		  {
    //			config.logFile = cli["log-file"].@as<string>();
    //		  }

    //		  if (cli.count("log-level") > 0)
    //		  {
    //			config.logLevel = cli["log-level"].@as<int>();
    //		  }

    //		  if (cli.count("no-console") > 0)
    //		  {
    //			config.noConsole = cli["no-console"].@as<bool>();
    //		  }

    //		  if (cli.count("db-max-open-files") > 0)
    //		  {
    //			config.dbMaxOpenFiles = cli["db-max-open-files"].@as<int>();
    //		  }

    //		  if (cli.count("db-read-buffer-size") > 0)
    //		  {
    //			config.dbReadCacheSize = cli["db-read-buffer-size"].@as<int>();
    //		  }

    //		  if (cli.count("db-threads") > 0)
    //		  {
    //			config.dbThreads = cli["db-threads"].@as<int>();
    //		  }

    //		  if (cli.count("db-write-buffer-size") > 0)
    //		  {
    //			config.dbWriteBufferSize = cli["db-write-buffer-size"].@as<int>();
    //		  }

    //		  if (cli.count("local-ip") > 0)
    //		  {
    //			config.localIp = cli["local-ip"].@as<bool>();
    //		  }

    //		  if (cli.count("hide-my-port") > 0)
    //		  {
    //			config.hideMyPort = cli["hide-my-port"].@as<bool>();
    //		  }

    //		  if (cli.count("p2p-bind-ip") > 0)
    //		  {
    //			config.p2pInterface = cli["p2p-bind-ip"].@as<string>();
    //		  }

    //		  if (cli.count("p2p-bind-port") > 0)
    //		  {
    //			config.p2pPort = cli["p2p-bind-port"].@as<int>();
    //		  }

    //		  if (cli.count("p2p-external-port") > 0)
    //		  {
    //			config.p2pExternalPort = cli["p2p-external-port"].@as<int>();
    //		  }

    //		  if (cli.count("rpc-bind-ip") > 0)
    //		  {
    //			config.rpcInterface = cli["rpc-bind-ip"].@as<string>();
    //		  }

    //		  if (cli.count("rpc-bind-port") > 0)
    //		  {
    //			config.rpcPort = cli["rpc-bind-port"].@as<int>();
    //		  }

    //		  if (cli.count("add-exclusive-node") > 0)
    //		  {
    //			config.exclusiveNodes = cli["add-exclusive-node"].@as<List<string>>();
    //		  }

    //		  if (cli.count("add-peer") > 0)
    //		  {
    //			config.peers = cli["add-peer"].@as<List<string>>();
    //		  }

    //		  if (cli.count("add-priority-node") > 0)
    //		  {
    //			config.priorityNodes = cli["add-priority-node"].@as<List<string>>();
    //		  }

    //		  if (cli.count("seed-node") > 0)
    //		  {
    //			config.seedNodes = cli["seed-node"].@as<List<string>>();
    //		  }

    //		  if (cli.count("enable-blockexplorer") > 0)
    //		  {
    //			config.enableBlockExplorer = cli["enable-blockexplorer"].@as<bool>();
    //		  }

    //		  if (cli.count("enable-cors") > 0)
    //		  {
    //			config.enableCors = cli["enable-cors"].@as<List<string>>();
    //		  }

    //		  if (cli.count("fee-address") > 0)
    //		  {
    //			config.feeAddress = cli["fee-address"].@as<string>();
    //		  }

    //		  if (cli.count("fee-amount") > 0)
    //		  {
    //			config.feeAmount = cli["fee-amount"].@as<int>();
    //		  }

    //		  if (config.help) // Do we want to display the help message?
    //		  {
    //			Console.Write(options.help({}));
    //			Console.Write("\n");
    //			Environment.Exit(0);
    //		  }
    //		  else if (config.version) // Do we want to display the software version?
    //		  {
    //			Console.Write(CryptoNote.getProjectCLIHeader());
    //			Console.Write("\n");
    //			Environment.Exit(0);
    //		  }
    //		  else if (config.osVersion) // Do we want to display the OS version information?
    //		  {
    //			Console.Write(CryptoNote.getProjectCLIHeader());
    //			Console.Write("OS: ");
    //			Console.Write(Tools.get_os_version_string());
    //			Console.Write("\n");
    //			Environment.Exit(0);
    //		  }
    //		}
    //		catch (cxxopts.OptionException e)
    //		{
    //		  Console.Write("Error: Unable to parse command line argument options: ");
    //		  Console.Write(e.what());
    //		  Console.Write("\n");
    //		  Console.Write("\n");
    //		  Console.Write(options.help({}));
    //		  Console.Write("\n");
    //		  Environment.Exit(1);
    //		}
    //	  }

    //	  public static void handleSettings(string configFile, DaemonConfiguration config)
    //	  {
    //		std::ifstream data = new std::ifstream(configFile);

    //		if (!data.good())
    //		{
    //		  throw new System.Exception("The --config-file you specified does not exist, please check the filename and try again.");
    //		}

    //		json j = new json();
    //		data >> j;

    //		if (j.find("data-dir") != j.end())
    //		{
    //		  config.dataDirectory = j["data-dir"].get<string>();
    //		}

    //		if (j.find("load-checkpoints") != j.end())
    //		{
    //		  config.checkPoints = j["load-checkpoints"].get<string>();
    //		}

    //		if (j.find("log-file") != j.end())
    //		{
    //		  config.logFile = j["log-file"].get<string>();
    //		}

    //		if (j.find("log-level") != j.end())
    //		{
    //		  config.logLevel = j["log-level"].get<int>();
    //		}

    //		if (j.find("no-console") != j.end())
    //		{
    //		  config.noConsole = j["no-console"].get<bool>();
    //		}

    //		if (j.find("db-max-open-files") != j.end())
    //		{
    //		  config.dbMaxOpenFiles = j["db-max-open-files"].get<int>();
    //		}

    //		if (j.find("db-read-buffer-size") != j.end())
    //		{
    //		  config.dbReadCacheSize = j["db-read-buffer-size"].get<int>();
    //		}

    //		if (j.find("db-threads") != j.end())
    //		{
    //		  config.dbThreads = j["db-threads"].get<int>();
    //		}

    //		if (j.find("db-write-buffer-size") != j.end())
    //		{
    //		  config.dbWriteBufferSize = j["db-write-buffer-size"].get<int>();
    //		}

    //		if (j.find("allow-local-ip") != j.end())
    //		{
    //		  config.localIp = j["allow-local-ip"].get<bool>();
    //		}

    //		if (j.find("hide-my-port") != j.end())
    //		{
    //		  config.hideMyPort = j["hide-my-port"].get<bool>();
    //		}

    //		if (j.find("p2p-bind-ip") != j.end())
    //		{
    //		  config.p2pInterface = j["p2p-bind-ip"].get<string>();
    //		}

    //		if (j.find("p2p-bind-port") != j.end())
    //		{
    //		  config.p2pPort = j["p2p-bind-port"].get<int>();
    //		}

    //		if (j.find("p2p-external-port") != j.end())
    //		{
    //		  config.p2pExternalPort = j["p2p-external-port"].get<int>();
    //		}

    //		if (j.find("rpc-bind-ip") != j.end())
    //		{
    //		  config.rpcInterface = j["rpc-bind-ip"].get<string>();
    //		}

    //		if (j.find("rpc-bind-port") != j.end())
    //		{
    //		  config.rpcPort = j["rpc-bind-port"].get<int>();
    //		}

    //		if (j.find("add-exclusive-node") != j.end())
    //		{
    //		  config.exclusiveNodes = j["add-exclusive-node"].get<List<string>>();
    //		}

    //		if (j.find("add-peer") != j.end())
    //		{
    //		  config.peers = j["add-peer"].get<List<string>>();
    //		}

    //		if (j.find("add-priority-node") != j.end())
    //		{
    //		  config.priorityNodes = j["add-priority-node"].get<List<string>>();
    //		}

    //		if (j.find("seed-node") != j.end())
    //		{
    //		  config.seedNodes = j["seed-node"].get<List<string>>();
    //		}

    //		if (j.find("enable-blockexplorer") != j.end())
    //		{
    //		  config.enableBlockExplorer = j["enable-blockexplorer"].get<bool>();
    //		}

    //		if (j.find("enable-cors") != j.end())
    //		{
    //		  config.enableCors = j["enable-cors"].get<List<string>>();
    //		}

    //		if (j.find("fee-address") != j.end())
    //		{
    //		  config.feeAddress = j["fee-address"].get<string>();
    //		}

    //		if (j.find("fee-amount") != j.end())
    //		{
    //		  config.feeAmount = j["fee-amount"].get<int>();
    //		}
    //	  }

    //	  public static json asJSON(DaemonConfiguration config)
    //	  {
    //		json j = new json(
    //		{
    //			{"data-dir", config.dataDirectory},
    //			{"load-checkpoints", config.checkPoints},
    //			{"log-file", config.logFile},
    //			{"log-level", config.logLevel},
    //			{"no-console", config.noConsole},
    //			{"db-max-open-files", config.dbMaxOpenFiles},
    //			{"db-read-buffer-size", config.dbReadCacheSize},
    //			{"db-threads", config.dbThreads},
    //			{"db-write-buffer-size", config.dbWriteBufferSize},
    //			{"allow-local-ip", config.localIp},
    //			{"hide-my-port", config.hideMyPort},
    //			{"p2p-bind-ip", config.p2pInterface},
    //			{"p2p-bind-port", config.p2pPort},
    //			{"p2p-external-port", config.p2pExternalPort},
    //			{"rpc-bind-ip", config.rpcInterface},
    //			{"rpc-bind-port", config.rpcPort},
    //			{"add-exclusive-node", config.exclusiveNodes},
    //			{"add-peer", config.peers},
    //			{"add-priority-node", config.priorityNodes},
    //			{"seed-node", config.seedNodes},
    //			{"enable-blockexplorer", config.enableBlockExplorer},
    //			{"enable-cors", config.enableCors},
    //			{"fee-address", config.feeAddress},
    //			{"fee-amount", config.feeAmount}
    //		});

    //		return j;
    //	  }

    //	  public static string asString(DaemonConfiguration config)
    //	  {
    //		json j = asJSON(config);
    //		return j.dump(2);
    //	  }

    //	  public static void asFile(DaemonConfiguration config, string filename)
    //	  {
    //		json j = asJSON(config);
    //		std::ofstream data = new std::ofstream(filename);
    //		data << std::setw(2) << j << std::endl;
    //	  }

    //	public static void print_genesis_tx_hex(List<string> rewardAddresses, bool blockExplorerMode, LoggerManager logManager)
    //	{
    //	  List<CryptoNote.AccountPublicAddress> rewardTargets = new List<CryptoNote.AccountPublicAddress>();

    //	  CryptoNote.CurrencyBuilder currencyBuilder = new CryptoNote.CurrencyBuilder(logManager);
    //	  currencyBuilder.isBlockexplorer(blockExplorerMode);

    //	  CryptoNote.Currency currency = currencyBuilder.currency();

    //	  foreach (var rewardAddress in rewardAddresses)
    //	  {
    //		CryptoNote.AccountPublicAddress address = new CryptoNote.AccountPublicAddress();
    //		if (!currency.parseAccountAddressString(rewardAddress, address))
    //		{
    //		  Console.Write("Failed to parse genesis reward address: ");
    //		  Console.Write(rewardAddress);
    //		  Console.Write("\n");
    //		  return;
    //		}
    //		rewardTargets.emplace_back(std::move(address));
    //	  }

    //	  CryptoNote.Transaction transaction = new CryptoNote.Transaction();

    //	  if (rewardTargets.Count == 0)
    //	  {
    //		if (CryptoNote.parameters.GENESIS_BLOCK_REWARD > 0)
    //		{
    //		  Console.Write("Error: Genesis Block Reward Addresses are not defined");
    //		  Console.Write("\n");
    //		  return;
    //		}
    //		transaction = new CryptoNote.CurrencyBuilder(logManager).generateGenesisTransaction();
    //	  }
    //	  else
    //	  {
    //		transaction = new CryptoNote.CurrencyBuilder(logManager).generateGenesisTransaction();
    //	  }

    //	  string transactionHex = Common.toHex(CryptoNote.GlobalMembers.toBinaryArray(transaction));
    //	  Console.Write(getProjectCLIHeader());
    //	  Console.Write("\n");
    //	  Console.Write("\n");
    //	  Console.Write("Replace the current GENESIS_COINBASE_TX_HEX line in src/config/CryptoNoteConfig.h with this one:");
    //	  Console.Write("\n");
    //	  Console.Write("const char GENESIS_COINBASE_TX_HEX[] = \"");
    //	  Console.Write(transactionHex);
    //	  Console.Write("\";");
    //	  Console.Write("\n");

    //	  return;
    //	}

    //	public static JsonValue buildLoggerConfiguration(Level level, string logfile)
    //	{
    //	  JsonValue loggerConfiguration = new JsonValue(JsonValue.OBJECT);
    //	  loggerConfiguration.insert("globalLevel", (long)level);

    //	  JsonValue cfgLoggers = loggerConfiguration.insert("loggers", JsonValue.ARRAY);

    //	  JsonValue fileLogger = cfgLoggers.pushBack(JsonValue.OBJECT);
    //	  fileLogger.insert("type", "file");
    //	  fileLogger.insert("filename", logfile);
    //	  fileLogger.insert("level", (long)TRACE);

    //	  JsonValue consoleLogger = cfgLoggers.pushBack(JsonValue.OBJECT);
    //	  consoleLogger.insert("type", "console");
    //	  consoleLogger.insert("level", (long)TRACE);
    //	  consoleLogger.insert("pattern", "%D %T %L ");

    //	  return loggerConfiguration;
    //	}

    //	/* Wait for input so users can read errors before the window closes if they
    //	   launch from a GUI rather than a terminal */
    //	public static void pause_for_input(int argc)
    //	{
    //	  /* if they passed arguments they're probably in a terminal so the errors will
    //	     stay visible */
    //	  if (argc == 1)
    //	  {
    //		#if WIN32
    //		if (_isatty(_fileno(stdout)) && _isatty(_fileno(stdin)))
    //		{
    //		#else
    //		if (isatty(fileno(stdout)) && isatty(fileno(stdin)))
    //		{
    //		#endif
    //		  Console.Write("Press any key to close the program: ");
    //		  Console.Read();
    //		}
    //	  }
    //	}

    //	static int Main(int argc, string[] args)
    //	{
    //	  DaemonConfiguration config = initConfiguration(args[0]);

    //	#if WIN32
    //	  _CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);
    //	#endif

    //	  LoggerManager logManager = new LoggerManager();
    //	  LoggerRef logger = new LoggerRef(logManager, "daemon");

    //	  // Initial loading of CLI parameters
    //	  handleSettings(argc, args, config);

    //	  if (config.printGenesisTx) // Do we weant to generate the Genesis Tx?
    //	  {
    //		print_genesis_tx_hex(new List<string>(config.genesisAwardAddresses), false, logManager);
    //		Environment.Exit(0);
    //	  }

    //	  // If the user passed in the --config-file option, we need to handle that first
    //	  if (!string.IsNullOrEmpty(config.configFile))
    //	  {
    //		try
    //		{
    //		  handleSettings(config.configFile, config);
    //		}
    //		catch (System.Exception e)
    //		{
    //		  Console.Write("\n");
    //		  Console.Write("There was an error parsing the specified configuration file. Please check the file and try again");
    //		  Console.Write("\n");
    //		  Console.Write(e.Message);
    //		  Console.Write("\n");
    //		  Environment.Exit(1);
    //		}
    //	  }

    //	  // Load in the CLI specified parameters again to overwrite anything from the config file
    //	  handleSettings(argc, args, config);

    //	  if (config.dumpConfig)
    //	  {
    //		Console.Write(getProjectCLIHeader());
    //		Console.Write(asString(config));
    //		Console.Write("\n");
    //		Environment.Exit(0);
    //	  }
    //	  else if (!string.IsNullOrEmpty(config.outputFile))
    //	  {
    //		try
    //		{
    //		  asFile(config, config.outputFile);
    //		  Console.Write(getProjectCLIHeader());
    //		  Console.Write("Configuration saved to: ");
    //		  Console.Write(config.outputFile);
    //		  Console.Write("\n");
    //		  Environment.Exit(0);
    //		}
    //		catch (System.Exception e)
    //		{
    //		  Console.Write(getProjectCLIHeader());
    //		  Console.Write("Could not save configuration to: ");
    //		  Console.Write(config.outputFile);
    //		  Console.Write("\n");
    //		  Console.Write(e.Message);
    //		  Console.Write("\n");
    //		  Environment.Exit(1);
    //		}
    //	  }

    //	  try
    //	  {
    //		var modulePath = Common.NativePathToGeneric(args[0]);
    //		var cfgLogFile = Common.NativePathToGeneric(config.logFile);

    //		if (cfgLogFile.empty())
    //		{
    //		  cfgLogFile = Common.ReplaceExtenstion(modulePath, ".log");
    //		}
    //		else
    //		{
    //		  if (!Common.HasParentPath(cfgLogFile))
    //		  {
    //	  cfgLogFile = Common.CombinePath(Common.GetPathDirectory(modulePath), cfgLogFile);
    //		  }
    //		}

    //		Level cfgLogLevel = (Level)((int)Logging.Level.ERROR + config.logLevel);

    //		// configure logging
    //		logManager.configure(buildLoggerConfiguration(cfgLogLevel, cfgLogFile));

    //		logger.functorMethod(INFO, BRIGHT_GREEN) << getProjectCLIHeader() << std::endl;

    //		logger.functorMethod(INFO) << "Program Working Directory: " << args[0];

    //		//create objects and link them
    //		CryptoNote.CurrencyBuilder currencyBuilder = new CryptoNote.CurrencyBuilder(logManager);
    //		currencyBuilder.isBlockexplorer(config.enableBlockExplorer);

    //		try
    //		{
    //		  currencyBuilder.currency();
    //		}
    //		catch (System.Exception)
    //		{
    //		  Console.Write("GENESIS_COINBASE_TX_HEX constant has an incorrect value. Please launch: ");
    //		  Console.Write(CryptoNote.CRYPTONOTE_NAME);
    //		  Console.Write("d --print-genesis-tx");
    //		  Console.Write("\n");
    //		  return 1;
    //		}
    //		CryptoNote.Currency currency = currencyBuilder.currency();

    //		bool use_checkpoints = !string.IsNullOrEmpty(config.checkPoints);
    //		CryptoNote.Checkpoints checkpoints = new CryptoNote.Checkpoints(logManager);

    //		if (use_checkpoints)
    //		{
    //		  logger.functorMethod(INFO) << "Loading Checkpoints for faster initial sync...";
    //		  if (config.checkPoints == "default")
    //		  {
    //			foreach (var cp in CryptoNote.CHECKPOINTS)
    //			{
    //			  checkpoints.addCheckpoint(cp.index, cp.blockId);
    //			}
    //			  logger.functorMethod(INFO) << "Loaded " << CryptoNote.CHECKPOINTS.size() << " default checkpoints";
    //		  }
    //		  else
    //		  {
    //			bool results = checkpoints.loadCheckpointsFromFile(config.checkPoints);
    //			if (!results)
    //			{
    //			  throw new System.Exception("Failed to load checkpoints");
    //			}
    //		  }
    //		}

    //		NetNodeConfig netNodeConfig = new NetNodeConfig();
    //		netNodeConfig.init(config.p2pInterface, config.p2pPort, config.p2pExternalPort, config.localIp, config.hideMyPort, config.dataDirectory, new List<string>(config.peers), new List<string>(config.exclusiveNodes), new List<string>(config.priorityNodes), new List<string>(config.seedNodes));

    //		DataBaseConfig dbConfig = new DataBaseConfig();
    //		dbConfig.init(config.dataDirectory, config.dbThreads, config.dbMaxOpenFiles, config.dbWriteBufferSize, config.dbReadCacheSize);

    //		if (dbConfig.isConfigFolderDefaulted())
    //		{
    //		  if (!Tools.create_directories_if_necessary(dbConfig.getDataDir()))
    //		  {
    //			throw new System.Exception("Can't create directory: " + dbConfig.getDataDir());
    //		  }
    //		}
    //		else
    //		{
    //		  if (!Tools.directoryExists(dbConfig.getDataDir()))
    //		  {
    //			throw new System.Exception("Directory does not exist: " + dbConfig.getDataDir());
    //		  }
    //		}

    //		RocksDBWrapper database = new RocksDBWrapper(logManager);
    //		database.init(dbConfig);
    //	Tools.ScopeExit dbShutdownOnExit(() =>
    //	{
    //		database.shutdown();
    //	});

    //		if (!DatabaseBlockchainCache.checkDBSchemeVersion(database, logManager))
    //		{
    //		  dbShutdownOnExit.cancel();
    //		  database.shutdown();

    //		  database.destroy(dbConfig);

    //		  database.init(dbConfig);
    //		  dbShutdownOnExit.resume();
    //		}

    //		System.Dispatcher dispatcher = new System.Dispatcher();
    //		logger.functorMethod(INFO) << "Initializing core...";
    //		CryptoNote.Core ccore = new CryptoNote.Core(currency, logManager, std::move(checkpoints), dispatcher, std::unique_ptr<IBlockchainCacheFactory>(new DatabaseBlockchainCacheFactory(database, logger.getLogger())), createSwappedMainChainStorage(config.dataDirectory, currency));

    //		ccore.load();
    //		logger.functorMethod(INFO) << "Core initialized OK";

    //		CryptoNote.CryptoNoteProtocolHandler cprotocol = new CryptoNote.CryptoNoteProtocolHandler(currency, dispatcher, ccore, null, logManager);
    //		CryptoNote.NodeServer p2psrv = new CryptoNote.NodeServer(dispatcher, cprotocol, logManager);
    //		CryptoNote.RpcServer rpcServer = new CryptoNote.RpcServer(dispatcher, logManager, ccore, p2psrv, cprotocol);

    //		cprotocol.set_p2p_endpoint(p2psrv);
    //		DaemonCommandsHandler dch = new DaemonCommandsHandler(ccore, p2psrv, logManager, rpcServer);
    //		logger.functorMethod(INFO) << "Initializing p2p server...";
    //		if (!p2psrv.init(netNodeConfig))
    //		{
    //		  logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to initialize p2p server.";
    //		  return 1;
    //		}

    //		logger.functorMethod(INFO) << "P2p server initialized OK";

    //		if (!config.noConsole)
    //		{
    //		  dch.start_handling();
    //		}

    //		// Fire up the RPC Server
    //		logger.functorMethod(INFO) << "Starting core rpc server on address " << config.rpcInterface << ":" << config.rpcPort;
    //		rpcServer.setFeeAddress(config.feeAddress);
    //		rpcServer.setFeeAmount(config.feeAmount);
    //		rpcServer.enableCors(new List<string>(config.enableCors));
    //		rpcServer.start(config.rpcInterface, config.rpcPort);
    //		logger.functorMethod(INFO) << "Core rpc server started ok";

    //	Tools.SignalHandler.install(() =>
    //	{
    //	  dch.stop_handling();
    //	  p2psrv.sendStopSignal();
    //	});

    //		logger.functorMethod(INFO) << "Starting p2p net loop...";
    //		p2psrv.run();
    //		logger.functorMethod(INFO) << "p2p net loop stopped";

    //		dch.stop_handling();

    //		//stop components
    //		logger.functorMethod(INFO) << "Stopping core rpc server...";
    //		rpcServer.stop();

    //		//deinitialize components
    //		logger.functorMethod(INFO) << "Deinitializing p2p...";
    //		p2psrv.deinit();

    //		cprotocol.set_p2p_endpoint(null);
    //		ccore.save();

    //	  }
    //	  catch (System.Exception e)
    //	  {
    //		logger.functorMethod(ERROR, BRIGHT_RED) << "Exception: " << e.Message;
    //		return 1;
    //	  }

    //	  logger.functorMethod(INFO) << "Node stopped.";
    //	}

    //	internal static bool print_as_json<T>(T obj)
    //	{
    //	  Console.Write(CryptoNote.GlobalMembers.storeToJson(obj));
    //	  Console.Write("\n");
    //	  return true;
    //	}

    //	public static string printTransactionShortInfo(CryptoNote.CachedTransaction transaction)
    //	{
    //	  std::stringstream ss = new std::stringstream();

    //	  ss << "id: " << transaction.getTransactionHash() << std::endl;
    //	  ss << "fee: " << transaction.getTransactionFee() << std::endl;
    //	  ss << "blobSize: " << transaction.getTransactionBinaryArray().size() << std::endl;

    //	  return ss.str();
    //	}

    //	public static string printTransactionFullInfo(CryptoNote.CachedTransaction transaction)
    //	{
    //	  std::stringstream ss = new std::stringstream();
    //	  ss << printTransactionShortInfo(transaction);
    //	  ss << "JSON: \n" << CryptoNote.GlobalMembers.storeToJson(transaction.getTransaction()) << std::endl;

    //	  return ss.str();
    //	}



    //	public static void throwIfNotGood(std::istream stream)
    //	{
    //	  if (!stream.good())
    //	  {
    //		if (stream.eof())
    //		{
    //		  throw std::system_error(make_error_code(CryptoNote.error.HttpParserErrorCodes.END_OF_STREAM));
    //		}
    //		else
    //		{
    //		  throw std::system_error(make_error_code(CryptoNote.error.HttpParserErrorCodes.STREAM_NOT_GOOD));
    //		}
    //	  }
    //	}

    //	public static std::error_code make_error_code(CryptoNote.error.HttpParserErrorCodes e)
    //	{
    //	  return std::error_code((int)e, CryptoNote.error.HttpParserErrorCategory.INSTANCE);
    //	}

    //	public static string getStatusString(CryptoNote.HttpResponse.HTTP_STATUS status)
    //	{
    //	  switch (status)
    //	  {
    //	  case CryptoNote.HttpResponse.STATUS_200:
    //		return "200 OK";
    //	  case CryptoNote.HttpResponse.STATUS_404:
    //		return "404 Not Found";
    //	  case CryptoNote.HttpResponse.STATUS_500:
    //		return "500 Internal Server Error";
    //	  default:
    //		throw new System.Exception("Unknown HTTP status code is given");
    //	  }

    //	  return ""; //unaccessible
    //	}

    //	public static string getErrorBody(CryptoNote.HttpResponse.HTTP_STATUS status)
    //	{
    //	  switch (status)
    //	  {
    //	  case CryptoNote.HttpResponse.STATUS_404:
    //		return "Requested url is not found\n";
    //	  case CryptoNote.HttpResponse.STATUS_500:
    //		return "Internal server error is occurred\n";
    //	  default:
    //		throw new System.Exception("Error body for given status is not available");
    //	  }

    //	  return ""; //unaccessible
    //	}



    //	static int Main(int argc, string[] args)
    //	{
    //	  try
    //	  {
    //		CryptoNote.MiningConfig config = new CryptoNote.MiningConfig();
    //		config.parse(argc, args);

    //		Logging.LoggerGroup loggerGroup = new Logging.LoggerGroup();
    //		Logging.ConsoleLogger consoleLogger = new Logging.ConsoleLogger((Logging.Level)config.logLevel);
    //		loggerGroup.addLogger(consoleLogger);

    //		System.Dispatcher dispatcher = new System.Dispatcher();
    //		Miner.MinerManager app = new Miner.MinerManager(dispatcher, config, loggerGroup.functorMethod);

    //		app.start();
    //	  }
    //	  catch (System.Exception e)
    //	  {
    //		std::cerr << "Fatal: " << e.Message << std::endl;
    //		return 1;
    //	  }

    //	}


    //	public static std::error_code make_error_code(CryptoNote.error.NodeErrorCodes e)
    //	{
    //	  return std::error_code((int)e, CryptoNote.error.NodeErrorCategory.INSTANCE);
    //	}

    //	public static readonly ulong LEVIN_SIGNATURE = 0x0101010101012101L; //Bender's nightmare
    //	public static readonly uint LEVIN_PACKET_REQUEST = 0x00000001;
    //	public static readonly uint LEVIN_PACKET_RESPONSE = 0x00000002;
    //	public static readonly uint LEVIN_DEFAULT_MAX_PACKET_SIZE = 100000000; //100MB by default
    //	public static readonly uint LEVIN_PROTOCOL_VER_1 = 1;

    //	public static uint get_random_index_with_fixed_probability(uint max_index)
    //	{
    //	  //divide by zero workaround
    //	  if (max_index == null)
    //	  {
    //		return 0;
    //	  }
    //	  uint x = Crypto.GlobalMembers.rand<uint>() % (max_index + 1);
    //	  return (x * x * x) / (max_index * max_index); //parabola \/
    //	}


    //	public static void addPortMapping(Logging.LoggerRef logger, uint port)
    //	{
    //	  // Add UPnP port mapping
    //	  logger.functorMethod(INFO) << "Attempting to add IGD port mapping.";
    //	  int result;
    //	  UPNPDev deviceList = upnpDiscover(1000, null, null, 0, 0, result);
    //	  UPNPUrls urls = new UPNPUrls();
    //	  IGDdatas igdData = new IGDdatas();
    //	  string lanAddress = new string(new char[64]);
    //	  result = UPNP_GetValidIGD(deviceList, urls, igdData, lanAddress, sizeof (char));
    //	  freeUPNPDevlist(deviceList);
    //	  if (result != 0)
    //	  {
    //		if (result == 1)
    //		{
    //		  std::ostringstream portString = new std::ostringstream();
    //		  portString << port;
    //		  if (UPNP_AddPortMapping(urls.controlURL, igdData.first.servicetype, portString.str().c_str(), portString.str().c_str(), lanAddress, CryptoNote.CRYPTONOTE_NAME, "TCP", 0, "0") != 0)
    //		  {
    //			logger.functorMethod(ERROR) << "UPNP_AddPortMapping failed.";
    //		  }
    //		  else
    //		  {
    //			logger.functorMethod(INFO) << "Added IGD port mapping.";
    //		  }
    //		}
    //		else if (result == 2)
    //		{
    //		  logger.functorMethod(INFO) << "IGD was found but reported as not connected.";
    //		}
    //		else if (result == 3)
    //		{
    //		  logger.functorMethod(INFO) << "UPnP device was found but not recognized as IGD.";
    //		}
    //		else
    //		{
    //		  logger.functorMethod(ERROR) << "UPNP_GetValidIGD returned an unknown result code.";
    //		}

    //		FreeUPNPUrls(urls);
    //	  }
    //	  else
    //	  {
    //		logger.functorMethod(INFO) << "No IGD was found.";
    //	  }
    //	}

    //	public static bool parse_peer_from_string(NetworkAddress pe, string node_addr)
    //	{
    //	  return Common.parseIpAddressAndPort(pe.ip, pe.port, node_addr);
    //	}




    //	//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
    //	//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

    //	public static void serialize(NetworkAddress na, CryptoNote.ISerializer s)
    //	{
    //		s.functorMethod(na.ip, "ip");
    //		s.functorMethod(na.port, "port");
    //	}

    //	public static void serialize(PeerlistEntry pe, CryptoNote.ISerializer s)
    //	{
    //		s.functorMethod(pe.adr, "adr");
    //		s.functorMethod(pe.id, "id");
    //		s.functorMethod(pe.last_seen, "last_seen");
    //	}



    //	//int swapcontext(ucontext UnnamedParameter, ucontext UnnamedParameter2);Tangible Method Implementation Not Found-swapcontext
    //private delegate void Delegate();

    //	//void makecontext(ucontext UnnamedParameter, voidDelegate void, intptr_t UnnamedParameter2);Tangible Method Implementation Not Found-makecontext
    //	//int getmcontext(mcontext UnnamedParameter);Tangible Method Implementation Not Found-getmcontext
    //	//void setmcontext(mcontext UnnamedParameter);Tangible Method Implementation Not Found-setmcontext
    //private delegate void funcDelegate();



    //	//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
    //	public static void makecontext(uctx ucp, funcDelegate func, intptr_t arg)
    //	{
    ////C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on this variable, so pointers on this variable are left unchanged:
    //	  int * sp;

    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
    //	  memset(ucp.uc_mcontext, 0, sizeof (ucp.uc_mcontext));
    //	  ucp.uc_mcontext.mc_rdi = (int)arg;
    //	  sp = (int)ucp.uc_stack.ss_sp + ucp.uc_stack.ss_size / sizeof(int);
    //	  sp -= 1;
    //	  sp = (object)((uintptr_t)sp - (uintptr_t)sp % 16); // 16-align for OS X
    //	  *--sp = 0; // return address
    //	  ucp.uc_mcontext.mc_rip = (int)func;
    //	  ucp.uc_mcontext.mc_rsp = (int)sp;
    //	}

    //	//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
    //	public static int swapcontext(uctx oucp, uctx ucp)
    //	{
    //	  if (getmcontext((oucp).uc_mcontext) == 0)
    //	  {
    //		setmcontext((ucp).uc_mcontext);
    //	  }
    //	  return 0;
    //	}

    //	public static void insertOrPush<T>(JsonValue js, Common.StringView name, T value)
    //	{
    //	  if (js.isArray())
    //	  {
    //		js.pushBack(new JsonValue(value));
    //	  }
    //	  else
    //	  {
    //		js.insert((string)name, new JsonValue(value));
    //	  }
    //	}


    //	public static T readPod<T>(Common.IInputStream s)
    //	{
    //	  T v = new default(T);
    //	  read(s, v, sizeof(T));
    //	  return v;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: C++ template specifiers containing defaults cannot be converted to C#:
    ////ORIGINAL LINE: template <typename T, typename JsonT = T>
    //	public static JsonValue readPodJson<T, JsonT = T>(Common.IInputStream s)
    //	{
    //	  JsonValue jv = new JsonValue();
    //	  jv = (JsonT)(readPod<T>(s));
    //	  return jv.functorMethod;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T>
    //	public static JsonValue readIntegerJson<T>(Common.IInputStream s)
    //	{
    //	  return readPodJson<T, long>(s);
    //	}

    //	public static ulong readVarint(Common.IInputStream s)
    //	{
    //	  ushort b = read<ushort>(s);
    //	  ushort size_mask = b & GlobalMembers.PORTABLE_RAW_SIZE_MARK_MASK;
    //	  ulong bytesLeft = 0;

    //	  switch (size_mask)
    //	  {
    //	  case GlobalMembers.PORTABLE_RAW_SIZE_MARK_BYTE:
    //		bytesLeft = 0;
    //		break;
    //	  case GlobalMembers.PORTABLE_RAW_SIZE_MARK_WORD:
    //		bytesLeft = 1;
    //		break;
    //	  case GlobalMembers.PORTABLE_RAW_SIZE_MARK_DWORD:
    //		bytesLeft = 3;
    //		break;
    //	  case GlobalMembers.PORTABLE_RAW_SIZE_MARK_INT64:
    //		bytesLeft = 7;
    //		break;
    //	  }

    //	  ulong value = b;

    //	  for (ulong i = 1; i <= bytesLeft; ++i)
    //	  {
    //		ulong n = read<ushort>(s);
    //		value |= n << (i * 8);
    //	  }

    //	  value >>= 2;
    //	  return value;
    //	}

    //	public static string readString(Common.IInputStream s)
    //	{
    //	  var size = readVarint(s);
    //	  string str;
    //	  str.resize(size);
    //	  if (size != null)
    //	  {
    //		read(s, str[0], size);
    //	  }
    //	  return str;
    //	}

    //	public static JsonValue readStringJson(Common.IInputStream s)
    //	{
    //	  return new JsonValue(readString(s));
    //	}

    //	public static void readName(Common.IInputStream s, string name)
    //	{
    //	  ushort len = readPod<ushort>(s);
    //	  if (len != null)
    //	  {
    //		name.resize(len);
    //		read(s, name[0], len);
    //	  }
    //	}

    //	public static JsonValue loadValue(Common.IInputStream stream, ushort type)
    //	{
    //	  switch (type)
    //	  {
    //	  case GlobalMembers.BIN_KV_SERIALIZE_TYPE_INT64:
    //		  return readIntegerJson<long>(stream);
    //	  case GlobalMembers.BIN_KV_SERIALIZE_TYPE_INT32:
    //		  return readIntegerJson<int>(stream);
    //	  case GlobalMembers.BIN_KV_SERIALIZE_TYPE_INT16:
    //		  return readIntegerJson<short>(stream);
    //	  case GlobalMembers.BIN_KV_SERIALIZE_TYPE_INT8:
    //		  return readIntegerJson<short>(stream);
    //	  case GlobalMembers.BIN_KV_SERIALIZE_TYPE_UINT64:
    //		  return readIntegerJson<ulong>(stream);
    //	  case GlobalMembers.BIN_KV_SERIALIZE_TYPE_UINT32:
    //		  return readIntegerJson<uint>(stream);
    //	  case GlobalMembers.BIN_KV_SERIALIZE_TYPE_UINT16:
    //		  return readIntegerJson<ushort>(stream);
    //	  case GlobalMembers.BIN_KV_SERIALIZE_TYPE_UINT8:
    //		  return readIntegerJson<ushort>(stream);
    //	  case GlobalMembers.BIN_KV_SERIALIZE_TYPE_DOUBLE:
    //		  return readPodJson<double>(stream);
    //	  case GlobalMembers.BIN_KV_SERIALIZE_TYPE_BOOL:
    //		  return new JsonValue(read<ushort>(stream) != 0);
    //	  case GlobalMembers.BIN_KV_SERIALIZE_TYPE_STRING:
    //		  return readStringJson(stream);
    //	  case GlobalMembers.BIN_KV_SERIALIZE_TYPE_OBJECT:
    //		  return loadSection(stream);
    //	  case GlobalMembers.BIN_KV_SERIALIZE_TYPE_ARRAY:
    //		  return loadArray(stream, new ushort(type));
    //	  default:
    //		throw new System.Exception("Unknown data type");
    //		break;
    //	  }
    //	}
    //	public static JsonValue loadSection(Common.IInputStream stream)
    //	{
    //	  JsonValue sec = new JsonValue(JsonValue.OBJECT);
    //	  ulong count = readVarint(stream);
    //	  string name;

    //	  while (count-- != null)
    //	  {
    //		readName(stream, name);
    //		sec.insert.functorMethod(name, loadEntry(stream));
    //	  }

    //	  return sec.functorMethod;
    //	}
    //	public static JsonValue loadEntry(Common.IInputStream stream)
    //	{
    //	  ushort type = readPod<ushort>(stream);

    //	  if (type & GlobalMembers.BIN_KV_SERIALIZE_FLAG_ARRAY != null)
    //	  {
    //		type &= ~GlobalMembers.BIN_KV_SERIALIZE_FLAG_ARRAY;
    //		return loadArray(stream, new ushort(type));
    //	  }

    //	  return loadValue(stream, new ushort(type));
    //	}
    //	public static JsonValue loadArray(Common.IInputStream stream, ushort itemType)
    //	{
    //	  JsonValue arr = new JsonValue(JsonValue.ARRAY);
    //	  ulong count = readVarint(stream);

    //	  while (count-- != null)
    //	  {
    //		arr.pushBack.functorMethod(loadValue(stream, new ushort(itemType)));
    //	  }

    //	  return arr.functorMethod;
    //	}


    //	public static JsonValue parseBinary(Common.IInputStream stream)
    //	{
    //	  var hdr = readPod<KVBinaryStorageBlockHeader>(stream);

    //	  if (hdr.m_signature_a != GlobalMembers.PORTABLE_STORAGE_SIGNATUREA || hdr.m_signature_b != GlobalMembers.PORTABLE_STORAGE_SIGNATUREB)
    //	  {
    //		throw new System.Exception("Invalid binary storage signature");
    //	  }

    //	  if (hdr.m_ver != GlobalMembers.PORTABLE_STORAGE_FORMAT_VER)
    //	  {
    //		throw new System.Exception("Unknown binary storage format version");
    //	  }

    //	  return loadSection(stream);
    //	}



    //	public static void writePod<T>(IOutputStream s, T value)
    //	{
    //	  write(s, value, sizeof(T));
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<class T>
    //	public static ulong packVarint<T>(IOutputStream s, ushort type_or, ulong pv)
    //	{
    //	  T v = (T)(pv << 2);
    //	  v |= type_or;
    //	  write(s, v, sizeof(T));
    //	  return sizeof(T);
    //	}

    //	public static void writeElementName(IOutputStream s, Common.StringView name)
    //	{
    //	  if (name.getSize() > ushort.MaxValue)
    //	  {
    //		throw new System.Exception("Element name is too long");
    //	  }

    //	  ushort len = (ushort)name.getSize();
    //	  write(s, len, sizeof(ushort));
    //	  write(s, name.getData(), len);
    //	}

    //	public static ulong writeArraySize(IOutputStream s, ulong val)
    //	{
    //	  if (val <= 63)
    //	  {
    //		return packVarint<ushort>(s, new ushort(GlobalMembers.PORTABLE_RAW_SIZE_MARK_BYTE), new ulong(val));
    //	  }
    //	  else if (val <= 16383)
    //	  {
    //		return packVarint<ushort>(s, new ushort(GlobalMembers.PORTABLE_RAW_SIZE_MARK_WORD), new ulong(val));
    //	  }
    //	  else if (val <= 1073741823)
    //	  {
    //		return packVarint<uint>(s, new ushort(GlobalMembers.PORTABLE_RAW_SIZE_MARK_DWORD), new ulong(val));
    //	  }
    //	  else
    //	  {
    //		if (val > 4611686018427387903)
    //		{
    //		  throw new System.Exception("failed to pack varint - too big amount");
    //		}
    //		return packVarint<ulong>(s, new ushort(GlobalMembers.PORTABLE_RAW_SIZE_MARK_INT64), new ulong(val));
    //	  }
    //	}
    //	#if MSVC
    //	public static char suppressMSVCWarningLNK4221;
    //	#endif


    //	public static readonly int RETRY_TIMEOUT = 5;


    //	public static HashSet<Crypto.Hash> transactions_hash_seen = new HashSet<Crypto.Hash>();
    //	public static HashSet<Crypto.PublicKey> public_keys_seen = new HashSet<Crypto.PublicKey>();
    //	public static object seen_mutex = new object();

    //	public static void checkOutputKey(KeyDerivation derivation, PublicKey key, uint keyIndex, uint outputIndex, HashSet<PublicKey> spendKeys, Dictionary<PublicKey, List<uint>> outputs)
    //	{

    //	  PublicKey spendKey = new PublicKey();
    //	  underive_public_key(derivation, keyIndex, key, spendKey);

    //	  if (spendKeys.find(spendKey) != spendKeys.end())
    //	  {
    //		outputs[spendKey].Add((uint)outputIndex);
    //	  }

    //	}

    //	public static void findMyOutputs(ITransactionReader tx, SecretKey viewSecretKey, HashSet<PublicKey> spendKeys, Dictionary<PublicKey, List<uint>> outputs)
    //	{

    //	  var txPublicKey = tx.getTransactionPublicKey();
    //	  KeyDerivation derivation = new KeyDerivation();

    //	  if (!generate_key_derivation(txPublicKey, viewSecretKey, derivation))
    //	  {
    //		return;
    //	  }

    //	  uint keyIndex = 0;
    //	  uint outputCount = tx.getOutputCount();

    //	  for (uint idx = 0; idx < outputCount; ++idx)
    //	  {

    //		var outType = tx.getOutputType(uint(idx));

    //		if (outType == CryptoNote.TransactionTypes.OutputType.Key)
    //		{

    //		  ulong amount;
    //		  KeyOutput @out = new KeyOutput();
    //		  tx.getOutput(idx, @out, ref amount);
    //		  checkOutputKey(derivation, @out.key, keyIndex, idx, spendKeys, outputs);
    //		  ++keyIndex;

    //		}
    //	  }
    //	}

    //	public static List<Crypto.Hash> getBlockHashes(CryptoNote.CompleteBlock[] blocks, uint count)
    //	{
    //	  List<Crypto.Hash> result = new List<Crypto.Hash>();
    //	  result.Capacity = count;

    //	  for (uint i = 0; i < count; ++i)
    //	  {
    //		result.Add(blocks[i].blockHash);
    //	  }

    //	  return result;
    //	}

    //	public static std::error_code make_error_code(CryptoNote.error.WalletErrorCodes e)
    //	{
    //	  return std::error_code((int)e, CryptoNote.error.WalletErrorCategory.INSTANCE);
    //	}

    //	public static void asyncRequestCompletion(System.Event requestFinished)
    //	{
    //	  requestFinished.set();
    //	}

    //	public static CryptoNote.WalletEvent makeTransactionUpdatedEvent(uint id)
    //	{
    //	  CryptoNote.WalletEvent event = new CryptoNote.WalletEvent();
    //	  event.type = CryptoNote.WalletEventType.TRANSACTION_UPDATED;
    //	  event.transactionUpdated.transactionIndex = id;

    //	  return event;
    //	}

    //	public static CryptoNote.WalletEvent makeTransactionCreatedEvent(uint id)
    //	{
    //	  CryptoNote.WalletEvent event = new CryptoNote.WalletEvent();
    //	  event.type = CryptoNote.WalletEventType.TRANSACTION_CREATED;
    //	  event.transactionCreated.transactionIndex = id;

    //	  return event;
    //	}

    //	public static CryptoNote.WalletEvent makeMoneyUnlockedEvent()
    //	{
    //	  CryptoNote.WalletEvent event = new CryptoNote.WalletEvent();
    //	  event.type = CryptoNote.WalletEventType.BALANCE_UNLOCKED;

    //	  return event;
    //	}

    //	public static CryptoNote.WalletEvent makeSyncProgressUpdatedEvent(uint current, uint total)
    //	{
    //	  CryptoNote.WalletEvent event = new CryptoNote.WalletEvent();
    //	  event.type = CryptoNote.WalletEventType.SYNC_PROGRESS_UPDATED;
    //	  event.synchronizationProgressUpdated.processedBlockCount = current;
    //	  event.synchronizationProgressUpdated.totalBlockCount = total;

    //	  return event;
    //	}

    //	public static CryptoNote.WalletEvent makeSyncCompletedEvent()
    //	{
    //	  CryptoNote.WalletEvent event = new CryptoNote.WalletEvent();
    //	  event.type = CryptoNote.WalletEventType.SYNC_COMPLETED;

    //	  return event;
    //	}

    //	public static uint getTransactionSize(ITransactionReader transaction)
    //	{
    //	  return transaction.getTransactionData().size();
    //	}

    //	public static ulong calculateDonationAmount(ulong freeAmount, ulong donationThreshold, ulong dustThreshold)
    //	{
    //	  List<ulong> decomposedAmounts = new List<ulong>();
    //	  decomposeAmount(freeAmount, dustThreshold, decomposedAmounts);

    //	  decomposedAmounts.Sort((a, b) => -1 * a.CompareTo(b));

    //	  ulong donationAmount = 0;
    //	  foreach (var amount in decomposedAmounts)
    //	  {
    //		if (amount > donationThreshold - donationAmount)
    //		{
    //		  continue;
    //		}

    //		donationAmount += amount;
    //	  }

    //	  Debug.Assert(donationAmount <= freeAmount);
    //	  return donationAmount;
    //	}

    //	public static void serialize(UnlockTransactionJobDtoV2 value, CryptoNote.ISerializer serializer)
    //	{
    //	  serializer.functorMethod(value.blockHeight, "blockHeight");
    //	  serializer.functorMethod(value.transactionHash, "transactionHash");
    //	  serializer.functorMethod(value.walletSpendPublicKey, "walletSpendPublicKey");
    //	}

    //	public static void serialize(WalletTransactionDtoV2 value, CryptoNote.ISerializer serializer)
    //	{

    //	  std::underlying_type<CryptoNote.WalletTransactionState>.type state = (std::underlying_type<CryptoNote.WalletTransactionState>.type)value.state;
    //	  serializer.functorMethod(state, "state");
    //	  value.state = (CryptoNote.WalletTransactionState)state;

    //	  serializer.functorMethod(value.timestamp, "timestamp");
    //	  CryptoNote.serializeBlockHeight(serializer.functorMethod, value.blockHeight, "blockHeight");
    //	  serializer.functorMethod(value.hash, "hash");
    //	  serializer.functorMethod(value.totalAmount, "totalAmount");
    //	  serializer.functorMethod(value.fee, "fee");
    //	  serializer.functorMethod(value.creationTime, "creationTime");
    //	  serializer.functorMethod(value.unlockTime, "unlockTime");
    //	  serializer.functorMethod(value.extra, "extra");
    //	  serializer.functorMethod(value.isBase, "isBase");
    //	}

    //	public static void serialize(WalletTransferDtoV2 value, CryptoNote.ISerializer serializer)
    //	{
    //	  serializer.functorMethod(value.address, "address");
    //	  serializer.functorMethod(value.amount, "amount");
    //	  serializer.functorMethod(value.type, "type");
    //	}


    //	public static PaymentGateService ppg;

    //	#if WIN32
    //	public static IntPtr serviceStatusHandle;

    //	public static string GetLastErrorMessage(uint errorMessageID)
    //	{
    //	  string messageBuffer = null;
    //	  uint size = FormatMessageA(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS, null, errorMessageID, 0, (string) messageBuffer, 0, null);

    //	  string message = new string(messageBuffer, size);

    //	  LocalFree(messageBuffer);

    //	  return message;
    //	}

    ////C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    ////ORIGINAL LINE: void __stdcall serviceHandler(uint fdwControl)
    //	public static void serviceHandler(uint fdwControl)
    //	{
    //	  if (fdwControl == SERVICE_CONTROL_STOP)
    //	  {
    //		Logging.LoggerRef log = new Logging.LoggerRef(ppg.getLogger(), "serviceHandler");
    //		log(Logging.INFO, Logging.BRIGHT_YELLOW) << "Stop signal caught";

    //		SERVICE_STATUS serviceStatus = new SERVICE_STATUS(SERVICE_WIN32_OWN_PROCESS, SERVICE_STOP_PENDING, 0, NO_ERROR, 0, 0, 0);
    //		SetServiceStatus(serviceStatusHandle, serviceStatus);

    //		ppg.stop();
    //	  }
    //	}

    ////C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    ////ORIGINAL LINE: void __stdcall serviceMain(uint dwArgc, char **lpszArgv)
    //	public static void serviceMain(uint dwArgc, string[] lpszArgv)
    //	{
    //	  Logging.LoggerRef logRef = new Logging.LoggerRef(ppg.getLogger(), "WindowsService");

    //	  serviceStatusHandle = RegisterServiceCtrlHandler("PaymentGate", serviceHandler);
    //	  if (serviceStatusHandle == null)
    //	  {
    //		logRef(Logging.FATAL, Logging.BRIGHT_RED) << "Couldn't make RegisterServiceCtrlHandler call: " << GetLastErrorMessage(GetLastError());
    //		return;
    //	  }

    //	  SERVICE_STATUS serviceStatus = new SERVICE_STATUS(SERVICE_WIN32_OWN_PROCESS, SERVICE_START_PENDING, 0, NO_ERROR, 0, 1, 3000);
    //	  if (SetServiceStatus(serviceStatusHandle, serviceStatus) != 1)
    //	  {
    //		logRef(Logging.FATAL, Logging.BRIGHT_RED) << "Couldn't make SetServiceStatus call: " << GetLastErrorMessage(GetLastError());
    //		return;
    //	  }

    //	  serviceStatus = new SERVICE_STATUS(SERVICE_WIN32_OWN_PROCESS, SERVICE_RUNNING, SERVICE_ACCEPT_STOP, NO_ERROR, 0, 0, 0);
    //	  if (SetServiceStatus(serviceStatusHandle, serviceStatus) != 1)
    //	  {
    //		logRef(Logging.FATAL, Logging.BRIGHT_RED) << "Couldn't make SetServiceStatus call: " << GetLastErrorMessage(GetLastError());
    //		return;
    //	  }

    //	  try
    //	  {
    //		ppg.run();
    //	  }
    //	  catch (System.Exception ex)
    //	  {
    //		logRef(Logging.FATAL, Logging.BRIGHT_RED) << "Error occurred: " << ex.Message;
    //	  }

    //	  serviceStatus = new SERVICE_STATUS(SERVICE_WIN32_OWN_PROCESS, SERVICE_STOPPED, 0, NO_ERROR, 0, 0, 0);
    //	  SetServiceStatus(serviceStatusHandle, serviceStatus);
    //	}
    //	#else
    //	public static int daemonize()
    //	{
    //	  pid_t pid = new pid_t();
    //	  pid = fork();

    //	  if (pid < 0)
    //	  {
    //		return pid;
    //	  }

    //	  if (pid > 0)
    //	  {
    //		return pid;
    //	  }

    //	  if (setsid() < 0)
    //	  {
    //		return -1;
    //	  }

    //	  signal(SIGCHLD, SIG_IGN);
    //	  signal(SIGHUP, SIG_IGN);
    //	  signal(SIGPIPE, SIG_IGN);

    //	  pid = fork();

    //	  if (pid < 0)
    //	  {
    //		return pid;
    //	  }

    //	  if (pid > 0)
    //	  {
    //		return pid;
    //	  }

    //	  umask(0);

    //	  return 0;
    //	}
    //	#endif

    //	public static int runDaemon()
    //	{
    //	#if WIN32

    //	  SERVICE_TABLE_ENTRY[] serviceTable =
    //	  {
    //		  {"Payment Gate", serviceMain},
    //		  {null, null}
    //	  };

    //	  Logging.LoggerRef logRef = new Logging.LoggerRef(ppg.getLogger(), "RunService");

    //	  if (StartServiceCtrlDispatcher(serviceTable) != 1)
    //	  {
    //		logRef(Logging.FATAL, Logging.BRIGHT_RED) << "Couldn't start service: " << GetLastErrorMessage(GetLastError());
    //		return 1;
    //	  }

    //	  logRef(Logging.INFO) << "Service stopped";
    //	  return 0;

    //	#else

    //	  int daemonResult = daemonize();
    //	  if (daemonResult > 0)
    //	  {
    //		//parent
    //		return 0;
    //	  }
    //	  else if (daemonResult < 0)
    //	  {
    //		//error occurred
    //		return 1;
    //	  }

    //	  ppg.run();

    //	  return 0;

    //	#endif
    //	}

    //	public static int registerService()
    //	{
    //	#if WIN32
    //	  Logging.LoggerRef logRef = new Logging.LoggerRef(ppg.getLogger(), "ServiceRegistrator");

    //	  string pathBuff = new string(new char[MAX_PATH]);
    //	  string modulePath;
    //	  IntPtr scManager = null;
    //	  IntPtr scService = null;
    //	  int ret = 0;

    //	  for (;;)
    //	  {
    //		if (GetModuleFileName(null, pathBuff, ARRAYSIZE(pathBuff)) == 0)
    //		{
    //		  logRef(Logging.FATAL, Logging.BRIGHT_RED) << "GetModuleFileName failed with error: " << GetLastErrorMessage(GetLastError());
    //		  ret = 1;
    //		  break;
    //		}

    //		modulePath.assign(pathBuff);

    //		string moduleDir = modulePath.Substring(0, modulePath.LastIndexOfAny((Convert.ToString('\\')).ToCharArray()) + 1);
    //		modulePath += " --config=" + moduleDir + "payment_service.conf -d";

    //		scManager = OpenSCManager(null, null, SC_MANAGER_CONNECT | SC_MANAGER_CREATE_SERVICE);
    //		if (scManager == null)
    //		{
    //		  logRef(Logging.FATAL, Logging.BRIGHT_RED) << "OpenSCManager failed with error: " << GetLastErrorMessage(GetLastError());
    //		  ret = 1;
    //		  break;
    //		}

    //		scService = CreateService(scManager, DefineConstants.SERVICE_NAME, null, SERVICE_QUERY_STATUS, SERVICE_WIN32_OWN_PROCESS, SERVICE_AUTO_START, SERVICE_ERROR_NORMAL, modulePath, null, null, null, null, null);

    //		if (scService == null)
    //		{
    //		  logRef(Logging.FATAL, Logging.BRIGHT_RED) << "CreateService failed with error: " << GetLastErrorMessage(GetLastError());
    //		  ret = 1;
    //		  break;
    //		}

    //		logRef(Logging.INFO) << "Service is registered successfully";
    //		logRef(Logging.INFO) << "Please make sure " << moduleDir + "payment_service.conf" << " exists";
    //		break;
    //	  }

    //	  if (scManager != null)
    //	  {
    //		CloseServiceHandle(scManager);
    //	  }

    //	  if (scService != null)
    //	  {
    //		CloseServiceHandle(scService);
    //	  }

    //	  return ret;
    //	#else
    //	  return 0;
    //	#endif
    //	}

    //	public static int unregisterService()
    //	{
    //	#if WIN32
    //	  Logging.LoggerRef logRef = new Logging.LoggerRef(ppg.getLogger(), "ServiceDeregistrator");

    //	  IntPtr scManager = null;
    //	  IntPtr scService = null;
    //	  SERVICE_STATUS ssSvcStatus = new SERVICE_STATUS();
    //	  int ret = 0;

    //	  for (;;)
    //	  {
    //		scManager = OpenSCManager(null, null, SC_MANAGER_CONNECT);
    //		if (scManager == null)
    //		{
    //		  logRef(Logging.FATAL, Logging.BRIGHT_RED) << "OpenSCManager failed with error: " << GetLastErrorMessage(GetLastError());
    //		  ret = 1;
    //		  break;
    //		}

    //		scService = OpenService(scManager, DefineConstants.SERVICE_NAME, SERVICE_STOP | SERVICE_QUERY_STATUS | DELETE);
    //		if (scService == null)
    //		{
    //		  logRef(Logging.FATAL, Logging.BRIGHT_RED) << "OpenService failed with error: " << GetLastErrorMessage(GetLastError());
    //		  ret = 1;
    //		  break;
    //		}

    //		if (ControlService(scService, SERVICE_CONTROL_STOP, ssSvcStatus))
    //		{
    //		  logRef(Logging.INFO) << "Stopping " << DefineConstants.SERVICE_NAME;
    //		  Sleep(1000);

    //		  while (QueryServiceStatus(scService, ssSvcStatus))
    //		  {
    //			if (ssSvcStatus.dwCurrentState == SERVICE_STOP_PENDING)
    //			{
    //			  logRef(Logging.INFO) << "Waiting...";
    //			  Sleep(1000);
    //			}
    //			else
    //			{
    //			  break;
    //			}
    //		  }

    //		  Console.Write("\n");
    //		  if (ssSvcStatus.dwCurrentState == SERVICE_STOPPED)
    //		  {
    //			logRef(Logging.INFO) << DefineConstants.SERVICE_NAME << " is stopped";
    //		  }
    //		  else
    //		  {
    //			logRef(Logging.FATAL, Logging.BRIGHT_RED) << DefineConstants.SERVICE_NAME << " failed to stop" << std::endl;
    //		  }
    //		}

    //		if (!DeleteService(scService))
    //		{
    //		  logRef(Logging.FATAL, Logging.BRIGHT_RED) << "DeleteService failed with error: " << GetLastErrorMessage(GetLastError());
    //		  ret = 1;
    //		  break;
    //		}

    //		logRef(Logging.INFO) << DefineConstants.SERVICE_NAME << " is removed";
    //		break;
    //	  }

    //	  if (scManager != null)
    //	  {
    //		CloseServiceHandle(scManager);
    //	  }

    //	  if (scService != null)
    //	  {
    //		CloseServiceHandle(scService);
    //	  }

    //	  return ret;
    //	#else
    //	  return 0;
    //	#endif
    //	}

    //	static int Main(int argc, string[] args)
    //	{
    //	  PaymentGateService pg = new PaymentGateService();
    //	  ppg = pg;

    //	  try
    //	  {
    //		if (!pg.init(argc, args))
    //		{
    //		}

    //		Console.Write(CryptoNote.getProjectCLIHeader());

    //		auto config = pg.getConfig();

    //		if (config.serviceConfig.generateNewContainer)
    //		{
    //		  System.Dispatcher d = new System.Dispatcher();
    //		  generateNewWallet(pg.getCurrency(), pg.getWalletConfig(), pg.getLogger(), d);
    //		}

    //		if (config.serviceConfig.registerService)
    //		{
    //		  return registerService();
    //		}

    //		if (config.serviceConfig.unregisterService)
    //		{
    //		  return unregisterService();
    //		}

    //		if (config.serviceConfig.daemonize)
    //		{
    //		  if (runDaemon() != 0)
    //		  {
    //			throw new System.Exception("Failed to start daemon");
    //		  }
    //		}
    //		else
    //		{
    //		  pg.run();
    //		}
    //	  }
    //	  catch (System.Exception ex)
    //	  {
    //		std::cerr << "Fatal error: " << ex.Message << std::endl;
    //		return 1;
    //	  }

    //	}


    //	public static void changeDirectory(string path)
    //	{
    //	  if (chdir(path))
    //	  {
    //		throw new System.Exception("Couldn't change directory to \'" + path + "\': " + strerror(errno));
    //	  }
    //	}

    //	public static void stopSignalHandler(PaymentGateService pg)
    //	{
    //	  pg.stop();
    //	}


    //	public static std::error_code make_error_code(CryptoNote.error.WalletServiceErrorCode e)
    //	{
    //	  return std::error_code((int)e, CryptoNote.error.WalletServiceErrorCategory.INSTANCE);
    //	}




    //	public static string getAddressBookName(AddressBook addressBook)
    //	{
    //		while (true)
    //		{
    //			string friendlyName;

    //			Console.Write(InformationMsg("What friendly name do you want to "));
    //			Console.Write(InformationMsg("give this address book entry?: "));

    //			friendlyName = Console.ReadLine();
    //			trim(friendlyName);

    //			var it = std::find(addressBook.begin(), addressBook.end(), AddressBookEntry(friendlyName));

    //			if (it != addressBook.end())
    //			{
    //				Console.Write(WarningMsg("An address book entry with this "));
    //				Console.Write(WarningMsg("name already exists!"));
    //				Console.Write("\n");
    //				Console.Write("\n");

    //				continue;
    //			}

    //			return friendlyName;
    //		}
    //	}

    //	public static Maybe<string> getAddressBookPaymentID()
    //	{
    //		std::stringstream msg = new std::stringstream();

    //		msg << std::endl << "Does this address book entry have a payment ID associated with it?" << std::endl;

    //		return getPaymentID(msg.str());
    //	}

    //	public static void addToAddressBook()
    //	{
    //		Console.Write(InformationMsg("Note: You can type cancel at any time to " + "cancel adding someone to your address book"));
    //		Console.Write("\n");
    //		Console.Write("\n");

    //		var addressBook = getAddressBook();

    //		string friendlyName = getAddressBookName(new AddressBook(addressBook));

    //		if (friendlyName == "cancel")
    //		{
    //			Console.Write(WarningMsg("Cancelling addition to address book."));
    //			Console.Write("\n");
    //			return;
    //		}

    //		var maybeAddress = getAddress("\nWhat address does this user have? ");

    //		if (!maybeAddress.isJust)
    //		{
    //			Console.Write(WarningMsg("Cancelling addition to address book."));
    //			Console.Write("\n");
    //			return;
    //		}

    //		string address = maybeAddress.x.second;
    //		string paymentID = "";

    //		bool integratedAddress = maybeAddress.x.first == IntegratedAddress;

    //		if (!integratedAddress)
    //		{
    //			var maybePaymentID = getAddressBookPaymentID();

    //			if (!maybePaymentID.isJust)
    //			{
    //				Console.Write(WarningMsg("Cancelling addition to address book."));
    //				Console.Write("\n");

    //				return;
    //			}

    //			paymentID = maybePaymentID.x;
    //		}

    //		addressBook.emplace_back(friendlyName, address, paymentID, integratedAddress);

    //		if (saveAddressBook(new AddressBook(addressBook)))
    //		{
    //			Console.Write("\n");
    //			Console.Write(SuccessMsg("A new entry has been added to your address "));
    //			Console.Write(SuccessMsg("book!"));
    //			Console.Write("\n");
    //		}
    //	}

    //	public static Maybe< AddressBookEntry> getAddressBookEntry(AddressBook addressBook)
    //	{
    //		while (true)
    //		{
    //			string friendlyName;

    //			Console.Write(InformationMsg("Who do you want to send to from your "));
    //			Console.Write(InformationMsg("address book?: "));

    //			friendlyName = Console.ReadLine();
    //			trim(friendlyName);

    //			if (friendlyName == "cancel")
    //			{
    //				return Nothing<const AddressBookEntry>();
    //			}

    //			var it = std::find(addressBook.begin(), addressBook.end(), AddressBookEntry(friendlyName));

    //			if (it != addressBook.end())
    //			{
    //				return Just<const AddressBookEntry>(*it);
    //			}

    //			Console.Write("\n");
    //			Console.Write(WarningMsg("Could not find a user with the name of "));
    //			Console.Write(InformationMsg(friendlyName));
    //			Console.Write(WarningMsg(" in your address book!"));
    //			Console.Write("\n");
    //			Console.Write("\n");

    //			bool list = confirm("Would you like to list everyone in your " + "address book?");

    //			Console.Write("\n");

    //			if (list)
    //			{
    //				listAddressBook();
    //			}
    //		}
    //	}

    //	public static void sendFromAddressBook(WalletInfo walletInfo, uint height, string feeAddress, uint feeAmount)
    //	{
    //		var addressBook = getAddressBook();

    //		if (isAddressBookEmpty(new AddressBook(addressBook)))
    //		{
    //			return;
    //		}

    //		Console.Write(InformationMsg("Note: You can type cancel at any time to "));
    //		Console.Write(InformationMsg("cancel the transaction"));
    //		Console.Write("\n");
    //		Console.Write("\n");

    //		var maybeAddressBookEntry = getAddressBookEntry(new AddressBook(addressBook));

    //		if (!maybeAddressBookEntry.isJust)
    //		{
    //			Console.Write(WarningMsg("Cancelling transaction."));
    //			Console.Write("\n");
    //			return;
    //		}

    //		var addressBookEntry = maybeAddressBookEntry.x;

    //		var maybeAmount = getTransferAmount();

    //		if (!maybeAmount.isJust)
    //		{
    //			Console.Write(WarningMsg("Cancelling transction."));
    //			Console.Write("\n");
    //			return;
    //		}

    //		/* Originally entered address, so we can preserve the correct integrated
    //		   address for confirmation screen */
    //		var originalAddress = addressBookEntry.address;
    //		var address = originalAddress;
    //		var amount = maybeAmount.x;
    //		var fee = WalletConfig.defaultFee;
    //		var extra = getExtraFromPaymentID(addressBookEntry.paymentID);
    //		var mixin = CryptoNote.getDefaultMixinByHeight(height);
    //		var integrated = addressBookEntry.integratedAddress;

    //		if (integrated)
    //		{
    //			var addrPaymentIDPair = extractIntegratedAddress(address);
    //			address = addrPaymentIDPair.x.first;
    //			extra = getExtraFromPaymentID(addrPaymentIDPair.x.second);
    //		}

    //		doTransfer(address, amount, fee, extra, walletInfo, height, integrated, mixin, feeAddress, feeAmount, originalAddress);
    //	}

    //	public static bool isAddressBookEmpty(AddressBook addressBook)
    //	{
    //		if (addressBook.empty())
    //		{
    //			Console.Write(WarningMsg("Your address book is empty! Add some people "));
    //			Console.Write(WarningMsg("to it first."));
    //			Console.Write("\n");

    //			return true;
    //		}

    //		return false;
    //	}

    //	public static void deleteFromAddressBook()
    //	{
    //		var addressBook = getAddressBook();

    //		if (isAddressBookEmpty(new AddressBook(addressBook)))
    //		{
    //			return;
    //		}

    //		while (true)
    //		{
    //			Console.Write(InformationMsg("Note: You can type cancel at any time "));
    //			Console.Write(InformationMsg("to cancel the deletion"));
    //			Console.Write("\n");
    //			Console.Write("\n");

    //			string friendlyName;

    //			Console.Write(InformationMsg("What address book entry do you want to "));
    //			Console.Write(InformationMsg("delete?: "));

    //			friendlyName = Console.ReadLine();
    //			trim(friendlyName);

    //			if (friendlyName == "cancel")
    //			{
    //				Console.Write(WarningMsg("Cancelling deletion."));
    //				Console.Write("\n");
    //				return;
    //			}

    //			var it = std::find(addressBook.begin(), addressBook.end(), AddressBookEntry(friendlyName));

    //			if (it != addressBook.end())
    //			{
    //				addressBook.erase(it);

    //				if (saveAddressBook(new AddressBook(addressBook)))
    //				{
    //					Console.Write("\n");
    //					Console.Write(SuccessMsg("This entry has been deleted from "));
    //					Console.Write(SuccessMsg("your address book!"));
    //					Console.Write("\n");
    //				}

    //				return;
    //			}

    //			Console.Write("\n");
    //			Console.Write(WarningMsg("Could not find a user with the name of "));
    //			Console.Write(InformationMsg(friendlyName));
    //			Console.Write(WarningMsg(" in your address book!"));
    //			Console.Write("\n");
    //			Console.Write("\n");

    //			bool list = confirm("Would you like to list everyone in your " + "address book?");

    //			Console.Write("\n");

    //			if (list)
    //			{
    //				listAddressBook();
    //			}
    //		}
    //	}

    //	public static void listAddressBook()
    //	{
    //		var addressBook = getAddressBook();

    //		if (isAddressBookEmpty(new AddressBook(addressBook)))
    //		{
    //			return;
    //		}

    //		int index = 1;

    //		foreach (var i in addressBook)
    //		{
    //			Console.Write(InformationMsg("Address Book Entry #"));
    //			Console.Write(InformationMsg(Convert.ToString(index)));
    //			Console.Write(InformationMsg(":"));
    //			Console.Write("\n");
    //			Console.Write("\n");
    //			Console.Write(InformationMsg("Friendly Name: "));
    //			Console.Write("\n");
    //			Console.Write(SuccessMsg(i.friendlyName));
    //			Console.Write("\n");
    //			Console.Write("\n");
    //			Console.Write(InformationMsg("Address: "));
    //			Console.Write("\n");
    //			Console.Write(SuccessMsg(i.address));
    //			Console.Write("\n");
    //			Console.Write("\n");

    //			if (i.paymentID != "")
    //			{
    //				Console.Write(InformationMsg("Payment ID: "));
    //				Console.Write("\n");
    //				Console.Write(SuccessMsg(i.paymentID));
    //				Console.Write("\n");
    //				Console.Write("\n");
    //				Console.Write("\n");
    //			}
    //			else
    //			{
    //				Console.Write("\n");
    //			}

    //			index++;
    //		}
    //	}

    //	public static AddressBook getAddressBook()
    //	{
    //		AddressBook addressBook = new AddressBook();

    //		std::ifstream input = new std::ifstream(WalletConfig.addressBookFilename);

    //		/* If file exists, read current values */
    //		if (input != null)
    //		{
    //			std::stringstream buffer = new std::stringstream();
    //			buffer << input.rdbuf();
    //			input.close();

    //			CryptoNote.loadFromJson(addressBook, buffer.str());
    //		}

    //		return addressBook;
    //	}

    //	public static bool saveAddressBook(AddressBook addressBook)
    //	{
    //		string jsonString = CryptoNote.storeToJson(addressBook);

    //		std::ofstream output = new std::ofstream(WalletConfig.addressBookFilename);

    //		if (output != null)
    //		{
    //			output << jsonString;
    //		}
    //		else
    //		{
    //			Console.Write(WarningMsg("Failed to save address book to disk!"));
    //			Console.Write("\n");
    //			Console.Write(WarningMsg("Check you are able to write files to your "));
    //			Console.Write(WarningMsg("current directory."));
    //			Console.Write("\n");

    //			return false;
    //		}

    //		output.close();

    //		return true;
    //	}

    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	// 
    //	// Please see the included LICENSE file for more information.

    //	////////////////////////////////////////
    //	////////////////////////////////////////


    //	public static bool handleCommand(string command, WalletInfo walletInfo, CryptoNote.INode node)
    //	{
    //		/* Basic commands */
    //		if (command == "advanced")
    //		{
    //			advanced(walletInfo);
    //		}
    //		else if (command == "address")
    //		{
    //			Console.Write(SuccessMsg(walletInfo.walletAddress));
    //			Console.Write("\n");
    //		}
    //		else if (command == "balance")
    //		{
    //			balance(node, walletInfo.wallet, walletInfo.viewWallet);
    //		}
    //		else if (command == "backup")
    //		{
    //			exportKeys(walletInfo);
    //		}
    //		else if (command == "exit")
    //		{
    //			return false;
    //		}
    //		else if (command == "help")
    //		{
    //			help(walletInfo);
    //		}
    //		else if (command == "transfer")
    //		{
    //			transfer(walletInfo, node.getLastKnownBlockHeight(), false, node.feeAddress(), node.feeAmount());
    //		}
    //		/* Advanced commands */
    //		else if (command == "ab_add")
    //		{
    //			addToAddressBook();
    //		}
    //		else if (command == "ab_delete")
    //		{
    //			deleteFromAddressBook();
    //		}
    //		else if (command == "ab_list")
    //		{
    //			listAddressBook();
    //		}
    //		else if (command == "ab_send")
    //		{
    //			sendFromAddressBook(walletInfo, node.getLastKnownBlockHeight(), node.feeAddress(), node.feeAmount());
    //		}
    //		else if (command == "change_password")
    //		{
    //			changePassword(walletInfo);
    //		}
    //		else if (command == "make_integrated_address")
    //		{
    //			createIntegratedAddress();
    //		}
    //		else if (command == "incoming_transfers")
    //		{
    //			listTransfers(true, false, walletInfo.wallet, node);
    //		}
    //		else if (command == "list_transfers")
    //		{
    //			listTransfers(true, true, walletInfo.wallet, node);
    //		}
    //		else if (command == "optimize")
    //		{
    //			fullOptimize(walletInfo.wallet, node.getLastKnownBlockHeight());
    //		}
    //		else if (command == "outgoing_transfers")
    //		{
    //			listTransfers(false, true, walletInfo.wallet, node);
    //		}
    //		else if (command == "reset")
    //		{
    //			reset(node, walletInfo);
    //		}
    //		else if (command == "save")
    //		{
    //			save(walletInfo.wallet);
    //		}
    //		else if (command == "save_csv")
    //		{
    //			saveCSV(walletInfo.wallet, node);
    //		}
    //		else if (command == "send_all")
    //		{
    //			transfer(walletInfo, node.getLastKnownBlockHeight(), true, node.feeAddress(), node.feeAmount());
    //		}
    //		else if (command == "status")
    //		{
    //			status(node, walletInfo.wallet);
    //		}
    //		/* This should never happen */
    //		else
    //		{
    //			throw new System.Exception("Command was defined but not hooked up!");
    //		}

    //		return true;
    //	}

    //	public static WalletInfo handleLaunchCommand(CryptoNote.WalletGreen wallet, string launchCommand, Config config)
    //	{
    //		if (launchCommand == "create")
    //		{
    //			return generateWallet(wallet);
    //		}
    //		else if (launchCommand == "open")
    //		{
    //			return openWallet(wallet, config);
    //		}
    //		else if (launchCommand == "seed_restore")
    //		{
    //			return mnemonicImportWallet(wallet);
    //		}
    //		else if (launchCommand == "key_restore")
    //		{
    //			return importWallet(wallet);
    //		}
    //		else if (launchCommand == "view_wallet")
    //		{
    //			return createViewWallet(wallet);
    //		}
    //		/* This should never happen */
    //		else
    //		{
    //			throw new System.Exception("Command was defined but not hooked up!");
    //		}
    //	}




    //	public static void changePassword(WalletInfo walletInfo)
    //	{
    //		/* Check the user knows the current password */
    //		confirmPassword(walletInfo.walletPass, "Confirm your current password: ");

    //		/* Get a new password for the wallet */
    //		string newPassword = getWalletPassword(true, "Enter your new password: ");

    //		/* Change the wallet password */
    //		walletInfo.wallet.changePassword(walletInfo.walletPass, newPassword);

    //		/* Change the stored wallet metadata */
    //		walletInfo.walletPass = newPassword;

    //		/* Make sure we save with the new password */
    //		walletInfo.wallet.save();

    //		Console.Write(SuccessMsg("Your password has been changed!"));
    //		Console.Write("\n");
    //	}

    //	public static void exportKeys(WalletInfo walletInfo)
    //	{
    //		confirmPassword(walletInfo.walletPass);
    //		printPrivateKeys(walletInfo.wallet, walletInfo.viewWallet);
    //	}

    //	public static void printPrivateKeys(CryptoNote.WalletGreen wallet, bool viewWallet)
    //	{
    //		Crypto.SecretKey privateViewKey = wallet.getViewKey().secretKey;

    //		if (viewWallet)
    //		{
    //			Console.Write(SuccessMsg("Private view key:"));
    //			Console.Write("\n");
    //			Console.Write(SuccessMsg(Common.podToHex(privateViewKey)));
    //			Console.Write("\n");
    //			return;
    //		}

    //		Crypto.SecretKey privateSpendKey = wallet.getAddressSpendKey(0).secretKey;

    //		Crypto.SecretKey derivedPrivateViewKey = new Crypto.SecretKey();

    //		CryptoNote.AccountBase.generateViewFromSpend(privateSpendKey, derivedPrivateViewKey);

    //		bool deterministicPrivateKeys = derivedPrivateViewKey == privateViewKey;

    //		Console.Write(SuccessMsg("Private spend key:"));
    //		Console.Write("\n");
    //		Console.Write(SuccessMsg(Common.podToHex(privateSpendKey)));
    //		Console.Write("\n");
    //		Console.Write("\n");
    //		Console.Write(SuccessMsg("Private view key:"));
    //		Console.Write("\n");
    //		Console.Write(SuccessMsg(Common.podToHex(privateViewKey)));
    //		Console.Write("\n");

    //		if (deterministicPrivateKeys)
    //		{
    //			Console.Write("\n");
    //			Console.Write(SuccessMsg("Mnemonic seed:"));
    //			Console.Write("\n");
    //			Console.Write(SuccessMsg(Mnemonics.PrivateKeyToMnemonic(privateSpendKey)));
    //			Console.Write("\n");
    //		}
    //	}

    //	public static void balance(CryptoNote.INode node, CryptoNote.WalletGreen wallet, bool viewWallet)
    //	{
    //		ulong unconfirmedBalance = wallet.getPendingBalance();
    //		ulong confirmedBalance = wallet.getActualBalance();

    //		uint localHeight = node.getLastLocalBlockHeight();
    //		uint remoteHeight = node.getLastKnownBlockHeight();
    //		uint walletHeight = wallet.getBlockCount();

    //		/* We can make a better approximation of the view wallet balance if we
    //		   ignore fusion transactions.
    //		   See https://github.com/turtlecoin/turtlecoin/issues/531 */
    //		if (viewWallet)
    //		{
    //			/* Not sure how to verify if a transaction is unlocked or not via
    //			   the WalletTransaction type, so this is technically not correct,
    //			   we might be including locked balance. */
    //			confirmedBalance = 0;

    //			uint numTransactions = wallet.getTransactionCount();

    //			for (uint i = 0; i < numTransactions; i++)
    //			{
    //				CryptoNote.WalletTransaction t = wallet.getTransaction(i);

    //				/* Fusion transactions are zero fee, skip them. Coinbase
    //				   transactions are also zero fee, include them. */
    //				if (t.fee != 0 || t.isBase)
    //				{
    //					confirmedBalance += t.totalAmount;
    //				}
    //			}
    //		}

    //		ulong totalBalance = unconfirmedBalance + confirmedBalance;

    //		Console.Write("Available balance: ");
    //		Console.Write(SuccessMsg(formatAmount(confirmedBalance)));
    //		Console.Write("\n");
    //		Console.Write("Locked (unconfirmed) balance: ");
    //		Console.Write(WarningMsg(formatAmount(unconfirmedBalance)));
    //		Console.Write("\n");
    //		Console.Write("Total balance: ");
    //		Console.Write(InformationMsg(formatAmount(totalBalance)));
    //		Console.Write("\n");

    //		if (viewWallet)
    //		{
    //			Console.Write("\n");
    //			Console.Write(InformationMsg("Please note that view only wallets " + "can only track incoming transactions,"));
    //			Console.Write("\n");
    //			Console.Write(InformationMsg("and so your wallet balance may appear " + "inflated."));
    //			Console.Write("\n");
    //		}

    //		if (localHeight < remoteHeight)
    //		{
    //			Console.Write("\n");
    //			Console.Write(InformationMsg("Your daemon is not fully synced with " + "the network!"));
    //			Console.Write("\n");
    //			Console.Write("Your balance may be incorrect until you are fully ");
    //			Console.Write("synced!");
    //			Console.Write("\n");
    //		}
    //		/* Small buffer because wallet height doesn't update instantly like node
    //		   height does */
    //		else if (walletHeight + 1000 < remoteHeight)
    //		{
    //			Console.Write("\n");
    //			Console.Write(InformationMsg("The blockchain is still being scanned for " + "your transactions."));
    //			Console.Write("\n");
    //			Console.Write("Balances might be incorrect whilst this is ongoing.");
    //			Console.Write("\n");
    //		}
    //	}

    //	public static void printHeights(uint localHeight, uint remoteHeight, uint walletHeight)
    //	{
    //		/* This is the height that the wallet has been scanned to. The blockchain
    //		   can be fully updated, but we have to walk the chain to find our
    //		   transactions, and this number indicates that progress. */
    //		Console.Write("Wallet blockchain height: ");

    //		/* Small buffer because wallet height doesn't update instantly like node
    //		   height does */
    //		if (walletHeight + 1000 > remoteHeight)
    //		{
    //			Console.Write(SuccessMsg(Convert.ToString(walletHeight)));
    //		}
    //		else
    //		{
    //			Console.Write(WarningMsg(Convert.ToString(walletHeight)));
    //		}

    //		Console.Write("\n");
    //		Console.Write("Local blockchain height: ");

    //		if (localHeight == remoteHeight)
    //		{
    //			Console.Write(SuccessMsg(Convert.ToString(localHeight)));
    //		}
    //		else
    //		{
    //			Console.Write(WarningMsg(Convert.ToString(localHeight)));
    //		}

    //		Console.Write("\n");
    //		Console.Write("Network blockchain height: ");
    //		Console.Write(SuccessMsg(Convert.ToString(remoteHeight)));
    //		Console.Write("\n");
    //	}

    //	public static void printSyncStatus(uint localHeight, uint remoteHeight, uint walletHeight)
    //	{
    //		string networkSyncPercentage = Common.get_sync_percentage(localHeight, remoteHeight) + "%";

    //		string walletSyncPercentage = Common.get_sync_percentage(walletHeight, remoteHeight) + "%";

    //		Console.Write("Network sync status: ");

    //		if (localHeight == remoteHeight)
    //		{
    //			Console.Write(SuccessMsg(networkSyncPercentage));
    //			Console.Write("\n");
    //		}
    //		else
    //		{
    //			Console.Write(WarningMsg(networkSyncPercentage));
    //			Console.Write("\n");
    //		}

    //		Console.Write("Wallet sync status: ");

    //		/* Small buffer because wallet height is not always completely accurate */
    //		if (walletHeight + 1000 > remoteHeight)
    //		{
    //			Console.Write(SuccessMsg(walletSyncPercentage));
    //			Console.Write("\n");
    //		}
    //		else
    //		{
    //			Console.Write(WarningMsg(walletSyncPercentage));
    //			Console.Write("\n");
    //		}
    //	}

    //	public static void printSyncSummary(uint localHeight, uint remoteHeight, uint walletHeight)
    //	{
    //		if (localHeight == 0 && remoteHeight == 0)
    //		{
    //			Console.Write(WarningMsg("Uh oh, it looks like you don't have "));
    //			Console.Write(WarningMsg(WalletConfig.daemonName));
    //			Console.Write(WarningMsg(" open!"));
    //			Console.Write("\n");
    //		}
    //		else if (walletHeight + 1000 < remoteHeight && localHeight == remoteHeight)
    //		{
    //			Console.Write(InformationMsg("You are synced with the network, but the " + "blockchain is still being scanned for " + "your transactions."));
    //			Console.Write("\n");
    //			Console.Write("Balances might be incorrect whilst this is ongoing.");
    //			Console.Write("\n");
    //		}
    //		else if (localHeight == remoteHeight)
    //		{
    //			Console.Write(SuccessMsg("Yay! You are synced!"));
    //			Console.Write("\n");
    //		}
    //		else
    //		{
    //			Console.Write(WarningMsg("Be patient, you are still syncing with the " + "network!"));
    //			Console.Write("\n");
    //		}
    //	}

    //	public static void printPeerCount(uint peerCount)
    //	{
    //		Console.Write("Peers: ");
    //		Console.Write(SuccessMsg(Convert.ToString(peerCount)));
    //		Console.Write("\n");
    //	}

    //	public static void printHashrate(ulong difficulty)
    //	{
    //		/* Offline node / not responding */
    //		if (difficulty == 0)
    //		{
    //			return;
    //		}

    //		/* Hashrate is difficulty divided by block target time */
    //		uint hashrate = (uint)Math.Round(difficulty / CryptoNote.parameters.DIFFICULTY_TARGET);

    //		Console.Write("Network hashrate: ");
    //		Console.Write(SuccessMsg(Common.get_mining_speed(hashrate)));
    //		Console.Write(" (Based on the last local block)");
    //		Console.Write("\n");
    //	}

    //	/* This makes sure to call functions on the node which only return cached
    //	   data. This ensures it returns promptly, and doesn't hang waiting for a
    //	   response when the node is having issues. */
    //	public static void status(CryptoNote.INode node, CryptoNote.WalletGreen wallet)
    //	{
    //		uint localHeight = node.getLastLocalBlockHeight();
    //		uint remoteHeight = node.getLastKnownBlockHeight();
    //		uint walletHeight = wallet.getBlockCount();

    //		/* Print the heights of local, remote, and wallet */
    //		printHeights(localHeight, remoteHeight, walletHeight);

    //		Console.Write("\n");

    //		/* Print the network and wallet sync status in percentage */
    //		printSyncStatus(localHeight, remoteHeight, walletHeight);

    //		Console.Write("\n");

    //		/* Print the network hashrate, based on the last local block */
    //		printHashrate(node.getLastLocalBlockHeaderInfo().difficulty);

    //		/* Print the amount of peers we have */
    //		printPeerCount(node.getPeerCount());

    //		Console.Write("\n");

    //		/* Print a summary of the sync status */
    //		printSyncSummary(localHeight, remoteHeight, walletHeight);
    //	}

    //	public static void reset(CryptoNote.INode node, WalletInfo walletInfo)
    //	{
    //		ulong scanHeight = getScanHeight();

    //		Console.Write("\n");
    //		Console.Write(InformationMsg("This process may take some time to complete."));
    //		Console.Write("\n");
    //		Console.Write(InformationMsg("You can't make any transactions during the "));
    //		Console.Write(InformationMsg("process."));
    //		Console.Write("\n");
    //		Console.Write("\n");

    //		if (!confirm("Are you sure?"))
    //		{
    //			return;
    //		}

    //		Console.Write(InformationMsg("Resetting wallet..."));
    //		Console.Write("\n");

    //		walletInfo.wallet.reset(scanHeight);

    //		syncWallet(node, walletInfo);
    //	}

    //	public static void saveCSV(CryptoNote.WalletGreen wallet, CryptoNote.INode node)
    //	{
    //		uint numTransactions = wallet.getTransactionCount();

    //		std::ofstream csv = new std::ofstream();
    //		csv.open(WalletConfig.csvFilename);

    //		if (csv == null)
    //		{
    //			Console.Write(WarningMsg("Couldn't open transactions.csv file for " + "saving!"));
    //			Console.Write("\n");
    //			Console.Write(WarningMsg("Ensure it is not open in any other " + "application."));
    //			Console.Write("\n");
    //			return;
    //		}

    //		Console.Write(InformationMsg("Saving CSV file..."));
    //		Console.Write("\n");

    //		/* Create CSV header */
    //		csv << "Timestamp,Block Height,Hash,Amount,In/Out" << std::endl;

    //		/* Loop through transactions */
    //		for (uint i = 0; i < numTransactions; i++)
    //		{
    //			CryptoNote.WalletTransaction t = wallet.getTransaction(i);

    //			/* Ignore fusion transactions */
    //			if (t.totalAmount == 0)
    //			{
    //				continue;
    //			}

    //			string amount = formatAmountBasic(Math.Abs(t.totalAmount));

    //			string direction = t.totalAmount > 0 ? "IN" : "OUT";

    //			csv << unixTimeToDate(t.timestamp) << "," << t.blockHeight << "," << Common.podToHex(t.hash) << "," << amount << "," << direction << std::endl;
    //		}

    //		csv.close();

    //		Console.Write(SuccessMsg("CSV successfully written to "));
    //		Console.Write(SuccessMsg(WalletConfig.csvFilename));
    //		Console.Write(SuccessMsg("!"));
    //		Console.Write("\n");
    //	}

    //	public static void printOutgoingTransfer(CryptoNote.WalletTransaction t, CryptoNote.INode node)
    //	{
    //		Console.Write(WarningMsg("Outgoing transfer:"));
    //		Console.Write("\n");
    //		Console.Write(WarningMsg("Hash: " + Common.podToHex(t.hash)));
    //		Console.Write("\n");

    //		if (t.timestamp != 0)
    //		{
    //			Console.Write(WarningMsg("Block height: "));
    //			Console.Write(WarningMsg(Convert.ToString(t.blockHeight)));
    //			Console.Write("\n");
    //			Console.Write(WarningMsg("Timestamp: "));
    //			Console.Write(WarningMsg(unixTimeToDate(t.timestamp)));
    //			Console.Write("\n");
    //		}

    //		Console.Write(WarningMsg("Spent: " + formatAmount(-t.totalAmount - t.fee)));
    //		Console.Write("\n");
    //		Console.Write(WarningMsg("Fee: " + formatAmount(t.fee)));
    //		Console.Write("\n");
    //		Console.Write(WarningMsg("Total Spent: " + formatAmount(-t.totalAmount)));
    //		Console.Write("\n");

    //		string paymentID = getPaymentIDFromExtra(t.extra);

    //		if (paymentID != "")
    //		{
    //			Console.Write(WarningMsg("Payment ID: " + paymentID));
    //			Console.Write("\n");
    //		}

    //		Console.Write("\n");
    //	}

    //	public static void printIncomingTransfer(CryptoNote.WalletTransaction t, CryptoNote.INode node)
    //	{
    //		Console.Write(SuccessMsg("Incoming transfer:"));
    //		Console.Write("\n");
    //		Console.Write(SuccessMsg("Hash: " + Common.podToHex(t.hash)));
    //		Console.Write("\n");

    //		if (t.timestamp != 0)
    //		{
    //			Console.Write(SuccessMsg("Block height: "));
    //			Console.Write(SuccessMsg(Convert.ToString(t.blockHeight)));
    //			Console.Write("\n");
    //			Console.Write(SuccessMsg("Timestamp: "));
    //			Console.Write(SuccessMsg(unixTimeToDate(t.timestamp)));
    //			Console.Write("\n");
    //		}


    //		Console.Write(SuccessMsg("Amount: " + formatAmount(t.totalAmount)));
    //		Console.Write("\n");

    //		string paymentID = getPaymentIDFromExtra(t.extra);

    //		if (paymentID != "")
    //		{
    //			Console.Write(SuccessMsg("Payment ID: " + paymentID));
    //			Console.Write("\n");
    //		}

    //		Console.Write("\n");
    //	}

    //	public static void listTransfers(bool incoming, bool outgoing, CryptoNote.WalletGreen wallet, CryptoNote.INode node)
    //	{
    //		uint numTransactions = wallet.getTransactionCount();

    //		long totalSpent = 0;
    //		long totalReceived = 0;

    //		for (uint i = 0; i < numTransactions; i++)
    //		{
    //			CryptoNote.WalletTransaction t = wallet.getTransaction(i);

    //			/* Is a fusion transaction (on a view only wallet). It appears to have
    //			   an incoming amount, because we can't detract the outputs (can't
    //			   decrypt them) */
    //			if (t.fee == 0 && !t.isBase)
    //			{
    //				continue;
    //			}

    //			if (t.totalAmount < 0 && outgoing)
    //			{
    //				printOutgoingTransfer(new CryptoNote.WalletTransaction(t), node);
    //				totalSpent += -t.totalAmount;
    //			}
    //			else if (t.totalAmount > 0 && incoming)
    //			{
    //				printIncomingTransfer(new CryptoNote.WalletTransaction(t), node);
    //				totalReceived += t.totalAmount;
    //			}
    //		}

    //		if (incoming)
    //		{
    //			Console.Write(SuccessMsg("Total received: " + formatAmount(totalReceived)));
    //			Console.Write("\n");
    //		}

    //		if (outgoing)
    //		{
    //			Console.Write(WarningMsg("Total spent: " + formatAmount(totalSpent)));
    //			Console.Write("\n");
    //		}
    //	}

    //	public static void save(CryptoNote.WalletGreen wallet)
    //	{
    //		Console.Write(InformationMsg("Saving."));
    //		Console.Write("\n");
    //		wallet.save();
    //		Console.Write(InformationMsg("Saved."));
    //		Console.Write("\n");
    //	}

    //	public static void createIntegratedAddress()
    //	{
    //		Console.Write(InformationMsg("Creating an integrated address from an "));
    //		Console.Write(InformationMsg("address and payment ID pair..."));
    //		Console.Write("\n");
    //		Console.Write("\n");

    //		string address;
    //		string paymentID;

    //		while (true)
    //		{
    //			Console.Write(InformationMsg("Address: "));

    //			address = Console.ReadLine();
    //			trim(address);

    //			Console.Write("\n");

    //			if (parseStandardAddress(address, true))
    //			{
    //				break;
    //			}
    //		}

    //		while (true)
    //		{
    //			Console.Write(InformationMsg("Payment ID: "));

    //			paymentID = Console.ReadLine();
    //			trim(paymentID);

    //			List<byte> extra = new List<byte>();

    //			Console.Write("\n");

    //			if (!CryptoNote.createTxExtraWithPaymentId(paymentID, extra))
    //			{
    //				Console.Write(WarningMsg("Failed to parse! Payment ID's are 64 " + "character hexadecimal strings."));
    //				Console.Write("\n");
    //				Console.Write("\n");

    //				continue;
    //			}

    //			break;
    //		}

    //		Console.Write(InformationMsg(createIntegratedAddress(address, paymentID)));
    //		Console.Write("\n");
    //	}

    //	public static void help(WalletInfo wallet)
    //	{
    //		if (wallet.viewWallet)
    //		{
    //			printCommands(basicViewWalletCommands());
    //		}
    //		else
    //		{
    //			printCommands(basicCommands());
    //		}
    //	}

    //	public static void advanced(WalletInfo wallet)
    //	{
    //		/* We pass the offset of the command to know what index to print for
    //		   command numbers */
    //		if (wallet.viewWallet)
    //		{
    //			printCommands(advancedViewWalletCommands(), basicViewWalletCommands().size());
    //		}
    //		else
    //		{
    //			printCommands(advancedCommands(), basicCommands().size());
    //		}
    //	}

    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	// 
    //	// Please see the included LICENSE file for more information.

    //	////////////////////////////////
    //	////////////////////////////////


    //	public static List<Command> startupCommands()
    //	{
    //		return new List<Command>() {Command("open", "Open a wallet already on your system"), Command("create", "Create a new wallet"), Command("seed_restore", "Restore a wallet using a seed phrase of words"), Command("key_restore", "Restore a wallet using a view and spend key"), Command("view_wallet", "Import a view only wallet"), Command("exit", "Exit the program")};
    //	}

    //	public static List<Command> nodeDownCommands()
    //	{
    //		return new List<Command>() {Command("try_again", "Try to connect to the node again"), Command("continue", "Continue to the wallet interface regardless"), Command("exit", "Exit the program")};
    //	}

    //	public static List<AdvancedCommand> allCommands()
    //	{
    //		return new List<AdvancedCommand>() {AdvancedCommand("advanced", "List available advanced commands", true, false), AdvancedCommand("address", "Display your payment address", true, false), AdvancedCommand("balance", "Display how much " + WalletConfig.ticker + " you have", true, false), AdvancedCommand("backup", "Backup your private keys and/or seed", true, false), AdvancedCommand("exit", "Exit and save your wallet", true, false), AdvancedCommand("help", "List this help message", true, false), AdvancedCommand("transfer", "Send " + WalletConfig.ticker + " to someone", false, false), AdvancedCommand("ab_add", "Add a person to your address book", true, true), AdvancedCommand("ab_delete", "Delete a person in your address book", true, true), AdvancedCommand("ab_list", "List everyone in your address book", true, true), AdvancedCommand("ab_send", "Send " + WalletConfig.ticker + " to someone in your address book", false, true), AdvancedCommand("change_password", "Change your wallet password", true, true), AdvancedCommand("make_integrated_address", "Make a combined address + payment ID", true, true), AdvancedCommand("incoming_transfers", "Show incoming transfers", true, true), AdvancedCommand("list_transfers", "Show all transfers", false, true), AdvancedCommand("optimize", "Optimize your wallet to send large amounts", false, true), AdvancedCommand("outgoing_transfers", "Show outgoing transfers", false, true), AdvancedCommand("reset", "Recheck the chain from zero for transactions", true, true), AdvancedCommand("save", "Save your wallet state", true, true), AdvancedCommand("save_csv", "Save all wallet transactions to a CSV file", true, true), AdvancedCommand("send_all", "Send all your balance to someone", false, true), AdvancedCommand("status", "Display sync status and network hashrate", true, true)};
    //	}

    //	public static List<AdvancedCommand> basicCommands()
    //	{
    //	return filter(allCommands(), (AdvancedCommand c) =>
    //	{
    //		return !c.advanced;
    //	});
    //	}

    //	public static List<AdvancedCommand> advancedCommands()
    //	{
    //	return filter(allCommands(), (AdvancedCommand c) =>
    //	{
    //		return c.advanced;
    //	});
    //	}

    //	public static List<AdvancedCommand> basicViewWalletCommands()
    //	{
    //	return filter(basicCommands(), (AdvancedCommand c) =>
    //	{
    //		return c.viewWalletSupport;
    //	});
    //	}

    //	public static List<AdvancedCommand> advancedViewWalletCommands()
    //	{
    //	return filter(advancedCommands(), (AdvancedCommand c) =>
    //	{
    //		return c.viewWalletSupport;
    //	});
    //	}

    //	public static List<AdvancedCommand> allViewWalletCommands()
    //	{
    //	return filter(allCommands(), (AdvancedCommand c) =>
    //	{
    //		return c.viewWalletSupport;
    //	});
    //	}

    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	// 
    //	// Please see the included LICENSE file for more information.

    //	/////////////////////////////
    //	/////////////////////////////




    //	public static uint makeFusionTransaction(CryptoNote.WalletGreen wallet, ulong threshold, ulong height)
    //	{
    //		ulong bestThreshold = threshold;
    //		uint optimizable = 0;

    //		/* Find the best threshold by starting at threshold and decreasing by
    //		   half till we get to the minimum amount, storing the threshold that
    //		   gave us the most amount of optimizable amounts */
    //		while (threshold > WalletConfig.minimumSend)
    //		{
    //			var fusionReadyCount = wallet.estimate(threshold).fusionReadyCount;

    //			if (fusionReadyCount > optimizable)
    //			{
    //				optimizable = fusionReadyCount;
    //				bestThreshold = threshold;
    //			}

    //			threshold /= 2;
    //		}

    //		/* Can't optimize */
    //		if (optimizable == 0)
    //		{
    //			return CryptoNote.WALLET_INVALID_TRANSACTION_ID;
    //		}

    //		try
    //		{
    //			return wallet.createFusionTransaction(bestThreshold, CryptoNote.getDefaultMixinByHeight(height), {}, wallet.getAddress(0));
    //		}
    //		catch (System.Exception e)
    //		{
    //			Console.Write(WarningMsg("Failed to send fusion transaction: "));
    //			Console.Write(WarningMsg(e.Message));
    //			Console.Write("\n");

    //			return CryptoNote.WALLET_INVALID_TRANSACTION_ID;
    //		}
    //	}

    //	public static void fullOptimize(CryptoNote.WalletGreen wallet, ulong height)
    //	{
    //		Console.Write("Attempting to optimize your wallet to allow you to ");
    //		Console.Write("send large amounts at once. ");
    //		Console.Write("\n");
    //		Console.Write(WarningMsg("This may take a very long time!"));
    //		Console.Write("\n");

    //		if (!confirm("Do you want to proceed?"))
    //		{
    //			Console.Write(WarningMsg("Cancelling optimization."));
    //			Console.Write("\n");
    //			return;
    //		}

    //		for (int i = 1;;i++)
    //		{
    //			Console.Write(InformationMsg("Running optimization round " + Convert.ToString(i) + "..."));
    //			Console.Write("\n");

    //			/* Optimize as many times as possible until optimization is no longer
    //			   possible. */
    //			if (!optimize(wallet, wallet.getActualBalance(), height))
    //			{
    //				break;
    //			}
    //		}

    //		Console.Write(SuccessMsg("Full optimization completed!"));
    //		Console.Write("\n");
    //	}

    //	public static bool optimize(CryptoNote.WalletGreen wallet, ulong threshold, ulong height)
    //	{
    //		List<Crypto.Hash> fusionTransactionHashes = new List<Crypto.Hash>();

    //		while (true)
    //		{
    //			/* Create as many fusion transactions until we can't send anymore,
    //			   either because balance is locked too much or we can no longer
    //			   optimize anymore transactions */
    //			uint tmpFusionTxID = makeFusionTransaction(wallet, threshold, height);

    //			if (tmpFusionTxID == CryptoNote.WALLET_INVALID_TRANSACTION_ID)
    //			{
    //				break;
    //			}
    //			else
    //			{
    //				CryptoNote.WalletTransaction w = wallet.getTransaction(tmpFusionTxID);

    //				fusionTransactionHashes.Add(w.hash);

    //				if (fusionTransactionHashes.Count == 1)
    //				{
    //					Console.Write(SuccessMsg("Created 1 fusion transaction!"));
    //					Console.Write("\n");
    //				}
    //				else
    //				{
    //					Console.Write(SuccessMsg("Created " + Convert.ToString(fusionTransactionHashes.Count) + " fusion transactions!"));
    //					Console.Write("\n");
    //				}
    //			}
    //		}

    //		if (fusionTransactionHashes.Count == 0)
    //		{
    //			return false;
    //		}

    //		/* Hurr durr grammar */
    //		if (fusionTransactionHashes.Count == 1)
    //		{
    //			Console.Write(SuccessMsg("1 fusion transaction has been sent, waiting " + "for balance to return and unlock"));
    //			Console.Write("\n");
    //			Console.Write("\n");
    //		}
    //		else
    //		{
    //			Console.Write(SuccessMsg(Convert.ToString(fusionTransactionHashes.Count) + " fusion transactions have been sent, waiting " + "for balance to return and unlock"));
    //			Console.Write("\n");
    //			Console.Write("\n");
    //		}

    //		wallet.updateInternalCache();

    //		/* Short sleep to ensure it's in the transaction pool when we poll it */
    //		std::this_thread.sleep_for(std::chrono.seconds(1));

    //		while (true)
    //		{
    //			List<CryptoNote.WalletTransactionWithTransfers> unconfirmedTransactions = wallet.getUnconfirmedTransactions();

    //			List<Crypto.Hash> unconfirmedTxHashes = new List<Crypto.Hash>();

    //			foreach (var t in unconfirmedTransactions)
    //			{
    //				unconfirmedTxHashes.Add(t.transaction.hash);
    //			}

    //			bool fusionCompleted = true;

    //			/* Is our fusion transaction still unconfirmed? We can't gain the
    //			   benefits of fusioning if the balance hasn't unlocked, so we can
    //			   send this new optimized balance */
    //			foreach (var tx in fusionTransactionHashes)
    //			{
    //				/* If the fusion transaction hash is present in the unconfirmed
    //				   transactions pool, we need to wait for it to complete. */
    //				if (unconfirmedTxHashes.Contains(tx))
    //				{
    //					fusionCompleted = false;
    //				}
    //				else
    //				{
    //					/* We can't find this transaction in the unconfirmed
    //					   transaction pool anymore, so it has been confirmed. Remove
    //					   it so we both have to check less transactions each time,
    //					   and we can easily update the transactions left to confirm
    //					   output message */
    ////C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'erase' method in C#:
    //					fusionTransactionHashes.erase(std::remove(fusionTransactionHashes.GetEnumerator(), fusionTransactionHashes.end(), tx), fusionTransactionHashes.end());
    //				}
    //			}

    //			if (!fusionCompleted)
    //			{
    //				Console.Write(WarningMsg("Balance is still locked, " + Convert.ToString(fusionTransactionHashes.Count)));

    //				/* More grammar... */
    //				if (fusionTransactionHashes.Count == 1)
    //				{
    //					Console.Write(WarningMsg(" fusion transaction still to be " + "confirmed."));
    //				}
    //				else
    //				{
    //					Console.Write(WarningMsg(" fusion transactions still to be " + "confirmed."));
    //				}

    //				Console.Write("\n");
    //				Console.Write(SuccessMsg("Will try again in 15 seconds..."));
    //				Console.Write("\n");

    //				std::this_thread.sleep_for(std::chrono.seconds(15));

    //				wallet.updateInternalCache();
    //			}
    //			else
    //			{
    //				Console.Write(SuccessMsg("All fusion transactions confirmed!"));
    //				Console.Write("\n");
    //				break;
    //			}
    //		}

    //		return true;
    //	}

    //	public static bool fusionTX(CryptoNote.WalletGreen wallet, CryptoNote.TransactionParameters p, ulong height)
    //	{
    //		Console.Write(WarningMsg("Your transaction is too large to be accepted by " + "the network!"));
    //		Console.Write("\n");
    //		Console.Write("We're attempting to optimize your ");
    //		Console.Write("wallet, which hopefully will make the transaction small ");
    //		Console.Write("enough to fit in a block.");
    //		Console.Write("\n");
    //		Console.Write("Please wait, this will take some time...");
    //		Console.Write("\n");
    //		Console.Write("\n");

    //		/* We could check if optimization succeeded, but it's not really needed
    //		   because we then check if the transaction is too large... it could have
    //		   potentially become valid because another payment came in. */
    //		optimize(wallet, p.destinations[0].amount + p.fee, height);

    //		var startTime = std::chrono.system_clock.now();

    //		while (wallet.getActualBalance() < p.destinations[0].amount + p.fee)
    //		{
    //			/* Break after a minute just in case something has gone wrong */
    //			if ((std::chrono.system_clock.now() - startTime) > std::chrono.minutes(5))
    //			{
    //				Console.Write(WarningMsg("Fusion transactions have " + "completed, however available " + "balance is less than transfer " + "amount specified."));
    //				Console.Write("\n");
    //				Console.Write(WarningMsg("Transfer aborted, please review " + "and start a new transfer."));
    //				Console.Write("\n");

    //				return false;
    //			}

    //			Console.Write(WarningMsg("Optimization completed, but balance " + "is not fully unlocked yet!"));
    //			Console.Write("\n");
    //			Console.Write(SuccessMsg("Will try again in 5 seconds..."));
    //			Console.Write("\n");

    //			std::this_thread.sleep_for(std::chrono.seconds(5));
    //		}

    //		return true;
    //	}





    //	/* Note: this is not portable, it only works with terminals that support ANSI
    //	   codes (e.g., not Windows) - however! due to the way linenoise-cpp works,
    //	   it will actually convert these codes for us to the windows equivalent. <3 */
    //	public static string yellowANSIMsg(string msg)
    //	{
    //		const string CYELLOW = "\x001B[1;33m";
    //		const string RESET = "\x001B[0m";
    //		return CYELLOW + msg + RESET;
    //	}

    //	public static string getPrompt(WalletInfo walletInfo)
    //	{
    //		const int promptLength = 20;
    //		const string extension = ".wallet";

    //		string walletName = walletInfo.walletFileName;

    //		/* Filename ends in .wallet, remove extension */
    //		if (std::equal(extension.rbegin(), extension.rend(), walletInfo.walletFileName.rbegin()))
    //		{
    //			uint extPos = walletInfo.walletFileName.find_last_of('.');

    //			walletName = walletInfo.walletFileName.substr(0, extPos);
    //		}

    //		string shortName = walletName.Substring(0, promptLength);

    //		return "[" + WalletConfig.ticker + " " + shortName + "]: ";
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	public static string getInputAndWorkInBackground<T>(List<T> availableCommands, string prompt, bool backgroundRefresh, WalletInfo walletInfo)
    //	{
    //		/* If we are in the main program, we need to check for transactions in
    //		   the background. Unfortunately, we have to do this on the main thread,
    //		   so the best way to do it, is to check whilst waiting for an input on
    //		   another thread. */
    //		if (backgroundRefresh)
    //		{
    //			var lastUpdated = std::chrono.system_clock.now();

    //		std::future<string> inputGetter = std::async(std::launch.async, () =>
    //		{
    //			return getInput(availableCommands, prompt);
    //		});


    //			while (true)
    //			{
    //				/* Check if the user has inputted something yet
    //				   (Wait for zero seconds to instantly return) */
    //				std::future_status status = inputGetter.wait_for(std::chrono.seconds(0));

    //				/* User has inputted, get what they inputted and return it */
    //				if (status == std::future_status.ready)
    //				{
    //					return inputGetter.get();
    //				}

    //				var currentTime = std::chrono.system_clock.now();

    //				/* Otherwise check if we need to update the wallet cache */
    //				if ((currentTime - lastUpdated) > std::chrono.seconds(5))
    //				{
    //					lastUpdated = currentTime;
    //					checkForNewTransactions(walletInfo);
    //				}

    //				/* Sleep for enough for it to not be noticeable when the user
    //				   enters something, but enough that we're not starving the CPU */
    //				std::this_thread.sleep_for(std::chrono.milliseconds(50));
    //			}
    //		}
    //		else
    //		{
    //			return getInput(availableCommands, prompt);
    //		}
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	public static string getInput<T>(List<T> availableCommands, string prompt)
    //	{
    ////C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
    ////ORIGINAL LINE: linenoise::SetCompletionCallback([availableCommands](const char *input, ClassicVector<string> &completions)
    //	linenoise.GlobalMembers.SetCompletionCallback((string input, List<string> completions) =>
    //	{
    //		/* Convert to std::string */
    //		string c = input;

    //		foreach (var command in availableCommands)
    //		{
    //			/* Does command begin with input? */
    //			if (command.commandName.compare(0, c.Length, c) == 0)
    //			{
    //				completions.Add(command.commandName);
    //			}
    //		}
    //	});

    //		/* Linenoise is printing this out, so we can't write colours to the stream
    //		   like we normally would - have to include the escape characters directly
    //		   in the string. Obviously this is not platform dependent - but linenoise
    //		   doesn't work on windows, so it's fine. */
    //		string promptMsg = yellowANSIMsg(prompt);

    //		/* 256 max commands in the wallet command history */
    //		linenoise.GlobalMembers.SetHistoryMaxLen(256);

    //		/* The inputted command */
    //		string command;

    //		bool quit = linenoise.GlobalMembers.Readline(promptMsg, command);

    //		/* Remove any whitespace */
    //		trim(command);

    //		if (command != "")
    //		{
    //			linenoise.GlobalMembers.AddHistory(command);
    //		}

    //		/* Ctrl-C, Ctrl-D, etc */
    //		if (quit)
    //		{
    //			return "exit";
    //		}

    //		return command;
    //	}

    //	/* Template instantations that we are going to use - this allows us to have
    //	   the template implementation in the .cpp file. */
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//template string getInput(ClassicVector<Command> availableCommands, string prompt);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//template string getInput(ClassicVector<AdvancedCommand> availableCommands, string prompt);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//template string getInputAndWorkInBackground(ClassicVector<Command> availableCommands, string prompt, bool backgroundRefresh, WalletInfo walletInfo);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//template string getInputAndWorkInBackground(ClassicVector<AdvancedCommand> availableCommands, string prompt, bool backgroundRefresh, WalletInfo walletInfo);

    //	public static string parseCommand<T>(List<T> printableCommands, List<T> availableCommands, string prompt, bool backgroundRefresh, WalletInfo walletInfo)
    //	{
    //		while (true)
    //		{
    //			/* Get the input, and refresh the wallet in the background if desired
    //			   (This will be done on the main screen, but not the launch screen) */
    //			string selection = getInputAndWorkInBackground(availableCommands, prompt, backgroundRefresh, walletInfo);

    //			/* Convert to lower case */
    //			std::transform(selection.GetEnumerator(), selection.end(), selection.GetEnumerator(), global::tolower);

    //			/* \n == no-op */
    //			if (selection == "")
    //			{
    //				continue;
    //			}

    //			try
    //			{
    //				int selectionNum = Convert.ToInt32(selection);

    //				/* Input is in 1 based indexing, we need 0 based indexing */
    //				selectionNum--;

    //				int numCommands = (int)availableCommands.Count;

    //				/* Must be in the bounds of the vector */
    //				if (selectionNum < 0 || selectionNum >= numCommands)
    //				{
    //					Console.Write(WarningMsg("Bad input, expected a command name, "));
    //					Console.Write(WarningMsg("or number from "));
    //					Console.Write(InformationMsg("1"));
    //					Console.Write(WarningMsg(" to "));
    //					Console.Write(InformationMsg(Convert.ToString(numCommands)));
    //					Console.Write("\n");

    //					/* Print the available commands again if the input is bad */
    //					printCommands(printableCommands);

    //					continue;
    //				}

    //				selection = availableCommands[selectionNum].commandName;
    //			}
    //			/* Input ain't a number */
    //			catch (System.ArgumentException)
    //			{
    //				/* Iterator pointing to the command, if it exists */
    ////C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
    //			var it = std::find_if(availableCommands.GetEnumerator(), availableCommands.end(), (Command c) =>
    //			{
    //									   return c.commandName == selection;
    //			});

    //				/* Command doesn't exist in availableCommands */
    //				if (it == availableCommands.end())
    //				{
    //					Console.Write("Unknown command: ");
    //					Console.Write(WarningMsg(selection));
    //					Console.Write("\n");

    //					/* Print the available commands again if the input is bad */
    //					printCommands(printableCommands);

    //					continue;
    //				}
    //			}

    //			/* All good */
    //			return selection;
    //		}
    //	}

    //	public static Tuple<bool, WalletInfo> selectionScreen(Config config, CryptoNote.WalletGreen wallet, CryptoNote.INode node)
    //	{
    //		while (true)
    //		{
    //			/* Get the users action */
    //			string launchCommand = getAction(config);

    //			/* User wants to exit */
    //			if (launchCommand == "exit")
    //			{
    //				return new Tuple<bool, WalletInfo>(true, null);
    //			}

    //			/* Handle the user input */
    //			WalletInfo walletInfo = handleLaunchCommand(wallet, launchCommand, config);

    //			/* Action failed, for example wallet file is corrupted. */
    //			if (walletInfo == null)
    //			{
    //				Console.Write(InformationMsg("Returning to selection screen..."));
    //				Console.Write("\n");

    //				continue;
    //			}

    //			/* Node is down, user wants to exit */
    //			if (!checkNodeStatus(node))
    //			{
    //				return new Tuple<bool, WalletInfo>(true, null);
    //			}

    //			/* If we're creating a wallet, don't print the lengthy sync process */
    //			if (launchCommand == "create")
    //			{
    //				std::stringstream str = new std::stringstream();

    //				str << std::endl << "Your wallet is syncing with the network in the background." << std::endl << "Until this is completed new transactions might not show " << "up." << std::endl << "Use the status command to check the progress." << std::endl;

    //				Console.Write(InformationMsg(str.str()));
    //			}
    //			else
    //			{
    //				/* Need another signal handler here, in case the user does
    //				   ctrl+c whilst syncing, to save the wallet. The walletInfo
    //				   ptr will be null in the parent scope, since we haven't returned
    //				   it yet. */
    //				bool alreadyShuttingDown = false;

    //			Tools.SignalHandler.install(() =>
    //			{
    //				if (shutdown(walletInfo, node, alreadyShuttingDown))
    //				{
    //					Environment.Exit(0);
    //				}
    //			});

    //				syncWallet(node, walletInfo);
    //			}

    //			/* Return the wallet info */
    //			return new Tuple<bool, WalletInfo>(false, walletInfo);
    //		}
    //	}

    //	public static bool checkNodeStatus(CryptoNote.INode node)
    //	{
    //		while (node.getLastKnownBlockHeight() == 0)
    //		{
    //			std::stringstream msg = new std::stringstream();

    //			msg << "It looks like " << WalletConfig.daemonName << " isn't open!" << std::endl << std::endl << "Ensure " << WalletConfig.daemonName << " is open and has finished initializing." << std::endl << "If it's still not working, try restarting " << WalletConfig.daemonName << "." << "The daemon sometimes gets stuck." << std::endl << "Alternatively, perhaps " << WalletConfig.daemonName << " can't communicate with any peers." << std::endl << std::endl << "The wallet can't function fully until it can communicate with " << "the network.";

    //			Console.Write(WarningMsg(msg.str()));
    //			Console.Write("\n");

    //			/* Print the commands */
    //			printCommands(nodeDownCommands());

    //			/* See what the user wants to do */
    //			string command = parseCommand(nodeDownCommands(), nodeDownCommands(), "What would you like to do?: ", false, null);

    //			/* If they want to try again, check the node height again */
    //			if (command == "try_again")
    //			{
    //				continue;
    //			}
    //			/* If they want to exit, exit */
    //			else if (command == "exit")
    //			{
    //				return false;
    //			}
    //			/* If they want to continue, proceed to the menu screen */
    //			else if (command == "continue")
    //			{
    //				return true;
    //			}
    //		}

    //		return true;
    //	}

    //	public static string getAction(Config config)
    //	{
    //		if (config.walletGiven || config.passGiven)
    //		{
    //			return "open";
    //		}

    //		printCommands(startupCommands());

    //		return parseCommand(startupCommands(), startupCommands(), "What would you like to do?: ", false, null);
    //	}

    //	public static void mainLoop(WalletInfo walletInfo, CryptoNote.INode node)
    //	{
    //		if (walletInfo.viewWallet)
    //		{
    //			printCommands(basicViewWalletCommands());
    //		}
    //		else
    //		{
    //			printCommands(basicCommands());
    //		}

    //		while (true)
    //		{
    //			string command;

    //			if (walletInfo.viewWallet)
    //			{
    //				command = parseCommand(basicViewWalletCommands(), allViewWalletCommands(), getPrompt(walletInfo), true, walletInfo);
    //			}
    //			else
    //			{
    //				command = parseCommand(basicCommands(), allCommands(), getPrompt(walletInfo), true, walletInfo);
    //			}

    //			/* User exited */
    //			if (!handleCommand(command, walletInfo, node))
    //			{
    //				return;
    //			}
    //		}
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	public static void printCommands<T>(List<T> commands, uint offset)
    //	{
    //		uint i = 1 + offset;

    //		Console.Write("\n");

    //		foreach (var command in commands)
    //		{
    //			Console.Write(InformationMsg(" "));
    //			Console.Write(InformationMsg(Convert.ToString(i)));
    //			Console.Write("\t");
    //			Console.Write(SuccessMsg(command.commandName, 25));
    //			Console.Write(command.description);
    //			Console.Write("\n");

    //			i++;
    //		}

    //		Console.Write("\n");
    //	}

    //	/* Template instantations that we are going to use - this allows us to have
    //	   the template implementation in the .cpp file. */
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//template string parseCommand(ClassicVector<Command> printableCommands, ClassicVector<Command> availableCommands, string prompt, bool backgroundRefresh, WalletInfo walletInfo);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//template string parseCommand(ClassicVector<AdvancedCommand> printableCommands, ClassicVector<AdvancedCommand> availableCommands, string prompt, bool backgroundRefresh, WalletInfo walletInfo);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//template void printCommands(ClassicVector<Command> commands, uint offset);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//template void printCommands(ClassicVector<AdvancedCommand> commands, uint offset);

    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	// 
    //	// Please see the included LICENSE file for more information.

    //	///////////////////////////
    //	///////////////////////////





    //	public static WalletInfo createViewWallet(CryptoNote.WalletGreen wallet)
    //	{
    //		Console.Write(WarningMsg("View wallets are only for viewing incoming "));
    //		Console.Write(WarningMsg("transactions, and cannot make transfers."));
    //		Console.Write("\n");

    //		bool create = confirm("Is this OK?");

    //		Console.Write("\n");

    //		if (!create)
    //		{
    //			return null;
    //		}

    //		Crypto.SecretKey privateViewKey = getPrivateKey("Private View Key: ");

    //		string address;

    //		while (true)
    //		{
    //			Console.Write(InformationMsg("Enter your public "));
    //			Console.Write(InformationMsg(WalletConfig.ticker));
    //			Console.Write(InformationMsg(" address: "));

    //			address = Console.ReadLine();
    //			trim(address);

    //			if (parseStandardAddress(address, true))
    //			{
    //				break;
    //			}
    //		}

    //		string walletFileName = getNewWalletFileName();

    //		const string msg = "Give your new wallet a password: ";

    //		string walletPass = getWalletPassword(true, msg);

    //		ulong scanHeight = getScanHeight();

    //		wallet.createViewWallet(walletFileName, walletPass, address, privateViewKey, scanHeight, false);

    //		Console.Write("\n");
    //		Console.Write(InformationMsg("Your view wallet "));
    //		Console.Write(InformationMsg(address));
    //		Console.Write(InformationMsg(" has been successfully imported!"));
    //		Console.Write("\n");
    //		Console.Write("\n");

    //		viewWalletMsg();

    //		return new WalletInfo(walletFileName, walletPass, address, true, wallet);
    //	}

    //	public static WalletInfo importWallet(CryptoNote.WalletGreen wallet)
    //	{
    //		Crypto.SecretKey privateSpendKey = getPrivateKey("Enter your private spend key: ");

    //		Crypto.SecretKey privateViewKey = getPrivateKey("Enter your private view key: ");

    //		return importFromKeys(wallet, new Crypto.SecretKey(privateSpendKey), new Crypto.SecretKey(privateViewKey));
    //	}

    //	public static WalletInfo mnemonicImportWallet(CryptoNote.WalletGreen wallet)
    //	{
    //		while (true)
    //		{
    //			Console.Write(InformationMsg("Enter your mnemonic phrase (25 words): "));

    //			string mnemonicPhrase;

    //			mnemonicPhrase = Console.ReadLine();

    //			trim(mnemonicPhrase);

    //			var (error, privateSpendKey) = Mnemonics.MnemonicToPrivateKey(mnemonicPhrase);

    //			if (!error.empty())
    //			{
    //				Console.Write("\n");
    //				Console.Write(WarningMsg(error));
    //				Console.Write("\n");
    //				Console.Write("\n");
    //			}
    //			else
    //			{
    //				Crypto.SecretKey privateViewKey = new Crypto.SecretKey();

    //				CryptoNote.AccountBase.generateViewFromSpend(privateSpendKey, privateViewKey);

    //				return importFromKeys(wallet, privateSpendKey, new Crypto.SecretKey(privateViewKey));
    //			}
    //		}
    //	}

    //	public static WalletInfo importFromKeys(CryptoNote.WalletGreen wallet, Crypto.SecretKey privateSpendKey, Crypto.SecretKey privateViewKey)
    //	{
    //		string walletFileName = getNewWalletFileName();

    //		const string msg = "Give your new wallet a password: ";

    //		string walletPass = getWalletPassword(true, msg);

    //		ulong scanHeight = getScanHeight();

    //		connectingMsg();

    //		wallet.initializeWithViewKey(walletFileName, walletPass, privateViewKey, scanHeight, false);

    //		string walletAddress = wallet.createAddress(privateSpendKey, scanHeight, false);

    //		Console.Write("\n");
    //		Console.Write(InformationMsg("Your wallet "));
    //		Console.Write(InformationMsg(walletAddress));
    //		Console.Write(InformationMsg(" has been successfully imported!"));
    //		Console.Write("\n");
    //		Console.Write("\n");

    //		return new WalletInfo(walletFileName, walletPass, walletAddress, false, wallet);
    //	}

    //	public static WalletInfo generateWallet(CryptoNote.WalletGreen wallet)
    //	{
    //		string walletFileName = getNewWalletFileName();

    //		const string msg = "Give your new wallet a password: ";

    //		string walletPass = getWalletPassword(true, msg);

    //		CryptoNote.KeyPair spendKey = new CryptoNote.KeyPair();
    //		Crypto.SecretKey privateViewKey = new Crypto.SecretKey();

    //		Crypto.generate_keys(spendKey.publicKey, spendKey.secretKey);

    //		CryptoNote.AccountBase.generateViewFromSpend(spendKey.secretKey, privateViewKey);

    //		wallet.initializeWithViewKey(walletFileName, walletPass, privateViewKey, 0, true);

    //		string walletAddress = wallet.createAddress(spendKey.secretKey, 0, true);

    //		promptSaveKeys(wallet);

    //		Console.Write(WarningMsg("If you lose these your wallet cannot be "));
    //		Console.Write(WarningMsg("recreated!"));
    //		Console.Write("\n");
    //		Console.Write("\n");

    //		return new WalletInfo(walletFileName, walletPass, walletAddress, false, wallet);
    //	}

    //	public static WalletInfo openWallet(CryptoNote.WalletGreen wallet, Config config)
    //	{
    //		string walletFileName = getExistingWalletFileName(config);

    //		bool initial = true;

    //		while (true)
    //		{
    //			string walletPass;

    //			/* Only use the command line pass once, otherwise we will infinite
    //			   loop if it is incorrect */
    //			if (initial && config.passGiven)
    //			{
    //				walletPass = config.walletPass;
    //			}
    //			else
    //			{
    //				walletPass = getWalletPassword(false, "Enter password: ");
    //			}

    //			initial = false;

    //			connectingMsg();

    //			try
    //			{
    //				wallet.load(walletFileName, walletPass);

    //				string walletAddress = wallet.getAddress(0);

    //				Crypto.SecretKey privateSpendKey = wallet.getAddressSpendKey(0).secretKey;

    //				bool viewWallet = false;

    //				if (privateSpendKey == CryptoNote.NULL_SECRET_KEY)
    //				{
    //					Console.Write("\n");
    //					Console.Write(InformationMsg("Your view only wallet "));
    //					Console.Write(InformationMsg(walletAddress));
    //					Console.Write(InformationMsg(" has been successfully opened!"));
    //					Console.Write("\n");
    //					Console.Write("\n");

    //					viewWalletMsg();

    //					viewWallet = true;

    //				}
    //				else
    //				{
    //					Console.Write("\n");
    //					Console.Write(InformationMsg("Your wallet "));
    //					Console.Write(InformationMsg(walletAddress));
    //					Console.Write(InformationMsg(" has been successfully opened!"));
    //					Console.Write("\n");
    //					Console.Write("\n");
    //				}

    //				return new WalletInfo(walletFileName, walletPass, walletAddress, viewWallet, wallet);

    //			}
    //			catch (std::system_error e)
    //			{
    //				bool handled = false;

    //				switch (e.code().value())
    //				{
    //					case CryptoNote.error.WRONG_PASSWORD:
    //					{
    //						Console.Write("\n");
    //						Console.Write(WarningMsg("Incorrect password! Try again."));
    //						Console.Write("\n");
    //						Console.Write("\n");

    //						handled = true;

    //						break;
    //					}
    //					case CryptoNote.error.WRONG_VERSION:
    //					{
    //						std::stringstream msg = new std::stringstream();

    //						msg << "Could not open wallet file! It doesn't appear " << "to be a valid wallet!" << std::endl << "Ensure you are opening a wallet file, and the " << "file has not gotten corrupted." << std::endl << "Try reimporting via keys, and always close " << WalletConfig.walletName << " with the exit " << "command to prevent corruption." << std::endl;

    //						Console.Write(WarningMsg(msg.str()));
    //						Console.Write("\n");

    //						return null;
    //					}
    //				}

    //				if (handled)
    //				{
    //					continue;
    //				}

    //				const string alreadyOpenMsg = "MemoryMappedFile::open: The process cannot access the file " + "because it is being used by another process.";

    //				string errorMsg = e.what();

    //				/* The message actually has a \r\n on the end but I'd prefer to
    //				   keep just the raw string in the source so check if it starts
    //				   with the message instead */
    //				if (startsWith(errorMsg, alreadyOpenMsg))
    //				{
    //					Console.Write(WarningMsg("Could not open wallet! It is already " + "open in another process."));
    //					Console.Write("\n");
    //					Console.Write(WarningMsg("Check with a task manager that you " + "don't have "));
    //					Console.Write(WalletConfig.walletName);
    //					Console.Write(WarningMsg(" open twice."));
    //					Console.Write("\n");
    //					Console.Write(WarningMsg("Also check you don't have another " + "wallet program open, such as a GUI " + "wallet or "));
    //					Console.Write(WarningMsg(WalletConfig.walletdName));
    //					Console.Write(WarningMsg("."));
    //					Console.Write("\n");
    //					Console.Write("\n");

    //					return null;
    //				}
    //				else
    //				{
    //					Console.Write("Unexpected error: ");
    //					Console.Write(errorMsg);
    //					Console.Write("\n");
    //					Console.Write("Please report this error message and what ");
    //					Console.Write("you did to cause it.");
    //					Console.Write("\n");
    //					Console.Write("\n");

    //					wallet.shutdown();
    //					return null;
    //				}
    //			}
    //		}
    //	}

    //	public static Crypto.SecretKey getPrivateKey(string msg)
    //	{
    //		const ulong privateKeyLen = 64;
    //		ulong size;

    //		string privateKeyString;
    //		Crypto.Hash privateKeyHash = new Crypto.Hash();
    //		Crypto.SecretKey privateKey = new Crypto.SecretKey();
    //		Crypto.PublicKey publicKey = new Crypto.PublicKey();

    //		while (true)
    //		{
    //			Console.Write(InformationMsg(msg));

    //			privateKeyString = Console.ReadLine();
    //			trim(privateKeyString);

    //			if (privateKeyString.Length != privateKeyLen)
    //			{
    //				Console.Write("\n");
    //				Console.Write(WarningMsg("Invalid private key, should be 64 "));
    //				Console.Write(WarningMsg("characters! Try again."));
    //				Console.Write("\n");
    //				Console.Write("\n");

    //				continue;
    //			}
    //			else if (!Common.fromHex(privateKeyString, privateKeyHash, sizeof(Crypto.Hash), size) || size != sizeof(Crypto.Hash))
    //			{
    //				Console.Write(WarningMsg("Invalid private key, it is not a valid "));
    //				Console.Write(WarningMsg("hex string! Try again."));
    //				Console.Write("\n");
    //				Console.Write("\n");

    //				continue;
    //			}

    //			privateKey = (Crypto.SecretKey) privateKeyHash;

    //			/* Just used for verification purposes before we pass it to
    //			   walletgreen */
    //			if (!Crypto.secret_key_to_public_key(privateKey, publicKey))
    //			{
    //				Console.Write("\n");
    //				Console.Write(WarningMsg("Invalid private key, is not on the "));
    //				Console.Write(WarningMsg("ed25519 curve!"));
    //				Console.Write("\n");
    //				Console.Write(WarningMsg("Probably a typo - ensure you entered "));
    //				Console.Write(WarningMsg("it correctly."));
    //				Console.Write("\n");
    //				Console.Write("\n");

    //				continue;
    //			}

    //			return privateKey;
    //		}
    //	}

    //	public static string getExistingWalletFileName(Config config)
    //	{
    //		bool initial = true;

    //		string walletName;

    //		while (true)
    //		{
    //			/* Only use wallet file once in case it is incorrect */
    //			if (config.walletGiven && initial)
    //			{
    //				walletName = config.walletFile;
    //			}
    //			else
    //			{
    //				Console.Write(InformationMsg("What is the name of the wallet "));
    //				Console.Write(InformationMsg("you want to open?: "));

    //				walletName = Console.ReadLine();
    //			}

    //			initial = false;

    //			string walletFileName = walletName + ".wallet";

    //			if (walletName == "")
    //			{
    //				Console.Write("\n");
    //				Console.Write(WarningMsg("Wallet name can't be blank! Try again."));
    //				Console.Write("\n");
    //				Console.Write("\n");
    //			}
    //			/* Allow people to enter wallet name with or without file extension */
    //			else if (fileExists(walletName))
    //			{
    //				return walletName;
    //			}
    //			else if (fileExists(walletFileName))
    //			{
    //				return walletFileName;
    //			}
    //			else
    //			{
    //				Console.Write("\n");
    //				Console.Write(WarningMsg("A wallet with the filename "));
    //				Console.Write(InformationMsg(walletName));
    //				Console.Write(WarningMsg(" or "));
    //				Console.Write(InformationMsg(walletFileName));
    //				Console.Write(WarningMsg(" doesn't exist!"));
    //				Console.Write("\n");
    //				Console.Write("Ensure you entered your wallet name correctly.");
    //				Console.Write("\n");
    //				Console.Write("\n");
    //			}
    //		}
    //	}

    //	public static string getNewWalletFileName()
    //	{
    //		string walletName;

    //		while (true)
    //		{
    //			Console.Write(InformationMsg("What would you like to call your "));
    //			Console.Write(InformationMsg("new wallet?: "));

    //			walletName = Console.ReadLine();

    //			string walletFileName = walletName + ".wallet";

    //			if (fileExists(walletFileName))
    //			{
    //				Console.Write("\n");
    //				Console.Write(WarningMsg("A wallet with the filename "));
    //				Console.Write(InformationMsg(walletFileName));
    //				Console.Write(WarningMsg(" already exists!"));
    //				Console.Write("\n");
    //				Console.Write("Try another name.");
    //				Console.Write("\n");
    //				Console.Write("\n");
    //			}
    //			else if (walletName == "")
    //			{
    //				Console.Write("\n");
    //				Console.Write(WarningMsg("Wallet name can't be blank! Try again."));
    //				Console.Write("\n");
    //				Console.Write("\n");
    //			}
    //			else
    //			{
    //				return walletFileName;
    //			}
    //		}
    //	}

    //	public static string getWalletPassword(bool verifyPwd, string msg)
    //	{
    //		Tools.PasswordContainer pwdContainer = new Tools.PasswordContainer();
    //		pwdContainer.read_password(verifyPwd, msg);
    //		return pwdContainer.password();
    //	}

    //	public static void viewWalletMsg()
    //	{
    //		Console.Write(InformationMsg("Please remember that when using a view wallet " + "you can only view incoming transactions!"));
    //		Console.Write("\n");
    //		Console.Write(InformationMsg("Therefore, if you have recieved transactions "));
    //		Console.Write(InformationMsg("which you then spent, your balance will "));
    //		Console.Write(InformationMsg("appear inflated."));
    //		Console.Write("\n");
    //	}

    //	public static void connectingMsg()
    //	{
    //		Console.Write("\n");
    //		Console.Write("Making initial contact with ");
    //		Console.Write(WalletConfig.daemonName);
    //		Console.Write(".");
    //		Console.Write("\n");
    //		Console.Write("Please wait, this sometimes can take a long time...");
    //		Console.Write("\n");
    //		Console.Write("\n");
    //	}

    //	public static void promptSaveKeys(CryptoNote.WalletGreen wallet)
    //	{
    //		Console.Write("Welcome to your new wallet, here is your payment address:");
    //		Console.Write("\n");
    //		Console.Write(InformationMsg(wallet.getAddress(0)));
    //		Console.Write("\n");
    //		Console.Write("\n");
    //		Console.Write("Please copy your secret keys and mnemonic seed and store ");
    //		Console.Write("them in a secure location: ");
    //		Console.Write("\n");

    //		printPrivateKeys(wallet, false);

    //		Console.Write("\n");
    //	}

    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	//
    //	// Please see the included LICENSE file for more information.

    //	/////////////////////////////////////
    //	/////////////////////////////////////

    //	//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
    //	//ORIGINAL LINE: #define __ROCKSDB_MAJOR__ ROCKSDB_MAJOR
    //	//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
    //	//ORIGINAL LINE: #define __ROCKSDB_MINOR__ ROCKSDB_MINOR
    //	//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
    //	//ORIGINAL LINE: #define __ROCKSDB_PATCH__ ROCKSDB_PATCH

    //	public static Config parseArguments(int argc, string[] argv)
    //	{
    //		Config config = new Config();

    //		std::stringstream defaultRemoteDaemon = new std::stringstream();
    //		defaultRemoteDaemon << config.host << ":" << config.port;

    //		cxxopts.Options options = new cxxopts.Options(argv[0], CryptoNote.getProjectCLIHeader());

    //		bool help;
    //		bool version;
    //		string remoteDaemon;

    //		options.add_options("Core")("h,help", "Display this help message", cxxopts.value<bool>(help).implicit_value("true"))("v,version", "Output software version information", cxxopts.value<bool>(version).default_value("false").implicit_value("true"))("debug", "Enable " + WalletConfig.walletdName + " debugging to " + WalletConfig.walletName + ".log", cxxopts.value<bool>(config.debug).default_value("false").implicit_value("true"));

    //		options.add_options("Daemon")("r,remote-daemon", "The daemon <host:port> combination to use for node operations.", cxxopts.value<string>(remoteDaemon).default_value(defaultRemoteDaemon.str()), "<host:port>");

    //		options.add_options("Wallet")("w,wallet-file", "Open the wallet <file>", cxxopts.value<string>(config.walletFile), "<file>")("p,password", "Use the password <pass> to open the wallet", cxxopts.value<string>(config.walletPass), "<pass>");

    //		try
    //		{
    //			var result = options.parse(argc, argv);
    //		}
    //		catch (cxxopts.OptionException e)
    //		{
    //			Console.Write("Error: Unable to parse command line argument options: ");
    //			Console.Write(e.what());
    //			Console.Write("\n");
    //			Console.Write("\n");
    //			Console.Write(options.help({}));
    //			Console.Write("\n");
    //			Environment.Exit(1);
    //		}

    //		if (help) // Do we want to display the help message?
    //		{
    //			Console.Write(options.help({}));
    //			Console.Write("\n");
    //			Environment.Exit(0);
    //		}
    //		else if (version) // Do we want to display the software version?
    //		{
    //			Console.Write(CryptoNote.getProjectCLIHeader());
    //			Console.Write("\n");
    //			Environment.Exit(0);
    //		}

    //		if (!config.walletFile.empty())
    //		{
    //			config.walletGiven = true;
    //		}

    //		if (!config.walletPass.empty())
    //		{
    //			config.passGiven = true;
    //		}

    //		if (!string.IsNullOrEmpty(remoteDaemon))
    //		{
    //			if (!parseDaemonAddressFromString(config.host, config.port, remoteDaemon))
    //			{
    //				Console.Write("There was an error parsing the --remote-daemon you specified");
    //				Console.Write("\n");
    //				Environment.Exit(1);
    //			}
    //		}

    //		return config;
    //	}
    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	// 
    //	// Please see the included LICENSE file for more information.

    //	///////////////////////////
    //	///////////////////////////



    //	public static void checkForNewTransactions(WalletInfo walletInfo)
    //	{
    //		walletInfo.wallet.updateInternalCache();

    //		uint newTransactionCount = walletInfo.wallet.getTransactionCount();

    //		if (newTransactionCount != walletInfo.knownTransactionCount)
    //		{
    //			for (uint i = walletInfo.knownTransactionCount; i < newTransactionCount; i++)
    //			{
    //				CryptoNote.WalletTransaction t = walletInfo.wallet.getTransaction(i);

    //				/* Don't print outgoing or fusion transfers */
    //				if (t.totalAmount > 0 && t.fee != 0)
    //				{
    ////C++ TO C# CONVERTER TODO TASK: The cout 'flush' manipulator is not converted by C++ to C# Converter:
    ////ORIGINAL LINE: std::cout << std::endl << InformationMsg("New transaction found!") << std::endl << SuccessMsg("Incoming transfer:") << std::endl << SuccessMsg("Hash: " + Common::podToHex(t.hash)) << std::endl << SuccessMsg("Amount: " + formatAmount(t.totalAmount)) << std::endl << InformationMsg(getPrompt(walletInfo)) << std::flush;
    //					Console.Write("\n");
    //					Console.Write(InformationMsg("New transaction found!"));
    //					Console.Write("\n");
    //					Console.Write(SuccessMsg("Incoming transfer:"));
    //					Console.Write("\n");
    //					Console.Write(SuccessMsg("Hash: " + Common.podToHex(t.hash)));
    //					Console.Write("\n");
    //					Console.Write(SuccessMsg("Amount: " + formatAmount(t.totalAmount)));
    //					Console.Write("\n");
    //					Console.Write(InformationMsg(getPrompt(walletInfo)));
    //				}
    //			}

    //			walletInfo.knownTransactionCount = newTransactionCount;
    //		}
    //	}

    //	public static void syncWallet(CryptoNote.INode node, WalletInfo walletInfo)
    //	{
    //		uint localHeight = node.getLastLocalBlockHeight();
    //		uint walletHeight = walletInfo.wallet.getBlockCount();
    //		uint remoteHeight = node.getLastKnownBlockHeight();

    //		uint transactionCount = walletInfo.wallet.getTransactionCount();

    //		int stuckCounter = 0;

    //		if (localHeight != remoteHeight)
    //		{
    //			Console.Write("Your ");
    //			Console.Write(WalletConfig.daemonName);
    //			Console.Write(" isn't fully ");
    //			Console.Write("synced yet!");
    //			Console.Write("\n");
    //			Console.Write("Until you are fully synced, you won't be able to send ");
    //			Console.Write("transactions,");
    //			Console.Write("\n");
    //			Console.Write("and your balance may be missing or ");
    //			Console.Write("incorrect!");
    //			Console.Write("\n");
    //			Console.Write("\n");
    //		}

    //		/* If we open a legacy wallet then it will load the transactions but not
    //		   have the walletHeight == transaction height. Lets just throw away the
    //		   transactions and rescan. */
    //		if (walletHeight == 1 && transactionCount != 0)
    //		{
    //			Console.Write("Upgrading your wallet from an older version of the ");
    //			Console.Write("software...");
    //			Console.Write("\n");
    //			Console.Write("Unfortunately, we have ");
    //			Console.Write("to rescan the chain to find your transactions.");
    //			Console.Write("\n");

    //			transactionCount = 0;

    //			walletInfo.wallet.clearCaches(true, false);
    //		}

    //		if (walletHeight == 1)
    //		{
    //			Console.Write("Scanning through the blockchain to find transactions ");
    //			Console.Write("that belong to you.");
    //			Console.Write("\n");
    //			Console.Write("Please wait, this will take some time.");
    //			Console.Write("\n");
    //			Console.Write("\n");
    //		}
    //		else
    //		{
    //			Console.Write("Scanning through the blockchain to find any new ");
    //			Console.Write("transactions you received");
    //			Console.Write("\n");
    //			Console.Write("whilst your wallet wasn't open.");
    //			Console.Write("\n");
    //			Console.Write("Please wait, this may take some time.");
    //			Console.Write("\n");
    //			Console.Write("\n");
    //		}

    //		int counter = 1;

    //		while (walletHeight < localHeight)
    //		{
    //			/* This MUST be called on the main thread! */
    //			walletInfo.wallet.updateInternalCache();

    //			localHeight = node.getLastLocalBlockHeight();
    //			remoteHeight = node.getLastKnownBlockHeight();
    //			Console.Write(SuccessMsg(Convert.ToString(walletHeight)));
    //			Console.Write(" of ");
    //			Console.Write(InformationMsg(Convert.ToString(localHeight)));
    //			Console.Write("\n");

    //			uint tmpWalletHeight = walletInfo.wallet.getBlockCount();

    //			int waitSeconds = 1;

    //			/* Save periodically so if someone closes before completion they don't
    //			   lose all their progress. Saving is actually quite slow with big
    //			   wallets so lets do it every 10 minutes */
    //			if (counter % 600 == 0)
    //			{
    //				Console.Write("\n");
    //				Console.Write(InformationMsg("Saving current progress..."));
    //				Console.Write("\n");
    //				Console.Write("\n");

    //				walletInfo.wallet.save();
    //			}

    //			if (tmpWalletHeight == walletHeight)
    //			{
    //				stuckCounter++;
    //				waitSeconds = 3;

    //				if (stuckCounter > 20)
    //				{
    //					Console.Write(WarningMsg("Syncing may be stuck. Try restarting "));
    //					Console.Write(WarningMsg(WalletConfig.daemonName));
    //					Console.Write(WarningMsg("."));
    //					Console.Write("\n");
    //					Console.Write(WarningMsg("If this persists, visit "));
    //					Console.Write(WarningMsg(WalletConfig.contactLink));
    //					Console.Write(WarningMsg(" for support."));
    //					Console.Write("\n");
    //				}
    //				else if (stuckCounter > 19)
    //				{
    //					/*
    //					   Calling save has the side-effect of starting
    //					   and stopping blockchainSynchronizer, which seems
    //					   to sometimes force the sync to resume properly.
    //					   So we'll try this before warning the user.
    //					*/
    //					walletInfo.wallet.save();
    //					waitSeconds = 5;
    //				}
    //			}
    //			else
    //			{
    //				stuckCounter = 0;
    //				walletHeight = tmpWalletHeight;

    //				uint tmpTransactionCount = walletInfo.wallet.getTransactionCount();

    //				if (tmpTransactionCount != transactionCount)
    //				{
    //					for (uint i = transactionCount; i < tmpTransactionCount; i++)
    //					{
    //						CryptoNote.WalletTransaction t = walletInfo.wallet.getTransaction(i);

    //						/* Don't print out fusion transactions */
    //						if (t.totalAmount != 0)
    //						{
    //							Console.Write("\n");
    //							Console.Write(InformationMsg("New transaction found!"));
    //							Console.Write("\n");
    //							Console.Write("\n");

    //							if (t.totalAmount < 0)
    //							{
    //								printOutgoingTransfer(t, node);
    //							}
    //							else
    //							{
    //								printIncomingTransfer(t, node);
    //							}
    //						}
    //					}

    //					transactionCount = tmpTransactionCount;
    //				}
    //			}

    //			counter++;

    //			std::this_thread.sleep_for(std::chrono.seconds(waitSeconds));
    //		}

    //		Console.Write("\n");
    //		Console.Write(SuccessMsg("Finished scanning blockchain!"));
    //		Console.Write("\n");

    //		/* In case the user force closes, we don't want them to have to rescan
    //		   the whole chain. */
    //		walletInfo.wallet.save();

    //		walletInfo.knownTransactionCount = transactionCount;
    //	}

    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	// 
    //	// Please see the included LICENSE file for more information.

    //	////////////////////////////
    //	////////////////////////////







    //	public static void confirmPassword(string walletPass, string msg)
    //	{
    //		/* Password container requires an rvalue, we don't want to wipe our current
    //		   pass so copy it into a tmp string and std::move that instead */
    //		string tmpString = walletPass;
    //		Tools.PasswordContainer pwdContainer = new Tools.PasswordContainer(std::move(tmpString));

    //		while (!pwdContainer.read_and_validate(msg))
    //		{
    //			Console.Write(WarningMsg("Incorrect password! Try again."));
    //			Console.Write("\n");
    //		}
    //	}

    //	/* Get the amount we need to divide to convert from atomic to pretty print,
    //	   e.g. 100 for 2 decimal places */
    //	public static ulong getDivisor()
    //	{
    //		return (ulong)Math.Pow(10, WalletConfig.numDecimalPlaces);
    //	}

    //	public static string formatAmount(ulong amount)
    //	{
    //		ulong divisor = getDivisor();
    //		ulong dollars = amount / divisor;
    //		ulong cents = amount % divisor;

    //		return formatDollars(dollars) + "." + formatCents(cents) + " " = new return();
    //			 + WalletConfig.ticker;
    //	}

    //	public static string formatAmountBasic(ulong amount)
    //	{
    //		ulong divisor = getDivisor();
    //		ulong dollars = amount / divisor;
    //		ulong cents = amount % divisor;

    //		return Convert.ToString(dollars) + "." + formatCents(cents);
    //	}

    //	public static string formatDollars(ulong amount)
    //	{
    //		/* We want to format our number with comma separators so it's easier to
    //		   use. Now, we could use the nice print_money() function to do this.
    //		   However, whilst this initially looks pretty handy, if we have a locale
    //		   such as ja_JP.utf8, 1 TRTL will actually be formatted as 100 TRTL, which
    //		   is terrible, and could really screw over users.

    //		   So, easy solution right? Just use en_US.utf8! Sure, it's not very
    //		   international, but it'll work! Unfortunately, no. The user has to have
    //		   the locale installed, and if they don't, we get a nasty error at
    //		   runtime.

    //		   Annoyingly, there's no easy way to comma separate numbers outside of
    //		   using the locale method, without writing a pretty long boiler plate
    //		   function. So, instead, we define our own locale, which just returns
    //		   the values we want.

    //		   It's less internationally friendly than we would potentially like
    //		   but that would require a ton of scrutinization which if not done could
    //		   land us with quite a few issues and rightfully angry users.
    //		   Furthermore, we'd still have to hack around cases like JP locale
    //		   formatting things incorrectly, and it makes reading in inputs harder
    //		   too. */

    //		/* Thanks to https://stackoverflow.com/a/7277333/8737306 for this neat
    //		   workaround */
    ////C++ TO C# CONVERTER TODO TASK: C# does not allow declaring types within methods:
    //	//	class comma_numpunct : public std::numpunct<char>
    //	//	{
    //	//	  protected:
    //	//		virtual char do_thousands_sep() const
    //	//		{
    //	//			return ',';
    //	//		}
    //	//
    //	//		virtual string do_grouping() const
    //	//		{
    //	//			return "\x0003";
    //	//		}
    //	//	};

    //		std::locale comma_locale = new std::locale(std::locale(), new comma_numpunct());
    //		std::stringstream stream = new std::stringstream();
    //		stream.imbue(comma_locale);
    //		stream << (int)amount;
    //		return stream.str();
    //	}

    //	/* Pad to the amount of decimal spaces, e.g. with 2 decimal spaces 5 becomes
    //	   05, 50 remains 50 */
    //	public static string formatCents(ulong amount)
    //	{
    //		std::stringstream stream = new std::stringstream();
    //		stream << std::setfill('0') << std::setw(WalletConfig.numDecimalPlaces) << (int)amount;
    //		return stream.str();
    //	}

    //	public static bool confirm(string msg)
    //	{
    //		return confirm(msg, true);
    //	}

    //	/* defaultReturn = what value we return on hitting enter, i.e. the "expected"
    //	   workflow */
    //	public static bool confirm(string msg, bool defaultReturn)
    //	{
    //		/* In unix programs, the upper case letter indicates the default, for
    //		   example when you hit enter */
    //		string prompt = " (Y/n): ";

    //		/* Yes, I know I can do !defaultReturn. It doesn't make as much sense
    //		   though. If someone deletes this comment to make me look stupid I'll be
    //		   mad >:( */
    //		if (defaultReturn == false)
    //		{
    //			prompt = " (y/N): ";
    //		}

    //		while (true)
    //		{
    //			Console.Write(InformationMsg(msg + prompt));

    //			string answer;
    //			answer = Console.ReadLine();

    //			char c = global::tolower(answer[0]);

    //			switch (c)
    //			{
    //				/* Lets people spam enter / choose default value */
    //				case '\0':
    //					return defaultReturn;
    //				case 'y':
    //					return true;
    //				case 'n':
    //					return false;
    //			}

    //			Console.Write(WarningMsg("Bad input: "));
    //			Console.Write(InformationMsg(answer));
    //			Console.Write(WarningMsg(" - please enter either Y or N."));
    //			Console.Write("\n");
    //		}
    //	}

    //	public static string getPaymentIDFromExtra(string extra)
    //	{
    //		string paymentID;

    //		if (extra.Length > 0)
    //		{
    //			List<byte> vecExtra = new List<byte>();

    //			foreach (var it in extra)
    //			{
    //				vecExtra.Add((byte)it);
    //			}

    //			Crypto.Hash paymentIdHash = new Crypto.Hash();

    //			if (CryptoNote.getPaymentIdFromTxExtra(vecExtra, paymentIdHash))
    //			{
    //				return Common.podToHex(paymentIdHash);
    //			}
    //		}

    //		return paymentID;
    //	}

    //	public static string unixTimeToDate(ulong timestamp)
    //	{
    //		std::DateTime time = timestamp;
    //		string buffer = new string(new char[100]);
    //		std::strftime(buffer, sizeof(char), "%F %R", std::localtime(time));
    //		return (string)buffer;
    //	}

    //	public static string createIntegratedAddress(string address, string paymentID)
    //	{
    //		ulong prefix;

    //		CryptoNote.AccountPublicAddress addr = new CryptoNote.AccountPublicAddress();

    //		/* Get the private + public key from the address */
    //		CryptoNote.parseAccountAddressString(prefix, addr, address);

    //		/* Pack as a binary array */
    //		CryptoNote.BinaryArray ba = new CryptoNote.BinaryArray();
    //		CryptoNote.toBinaryArray(addr, ba);
    //		string keys = Common.asString(ba);

    //		/* Encode prefix + paymentID + keys as an address */
    //		return Tools.Base58.encode_addr(CryptoNote.parameters.CRYPTONOTE_PUBLIC_ADDRESS_BASE58_PREFIX, paymentID + keys);
    //	}

    //	public static ulong getScanHeight()
    //	{
    //		while (true)
    //		{
    //			Console.Write(InformationMsg("What height would you like to begin "));
    //			Console.Write(InformationMsg("scanning your wallet from?"));
    //			Console.Write("\n");
    //			Console.Write("\n");
    //			Console.Write("This can greatly speed up the initial wallet ");
    //			Console.Write("scanning process.");
    //			Console.Write("\n");
    //			Console.Write("\n");
    //			Console.Write("If you do not know the exact height, ");
    //			Console.Write("err on the side of caution so transactions do not ");
    //			Console.Write("get missed.");
    //			Console.Write("\n");
    //			Console.Write("\n");
    //			Console.Write(InformationMsg("Hit enter for the sub-optimal default "));
    //			Console.Write(InformationMsg("of zero: "));

    //			string stringHeight;

    //			stringHeight = Console.ReadLine();

    //			/* Remove commas so user can enter height as e.g. 200,000 */
    //			removeCharFromString(stringHeight, ',');

    //			if (stringHeight == "")
    //			{
    //				return 0;
    //			}

    //			try
    //			{
    //				return Convert.ToInt32(stringHeight);
    //			}
    //			catch (System.ArgumentException)
    //			{
    //				Console.Write(WarningMsg("Failed to parse height - input is not "));
    //				Console.Write(WarningMsg("a number!"));
    //				Console.Write("\n");
    //				Console.Write("\n");
    //			}
    //		}
    //	}

    //	/* Erases all instances of c from the string. E.g. 2,000,000 becomes 2000000 */
    //	public static void removeCharFromString(string str, char c)
    //	{
    //		str = str.Remove(std::remove(str.GetEnumerator(), str.end(), c), str.end());
    //	}

    //	/* Trims any whitespace from left and right */
    //	public static void trim(string str)
    //	{
    //		rightTrim(str);
    //		leftTrim(str);
    //	}

    //	public static void leftTrim(string str)
    //	{
    //		string whitespace = " \t\n\r\f\v";

    //		str = str.Remove(0, str.find_first_not_of(whitespace));
    //	}

    //	public static void rightTrim(string str)
    //	{
    //		string whitespace = " \t\n\r\f\v";

    //		str = str.erase(str.find_last_not_of(whitespace) + 1);
    //	}

    //	/* Checks if str begins with substring */
    //	public static bool startsWith(string str, string substring)
    //	{
    //		return str.LastIndexOf(substring, 0) == 0;
    //	}

    //	/* Does the given filename exist on disk? */
    //	public static bool fileExists(string filename)
    //	{
    //		/* Bool conversion needs an explicit cast */
    //		return (bool)std::ifstream(filename);
    //	}

    //	public static bool shutdown(WalletInfo walletInfo, CryptoNote.INode node, ref bool alreadyShuttingDown)
    //	{
    //		if (alreadyShuttingDown)
    //		{
    //			Console.Write("Patience little turtle, we're already shutting down!");
    //			Console.Write("\n");

    //			return false;
    //		}

    //		Console.Write(InformationMsg("Shutting down..."));
    //		Console.Write("\n");

    //		alreadyShuttingDown = true;

    //		bool finishedShutdown = false;

    //	std::thread timelyShutdown(() =>
    //	{
    //		var startTime = std::chrono.system_clock.now();

    //		/* Has shutdown finished? */
    //		while (!finishedShutdown)
    //		{
    //			var currentTime = std::chrono.system_clock.now();

    //			/* If not, wait for a max of 20 seconds then force exit. */
    //			if ((currentTime - startTime) > std::chrono.seconds(20))
    //			{
    //				Console.Write(WarningMsg("Wallet took too long to save! " + "Force closing."));
    //				Console.Write("\n");
    //				Console.Write("Bye.");
    //				Console.Write("\n");
    //				Environment.Exit(0);
    //			}

    //			std::this_thread.sleep_for(std::chrono.seconds(1));
    //		}
    //	});

    //		if (walletInfo != null)
    //		{
    //			Console.Write(InformationMsg("Saving wallet file..."));
    //			Console.Write("\n");

    //			walletInfo.wallet.save();

    //			Console.Write(InformationMsg("Shutting down wallet interface..."));
    //			Console.Write("\n");

    //			walletInfo.wallet.shutdown();
    //		}

    //		Console.Write(InformationMsg("Shutting down node connection..."));
    //		Console.Write("\n");

    //		node.shutdown();

    //		finishedShutdown = true;

    //		/* Wait for shutdown watcher to finish */
    //		timelyShutdown.join();

    //		Console.Write("Bye.");
    //		Console.Write("\n");

    //		return true;
    //	}

    //	public static List<string> split(string str, char delim = ' ')
    //	{
    //		List<string> cont = new List<string>();
    //		std::stringstream ss = new std::stringstream(str);
    //		string token;
    //		while (getline(ss, token, delim))
    //		{
    //			cont.Add(token);
    //		}
    //		return cont;
    //	}

    //	public static bool parseDaemonAddressFromString(ref string host, ref int port, string address)
    //	{
    //		List<string> parts = split(address, ':');

    //		if (parts.Count == 0)
    //		{
    //			return false;
    //		}
    //		else if (parts.Count >= 2)
    //		{
    //			try
    //			{
    //				host = parts[0];
    //				port = Convert.ToInt32(parts[1]);
    //				return true;
    //			}
    //			catch (System.ArgumentException)
    //			{
    //			  return false;
    //			}
    //		}

    //		host = parts[0];
    //		port = CryptoNote.RPC_DEFAULT_PORT;
    //		return true;
    //	}



    //	public static bool parseAmount(string strAmount, ref ulong amount)
    //	{
    //		/* Trim any whitespace */
    //		trim(strAmount);

    //		/* If the user entered thousand separators, remove them */
    //		removeCharFromString(strAmount, ',');

    //		uint pointIndex = strAmount.IndexOfAny((Convert.ToString('.')).ToCharArray());
    //		uint numDecimalPlaces = WalletConfig.numDecimalPlaces;

    //		uint fractionSize;

    //		if (-1 != pointIndex)
    //		{
    //			fractionSize = strAmount.Length - pointIndex - 1;

    //			while (numDecimalPlaces < fractionSize && '0' == strAmount.back())
    //			{
    //				strAmount = strAmount.Remove(strAmount.Length - 1, 1);
    //				fractionSize--;
    //			}

    //			if (numDecimalPlaces < fractionSize)
    //			{
    //				return false;
    //			}

    //			strAmount = strAmount.Remove(pointIndex, 1);
    //		}
    //		else
    //		{
    //			fractionSize = 0;
    //		}

    //		if (string.IsNullOrEmpty(strAmount))
    //		{
    //			return false;
    //		}

    //		if (!std::all_of(strAmount.GetEnumerator(), strAmount.end(), global::isdigit))
    //		{
    //			return false;
    //		}

    //		if (fractionSize < numDecimalPlaces)
    //		{
    //			strAmount.append(numDecimalPlaces - fractionSize, '0');
    //		}

    //		bool success = Common.fromString(strAmount, amount);

    //		if (!success)
    //		{
    //			return false;
    //		}

    //		return amount >= WalletConfig.minimumSend;
    //	}

    //	public static bool confirmTransaction(CryptoNote.TransactionParameters t, WalletInfo walletInfo, bool integratedAddress, uint nodeFee, string originalAddress)
    //	{
    //		Console.Write("\n");
    //		Console.Write(InformationMsg("Confirm Transaction?"));
    //		Console.Write("\n");

    //		Console.Write("You are sending ");
    //		Console.Write(SuccessMsg(formatAmount(t.destinations[0].amount)));
    //		Console.Write(", with a network fee of ");
    //		Console.Write(SuccessMsg(formatAmount(t.fee)));
    //		Console.Write(",");
    //		Console.Write("\n");
    //		Console.Write("and a node fee of ");
    //		Console.Write(SuccessMsg(formatAmount(nodeFee)));

    //		string paymentID = getPaymentIDFromExtra(t.extra);

    //		/* Lets not split the integrated address out into its address and
    //		   payment ID combo. It'll confused users. */
    //		if (paymentID != "" && !integratedAddress)
    //		{
    //			Console.Write(", ");
    //			Console.Write("\n");
    //			Console.Write("and a Payment ID of ");
    //			Console.Write(SuccessMsg(paymentID));
    //		}
    //		else
    //		{
    //			Console.Write(".");
    //		}

    //		Console.Write("\n");
    //		Console.Write("\n");
    //		Console.Write("FROM: ");
    //		Console.Write(SuccessMsg(walletInfo.walletFileName));
    //		Console.Write("\n");
    //		Console.Write("TO: ");
    //		Console.Write(SuccessMsg(originalAddress));
    //		Console.Write("\n");
    //		Console.Write("\n");

    //		if (confirm("Is this correct?"))
    //		{
    //			confirmPassword(walletInfo.walletPass);
    //			return true;
    //		}

    //		return false;
    //	}

    //	/* Note that the originalTXParams, and thus the splitTXParams already has the
    //	   node transfer added */
    //	public static void splitTX(CryptoNote.WalletGreen wallet, CryptoNote.TransactionParameters originalTXParams, uint nodeFee)
    //	{
    //		Console.Write("Transaction is still too large to send, splitting into ");
    //		Console.Write("multiple chunks.");
    //		Console.Write("\n");
    //		Console.Write("It will slightly raise the fee you have to pay,");
    //		Console.Write("\n");
    //		Console.Write("and hence reduce the total amount you can send if");
    //		Console.Write("\n");
    //		Console.Write("your balance cannot cover it.");
    //		Console.Write("\n");

    //		if (!confirm("Is this OK?"))
    //		{
    //			Console.Write(WarningMsg("Cancelling transaction."));
    //			Console.Write("\n");
    //			return;
    //		}

    //		ulong balance = wallet.getActualBalance();

    //		ulong totalAmount = originalTXParams.destinations[0].amount;
    //		ulong sentAmount = 0;
    //		ulong remainder = totalAmount - sentAmount;

    //		/* How much to split the remaining balance to be sent into each individual
    //		   transaction. If it's 1, then we'll attempt to send the full amount,
    //		   if it's 2, we'll send half, and so on. */
    //		ulong amountDivider = 1;

    //		int txNumber = 1;

    //		while (true)
    //		{
    //			var splitTXParams = originalTXParams;

    //			splitTXParams.destinations[0].amount = totalAmount / amountDivider;

    //			/* If we have odd numbers, we can have an amount that is smaller
    //			   than the remainder to send, but the remainder is less than
    //			   2 * amount.
    //			   So, we include this amount in our current transaction to prevent
    //			   this change not being sent.
    //			   If we're trying to send more than the remaining amount, set to
    //			   the remaining amount. */
    //			if ((splitTXParams.destinations[0].amount != remainder && remainder < (splitTXParams.destinations[0].amount * 2)) || (splitTXParams.destinations[0].amount > remainder))
    //			{
    //				splitTXParams.destinations[0].amount = remainder;
    //			}
    //			else if (splitTXParams.destinations[0].amount + splitTXParams.fee + nodeFee > balance)
    //			{
    //				splitTXParams.destinations[0].amount = balance - splitTXParams.fee - nodeFee;
    //			}

    //			if (splitTXParams.destinations[0].amount < WalletConfig.minimumSend)
    //			{
    //				Console.Write(WarningMsg("Failed to split up transaction, sorry."));
    //				Console.Write("\n");

    //				return;
    //			}

    //			ulong totalNeeded = splitTXParams.destinations[0].amount + splitTXParams.fee + nodeFee;

    //			/* Need to update before checking intially */
    //			wallet.updateInternalCache();

    //			/* Balance is going to get locked as we send, wait for it to unlock
    //			   and then send */
    //			while (wallet.getActualBalance() < totalNeeded)
    //			{
    //				Console.Write(WarningMsg("Waiting for balance to unlock to send "));
    //				Console.Write(WarningMsg("next transaction."));
    //				Console.Write("\n");
    //				Console.Write(WarningMsg("Will try again in 15 seconds..."));
    //				Console.Write("\n");
    //				Console.Write("\n");

    //				std::this_thread.sleep_for(std::chrono.seconds(15));

    //				wallet.updateInternalCache();
    //			}

    //			var preparedTransaction = wallet.formTransaction(splitTXParams);

    //			/* Still too large, increase divider and try again */
    //			if (wallet.txIsTooLarge(preparedTransaction))
    //			{
    //				amountDivider++;

    //				/* This can take quite a long time getting mixins each time
    //				   so let them know it's not frozen */
    //				Console.Write(InformationMsg("Working..."));
    //				Console.Write("\n");

    //				continue;
    //			}

    //			Console.Write(InformationMsg("Sending transaction number "));
    //			Console.Write(InformationMsg(Convert.ToString(txNumber)));
    //			Console.Write(InformationMsg("..."));
    //			Console.Write("\n");

    //			uint id = wallet.transfer(preparedTransaction);
    //			var hash = wallet.getTransaction(id).hash;

    //			std::stringstream stream = new std::stringstream();

    //			stream << "Transaction has been sent!" << std::endl << "Hash: " << Common.podToHex(hash) << std::endl << "Amount: " << formatAmount(splitTXParams.destinations[0].amount) << std::endl << std::endl;

    //			Console.Write(SuccessMsg(stream.str()));
    //			Console.Write("\n");

    //			txNumber++;

    //			sentAmount += splitTXParams.destinations[0].amount;

    //			/* Remember to remove the fee and node fee as well from balance */
    //			balance -= splitTXParams.destinations[0].amount - splitTXParams.fee - nodeFee;

    //			remainder = totalAmount - sentAmount;

    //			/* We've sent the full amount required now */
    //			if (sentAmount == totalAmount)
    //			{
    //				Console.Write(InformationMsg("All transactions have been sent!"));
    //				Console.Write("\n");

    //				return;
    //			}

    //			/* Went well, lets restart, trying to send the max amount */
    //			amountDivider = 1;
    //		}
    //	}

    //	public static void transfer(WalletInfo walletInfo, uint height, bool sendAll, string nodeAddress, uint nodeFee)
    //	{
    //		Console.Write(InformationMsg("Note: You can type cancel at any time to " + "cancel the transaction"));
    //		Console.Write("\n");
    //		Console.Write("\n");

    //		ulong balance = walletInfo.wallet.getActualBalance();

    //		ulong balanceNoDust = walletInfo.wallet.getBalanceMinusDust({walletInfo.walletAddress});

    //		var maybeAddress = getAddress("What address do you want to transfer" + " to?: ");

    //		if (!maybeAddress.isJust)
    //		{
    //			Console.Write(WarningMsg("Cancelling transaction."));
    //			Console.Write("\n");
    //			return;
    //		}

    //		/* We keep around the original entered address since we can't get back
    //		   the original integratedAddress from extra, since payment ID's can
    //		   be upper, lower, or mixed case, but they're only stored as lower in
    //		   extra. We want this for the confirmation screen. */
    //		string originalAddress = maybeAddress.x.second;

    //		string address = originalAddress;

    //		string extra;

    //		bool integratedAddress = maybeAddress.x.first == IntegratedAddress;

    //		/* It's an integrated address, so lets extract out the true address and
    //		   payment ID from the pair */
    //		if (integratedAddress)
    //		{
    //			var addrPaymentIDPair = extractIntegratedAddress(maybeAddress.x.second);
    //			address = addrPaymentIDPair.x.first;
    //			extra = getExtraFromPaymentID(addrPaymentIDPair.x.second);
    //		}

    //		/* Don't need to prompt for payment ID if they used an integrated
    //		   address */
    //		if (!integratedAddress)
    //		{
    //			var maybeExtra = getExtra();

    //			if (!maybeExtra.isJust)
    //			{
    //				Console.Write(WarningMsg("Cancelling transaction."));
    //				Console.Write("\n");
    //				return;
    //			}

    //			extra = maybeExtra.x;
    //		}

    //		/* Make sure we set this later if we're sending everything by deducting
    //		   the fee from full balance */
    //		ulong amount = 0;

    //		ulong mixin = CryptoNote.getDefaultMixinByHeight(height);

    //		/* If we're sending everything, obviously we don't need to ask them how
    //		   much to send */
    //		if (!sendAll)
    //		{
    //			var maybeAmount = getTransferAmount();

    //			if (!maybeAmount.isJust)
    //			{
    //				Console.Write(WarningMsg("Cancelling transaction."));
    //				Console.Write("\n");
    //				return;
    //			}

    //			amount = maybeAmount.x;

    //			switch (doWeHaveEnoughBalance(amount, WalletConfig.defaultFee, walletInfo, height, nodeFee))
    //			{
    //				case NotEnoughBalance:
    //				{
    //					Console.Write(WarningMsg("Cancelling transaction."));
    //					Console.Write("\n");
    //					return;
    //				}
    //				case SetMixinToZero:
    //				{
    //					mixin = 0;
    //					break;
    //				}
    //				default:
    //				{
    //					break;
    //				}
    //			}
    //		}

    //		var maybeFee = getFee();

    //		if (!maybeFee.isJust)
    //		{
    //			Console.Write(WarningMsg("Cancelling transaction."));
    //			Console.Write("\n");
    //			return;
    //		}

    //		ulong fee = maybeFee.x;

    //		switch (doWeHaveEnoughBalance(amount, fee, walletInfo, height, nodeFee))
    //		{
    //			case NotEnoughBalance:
    //			{
    //				Console.Write(WarningMsg("Cancelling transaction."));
    //				Console.Write("\n");
    //				return;
    //			}
    //			case SetMixinToZero:
    //			{
    //				mixin = 0;
    //				break;
    //			}
    //			default:
    //			{
    //				break;
    //			}
    //		}

    //		/* This doesn't account for dust. We should probably make a function to
    //		   check for balance minus dust */
    //		if (sendAll)
    //		{
    //			if (CryptoNote.getDefaultMixinByHeight(height) != 0 && balance != balanceNoDust)
    //			{
    //				ulong unsendable = balance - balanceNoDust;

    //				amount = balanceNoDust - fee - nodeFee;

    //				Console.Write(WarningMsg("Due to dust inputs, we are unable to "));
    //				Console.Write(WarningMsg("send "));
    //				Console.Write(InformationMsg(formatAmount(unsendable)));
    //				Console.Write(WarningMsg("of your balance."));
    //				Console.Write("\n");

    //				if (!WalletConfig.mixinZeroDisabled || height < WalletConfig.mixinZeroDisabledHeight)
    //				{
    //					Console.Write("Alternatively, you can set the mixin count to ");
    //					Console.Write("zero to send it all.");
    //					Console.Write("\n");

    //					if (confirm("Set mixin to 0 so we can send your whole balance? " + "This will compromise privacy."))
    //					{
    //						mixin = 0;
    //						amount = balance - fee - nodeFee;
    //					}
    //				}
    //				else
    //				{
    //					Console.Write("Sorry.");
    //					Console.Write("\n");
    //				}
    //			}
    //			else
    //			{
    //				amount = balance - fee - nodeFee;
    //			}
    //		}

    //		doTransfer(address, amount, fee, extra, walletInfo, height, integratedAddress, mixin, nodeAddress, nodeFee, originalAddress);
    //	}

    //	public static BalanceInfo doWeHaveEnoughBalance(ulong amount, ulong fee, WalletInfo walletInfo, ulong height, uint nodeFee)
    //	{
    //		ulong balance = walletInfo.wallet.getActualBalance();

    //		ulong balanceNoDust = walletInfo.wallet.getBalanceMinusDust({walletInfo.walletAddress});

    //		/* They have to include at least a fee of this large */
    //		if (balance < amount + fee + nodeFee)
    //		{
    //			Console.Write("\n");
    //			Console.Write(WarningMsg("You don't have enough funds to cover "));
    //			Console.Write(WarningMsg("this transaction!"));
    //			Console.Write("\n");
    //			Console.Write("\n");
    //			Console.Write("Funds needed: ");
    //			Console.Write(InformationMsg(formatAmount(amount + fee + nodeFee)));
    //			Console.Write(" (Includes a network fee of ");
    //			Console.Write(InformationMsg(formatAmount(fee)));
    //			Console.Write(" and a node fee of ");
    //			Console.Write(InformationMsg(formatAmount(nodeFee)));
    //			Console.Write(")");
    //			Console.Write("\n");
    //			Console.Write("Funds available: ");
    //			Console.Write(SuccessMsg(formatAmount(balance)));
    //			Console.Write("\n");
    //			Console.Write("\n");

    //			return NotEnoughBalance;
    //		}
    //		else if (CryptoNote.getDefaultMixinByHeight(height) != 0 && balanceNoDust < amount + WalletConfig.minimumFee + nodeFee)
    //		{
    //			Console.Write("\n");
    //			Console.Write(WarningMsg("This transaction is unable to be sent "));
    //			Console.Write(WarningMsg("due to dust inputs."));
    //			Console.Write("\n");
    //			Console.Write("You can send ");
    //			Console.Write(InformationMsg(formatAmount(balanceNoDust)));
    //			Console.Write(" without issues (includes a network fee of ");
    //			Console.Write(InformationMsg(formatAmount(fee)));
    //			Console.Write(" and ");
    //			Console.Write(" a node fee of ");
    //			Console.Write(InformationMsg(formatAmount(nodeFee)));
    //			Console.Write(")");
    //			Console.Write("\n");

    //			if (!WalletConfig.mixinZeroDisabled || height < WalletConfig.mixinZeroDisabledHeight)
    //			{
    //				Console.Write("Alternatively, you can sent the mixin ");
    //				Console.Write("count to 0.");
    //				Console.Write("\n");

    //				if (confirm("Set mixin to 0? This will compromise privacy."))
    //				{
    //					return SetMixinToZero;
    //				}
    //			}
    //		}
    //		else
    //		{
    //			return EnoughBalance;
    //		}

    //		return NotEnoughBalance;
    //	}

    //	public static void doTransfer(string address, ulong amount, ulong fee, string extra, WalletInfo walletInfo, uint height, bool integratedAddress, ulong mixin, string nodeAddress, uint nodeFee, string originalAddress)
    //	{
    //		ulong balance = walletInfo.wallet.getActualBalance();

    //		if (balance < amount + fee + nodeFee)
    //		{
    //			Console.Write(WarningMsg("You don't have enough funds to cover this "));
    //			Console.Write(WarningMsg("transaction!"));
    //			Console.Write("\n");
    //			Console.Write(InformationMsg("Funds needed: "));
    //			Console.Write(InformationMsg(formatAmount(amount + fee + nodeFee)));
    //			Console.Write("\n");
    //			Console.Write(SuccessMsg("Funds available: " + formatAmount(balance)));
    //			Console.Write("\n");
    //			return;
    //		}

    //		CryptoNote.TransactionParameters p = new CryptoNote.TransactionParameters();

    //		p.destinations = new List<CryptoNote.WalletOrder> ()
    //		{
    //			{address, amount}
    //		};

    //		if (!string.IsNullOrEmpty(nodeAddress) && nodeFee != 0)
    //		{
    //			p.destinations.Add({nodeAddress, nodeFee});
    //		}

    //		p.fee = fee;
    //		p.mixIn = (ushort)mixin;
    //		p.extra = extra;
    //		p.changeDestination = walletInfo.walletAddress;

    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
    ////ORIGINAL LINE: if (!confirmTransaction(p, walletInfo, integratedAddress, nodeFee, originalAddress))
    //		if (!confirmTransaction(new CryptoNote.TransactionParameters(p), walletInfo, integratedAddress, nodeFee, originalAddress))
    //		{
    //			Console.Write(WarningMsg("Cancelling transaction."));
    //			Console.Write("\n");
    //			return;
    //		}

    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
    ////ORIGINAL LINE: sendTX(walletInfo, p, height, false, nodeFee);
    //		sendTX(walletInfo, new CryptoNote.TransactionParameters(p), height, false, nodeFee);
    //	}

    //	public static void sendTX(WalletInfo walletInfo, CryptoNote.TransactionParameters p, uint height, bool retried, uint nodeFee)
    //	{
    //		try
    //		{
    //			var tx = walletInfo.wallet.formTransaction(p);

    //			/* Transaction is too large. Lets try and perform fusion to let us
    //			   send more at once */
    //			if (walletInfo.wallet.txIsTooLarge(tx))
    //			{
    //				/* If the fusion transactions didn't completely unlock, abort tx */
    //				if (!fusionTX(walletInfo.wallet, p, height))
    //				{
    //					return;
    //				}

    //				/* Reform with the optimized inputs */
    //				tx = walletInfo.wallet.formTransaction(p);

    //				/* If the transaction is still too large, lets split it up into 
    //				   smaller chunks */
    //				if (walletInfo.wallet.txIsTooLarge(tx))
    //				{
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
    ////ORIGINAL LINE: splitTX(walletInfo->wallet, p, nodeFee);
    //					splitTX(walletInfo.wallet, new CryptoNote.TransactionParameters(p), nodeFee);
    //					return;
    //				}
    //			}

    //			uint id = walletInfo.wallet.transfer(tx);
    //			var hash = walletInfo.wallet.getTransaction(id).hash;

    //			Console.Write(SuccessMsg("Transaction has been sent!"));
    //			Console.Write("\n");
    //			Console.Write(SuccessMsg("Hash: "));
    //			Console.Write(SuccessMsg(Common.podToHex(hash)));
    //			Console.Write("\n");
    //		}
    //		/* Lets handle the error and possibly resend the transaction */
    //		catch (std::system_error e)
    //		{
    //			bool setMixinToZero = handleTransferError(e, retried, height);

    //			if (setMixinToZero)
    //			{
    //				p.mixIn = 0;
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
    ////ORIGINAL LINE: sendTX(walletInfo, p, height, true, nodeFee);
    //				sendTX(walletInfo, new CryptoNote.TransactionParameters(p), height, true, nodeFee);
    //			}
    //		}
    //	}

    //	public static bool handleTransferError(std::system_error e, bool retried, uint height)
    //	{
    //		if (retried)
    //		{
    //			Console.Write(WarningMsg("Failed to send transaction!"));
    //			Console.Write("\n");
    //			Console.Write("Error message: ");
    //			Console.Write(e.what());
    //			Console.Write("\n");

    //			return false;
    //		}

    //		bool wrongAmount = false;

    //		switch (e.code().value())
    //		{
    //			case WalletErrors.CryptoNote.error.WRONG_AMOUNT:
    //			{
    //				wrongAmount = true;
    //	//C++ TO C# CONVERTER TODO TASK: C++11 attributes are not converted to C#:
    //	//ORIGINAL LINE: [[fallthrough]];
    //			   ;
    //			}
    ////C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
    //			case WalletErrors.CryptoNote.error.MIXIN_COUNT_TOO_BIG:
    //			case NodeErrors.CryptoNote.error.INTERNAL_NODE_ERROR:
    //			{

    //				if (wrongAmount)
    //				{
    //					Console.Write(WarningMsg("Failed to send transaction " + "- not enough funds!"));
    //					Console.Write("\n");
    //					Console.Write("Unable to send dust inputs.");
    //					Console.Write("\n");
    //				}
    //				else
    //				{
    //					Console.Write(WarningMsg("Failed to send transaction!"));
    //					Console.Write("\n");
    //					Console.Write("Unable to find enough outputs to ");
    //					Console.Write("mix with.");
    //					Console.Write("\n");
    //				}

    //				Console.Write("Try lowering the amount you are sending ");
    //				Console.Write("in one transaction.");
    //				Console.Write("\n");

    //				/* If a mixin of zero is allowed, or we are below the
    //				   fork height when it's banned, ask them to resend with
    //				   zero */
    //				if (!WalletConfig.mixinZeroDisabled || height < WalletConfig.mixinZeroDisabledHeight)
    //				{
    //					Console.Write("Alternatively, you can set the mixin ");
    //					Console.Write("count to 0.");
    //					Console.Write("\n");

    //					if (confirm("Retry transaction with mixin of 0? " + "This will compromise privacy."))
    //					{
    //						return true;
    //					}
    //				}

    //				Console.Write(WarningMsg("Cancelling transaction."));
    //				Console.Write("\n");

    //				break;
    //			}
    //			case NodeErrors.CryptoNote.error.NETWORK_ERROR:
    //			case NodeErrors.CryptoNote.error.CONNECT_ERROR:
    //			{
    //				Console.Write(WarningMsg("Couldn't connect to the network " + "to send the transaction!"));
    //				Console.Write("\n");
    //				Console.Write("Ensure ");
    //				Console.Write(WalletConfig.daemonName);
    //				Console.Write(" or the remote node you are using is open ");
    //				Console.Write("and functioning.");
    //				Console.Write("\n");
    //				break;
    //			}
    //			default:
    //			{
    //				/* Some errors don't have an associated value, just an
    //				   error string */
    //				string msg = e.what();

    //				if (msg == "Failed add key input: key image already spent")
    //				{
    //					Console.Write(WarningMsg("Failed to send transaction - " + "wallet is not synced yet!"));
    //					Console.Write("\n");
    //					Console.Write("Use the ");
    //					Console.Write(InformationMsg("bc_height"));
    //					Console.Write(" command to view the wallet sync status.");
    //					Console.Write("\n");

    //					break;
    //				}

    //				Console.Write(WarningMsg("Failed to send transaction!"));
    //				Console.Write("\n");
    //				Console.Write("Error message: ");
    //				Console.Write(msg);
    //				Console.Write("\n");
    //				Console.Write("Please report what you were doing to cause ");
    //				Console.Write("this error so we can fix it! :)");
    //				Console.Write("\n");
    //				break;
    //			}
    //		}

    //		return false;
    //	}

    //	public static Maybe<string> getPaymentID(string msg)
    //	{
    //		while (true)
    //		{
    //			string paymentID;

    //			Console.Write(InformationMsg(msg));
    //			Console.Write(WarningMsg("Warning: If you were given a payment ID,"));
    //			Console.Write("\n");
    //			Console.Write(WarningMsg("you MUST use it, or your funds may be lost!"));
    //			Console.Write("\n");
    //			Console.Write("Hit enter for the default of no payment ID: ");

    //			paymentID = Console.ReadLine();

    //			if (paymentID == "")
    //			{
    //				return Just<string>(paymentID);
    //			}

    //			if (paymentID == "cancel")
    //			{
    //				return Nothing<string>();
    //			}

    //			List<byte> extra = new List<byte>();

    //			/* Convert the payment ID into an "extra" */
    //			if (!CryptoNote.createTxExtraWithPaymentId(paymentID, extra))
    //			{
    //				Console.Write(WarningMsg("Failed to parse! Payment ID's are 64 " + "character hexadecimal strings."));
    //				Console.Write("\n");
    //				continue;
    //			}

    //			return Just<string>(paymentID);
    //		}
    //	}

    //	public static string getExtraFromPaymentID(string paymentID)
    //	{
    //		if (paymentID == "")
    //		{
    //			return paymentID;
    //		}

    //		List<byte> extra = new List<byte>();

    //		/* Convert the payment ID into an "extra" */
    //		CryptoNote.createTxExtraWithPaymentId(paymentID, extra);

    //		/* Then convert the "extra" back into a string so we can pass
    //		   the argument that walletgreen expects. Note this string is not
    //		   the same as the original paymentID string! */
    //		string extraString;

    //		foreach (var i in extra)
    //		{
    //			extraString += (char)i;
    //		}

    //		return extraString;
    //	}

    //	public static Maybe<string> getExtra()
    //	{
    //		std::stringstream msg = new std::stringstream();

    //		msg << std::endl << "What payment ID do you want to use?" << std::endl << "These are usually used for sending to exchanges." << std::endl;

    //		var maybePaymentID = getPaymentID(msg.str());

    //		if (!maybePaymentID.isJust)
    //		{
    //			return maybePaymentID;
    //		}

    //		if (maybePaymentID.x == "")
    //		{
    //			return maybePaymentID;
    //		}

    //		return Just<string>(getExtraFromPaymentID(maybePaymentID.x));
    //	}

    //	public static Maybe<ulong> getFee()
    //	{
    //		while (true)
    //		{
    //			string stringAmount;
    //			Console.Write("\n");
    //			Console.Write(InformationMsg("What fee do you want to use?"));
    //			Console.Write("\n");
    //			Console.Write("Hit enter for the default fee of ");
    //			Console.Write(formatAmount(WalletConfig.defaultFee));
    //			Console.Write(": ");

    //			stringAmount = Console.ReadLine();

    //			if (stringAmount == "")
    //			{
    //				return Just<ulong>(WalletConfig.defaultFee);
    //			}

    //			if (stringAmount == "cancel")
    //			{
    //				return Nothing<ulong>();
    //			}

    //			ulong amount;

    //			if (parseFee(stringAmount))
    //			{
    //				parseAmount(stringAmount, ref amount);
    //				return Just<ulong>(amount);
    //			}
    //		}
    //	}

    //	public static Maybe<ulong> getTransferAmount()
    //	{
    //		while (true)
    //		{
    //			string stringAmount;

    //			Console.Write("\n");
    //			Console.Write(InformationMsg("How much "));
    //			Console.Write(InformationMsg(WalletConfig.ticker));
    //			Console.Write(InformationMsg(" do you want to send?: "));

    //			stringAmount = Console.ReadLine();

    //			if (stringAmount == "cancel")
    //			{
    //				return Nothing<ulong>();
    //			}

    //			ulong amount;

    //			if (parseAmount(stringAmount))
    //			{
    //				parseAmount(stringAmount, ref amount);
    //				return Just<ulong>(amount);
    //			}
    //		}
    //	}

    //	public static bool parseFee(string feeString)
    //	{
    //		ulong fee;

    //		if (!parseAmount(feeString, ref fee))
    //		{
    //			Console.Write(WarningMsg("Failed to parse fee! Ensure you entered the " + "value correctly."));
    //			Console.Write("\n");
    //			Console.Write("Please note, you can only use ");
    //			Console.Write(WalletConfig.numDecimalPlaces);
    //			Console.Write(" decimal places.");
    //			Console.Write("\n");

    //			return false;
    //		}
    //		else if (fee < WalletConfig.minimumFee)
    //		{
    //			Console.Write(WarningMsg("Fee must be at least "));
    //			Console.Write(formatAmount(WalletConfig.minimumFee));
    //			Console.Write("!");
    //			Console.Write("\n");

    //			return false;
    //		}

    //		return true;
    //	}

    //	public static Maybe<Tuple<string, string>> extractIntegratedAddress(string integratedAddress)
    //	{
    //		if (integratedAddress.Length != WalletConfig.integratedAddressLength)
    //		{
    //			return Nothing<Tuple<string, string>>();
    //		}

    //		string decoded;
    //		ulong prefix;

    //		/* Need to be able to decode the string as an address */
    //		if (!Tools.Base58.decode_addr(integratedAddress, prefix, decoded))
    //		{
    //			return Nothing<Tuple<string, string>>();
    //		}

    //		/* The prefix needs to be the same as the base58 prefix */
    //		if (prefix != CryptoNote.parameters.CRYPTONOTE_PUBLIC_ADDRESS_BASE58_PREFIX)
    //		{
    //			return Nothing<Tuple<string, string>>();
    //		}

    //		const ulong paymentIDLen = 64;

    //		/* Grab the payment ID from the decoded address */
    //		string paymentID = decoded.Substring(0, paymentIDLen);

    //		/* The binary array encoded keys are the rest of the address */
    //		string keys = decoded.Substring(paymentIDLen, -1);

    //		CryptoNote.AccountPublicAddress addr = new CryptoNote.AccountPublicAddress();
    //		List<byte> ba = Common.asBinaryArray(keys);

    //		if (!CryptoNote.fromBinaryArray(addr, ba))
    //		{
    //			return Nothing<Tuple<string, string>>();
    //		}

    //		/* Parse the AccountPublicAddress into a standard wallet address */
    //		/* Use the calculated prefix from earlier for less typing :p */
    //		string address = CryptoNote.getAccountAddressAsStr(prefix, addr);

    //		/* The address out should of course be a valid address */
    //		if (!parseStandardAddress(address))
    //		{
    //			return Nothing<Tuple<string, string>>();
    //		}

    //		List<byte> extra = new List<byte>();

    //		/* And the payment ID out should be valid as well! */
    //		if (!CryptoNote.createTxExtraWithPaymentId(paymentID, extra))
    //		{
    //			return Nothing<Tuple<string, string>>();
    //		}

    //		return Just<Tuple<string, string>>({address, paymentID});
    //	}

    //	public static Maybe<Tuple<AddressType, string>> getAddress(string msg)
    //	{
    //		while (true)
    //		{
    //			string address;

    //			Console.Write(InformationMsg(msg));

    //			address = Console.ReadLine();
    //			trim(address);

    //			if (address == "cancel")
    //			{
    //				return Nothing<Tuple<AddressType, string>>();
    //			}

    //			var addressType = parseAddress(address);

    //			if (addressType != NotAnAddress)
    //			{
    //				return Just<Tuple<AddressType, string>> ({addressType, address});
    //			}
    //		}
    //	}

    //	public static AddressType parseAddress(string address)
    //	{
    //		if (parseStandardAddress(address))
    //		{
    //			return StandardAddress;
    //		}

    //		if (parseIntegratedAddress(address))
    //		{
    //			return IntegratedAddress;
    //		}

    //		/* Failed to parse, lets try and diagnose a more accurate failure message */

    //		if (address.Length != WalletConfig.standardAddressLength && address.Length != WalletConfig.integratedAddressLength)
    //		{
    //			Console.Write(WarningMsg("Address is wrong length!"));
    //			Console.Write("\n");
    //			Console.Write("It should be ");
    //			Console.Write(WalletConfig.standardAddressLength);
    //			Console.Write(" or ");
    //			Console.Write(WalletConfig.integratedAddressLength);
    //			Console.Write(" characters long, but it is ");
    //			Console.Write(address.Length);
    //			Console.Write(" characters long!");
    //			Console.Write("\n");
    //			Console.Write("\n");

    //			return NotAnAddress;
    //		}

    //		if (address.Substring(0, WalletConfig.addressPrefix.length()) != WalletConfig.addressPrefix)
    //		{
    //			Console.Write(WarningMsg("Invalid address! It should start with "));
    //			Console.Write(WarningMsg(WalletConfig.addressPrefix));
    //			Console.Write(WarningMsg("!"));
    //			Console.Write("\n");
    //			Console.Write("\n");

    //			return NotAnAddress;
    //		}

    //		Console.Write(WarningMsg("Failed to parse address, address is not a "));
    //		Console.Write(WarningMsg("valid "));
    //		Console.Write(WarningMsg(WalletConfig.ticker));
    //		Console.Write(WarningMsg(" address!"));
    //		Console.Write("\n");
    //		Console.Write("\n");

    //		return NotAnAddress;
    //	}

    //	public static bool parseIntegratedAddress(string integratedAddress)
    //	{
    //		return extractIntegratedAddress(integratedAddress).isJust;
    //	}

    //	public static bool parseStandardAddress(string address, bool printErrors)
    //	{
    //		ulong prefix;

    //		CryptoNote.AccountPublicAddress addr = new CryptoNote.AccountPublicAddress();

    //		bool valid = CryptoNote.parseAccountAddressString(prefix, addr, address);

    //		if (address.Length != WalletConfig.standardAddressLength)
    //		{
    //			if (printErrors)
    //			{
    //				Console.Write(WarningMsg("Address is wrong length!"));
    //				Console.Write("\n");
    //				Console.Write("It should be ");
    //				Console.Write(WalletConfig.standardAddressLength);
    //				Console.Write(" characters long, but it is ");
    //				Console.Write(address.Length);
    //				Console.Write(" characters long!");
    //				Console.Write("\n");
    //				Console.Write("\n");
    //			}

    //			return false;
    //		}
    //		/* We can't get the actual prefix if the address is invalid for other
    //		   reasons. To work around this, we can just check that the address starts
    //		   with TRTL, as long as the prefix is the TRTL prefix. This keeps it
    //		   working on testnets with different prefixes. */
    //		else if (address.Substring(0, WalletConfig.addressPrefix.length()) != WalletConfig.addressPrefix)
    //		{
    //			if (printErrors)
    //			{
    //				Console.Write(WarningMsg("Invalid address! It should start with "));
    //				Console.Write(WarningMsg(WalletConfig.addressPrefix));
    //				Console.Write(WarningMsg("!"));
    //				Console.Write("\n");
    //				Console.Write("\n");
    //			}

    //			return false;
    //		}
    //		/* We can return earlier by checking the value of valid, but then we don't
    //		   get to give more detailed error messages about the address */
    //		else if (!valid)
    //		{
    //			if (printErrors)
    //			{
    //				Console.Write(WarningMsg("Failed to parse address, address is not a "));
    //				Console.Write(WarningMsg("valid "));
    //				Console.Write(WarningMsg(WalletConfig.ticker));
    //				Console.Write(WarningMsg(" address!"));
    //				Console.Write("\n");
    //				Console.Write("\n");
    //			}

    //			return false;
    //		}

    //		return true;
    //	}

    //	public static bool parseAmount(string amountString)
    //	{
    //		ulong amount;

    //		if (!parseAmount(amountString, ref amount))
    //		{
    //			Console.Write(WarningMsg("Failed to parse amount! Ensure you entered " + "the value correctly."));
    //			Console.Write("\n");
    //			Console.Write("Please note, the minimum you can send is ");
    //			Console.Write(formatAmount(WalletConfig.minimumSend));
    //			Console.Write(",");
    //			Console.Write("\n");
    //			Console.Write("and you can only use ");
    //			Console.Write(WalletConfig.numDecimalPlaces);
    //			Console.Write(" decimal places.");
    //			Console.Write("\n");

    //			return false;
    //		}

    //		return true;
    //	}



    //	static void Main(int argc, string[] args)
    //	{
    //		/* On ctrl+c the program seems to throw "zedwallet.exe has stopped
    //		   working" when calling exit(0)... I'm not sure why, this is a bit of
    //		   a hack, it disables that - possibly some deconstructers calling
    //		   terminate() */
    //		#if _WIN32
    //		SetErrorMode(SEM_FAILCRITICALERRORS | SEM_NOGPFAULTERRORBOX);
    //		#endif

    //		Config config = parseArguments(argc, args);

    //		Console.Write(InformationMsg(CryptoNote.getProjectCLIHeader()));
    //		Console.Write("\n");

    //		Logging.LoggerManager logManager = new Logging.LoggerManager();

    //		/* We'd like these lines to be in the below if(), but because some genius
    //		   thought it was a good idea to pass everything by reference and then
    //		   use them after the functions lifetime they go out of scope and break
    //		   stuff */
    //		logManager.setMaxLevel(Logging.DEBUGGING);

    //		Logging.FileLogger fileLogger = new Logging.FileLogger();

    //		if (config.debug)
    //		{
    //			fileLogger.init(WalletConfig.walletName + ".log");
    //			logManager.addLogger(fileLogger);
    //		}

    //		Logging.LoggerRef logger = new Logging.LoggerRef(logManager, WalletConfig.walletName);

    //		/* Currency contains our coin parameters, such as decimal places, supply */
    //		CryptoNote.Currency currency = CryptoNote.CurrencyBuilder(logManager).currency();

    //		System.Dispatcher localDispatcher = new System.Dispatcher();
    //		System.Dispatcher dispatcher = localDispatcher;

    //		/* Our connection to turtlecoind */
    //		std::unique_ptr<CryptoNote.INode> node = new std::unique_ptr<CryptoNote.INode>(new CryptoNote.NodeRpcProxy(config.host, config.port, logger.getLogger()));

    //		std::promise<std::error_code> errorPromise = new std::promise<std::error_code>();

    //		/* Once the function is complete, set the error value from the promise */
    ////C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
    //	var callback = (std::error_code e) =>
    //	{
    //		errorPromise.set_value(e);
    //	};

    //		/* Get the future of the result */
    //		var initNode = errorPromise.get_future();

    //		node.init(callback);

    //		/* Connection took to long to remote node, let program continue regardless
    //		   as they could perform functions like export_keys without being
    //		   connected */
    //		if (initNode.wait_for(std::chrono.seconds(20)) != std::future_status.ready)
    //		{
    //			if (config.host != "127.0.0.1")
    //			{
    //				Console.Write(WarningMsg("Unable to connect to remote node, " + "connection timed out."));
    //				Console.Write("\n");
    //				Console.Write(WarningMsg("Confirm the remote node is functioning, " + "or try a different remote node."));
    //				Console.Write("\n");
    //				Console.Write("\n");
    //			}
    //			else
    //			{
    //				Console.Write(WarningMsg("Unable to connect to node, " + "connection timed out."));
    //				Console.Write("\n");
    //				Console.Write("\n");
    //			}
    //		}

    //		/*
    //		  This will check to see if the node responded to /feeinfo and actually
    //		  returned something that it expects us to use for convenience charges
    //		  for using that node to send transactions.
    //		*/
    //		if (node.feeAmount() != 0 && !node.feeAddress().empty())
    //		{
    //		  std::stringstream feemsg = new std::stringstream();

    //		  feemsg << std::endl << "You have connected to a node that charges " << "a fee to send transactions." << std::endl << std::endl << "The fee for sending transactions is: " << formatAmount(node.feeAmount()) << " per transaction." << std::endl << std::endl << "If you don't want to pay the node fee, please " << "relaunch " << WalletConfig.walletName << " and specify a different node or run your own." << std::endl;

    //		  Console.Write(WarningMsg(feemsg.str()));
    //		  Console.Write("\n");
    //		}

    //		/* Create the wallet instance */
    //		CryptoNote.WalletGreen wallet = new CryptoNote.WalletGreen(dispatcher, currency, *node, logger.getLogger());

    //		/* Run the interactive wallet interface */
    //		run(wallet, *node, config);
    //	}

    //	public static void run(CryptoNote.WalletGreen wallet, CryptoNote.INode node, Config config)
    //	{
    //		var (quit, walletInfo) = selectionScreen(config, wallet, node);

    //		bool alreadyShuttingDown = false;

    //		if (!quit)
    //		{
    //			/* Call shutdown on ctrl+c */
    //			/* walletInfo = walletInfo - workaround for
    //			   https://stackoverflow.com/a/46115028/8737306 - standard &
    //			   capture works in newer compilers. */
    //		Tools.SignalHandler.install(() =>
    //		{
    //			/* If we're already shutting down let control flow continue
    //			   as normal */
    //			if (shutdown(walletInfo, node, alreadyShuttingDown))
    //			{
    //				Environment.Exit(0);
    //			}
    //		});

    //			mainLoop(walletInfo, node);
    //		}

    //		shutdown(walletInfo, node, alreadyShuttingDown);
    //	}

    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	//
    //	// Please see the included LICENSE file for more information


    //	public static readonly string windowsAsciiArt = "\n _______         _   _       _____      _        \n" + "|__   __|       | | | |     / ____|    (_)      \n" + "   | |_   _ _ __| |_| | ___| |     ___  _ _ __  \n" + "   | | | | | '__| __| |/ _ \\ |    / _ \\| | '_ \\ \n" + "   | | |_| | |  | |_| |  __/ |___| (_) | | | | |\n" + "   |_|\\__ _|_|   \\__|_|\\___|\\_____\\___/|_|_| |_|\n";

    //	public static readonly string nonWindowsAsciiArt = "\n                                                                            \n" + "████████╗██╗  ██╗██████╗ ████████╗██╗    ██████╗ █████╗ █████╗ ██╗███╗   ██╗\n" + "╚══██╔══╝██║  ██║██╔══██╗╚══██╔══╝██║    ██╔═══╝██╔═══╝██╔══██╗██║████╗  ██║\n" + "   ██║   ██║  ██║██████╔╝   ██║   ██║    ████╗  ██║    ██║  ██║██║██╔██╗ ██║\n" + "   ██║   ██║  ██║██╔══██╗   ██║   ██║    ██╔═╝  ██║    ██║  ██║██║██║╚██╗██║\n" + "   ██║   ╚█████╔╝██║  ██║   ██║   ██████╗██████╗╚█████╗╚█████╔╝██║██║ ╚████║\n" + "   ╚═╝    ╚════╝ ╚═╝  ╚═╝   ╚═╝   ╚═════╝╚═════╝ ╚════╝ ╚════╝ ╚═╝╚═╝  ╚═══╝\n";

    //	/* Windows has some characters it won't display in a terminal. If your ascii
    //	   art works fine on Windows and Linux terminals, just replace 'asciiArt' with
    //	   the art itself, and remove these two #ifdefs and above ascii arts */
    //	#if _WIN32
    //	public static readonly string asciiArt = windowsAsciiArt;
    //	#else
    //	public static readonly string asciiArt = nonWindowsAsciiArt;
    //	#endif

    //	public static readonly uint[] T = {0xa5f432c6, 0xc6a597f4, 0x84976ff8, 0xf884eb97, 0x99b05eee, 0xee99c7b0, 0x8d8c7af6, 0xf68df78c, 0xd17e8ff, 0xff0de517, 0xbddc0ad6, 0xd6bdb7dc, 0xb1c816de, 0xdeb1a7c8, 0x54fc6d91, 0x915439fc, 0x50f09060, 0x6050c0f0, 0x3050702, 0x2030405, 0xa9e02ece, 0xcea987e0, 0x7d87d156, 0x567dac87, 0x192bcce7, 0xe719d52b, 0x62a613b5, 0xb56271a6, 0xe6317c4d, 0x4de69a31, 0x9ab559ec, 0xec9ac3b5, 0x45cf408f, 0x8f4505cf, 0x9dbca31f, 0x1f9d3ebc, 0x40c04989, 0x894009c0, 0x879268fa, 0xfa87ef92, 0x153fd0ef, 0xef15c53f, 0xeb2694b2, 0xb2eb7f26, 0xc940ce8e, 0x8ec90740, 0xb1de6fb, 0xfb0bed1d, 0xec2f6e41, 0x41ec822f, 0x67a91ab3, 0xb3677da9, 0xfd1c435f, 0x5ffdbe1c, 0xea256045, 0x45ea8a25, 0xbfdaf923, 0x23bf46da, 0xf7025153, 0x53f7a602, 0x96a145e4, 0xe496d3a1, 0x5bed769b, 0x9b5b2ded, 0xc25d2875, 0x75c2ea5d, 0x1c24c5e1, 0xe11cd924, 0xaee9d43d, 0x3dae7ae9, 0x6abef24c, 0x4c6a98be, 0x5aee826c, 0x6c5ad8ee, 0x41c3bd7e, 0x7e41fcc3, 0x206f3f5, 0xf502f106, 0x4fd15283, 0x834f1dd1, 0x5ce48c68, 0x685cd0e4, 0xf4075651, 0x51f4a207, 0x345c8dd1, 0xd134b95c, 0x818e1f9, 0xf908e918, 0x93ae4ce2, 0xe293dfae, 0x73953eab, 0xab734d95, 0x53f59762, 0x6253c4f5, 0x3f416b2a, 0x2a3f5441, 0xc141c08, 0x80c1014, 0x52f66395, 0x955231f6, 0x65afe946, 0x46658caf, 0x5ee27f9d, 0x9d5e21e2, 0x28784830, 0x30286078, 0xa1f8cf37, 0x37a16ef8, 0xf111b0a, 0xa0f1411, 0xb5c4eb2f, 0x2fb55ec4, 0x91b150e, 0xe091c1b, 0x365a7e24, 0x2436485a, 0x9bb6ad1b, 0x1b9b36b6, 0x3d4798df, 0xdf3da547, 0x266aa7cd, 0xcd26816a, 0x69bbf54e, 0x4e699cbb, 0xcd4c337f, 0x7fcdfe4c, 0x9fba50ea, 0xea9fcfba, 0x1b2d3f12, 0x121b242d, 0x9eb9a41d, 0x1d9e3ab9, 0x749cc458, 0x5874b09c, 0x2e724634, 0x342e6872, 0x2d774136, 0x362d6c77, 0xb2cd11dc, 0xdcb2a3cd, 0xee299db4, 0xb4ee7329, 0xfb164d5b, 0x5bfbb616, 0xf601a5a4, 0xa4f65301, 0x4dd7a176, 0x764decd7, 0x61a314b7, 0xb76175a3, 0xce49347d, 0x7dcefa49, 0x7b8ddf52, 0x527ba48d, 0x3e429fdd, 0xdd3ea142, 0x7193cd5e, 0x5e71bc93, 0x97a2b113, 0x139726a2, 0xf504a2a6, 0xa6f55704, 0x68b801b9, 0xb96869b8, 0x0, 0x0, 0x2c74b5c1, 0xc12c9974, 0x60a0e040, 0x406080a0, 0x1f21c2e3, 0xe31fdd21, 0xc8433a79, 0x79c8f243, 0xed2c9ab6, 0xb6ed772c, 0xbed90dd4, 0xd4beb3d9, 0x46ca478d, 0x8d4601ca, 0xd9701767, 0x67d9ce70, 0x4bddaf72, 0x724be4dd, 0xde79ed94, 0x94de3379, 0xd467ff98, 0x98d42b67, 0xe82393b0, 0xb0e87b23, 0x4ade5b85, 0x854a11de, 0x6bbd06bb, 0xbb6b6dbd, 0x2a7ebbc5, 0xc52a917e, 0xe5347b4f, 0x4fe59e34, 0x163ad7ed, 0xed16c13a, 0xc554d286, 0x86c51754, 0xd762f89a, 0x9ad72f62, 0x55ff9966, 0x6655ccff, 0x94a7b611, 0x119422a7, 0xcf4ac08a, 0x8acf0f4a, 0x1030d9e9, 0xe910c930, 0x60a0e04, 0x406080a, 0x819866fe, 0xfe81e798, 0xf00baba0, 0xa0f05b0b, 0x44ccb478, 0x7844f0cc, 0xbad5f025, 0x25ba4ad5, 0xe33e754b, 0x4be3963e, 0xf30eaca2, 0xa2f35f0e, 0xfe19445d, 0x5dfeba19, 0xc05bdb80, 0x80c01b5b, 0x8a858005, 0x58a0a85, 0xadecd33f, 0x3fad7eec, 0xbcdffe21, 0x21bc42df, 0x48d8a870, 0x7048e0d8, 0x40cfdf1, 0xf104f90c, 0xdf7a1963, 0x63dfc67a, 0xc1582f77, 0x77c1ee58, 0x759f30af, 0xaf75459f, 0x63a5e742, 0x426384a5, 0x30507020, 0x20304050, 0x1a2ecbe5, 0xe51ad12e, 0xe12effd, 0xfd0ee112, 0x6db708bf, 0xbf6d65b7, 0x4cd45581, 0x814c19d4, 0x143c2418, 0x1814303c, 0x355f7926, 0x26354c5f, 0x2f71b2c3, 0xc32f9d71, 0xe13886be, 0xbee16738, 0xa2fdc835, 0x35a26afd, 0xcc4fc788, 0x88cc0b4f, 0x394b652e, 0x2e395c4b, 0x57f96a93, 0x93573df9, 0xf20d5855, 0x55f2aa0d, 0x829d61fc, 0xfc82e39d, 0x47c9b37a, 0x7a47f4c9, 0xacef27c8, 0xc8ac8bef, 0xe73288ba, 0xbae76f32, 0x2b7d4f32, 0x322b647d, 0x95a442e6, 0xe695d7a4, 0xa0fb3bc0, 0xc0a09bfb, 0x98b3aa19, 0x199832b3, 0xd168f69e, 0x9ed12768, 0x7f8122a3, 0xa37f5d81, 0x66aaee44, 0x446688aa, 0x7e82d654, 0x547ea882, 0xabe6dd3b, 0x3bab76e6, 0x839e950b, 0xb83169e, 0xca45c98c, 0x8cca0345, 0x297bbcc7, 0xc729957b, 0xd36e056b, 0x6bd3d66e, 0x3c446c28, 0x283c5044, 0x798b2ca7, 0xa779558b, 0xe23d81bc, 0xbce2633d, 0x1d273116, 0x161d2c27, 0x769a37ad, 0xad76419a, 0x3b4d96db, 0xdb3bad4d, 0x56fa9e64, 0x6456c8fa, 0x4ed2a674, 0x744ee8d2, 0x1e223614, 0x141e2822, 0xdb76e492, 0x92db3f76, 0xa1e120c, 0xc0a181e, 0x6cb4fc48, 0x486c90b4, 0xe4378fb8, 0xb8e46b37, 0x5de7789f, 0x9f5d25e7, 0x6eb20fbd, 0xbd6e61b2, 0xef2a6943, 0x43ef862a, 0xa6f135c4, 0xc4a693f1, 0xa8e3da39, 0x39a872e3, 0xa4f7c631, 0x31a462f7, 0x37598ad3, 0xd337bd59, 0x8b8674f2, 0xf28bff86, 0x325683d5, 0xd532b156, 0x43c54e8b, 0x8b430dc5, 0x59eb856e, 0x6e59dceb, 0xb7c218da, 0xdab7afc2, 0x8c8f8e01, 0x18c028f, 0x64ac1db1, 0xb16479ac, 0xd26df19c, 0x9cd2236d, 0xe03b7249, 0x49e0923b, 0xb4c71fd8, 0xd8b4abc7, 0xfa15b9ac, 0xacfa4315, 0x709faf3, 0xf307fd09, 0x256fa0cf, 0xcf25856f, 0xafea20ca, 0xcaaf8fea, 0x8e897df4, 0xf48ef389, 0xe9206747, 0x47e98e20, 0x18283810, 0x10182028, 0xd5640b6f, 0x6fd5de64, 0x888373f0, 0xf088fb83, 0x6fb1fb4a, 0x4a6f94b1, 0x7296ca5c, 0x5c72b896, 0x246c5438, 0x3824706c, 0xf1085f57, 0x57f1ae08, 0xc7522173, 0x73c7e652, 0x51f36497, 0x975135f3, 0x2365aecb, 0xcb238d65, 0x7c8425a1, 0xa17c5984, 0x9cbf57e8, 0xe89ccbbf, 0x21635d3e, 0x3e217c63, 0xdd7cea96, 0x96dd377c, 0xdc7f1e61, 0x61dcc27f, 0x86919c0d, 0xd861a91, 0x85949b0f, 0xf851e94, 0x90ab4be0, 0xe090dbab, 0x42c6ba7c, 0x7c42f8c6, 0xc4572671, 0x71c4e257, 0xaae529cc, 0xccaa83e5, 0xd873e390, 0x90d83b73, 0x50f0906, 0x6050c0f, 0x103f4f7, 0xf701f503, 0x12362a1c, 0x1c123836, 0xa3fe3cc2, 0xc2a39ffe, 0x5fe18b6a, 0x6a5fd4e1, 0xf910beae, 0xaef94710, 0xd06b0269, 0x69d0d26b, 0x91a8bf17, 0x17912ea8, 0x58e87199, 0x995829e8, 0x2769533a, 0x3a277469, 0xb9d0f727, 0x27b94ed0, 0x384891d9, 0xd938a948, 0x1335deeb, 0xeb13cd35, 0xb3cee52b, 0x2bb356ce, 0x33557722, 0x22334455, 0xbbd604d2, 0xd2bbbfd6, 0x709039a9, 0xa9704990, 0x89808707, 0x7890e80, 0xa7f2c133, 0x33a766f2, 0xb6c1ec2d, 0x2db65ac1, 0x22665a3c, 0x3c227866, 0x92adb815, 0x15922aad, 0x2060a9c9, 0xc9208960, 0x49db5c87, 0x874915db, 0xff1ab0aa, 0xaaff4f1a, 0x7888d850, 0x5078a088, 0x7a8e2ba5, 0xa57a518e, 0x8f8a8903, 0x38f068a, 0xf8134a59, 0x59f8b213, 0x809b9209, 0x980129b, 0x1739231a, 0x1a173439, 0xda751065, 0x65daca75, 0x315384d7, 0xd731b553, 0xc651d584, 0x84c61351, 0xb8d303d0, 0xd0b8bbd3, 0xc35edc82, 0x82c31f5e, 0xb0cbe229, 0x29b052cb, 0x7799c35a, 0x5a77b499, 0x11332d1e, 0x1e113c33, 0xcb463d7b, 0x7bcbf646, 0xfc1fb7a8, 0xa8fc4b1f, 0xd6610c6d, 0x6dd6da61, 0x3a4e622c, 0x2c3a584e};



    //	public static bool operator < (NetworkAddress a, NetworkAddress b)
    //	{
    //		return std::tie(a.ip, a.port) < std::tie(b.ip, b.port);
    //	}

    //	public static bool operator == (NetworkAddress a, NetworkAddress b)
    //	{
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
    //		return memcmp(a, b, sizeof(NetworkAddress)) == 0;
    //	}

    //	public static std::ostream operator << (std::ostream s, NetworkAddress na)
    //	{
    //		return s << Common.ipAddressToString(na.ip) << ":" << Convert.ToString(na.port);
    //	}

    //	public static uint hostToNetwork(uint n)
    //	{
    //		return (n << 24) | (n & 0xff00) << 8 | (n & 0xff0000) >> 8 | (n >> 24);
    //	}

    //	public static uint networkToHost(uint n)
    //	{
    //		return hostToNetwork(n); // the same
    //	}
    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	// 
    //	// Please see the included LICENSE file for more information.


    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void addToAddressBook();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void sendFromAddressBook(WalletInfo walletInfo, uint height, string nodeAddress, uint nodeFee);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void deleteFromAddressBook();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void listAddressBook();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//Maybe<string> getAddressBookPaymentID();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//Maybe< string> getAddressBookAddress();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//Maybe< AddressBookEntry> getAddressBookEntry(AddressBook addressBook);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//string getAddressBookName(AddressBook addressBook);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//AddressBook getAddressBook();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool saveAddressBook(AddressBook addressBook);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool isAddressBookEmpty(AddressBook addressBook);

    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	// 
    //	// Please see the included LICENSE file for more information.



    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool handleCommand(string command, WalletInfo walletInfo, CryptoNote::INode node);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//WalletInfo handleLaunchCommand(CryptoNote::WalletGreen wallet, string launchCommand, Config config);

    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	// 
    //	// Please see the included LICENSE file for more information.




    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool handleCommand(string command, WalletInfo walletInfo, CryptoNote::INode node);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void changePassword(WalletInfo walletInfo);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void printPrivateKeys(CryptoNote::WalletGreen wallet, bool viewWallet);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void reset(CryptoNote::INode node, WalletInfo walletInfo);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void status(CryptoNote::INode node, CryptoNote::WalletGreen wallet);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void printHeights(uint localHeight, uint remoteHeight, uint walletHeight);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void printSyncStatus(uint localHeight, uint remoteHeight, uint walletHeight);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void printSyncSummary(uint localHeight, uint remoteHeight, uint walletHeight);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void printPeerCount(uint peerCount);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void printHashrate(ulong difficulty);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void balance(CryptoNote::INode node, CryptoNote::WalletGreen wallet, bool viewWallet);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void exportKeys(WalletInfo walletInfo);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void saveCSV(CryptoNote::WalletGreen wallet, CryptoNote::INode node);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void save(CryptoNote::WalletGreen wallet);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void listTransfers(bool incoming, bool outgoing, CryptoNote::WalletGreen wallet, CryptoNote::INode node);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void printOutgoingTransfer(CryptoNote::WalletTransaction t, CryptoNote::INode node);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void printIncomingTransfer(CryptoNote::WalletTransaction t, CryptoNote::INode node);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void createIntegratedAddress();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void help(WalletInfo wallet);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void advanced(WalletInfo wallet);


    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//ClassicVector<Command> startupCommands();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//ClassicVector<Command> nodeDownCommands();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//ClassicVector<AdvancedCommand> allCommands();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//ClassicVector<AdvancedCommand> basicCommands();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//ClassicVector<AdvancedCommand> advancedCommands();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//ClassicVector<AdvancedCommand> basicViewWalletCommands();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//ClassicVector<AdvancedCommand> advancedViewWalletCommands();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//ClassicVector<AdvancedCommand> allViewWalletCommands();

    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	// 
    //	// Please see the included LICENSE file for more information.



    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool fusionTX(CryptoNote::WalletGreen wallet, CryptoNote::TransactionParameters p, ulong height);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool optimize(CryptoNote::WalletGreen wallet, ulong threshold, ulong height);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void fullOptimize(CryptoNote::WalletGreen wallet, ulong height);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//uint makeFusionTransaction(CryptoNote::WalletGreen wallet, ulong threshold, ulong height);

    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	// 
    //	// Please see the included LICENSE file for more information.





    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//string yellowANSIMsg(string msg);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//string getPrompt(WalletInfo walletInfo);

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//string getInputAndWorkInBackground<T>(ClassicVector<T> availableCommands, string prompt, bool backgroundRefresh, WalletInfo walletInfo);

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//string getInput<T>(ClassicVector<T> availableCommands, string prompt);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//string parseCommand<T>(ClassicVector<T> printableCommands, ClassicVector<T> availableCommands, string prompt, bool backgroundRefresh, WalletInfo walletInfo);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//System.Tuple<bool, WalletInfo> selectionScreen<T>(Config config, CryptoNote::WalletGreen wallet, CryptoNote::INode node);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool checkNodeStatus<T>(CryptoNote::INode node);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//string getAction<T>(Config config);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void mainLoop<T>(WalletInfo walletInfo, CryptoNote::INode node);

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void printCommands<T>(ClassicVector<T> commands, uint offset = 0);

    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	// 
    //	// Please see the included LICENSE file for more information.



    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//WalletInfo importFromKeys(CryptoNote::WalletGreen wallet, Crypto::SecretKey privateSpendKey, Crypto::SecretKey privateViewKey);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//WalletInfo openWallet(CryptoNote::WalletGreen wallet, Config config);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//WalletInfo createViewWallet(CryptoNote::WalletGreen wallet);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//WalletInfo importWallet(CryptoNote::WalletGreen wallet);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//WalletInfo createViewWallet(CryptoNote::WalletGreen wallet);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//WalletInfo mnemonicImportWallet(CryptoNote::WalletGreen wallet);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//WalletInfo generateWallet(CryptoNote::WalletGreen wallet);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//Crypto::SecretKey getPrivateKey(string outputMsg);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//string getNewWalletFileName();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//string getExistingWalletFileName(Config config);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//string getWalletPassword(bool verifyPwd, string msg);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool isValidMnemonic(string mnemonic_phrase, Crypto::SecretKey private_spend_key);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void logIncorrectMnemonicWords(ClassicVector<string> words);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void viewWalletMsg();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void connectingMsg();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void promptSaveKeys(CryptoNote::WalletGreen wallet);

    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	//
    //	// Please see the included LICENSE file for more information.



    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//Config parseArguments(int argc, string[] argv);

    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	// 
    //	// Please see the included LICENSE file for more information.



    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void syncWallet(CryptoNote::INode node, WalletInfo walletInfo);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void checkForNewTransactions(WalletInfo walletInfo);

    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	// 
    //	// Please see the included LICENSE file for more information.








    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void confirmPassword(string walletPass, string msg = "");

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void removeCharFromString(string str, char c);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void trim(string str);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void leftTrim(string str);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void rightTrim(string str);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool confirm(string msg);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool confirm(string msg, bool defaultReturn);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool startsWith(string str, string substring);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool fileExists(string filename);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool shutdown(WalletInfo walletInfo, CryptoNote::INode node, ref bool alreadyShuttingDown);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//string formatAmountBasic(ulong amount);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//string formatAmount(ulong amount);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//string formatDollars(ulong amount);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//string formatCents(ulong amount);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//string getPaymentIDFromExtra(string extra);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//string unixTimeToDate(ulong timestamp);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//string createIntegratedAddress(string address, string paymentID);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//ulong getDivisor();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//ulong getScanHeight();

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T, typename Function>
    //	public static List<T> filter<T, Function>(List<T> input, Function predicate)
    //	{
    //		List<T> result = new List<T>();

    //		std::copy_if(input.GetEnumerator(), input.end(), std::back_inserter(result), predicate);

    //		return result;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//ClassicVector<string> split(string str, char delim);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool parseDaemonAddressFromString(string host, ref int port, string address);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void transfer(WalletInfo walletInfo, uint height, bool sendAll = false, string nodeAddress = string(), uint nodeFee = 0);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void doTransfer(string address, ulong amount, ulong fee, string extra, WalletInfo walletInfo, uint height, bool integratedAddress, ulong mixin, string nodeAddress, uint nodeFee, string originalAddress);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void splitTX(CryptoNote::WalletGreen wallet, CryptoNote::TransactionParameters splitTXParams, uint nodeFee);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void sendTX(WalletInfo walletInfo, CryptoNote::TransactionParameters p, uint height, bool retried = false, uint nodeFee = 0);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool confirmTransaction(CryptoNote::TransactionParameters t, WalletInfo walletInfo, bool integratedAddress, uint nodeFee, string originalAddress);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool parseAmount(string amountString);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool parseStandardAddress(string address, bool printErrors = false);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool parseIntegratedAddress(string address);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool parseFee(string feeString);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//bool handleTransferError(std::system_error e, bool retried, uint height);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//AddressType parseAddress(string address);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//string getExtraFromPaymentID(string paymentID);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//Maybe<string> getPaymentID(string msg);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//Maybe<string> getExtra();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//Maybe<System.Tuple<AddressType, string>> getAddress(string msg);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//Maybe<ulong> getFee();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//Maybe<ulong> getTransferAmount();

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//Maybe<System.Tuple<string, string>> extractIntegratedAddress(string integratedAddress);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//BalanceInfo doWeHaveEnoughBalance(ulong amount, ulong fee, WalletInfo walletInfo, ulong height, uint nodeFee);

    //	public static Maybe<X> Just<X>(X x)
    //	{
    //		return new Maybe<X>(x);
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <class X>
    //	public static Maybe<X> Nothing<X>()
    //	{
    //		return new Maybe<X>();
    //	}

    //	// Copyright (c) 2018, The TurtleCoin Developers
    //	// 
    //	// Please see the included LICENSE file for more information.




    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//static int Main(int argc, string[] argv);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void run(CryptoNote::WalletGreen wallet, CryptoNote::INode node, Config config);

    //	}

    //namespace Tools.Base58
    //{
    //	public static class GlobalMembers
    //	{
    //		public static string encode(string data)
    //		{
    //		  if (string.IsNullOrEmpty(data))
    //		  {
    //			return string();
    //		  }

    //		  ulong full_block_count = data.Length / GlobalMembers.full_block_size;
    //		  ulong last_block_size = data.Length % GlobalMembers.full_block_size;
    //		  ulong res_size = full_block_count * GlobalMembers.full_encoded_block_size + GlobalMembers.encoded_block_sizes[last_block_size];

    //		  string res = new string(res_size, GlobalMembers.alphabet[0]);
    //		  for (ulong i = 0; i < full_block_count; ++i)
    //		  {
    //			GlobalMembers.encode_block(data.data() + i * GlobalMembers.full_block_size, new ulong(GlobalMembers.full_block_size), ref res[i * GlobalMembers.full_encoded_block_size]);
    //		  }

    //		  if (0 < last_block_size)
    //		  {
    //			GlobalMembers.encode_block(data.data() + full_block_count * GlobalMembers.full_block_size, new ulong(last_block_size), ref res[full_block_count * GlobalMembers.full_encoded_block_size]);
    //		  }

    //		  return res;
    //		}
    //		public static bool decode(string enc, string data)
    //		{
    //		  if (string.IsNullOrEmpty(enc))
    //		  {
    //			data = "";
    //			return true;
    //		  }

    //		  ulong full_block_count = enc.Length / GlobalMembers.full_encoded_block_size;
    //		  ulong last_block_size = enc.Length % GlobalMembers.full_encoded_block_size;
    //		  int last_block_decoded_size = decoded_block_sizes.instance(last_block_size);
    //		  if (last_block_decoded_size < 0)
    //		  {
    //			return false; // Invalid enc length
    //		  }
    //		  ulong data_size = full_block_count * GlobalMembers.full_block_size + last_block_decoded_size;

    //		  data.resize(data_size, 0);
    //		  for (ulong i = 0; i < full_block_count; ++i)
    //		  {
    //			if (!GlobalMembers.decode_block(enc.data() + i * GlobalMembers.full_encoded_block_size, new ulong(GlobalMembers.full_encoded_block_size), ref data[i * GlobalMembers.full_block_size]))
    //			{
    //			  return false;
    //			}
    //		  }

    //		  if (0 < last_block_size)
    //		  {
    //			if (!GlobalMembers.decode_block(enc.data() + full_block_count * GlobalMembers.full_encoded_block_size, new ulong(last_block_size), ref data[full_block_count * GlobalMembers.full_block_size]))
    //			{
    //			  return false;
    //			}
    //		  }

    //		  return true;
    //		}

    //		public static string encode_addr(ulong tag, string data)
    //		{
    //		  string buf = Tools.GlobalMembers.get_varint_data(tag);
    //		  buf += data;
    //		  Crypto.Hash hash = Crypto.GlobalMembers.cn_fast_hash(buf.data(), buf.Length);
    ////C++ TO C# CONVERTER TODO TASK: C# does not have an equivalent to pointers to value types:
    ////ORIGINAL LINE: const char* hash_data = reinterpret_cast<const char*>(&hash);
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		  char hash_data = reinterpret_cast<const char>(hash);
    //		  buf.append(hash_data, GlobalMembers.addr_checksum_size);
    //		  return Base58.GlobalMembers.encode(buf);
    //		}
    //		public static bool decode_addr(string addr, ulong tag, ref string data)
    //		{
    //		  string addr_data;
    //		  bool r = Base58.GlobalMembers.decode(addr, addr_data);
    //		  if (!r)
    //		  {
    //			  return false;
    //		  }
    //		  if (addr_data.Length <= GlobalMembers.addr_checksum_size)
    //		  {
    //			  return false;
    //		  }

    //		  string checksum = new string(GlobalMembers.addr_checksum_size, '\0');
    //		  checksum = addr_data.Substring(addr_data.Length - GlobalMembers.addr_checksum_size);

    //		  addr_data.resize(addr_data.Length - GlobalMembers.addr_checksum_size);
    //		  Crypto.Hash hash = Crypto.GlobalMembers.cn_fast_hash(addr_data.data(), addr_data.Length);
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		  string expected_checksum = new string(reinterpret_cast<const char>(hash), GlobalMembers.addr_checksum_size);
    //		  if (expected_checksum != checksum)
    //		  {
    //			  return false;
    //		  }

    //		  int read = Tools.GlobalMembers.read_varint(addr_data.GetEnumerator(), addr_data.end(), tag);
    //		  if (read <= 0)
    //		  {
    //			  return false;
    //		  }

    //		  data = addr_data.Substring(read);
    //		  return true;
    //		}
    //		  public const string alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
    //		  public static readonly ulong alphabet_size = sizeof(alphabet) - 1;
    //		  public static readonly ulong[] encoded_block_sizes = {0, 2, 3, 5, 6, 7, 9, 10, 11};
    ////C++ TO C# CONVERTER WARNING: This 'sizeof' ratio was replaced with a direct reference to the array length:
    ////ORIGINAL LINE: const ulong full_block_size = sizeof(encoded_block_sizes) / sizeof(encoded_block_sizes[0]) - 1;
    //		  public static readonly ulong full_block_size = encoded_block_sizes.Length - 1;
    //		  public static readonly ulong full_encoded_block_size = encoded_block_sizes[full_block_size];
    //		  public static readonly ulong addr_checksum_size = 4;


    ////C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on the parameter 'data', so pointers on this parameter are left unchanged:
    //		  public static ulong uint_8be_to_64(ushort * data, ulong size)
    //		  {
    //			Debug.Assert(1 <= size != null && size <= sizeof(ulong));

    //			ulong res = 0;
    //			switch (9 - size)
    //			{
    //			case 1:
    //				res |= *data++; // fallthrough
    ////C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
    //			case 2:
    //				res <<= 8;
    //				res |= *data++; // fallthrough
    ////C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
    //			case 3:
    //				res <<= 8;
    //				res |= *data++; // fallthrough
    ////C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
    //			case 4:
    //				res <<= 8;
    //				res |= *data++; // fallthrough
    ////C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
    //			case 5:
    //				res <<= 8;
    //				res |= *data++; // fallthrough
    ////C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
    //			case 6:
    //				res <<= 8;
    //				res |= *data++; // fallthrough
    ////C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
    //			case 7:
    //				res <<= 8;
    //				res |= *data++; // fallthrough
    ////C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
    //			case 8:
    //				res <<= 8;
    //				res |= *data;
    //				break;
    //			default:
    //				Debug.Assert(false);
    //			break;
    //			}

    //			return res;
    //		  }

    //		  public static void uint_64_to_8be(ulong num, ulong size, ushort data)
    //		  {
    //			Debug.Assert(1 <= size != null && size <= sizeof(ulong));

    //			ulong num_be = SWAP64BE(num);
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
    //			memcpy(data, reinterpret_cast<ushort>(num_be) + sizeof(ulong) - size, size);
    //		  }

    //		  public static void encode_block(string block, ulong size, ref string res)
    //		  {
    //			Debug.Assert(1 <= size != null && size <= GlobalMembers.full_block_size);

    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //			ulong num = GlobalMembers.uint_8be_to_64(reinterpret_cast<const ushort>(block), new ulong(size));
    //			int i = (int)GlobalMembers.encoded_block_sizes[size] - 1;
    //			while (0 < num)
    //			{
    //			  ulong remainder = num % GlobalMembers.alphabet_size;
    //			  num /= GlobalMembers.alphabet_size;
    //			  res[i] = GlobalMembers.alphabet[remainder];
    //			  --i;
    //			}
    //		  }

    //		  public static bool decode_block(string block, ulong size, ref string res)
    //		  {
    //			Debug.Assert(1 <= size != null && size <= GlobalMembers.full_encoded_block_size);

    //			int res_size = decoded_block_sizes.instance(size);
    //			if (res_size <= 0)
    //			{
    //			  return false; // Invalid block size
    //			}

    //			ulong res_num = 0;
    //			ulong order = 1;
    //			for (ulong i = size - 1; i < size; --i)
    //			{
    //			  int digit = reverse_alphabet.instance(block[i]);
    //			  if (digit < 0)
    //			  {
    //				return false; // Invalid symbol
    //			  }

    //			  ulong product_hi = new ulong();
    //			  ulong tmp = res_num + GlobalMembers.mul128(new ulong(order), digit, product_hi);
    //			  if (tmp < res_num || 0 != product_hi)
    //			  {
    //				return false; // Overflow
    //			  }

    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: res_num = tmp;
    //			  res_num.CopyFrom(tmp);
    //			  order *= GlobalMembers.alphabet_size; // Never overflows, 58^10 < 2^64
    //			}

    //			if ((ulong)res_size < GlobalMembers.full_block_size && (UINT64_C(1) << (8 * res_size)) <= res_num)
    //			{
    //			  return false; // Overflow
    //			}

    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //			GlobalMembers.uint_64_to_8be(new ulong(res_num), res_size, reinterpret_cast<ushort>(res));

    //			return true;
    //		  }
    //	}
    //}

    //namespace command_line
    //{
    //	public static class GlobalMembers
    //	{
    //	  public static boost::program_options.typed_value<T, char> make_semantic<T>(arg_descriptor<T, true> UnnamedParameter)
    //	  {
    //		return boost::program_options.value<T>().required();
    //	  }

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	  public static boost::program_options.typed_value<T, char> make_semantic<T>(arg_descriptor<T, false> arg)
    //	  {
    //		var semantic = boost::program_options.value<T>();
    //		if (!arg.not_use_default)
    //		{
    //		  semantic.default_value(arg.default_value);
    //		}
    //		return semantic;
    //	  }

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	  public static boost::program_options.typed_value<T, char> make_semantic<T>(arg_descriptor<T, false> arg, T def)
    //	  {
    //		var semantic = boost::program_options.value<T>();
    //		if (!arg.not_use_default)
    //		{
    //		  semantic.default_value(def);
    //		}
    //		return semantic;
    //	  }

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	  public static boost::program_options.typed_value<List<T>, char> make_semantic<T>(arg_descriptor<List<T>, false> UnnamedParameter)
    //	  {
    //		var semantic = boost::program_options.value< List<T>>();
    //		semantic.default_value(new List<T>(), "");
    //		return semantic;
    //	  }

    ////C++ TO C# CONVERTER TODO TASK: C++ template specifiers with non-type parameters cannot be converted to C#:
    ////ORIGINAL LINE: template<typename T, bool required>
    //	  public static void add_arg<T, bool required>(boost::program_options.options_description description, arg_descriptor<T, required> arg, bool unique = true)
    //	  {
    //		if (unique && 0 != description.find_nothrow(arg.name, false))
    //		{
    //		  std::cerr << "Argument already exists: " << arg.name << std::endl;
    //		  return;
    //		}

    //		description.add_options()(arg.name, make_semantic(arg), arg.description);
    //	  }

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	  public static void add_arg<T>(boost::program_options.options_description description, arg_descriptor<T, false> arg, T def, bool unique = true)
    //	  {
    //		if (unique && 0 != description.find_nothrow(arg.name, false))
    //		{
    //		  std::cerr << "Argument already exists: " << arg.name << std::endl;
    //		  return;
    //		}

    //		description.add_options()(arg.name, make_semantic(arg, def), arg.description);
    //	  }

    //	  public static void add_arg(boost::program_options.options_description description, arg_descriptor<bool, false> arg, bool unique)
    //	  {
    //		if (unique && 0 != description.find_nothrow(arg.name, false))
    //		{
    //		  std::cerr << "Argument already exists: " << arg.name << std::endl;
    //		  return;
    //		}

    //		description.add_options()(arg.name, boost::program_options.bool_switch(), arg.description);
    //	  }

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename charT>
    //	  public static boost::program_options.basic_parsed_options<charT> parse_command_line<charT>(int argc, charT[] argv, boost::program_options.options_description desc, bool allow_unregistered = false)
    //	  {
    //		var parser = boost::program_options.command_line_parser(argc, argv);
    //		parser.options(desc);
    //		if (allow_unregistered)
    //		{
    //		  parser.allow_unregistered();
    //		}
    //		return parser.run();
    //	  }

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename F>
    //	  public static bool handle_error_helper<F>(boost::program_options.options_description desc, F parser)
    //	  {
    //		try
    //		{
    //		  return parser();
    //		}
    //		catch (System.Exception e)
    //		{
    //		  std::cerr << "Failed to parse arguments: " << e.Message << std::endl;
    //		  std::cerr << desc << std::endl;
    //		  return false;
    //		}
    //		catch
    //		{
    //		  std::cerr << "Failed to parse arguments: unknown exception" << std::endl;
    //		  std::cerr << desc << std::endl;
    //		  return false;
    //		}
    //	  }

    ////C++ TO C# CONVERTER TODO TASK: C++ template specifiers with non-type parameters cannot be converted to C#:
    ////ORIGINAL LINE: template<typename T, bool required>
    //	  public static bool has_arg<T, bool required>(boost::program_options.variables_map vm, arg_descriptor<T, required> arg)
    //	  {
    //		var value = vm[arg.name];
    //		return !value.empty();
    //	  }


    ////C++ TO C# CONVERTER TODO TASK: C++ template specifiers with non-type parameters cannot be converted to C#:
    ////ORIGINAL LINE: template<typename T, bool required>
    //	  public static T get_arg<T, bool required>(boost::program_options.variables_map vm, arg_descriptor<T, required> arg)
    //	  {
    //		return vm[arg.name].template @as<T>();
    //	  }

    ////C++ TO C# CONVERTER TODO TASK: C++ template specialization was removed by C++ to C# Converter:
    ////ORIGINAL LINE: inline bool has_arg<bool, false>(const boost::program_options::variables_map& vm, const arg_descriptor<bool, false>& arg)
    //	  public static bool has_arg(boost::program_options.variables_map vm, arg_descriptor<bool, false> arg)
    //	  {
    //		return get_arg<bool, false>(vm, arg);
    //	  }


    ////C++ TO C# CONVERTER NOTE: 'extern' variable declarations are not required in C#:
    //	//  extern const arg_descriptor<bool> arg_help;
    ////C++ TO C# CONVERTER NOTE: 'extern' variable declarations are not required in C#:
    //	//  extern const arg_descriptor<bool> arg_version;
    ////C++ TO C# CONVERTER NOTE: 'extern' variable declarations are not required in C#:
    //	//  extern const arg_descriptor<string> arg_data_dir;
    //	  public static readonly arg_descriptor<bool> arg_help = new arg_descriptor<bool>("help", "Produce help message");
    //	  public static readonly arg_descriptor<bool> arg_version = new arg_descriptor<bool>("version", "Output version information");
    //	  public static readonly arg_descriptor<string> arg_data_dir = new arg_descriptor<string>("data-dir", "Specify data directory");
    //	}
    //}

    //namespace Common.Console
    //{
    //	public static class GlobalMembers
    //	{
    ////C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
    //private static ushort[] setTextColor_winColors = {FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE, FOREGROUND_BLUE, FOREGROUND_GREEN, FOREGROUND_RED, FOREGROUND_RED | FOREGROUND_GREEN, FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE, FOREGROUND_GREEN | FOREGROUND_BLUE, FOREGROUND_RED | FOREGROUND_BLUE, FOREGROUND_BLUE | FOREGROUND_INTENSITY, FOREGROUND_GREEN | FOREGROUND_INTENSITY, FOREGROUND_RED | FOREGROUND_INTENSITY, FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_INTENSITY, FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE | FOREGROUND_INTENSITY, FOREGROUND_GREEN | FOREGROUND_BLUE | FOREGROUND_INTENSITY, FOREGROUND_RED | FOREGROUND_BLUE | FOREGROUND_INTENSITY};

    //	public static void setTextColor(Color color)
    //	{
    //	  if (!Console.GlobalMembers.isConsoleTty())
    //	  {
    //		return;
    //	  }

    //	  if (color > Color.BrightMagenta)
    //	  {
    //		color = Color.Default;
    //	  }

    //	#if _WIN32

    //	//C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
    //	//  static ushort winColors[] = { FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE, FOREGROUND_BLUE, FOREGROUND_GREEN, FOREGROUND_RED, FOREGROUND_RED | FOREGROUND_GREEN, FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE, FOREGROUND_GREEN | FOREGROUND_BLUE, FOREGROUND_RED | FOREGROUND_BLUE, FOREGROUND_BLUE | FOREGROUND_INTENSITY, FOREGROUND_GREEN | FOREGROUND_INTENSITY, FOREGROUND_RED | FOREGROUND_INTENSITY, FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_INTENSITY, FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE | FOREGROUND_INTENSITY, FOREGROUND_GREEN | FOREGROUND_BLUE | FOREGROUND_INTENSITY, FOREGROUND_RED | FOREGROUND_BLUE | FOREGROUND_INTENSITY };

    //	  SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), setTextColor_winColors[(ulong)color]);

    //	#else

    //	  string[] ansiColors = {"\x001B[0m", "\x001B[0;34m", "\x001B[0;32m", "\x001B[0;31m", "\x001B[0;33m", "\x001B[0;37m", "\x001B[0;36m", "\x001B[0;35m", "\x001B[1;34m", "\x001B[1;32m", "\x001B[1;31m", "\x001B[1;33m", "\x001B[1;37m", "\x001B[1;36m", "\x001B[1;35m"};

    //	  Console.Write(ansiColors[(ulong)color]);

    //	#endif

    //	}
    ////C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
    //private static bool isConsoleTty_istty = 0 != _isatty(_fileno(stdout));
    ////C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
    //private static bool isConsoleTty_istty = 0 != isatty(fileno(stdout));
    //	public static bool isConsoleTty()
    //	{
    //	#if WIN32
    //	//C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
    //	//  static bool istty = 0 != _isatty(_fileno(stdout));
    //	#else
    //	//C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
    //	//  static bool istty = 0 != isatty(fileno(stdout));
    //	#endif
    //	  return isConsoleTty_istty;
    //	}
    //	}
    //}

    //namespace Common
    //{
    //	public static class GlobalMembers
    //	{


    //  //--------------------------------------------------------------------------------
    //	  public static string get_mining_speed(uint hr)
    //	  {
    //		if (hr > 1e9)
    //		{
    //			return (boost::format("%.2f GH/s") % (hr / 1e9)).str();
    //		}
    //		if (hr > 1e6)
    //		{
    //			return (boost::format("%.2f MH/s") % (hr / 1e6)).str();
    //		}
    //		if (hr > 1e3)
    //		{
    //			return (boost::format("%.2f KH/s") % (hr / 1e3)).str();
    //		}

    //		return (boost::format("%.0f H/s") % hr).str();
    //	  }

    //  //--------------------------------------------------------------------------------
    //	  public static string get_sync_percentage(ulong height, ulong target_height)
    //	  {
    //		/* Don't divide by zero */
    //		if (height == 0 || target_height == 0)
    //		{
    //		  return "0.00";
    //		}

    //		/* So we don't have > 100% */
    //		if (height > target_height)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: height = target_height;
    //			height.CopyFrom(target_height);
    //		}

    //		float pc = 100.0f * height / target_height;

    //		if (height < target_height && pc > 99.99f)
    //		{
    //		  pc = 99.99f; // to avoid 100% when not fully synced
    //		}

    //		return (boost::format("%.2f") % pc).str();
    //	  }
    //	//  string get_upgrade_time(ulong height, ulong upgrade_height);Tangible Method Implementation Not FoundCommon-get_upgrade_time

    //  //--------------------------------------------------------------------------------
    //	  public static string get_status_string(CryptoNote.COMMAND_RPC_GET_INFO.response iresp)
    //	  {
    //		std::stringstream ss = new std::stringstream();
    //		std::DateTime uptime = std::time(null) - iresp.start_time;
    //		var forkStatus = get_fork_status(new uint(iresp.network_height), new List<ulong>(iresp.upgrade_heights), new ulong(iresp.supported_height));

    //		ss << "Height: " << iresp.height << "/" << iresp.network_height << " (" << get_sync_percentage(new ulong(iresp.height), new uint(iresp.network_height)) << "%) " << "on " << (iresp.testnet ? "testnet, " : "mainnet, ") << (iresp.synced ? "synced, " : "syncing, ") << "net hash " << get_mining_speed(new uint(iresp.hashrate)) << ", " << "v" << +iresp.major_version << "," << get_update_status(forkStatus, new uint(iresp.network_height), new List<ulong>(iresp.upgrade_heights)) << ", " << iresp.outgoing_connections_count << "(out)+" << iresp.incoming_connections_count << "(in) connections, " << "uptime " << (uint)Math.Floor(uptime / 60.0 / 60.0 / 24.0) << "d " << (uint)Math.Floor(fmod((uptime / 60.0 / 60.0), 24.0)) << "h " << (uint)Math.Floor(fmod((uptime / 60.0), 60.0)) << "m " << (uint)fmod(uptime, 60.0) << "s";

    //		if (forkStatus == ForkStatus.OutOfDate)
    //		{
    //			ss << std::endl << get_upgrade_info(new ulong(iresp.supported_height), new List<ulong>(iresp.upgrade_heights));
    //		}

    //		return ss.str();
    //	  }

    //	public static ForkStatus get_fork_status(ulong height, List<ulong> upgrade_heights, ulong supported_height)
    //	{
    //		/* Allow fork heights to be empty */
    //		if (upgrade_heights.Count == 0)
    //		{
    //			return ForkStatus.UpToDate;
    //		}

    //		ulong next_fork = 0;

    //		foreach (var upgrade in upgrade_heights)
    //		{
    //			/* We have hit an upgrade already that the user cannot support */
    //			if (height >= upgrade != null && supported_height < upgrade)
    //			{
    //				return ForkStatus.OutOfDate;
    //			}

    //			/* Get the next fork height */
    //			if (upgrade > height)
    //			{
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: next_fork = upgrade;
    //				next_fork.CopyFrom(upgrade);
    //				break;
    //			}
    //		}

    //		float days = (next_fork - height) / CryptoNote.parameters.EXPECTED_NUMBER_OF_BLOCKS_PER_DAY;

    //		/* Next fork in < 30 days away */
    //		if (days < 30F)
    //		{
    //			/* Software doesn't support the next fork yet */
    //			if (supported_height < next_fork)
    //			{
    //				return ForkStatus.ForkSoonNotReady;
    //			}
    //			else
    //			{
    //				return ForkStatus.ForkSoonReady;
    //			}
    //		}

    //		if (height > next_fork)
    //		{
    //			return ForkStatus.UpToDate;
    //		}

    //		return ForkStatus.ForkLater;
    //	}

    //	public static string get_fork_time(ulong height, List<ulong> upgrade_heights)
    //	{
    //		ulong next_fork = 0;

    //		foreach (var upgrade in upgrade_heights)
    //		{
    //			/* Get the next fork height */
    //			if (upgrade > height)
    //			{
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: next_fork = upgrade;
    //				next_fork.CopyFrom(upgrade);
    //				break;
    //			}
    //		}

    //		float days = (next_fork - height) / CryptoNote.parameters.EXPECTED_NUMBER_OF_BLOCKS_PER_DAY;

    //		if (height == next_fork)
    //		{
    //			return " (forking now),";
    //		}
    //		else if (days < 1F)
    //		{
    //			return (boost::format(" (next fork in %.1f hours),") % (days * 24)).str();
    //		}
    //		else
    //		{
    //			return (boost::format(" (next fork in %.1f days),") % days).str();
    //		}
    //	}

    //	public static string get_update_status(ForkStatus forkStatus, ulong height, List<ulong> upgrade_heights)
    //	{
    //		switch (forkStatus)
    //		{
    //			case ForkStatus.UpToDate:
    //			case ForkStatus.ForkLater:
    //			{
    //				return " up to date";
    //			}
    //			case ForkStatus.ForkSoonReady:
    //			{
    //				return get_fork_time(new ulong(height), new List<ulong>(upgrade_heights)) + " up to date";
    //			}
    //			case ForkStatus.ForkSoonNotReady:
    //			{
    //				return get_fork_time(new ulong(height), new List<ulong>(upgrade_heights)) + " update needed";
    //			}
    //			case ForkStatus.OutOfDate:
    //			{
    //				return " out of date, likely forked";
    //			}
    //			default:
    //			{
    //				throw new System.Exception("Unexpected case unhandled");
    //			}
    //		}
    //	}

    //	//--------------------------------------------------------------------------------
    //	public static string get_upgrade_info(ulong supported_height, List<ulong> upgrade_heights)
    //	{
    //		foreach (var upgrade in upgrade_heights)
    //		{
    //			if (upgrade > supported_height)
    //			{
    //				return "The network forked at height " + Convert.ToString(upgrade) + ", please update your software: " + CryptoNote.LATEST_VERSION_URL;
    //			}
    //		}

    //		/* This shouldnt happen */
    //		return string();
    //	}

    ////C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
    ////ORIGINAL LINE: JsonValue& JsonValue::operator =(const Array& value)
    //	public static JsonValue JsonValue.CopyFrom(Array value)
    //	{
    //	  if (type != ARRAY)
    //	  {
    //		destructValue();
    //		type = NIL;
    //		new(valueArray)Array(value);
    //		type = ARRAY;
    //	  }
    //	  else
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		*reinterpret_cast<Array>(valueArray) = value;
    //	  }

    //	  return this;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
    ////C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
    //	public static JsonValue JsonValue.operator = (Array && value)
    //	{
    //	  if (type != ARRAY)
    //	  {
    //		destructValue();
    //		type = NIL;
    //		new(valueArray)Array(std::move(value));
    //		type = ARRAY;
    //	  }
    //	  else
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		*reinterpret_cast<Array>(valueArray) = std::move(value);
    //	  }

    //	  return this;
    //	}

    //	//JsonValue& JsonValue::operator=(Bool value) {
    //	//  if (type != BOOL) {
    //	//    destructValue();
    //	//    type = BOOL;
    //	//  }
    //	//
    //	//  valueBool = value;
    //	//  return *this;
    //	//}

    ////C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
    ////ORIGINAL LINE: JsonValue& JsonValue::operator =(Integer value)
    //	public static JsonValue JsonValue.CopyFrom(Integer value)
    //	{
    //	  if (type != INTEGER)
    //	  {
    //		destructValue();
    //		type = INTEGER;
    //	  }

    //	  valueInteger = value;
    //	  return this;
    //	}

    ////C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
    ////ORIGINAL LINE: JsonValue& JsonValue::operator =(Nil)
    //	public static JsonValue JsonValue.CopyFrom(Nil UnnamedParameter)
    //	{
    //	  if (type != NIL)
    //	  {
    //		destructValue();
    //		type = NIL;
    //	  }

    //	  return this;
    //	}

    ////C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
    ////ORIGINAL LINE: JsonValue& JsonValue::operator =(const Object& value)
    //	public static JsonValue JsonValue.CopyFrom(Object value)
    //	{
    //	  if (type != OBJECT)
    //	  {
    //		destructValue();
    //		type = NIL;
    //		new(valueObject)Object(value);
    //		type = OBJECT;
    //	  }
    //	  else
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		*reinterpret_cast<Object>(valueObject) = value;
    //	  }

    //	  return this;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
    ////C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
    //	public static JsonValue JsonValue.operator = (Object && value)
    //	{
    //	  if (type != OBJECT)
    //	  {
    //		destructValue();
    //		type = NIL;
    //		new(valueObject)Object(std::move(value));
    //		type = OBJECT;
    //	  }
    //	  else
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		*reinterpret_cast<Object>(valueObject) = std::move(value);
    //	  }

    //	  return this;
    //	}

    ////C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
    ////ORIGINAL LINE: JsonValue& JsonValue::operator =(Real value)
    //	public static JsonValue JsonValue.CopyFrom(Real value)
    //	{
    //	  if (type != REAL)
    //	  {
    //		destructValue();
    //		type = REAL;
    //	  }

    //	  valueReal = value;
    //	  return this;
    //	}

    ////C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
    ////ORIGINAL LINE: JsonValue& JsonValue::operator =(const String& value)
    //	public static JsonValue JsonValue.CopyFrom(String value)
    //	{
    //	  if (type != STRING)
    //	  {
    //		destructValue();
    //		type = NIL;
    //		new(valueString)String(value);
    //		type = STRING;
    //	  }
    //	  else
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		*reinterpret_cast<String>(valueString) = value;
    //	  }

    //	  return this;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
    ////C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
    //	public static JsonValue JsonValue.operator = (String && value)
    //	{
    //	  if (type != STRING)
    //	  {
    //		destructValue();
    //		type = NIL;
    //		new(valueString)String(std::move(value));
    //		type = STRING;
    //	  }
    //	  else
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		*reinterpret_cast<String>(valueString) = std::move(value);
    //	  }

    //	  return this;
    //	}

    //	public static JsonValue JsonValue.operator ()(Key key)
    //	{
    //	  return getObject().at(key);
    //	}

    ////C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
    ////ORIGINAL LINE: const JsonValue& JsonValue::operator ()(const Key& key) const
    //	public static JsonValue JsonValue.operator ()(Key key)
    //	{
    //	  return getObject().at(key);
    //	}

    //	public static char readChar(std::istream in)
    //	{
    //	  char c;

    //	  if ((in >> c) == 0)
    //	  {
    //		throw new System.Exception("Unable to parse: unexpected end of stream");
    //	  }

    //	  return c;
    //	}

    //	public static char readNonWsChar(std::istream in)
    //	{
    //	  char c;

    //	  do
    //	  {
    //		c = GlobalMembers.readChar(in);
    //	  } while (isspace(c));

    //	  return c;
    //	}

    //	public static string readStringToken(std::istream in)
    //	{
    //	  char c;
    //	  string value;

    //	  while (in != null)
    //	  {
    //		c = GlobalMembers.readChar(in);

    //		if (c == '"')
    //		{
    //		  break;
    //		}

    //		if (c == '\\')
    //		{
    //		  value += c;
    //		  c = GlobalMembers.readChar(in);
    //		}

    //		value += c;
    //	  }

    //	  return value;
    //	}
    //	public static T medianValue<T>(List<T> v)
    //	{
    //	  if (v.Count == 0)
    //	  {
    //		return default(T);
    //	  }

    //	  if (v.Count == 1)
    //	  {
    //		return v[0];
    //	  }

    //	  var n = (v.Count) / 2;
    //	  v.Sort();
    //	  //nth_element(v.begin(), v.begin()+n-1, v.end());
    //	  if (v.Count % 2 != 0)
    //	  { //1, 3, 5...
    //		return v[n];
    //	  }
    //	  else
    //	  { //2, 4, 6...
    //		return (v[n - 1] + v[n]) / 2;
    //	  }
    //	}

    //	public static string NativePathToGeneric(string nativePath)
    //	{
    //	  if (GlobalMembers.GENERIC_PATH_SEPARATOR == GlobalMembers.NATIVE_PATH_SEPARATOR)
    //	  {
    //		return nativePath;
    //	  }
    //	  string genericPath = nativePath;
    //	  std::replace(genericPath.GetEnumerator(), genericPath.end(), GlobalMembers.NATIVE_PATH_SEPARATOR, GlobalMembers.GENERIC_PATH_SEPARATOR);
    //	  return genericPath;
    //	}

    //	public static string GetPathDirectory(string path)
    //	{
    //	  var slashPos = path.LastIndexOf(GlobalMembers.GENERIC_PATH_SEPARATOR);
    //	  if (slashPos == -1)
    //	  {
    //		return string();
    //	  }
    //	  return path.Substring(0, slashPos);
    //	}
    //	public static string GetPathFilename(string path)
    //	{
    //	  var slashPos = path.LastIndexOf(GlobalMembers.GENERIC_PATH_SEPARATOR);
    //	  if (slashPos == -1)
    //	  {
    //		return path;
    //	  }
    //	  return path.Substring(slashPos + 1);
    //	}
    //	public static void SplitPath(string path, ref string directory, ref string filename)
    //	{
    //	  directory = GetPathDirectory(path);
    //	  filename = GetPathFilename(path);
    //	}

    //	public static string CombinePath(string path1, string path2)
    //	{
    //	  return path1 + GlobalMembers.GENERIC_PATH_SEPARATOR + path2;
    //	}
    //	public static string GetExtension(string path)
    //	{
    //	  var pos = GlobalMembers.findExtensionPosition(path);
    //	  if (pos != -1)
    //	  {
    //		return path.Substring(pos);
    //	  }
    //	  return string();
    //	}
    //	public static string RemoveExtension(string filename)
    //	{
    //	  var pos = GlobalMembers.findExtensionPosition(filename);

    //	  if (pos == -1)
    //	  {
    //		return filename;
    //	  }

    //	  return filename.Substring(0, pos);
    //	}
    //	public static string ReplaceExtenstion(string path, string extension)
    //	{
    //	  return RemoveExtension(path) + extension;
    //	}
    //	public static bool HasParentPath(string path)
    //	{
    //	  return path.IndexOf(GlobalMembers.GENERIC_PATH_SEPARATOR) != -1;
    //	}

    //	public static void read(IInputStream in, object data, ulong size)
    //	{
    //	  while (size > 0)
    //	  {
    //		ulong readSize = in.readSome(data, new ulong(size));
    //		if (readSize == 0)
    //		{
    //		  throw new System.Exception("Failed to read from IInputStream");
    //		}

    //		data = (ushort)data + readSize;
    //		size -= readSize;
    //	  }
    //	}
    //	public static void read(IInputStream in, short value)
    //	{
    //	  read(in, value, sizeof(short));
    //	}
    //	public static void read(IInputStream in, short value)
    //	{
    //	  // TODO: Convert from little endian on big endian platforms
    //	  read(in, value, sizeof(short));
    //	}
    //	public static void read(IInputStream in, int value)
    //	{
    //	  // TODO: Convert from little endian on big endian platforms
    //	  read(in, value, sizeof(int));
    //	}
    //	public static void read(IInputStream in, long value)
    //	{
    //	  // TODO: Convert from little endian on big endian platforms
    //	  read(in, value, sizeof(long));
    //	}
    //	public static void read(IInputStream in, ushort value)
    //	{
    //	  read(in, value, sizeof(ushort));
    //	}
    //	public static void read(IInputStream in, ushort value)
    //	{
    //	  // TODO: Convert from little endian on big endian platforms
    //	  read(in, value, sizeof(ushort));
    //	}
    //	public static void read(IInputStream in, uint value)
    //	{
    //	  // TODO: Convert from little endian on big endian platforms
    //	  read(in, value, sizeof(uint));
    //	}
    //	public static T read(IInputStream in, ulong size)
    //	{
    //	  T value = new T();
    //	  read(in, value, new ulong(size));
    //	  return value;
    //	}
    //	public static void read(IInputStream in, List<ushort> data, ulong size)
    //	{
    //	  data.Resize(size);
    //	  read(in, data.data(), new ulong(size));
    //	}
    //	public static void read(IInputStream in, string data, ulong size)
    //	{
    //	  List<char> temp = new List<char>(size);
    //	  read(in, temp.data(), new ulong(size));
    //	  data.assign(temp.data(), size);
    //	}
    //	public static void readVarint(IInputStream in, ref ushort value)
    //	{
    //	  ushort temp = 0;
    //	  for (ushort shift = 0;; shift += 7)
    //	  {
    //		ushort piece = new ushort();
    //		read(in, piece);
    //		if (shift >= sizeof(ushort) * 8 - 7 && piece >= 1 << (sizeof(ushort) * 8 - shift))
    //		{
    //		  throw new System.Exception("readVarint, value overflow");
    //		}

    //		temp |= (ulong)(piece & 0x7f) << shift;
    //		if ((piece & 0x80) == 0)
    //		{
    //		  if (piece == 0 && shift != 0)
    //		  {
    //			throw new System.Exception("readVarint, invalid value representation");
    //		  }

    //		  break;
    //		}
    //	  }

    //	  value = temp;
    //	}
    //	public static void readVarint(IInputStream in, ref ushort value)
    //	{
    //	  ushort temp = 0;
    //	  for (ushort shift = 0;; shift += 7)
    //	  {
    //		ushort piece = new ushort();
    //		read(in, piece);
    //		if (shift >= sizeof(ushort) * 8 - 7 && piece >= 1 << (sizeof(ushort) * 8 - shift))
    //		{
    //		  throw new System.Exception("readVarint, value overflow");
    //		}

    //		temp |= (ulong)(piece & 0x7f) << shift;
    //		if ((piece & 0x80) == 0)
    //		{
    //		  if (piece == 0 && shift != 0)
    //		  {
    //			throw new System.Exception("readVarint, invalid value representation");
    //		  }

    //		  break;
    //		}
    //	  }

    //	  value = temp;
    //	}
    //	public static void readVarint(IInputStream in, ref uint value)
    //	{
    //	  uint temp = 0;
    //	  for (ushort shift = 0;; shift += 7)
    //	  {
    //		ushort piece = new ushort();
    //		read(in, piece);
    //		if (shift >= sizeof(uint) * 8 - 7 && piece >= 1 << (sizeof(uint) * 8 - shift))
    //		{
    //		  throw new System.Exception("readVarint, value overflow");
    //		}

    //		temp |= (ulong)(piece & 0x7f) << shift;
    //		if ((piece & 0x80) == 0)
    //		{
    //		  if (piece == 0 && shift != 0)
    //		  {
    //			throw new System.Exception("readVarint, invalid value representation");
    //		  }

    //		  break;
    //		}
    //	  }

    //	  value = temp;
    //	}
    //	public static void readVarint(IInputStream in, ref ulong value)
    //	{
    //	  ulong temp = 0;
    //	  for (ushort shift = 0;; shift += 7)
    //	  {
    //		ushort piece = new ushort();
    //		read(in, piece);
    //		if (shift >= sizeof(ulong) * 8 - 7 && piece >= 1 << (sizeof(ulong) * 8 - shift))
    //		{
    //		  throw new System.Exception("readVarint, value overflow");
    //		}

    //		temp |= (ulong)(piece & 0x7f) << shift;
    //		if ((piece & 0x80) == 0)
    //		{
    //		  if (piece == 0 && shift != 0)
    //		  {
    //			throw new System.Exception("readVarint, invalid value representation");
    //		  }

    //		  break;
    //		}
    //	  }

    //	  value = temp;
    //	}

    //	public static void write(IOutputStream @out, object data, ulong size)
    //	{
    //	  while (size > 0)
    //	  {
    //		ulong writtenSize = @out.writeSome(data, new ulong(size));
    //		if (writtenSize == 0)
    //		{
    //		  throw new System.Exception("Failed to write to IOutputStream");
    //		}

    //		data = (ushort)data + writtenSize;
    //		size -= writtenSize;
    //	  }
    //	}
    //	public static void write(IOutputStream @out, short value)
    //	{
    //	  write(@out, value, sizeof(short));
    //	}
    //	public static void write(IOutputStream @out, short value)
    //	{
    //	  // TODO: Convert to little endian on big endian platforms
    //	  write(@out, value, sizeof(short));
    //	}
    //	public static void write(IOutputStream @out, int value)
    //	{
    //	  // TODO: Convert to little endian on big endian platforms
    //	  write(@out, value, sizeof(int));
    //	}
    //	public static void write(IOutputStream @out, long value)
    //	{
    //	  // TODO: Convert to little endian on big endian platforms
    //	  write(@out, value, sizeof(long));
    //	}
    //	public static void write(IOutputStream @out, ushort value)
    //	{
    //	  write(@out, value, sizeof(ushort));
    //	}
    //	public static void write(IOutputStream @out, ushort value)
    //	{
    //	  // TODO: Convert to little endian on big endian platforms
    //	  write(@out, value, sizeof(ushort));
    //	}
    //	public static void write(IOutputStream @out, uint value)
    //	{
    //	  // TODO: Convert to little endian on big endian platforms
    //	  write(@out, value, sizeof(uint));
    //	}
    //	public static void write(IOutputStream @out, ulong value)
    //	{
    //	  // TODO: Convert to little endian on big endian platforms
    //	  write(@out, value, sizeof(ulong));
    //	}
    //	public static void write(IOutputStream @out, List<ushort> data)
    //	{
    //	  write(@out, data.data(), data.Count);
    //	}
    //	public static void write(IOutputStream @out, string data)
    //	{
    //	  write(@out, data.data(), data.Length);
    //	}
    //	public static void writeVarint(IOutputStream @out, ulong value)
    //	{
    //	  while (value >= 0x80)
    //	  {
    //		write(@out, (ushort)(value | 0x80));
    //		value >>= 7;
    //	  }

    //	  write(@out, (ushort)value);
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	public static T read<T>(IInputStream in)
    //	{
    //	  T value = new default(T);
    //	  read(in, value);
    //	  return value;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	public static T readVarint<T>(IInputStream in)
    //	{
    //	  T value = new default(T);
    //	  readVarint(in, ref value);
    //	  return value;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	public static ContainerFormatter<T> makeContainerFormatter<T>(T container)
    //	{
    //	  return new ContainerFormatter<T>(container);
    //	}

    //	public static void read(IInputStream in, ulong value)
    //	{
    //	  // TODO: Convert from little endian on big endian platforms
    //	  read(in, value, sizeof(ulong));
    //	}

    //	public static void writeVarint(IOutputStream @out, uint value)
    //	{
    //	  while (value >= 0x80)
    //	  {
    //		write(@out, (ushort)(value | 0x80));
    //		value >>= 7;
    //	  }

    //	  write(@out, (ushort)value);
    //	}

    //	public static string asString(object data, ulong size)
    //	{
    //	  return (string)((char)data, size);
    //	}
    //	public static string asString(List<ushort> data)
    //	{
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //	  return (string)(reinterpret_cast<const char>(data.data()), data.Count);
    //	}
    //	public static List<ushort> asBinaryArray(string data)
    //	{
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //	  var dataPtr = reinterpret_cast<ushort>(data.data());
    //	  return new List<ushort>(dataPtr, dataPtr + data.Length);
    //	}

    //	public static ushort fromHex(char character)
    //	{
    //	  ushort value = GlobalMembers.characterValues[(byte)character];
    //	  if (value > 0x0f)
    //	  {
    //		throw new System.Exception("fromHex: invalid character");
    //	  }

    //	  return value;
    //	}
    //	public static bool fromHex(char character, ref ushort value)
    //	{
    //	  if (GlobalMembers.characterValues[(byte)character] > 0x0f)
    //	  {
    //		return false;
    //	  }

    //	  value = GlobalMembers.characterValues[(byte)character];
    //	  return true;
    //	}
    //	public static ulong fromHex(string text, object data, ulong bufferSize)
    //	{
    //	  if ((text.Length & 1) != 0)
    //	  {
    //		throw new System.Exception("fromHex: invalid string size");
    //	  }

    //	  if (text.Length >> 1 > bufferSize)
    //	  {
    //		throw new System.Exception("fromHex: invalid buffer size");
    //	  }

    //	  for (ulong i = 0; i < text.Length >> 1; ++i)
    //	  {
    //		(ushort)data[i] = fromHex(text[i << 1]) << 4 | fromHex(text[(i << 1) + 1]);
    //	  }

    //	  return text.Length >> 1;
    //	}
    //	public static bool fromHex(string text, object data, ulong bufferSize, ref ulong size)
    //	{
    //	  if ((text.Length & 1) != 0)
    //	  {
    //		return false;
    //	  }

    //	  if (text.Length >> 1 > bufferSize)
    //	  {
    //		return false;
    //	  }

    //	  for (ulong i = 0; i < text.Length >> 1; ++i)
    //	  {
    //		ushort value1 = new ushort();
    //		if (!fromHex(text[i << 1], ref value1))
    //		{
    //		  return false;
    //		}

    //		ushort value2 = new ushort();
    //		if (!fromHex(text[(i << 1) + 1], ref value2))
    //		{
    //		  return false;
    //		}

    //		(ushort)data[i] = value1 << 4 | value2;
    //	  }

    //	  size = text.Length >> 1;
    //	  return true;
    //	}
    //	public static List<ushort> fromHex(string text)
    //	{
    //	  if ((text.Length & 1) != 0)
    //	  {
    //		throw new System.Exception("fromHex: invalid string size");
    //	  }

    //	  List<ushort> data = new List<ushort>(text.Length >> 1);
    //	  for (ulong i = 0; i < data.Count; ++i)
    //	  {
    //		data[i] = fromHex(text[i << 1]) << 4 | fromHex(text[(i << 1) + 1]);
    //	  }

    //	  return data;
    //	}
    //	public static bool fromHex(string text, List<ushort> data)
    //	{
    //	  if ((text.Length & 1) != 0)
    //	  {
    //		return false;
    //	  }

    //	  for (ulong i = 0; i < text.Length >> 1; ++i)
    //	  {
    //		ushort value1 = new ushort();
    //		if (!fromHex(text[i << 1], ref value1))
    //		{
    //		  return false;
    //		}

    //		ushort value2 = new ushort();
    //		if (!fromHex(text[(i << 1) + 1], ref value2))
    //		{
    //		  return false;
    //		}

    //		data.Add(value1 << 4 | value2);
    //	  }

    //	  return true;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T>
    //	public static bool podFromHex<T>(string text, T val)
    //	{
    //	  ulong outSize = new ulong();
    //	  return fromHex(text, val, sizeof(T), ref outSize) && outSize == sizeof(T);
    //	}

    //	public static string toHex(object data, ulong size)
    //	{
    //	  string text;
    //	  for (ulong i = 0; i < size; ++i)
    //	  {
    //		text += "0123456789abcdef"[(ushort)data[i] >> 4];
    //		text += "0123456789abcdef"[(ushort)data[i] & 15];
    //	  }

    //	  return text;
    //	}
    //	public static void toHex(object data, ulong size, string text)
    //	{
    //	  for (ulong i = 0; i < size; ++i)
    //	  {
    //		text += "0123456789abcdef"[(ushort)data[i] >> 4];
    //		text += "0123456789abcdef"[(ushort)data[i] & 15];
    //	  }
    //	}
    //	public static string toHex(List<ushort> data)
    //	{
    //	  string text;
    //	  for (ulong i = 0; i < data.Count; ++i)
    //	  {
    //		text += "0123456789abcdef"[data[i] >> 4];
    //		text += "0123456789abcdef"[data[i] & 15];
    //	  }

    //	  return text;
    //	}
    //	public static void toHex(List<ushort> data, string text)
    //	{
    //	  for (ulong i = 0; i < data.Count; ++i)
    //	  {
    //		text += "0123456789abcdef"[data[i] >> 4];
    //		text += "0123456789abcdef"[data[i] & 15];
    //	  }
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<class T>
    //	public static string podToHex<T>(T s)
    //	{
    //	  return toHex(s, sizeof(T));
    //	}

    //	public static string extract(ref string text, char delimiter)
    //	{
    //	  ulong delimiterPosition = text.IndexOf(delimiter);
    //	  string subText;
    //	  if (delimiterPosition != -1)
    //	  {
    //		subText = text.Substring(0, delimiterPosition);
    //		text = text.Substring(delimiterPosition + 1);
    //	  }
    //	  else
    //	  {
    //		subText.swap(text);
    //	  }

    //	  return subText;
    //	}
    //	public static string extract(string text, char delimiter, ref ulong offset)
    //	{
    //	  ulong delimiterPosition = text.IndexOf(delimiter, offset);
    //	  if (delimiterPosition != -1)
    //	  {
    //		offset = delimiterPosition + 1;
    //		return text.Substring(offset, delimiterPosition);
    //	  }
    //	  else
    //	  {
    //		offset = text.Length;
    //		return text.Substring(offset);
    //	  }
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	public static T fromString<T>(string text)
    //	{ // Throws on error
    //	  T value = new default(T);
    //	  std::istringstream stream = new std::istringstream(text);
    //	  stream >> value;
    //	  if (stream.fail())
    //	  {
    //		throw new System.Exception("fromString: unable to parse value");
    //	  }

    //	  return value;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	public static bool fromString<T>(string text, T value)
    //	{ // Does not throw
    //	  std::istringstream stream = new std::istringstream(text);
    //	  stream >> value;
    //	  return !stream.fail();
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	public static List<T> fromDelimitedString<T>(string source, char delimiter)
    //	{ // Throws on error
    //	  List<T> data = new List<T>();
    //	  for (ulong offset = 0; offset != source.Length;)
    //	  {
    //		data.emplace_back(fromString<T>(extract(source, delimiter, ref offset)));
    //	  }

    //	  return data;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	public static bool fromDelimitedString<T>(string source, char delimiter, List<T> data)
    //	{ // Does not throw
    //	  for (ulong offset = 0; offset != source.Length;)
    //	  {
    //		T value = new default(T);
    //		if (!fromString<T>(extract(source, delimiter, ref offset), value))
    //		{
    //		  return false;
    //		}

    //		data.emplace_back(value);
    //	  }

    //	  return true;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	public static string toString<T>(T value)
    //	{ // Does not throw
    //	  std::ostringstream stream = new std::ostringstream();
    //	  stream << value;
    //	  return stream.str();
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	public static void toString<T>(T value, string text)
    //	{ // Does not throw
    //	  std::ostringstream stream = new std::ostringstream();
    //	  stream << value;
    //	  text += stream.str();
    //	}

    //	public static bool loadFileToString(string filepath, string buf)
    //	{
    //	  try
    //	  {
    //		std::ifstream fstream = new std::ifstream();
    //		fstream.exceptions(std::ifstream.failbit | std::ifstream.badbit);
    //		fstream.open(filepath, std::ios_base.binary | std::ios_base.in | std::ios.ate);

    //		ulong fileSize = (ulong)fstream.tellg();
    //		buf.resize(fileSize);

    //		if (fileSize > 0)
    //		{
    //		  fstream.seekg(0, std::ios.beg);
    //		  fstream.read(buf[0], buf.Length);
    //		}
    //	  }
    //	  catch (System.Exception)
    //	  {
    //		return false;
    //	  }

    //	  return true;
    //	}
    //	public static bool saveStringToFile(string filepath, string buf)
    //	{
    //	  try
    //	  {
    //		std::ofstream fstream = new std::ofstream();
    //		fstream.exceptions(std::ifstream.failbit | std::ifstream.badbit);
    //		fstream.open(filepath, std::ios_base.binary | std::ios_base.@out | std::ios_base.trunc);
    //		fstream << buf;
    //	  }
    //	  catch (System.Exception)
    //	  {
    //		return false;
    //	  }

    //	  return true;
    //	}


    //	public static string base64Decode(string encoded_string)
    //	{
    //	  ulong in_len = encoded_string.Length;
    //	  ulong i = 0;
    //	  ulong j = 0;
    //	  ulong in_ = 0;
    //	  byte[] char_array_4 = new byte[4];
    //	  byte[] char_array_3 = new byte[3];
    //	  string ret;

    //	  while (in_len-- && (encoded_string[in_] != '=') && GlobalMembers.is_base64(encoded_string[in_]))
    //	  {
    //		char_array_4[i++] = encoded_string[in_];
    //		in_++;
    //		if (i == 4)
    //		{
    //		  for (i = 0; i < 4; i++)
    //		  {
    //			char_array_4[i] = (byte)GlobalMembers.base64chars.IndexOf(char_array_4[i]);
    //		  }

    //		  char_array_3[0] = (char_array_4[0] << 2) + ((char_array_4[1] & 0x30) >> 4);
    //		  char_array_3[1] = ((char_array_4[1] & 0xf) << 4) + ((char_array_4[2] & 0x3c) >> 2);
    //		  char_array_3[2] = ((char_array_4[2] & 0x3) << 6) + char_array_4[3];

    //		  for (i = 0; (i < 3); i++)
    //		  {
    //			ret += char_array_3[i];
    //		  }
    //		  i = 0;
    //		}
    //	  }

    //	  if (i != null)
    //	  {
    //		for (j = i; j < 4; j++)
    //		{
    //		  char_array_4[j] = 0;
    //		}

    //		for (j = 0; j < 4; j++)
    //		{
    //		  char_array_4[j] = (byte)GlobalMembers.base64chars.IndexOf(char_array_4[j]);
    //		}

    //		char_array_3[0] = (char_array_4[0] << 2) + ((char_array_4[1] & 0x30) >> 4);
    //		char_array_3[1] = ((char_array_4[1] & 0xf) << 4) + ((char_array_4[2] & 0x3c) >> 2);
    //		char_array_3[2] = ((char_array_4[2] & 0x3) << 6) + char_array_4[3];

    //		for (j = 0; (j < i - 1); j++)
    //		{
    //			ret += char_array_3[j];
    //		}
    //	  }

    //	  return ret;
    //	}

    //	public static string ipAddressToString(uint ip)
    //	{
    //	  ushort[] bytes = Arrays.InitializeWithDefaultInstances<ushort>(4);
    //	  bytes[0] = ip & 0xFF;
    //	  bytes[1] = (ip >> 8) & 0xFF;
    //	  bytes[2] = (ip >> 16) & 0xFF;
    //	  bytes[3] = (ip >> 24) & 0xFF;

    //	  string buf = new string(new char[16]);
    //	  buf = string.Format("{0:D}.{1:D}.{2:D}.{3:D}", bytes[0], bytes[1], bytes[2], bytes[3]);

    //	  return (string)buf;
    //	}
    //	public static bool parseIpAddressAndPort(ref uint ip, ref uint port, string addr)
    //	{
    //	  uint[] v = Arrays.InitializeWithDefaultInstances<uint>(4);
    //	  uint localPort = new uint();

    //	  if (sscanf(addr, "%d.%d.%d.%d:%d", v[0], v[1], v[2], v[3], localPort) != 5)
    //	  {
    //		return false;
    //	  }

    //	  for (int i = 0; i < 4; ++i)
    //	  {
    //		if (v[i] > 0xff)
    //		{
    //		  return false;
    //		}
    //	  }

    //	  ip = (v[3] << 24) | (v[2] << 16) | (v[1] << 8) | v[0];
    //	  port = localPort;
    //	  return true;
    //	}

    //	public static string timeIntervalToString(ulong intervalInSeconds)
    //	{
    //	  var tail = intervalInSeconds;

    //	  var days = tail / (60 * 60 * 24);
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: tail = tail % (60 * 60 * 24);
    //	  tail.CopyFrom(tail % (60 * 60 * 24));
    //	  var hours = tail / (60 * 60);
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: tail = tail % (60 * 60);
    //	  tail.CopyFrom(tail % (60 * 60));
    //	  var minutes = tail / (60);
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: tail = tail % (60);
    //	  tail.CopyFrom(tail % (60));
    //	  var seconds = tail;

    //	  std::stringstream ss = new std::stringstream();
    //	  ss << "d" << days << std::setfill('0') << ".h" << std::setw(2) << hours << ".m" << std::setw(2) << minutes << ".s" << std::setw(2) << seconds;

    //	  return ss.str();
    //	}

    //	public static readonly ushort[] characterValues = {0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff};

    //	internal const string base64chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "abcdefghijklmnopqrstuvwxyz" + "0123456789+/";

    //	public static bool is_base64(byte c)
    //	{
    //	  return (isalnum(c) || (c == (byte)'+') || (c == (byte)'/'));
    //	}

    ////C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
    ////ORIGINAL LINE: const char& StringView::operator [](Size index) const
    //	public static char StringView.this[Size index]
    //	{
    //		get
    //		{
    //		  Debug.Assert(data != null || size == 0);
    //		  Debug.Assert(index < size);
    //		  return *(data + index);
    //		}
    //	}
    //	public static T getValueAs<T>(JsonValue js)
    //	{
    //	  return js.functorMethod;
    //	  //cdstatic_assert(false, "undefined conversion");
    //	}

    ////C++ TO C# CONVERTER TODO TASK: C++ template specialization was removed by C++ to C# Converter:
    ////ORIGINAL LINE: inline string getValueAs<string>(const JsonValue& js)
    //	public static string getValueAs(JsonValue js)
    //	{
    //		return js.getString();
    //	}

    ////C++ TO C# CONVERTER TODO TASK: C++ template specialization was removed by C++ to C# Converter:
    ////ORIGINAL LINE: inline ulong getValueAs<ulong>(const JsonValue& js)
    //	public static ulong getValueAs(JsonValue js)
    //	{
    //		return (ulong)js.getInteger();
    //	}
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to templates on variables:
    //	public static readonly Size ArrayView<Object, Size>.INVALID = Size.MaxValue;
    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<class Object, class Size>
    ////C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
    //	const ArrayView<Object, Size> ArrayView<Object, Size>.EMPTY(reinterpret_cast<Object*>(1), 0);
    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<class Object, class Size>
    ////C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
    //	const ArrayView<Object, Size> ArrayView<Object, Size>.NIL(null, 0);
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to templates on variables:
    //	public static readonly Size ArrayRef<Object, Size>.INVALID = Size.MaxValue;
    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<class Object, class Size>
    ////C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
    //	const ArrayRef<Object, Size> ArrayRef<Object, Size>.EMPTY(reinterpret_cast<Object*>(1), 0);
    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<class Object, class Size>
    ////C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
    //	const ArrayRef<Object, Size> ArrayRef<Object, Size>.NIL(null, 0);
    //	//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to templates on variables:
    //	public static readonly typename StringBuffer<MAXIMUM_SIZE>.Size StringBuffer<MAXIMUM_SIZE>.INVALID = typename StringBuffer<MAXIMUM_SIZE>.Size.MaxValue;
    //	}
    //}

    //namespace Tools
    //{
    //	public static class GlobalMembers
    //	{
    //	  public static string getDefaultDataDirectory()
    //	  {
    //		//namespace fs = boost::filesystem;
    //		// Windows < Vista: C:\Documents and Settings\Username\Application Data\CRYPTONOTE_NAME
    //		// Windows >= Vista: C:\Users\Username\AppData\Roaming\CRYPTONOTE_NAME
    //		// Mac: ~/Library/Application Support/CRYPTONOTE_NAME
    //		// Unix: ~/.CRYPTONOTE_NAME
    //		string config_folder;
    //	#if WIN32
    //		// Windows
    //		config_folder = get_special_folder_path(CSIDL_APPDATA, true) + "/" + CryptoNote.CRYPTONOTE_NAME;
    //	#else
    //		string pathRet;
    ////C++ TO C# CONVERTER TODO TASK: C# does not have an equivalent to pointers to value types:
    ////ORIGINAL LINE: char* pszHome = getenv("HOME");
    //		char pszHome = getenv("HOME");
    //		if (pszHome == null || pszHome.Length == 0)
    //		{
    //		  pathRet = "/";
    //		}
    //		else
    //		{
    //		  pathRet = pszHome;
    //		}
    //	#if MAC_OSX
    //		// Mac
    //		pathRet /= "Library/Application Support";
    //		config_folder = (pathRet + "/" + CryptoNote.CRYPTONOTE_NAME);
    //	#else
    //		// Unix
    //		config_folder = (pathRet + "/." + CryptoNote.CRYPTONOTE_NAME);
    //	#endif
    //	#endif

    //		return config_folder;
    //	  }
    //	  public static string getDefaultCacheFile(string dataDir)
    //	  {
    //		const string name = "cache_file";

    //		namespace bf = boost::filesystem;
    //		bf.path dir = dataDir;

    //		if (!bf.exists(dir))
    //		{
    //		  throw new System.Exception("Directory \"" + dir.string() + "\" doesn't exist");
    //		}

    //		if (!bf.exists(dir / name))
    //		{
    //		  throw new System.Exception("File \"" + boost::filesystem.path(dir / name).string() + "\" doesn't exist");
    //		}

    //		return boost::filesystem.path(dir / name).string();
    //	  }
    //	  public static string get_os_version_string()
    //	  {
    //	#if WIN32
    //		return get_windows_version_display_string();
    //	#else
    //		return get_nix_version_display_string();
    //	#endif
    //	  }
    //	  public static bool create_directories_if_necessary(string path)
    //	  {
    //		namespace fs = boost::filesystem;
    //		boost::system.error_code ec = new boost::system.error_code();
    //		fs.path fs_path = new fs.path(path);
    //		if (fs.is_directory(fs_path, ec))
    //		{
    //		  return true;
    //		}

    //		return fs.create_directories(fs_path, ec);
    //	  }
    //	  public static std::error_code replace_file(string replacement_name, string replaced_name)
    //	  {
    //		int code;
    //	#if WIN32
    //		// Maximizing chances for success
    //		uint attributes = global::GetFileAttributes(replaced_name);
    //		if (INVALID_FILE_ATTRIBUTES != attributes)
    //		{
    //		  global::SetFileAttributes(replaced_name, attributes & (~FILE_ATTRIBUTE_READONLY));
    //		}

    //		bool ok = 0 != global::MoveFileEx(replacement_name, replaced_name, MOVEFILE_REPLACE_EXISTING);
    //		code = ok ? 0 : (int)global::GetLastError();
    //	#else
    //		bool ok = 0 == std::rename(replacement_name, replaced_name);
    //		code = ok ? 0 : errno;
    //	#endif
    //		return std::error_code(code, std::system_category());
    //	  }
    //	  public static bool directoryExists(string path)
    //	  {
    //		boost::system.error_code ec = new boost::system.error_code();
    //		return boost::filesystem.is_directory(path, ec);
    //	  }
    //	#if WIN32
    //	  public static string get_windows_version_display_string()
    //	  {
    //	#define BUFSIZE

    //		char[] pszOS = {0};
    //		OSVERSIONINFOEX osvi = new OSVERSIONINFOEX();
    //		SYSTEM_INFO si = new SYSTEM_INFO();
    //		PGNSI pGNSI = new PGNSI();
    //		PGPI pGPI = new PGPI();
    //		int bOsVersionInfoEx;
    //		uint dwType;

    //		ZeroMemory(si, sizeof(SYSTEM_INFO));
    //		ZeroMemory(osvi, sizeof(OSVERSIONINFOEX));

    //		osvi.dwOSVersionInfoSize = sizeof(OSVERSIONINFOEX);
    //		bOsVersionInfoEx = GetVersionEx((OSVERSIONINFO) osvi);

    //		if (bOsVersionInfoEx == 0)
    //		{
    //			return pszOS;
    //		}

    //		// Call GetNativeSystemInfo if supported or GetSystemInfo otherwise.

    //		pGNSI = GetNativeSystemInfo;
    //		if (null != pGNSI)
    //		{
    //		  pGNSI(si);
    //		}
    //		else
    //		{
    //			GetSystemInfo(si);
    //		}

    //		if (VER_PLATFORM_WIN32_NT == osvi.dwPlatformId && osvi.dwMajorVersion > 4)
    //		{
    //		  StringCchCopy(pszOS, DefineConstants.BUFSIZE, "Microsoft ");

    //		  // Test for the specific product.

    //		  if (osvi.dwMajorVersion == 6)
    //		  {
    //			if (osvi.dwMinorVersion == 0)
    //			{
    //			  if (osvi.wProductType == VER_NT_WORKSTATION)
    //			  {
    //				StringCchCat(pszOS, DefineConstants.BUFSIZE, "Windows Vista ");
    //			  }
    //			  else
    //			  {
    //				  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Windows Server 2008 ");
    //			  }
    //			}

    //			if (osvi.dwMinorVersion == 1)
    //			{
    //			  if (osvi.wProductType == VER_NT_WORKSTATION)
    //			  {
    //				StringCchCat(pszOS, DefineConstants.BUFSIZE, "Windows 7 ");
    //			  }
    //			  else
    //			  {
    //				  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Windows Server 2008 R2 ");
    //			  }
    //			}

    //			pGPI = GetProductInfo;

    //			pGPI(osvi.dwMajorVersion, osvi.dwMinorVersion, 0, 0, dwType);

    //			switch (dwType)
    //			{
    //			case PRODUCT_ULTIMATE:
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Ultimate Edition");
    //			  break;
    //			case PRODUCT_PROFESSIONAL:
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Professional");
    //			  break;
    //			case PRODUCT_HOME_PREMIUM:
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Home Premium Edition");
    //			  break;
    //			case PRODUCT_HOME_BASIC:
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Home Basic Edition");
    //			  break;
    //			case PRODUCT_ENTERPRISE:
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Enterprise Edition");
    //			  break;
    //			case PRODUCT_BUSINESS:
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Business Edition");
    //			  break;
    //			case PRODUCT_STARTER:
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Starter Edition");
    //			  break;
    //			case PRODUCT_CLUSTER_SERVER:
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Cluster Server Edition");
    //			  break;
    //			case PRODUCT_DATACENTER_SERVER:
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Datacenter Edition");
    //			  break;
    //			case PRODUCT_DATACENTER_SERVER_CORE:
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Datacenter Edition (core installation)");
    //			  break;
    //			case PRODUCT_ENTERPRISE_SERVER:
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Enterprise Edition");
    //			  break;
    //			case PRODUCT_ENTERPRISE_SERVER_CORE:
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Enterprise Edition (core installation)");
    //			  break;
    //			case PRODUCT_ENTERPRISE_SERVER_IA64:
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Enterprise Edition for Itanium-based Systems");
    //			  break;
    //			case PRODUCT_SMALLBUSINESS_SERVER:
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Small Business Server");
    //			  break;
    //			case PRODUCT_SMALLBUSINESS_SERVER_PREMIUM:
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Small Business Server Premium Edition");
    //			  break;
    //			case PRODUCT_STANDARD_SERVER:
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Standard Edition");
    //			  break;
    //			case PRODUCT_STANDARD_SERVER_CORE:
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Standard Edition (core installation)");
    //			  break;
    //			case PRODUCT_WEB_SERVER:
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Web Server Edition");
    //			  break;
    //			}
    //		  }

    //		  if (osvi.dwMajorVersion == 5 && osvi.dwMinorVersion == 2)
    //		  {
    //			if (GetSystemMetrics(SM_SERVERR2))
    //			{
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Windows Server 2003 R2, ");
    //			}
    //			else if (osvi.wSuiteMask & VER_SUITE_STORAGE_SERVER)
    //			{
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Windows Storage Server 2003");
    //			}
    //			else if (osvi.wSuiteMask & VER_SUITE_WH_SERVER)
    //			{
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Windows Home Server");
    //			}
    //			else if (osvi.wProductType == VER_NT_WORKSTATION && si.wProcessorArchitecture == PROCESSOR_ARCHITECTURE_AMD64)
    //			{
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Windows XP Professional x64 Edition");
    //			}
    //			else
    //			{
    //				StringCchCat(pszOS, DefineConstants.BUFSIZE, "Windows Server 2003, ");
    //			}

    //			// Test for the server type.
    //			if (osvi.wProductType != VER_NT_WORKSTATION)
    //			{
    //			  if (si.wProcessorArchitecture == PROCESSOR_ARCHITECTURE_IA64)
    //			  {
    //				if ((osvi.wSuiteMask & VER_SUITE_DATACENTER) != 0)
    //				{
    //				  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Datacenter Edition for Itanium-based Systems");
    //				}
    //				else if (osvi.wSuiteMask & VER_SUITE_ENTERPRISE)
    //				{
    //				  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Enterprise Edition for Itanium-based Systems");
    //				}
    //			  }

    //			  else if (si.wProcessorArchitecture == PROCESSOR_ARCHITECTURE_AMD64)
    //			  {
    //				if ((osvi.wSuiteMask & VER_SUITE_DATACENTER) != 0)
    //				{
    //				  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Datacenter x64 Edition");
    //				}
    //				else if (osvi.wSuiteMask & VER_SUITE_ENTERPRISE)
    //				{
    //				  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Enterprise x64 Edition");
    //				}
    //				else
    //				{
    //					StringCchCat(pszOS, DefineConstants.BUFSIZE, "Standard x64 Edition");
    //				}
    //			  }

    //			  else
    //			  {
    //				if ((osvi.wSuiteMask & VER_SUITE_COMPUTE_SERVER) != 0)
    //				{
    //				  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Compute Cluster Edition");
    //				}
    //				else if (osvi.wSuiteMask & VER_SUITE_DATACENTER)
    //				{
    //				  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Datacenter Edition");
    //				}
    //				else if (osvi.wSuiteMask & VER_SUITE_ENTERPRISE)
    //				{
    //				  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Enterprise Edition");
    //				}
    //				else if (osvi.wSuiteMask & VER_SUITE_BLADE)
    //				{
    //				  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Web Edition");
    //				}
    //				else
    //				{
    //					StringCchCat(pszOS, DefineConstants.BUFSIZE, "Standard Edition");
    //				}
    //			  }
    //			}
    //		  }

    //		  if (osvi.dwMajorVersion == 5 && osvi.dwMinorVersion == 1)
    //		  {
    //			StringCchCat(pszOS, DefineConstants.BUFSIZE, "Windows XP ");
    //			if ((osvi.wSuiteMask & VER_SUITE_PERSONAL) != 0)
    //			{
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Home Edition");
    //			}
    //			else
    //			{
    //				StringCchCat(pszOS, DefineConstants.BUFSIZE, "Professional");
    //			}
    //		  }

    //		  if (osvi.dwMajorVersion == 5 && osvi.dwMinorVersion == 0)
    //		  {
    //			StringCchCat(pszOS, DefineConstants.BUFSIZE, "Windows 2000 ");

    //			if (osvi.wProductType == VER_NT_WORKSTATION)
    //			{
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Professional");
    //			}
    //			else
    //			{
    //			  if ((osvi.wSuiteMask & VER_SUITE_DATACENTER) != 0)
    //			  {
    //				StringCchCat(pszOS, DefineConstants.BUFSIZE, "Datacenter Server");
    //			  }
    //			  else if (osvi.wSuiteMask & VER_SUITE_ENTERPRISE)
    //			  {
    //				StringCchCat(pszOS, DefineConstants.BUFSIZE, "Advanced Server");
    //			  }
    //			  else
    //			  {
    //				  StringCchCat(pszOS, DefineConstants.BUFSIZE, "Server");
    //			  }
    //			}
    //		  }

    //		  // Include service pack (if any) and build number.

    //		  if (osvi.szCSDVersion.Length > 0)
    //		  {
    //			StringCchCat(pszOS, DefineConstants.BUFSIZE, " ");
    //			StringCchCat(pszOS, DefineConstants.BUFSIZE, osvi.szCSDVersion);
    //		  }

    //		  string buf = new string(new char[80]);

    //		  StringCchPrintf(buf, 80, " (build %d)", osvi.dwBuildNumber);
    //		  StringCchCat(pszOS, DefineConstants.BUFSIZE, buf);

    //		  if (osvi.dwMajorVersion >= 6)
    //		  {
    //			if (si.wProcessorArchitecture == PROCESSOR_ARCHITECTURE_AMD64)
    //			{
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, ", 64-bit");
    //			}
    //			else if (si.wProcessorArchitecture == PROCESSOR_ARCHITECTURE_INTEL)
    //			{
    //			  StringCchCat(pszOS, DefineConstants.BUFSIZE, ", 32-bit");
    //			}
    //		  }

    //		  return pszOS;
    //		}
    //		else
    //		{
    //		  Console.Write("This sample does not support this version of Windows.\n");
    //		  return pszOS;
    //		}
    //	  }

    //	  public delegate void PGNSI(LPSYSTEM_INFO UnnamedParameter);

    //	  public delegate int PGPI(uint UnnamedParameter, uint UnnamedParameter2, uint UnnamedParameter3, uint UnnamedParameter4, ref uint UnnamedParameter5);
    //	#else
    //	public static string get_nix_version_display_string()
    //	{
    //	  utsname un = new utsname();

    //	  if (uname(un) < 0)
    //	  {
    //		return "*nix: failed to get os version";
    //	  }
    //	  return string() + un.sysname + " " + un.version + " " + un.release;
    //	}
    //	#endif



    //	#if WIN32
    //	  public static string get_special_folder_path(int nfolder, bool iscreate)
    //	  {
    //		namespace fs = boost::filesystem;
    //		const string psz_path = "";

    //		if (SHGetSpecialFolderPathA(null, psz_path, nfolder, iscreate))
    //		{
    //		  return psz_path;
    //		}

    //		return "";
    //	  }
    //	#endif
    //		public static bool is_cin_tty()
    //		{
    //		  return 0 != _isatty(_fileno(stdin));
    //		}
    //		public static bool is_cin_tty()
    //		{
    //		  return 0 != isatty(fileno(stdin));
    //		}

    //		public static int getch()
    //		{
    //		  termios tty_old = new termios();
    //		  tcgetattr(STDIN_FILENO, tty_old);

    //		  termios tty_new = new termios();
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: tty_new = tty_old;
    //		  tty_new.CopyFrom(tty_old);
    //		  tty_new.c_lflag &= ~(ICANON | ECHO);
    //		  tcsetattr(STDIN_FILENO, TCSANOW, tty_new);

    //		  int ch = Console.Read();

    //		  tcsetattr(STDIN_FILENO, TCSANOW, tty_old);

    //		  return ch;
    //		}
    ////C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
    //		public static std::enable_if<std::is_integral<T>.value && std::is_unsigned<T>.value>.type write_varint<OutputIt, T>(OutputIt && dest, T i)
    //		{
    //			while (i >= 0x80)
    //			{
    //				*dest++= ((char)i & 0x7f) | 0x80;
    //				i >>= 7;
    //			}
    //			*dest++= (char)i;
    //		}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename t_type>
    //		public static string get_varint_data<t_type>(t_type v)
    //		{
    //		  std::stringstream ss = new std::stringstream();
    //		  write_varint(std::ostreambuf_iterator<char>(ss), v);
    //		  return ss.str();
    //		}

    ////C++ TO C# CONVERTER TODO TASK: C++ template specifiers with non-type parameters cannot be converted to C#:
    ////ORIGINAL LINE: template<int bits, typename InputIt, typename T>
    ////C++ TO C# CONVERTER TODO TASK: The following method format was not recognized, possibly due to an unrecognized macro:
    //		typename std::enable_if < std::is_integral<T>.value && std::is_unsigned<T>.value && 0 <= bits && bits <= std::numeric_limits<T>.digits, int>.type read_varint(InputIt && first, InputIt && last, T & i)
    //		{
    //			int read = 0;
    //			i = 0;
    //			for (int shift = 0;; shift += 7)
    //			{
    //				if (first == last)
    //				{
    //					return read; // End of input.
    //				}
    //				byte @byte = first++;
    //				++read;
    //				if (shift + 7 >= bits && @byte >= 1 << (bits - shift))
    //				{
    //					return -1; // Overflow.
    //				}
    //				if (@byte == 0 && shift != 0)
    //				{
    //					return -2; // Non-canonical representation.
    //				}
    //				i |= (T)(byte & 0x7f) << shift;
    //				if ((byte & 0x80) == 0)
    //				{
    //					break;
    //				}
    //			}
    //			return read;
    //		}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename InputIt, typename T>
    ////C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
    //		public static int read_varint<InputIt, T>(InputIt && first, InputIt && last, T i)
    //		{
    //			return read_varint<numeric_limits<T>.digits, InputIt, T>(std::move(first), std::move(last), i);
    //		}
    //	}
    //}

    //namespace Crypto
    //{
    //	public static class GlobalMembers
    //	{
    ////C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on the parameter 'cipher', so pointers on this parameter are left unchanged:
    //		public static void chacha8(object data, uint length, ushort key, ushort iv, char * cipher)
    //		{
    //		  uint x0 = new uint();
    //		  uint x1 = new uint();
    //		  uint x2 = new uint();
    //		  uint x3 = new uint();
    //		  uint x4 = new uint();
    //		  uint x5 = new uint();
    //		  uint x6 = new uint();
    //		  uint x7 = new uint();
    //		  uint x8 = new uint();
    //		  uint x9 = new uint();
    //		  uint x10 = new uint();
    //		  uint x11 = new uint();
    //		  uint x12 = new uint();
    //		  uint x13 = new uint();
    //		  uint x14 = new uint();
    //		  uint x15 = new uint();
    //		  uint j0 = new uint();
    //		  uint j1 = new uint();
    //		  uint j2 = new uint();
    //		  uint j3 = new uint();
    //		  uint j4 = new uint();
    //		  uint j5 = new uint();
    //		  uint j6 = new uint();
    //		  uint j7 = new uint();
    //		  uint j8 = new uint();
    //		  uint j9 = new uint();
    //		  uint j10 = new uint();
    //		  uint j11 = new uint();
    //		  uint j12 = new uint();
    //		  uint j13 = new uint();
    //		  uint j14 = new uint();
    //		  uint j15 = new uint();
    //		  string ctarget = null;
    //		  string tmp = new string(new char[64]);
    //		  int i;

    //		  if (length == null)
    //		  {
    //			  return;
    //		  }

    //		  j0 = SWAP32LE(((uint)(GlobalMembers.sigma.Substring(0)))[0]);
    //		  j1 = SWAP32LE(((uint)(GlobalMembers.sigma.Substring(4)))[0]);
    //		  j2 = SWAP32LE(((uint)(GlobalMembers.sigma.Substring(8)))[0]);
    //		  j3 = SWAP32LE(((uint)(GlobalMembers.sigma.Substring(12)))[0]);
    //		  j4 = SWAP32LE(((uint)(key + 0))[0]);
    //		  j5 = SWAP32LE(((uint)(key + 4))[0]);
    //		  j6 = SWAP32LE(((uint)(key + 8))[0]);
    //		  j7 = SWAP32LE(((uint)(key + 12))[0]);
    //		  j8 = SWAP32LE(((uint)(key + 16))[0]);
    //		  j9 = SWAP32LE(((uint)(key + 20))[0]);
    //		  j10 = SWAP32LE(((uint)(key + 24))[0]);
    //		  j11 = SWAP32LE(((uint)(key + 28))[0]);
    //		  j12 = 0;
    //		  j13 = 0;
    //		  j14 = SWAP32LE(((uint)(iv + 0))[0]);
    //		  j15 = SWAP32LE(((uint)(iv + 4))[0]);

    //		  for (;;)
    //		  {
    //			if (length < 64)
    //			{
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
    //			  memcpy(tmp, data, length);
    //			  data = tmp;
    //			  ctarget = cipher;
    //			  cipher = tmp;
    //			}
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x0 = j0;
    //			x0.CopyFrom(j0);
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x1 = j1;
    //			x1.CopyFrom(j1);
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x2 = j2;
    //			x2.CopyFrom(j2);
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x3 = j3;
    //			x3.CopyFrom(j3);
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x4 = j4;
    //			x4.CopyFrom(j4);
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x5 = j5;
    //			x5.CopyFrom(j5);
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x6 = j6;
    //			x6.CopyFrom(j6);
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x7 = j7;
    //			x7.CopyFrom(j7);
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x8 = j8;
    //			x8.CopyFrom(j8);
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x9 = j9;
    //			x9.CopyFrom(j9);
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x10 = j10;
    //			x10.CopyFrom(j10);
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x11 = j11;
    //			x11.CopyFrom(j11);
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x12 = j12;
    //			x12.CopyFrom(j12);
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x13 = j13;
    //			x13.CopyFrom(j13);
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x14 = j14;
    //			x14.CopyFrom(j14);
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x15 = j15;
    //			x15.CopyFrom(j15);
    //			for (i = 8;i > 0;i -= 2)
    //			{
    //			  x0 = (((uint)((x0) + (x4)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x12 = (rol32(((x12) ^ (x0)),16));
    //			  x12.CopyFrom(GlobalMembers.rol32(((x12) ^ (x0)), 16));
    //			  x8 = (((uint)((x8) + (x12)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x4 = (rol32(((x4) ^ (x8)),12));
    //			  x4.CopyFrom(GlobalMembers.rol32(((x4) ^ (x8)), 12));
    //			  x0 = (((uint)((x0) + (x4)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x12 = (rol32(((x12) ^ (x0)),8));
    //			  x12.CopyFrom(GlobalMembers.rol32(((x12) ^ (x0)), 8));
    //			  x8 = (((uint)((x8) + (x12)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x4 = (rol32(((x4) ^ (x8)),7));
    //			  x4.CopyFrom(GlobalMembers.rol32(((x4) ^ (x8)), 7));
    //			  x1 = (((uint)((x1) + (x5)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x13 = (rol32(((x13) ^ (x1)),16));
    //			  x13.CopyFrom(GlobalMembers.rol32(((x13) ^ (x1)), 16));
    //			  x9 = (((uint)((x9) + (x13)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x5 = (rol32(((x5) ^ (x9)),12));
    //			  x5.CopyFrom(GlobalMembers.rol32(((x5) ^ (x9)), 12));
    //			  x1 = (((uint)((x1) + (x5)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x13 = (rol32(((x13) ^ (x1)),8));
    //			  x13.CopyFrom(GlobalMembers.rol32(((x13) ^ (x1)), 8));
    //			  x9 = (((uint)((x9) + (x13)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x5 = (rol32(((x5) ^ (x9)),7));
    //			  x5.CopyFrom(GlobalMembers.rol32(((x5) ^ (x9)), 7));
    //			  x2 = (((uint)((x2) + (x6)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x14 = (rol32(((x14) ^ (x2)),16));
    //			  x14.CopyFrom(GlobalMembers.rol32(((x14) ^ (x2)), 16));
    //			  x10 = (((uint)((x10) + (x14)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x6 = (rol32(((x6) ^ (x10)),12));
    //			  x6.CopyFrom(GlobalMembers.rol32(((x6) ^ (x10)), 12));
    //			  x2 = (((uint)((x2) + (x6)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x14 = (rol32(((x14) ^ (x2)),8));
    //			  x14.CopyFrom(GlobalMembers.rol32(((x14) ^ (x2)), 8));
    //			  x10 = (((uint)((x10) + (x14)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x6 = (rol32(((x6) ^ (x10)),7));
    //			  x6.CopyFrom(GlobalMembers.rol32(((x6) ^ (x10)), 7));
    //			  x3 = (((uint)((x3) + (x7)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x15 = (rol32(((x15) ^ (x3)),16));
    //			  x15.CopyFrom(GlobalMembers.rol32(((x15) ^ (x3)), 16));
    //			  x11 = (((uint)((x11) + (x15)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x7 = (rol32(((x7) ^ (x11)),12));
    //			  x7.CopyFrom(GlobalMembers.rol32(((x7) ^ (x11)), 12));
    //			  x3 = (((uint)((x3) + (x7)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x15 = (rol32(((x15) ^ (x3)),8));
    //			  x15.CopyFrom(GlobalMembers.rol32(((x15) ^ (x3)), 8));
    //			  x11 = (((uint)((x11) + (x15)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x7 = (rol32(((x7) ^ (x11)),7));
    //			  x7.CopyFrom(GlobalMembers.rol32(((x7) ^ (x11)), 7));
    //			  x0 = (((uint)((x0) + (x5)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x15 = (rol32(((x15) ^ (x0)),16));
    //			  x15.CopyFrom(GlobalMembers.rol32(((x15) ^ (x0)), 16));
    //			  x10 = (((uint)((x10) + (x15)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x5 = (rol32(((x5) ^ (x10)),12));
    //			  x5.CopyFrom(GlobalMembers.rol32(((x5) ^ (x10)), 12));
    //			  x0 = (((uint)((x0) + (x5)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x15 = (rol32(((x15) ^ (x0)),8));
    //			  x15.CopyFrom(GlobalMembers.rol32(((x15) ^ (x0)), 8));
    //			  x10 = (((uint)((x10) + (x15)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x5 = (rol32(((x5) ^ (x10)),7));
    //			  x5.CopyFrom(GlobalMembers.rol32(((x5) ^ (x10)), 7));
    //			  x1 = (((uint)((x1) + (x6)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x12 = (rol32(((x12) ^ (x1)),16));
    //			  x12.CopyFrom(GlobalMembers.rol32(((x12) ^ (x1)), 16));
    //			  x11 = (((uint)((x11) + (x12)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x6 = (rol32(((x6) ^ (x11)),12));
    //			  x6.CopyFrom(GlobalMembers.rol32(((x6) ^ (x11)), 12));
    //			  x1 = (((uint)((x1) + (x6)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x12 = (rol32(((x12) ^ (x1)),8));
    //			  x12.CopyFrom(GlobalMembers.rol32(((x12) ^ (x1)), 8));
    //			  x11 = (((uint)((x11) + (x12)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x6 = (rol32(((x6) ^ (x11)),7));
    //			  x6.CopyFrom(GlobalMembers.rol32(((x6) ^ (x11)), 7));
    //			  x2 = (((uint)((x2) + (x7)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x13 = (rol32(((x13) ^ (x2)),16));
    //			  x13.CopyFrom(GlobalMembers.rol32(((x13) ^ (x2)), 16));
    //			  x8 = (((uint)((x8) + (x13)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x7 = (rol32(((x7) ^ (x8)),12));
    //			  x7.CopyFrom(GlobalMembers.rol32(((x7) ^ (x8)), 12));
    //			  x2 = (((uint)((x2) + (x7)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x13 = (rol32(((x13) ^ (x2)),8));
    //			  x13.CopyFrom(GlobalMembers.rol32(((x13) ^ (x2)), 8));
    //			  x8 = (((uint)((x8) + (x13)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x7 = (rol32(((x7) ^ (x8)),7));
    //			  x7.CopyFrom(GlobalMembers.rol32(((x7) ^ (x8)), 7));
    //			  x3 = (((uint)((x3) + (x4)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x14 = (rol32(((x14) ^ (x3)),16));
    //			  x14.CopyFrom(GlobalMembers.rol32(((x14) ^ (x3)), 16));
    //			  x9 = (((uint)((x9) + (x14)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x4 = (rol32(((x4) ^ (x9)),12));
    //			  x4.CopyFrom(GlobalMembers.rol32(((x4) ^ (x9)), 12));
    //			  x3 = (((uint)((x3) + (x4)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x14 = (rol32(((x14) ^ (x3)),8));
    //			  x14.CopyFrom(GlobalMembers.rol32(((x14) ^ (x3)), 8));
    //			  x9 = (((uint)((x9) + (x14)) & UINT32_C(0xFFFFFFFF)));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x4 = (rol32(((x4) ^ (x9)),7));
    //			  x4.CopyFrom(GlobalMembers.rol32(((x4) ^ (x9)), 7));
    //			}
    //			x0 = (((uint)((x0) + (j0)) & UINT32_C(0xFFFFFFFF)));
    //			x1 = (((uint)((x1) + (j1)) & UINT32_C(0xFFFFFFFF)));
    //			x2 = (((uint)((x2) + (j2)) & UINT32_C(0xFFFFFFFF)));
    //			x3 = (((uint)((x3) + (j3)) & UINT32_C(0xFFFFFFFF)));
    //			x4 = (((uint)((x4) + (j4)) & UINT32_C(0xFFFFFFFF)));
    //			x5 = (((uint)((x5) + (j5)) & UINT32_C(0xFFFFFFFF)));
    //			x6 = (((uint)((x6) + (j6)) & UINT32_C(0xFFFFFFFF)));
    //			x7 = (((uint)((x7) + (j7)) & UINT32_C(0xFFFFFFFF)));
    //			x8 = (((uint)((x8) + (j8)) & UINT32_C(0xFFFFFFFF)));
    //			x9 = (((uint)((x9) + (j9)) & UINT32_C(0xFFFFFFFF)));
    //			x10 = (((uint)((x10) + (j10)) & UINT32_C(0xFFFFFFFF)));
    //			x11 = (((uint)((x11) + (j11)) & UINT32_C(0xFFFFFFFF)));
    //			x12 = (((uint)((x12) + (j12)) & UINT32_C(0xFFFFFFFF)));
    //			x13 = (((uint)((x13) + (j13)) & UINT32_C(0xFFFFFFFF)));
    //			x14 = (((uint)((x14) + (j14)) & UINT32_C(0xFFFFFFFF)));
    //			x15 = (((uint)((x15) + (j15)) & UINT32_C(0xFFFFFFFF)));

    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x0 = ((x0) ^ (SWAP32LE(((uint*)((ushort*)data + 0))[0])));
    //			x0.CopyFrom(((x0) ^ (SWAP32LE(((uint)((ushort)data + 0))[0]))));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x1 = ((x1) ^ (SWAP32LE(((uint*)((ushort*)data + 4))[0])));
    //			x1.CopyFrom(((x1) ^ (SWAP32LE(((uint)((ushort)data + 4))[0]))));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x2 = ((x2) ^ (SWAP32LE(((uint*)((ushort*)data + 8))[0])));
    //			x2.CopyFrom(((x2) ^ (SWAP32LE(((uint)((ushort)data + 8))[0]))));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x3 = ((x3) ^ (SWAP32LE(((uint*)((ushort*)data + 12))[0])));
    //			x3.CopyFrom(((x3) ^ (SWAP32LE(((uint)((ushort)data + 12))[0]))));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x4 = ((x4) ^ (SWAP32LE(((uint*)((ushort*)data + 16))[0])));
    //			x4.CopyFrom(((x4) ^ (SWAP32LE(((uint)((ushort)data + 16))[0]))));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x5 = ((x5) ^ (SWAP32LE(((uint*)((ushort*)data + 20))[0])));
    //			x5.CopyFrom(((x5) ^ (SWAP32LE(((uint)((ushort)data + 20))[0]))));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x6 = ((x6) ^ (SWAP32LE(((uint*)((ushort*)data + 24))[0])));
    //			x6.CopyFrom(((x6) ^ (SWAP32LE(((uint)((ushort)data + 24))[0]))));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x7 = ((x7) ^ (SWAP32LE(((uint*)((ushort*)data + 28))[0])));
    //			x7.CopyFrom(((x7) ^ (SWAP32LE(((uint)((ushort)data + 28))[0]))));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x8 = ((x8) ^ (SWAP32LE(((uint*)((ushort*)data + 32))[0])));
    //			x8.CopyFrom(((x8) ^ (SWAP32LE(((uint)((ushort)data + 32))[0]))));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x9 = ((x9) ^ (SWAP32LE(((uint*)((ushort*)data + 36))[0])));
    //			x9.CopyFrom(((x9) ^ (SWAP32LE(((uint)((ushort)data + 36))[0]))));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x10 = ((x10) ^ (SWAP32LE(((uint*)((ushort*)data + 40))[0])));
    //			x10.CopyFrom(((x10) ^ (SWAP32LE(((uint)((ushort)data + 40))[0]))));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x11 = ((x11) ^ (SWAP32LE(((uint*)((ushort*)data + 44))[0])));
    //			x11.CopyFrom(((x11) ^ (SWAP32LE(((uint)((ushort)data + 44))[0]))));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x12 = ((x12) ^ (SWAP32LE(((uint*)((ushort*)data + 48))[0])));
    //			x12.CopyFrom(((x12) ^ (SWAP32LE(((uint)((ushort)data + 48))[0]))));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x13 = ((x13) ^ (SWAP32LE(((uint*)((ushort*)data + 52))[0])));
    //			x13.CopyFrom(((x13) ^ (SWAP32LE(((uint)((ushort)data + 52))[0]))));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x14 = ((x14) ^ (SWAP32LE(((uint*)((ushort*)data + 56))[0])));
    //			x14.CopyFrom(((x14) ^ (SWAP32LE(((uint)((ushort)data + 56))[0]))));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: x15 = ((x15) ^ (SWAP32LE(((uint*)((ushort*)data + 60))[0])));
    //			x15.CopyFrom(((x15) ^ (SWAP32LE(((uint)((ushort)data + 60))[0]))));

    //			j12 = ((((uint)(((j12)) + (1)) & UINT32_C(0xFFFFFFFF))));
    //			if (j12 == null)
    //			{
    //			  j13 = ((((uint)(((j13)) + (1)) & UINT32_C(0xFFFFFFFF))));
    //			  /* stopping at 2^70 bytes per iv is user's responsibility */
    //			}

    //			(((uint)(cipher.Substring(0)))[0] = SWAP32LE(x0));
    //			(((uint)(cipher.Substring(4)))[0] = SWAP32LE(x1));
    //			(((uint)(cipher.Substring(8)))[0] = SWAP32LE(x2));
    //			(((uint)(cipher.Substring(12)))[0] = SWAP32LE(x3));
    //			(((uint)(cipher.Substring(16)))[0] = SWAP32LE(x4));
    //			(((uint)(cipher.Substring(20)))[0] = SWAP32LE(x5));
    //			(((uint)(cipher.Substring(24)))[0] = SWAP32LE(x6));
    //			(((uint)(cipher.Substring(28)))[0] = SWAP32LE(x7));
    //			(((uint)(cipher.Substring(32)))[0] = SWAP32LE(x8));
    //			(((uint)(cipher.Substring(36)))[0] = SWAP32LE(x9));
    //			(((uint)(cipher.Substring(40)))[0] = SWAP32LE(x10));
    //			(((uint)(cipher.Substring(44)))[0] = SWAP32LE(x11));
    //			(((uint)(cipher.Substring(48)))[0] = SWAP32LE(x12));
    //			(((uint)(cipher.Substring(52)))[0] = SWAP32LE(x13));
    //			(((uint)(cipher.Substring(56)))[0] = SWAP32LE(x14));
    //			(((uint)(cipher.Substring(60)))[0] = SWAP32LE(x15));

    //			if (length <= 64)
    //			{
    //			  if (length < 64)
    //			  {
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
    //				memcpy(ctarget, cipher, length);
    //			  }
    //			  return;
    //			}
    //			length -= 64;
    //			cipher += 64;
    //			data = (ushort)data + 64;
    //		  }
    //		}
    //	//C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
    //	//#pragma pack(pop)

    //	//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
    //	//  static_assert(sizeof(chacha8_key) == DefineConstants.CHACHA8_KEY_SIZE && sizeof(chacha8_iv) == DefineConstants.CHACHA8_IV_SIZE, "Invalid structure size");

    //	  public static void chacha8(object data, uint length, chacha8_key key, chacha8_iv iv, ref string cipher)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		chacha8(data, new uint(length), reinterpret_cast<const ushort>(key), reinterpret_cast<const ushort>(iv), ref cipher);
    //	  }

    //	  public static void generate_chacha8_key(string password, chacha8_key key)
    //	  {
    //	//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
    //	//	static_assert(sizeof(chacha8_key) <= sizeof(Hash), "Size of hash must be at least that of chacha8_key");
    //		Hash pwd_hash = new Hash();
    //		cn_slow_hash_v0(password.data(), password.Length, pwd_hash);
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
    //		memcpy(key, pwd_hash, sizeof(Crypto.chacha8_key));
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
    //		memset(pwd_hash, 0, sizeof(Hash));
    //	  }


    ////C++ TO C# CONVERTER NOTE: 'extern' variable declarations are not required in C#:
    //	//  extern object random_lock;
    //	  public static std::enable_if<std::is_pod<T>.value, T>.type rand<T>()
    //	  {
    //		std::remove_cv<T>.type res = new std::remove_cv<T>.type();
    //		lock (random_lock)
    //		{
    //			generate_random_bytes(sizeof(T), res);
    //		}
    //		return res;
    //	  }

    //	  /* Random number engine based on Crypto::rand()
    //	   */
    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T>

    //	  public static void derive_secret_key(KeyDerivation derivation, uint output_index, SecretKey @base, ushort prefix, uint prefixLength, SecretKey derived_key)
    //	  {
    //		crypto_ops.derive_secret_key(derivation, output_index, @base, prefix, prefixLength, derived_key);
    //	  }

    //	  public static void derive_secret_key(KeyDerivation derivation, uint output_index, SecretKey @base, SecretKey derived_key)
    //	  {
    //		crypto_ops.derive_secret_key(derivation, output_index, @base, derived_key);
    //	  }

    //	  public static void generate_ring_signature(Hash prefix_hash, KeyImage image, PublicKey[] pubs, uint pubs_count, SecretKey sec, uint sec_index, Signature sig)
    //	  {
    //		crypto_ops.generate_ring_signature(prefix_hash, image, pubs, pubs_count, sec, sec_index, sig);
    //	  }

    //	  /* Variants with vector<const PublicKey *> parameters.
    //	   */
    //	  public static void generate_ring_signature(Hash prefix_hash, KeyImage image, List<const PublicKey > pubs, SecretKey sec, uint sec_index, Signature sig)
    //	  {
    //		generate_ring_signature(prefix_hash, image, pubs.data(), pubs.Count, sec, new uint(sec_index), sig);
    //	  }
    //	  public static bool check_ring_signature(Hash prefix_hash, KeyImage image, List<const PublicKey > pubs, Signature sig, bool checkKeyImage)
    //	  {
    //		return check_ring_signature(prefix_hash, image, pubs.data(), pubs.Count, sig, checkKeyImage);
    //	  }
    //		public static bool operator == (PublicKey _v1, PublicKey _v2)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
    //			return memcmp(_v1, _v2, sizeof(PublicKey)) == 0;
    //		}
    //		public static bool operator != (PublicKey _v1, PublicKey _v2)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
    //			return memcmp(_v1, _v2, sizeof(PublicKey)) != 0;
    //		}
    //	//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
    //	//	static_assert(sizeof(uint) <= sizeof(PublicKey), "Size of " "PublicKey" " must be at least that of uint");
    //		public static uint hash_value(PublicKey _v)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //			return reinterpret_cast<const uint &>(_v);
    //		}
    //		public static bool operator == (KeyImage _v1, KeyImage _v2)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
    //			return memcmp(_v1, _v2, sizeof(KeyImage)) == 0;
    //		}
    //		public static bool operator != (KeyImage _v1, KeyImage _v2)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
    //			return memcmp(_v1, _v2, sizeof(KeyImage)) != 0;
    //		}
    //	//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
    //	//	static_assert(sizeof(uint) <= sizeof(KeyImage), "Size of " "KeyImage" " must be at least that of uint");
    //		public static uint hash_value(KeyImage _v)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //			return reinterpret_cast<const uint &>(_v);
    //		}
    //		public static bool operator == (Signature _v1, Signature _v2)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
    //			return memcmp(_v1, _v2, sizeof(Signature)) == 0;
    //		}
    //		public static bool operator != (Signature _v1, Signature _v2)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
    //			return memcmp(_v1, _v2, sizeof(Signature)) != 0;
    //		}
    //		public static bool operator == (SecretKey _v1, SecretKey _v2)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
    //			return memcmp(_v1, _v2, sizeof(SecretKey)) == 0;
    //		}
    //		public static bool operator != (SecretKey _v1, SecretKey _v2)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
    //			return memcmp(_v1, _v2, sizeof(SecretKey)) != 0;
    //		}


    //	  public static mutex random_lock = new mutex();

    //	  internal static void random_scalar(EllipticCurveScalar res)
    //	  {
    //		byte[] tmp = new byte[64];
    //		generate_random_bytes(64, tmp);
    //		sc_reduce(tmp);
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
    //		memcpy(res, tmp, 32);
    //	  }

    //	  internal static void hash_to_scalar(object data, uint length, EllipticCurveScalar res)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		cn_fast_hash(data, new uint(length), reinterpret_cast<Hash &>(res));
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		sc_reduce32(reinterpret_cast<byte>(res));
    //	  }

    //	  internal static void derivation_to_scalar(KeyDerivation derivation, uint output_index, EllipticCurveScalar res)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: C# does not allow declaring types within methods:
    //	//	struct
    //	//	{
    //	//	  KeyDerivation derivation;
    //	//	  char output_index[(sizeof(uint) * 8 + 6) / 7];
    //	//	}
    //	//	buf;
    //		string end = buf.output_index;
    //		buf.derivation = derivation;
    //		Tools.GlobalMembers.write_varint(end, new uint(output_index));
    //		Debug.Assert(end <= buf.output_index + sizeof (buf.output_index));
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		hash_to_scalar(buf, end - reinterpret_cast<char>(buf), res);
    //	  }

    //	  internal static void derivation_to_scalar(KeyDerivation derivation, uint output_index, ushort suffix, uint suffixLength, EllipticCurveScalar res)
    //	  {
    //		Debug.Assert(suffixLength <= 32);
    ////C++ TO C# CONVERTER TODO TASK: C# does not allow declaring types within methods:
    //	//	struct
    //	//	{
    //	//	  KeyDerivation derivation;
    //	//	  char output_index[(sizeof(uint) * 8 + 6) / 7 + 32];
    //	//	}
    //	//	buf;
    //		string end = buf.output_index;
    //		buf.derivation = derivation;
    //		Tools.GlobalMembers.write_varint(end, new uint(output_index));
    //		Debug.Assert(end <= buf.output_index + sizeof (buf.output_index));
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		uint bufSize = end - reinterpret_cast<char>(buf);
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
    //		memcpy(end, suffix, suffixLength);
    //		hash_to_scalar(buf, bufSize + suffixLength, res);
    //	  }

    //	  internal static void hash_to_ec(PublicKey key, ge_p3 res)
    //	  {
    //		Hash h = new Hash();
    //		ge_p2 point = new ge_p2();
    //		ge_p1p1 point2 = new ge_p1p1();
    //		cn_fast_hash(std::addressof(key), sizeof(PublicKey), h);
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		ge_fromfe_frombytes_vartime(point, reinterpret_cast<const byte>(h));
    //		ge_mul8(point2, point);
    //		ge_p1p1_to_p3(res, point2);
    //	  }

    //	#if _MSC_VER
    //	//C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
    //	//#pragma warning(disable: 4200)
    //	#endif

    //	  internal static uint rs_comm_size(uint pubs_count)
    //	  {
    //		return sizeof(rs_comm) + pubs_count * sizeof(Crypto.rs_comm.AnonymousClass);
    //	  }
    //	  public static std::ostream operator << (std::ostream o, Crypto.PublicKey v)
    //	  {
    //		  return GlobalMembers.print256(o, v);
    //	  }
    //	  public static std::ostream operator << (std::ostream o, Crypto.SecretKey v)
    //	  {
    //		  return GlobalMembers.print256(o, v);
    //	  }
    //	  public static std::ostream operator << (std::ostream o, Crypto.KeyDerivation v)
    //	  {
    //		  return GlobalMembers.print256(o, v);
    //	  }
    //	  public static std::ostream operator << (std::ostream o, Crypto.KeyImage v)
    //	  {
    //		  return GlobalMembers.print256(o, v);
    //	  }
    //	  public static std::ostream operator << (std::ostream o, Crypto.Signature v)
    //	  {
    //		  return GlobalMembers.print256(o, v);
    //	  }
    //	  public static std::ostream operator << (std::ostream o, Crypto.Hash v)
    //	  {
    //		  return GlobalMembers.print256(o, v);
    //	  }

    //	public static bool serialize(PublicKey pubKey, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  return GlobalMembers.serializePod(pubKey, new Common.StringView(name), serializer.functorMethod);
    //	}
    //	public static bool serialize(SecretKey secKey, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  return GlobalMembers.serializePod(secKey, new Common.StringView(name), serializer.functorMethod);
    //	}
    //	public static bool serialize(Hash h, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  return GlobalMembers.serializePod(h, new Common.StringView(name), serializer.functorMethod);
    //	}
    //	public static bool serialize(chacha8_iv chacha, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  return GlobalMembers.serializePod(chacha, new Common.StringView(name), serializer.functorMethod);
    //	}
    //	public static bool serialize(KeyImage keyImage, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  return GlobalMembers.serializePod(keyImage, new Common.StringView(name), serializer.functorMethod);
    //	}
    //	public static bool serialize(Signature sig, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  return GlobalMembers.serializePod(sig, new Common.StringView(name), serializer.functorMethod);
    //	}
    //	public static bool serialize(EllipticCurveScalar ecScalar, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  return GlobalMembers.serializePod(ecScalar, new Common.StringView(name), serializer.functorMethod);
    //	}
    //	public static bool serialize(EllipticCurvePoint ecPoint, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  return GlobalMembers.serializePod(ecPoint, new Common.StringView(name), serializer.functorMethod);
    //	}


    //	  /*
    //	    Cryptonight hash functions
    //	  */

    //	  public static void cn_fast_hash(object data, uint length, Hash hash)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		cn_fast_hash(data, length, reinterpret_cast<char>(hash));
    //	  }

    //	  public static Hash cn_fast_hash(object data, uint length)
    //	  {
    //		Hash h = new Hash();
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		cn_fast_hash(data, length, reinterpret_cast<char>(h));
    //		return h;
    //	  }

    //	  // Standard CryptoNight
    //	  public static void cn_slow_hash_v0(object data, uint length, Hash hash)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		cn_slow_hash(data, length, reinterpret_cast<char>(hash), 0, 0, 0, DefineConstants.CN_PAGE_SIZE, DefineConstants.CN_SCRATCHPAD, DefineConstants.CN_ITERATIONS);
    //	  }

    //	  public static void cn_slow_hash_v1(object data, uint length, Hash hash)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		cn_slow_hash(data, length, reinterpret_cast<char>(hash), 0, 1, 0, DefineConstants.CN_PAGE_SIZE, DefineConstants.CN_SCRATCHPAD, DefineConstants.CN_ITERATIONS);
    //	  }

    //	  public static void cn_slow_hash_v2(object data, uint length, Hash hash)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		cn_slow_hash(data, length, reinterpret_cast<char>(hash), 0, 2, 0, DefineConstants.CN_PAGE_SIZE, DefineConstants.CN_SCRATCHPAD, DefineConstants.CN_ITERATIONS);
    //	  }

    //	  // Standard CryptoNight Lite
    //	  public static void cn_lite_slow_hash_v0(object data, uint length, Hash hash)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		cn_slow_hash(data, length, reinterpret_cast<char>(hash), 1, 0, 0, DefineConstants.CN_LITE_PAGE_SIZE, DefineConstants.CN_LITE_SCRATCHPAD, DefineConstants.CN_LITE_ITERATIONS);
    //	  }

    //	  public static void cn_lite_slow_hash_v1(object data, uint length, Hash hash)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		cn_slow_hash(data, length, reinterpret_cast<char>(hash), 1, 1, 0, DefineConstants.CN_LITE_PAGE_SIZE, DefineConstants.CN_LITE_SCRATCHPAD, DefineConstants.CN_LITE_ITERATIONS);
    //	  }

    //	  public static void cn_lite_slow_hash_v2(object data, uint length, Hash hash)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		cn_slow_hash(data, length, reinterpret_cast<char>(hash), 1, 2, 0, DefineConstants.CN_LITE_PAGE_SIZE, DefineConstants.CN_LITE_SCRATCHPAD, DefineConstants.CN_LITE_ITERATIONS);
    //	  }

    //	  // CryptoNight Soft Shell
    //	  public static void cn_soft_shell_slow_hash_v0(object data, uint length, Hash hash, uint height)
    //	  {
    //		uint base_offset = (height % DefineConstants.CN_SOFT_SHELL_WINDOW);
    //		int offset = (height % (DefineConstants.CN_SOFT_SHELL_WINDOW * 2)) - (base_offset * 2);
    //		if (offset < 0)
    //		{
    //		  offset = base_offset;
    //		}

    //		uint scratchpad = DefineConstants.CN_SOFT_SHELL_MEMORY + ((uint)offset * (DefineConstants.CN_SOFT_SHELL_WINDOW / DefineConstants.CN_SOFT_SHELL_MULTIPLIER));
    //		uint iterations = (DefineConstants.CN_SOFT_SHELL_MEMORY / 2) + ((uint)offset * ((DefineConstants.CN_SOFT_SHELL_WINDOW / DefineConstants.CN_SOFT_SHELL_MULTIPLIER) / 2));
    //		uint pagesize = scratchpad;

    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		cn_slow_hash(data, length, reinterpret_cast<char>(hash), 1, 0, 0, pagesize, scratchpad, iterations);
    //	  }

    //	  public static void cn_soft_shell_slow_hash_v1(object data, uint length, Hash hash, uint height)
    //	  {
    //		uint base_offset = (height % DefineConstants.CN_SOFT_SHELL_WINDOW);
    //		int offset = (height % (DefineConstants.CN_SOFT_SHELL_WINDOW * 2)) - (base_offset * 2);
    //		if (offset < 0)
    //		{
    //		  offset = base_offset;
    //		}

    //		uint scratchpad = DefineConstants.CN_SOFT_SHELL_MEMORY + ((uint)offset * (DefineConstants.CN_SOFT_SHELL_WINDOW / DefineConstants.CN_SOFT_SHELL_MULTIPLIER));
    //		uint iterations = (DefineConstants.CN_SOFT_SHELL_MEMORY / 2) + ((uint)offset * ((DefineConstants.CN_SOFT_SHELL_WINDOW / DefineConstants.CN_SOFT_SHELL_MULTIPLIER) / 2));
    //		uint pagesize = scratchpad;

    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		cn_slow_hash(data, length, reinterpret_cast<char>(hash), 1, 1, 0, pagesize, scratchpad, iterations);
    //	  }

    //	  public static void cn_soft_shell_slow_hash_v2(object data, uint length, Hash hash, uint height)
    //	  {
    //		uint base_offset = (height % DefineConstants.CN_SOFT_SHELL_WINDOW);
    //		int offset = (height % (DefineConstants.CN_SOFT_SHELL_WINDOW * 2)) - (base_offset * 2);
    //		if (offset < 0)
    //		{
    //		  offset = base_offset;
    //		}

    //		uint scratchpad = DefineConstants.CN_SOFT_SHELL_MEMORY + ((uint)offset * (DefineConstants.CN_SOFT_SHELL_WINDOW / DefineConstants.CN_SOFT_SHELL_MULTIPLIER));
    //		uint iterations = (DefineConstants.CN_SOFT_SHELL_MEMORY / 2) + ((uint)offset * ((DefineConstants.CN_SOFT_SHELL_WINDOW / DefineConstants.CN_SOFT_SHELL_MULTIPLIER) / 2));
    //		uint pagesize = scratchpad;

    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		cn_slow_hash(data, length, reinterpret_cast<char>(hash), 1, 2, 0, pagesize, scratchpad, iterations);
    //	  }

    //	  public static void tree_hash(Hash hashes, uint count, Hash root_hash)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		tree_hash(reinterpret_cast<const char(*)[HASH_SIZE]>(hashes), count, reinterpret_cast<char>(root_hash));
    //	  }

    //	  public static void tree_branch(Hash hashes, uint count, Hash branch)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		tree_branch(reinterpret_cast<const char(*)[HASH_SIZE]>(hashes), count, reinterpret_cast<char(*)[HASH_SIZE]>(branch));
    //	  }

    //	  public static void tree_hash_from_branch(Hash branch, uint depth, Hash leaf, object path, Hash root_hash)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		tree_hash_from_branch(reinterpret_cast<const char(*)[HASH_SIZE]>(branch), depth, reinterpret_cast<const char>(leaf), path, reinterpret_cast<char>(root_hash));
    //	  }
    //	}
    //}

    //namespace CryptoNote.error
    //{
    //	public static class GlobalMembers
    //	{
    //	public static std::error_condition make_error_condition(AddBlockErrorCondition e)
    //	{
    //	  return std::error_condition((int)e, AddBlockErrorConditionCategory.INSTANCE);
    //	}

    //	public static std::error_code make_error_code(CryptoNote.error.AddBlockErrorCode e)
    //	{
    //	  return std::error_code((int)e, CryptoNote.error.AddBlockErrorCategory.INSTANCE);
    //	}

    //	public static std::error_code make_error_code(CryptoNote.error.BlockValidationError e)
    //	{
    //	  return std::error_code((int)e, CryptoNote.error.BlockValidationErrorCategory.INSTANCE);
    //	}

    //	public static std::error_code make_error_code(CryptoNote.error.CoreErrorCode e)
    //	{
    //	  return std::error_code((int)e, CryptoNote.error.CoreErrorCategory.INSTANCE);
    //	}

    //	public static std::error_code make_error_code(CryptoNote.error.TransactionValidationError e)
    //	{
    //	  return std::error_code((int)e, CryptoNote.error.TransactionValidationErrorCategory.INSTANCE);
    //	}
    //	}
    //}

    //namespace CryptoNote
    //{
    //	public static class GlobalMembers
    //	{
    //	public static bool serialize(PackedOutIndex value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  return serializer.functorMethod(value.packedValue, name);
    //	}

    ////C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
    //	//class DatabaseBlockchainCache;

    //	public static UseGenesis addGenesisBlock = new UseGenesis(true);
    //	public static UseGenesis skipGenesisBlock = new UseGenesis(false);

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <class T, class F>
    //	public static void splitGlobalIndexes<T, F>(T sourceContainer, T destinationContainer, uint splitBlockIndex, F lowerBoundFunction)
    //	{
    //	  for (var it = sourceContainer.begin(); it != sourceContainer.end();)
    //	  {
    //		var newCacheOutputsIteratorStart = lowerBoundFunction(it.second.outputs.begin(), it.second.outputs.end(), splitBlockIndex);

    //		auto indexesForAmount = destinationContainer[it.first];
    //		var newCacheOutputsCount = (uint)std::distance(newCacheOutputsIteratorStart, it.second.outputs.end());
    //		indexesForAmount.outputs.reserve(newCacheOutputsCount);

    //		indexesForAmount.startIndex = it.second.startIndex + (uint)it.second.outputs.size() - newCacheOutputsCount;

    //		std::move(newCacheOutputsIteratorStart, it.second.outputs.end(), std::back_inserter(indexesForAmount.outputs));
    //		it.second.outputs.erase(newCacheOutputsIteratorStart, it.second.outputs.end());

    //		if (indexesForAmount.outputs.empty())
    //		{
    //		  destinationContainer.erase(it.first);
    //		}

    //		if (it.second.outputs.empty())
    //		{
    //		  // if we gave all of our outputs we don't need this amount entry any more
    //		  it = sourceContainer.erase(it);
    //		}
    //		else
    //		{
    //		  ++it;
    //		}
    //	  }
    //	}

    //	// factory functions
    ////C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
    //	public static BlockchainMessage makeChainSwitchMessage(uint index, List<Crypto.Hash>&& hashes)
    //	{
    //	  return BlockchainMessage
    //	  {
    //		  Messages.ChainSwitch{index, std::move(hashes)}
    //	  };
    //	}
    //	public static BlockchainMessage makeNewAlternativeBlockMessage(uint index, Crypto.Hash hash)
    //	{
    //	  return BlockchainMessage
    //	  {
    //		  Messages.NewAlternativeBlock{index, std::move(hash)}
    //	  };
    //	}
    //	public static BlockchainMessage makeNewBlockMessage(uint index, Crypto.Hash hash)
    //	{
    //	  return BlockchainMessage
    //	  {
    //		  Messages.NewBlock{index, std::move(hash)}
    //	  };
    //	}
    ////C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
    //	public static BlockchainMessage makeAddTransactionMessage(List<Crypto.Hash>&& hashes)
    //	{
    //	  return BlockchainMessage
    //	  {
    //		  Messages.AddTransaction{std::move(hashes)}
    //	  };
    //	}
    ////C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
    //	public static BlockchainMessage makeDelTransactionMessage(List<Crypto.Hash>&& hashes, Messages.DeleteTransaction.Reason reason)
    //	{
    //	  return BlockchainMessage
    //	  {
    //		  Messages.DeleteTransaction{std::move(hashes), reason}
    //	  };
    //	}
    //	public static bool check_hash(Crypto.Hash hash, ulong difficulty)
    //	{
    //	  ulong low = new ulong();
    //	  ulong high = new ulong();
    //	  ulong top = new ulong();
    //	  ulong cur = new ulong();
    //	  // First check the highest word, this will most likely fail for a random hash.
    //	  mul(swap64le(((ulong) hash)[3]), new ulong(difficulty), ref top, ref high);
    //	  if (high != 0)
    //	  {
    //		return false;
    //	  }
    //	  mul(swap64le(((ulong) hash)[0]), new ulong(difficulty), ref low, ref cur);
    //	  mul(swap64le(((ulong) hash)[1]), new ulong(difficulty), ref low, ref high);
    //	  bool carry = cadd(new ulong(cur), new ulong(low));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: cur = high;
    //	  cur.CopyFrom(high);
    //	  mul(swap64le(((ulong) hash)[2]), new ulong(difficulty), ref low, ref high);
    //	  carry = cadc(new ulong(cur), new ulong(low), carry);
    //	  carry = cadc(new ulong(high), new ulong(top), carry);
    //	  return !carry;
    //	}

    //	#if __SIZEOF_INT128__

    //	  internal static void mul(ulong a, ulong b, ref ulong low, ref ulong high)
    //	  {
    //	//C++ TO C# CONVERTER TODO TASK: Typedefs defined in multiple preprocessor conditionals can only be replaced within the scope of the preprocessor conditional:
    //	//	typedef uint __int128 uint128_t;
    //		uint __int128 res = (uint __int128) a * (uint __int128) b;
    //		low = (ulong) res;
    //		high = (ulong)(res >> 64);
    //	  }

    //	#else

    //	  internal static void mul(ulong a, ulong b, ref ulong low, ulong high)
    //	  {
    //		low = GlobalMembers.mul128(new ulong(a), new ulong(b), high);
    //	  }

    //	#endif

    //	  internal static bool cadd(ulong a, ulong b)
    //	  {
    //		return a + b < a;
    //	  }

    //	  internal static bool cadc(ulong a, ulong b, bool c)
    //	  {
    //		return a + b < a || (c && a + b == (ulong) - 1);
    //	  }
    //	public static List<T> preallocateVector<T>(uint elements)
    //	{
    //	  List<T> vect = new List<T>();
    //	  vect.Capacity = elements;
    //	  return vect;
    //	}
    //	public static UseGenesis addGenesisBlock = new UseGenesis(true);

    //	public static IBlockchainCache findIndexInChain(IBlockchainCache blockSegment, Crypto.Hash blockHash)
    //	{
    //	  Debug.Assert(blockSegment != null);
    //	  while (blockSegment != null)
    //	  {
    //		if (blockSegment.hasBlock(blockHash))
    //		{
    //		  return blockSegment;
    //		}

    //		blockSegment = blockSegment.getParent();
    //	  }

    //	  return null;
    //	}

    //	public static IBlockchainCache findIndexInChain(IBlockchainCache blockSegment, uint blockIndex)
    //	{
    //	  Debug.Assert(blockSegment != null);
    //	  while (blockSegment != null)
    //	  {
    //		if (blockIndex >= blockSegment.getStartBlockIndex() != null && blockIndex < blockSegment.getStartBlockIndex() + blockSegment.getBlockCount())
    //		{
    //		  return blockSegment;
    //		}

    //		blockSegment = blockSegment.getParent();
    //	  }

    //	  return null;
    //	}

    //	public static uint getMaximumTransactionAllowedSize(uint blockSizeMedian, Currency currency)
    //	{
    //	  Debug.Assert(blockSizeMedian * 2 > currency.minerTxBlobReservedSize());

    //	  return blockSizeMedian * 2 - currency.minerTxBlobReservedSize();
    //	}

    //	public static BlockTemplate extractBlockTemplate(RawBlock block)
    //	{
    //	  BlockTemplate blockTemplate = new BlockTemplate();
    //	  if (!fromBinaryArray(ref blockTemplate, block.block))
    //	  {
    //		throw std::system_error(error.GlobalMembers.make_error_code(error.AddBlockErrorCode.DESERIALIZATION_FAILED));
    //	  }

    //	  return blockTemplate;
    //	}

    //	public static Crypto.Hash getBlockHash(RawBlock block)
    //	{
    //	  BlockTemplate blockTemplate = GlobalMembers.extractBlockTemplate(block);
    //	  return new CachedBlock(blockTemplate).getBlockHash();
    //	}

    //	public static TransactionValidatorState extractSpentOutputs(CachedTransaction transaction)
    //	{
    //	  TransactionValidatorState spentOutputs = new TransactionValidatorState();
    //	  auto cryptonoteTransaction = transaction.getTransaction();

    //	  foreach (var input in cryptonoteTransaction.inputs)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //		if (input.type() == typeid(KeyInput))
    //		{
    //		  KeyInput in = boost::get<KeyInput>(input);
    //		  bool r = spentOutputs.spentKeyImages.Add(in.keyImage).second;
    //		  if (r)
    //		  {
    //		  }
    //		  Debug.Assert(r);
    //		}
    //		else
    //		{
    //		  Debug.Assert(false);
    //		}
    //	  }

    //	  return spentOutputs;
    //	}

    //	public static TransactionValidatorState extractSpentOutputs(List<CachedTransaction> transactions)
    //	{
    //	  TransactionValidatorState resultOutputs = new TransactionValidatorState();
    //	  foreach (var transaction in transactions)
    //	  {
    //		var transactionOutputs = GlobalMembers.extractSpentOutputs(transaction);
    //		mergeStates(resultOutputs, transactionOutputs);
    //	  }

    //	  return resultOutputs;
    //	}

    //	public static long getEmissionChange(Currency currency, IBlockchainCache segment, uint previousBlockIndex, CachedBlock cachedBlock, ulong cumulativeSize, ulong cumulativeFee)
    //	{

    //	  ulong reward = 0;
    //	  long emissionChange = 0;
    //	  var alreadyGeneratedCoins = segment.getAlreadyGeneratedCoins(new uint(previousBlockIndex));
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
    ////ORIGINAL LINE: auto lastBlocksSizes = segment.getLastBlocksSizes(currency.rewardBlocksWindow(), previousBlockIndex, addGenesisBlock);
    //	  var lastBlocksSizes = segment.getLastBlocksSizes(currency.rewardBlocksWindow(), new uint(previousBlockIndex), new CryptoNote.UseGenesis(GlobalMembers.addGenesisBlock));
    //	  var blocksSizeMedian = Common.GlobalMembers.medianValue(lastBlocksSizes);
    //	  if (!currency.getBlockReward(new ushort(cachedBlock.getBlock().majorVersion), blocksSizeMedian, new ulong(cumulativeSize), new ulong(alreadyGeneratedCoins), new ulong(cumulativeFee), reward, emissionChange))
    //	  {
    //		throw std::system_error(error.GlobalMembers.make_error_code(error.BlockValidationError.CUMULATIVE_BLOCK_uintOO_BIG));
    //	  }

    //	  return emissionChange;
    //	}

    //	public static uint findCommonRoot(IMainChainStorage storage, IBlockchainCache rootSegment)
    //	{
    //	  Debug.Assert(storage.getBlockCount());
    //	  Debug.Assert(rootSegment.getBlockCount());
    //	  Debug.Assert(rootSegment.getStartBlockIndex() == 0);
    //	  Debug.Assert(GlobalMembers.getBlockHash(storage.getBlockByIndex(0)) == rootSegment.getBlockHash(0));

    //	  uint left = 0;
    //	  uint right = Math.Min(storage.getBlockCount() - 1, rootSegment.getBlockCount() - 1);
    //	  while (left != right)
    //	  {
    //		Debug.Assert(right >= left);
    //		uint checkElement = left + (right - left) / 2 + 1;
    //		if (GlobalMembers.getBlockHash(storage.getBlockByIndex(new uint(checkElement))) == rootSegment.getBlockHash(new uint(checkElement)))
    //		{
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: left = checkElement;
    //		  left.CopyFrom(checkElement);
    //		}
    //		else
    //		{
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: right = checkElement - 1;
    //		  right.CopyFrom(checkElement - 1);
    //		}
    //	  }

    //	  return left;
    //	}

    //	public static readonly std::chrono.seconds OUTDATED_TRANSACTION_POLLING_INTERVAL = new std::chrono.seconds(60);
    //	  public static readonly Crypto.Hash NULL_HASH = boost::value_initialized<Crypto.Hash>();
    //	  public static readonly Crypto.PublicKey NULL_PUBLIC_KEY = boost::value_initialized<Crypto.PublicKey>();
    //	  public static readonly Crypto.SecretKey NULL_SECRET_KEY = boost::value_initialized<Crypto.SecretKey>();

    //	  public static KeyPair generateKeyPair()
    //	  {
    //		KeyPair k = new KeyPair();
    //		Crypto.generate_keys(k.publicKey, k.secretKey);
    //		return k;
    //	  }

    //	  public static ParentBlockSerializer makeParentBlockSerializer(BlockTemplate b, bool hashingSerialization, bool headerOnly)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
    //		BlockTemplate blockRef = const_cast<BlockTemplate&>(b);
    //		return new ParentBlockSerializer(blockRef.parentBlock, blockRef.timestamp, blockRef.nonce, hashingSerialization, headerOnly);
    //	  }

    //  /************************************************************************/
    //  /* CryptoNote helper functions                                          */
    //  /************************************************************************/
    //  //-----------------------------------------------------------------------------------------------
    //	  /************************************************************************/
    //	  /* CryptoNote helper functions                                          */
    //	  /************************************************************************/
    //	  public static ulong getPenalizedAmount(ulong amount, uint medianSize, uint currentBlockSize)
    //	  {
    //	//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
    //	//	static_assert(sizeof(uint) >= sizeof(uint), "uint is too small");
    //		Debug.Assert(currentBlockSize <= 2 * medianSize);
    //		Debug.Assert(medianSize <= uint.MaxValue);
    //		Debug.Assert(currentBlockSize <= uint.MaxValue);

    //		if (amount == 0)
    //		{
    //		  return 0;
    //		}

    //		if (currentBlockSize <= medianSize)
    //		{
    //		  return amount;
    //		}

    //		ulong productHi = new ulong();
    //		ulong productLo = GlobalMembers.mul128(new ulong(amount), currentBlockSize * (UINT64_C(2) * medianSize - currentBlockSize), productHi);

    //		ulong penalizedAmountHi = new ulong();
    //		ulong penalizedAmountLo = new ulong();
    //		GlobalMembers.div128_32(new ulong(productHi), new ulong(productLo), (uint)medianSize, penalizedAmountHi, penalizedAmountLo);
    //		GlobalMembers.div128_32(new ulong(penalizedAmountHi), new ulong(penalizedAmountLo), (uint)medianSize, penalizedAmountHi, penalizedAmountLo);

    //		Debug.Assert(0 == penalizedAmountHi);
    //		Debug.Assert(penalizedAmountLo < amount);

    //		return penalizedAmountLo;
    //	  }
    //  //-----------------------------------------------------------------------
    //	  public static string getAccountAddressAsStr(ulong prefix, AccountPublicAddress adr)
    //	  {
    //		BinaryArray ba = new BinaryArray();
    //		bool r = toBinaryArray(adr, ref ba);
    //		if (r)
    //		{
    //		}
    //		Debug.Assert(r);
    //		return Tools.Base58.encode_addr(prefix, Common.asString(ba));
    //	  }
    //  //-----------------------------------------------------------------------
    //	  public static bool parseAccountAddressString(ulong prefix, AccountPublicAddress adr, string str)
    //	  {
    //		string data;

    //		return Tools.Base58.decode_addr(str, prefix, data) && fromBinaryArray(ref adr, asBinaryArray(data)) && check_key(adr.spendPublicKey) && check_key(adr.viewPublicKey);
    //	  }
    //  //-----------------------------------------------------------------------
    //	  public static bool is_coinbase(Transaction tx)
    //	  {
    //		if (tx.inputs.size() != 1)
    //		{
    //		  return false;
    //		}

    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //		if (tx.inputs[0].type() != typeid(BaseInput))
    //		{
    //		  return false;
    //		}

    //		return true;
    //	  }

    ////std::string print_money(ulong amount, unsigned int decimal_point)
    ////{
    ////	/*if (decimal_point == (unsigned int)-1)
    ////		decimal_point = default_decimal_point;*/
    ////	if (decimal_point == (unsigned int)-1)
    ////		decimal_point = 2;
    ////	std::string s = std::to_string(amount);
    ////	if (s.size() < decimal_point + 1)
    ////	{
    ////		s.insert(0, decimal_point + 1 - s.size(), '0');
    ////	}
    ////	if (decimal_point > 0)
    ////		s.insert(s.size() - decimal_point, ".");
    ////	return s;
    ////}

    //	//std::string print_money(ulong amount, unsigned int decimal_point = -1);

    //	public static bool parseAndValidateTransactionFromBinaryArray(BinaryArray tx_blob, Transaction tx, Hash tx_hash, Hash tx_prefix_hash)
    //	{
    //	  if (!fromBinaryArray(ref tx, tx_blob))
    //	  {
    //		return false;
    //	  }

    //	  //TODO: validate tx
    //	  Crypto.GlobalMembers.cn_fast_hash(tx_blob.data(), tx_blob.size(), tx_hash);
    //	  getObjectHash(*(TransactionPrefix)tx, ref tx_prefix_hash);
    //	  return true;
    //	}


    //	public static bool constructTransaction(AccountKeys sender_account_keys, List<TransactionSourceEntry> sources, List<TransactionDestinationEntry> destinations, List<ushort> extra, Transaction tx, ulong unlock_time, Logging.ILogger log)
    //	{
    //	  LoggerRef logger = new LoggerRef(log, "construct_tx");

    //	  tx.inputs.clear();
    //	  tx.outputs.clear();
    //	  tx.signatures.clear();

    //	  tx.version = CURRENT_TRANSACTION_VERSION;
    //	  tx.unlockTime = unlock_time;

    //	  tx.extra = extra;
    //	  KeyPair txkey = generateKeyPair();
    //	  addTransactionPublicKeyToExtra(tx.extra, txkey.publicKey);

    ////C++ TO C# CONVERTER TODO TASK: C# does not allow declaring types within methods:
    //	//  struct input_generation_context_data
    //	//  {
    //	//	KeyPair in_ephemeral;
    //	//  };

    //	  List<input_generation_context_data> in_contexts = new List<input_generation_context_data>();
    //	  ulong summary_inputs_money = 0;
    //	  //fill inputs
    //	  foreach (TransactionSourceEntry src_entr in sources)
    //	  {
    //		if (src_entr.realOutput >= src_entr.outputs.Count)
    //		{
    //		  logger(ERROR) << "real_output index (" << src_entr.realOutput << ")bigger than output_keys.size()=" << src_entr.outputs.Count;
    //		  return false;
    //		}
    //		summary_inputs_money += src_entr.amount;

    //		//KeyDerivation recv_derivation;
    //		in_contexts.Add(input_generation_context_data());
    //		KeyPair in_ephemeral = in_contexts[in_contexts.Count - 1].in_ephemeral;
    //		KeyImage img = new KeyImage();
    //		if (!generate_key_image_helper(sender_account_keys, src_entr.realTransactionPublicKey, new uint(src_entr.realOutputIndexInTransaction), in_ephemeral, img))
    //		{
    //		  return false;
    //		}

    //		//check that derived key is equal with real output key
    //		if (!(in_ephemeral.publicKey == src_entr.outputs[src_entr.realOutput].Item2))
    //		{
    //		  logger(ERROR) << "derived public key mismatch with output public key! " << ENDL << "derived_key:" << Common.GlobalMembers.podToHex(in_ephemeral.publicKey) << ENDL << "real output_public_key:" << Common.GlobalMembers.podToHex(src_entr.outputs[src_entr.realOutput].Item2);
    //		  return false;
    //		}

    //		//put key image into tx input
    //		KeyInput input_to_key = new KeyInput();
    //		input_to_key.amount = src_entr.amount;
    //		input_to_key.keyImage = img;

    //		//fill outputs array and use relative offsets
    //		foreach (Tuple <uint in :PublicKey>& out_entry : src_entr.outputs)
    //		Tuple Crypto = new Tuple();
    //		{
    //		  input_to_key.outputIndexes.push_back(out_entry.first);
    //		}

    //		input_to_key.outputIndexes = absolute_output_offsets_to_relative(input_to_key.outputIndexes);
    //		tx.inputs.push_back(input_to_key);
    //	  }

    //	  // "Shuffle" outs
    //	  List<TransactionDestinationEntry> shuffled_dsts = new List<TransactionDestinationEntry>(destinations);
    ////C++ TO C# CONVERTER TODO TASK: The 'Compare' parameter of std::sort produces a boolean value, while the .NET Comparison parameter produces a tri-state result:
    ////ORIGINAL LINE: std::sort(shuffled_dsts.begin(), shuffled_dsts.end(), [](const TransactionDestinationEntry& de1, const TransactionDestinationEntry& de2)
    //  shuffled_dsts.Sort((TransactionDestinationEntry de1, TransactionDestinationEntry de2) =>
    //  {
    //	  return de1.amount < de2.amount;
    //  });

    //	  ulong summary_outs_money = 0;
    //	  //fill outputs
    //	  uint output_index = 0;
    //	  foreach (TransactionDestinationEntry dst_entr in shuffled_dsts)
    //	  {
    //		if (!(dst_entr.amount > 0))
    //		{
    //		  logger(ERROR, BRIGHT_RED) << "Destination with wrong amount: " << dst_entr.amount;
    //		  return false;
    //		}
    //		KeyDerivation derivation = new KeyDerivation();
    //		PublicKey out_eph_public_key = new PublicKey();
    //		bool r = generate_key_derivation(dst_entr.addr.viewPublicKey, txkey.secretKey, derivation);

    //		if (!(r))
    //		{
    //		  logger(ERROR, BRIGHT_RED) << "at creation outs: failed to generate_key_derivation(" << dst_entr.addr.viewPublicKey << ", " << txkey.secretKey << ")";
    //		  return false;
    //		}

    //		r = derive_public_key(derivation, output_index, dst_entr.addr.spendPublicKey, out_eph_public_key);
    //		if (!(r))
    //		{
    //		  logger(ERROR, BRIGHT_RED) << "at creation outs: failed to derive_public_key(" << derivation << ", " << output_index << ", " << dst_entr.addr.spendPublicKey << ")";
    //		  return false;
    //		}

    //		TransactionOutput @out = new TransactionOutput();
    //		@out.amount = dst_entr.amount;
    //		KeyOutput tk = new KeyOutput();
    //		tk.key = out_eph_public_key;
    //		@out.target = tk;
    //		tx.outputs.push_back(@out);
    //		output_index++;
    //		summary_outs_money += dst_entr.amount;
    //	  }

    //	  //check money
    //	  if (summary_outs_money > summary_inputs_money)
    //	  {
    //		logger(ERROR) << "Transaction inputs money (" << summary_inputs_money << ") less than outputs money (" << summary_outs_money << ")";
    //		return false;
    //	  }

    //	  //generate ring signatures
    //	  Hash tx_prefix_hash = new Hash();
    //	  getObjectHash(*(TransactionPrefix)tx, ref tx_prefix_hash);

    //	  uint i = 0;
    //	  foreach (TransactionSourceEntry src_entr in sources)
    //	  {
    //		List<PublicKey> keys_ptrs = new List<PublicKey>();
    //		foreach (Tuple <uint in :PublicKey>& o : src_entr.outputs)
    //		Tuple Crypto = new Tuple();
    //		{
    //		  keys_ptrs.Add(o.second);
    //		}

    //		tx.signatures.push_back(new List<Signature>());
    //		List<Signature> sigs = tx.signatures.back();
    //		sigs.Resize(src_entr.outputs.Count);
    //		Crypto.GlobalMembers.generate_ring_signature(tx_prefix_hash, boost::get<KeyInput>(tx.inputs[i]).keyImage, keys_ptrs, in_contexts[i].in_ephemeral.secretKey, new uint(src_entr.realOutput), sigs.data());
    //		i++;
    //	  }

    //	  return true;
    //	}


    //	public static bool is_out_to_acc(AccountKeys acc, KeyOutput out_key, PublicKey tx_pub_key, uint keyIndex)
    //	{
    //	  KeyDerivation derivation = new KeyDerivation();
    //	  generate_key_derivation(tx_pub_key, acc.viewSecretKey, derivation);
    //	  return is_out_to_acc(acc, out_key, derivation, new uint(keyIndex));
    //	}
    //	public static bool is_out_to_acc(AccountKeys acc, KeyOutput out_key, KeyDerivation derivation, uint keyIndex)
    //	{
    //	  PublicKey pk = new PublicKey();
    //	  derive_public_key(derivation, keyIndex, acc.address.spendPublicKey, pk);
    //	  return pk == out_key.key;
    //	}
    //	public static bool lookup_acc_outs(AccountKeys acc, Transaction tx, PublicKey tx_pub_key, List<uint> outs, ref ulong money_transfered)
    //	{
    //	  money_transfered = 0;
    //	  uint keyIndex = 0;
    //	  uint outputIndex = 0;

    //	  KeyDerivation derivation = new KeyDerivation();
    //	  generate_key_derivation(tx_pub_key, acc.viewSecretKey, derivation);

    //	  foreach (TransactionOutput o in tx.outputs)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //		Debug.Assert(o.target.type() == typeid(KeyOutput));
    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //		if (o.target.type() == typeid(KeyOutput))
    //		{
    //		  if (is_out_to_acc(acc, boost::get<KeyOutput>(o.target), derivation, new uint(keyIndex)))
    //		  {
    //			outs.Add(outputIndex);
    //			money_transfered += o.amount;
    //		  }

    //		  ++keyIndex;
    //		}

    //		++outputIndex;
    //	  }
    //	  return true;
    //	}
    //	public static bool lookup_acc_outs(AccountKeys acc, Transaction tx, List<uint> outs, ulong money_transfered)
    //	{
    //	  PublicKey transactionPublicKey = getTransactionPublicKeyFromExtra(tx.extra);
    //	  if (transactionPublicKey == NULL_PUBLIC_KEY)
    //	  {
    //		return false;
    //	  }
    //	  return lookup_acc_outs(acc, tx, transactionPublicKey, outs, ref money_transfered);
    //	}
    //	public static bool get_tx_fee(Transaction tx, ref ulong fee)
    //	{
    //	  ulong amount_in = 0;
    //	  ulong amount_out = 0;

    //	  foreach (var in in tx.inputs)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //		if (in.type() == typeid(KeyInput))
    //		{
    //		  amount_in += boost::get<KeyInput>(in).amount;
    //		}
    //	  }

    //	  foreach (var o in tx.outputs)
    //	  {
    //		amount_out += o.amount;
    //	  }

    //	  if (!(amount_in >= amount_out))
    //	  {
    //		return false;
    //	  }

    //	  fee = amount_in - amount_out;
    //	  return true;
    //	}
    //	public static ulong get_tx_fee(Transaction tx)
    //	{
    //	  ulong r = 0;
    //	  if (!get_tx_fee(tx, ref r))
    //	  {
    //		return 0;
    //	  }
    //	  return r;
    //	}
    //	public static bool generate_key_image_helper(AccountKeys ack, PublicKey tx_public_key, uint real_output_index, KeyPair in_ephemeral, KeyImage ki)
    //	{
    //	  KeyDerivation recv_derivation = new KeyDerivation();
    //	  bool r = generate_key_derivation(tx_public_key, ack.viewSecretKey, recv_derivation);

    //	  Debug.Assert(r && "key image helper: failed to generate_key_derivation");

    //	  if (!r)
    //	  {
    //		return false;
    //	  }

    //	  r = derive_public_key(recv_derivation, real_output_index, ack.address.spendPublicKey, in_ephemeral.publicKey);

    //	  Debug.Assert(r && "key image helper: failed to derive_public_key");

    //	  if (!r)
    //	  {
    //		return false;
    //	  }

    //	  Crypto.GlobalMembers.derive_secret_key(recv_derivation, new uint(real_output_index), ack.spendSecretKey, in_ephemeral.secretKey);
    //	  generate_key_image(in_ephemeral.publicKey, in_ephemeral.secretKey, ki);
    //	  return true;
    //	}
    //	public static bool getInputsMoneyAmount(Transaction tx, ref ulong money)
    //	{
    //	  money = 0;

    //	  foreach (var in in tx.inputs)
    //	  {
    //		ulong amount = 0;

    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //		if (in.type() == typeid(KeyInput))
    //		{
    //		  amount = boost::get<KeyInput>(in).amount;
    //		}

    //		money += amount;
    //	  }
    //	  return true;
    //	}
    //	public static bool checkInputTypesSupported(TransactionPrefix tx)
    //	{
    //	  foreach (var in in tx.inputs)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //		if (in.type() != typeid(KeyInput))
    //		{
    //		  return false;
    //		}
    //	  }

    //	  return true;
    //	}
    //	public static bool checkOutsValid(TransactionPrefix tx, string error = null)
    //	{
    //	  foreach (TransactionOutput @out in tx.outputs)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //		if (@out.target.type() == typeid(KeyOutput))
    //		{
    //		  if (@out.amount == 0)
    //		  {
    //			if (error != null)
    //			{
    //			  error = "Zero amount ouput";
    //			}
    //			return false;
    //		  }

    //		  if (!check_key(boost::get<KeyOutput>(@out.target).key))
    //		  {
    //			if (error != null)
    //			{
    //			  error = "Output with invalid key";
    //			}
    //			return false;
    //		  }
    //		}
    //		else
    //		{
    //		  if (error != null)
    //		  {
    //			error = "Output with invalid type";
    //		  }
    //		  return false;
    //		}
    //	  }

    //	  return true;
    //	}
    //	public static bool checkMoneyOverflow(TransactionPrefix tx)
    //	{
    //	  return checkInputsOverflow(tx) && checkOutsOverflow(tx);
    //	}
    //	public static bool checkInputsOverflow(TransactionPrefix tx)
    //	{
    //	  ulong money = 0;

    //	  foreach (var in in tx.inputs)
    //	  {
    //		ulong amount = 0;

    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //		if (in.type() == typeid(KeyInput))
    //		{
    //		  amount = boost::get<KeyInput>(in).amount;
    //		}

    //		if (money > amount + money)
    //		{
    //		  return false;
    //		}

    //		money += amount;
    //	  }
    //	  return true;
    //	}
    //	public static bool checkOutsOverflow(TransactionPrefix tx)
    //	{
    //	  ulong money = 0;
    //	  foreach (var o in tx.outputs)
    //	  {
    //		if (money > o.amount + money)
    //		{
    //		  return false;
    //		}
    //		money += o.amount;
    //	  }
    //	  return true;
    //	}
    //	public static ulong get_outs_money_amount(Transaction tx)
    //	{
    //	  ulong outputs_amount = 0;
    //	  foreach (var o in tx.outputs)
    //	  {
    //		outputs_amount += o.amount;
    //	  }
    //	  return outputs_amount;
    //	}
    //	public static string short_hash_str(Hash h)
    //	{
    //	  string res = Common.GlobalMembers.podToHex(h);

    //	  if (res.Length == 64)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to the standard string 'erase' method in C# if it's used as an rvalue:
    //		var erased_pos = res.erase(8, 48);
    //		res = res.Insert(8, "....");
    //	  }

    //	  return res;
    //	}

    //	public static List<uint> relativeOutputOffsetsToAbsolute(List<uint> off)
    //	{
    //	  List<uint> res = new List(off);
    //	  for (uint i = 1; i < res.Count; i++)
    //	  {
    //		res[i] += res[i - 1];
    //	  }
    //	  return res;
    //	}
    //	public static List<uint> absolute_output_offsets_to_relative(List<uint> off)
    //	{
    //	  if (off.Count == 0)
    //	  {
    //		  return new List<uint>();
    //	  }
    //	  var copy = off;
    //	  for (uint i = 1; i < copy.Count; ++i)
    //	  {
    //		copy[i] = off[i] - off[i - 1];
    //	  }
    //	  return copy;
    //	}


    //	// 62387455827 -> 455827 + 7000000 + 80000000 + 300000000 + 2000000000 + 60000000000, where 455827 <= dust_threshold
    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename chunk_handler_t, typename dust_handler_t>
    //	public static void decompose_amount_into_digits<chunk_handler_t, dust_handler_t>(ulong amount, ulong dust_threshold, chunk_handler_t chunk_handler, dust_handler_t dust_handler)
    //	{
    //	  if (0 == amount)
    //	  {
    //		return;
    //	  }

    //	  bool is_dust_handled = false;
    //	  ulong dust = 0;
    //	  ulong order = 1;
    //	  while (0 != amount)
    //	  {
    //		ulong chunk = (amount % 10) * order;
    //		amount /= 10;
    //		order *= 10;

    //		if (dust + chunk <= dust_threshold != null)
    //		{
    //		  dust += chunk;
    //		}
    //		else
    //		{
    //		  if (!is_dust_handled && 0 != dust)
    //		  {
    //			dust_handler(dust);
    //			is_dust_handled = true;
    //		  }
    //		  if (0 != chunk)
    //		  {
    //			chunk_handler(chunk);
    //		  }
    //		}
    //	  }

    //	  if (!is_dust_handled && 0 != dust)
    //	  {
    //		dust_handler(dust);
    //	  }
    //	}

    //	public static ulong power_integral(ulong a, ulong b)
    //	{
    //	  if (b == 0)
    //	  {
    //		return 1;
    //	  }
    //	  ulong total = new ulong(a);
    //	  for (ulong i = 1; i != b; i++)
    //	  {
    //		total *= a;
    //	  }
    //	  return total;
    //	}

    //	public static void serialize(TransactionPrefix txP, ISerializer serializer)
    //	{
    //	  serializer.functorMethod(txP.version, "version");

    //	  if (CURRENT_TRANSACTION_VERSION < txP.version && serializer.type() == ISerializer.INPUT)
    //	  {
    //		throw new System.Exception("Wrong transaction version");
    //	  }

    //	  serializer.functorMethod(txP.unlockTime, "unlock_time");
    //	  serializer.functorMethod(txP.inputs, "vin");
    //	  serializer.functorMethod(txP.outputs, "vout");
    //	  serializeAsBinary(txP.extra, "extra", serializer.functorMethod);
    //	}
    //	public static void serialize(Transaction tx, ISerializer serializer)
    //	{
    //	  serialize((TransactionPrefix)tx, serializer.functorMethod);

    //	  ulong sigSize = tx.inputs.size();
    //	  //TODO: make arrays without sizes
    //	//  serializer.beginArray(sigSize, "signatures");

    //	  // ignore base transaction
    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //	  if (serializer.type() == ISerializer.INPUT && !(sigSize == 1 && tx.inputs[0].type() == typeid(BaseInput)))
    //	  {
    //		tx.signatures.resize(sigSize);
    //	  }

    //	  bool signaturesNotExpected = tx.signatures.empty();
    //	  if (!signaturesNotExpected && tx.inputs.size() != tx.signatures.size())
    //	  {
    //		throw new System.Exception("Serialization error: unexpected signatures size");
    //	  }

    //	  for (ulong i = 0; i < tx.inputs.size(); ++i)
    //	  {
    //		ulong signatureSize = GlobalMembers.getSignaturesCount(tx.inputs[i]);
    //		if (signaturesNotExpected)
    //		{
    //		  if (signatureSize == 0)
    //		  {
    //			continue;
    //		  }
    //		  else
    //		  {
    //			throw new System.Exception("Serialization error: signatures are not expected");
    //		  }
    //		}

    //		if (serializer.type() == ISerializer.OUTPUT)
    //		{
    //		  if (signatureSize != tx.signatures[i].size())
    //		  {
    //			throw new System.Exception("Serialization error: unexpected signatures size");
    //		  }

    //		  foreach (Crypto  in :Signature & sig : tx.signatures[i])
    //		  {
    //			GlobalMembers.serializePod(sig, "", serializer.functorMethod);
    //		  }

    //		}
    //		else
    //		{
    //		  List<Crypto.Signature> signatures = new List<Crypto.Signature>(signatureSize);
    //		  foreach (Crypto  in :Signature & sig : signatures)
    //		  {
    //			GlobalMembers.serializePod(sig, "", serializer.functorMethod);
    //		  }

    //		  tx.signatures[i] = std::move(signatures);
    //		}
    //	  }
    //	//  serializer.endArray();
    //	}
    //	public static void serialize(BaseTransaction tx, ISerializer serializer)
    //	{
    //	  serializer.functorMethod(tx.version, "version");
    //	  serializer.functorMethod(tx.unlockTime, "unlock_time");
    //	  serializer.functorMethod(tx.inputs, "vin");
    //	  serializer.functorMethod(tx.outputs, "vout");
    //	  serializeAsBinary(tx.extra, "extra", serializer.functorMethod);

    //	  if (tx.version >= TRANSACTION_VERSION_2)
    //	  {
    //		ulong ignored = 0;
    //		serializer.functorMethod(ignored, "ignored");
    //	  }
    //	}
    //	public static void serialize(TransactionInput in, ISerializer serializer)
    //	{
    //	  if (serializer.type() == ISerializer.OUTPUT)
    //	  {
    //		BinaryVariantTagGetter tagGetter = new BinaryVariantTagGetter();
    //		ushort tag = boost::apply_visitor(tagGetter.functorMethod, in);
    //		serializer.binary(tag, sizeof(ushort), "type");

    //		VariantSerializer visitor = new VariantSerializer(serializer.functorMethod, "value");
    //		boost::apply_visitor(visitor.functorMethod, in);
    //	  }
    //	  else
    //	  {
    //		ushort tag = new ushort();
    //		serializer.binary(tag, sizeof(ushort), "type");

    //		GlobalMembers.getVariantValue(serializer.functorMethod, new ushort(tag), ref in);
    //	  }
    //	}
    //	public static void serialize(TransactionOutput output, ISerializer serializer)
    //	{
    //	  serializer.functorMethod(output.amount, "amount");
    //	  serializer.functorMethod(output.target, "target");
    //	}

    //	public static void serialize(BaseInput gen, ISerializer serializer)
    //	{
    //	  serializer.functorMethod(gen.blockIndex, "height");
    //	}
    //	public static void serialize(KeyInput key, ISerializer serializer)
    //	{
    //	  serializer.functorMethod(key.amount, "amount");
    //	  GlobalMembers.serializeVarintVector(key.outputIndexes, serializer.functorMethod, "key_offsets");
    //	  serializer.functorMethod(key.keyImage, "k_image");
    //	}

    //	//void serialize(TransactionOutput output, ISerializer serializer);Tangible Method Implementation Not FoundCryptoNote-serialize
    //	public static void serialize(TransactionOutputTarget output, ISerializer serializer)
    //	{
    //	  if (serializer.type() == ISerializer.OUTPUT)
    //	  {
    //		BinaryVariantTagGetter tagGetter = new BinaryVariantTagGetter();
    //		ushort tag = boost::apply_visitor(tagGetter.functorMethod, output);
    //		serializer.binary(tag, sizeof(ushort), "type");

    //		VariantSerializer visitor = new VariantSerializer(serializer.functorMethod, "data");
    //		boost::apply_visitor(visitor.functorMethod, output);
    //	  }
    //	  else
    //	  {
    //		ushort tag = new ushort();
    //		serializer.binary(tag, sizeof(ushort), "type");

    //		GlobalMembers.getVariantValue(serializer.functorMethod, new ushort(tag), ref output);
    //	  }
    //	}
    //	public static void serialize(KeyOutput key, ISerializer serializer)
    //	{
    //	  serializer.functorMethod(key.key, "key");
    //	}

    //	public static void serialize(BlockHeader header, ISerializer serializer)
    //	{
    //	  serializeBlockHeader(header, serializer.functorMethod);
    //	}
    //	public static void serialize(BlockTemplate block, ISerializer serializer)
    //	{
    //	  serializeBlockHeader(block, serializer.functorMethod);

    //	  if (block.majorVersion >= BLOCK_MAJOR_VERSION_2)
    //	  {
    //		var parentBlockSerializer = makeParentBlockSerializer(block, false, false);
    //		serializer.functorMethod(parentBlockSerializer, "parent_block");
    //	  }

    //	  serializer.functorMethod(block.baseTransaction, "miner_tx");
    //	  serializer.functorMethod(block.transactionHashes, "tx_hashes");
    //	}
    //	public static void serialize(ParentBlockSerializer pbs, ISerializer serializer)
    //	{
    //	  serializer.functorMethod(pbs.m_parentBlock.majorVersion, "majorVersion");

    //	  serializer.functorMethod(pbs.m_parentBlock.minorVersion, "minorVersion");
    //	  serializer.functorMethod(pbs.m_timestamp, "timestamp");
    //	  serializer.functorMethod(pbs.m_parentBlock.previousBlockHash, "prevId");
    //	  serializer.binary(pbs.m_nonce, sizeof(uint), "nonce");

    //	  if (pbs.m_hashingSerialization)
    //	  {
    //		Crypto.Hash minerTxHash = new Crypto.Hash();
    //		if (!getBaseTransactionHash(pbs.m_parentBlock.baseTransaction, ref minerTxHash))
    //		{
    //		  throw new System.Exception("Get transaction hash error");
    //		}

    //		Crypto.Hash merkleRoot = new Crypto.Hash();
    //		Crypto.GlobalMembers.tree_hash_from_branch(pbs.m_parentBlock.baseTransactionBranch.data(), pbs.m_parentBlock.baseTransactionBranch.size(), minerTxHash, 0, merkleRoot);

    //		serializer.functorMethod(merkleRoot, "merkleRoot");
    //	  }

    //	  ulong txNum = (ulong)pbs.m_parentBlock.transactionCount;
    //	  serializer.functorMethod(txNum, "numberOfTransactions");
    //	  pbs.m_parentBlock.transactionCount = (ushort)txNum;
    //	  if (pbs.m_parentBlock.transactionCount < 1)
    //	  {
    //		throw new System.Exception("Wrong transactions number");
    //	  }

    //	  if (pbs.m_headerOnly)
    //	  {
    //		return;
    //	  }

    //	  ulong branchSize = Crypto.tree_depth(pbs.m_parentBlock.transactionCount);
    //	  if (serializer.type() == ISerializer.OUTPUT)
    //	  {
    //		if (pbs.m_parentBlock.baseTransactionBranch.size() != branchSize)
    //		{
    //		  throw new System.Exception("Wrong miner transaction branch size");
    //		}
    //	  }
    //	  else
    //	  {
    //		pbs.m_parentBlock.baseTransactionBranch.resize(branchSize);
    //	  }

    //	//  serializer(m_parentBlock.baseTransactionBranch, "baseTransactionBranch");
    //	  //TODO: Make arrays with computable size! This code won't work with json serialization!
    //	  foreach (Crypto  in :Hash & hash: pbs.m_parentBlock.baseTransactionBranch)
    //	  {
    //		serializer.functorMethod(hash, "");
    //	  }

    //	  serializer.functorMethod(pbs.m_parentBlock.baseTransaction, "minerTx");

    //	  TransactionExtraMergeMiningTag mmTag = new TransactionExtraMergeMiningTag();
    //	  if (!getMergeMiningTagFromExtra(pbs.m_parentBlock.baseTransaction.extra, mmTag))
    //	  {
    //		throw new System.Exception("Can't get extra merge mining tag");
    //	  }

    //	  if (mmTag.depth > 8 * sizeof(Crypto.Hash))
    //	  {
    //		throw new System.Exception("Wrong merge mining tag depth");
    //	  }

    //	  if (serializer.type() == ISerializer.OUTPUT)
    //	  {
    //		if (mmTag.depth != pbs.m_parentBlock.blockchainBranch.size())
    //		{
    //		  throw new System.Exception("Blockchain branch size must be equal to merge mining tag depth");
    //		}
    //	  }
    //	  else
    //	  {
    //		pbs.m_parentBlock.blockchainBranch.resize(mmTag.depth);
    //	  }

    //	//  serializer(m_parentBlock.blockchainBranch, "blockchainBranch");
    //	  //TODO: Make arrays with computable size! This code won't work with json serialization!
    //	  foreach (Crypto  in :Hash & hash: pbs.m_parentBlock.blockchainBranch)
    //	  {
    //		serializer.functorMethod(hash, "");
    //	  }
    //	}
    //	public static void serialize(TransactionExtraMergeMiningTag tag, ISerializer serializer)
    //	{
    //	  if (serializer.type() == ISerializer.OUTPUT)
    //	  {
    //		string field;
    //		StringOutputStream os = new StringOutputStream(field);
    //		BinaryOutputStreamSerializer output = new BinaryOutputStreamSerializer(os);
    //		doSerialize(tag, output.functorMethod);
    //		serializer.functorMethod(field, "");
    //	  }
    //	  else
    //	  {
    //		string field;
    //		serializer.functorMethod(field, "mm_tag");
    //		MemoryInputStream stream = new MemoryInputStream(field.data(), field.Length);
    //		BinaryInputStreamSerializer input = new BinaryInputStreamSerializer(stream);
    //		doSerialize(tag, input.functorMethod);
    //	  }
    //	}

    //	public static void serialize(AccountPublicAddress address, ISerializer serializer)
    //	{
    //	  serializer.functorMethod(address.spendPublicKey, "m_spend_public_key");
    //	  serializer.functorMethod(address.viewPublicKey, "m_view_public_key");
    //	}
    //	public static void serialize(AccountKeys keys, ISerializer s)
    //	{
    //	  s.functorMethod(keys.address, "m_account_address");
    //	  s.functorMethod(keys.spendSecretKey, "m_spend_secret_key");
    //	  s.functorMethod(keys.viewSecretKey, "m_view_secret_key");
    //	}

    //	public static void serialize(KeyPair keyPair, ISerializer serializer)
    //	{
    //	  serializer.functorMethod(keyPair.secretKey, "secret_key");
    //	  serializer.functorMethod(keyPair.publicKey, "public_key");
    //	}

    //// unpack to strings to maintain protocol compatibility with older versions
    //	public static void serialize(RawBlock rawBlock, ISerializer serializer)
    //	{
    //	  if (serializer.type() == ISerializer.INPUT)
    //	  {
    //		ulong blockSize = new ulong();
    //		serializer.functorMethod(blockSize, "block_size");
    //		rawBlock.block.resize((ulong)blockSize);
    //	  }
    //	  else
    //	  {
    //		ulong blockSize = rawBlock.block.size();
    //		serializer.functorMethod(blockSize, "block_size");
    //	  }

    //	  serializer.binary(rawBlock.block.data(), rawBlock.block.size(), "block");

    //	  if (serializer.type() == ISerializer.INPUT)
    //	  {
    //		ulong txCount = new ulong();
    //		serializer.functorMethod(txCount, "tx_count");
    //		rawBlock.transactions.resize((ulong)txCount);

    //		foreach (var txBlob in rawBlock.transactions)
    //		{
    //		  ulong txSize = new ulong();
    //		  serializer.functorMethod(txSize, "tx_size");
    //		  txBlob.resize(txSize);
    //		  serializer.binary(txBlob.data(), txBlob.size(), "transaction");
    //		}
    //	  }
    //	  else
    //	  {
    //		ulong txCount = rawBlock.transactions.size();
    //		serializer.functorMethod(txCount, "tx_count");

    //		foreach (var txBlob in rawBlock.transactions)
    //		{
    //		  ulong txSize = txBlob.size();
    //		  serializer.functorMethod(txSize, "tx_size");
    //		  serializer.binary(txBlob.data(), txBlob.size(), "transaction");
    //		}
    //	  }
    //	}

    //	public static void serializeBlockHeader(BlockHeader header, ISerializer serializer)
    //	{
    //	  serializer.functorMethod(header.majorVersion, "major_version");
    //	  if (header.majorVersion > BLOCK_MAJOR_VERSION_4)
    //	  {
    //		throw new System.Exception("Wrong major version");
    //	  }

    //	  serializer.functorMethod(header.minorVersion, "minor_version");
    //	  if (header.majorVersion == BLOCK_MAJOR_VERSION_1)
    //	  {
    //		serializer.functorMethod(header.timestamp, "timestamp");
    //		serializer.functorMethod(header.previousBlockHash, "prev_id");
    //		serializer.binary(header.nonce, sizeof(header.nonce), "nonce");
    //	  }
    //	  else if (header.majorVersion >= BLOCK_MAJOR_VERSION_2)
    //	  {
    //		serializer.functorMethod(header.previousBlockHash, "prev_id");
    //	  }
    //	  else
    //	  {
    //		throw new System.Exception("Wrong major version");
    //	  }
    //	}

    //	public static void doSerialize(TransactionExtraMergeMiningTag tag, ISerializer serializer)
    //	{
    //	  ulong depth = (ulong)tag.depth;
    //	  serializer.functorMethod(depth, "depth");
    //	  tag.depth = (ulong)depth;
    //	  serializer.functorMethod(tag.merkleRoot, "merkle_root");
    //	}

    //	public static void getBinaryArrayHash(BinaryArray binaryArray, Crypto.Hash hash)
    //	{
    //	  Crypto.GlobalMembers.cn_fast_hash(binaryArray.data(), binaryArray.size(), hash);
    //	}
    //	public static Crypto.Hash getBinaryArrayHash(BinaryArray binaryArray)
    //	{
    //	  Crypto.Hash hash = new Crypto.Hash();
    //	  getBinaryArrayHash(binaryArray, hash);
    //	  return hash;
    //	}

    //	// noexcept
    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<class T>
    //	public static bool toBinaryArray<T>(T @object, ref BinaryArray binaryArray)
    //	{
    //	  try
    //	  {
    //		binaryArray = toBinaryArray(@object);
    //	  }
    //	  catch (System.Exception)
    //	  {
    //		return false;
    //	  }

    //	  return true;
    //	}

    //	public static bool toBinaryArray(BinaryArray @object, BinaryArray binaryArray)
    //	{
    //	  try
    //	  {
    //		Common.VectorOutputStream stream = new Common.VectorOutputStream(binaryArray);
    //		BinaryOutputStreamSerializer serializer = new BinaryOutputStreamSerializer(stream);
    //		string oldBlob = Common.asString(@object);
    //		serializer.functorMethod(oldBlob, "");
    //	  }
    //	  catch (System.Exception)
    //	  {
    //		return false;
    //	  }

    //	  return true;
    //	}

    //	// throws exception if serialization failed
    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<class T>
    //	public static BinaryArray toBinaryArray<T>(T @object)
    //	{
    //	  BinaryArray ba = new BinaryArray();
    //	  global::Common.VectorOutputStream stream = new global::Common.VectorOutputStream(ba);
    //	  BinaryOutputStreamSerializer serializer = new BinaryOutputStreamSerializer(stream);
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
    //	  serialize(const_cast<T&>(@object), serializer.functorMethod);
    //	  return ba;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<class T>
    //	public static T fromBinaryArray<T>(BinaryArray binaryArray)
    //	{
    //	  T @object = new default(T);
    //	  Common.MemoryInputStream stream = new Common.MemoryInputStream(binaryArray.data(), binaryArray.size());
    //	  BinaryInputStreamSerializer serializer = new BinaryInputStreamSerializer(stream);
    //	  serialize(@object, serializer.functorMethod);
    //	  if (!stream.endOfStream())
    //	  { // check that all data was consumed
    //		throw new System.Exception("failed to unpack type");
    //	  }

    //	  return @object;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<class T>
    //	public static bool fromBinaryArray<T>(ref T @object, BinaryArray binaryArray)
    //	{
    //	  try
    //	  {
    //		@object = fromBinaryArray<T>(binaryArray);
    //	  }
    //	  catch (System.Exception)
    //	  {
    //		return false;
    //	  }

    //	  return true;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<class T>
    //	public static bool getObjectBinarySize<T>(T @object, ref uint size)
    //	{
    //	  BinaryArray ba = new BinaryArray();
    //	  if (!toBinaryArray(@object, ref ba))
    //	  {
    //		size = (numeric_limits<uint>.max)();
    //		return false;
    //	  }

    //	  size = ba.size();
    //	  return true;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<class T>
    //	public static uint getObjectBinarySize<T>(T @object)
    //	{
    //	  uint size = new uint();
    //	  getObjectBinarySize(@object, ref size);
    //	  return size;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<class T>
    //	public static bool getObjectHash<T>(T @object, ref Crypto.Hash hash)
    //	{
    //	  BinaryArray ba = new BinaryArray();
    //	  if (!toBinaryArray(@object, ref ba))
    //	  {
    //		hash = NULL_HASH;
    //		return false;
    //	  }

    //	  hash = getBinaryArrayHash(ba);
    //	  return true;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<class T>
    //	public static bool getObjectHash<T>(T @object, ref Crypto.Hash hash, ref uint size)
    //	{
    //	  BinaryArray ba = new BinaryArray();
    //	  if (!toBinaryArray(@object, ref ba))
    //	  {
    //		hash = NULL_HASH;
    //		size = (numeric_limits<uint>.max)();
    //		return false;
    //	  }

    //	  size = ba.size();
    //	  hash = getBinaryArrayHash(ba);
    //	  return true;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<class T>
    //	public static Crypto.Hash getObjectHash<T>(T @object)
    //	{
    //	  Crypto.Hash hash = new Crypto.Hash();
    //	  getObjectHash(@object, ref hash);
    //	  return hash;
    //	}

    //	public static bool getBaseTransactionHash(BaseTransaction tx, ref Crypto.Hash hash)
    //	{
    //	  if (tx.version < TRANSACTION_VERSION_2)
    //	  {
    //		return getObjectHash(tx, ref hash);
    //	  }
    //	  else
    //	  {
    //		BinaryArray data = new BinaryArray({0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xbc, 0x36, 0x78, 0x9e, 0x7a, 0x1e, 0x28, 0x14, 0x36, 0x46, 0x42, 0x29, 0x82, 0x8f, 0x81, 0x7d, 0x66, 0x12, 0xf7, 0xb4, 0x77, 0xd6, 0x65, 0x91, 0xff, 0x96, 0xa9, 0xe0, 0x64, 0xbc, 0xc9, 0x8a, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00});
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		if (getObjectHash((TransactionPrefix)tx, ref * reinterpret_cast<Crypto.Hash>(data.data())))
    //		{
    //		  hash = getBinaryArrayHash(data);
    //		  return true;
    //		}
    //		else
    //		{
    //		  return false;
    //		}
    //	  }
    //	}

    //	public static ulong getInputAmount(Transaction transaction)
    //	{
    //	  ulong amount = 0;
    //	  foreach (var input in transaction.inputs)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //		if (input.type() == typeid(KeyInput))
    //		{
    //		  amount += boost::get<KeyInput>(input).amount;
    //		}
    //	  }

    //	  return amount;
    //	}
    //	public static List<ulong> getInputsAmounts(Transaction transaction)
    //	{
    //	  List<ulong> inputsAmounts = new List<ulong>();
    //	  inputsAmounts.Capacity = transaction.inputs.size();

    //	  foreach (var input in transaction.inputs)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //		if (input.type() == typeid(KeyInput))
    //		{
    //		  inputsAmounts.Add(boost::get<KeyInput>(input).amount);
    //		}
    //	  }

    //	  return inputsAmounts;
    //	}
    //	public static ulong getOutputAmount(Transaction transaction)
    //	{
    //	  ulong amount = 0;
    //	  foreach (var output in transaction.outputs)
    //	  {
    //		amount += output.amount;
    //	  }

    //	  return amount;
    //	}
    //	public static void decomposeAmount(ulong amount, ulong dustThreshold, List<ulong> decomposedAmounts)
    //	{
    //  decompose_amount_into_digits(new ulong(amount), new ulong(dustThreshold), (ulong amount) =>
    //  {
    //	decomposedAmounts.Add(amount);
    //  }, (ulong dust) =>
    //  {
    //	decomposedAmounts.Add(dust);
    //  });
    //	}

    //	public static readonly List<ulong> Currency.PRETTY_AMOUNTS = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000, 20000, 30000, 40000, 50000, 60000, 70000, 80000, 90000, 100000, 200000, 300000, 400000, 500000, 600000, 700000, 800000, 900000, 1000000, 2000000, 3000000, 4000000, 5000000, 6000000, 7000000, 8000000, 9000000, 10000000, 20000000, 30000000, 40000000, 50000000, 60000000, 70000000, 80000000, 90000000, 100000000, 200000000, 300000000, 400000000, 500000000, 600000000, 700000000, 800000000, 900000000, 1000000000, 2000000000, 3000000000, 4000000000, 5000000000, 6000000000, 7000000000, 8000000000, 9000000000, 10000000000, 20000000000, 30000000000, 40000000000, 50000000000, 60000000000, 70000000000, 80000000000, 90000000000, 100000000000, 200000000000, 300000000000, 400000000000, 500000000000, 600000000000, 700000000000, 800000000000, 900000000000, 1000000000000, 2000000000000, 3000000000000, 4000000000000, 5000000000000, 6000000000000, 7000000000000, 8000000000000, 9000000000000, 10000000000000, 20000000000000, 30000000000000, 40000000000000, 50000000000000, 60000000000000, 70000000000000, 80000000000000, 90000000000000, 100000000000000, 200000000000000, 300000000000000, 400000000000000, 500000000000000, 600000000000000, 700000000000000, 800000000000000, 900000000000000, 1000000000000000, 2000000000000000, 3000000000000000, 4000000000000000, 5000000000000000, 6000000000000000, 7000000000000000, 8000000000000000, 9000000000000000, 10000000000000000, 20000000000000000, 30000000000000000, 40000000000000000, 50000000000000000, 60000000000000000, 70000000000000000, 80000000000000000, 90000000000000000, 100000000000000000, 200000000000000000, 300000000000000000, 400000000000000000, 500000000000000000, 600000000000000000, 700000000000000000, 800000000000000000, 900000000000000000, 1000000000000000000, 2000000000000000000, 3000000000000000000, 4000000000000000000, 5000000000000000000, 6000000000000000000, 7000000000000000000, 8000000000000000000, 9000000000000000000, 10000000000000000000Ul};
    //	public static bool findTransactionExtraFieldByType<T>(List<boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag>> tx_extra_fields, ref T field)
    //	{
    ////C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
    //  var it = std::find_if(tx_extra_fields.GetEnumerator(), tx_extra_fields.end(), (boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag> f) =>
    //  {
    //	  return typeid(T) == f.type();
    //  });

    //	  if (tx_extra_fields.end() == it)
    //	  {
    //		return false;
    //	  }

    //	  field = boost::get<T>(*it);
    //	  return true;
    //	}

    //	//bool parseTransactionExtra(ClassicVector<ushort> tx_extra, ClassicVector<boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag>> tx_extra_fields);Tangible Method Implementation Not FoundCryptoNote-parseTransactionExtra
    //	//bool writeTransactionExtra(ClassicVector<ushort> tx_extra, ClassicVector<boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag>> tx_extra_fields);Tangible Method Implementation Not FoundCryptoNote-writeTransactionExtra

    //	//Crypto::PublicKey getTransactionPublicKeyFromExtra(ClassicVector<ushort> tx_extra);Tangible Method Implementation Not FoundCryptoNote-getTransactionPublicKeyFromExtra
    //	//bool addTransactionPublicKeyToExtra(ClassicVector<ushort> tx_extra, Crypto::PublicKey tx_pub_key);Tangible Method Implementation Not FoundCryptoNote-addTransactionPublicKeyToExtra
    //	//bool addExtraNonceToTransactionExtra(ClassicVector<ushort> tx_extra, ClassicVector<ushort> extra_nonce);Tangible Method Implementation Not FoundCryptoNote-addExtraNonceToTransactionExtra
    //	//void setPaymentIdToTransactionExtraNonce(ClassicVector<ushort> extra_nonce, Crypto::Hash payment_id);Tangible Method Implementation Not FoundCryptoNote-setPaymentIdToTransactionExtraNonce
    //	//bool getPaymentIdFromTransactionExtraNonce(ClassicVector<ushort> extra_nonce, Crypto::Hash payment_id);Tangible Method Implementation Not FoundCryptoNote-getPaymentIdFromTransactionExtraNonce
    //	//bool appendMergeMiningTagToExtra(ClassicVector<ushort> tx_extra, TransactionExtraMergeMiningTag mm_tag);Tangible Method Implementation Not FoundCryptoNote-appendMergeMiningTagToExtra
    //	//bool getMergeMiningTagFromExtra(ClassicVector<ushort> tx_extra, TransactionExtraMergeMiningTag mm_tag);Tangible Method Implementation Not FoundCryptoNote-getMergeMiningTagFromExtra

    //	//bool createTxExtraWithPaymentId(string paymentIdString, ClassicVector<ushort> extra);Tangible Method Implementation Not FoundCryptoNote-createTxExtraWithPaymentId
    //	//returns false if payment id is not found or parse error
    //	//bool getPaymentIdFromTxExtra(ClassicVector<ushort> extra, Crypto::Hash paymentId);Tangible Method Implementation Not FoundCryptoNote-getPaymentIdFromTxExtra
    //	//bool parsePaymentId(string paymentIdString, Crypto::Hash paymentId);Tangible Method Implementation Not FoundCryptoNote-parsePaymentId

    //	public static readonly uint ONE_DAY_SECONDS = 60 * 60 * 24;
    //	public static readonly CachedBlockInfo NULL_CACHED_BLOCK_INFO = new CachedBlockInfo(NULL_HASH, 0, 0, 0, 0, 0);

    //	public static bool requestPackedOutputs(IBlockchainCache.Amount amount, Common.ArrayView<uint> globalIndexes, IDataBase database, List<PackedOutIndex> result)
    //	{
    //	  BlockchainReadBatch readBatch = new BlockchainReadBatch();
    //	  result.Capacity = result.Count + globalIndexes.getSize();

    //	  foreach (var globalIndex in globalIndexes)
    //	  {
    //		readBatch.requestKeyOutputGlobalIndexForAmount(amount, globalIndex);
    //	  }

    //	  var dbResult = database.read(readBatch);
    //	  if (dbResult)
    //	  {
    //		return false;
    //	  }

    //	  try
    //	  {
    //		var readResult = readBatch.extractResult();
    //		auto packedOutsMap = readResult.getKeyOutputGlobalIndexesForAmounts();
    //		foreach (var globalIndex in globalIndexes)
    //		{
    //		  result.Add(packedOutsMap.at(Tuple.Create(amount, globalIndex)));
    //		}
    //	  }
    //	  catch (System.Exception)
    //	  {
    //		return false;
    //	  }

    //	  return true;
    //	}

    //	public static bool requestTransactionHashesForGlobalOutputIndexes(List<PackedOutIndex> packedOuts, IDataBase database, List<Crypto.Hash> transactionHashes)
    //	{
    //	  BlockchainReadBatch readHashesBatch = new BlockchainReadBatch();

    //	  SortedSet<uint> blockIndexes = new SortedSet<uint>();
    //  packedOuts.ForEach((PackedOutIndex @out) =>
    //  {
    //	  blockIndexes.Add(@out.blockIndex);
    //  });
    //  blockIndexes.ForEach((uint blockIndex) =>
    //  {
    //	  readHashesBatch.requestTransactionHashesByBlock(blockIndex);
    //  });

    //	  var dbResult = database.read(readHashesBatch);
    //	  if (dbResult)
    //	  {
    //		return false;
    //	  }

    //	  var readResult = readHashesBatch.extractResult();
    //	  auto transactionHashesMap = readResult.getTransactionHashesByBlocks();

    //	  if (transactionHashesMap.size() != blockIndexes.Count)
    //	  {
    //		return false;
    //	  }

    //	  transactionHashes.Capacity = transactionHashes.Count + packedOuts.Count;
    //	  foreach (var output in packedOuts)
    //	  {
    //		if (output.transactionIndex >= transactionHashesMap.at(output.blockIndex).size())
    //		{
    //		  return false;
    //		}

    //		transactionHashes.Add(transactionHashesMap.at(output.blockIndex)[output.transactionIndex]);
    //	  }

    //	  return true;
    //	}

    //	public static bool requestCachedTransactionInfos(List<Crypto.Hash> transactionHashes, IDataBase database, List<CachedTransactionInfo> result)
    //	{
    //	  result.Capacity = result.Count + transactionHashes.Count;

    //	  BlockchainReadBatch transactionsBatch = new BlockchainReadBatch();
    //  transactionHashes.ForEach((Crypto.Hash hash) =>
    //  {
    //	  transactionsBatch.requestCachedTransaction(hash);
    //  });
    //	  var dbResult = database.read(transactionsBatch);
    //	  if (dbResult)
    //	  {
    //		return false;
    //	  }

    //	  var readResult = transactionsBatch.extractResult();
    //	  auto transactions = readResult.getCachedTransactions();
    //	  if (transactions.size() != transactionHashes.Count)
    //	  {
    //		return false;
    //	  }

    //	  foreach (var hash in transactionHashes)
    //	  {
    //		result.Add(transactions.at(hash));
    //	  }

    //	  return true;
    //	}

    //	//returns CachedTransactionInfos in the same or as packedOuts are
    //	/*
    //	bool requestCachedTransactionInfos(const std::vector<PackedOutIndex>& packedOuts, IDataBase& database, std::vector<CachedTransactionInfo>& result) {
    //	  std::vector<Crypto::Hash> transactionHashes;
    //	  if (!requestTransactionHashesForGlobalOutputIndexes(packedOuts, database, transactionHashes)) {
    //	    return false;
    //	  }

    //	  return requestCachedTransactionInfos(transactionHashes, database, result);
    //	}
    //	*/

    //	public static bool requestExtendedTransactionInfos(List<Crypto.Hash> transactionHashes, IDataBase database, List<ExtendedTransactionInfo> result)
    //	{
    //	  result.Capacity = result.Count + transactionHashes.Count;

    //	  BlockchainReadBatch transactionsBatch = new BlockchainReadBatch();
    //  transactionHashes.ForEach((Crypto.Hash hash) =>
    //  {
    //	  transactionsBatch.requestCachedTransaction(hash);
    //  });
    //	  var dbResult = database.read(transactionsBatch);
    //	  if (dbResult)
    //	  {
    //		return false;
    //	  }

    //	  var readResult = transactionsBatch.extractResult();
    //	  auto transactions = readResult.getCachedTransactions();

    //	  HashSet<Crypto.Hash> uniqueTransactionHashes = new HashSet<Crypto.Hash>(transactionHashes.GetEnumerator(), transactionHashes.end());
    //	  if (transactions.size() != uniqueTransactionHashes.Count)
    //	  {
    //		return false;
    //	  }

    //	  foreach (var hash in transactionHashes)
    //	  {
    //		result.Add(transactions.at(hash));
    //	  }

    //	  return true;
    //	}

    //	//returns ExtendedTransactionInfos in the same order as packedOuts are
    //	public static bool requestExtendedTransactionInfos(List<PackedOutIndex> packedOuts, IDataBase database, List<ExtendedTransactionInfo> result)
    //	{
    //	  List<Crypto.Hash> transactionHashes = new List<Crypto.Hash>();
    //	  if (!GlobalMembers.requestTransactionHashesForGlobalOutputIndexes(packedOuts, database, transactionHashes))
    //	  {
    //		return false;
    //	  }

    //	  return GlobalMembers.requestExtendedTransactionInfos(transactionHashes, database, result);
    //	}

    //	public static ulong roundToMidnight(ulong timestamp)
    //	{
    //	  if (timestamp > (ulong)(DateTime.MaxValue))
    //	  {
    //		throw new System.Exception("Timestamp is too big");
    //	  }

    //	  return (ulong)((timestamp / GlobalMembers.ONE_DAY_SECONDS) * GlobalMembers.ONE_DAY_SECONDS);
    //	}

    //	public static Tuple<boost.optional<uint>, bool> requestClosestBlockIndexByTimestamp(ulong timestamp, IDataBase database)
    //	{
    //	  Tuple<boost.optional<uint>, bool> result = new Tuple<boost.optional<uint>, bool>({}, false);

    //	  BlockchainReadBatch readBatch = new BlockchainReadBatch();
    //	  readBatch.requestClosestTimestampBlockIndex(timestamp);
    //	  var dbResult = database.read(readBatch);
    //	  if (dbResult)
    //	  {
    //		return result;
    //	  }

    //	  result.Item2 = true;
    //	  var readResult = readBatch.extractResult();
    //	  if (readResult.getClosestTimestampBlockIndex().count(timestamp))
    //	  {
    //		result.Item1 = readResult.getClosestTimestampBlockIndex().at(timestamp);
    //	  }

    //	  return result;
    //	}

    //	public static bool requestRawBlock(IDataBase database, uint blockIndex, ref RawBlock block)
    //	{
    //	  var batch = BlockchainReadBatch().requestRawBlock(blockIndex);

    //	  var error = database.read(batch);
    //	  if (error)
    //	  {
    //		//may be throw in all similiar functions???
    //		return false;
    //	  }

    //	  var result = batch.extractResult();
    //	  if (result.getRawBlocks().count(blockIndex) == 0)
    //	  {
    //		return false;
    //	  }

    //	  block = result.getRawBlocks().at(blockIndex);
    //	  return true;
    //	}

    //	public static Transaction extractTransaction(RawBlock block, uint transactionIndex)
    //	{
    //	  Debug.Assert(transactionIndex < block.transactions.Count + 1);

    //	  if (transactionIndex != 0)
    //	  {
    //		Transaction transaction = new Transaction();
    //		bool r = fromBinaryArray(ref transaction, block.transactions[transactionIndex - 1]);
    //		if (r)
    //		{
    //		}
    //		Debug.Assert(r);

    //		return transaction;
    //	  }

    //	  BlockTemplate blockTemplate = new BlockTemplate();
    //	  bool r = fromBinaryArray(ref blockTemplate, block.block);
    //	  if (r)
    //	  {
    //	  }
    //	  Debug.Assert(r);

    //	  return blockTemplate.baseTransaction;
    //	}

    //	public static uint requestPaymentIdTransactionsCount(IDataBase database, Crypto.Hash paymentId)
    //	{
    //	  var batch = BlockchainReadBatch().requestTransactionCountByPaymentId(paymentId);
    //	  var error = database.read(batch);
    //	  if (error)
    //	  {
    //		throw std::system_error(error, "Error while reading transactions count by payment id");
    //	  }

    //	  var result = batch.extractResult();
    //	  if (result.getTransactionCountByPaymentIds().count(paymentId) == 0)
    //	  {
    //		return 0;
    //	  }

    //	  return result.getTransactionCountByPaymentIds().at(paymentId);
    //	}

    //	public static bool requestPaymentId(IDataBase database, Crypto.Hash transactionHash, Crypto.Hash paymentId)
    //	{
    //	  List<CachedTransactionInfo> cachedTransactions = new List<CachedTransactionInfo>();

    //	  if (!GlobalMembers.requestCachedTransactionInfos(new List<Crypto.Hash>() {transactionHash}, database, cachedTransactions))
    //	  {
    //		return false;
    //	  }

    //	  if (cachedTransactions.Count == 0)
    //	  {
    //		return false;
    //	  }

    //	  RawBlock block = new RawBlock();
    //	  if (!GlobalMembers.requestRawBlock(database, cachedTransactions[0].blockIndex, ref block))
    //	  {
    //		return false;
    //	  }

    //	  Transaction transaction = GlobalMembers.extractTransaction(block, cachedTransactions[0].transactionIndex);
    //	  return getPaymentIdFromTxExtra(transaction.extra, paymentId);
    //	}

    //	public static uint requestKeyOutputGlobalIndexesCountForAmount(IBlockchainCache.Amount amount, IDataBase database)
    //	{
    //	  var batch = BlockchainReadBatch().requestKeyOutputGlobalIndexesCountForAmount(amount);
    //	  var dbError = database.read(batch);
    //	  if (dbError)
    //	  {
    //		throw std::system_error(dbError, "Cannot perform requestKeyOutputGlobalIndexesCountForAmount query");
    //	  }

    //	  var result = batch.extractResult();

    //	  if (result.getKeyOutputGlobalIndexesCountForAmounts().count(amount) != 0)
    //	  {
    //		return result.getKeyOutputGlobalIndexesCountForAmounts().at(amount);
    //	  }
    //	  else
    //	  {
    //		return 0;
    //	  }
    //	}

    //	public static PackedOutIndex retrieveKeyOutput(IBlockchainCache.Amount amount, uint globalOutputIndex, IDataBase database)
    //	{
    //	  BlockchainReadBatch batch = new BlockchainReadBatch();
    //	  var dbError = database.read(batch.requestKeyOutputGlobalIndexForAmount(amount, globalOutputIndex));
    //	  if (dbError)
    //	  {
    //		throw std::system_error(dbError, "Error during retrieving key output by global output index");
    //	  }

    //	  var result = batch.extractResult();

    //	  try
    //	  {
    //		return result.getKeyOutputGlobalIndexesForAmounts().at(Tuple.Create(amount, globalOutputIndex));
    //	  }
    //	  catch (System.Exception)
    //	  {
    //		Debug.Assert(false);
    //		throw new System.Exception("Couldn't find key output for amount " + Convert.ToString(amount) + " with global output index " + Convert.ToString(globalOutputIndex));
    //	  }
    //	}

    //	public static SortedDictionary<IBlockchainCache.Amount, IBlockchainCache.GlobalOutputIndex> getMinGlobalIndexesByAmount(SortedDictionary<IBlockchainCache.Amount, List<IBlockchainCache.GlobalOutputIndex>> outputIndexes)
    //	{

    //	  SortedDictionary<IBlockchainCache.Amount, IBlockchainCache.GlobalOutputIndex> minIndexes = new SortedDictionary<IBlockchainCache.Amount, IBlockchainCache.GlobalOutputIndex>();
    //	  foreach (var kv in outputIndexes)
    //	  {
    //		var min = std::min_element(kv.second.begin(), kv.second.end());
    //		if (min == kv.second.end())
    //		{
    //		  continue;
    //		}

    //		minIndexes.Add(kv.first, *min);
    //	  }

    //	  return minIndexes;
    //	}

    //	public static void mergeOutputsSplitBoundaries(SortedDictionary<IBlockchainCache.Amount, IBlockchainCache.GlobalOutputIndex> dest, SortedDictionary<IBlockchainCache.Amount, IBlockchainCache.GlobalOutputIndex> src)
    //	{
    //	  foreach (var elem in src)
    //	  {
    //		var it = dest.find(elem.first);
    //		if (it == dest.end())
    //		{
    //		  dest.Add(elem.first, elem.second);
    //		  continue;
    //		}

    ////C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
    //		if (it.second > elem.second)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
    //		  it.second = elem.second;
    //		}
    //	  }
    //	}

    //	public static void cutTail(LinkedList<CachedBlockInfo> cache, uint count)
    //	{
    //	  if (count >= cache.Count)
    //	  {
    //		cache.Clear();
    //		return;
    //	  }

    ////C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL list 'erase' method in C#:
    //	  cache.erase(std::next(cache.GetEnumerator(), cache.Count - count), cache.end());
    //	}

    //	public static readonly string DB_VERSION_KEY = "db_scheme_version";

    //	public static readonly uint CURRENT_DB_SCHEME_VERSION = 2;

    //	public static std::unique_ptr<IMainChainStorage> createSwappedMainChainStorage(string dataDir, Currency currency)
    //	{
    //	  boost::filesystem.path blocksFilename = boost::filesystem.path(dataDir) / currency.blocksFileName();
    //	  boost::filesystem.path indexesFilename = boost::filesystem.path(dataDir) / currency.blockIndexesFileName();

    //	  std::unique_ptr<IMainChainStorage> storage = new std::unique_ptr<IMainChainStorage>(new MainChainStorage(blocksFilename.string(), indexesFilename.string()));
    //	  if (storage.getBlockCount() == 0)
    //	  {
    //		RawBlock genesis = new RawBlock();
    //		genesis.block = toBinaryArray(currency.genesisBlock());
    //		storage.pushBlock(genesis);
    //	  }

    //	  return storage;
    //	}

    //	public static readonly uint STORAGE_CACHE_SIZE = 100;

    //	//bool checkInputsKeyimagesDiff(CryptoNote::TransactionPrefix tx);Tangible Method Implementation Not FoundCryptoNote-checkInputsKeyimagesDiff

    //	// TransactionInput helper functions
    //	//uint getRequiredSignaturesCount(boost::variant<BaseInput, KeyInput> in);Tangible Method Implementation Not FoundCryptoNote-getRequiredSignaturesCount
    //	//ulong getTransactionInputAmount(boost::variant<BaseInput, KeyInput> in);Tangible Method Implementation Not FoundCryptoNote-getTransactionInputAmount
    //	//TransactionTypes::InputType getTransactionInputType(boost::variant<BaseInput, KeyInput> in);Tangible Method Implementation Not FoundCryptoNote-getTransactionInputType
    //	//boost::variant<BaseInput, KeyInput> getInputChecked(CryptoNote::TransactionPrefix transaction, uint index);Tangible Method Implementation Not FoundCryptoNote-getInputChecked
    //	//boost::variant<BaseInput, KeyInput> getInputChecked(CryptoNote::TransactionPrefix transaction, uint index, TransactionTypes::InputType type);Tangible Method Implementation Not FoundCryptoNote-getInputChecked

    //	//bool isOutToKey(Crypto::PublicKey spendPublicKey, Crypto::PublicKey outKey, Crypto::KeyDerivation derivation, uint keyIndex);Tangible Method Implementation Not FoundCryptoNote-isOutToKey

    //	// TransactionOutput helper functions
    //	//TransactionTypes::OutputType getTransactionOutputType(boost::variant<KeyOutput> @out);Tangible Method Implementation Not FoundCryptoNote-getTransactionOutputType
    //	//TransactionOutput getOutputChecked(CryptoNote::TransactionPrefix transaction, uint index);Tangible Method Implementation Not FoundCryptoNote-getOutputChecked
    //	//TransactionOutput getOutputChecked(CryptoNote::TransactionPrefix transaction, uint index, TransactionTypes::OutputType type);Tangible Method Implementation Not FoundCryptoNote-getOutputChecked

    //	//bool findOutputsToAccount(CryptoNote::TransactionPrefix transaction, AccountPublicAddress addr, Crypto::SecretKey viewSecretKey, ClassicVector<uint> @out, ulong amount);Tangible Method Implementation Not FoundCryptoNote-findOutputsToAccount


    //	  ////////////////////////////////////////////////////////////////////////
    //	  // class Transaction implementation
    //	  ////////////////////////////////////////////////////////////////////////

    //	  public static std::unique_ptr<ITransaction> createTransaction()
    //	  {
    //		return std::unique_ptr<ITransaction>(new TransactionImpl());
    //	  }

    //	  public static std::unique_ptr<ITransaction> createTransaction(List<ushort> transactionBlob)
    //	  {
    //		return std::unique_ptr<ITransaction>(new TransactionImpl(transactionBlob));
    //	  }

    //	  public static std::unique_ptr<ITransaction> createTransaction(CryptoNote.Transaction tx)
    //	  {
    //		return std::unique_ptr<ITransaction>(new TransactionImpl(tx));
    //	  }
    //	public static bool findTransactionExtraFieldByType<T>(List<boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag>> tx_extra_fields, ref T field)
    //	{
    ////C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
    //  var it = std::find_if(tx_extra_fields.GetEnumerator(), tx_extra_fields.end(), (boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag> f) =>
    //  {
    //	  return typeid(T) == f.type();
    //  });

    //	  if (tx_extra_fields.end() == it)
    //	  {
    //		return false;
    //	  }

    //	  field = boost::get<T>(*it);
    //	  return true;
    //	}

    //	public static bool parseTransactionExtra(List<ushort> transactionExtra, List<boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag>> transactionExtraFields)
    //	{
    //	  transactionExtraFields.Clear();

    //	  if (transactionExtra.Count == 0)
    //	  {
    //		return true;
    //	  }

    //	  bool seen_tx_extra_tag_padding = false;
    //	  bool seen_tx_extra_tag_pubkey = false;
    //	  bool seen_tx_extra_nonce = false;
    //	  bool seen_tx_extra_merge_mining_tag = false;

    //	  try
    //	  {
    //		MemoryInputStream iss = new MemoryInputStream(transactionExtra.data(), transactionExtra.Count);
    //		BinaryInputStreamSerializer ar = new BinaryInputStreamSerializer(iss);

    //		int c = 0;

    //		while (!iss.endOfStream())
    //		{
    //		  c = Common.GlobalMembers.read<ushort>(iss);
    //		  switch (c)
    //		  {
    //		  case DefineConstants.TX_EXTRA_TAG_PADDING:
    //		  {
    //			if (seen_tx_extra_tag_padding)
    //			{
    //				return true;
    //			}

    //			seen_tx_extra_tag_padding = true;

    //			uint size = 1;
    //			for (; !iss.endOfStream() && size <= DefineConstants.TX_EXTRA_PADDING_MAX_COUNT; ++size)
    //			{
    //			  if (Common.GlobalMembers.read<ushort>(iss) != 0)
    //			  {
    //				return false; // all bytes should be zero
    //			  }
    //			}

    //			if (size > DefineConstants.TX_EXTRA_PADDING_MAX_COUNT)
    //			{
    //			  return false;
    //			}

    //			transactionExtraFields.Add(new TransactionExtraPadding({size}));
    //			break;
    //		  }

    //		  case DefineConstants.TX_EXTRA_TAG_PUBKEY:
    //		  {
    //			if (seen_tx_extra_tag_pubkey)
    //			{
    //				return true;
    //			}

    //			seen_tx_extra_tag_pubkey = true;

    //			TransactionExtraPublicKey extraPk = new TransactionExtraPublicKey();
    //			ar.functorMethod(extraPk.publicKey, "public_key");
    //			transactionExtraFields.Add(extraPk);
    //			break;
    //		  }

    //		  case DefineConstants.TX_EXTRA_NONCE:
    //		  {
    //			if (seen_tx_extra_nonce)
    //			{
    //				return true;
    //			}

    //			seen_tx_extra_nonce = true;

    //			TransactionExtraNonce extraNonce = new TransactionExtraNonce();
    //			ushort size = Common.GlobalMembers.read<ushort>(iss);
    //			if (size > 0)
    //			{
    //			  extraNonce.nonce.Resize(size);
    //			  read(iss, extraNonce.nonce.data(), extraNonce.nonce.Count);
    //			}

    //			transactionExtraFields.Add(extraNonce);
    //			break;
    //		  }

    //		  case DefineConstants.TX_EXTRA_MERGE_MINING_TAG:
    //		  {
    //			if (seen_tx_extra_merge_mining_tag)
    //			{
    //				break;
    //			}

    //			seen_tx_extra_merge_mining_tag = true;

    //			TransactionExtraMergeMiningTag mmTag = new TransactionExtraMergeMiningTag();
    //			ar.functorMethod(mmTag, "mm_tag");
    //			transactionExtraFields.Add(mmTag);
    //			break;
    //		  }
    //		  }
    //		}
    //	  }
    //	  catch (System.Exception)
    //	  {
    //		return false;
    //	  }

    //	  return true;
    //	}
    //	public static bool writeTransactionExtra(List<ushort> tx_extra, List<boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag>> tx_extra_fields)
    //	{
    //	  ExtraSerializerVisitor visitor = new ExtraSerializerVisitor(tx_extra);

    //	  foreach (var tag in tx_extra_fields)
    //	  {
    //		if (!boost::apply_visitor(visitor.functorMethod, tag))
    //		{
    //		  return false;
    //		}
    //	  }

    //	  return true;
    //	}

    //	public static PublicKey getTransactionPublicKeyFromExtra(List<ushort> tx_extra)
    //	{
    //	  List<boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag>> tx_extra_fields = new List<boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag>>();
    //	  parseTransactionExtra(tx_extra, tx_extra_fields);

    //	  TransactionExtraPublicKey pub_key_field = new TransactionExtraPublicKey();
    //	  if (!findTransactionExtraFieldByType(tx_extra_fields, ref pub_key_field))
    //	  {
    //		return boost::value_initialized<PublicKey>();
    //	  }

    //	  return pub_key_field.publicKey;
    //	}
    //	public static bool addTransactionPublicKeyToExtra(List<ushort> tx_extra, PublicKey tx_pub_key)
    //	{
    //	  tx_extra.Resize(tx_extra.Count + 1 + sizeof(PublicKey));
    //	  tx_extra[tx_extra.Count - 1 - sizeof(PublicKey)] = DefineConstants.TX_EXTRA_TAG_PUBKEY;
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //	  *reinterpret_cast<PublicKey>(tx_extra[tx_extra.Count - sizeof(PublicKey)]) = tx_pub_key;
    //	  return true;
    //	}
    //	public static bool addExtraNonceToTransactionExtra(List<ushort> tx_extra, BinaryArray extra_nonce)
    //	{
    //	  if (extra_nonce.size() > DefineConstants.TX_EXTRA_NONCE_MAX_COUNT)
    //	  {
    //		return false;
    //	  }

    //	  uint start_pos = tx_extra.Count;
    //	  tx_extra.Resize(tx_extra.Count + 2 + extra_nonce.size());
    //	  //write tag
    //	  tx_extra[start_pos] = DefineConstants.TX_EXTRA_NONCE;
    //	  //write len
    //	  ++start_pos;
    //	  tx_extra[start_pos] = (ushort)extra_nonce.size();
    //	  //write data
    //	  ++start_pos;
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
    //	  memcpy(tx_extra[start_pos], extra_nonce.data(), extra_nonce.size());
    //	  return true;
    //	}
    //	//void setPaymentIdToTransactionExtraNonce(BinaryArray extra_nonce, Crypto::Hash payment_id);Tangible Method Implementation Not FoundCryptoNote-setPaymentIdToTransactionExtraNonce
    //	//bool getPaymentIdFromTransactionExtraNonce(BinaryArray extra_nonce, Crypto::Hash payment_id);Tangible Method Implementation Not FoundCryptoNote-getPaymentIdFromTransactionExtraNonce
    //	public static bool appendMergeMiningTagToExtra(List<ushort> tx_extra, TransactionExtraMergeMiningTag mm_tag)
    //	{
    //	  BinaryArray blob = new BinaryArray();
    //	  if (!toBinaryArray(mm_tag, ref blob))
    //	  {
    //		return false;
    //	  }

    //	  tx_extra.Add(DefineConstants.TX_EXTRA_MERGE_MINING_TAG);
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //	  std::copy(reinterpret_cast<const ushort>(blob.data()), reinterpret_cast<const ushort>(blob.data() + blob.size()), std::back_inserter(tx_extra));
    //	  return true;
    //	}
    //	public static bool getMergeMiningTagFromExtra(List<ushort> tx_extra, TransactionExtraMergeMiningTag mm_tag)
    //	{
    //	  List<boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag>> tx_extra_fields = new List<boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag>>();
    //	  parseTransactionExtra(tx_extra, tx_extra_fields);

    //	  return findTransactionExtraFieldByType(tx_extra_fields, ref mm_tag);
    //	}

    //	public static bool createTxExtraWithPaymentId(string paymentIdString, List<ushort> extra)
    //	{
    //	  Hash paymentIdBin = new Hash();

    //	  if (!parsePaymentId(paymentIdString, paymentIdBin))
    //	  {
    //		return false;
    //	  }

    //	  List<ushort> extraNonce = new List<ushort>();
    //	  CryptoNote.GlobalMembers.setPaymentIdToTransactionExtraNonce(extraNonce, paymentIdBin);

    //	  if (!CryptoNote.GlobalMembers.addExtraNonceToTransactionExtra(extra, extraNonce))
    //	  {
    //		return false;
    //	  }

    //	  return true;
    //	}
    //	//returns false if payment id is not found or parse error
    //	public static bool getPaymentIdFromTxExtra(List<ushort> extra, Hash paymentId)
    //	{
    //	  List<boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag>> tx_extra_fields = new List<boost::variant<TransactionExtraPadding, TransactionExtraPublicKey, TransactionExtraNonce, TransactionExtraMergeMiningTag>>();
    //	  if (!parseTransactionExtra(extra, tx_extra_fields))
    //	  {
    //		return false;
    //	  }

    //	  TransactionExtraNonce extra_nonce = new TransactionExtraNonce();
    //	  if (findTransactionExtraFieldByType(tx_extra_fields, ref extra_nonce))
    //	  {
    //		if (!getPaymentIdFromTransactionExtraNonce(extra_nonce.nonce, ref paymentId))
    //		{
    //		  return false;
    //		}
    //	  }
    //	  else
    //	  {
    //		return false;
    //	  }

    //	  return true;
    //	}
    //	public static bool parsePaymentId(string paymentIdString, Hash paymentId)
    //	{
    //	  return Common.GlobalMembers.podFromHex(paymentIdString, paymentId);
    //	}

    //	public static void setPaymentIdToTransactionExtraNonce(List<ushort> extra_nonce, Hash payment_id)
    //	{
    //	  extra_nonce.Clear();
    //	  extra_nonce.Add(DefineConstants.TX_EXTRA_NONCE_PAYMENT_ID);
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //	  ushort payment_id_ptr = reinterpret_cast<const ushort>(payment_id);
    //	  std::copy(payment_id_ptr, payment_id_ptr + sizeof(Hash), std::back_inserter(extra_nonce));
    //	}

    //	public static bool getPaymentIdFromTransactionExtraNonce(List<ushort> extra_nonce, ref Hash payment_id)
    //	{
    //	  if (sizeof(Hash) + 1 != extra_nonce.Count)
    //	  {
    //		return false;
    //	  }
    //	  if (DefineConstants.TX_EXTRA_NONCE_PAYMENT_ID != extra_nonce[0])
    //	  {
    //		return false;
    //	  }
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //	  payment_id = reinterpret_cast<const Hash>(extra_nonce.data() + 1);
    //	  return true;
    //	}

    //	public static TransactionPoolMessage makeAddTransaction(Crypto.Hash hash)
    //	{
    //	  return TransactionPoolMessage
    //	  {
    //		  AddTransaction{hash}
    //	  };
    //	}

    //	public static TransactionPoolMessage makeDelTransaction(Crypto.Hash hash)
    //	{
    //	  return TransactionPoolMessage
    //	  {
    //		  DeleteTransaction{hash}
    //	  };
    //	}


    //	public static std::unique_ptr<ITransactionReader> createTransactionPrefix(TransactionPrefix prefix, Hash transactionHash)
    //	{
    //	  return std::unique_ptr<ITransactionReader> (new TransactionPrefixImpl(prefix, transactionHash));
    //	}

    //	public static std::unique_ptr<ITransactionReader> createTransactionPrefix(Transaction fullTransaction)
    //	{
    //	  return std::unique_ptr<ITransactionReader> (new TransactionPrefixImpl(fullTransaction, getObjectHash(fullTransaction)));
    //	}

    //	public static bool checkInputsKeyimagesDiff(CryptoNote.TransactionPrefix tx)
    //	{
    //	  HashSet<Crypto.KeyImage> ki = new HashSet<Crypto.KeyImage>();
    //	  foreach (var in in tx.inputs)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //		if (in.type() == typeid(KeyInput))
    //		{
    //		  if (!ki.Add(boost::get<KeyInput>(in).keyImage).second)
    //		  {
    //			return false;
    //		  }
    //		}
    //	  }

    //	  return true;
    //	}

    //// TransactionInput helper functions


    //	// TransactionInput helper functions
    //	public static uint getRequiredSignaturesCount(boost::variant<BaseInput, KeyInput> in)
    //	{
    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //	  if (in.type() == typeid(KeyInput))
    //	  {
    //		return boost::get<KeyInput>(in).outputIndexes.size();
    //	  }

    //	  return 0;
    //	}
    //	public static ulong getTransactionInputAmount(boost::variant<BaseInput, KeyInput> in)
    //	{
    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //	  if (in.type() == typeid(KeyInput))
    //	  {
    //		return boost::get<KeyInput>(in).amount;
    //	  }

    //	  return 0;
    //	}
    //	public static TransactionTypes.InputType getTransactionInputType(boost::variant<BaseInput, KeyInput> in)
    //	{
    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //	  if (in.type() == typeid(KeyInput))
    //	  {
    //		return TransactionTypes.InputType.Key;
    //	  }

    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //	  if (in.type() == typeid(BaseInput))
    //	  {
    //		return TransactionTypes.InputType.Generating;
    //	  }

    //	  return TransactionTypes.InputType.Invalid;
    //	}
    //	public static boost::variant<BaseInput, KeyInput> getInputChecked(CryptoNote.TransactionPrefix transaction, uint index)
    //	{
    //	  if (transaction.inputs.Count <= index)
    //	  {
    //		throw new System.Exception("Transaction input index out of range");
    //	  }

    //	  return transaction.inputs[index];
    //	}
    //	public static boost::variant<BaseInput, KeyInput> getInputChecked(CryptoNote.TransactionPrefix transaction, uint index, TransactionTypes.InputType type)
    //	{
    //	  auto input = getInputChecked(transaction, new uint(index));
    //	  if (getTransactionInputType(input) != type)
    //	  {
    //		throw new System.Exception("Unexpected transaction input type");
    //	  }

    //	  return input;
    //	}

    //	public static bool isOutToKey(Crypto.PublicKey spendPublicKey, Crypto.PublicKey outKey, Crypto.KeyDerivation derivation, uint keyIndex)
    //	{
    //	  Crypto.PublicKey pk = new Crypto.PublicKey();
    //	  derive_public_key(derivation, keyIndex, spendPublicKey, pk);
    //	  return pk == outKey;
    //	}

    //// TransactionOutput helper functions


    //	// TransactionOutput helper functions
    //	public static TransactionTypes.OutputType getTransactionOutputType(boost::variant<KeyOutput> @out)
    //	{
    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //	  if (@out.type() == typeid(KeyOutput))
    //	  {
    //		return TransactionTypes.OutputType.Key;
    //	  }

    //	  return TransactionTypes.OutputType.Invalid;
    //	}
    //	public static TransactionOutput getOutputChecked(CryptoNote.TransactionPrefix transaction, uint index)
    //	{
    //	  if (transaction.outputs.Count <= index)
    //	  {
    //		throw new System.Exception("Transaction output index out of range");
    //	  }

    //	  return transaction.outputs[index];
    //	}
    //	public static TransactionOutput getOutputChecked(CryptoNote.TransactionPrefix transaction, uint index, TransactionTypes.OutputType type)
    //	{
    //	  auto output = getOutputChecked(transaction, new uint(index));
    //	  if (getTransactionOutputType(output.target) != type)
    //	  {
    //		throw new System.Exception("Unexpected transaction output target type");
    //	  }

    //	  return output;
    //	}

    //	public static bool findOutputsToAccount(CryptoNote.TransactionPrefix transaction, AccountPublicAddress addr, SecretKey viewSecretKey, List<uint> @out, ref ulong amount)
    //	{
    //	  AccountKeys keys = new AccountKeys();
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: keys.address = addr;
    //	  keys.address.CopyFrom(addr);
    //	  // only view secret key is used, spend key is not needed
    //	  keys.viewSecretKey = viewSecretKey;

    //	  Crypto.PublicKey txPubKey = getTransactionPublicKeyFromExtra(transaction.extra);

    //	  amount = 0;
    //	  uint keyIndex = 0;
    //	  uint outputIndex = 0;

    //	  Crypto.KeyDerivation derivation = new Crypto.KeyDerivation();
    //	  generate_key_derivation(txPubKey, keys.viewSecretKey, derivation);

    //	  foreach (TransactionOutput o in transaction.outputs)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //		Debug.Assert(o.target.type() == typeid(KeyOutput));
    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //		if (o.target.type() == typeid(KeyOutput))
    //		{
    //		  if (is_out_to_acc(keys, boost::get<KeyOutput>(o.target), derivation, new uint(keyIndex)))
    //		  {
    //			@out.Add(outputIndex);
    //			amount += o.amount;
    //		  }

    //		  ++keyIndex;
    //		}

    //		++outputIndex;
    //	  }

    //	  return true;
    //	}

    //	public static void mergeStates(TransactionValidatorState destination, TransactionValidatorState source)
    //	{
    //	  destination.spentKeyImages.insert(source.spentKeyImages.GetEnumerator(), source.spentKeyImages.end());
    //	}
    //	public static bool hasIntersections(TransactionValidatorState destination, TransactionValidatorState source)
    //	{
    //  return std::any_of(source.spentKeyImages.GetEnumerator(), source.spentKeyImages.end(), (Crypto.KeyImage ki) =>
    //  {
    //	  return destination.spentKeyImages.count(ki) != 0;
    //  });
    //	}
    //	public static void excludeFromState(TransactionValidatorState state, CachedTransaction cachedTransaction)
    //	{
    //	  auto transaction = cachedTransaction.getTransaction();
    //	  foreach (var input in transaction.inputs)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
    //		if (input.type() == typeid(KeyInput))
    //		{
    //		  auto in = boost::get<KeyInput>(input);
    //		  Debug.Assert(state.spentKeyImages.count(in.keyImage) > 0);
    //		  state.spentKeyImages.erase(in.keyImage);
    //		}
    //		else
    //		{
    //		  Debug.Assert(false);
    //		}
    //	  }
    //	}

    //	public static std::unique_ptr<IUpgradeDetector> makeUpgradeDetector(ushort targetVersion, uint upgradeIndex)
    //	{
    //	  return std::unique_ptr<SimpleUpgradeDetector>(new SimpleUpgradeDetector(new ushort(targetVersion), new uint(upgradeIndex)));
    //	}
    //	public static bool post_notify<t_parametr>(IP2pEndpoint p2p, t_parametr.request arg, CryptoNoteConnectionContext context)
    //	{
    //	  return p2p.invoke_notify_to_peer(t_parametr.ID, LevinProtocol.encode(arg), context);
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<class t_parametr>
    //	public static void relay_post_notify<t_parametr>(IP2pEndpoint p2p, t_parametr.request arg, boost::uuids.uuid excludeConnection = null)
    //	{
    //	  p2p.externalRelayNotifyToAll(t_parametr.ID, LevinProtocol.encode(arg), excludeConnection);
    //	}

    //	public static List<RawBlockLegacy> convertRawBlocksToRawBlocksLegacy(List<RawBlock> rawBlocks)
    //	{
    //	  List<RawBlockLegacy> legacy = new List<RawBlockLegacy>();
    //	  legacy.Capacity = rawBlocks.Count;

    //	  foreach (var rawBlock in rawBlocks)
    //	  {
    //		legacy.emplace_back(new RawBlockLegacy({rawBlock.block, rawBlock.transactions}));
    //	  }

    //	  return legacy;
    //	}

    //	public static List<RawBlock> convertRawBlocksLegacyToRawBlocks(List<RawBlockLegacy> legacy)
    //	{
    //	  List<RawBlock> rawBlocks = new List<RawBlock>();
    //	  rawBlocks.Capacity = legacy.Count;

    //	  foreach (var legacyBlock in legacy)
    //	  {
    //		rawBlocks.emplace_back(new RawBlock({legacyBlock.block, legacyBlock.transactions}));
    //	  }

    //	  return rawBlocks;
    //	}


    //	// unpack to strings to maintain protocol compatibility with older versions
    //	internal static void serialize(RawBlockLegacy rawBlock, ISerializer serializer)
    //	{
    //	  string block;
    //	  List<string> transactions = new List<string>();
    //	  if (serializer.type() == ISerializer.INPUT)
    //	  {
    //		serializer.functorMethod(block, "block");
    //		serializer.functorMethod(transactions, "txs");
    //		rawBlock.block.reserve(block.Length);
    //		rawBlock.transactions.Capacity = transactions.Count;
    //		std::copy(block.GetEnumerator(), block.end(), std::back_inserter(rawBlock.block));
    //	std::transform(transactions.GetEnumerator(), transactions.end(), std::back_inserter(rawBlock.transactions), (string s) =>
    //	{
    //	  return new List<ushort>(s.GetEnumerator(), s.end());
    //	});
    //	  }
    //	  else
    //	  {
    //		block.reserve(rawBlock.block.size());
    //		transactions.Capacity = rawBlock.transactions.Count;
    //		std::copy(rawBlock.block.begin(), rawBlock.block.end(), std::back_inserter(block));
    //	std::transform(rawBlock.transactions.GetEnumerator(), rawBlock.transactions.end(), std::back_inserter(transactions), (List<ushort> s) =>
    //	{
    //	  return (string)(s.GetEnumerator(), s.end());
    //	});
    //		serializer.functorMethod(block, "block");
    //		serializer.functorMethod(transactions, "txs");
    //	  }
    //	}

    //	internal static void serialize(NOTIFY_NEW_BLOCK_request request, ISerializer s)
    //	{
    //	  s.functorMethod(request.b, "b");
    //	  s.functorMethod(request.current_blockchain_height, "current_blockchain_height");
    //	  s.functorMethod(request.hop, "hop");
    //	}

    //	// unpack to strings to maintain protocol compatibility with older versions
    //	internal static void serialize(NOTIFY_NEW_TRANSACTIONS_request request, ISerializer s)
    //	{
    //	  List<string> transactions = new List<string>();
    //	  if (s.type() == ISerializer.INPUT)
    //	  {
    //		s.functorMethod(transactions, "txs");
    //		request.txs.Capacity = transactions.Count;
    //	std::transform(transactions.GetEnumerator(), transactions.end(), std::back_inserter(request.txs), (string s) =>
    //	{
    //	  return new List<ushort>(s.GetEnumerator(), s.end());
    //	});
    //	  }
    //	  else
    //	  {
    //		transactions.Capacity = request.txs.Count;
    //	std::transform(request.txs.GetEnumerator(), request.txs.end(), std::back_inserter(transactions), (List<ushort> s) =>
    //	{
    //	  return (string)(s.GetEnumerator(), s.end());
    //	});
    //		s(transactions, "txs");
    //	  }
    //	}

    //	internal static void serialize(NOTIFY_RESPONSE_GET_OBJECTS_request request, ISerializer s)
    //	{
    //	  s.functorMethod(request.txs, "txs");
    //	  s.functorMethod(request.blocks, "blocks");
    //	  serializeAsBinary(request.missed_ids, "missed_ids", s.functorMethod);
    //	  s.functorMethod(request.current_blockchain_height, "current_blockchain_height");
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename Command, typename Handler>
    //	public static int notifyAdaptor<Command, Handler>(List<ushort> reqBuf, CryptoNoteConnectionContext ctx, Handler handler)
    //	{

    //	  int command = Command.ID;

    //	  Command.request req = boost::value_initialized<typename Command.request>();
    //	  if (!LevinProtocol.decode(reqBuf, req))
    //	  {
    //		throw new System.Exception("Failed to load_from_binary in command " + Convert.ToString(command));
    //	  }

    //	  return handler(command, req, ctx);
    //	}
    //	public static bool serialize<T>(T value, Common.StringView name, ISerializer serializer)
    //	{
    //	  if (!serializer.beginObject(new Common.StringView(name)))
    //	  {
    //		return false;
    //	  }

    //	  serialize(value, serializer.functorMethod);
    //	  serializer.endObject();
    //	  return true;
    //	}

    //	/* WARNING: If you get a compiler error pointing to this line, when serializing
    //	   a ulong, or other numeric type, this is due to your compiler treating some
    //	   typedef's differently, so it does not correspond to one of the numeric
    //	   types above. I tried using some template hackery to get around this, but
    //	   it did not work. I resorted to just using a ulong instead. */
    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	public static void serialize<T>(T value, ISerializer serializer)
    //	{
    //	  value.serialize(serializer.functorMethod);
    //	}
    //	public static std::enable_if<std::is_pod<T>.value>.type serializeAsBinary<T>(List<T> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  string blob;
    //	  if (serializer.type() == ISerializer.INPUT)
    //	  {
    //		serializer.binary(blob, new Common.StringView(name));
    //		value.Resize(blob.Length / sizeof(T));
    //		if (blob.Length != 0)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
    //		  memcpy(value[0], blob.data(), blob.Length);
    //		}
    //	  }
    //	  else
    //	  {
    //		if (value.Count > 0)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		  blob.assign(reinterpret_cast<const char>(value[0]), value.Count * sizeof(T));
    //		}
    //		serializer.binary(blob, new Common.StringView(name));
    //	  }
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	public static std::enable_if<std::is_pod<T>.value>.type serializeAsBinary<T>(LinkedList<T> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  string blob;
    //	  if (serializer.type() == ISerializer.INPUT)
    //	  {
    //		serializer.binary(blob, new Common.StringView(name));

    //		ulong count = blob.Length / sizeof(T);
    ////C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on this variable, so pointers on this variable are left unchanged:
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		T * ptr = reinterpret_cast<const T>(blob.data());

    //		while (count-- != null)
    //		{
    //		  value.AddLast(*ptr++);
    //		}
    //	  }
    //	  else
    //	  {
    //		if (value.Count > 0)
    //		{
    //		  blob.resize(value.Count * sizeof(T));
    ////C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on this variable, so pointers on this variable are left unchanged:
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		  T * ptr = reinterpret_cast<T>(blob[0]);

    //		  foreach (var item in value)
    //		  {
    //			*ptr++= item;
    //		  }
    //		}
    //		serializer.binary(blob, new Common.StringView(name));
    //	  }
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename Cont>
    //	public static bool serializeContainer<Cont>(Cont value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  ulong size = value.size();
    //	  if (!serializer.beginArray(size, new Common.StringView(name)))
    //	  {
    //		if (serializer.type() == ISerializer.INPUT)
    //		{
    //		  value.clear();
    //		}

    //		return false;
    //	  }

    //	  value.resize(size);

    //	  foreach (var item in value)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
    //		serializer.functorMethod(const_cast<typename Cont.value_type&>(item), "");
    //	  }

    //	  serializer.endArray();
    //	  return true;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename E>
    //	public static bool serializeEnumClass<E>(ref E value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
    //	//  static_assert(std::is_enum<E>::value, "E must be an enum class");


    //	  if (serializer.type() == CryptoNote.ISerializer.INPUT)
    //	  {
    //		std::underlying_type<E>.type numericValue = new std::underlying_type<E>.type();
    //		serializer.functorMethod(numericValue, name);
    //		value = (E)numericValue;
    //	  }
    //	  else
    //	  {
    //		var numericValue = (typename std::underlying_type<E>.type)value;
    //		serializer.functorMethod(numericValue, name);
    //	  }

    //	  return true;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	public static bool serialize<T>(List<T> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  return serializeContainer(value, new Common.StringView(name), serializer.functorMethod);
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	public static bool serialize<T>(LinkedList<T> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  return serializeContainer(value, new Common.StringView(name), serializer.functorMethod);
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename MapT, typename ReserveOp>
    //	public static bool serializeMap<MapT, ReserveOp>(MapT value, Common.StringView name, CryptoNote.ISerializer serializer, ReserveOp reserve)
    //	{
    //	  ulong size = value.size();

    //	  if (!serializer.beginArray(size, new Common.StringView(name)))
    //	  {
    //		if (serializer.type() == ISerializer.INPUT)
    //		{
    //		  value.clear();
    //		}

    //		return false;
    //	  }

    //	  if (serializer.type() == CryptoNote.ISerializer.INPUT)
    //	  {
    //		reserve(size);

    //		for (ulong i = 0; i < size; ++i)
    //		{
    //		  MapT.key_type key = new MapT.key_type();
    //		  MapT.mapped_type v = new MapT.mapped_type();

    //		  serializer.beginObject("");
    //		  serializer.functorMethod(key, "key");
    //		  serializer.functorMethod(v, "value");
    //		  serializer.endObject();

    //		  value.insert(Tuple.Create(std::move(key), std::move(v)));
    //		}
    //	  }
    //	  else
    //	  {
    //		foreach (var kv in value)
    //		{
    //		  serializer.beginObject("");
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
    //		  serializer.functorMethod(const_cast<typename MapT.key_type&>(kv.first), "key");
    //		  serializer.functorMethod(kv.second, "value");
    //		  serializer.endObject();
    //		}
    //	  }

    //	  serializer.endArray();
    //	  return true;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename SetT>
    //	public static bool serializeSet<SetT>(SetT value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  ulong size = value.size();

    //	  if (!serializer.beginArray(size, new Common.StringView(name)))
    //	  {
    //		if (serializer.type() == ISerializer.INPUT)
    //		{
    //		  value.clear();
    //		}

    //		return false;
    //	  }

    //	  if (serializer.type() == CryptoNote.ISerializer.INPUT)
    //	  {
    //		for (ulong i = 0; i < size; ++i)
    //		{
    //		  SetT.value_type key = new SetT.value_type();
    //		  serializer.functorMethod(key, "");
    //		  value.insert(std::move(key));
    //		}
    //	  }
    //	  else
    //	  {
    //		foreach (var key in value)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
    //		  serializer.functorMethod(const_cast<typename SetT.value_type&>(key), "");
    //		}
    //	  }

    //	  serializer.endArray();
    //	  return true;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename K, typename Hash>
    //	public static bool serialize<K, Hash>(HashSet<K, Hash> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  return serializeSet(value, new Common.StringView(name), serializer.functorMethod);
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename K, typename Cmp>
    //	public static bool serialize<K, Cmp>(SortedSet<K, Cmp> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  return serializeSet(value, new Common.StringView(name), serializer.functorMethod);
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename K, typename V, typename Hash>
    //	public static bool serialize<K, V, Hash>(Dictionary<K, V, Hash> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //  return serializeMap(value, new Common.StringView(name), serializer.functorMethod, (ulong size) =>
    //  {
    //	  value.reserve(size);
    //  });
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename K, typename V, typename Hash>
    //	public static bool serialize<K, V, Hash>(std::unordered_multimap<K, V, Hash> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //  return serializeMap(value, new Common.StringView(name), serializer.functorMethod, (ulong size) =>
    //  {
    //	  value.reserve(size);
    //  });
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename K, typename V, typename Hash>
    //	public static bool serialize<K, V, Hash>(SortedDictionary<K, V, Hash> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //  return serializeMap(value, new Common.StringView(name), serializer.functorMethod, (ulong size) =>
    //  {
    //  });
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename K, typename V, typename Hash>
    //	public static bool serialize<K, V, Hash>(std::multimap<K, V, Hash> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //  return serializeMap(value, new Common.StringView(name), serializer.functorMethod, (ulong size) =>
    //  {
    //  });
    //	}

    //	//C++ TO C# CONVERTER TODO TASK: C++ 'constraints' are not converted by C++ to C# Converter:
    //	//ORIGINAL LINE: template<ulong size>
    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename size>
    //	public static bool serialize<size>(List<ushort> value, Common.StringView name, CryptoNote.ISerializer s)
    //	{
    //	  return s.binary(value.data(), value.Count, new Common.StringView(name));
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T1, typename T2>
    //	public static void serialize<T1, T2>(Tuple<T1, T2> value, ISerializer s)
    //	{
    //	  s.functorMethod(value.Item1, "first");
    //	  s.functorMethod(value.Item2, "second");
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename Element, typename Iterator>
    //	public static void writeSequence<Element, Iterator>(Iterator begin, Iterator end, Common.StringView name, ISerializer s)
    //	{
    //	  ulong size = std::distance(begin, end);
    //	  s.beginArray(size, new Common.StringView(name));
    //	  for (Iterator i = begin; i != end; ++i)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
    //		s.functorMethod(const_cast<Element&>(*i), "");
    //	  }
    //	  s.endArray();
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename Element, typename Iterator>
    //	public static void readSequence<Element, Iterator>(Iterator outputIterator, Common.StringView name, ISerializer s)
    //	{
    //	  ulong size = 0;
    //	  // array of zero size is not written in KVBinaryOutputStreamSerializer
    //	  if (!s.beginArray(size, new Common.StringView(name)))
    //	  {
    //		return;
    //	  }

    //	  while (size-- != null)
    //	  {
    //		Element e = new default(Element);
    //		s.functorMethod(e, "");
    //		*outputIterator++= std::move(e);
    //	  }

    //	  s.endArray();
    //	}

    //	//convinience function since we change block height type
    //	//void serializeBlockHeight(ISerializer s, uint blockHeight, Common::StringView name);Tangible Method Implementation Not FoundCryptoNote-serializeBlockHeight

    //	//convinience function since we change global output index type
    //	//void serializeGlobalOutputIndex(ISerializer s, uint globalOutputIndex, Common::StringView name);Tangible Method Implementation Not FoundCryptoNote-serializeGlobalOutputIndex

    //	//void serialize(TransactionOutputDetails output, ISerializer serializer);Tangible Method Implementation Not FoundCryptoNote-serialize
    //	//void serialize(TransactionOutputReferenceDetails outputReference, ISerializer serializer);Tangible Method Implementation Not FoundCryptoNote-serialize

    //	//void serialize(BaseInputDetails inputBase, ISerializer serializer);Tangible Method Implementation Not FoundCryptoNote-serialize
    //	//void serialize(KeyInputDetails inputToKey, ISerializer serializer);Tangible Method Implementation Not FoundCryptoNote-serialize
    //	//void serialize(boost::variant<BaseInputDetails, KeyInputDetails> input, ISerializer serializer);Tangible Method Implementation Not FoundCryptoNote-serialize

    //	//void serialize(TransactionExtraDetails extra, ISerializer serializer);Tangible Method Implementation Not FoundCryptoNote-serialize
    //	//void serialize(TransactionDetails transaction, ISerializer serializer);Tangible Method Implementation Not FoundCryptoNote-serialize

    //	//void serialize(BlockDetails block, ISerializer serializer);Tangible Method Implementation Not FoundCryptoNote-serialize
    //	public static Common.JsonValue storeToJsonValue<T>(T v)
    //	{
    //	  JsonOutputStreamSerializer s = new JsonOutputStreamSerializer();
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
    //	  serialize(const_cast<T&>(v), s.functorMethod);
    //	  return s.getValue.functorMethod();
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T>
    //	public static Common.JsonValue storeContainerToJsonValue<T>(T cont)
    //	{
    //	  Common.JsonValue js = new Common.JsonValue(Common.JsonValue.ARRAY);
    //	  foreach (var item in cont)
    //	  {
    //		js.pushBack.functorMethod(item);
    //	  }
    //	  return js.functorMethod;
    //	}

    //	public static Common.JsonValue storeContainerToJsonValue(List<AddressBookEntry> cont)
    //	{
    //	  Common.JsonValue js = new Common.JsonValue(Common.JsonValue.ARRAY);
    //	  foreach (var item in cont)
    //	  {
    //		js.pushBack.functorMethod(storeToJsonValue.functorMethod(item));
    //	  }
    //	  return js.functorMethod;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T>
    //	public static Common.JsonValue storeToJsonValue<T>(List<T> v)
    //	{
    //		return storeContainerToJsonValue.functorMethod(v);
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T>
    //	public static Common.JsonValue storeToJsonValue<T>(LinkedList<T> v)
    //	{
    //		return storeContainerToJsonValue.functorMethod(v);
    //	}

    //	public static Common.JsonValue storeToJsonValue(string v)
    //	{
    //		return new Common.JsonValue(v);
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T>
    //	public static void loadFromJsonValue<T>(T v, Common.JsonValue js)
    //	{
    //	  JsonInputValueSerializer s = new JsonInputValueSerializer(js.functorMethod);
    //	  serialize(v, s.functorMethod);
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T>
    //	public static void loadFromJsonValue<T>(List<T> v, Common.JsonValue js)
    //	{
    //	  for (ulong i = 0; i < js.size(); ++i)
    //	  {
    //		v.Add(Common.GlobalMembers.getValueAs<T>(js.functorMethod[i]));
    //	  }
    //	}

    //	public static void loadFromJsonValue(AddressBook v, Common.JsonValue js)
    //	{
    //	  for (ulong i = 0; i < js.size(); ++i)
    //	  {
    //		AddressBookEntry type = new AddressBookEntry();
    //		loadFromJsonValue(type, js.functorMethod[i]);
    //		v.push_back(type);
    //	  }
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T>
    //	public static void loadFromJsonValue<T>(LinkedList<T> v, Common.JsonValue js)
    //	{
    //	  for (ulong i = 0; i < js.size(); ++i)
    //	  {
    //		v.AddLast(Common.GlobalMembers.getValueAs<T>(js.functorMethod[i]));
    //	  }
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T>
    //	public static string storeToJson<T>(T v)
    //	{
    //	  return storeToJsonValue.functorMethod(v).toString();
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T>
    //	public static bool loadFromJson<T>(T v, string buf)
    //	{
    //	  try
    //	  {
    //		if (string.IsNullOrEmpty(buf))
    //		{
    //		  return true;
    //		}
    //		var js = Common.JsonValue.fromString.functorMethod(buf);
    //		loadFromJsonValue(v, js.functorMethod);
    //	  }
    //	  catch (System.Exception)
    //	  {
    //		return false;
    //	  }
    //	  return true;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T>
    //	public static string storeToBinaryKeyValue<T>(T v)
    //	{
    //	  KVBinaryOutputStreamSerializer s = new KVBinaryOutputStreamSerializer();
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
    //	  serialize(const_cast<T&>(v), s.functorMethod);

    //	  string result;
    //	  Common.StringOutputStream stream = new Common.StringOutputStream(result);
    //	  s.dump(stream);
    //	  return result;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T>
    //	public static bool loadFromBinaryKeyValue<T>(T v, string buf)
    //	{
    //	  try
    //	  {
    //		Common.MemoryInputStream stream = new Common.MemoryInputStream(buf.data(), buf.Length);
    //		KVBinaryInputStreamSerializer s = new KVBinaryInputStreamSerializer(stream);
    //		serialize(v, s);
    //		return true;
    //	  }
    //	  catch (System.Exception)
    //	  {
    //		return false;
    //	  }
    //	}

    //	public static readonly int LEVIN_PROTOCOL_RETCODE_SUCCESS = 1;

    //	public static readonly uint CONCURRENCY_LEVEL = std::thread.hardware_concurrency();

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <class Container>
    //	public static void split<Container>(string str, Container cont, char delim = ' ')
    //	{
    //		std::stringstream ss = new std::stringstream(str);
    //		string token;
    //		while (getline(ss, token, delim))
    //		{
    //			cont.push_back(token);
    //		}
    //	}

    //	public static bool parseDaemonAddressFromString(ref string host, ref int port, string address)
    //	{
    //	  List<string> parts = new List<string>();
    //	  GlobalMembers.split(address, parts, ':');

    //	  if (parts.Count == 0)
    //	  {
    //		return false;
    //	  }
    //	  else if (parts.Count >= 2)
    //	  {
    //		try
    //		{
    //		  host = parts[0];
    //		  port = Convert.ToInt32(parts[1]);
    //		  return true;
    //		}
    //		catch (System.Exception e)
    //		{
    //		  return false;
    //		}
    //	  }

    //	  host = parts[0];
    //	  port = CryptoNote.RPC_DEFAULT_PORT;
    //	  return true;
    //	}

    //	public static std::error_code interpretResponseStatus(string status)
    //	{
    //	  if (DefineConstants.CORE_RPC_STATUS_BUSY == status)
    //	  {
    //		return GlobalMembers.make_error_code(error.NODE_BUSY);
    //	  }
    //	  else if (DefineConstants.CORE_RPC_STATUS_OK != status)
    //	  {
    //		return GlobalMembers.make_error_code(error.INTERNAL_NODE_ERROR);
    //	  }
    //	  return std::error_code();
    //	}


    //	internal static void serialize(COMMAND_RPC_GET_BLOCKS_FAST.response response, ISerializer s)
    //	{
    //	  s.functorMethod(response.blocks, "response.blocks");
    //	  s.functorMethod(response.start_height, "response.start_height");
    //	  s.functorMethod(response.current_height, "response.current_height");
    //	  s.functorMethod(response.status, "response.status");
    //	}

    //	public static readonly int LEVIN_PROTOCOL_RETCODE_SUCCESS = 1;

    //	public static readonly command_line.arg_descriptor<string> arg_p2p_bind_ip = new command_line.arg_descriptor<string>("p2p-bind-ip", "Interface for p2p network protocol", "0.0.0.0");
    //	public static readonly command_line.arg_descriptor<string> arg_p2p_bind_port = new command_line.arg_descriptor<string>("p2p-bind-port", "Port for p2p network protocol", std::to_string(CryptoNote.P2P_DEFAULT_PORT));
    //	public static readonly command_line.arg_descriptor<uint> arg_p2p_external_port = new command_line.arg_descriptor<uint>("p2p-external-port", "External port for p2p network protocol (if port forwarding used with NAT)", 0);
    //	public static readonly command_line.arg_descriptor<bool> arg_p2p_allow_local_ip = new command_line.arg_descriptor<bool>("allow-local-ip", "Allow local ip add to peer list, mostly in debug purposes");
    //	public static readonly command_line.arg_descriptor<List<string>> arg_p2p_add_peer = new command_line.arg_descriptor<List<string>>("add-peer", "Manually add peer to local peerlist");
    //	public static readonly command_line.arg_descriptor<List<string>> arg_p2p_add_priority_node = new command_line.arg_descriptor<List<string>>("add-priority-node", "Specify list of peers to connect to and attempt to keep the connection open");
    //	public static readonly command_line.arg_descriptor<List<string>> arg_p2p_add_exclusive_node = new command_line.arg_descriptor<List<string>>("add-exclusive-node", "Specify list of peers to connect to only." " If this option is given the options add-priority-node and seed-node are ignored");
    //	public static readonly command_line.arg_descriptor<List<string>> arg_p2p_seed_node = new command_line.arg_descriptor<List<string>>("seed-node", "Connect to a node to retrieve peer addresses, and disconnect");
    //	public static readonly command_line.arg_descriptor<bool> arg_p2p_hide_my_port = new command_line.arg_descriptor<bool>("hide-my-port", "Do not announce yourself as peerlist candidate", false, true);

    //	public static string print_peerlist_to_string(LinkedList<PeerlistEntry> pl)
    //	{
    //	  DateTime now_time = 0;
    //	  time(now_time);
    //	  std::stringstream ss = new std::stringstream();
    //	  ss << std::setfill('0') << std::setw(8) << std::hex << std::noshowbase;
    //	  foreach (var pe in pl)
    //	  {
    //		ss << pe.id << "\t" << pe.adr << " \tlast_seen: " << Common.timeIntervalToString(now_time - pe.last_seen) << std::endl;
    //	  }
    //	  return ss.str();
    //	}
    //	  public static int invokeAdaptor<Command, Handler>(List<ushort> reqBuf, ref List<ushort> resBuf, P2pConnectionContext ctx, Handler handler)
    //	  {
    //		int command = Command.ID;

    //		Command.request req = boost::value_initialized<typename Command.request>();

    //		if (!LevinProtocol.decode(reqBuf, req))
    //		{
    //		  throw new System.Exception("Failed to load_from_binary in command " + Convert.ToString(command));
    //		}

    //		Command.response res = boost::value_initialized<typename Command.response>();
    //		int ret = handler(command, req, res, ctx);
    //		resBuf = LevinProtocol.encode(res);
    //		return ret;
    //	  }

    //	public static bool parsePeerFromString(NetworkAddress pe, string node_addr)
    //	{
    //	  return Common.parseIpAddressAndPort(pe.ip, pe.port, node_addr);
    //	}

    //	public static bool parsePeersAndAddToNetworkContainer(List<string> peerList, List<NetworkAddress> container)
    //	{
    //	  foreach (string peer in peerList)
    //	  {
    //		NetworkAddress networkAddress = new NetworkAddress();
    //		if (!GlobalMembers.parsePeerFromString(networkAddress, peer))
    //		{
    //		  return false;
    //		}
    //		container.Add(networkAddress);
    //	  }
    //	  return true;
    //	}

    //	public static bool parsePeersAndAddToPeerListContainer(List<string> peerList, List<PeerlistEntry> container)
    //	{
    //	  foreach (string peer in peerList)
    //	  {
    //		PeerlistEntry peerListEntry = new PeerlistEntry();
    //		peerListEntry.id = Crypto.GlobalMembers.rand<ulong>();
    //		if (!GlobalMembers.parsePeerFromString(peerListEntry.adr, peer))
    //		{
    //		  return false;
    //		}
    //		container.Add(peerListEntry);
    //	  }
    //	  return true;
    //	}

    //	public static P2pContext.Message makeReply(uint command, List<ushort> data, uint returnCode)
    //	{
    //	  return new P2pContext.Message(new P2pMessage({command, data}), P2pContext.Message.REPLY, new uint(returnCode));
    //	}
    //	public static P2pContext.Message makeRequest(uint command, List<ushort> data)
    //	{
    //	  return new P2pContext.Message(new P2pMessage({command, data}), P2pContext.Message.REQUEST);
    //	}

    //	public static std::ostream operator << (std::ostream s, P2pContext conn)
    //	{
    //	  return s << "[" << conn.getRemoteAddress() << "]";
    //	}

    //	public static NetworkAddress getRemoteAddress(TcpConnection connection)
    //	{
    //	  var addressAndPort = connection.getPeerAddressAndPort();
    //	  NetworkAddress remoteAddress = new NetworkAddress();
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: remoteAddress.ip = hostToNetwork(addressAndPort.first.getValue());
    //	  remoteAddress.ip.CopyFrom(GlobalMembers.hostToNetwork(addressAndPort.first.getValue()));
    //	  remoteAddress.port = addressAndPort.second;
    //	  return remoteAddress;
    //	}

    //	public static void doWithTimeoutAndThrow(System.Dispatcher dispatcher, std::chrono.nanoseconds timeout, Action f)
    //	{
    //	  string result;
    //	  System.ContextGroup cg = new System.ContextGroup(dispatcher);
    //	  System.ContextGroupTimeout cgTimeout = new System.ContextGroupTimeout(dispatcher, cg, timeout);

    //  cg.spawn(() =>
    //  {
    //	try
    //	{
    //	  f();
    //	}
    //	catch (System.InterruptedException)
    //	{
    //	  result = "Operation timeout";
    //	}
    //	catch (System.Exception e)
    //	{
    //	  result = e.Message;
    //	}
    //  });

    //	  cg.wait();

    //	  if (!string.IsNullOrEmpty(result))
    //	  {
    //		throw new System.Exception(result);
    //	  }
    //	}

    //	public static LinkedList<PeerlistEntry> fixTimeDelta(LinkedList<PeerlistEntry> peerlist, DateTime remoteTime)
    //	{
    //	  //fix time delta
    //	  long delta = time(null) - remoteTime;
    //	  LinkedList<PeerlistEntry> peerlistCopy = new LinkedList<PeerlistEntry>(peerlist);

    //	  foreach (PeerlistEntry be in peerlistCopy)
    //	  {
    //		if (be.last_seen > ulong(remoteTime))
    //		{
    //		  throw new System.Exception("Invalid peerlist entry (time in future)");
    //		}

    //		be.last_seen += delta;
    //	  }

    //	  return peerlistCopy;
    //	}

    //	public static readonly std::chrono.nanoseconds P2P_DEFAULT_CONNECT_INTERVAL = std::chrono.seconds(2);
    //	public static readonly uint P2P_DEFAULT_CONNECT_RANGE = 20;
    //	public static readonly uint P2P_DEFAULT_PEERLIST_GET_TRY_COUNT = 10;
    //	public static void invokeJsonCommand<Request, Response>(HttpClient client, string url, Request req, Response res)
    //	{
    //	  HttpRequest hreq = new HttpRequest();
    //	  HttpResponse hres = new HttpResponse();

    //	  hreq.addHeader("Content-Type", "application/json");
    //	  hreq.setUrl(url);
    //	  hreq.setBody(storeToJson(req));
    //	  client.request(hreq, hres);

    //	  if (hres.getStatus() != HttpResponse.STATUS_200)
    //	  {
    //		throw new System.Exception("HTTP status: " + Convert.ToString(hres.getStatus()));
    //	  }

    //	  if (!loadFromJson(res, hres.getBody()))
    //	  {
    //		throw new System.Exception("Failed to parse JSON response");
    //	  }
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename Request, typename Response>
    //	public static void invokeBinaryCommand<Request, Response>(HttpClient client, string url, Request req, Response res)
    //	{
    //	  HttpRequest hreq = new HttpRequest();
    //	  HttpResponse hres = new HttpResponse();

    //	  hreq.setUrl(url);
    //	  hreq.setBody(storeToBinaryKeyValue(req));
    //	  client.request(hreq, hres);

    //	  if (!loadFromBinaryKeyValue(res, hres.getBody()))
    //	  {
    //		throw new System.Exception("Failed to parse binary response");
    //	  }
    //	}

    //	internal static void serialize(COMMAND_RPC_GET_BLOCKS_FAST.response response, ISerializer s)
    //	{
    //	  s.functorMethod(response.blocks, "response.blocks");
    //	  s.functorMethod(response.start_height, "response.start_height");
    //	  s.functorMethod(response.current_height, "response.current_height");
    //	  s.functorMethod(response.status, "response.status");
    //	}

    //	public static void serialize(BlockFullInfo blockFullInfo, ISerializer s)
    //	{
    //	  s.functorMethod(blockFullInfo.block_id, "blockFullInfo.block_id");
    //	  s.functorMethod(blockFullInfo.block, "blockFullInfo.block");
    //	  s.functorMethod(blockFullInfo.transactions, "txs");
    //	}

    //	public static void serialize(TransactionPrefixInfo transactionPrefixInfo, ISerializer s)
    //	{
    //	  s.functorMethod(transactionPrefixInfo.txHash, "transactionPrefixInfo.txHash");
    //	  s.functorMethod(transactionPrefixInfo.txPrefix, "transactionPrefixInfo.txPrefix");
    //	}

    //	public static void serialize(BlockShortInfo blockShortInfo, ISerializer s)
    //	{
    //	  s.functorMethod(blockShortInfo.blockId, "blockShortInfo.blockId");
    //	  s.functorMethod(blockShortInfo.block, "blockShortInfo.block");
    //	  s.functorMethod(blockShortInfo.txPrefixes, "blockShortInfo.txPrefixes");
    //	}
    //private delegate bool handlerDelegate(Command.request UnnamedParameter, Command.response UnnamedParameter2);

    //	public static RpcServer.HandlerFunction jsonMethod<Command>(handlerDelegate handler)
    //	{
    ////C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
    ////ORIGINAL LINE: return [handler](RpcServer* obj, const HttpRequest& request, HttpResponse& response)
    //  return (RpcServer obj, HttpRequest request, HttpResponse response) =>
    //  {

    //	boost::value_initialized<typename Command.request> req = new boost::value_initialized<typename Command.request>();
    //	boost::value_initialized<typename Command.response> res = new boost::value_initialized<typename Command.response>();

    //	if (!loadFromJson((typename Command.request)req, request.getBody()))
    //	{
    //	  return false;
    //	}

    //	bool result = handler(req, res);
    //	foreach (var cors_domain in obj.getCorsDomains())
    //	{
    //	  response.addHeader("Access-Control-Allow-Origin", cors_domain);
    //	}
    //	response.addHeader("Content-Type", "application/json");
    //	response.setBody(storeToJson(res.data()));
    //	return result;
    //  };
    //	}



    //	public static Dictionary<string, RpcServer.RpcHandler<RpcServer.HandlerFunction>> RpcServer.s_handlers =
    //	{
    //		{
    //			"/getinfo", {jsonMethod<COMMAND_RPC_GET_INFO>(RpcServer.on_get_info), true}
    //		},
    //		{
    //			"/getheight", {jsonMethod<COMMAND_RPC_GET_HEIGHT>(RpcServer.on_get_height), true}
    //		},
    //		{
    //			"/feeinfo", {jsonMethod<COMMAND_RPC_GET_FEE_ADDRESS>(RpcServer.on_get_fee_info), true}
    //		},
    //		{
    //			"/getpeers", {jsonMethod<COMMAND_RPC_GET_PEERS>(RpcServer.on_get_peers), true}
    //		},
    //		{
    //			"/info", {jsonMethod<COMMAND_RPC_GET_INFO>(RpcServer.on_get_info), true}
    //		},
    //		{
    //			"/height", {jsonMethod<COMMAND_RPC_GET_HEIGHT>(RpcServer.on_get_height), true}
    //		},
    //		{
    //			"/fee", {jsonMethod<COMMAND_RPC_GET_FEE_ADDRESS>(RpcServer.on_get_fee_info), true}
    //		},
    //		{
    //			"/peers", {jsonMethod<COMMAND_RPC_GET_PEERS>(RpcServer.on_get_peers), true}
    //		},
    //		{
    //			"/gettransactions", {jsonMethod<COMMAND_RPC_GET_TRANSACTIONS>(RpcServer.on_get_transactions), false}
    //		},
    //		{
    //			"/sendrawtransaction", {jsonMethod<COMMAND_RPC_SEND_RAW_TX>(RpcServer.on_send_raw_tx), false}
    //		},
    //		{
    //			"/getblocks", {jsonMethod<COMMAND_RPC_GET_BLOCKS_FAST>(RpcServer.on_get_blocks), false}
    //		},
    //		{
    //			"/queryblocks", {jsonMethod<COMMAND_RPC_QUERY_BLOCKS>(RpcServer.on_query_blocks), false}
    //		},
    //		{
    //			"/queryblockslite", {jsonMethod<COMMAND_RPC_QUERY_BLOCKS_LITE>(RpcServer.on_query_blocks_lite), false}
    //		},
    //		{
    //			"/get_o_indexes", {jsonMethod<COMMAND_RPC_GET_TX_GLOBAL_OUTPUTS_INDEXES>(RpcServer.on_get_indexes), false}
    //		},
    //		{
    //			"/getrandom_outs", {jsonMethod<COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS>(RpcServer.on_get_random_outs), false}
    //		},
    //		{
    //			"/get_pool_changes", {jsonMethod<COMMAND_RPC_GET_POOL_CHANGES>(RpcServer.onGetPoolChanges), false}
    //		},
    //		{
    //			"/get_pool_changes_lite", {jsonMethod<COMMAND_RPC_GET_POOL_CHANGES_LITE>(RpcServer.onGetPoolChangesLite), false}
    //		},
    //		{
    //			"/get_block_details_by_height", {jsonMethod<COMMAND_RPC_GET_BLOCK_DETAILS_BY_HEIGHT>(RpcServer.onGetBlockDetailsByHeight), false}
    //		},
    //		{
    //			"/get_blocks_details_by_heights", {jsonMethod<COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HEIGHTS>(RpcServer.onGetBlocksDetailsByHeights), false}
    //		},
    //		{
    //			"/get_blocks_details_by_hashes", {jsonMethod<COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HASHES>(RpcServer.onGetBlocksDetailsByHashes), false}
    //		},
    //		{
    //			"/get_blocks_hashes_by_timestamps", {jsonMethod<COMMAND_RPC_GET_BLOCKS_HASHES_BY_TIMESTAMPS>(RpcServer.onGetBlocksHashesByTimestamps), false}
    //		},
    //		{
    //			"/get_transaction_details_by_hashes", {jsonMethod<COMMAND_RPC_GET_TRANSACTION_DETAILS_BY_HASHES>(RpcServer.onGetTransactionDetailsByHashes), false}
    //		},
    //		{
    //			"/get_transaction_hashes_by_payment_id", {jsonMethod<COMMAND_RPC_GET_TRANSACTION_HASHES_BY_PAYMENT_ID>(RpcServer.onGetTransactionHashesByPaymentId), false}
    //		},
    //		{
    //			"/json_rpc", {std::bind(RpcServer.processJsonRpcRequest, std::placeholders._1, std::placeholders._2, std::placeholders._3), true}
    //		}
    //	};
    //	  public static ulong slow_memmem(object start_buff, uint buflen, object pat, uint patlen)
    //	  {
    //		object buf = start_buff;
    //		object end = (string)buf + buflen - patlen;
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memchr' has no equivalent in C#:
    //		while ((buf = memchr(buf, ((string)pat)[0], buflen)))
    //		{
    //		  if (buf > end)
    //		  {
    //			return 0;
    //		  }
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
    //		  if (memcmp(buf, pat, patlen) == 0)
    //		  {
    //			return (string)buf - (string)start_buff;
    //		  }
    //		  buf = (string)buf + 1;
    //		}
    //		return 0;
    //	  }

    //	public static ulong get_block_reward(BlockTemplate blk)
    //	{
    //	  ulong reward = 0;
    //	  foreach (TransactionOutput @out in blk.baseTransaction.outputs)
    //	  {
    //		reward += @out.amount;
    //	  }

    //	  return reward;
    //	}
    //	public static void readVarintAs<StorageType, T>(IInputStream s, ref T i)
    //	{
    //	  i = (T)(readVarint<StorageType>(s));
    //	}

    ////namespace CryptoNote {


    //	public static void serialize(TransactionOutputDetails output, ISerializer serializer)
    //	{
    //	  serializer.functorMethod(output.output, "output");
    //	  serializer.functorMethod(output.globalIndex, "globalIndex");
    //	}
    //	public static void serialize(TransactionOutputReferenceDetails outputReference, ISerializer serializer)
    //	{
    //	  GlobalMembers.serializePod(outputReference.transactionHash, "transactionHash", serializer.functorMethod);
    //	  serializer.functorMethod(outputReference.number, "number");
    //	}

    //	public static void serialize(BaseInputDetails inputBase, ISerializer serializer)
    //	{
    //	  serializer.functorMethod(inputBase.input, "input");
    //	  serializer.functorMethod(inputBase.amount, "amount");
    //	}
    //	public static void serialize(KeyInputDetails inputToKey, ISerializer serializer)
    //	{
    //	  serializer.functorMethod(inputToKey.input, "input");
    //	  serializer.functorMethod(inputToKey.mixin, "mixin");
    //	  serializer.functorMethod(inputToKey.output, "output");
    //	}
    //	public static void serialize(boost::variant<BaseInputDetails, KeyInputDetails> input, ISerializer serializer)
    //	{
    //	  if (serializer.type() == ISerializer.OUTPUT)
    //	  {
    //		BinaryVariantTagGetter tagGetter = new BinaryVariantTagGetter();
    //		ushort tag = boost::apply_visitor(tagGetter.functorMethod, input);
    //		serializer.binary(tag, sizeof(ushort), "type");

    //		VariantSerializer visitor = new VariantSerializer(serializer.functorMethod, "data");
    //		boost::apply_visitor(visitor.functorMethod, input);
    //	  }
    //	  else
    //	  {
    //		ushort tag = new ushort();
    //		serializer.binary(tag, sizeof(ushort), "type");

    //		GlobalMembers.getVariantValue(serializer.functorMethod, new ushort(tag), ref input);
    //	  }
    //	}

    //	public static void serialize(TransactionExtraDetails extra, ISerializer serializer)
    //	{
    //	  GlobalMembers.serializePod(extra.publicKey, "publicKey", serializer.functorMethod);
    //	  serializer.functorMethod(extra.nonce, "nonce");
    //	  serializeAsBinary(extra.raw, "raw", serializer.functorMethod);
    //	}
    //	public static void serialize(TransactionDetails transaction, ISerializer serializer)
    //	{
    //	  GlobalMembers.serializePod(transaction.hash, "hash", serializer.functorMethod);
    //	  serializer.functorMethod(transaction.size, "size");
    //	  serializer.functorMethod(transaction.fee, "fee");
    //	  serializer.functorMethod(transaction.totalInputsAmount, "totalInputsAmount");
    //	  serializer.functorMethod(transaction.totalOutputsAmount, "totalOutputsAmount");
    //	  serializer.functorMethod(transaction.mixin, "mixin");
    //	  serializer.functorMethod(transaction.unlockTime, "unlockTime");
    //	  serializer.functorMethod(transaction.timestamp, "timestamp");
    //	  GlobalMembers.serializePod(transaction.paymentId, "paymentId", serializer.functorMethod);
    //	  serializer.functorMethod(transaction.inBlockchain, "inBlockchain");
    //	  GlobalMembers.serializePod(transaction.blockHash, "blockHash", serializer.functorMethod);
    //	  serializer.functorMethod(transaction.blockIndex, "blockIndex");
    //	  serializer.functorMethod(transaction.extra, "extra");
    //	  serializer.functorMethod(transaction.inputs, "inputs");
    //	  serializer.functorMethod(transaction.outputs, "outputs");

    //	  //serializer(transaction.signatures, "signatures");
    //	  if (serializer.type() == ISerializer.OUTPUT)
    //	  {
    //		List<Tuple<ulong, Crypto.Signature>> signaturesForSerialization = new List<Tuple<ulong, Crypto.Signature>>();
    //		signaturesForSerialization.Capacity = transaction.signatures.Count;
    //		ulong ctr = 0;
    //		foreach (var signaturesV in transaction.signatures)
    //		{
    //		  foreach (var signature in signaturesV)
    //		  {
    //			signaturesForSerialization.emplace_back(ctr, std::move(signature));
    //		  }
    //		  ++ctr;
    //		}
    //		ulong size = transaction.signatures.Count;
    //		serializer.functorMethod(size, "signaturesSize");
    //		serializer.functorMethod(signaturesForSerialization, "signatures");
    //	  }
    //	  else
    //	  {
    //		ulong size = 0;
    //		serializer.functorMethod(size, "signaturesSize");
    //		transaction.signatures.Resize(size);

    //		List<Tuple<ulong, Crypto.Signature>> signaturesForSerialization = new List<Tuple<ulong, Crypto.Signature>>();
    //		serializer.functorMethod(signaturesForSerialization, "signatures");

    //		foreach (var signatureWithIndex in signaturesForSerialization)
    //		{
    //		  transaction.signatures[signatureWithIndex.Item1].Add(signatureWithIndex.Item2);
    //		}
    //	  }
    //	}

    //	public static void serialize(BlockDetails block, ISerializer serializer)
    //	{
    //	  serializer.functorMethod(block.majorVersion, "majorVersion");
    //	  serializer.functorMethod(block.minorVersion, "minorVersion");
    //	  serializer.functorMethod(block.timestamp, "timestamp");
    //	  GlobalMembers.serializePod(block.prevBlockHash, "prevBlockHash", serializer.functorMethod);
    //	  serializer.functorMethod(block.nonce, "nonce");
    //	  serializer.functorMethod(block.index, "index");
    //	  GlobalMembers.serializePod(block.hash, "hash", serializer.functorMethod);
    //	  serializer.functorMethod(block.difficulty, "difficulty");
    //	  serializer.functorMethod(block.reward, "reward");
    //	  serializer.functorMethod(block.baseReward, "baseReward");
    //	  serializer.functorMethod(block.blockSize, "blockSize");
    //	  serializer.functorMethod(block.transactionsCumulativeSize, "transactionsCumulativeSize");
    //	  serializer.functorMethod(block.alreadyGeneratedCoins, "alreadyGeneratedCoins");
    //	  serializer.functorMethod(block.alreadyGeneratedTransactions, "alreadyGeneratedTransactions");
    //	  serializer.functorMethod(block.sizeMedian, "sizeMedian");
    //	  /* Some serializers don't support doubles, which causes this to fail and
    //	     not serialize the whole object
    //	  serializer(block.penalty, "penalty");
    //	  */
    //	  serializer.functorMethod(block.totalFeeAmount, "totalFeeAmount");
    //	  serializer.functorMethod(block.transactions, "transactions");
    //	}

    //	public static void getVariantValue(CryptoNote.ISerializer serializer, ushort tag, ref boost::variant<CryptoNote.BaseInputDetails, CryptoNote.KeyInputDetails> in)
    //	{
    //	  switch ((SerializationTag)tag)
    //	  {
    //	  case SerializationTag.Base:
    //	  {
    //		CryptoNote.BaseInputDetails v = new CryptoNote.BaseInputDetails();
    //		serializer.functorMethod(v, "data");
    //		in = v;
    //		break;
    //	  }
    //	  case SerializationTag.Key:
    //	  {
    //		CryptoNote.KeyInputDetails v = new CryptoNote.KeyInputDetails();
    //		serializer.functorMethod(v, "data");
    //		in = v;
    //		break;
    //	  }
    //	  default:
    //		throw new System.Exception("Unknown variant tag");
    //	  }
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T>
    //	public static bool serializePod<T>(T v, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  return serializer.binary(v, sizeof(T), new Common.StringView(name));
    //	}

    //	public static Common.JsonValue getJsonValueFromStreamHelper(std::istream stream)
    //	{
    //	  Common.JsonValue value = new Common.JsonValue();
    //	  stream >> value.functorMethod;
    //	  return value.functorMethod;
    //	}
    //	public static std::enable_if<std::is_pod<T>.value>.type serializeAsBinary<T>(List<T> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  string blob;
    //	  if (serializer.type() == ISerializer.INPUT)
    //	  {
    //		serializer.binary(blob, new Common.StringView(name));
    //		value.Resize(blob.Length / sizeof(T));
    //		if (blob.Length != 0)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
    //		  memcpy(value[0], blob.data(), blob.Length);
    //		}
    //	  }
    //	  else
    //	  {
    //		if (value.Count > 0)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		  blob.assign(reinterpret_cast<const char>(value[0]), value.Count * sizeof(T));
    //		}
    //		serializer.binary(blob, new Common.StringView(name));
    //	  }
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	public static std::enable_if<std::is_pod<T>.value>.type serializeAsBinary<T>(LinkedList<T> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  string blob;
    //	  if (serializer.type() == ISerializer.INPUT)
    //	  {
    //		serializer.binary(blob, new Common.StringView(name));

    //		ulong count = blob.Length / sizeof(T);
    ////C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on this variable, so pointers on this variable are left unchanged:
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		T * ptr = reinterpret_cast<const T>(blob.data());

    //		while (count-- != null)
    //		{
    //		  value.AddLast(*ptr++);
    //		}
    //	  }
    //	  else
    //	  {
    //		if (value.Count > 0)
    //		{
    //		  blob.resize(value.Count * sizeof(T));
    ////C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on this variable, so pointers on this variable are left unchanged:
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		  T * ptr = reinterpret_cast<T>(blob[0]);

    //		  foreach (var item in value)
    //		  {
    //			*ptr++= item;
    //		  }
    //		}
    //		serializer.binary(blob, new Common.StringView(name));
    //	  }
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename Cont>
    //	public static bool serializeContainer<Cont>(Cont value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  ulong size = value.size();
    //	  if (!serializer.beginArray(size, new Common.StringView(name)))
    //	  {
    //		if (serializer.type() == ISerializer.INPUT)
    //		{
    //		  value.clear();
    //		}

    //		return false;
    //	  }

    //	  value.resize(size);

    //	  foreach (var item in value)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
    //		serializer.functorMethod(const_cast<typename Cont.value_type&>(item), "");
    //	  }

    //	  serializer.endArray();
    //	  return true;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename E>
    //	public static bool serializeEnumClass<E>(ref E value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
    //	//  static_assert(std::is_enum<E>::value, "E must be an enum class");


    //	  if (serializer.type() == CryptoNote.ISerializer.INPUT)
    //	  {
    //		std::underlying_type<E>.type numericValue = new std::underlying_type<E>.type();
    //		serializer.functorMethod(numericValue, name);
    //		value = (E)numericValue;
    //	  }
    //	  else
    //	  {
    //		var numericValue = (typename std::underlying_type<E>.type)value;
    //		serializer.functorMethod(numericValue, name);
    //	  }

    //	  return true;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	public static bool serialize<T>(List<T> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  return serializeContainer(value, new Common.StringView(name), serializer.functorMethod);
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename T>
    //	public static bool serialize<T>(LinkedList<T> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  return serializeContainer(value, new Common.StringView(name), serializer.functorMethod);
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename MapT, typename ReserveOp>
    //	public static bool serializeMap<MapT, ReserveOp>(MapT value, Common.StringView name, CryptoNote.ISerializer serializer, ReserveOp reserve)
    //	{
    //	  ulong size = value.size();

    //	  if (!serializer.beginArray(size, new Common.StringView(name)))
    //	  {
    //		if (serializer.type() == ISerializer.INPUT)
    //		{
    //		  value.clear();
    //		}

    //		return false;
    //	  }

    //	  if (serializer.type() == CryptoNote.ISerializer.INPUT)
    //	  {
    //		reserve(size);

    //		for (ulong i = 0; i < size; ++i)
    //		{
    //		  MapT.key_type key = new MapT.key_type();
    //		  MapT.mapped_type v = new MapT.mapped_type();

    //		  serializer.beginObject("");
    //		  serializer.functorMethod(key, "key");
    //		  serializer.functorMethod(v, "value");
    //		  serializer.endObject();

    //		  value.insert(Tuple.Create(std::move(key), std::move(v)));
    //		}
    //	  }
    //	  else
    //	  {
    //		foreach (var kv in value)
    //		{
    //		  serializer.beginObject("");
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
    //		  serializer.functorMethod(const_cast<typename MapT.key_type&>(kv.first), "key");
    //		  serializer.functorMethod(kv.second, "value");
    //		  serializer.endObject();
    //		}
    //	  }

    //	  serializer.endArray();
    //	  return true;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename SetT>
    //	public static bool serializeSet<SetT>(SetT value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  ulong size = value.size();

    //	  if (!serializer.beginArray(size, new Common.StringView(name)))
    //	  {
    //		if (serializer.type() == ISerializer.INPUT)
    //		{
    //		  value.clear();
    //		}

    //		return false;
    //	  }

    //	  if (serializer.type() == CryptoNote.ISerializer.INPUT)
    //	  {
    //		for (ulong i = 0; i < size; ++i)
    //		{
    //		  SetT.value_type key = new SetT.value_type();
    //		  serializer.functorMethod(key, "");
    //		  value.insert(std::move(key));
    //		}
    //	  }
    //	  else
    //	  {
    //		foreach (var key in value)
    //		{
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
    //		  serializer.functorMethod(const_cast<typename SetT.value_type&>(key), "");
    //		}
    //	  }

    //	  serializer.endArray();
    //	  return true;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename K, typename Hash>
    //	public static bool serialize<K, Hash>(HashSet<K, Hash> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  return serializeSet(value, new Common.StringView(name), serializer.functorMethod);
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename K, typename Cmp>
    //	public static bool serialize<K, Cmp>(SortedSet<K, Cmp> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //	  return serializeSet(value, new Common.StringView(name), serializer.functorMethod);
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename K, typename V, typename Hash>
    //	public static bool serialize<K, V, Hash>(Dictionary<K, V, Hash> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //  return serializeMap(value, new Common.StringView(name), serializer.functorMethod, (ulong size) =>
    //  {
    //	  value.reserve(size);
    //  });
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename K, typename V, typename Hash>
    //	public static bool serialize<K, V, Hash>(std::unordered_multimap<K, V, Hash> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //  return serializeMap(value, new Common.StringView(name), serializer.functorMethod, (ulong size) =>
    //  {
    //	  value.reserve(size);
    //  });
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename K, typename V, typename Hash>
    //	public static bool serialize<K, V, Hash>(SortedDictionary<K, V, Hash> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //  return serializeMap(value, new Common.StringView(name), serializer.functorMethod, (ulong size) =>
    //  {
    //  });
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename K, typename V, typename Hash>
    //	public static bool serialize<K, V, Hash>(std::multimap<K, V, Hash> value, Common.StringView name, CryptoNote.ISerializer serializer)
    //	{
    //  return serializeMap(value, new Common.StringView(name), serializer.functorMethod, (ulong size) =>
    //  {
    //  });
    //	}

    //	//C++ TO C# CONVERTER TODO TASK: C++ 'constraints' are not converted by C++ to C# Converter:
    //	//ORIGINAL LINE: template<ulong size>
    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<typename size>
    //	public static bool serialize<size>(List<ushort> value, Common.StringView name, CryptoNote.ISerializer s)
    //	{
    //	  return s.binary(value.data(), value.Count, new Common.StringView(name));
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T1, typename T2>
    //	public static void serialize<T1, T2>(Tuple<T1, T2> value, ISerializer s)
    //	{
    //	  s.functorMethod(value.Item1, "first");
    //	  s.functorMethod(value.Item2, "second");
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename Element, typename Iterator>
    //	public static void writeSequence<Element, Iterator>(Iterator begin, Iterator end, Common.StringView name, ISerializer s)
    //	{
    //	  ulong size = std::distance(begin, end);
    //	  s.beginArray(size, new Common.StringView(name));
    //	  for (Iterator i = begin; i != end; ++i)
    //	  {
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
    //		s.functorMethod(const_cast<Element&>(*i), "");
    //	  }
    //	  s.endArray();
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename Element, typename Iterator>
    //	public static void readSequence<Element, Iterator>(Iterator outputIterator, Common.StringView name, ISerializer s)
    //	{
    //	  ulong size = 0;
    //	  // array of zero size is not written in KVBinaryOutputStreamSerializer
    //	  if (!s.beginArray(size, new Common.StringView(name)))
    //	  {
    //		return;
    //	  }

    //	  while (size-- != null)
    //	  {
    //		Element e = new default(Element);
    //		s.functorMethod(e, "");
    //		*outputIterator++= std::move(e);
    //	  }

    //	  s.endArray();
    //	}

    //	//convinience function since we change block height type
    //	public static void serializeBlockHeight(ISerializer s, ref uint blockHeight, Common.StringView name)
    //	{
    //	  if (s.type() == ISerializer.INPUT)
    //	  {
    //		ulong height = new ulong();
    //		s.functorMethod(height, name);

    //		if (height == ulong.MaxValue)
    //		{
    //		  blockHeight = uint.MaxValue;
    //		}
    //		else if (height > uint.MaxValue && height < ulong.MaxValue)
    //		{
    //		  throw new System.Exception("Deserialization error: wrong value");
    //		}
    //		else
    //		{
    //		  blockHeight = (uint)height;
    //		}
    //	  }
    //	  else
    //	  {
    //		s.functorMethod(blockHeight, name);
    //	  }
    //	}

    //	//convinience function since we change global output index type
    //	public static void serializeGlobalOutputIndex(ISerializer s, uint globalOutputIndex, Common.StringView name)
    //	{
    //	  serializeBlockHeight(s.functorMethod, ref globalOutputIndex, new Common.StringView(name));
    //	}

    //	public static std::error_code createTransfers(AccountKeys account, TransactionBlockInfo blockInfo, ITransactionReader tx, List<uint> outputs, List<uint> globalIdxs, List<TransactionOutputInformationIn> transfers, Logging.LoggerRef m_logger)
    //	{

    //	  var txPubKey = tx.getTransactionPublicKey();
    //	  List<PublicKey> temp_keys = new List<PublicKey>();
    //	  lock (seen_mutex)
    //	  {

    //		  foreach (var idx in outputs)
    //		  {
    //			bool isDuplicate = false;

    //			if (idx >= tx.getOutputCount())
    //			{
    //			  return std::make_error_code(std::errc.argument_out_of_domain);
    //			}

    //			var outType = tx.getOutputType(uint(idx));

    //			if (outType != CryptoNote.TransactionTypes.OutputType.Key)
    //			{
    //			  continue;
    //			}

    //			TransactionOutputInformationIn info = new TransactionOutputInformationIn();

    //			info.type = outType;
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: info.transactionPublicKey = txPubKey;
    //			info.transactionPublicKey.CopyFrom(txPubKey);
    //			info.outputInTransaction = idx;
    //			info.globalOutputIndex = (blockInfo.height == WALLET_UNCONFIRMED_TRANSACTION_HEIGHT) ? UNCONFIRMED_TRANSACTION_GLOBAL_OUTPUT_INDEX : globalIdxs[idx];

    //			if (outType == CryptoNote.TransactionTypes.OutputType.Key)
    //			{
    //			  ulong amount;
    //			  KeyOutput @out = new KeyOutput();
    //			  tx.getOutput(idx, @out, ref amount);

    //			  CryptoNote.KeyPair in_ephemeral = new CryptoNote.KeyPair();
    //			  CryptoNote.generate_key_image_helper(account, txPubKey, idx, in_ephemeral, info.keyImage);

    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //			  Debug.Assert(@out.key == reinterpret_cast<const PublicKey&>(in_ephemeral.publicKey));

    //			  if (GlobalMembers.transactions_hash_seen.find(tx.getTransactionHash()) == GlobalMembers.transactions_hash_seen.end())
    //			  {
    //				if (GlobalMembers.public_keys_seen.find(@out.key) != GlobalMembers.public_keys_seen.end())
    //				{
    //				  m_logger.functorMethod(WARNING, BRIGHT_RED) << "A duplicate public key was found in " << Common.GlobalMembers.podToHex(tx.getTransactionHash());
    //				  isDuplicate = true;
    //				}
    //				else
    //				{
    //				  temp_keys.Add(@out.key);
    //				}
    //			  }

    //			  info.amount = amount;
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: info.outputKey = out.key;
    //			  info.outputKey.CopyFrom(@out.key);
    //			}

    //			if (!isDuplicate)
    //			{
    //			  transfers.Add(info);
    //			}
    //		  }
    //	  }

    //	  GlobalMembers.transactions_hash_seen.Add(tx.getTransactionHash());
    //	  std::copy(temp_keys.GetEnumerator(), temp_keys.end(), std::inserter(GlobalMembers.public_keys_seen, GlobalMembers.public_keys_seen.end()));
    //	  return std::error_code();
    //	}

    //	public static void serialize(TransactionInformation ti, CryptoNote.ISerializer s)
    //	{
    //	  s.functorMethod(ti.transactionHash, "");
    //	  s.functorMethod(ti.publicKey, "");
    //	  serializeBlockHeight(s.functorMethod, ref ti.blockHeight, "");
    //	  s.functorMethod(ti.timestamp, "");
    //	  s.functorMethod(ti.unlockTime, "");
    //	  s.functorMethod(ti.totalAmountIn, "");
    //	  s.functorMethod(ti.totalAmountOut, "");
    //	  s.functorMethod(ti.extra, "");
    //	  s.functorMethod(ti.paymentId, "");
    //	}

    //	public static readonly uint TRANSFERS_CONTAINER_STORAGE_VERSION = 0;
    //	  public static TransferIteratorList<TIterator> createTransferIteratorList<TIterator>(Tuple<TIterator, TIterator> itPair)
    //	  {
    //		return new TransferIteratorList<TIterator>(itPair.Item1, itPair.Item2);
    //	  }
    //	  public static void updateVisibility<C, T>(C collection, T range, bool visible)
    //	  {
    //		for (var it = range.first; it != range.second; ++it)
    //		{
    //		  var updated = it;
    //		  updated.visible = visible;
    //		  collection.replace(it, updated);
    //		}
    //	  }

    //	public static readonly uint TRANSFERS_STORAGE_ARCHIVE_VERSION = 0;
    //	public static string getObjectState(IStreamSerializable obj)
    //	{
    //	  std::stringstream stream = new std::stringstream();
    //	  obj.save(stream);
    //	  return stream.str();
    //	}

    //	public static void setObjectState(IStreamSerializable obj, string state)
    //	{
    //	  std::stringstream stream = new std::stringstream(state);
    //	  obj.load(stream);
    //	}

    //	public static ulong getDefaultMixinByHeight(ulong height)
    //	{
    //		if (height >= CryptoNote.parameters.MIXIN_LIMITS_V3_HEIGHT)
    //		{
    //			return CryptoNote.parameters.DEFAULT_MIXIN_V3;
    //		}
    //		if (height >= CryptoNote.parameters.MIXIN_LIMITS_V2_HEIGHT)
    //		{
    //			return CryptoNote.parameters.DEFAULT_MIXIN_V2;
    //		}
    //		else if (height >= CryptoNote.parameters.MIXIN_LIMITS_V1_HEIGHT)
    //		{
    //			return CryptoNote.parameters.DEFAULT_MIXIN_V1;
    //		}
    //		else
    //		{
    //			return CryptoNote.parameters.DEFAULT_MIXIN_V0;
    //		}
    //	}
    //	public static void throwIfKeysMismatch(Crypto.SecretKey secretKey, Crypto.PublicKey expectedPublicKey, string message = "")
    //	{
    //	  Crypto.PublicKey pub = new Crypto.PublicKey();
    //	  bool r = Crypto.secret_key_to_public_key(secretKey, pub);
    //	  if (!r || expectedPublicKey != pub)
    //	  {
    //		throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.WRONG_PASSWORD), message);
    //	  }
    //	}
    //	public static bool validateAddress(string address, CryptoNote.Currency currency)
    //	{
    //	  CryptoNote.AccountPublicAddress ignore = new CryptoNote.AccountPublicAddress();
    //	  return currency.parseAccountAddressString(address, ignore);
    //	}

    //	public static std::ostream operator << (std::ostream os, CryptoNote.WalletTransactionState state)
    //	{
    //	  switch (state)
    //	  {
    //	  case CryptoNote.WalletTransactionState.SUCCEEDED:
    //		os << "SUCCEEDED";
    //		break;
    //	  case CryptoNote.WalletTransactionState.FAILED:
    //		os << "FAILED";
    //		break;
    //	  case CryptoNote.WalletTransactionState.CANCELLED:
    //		os << "CANCELLED";
    //		break;
    //	  case CryptoNote.WalletTransactionState.CREATED:
    //		os << "CREATED";
    //		break;
    //	  case CryptoNote.WalletTransactionState.DELETED:
    //		os << "DELETED";
    //		break;
    //	  default:
    //		os << "<UNKNOWN>";
    //	break;
    //	  }

    //	  return os << " (" << (int)state << ')';
    //	}
    //	public static std::ostream operator << (std::ostream os, CryptoNote.WalletTransferType type)
    //	{
    //	  switch (type)
    //	  {
    //	  case CryptoNote.WalletTransferType.USUAL:
    //		os << "USUAL";
    //		break;
    //	  case CryptoNote.WalletTransferType.DONATION:
    //		os << "DONATION";
    //		break;
    //	  case CryptoNote.WalletTransferType.CHANGE:
    //		os << "CHANGE";
    //		break;
    //	  default:
    //		os << "<UNKNOWN>";
    //	break;
    //	  }

    //	  return os << " (" << (int)type << ')';
    //	}
    //	public static std::ostream operator << (std::ostream os, CryptoNote.WalletGreen.WalletState state)
    //	{
    //	  switch (state)
    //	  {
    //	  case CryptoNote.WalletGreen.WalletState.INITIALIZED:
    //		os << "INITIALIZED";
    //		break;
    //	  case CryptoNote.WalletGreen.WalletState.NOT_INITIALIZED:
    //		os << "NOT_INITIALIZED";
    //		break;
    //	  default:
    //		os << "<UNKNOWN>";
    //	break;
    //	  }

    //	  return os << " (" << (int)state << ')';
    //	}
    //	public static std::ostream operator << (std::ostream os, CryptoNote.WalletGreen.WalletTrackingMode mode)
    //	{
    //	  switch (mode)
    //	  {
    //	  case CryptoNote.WalletGreen.WalletTrackingMode.TRACKING:
    //		os << "TRACKING";
    //		break;
    //	  case CryptoNote.WalletGreen.WalletTrackingMode.NOT_TRACKING:
    //		os << "NOT_TRACKING";
    //		break;
    //	  case CryptoNote.WalletGreen.WalletTrackingMode.NO_ADDRESSES:
    //		os << "NO_ADDRESSES";
    //		break;
    //	  default:
    //		os << "<UNKNOWN>";
    //	break;
    //	  }

    //	  return os << " (" << (int)mode << ')';
    //	}

    //	public static readonly uint WALLET_INVALID_TRANSACTION_ID = uint.MaxValue;
    //	public static readonly uint WALLET_INVALID_TRANSFER_ID = uint.MaxValue;
    //	public static readonly uint WALLET_UNCONFIRMED_TRANSACTION_HEIGHT = uint.MaxValue;
    //	  public static string getProjectCLIHeader()
    //	  {
    //		std::stringstream programHeader = new std::stringstream();
    //		programHeader << std::endl << asciiArt << std::endl << " " << CryptoNote.CRYPTONOTE_NAME << " v" << PROJECT_VERSION_LONG << std::endl << " This software is distributed under the General Public License v3.0" << std::endl << std::endl << " " << PROJECT_COPYRIGHT << std::endl << std::endl << " Additional Copyright(s) may apply, please see the included LICENSE file for more information." << std::endl << " If you did not receive a copy of the LICENSE, please visit:" << std::endl << " " << CryptoNote.LICENSE_URL << std::endl << std::endl;

    //		return programHeader.str();
    //	  }

    //	public static readonly std::initializer_list<CheckpointData> CHECKPOINTS = new std::initializer_list<CheckpointData>({0, "7fb97df81221dd1366051b2d0bc7f49c66c22ac4431d879c895b06d66ef66f4c"}, {5000, "1325029c8be54b9e027f17ec481a0a361e4821381d1d12de1492e92cd38d4c11"}, {10000, "08b25f220656df008499f36c593bc7b875e3598eae0c9c32ff195e735e51ff1d"}, {15000, "8d83bdfde2e42b3be9ef194cd327ad5f893c9cab249395378e03aee8db9d420a"}, {20000, "aed3a0fe6adc5a828e4b9cfa97cea53d2ae565089b583756058e5c71682fde4d"}, {25000, "cd175f1ed7d6b2bf3e0ed7585aacd17c57b4cd708c4bd32183224d45716767c5"}, {30000, "90f91f95ea58a37b98cacd3c024fa2eaaa33e0eadc632aa3b61b061fb06b3efe"}, {35000, "7da853cb5d44abf72f2b2da43c6571e8287eec077a9c9059692f5b5c1ed3f73e"}, {40000, "da9451a44540a96aff987d4046a13b20ffe4bc08a0ac536a0c196f00d24a90ee"}, {45000, "122e4010229f1a2a99ed81ab9c7519a4cd5c23e9754f21a17b4a1e33411caf28"}, {50000, "dd40ba6a33e7c6ff84927d510881e285eba9a17cbde43da587aa6cc41883b852"}, {55000, "0caf8cd5552afc5701cfcc29bf8e8292064a65cbb4ffc26ae24decf7e43838bf"}, {60000, "1e2f5f6c9c4b4b3c44e45ea1bd9aa2893721a44793fb743564e6b105d5f978ba"}, {65000, "b55164b838f342d2a2258aac5b6ce230141f14e9f2472818ac0e4cf0263130ed"}, {70000, "33c5871e424d72525ac351baad3d698f0886e11836eed8db878ae6b436128074"}, {75000, "2425d24ba8bd69f9601430f3a5a545bdc3931ddd30f06f82ceeec2766c9ca74a"}, {80000, "9a62c2b5d8ae89a86019c52964c7f74c8b29504c0088d21d7f1f8484ad103440"}, {85000, "dc2d2e4ecb85c13d28872d875cccaf4f90d7e3ffd5cd134119c53633f802296a"}, {90000, "a123b24e7d1ea23eb610129ffad2b575baefac243c026c1ea7381fefcd1bd743"}, {95000, "04310be9aa0aaa2432c97cfce9db8153c3872942024015132220d95837a7f7df"}, {100000, "0559c2aad34dc8f47b30fefa2652aca9039d6ec0137af3fd0beb16d7c6f91b1d"}, {105000, "feaf9c526496c66efbe237d929fb65e6f102fa9b9158424c97d7ee3736a6b658"}, {110000, "23c14bb24a598df879e154ca745a6610e759765e68e8aa6b9c960f2c621d41f4"}, {115000, "e874317e8308896ee6694eb73159edccabdaacc09981908ec7a06640be6a6a46"}, {120000, "c78efd3fd072f3993bb1fff4bc02998057e749846dac1f63fb57e6b1fa1abb2d"}, {125000, "eb7115cc9701d5757bc9e89c29150cac97115502860c919580ab3e51bf55bc13"}, {130000, "6db14e442b7774e3f240342e4efc56ed73165fb94e3bc9d2798e9e9011a4fa02"}, {135000, "dc90ae322b120f9b34474ff536edf3d16b7c5e9794e14aca5343a22279a803d2"}, {140000, "0138d4b8a46de402691e40e0b912ceb54e1c7894583f6a9177bea18b66d5385a"}, {145000, "3713cd82b49e2300ebd74e359895709a0f9da1ace1903dd1731b66d02b7cc4bb"}, {150000, "ff867db0eb78ada7375656aafc452fe46a364ce998eb9add59399c13fc91accb"}, {155000, "a03383ee6938ab7f2ddbd0a2036c8b41db82e32cc91bb17b1d3d86ae50361170"}, {160000, "ec666aaa200f72c1e928ac9b8b38c43bf0d1c97b7d4020dea3fe94a5a041da4a"}, {165000, "a0899e0fd93a48c3828a472c960012f2f0582d320de7f2ca9b80ff3ee139f1f0"}, {170000, "9453b1bf99e98901949c7d88c9f6db8744b413f8160aa0ac0a467d2375c1f8fd"}, {175000, "d5286be301c0c9f3df317929bae432dec1160baf658c24cace8adc942c62dd44"}, {180000, "195deec0a5dff662f89f6bad28f205ddb3c72d778e7bfe9e0362cf9b244b791d"}, {185000, "d741fcfa4c085a055bc248e56db5ff18d6e0665590d8271e9a445819e50cbed4"}, {190000, "d881cd9e76ca0890bd881e75dd1a611558a44543a68326aeefe33f656db0fb21"}, {195000, "48b5c133876d2cc4d60976501981c0af1057321c4e404531c1fa6e04e3b31c50"}, {200000, "3438412001e186b0de417819353046199ad99c4e05b488e8391d7cc6c80a918e"}, {205000, "43b5199c3af492afcdd22072d3b1a0673dbbf24b892cf9d141612f00244dbfe6"}, {210000, "8c784e294af9694f4d5d817ef6637e561eb4a478d99d942d2da1a3906359a2ff"}, {215000, "98f0ec88e9e9c756808b3a36e1860888f466868ef0fca7de360b82ad26f65c7b"}, {220000, "5e43f24ff1e5e85e1c6f86cd09d1854ea76c605ca6c6ada4ea5fd3873c062cec"}, {225000, "816c3af64da7b26d3d77ee6087e3f5c44652ffe1f66f94902f72afb94f4f44e9"}, {230000, "95b16e0e248c39a7fd26c6f7fac420f317dd6deb9ee9bc541022e4412a9517be"}, {235000, "c629cd44e582f30aab010d071c46297f104ec8f093e798ed5de003faef4c3f0d"}, {240000, "c6617c3d1e873071e217d3970ec5aa56eab3839061ad80b45d3cd137b2c1f3c5"}, {245000, "44a0a64a7817c7b2c56d27cd5a396892418b8c797433c1c3517f3d3425080c01"}, {250000, "2df370f9c7648d158c570344066ce479abd0425162023afb8088109aa311fbe4"}, {255000, "2d6e05ef80ad0a51109e39ead7e9d0ef06f9668c6a7358093497ed12ae5569e0"}, {260000, "de79c513a8ff2408968c813f95e9bacb346dc736fd4db12aa522b6c35b61870b"}, {265000, "a5908d9b7ab9a81fb4788b3297e207a338d3eb95e43d536dbd65ef3416a95a3e"}, {270000, "81025a563355ec4b4cc327b4d22a421ef2b72e2fc80309994ee417ab1e34be04"}, {275000, "9935e59e95b81251b053c9aa1b82eef393a13004e4c0b6efff1e181f5262feca"}, {280000, "0864401ffb378cd03ad45ce3b7f8077dfd1cbe563e56ddf5155a87347ed0cf44"}, {285000, "0281cdc8bb3c7a076cd7244fc7816c8feb1c8eb985869ff859a444cd8c2e9d65"}, {290000, "f571dfd0f9c8e332d1c683953bfe7fc94b5e7344c1df1ccd80909fd746656bc2"}, {295000, "f33b271c9cfeab3dba4957be57fbecf45a4cea52861979a9dd9d8d2b56c48ec0"}, {300000, "a0ad53a2bc6c7ac6fd6f92586ade93994c49bdc4fb269f4c43bcc87ed7d07e66"}, {305000, "33792d85e883f054158ad253285f2ced61798e393135a2d591ccb5ccc60ee724"}, {310000, "5143cea534ef2ba5a5e18c0877c9c8877d469d3ae040406a65cbf23ea06b4585"}, {315000, "7bb70271b257d3cb784b026dd9dea1aacb1e6feb8b31c0151819861226b75e88"}, {320000, "f9f4f51eea457570bf6c1beb599147d03ba4c13c573222fde9454018a0e531f7"}, {325000, "18cc18d052c4b16f3aa415aa860f012d37028cf61c77f0fd4126aed67f0803b3"}, {330000, "d63c41bfb0c5468461c924b3983c05816295f358df2599363db96fd1eb257036"}, {335000, "528ae5c23aee04bee553a963832b59f72bf08903209caac1f84ea4b0056d5464"}, {340000, "8181936abc5edc3b25c3d5f39fce7ef4a63f332d9d310afa507170f793a3dbd8"}, {345000, "89bacab01b987b42af745c67571bf87d3edfd686f3c16f0f1bed0fe99fcfa0d4"}, {350000, "45aaec7312e7b85ae1c05408fbf4fe88ee44a72b85d9c9277f17596a0da84bfd"}, {355000, "139e4926dc3a688f2ec6f043062b8398b605e608befff67aac37eeba7c53e2ef"}, {360000, "e3820a2023ea1c14dc866f1e6c06299da7b823bbd0ddcf00165d30e54ad21e79"}, {365000, "b7f30224ec3ff327bfaecf566e183d91027800f6ea35b1f83fb5e0d8e496b91d"}, {370000, "7a9e8b08ce3eaa8fc41f5367953f23d825db2efa8a1e90870c891a9fe9d85963"}, {375000, "e7b60d984e97f4b2868757acac22592c2f392f691baf22b1c481180b4b8c6c8b"}, {380000, "a22ac5093c02908015f1f5daa75e29bbb8e2a481fc87f3d5b299c7429ebb04ce"}, {385000, "220f6db2928f4e99eb2c23a81d8c713a6fb1e155d6ca213747d7aebbb3d8c088"}, {390000, "cce9f7ce3b2108809cf53a9d950a0b0deaa4554d65b5ec1444cfc8485833934c"}, {395000, "ecab3b211d9f9a56ba2f3f579bc829d54310890bd9559da03541619af75e51a7"}, {400000, "956c87d587448b688ff412804255e5a832b069c25c8d6050c3f70b8db0a68225"}, {405000, "f0e2816a69913b509094b62a13a6091cbd62f55baabf21212cd4f6bba7c59f55"}, {410000, "bae4358d5bab0742e16b2c07a0685b4169da5ef64aa89456168b2d0013e99106"}, {415000, "421570a88b01c7494e48d1be18b82e3d107d69bbb7cbd73a1ba86ad26f7dd8fb"}, {420000, "abdd8c64b6208ba834a1ffbf02dd00a1c68e546fcdc336c9332a48ff78133268"}, {425000, "f7bc310d2f97eba33445c78fc37b7879d6cf836587649d53ffea169f7fd0318a"}, {430000, "6e49d1584c8f858703b9761731948de097244d038b19fb7274e96ccb50dec220"}, {435000, "8070296e0236c616cc2503feb0cf1cb90ae7c673c4053c08f5486d9d0fd73296"}, {440000, "88f5405105d7258ffb061983f3d8cc17adfead8f6348aeb9d23cf16d54935b76"}, {445000, "8abba34d38fe6c2974436c203ea75e43a38e415bb79ecd133af6b860103dbc20"}, {450000, "6660add67afd71598faf36a05e9d1f83025a3486711faf5a1fdb1ef9b611149f"}, {455000, "afd91e635df92abd8e55df11c3d10aee110f5b9d70092257eca8ed40f1ff5dec"}, {460000, "274fe3d3a09977c041b5ec5a870c057af6ce798e68e302962504da10512803fb"}, {465000, "7476451d3a53070372306e76f0a0e1cd35f86440f3a721ea07654efbe542114f"}, {470000, "87ee44fcc68d75c6b82c14e6989097421861fcb8270e18e44f331978e8ce1051"}, {475000, "d7b0a23790b03ef71feefda1c81a98520af66498b4547a3fd3c183a4b744de0f"}, {480000, "0d5eeefb2bd097b1ae2da69f22a6ca327e541745f6ce9cbe5cfcd5e256253be6"}, {485000, "bee7b183804a22f10a758be2bce45ff356546c52d0e985355e461a99dcfeb2b2"}, {490000, "212288303c9bad46584db9dec680f7f9756f448da398b1e1b8c8615637df18fd"}, {495000, "b93aad27a0f70162e26bcd82b299d7b0d5baf6213313615e11be7a0ebf2a1091"}, {500000, "62f0058453292af5e1aa070f8526f7642ab6974c6af2c17088c21b31679c813d"}, {505000, "42fdc7f9e2587f22ca3b65d6ae3eebfe1896fe1296bc4b4195171443b9bca759"}, {510000, "6818d833049dd02ee8b2dccfce6fbc38254f9803738fdc2a9103953977286151"}, {515000, "98bf8d74e1f664e3886e15f3c72a90b816446ae319b5a47e129643c6b8c8d4b9"}, {520000, "4fc64bd70e644ece44751cad91a7b156697c8a4ba1004f28577deae1a06765f3"}, {525000, "b3c708d066435b3781f6945059f620dbf023028ea7a57fdc0a89e2c0c5ccb742"}, {530000, "b2c8e2206b2d8a29d71d8b08da59806662deac36274530b069bda1a609e3cd2a"}, {535000, "bd015d5d0c06f99716d2adf6d64b8269a95dd39761552ba67ce3f021cac515d3"}, {540000, "0a5574e0880cd59dd9a3aaa04ca53c6b7b5bda5754f8c9ed3ab6673eaf5bcb64"}, {545000, "c971e2534ffae5ee9d0e7f9cd2f75fb4d21a0600beedc4fdcbaff9c43aa921ef"}, {550000, "e839382ffc21a54ee43de63c77a38352f85e948c0806e918f94d6cb94af2d74b"}, {555000, "8f26c4a573fb479e009250e906a696ee229e698a2ba4d5d36186da04f9155f68"}, {560000, "609ee1ab220bf00dc88e616f21db9684c1b570db9184799c952a590811d364a4"}, {565000, "66fc9f981bd2f7f1775b5bbc9cdf983623f5dfdcc870e6a38d95c196ba0c677f"}, {570000, "bd281d68772ec430be555dee57e485dfeee0ed106e22aec5e2f1409a1638ed93"}, {575000, "c7efe5a723c854bb81ef77a5a3370e68cd4eadac40f9a4e8e390fe3761db83ef"}, {580000, "445fe4a61f1ca17f6558e61bda6d59cca3d30e0d4837807f4b69d0499e8443fe"}, {585000, "d59efaf1422c5c9dde2ee00942eaedbce567be0b342ccd0227853c52555e404f"}, {590000, "b867144a8df2b85a6cd6426fc053c25d1aa56ea1babb076947d77e03eb6d57b0"}, {595000, "d93adf2255f86caf30460b45dfeea538f36d9fc783e143616bbe6f8dc3a32157"}, {600000, "234266e7a2b03534df7d7a0b9403eeaabad316b86222575076c599f77c812200"}, {605000, "cba35971385cd7e243b645d3e06ab3e5f0430285d257c8390791cedd8840810f"}, {610000, "0fc4afd5f049f7c39ba95b54eaec703da8ea90f3358019079e4a57c411fdf360"}, {615000, "b0d784591bca95488996b017e53f55d71ec757e2c735f90b973fb5d90dcd3eb6"}, {620000, "01883f5bac52d683ea3d4dd56a53a362bd8eada339448428f0d42b125eac9e2d"}, {625000, "32462524739adac6af7afac9e791b7a9e4d2bebab76347c488aaa3b504e90df0"}, {630000, "6650cc9d4546e82dc5ac1faecab83fb2523624f4fce0bbb3050bebec29ab110b"}, {635000, "172e403932f16532a6c0ee8465edd4687914312ca73ccde8cd05fd1334effc4b"}, {640000, "2cbf230b2218d8a0cbc353a861b20d0d7c1c7ed7cfd0a0d912af4a8f8d20c31d"}, {645000, "6bcad95101dc68977b44b7fdf064dd395d922d39ff0e487fe8f57ad9651db7cc"}, {650000, "ca5939a8fd0823c663e199af8657ed9d67f324abf8a2aaf4b58ee565bd6dc1ab"}, {655000, "679e87b0975fae8f1700e4708835f91786de98f543b94304d5b306f9c694b766"}, {660000, "ab1e32754eaba47429f1d5bfff1e7b44e6ca68624eafb9b36c4ecdd65e5d4b2f"}, {665000, "744b56bf373abeb7ebce54427d477eb56859be57bed99d70e72014e48433235d"}, {670000, "3ba7cc34927c9a1edf24dbb05fd7022f782f50067774a6477a8f3baff09da9bc"}, {675000, "1aa8c6029432c29d6a26f1c3e6bd895a9c60e14ac951d7860e8d05e815994d99"}, {680000, "2b4984c2aa62923a80927b158c3b71197e8e95f7cfb2e88115ee47cd1b24cb4b"}, {685000, "8baf7a3c2cb656495b8eed6c12722aff9c61a420b643d0b824683d8c401e5b00"}, {690000, "6d562412e80ba57c9499ad800172d7724cb92537cc378d6d00b2c464b4db966c"}, {695000, "602c4944dd964f42bf934fcfaa2ead2461c383e3f72c8c9a572736bc32bb80b6"}, {700000, "d339790bcff3313b935a3442c9da913f526b6bce8fbaebda54f8858b9c1c5aa9"}, {705000, "4da1513d0a01a026db41316567af7b1616ebca39fea33c6140f8e9cc8cbe2e54"}, {710000, "ff9c3e233f1a8c7c21cd0b6961b1b68a8545c7b54355cd40e7fa1982f828fb76"}, {715000, "3459af0d43678f663b1c28bdbe89417878f447df529f7c2d699acc9cbed013a9"}, {720000, "6db93223babb9499c94171dedf39f55a29bcbb9cd469f49da8aca78c60b48946"}, {725000, "e4dce12039c887f40bac58f65d401f343d15e20e61c40b955ba577bed313f291"}, {730000, "73d856bfdac0e052f1b9bdf7f157ca6e8cc85fe1c040eca6f529a451034991bb"}, {735000, "d723da51b1663eb9970d61cbd522fcdc2667da971f520e80d22bd9bf6ba86a12"}, {740000, "e8b3b83b494457ed041ff1ad01928dcdee3689ac8a33a9429169fded0ee0cdfc"}, {745000, "1fd0b85a31b64d1631c09672c03c644296439c775f3dc9657bd812c06b98d895"}, {750000, "8b7a9bcb49b8d127723b26b3ab91b9e3ee52ed9bc4c9cc3ef6d9ce1b4f6c9520"}, {755000, "aaabbd901cc90c6f7b86d4573bf96529ff5bd6810ab26e0d7798bab407d9e0e3"}, {760000, "b10a349dd60a1fe0160eba2ea9eaea8b8726919e1533509d025358f42163d34c"}, {765000, "6f0c679f296e24d564e8a4e406fa80c471c5fb31698f23f03728f009ef1f04ef"}, {770000, "d6cea5520251ba57d9aec70711a0156ca9b6b933c68e0fbfc102237b63e0178b"}, {775000, "1b87b145d1b4dc99fc681e93bcd337da1e06c271bae4dc66e8ff9a994768ea09"}, {780000, "2822709e1d827482e0c064dd33c7dcb311ca5e8300a3ec5e5509678c3f9c6363"}, {785000, "1b328f0d30edf12964eafffe7609680c54abf1720ba296f3d6e8131c145e30a2"}, {790000, "e60e00899d841b870c64ab253b6678127f4707ff6568ed1574c3679c7fd2df97"}, {795000, "aebcd0114155d1b99b3bdd0dfda952def566b4f0446a1cac50d7f8e2b988c236"}, {800000, "32df598e8f48bcd100371d70136c3f7287a83d0fffd00277e8b624fb17dfe738"}, {805000, "951d5e97b7a3bfecebe284368f6b5ed09ec94f0efc44f096bb331b25c130e845"}, {810000, "7eb228ffa07d24f8e274d1367342e13169ed83c2659cba95036ecabf0e292f66"}, {815000, "4a41251470cd5da0f6baabfc1aa136c48c7bb0aea8b4c171e4db3687c7a053b7"}, {820000, "52489870b9e7678b88425ae0f9e05cb7e38607374b87f083f89fc2d66d98458a"}, {825000, "4a930cf52eb5a3aed39d471bb6a9ba419ae99efa1a0c20687ad7b1e7ae55320b"}, {830000, "3cdb9d60b02977b24a1a6daea782d7f12e2a3a5bafd5cad2698395a4d59285f4"}, {835000, "7e2298d44d69a5cf46fb5ae59032d9a32044ea54c74b2ec6027e021435c3c325"}, {840000, "475764354a1fb33ea3bf2639ae9683caa77fdb93b4a4ef205c28a12fe3b37d21"}, {845000, "09119e4e03e7ef5f4919d966ffb051a5e8d4a73b277bbc1402a6105bdb11d54b"}, {850000, "d52586efb350fd5c263374fa92e24eec7be928e02a6ddaf95c5c255ec0ff33d5"}, {855000, "43da13e86cf37f78c286b76def50251ffd64483c5194541f98a103aa2ea6f7eb"}, {860000, "3f609c720e1fe206ab466647fb4461916ef3fc2862d086f3fde57a9da195bc64"}, {865000, "db01344e07347bda9d0c74fd9009fb3643c9cd4af0b893ece171e129e6657f08"});

    //	public const string CRYPTONOTE_NAME = "TurtleCoin";

    //	public static readonly byte TRANSACTION_VERSION_1 = 1;
    //	public static readonly byte TRANSACTION_VERSION_2 = 2;
    //	public static readonly byte CURRENT_TRANSACTION_VERSION = TRANSACTION_VERSION_1;

    //	public static readonly byte BLOCK_MAJOR_VERSION_1 = 1;
    //	public static readonly byte BLOCK_MAJOR_VERSION_2 = 2;
    //	public static readonly byte BLOCK_MAJOR_VERSION_3 = 3;
    //	public static readonly byte BLOCK_MAJOR_VERSION_4 = 4;

    //	public static readonly byte BLOCK_MINOR_VERSION_0 = 0;
    //	public static readonly byte BLOCK_MINOR_VERSION_1 = 1;

    //	public static readonly uint BLOCKS_IDS_SYNCHRONIZING_DEFAULT_COUNT = 10000; //by default, blocks ids count in synchronizing
    //	public static readonly uint BLOCKS_SYNCHRONIZING_DEFAULT_COUNT = 100; //by default, blocks count in blocks downloading
    //	public static readonly uint COMMAND_RPC_GET_BLOCKS_FAST_MAX_COUNT = 1000;

    //	public static readonly int P2P_DEFAULT_PORT = 11897;
    //	public static readonly int RPC_DEFAULT_PORT = 11898;
    //	public static readonly int SERVICE_DEFAULT_PORT = 8070;

    //	public static readonly uint P2P_LOCAL_WHITE_PEERLIST_LIMIT = 1000;
    //	public static readonly uint P2P_LOCAL_GRAY_PEERLIST_LIMIT = 5000;

    //	// P2P Network Configuration Section - This defines our current P2P network version
    //	// and the minimum version for communication between nodes
    //	public static readonly byte P2P_CURRENT_VERSION = 4;
    //	public static readonly byte P2P_MINIMUM_VERSION = 2;
    //	// This defines the number of versions ahead we must see peers before we start displaying
    //	// warning messages that we need to upgrade our software.
    //	public static readonly byte P2P_UPGRADE_WINDOW = 2;

    //	public static readonly uint P2P_CONNECTION_MAX_WRITE_BUFFER_SIZE = 32 * 1024 * 1024; // 32 MB
    //	public static readonly uint P2P_DEFAULT_CONNECTIONS_COUNT = 8;
    //	public static readonly uint P2P_DEFAULT_WHITELIST_CONNECTIONS_PERCENT = 70;
    //	public static readonly uint P2P_DEFAULT_HANDSHAKE_INTERVAL = 60; // seconds
    //	public static readonly uint P2P_DEFAULT_PACKET_MAX_SIZE = 50000000; // 50000000 bytes maximum packet size
    //	public static readonly uint P2P_DEFAULT_PEERS_IN_HANDSHAKE = 250;
    //	public static readonly uint P2P_DEFAULT_CONNECTION_TIMEOUT = 5000; // 5 seconds
    //	public static readonly uint P2P_DEFAULT_PING_CONNECTION_TIMEOUT = 2000; // 2 seconds
    //	public static readonly ulong P2P_DEFAULT_INVOKE_TIMEOUT = 60 * 2 * 1000; // 2 minutes
    //	public static readonly uint P2P_DEFAULT_HANDSHAKE_INVOKE_TIMEOUT = 5000; // 5 seconds
    //	public const string P2P_STAT_TRUSTED_PUB_KEY = "";

    //	public static readonly ulong DATABASE_WRITE_BUFFER_MB_DEFAULT_SIZE = 256;
    //	public static readonly ulong DATABASE_READ_BUFFER_MB_DEFAULT_SIZE = 10;
    //	public static readonly uint DATABASE_DEFAULT_MAX_OPEN_FILES = 100;
    //	public static readonly ushort DATABASE_DEFAULT_BACKGROUND_THREADS_COUNT = 2;

    //	public const string LATEST_VERSION_URL = "http://latest.turtlecoin.lol";
    //	public static readonly string LICENSE_URL = "https://github.com/turtlecoin/turtlecoin/blob/master/LICENSE";
    //	internal boost::uuids.uuid CRYPTONOTE_NETWORK = new boost::uuids.uuid({0xb5, 0x0c, 0x4a, 0x6c, 0xcf, 0x52, 0x57, 0x41, 0x65, 0xf9, 0x91, 0xa4, 0xb6, 0xc1, 0x43, 0xe9});

    //	public static readonly string[] SEED_NODES = {"206.189.142.142:11897", "145.239.88.119:11999", "142.44.242.106:11897", "165.227.252.132:11897"};
    //	//{
    //	//  struct
    //	//  {
    //	//	uint blockIndex;
    //	//	ushort transactionIndex;
    //	//	ushort outputIndex;
    //	//  };
    //	//
    //	//  ulong packedValue;
    //	//};

    //	public static readonly uint INVALID_BLOCK_INDEX = uint.MaxValue;

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void serialize(BlockFullInfo UnnamedParameter, ISerializer UnnamedParameter2);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void serialize(TransactionPrefixInfo UnnamedParameter, ISerializer UnnamedParameter2);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//void serialize(BlockShortInfo UnnamedParameter, ISerializer UnnamedParameter2);
    ////C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
    ////ORIGINAL LINE: bool IntrusiveLinkedList<Value>::iterator::operator !=(const typename IntrusiveLinkedList<Value>::iterator& other) const
    //	public static bool IntrusiveLinkedList<Value>.iterator.operator != <Value>(IntrusiveLinkedList<Value>.iterator other)
    //	{
    //	  return currentElement != other.currentElement;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<class Value>
    ////C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
    ////ORIGINAL LINE: bool IntrusiveLinkedList<Value>::iterator::operator ==(const typename IntrusiveLinkedList<Value>::iterator& other) const
    //	public static bool IntrusiveLinkedList<Value>.iterator.operator == <Value>(IntrusiveLinkedList<Value>.iterator other)
    //	{
    //	  return currentElement == other.currentElement;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//std::unique_ptr<IUpgradeDetector> makeUpgradeDetector(byte targetVersion, uint upgradeIndex);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//  std::unique_ptr<ITransaction> createTransaction();
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//  std::unique_ptr<ITransaction> createTransaction(ClassicVector<byte> transactionBlob);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//  std::unique_ptr<ITransaction> createTransaction(Transaction tx);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//  std::unique_ptr<ITransactionReader> createTransactionPrefix(TransactionPrefix prefix, Crypto::Hash transactionHash);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//  std::unique_ptr<ITransactionReader> createTransactionPrefix(Transaction fullTransaction);

    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//TransactionPoolMessage makeAddTransaction(Crypto::Hash hash);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //	//TransactionPoolMessage makeDelTransaction(Crypto::Hash hash);

    //	public static string get_protocol_state_string(CryptoNoteConnectionContext.state s)
    //	{
    //	  switch (s)
    //	  {
    //	  case CryptoNoteConnectionContext.state_befor_handshake:
    //		return "state_befor_handshake";
    //	  case CryptoNoteConnectionContext.state_synchronizing:
    //		return "state_synchronizing";
    //	  case CryptoNoteConnectionContext.state_idle:
    //		return "state_idle";
    //	  case CryptoNoteConnectionContext.state_normal:
    //		return "state_normal";
    //	  case CryptoNoteConnectionContext.state_sync_required:
    //		return "state_sync_required";
    //	  case CryptoNoteConnectionContext.state_pool_sync_required:
    //		return "state_pool_sync_required";
    //	  case CryptoNoteConnectionContext.state_shutdown:
    //		return "state_shutdown";
    //	  default:
    //		return "unknown";
    //	  }
    //	}
    //	  public static bool serialize(boost::uuids.uuid v, Common.StringView name, ISerializer s)
    //	  {
    //		return s.binary(v, sizeof(boost::uuids.uuid), new Common.StringView(name));
    //	  }

    //	  public static Crypto.Hash get_proof_of_trust_hash(proof_of_trust pot)
    //	  {
    //		string s;
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		s.append(reinterpret_cast<const char>(pot.peer_id), sizeof(ulong));
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
    //		s.append(reinterpret_cast<const char>(pot.time), sizeof(ulong));
    //		return Crypto.GlobalMembers.cn_fast_hash(s.data(), s.Length);
    //	  }
    //	public static BinaryArray storeToBinary<T>(T obj)
    //	{
    //	  BinaryArray result = new BinaryArray();
    //	  Common.VectorOutputStream stream = new Common.VectorOutputStream(result);
    //	  BinaryOutputStreamSerializer ba = new BinaryOutputStreamSerializer(stream);
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
    //	  serialize(const_cast<T&>(obj), ba.functorMethod);
    //	  return result;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T>
    //	public static void loadFromBinary<T>(T obj, BinaryArray blob)
    //	{
    //	  Common.MemoryInputStream stream = new Common.MemoryInputStream(blob.data(), blob.size());
    //	  BinaryInputStreamSerializer ba = new BinaryInputStreamSerializer(stream);
    //	  serialize(obj, ba.functorMethod);
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename T>
    //	public static bool storeToBinaryFile<T>(T obj, string filename)
    //	{
    //	  try
    //	  {
    //		std::ofstream dataFile = new std::ofstream();
    //		dataFile.open(filename, std::ios_base.binary | std::ios_base.@out | std::ios.trunc);
    //		if (dataFile.fail())
    //		{
    //		  return false;
    //		}

    //		Common.StdOutputStream stream = new Common.StdOutputStream(dataFile);
    //		BinaryOutputStreamSerializer @out = new BinaryOutputStreamSerializer(stream);
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
    //		CryptoNote.GlobalMembers.serialize(const_cast<T&>(obj), @out.functorMethod);

    //		if (dataFile.fail())
    //		{
    //		  return false;
    //		}

    //		dataFile.flush();
    //	  }
    //	  catch (System.Exception)
    //	  {
    //		return false;
    //	  }

    //	  return true;
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template<class T>
    //	public static bool loadFromBinaryFile<T>(T obj, string filename)
    //	{
    //	  try
    //	  {
    //		std::ifstream dataFile = new std::ifstream();
    //		dataFile.open(filename, std::ios_base.binary | std::ios_base.in);
    //		if (dataFile.fail())
    //		{
    //		  return false;
    //		}

    //		Common.StdInputStream stream = new Common.StdInputStream(dataFile);
    //		BinaryInputStreamSerializer in = new BinaryInputStreamSerializer(stream);
    //		serialize(obj, in.functorMethod);
    //		return !dataFile.fail();
    //	  }
    //	  catch (System.Exception)
    //	  {
    //		return false;
    //	  }
    //	}

    //	public static readonly uint PORTABLE_STORAGE_SIGNATUREA = 0x01011101;
    //	public static readonly uint PORTABLE_STORAGE_SIGNATUREB = 0x01020101; // bender's nightmare
    //	public static readonly byte PORTABLE_STORAGE_FORMAT_VER = 1;

    //	public static readonly byte PORTABLE_RAW_SIZE_MARK_MASK = 0x03;
    //	public static readonly byte PORTABLE_RAW_SIZE_MARK_BYTE = 0;
    //	public static readonly byte PORTABLE_RAW_SIZE_MARK_WORD = 1;
    //	public static readonly byte PORTABLE_RAW_SIZE_MARK_DWORD = 2;
    //	public static readonly byte PORTABLE_RAW_SIZE_MARK_INT64 = 3;


    //	//data types 

    //	public static readonly byte BIN_KV_SERIALIZE_TYPE_INT64 = 1;
    //	public static readonly byte BIN_KV_SERIALIZE_TYPE_INT32 = 2;
    //	public static readonly byte BIN_KV_SERIALIZE_TYPE_INT16 = 3;
    //	public static readonly byte BIN_KV_SERIALIZE_TYPE_INT8 = 4;
    //	public static readonly byte BIN_KV_SERIALIZE_TYPE_UINT64 = 5;
    //	public static readonly byte BIN_KV_SERIALIZE_TYPE_UINT32 = 6;
    //	public static readonly byte BIN_KV_SERIALIZE_TYPE_UINT16 = 7;
    //	public static readonly byte BIN_KV_SERIALIZE_TYPE_UINT8 = 8;
    //	public static readonly byte BIN_KV_SERIALIZE_TYPE_DOUBLE = 9;
    //	public static readonly byte BIN_KV_SERIALIZE_TYPE_STRING = 10;
    //	public static readonly byte BIN_KV_SERIALIZE_TYPE_BOOL = 11;
    //	public static readonly byte BIN_KV_SERIALIZE_TYPE_OBJECT = 12;
    //	public static readonly byte BIN_KV_SERIALIZE_TYPE_ARRAY = 13;
    //	public static readonly byte BIN_KV_SERIALIZE_FLAG_ARRAY = 0x80;

    //	public static readonly uint UNCONFIRMED_TRANSACTION_GLOBAL_OUTPUT_INDEX = uint.MaxValue;

    //	public static bool operator == (AccountPublicAddress _v1, AccountPublicAddress _v2)
    //	{
    ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
    //	  return memcmp(_v1, _v2, sizeof(AccountPublicAddress)) == 0;
    //	}

    //	public static readonly ulong ACCOUNT_CREATE_TIME_ACCURACY = 60 * 60 * 24;

    //	public static typedef boost::multi_index_container < WalletRecord, boost::multi_index.indexed_by < boost::multi_index.random_access < boost::multi_index.tag <RandomAccessIndex>>, boost::multi_index.hashed_unique < boost::multi_index.tag <KeysIndex>, BOOST_MULTI_INDEX_MEMBER(WalletRecord, Crypto.PublicKey, spendPublicKey)>, boost::multi_index.hashed_unique < boost::multi_index.tag <TransfersContainerIndex>, BOOST_MULTI_INDEX_MEMBER(WalletRecord, CryptoNote.ITransfersContainer*, container) >> > WalletsContainer = new typedef();

    //	public static typedef boost::multi_index_container < UnlockTransactionJob, boost::multi_index.indexed_by < boost::multi_index.ordered_non_unique < boost::multi_index.tag <BlockHeightIndex>, BOOST_MULTI_INDEX_MEMBER(UnlockTransactionJob, uint, blockHeight) >, boost::multi_index.hashed_non_unique < boost::multi_index.tag <TransactionHashIndex>, BOOST_MULTI_INDEX_MEMBER(UnlockTransactionJob, Crypto.Hash, transactionHash) >> > UnlockTransactionJobs = new typedef();
    //	}
    //}

    //namespace CryptoNote.Utils
    //{
    //	public static class GlobalMembers
    //	{
    //	public static bool restoreCachedTransactions(List<BinaryArray> binaryTransactions, List<CachedTransaction> transactions)
    //	{
    //	  transactions.Capacity = binaryTransactions.Count;

    //	  foreach (var binaryTransaction in binaryTransactions)
    //	  {
    //		Transaction transaction = new Transaction();
    //		if (!CryptoNote.GlobalMembers.fromBinaryArray(ref transaction, binaryTransaction))
    //		{
    //		  return false;
    //		}

    //		transactions.emplace_back(std::move(transaction));
    //	  }

    //	  return true;
    //	}
    //	}
    //}

    //namespace CryptoNote.DB
    //{
    //	public static class GlobalMembers
    //	{
    //	  public static readonly string BLOCK_INDEX_TO_KEY_IMAGE_PREFIX = "0";
    //	  public static readonly string BLOCK_INDEX_TO_TX_HASHES_PREFIX = "1";
    //	  public static readonly string BLOCK_INDEX_TO_TRANSACTION_INFO_PREFIX = "2";
    //	  public static readonly string BLOCK_INDEX_TO_RAW_BLOCK_PREFIX = "4";

    //	  public static readonly string BLOCK_HASH_TO_BLOCK_INDEX_PREFIX = "5";
    //	  public static readonly string BLOCK_INDEX_TO_BLOCK_INFO_PREFIX = "6";

    //	  public static readonly string KEY_IMAGE_TO_BLOCK_INDEX_PREFIX = "7";
    //	  public static readonly string BLOCK_INDEX_TO_BLOCK_HASH_PREFIX = "8";

    //	  public static readonly string TRANSACTION_HASH_TO_TRANSACTION_INFO_PREFIX = "a";

    //	  public static readonly string KEY_OUTPUT_AMOUNT_PREFIX = "b";

    //	  public static readonly string CLOSEST_TIMESTAMP_BLOCK_INDEX_PREFIX = "e";

    //	  public static readonly string PAYMENT_ID_TO_TX_HASH_PREFIX = "f";

    //	  public static readonly string TIMESTAMP_TO_BLOCKHASHES_PREFIX = "g";

    //	  public static readonly string KEY_OUTPUT_AMOUNTS_COUNT_PREFIX = "h";

    //	  public static readonly string LAST_BLOCK_INDEX_KEY = "last_block_index";

    //	  public static readonly string KEY_OUTPUT_AMOUNTS_COUNT_KEY = "key_amounts_count";

    //	  public static readonly string TRANSACTIONS_COUNT_KEY = "txs_count";

    //	  public static readonly string KEY_OUTPUT_KEY_PREFIX = "j";

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <class Value>
    //	  public static string serialize<Value>(Value value, string name)
    //	  {
    //		CryptoNote.KVBinaryOutputStreamSerializer serializer = new CryptoNote.KVBinaryOutputStreamSerializer();
    //		std::stringstream ss = new std::stringstream();
    //		Common.StdOutputStream stream = new Common.StdOutputStream(ss);

    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
    //		serializer.functorMethod(const_cast<Value&>(value), name);
    //		serializer.dump(stream);

    //		return ss.str();
    //	  }

    //	  public static string serialize(RawBlock value, string name)
    //	  {
    //		std::stringstream ss = new std::stringstream();
    //		Common.StdOutputStream stream = new Common.StdOutputStream(ss);
    //		CryptoNote.BinaryOutputStreamSerializer serializer = new CryptoNote.BinaryOutputStreamSerializer(stream);

    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
    //		serializer.functorMethod(const_cast<RawBlock&>(value).block, GlobalMembers.RAW_BLOCK_NAME);
    ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
    //		serializer.functorMethod(const_cast<RawBlock&>(value).transactions, GlobalMembers.RAW_TXS_NAME);

    //		return ss.str();
    //	  }

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <class Key, class Value>
    //	  public static Tuple<string, string> serialize<Key, Value>(string keyPrefix, Key key, Value value)
    //	  {
    //		return new Tuple<string, string>(DB.GlobalMembers.serialize(Tuple.Create(keyPrefix, key), keyPrefix), DB.GlobalMembers.serialize(value, keyPrefix));
    //	  }

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <class Key>
    //	  public static string serializeKey<Key>(string keyPrefix, Key key)
    //	  {
    //		return DB.GlobalMembers.serialize(Tuple.Create(keyPrefix, key), keyPrefix);
    //	  }

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <class Value>
    //	  public static void deserialize<Value>(string serialized, Value value, string name)
    //	  {
    //		std::stringstream ss = new std::stringstream(serialized);
    //		Common.StdInputStream stream = new Common.StdInputStream(ss);
    //		CryptoNote.KVBinaryInputStreamSerializer serializer = new CryptoNote.KVBinaryInputStreamSerializer(stream);
    //		serializer(value, name);
    //	  }

    //	  public static void deserialize(string serialized, RawBlock value, string name)
    //	  {
    //		std::stringstream ss = new std::stringstream(serialized);
    //		Common.StdInputStream stream = new Common.StdInputStream(ss);
    //		CryptoNote.BinaryInputStreamSerializer serializer = new CryptoNote.BinaryInputStreamSerializer(stream);
    //		serializer.functorMethod(value.block, GlobalMembers.RAW_BLOCK_NAME);
    //		serializer.functorMethod(value.transactions, GlobalMembers.RAW_TXS_NAME);
    //	  }

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <class Key, class Value>
    //	  public static void serializeKeys<Key, Value>(List<string> rawKeys, string keyPrefix, Dictionary<Key, Value> map)
    //	  {
    //		foreach (Tuple<Key, Value> kv in map)
    //		{
    //		  rawKeys.emplace_back(DB.GlobalMembers.serializeKey(keyPrefix, kv.Item1));
    //		}
    //	  }

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <class Key, class Value, class Iterator>
    //	  public static void deserializeValues<Key, Value, Iterator>(Dictionary<Key, Value> map, Iterator serializedValuesIter, string name)
    //	  {
    //		for (var iter = map.GetEnumerator(); iter != map.end(); ++serializedValuesIter)
    //		{
    //		  if (boost::get<1>(*serializedValuesIter))
    //		  {
    //			DB.GlobalMembers.deserialize(boost::get<0>(*serializedValuesIter), iter.second, name);
    //			++iter;
    //		  }
    //		  else
    //		  {
    //			iter = map.Remove(iter);
    //		  }
    //		}
    //	  }

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <class Value, class Iterator>
    //	  public static void deserializeValue<Value, Iterator>(ref Tuple<Value, bool> pair, Iterator serializedValuesIter, string name)
    //	  {
    //		if (pair.Item2)
    //		{
    //		  if (boost::get<1>(*serializedValuesIter))
    //		  {
    //			DB.GlobalMembers.deserialize(boost::get<0>(*serializedValuesIter), pair.Item1, name);
    //		  }
    //		  else
    //		  {
    //			pair = new Tuple<Value, bool>(Value {}, false);
    //		  }
    //		  ++serializedValuesIter;
    //		}
    //	  }
    //	}
    //}

    //namespace CryptoNote.JsonRpc
    //{
    //	public static class GlobalMembers
    //	{
    //	public static readonly int errParseError = -32700;
    //	public static readonly int errInvalidRequest = -32600;
    //	public static readonly int errMethodNotFound = -32601;
    //	public static readonly int errInvalidParams = -32602;
    //	public static readonly int errInternalError = -32603;
    //	public static readonly int errInvalidPassword = -32604;


    //	//void invokeJsonRpcCommand(HttpClient httpClient, JsonRpcRequest req, JsonRpcResponse res);Tangible Method Implementation Not FoundCryptoNote.JsonRpc-invokeJsonRpcCommand

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename Request, typename Response>
    //	public static void invokeJsonRpcCommand<Request, Response>(HttpClient httpClient, string method, Request req, Response res)
    //	{
    //	  JsonRpcRequest jsReq = new JsonRpcRequest();
    //	  JsonRpcResponse jsRes = new JsonRpcResponse();

    //	  jsReq.setMethod(method);
    //	  jsReq.setParams(req);

    //	  invokeJsonRpcCommand(httpClient, jsReq, jsRes);

    //	  jsRes.getResult(res);
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename Request, typename Response, typename Handler>
    //	public static bool invokeMethod<Request, Response, Handler>(JsonRpcRequest jsReq, JsonRpcResponse jsRes, Handler handler)
    //	{
    //	  Request req = new default(Request);
    //	  Response res = new default(Response);

    //	  if (!std::is_same<Request, CryptoNote.EMPTY_STRUCT>.value && !jsReq.loadParams(req))
    //	  {
    //		throw new JsonRpcError(JsonRpc.GlobalMembers.errInvalidParams);
    //	  }

    //	  bool result = handler(req, res);

    //	  if (result)
    //	  {
    //		if (!jsRes.setResult(res))
    //		{
    //		  throw new JsonRpcError(JsonRpc.GlobalMembers.errInternalError);
    //		}
    //	  }
    //	  return result;
    //	}

    //	public delegate bool JsonMemberMethod(object UnnamedParameter, JsonRpcRequest req, JsonRpcResponse res);

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename Class, typename Params, typename Result>
    //private delegate bool handlerDelegate(Params UnnamedParameter, Result UnnamedParameter2);

    //	public static JsonMemberMethod makeMemberMethod<Class, Params, Result>(handlerDelegate handler)
    //	{
    ////C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
    ////ORIGINAL LINE: return [handler](object* obj, const JsonRpcRequest& req, JsonRpcResponse& res)
    //  return (object obj, JsonRpcRequest req, JsonRpcResponse res) =>
    //  {
    //	return JsonRpc.invokeMethod<Params, Result>(req, res, std::bind(handler, (Class)obj, std::placeholders._1, std::placeholders._2));
    //  };
    //	}

    //	public static readonly int errParseError = -32700;
    //	public static readonly int errInvalidRequest = -32600;
    //	public static readonly int errMethodNotFound = -32601;
    //	public static readonly int errInvalidParams = -32602;
    //	public static readonly int errInternalError = -32603;
    //	public static readonly int errInvalidPassword = -32604;


    //	public static void invokeJsonRpcCommand(HttpClient httpClient, JsonRpcRequest jsReq, JsonRpcResponse jsRes)
    //	{
    //	  HttpRequest httpReq = new HttpRequest();
    //	  HttpResponse httpRes = new HttpResponse();

    //	  httpReq.addHeader("Content-Type", "application/json");
    //	  httpReq.setUrl("/json_rpc");
    //	  httpReq.setBody(jsReq.getBody());

    //	  httpClient.request(httpReq, httpRes);

    //	  if (httpRes.getStatus() != HttpResponse.STATUS_200)
    //	  {
    //		throw new System.Exception("JSON-RPC call failed, HTTP status = " + Convert.ToString(httpRes.getStatus()));
    //	  }

    //	  jsRes.parse(httpRes.getBody());

    //	  JsonRpcError err = new JsonRpcError();
    //	  if (jsRes.getError(err))
    //	  {
    //		throw err;
    //	  }
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename Request, typename Response>
    //	public static void invokeJsonRpcCommand<Request, Response>(HttpClient httpClient, string method, Request req, Response res)
    //	{
    //	  JsonRpcRequest jsReq = new JsonRpcRequest();
    //	  JsonRpcResponse jsRes = new JsonRpcResponse();

    //	  jsReq.setMethod(method);
    //	  jsReq.setParams(req);

    //	  JsonRpc.GlobalMembers.invokeJsonRpcCommand(httpClient, jsReq, jsRes);

    //	  jsRes.getResult(res);
    //	}

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename Request, typename Response, typename Handler>
    //	public static bool invokeMethod<Request, Response, Handler>(JsonRpcRequest jsReq, JsonRpcResponse jsRes, Handler handler)
    //	{
    //	  Request req = new default(Request);
    //	  Response res = new default(Response);

    //	  if (!std::is_same<Request, CryptoNote.EMPTY_STRUCT>.value && !jsReq.loadParams(req))
    //	  {
    //		throw new JsonRpcError(JsonRpc.GlobalMembers.errInvalidParams);
    //	  }

    //	  bool result = handler(req, res);

    //	  if (result)
    //	  {
    //		if (!jsRes.setResult(res))
    //		{
    //		  throw new JsonRpcError(JsonRpc.GlobalMembers.errInternalError);
    //		}
    //	  }
    //	  return result;
    //	}

    //	public delegate bool JsonMemberMethod(object UnnamedParameter, JsonRpcRequest req, JsonRpcResponse res);

    ////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    ////ORIGINAL LINE: template <typename Class, typename Params, typename Result>
    //private delegate bool handlerDelegate(Params UnnamedParameter, Result UnnamedParameter2);

    //	public static JsonMemberMethod makeMemberMethod<Class, Params, Result>(handlerDelegate handler)
    //	{
    ////C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
    ////ORIGINAL LINE: return [handler](object* obj, const JsonRpcRequest& req, JsonRpcResponse& res)
    //  return (object obj, JsonRpcRequest req, JsonRpcResponse res) =>
    //  {
    //	return JsonRpc.invokeMethod<Params, Result>(req, res, std::bind(handler, (Class)obj, std::placeholders._1, std::placeholders._2));
    //  };
    //	}
    //	}
    //}

    //namespace Miner
    //{
    //	public static class GlobalMembers
    //	{
    //	public static MinerEvent BlockMinedEvent()
    //	{
    //	  MinerEvent event = new MinerEvent();
    //	  event.type = MinerEventType.BLOCK_MINED;
    //	  return event;
    //	}

    //	public static MinerEvent BlockchainUpdatedEvent()
    //	{
    //	  MinerEvent event = new MinerEvent();
    //	  event.type = MinerEventType.BLOCKCHAIN_UPDATED;
    //	  return event;
    //	}

    //	public static void adjustMergeMiningTag(BlockTemplate blockTemplate)
    //	{
    //	  CachedBlock cachedBlock = new CachedBlock(blockTemplate);
    //	  if (blockTemplate.majorVersion >= BLOCK_MAJOR_VERSION_2)
    //	  {
    //		CryptoNote.TransactionExtraMergeMiningTag mmTag = new CryptoNote.TransactionExtraMergeMiningTag();
    //		mmTag.depth = 0;
    ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    ////ORIGINAL LINE: mmTag.merkleRoot = cachedBlock.getAuxiliaryBlockHeaderHash();
    //		mmTag.merkleRoot.CopyFrom(cachedBlock.getAuxiliaryBlockHeaderHash());

    //		blockTemplate.parentBlock.baseTransaction.extra.Clear();
    //		if (!CryptoNote.appendMergeMiningTagToExtra(blockTemplate.parentBlock.baseTransaction.extra, mmTag))
    //		{
    //		  throw new System.Exception("Couldn't append merge mining tag");
    //		}
    //	  }
    //	}


    //	private void MinerManager.pushEvent(MinerEvent && event)
    //	{
    //	  m_events.push(std::move(event));
    //	  m_eventOccurred.set();
    //	}
    //	}
    //}

        //namespace Mnemonics
        //{
        //	public static class GlobalMembers
        //	{
        //		public static Tuple<string, Crypto.SecretKey> MnemonicToPrivateKey(string words)
        //		{
        //			List<string> wordsList = new List<string>();

        //			std::istringstream stream = new std::istringstream(words);

        //			/* Convert whitespace separated string into vector of words */
        //			for (string s; stream >> s;)
        //			{
        //				wordsList.Add(s);
        //			}

        //			return MnemonicToPrivateKey(new List<string>(wordsList));
        //		}

        //		/* Note - if the returned string is not empty, it is an error message, and
        //		   the returned secret key is not initialized. */
        //		public static Tuple<string, Crypto.SecretKey> MnemonicToPrivateKey(List<string> words)
        //		{
        //			Crypto.SecretKey key = new Crypto.SecretKey();

        //			uint len = words.Count;

        //			/* Mnemonics must be 25 words long */
        //			if (len != 25)
        //			{
        //				std::stringstream str = new std::stringstream();

        //				/* Write out "word" or "words" to make the grammar of the next sentence
        //				   correct, based on if we have 1 or more words */
        //				string wordPlural = len == 1 ? "word" : "words";

        //				str << "Mnemonic seed is wrong length - It should be 25 words " << "long, but it is " << len << " " << wordPlural << " long!";

        //				return new Tuple<string, Crypto.SecretKey>(str.str(), key);
        //			}

        //			/* All words must be present in the word list */
        //			foreach (var word in words)
        //			{
        //				std::transform(word.GetEnumerator(), word.end(), word.GetEnumerator(), global::tolower);
        //				if (std::find(WordList.English.begin(), WordList.English.end(), word) == WordList.English.end())
        //				{
        //					std::stringstream str = new std::stringstream();

        //					str << "Mnemonic seed has invalid word - " << word << " is not in the English word list!";

        //					return new Tuple<string, Crypto.SecretKey>(str.str(), key);
        //				}
        //			}

        //			/* The checksum must be correct */
        //			if (!HasValidChecksum(new List<string>(words)))
        //			{
        //				return new Tuple<string, Crypto.SecretKey>("Mnemonic seed has incorrect checksum!", key);
        //			}

        //			var wordIndexes = GetWordIndexes(new List<string>(words));

        //			List<ushort> data = new List<ushort>();

        //			for (uint i = 0; i < words.Count - 1; i += 3)
        //			{
        //				/* Take the indexes of these three words in the word list */
        //				uint w1 = wordIndexes[i];
        //				uint w2 = wordIndexes[i + 1];
        //				uint w3 = wordIndexes[i + 2];

        //				/* Word list length */
        //				uint wlLen = WordList.English.size();

        //				/* no idea what this does lol */
        //				uint val = (uint)(w1 + wlLen * (((wlLen - w1) + w2) % wlLen) + wlLen * wlLen * (((wlLen - w2) + w3) % wlLen));

        //				/* Don't know what this is testing either */
        //				if ((val % wlLen == w1) == null)
        //				{
        //					return new Tuple<string, Crypto.SecretKey>("Mnemonic seed is invalid!", key);
        //				}

        //				/* Interpret val as 4 ushort's */
        ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
        //				var ptr = reinterpret_cast<const ushort>(val);

        //				/* Append to private key */
        //				for (int j = 0; j < 4; j++)
        //				{
        //					data.Add(ptr[j]);
        //				}
        //			}

        //			/* Copy the data to the secret key */
        //			std::copy(data.GetEnumerator(), data.end(), key.data);

        //			return new Tuple<string, Crypto.SecretKey>(string(), key);
        //		}

        //		public static string PrivateKeyToMnemonic(Crypto.SecretKey privateKey)
        //		{
        //			List<string> words = new List<string>();

        //			for (int i = 0; i < 32 - 1; i += 4)
        //			{
        //				/* Read the array as a uint array */
        //				var ptr = (uint) privateKey.data[i];

        //				/* Take the first element of the array (since we have already 
        //				   done the offset */
        //				uint val = ptr[0];

        //				uint wlLen = WordList.English.size();

        //				uint w1 = val % wlLen;
        //				uint w2 = ((val / wlLen) + w1) % wlLen;
        //				uint w3 = (((val / wlLen) / wlLen) + w2) % wlLen;

        //				words.Add(WordList.English[w1]);
        //				words.Add(WordList.English[w2]);
        //				words.Add(WordList.English[w3]);
        //			}

        //			words.Add(GetChecksumWord(new List<string>(words)));

        //			string result;

        //			foreach (string it in words)
        //			{
        //				if (it != words.GetEnumerator())
        //				{
        //					result += " ";
        //				}

        //				result += it;
        //			}

        //			return result;
        //		}

        //		/* Assumes the input is 25 words long */
        //		public static bool HasValidChecksum(List<string> words)
        //		{
        //			/* Make a copy since erase() is mutating */
        //			var wordsNoChecksum = words;

        //			/* Remove the last checksum word */
        ////C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'erase' method in C#:
        //			wordsNoChecksum.erase(wordsNoChecksum.end() - 1);

        //			/* Assert the last word (the checksum word) is equal to the derived
        //			   checksum */
        //			return words[words.Count - 1] == GetChecksumWord(new List<string>(wordsNoChecksum));
        //		}

        //		public static string GetChecksumWord(List<string> words)
        //		{
        //			string trimmed;

        //			/* Take the first 3 char from each of the 24 words */
        //			foreach (var word in words)
        //			{
        //				trimmed += word.Substring(0, 3);
        //			}

        //			/* Hash the data */
        //			ulong hash = CRC32.crc32(trimmed);

        //			/* Modulus the hash by the word length to get the index of the 
        //			   checksum word */
        //			return words[hash % words.Count];
        //		}

        //		public static List<int> GetWordIndexes(List<string> words)
        //		{
        //			List<int> result = new List<int>();

        //			foreach (var word in words)
        //			{
        //				/* Find the iterator of our word in the wordlist */
        //				var it = std::find(WordList.English.begin(), WordList.English.end(), word);

        //				/* Take it away from the beginning of the vector, giving us the
        //				   index of the item in the vector */
        //				result.Add((int)std::distance(WordList.English.begin(), it));
        //			}

        //			return result;
        //		}
        ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //	//	System.Tuple<string, Crypto::SecretKey> MnemonicToPrivateKey(string words);

        ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //	//	System.Tuple<string, Crypto::SecretKey> MnemonicToPrivateKey(ClassicVector<string> words);

        ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //	//	string PrivateKeyToMnemonic(Crypto::SecretKey privateKey);

        ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //	//	bool HasValidChecksum(ClassicVector<string> words);

        ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //	//	string GetChecksumWord(ClassicVector<string> words);

        ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //	//	ClassicVector<int> GetWordIndexes(ClassicVector<string> words);
        //	}
        //}

        //namespace System
        //{
        //	public static class GlobalMembers
        //	{
        //	//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
        //	//static_assert(Dispatcher::SIZEOF_PTHREAD_MUTEX_T == sizeof(pthread_mutex_t), "invalid pthread mutex size");

        //	public static readonly uint STACK_SIZE = 64 * 1024;
        //	public static string lastErrorMessage()
        //	{
        //	  return errorMessage(errno);
        //	}
        //	public static string errorMessage(int err)
        //	{
        //	  return "result=" + Convert.ToString(err) + ", " + std::strerror(err);
        //	}

        //	public static readonly uint STACK_SIZE = 64 * 1024;

        //	public static string lastErrorMessage()
        //	{
        //	  return errorMessage(errno);
        //	}

        //	public static string errorMessage(int err)
        //	{
        //	  return "result=" + Convert.ToString(err) + ", " + std::strerror(err);
        //	}

        ////C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
        ////C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
        //	public static Ipv4Resolver Ipv4Resolver.operator = (Ipv4Resolver && other)
        //	{
        //	  dispatcher = other.dispatcher;
        //	  if (dispatcher != null)
        //	  {
        //		other.dispatcher = null;
        //	  }

        //	  return this;
        //	}

        ////C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
        ////C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
        //	public static TcpConnection TcpConnection.operator = (TcpConnection && other)
        //	{
        //	  if (dispatcher != null)
        //	  {
        //		Debug.Assert(readContext == null);
        //		Debug.Assert(writeContext == null);
        //		if (close(connection) == -1)
        //		{
        //		  throw new System.Exception("TcpConnection::operator=, close failed, " + lastErrorMessage());
        //		}
        //	  }

        //	  dispatcher = other.dispatcher;
        //	  if (other.dispatcher != null)
        //	  {
        //		Debug.Assert(other.readContext == null);
        //		Debug.Assert(other.writeContext == null);
        //		connection = other.connection;
        //		readContext = null;
        //		writeContext = null;
        //		other.dispatcher = null;
        //	  }

        //	  return this;
        //	}

        ////C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
        ////C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
        //	public static TcpConnector TcpConnector.operator = (TcpConnector && other)
        //	{
        //	  dispatcher = other.dispatcher;
        //	  if (other.dispatcher != null)
        //	  {
        //		Debug.Assert(other.context == null);
        //		context = null;
        //		other.dispatcher = null;
        //	  }

        //	  return this;
        //	}

        ////C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
        ////C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
        //	public static TcpListener TcpListener.operator = (TcpListener && other)
        //	{
        //	  if (dispatcher != null)
        //	  {
        //		Debug.Assert(context == null);
        //		if (close(listener) == -1)
        //		{
        //		  throw new System.Exception("TcpListener::operator=, close failed, " + lastErrorMessage());
        //		}
        //	  }

        //	  dispatcher = other.dispatcher;
        //	  if (other.dispatcher != null)
        //	  {
        //		Debug.Assert(other.context == null);
        //		listener = other.listener;
        //		context = null;
        //		other.dispatcher = null;
        //	  }

        //	  return this;
        //	}

        ////C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
        ////C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
        //	public static Timer Timer.operator = (Timer && other)
        //	{
        //	  Debug.Assert(dispatcher == null || context == null);
        //	  dispatcher = other.dispatcher;
        //	  if (other.dispatcher != null)
        //	  {
        //		Debug.Assert(other.context == null);
        //		timer = other.timer;
        //		context = null;
        //		other.dispatcher = null;
        //		other.timer = -1;
        //	  }

        //	  return this;
        //	}

        //	public static readonly uint STACK_SIZE = 16384;
        //	public static readonly uint RESERVE_STACK_SIZE = 2097152;

        //	public static string lastErrorMessage()
        //	{
        //	  return errorMessage(GetLastError());
        //	}

        //	public static string errorMessage(int error)
        //	{
        ////C++ TO C# CONVERTER TODO TASK: C# does not allow declaring types within methods:
        //	//  struct Buffer
        //	//  {
        //	//	~Buffer()
        //	//	{
        //	//	  if (pointer != null)
        //	//	  {
        //	//		LocalFree(pointer);
        //	//	  }
        //	//	}
        //	//
        //	//	string pointer = null;
        //	//  }
        //	//  buffer;

        ////C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
        //	  var size = FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_ALLOCATE_BUFFER, null, error, MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), reinterpret_cast<string>(buffer.pointer), 0, null);
        //	  return "result=" + Convert.ToString(error) + ", " + (string)(buffer.pointer, size);
        //	}

        ////C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
        ////C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
        //	public static Ipv4Resolver Ipv4Resolver.operator = (Ipv4Resolver && other)
        //	{
        //	  dispatcher = other.dispatcher;
        //	  if (dispatcher != null)
        //	  {
        //		other.dispatcher = null;
        //	  }

        //	  return this;
        //	}

        ////C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
        ////C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
        //	public static TcpConnection TcpConnection.operator = (TcpConnection && other)
        //	{
        //	  if (dispatcher != null)
        //	  {
        //		Debug.Assert(readContext == null);
        //		Debug.Assert(writeContext == null);
        //		if (closesocket(connection) != 0)
        //		{
        //		  throw new System.Exception("TcpConnection::operator=, closesocket failed, " + errorMessage(WSAGetLastError()));
        //		}
        //	  }

        //	  dispatcher = other.dispatcher;
        //	  if (dispatcher != null)
        //	  {
        //		Debug.Assert(other.readContext == null);
        //		Debug.Assert(other.writeContext == null);
        //		connection = other.connection;
        //		readContext = null;
        //		writeContext = null;
        //		other.dispatcher = null;
        //	  }

        //	  return this;
        //	}

        //	public static LPFN_CONNECTEX connectEx = null;

        ////C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
        ////C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
        //	public static TcpConnector TcpConnector.operator = (TcpConnector && other)
        //	{
        //	  Debug.Assert(dispatcher == null || context == null);
        //	  dispatcher = other.dispatcher;
        //	  if (dispatcher != null)
        //	  {
        //		Debug.Assert(other.context == null);
        //		context = null;
        //		other.dispatcher = null;
        //	  }

        //	  return this;
        //	}

        //	public static LPFN_ACCEPTEX acceptEx = null;

        ////C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
        ////C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
        //	public static TcpListener TcpListener.operator = (TcpListener && other)
        //	{
        //	  if (dispatcher != null)
        //	  {
        //		Debug.Assert(context == null);
        //		if (closesocket(listener) != 0)
        //		{
        //		  throw new System.Exception("TcpListener::operator=, closesocket failed, " + errorMessage(WSAGetLastError()));
        //		}
        //	  }

        //	  dispatcher = other.dispatcher;
        //	  if (dispatcher != null)
        //	  {
        //		Debug.Assert(other.context == null);
        //		listener = other.listener;
        //		context = null;
        //		other.dispatcher = null;
        //	  }

        //	  return this;
        //	}

        ////C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
        ////C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
        //	public static Timer Timer.operator = (Timer && other)
        //	{
        //	  Debug.Assert(dispatcher == null || context == null);
        //	  dispatcher = other.dispatcher;
        //	  if (dispatcher != null)
        //	  {
        //		Debug.Assert(other.context == null);
        //		context = null;
        //		other.dispatcher = null;
        //	  }

        //	  return this;
        //	}

        //	public static ushort readUint8(string source, ref uint offset)
        //	{
        //	  if (offset == source.Length || source[offset] < '0' || source[offset] > '9')
        //	  {
        //		throw new System.Exception("Unable to read value from string");
        //	  }

        //	  ushort value = source[offset] - '0';
        //	  if (offset + 1 == source.Length || source[offset + 1] < '0' || source[offset + 1] > '9')
        //	  {
        //		offset = offset + 1;
        //		return value;
        //	  }

        //	  if (value == 0)
        //	  {
        //		throw new System.Exception("Unable to read value from string");
        //	  }

        ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
        ////ORIGINAL LINE: value = value * 10 + (source[offset + 1] - '0');
        //	  value.CopyFrom(value * 10 + (source[offset + 1] - '0'));
        //	  if (offset + 2 == source.Length || source[offset + 2] < '0' || source[offset + 2] > '9')
        //	  {
        //		offset = offset + 2;
        //		return value;
        //	  }

        //	  if ((value == 25 && source[offset + 2] > '5') || value > 25)
        //	  {
        //		throw new System.Exception("Unable to read value from string");
        //	  }

        ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
        ////ORIGINAL LINE: value = value * 10 + (source[offset + 2] - '0');
        //	  value.CopyFrom(value * 10 + (source[offset + 2] - '0'));
        //	  offset = offset + 3;
        //	  return value;
        //	}

        //	  // Run other task on dispatcher until future is ready.
        ////C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        ////ORIGINAL LINE: void wait() const
        //	  public static void wait()
        //	  {
        //		while (!event.get())
        //		{
        //		  try
        //		  {
        //			event.wait();
        //		  }
        //		  catch (InterruptedException)
        //		  {
        //			interrupted = true;
        //		  }
        //		}

        //		if (interrupted)
        //		{
        //		  dispatcher.interrupt();
        //		}
        //	  }

        //	  // Wait future to complete.
        //	  public void Dispose()
        //	  {
        //		try
        //		{
        //		  wait();
        //		}
        //		catch
        //		{
        //		}

        //		try
        //		{
        //		  // windows future implementation doesn't wait for completion on destruction
        //		  if (future.valid())
        //		  {
        //			future.wait();
        //		  }
        //		}
        //		catch
        //		{
        //		}
        //	  }

        //	  // This function is executed in future object
        //	  public static T asyncProcedure()
        //	  {
        //		NotifyOnDestruction guard = new NotifyOnDestruction(dispatcher, event);
        //		Debug.Assert(procedure != null);
        //		return procedure();
        //	  }

        //	  public static Dispatcher dispatcher;
        //	  private mutable Event event;
        //	  public static Func<T> procedure;
        //	  public static System.Detail.Future<T> future = new System.Detail.Future<T>();
        //	  public static bool interrupted;
        //	}
        //}

        //namespace PaymentService
        //{
        //	public static class GlobalMembers
        //	{
        ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //	//void generateNewWallet(CryptoNote::Currency currency, WalletConfiguration conf, Logging::ILogger logger, System::Dispatcher dispatcher);

        ////C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
        //	//struct TransactionsInBlockInfoFilter;

        //	public static void generateNewWallet(CryptoNote.Currency currency, WalletConfiguration conf, Logging.ILogger logger, System.Dispatcher dispatcher)
        //	{
        //	  Logging.LoggerRef log = new Logging.LoggerRef(logger, "generateNewWallet");

        //	  CryptoNote.INode nodeStub = NodeFactory.createNodeStub();
        //	  std::unique_ptr<CryptoNote.INode> nodeGuard = new std::unique_ptr<CryptoNote.INode>(nodeStub);

        //	  CryptoNote.IWallet wallet = new CryptoNote.WalletGreen(dispatcher, currency, nodeStub, logger);
        //	  std::unique_ptr<CryptoNote.IWallet> walletGuard = new std::unique_ptr<CryptoNote.IWallet>(wallet);

        //	  string address;
        //	  if (string.IsNullOrEmpty(conf.secretSpendKey) && string.IsNullOrEmpty(conf.secretViewKey) && string.IsNullOrEmpty(conf.mnemonicSeed))
        //	  {
        //		log.functorMethod(Logging.Level.INFO, Logging.BRIGHT_WHITE) << "Generating new wallet";

        //		Crypto.SecretKey private_view_key = new Crypto.SecretKey();
        //		CryptoNote.KeyPair spendKey = new CryptoNote.KeyPair();

        //		Crypto.generate_keys(spendKey.publicKey, spendKey.secretKey);
        //		CryptoNote.AccountBase.generateViewFromSpend(spendKey.secretKey, private_view_key);

        //		wallet.initializeWithViewKey(conf.walletFile, conf.walletPassword, private_view_key, 0, true);
        //		address = wallet.createAddress(spendKey.secretKey, 0, true);

        //		  log.functorMethod(Logging.Level.INFO, Logging.BRIGHT_WHITE) << "New wallet is generated. Address: " << address;
        //	  }
        //	  else if (!string.IsNullOrEmpty(conf.mnemonicSeed))
        //	  {
        //		log.functorMethod(Logging.Level.INFO, Logging.BRIGHT_WHITE) << "Attempting to import wallet from mnemonic seed";

        //		Crypto.SecretKey private_view_key = new Crypto.SecretKey();

        //		var (error, private_spend_key) = Mnemonics.MnemonicToPrivateKey(conf.mnemonicSeed);

        //		if (!error.empty())
        //		{
        //			log.functorMethod(Logging.Level.ERROR, Logging.BRIGHT_RED) << error;
        //			return;
        //		}

        //		CryptoNote.AccountBase.generateViewFromSpend(private_spend_key, private_view_key);
        //		wallet.initializeWithViewKey(conf.walletFile, conf.walletPassword, private_view_key, conf.scanHeight, false);
        //		address = wallet.createAddress(private_spend_key, conf.scanHeight, false);
        //		log.functorMethod(Logging.Level.INFO, Logging.BRIGHT_WHITE) << "Imported wallet successfully.";
        //	  }
        //	  else
        //	  {
        //		  if (string.IsNullOrEmpty(conf.secretSpendKey) || string.IsNullOrEmpty(conf.secretViewKey))
        //		  {
        //			  log.functorMethod(Logging.Level.ERROR, Logging.BRIGHT_RED) << "Need both secret spend key and secret view key.";
        //			  return;
        //		  }
        //		else
        //		{
        //			  log.functorMethod(Logging.Level.INFO, Logging.BRIGHT_WHITE) << "Attemping to import wallet from keys";
        //			  Crypto.Hash private_spend_key_hash = new Crypto.Hash();
        //			  Crypto.Hash private_view_key_hash = new Crypto.Hash();
        //			  ulong size;
        //			  if (!Common.fromHex(conf.secretSpendKey, private_spend_key_hash, sizeof(Crypto.Hash), size) || size != sizeof(Crypto.Hash))
        //			  {
        //				  log.functorMethod(Logging.Level.ERROR, Logging.BRIGHT_RED) << "Invalid spend key";
        //				  return;
        //			  }
        //			  if (!Common.fromHex(conf.secretViewKey, private_view_key_hash, sizeof(Crypto.Hash), size) || size != sizeof(Crypto.Hash))
        //			  {
        //				  log.functorMethod(Logging.Level.ERROR, Logging.BRIGHT_RED) << "Invalid view key";
        //				  return;
        //			  }
        //			  Crypto.SecretKey private_spend_key = (Crypto.SecretKey) private_spend_key_hash;
        //			  Crypto.SecretKey private_view_key = (Crypto.SecretKey) private_view_key_hash;

        //			  wallet.initializeWithViewKey(conf.walletFile, conf.walletPassword, private_view_key, conf.scanHeight, false);
        //			  address = wallet.createAddress(private_spend_key, conf.scanHeight, false);
        //			  log.functorMethod(Logging.Level.INFO, Logging.BRIGHT_WHITE) << "Imported wallet successfully.";
        //		}
        //	  }

        //	  wallet.save(CryptoNote.WalletSaveLevel.SAVE_KEYS_ONLY);
        //	  log.functorMethod(Logging.Level.INFO, Logging.BRIGHT_WHITE) << "Wallet is saved";
        //	}

        ////C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
        //	//struct TransactionsInBlockInfoFilter;

        //	public static bool checkPaymentId(string paymentId)
        //	{
        //	  if (paymentId.Length != 64)
        //	  {
        //		return false;
        //	  }

        //  return std::all_of(paymentId.GetEnumerator(), paymentId.end(), (char c) =>
        //  {
        //	if (c >= '0' && c <= '9')
        //	{
        //	  return true;
        //	}

        //	if (c >= 'a' && c <= 'f')
        //	{
        //	  return true;
        //	}

        //	if (c >= 'A' && c <= 'F')
        //	{
        //	  return true;
        //	}

        //	return false;
        //  });
        //	}

        //	public static Crypto.Hash parsePaymentId(string paymentIdStr)
        //	{
        //	  if (!GlobalMembers.checkPaymentId(paymentIdStr))
        //	  {
        //		throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.WalletServiceErrorCode.WRONG_PAYMENT_ID_FORMAT));
        //	  }

        //	  Crypto.Hash paymentId = new Crypto.Hash();
        //	  bool r = Common.GlobalMembers.podFromHex(paymentIdStr, paymentId);
        //	  if (r)
        //	  {
        //	  }
        //	  Debug.Assert(r);

        //	  return paymentId;
        //	}

        //	public static bool getPaymentIdFromExtra(string binaryString, Crypto.Hash paymentId)
        //	{
        //	  return CryptoNote.getPaymentIdFromTxExtra(Common.asBinaryArray(binaryString), paymentId);
        //	}

        //	public static string getPaymentIdStringFromExtra(string binaryString)
        //	{
        //	  Crypto.Hash paymentId = new Crypto.Hash();

        //	  try
        //	  {
        //		if (!GlobalMembers.getPaymentIdFromExtra(binaryString, paymentId))
        //		{
        //		  return string();
        //		}
        //	  }
        //	  catch (System.Exception)
        //	  {
        //		return string();
        //	  }

        //	  return Common.GlobalMembers.podToHex(paymentId);
        //	}

        //	public static void addPaymentIdToExtra(string paymentId, string extra)
        //	{
        //	  List<byte> extraVector = new List<byte>();
        //	  if (!CryptoNote.createTxExtraWithPaymentId(paymentId, extraVector))
        //	  {
        //		throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BAD_PAYMENT_ID));
        //	  }

        //	  std::copy(extraVector.GetEnumerator(), extraVector.end(), std::back_inserter(extra));
        //	}

        //	public static void validatePaymentId(string paymentId, Logging.LoggerRef logger)
        //	{
        //	  if (!GlobalMembers.checkPaymentId(paymentId))
        //	  {
        //		logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Can't validate payment id: " << paymentId;
        //		throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.WalletServiceErrorCode.WRONG_PAYMENT_ID_FORMAT));
        //	  }
        //	}



        //	public static Crypto.Hash parseHash(string hashString, Logging.LoggerRef logger)
        //	{
        //	  Crypto.Hash hash = new Crypto.Hash();

        //	  if (!Common.GlobalMembers.podFromHex(hashString, hash))
        //	  {
        //		logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Can't parse hash string " << hashString;
        //		throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.WalletServiceErrorCode.WRONG_HASH_FORMAT));
        //	  }

        //	  return hash;
        //	}

        //	public static List<CryptoNote.TransactionsInBlockInfo> filterTransactions(List<CryptoNote.TransactionsInBlockInfo> blocks, TransactionsInBlockInfoFilter filter)
        //	{

        //	  List<CryptoNote.TransactionsInBlockInfo> result = new List<CryptoNote.TransactionsInBlockInfo>();

        //	  foreach (var block in blocks)
        //	  {
        //		CryptoNote.TransactionsInBlockInfo item = new CryptoNote.TransactionsInBlockInfo();
        ////C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
        ////ORIGINAL LINE: item.blockHash = block.blockHash;
        //		item.blockHash.CopyFrom(block.blockHash);

        //		foreach (var transaction in block.transactions)
        //		{
        //		  if (transaction.transaction.state != CryptoNote.WalletTransactionState.DELETED && filter.checkTransaction(transaction))
        //		  {
        //			item.transactions.Add(transaction);
        //		  }
        //		}

        //		if (block.transactions.Count > 0)
        //		{
        //		  result.Add(std::move(item));
        //		}
        //	  }

        //	  return result;
        //	}

        //	public static PaymentService.TransactionRpcInfo convertTransactionWithTransfersToTransactionRpcInfo(CryptoNote.WalletTransactionWithTransfers transactionWithTransfers)
        //	{

        //	  PaymentService.TransactionRpcInfo transactionInfo = new PaymentService.TransactionRpcInfo();

        //	  transactionInfo.state = (byte)transactionWithTransfers.transaction.state;
        //	  transactionInfo.transactionHash = Common.GlobalMembers.podToHex(transactionWithTransfers.transaction.hash);
        //	  transactionInfo.blockIndex = transactionWithTransfers.transaction.blockHeight;
        //	  transactionInfo.timestamp = transactionWithTransfers.transaction.timestamp;
        //	  transactionInfo.isBase = transactionWithTransfers.transaction.isBase;
        //	  transactionInfo.unlockTime = transactionWithTransfers.transaction.unlockTime;
        //	  transactionInfo.amount = transactionWithTransfers.transaction.totalAmount;
        //	  transactionInfo.fee = transactionWithTransfers.transaction.fee;
        //	  transactionInfo.extra = Common.toHex(transactionWithTransfers.transaction.extra.data(), transactionWithTransfers.transaction.extra.Length);
        //	  transactionInfo.paymentId = GlobalMembers.getPaymentIdStringFromExtra(transactionWithTransfers.transaction.extra);

        //	  foreach (CryptoNote  in :WalletTransfer & transfer: transactionWithTransfers.transfers)
        //	  {
        //		PaymentService.TransferRpcInfo rpcTransfer = new PaymentService.TransferRpcInfo();
        //		rpcTransfer.address = transfer.address;
        //		rpcTransfer.amount = transfer.amount;
        //		rpcTransfer.type = (byte)transfer.type;

        //		transactionInfo.transfers.Add(std::move(rpcTransfer));
        //	  }

        //	  return transactionInfo;
        //	}

        //	public static List<PaymentService.TransactionsInBlockRpcInfo> convertTransactionsInBlockInfoToTransactionsInBlockRpcInfo(List<CryptoNote.TransactionsInBlockInfo> blocks)
        //	{

        //	  List<PaymentService.TransactionsInBlockRpcInfo> rpcBlocks = new List<PaymentService.TransactionsInBlockRpcInfo>();
        //	  rpcBlocks.Capacity = blocks.Count;
        //	  foreach (var block in blocks)
        //	  {
        //		PaymentService.TransactionsInBlockRpcInfo rpcBlock = new PaymentService.TransactionsInBlockRpcInfo();
        //		rpcBlock.blockHash = Common.GlobalMembers.podToHex(block.blockHash);

        //		foreach (CryptoNote  in :WalletTransactionWithTransfers & transactionWithTransfers: block.transactions)
        //		{
        //		  PaymentService.TransactionRpcInfo transactionInfo = GlobalMembers.convertTransactionWithTransfersToTransactionRpcInfo(transactionWithTransfers);
        //		  rpcBlock.transactions.Add(std::move(transactionInfo));
        //		}

        //		rpcBlocks.Add(std::move(rpcBlock));
        //	  }

        //	  return rpcBlocks;
        //	}

        //	public static List<PaymentService.TransactionHashesInBlockRpcInfo> convertTransactionsInBlockInfoToTransactionHashesInBlockRpcInfo(List<CryptoNote.TransactionsInBlockInfo> blocks)
        //	{

        //	  List<PaymentService.TransactionHashesInBlockRpcInfo> transactionHashes = new List<PaymentService.TransactionHashesInBlockRpcInfo>();
        //	  transactionHashes.Capacity = blocks.Count;
        //	  foreach (CryptoNote  in :TransactionsInBlockInfo & block: blocks)
        //	  {
        //		PaymentService.TransactionHashesInBlockRpcInfo item = new PaymentService.TransactionHashesInBlockRpcInfo();
        //		item.blockHash = Common.GlobalMembers.podToHex(block.blockHash);

        //		foreach (CryptoNote  in :WalletTransactionWithTransfers & transaction: block.transactions)
        //		{
        //		  item.transactionHashes.emplace_back(Common.GlobalMembers.podToHex(transaction.transaction.hash));
        //		}

        //		transactionHashes.Add(std::move(item));
        //	  }

        //	  return transactionHashes;
        //	}

        //	public static void validateAddresses(List<string> addresses, CryptoNote.Currency currency, Logging.LoggerRef logger)
        //	{
        //	  foreach (var address in addresses)
        //	  {
        //		if (!CryptoNote.validateAddress(address, currency))
        //		{
        //		  logger.functorMethod(Logging.Level.WARNING, Logging.BRIGHT_YELLOW) << "Can't validate address " << address;
        //		  throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BAD_ADDRESS));
        //		}
        //	  }
        //	}

        //	public static Tuple<string, string> decodeIntegratedAddress(string integratedAddr, CryptoNote.Currency currency, Logging.LoggerRef logger)
        //	{
        //		string decoded;
        //		ulong prefix;

        //		/* Need to be able to decode the string as an address */
        //		if (!Tools.Base58.decode_addr(integratedAddr, prefix, decoded))
        //		{
        //			throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BAD_ADDRESS));
        //		}

        //		/* The prefix needs to be the same as the base58 prefix */
        //		if (prefix != CryptoNote.parameters.CRYPTONOTE_PUBLIC_ADDRESS_BASE58_PREFIX)
        //		{
        //			throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BAD_ADDRESS));
        //		}

        //		const ulong paymentIDLen = 64;
        //		/* Grab the payment ID from the decoded address */
        //		string paymentID = decoded.Substring(0, paymentIDLen);

        //		/* Check the extracted payment ID is good. */
        //		GlobalMembers.validatePaymentId(paymentID, logger.functorMethod);

        //		/* The binary array encoded keys are the rest of the address */
        //		string keys = decoded.Substring(paymentIDLen, -1);

        //		CryptoNote.AccountPublicAddress addr = new CryptoNote.AccountPublicAddress();
        //		List<byte> ba = Common.asBinaryArray(keys);

        //		if (!CryptoNote.GlobalMembers.fromBinaryArray(ref addr, ba))
        //		{
        //			throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BAD_ADDRESS));
        //		}

        //		/* Parse the AccountPublicAddress into a standard wallet address */
        //		/* Use the calculated prefix from earlier for less typing :p */
        //		string address = CryptoNote.getAccountAddressAsStr(prefix, addr);

        //		/* Check the extracted address is good. */
        //		GlobalMembers.validateAddresses(new List<string>() {address}, currency, logger.functorMethod);

        //		return Tuple.Create(address, paymentID);
        //	}

        //	public static string getValidatedTransactionExtraString(string extraString)
        //	{
        //	  List<byte> binary = new List<byte>();
        //	  if (!Common.fromHex(extraString, binary))
        //	  {
        //		throw std::system_error(GlobalMembers.make_error_code(CryptoNote.error.BAD_TRANSACTION_EXTRA));
        //	  }

        //	  return Common.asString(binary);
        //	}

        //	public static List<string> collectDestinationAddresses(List<PaymentService.WalletRpcOrder> orders)
        //	{
        //	  List<string> result = new List<string>();

        //	  result.Capacity = orders.Count;
        //	  foreach (var order in orders)
        //	  {
        //		result.Add(order.address);
        //	  }

        //	  return result;
        //	}

        //	public static List<CryptoNote.WalletOrder> convertWalletRpcOrdersToWalletOrders(List<PaymentService.WalletRpcOrder> orders, string nodeAddress, uint nodeFee)
        //	{
        //	  List<CryptoNote.WalletOrder> result = new List<CryptoNote.WalletOrder>();

        //	  if (!string.IsNullOrEmpty(nodeAddress) && nodeFee != 0)
        //	  {
        //		result.Capacity = orders.Count + 1;
        //		result.emplace_back(new CryptoNote.WalletOrder ({nodeAddress, nodeFee}));
        //	  }
        //	  else
        //	  {
        //		result.Capacity = orders.Count;
        //	  }

        //	  foreach (var order in orders)
        //	  {
        //		result.emplace_back(new CryptoNote.WalletOrder ({order.address, order.amount}));
        //	  }

        //	  return result;
        //	}

        //	  public static WalletServiceConfiguration initConfiguration()
        //	  {
        //		WalletServiceConfiguration config = new WalletServiceConfiguration();

        //		config.daemonAddress = "127.0.0.1";
        //		config.bindAddress = "127.0.0.1";
        //		config.logFile = "service.log";
        //		config.daemonPort = CryptoNote.RPC_DEFAULT_PORT;
        //		config.bindPort = CryptoNote.SERVICE_DEFAULT_PORT;
        //		config.logLevel = Logging.INFO;
        //		config.legacySecurity = false;
        //		config.help = false;
        //		config.version = false;
        //		config.dumpConfig = false;
        //		config.generateNewContainer = false;
        //		config.daemonize = false;
        //		config.registerService = false;
        //		config.unregisterService = false;
        //		config.printAddresses = false;
        //		config.syncFromZero = false;

        //		return config;
        //	  }

        //	  public static void handleSettings(int argc, string[] argv, WalletServiceConfiguration config)
        //	  {
        //		cxxopts.Options options = new cxxopts.Options(argv[0], CryptoNote.getProjectCLIHeader());

        //		options.add_options("Core")("h,help", "Display this help message", cxxopts.value<bool>().implicit_value("true"))("v,version", "Output software version information", cxxopts.value<bool>().default_value("false").implicit_value("true"));

        //		options.add_options("Daemon")("daemon-address", "The daemon host to use for node operations",cxxopts.value<string>().default_value(config.daemonAddress), "<ip>")("daemon-port", "The daemon RPC port to use for node operations", cxxopts.value<int>().default_value(Convert.ToString(config.daemonPort)), "<port>");

        //		options.add_options("Service")("c,config", "Specify the configuration <file> to use instead of CLI arguments", cxxopts.value<string>(), "<file>")("dump-config", "Prints the current configuration to the screen", cxxopts.value<bool>().default_value("false").implicit_value("true"))("log-file", "Specify log <file> location", cxxopts.value<string>().default_value(config.logFile), "<file>")("log-level", "Specify log level", cxxopts.value<int>().default_value(Convert.ToString(config.logLevel)), "#")("server-root", "The service will use this <path> as the working directory", cxxopts.value<string>(), "<path>")("save-config", "Save the configuration to the specified <file>", cxxopts.value<string>(), "<file>");

        //		options.add_options("Wallet")("address", "Print the wallet addresses and then exit", cxxopts.value<bool>().default_value("false").implicit_value("true"))("w,container-file", "Wallet container <file>", cxxopts.value<string>(), "<file>")("p,container-password", "Wallet container <password>", cxxopts.value<string>(), "<password>")("g,generate-container", "Generate a new wallet container", cxxopts.value<bool>().default_value("false").implicit_value("true"))("view-key", "Generate a wallet container with this secret view <key>", cxxopts.value<string>(), "<key>")("spend-key", "Generate a wallet container with this secret spend <key>", cxxopts.value<string>(), "<key>")("mnemonic-seed", "Generate a wallet container with this Mnemonic <seed>", cxxopts.value<string>(), "<seed>")("scan-height", "Start scanning for transactions from this Blockchain height", cxxopts.value<ulong>().default_value("0"), "#")("SYNC_FROM_ZERO", "Force the wallet to sync from 0", cxxopts.value<bool>().default_value("false").implicit_value("true"));

        //		options.add_options("Network")("bind-address", "Interface IP address for the RPC service", cxxopts.value<string>().default_value(config.bindAddress), "<ip>")("bind-port", "TCP port for the RPC service", cxxopts.value<int>().default_value(Convert.ToString(config.bindPort)), "<port>");

        //		options.add_options("RPC")("enable-cors", "Adds header 'Access-Control-Allow-Origin' to the RPC responses. Uses the value specified as the domain. Use * for all.", cxxopts.value<string>(), "<domain>")("rpc-legacy-security", "Enable legacy mode (no password for RPC). WARNING: INSECURE. USE ONLY AS A LAST RESORT.", cxxopts.value<bool>().default_value("false").implicit_value("true"))("rpc-password", "Specify the <password> to access the RPC server.", cxxopts.value<string>(), "<password>");

        //	#if WIN32
        //		options.add_options("Windows Only")("daemonize", "Run the service as a daemon", cxxopts.value<bool>().default_value("false").implicit_value("true"))("register-service", "Registers this program as a Windows service",cxxopts.value<bool>().default_value("false").implicit_value("true"))("unregister-service", "Unregisters this program from being a Windows service", cxxopts.value<bool>().default_value("false").implicit_value("true"));
        //	#endif

        //		try
        //		{
        //		  var cli = options.parse(argc, argv);

        //		  if (cli.count("help") > 0)
        //		  {
        //			config.help = cli["help"].@as<bool>();
        //		  }

        //		  if (cli.count("version") > 0)
        //		  {
        //			config.version = cli["version"].@as<bool>();
        //		  }

        //		  if (cli.count("config") > 0)
        //		  {
        //			config.configFile = cli["config"].@as<string>();
        //		  }

        //		  if (cli.count("save-config") > 0)
        //		  {
        //			config.outputFile = cli["save-config"].@as<string>();
        //		  }

        //		  if (cli.count("dump-config") > 0)
        //		  {
        //			config.dumpConfig = cli["dump-config"].@as<bool>();
        //		  }

        //		  if (cli.count("daemon-address") > 0)
        //		  {
        //			config.daemonAddress = cli["daemon-address"].@as<string>();
        //		  }

        //		  if (cli.count("daemon-port") > 0)
        //		  {
        //			config.daemonPort = cli["daemon-port"].@as<int>();
        //		  }

        //		  if (cli.count("log-file") > 0)
        //		  {
        //			config.logFile = cli["log-file"].@as<string>();
        //		  }

        //		  if (cli.count("log-level") > 0)
        //		  {
        //			config.logLevel = cli["log-level"].@as<int>();
        //		  }

        //		  if (cli.count("container-file") > 0)
        //		  {
        //			config.containerFile = cli["container-file"].@as<string>();
        //		  }

        //		  if (cli.count("container-password") > 0)
        //		  {
        //			config.containerPassword = cli["container-password"].@as<string>();
        //		  }

        //		  if (cli.count("bind-address") > 0)
        //		  {
        //			config.bindAddress = cli["bind-address"].@as<string>();
        //		  }

        //		  if (cli.count("bind-port") > 0)
        //		  {
        //			config.bindPort = cli["bind-port"].@as<int>();
        //		  }

        //		  if (cli.count("enable-cors") > 0)
        //		  {
        //			config.corsHeader = cli["enable-cors"].@as<string>();
        //		  }

        //		  if (cli.count("rpc-legacy-security") > 0)
        //		  {
        //			config.legacySecurity = cli["rpc-legacy-security"].@as<bool>();
        //		  }

        //		  if (cli.count("rpc-password") > 0)
        //		  {
        //			config.rpcPassword = cli["rpc-password"].@as<string>();
        //		  }

        //		  if (cli.count("server-root") > 0)
        //		  {
        //			config.serverRoot = cli["server-root"].@as<string>();
        //		  }

        //		  if (cli.count("view-key") > 0)
        //		  {
        //			config.secretViewKey = cli["view-key"].@as<string>();
        //		  }

        //		  if (cli.count("spend-key") > 0)
        //		  {
        //			config.secretSpendKey = cli["spend-key"].@as<string>();
        //		  }

        //		  if (cli.count("mnemonic-seed") > 0)
        //		  {
        //			config.mnemonicSeed = cli["mnemonic-seed"].@as<string>();
        //		  }

        //		  if (cli.count("generate-container") > 0)
        //		  {
        //			config.generateNewContainer = cli["generate-container"].@as<bool>();
        //		  }

        //		  if (cli.count("daemonize") > 0)
        //		  {
        //			config.daemonize = cli["daemonize"].@as<bool>();
        //		  }

        //		  if (cli.count("register-service") > 0)
        //		  {
        //			config.registerService = cli["register-service"].@as<bool>();
        //		  }

        //		  if (cli.count("unregister-service") > 0)
        //		  {
        //			config.unregisterService = cli["unregister-service"].@as<bool>();
        //		  }

        //		  if (cli.count("address") > 0)
        //		  {
        //			config.printAddresses = cli["address"].@as<bool>();
        //		  }

        //		  if (cli.count("SYNC_FROM_ZERO") > 0)
        //		  {
        //			config.syncFromZero = cli["SYNC_FROM_ZERO"].@as<bool>();
        //		  }

        //		  if (cli.count("scan-height") > 0)
        //		  {
        //			config.scanHeight = cli["scan-height"].@as<ulong>();
        //		  }

        //		  if (config.help) // Do we want to display the help message?
        //		  {
        //			Console.Write(options.help({}));
        //			Console.Write("\n");
        //			Environment.Exit(0);
        //		  }
        //		  else if (config.version) // Do we want to display the software version?
        //		  {
        //			Console.Write(CryptoNote.getProjectCLIHeader());
        //			Console.Write("\n");
        //			Environment.Exit(0);
        //		  }
        //		}
        //		catch (cxxopts.OptionException e)
        //		{
        //		  Console.Write("Error: Unable to parse command line argument options: ");
        //		  Console.Write(e.what());
        //		  Console.Write("\n");
        //		  Console.Write("\n");
        //		  Console.Write(options.help({}));
        //		  Console.Write("\n");
        //		  Environment.Exit(1);
        //		}
        //	  }

        //	   public static void handleIniConfig(std::ifstream data, WalletServiceConfiguration config)
        //	   {
        //		// find key=value pair, respect whitespace before/after "="
        //		// g0: full match, g1: match key, g2: match value
        //		std::regex cfgItem = new std::regex(@"\s*(\S[^ \t=]*)\s*=\s*((\s?\S+)+)\s*$");

        //		// comments, first non space starts with # or ;
        //		std::regex cfgComment = new std::regex(@"\s*[;#]");
        //		std::smatch item = new std::smatch();
        //		string cfgKey;
        //		string cfgValue;

        //		for (string line; getline(data, line);)
        //		{
        //		  if (line.empty() || std::regex_match(line, item, cfgComment))
        //		  {
        //			continue;
        //		  }
        //		  else if (std::regex_match(line, item, cfgItem))
        //		  {
        //			if (item.size() == 4)
        //			{
        //			  cfgKey = item[1].str();
        //			  cfgValue = item[2].str();

        //			  if (cfgKey.CompareTo("daemon-address") == 0)
        //			  {
        //				config.daemonAddress = cfgValue;
        //			  }
        //			  else if (cfgKey.CompareTo("daemon-port") == 0)
        //			  {
        //				config.daemonPort = Convert.ToInt32(cfgValue);
        //			  }
        //			  else if (cfgKey.CompareTo("log-file") == 0)
        //			  {
        //				config.logFile = cfgValue;
        //			  }
        //			  else if (cfgKey.CompareTo("log-level") == 0)
        //			  {
        //				config.logLevel = Convert.ToInt32(cfgValue);
        //			  }
        //			  else if (cfgKey.CompareTo("container-file") == 0)
        //			  {
        //				config.containerFile = cfgValue;
        //			  }
        //			  else if (cfgKey.CompareTo("container-password") == 0)
        //			  {
        //				config.containerPassword = cfgValue;
        //			  }
        //			  else if (cfgKey.CompareTo("bind-address") == 0)
        //			  {
        //				config.bindAddress = cfgValue;
        //			  }
        //			  else if (cfgKey.CompareTo("bind-port") == 0)
        //			  {
        //				config.bindPort = Convert.ToInt32(cfgValue);
        //			  }
        //			  else if (cfgKey.CompareTo("enable-cors") == 0)
        //			  {
        //				config.corsHeader = cfgValue;
        //			  }
        //			  else if (cfgKey.CompareTo("rpc-legacy-security") == 0)
        //			  {
        //				config.legacySecurity = cfgValue[0] == '1' ? true : false;
        //			  }
        //			  else if (cfgKey.CompareTo("rpc-password") == 0)
        //			  {
        //				config.rpcPassword = cfgValue;
        //			  }
        //			  else if (cfgKey.CompareTo("server-root") == 0)
        //			  {
        //				config.serverRoot = cfgValue;
        //			  }
        //			  else
        //			  {
        //				throw new System.Exception("One or more options in your config file was invalid!");
        //			  }
        //			}
        //		  }
        //		}
        //	   }

        //	  public static void handleSettings(string configFile, WalletServiceConfiguration config)
        //	  {
        //		std::ifstream data = new std::ifstream(configFile);

        //		if (!data.good())
        //		{
        //		  throw new System.Exception("The --config-file you specified does not exist, please check the filename and try again.");
        //		}

        //		try
        //		{
        //		  json j = new json();
        //		  data >> j;

        //		  if (j.find("daemon-address") != j.end())
        //		  {
        //			config.daemonAddress = j["daemon-address"].get<string>();
        //		  }

        //		  if (j.find("daemon-port") != j.end())
        //		  {
        //			config.daemonPort = j["daemon-port"].get<int>();
        //		  }

        //		  if (j.find("log-file") != j.end())
        //		  {
        //			config.logFile = j["log-file"].get<string>();
        //		  }

        //		  if (j.find("log-level") != j.end())
        //		  {
        //			config.logLevel = j["log-level"].get<int>();
        //		  }

        //		  if (j.find("container-file") != j.end())
        //		  {
        //			config.containerFile = j["container-file"].get<string>();
        //		  }

        //		  if (j.find("container-password") != j.end())
        //		  {
        //			config.containerPassword = j["container-password"].get<string>();
        //		  }

        //		  if (j.find("bind-address") != j.end())
        //		  {
        //			config.bindAddress = j["bind-address"].get<string>();
        //		  }

        //		  if (j.find("bind-port") != j.end())
        //		  {
        //			config.bindPort = j["bind-port"].get<int>();
        //		  }

        //		  if (j.find("enable-cors") != j.end())
        //		  {
        //			config.corsHeader = j["enable-cors"].get<string>();
        //		  }

        //		  if (j.find("rpc-legacy-security") != j.end())
        //		  {
        //			config.legacySecurity = j["rpc-legacy-security"].get<bool>();
        //		  }

        //		  if (j.find("rpc-password") != j.end())
        //		  {
        //			config.rpcPassword = j["rpc-password"].get<string>();
        //		  }

        //		  if (j.find("server-root") != j.end())
        //		  {
        //			config.serverRoot = j["server-root"].get<string>();
        //		  }
        //		}
        //		catch (System.Exception e)
        //		{
        //			// when failed reading as json, try reading it as old flat ini
        //			// clear eof + fail bits
        //			data.clear();
        //			// reread from start
        //			data.seekg(0, std::ios.beg);
        //			handleIniConfig(data, config);
        //		}
        //	  }

        //	  public static json asJSON(WalletServiceConfiguration config)
        //	  {
        //		json j = new json(
        //		{
        //			{"daemon-address", config.daemonAddress},
        //			{"daemon-port", config.daemonPort},
        //			{"log-file", config.logFile},
        //			{"log-level", config.logLevel},
        //			{"container-file", config.containerFile},
        //			{"container-password", config.containerPassword},
        //			{"bind-address", config.bindAddress},
        //			{"bind-port", config.bindPort},
        //			{"enable-cors", config.corsHeader},
        //			{"rpc-legacy-security", config.legacySecurity},
        //			{"rpc-password", config.rpcPassword},
        //			{"server-root", config.serverRoot}
        //		});

        //		return j;
        //	  }

        //	  public static string asString(WalletServiceConfiguration config)
        //	  {
        //		json j = asJSON(config);
        //		return j.dump(2);
        //	  }

        //	  public static void asFile(WalletServiceConfiguration config, string filename)
        //	  {
        //		json j = asJSON(config);
        //		std::ofstream data = new std::ofstream(filename);
        //		data << std::setw(2) << j << std::endl;
        //	  }
        //	}
        //}

        //namespace linenoise.ansi
        //{
        //	public static class GlobalMembers
        //	{
        //	//C++ TO C# CONVERTER TODO TASK: Typedefs defined in multiple preprocessor conditionals can only be replaced within the scope of the preprocessor conditional:
        //	//typedef GRM *PGRM;
        //	public static readonly char SO = '\x0E'; // Shift Out
        //	public static readonly char SI = '\x0F'; // Shift In

        //	public static readonly int MAX_ARG = 16; // max number of args in an escape sequence
        //	public static int state; // automata state
        //	public static char prefix; // escape sequence prefix ( '[', ']' or '(' );
        //	public static char prefix2; // secondary prefix ( '?' or '>' );
        //	public static char suffix; // escape sequence suffix
        //	public static int es_argc; // escape sequence args count
        //	public static int[] es_argv = new int[MAX_ARG]; // escape sequence args
        //	public static string Pt_arg = new string(new char[MAX_PATH * 2]); // text parameter for Operating System Command
        //	public static int Pt_len;
        //	public static int shifted;


        //	// DEC Special Graphics Character Set from
        //	// http://vt100.net/docs/vt220-rm/table2-4.html
        //	// Some of these may not look right, depending on the font and code page (in
        //	// particular, the Control Pictures probably won't work at all).
        //	public static readonly char[] G1 = {' ', '\x2666', '\x2592', '\x2409', '\x240c', '\x240d', '\x240a', '\x00b0', '\x00b1', '\x2424', '\x240b', '\x2518', '\x2510', '\x250c', '\x2514', '\x253c', '\x00af', '\x25ac', '\x2500', '_', '_', '\x251c', '\x2524', '\x2534', '\x252c', '\x2502', '\x2264', '\x2265', '\x03c0', '\x2260', '\x00a3', '\x00b7'};



        //	// color constants

        //	//C++ TO C# CONVERTER TODO TASK: #define macros defined in multiple preprocessor conditionals can only be replaced within the scope of the preprocessor conditional:
        //	//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
        //	//ORIGINAL LINE: #define FOREGROUND_WHITE FOREGROUND_RED|FOREGROUND_GREEN|FOREGROUND_BLUE

        //	//C++ TO C# CONVERTER TODO TASK: #define macros defined in multiple preprocessor conditionals can only be replaced within the scope of the preprocessor conditional:
        //	//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
        //	//ORIGINAL LINE: #define BACKGROUND_WHITE BACKGROUND_RED|BACKGROUND_GREEN|BACKGROUND_BLUE

        //	public static readonly byte[] foregroundcolor = {DefineConstants.FOREGROUND_BLACK, FOREGROUND_RED, FOREGROUND_GREEN, FOREGROUND_RED | FOREGROUND_GREEN, FOREGROUND_BLUE, FOREGROUND_BLUE | FOREGROUND_RED, FOREGROUND_BLUE | FOREGROUND_GREEN, FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE};

        //	public static readonly byte[] backgroundcolor = {DefineConstants.BACKGROUND_BLACK, BACKGROUND_RED, BACKGROUND_GREEN, BACKGROUND_RED | BACKGROUND_GREEN, BACKGROUND_BLUE, BACKGROUND_BLUE | BACKGROUND_RED, BACKGROUND_BLUE | BACKGROUND_GREEN, BACKGROUND_RED | BACKGROUND_GREEN | BACKGROUND_BLUE};

        //	public static readonly byte[] attr2ansi = {0, 4, 2, 6, 1, 5, 3, 7};

        //	public static GRM grm = new GRM();

        //	// saved cursor position
        //	public static COORD SavePos = new COORD();

        //	// ========== Print Buffer functions


        //	public static int nCharInBuffer;
        //	public static string ChBuffer = new string(new char[DefineConstants.BUFFER_SIZE]);

        //	//-----------------------------------------------------------------------------
        //	//   FlushBuffer()
        //	// Writes the buffer to the console and empties it.
        //	//-----------------------------------------------------------------------------

        //	public static void FlushBuffer()
        //	{
        //		uint nWritten;
        //		if (GlobalMembers.nCharInBuffer <= 0)
        //		{
        //			return;
        //		}
        //		WriteConsoleW(hConOut, GlobalMembers.ChBuffer, GlobalMembers.nCharInBuffer, nWritten, null);
        //		GlobalMembers.nCharInBuffer = 0;
        //	}

        //	//-----------------------------------------------------------------------------
        //	//   PushBuffer( WCHAR c )
        //	// Adds a character in the buffer.
        //	//-----------------------------------------------------------------------------

        //	public static void PushBuffer(char c)
        //	{
        //		if (GlobalMembers.shifted != 0 && c >= DefineConstants.FIRST_G1 && c <= DefineConstants.LAST_G1)
        //		{
        //			c = GlobalMembers.G1[c - DefineConstants.FIRST_G1];
        //		}
        //		GlobalMembers.ChBuffer = StringFunctions.ChangeCharacter(GlobalMembers.ChBuffer, GlobalMembers.nCharInBuffer, c);
        //		if (++GlobalMembers.nCharInBuffer == DefineConstants.BUFFER_SIZE)
        //		{
        //			ansi.GlobalMembers.FlushBuffer();
        //		}
        //	}

        //	//-----------------------------------------------------------------------------
        //	//   SendSequence( LPCWSTR seq )
        //	// Send the string to the input buffer.
        //	//-----------------------------------------------------------------------------

        //	public static void SendSequence(string seq)
        //	{
        //		uint @out;
        //		INPUT_RECORD in = new INPUT_RECORD();
        //		IntPtr hStdIn = GetStdHandle(STD_INPUT_HANDLE);

        //		in.EventType = KEY_EVENT;
        //		in.Event.KeyEvent.bKeyDown = 1;
        //		in.Event.KeyEvent.wRepeatCount = 1;
        //		in.Event.KeyEvent.wVirtualKeyCode = 0;
        //		in.Event.KeyEvent.wVirtualScanCode = 0;
        //		in.Event.KeyEvent.dwControlKeyState = 0;
        //		for (; seq != null; ++seq)
        //		{
        //			in.Event.KeyEvent.uChar.UnicodeChar = seq;
        //			WriteConsoleInput(hStdIn, in, 1, @out);
        //		}
        //	}

        //	// ========== Print functions

        //	//-----------------------------------------------------------------------------
        //	//   InterpretEscSeq()
        //	// Interprets the last escape sequence scanned by ParseAndPrintANSIString
        //	//   prefix             escape sequence prefix
        //	//   es_argc            escape sequence args count
        //	//   es_argv[]          escape sequence args array
        //	//   suffix             escape sequence suffix
        //	//
        //	// for instance, with \e[33;45;1m we have
        //	// prefix = '[',
        //	// es_argc = 3, es_argv[0] = 33, es_argv[1] = 45, es_argv[2] = 1
        //	// suffix = 'm'
        //	//-----------------------------------------------------------------------------

        //	public static void InterpretEscSeq()
        //	{
        //		int i;
        //		ushort attribut;
        //		CONSOLE_SCREEN_BUFFER_INFO Info = new CONSOLE_SCREEN_BUFFER_INFO();
        //		CONSOLE_CURSOR_INFO CursInfo = new CONSOLE_CURSOR_INFO();
        //		uint len;
        //		uint NumberOfCharsWritten;
        //		COORD Pos = new COORD();
        //		SMALL_RECT Rect = new SMALL_RECT();
        //		CHAR_INFO CharInfo = new CHAR_INFO();

        //		if (GlobalMembers.prefix == '[')
        //		{
        //			if (GlobalMembers.prefix2 == '?' && (GlobalMembers.suffix == 'h' || GlobalMembers.suffix == 'l'))
        //			{
        //				if (GlobalMembers.es_argc == 1 && GlobalMembers.es_argv[0] == 25)
        //				{
        //					GetConsoleCursorInfo(hConOut, CursInfo);
        //					CursInfo.bVisible = (GlobalMembers.suffix == 'h');
        //					SetConsoleCursorInfo(hConOut, CursInfo);
        //					return;
        //				}
        //			}
        //			// Ignore any other \e[? or \e[> sequences.
        //			if (GlobalMembers.prefix2 != 0)
        //			{
        //				return;
        //			}

        //			GetConsoleScreenBufferInfo(hConOut, Info);
        //			switch (GlobalMembers.suffix)
        //			{
        //					case 'm':
        //						if (GlobalMembers.es_argc == 0)
        //						{
        //							GlobalMembers.es_argv[GlobalMembers.es_argc++] = 0;
        //						}
        //						for (i = 0; i < GlobalMembers.es_argc; i++)
        //						{
        //							if (30 <= GlobalMembers.es_argv[i] != 0 && GlobalMembers.es_argv[i] <= 37)
        //							{
        //								GlobalMembers.grm.foreground = GlobalMembers.es_argv[i] - 30;
        //							}
        //							else if (40 <= GlobalMembers.es_argv[i] != 0 && GlobalMembers.es_argv[i] <= 47)
        //							{
        //								GlobalMembers.grm.background = GlobalMembers.es_argv[i] - 40;
        //							}
        //							else
        //							{
        //								switch (GlobalMembers.es_argv[i])
        //								{
        //									case 0:
        //									case 39:
        //									case 49:
        //									{
        //											string def = new string(new char[4]);
        //											int a;
        //											*def = '7';
        //											def = StringFunctions.ChangeCharacter(def, 1, '\0');
        ////C++ TO C# CONVERTER WARNING: This 'sizeof' ratio was replaced with a direct reference to the array length:
        ////ORIGINAL LINE: GetEnvironmentVariableW("ANSICON_DEF", def, (sizeof(def)/sizeof(*(def))));
        //											GetEnvironmentVariableW("ANSICON_DEF", def, (def.Length));
        //											a = wcstol(def, null, 16);
        //											GlobalMembers.grm.reverse = 0;
        //											if (a < 0)
        //											{
        //												GlobalMembers.grm.reverse = 1;
        //												a = -a;
        //											}
        //											if (GlobalMembers.es_argv[i] != 49)
        //											{
        //												GlobalMembers.grm.foreground = GlobalMembers.attr2ansi[a & 7];
        //											}
        //											if (GlobalMembers.es_argv[i] != 39)
        //											{
        //												GlobalMembers.grm.background = GlobalMembers.attr2ansi[(a >> 4) & 7];
        //											}
        //											if (GlobalMembers.es_argv[i] == 0)
        //											{
        //												if (GlobalMembers.es_argc == 1)
        //												{
        //													GlobalMembers.grm.bold = a & FOREGROUND_INTENSITY;
        //													GlobalMembers.grm.underline = a & BACKGROUND_INTENSITY;
        //												}
        //												else
        //												{
        //													GlobalMembers.grm.bold = 0;
        //													GlobalMembers.grm.underline = 0;
        //												}
        //												GlobalMembers.grm.rvideo = 0;
        //												GlobalMembers.grm.concealed = 0;
        //											}
        //									}
        //											break;

        //									case 1:
        //										GlobalMembers.grm.bold = FOREGROUND_INTENSITY;
        //										break;
        //									case 5: // blink
        //									case 4:
        //										GlobalMembers.grm.underline = BACKGROUND_INTENSITY;
        //										break;
        //									case 7:
        //										GlobalMembers.grm.rvideo = 1;
        //										break;
        //									case 8:
        //										GlobalMembers.grm.concealed = 1;
        //										break;
        //									case 21: // oops, this actually turns on double underline
        //									case 22:
        //										GlobalMembers.grm.bold = 0;
        //										break;
        //									case 25:
        //									case 24:
        //										GlobalMembers.grm.underline = 0;
        //										break;
        //									case 27:
        //										GlobalMembers.grm.rvideo = 0;
        //										break;
        //									case 28:
        //										GlobalMembers.grm.concealed = 0;
        //										break;
        //								}
        //							}
        //						}
        //						if (GlobalMembers.grm.concealed != 0)
        //						{
        //							if (GlobalMembers.grm.rvideo != 0)
        //							{
        //								attribut = GlobalMembers.foregroundcolor[GlobalMembers.grm.foreground] | GlobalMembers.backgroundcolor[GlobalMembers.grm.foreground];
        //								if (GlobalMembers.grm.bold != 0)
        //								{
        //									attribut |= FOREGROUND_INTENSITY | BACKGROUND_INTENSITY;
        //								}
        //							}
        //							else
        //							{
        //								attribut = GlobalMembers.foregroundcolor[GlobalMembers.grm.background] | GlobalMembers.backgroundcolor[GlobalMembers.grm.background];
        //								if (GlobalMembers.grm.underline != 0)
        //								{
        //									attribut |= FOREGROUND_INTENSITY | BACKGROUND_INTENSITY;
        //								}
        //							}
        //						}
        //						else if (GlobalMembers.grm.rvideo)
        //						{
        //							attribut = GlobalMembers.foregroundcolor[GlobalMembers.grm.background] | GlobalMembers.backgroundcolor[GlobalMembers.grm.foreground];
        //							if (GlobalMembers.grm.bold != 0)
        //							{
        //								attribut |= BACKGROUND_INTENSITY;
        //							}
        //							if (GlobalMembers.grm.underline != 0)
        //							{
        //								attribut |= FOREGROUND_INTENSITY;
        //							}
        //						}
        //						else
        //						{
        //							attribut = GlobalMembers.foregroundcolor[GlobalMembers.grm.foreground] | GlobalMembers.grm.bold | GlobalMembers.backgroundcolor[GlobalMembers.grm.background] | GlobalMembers.grm.underline;
        //						}
        //						if (GlobalMembers.grm.reverse != 0)
        //						{
        //							attribut = ((attribut >> 4) & 15) | ((attribut & 15) << 4);
        //						}
        //						SetConsoleTextAttribute(hConOut, attribut);
        //						return;

        //					case 'J':
        //						if (GlobalMembers.es_argc == 0)
        //						{
        //							GlobalMembers.es_argv[GlobalMembers.es_argc++] = 0; // ESC[J == ESC[0J
        //						}
        //						if (GlobalMembers.es_argc != 1)
        //						{
        //							return;
        //						}
        //						switch (GlobalMembers.es_argv[0])
        //						{
        //								case 0: // ESC[0J erase from cursor to end of display
        //									len = (Info.dwSize.Y - Info.dwCursorPosition.Y - 1) * Info.dwSize.X + Info.dwSize.X - Info.dwCursorPosition.X - 1;
        //									FillConsoleOutputCharacter(hConOut, ' ', len, Info.dwCursorPosition, NumberOfCharsWritten);
        //									FillConsoleOutputAttribute(hConOut, Info.wAttributes, len, Info.dwCursorPosition, NumberOfCharsWritten);
        //									return;

        //								case 1: // ESC[1J erase from start to cursor.
        //									Pos.X = 0;
        //									Pos.Y = 0;
        //									len = Info.dwCursorPosition.Y * Info.dwSize.X + Info.dwCursorPosition.X + 1;
        //									FillConsoleOutputCharacter(hConOut, ' ', len, Pos, NumberOfCharsWritten);
        //									FillConsoleOutputAttribute(hConOut, Info.wAttributes, len, Pos, NumberOfCharsWritten);
        //									return;

        //								case 2: // ESC[2J Clear screen and home cursor
        //									Pos.X = 0;
        //									Pos.Y = 0;
        //									len = Info.dwSize.X * Info.dwSize.Y;
        //									FillConsoleOutputCharacter(hConOut, ' ', len, Pos, NumberOfCharsWritten);
        //									FillConsoleOutputAttribute(hConOut, Info.wAttributes, len, Pos, NumberOfCharsWritten);
        //									SetConsoleCursorPosition(hConOut, Pos);
        //									return;

        //								default:
        //									return;
        //						}

        //					case 'K':
        //						if (GlobalMembers.es_argc == 0)
        //						{
        //							GlobalMembers.es_argv[GlobalMembers.es_argc++] = 0; // ESC[K == ESC[0K
        //						}
        //						if (GlobalMembers.es_argc != 1)
        //						{
        //							return;
        //						}
        //						switch (GlobalMembers.es_argv[0])
        //						{
        //								case 0: // ESC[0K Clear to end of line
        //									len = Info.dwSize.X - Info.dwCursorPosition.X + 1;
        //									FillConsoleOutputCharacter(hConOut, ' ', len, Info.dwCursorPosition, NumberOfCharsWritten);
        //									FillConsoleOutputAttribute(hConOut, Info.wAttributes, len, Info.dwCursorPosition, NumberOfCharsWritten);
        //									return;

        //								case 1: // ESC[1K Clear from start of line to cursor
        //									Pos.X = 0;
        //									Pos.Y = Info.dwCursorPosition.Y;
        //									FillConsoleOutputCharacter(hConOut, ' ', Info.dwCursorPosition.X + 1, Pos, NumberOfCharsWritten);
        //									FillConsoleOutputAttribute(hConOut, Info.wAttributes, Info.dwCursorPosition.X + 1, Pos, NumberOfCharsWritten);
        //									return;

        //								case 2: // ESC[2K Clear whole line.
        //									Pos.X = 0;
        //									Pos.Y = Info.dwCursorPosition.Y;
        //									FillConsoleOutputCharacter(hConOut, ' ', Info.dwSize.X, Pos, NumberOfCharsWritten);
        //									FillConsoleOutputAttribute(hConOut, Info.wAttributes, Info.dwSize.X, Pos, NumberOfCharsWritten);
        //									return;

        //								default:
        //									return;
        //						}

        //					case 'X': // ESC[#X Erase # characters.
        //						if (GlobalMembers.es_argc == 0)
        //						{
        //							GlobalMembers.es_argv[GlobalMembers.es_argc++] = 1; // ESC[X == ESC[1X
        //						}
        //						if (GlobalMembers.es_argc != 1)
        //						{
        //							return;
        //						}
        //						FillConsoleOutputCharacter(hConOut, ' ', GlobalMembers.es_argv[0], Info.dwCursorPosition, NumberOfCharsWritten);
        //						FillConsoleOutputAttribute(hConOut, Info.wAttributes, GlobalMembers.es_argv[0], Info.dwCursorPosition, NumberOfCharsWritten);
        //						return;

        //					case 'L': // ESC[#L Insert # blank lines.
        //						if (GlobalMembers.es_argc == 0)
        //						{
        //							GlobalMembers.es_argv[GlobalMembers.es_argc++] = 1; // ESC[L == ESC[1L
        //						}
        //						if (GlobalMembers.es_argc != 1)
        //						{
        //							return;
        //						}
        //						Rect.Left = 0;
        //						Rect.Top = Info.dwCursorPosition.Y;
        //						Rect.Right = Info.dwSize.X - 1;
        //						Rect.Bottom = Info.dwSize.Y - 1;
        //						Pos.X = 0;
        //						Pos.Y = Info.dwCursorPosition.Y + GlobalMembers.es_argv[0];
        //						CharInfo.Char.UnicodeChar = ' ';
        //						CharInfo.Attributes = Info.wAttributes;
        //						ScrollConsoleScreenBuffer(hConOut, Rect, null, Pos, CharInfo);
        //						return;

        //					case 'M': // ESC[#M Delete # lines.
        //						if (GlobalMembers.es_argc == 0)
        //						{
        //							GlobalMembers.es_argv[GlobalMembers.es_argc++] = 1; // ESC[M == ESC[1M
        //						}
        //						if (GlobalMembers.es_argc != 1)
        //						{
        //							return;
        //						}
        //						if (GlobalMembers.es_argv[0] > Info.dwSize.Y - Info.dwCursorPosition.Y)
        //						{
        //							GlobalMembers.es_argv[0] = Info.dwSize.Y - Info.dwCursorPosition.Y;
        //						}
        //						Rect.Left = 0;
        //						Rect.Top = Info.dwCursorPosition.Y + GlobalMembers.es_argv[0];
        //						Rect.Right = Info.dwSize.X - 1;
        //						Rect.Bottom = Info.dwSize.Y - 1;
        //						Pos.X = 0;
        //						Pos.Y = Info.dwCursorPosition.Y;
        //						CharInfo.Char.UnicodeChar = ' ';
        //						CharInfo.Attributes = Info.wAttributes;
        //						ScrollConsoleScreenBuffer(hConOut, Rect, null, Pos, CharInfo);
        //						return;

        //					case 'P': // ESC[#P Delete # characters.
        //						if (GlobalMembers.es_argc == 0)
        //						{
        //							GlobalMembers.es_argv[GlobalMembers.es_argc++] = 1; // ESC[P == ESC[1P
        //						}
        //						if (GlobalMembers.es_argc != 1)
        //						{
        //							return;
        //						}
        //						if (Info.dwCursorPosition.X + GlobalMembers.es_argv[0] > Info.dwSize.X - 1)
        //						{
        //							GlobalMembers.es_argv[0] = Info.dwSize.X - Info.dwCursorPosition.X;
        //						}
        //						Rect.Left = Info.dwCursorPosition.X + GlobalMembers.es_argv[0];
        //						Rect.Top = Info.dwCursorPosition.Y;
        //						Rect.Right = Info.dwSize.X - 1;
        //						Rect.Bottom = Info.dwCursorPosition.Y;
        //						CharInfo.Char.UnicodeChar = ' ';
        //						CharInfo.Attributes = Info.wAttributes;
        //						ScrollConsoleScreenBuffer(hConOut, Rect, null, Info.dwCursorPosition, CharInfo);
        //						return;

        //					case '@': // ESC[#@ Insert # blank characters.
        //						if (GlobalMembers.es_argc == 0)
        //						{
        //							GlobalMembers.es_argv[GlobalMembers.es_argc++] = 1; // ESC[@ == ESC[1@
        //						}
        //						if (GlobalMembers.es_argc != 1)
        //						{
        //							return;
        //						}
        //						if (Info.dwCursorPosition.X + GlobalMembers.es_argv[0] > Info.dwSize.X - 1)
        //						{
        //							GlobalMembers.es_argv[0] = Info.dwSize.X - Info.dwCursorPosition.X;
        //						}
        //						Rect.Left = Info.dwCursorPosition.X;
        //						Rect.Top = Info.dwCursorPosition.Y;
        //						Rect.Right = Info.dwSize.X - 1 - GlobalMembers.es_argv[0];
        //						Rect.Bottom = Info.dwCursorPosition.Y;
        //						Pos.X = Info.dwCursorPosition.X + GlobalMembers.es_argv[0];
        //						Pos.Y = Info.dwCursorPosition.Y;
        //						CharInfo.Char.UnicodeChar = ' ';
        //						CharInfo.Attributes = Info.wAttributes;
        //						ScrollConsoleScreenBuffer(hConOut, Rect, null, Pos, CharInfo);
        //						return;

        //					case 'k': // ESC[#k
        //					case 'A': // ESC[#A Moves cursor up # lines
        //						if (GlobalMembers.es_argc == 0)
        //						{
        //							GlobalMembers.es_argv[GlobalMembers.es_argc++] = 1; // ESC[A == ESC[1A
        //						}
        //						if (GlobalMembers.es_argc != 1)
        //						{
        //							return;
        //						}
        //						Pos.Y = Info.dwCursorPosition.Y - GlobalMembers.es_argv[0];
        //						if (Pos.Y < 0)
        //						{
        //							Pos.Y = 0;
        //						}
        //						Pos.X = Info.dwCursorPosition.X;
        //						SetConsoleCursorPosition(hConOut, Pos);
        //						return;

        //					case 'e': // ESC[#e
        //					case 'B': // ESC[#B Moves cursor down # lines
        //						if (GlobalMembers.es_argc == 0)
        //						{
        //							GlobalMembers.es_argv[GlobalMembers.es_argc++] = 1; // ESC[B == ESC[1B
        //						}
        //						if (GlobalMembers.es_argc != 1)
        //						{
        //							return;
        //						}
        //						Pos.Y = Info.dwCursorPosition.Y + GlobalMembers.es_argv[0];
        //						if (Pos.Y >= Info.dwSize.Y)
        //						{
        //							Pos.Y = Info.dwSize.Y - 1;
        //						}
        //						Pos.X = Info.dwCursorPosition.X;
        //						SetConsoleCursorPosition(hConOut, Pos);
        //						return;

        //					case 'a': // ESC[#a
        //					case 'C': // ESC[#C Moves cursor forward # spaces
        //						if (GlobalMembers.es_argc == 0)
        //						{
        //							GlobalMembers.es_argv[GlobalMembers.es_argc++] = 1; // ESC[C == ESC[1C
        //						}
        //						if (GlobalMembers.es_argc != 1)
        //						{
        //							return;
        //						}
        //						Pos.X = Info.dwCursorPosition.X + GlobalMembers.es_argv[0];
        //						if (Pos.X >= Info.dwSize.X)
        //						{
        //							Pos.X = Info.dwSize.X - 1;
        //						}
        //						Pos.Y = Info.dwCursorPosition.Y;
        //						SetConsoleCursorPosition(hConOut, Pos);
        //						return;

        //					case 'j': // ESC[#j
        //					case 'D': // ESC[#D Moves cursor back # spaces
        //						if (GlobalMembers.es_argc == 0)
        //						{
        //							GlobalMembers.es_argv[GlobalMembers.es_argc++] = 1; // ESC[D == ESC[1D
        //						}
        //						if (GlobalMembers.es_argc != 1)
        //						{
        //							return;
        //						}
        //						Pos.X = Info.dwCursorPosition.X - GlobalMembers.es_argv[0];
        //						if (Pos.X < 0)
        //						{
        //							Pos.X = 0;
        //						}
        //						Pos.Y = Info.dwCursorPosition.Y;
        //						SetConsoleCursorPosition(hConOut, Pos);
        //						return;

        //					case 'E': // ESC[#E Moves cursor down # lines, column 1.
        //						if (GlobalMembers.es_argc == 0)
        //						{
        //							GlobalMembers.es_argv[GlobalMembers.es_argc++] = 1; // ESC[E == ESC[1E
        //						}
        //						if (GlobalMembers.es_argc != 1)
        //						{
        //							return;
        //						}
        //						Pos.Y = Info.dwCursorPosition.Y + GlobalMembers.es_argv[0];
        //						if (Pos.Y >= Info.dwSize.Y)
        //						{
        //							Pos.Y = Info.dwSize.Y - 1;
        //						}
        //						Pos.X = 0;
        //						SetConsoleCursorPosition(hConOut, Pos);
        //						return;

        //					case 'F': // ESC[#F Moves cursor up # lines, column 1.
        //						if (GlobalMembers.es_argc == 0)
        //						{
        //							GlobalMembers.es_argv[GlobalMembers.es_argc++] = 1; // ESC[F == ESC[1F
        //						}
        //						if (GlobalMembers.es_argc != 1)
        //						{
        //							return;
        //						}
        //						Pos.Y = Info.dwCursorPosition.Y - GlobalMembers.es_argv[0];
        //						if (Pos.Y < 0)
        //						{
        //							Pos.Y = 0;
        //						}
        //						Pos.X = 0;
        //						SetConsoleCursorPosition(hConOut, Pos);
        //						return;

        //					case '`': // ESC[#`
        //					case 'G': // ESC[#G Moves cursor column # in current row.
        //						if (GlobalMembers.es_argc == 0)
        //						{
        //							GlobalMembers.es_argv[GlobalMembers.es_argc++] = 1; // ESC[G == ESC[1G
        //						}
        //						if (GlobalMembers.es_argc != 1)
        //						{
        //							return;
        //						}
        //						Pos.X = GlobalMembers.es_argv[0] - 1;
        //						if (Pos.X >= Info.dwSize.X)
        //						{
        //							Pos.X = Info.dwSize.X - 1;
        //						}
        //						if (Pos.X < 0)
        //						{
        //							Pos.X = 0;
        //						}
        //						Pos.Y = Info.dwCursorPosition.Y;
        //						SetConsoleCursorPosition(hConOut, Pos);
        //						return;

        //					case 'd': // ESC[#d Moves cursor row #, current column.
        //						if (GlobalMembers.es_argc == 0)
        //						{
        //							GlobalMembers.es_argv[GlobalMembers.es_argc++] = 1; // ESC[d == ESC[1d
        //						}
        //						if (GlobalMembers.es_argc != 1)
        //						{
        //							return;
        //						}
        //						Pos.Y = GlobalMembers.es_argv[0] - 1;
        //						if (Pos.Y < 0)
        //						{
        //							Pos.Y = 0;
        //						}
        //						if (Pos.Y >= Info.dwSize.Y)
        //						{
        //							Pos.Y = Info.dwSize.Y - 1;
        //						}
        //						SetConsoleCursorPosition(hConOut, Pos);
        //						return;

        //					case 'f': // ESC[#;#f
        //					case 'H': // ESC[#;#H Moves cursor to line #, column #
        //						if (GlobalMembers.es_argc == 0)
        //						{
        //							GlobalMembers.es_argv[GlobalMembers.es_argc++] = 1; // ESC[H == ESC[1;1H
        //						}
        //						if (GlobalMembers.es_argc == 1)
        //						{
        //							GlobalMembers.es_argv[GlobalMembers.es_argc++] = 1; // ESC[#H == ESC[#;1H
        //						}
        //						if (GlobalMembers.es_argc > 2)
        //						{
        //							return;
        //						}
        //						Pos.X = GlobalMembers.es_argv[1] - 1;
        //						if (Pos.X < 0)
        //						{
        //							Pos.X = 0;
        //						}
        //						if (Pos.X >= Info.dwSize.X)
        //						{
        //							Pos.X = Info.dwSize.X - 1;
        //						}
        //						Pos.Y = GlobalMembers.es_argv[0] - 1;
        //						if (Pos.Y < 0)
        //						{
        //							Pos.Y = 0;
        //						}
        //						if (Pos.Y >= Info.dwSize.Y)
        //						{
        //							Pos.Y = Info.dwSize.Y - 1;
        //						}
        //						SetConsoleCursorPosition(hConOut, Pos);
        //						return;

        //					case 's': // ESC[s Saves cursor position for recall later
        //						if (GlobalMembers.es_argc != 0)
        //						{
        //							return;
        //						}
        //						GlobalMembers.SavePos = Info.dwCursorPosition;
        //						return;

        //					case 'u': // ESC[u Return to saved cursor position
        //						if (GlobalMembers.es_argc != 0)
        //						{
        //							return;
        //						}
        //						SetConsoleCursorPosition(hConOut, GlobalMembers.SavePos);
        //						return;

        //					case 'n': // ESC[#n Device status report
        //						if (GlobalMembers.es_argc != 1)
        //						{
        //							return; // ESC[n == ESC[0n -> ignored
        //						}
        //						switch (GlobalMembers.es_argv[0])
        //						{
        //								case 5: // ESC[5n Report status
        //									ansi.GlobalMembers.SendSequence("\x001B[0n"); // "OK"
        //									return;

        //								case 6: // ESC[6n Report cursor position
        //								{
        //										string buf = new string(new char[32]);
        //										swprintf(buf, 32, "\x001B[%d;%dR", Info.dwCursorPosition.Y + 1, Info.dwCursorPosition.X + 1);
        //										ansi.GlobalMembers.SendSequence(buf);
        //								}
        //										return;

        //								default:
        //									return;
        //						}

        //					case 't': // ESC[#t Window manipulation
        //						if (GlobalMembers.es_argc != 1)
        //						{
        //							return;
        //						}
        //						if (GlobalMembers.es_argv[0] == 21) // ESC[21t Report xterm window's title
        //						{
        //							string buf = new string(new char[MAX_PATH * 2]);
        ////C++ TO C# CONVERTER WARNING: This 'sizeof' ratio was replaced with a direct reference to the array length:
        ////ORIGINAL LINE: uint len = GetConsoleTitleW(buf + 3, (sizeof(buf)/sizeof(*(buf))) - 3 - 2);
        //							uint len = GetConsoleTitleW(buf.Substring(3), (buf.Length) - 3 - 2);
        //							// Too bad if it's too big or fails.
        //							buf = StringFunctions.ChangeCharacter(buf, 0, ESC);
        //							buf = StringFunctions.ChangeCharacter(buf, 1, ']');
        //							buf = StringFunctions.ChangeCharacter(buf, 2, 'l');
        //							buf = StringFunctions.ChangeCharacter(buf, 3 + len, ESC);
        //							buf = StringFunctions.ChangeCharacter(buf, 3 + len + 1, '\\');
        //							buf = StringFunctions.ChangeCharacter(buf, 3 + len + 2, '\0');
        //							ansi.GlobalMembers.SendSequence(buf);
        //						}
        //						return;

        //					default:
        //						return;
        //			}
        //		}
        //		else // (prefix == ']')
        //		{
        //			// Ignore any \e]? or \e]> sequences.
        //			if (GlobalMembers.prefix2 != 0)
        //			{
        //				return;
        //			}

        //			if (GlobalMembers.es_argc == 1 && GlobalMembers.es_argv[0] == 0) // ESC]0;titleST
        //			{
        //				SetConsoleTitleW(GlobalMembers.Pt_arg);
        //			}
        //		}
        //	}

        //	//-----------------------------------------------------------------------------
        //	//   ParseAndPrintANSIString(hDev, lpBuffer, nNumberOfBytesToWrite)
        //	// Parses the string lpBuffer, interprets the escapes sequences and prints the
        //	// characters in the device hDev (console).
        //	// The lexer is a three states automata.
        //	// If the number of arguments es_argc > MAX_ARG, only the MAX_ARG-1 firsts and
        //	// the last arguments are processed (no es_argv[] overflow).
        //	//-----------------------------------------------------------------------------

        //	public static int ParseAndPrintANSIString(IntPtr hDev, LPCVOID lpBuffer, uint nNumberOfBytesToWrite, ref uint lpNumberOfBytesWritten)
        //	{
        //		uint i;
        //		string s;

        //		if (hDev != hConOut) // reinit if device has changed
        //		{
        //			hConOut = hDev;
        //			GlobalMembers.state = 1;
        //			GlobalMembers.shifted = 0;
        //		}
        //		for (i = nNumberOfBytesToWrite, s = (string)lpBuffer; i > 0; i--, s++)
        //		{
        //			if (GlobalMembers.state == 1)
        //			{
        //				if (s == ESC)
        //				{
        //					GlobalMembers.state = 2;
        //				}
        //				else if (s == GlobalMembers.SO)
        //				{
        //					GlobalMembers.shifted = 1;
        //				}
        //				else if (s == GlobalMembers.SI)
        //				{
        //					GlobalMembers.shifted = 0;
        //				}
        //				else
        //				{
        //					ansi.GlobalMembers.PushBuffer(s);
        //				}
        //			}
        //			else if (GlobalMembers.state == 2)
        //			{
        //				if (s == ESC)
        //				{
        //					; // \e\e...\e == \e
        //				}
        //				else if ((s == '[') || (s == ']'))
        //				{
        //					ansi.GlobalMembers.FlushBuffer();
        //					GlobalMembers.prefix = s;
        //					GlobalMembers.prefix2 = 0;
        //					GlobalMembers.state = 3;
        //					GlobalMembers.Pt_len = 0;
        //					*GlobalMembers.Pt_arg = '\0';
        //				}
        //				else if (s == ')' || s == '(')
        //				{
        //					GlobalMembers.state = 6;
        //				}
        //				else
        //				{
        //					GlobalMembers.state = 1;
        //				}
        //			}
        //			else if (GlobalMembers.state == 3)
        //			{
        //				if (is_digit(s))
        //				{
        //					GlobalMembers.es_argc = 0;
        //					GlobalMembers.es_argv[0] = s - '0';
        //					GlobalMembers.state = 4;
        //				}
        //				else if (s == ';')
        //				{
        //					GlobalMembers.es_argc = 1;
        //					GlobalMembers.es_argv[0] = 0;
        //					GlobalMembers.es_argv[1] = 0;
        //					GlobalMembers.state = 4;
        //				}
        //				else if (s == '?' || s == '>')
        //				{
        //					GlobalMembers.prefix2 = s;
        //				}
        //				else
        //				{
        //					GlobalMembers.es_argc = 0;
        //					GlobalMembers.suffix = s;
        //					ansi.GlobalMembers.InterpretEscSeq();
        //					GlobalMembers.state = 1;
        //				}
        //			}
        //			else if (GlobalMembers.state == 4)
        //			{
        //				if (is_digit(s))
        //				{
        //					GlobalMembers.es_argv[GlobalMembers.es_argc] = 10 * GlobalMembers.es_argv[GlobalMembers.es_argc] + (s - '0');
        //				}
        //				else if (s == ';')
        //				{
        //					if (GlobalMembers.es_argc < GlobalMembers.MAX_ARG - 1)
        //					{
        //						GlobalMembers.es_argc++;
        //					}
        //					GlobalMembers.es_argv[GlobalMembers.es_argc] = 0;
        //					if (GlobalMembers.prefix == ']')
        //					{
        //						GlobalMembers.state = 5;
        //					}
        //				}
        //				else
        //				{
        //					GlobalMembers.es_argc++;
        //					GlobalMembers.suffix = s;
        //					ansi.GlobalMembers.InterpretEscSeq();
        //					GlobalMembers.state = 1;
        //				}
        //			}
        //			else if (GlobalMembers.state == 5)
        //			{
        //				if (s == BEL)
        //				{
        //					GlobalMembers.Pt_arg = StringFunctions.ChangeCharacter(GlobalMembers.Pt_arg, GlobalMembers.Pt_len, '\0');
        //					ansi.GlobalMembers.InterpretEscSeq();
        //					GlobalMembers.state = 1;
        //				}
        //				else if (s == '\\' && GlobalMembers.Pt_len > 0 && GlobalMembers.Pt_arg[GlobalMembers.Pt_len - 1] == ESC)
        //				{
        //					GlobalMembers.Pt_arg = StringFunctions.ChangeCharacter(GlobalMembers.Pt_arg, --GlobalMembers.Pt_len, '\0');
        //					ansi.GlobalMembers.InterpretEscSeq();
        //					GlobalMembers.state = 1;
        //				}
        ////C++ TO C# CONVERTER WARNING: This 'sizeof' ratio was replaced with a direct reference to the array length:
        ////ORIGINAL LINE: else if (Pt_len < (sizeof(Pt_arg)/sizeof(*(Pt_arg))) - 1)
        //				else if (GlobalMembers.Pt_len < (GlobalMembers.Pt_arg.Length) - 1)
        //				{
        //					GlobalMembers.Pt_arg = StringFunctions.ChangeCharacter(GlobalMembers.Pt_arg, GlobalMembers.Pt_len++, s);
        //				}
        //			}
        //			else if (GlobalMembers.state == 6)
        //			{
        //				// Ignore it (ESC ) 0 is implicit; nothing else is supported).
        //				GlobalMembers.state = 1;
        //			}
        //		}
        //		ansi.GlobalMembers.FlushBuffer();
        //		if (lpNumberOfBytesWritten != null)
        //		{
        //			lpNumberOfBytesWritten = nNumberOfBytesToWrite - i;
        //		}
        //		return (i == 0);
        //	}
        //	}
        //}

        //namespace linenoise
        //{
        //	public static class GlobalMembers
        //	{
        //	public static IntPtr hOut;
        //	public static IntPtr hIn;
        //	public static uint consolemodeIn = 0;

        //	public static int win32read(ref int c)
        //	{
        //		uint foo;
        //		INPUT_RECORD b = new INPUT_RECORD();
        //		KEY_EVENT_RECORD e = new KEY_EVENT_RECORD();
        //		int altgr;

        //		while (true)
        //		{
        //			if (!ReadConsoleInput(hIn, b, 1, foo))
        //			{
        //				return 0;
        //			}
        //			if (foo == 0)
        //			{
        //				return 0;
        //			}

        //			if (b.EventType == KEY_EVENT && b.Event.KeyEvent.bKeyDown)
        //			{

        //				e = b.Event.KeyEvent;
        //				c = b.Event.KeyEvent.uChar.AsciiChar;

        //				altgr = e.dwControlKeyState & (LEFT_CTRL_PRESSED | RIGHT_ALT_PRESSED);

        //				if (e.dwControlKeyState & (LEFT_CTRL_PRESSED | RIGHT_CTRL_PRESSED) && altgr == 0)
        //				{

        //					/* Ctrl+Key */
        //					switch (c)
        //					{
        //						case 'D':
        //							c = 4;
        //							return 1;
        //						case 'C':
        //							c = 3;
        //							return 1;
        //						case 'H':
        //							c = 8;
        //							return 1;
        //						case 'T':
        //							c = 20;
        //							return 1;
        //						case 'B': // ctrl-b, left_arrow
        //							c = 2;
        //							return 1;
        //						case 'F': // ctrl-f right_arrow
        //							c = 6;
        //							return 1;
        //						case 'P': // ctrl-p up_arrow
        //							c = 16;
        //							return 1;
        //						case 'N': // ctrl-n down_arrow
        //							c = 14;
        //							return 1;
        //						case 'U': // Ctrl+u, delete the whole line.
        //							c = 21;
        //							return 1;
        //						case 'K': // Ctrl+k, delete from current to end of line.
        //							c = 11;
        //							return 1;
        //						case 'A': // Ctrl+a, go to the start of the line
        //							c = 1;
        //							return 1;
        //						case 'E': // ctrl+e, go to the end of the line
        //							c = 5;
        //							return 1;
        //					}

        //					/* Other Ctrl+KEYs ignored */
        //				}
        //				else
        //				{

        //					switch (e.wVirtualKeyCode)
        //					{

        //						case VK_ESCAPE: // ignore - send ctrl-c, will return -1
        //							c = 3;
        //							return 1;
        //						case VK_RETURN: // enter
        //							c = 13;
        //							return 1;
        //						case VK_LEFT: // left
        //							c = 2;
        //							return 1;
        //						case VK_RIGHT: // right
        //							c = 6;
        //							return 1;
        //						case VK_UP: // up
        //							c = 16;
        //							return 1;
        //						case VK_DOWN: // down
        //							c = 14;
        //							return 1;
        //						case VK_HOME:
        //							c = 1;
        //							return 1;
        //						case VK_END:
        //							c = 5;
        //							return 1;
        //						case VK_BACK:
        //							c = 8;
        //							return 1;
        //						case VK_DELETE:
        //							c = 127;
        //							return 1;
        //						default:
        //							if (c != 0)
        //							{
        //								return 1;
        //							}
        //					}
        //				}
        //			}
        //		}

        //		return -1; // Makes compiler happy
        //	}

        //	public static int win32_write(int fd, object buffer, uint count)
        //	{
        //		if (fd == _fileno(stdout))
        //		{
        //			uint bytesWritten = 0;
        //			if (0 != ansi.GlobalMembers.ParseAndPrintANSIString(GetStdHandle(STD_OUTPUT_HANDLE), buffer, (uint)count, ref bytesWritten))
        //			{
        //				return (int)bytesWritten;
        //			}
        //			else
        //			{
        //				errno = GetLastError();
        //				return 0;
        //			}
        //		}
        //		else if (fd == _fileno(stderr))
        //		{
        //			uint bytesWritten = 0;
        //			if (0 != ansi.GlobalMembers.ParseAndPrintANSIString(GetStdHandle(STD_ERROR_HANDLE), buffer, (uint)count, ref bytesWritten))
        //			{
        //				return (int)bytesWritten;
        //			}
        //			else
        //			{
        //				errno = GetLastError();
        //				return 0;
        //			}
        //		}
        //		else
        //		{
        //			return _write(fd, buffer, count);
        //		}
        //	}
        //#endif

        //	internal string[] unsupported_term = {"dumb", "cons25", "emacs", null};
        //	internal static CompletionCallback completionCallback;

        //	#if ! _WIN32
        //	internal static termios orig_termios = new termios(); // In order to restore at exit.
        //	#endif
        //	internal static bool rawmode = false; // For atexit() function to check if restore is needed
        //	internal static bool mlmode = false; // Multi line mode. Default is single line.
        //	internal static bool atexit_registered = false; // Register atexit just 1 time.
        //	internal static uint history_max_len = DefineConstants.LINENOISE_DEFAULT_HISTORY_MAX_LEN;
        //	internal static List<string> history = new List<string>();

        ///* ================================ History ================================= */

        ///* At exit we'll try to fix the terminal to the initial conditions. */

        //	public static void linenoiseAtExit()
        //	{
        //		disableRawMode((_fileno(stdin)));
        //	}

        ///* This is the API call to add a new entry in the linenoise history.
        // * It uses a fixed array of char pointers that are shifted (memmoved)
        // * when the history max length is reached in order to remove the older
        // * entry and make room for the new one, so it is not exactly suitable for huge
        // * histories, but will work well for a few hundred of entries.
        // *
        // * Using a circular buffer is smarter, but a bit more complex to handle. */
        //	public static bool AddHistory(string line)
        //	{
        //		if (history_max_len == 0)
        //		{
        //			return false;
        //		}

        //		/* Don't add duplicated lines. */
        //		if (history.Count > 0 && history[history.Count - 1] == line)
        //		{
        //			return false;
        //		}

        //		/* If we reached the max length, remove the older line. */
        //		if (history.Count == history_max_len)
        //		{
        ////C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'erase' method in C#:
        //			history.erase(history.GetEnumerator());
        //		}
        //		history.Add(line);

        //		return true;
        //	}

        ///* Calls the two low level functions refreshSingleLine() or
        // * refreshMultiLine() according to the selected mode. */
        //	public static void refreshLine(linenoiseState l)
        //	{
        //		if (mlmode)
        //		{
        //			refreshMultiLine(l);
        //		}
        //		else
        //		{
        //			refreshSingleLine(l);
        //		}
        //	}

        //	/* ============================ UTF8 utilities ============================== */

        //	internal static uint[,] unicodeWideCharTable =
        //	{
        //		{0x1100, 0x115F},
        //		{0x2329, 0x232A},
        //		{0x2E80, 0x2E99},
        //		{0x2E9B, 0x2EF3},
        //		{0x2F00, 0x2FD5},
        //		{0x2FF0, 0x2FFB},
        //		{0x3000, 0x303E},
        //		{0x3041, 0x3096},
        //		{0x3099, 0x30FF},
        //		{0x3105, 0x312D},
        //		{0x3131, 0x318E},
        //		{0x3190, 0x31BA},
        //		{0x31C0, 0x31E3},
        //		{0x31F0, 0x321E},
        //		{0x3220, 0x3247},
        //		{0x3250, 0x4DBF},
        //		{0x4E00, 0xA48C},
        //		{0xA490, 0xA4C6},
        //		{0xA960, 0xA97C},
        //		{0xAC00, 0xD7A3},
        //		{0xF900, 0xFAFF},
        //		{0xFE10, 0xFE19},
        //		{0xFE30, 0xFE52},
        //		{0xFE54, 0xFE66},
        //		{0xFE68, 0xFE6B},
        //		{0xFF01, 0xFFE6},
        //		{0x1B000, 0x1B001},
        //		{0x1F200, 0x1F202},
        //		{0x1F210, 0x1F23A},
        //		{0x1F240, 0x1F248},
        //		{0x1F250, 0x1F251},
        //		{0x20000, 0x3FFFD}
        //	};

        ////C++ TO C# CONVERTER WARNING: This 'sizeof' ratio was replaced with a direct reference to the array length:
        ////ORIGINAL LINE: static int unicodeWideCharTableSize = sizeof(unicodeWideCharTable) / sizeof(unicodeWideCharTable[0]);
        //	internal static int unicodeWideCharTableSize = unicodeWideCharTable.Length;

        //	internal static int unicodeIsWideChar(uint cp)
        //	{
        //		int i;
        //		for (i = 0; i < unicodeWideCharTableSize; i++)
        //		{
        //			if (unicodeWideCharTable[i, 0] <= cp != 0 && cp <= unicodeWideCharTable[i, 1])
        //			{
        //				return 1;
        //			}
        //		}
        //		return 0;
        //	}

        //	internal static uint[] unicodeCombiningCharTable = {0x0300, 0x0301, 0x0302, 0x0303, 0x0304, 0x0305, 0x0306, 0x0307, 0x0308, 0x0309, 0x030A, 0x030B, 0x030C, 0x030D, 0x030E, 0x030F, 0x0310, 0x0311, 0x0312, 0x0313, 0x0314, 0x0315, 0x0316, 0x0317, 0x0318, 0x0319, 0x031A, 0x031B, 0x031C, 0x031D, 0x031E, 0x031F, 0x0320, 0x0321, 0x0322, 0x0323, 0x0324, 0x0325, 0x0326, 0x0327, 0x0328, 0x0329, 0x032A, 0x032B, 0x032C, 0x032D, 0x032E, 0x032F, 0x0330, 0x0331, 0x0332, 0x0333, 0x0334, 0x0335, 0x0336, 0x0337, 0x0338, 0x0339, 0x033A, 0x033B, 0x033C, 0x033D, 0x033E, 0x033F, 0x0340, 0x0341, 0x0342, 0x0343, 0x0344, 0x0345, 0x0346, 0x0347, 0x0348, 0x0349, 0x034A, 0x034B, 0x034C, 0x034D, 0x034E, 0x034F, 0x0350, 0x0351, 0x0352, 0x0353, 0x0354, 0x0355, 0x0356, 0x0357, 0x0358, 0x0359, 0x035A, 0x035B, 0x035C, 0x035D, 0x035E, 0x035F, 0x0360, 0x0361, 0x0362, 0x0363, 0x0364, 0x0365, 0x0366, 0x0367, 0x0368, 0x0369, 0x036A, 0x036B, 0x036C, 0x036D, 0x036E, 0x036F, 0x0483, 0x0484, 0x0485, 0x0486, 0x0487, 0x0591, 0x0592, 0x0593, 0x0594, 0x0595, 0x0596, 0x0597, 0x0598, 0x0599, 0x059A, 0x059B, 0x059C, 0x059D, 0x059E, 0x059F, 0x05A0, 0x05A1, 0x05A2, 0x05A3, 0x05A4, 0x05A5, 0x05A6, 0x05A7, 0x05A8, 0x05A9, 0x05AA, 0x05AB, 0x05AC, 0x05AD, 0x05AE, 0x05AF, 0x05B0, 0x05B1, 0x05B2, 0x05B3, 0x05B4, 0x05B5, 0x05B6, 0x05B7, 0x05B8, 0x05B9, 0x05BA, 0x05BB, 0x05BC, 0x05BD, 0x05BF, 0x05C1, 0x05C2, 0x05C4, 0x05C5, 0x05C7, 0x0610, 0x0611, 0x0612, 0x0613, 0x0614, 0x0615, 0x0616, 0x0617, 0x0618, 0x0619, 0x061A, 0x064B, 0x064C, 0x064D, 0x064E, 0x064F, 0x0650, 0x0651, 0x0652, 0x0653, 0x0654, 0x0655, 0x0656, 0x0657, 0x0658, 0x0659, 0x065A, 0x065B, 0x065C, 0x065D, 0x065E, 0x065F, 0x0670, 0x06D6, 0x06D7, 0x06D8, 0x06D9, 0x06DA, 0x06DB, 0x06DC, 0x06DF, 0x06E0, 0x06E1, 0x06E2, 0x06E3, 0x06E4, 0x06E7, 0x06E8, 0x06EA, 0x06EB, 0x06EC, 0x06ED, 0x0711, 0x0730, 0x0731, 0x0732, 0x0733, 0x0734, 0x0735, 0x0736, 0x0737, 0x0738, 0x0739, 0x073A, 0x073B, 0x073C, 0x073D, 0x073E, 0x073F, 0x0740, 0x0741, 0x0742, 0x0743, 0x0744, 0x0745, 0x0746, 0x0747, 0x0748, 0x0749, 0x074A, 0x07A6, 0x07A7, 0x07A8, 0x07A9, 0x07AA, 0x07AB, 0x07AC, 0x07AD, 0x07AE, 0x07AF, 0x07B0, 0x07EB, 0x07EC, 0x07ED, 0x07EE, 0x07EF, 0x07F0, 0x07F1, 0x07F2, 0x07F3, 0x0816, 0x0817, 0x0818, 0x0819, 0x081B, 0x081C, 0x081D, 0x081E, 0x081F, 0x0820, 0x0821, 0x0822, 0x0823, 0x0825, 0x0826, 0x0827, 0x0829, 0x082A, 0x082B, 0x082C, 0x082D, 0x0859, 0x085A, 0x085B, 0x08E3, 0x08E4, 0x08E5, 0x08E6, 0x08E7, 0x08E8, 0x08E9, 0x08EA, 0x08EB, 0x08EC, 0x08ED, 0x08EE, 0x08EF, 0x08F0, 0x08F1, 0x08F2, 0x08F3, 0x08F4, 0x08F5, 0x08F6, 0x08F7, 0x08F8, 0x08F9, 0x08FA, 0x08FB, 0x08FC, 0x08FD, 0x08FE, 0x08FF, 0x0900, 0x0901, 0x0902, 0x093A, 0x093C, 0x0941, 0x0942, 0x0943, 0x0944, 0x0945, 0x0946, 0x0947, 0x0948, 0x094D, 0x0951, 0x0952, 0x0953, 0x0954, 0x0955, 0x0956, 0x0957, 0x0962, 0x0963, 0x0981, 0x09BC, 0x09C1, 0x09C2, 0x09C3, 0x09C4, 0x09CD, 0x09E2, 0x09E3, 0x0A01, 0x0A02, 0x0A3C, 0x0A41, 0x0A42, 0x0A47, 0x0A48, 0x0A4B, 0x0A4C, 0x0A4D, 0x0A51, 0x0A70, 0x0A71, 0x0A75, 0x0A81, 0x0A82, 0x0ABC, 0x0AC1, 0x0AC2, 0x0AC3, 0x0AC4, 0x0AC5, 0x0AC7, 0x0AC8, 0x0ACD, 0x0AE2, 0x0AE3, 0x0B01, 0x0B3C, 0x0B3F, 0x0B41, 0x0B42, 0x0B43, 0x0B44, 0x0B4D, 0x0B56, 0x0B62, 0x0B63, 0x0B82, 0x0BC0, 0x0BCD, 0x0C00, 0x0C3E, 0x0C3F, 0x0C40, 0x0C46, 0x0C47, 0x0C48, 0x0C4A, 0x0C4B, 0x0C4C, 0x0C4D, 0x0C55, 0x0C56, 0x0C62, 0x0C63, 0x0C81, 0x0CBC, 0x0CBF, 0x0CC6, 0x0CCC, 0x0CCD, 0x0CE2, 0x0CE3, 0x0D01, 0x0D41, 0x0D42, 0x0D43, 0x0D44, 0x0D4D, 0x0D62, 0x0D63, 0x0DCA, 0x0DD2, 0x0DD3, 0x0DD4, 0x0DD6, 0x0E31, 0x0E34, 0x0E35, 0x0E36, 0x0E37, 0x0E38, 0x0E39, 0x0E3A, 0x0E47, 0x0E48, 0x0E49, 0x0E4A, 0x0E4B, 0x0E4C, 0x0E4D, 0x0E4E, 0x0EB1, 0x0EB4, 0x0EB5, 0x0EB6, 0x0EB7, 0x0EB8, 0x0EB9, 0x0EBB, 0x0EBC, 0x0EC8, 0x0EC9, 0x0ECA, 0x0ECB, 0x0ECC, 0x0ECD, 0x0F18, 0x0F19, 0x0F35, 0x0F37, 0x0F39, 0x0F71, 0x0F72, 0x0F73, 0x0F74, 0x0F75, 0x0F76, 0x0F77, 0x0F78, 0x0F79, 0x0F7A, 0x0F7B, 0x0F7C, 0x0F7D, 0x0F7E, 0x0F80, 0x0F81, 0x0F82, 0x0F83, 0x0F84, 0x0F86, 0x0F87, 0x0F8D, 0x0F8E, 0x0F8F, 0x0F90, 0x0F91, 0x0F92, 0x0F93, 0x0F94, 0x0F95, 0x0F96, 0x0F97, 0x0F99, 0x0F9A, 0x0F9B, 0x0F9C, 0x0F9D, 0x0F9E, 0x0F9F, 0x0FA0, 0x0FA1, 0x0FA2, 0x0FA3, 0x0FA4, 0x0FA5, 0x0FA6, 0x0FA7, 0x0FA8, 0x0FA9, 0x0FAA, 0x0FAB, 0x0FAC, 0x0FAD, 0x0FAE, 0x0FAF, 0x0FB0, 0x0FB1, 0x0FB2, 0x0FB3, 0x0FB4, 0x0FB5, 0x0FB6, 0x0FB7, 0x0FB8, 0x0FB9, 0x0FBA, 0x0FBB, 0x0FBC, 0x0FC6, 0x102D, 0x102E, 0x102F, 0x1030, 0x1032, 0x1033, 0x1034, 0x1035, 0x1036, 0x1037, 0x1039, 0x103A, 0x103D, 0x103E, 0x1058, 0x1059, 0x105E, 0x105F, 0x1060, 0x1071, 0x1072, 0x1073, 0x1074, 0x1082, 0x1085, 0x1086, 0x108D, 0x109D, 0x135D, 0x135E, 0x135F, 0x1712, 0x1713, 0x1714, 0x1732, 0x1733, 0x1734, 0x1752, 0x1753, 0x1772, 0x1773, 0x17B4, 0x17B5, 0x17B7, 0x17B8, 0x17B9, 0x17BA, 0x17BB, 0x17BC, 0x17BD, 0x17C6, 0x17C9, 0x17CA, 0x17CB, 0x17CC, 0x17CD, 0x17CE, 0x17CF, 0x17D0, 0x17D1, 0x17D2, 0x17D3, 0x17DD, 0x180B, 0x180C, 0x180D, 0x18A9, 0x1920, 0x1921, 0x1922, 0x1927, 0x1928, 0x1932, 0x1939, 0x193A, 0x193B, 0x1A17, 0x1A18, 0x1A1B, 0x1A56, 0x1A58, 0x1A59, 0x1A5A, 0x1A5B, 0x1A5C, 0x1A5D, 0x1A5E, 0x1A60, 0x1A62, 0x1A65, 0x1A66, 0x1A67, 0x1A68, 0x1A69, 0x1A6A, 0x1A6B, 0x1A6C, 0x1A73, 0x1A74, 0x1A75, 0x1A76, 0x1A77, 0x1A78, 0x1A79, 0x1A7A, 0x1A7B, 0x1A7C, 0x1A7F, 0x1AB0, 0x1AB1, 0x1AB2, 0x1AB3, 0x1AB4, 0x1AB5, 0x1AB6, 0x1AB7, 0x1AB8, 0x1AB9, 0x1ABA, 0x1ABB, 0x1ABC, 0x1ABD, 0x1B00, 0x1B01, 0x1B02, 0x1B03, 0x1B34, 0x1B36, 0x1B37, 0x1B38, 0x1B39, 0x1B3A, 0x1B3C, 0x1B42, 0x1B6B, 0x1B6C, 0x1B6D, 0x1B6E, 0x1B6F, 0x1B70, 0x1B71, 0x1B72, 0x1B73, 0x1B80, 0x1B81, 0x1BA2, 0x1BA3, 0x1BA4, 0x1BA5, 0x1BA8, 0x1BA9, 0x1BAB, 0x1BAC, 0x1BAD, 0x1BE6, 0x1BE8, 0x1BE9, 0x1BED, 0x1BEF, 0x1BF0, 0x1BF1, 0x1C2C, 0x1C2D, 0x1C2E, 0x1C2F, 0x1C30, 0x1C31, 0x1C32, 0x1C33, 0x1C36, 0x1C37, 0x1CD0, 0x1CD1, 0x1CD2, 0x1CD4, 0x1CD5, 0x1CD6, 0x1CD7, 0x1CD8, 0x1CD9, 0x1CDA, 0x1CDB, 0x1CDC, 0x1CDD, 0x1CDE, 0x1CDF, 0x1CE0, 0x1CE2, 0x1CE3, 0x1CE4, 0x1CE5, 0x1CE6, 0x1CE7, 0x1CE8, 0x1CED, 0x1CF4, 0x1CF8, 0x1CF9, 0x1DC0, 0x1DC1, 0x1DC2, 0x1DC3, 0x1DC4, 0x1DC5, 0x1DC6, 0x1DC7, 0x1DC8, 0x1DC9, 0x1DCA, 0x1DCB, 0x1DCC, 0x1DCD, 0x1DCE, 0x1DCF, 0x1DD0, 0x1DD1, 0x1DD2, 0x1DD3, 0x1DD4, 0x1DD5, 0x1DD6, 0x1DD7, 0x1DD8, 0x1DD9, 0x1DDA, 0x1DDB, 0x1DDC, 0x1DDD, 0x1DDE, 0x1DDF, 0x1DE0, 0x1DE1, 0x1DE2, 0x1DE3, 0x1DE4, 0x1DE5, 0x1DE6, 0x1DE7, 0x1DE8, 0x1DE9, 0x1DEA, 0x1DEB, 0x1DEC, 0x1DED, 0x1DEE, 0x1DEF, 0x1DF0, 0x1DF1, 0x1DF2, 0x1DF3, 0x1DF4, 0x1DF5, 0x1DFC, 0x1DFD, 0x1DFE, 0x1DFF, 0x20D0, 0x20D1, 0x20D2, 0x20D3, 0x20D4, 0x20D5, 0x20D6, 0x20D7, 0x20D8, 0x20D9, 0x20DA, 0x20DB, 0x20DC, 0x20E1, 0x20E5, 0x20E6, 0x20E7, 0x20E8, 0x20E9, 0x20EA, 0x20EB, 0x20EC, 0x20ED, 0x20EE, 0x20EF, 0x20F0, 0x2CEF, 0x2CF0, 0x2CF1, 0x2D7F, 0x2DE0, 0x2DE1, 0x2DE2, 0x2DE3, 0x2DE4, 0x2DE5, 0x2DE6, 0x2DE7, 0x2DE8, 0x2DE9, 0x2DEA, 0x2DEB, 0x2DEC, 0x2DED, 0x2DEE, 0x2DEF, 0x2DF0, 0x2DF1, 0x2DF2, 0x2DF3, 0x2DF4, 0x2DF5, 0x2DF6, 0x2DF7, 0x2DF8, 0x2DF9, 0x2DFA, 0x2DFB, 0x2DFC, 0x2DFD, 0x2DFE, 0x2DFF, 0x302A, 0x302B, 0x302C, 0x302D, 0x3099, 0x309A, 0xA66F, 0xA674, 0xA675, 0xA676, 0xA677, 0xA678, 0xA679, 0xA67A, 0xA67B, 0xA67C, 0xA67D, 0xA69E, 0xA69F, 0xA6F0, 0xA6F1, 0xA802, 0xA806, 0xA80B, 0xA825, 0xA826, 0xA8C4, 0xA8E0, 0xA8E1, 0xA8E2, 0xA8E3, 0xA8E4, 0xA8E5, 0xA8E6, 0xA8E7, 0xA8E8, 0xA8E9, 0xA8EA, 0xA8EB, 0xA8EC, 0xA8ED, 0xA8EE, 0xA8EF, 0xA8F0, 0xA8F1, 0xA926, 0xA927, 0xA928, 0xA929, 0xA92A, 0xA92B, 0xA92C, 0xA92D, 0xA947, 0xA948, 0xA949, 0xA94A, 0xA94B, 0xA94C, 0xA94D, 0xA94E, 0xA94F, 0xA950, 0xA951, 0xA980, 0xA981, 0xA982, 0xA9B3, 0xA9B6, 0xA9B7, 0xA9B8, 0xA9B9, 0xA9BC, 0xA9E5, 0xAA29, 0xAA2A, 0xAA2B, 0xAA2C, 0xAA2D, 0xAA2E, 0xAA31, 0xAA32, 0xAA35, 0xAA36, 0xAA43, 0xAA4C, 0xAA7C, 0xAAB0, 0xAAB2, 0xAAB3, 0xAAB4, 0xAAB7, 0xAAB8, 0xAABE, 0xAABF, 0xAAC1, 0xAAEC, 0xAAED, 0xAAF6, 0xABE5, 0xABE8, 0xABED, 0xFB1E, 0xFE00, 0xFE01, 0xFE02, 0xFE03, 0xFE04, 0xFE05, 0xFE06, 0xFE07, 0xFE08, 0xFE09, 0xFE0A, 0xFE0B, 0xFE0C, 0xFE0D, 0xFE0E, 0xFE0F, 0xFE20, 0xFE21, 0xFE22, 0xFE23, 0xFE24, 0xFE25, 0xFE26, 0xFE27, 0xFE28, 0xFE29, 0xFE2A, 0xFE2B, 0xFE2C, 0xFE2D, 0xFE2E, 0xFE2F, 0x101FD, 0x102E0, 0x10376, 0x10377, 0x10378, 0x10379, 0x1037A, 0x10A01, 0x10A02, 0x10A03, 0x10A05, 0x10A06, 0x10A0C, 0x10A0D, 0x10A0E, 0x10A0F, 0x10A38, 0x10A39, 0x10A3A, 0x10A3F, 0x10AE5, 0x10AE6, 0x11001, 0x11038, 0x11039, 0x1103A, 0x1103B, 0x1103C, 0x1103D, 0x1103E, 0x1103F, 0x11040, 0x11041, 0x11042, 0x11043, 0x11044, 0x11045, 0x11046, 0x1107F, 0x11080, 0x11081, 0x110B3, 0x110B4, 0x110B5, 0x110B6, 0x110B9, 0x110BA, 0x11100, 0x11101, 0x11102, 0x11127, 0x11128, 0x11129, 0x1112A, 0x1112B, 0x1112D, 0x1112E, 0x1112F, 0x11130, 0x11131, 0x11132, 0x11133, 0x11134, 0x11173, 0x11180, 0x11181, 0x111B6, 0x111B7, 0x111B8, 0x111B9, 0x111BA, 0x111BB, 0x111BC, 0x111BD, 0x111BE, 0x111CA, 0x111CB, 0x111CC, 0x1122F, 0x11230, 0x11231, 0x11234, 0x11236, 0x11237, 0x112DF, 0x112E3, 0x112E4, 0x112E5, 0x112E6, 0x112E7, 0x112E8, 0x112E9, 0x112EA, 0x11300, 0x11301, 0x1133C, 0x11340, 0x11366, 0x11367, 0x11368, 0x11369, 0x1136A, 0x1136B, 0x1136C, 0x11370, 0x11371, 0x11372, 0x11373, 0x11374, 0x114B3, 0x114B4, 0x114B5, 0x114B6, 0x114B7, 0x114B8, 0x114BA, 0x114BF, 0x114C0, 0x114C2, 0x114C3, 0x115B2, 0x115B3, 0x115B4, 0x115B5, 0x115BC, 0x115BD, 0x115BF, 0x115C0, 0x115DC, 0x115DD, 0x11633, 0x11634, 0x11635, 0x11636, 0x11637, 0x11638, 0x11639, 0x1163A, 0x1163D, 0x1163F, 0x11640, 0x116AB, 0x116AD, 0x116B0, 0x116B1, 0x116B2, 0x116B3, 0x116B4, 0x116B5, 0x116B7, 0x1171D, 0x1171E, 0x1171F, 0x11722, 0x11723, 0x11724, 0x11725, 0x11727, 0x11728, 0x11729, 0x1172A, 0x1172B, 0x16AF0, 0x16AF1, 0x16AF2, 0x16AF3, 0x16AF4, 0x16B30, 0x16B31, 0x16B32, 0x16B33, 0x16B34, 0x16B35, 0x16B36, 0x16F8F, 0x16F90, 0x16F91, 0x16F92, 0x1BC9D, 0x1BC9E, 0x1D167, 0x1D168, 0x1D169, 0x1D17B, 0x1D17C, 0x1D17D, 0x1D17E, 0x1D17F, 0x1D180, 0x1D181, 0x1D182, 0x1D185, 0x1D186, 0x1D187, 0x1D188, 0x1D189, 0x1D18A, 0x1D18B, 0x1D1AA, 0x1D1AB, 0x1D1AC, 0x1D1AD, 0x1D242, 0x1D243, 0x1D244, 0x1DA00, 0x1DA01, 0x1DA02, 0x1DA03, 0x1DA04, 0x1DA05, 0x1DA06, 0x1DA07, 0x1DA08, 0x1DA09, 0x1DA0A, 0x1DA0B, 0x1DA0C, 0x1DA0D, 0x1DA0E, 0x1DA0F, 0x1DA10, 0x1DA11, 0x1DA12, 0x1DA13, 0x1DA14, 0x1DA15, 0x1DA16, 0x1DA17, 0x1DA18, 0x1DA19, 0x1DA1A, 0x1DA1B, 0x1DA1C, 0x1DA1D, 0x1DA1E, 0x1DA1F, 0x1DA20, 0x1DA21, 0x1DA22, 0x1DA23, 0x1DA24, 0x1DA25, 0x1DA26, 0x1DA27, 0x1DA28, 0x1DA29, 0x1DA2A, 0x1DA2B, 0x1DA2C, 0x1DA2D, 0x1DA2E, 0x1DA2F, 0x1DA30, 0x1DA31, 0x1DA32, 0x1DA33, 0x1DA34, 0x1DA35, 0x1DA36, 0x1DA3B, 0x1DA3C, 0x1DA3D, 0x1DA3E, 0x1DA3F, 0x1DA40, 0x1DA41, 0x1DA42, 0x1DA43, 0x1DA44, 0x1DA45, 0x1DA46, 0x1DA47, 0x1DA48, 0x1DA49, 0x1DA4A, 0x1DA4B, 0x1DA4C, 0x1DA4D, 0x1DA4E, 0x1DA4F, 0x1DA50, 0x1DA51, 0x1DA52, 0x1DA53, 0x1DA54, 0x1DA55, 0x1DA56, 0x1DA57, 0x1DA58, 0x1DA59, 0x1DA5A, 0x1DA5B, 0x1DA5C, 0x1DA5D, 0x1DA5E, 0x1DA5F, 0x1DA60, 0x1DA61, 0x1DA62, 0x1DA63, 0x1DA64, 0x1DA65, 0x1DA66, 0x1DA67, 0x1DA68, 0x1DA69, 0x1DA6A, 0x1DA6B, 0x1DA6C, 0x1DA75, 0x1DA84, 0x1DA9B, 0x1DA9C, 0x1DA9D, 0x1DA9E, 0x1DA9F, 0x1DAA1, 0x1DAA2, 0x1DAA3, 0x1DAA4, 0x1DAA5, 0x1DAA6, 0x1DAA7, 0x1DAA8, 0x1DAA9, 0x1DAAA, 0x1DAAB, 0x1DAAC, 0x1DAAD, 0x1DAAE, 0x1DAAF, 0x1E8D0, 0x1E8D1, 0x1E8D2, 0x1E8D3, 0x1E8D4, 0x1E8D5, 0x1E8D6, 0xE0100, 0xE0101, 0xE0102, 0xE0103, 0xE0104, 0xE0105, 0xE0106, 0xE0107, 0xE0108, 0xE0109, 0xE010A, 0xE010B, 0xE010C, 0xE010D, 0xE010E, 0xE010F, 0xE0110, 0xE0111, 0xE0112, 0xE0113, 0xE0114, 0xE0115, 0xE0116, 0xE0117, 0xE0118, 0xE0119, 0xE011A, 0xE011B, 0xE011C, 0xE011D, 0xE011E, 0xE011F, 0xE0120, 0xE0121, 0xE0122, 0xE0123, 0xE0124, 0xE0125, 0xE0126, 0xE0127, 0xE0128, 0xE0129, 0xE012A, 0xE012B, 0xE012C, 0xE012D, 0xE012E, 0xE012F, 0xE0130, 0xE0131, 0xE0132, 0xE0133, 0xE0134, 0xE0135, 0xE0136, 0xE0137, 0xE0138, 0xE0139, 0xE013A, 0xE013B, 0xE013C, 0xE013D, 0xE013E, 0xE013F, 0xE0140, 0xE0141, 0xE0142, 0xE0143, 0xE0144, 0xE0145, 0xE0146, 0xE0147, 0xE0148, 0xE0149, 0xE014A, 0xE014B, 0xE014C, 0xE014D, 0xE014E, 0xE014F, 0xE0150, 0xE0151, 0xE0152, 0xE0153, 0xE0154, 0xE0155, 0xE0156, 0xE0157, 0xE0158, 0xE0159, 0xE015A, 0xE015B, 0xE015C, 0xE015D, 0xE015E, 0xE015F, 0xE0160, 0xE0161, 0xE0162, 0xE0163, 0xE0164, 0xE0165, 0xE0166, 0xE0167, 0xE0168, 0xE0169, 0xE016A, 0xE016B, 0xE016C, 0xE016D, 0xE016E, 0xE016F, 0xE0170, 0xE0171, 0xE0172, 0xE0173, 0xE0174, 0xE0175, 0xE0176, 0xE0177, 0xE0178, 0xE0179, 0xE017A, 0xE017B, 0xE017C, 0xE017D, 0xE017E, 0xE017F, 0xE0180, 0xE0181, 0xE0182, 0xE0183, 0xE0184, 0xE0185, 0xE0186, 0xE0187, 0xE0188, 0xE0189, 0xE018A, 0xE018B, 0xE018C, 0xE018D, 0xE018E, 0xE018F, 0xE0190, 0xE0191, 0xE0192, 0xE0193, 0xE0194, 0xE0195, 0xE0196, 0xE0197, 0xE0198, 0xE0199, 0xE019A, 0xE019B, 0xE019C, 0xE019D, 0xE019E, 0xE019F, 0xE01A0, 0xE01A1, 0xE01A2, 0xE01A3, 0xE01A4, 0xE01A5, 0xE01A6, 0xE01A7, 0xE01A8, 0xE01A9, 0xE01AA, 0xE01AB, 0xE01AC, 0xE01AD, 0xE01AE, 0xE01AF, 0xE01B0, 0xE01B1, 0xE01B2, 0xE01B3, 0xE01B4, 0xE01B5, 0xE01B6, 0xE01B7, 0xE01B8, 0xE01B9, 0xE01BA, 0xE01BB, 0xE01BC, 0xE01BD, 0xE01BE, 0xE01BF, 0xE01C0, 0xE01C1, 0xE01C2, 0xE01C3, 0xE01C4, 0xE01C5, 0xE01C6, 0xE01C7, 0xE01C8, 0xE01C9, 0xE01CA, 0xE01CB, 0xE01CC, 0xE01CD, 0xE01CE, 0xE01CF, 0xE01D0, 0xE01D1, 0xE01D2, 0xE01D3, 0xE01D4, 0xE01D5, 0xE01D6, 0xE01D7, 0xE01D8, 0xE01D9, 0xE01DA, 0xE01DB, 0xE01DC, 0xE01DD, 0xE01DE, 0xE01DF, 0xE01E0, 0xE01E1, 0xE01E2, 0xE01E3, 0xE01E4, 0xE01E5, 0xE01E6, 0xE01E7, 0xE01E8, 0xE01E9, 0xE01EA, 0xE01EB, 0xE01EC, 0xE01ED, 0xE01EE, 0xE01EF};

        ////C++ TO C# CONVERTER WARNING: This 'sizeof' ratio was replaced with a direct reference to the array length:
        ////ORIGINAL LINE: static int unicodeCombiningCharTableSize = sizeof(unicodeCombiningCharTable) / sizeof(unicodeCombiningCharTable[0]);
        //	internal static int unicodeCombiningCharTableSize = unicodeCombiningCharTable.Length;

        //	public static int unicodeIsCombiningChar(uint cp)
        //	{
        //		int i;
        //		for (i = 0; i < unicodeCombiningCharTableSize; i++)
        //		{
        //			if (unicodeCombiningCharTable[i] == cp)
        //			{
        //				return 1;
        //			}
        //		}
        //		return 0;
        //	}

        //	/* Get length of previous UTF8 character
        //	 */
        //	public static int unicodePrevUTF8CharLen(ref string buf, int pos)
        //	{
        //		int end = pos--;
        //		while (pos >= 0 && ((byte)buf[pos] & 0xC0) == 0x80)
        //		{
        //			pos--;
        //		}
        //		return end - pos;
        //	}

        //	/* Get length of previous UTF8 character
        //	 */
        //	public static int unicodeUTF8CharLen(ref string buf, int buf_len, int pos)
        //	{
        //		if (pos == buf_len)
        //		{
        //			return 0;
        //		}
        //		byte ch = buf[pos];
        //		if (ch < 0x80)
        //		{
        //			return 1;
        //		}
        //		else if (ch < 0xE0)
        //		{
        //			return 2;
        //		}
        //		else if (ch < 0xF0)
        //		{
        //			return 3;
        //		}
        //		else
        //		{
        //			return 4;
        //		}
        //	}

        //	/* Convert UTF8 to Unicode code point
        //	 */
        //	public static int unicodeUTF8CharToCodePoint(string buf, int len, ref int cp)
        //	{
        //		if (len != 0)
        //		{
        //			byte @byte = buf[0];
        //			if ((byte & 0x80) == 0)
        //			{
        //				cp = byte;
        //				return 1;
        //			}
        //			else if ((byte & 0xE0) == 0xC0)
        //			{
        //				if (len >= 2)
        //				{
        //					cp = (((uint)(buf[0] & 0x1F)) << 6) | ((uint)(buf[1] & 0x3F));
        //					return 2;
        //				}
        //			}
        //			else if ((byte & 0xF0) == 0xE0)
        //			{
        //				if (len >= 3)
        //				{
        //					cp = (((uint)(buf[0] & 0x0F)) << 12) | (((uint)(buf[1] & 0x3F)) << 6) | ((uint)(buf[2] & 0x3F));
        //					return 3;
        //				}
        //			}
        //			else if ((byte & 0xF8) == 0xF0)
        //			{
        //				if (len >= 4)
        //				{
        //					cp = (((uint)(buf[0] & 0x07)) << 18) | (((uint)(buf[1] & 0x3F)) << 12) | (((uint)(buf[2] & 0x3F)) << 6) | ((uint)(buf[3] & 0x3F));
        //					return 4;
        //				}
        //			}
        //		}
        //		return 0;
        //	}

        //	/* Get length of grapheme
        //	 */
        //	public static int unicodeGraphemeLen(ref string buf, int buf_len, int pos)
        //	{
        //		if (pos == buf_len)
        //		{
        //			return 0;
        //		}
        //		int beg = pos;
        //		pos += unicodeUTF8CharLen(ref buf, buf_len, pos);
        //		while (pos < buf_len)
        //		{
        //			int len = unicodeUTF8CharLen(ref buf, buf_len, pos);
        //			int cp = 0;
        //			unicodeUTF8CharToCodePoint(buf.Substring(pos), len, ref cp);
        //			if (unicodeIsCombiningChar(cp) == 0)
        //			{
        //				return pos - beg;
        //			}
        //			pos += len;
        //		}
        //		return pos - beg;
        //	}

        //	/* Get length of previous grapheme
        //	 */
        //	public static int unicodePrevGraphemeLen(ref string buf, int pos)
        //	{
        //		if (pos == 0)
        //		{
        //			return 0;
        //		}
        //		int end = pos;
        //		while (pos > 0)
        //		{
        //			int len = unicodePrevUTF8CharLen(ref buf, pos);
        //			pos -= len;
        //			int cp = 0;
        //			unicodeUTF8CharToCodePoint(buf.Substring(pos), len, ref cp);
        //			if (unicodeIsCombiningChar(cp) == 0)
        //			{
        //				return end - pos;
        //			}
        //		}
        //		return 0;
        //	}

        //	public static int isAnsiEscape(string buf, int buf_len, ref int len)
        //	{
        ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
        //		if (buf_len > 2 && !memcmp("\x001B[", buf, 2))
        //		{
        //			int off = 2;
        //			while (off < buf_len)
        //			{
        //				switch (buf[off++])
        //				{
        //				case 'A':
        //			case 'B':
        //		case 'C':
        //	case 'D':
        //				case 'E':
        //			case 'F':
        //		case 'G':
        //	case 'H':
        //				case 'J':
        //			case 'K':
        //		case 'S':
        //	case 'T':
        //				case 'f':
        //			case 'm':
        //					len = off;
        //					return 1;
        //				}
        //			}
        //		}
        //		return 0;
        //	}

        //	/* Get column position for the single line mode.
        //	 */
        //	public static int unicodeColumnPos(string buf, int buf_len)
        //	{
        //		int ret = 0;

        //		int off = 0;
        //		while (off < buf_len)
        //		{
        //			int len;
        //			if (isAnsiEscape(buf.Substring(off), buf_len - off, ref len))
        //			{
        //				off += len;
        //				continue;
        //			}

        //			int cp = 0;
        //			len = unicodeUTF8CharToCodePoint(buf.Substring(off), buf_len - off, ref cp);

        //			if (unicodeIsCombiningChar(cp) == 0)
        //			{
        //				ret += unicodeIsWideChar(cp) != 0 ? 2 : 1;
        //			}

        //			off += len;
        //		}

        //		return ret;
        //	}

        //	/* Get column position for the multi line mode.
        //	 */
        //	public static int unicodeColumnPosForMultiLine(ref string buf, int buf_len, int pos, int cols, int ini_pos)
        //	{
        //		int ret = 0;
        //		int colwid = ini_pos;

        //		int off = 0;
        //		while (off < buf_len)
        //		{
        //			int cp = 0;
        //			int len = unicodeUTF8CharToCodePoint(buf.Substring(off), buf_len - off, ref cp);

        //			int wid = 0;
        //			if (unicodeIsCombiningChar(cp) == 0)
        //			{
        //				wid = unicodeIsWideChar(cp) != 0 ? 2 : 1;
        //			}

        //			int dif = (int)(colwid + wid) - (int)cols;
        //			if (dif > 0)
        //			{
        //				ret += dif;
        //				colwid = wid;
        //			}
        //			else if (dif == 0)
        //			{
        //				colwid = 0;
        //			}
        //			else
        //			{
        //				colwid += wid;
        //			}

        //			if (off >= pos)
        //			{
        //				break;
        //			}

        //			off += len;
        //			ret += wid;
        //		}

        //		return ret;
        //	}

        //	/* Read UTF8 character from file.
        //	 */
        //	public static int unicodeReadUTF8Char(int fd, ref string buf, ref int cp)
        //	{
        //		int nread = _read(fd, buf[0], 1);

        //		if (nread <= 0)
        //		{
        //			return nread;
        //		}

        //		byte @byte = buf[0];

        //		if ((byte & 0x80) == 0)
        //		{
        //			;
        //		}
        //		else if ((byte & 0xE0) == 0xC0)
        //		{
        //			nread = _read(fd, buf[1], 1);
        //			if (nread <= 0)
        //			{
        //				return nread;
        //			}
        //		}
        //		else if ((byte & 0xF0) == 0xE0)
        //		{
        //			nread = _read(fd, buf[1], 2);
        //			if (nread <= 0)
        //			{
        //				return nread;
        //			}
        //		}
        //		else if ((byte & 0xF8) == 0xF0)
        //		{
        //			nread = _read(fd, buf[1], 3);
        //			if (nread <= 0)
        //			{
        //				return nread;
        //			}
        //		}
        //		else
        //		{
        //			return -1;
        //		}

        //		return unicodeUTF8CharToCodePoint(buf, 4, ref cp);
        //	}

        //	/* ======================= Low level terminal handling ====================== */

        //	/* Set if to use or not the multi line mode. */
        //	public static void SetMultiLine(bool ml)
        //	{
        //		mlmode = ml;
        //	}

        //	/* Return true if the terminal name is in the list of terminals we know are
        //	 * not able to understand basic escape sequences. */
        //	public static bool isUnsupportedTerm()
        //	{
        //	#if ! _WIN32
        ////C++ TO C# CONVERTER TODO TASK: C# does not have an equivalent to pointers to value types:
        ////ORIGINAL LINE: char *term = getenv("TERM");
        //		char term = getenv("TERM");
        //		int j;

        //		if (term == null)
        //		{
        //			return false;
        //		}
        //		for (j = 0; unsupported_term[j]; j++)
        //		{
        //			if (!strcasecmp(term,unsupported_term[j]))
        //			{
        //				return true;
        //			}
        //		}
        //	#endif
        //		return false;
        //	}

        //	/* Raw mode: 1960 magic shit. */
        //	public static bool enableRawMode(int fd)
        //	{
        //	#if ! _WIN32
        //		termios raw = new termios();

        //		if (!_isatty((_fileno(stdin))))
        //		{
        //			goto fatal;
        //		}
        //		if (!atexit_registered)
        //		{
        //			atexit(linenoiseAtExit);
        //			atexit_registered = true;
        //		}
        //		if (tcgetattr(fd,orig_termios) == -1)
        //		{
        //			goto fatal;
        //		}

        //		raw = orig_termios; // modify the original mode
        //		/* input modes: no break, no CR to NL, no parity check, no strip char,
        //		 * no start/stop output control. */
        //		raw.c_iflag &= ~(BRKINT | ICRNL | INPCK | ISTRIP | IXON);
        //		/* output modes - disable post processing */
        //		// NOTE: Multithreaded issue #20 (https://github.com/yhirose/cpp-linenoise/issues/20)
        //		// raw.c_oflag &= ~(OPOST);
        //		/* control modes - set 8 bit chars */
        //		raw.c_cflag |= (CS8);
        //		/* local modes - choing off, canonical off, no extended functions,
        //		 * no signal chars (^Z,^C) */
        //		raw.c_lflag &= ~(ECHO | ICANON | IEXTEN | ISIG);
        //		/* control chars - set return condition: min number of bytes and timer.
        //		 * We want read to return every single byte, without timeout. */
        //		raw.c_cc[VMIN] = 1;
        //		raw.c_cc[VTIME] = 0; // 1 byte, no timer

        //		/* put terminal in raw mode after flushing */
        //		if (tcsetattr(fd, TCSAFLUSH, raw) < 0)
        //		{
        //			goto fatal;
        //		}
        //		rawmode = true;
        //	#else
        //		if (!atexit_registered)
        //		{
        //			/* Cleanup them at exit */
        //			atexit(linenoiseAtExit);
        //			atexit_registered = true;

        //			/* Init windows console handles only once */
        //			hOut = GetStdHandle(STD_OUTPUT_HANDLE);
        //			if (hOut == INVALID_HANDLE_VALUE)
        //			{
        //				goto fatal;
        //			}
        //		}

        //		uint consolemodeOut;
        //		if (!GetConsoleMode(hOut, consolemodeOut))
        //		{
        //			CloseHandle(hOut);
        //			errno = ENOTTY;
        //			return false;
        //		};

        //		hIn = GetStdHandle(STD_INPUT_HANDLE);
        //		if (hIn == INVALID_HANDLE_VALUE)
        //		{
        //			CloseHandle(hOut);
        //			errno = ENOTTY;
        //			return false;
        //		}

        //		GetConsoleMode(hIn, consolemodeIn);
        //		uint consolemodeInWithRaw = consolemodeIn & ~ENABLE_PROCESSED_INPUT;
        //		SetConsoleMode(hIn, consolemodeInWithRaw);

        //		rawmode = true;
        //	#endif
        //		return true;

        //	fatal:
        //		errno = ENOTTY;
        //		return false;
        //	}

        //	public static void disableRawMode(int fd)
        //	{
        //	#if _WIN32
        //		if (consolemodeIn)
        //		{
        //		  SetConsoleMode(hIn, consolemodeIn);
        //		  consolemodeIn = 0;
        //		}
        //		rawmode = false;
        //	#else
        //		/* Don't even check the return value as it's too late. */
        //		if (rawmode && tcsetattr(fd,TCSAFLUSH,orig_termios) != -1)
        //		{
        //			rawmode = false;
        //		}
        //	#endif
        //	}

        //	/* Use the ESC [6n escape sequence to query the horizontal cursor position
        //	 * and return it. On error -1 is returned, on success the position of the
        //	 * cursor. */
        //	public static int getCursorPosition(int ifd, int ofd)
        //	{
        //		string buf = new string(new char[32]);
        //		int cols;
        //		int rows;
        //		uint i = 0;

        //		/* Report cursor location */
        //		if (win32_write(ofd, "\x1b[6n", 4) != 4)
        //		{
        //			return -1;
        //		}

        //		/* Read the response: ESC [ rows ; cols R */
        //		while (i < sizeof(char) - 1)
        //		{
        //			if (_read(ifd,buf.Substring(i),1) != 1)
        //			{
        //				break;
        //			}
        //			if (buf[i] == 'R')
        //			{
        //				break;
        //			}
        //			i++;
        //		}
        //		buf = StringFunctions.ChangeCharacter(buf, i, '\0');

        //		/* Parse it. */
        //		if (buf[0] != KEY_ACTION.ESC || buf[1] != '[')
        //		{
        //			return -1;
        //		}
        //		if (sscanf(buf.Substring(2), "%d;%d", rows, cols) != 2)
        //		{
        //			return -1;
        //		}
        //		return cols;
        //	}

        //	/* Try to get the number of columns in the current terminal, or assume 80
        //	 * if it fails. */
        //	public static int getColumns(int ifd, int ofd)
        //	{
        //	#if _WIN32
        //		CONSOLE_SCREEN_BUFFER_INFO b = new CONSOLE_SCREEN_BUFFER_INFO();

        //		if (!GetConsoleScreenBufferInfo(hOut, b))
        //		{
        //			return 80;
        //		}
        //		return b.srWindow.Right - b.srWindow.Left;
        //	#else
        //		winsize ws = new winsize();

        //		if (ioctl(1, TIOCGWINSZ, ws) == -1 || ws.ws_col == 0)
        //		{
        //			/* ioctl() failed. Try to query the terminal itself. */
        //			int start;
        //			int cols;

        //			/* Get the initial position so we can restore it later. */
        //			start = getCursorPosition(ifd, ofd);
        //			if (start == -1)
        //			{
        //				goto failed;
        //			}

        //			/* Go to right margin and get position. */
        //			if (win32_write(ofd, "\x1b[999C", 6) != 6)
        //			{
        //				goto failed;
        //			}
        //			cols = getCursorPosition(ifd, ofd);
        //			if (cols == -1)
        //			{
        //				goto failed;
        //			}

        //			/* Restore position. */
        //			if (cols > start)
        //			{
        //				string seq = new string(new char[32]);
        //				snprintf(seq,32,"\x1b[%dD",cols - start);
        //				if (win32_write(ofd, seq, seq.Length) == -1)
        //				{
        //					/* Can't recover... */
        //				}
        //			}
        //			return cols;
        //		}
        //		else
        //		{
        //			return ws.ws_col;
        //		}

        //	failed:
        //		return 80;
        //	#endif
        //	}

        //	/* Clear the screen. Used to handle ctrl+l */
        //	public static void linenoiseClearScreen()
        //	{
        //		if (win32_write(DefineConstants.STDOUT_FILENO, "\x1b[H\x1b[2J", 7) <= 0)
        //		{
        //			/* nothing to do, just to avoid warning. */
        //		}
        //	}

        //	/* Beep, used for completion when there is nothing to complete or when all
        //	 * the choices were already shown. */
        //	public static void linenoiseBeep()
        //	{
        //		Console.Error.Write("\x7");
        //		fflush(stderr);
        //	}

        //	/* ============================== Completion ================================ */

        //	/* This is an helper function for linenoiseEdit() and is called when the
        //	 * user types the <tab> key in order to complete the string currently in the
        //	 * input.
        //	 *
        //	 * The state of the editing is encapsulated into the pointed linenoiseState
        //	 * structure as described in the structure definition. */
        //	public static int completeLine(linenoiseState ls, ref string cbuf, ref int c)
        //	{
        //		List<string> lc = new List<string>();
        //		int nread = 0;
        //		int nwritten;
        //		c = null;

        //		completionCallback(ls.buf,lc);
        //		if (lc.Count == 0)
        //		{
        //			linenoiseBeep();
        //		}
        //		else
        //		{
        //			int stop = 0;
        //			int i = 0;

        //			while (stop == 0)
        //			{
        //				/* Show completion or original buffer */
        //				if (i < (int)lc.Count)
        //				{
        ////C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
        ////ORIGINAL LINE: struct linenoiseState saved = *ls;
        //					linenoiseState saved = new linenoiseState(ls);

        //					ls.len = ls.pos = (int)(lc[i].Length);
        //					ls.buf = lc[i][0];
        //					refreshLine(ls);
        //					ls.len = saved.len;
        //					ls.pos = saved.pos;
        //					ls.buf = saved.buf;
        //				}
        //				else
        //				{
        //					refreshLine(ls);
        //				}

        //				//nread = read(ls->ifd,&c,1);
        //	#if _WIN32
        //				nread = win32read(ref c);
        //				if (nread == 1)
        //				{
        //					cbuf[0] = c;
        //				}
        //	#else
        //				nread = unicodeReadUTF8Char(ls.ifd, ref cbuf, ref c);
        //	#endif
        //				if (nread <= 0)
        //				{
        //					c = -1;
        //					return nread;
        //				}

        //				switch (c)
        //				{
        //					case 9: // tab
        //						i = (i + 1) % (lc.Count + 1);
        //						if (i == (int)lc.Count)
        //						{
        //							linenoiseBeep();
        //						}
        //						break;
        //					case 27: // escape
        //						/* Re-show original buffer */
        //						if (i < (int)lc.Count)
        //						{
        //							refreshLine(ls);
        //						}
        //						stop = 1;
        //						break;
        //					default:
        //						/* Update buffer and return */
        //						if (i < (int)lc.Count)
        //						{
        //							nwritten = snprintf(ls.buf, ls.buflen, "%s", lc[i][0]);
        //							ls.len = ls.pos = nwritten;
        //						}
        //						stop = 1;
        //						break;
        //				}
        //			}
        //		}

        //		return nread;
        //	}

        //	/* Register a callback function to be called for tab-completion. */
        //	public static void SetCompletionCallback(CompletionCallback fn)
        //	{
        //		completionCallback = fn;
        //	}

        //	/* =========================== Line editing ================================= */

        //	/* Single line low level line refresh.
        //	 *
        //	 * Rewrite the currently edited line accordingly to the buffer content,
        //	 * cursor position, and number of columns of the terminal. */
        //	public static void refreshSingleLine(linenoiseState l)
        //	{
        //		string seq = new string(new char[64]);
        //		int pcolwid = unicodeColumnPos(l.prompt, (int)l.prompt.Length);
        //		int fd = l.ofd;
        ////C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on this variable, so pointers on this variable are left unchanged:
        //		char * buf = l.buf;
        //		int len = l.len;
        //		int pos = l.pos;
        //		string ab;

        //		while ((pcolwid + unicodeColumnPos(buf, pos)) >= l.cols)
        //		{
        //			int glen = unicodeGraphemeLen(ref buf, len, 0);
        //			buf += glen;
        //			len -= glen;
        //			pos -= glen;
        //		}
        //		while (pcolwid + unicodeColumnPos(buf, len) > l.cols)
        //		{
        //			len -= unicodePrevGraphemeLen(ref buf, len);
        //		}

        //		/* Cursor to left edge */
        //		snprintf(seq,64,"\r");
        //		ab += seq;
        //		/* Write the prompt and the current buffer content */
        //		ab += l.prompt;
        //		ab.append(buf, len);
        //		/* Erase to right */
        //		snprintf(seq,64,"\x1b[0K");
        //		ab += seq;
        //		/* Move cursor to original position. */
        //		snprintf(seq,64,"\r\x1b[%dC", (int)(unicodeColumnPos(buf, pos) + pcolwid));
        //		ab += seq;
        //		if (win32_write(fd, ab, (int)ab.Length) == -1)
        //		{
        //		} // Can't recover from write error.
        //	}

        //	/* Multi line low level line refresh.
        //	 *
        //	 * Rewrite the currently edited line accordingly to the buffer content,
        //	 * cursor position, and number of columns of the terminal. */
        //	public static void refreshMultiLine(linenoiseState l)
        //	{
        //		string seq = new string(new char[64]);
        //		int pcolwid = unicodeColumnPos(l.prompt, (int)l.prompt.Length);
        //		int colpos = unicodeColumnPosForMultiLine(ref l.buf, l.len, l.len, l.cols, pcolwid);
        //		int colpos2; // cursor column position.
        //		int rows = (pcolwid + colpos + l.cols - 1) / l.cols; // rows used by current buf.
        //		int rpos = (pcolwid + l.oldcolpos + l.cols) / l.cols; // cursor relative row.
        //		int rpos2; // rpos after refresh.
        //		int col; // colum position, zero-based.
        //		int old_rows = (int)l.maxrows;
        //		int fd = l.ofd;
        //		int j;
        //		string ab;

        //		/* Update maxrows if needed. */
        //		if (rows > (int)l.maxrows)
        //		{
        //			l.maxrows = rows;
        //		}

        //		/* First step: clear all the lines used before. To do so start by
        //		 * going to the last row. */
        //		if (old_rows - rpos > 0)
        //		{
        //			snprintf(seq,64,"\x1b[%dB", old_rows - rpos);
        //			ab += seq;
        //		}

        //		/* Now for every row clear it, go up. */
        //		for (j = 0; j < old_rows - 1; j++)
        //		{
        //			snprintf(seq,64,"\r\x1b[0K\x1b[1A");
        //			ab += seq;
        //		}

        //		/* Clean the top line. */
        //		snprintf(seq,64,"\r\x1b[0K");
        //		ab += seq;

        //		/* Write the prompt and the current buffer content */
        //		ab += l.prompt;
        //		ab.append(l.buf, l.len);

        //		/* Get text width to cursor position */
        //		colpos2 = unicodeColumnPosForMultiLine(ref l.buf, l.len, l.pos, l.cols, pcolwid);

        //		/* If we are at the very end of the screen with our prompt, we need to
        //		 * emit a newline and move the prompt to the first column. */
        //		if (l.pos != 0 && l.pos == l.len && (colpos2 + pcolwid) % l.cols == 0)
        //		{
        //			ab += "\n";
        //			snprintf(seq,64,"\r");
        //			ab += seq;
        //			rows++;
        //			if (rows > (int)l.maxrows)
        //			{
        //				l.maxrows = rows;
        //			}
        //		}

        //		/* Move cursor to right position. */
        //		rpos2 = (pcolwid + colpos2 + l.cols) / l.cols; // current cursor relative row.

        //		/* Go up till we reach the expected positon. */
        //		if (rows - rpos2 > 0)
        //		{
        //			snprintf(seq,64,"\x1b[%dA", rows - rpos2);
        //			ab += seq;
        //		}

        //		/* Set column. */
        //		col = (pcolwid + colpos2) % l.cols;
        //		if (col != 0)
        //		{
        //			snprintf(seq,64,"\r\x1b[%dC", col);
        //		}
        //		else
        //		{
        //			snprintf(seq,64,"\r");
        //		}
        //		ab += seq;

        //		l.oldcolpos = colpos2;

        //		if (win32_write(fd, ab, (int)ab.Length) == -1)
        //		{
        //		} // Can't recover from write error.
        //	}

        //	/* Insert the character 'c' at cursor current position.
        //	 *
        //	 * On error writing to the terminal -1 is returned, otherwise 0. */
        //	public static int linenoiseEditInsert(linenoiseState l, string cbuf, int clen)
        //	{
        //		if (l.len < l.buflen)
        //		{
        //			if (l.len == l.pos)
        //			{
        ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
        //				memcpy(l.buf[l.pos], cbuf, clen);
        //				l.pos += clen;
        //				l.len += clen;
        //				l.buf = StringFunctions.ChangeCharacter(l.buf, l.len, '\0');
        //				if ((!mlmode && unicodeColumnPos(l.prompt, (int)l.prompt.Length) + unicodeColumnPos(l.buf, l.len) < l.cols))
        //				{
        //					/* Avoid a full update of the line in the
        //					 * trivial case. */
        //					if (win32_write(l.ofd, cbuf, clen) == -1)
        //					{
        //						return -1;
        //					}
        //				}
        //				else
        //				{
        //					refreshLine(l);
        //				}
        //			}
        //			else
        //			{
        ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memmove' has no equivalent in C#:
        //				memmove(l.buf.Substring(l.pos) + clen,l.buf.Substring(l.pos),l.len - l.pos);
        ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
        //				memcpy(l.buf[l.pos], cbuf, clen);
        //				l.pos += clen;
        //				l.len += clen;
        //				l.buf = StringFunctions.ChangeCharacter(l.buf, l.len, '\0');
        //				refreshLine(l);
        //			}
        //		}
        //		return 0;
        //	}

        //	/* Move cursor on the left. */
        //	public static void linenoiseEditMoveLeft(linenoiseState l)
        //	{
        //		if (l.pos > 0)
        //		{
        //			l.pos -= unicodePrevGraphemeLen(ref l.buf, l.pos);
        //			refreshLine(l);
        //		}
        //	}

        //	/* Move cursor on the right. */
        //	public static void linenoiseEditMoveRight(linenoiseState l)
        //	{
        //		if (l.pos != l.len)
        //		{
        //			l.pos += unicodeGraphemeLen(ref l.buf, l.len, l.pos);
        //			refreshLine(l);
        //		}
        //	}

        //	/* Move cursor to the start of the line. */
        //	public static void linenoiseEditMoveHome(linenoiseState l)
        //	{
        //		if (l.pos != 0)
        //		{
        //			l.pos = 0;
        //			refreshLine(l);
        //		}
        //	}

        //	/* Move cursor to the end of the line. */
        //	public static void linenoiseEditMoveEnd(linenoiseState l)
        //	{
        //		if (l.pos != l.len)
        //		{
        //			l.pos = l.len;
        //			refreshLine(l);
        //		}
        //	}

        //	/* Substitute the currently edited line with the next or previous history
        //	 * entry as specified by 'dir'. */
        //	public static void linenoiseEditHistoryNext(linenoiseState l, int dir)
        //	{
        //		if (history.Count > 1)
        //		{
        //			/* Update the current history entry before to
        //			 * overwrite it with the next one. */
        //			history[history.Count - 1 - l.history_index] = l.buf;
        //			/* Show the new entry */
        //			l.history_index += (dir == DefineConstants.LINENOISE_HISTORY_PREV) ? 1 : -1;
        //			if (l.history_index < 0)
        //			{
        //				l.history_index = 0;
        //				return;
        //			}
        //			else if (l.history_index >= (int)history.Count)
        //			{
        //				l.history_index = (int)history.Count - 1;
        //				return;
        //			}
        ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
        //			memset(l.buf, 0, l.buflen);
        //			l.buf = history[history.Count - 1 - l.history_index];
        //			l.len = l.pos = (int)l.buf.Length;
        //			refreshLine(l);
        //		}
        //	}

        //	/* Delete the character at the right of the cursor without altering the cursor
        //	 * position. Basically this is what happens with the "Delete" keyboard key. */
        //	public static void linenoiseEditDelete(linenoiseState l)
        //	{
        //		if (l.len > 0 && l.pos < l.len)
        //		{
        //			int glen = unicodeGraphemeLen(ref l.buf, l.len, l.pos);
        ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memmove' has no equivalent in C#:
        //			memmove(l.buf.Substring(l.pos),l.buf.Substring(l.pos) + glen,l.len - l.pos - glen);
        //			l.len -= glen;
        //			l.buf = StringFunctions.ChangeCharacter(l.buf, l.len, '\0');
        //			refreshLine(l);
        //		}
        //	}

        //	/* Backspace implementation. */
        //	public static void linenoiseEditBackspace(linenoiseState l)
        //	{
        //		if (l.pos > 0 && l.len > 0)
        //		{
        //			int glen = unicodePrevGraphemeLen(ref l.buf, l.pos);
        ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memmove' has no equivalent in C#:
        //			memmove(l.buf.Substring(l.pos) - glen,l.buf.Substring(l.pos),l.len - l.pos);
        //			l.pos -= glen;
        //			l.len -= glen;
        //			l.buf = StringFunctions.ChangeCharacter(l.buf, l.len, '\0');
        //			refreshLine(l);
        //		}
        //	}

        //	/* Delete the previosu word, maintaining the cursor at the start of the
        //	 * current word. */
        //	public static void linenoiseEditDeletePrevWord(linenoiseState l)
        //	{
        //		int old_pos = l.pos;
        //		int diff;

        //		while (l.pos > 0 && l.buf[l.pos - 1] == ' ')
        //		{
        //			l.pos--;
        //		}
        //		while (l.pos > 0 && l.buf[l.pos - 1] != ' ')
        //		{
        //			l.pos--;
        //		}
        //		diff = old_pos - l.pos;
        ////C++ TO C# CONVERTER TODO TASK: The memory management function 'memmove' has no equivalent in C#:
        //		memmove(l.buf.Substring(l.pos),l.buf.Substring(old_pos),l.len - old_pos + 1);
        //		l.len -= diff;
        //		refreshLine(l);
        //	}

        //	/* This function is the core of the line editing capability of linenoise.
        //	 * It expects 'fd' to be already in "raw mode" so that every key pressed
        //	 * will be returned ASAP to read().
        //	 *
        //	 * The resulting string is put into 'buf' when the user type enter, or
        //	 * when ctrl+d is typed.
        //	 *
        //	 * The function returns the length of the current buffer. */
        //	public static int linenoiseEdit(int stdin_fd, int stdout_fd, ref string buf, int buflen, string prompt)
        //	{
        //		linenoiseState l = new linenoiseState();

        //		/* Populate the linenoise state that we pass to functions implementing
        //		 * specific editing functionalities. */
        //		l.ifd = stdin_fd;
        //		l.ofd = stdout_fd;
        //		l.buf = buf;
        //		l.buflen = buflen;
        //		l.prompt = prompt;
        //		l.oldcolpos = l.pos = 0;
        //		l.len = 0;
        //		l.cols = getColumns(stdin_fd, stdout_fd);
        //		l.maxrows = 0;
        //		l.history_index = 0;

        //		/* Buffer starts empty. */
        //		l.buf = StringFunctions.ChangeCharacter(l.buf, 0, '\0');
        //		l.buflen--; // Make sure there is always space for the nulterm

        //		/* The latest history entry is always our current buffer, that
        //		 * initially is just an empty string. */
        //		AddHistory("");

        //		if (win32_write(l.ofd, prompt, (int)l.prompt.Length) == -1)
        //		{
        //			return -1;
        //		}
        //		while (true)
        //		{
        //			int c;
        //			string cbuf = new string(new char[4]);
        //			int nread;
        //			string seq = new string(new char[3]);

        //	#if _WIN32
        //			nread = win32read(ref c);
        //			if (nread == 1)
        //			{
        //				cbuf = StringFunctions.ChangeCharacter(cbuf, 0, c);
        //			}
        //	#else
        //			nread = unicodeReadUTF8Char(l.ifd, ref cbuf, ref c);
        //	#endif
        //			if (nread <= 0)
        //			{
        //				return (int)l.len;
        //			}

        //			/* Only autocomplete when the callback is set. It returns < 0 when
        //			 * there was an error reading from fd. Otherwise it will return the
        //			 * character that should be handled next. */
        //			if (c == 9 && completionCallback != null)
        //			{
        //				nread = completeLine(l, ref cbuf, ref c);
        //				/* Return on errors */
        //				if (c < 0)
        //				{
        //					return l.len;
        //				}
        //				/* Read next character when 0 */
        //				if (c == 0)
        //				{
        //					continue;
        //				}
        //			}

        //			switch (c)
        //			{
        //			case KEY_ACTION.ENTER: // enter
        //				if (history.Count > 0)
        //				{
        //					history.RemoveAt(history.Count - 1);
        //				}
        //				if (mlmode)
        //				{
        //					linenoiseEditMoveEnd(l);
        //				}
        //				return (int)l.len;
        //			case KEY_ACTION.CTRL_C: // ctrl-c
        //				errno = EAGAIN;
        //				return -1;
        //			case KEY_ACTION.BACKSPACE: // backspace
        //			case 8: // ctrl-h
        //				linenoiseEditBackspace(l);
        //				break;
        //			case KEY_ACTION.CTRL_D: /* ctrl-d, remove char at right of cursor, or if the
        //	                            line is empty, act as end-of-file. */
        //				if (l.len > 0)
        //				{
        //					linenoiseEditDelete(l);
        //				}
        //				else
        //				{
        //					history.RemoveAt(history.Count - 1);
        //					return -1;
        //				}
        //				break;
        //			case KEY_ACTION.CTRL_T: // ctrl-t, swaps current character with previous.
        //				if (l.pos > 0 && l.pos < l.len)
        //				{
        //					char aux = buf[l.pos - 1];
        //					buf[l.pos - 1] = buf[l.pos];
        //					buf[l.pos] = aux;
        //					if (l.pos != l.len - 1)
        //					{
        //						l.pos++;
        //					}
        //					refreshLine(l);
        //				}
        //				break;
        //			case KEY_ACTION.CTRL_B: // ctrl-b
        //				linenoiseEditMoveLeft(l);
        //				break;
        //			case KEY_ACTION.CTRL_F: // ctrl-f
        //				linenoiseEditMoveRight(l);
        //				break;
        //			case KEY_ACTION.CTRL_P: // ctrl-p
        //				linenoiseEditHistoryNext(l, DefineConstants.LINENOISE_HISTORY_PREV);
        //				break;
        //			case KEY_ACTION.CTRL_N: // ctrl-n
        //				linenoiseEditHistoryNext(l, DefineConstants.LINENOISE_HISTORY_NEXT);
        //				break;
        //			case KEY_ACTION.ESC: // escape sequence
        //				/* Read the next two bytes representing the escape sequence.
        //				 * Use two calls to handle slow terminals returning the two
        //				 * chars at different times. */
        //				if (_read(l.ifd,seq,1) == -1)
        //				{
        //					break;
        //				}
        //				if (_read(l.ifd,seq.Substring(1),1) == -1)
        //				{
        //					break;
        //				}

        //				/* ESC [ sequences. */
        //				if (seq[0] == '[')
        //				{
        //					if (seq[1] >= '0' && seq[1] <= '9')
        //					{
        //						/* Extended escape, read additional byte. */
        //						if (_read(l.ifd,seq.Substring(2),1) == -1)
        //						{
        //							break;
        //						}
        //						if (seq[2] == '~')
        //						{
        //							switch (seq[1])
        //							{
        //							case '3': // Delete key.
        //								linenoiseEditDelete(l);
        //								break;
        //							}
        //						}
        //					}
        //					else
        //					{
        //						switch (seq[1])
        //						{
        //						case 'A': // Up
        //							linenoiseEditHistoryNext(l, DefineConstants.LINENOISE_HISTORY_PREV);
        //							break;
        //						case 'B': // Down
        //							linenoiseEditHistoryNext(l, DefineConstants.LINENOISE_HISTORY_NEXT);
        //							break;
        //						case 'C': // Right
        //							linenoiseEditMoveRight(l);
        //							break;
        //						case 'D': // Left
        //							linenoiseEditMoveLeft(l);
        //							break;
        //						case 'H': // Home
        //							linenoiseEditMoveHome(l);
        //							break;
        //						case 'F': // End
        //							linenoiseEditMoveEnd(l);
        //							break;
        //						}
        //					}
        //				}

        //				/* ESC O sequences. */
        //				else if (seq[0] == 'O')
        //				{
        //					switch (seq[1])
        //					{
        //					case 'H': // Home
        //						linenoiseEditMoveHome(l);
        //						break;
        //					case 'F': // End
        //						linenoiseEditMoveEnd(l);
        //						break;
        //					}
        //				}
        //				break;
        //			default:
        //				if (linenoiseEditInsert(l, cbuf, nread))
        //				{
        //					return -1;
        //				}
        //				break;
        //			case KEY_ACTION.CTRL_U: // Ctrl+u, delete the whole line.
        //				buf[0] = '\0';
        //				l.pos = l.len = 0;
        //				refreshLine(l);
        //				break;
        //			case KEY_ACTION.CTRL_K: // Ctrl+k, delete from current to end of line.
        //				buf[l.pos] = '\0';
        //				l.len = l.pos;
        //				refreshLine(l);
        //				break;
        //			case KEY_ACTION.CTRL_A: // Ctrl+a, go to the start of the line
        //				linenoiseEditMoveHome(l);
        //				break;
        //			case KEY_ACTION.CTRL_E: // ctrl+e, go to the end of the line
        //				linenoiseEditMoveEnd(l);
        //				break;
        //			case KEY_ACTION.CTRL_L: // ctrl+l, clear screen
        //				linenoiseClearScreen();
        //				refreshLine(l);
        //				break;
        //			case KEY_ACTION.CTRL_W: // ctrl+w, delete previous word
        //				linenoiseEditDeletePrevWord(l);
        //				break;
        //			}
        //		}
        //		return l.len;
        //	}

        //	/* This function calls the line editing function linenoiseEdit() using
        //	 * the STDIN file descriptor set in raw mode. */
        //	public static bool linenoiseRaw(string prompt, string line)
        //	{
        //		bool quit = false;

        //		if (!_isatty((_fileno(stdin))))
        //		{
        //			/* Not a tty: read from file / pipe. */
        //			line = Console.ReadLine();
        //		}
        //		else
        //		{
        //			/* Interactive editing. */
        //			if (enableRawMode((_fileno(stdin))) == false)
        //			{
        //				return quit;
        //			}

        //			string buf = new string(new char[DefineConstants.LINENOISE_MAX_LINE]);
        //			var count = linenoiseEdit((_fileno(stdin)), DefineConstants.STDOUT_FILENO, ref buf, DefineConstants.LINENOISE_MAX_LINE, prompt);
        //			if (count == -1)
        //			{
        //				quit = true;
        //			}
        //			else
        //			{
        //				line.assign(buf, count);
        //			}

        //			disableRawMode((_fileno(stdin)));
        //			Console.Write("\n");
        //		}
        //		return quit;
        //	}

        //	/* The high level function that is the main API of the linenoise library.
        //	 * This function checks if the terminal has basic capabilities, just checking
        //	 * for a blacklist of stupid terminals, and later either calls the line
        //	 * editing function or uses dummy fgets() so that you will be able to type
        //	 * something even in the most desperate of the conditions. */
        //	public static bool Readline(string prompt, string line)
        //	{
        //		if (isUnsupportedTerm())
        //		{
        //			Console.Write("{0}",prompt);
        //			fflush(stdout);
        //			line = Console.ReadLine();
        //			return false;
        //		}
        //		else
        //		{
        //			return linenoiseRaw(prompt, line);
        //		}
        //	}

        //	public static string Readline(string prompt, ref bool quit)
        //	{
        //		string line;
        //		quit = Readline(prompt, ref line);
        //		return line;
        //	}

        //	public static string Readline(string prompt)
        //	{
        //		bool quit; // dummy
        //		return Readline(prompt, ref quit);
        //	}

        //	/* Set the maximum length for the history. This function can be called even
        //	 * if there is already some history, the function will make sure to retain
        //	 * just the latest 'len' elements if the new history length value is smaller
        //	 * than the amount of items already inside the history. */
        //	public static bool SetHistoryMaxLen(uint len)
        //	{
        //		if (len < 1)
        //		{
        //			return false;
        //		}
        //		history_max_len = len;
        //		if (len < history.Count)
        //		{
        //			history.Resize(len);
        //		}
        //		return true;
        //	}

        //	/* Save the history in the specified file. On success *true* is returned
        //	 * otherwise *false* is returned. */
        //	public static bool SaveHistory(string path)
        //	{
        //		std::ofstream f = new std::ofstream(path); // TODO: need 'std::ios::binary'?
        //		if (f == null)
        //		{
        //			return false;
        //		}
        //		foreach (var h in history)
        //		{
        //			f << h << std::endl;
        //		}
        //		return true;
        //	}

        //	/* Load the history from the specified file. If the file does not exist
        //	 * zero is returned and no operation is performed.
        //	 *
        //	 * If the file exists and the operation succeeded *true* is returned, otherwise
        //	 * on error *false* is returned. */
        //	public static bool LoadHistory(string path)
        //	{
        //		std::ifstream f = new std::ifstream(path);
        //		if (f == null)
        //		{
        //			return false;
        //		}
        //		string line;
        //		while (getline(f, line))
        //		{
        //			AddHistory(line);
        //		}
        //		return true;
        //	}

        //	public static List<string> GetHistory()
        //	{
        //		return history;
        //	}
        //	}
        //}

        //namespace CryptoNote.parameters
        //{
        //	public static class GlobalMembers
        //	{
        //	public static readonly ulong DIFFICULTY_TARGET = 30; // seconds

        //	public static readonly uint CRYPTONOTE_MAX_BLOCK_NUMBER = 500000000;
        //	public static readonly uint CRYPTONOTE_MAX_BLOCK_BLOB_SIZE = 500000000;
        //	public static readonly uint CRYPTONOTE_MAX_TX_SIZE = 1000000000;
        //	public static readonly ulong CRYPTONOTE_PUBLIC_ADDRESS_BASE58_PREFIX = 3914525;
        //	public static readonly uint CRYPTONOTE_MINED_MONEY_UNLOCK_WINDOW = 40;
        //	public static readonly ulong CRYPTONOTE_BLOCK_FUTURE_TIME_LIMIT = 60 * 60 * 2;
        //	public static readonly ulong CRYPTONOTE_BLOCK_FUTURE_TIME_LIMIT_V3 = 3 * DIFFICULTY_TARGET;
        //	public static readonly ulong CRYPTONOTE_BLOCK_FUTURE_TIME_LIMIT_V4 = 6 * DIFFICULTY_TARGET;

        //	public static readonly uint BLOCKCHAIN_TIMESTAMP_CHECK_WINDOW = 60;
        //	public static readonly uint BLOCKCHAIN_TIMESTAMP_CHECK_WINDOW_V3 = 11;

        //	// MONEY_SUPPLY - total number coins to be generated
        //	public static readonly ulong MONEY_SUPPLY = UINT64_C(100000000000000);
        //	public static readonly uint ZAWY_DIFFICULTY_BLOCK_INDEX = 187000;
        //	public static readonly uint ZAWY_DIFFICULTY_V2 = 0;
        //	public static readonly byte ZAWY_DIFFICULTY_DIFFICULTY_BLOCK_VERSION = 3;

        //	public static readonly ulong LWMA_2_DIFFICULTY_BLOCK_INDEX = 620000;
        //	public static readonly ulong LWMA_2_DIFFICULTY_BLOCK_INDEX_V2 = 700000;
        //	public static readonly ulong LWMA_2_DIFFICULTY_BLOCK_INDEX_V3 = 800000;

        //	public static readonly ulong LWMA_3_DIFFICULTY_BLOCK_INDEX = 1000000;

        //	public static readonly uint EMISSION_SPEED_FACTOR = 25;
        //	//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
        //	//static_assert(EMISSION_SPEED_FACTOR <= 8 * sizeof(ulong), "Bad EMISSION_SPEED_FACTOR");

        //	/* Premine amount */
        //	public static readonly ulong GENESIS_BLOCK_REWARD = UINT64_C(0);

        //	/* How to generate a premine:

        //	* Compile your code

        //	* Run zedwallet, ignore that it can't connect to the daemon, and generate an
        //	  address. Save this and the keys somewhere safe.

        //	* Launch the daemon with these arguments:
        //	--print-genesis-tx --genesis-block-reward-address <premine wallet address>

        //	For example:
        //	TurtleCoind --print-genesis-tx --genesis-block-reward-address TRTLv2Fyavy8CXG8BPEbNeCHFZ1fuDCYCZ3vW5H5LXN4K2M2MHUpTENip9bbavpHvvPwb4NDkBWrNgURAd5DB38FHXWZyoBh4wW

        //	* Take the hash printed, and replace it with the hash below in GENESIS_COINBASE_TX_HEX

        //	* Recompile, setup your seed nodes, and start mining

        //	* You should see your premine appear in the previously generated wallet.

        //	*/
        //	public const string GENESIS_COINBASE_TX_HEX = "010a01ff000188f3b501029b2e4c0281c0b02e7c53291a94d1d0cbff8883f8024f5142ee494ffbbd088071210142694232c5b04151d9e4c27d31ec7a68ea568b19488cfcb422659a07a0e44dd5";
        //	//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
        //	//static_assert(sizeof(GENESIS_COINBASE_TX_HEX)/sizeof(*GENESIS_COINBASE_TX_HEX) != 1, "GENESIS_COINBASE_TX_HEX must not be empty.");

        //	/* This is the unix timestamp of the first "mined" block (technically block 2, not the genesis block)
        //	   You can get this value by doing "print_block 2" in TurtleCoind. It is used to know what timestamp
        //	   to import from when the block height cannot be found in the node or the node is offline. */
        //	public static readonly ulong GENESIS_BLOCK_TIMESTAMP = 1512800692;

        //	public static readonly uint CRYPTONOTE_REWARD_BLOCKS_WINDOW = 100;
        //	public static readonly uint CRYPTONOTE_BLOCK_GRANTED_FULL_REWARD_ZONE = 100000; //size of block (bytes) after which reward for block calculated using block size
        //	public static readonly uint CRYPTONOTE_BLOCK_GRANTED_FULL_REWARD_ZONE_V2 = 20000;
        //	public static readonly uint CRYPTONOTE_BLOCK_GRANTED_FULL_REWARD_ZONE_V1 = 10000;
        //	public static readonly uint CRYPTONOTE_BLOCK_GRANTED_FULL_REWARD_ZONE_CURRENT = CRYPTONOTE_BLOCK_GRANTED_FULL_REWARD_ZONE;
        //	public static readonly uint CRYPTONOTE_COINBASE_BLOB_RESERVED_SIZE = 600;

        //	public static readonly uint CRYPTONOTE_DISPLAY_DECIMAL_POINT = 2;

        //	public static readonly ulong MINIMUM_FEE = UINT64_C(10);

        //	/* This section defines our minimum and maximum mixin counts required for transactions */
        //	public static readonly ulong MINIMUM_MIXIN_V1 = 0;
        //	public static readonly ulong MAXIMUM_MIXIN_V1 = 100;

        //	public static readonly ulong MINIMUM_MIXIN_V2 = 7;
        //	public static readonly ulong MAXIMUM_MIXIN_V2 = 7;

        //	public static readonly ulong MINIMUM_MIXIN_V3 = 3;
        //	public static readonly ulong MAXIMUM_MIXIN_V3 = 3;

        //	/* The heights to activate the mixin limits at */
        //	public static readonly uint MIXIN_LIMITS_V1_HEIGHT = 440000;
        //	public static readonly uint MIXIN_LIMITS_V2_HEIGHT = 620000;
        //	public static readonly uint MIXIN_LIMITS_V3_HEIGHT = 800000;

        //	/* The mixin to use by default with zedwallet and turtle-service */
        //	/* DEFAULT_MIXIN_V0 is the mixin used before MIXIN_LIMITS_V1_HEIGHT is started */
        //	public static readonly ulong DEFAULT_MIXIN_V0 = 3;
        //	public static readonly ulong DEFAULT_MIXIN_V1 = MAXIMUM_MIXIN_V1;
        //	public static readonly ulong DEFAULT_MIXIN_V2 = MAXIMUM_MIXIN_V2;
        //	public static readonly ulong DEFAULT_MIXIN_V3 = MAXIMUM_MIXIN_V3;

        //	public static readonly ulong DEFAULT_DUST_THRESHOLD = UINT64_C(10);
        //	public static readonly ulong DEFAULT_DUST_THRESHOLD_V2 = UINT64_C(0);

        //	public static readonly uint DUST_THRESHOLD_V2_HEIGHT = MIXIN_LIMITS_V2_HEIGHT;
        //	public static readonly uint FUSION_DUST_THRESHOLD_HEIGHT_V2 = 800000;
        //	public static readonly ulong EXPECTED_NUMBER_OF_BLOCKS_PER_DAY = 24 * 60 * 60 / DIFFICULTY_TARGET;

        //	public static readonly uint DIFFICULTY_WINDOW = 17;
        //	public static readonly uint DIFFICULTY_WINDOW_V1 = 2880;
        //	public static readonly uint DIFFICULTY_WINDOW_V2 = 2880;
        //	public static readonly ulong DIFFICULTY_WINDOW_V3 = 60;
        //	public static readonly ulong DIFFICULTY_BLOCKS_COUNT_V3 = DIFFICULTY_WINDOW_V3 + 1;

        //	public static readonly uint DIFFICULTY_CUT = 0; // timestamps to cut after sorting
        //	public static readonly uint DIFFICULTY_CUT_V1 = 60;
        //	public static readonly uint DIFFICULTY_CUT_V2 = 60;
        //	public static readonly uint DIFFICULTY_LAG = 0; // !!!
        //	public static readonly uint DIFFICULTY_LAG_V1 = 15;
        //	public static readonly uint DIFFICULTY_LAG_V2 = 15;
        //	//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
        //	//static_assert(2 * DIFFICULTY_CUT <= DIFFICULTY_WINDOW - 2, "Bad DIFFICULTY_WINDOW or DIFFICULTY_CUT");

        //	public static readonly uint MAX_BLOCK_SIZE_INITIAL = 100000;
        //	public static readonly ulong MAX_BLOCK_SIZE_GROWTH_SPEED_NUMERATOR = 100 * 1024;
        //	public static readonly ulong MAX_BLOCK_SIZE_GROWTH_SPEED_DENOMINATOR = 365 * 24 * 60 * 60 / DIFFICULTY_TARGET;
        //	public static readonly ulong MAX_EXTRA_SIZE = 140000;

        //	public static readonly ulong CRYPTONOTE_LOCKED_TX_ALLOWED_DELTA_BLOCKS = 1;
        //	public static readonly ulong CRYPTONOTE_LOCKED_TX_ALLOWED_DELTA_SECONDS = DIFFICULTY_TARGET * CRYPTONOTE_LOCKED_TX_ALLOWED_DELTA_BLOCKS;

        //	public static readonly ulong CRYPTONOTE_MEMPOOL_TX_LIVETIME = 60 * 60 * 24; //seconds, one day
        //	public static readonly ulong CRYPTONOTE_MEMPOOL_TX_FROM_ALT_BLOCK_LIVETIME = 60 * 60 * 24 * 7; //seconds, one week
        //	public static readonly ulong CRYPTONOTE_NUMBER_OF_PERIODS_TO_FORGET_TX_DELETED_FROM_POOL = 7; // CRYPTONOTE_NUMBER_OF_PERIODS_TO_FORGET_TX_DELETED_FROM_POOL * CRYPTONOTE_MEMPOOL_TX_LIVETIME = time to forget tx

        //	public static readonly uint FUSION_TX_MAX_SIZE = CRYPTONOTE_BLOCK_GRANTED_FULL_REWARD_ZONE_CURRENT * 30 / 100;
        //	public static readonly uint FUSION_TX_MIN_INPUT_COUNT = 12;
        //	public static readonly uint FUSION_TX_MIN_IN_OUT_COUNT_RATIO = 4;

        //	public static readonly uint KEY_IMAGE_CHECKING_BLOCK_INDEX = 0;

        //	public static readonly uint UPGRADE_HEIGHT_V2 = 1;
        //	public static readonly uint UPGRADE_HEIGHT_V3 = 2;
        //	public static readonly uint UPGRADE_HEIGHT_V4 = 350000; // Upgrade height for CN-Lite Variant 1 switch.
        //	public static readonly uint UPGRADE_HEIGHT_CURRENT = UPGRADE_HEIGHT_V4;

        //	public static readonly uint UPGRADE_VOTING_THRESHOLD = 90; // percent
        //	public static readonly uint UPGRADE_VOTING_WINDOW = EXPECTED_NUMBER_OF_BLOCKS_PER_DAY; // blocks
        //	public static readonly uint UPGRADE_WINDOW = EXPECTED_NUMBER_OF_BLOCKS_PER_DAY; // blocks
        //	//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
        //	//static_assert(0 < UPGRADE_VOTING_THRESHOLD && UPGRADE_VOTING_THRESHOLD <= 100, "Bad UPGRADE_VOTING_THRESHOLD");
        //	//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
        //	//static_assert(UPGRADE_VOTING_WINDOW > 1, "Bad UPGRADE_VOTING_WINDOW");

        //	/* Block heights we are going to have hard forks at */
        //	public static readonly ulong[] FORK_HEIGHTS = {187000, 350000, 440000, 620000, 700000, 800000, 1000000, 1200000, 1400000, 1600000, 1800000, 2000000};

        //	/* MAKE SURE TO UPDATE THIS VALUE WITH EVERY MAJOR RELEASE BEFORE A FORK */
        //	public static readonly ulong SOFTWARE_SUPPORTED_FORK_INDEX = 6;

        //	public static readonly ulong FORK_HEIGHTS_SIZE = sizeof(FORK_HEIGHTS) / sizeofFORK_HEIGHTS;

        //	/* The index in the FORK_HEIGHTS array that this version of the software will
        //	   support. For example, if CURRENT_FORK_INDEX is 3, this version of the
        //	   software will support the fork at 600,000 blocks.

        //	   This will default to zero if the FORK_HEIGHTS array is empty, so you don't
        //	   need to change it manually. */
        ////C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
        //	public const byte CURRENT_FORK_INDEX = FORK_HEIGHTS_SIZE == 0 ? 0 : SOFTWARE_SUPPORTED_FORK_INDEX;

        //	//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
        //	//static_assert(CURRENT_FORK_INDEX >= 0, "CURRENT FORK INDEX must be >= 0");
        //	/* Make sure CURRENT_FORK_INDEX is a valid index, unless FORK_HEIGHTS is empty */
        //	//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
        //	//static_assert(FORK_HEIGHTS_SIZE == 0 || CURRENT_FORK_INDEX < FORK_HEIGHTS_SIZE, "CURRENT_FORK_INDEX out of range of FORK_HEIGHTS!");

        //	public const string CRYPTONOTE_BLOCKS_FILENAME = "blocks.bin";
        //	public const string CRYPTONOTE_BLOCKINDEXES_FILENAME = "blockindexes.bin";
        //	public const string CRYPTONOTE_POOLDATA_FILENAME = "poolstate.bin";
        //	public const string P2P_NET_DATA_FILENAME = "p2pstate.bin";
        //	public const string MINER_CONFIG_FILE_NAME = "miner_conf.json";
        //	}
        //}

        //namespace WalletConfig
        //{
        //	public static class GlobalMembers
        //	{
        //		/* The prefix your coins address starts with */
        //		public static readonly string addressPrefix = "TRTL";

        //		/* Your coins 'Ticker', e.g. Monero = XMR, Bitcoin = BTC */
        //		public static readonly string ticker = "TRTL";

        //		/* The filename to output the CSV to in save_csv */
        //		public static readonly string csvFilename = "transactions.csv";

        //		/* The filename to read+write the address book to - consider starting with
        //		   a leading '.' to make it hidden under mac+linux */
        //		public static readonly string addressBookFilename = ".addressBook.json";

        //		/* The name of your deamon */
        //		public static readonly string daemonName = "TurtleCoind";

        //		/* The name to call this wallet */
        //		public static readonly string walletName = "zedwallet";

        //		/* The name of service/walletd, the programmatic rpc interface to a
        //		   wallet */
        //		public static readonly string walletdName = "turtle-service";

        //		/* The full name of your crypto */
        //		public static readonly string coinName = CryptoNote.CRYPTONOTE_NAME;

        //		/* Where can your users contact you for support? E.g. discord */
        //		public static readonly string contactLink = "http://chat.turtlecoin.lol";


        //		/* The number of decimals your coin has */
        //		public static readonly int numDecimalPlaces = CryptoNote.parameters.CRYPTONOTE_DISPLAY_DECIMAL_POINT;


        //		/* The length of a standard address for your coin */
        //		public static readonly uint standardAddressLength = 99;

        //		/* The length of an integrated address for your coin - It's the same as
        //		   a normal address, but there is a paymentID included in there - since
        //		   payment ID's are 64 chars, and base58 encoding is done by encoding
        //		   chunks of 8 chars at once into blocks of 11 chars, we can calculate
        //		   this automatically */
        //		public static readonly uint integratedAddressLength = standardAddressLength + ((64 * 11) / 8);

        //		/* The default fee value to use with transactions (in ATOMIC units!) */
        //		public static readonly ulong defaultFee = CryptoNote.parameters.MINIMUM_FEE;

        //		/* The minimum fee value to allow with transactions (in ATOMIC units!) */
        //		public static readonly ulong minimumFee = CryptoNote.parameters.MINIMUM_FEE;

        //		/* The minimum amount allowed to be sent - usually 1 (in ATOMIC units!) */
        //		public static readonly ulong minimumSend = 1;

        //		/* Is a mixin of zero disabled on your network? */
        //		public static readonly bool mixinZeroDisabled = true;

        //		/* If a mixin of zero is disabled, at what height was it disabled? E.g.
        //		   fork height, or 0, if never allowed. This is ignored if a mixin of
        //		   zero is allowed */
        //		public static readonly ulong mixinZeroDisabledHeight = CryptoNote.parameters.MIXIN_LIMITS_V2_HEIGHT;
        //	}
        //}

        //namespace CRC32
        //{
        //	public static class GlobalMembers
        //	{
        //		/* oof */
        //		public static List<ulong> table = new List<ulong>() {0, 1996959894, 3993919788, 2567524794, 124634137, 1886057615, 3915621685, 2657392035, 249268274, 2044508324, 3772115230, 2547177864, 162941995, 2125561021, 3887607047, 2428444049, 498536548, 1789927666, 4089016648, 2227061214, 450548861, 1843258603, 4107580753, 2211677639, 325883990, 1684777152, 4251122042, 2321926636, 335633487, 1661365465, 4195302755, 2366115317, 997073096, 1281953886, 3579855332, 2724688242, 1006888145, 1258607687, 3524101629, 2768942443, 901097722, 1119000684, 3686517206, 2898065728, 853044451, 1172266101, 3705015759, 2882616665, 651767980, 1373503546, 3369554304, 3218104598, 565507253, 1454621731, 3485111705, 3099436303, 671266974, 1594198024, 3322730930, 2970347812, 795835527, 1483230225, 3244367275, 3060149565, 1994146192, 31158534, 2563907772, 4023717930, 1907459465, 112637215, 2680153253, 3904427059, 2013776290, 251722036, 2517215374, 3775830040, 2137656763, 141376813, 2439277719, 3865271297, 1802195444, 476864866, 2238001368, 4066508878, 1812370925, 453092731, 2181625025, 4111451223, 1706088902, 314042704, 2344532202, 4240017532, 1658658271, 366619977, 2362670323, 4224994405, 1303535960, 984961486, 2747007092, 3569037538, 1256170817, 1037604311, 2765210733, 3554079995, 1131014506, 879679996, 2909243462, 3663771856, 1141124467, 855842277, 2852801631, 3708648649, 1342533948, 654459306, 3188396048, 3373015174, 1466479909, 544179635, 3110523913, 3462522015, 1591671054, 702138776, 2966460450, 3352799412, 1504918807, 783551873, 3082640443, 3233442989, 3988292384, 2596254646, 62317068, 1957810842, 3939845945, 2647816111, 81470997, 1943803523, 3814918930, 2489596804, 225274430, 2053790376, 3826175755, 2466906013, 167816743, 2097651377, 4027552580, 2265490386, 503444072, 1762050814, 4150417245, 2154129355, 426522225, 1852507879, 4275313526, 2312317920, 282753626, 1742555852, 4189708143, 2394877945, 397917763, 1622183637, 3604390888, 2714866558, 953729732, 1340076626, 3518719985, 2797360999, 1068828381, 1219638859, 3624741850, 2936675148, 906185462, 1090812512, 3747672003, 2825379669, 829329135, 1181335161, 3412177804, 3160834842, 628085408, 1382605366, 3423369109, 3138078467, 570562233, 1426400815, 3317316542, 2998733608, 733239954, 1555261956, 3268935591, 3050360625, 752459403, 1541320221, 2607071920, 3965973030, 1969922972, 40735498, 2617837225, 3943577151, 1913087877, 83908371, 2512341634, 3803740692, 2075208622, 213261112, 2463272603, 3855990285, 2094854071, 198958881, 2262029012, 4057260610, 1759359992, 534414190, 2176718541, 4139329115, 1873836001, 414664567, 2282248934, 4279200368, 1711684554, 285281116, 2405801727, 4167216745, 1634467795, 376229701, 2685067896, 3608007406, 1308918612, 956543938, 2808555105, 3495958263, 1231636301, 1047427035, 2932959818, 3654703836, 1088359270, 936918000, 2847714899, 3736837829, 1202900863, 817233897, 3183342108, 3401237130, 1404277552, 615818150, 3134207493, 3453421203, 1423857449, 601450431, 3009837614, 3294710456, 1567103746, 711928724, 3020668471, 3272380065, 1510334235, 755167117};

        //		public static ulong crc32(string input)
        //		{
        //			ulong crc = 0xFFFFFFFF;

        //			foreach (char c in input)
        //			{
        //				ulong byteIndex = (c ^ crc) & 0xff;
        //				crc = ((crc >> 8) ^ table[byteIndex]);
        //			}

        //			return crc ^ 0xFFFFFFFF;
        //		}
        //	}
        //}

        //namespace Mnemonics.WordList
        //{
        //	public static class GlobalMembers
        //	{
        //			public static readonly List<string> English = new List<string>() {"abbey", "abducts", "ability", "ablaze", "abnormal", "abort", "abrasive", "absorb", "abyss", "academy", "aces", "aching", "acidic", "acoustic", "acquire", "across", "actress", "acumen", "adapt", "addicted", "adept", "adhesive", "adjust", "adopt", "adrenalin", "adult", "adventure", "aerial", "afar", "affair", "afield", "afloat", "afoot", "afraid", "after", "against", "agenda", "aggravate", "agile", "aglow", "agnostic", "agony", "agreed", "ahead", "aided", "ailments", "aimless", "airport", "aisle", "ajar", "akin", "alarms", "album", "alchemy", "alerts", "algebra", "alkaline", "alley", "almost", "aloof", "alpine", "already", "also", "altitude", "alumni", "always", "amaze", "ambush", "amended", "amidst", "ammo", "amnesty", "among", "amply", "amused", "anchor", "android", "anecdote", "angled", "ankle", "annoyed", "answers", "antics", "anvil", "anxiety", "anybody", "apart", "apex", "aphid", "aplomb", "apology", "apply", "apricot", "aptitude", "aquarium", "arbitrary", "archer", "ardent", "arena", "argue", "arises", "army", "around", "arrow", "arsenic", "artistic", "ascend", "ashtray", "aside", "asked", "asleep", "aspire", "assorted", "asylum", "athlete", "atlas", "atom", "atrium", "attire", "auburn", "auctions", "audio", "august", "aunt", "austere", "autumn", "avatar", "avidly", "avoid", "awakened", "awesome", "awful", "awkward", "awning", "awoken", "axes", "axis", "axle", "aztec", "azure", "baby", "bacon", "badge", "baffles", "bagpipe", "bailed", "bakery", "balding", "bamboo", "banjo", "baptism", "basin", "batch", "bawled", "bays", "because", "beer", "befit", "begun", "behind", "being", "below", "bemused", "benches", "berries", "bested", "betting", "bevel", "beware", "beyond", "bias", "bicycle", "bids", "bifocals", "biggest", "bikini", "bimonthly", "binocular", "biology", "biplane", "birth", "biscuit", "bite", "biweekly", "blender", "blip", "bluntly", "boat", "bobsled", "bodies", "bogeys", "boil", "boldly", "bomb", "border", "boss", "both", "bounced", "bovine", "bowling", "boxes", "boyfriend", "broken", "brunt", "bubble", "buckets", "budget", "buffet", "bugs", "building", "bulb", "bumper", "bunch", "business", "butter", "buying", "buzzer", "bygones", "byline", "bypass", "cabin", "cactus", "cadets", "cafe", "cage", "cajun", "cake", "calamity", "camp", "candy", "casket", "catch", "cause", "cavernous", "cease", "cedar", "ceiling", "cell", "cement", "cent", "certain", "chlorine", "chrome", "cider", "cigar", "cinema", "circle", "cistern", "citadel", "civilian", "claim", "click", "clue", "coal", "cobra", "cocoa", "code", "coexist", "coffee", "cogs", "cohesive", "coils", "colony", "comb", "cool", "copy", "corrode", "costume", "cottage", "cousin", "cowl", "criminal", "cube", "cucumber", "cuddled", "cuffs", "cuisine", "cunning", "cupcake", "custom", "cycling", "cylinder", "cynical", "dabbing", "dads", "daft", "dagger", "daily", "damp", "dangerous", "dapper", "darted", "dash", "dating", "dauntless", "dawn", "daytime", "dazed", "debut", "decay", "dedicated", "deepest", "deftly", "degrees", "dehydrate", "deity", "dejected", "delayed", "demonstrate", "dented", "deodorant", "depth", "desk", "devoid", "dewdrop", "dexterity", "dialect", "dice", "diet", "different", "digit", "dilute", "dime", "dinner", "diode", "diplomat", "directed", "distance", "ditch", "divers", "dizzy", "doctor", "dodge", "does", "dogs", "doing", "dolphin", "domestic", "donuts", "doorway", "dormant", "dosage", "dotted", "double", "dove", "down", "dozen", "dreams", "drinks", "drowning", "drunk", "drying", "dual", "dubbed", "duckling", "dude", "duets", "duke", "dullness", "dummy", "dunes", "duplex", "duration", "dusted", "duties", "dwarf", "dwelt", "dwindling", "dying", "dynamite", "dyslexic", "each", "eagle", "earth", "easy", "eating", "eavesdrop", "eccentric", "echo", "eclipse", "economics", "ecstatic", "eden", "edgy", "edited", "educated", "eels", "efficient", "eggs", "egotistic", "eight", "either", "eject", "elapse", "elbow", "eldest", "eleven", "elite", "elope", "else", "eluded", "emails", "ember", "emerge", "emit", "emotion", "empty", "emulate", "energy", "enforce", "enhanced", "enigma", "enjoy", "enlist", "enmity", "enough", "enraged", "ensign", "entrance", "envy", "epoxy", "equip", "erase", "erected", "erosion", "error", "eskimos", "espionage", "essential", "estate", "etched", "eternal", "ethics", "etiquette", "evaluate", "evenings", "evicted", "evolved", "examine", "excess", "exhale", "exit", "exotic", "exquisite", "extra", "exult", "fabrics", "factual", "fading", "fainted", "faked", "fall", "family", "fancy", "farming", "fatal", "faulty", "fawns", "faxed", "fazed", "feast", "february", "federal", "feel", "feline", "females", "fences", "ferry", "festival", "fetches", "fever", "fewest", "fiat", "fibula", "fictional", "fidget", "fierce", "fifteen", "fight", "films", "firm", "fishing", "fitting", "five", "fixate", "fizzle", "fleet", "flippant", "flying", "foamy", "focus", "foes", "foggy", "foiled", "folding", "fonts", "foolish", "fossil", "fountain", "fowls", "foxes", "foyer", "framed", "friendly", "frown", "fruit", "frying", "fudge", "fuel", "fugitive", "fully", "fuming", "fungal", "furnished", "fuselage", "future", "fuzzy", "gables", "gadget", "gags", "gained", "galaxy", "gambit", "gang", "gasp", "gather", "gauze", "gave", "gawk", "gaze", "gearbox", "gecko", "geek", "gels", "gemstone", "general", "geometry", "germs", "gesture", "getting", "geyser", "ghetto", "ghost", "giant", "giddy", "gifts", "gigantic", "gills", "gimmick", "ginger", "girth", "giving", "glass", "gleeful", "glide", "gnaw", "gnome", "goat", "goblet", "godfather", "goes", "goggles", "going", "goldfish", "gone", "goodbye", "gopher", "gorilla", "gossip", "gotten", "gourmet", "governing", "gown", "greater", "grunt", "guarded", "guest", "guide", "gulp", "gumball", "guru", "gusts", "gutter", "guys", "gymnast", "gypsy", "gyrate", "habitat", "hacksaw", "haggled", "hairy", "hamburger", "happens", "hashing", "hatchet", "haunted", "having", "hawk", "haystack", "hazard", "hectare", "hedgehog", "heels", "hefty", "height", "hemlock", "hence", "heron", "hesitate", "hexagon", "hickory", "hiding", "highway", "hijack", "hiker", "hills", "himself", "hinder", "hippo", "hire", "history", "hitched", "hive", "hoax", "hobby", "hockey", "hoisting", "hold", "honked", "hookup", "hope", "hornet", "hospital", "hotel", "hounded", "hover", "howls", "hubcaps", "huddle", "huge", "hull", "humid", "hunter", "hurried", "husband", "huts", "hybrid", "hydrogen", "hyper", "iceberg", "icing", "icon", "identity", "idiom", "idled", "idols", "igloo", "ignore", "iguana", "illness", "imagine", "imbalance", "imitate", "impel", "inactive", "inbound", "incur", "industrial", "inexact", "inflamed", "ingested", "initiate", "injury", "inkling", "inline", "inmate", "innocent", "inorganic", "input", "inquest", "inroads", "insult", "intended", "inundate", "invoke", "inwardly", "ionic", "irate", "iris", "irony", "irritate", "island", "isolated", "issued", "italics", "itches", "items", "itinerary", "itself", "ivory", "jabbed", "jackets", "jaded", "jagged", "jailed", "jamming", "january", "jargon", "jaunt", "javelin", "jaws", "jazz", "jeans", "jeers", "jellyfish", "jeopardy", "jerseys", "jester", "jetting", "jewels", "jigsaw", "jingle", "jittery", "jive", "jobs", "jockey", "jogger", "joining", "joking", "jolted", "jostle", "journal", "joyous", "jubilee", "judge", "juggled", "juicy", "jukebox", "july", "jump", "junk", "jury", "justice", "juvenile", "kangaroo", "karate", "keep", "kennel", "kept", "kernels", "kettle", "keyboard", "kickoff", "kidneys", "king", "kiosk", "kisses", "kitchens", "kiwi", "knapsack", "knee", "knife", "knowledge", "knuckle", "koala", "laboratory", "ladder", "lagoon", "lair", "lakes", "lamb", "language", "laptop", "large", "last", "later", "launching", "lava", "lawsuit", "layout", "lazy", "lectures", "ledge", "leech", "left", "legion", "leisure", "lemon", "lending", "leopard", "lesson", "lettuce", "lexicon", "liar", "library", "licks", "lids", "lied", "lifestyle", "light", "likewise", "lilac", "limits", "linen", "lion", "lipstick", "liquid", "listen", "lively", "loaded", "lobster", "locker", "lodge", "lofty", "logic", "loincloth", "long", "looking", "lopped", "lordship", "losing", "lottery", "loudly", "love", "lower", "loyal", "lucky", "luggage", "lukewarm", "lullaby", "lumber", "lunar", "lurk", "lush", "luxury", "lymph", "lynx", "lyrics", "macro", "madness", "magically", "mailed", "major", "makeup", "malady", "mammal", "maps", "masterful", "match", "maul", "maverick", "maximum", "mayor", "maze", "meant", "mechanic", "medicate", "meeting", "megabyte", "melting", "memoir", "menu", "merger", "mesh", "metro", "mews", "mice", "midst", "mighty", "mime", "mirror", "misery", "mittens", "mixture", "moat", "mobile", "mocked", "mohawk", "moisture", "molten", "moment", "money", "moon", "mops", "morsel", "mostly", "motherly", "mouth", "movement", "mowing", "much", "muddy", "muffin", "mugged", "mullet", "mumble", "mundane", "muppet", "mural", "musical", "muzzle", "myriad", "mystery", "myth", "nabbing", "nagged", "nail", "names", "nanny", "napkin", "narrate", "nasty", "natural", "nautical", "navy", "nearby", "necklace", "needed", "negative", "neither", "neon", "nephew", "nerves", "nestle", "network", "neutral", "never", "newt", "nexus", "nibs", "niche", "niece", "nifty", "nightly", "nimbly", "nineteen", "nirvana", "nitrogen", "nobody", "nocturnal", "nodes", "noises", "nomad", "noodles", "northern", "nostril", "noted", "nouns", "novelty", "nowhere", "nozzle", "nuance", "nucleus", "nudged", "nugget", "nuisance", "null", "number", "nuns", "nurse", "nutshell", "nylon", "oaks", "oars", "oasis", "oatmeal", "obedient", "object", "obliged", "obnoxious", "observant", "obtains", "obvious", "occur", "ocean", "october", "odds", "odometer", "offend", "often", "oilfield", "ointment", "okay", "older", "olive", "olympics", "omega", "omission", "omnibus", "onboard", "oncoming", "oneself", "ongoing", "onion", "online", "onslaught", "onto", "onward", "oozed", "opacity", "opened", "opposite", "optical", "opus", "orange", "orbit", "orchid", "orders", "organs", "origin", "ornament", "orphans", "oscar", "ostrich", "otherwise", "otter", "ouch", "ought", "ounce", "ourselves", "oust", "outbreak", "oval", "oven", "owed", "owls", "owner", "oxidant", "oxygen", "oyster", "ozone", "pact", "paddles", "pager", "pairing", "palace", "pamphlet", "pancakes", "paper", "paradise", "pastry", "patio", "pause", "pavements", "pawnshop", "payment", "peaches", "pebbles", "peculiar", "pedantic", "peeled", "pegs", "pelican", "pencil", "people", "pepper", "perfect", "pests", "petals", "phase", "pheasants", "phone", "phrases", "physics", "piano", "picked", "pierce", "pigment", "piloted", "pimple", "pinched", "pioneer", "pipeline", "pirate", "pistons", "pitched", "pivot", "pixels", "pizza", "playful", "pledge", "pliers", "plotting", "plus", "plywood", "poaching", "pockets", "podcast", "poetry", "point", "poker", "polar", "ponies", "pool", "popular", "portents", "possible", "potato", "pouch", "poverty", "powder", "pram", "present", "pride", "problems", "pruned", "prying", "psychic", "public", "puck", "puddle", "puffin", "pulp", "pumpkins", "punch", "puppy", "purged", "push", "putty", "puzzled", "pylons", "pyramid", "python", "queen", "quick", "quote", "rabbits", "racetrack", "radar", "rafts", "rage", "railway", "raking", "rally", "ramped", "randomly", "rapid", "rarest", "rash", "rated", "ravine", "rays", "razor", "react", "rebel", "recipe", "reduce", "reef", "refer", "regular", "reheat", "reinvest", "rejoices", "rekindle", "relic", "remedy", "renting", "reorder", "repent", "request", "reruns", "rest", "return", "reunion", "revamp", "rewind", "rhino", "rhythm", "ribbon", "richly", "ridges", "rift", "rigid", "rims", "ringing", "riots", "ripped", "rising", "ritual", "river", "roared", "robot", "rockets", "rodent", "rogue", "roles", "romance", "roomy", "roped", "roster", "rotate", "rounded", "rover", "rowboat", "royal", "ruby", "rudely", "ruffled", "rugged", "ruined", "ruling", "rumble", "runway", "rural", "rustled", "ruthless", "sabotage", "sack", "sadness", "safety", "saga", "sailor", "sake", "salads", "sample", "sanity", "sapling", "sarcasm", "sash", "satin", "saucepan", "saved", "sawmill", "saxophone", "sayings", "scamper", "scenic", "school", "science", "scoop", "scrub", "scuba", "seasons", "second", "sedan", "seeded", "segments", "seismic", "selfish", "semifinal", "sensible", "september", "sequence", "serving", "session", "setup", "seventh", "sewage", "shackles", "shelter", "shipped", "shocking", "shrugged", "shuffled", "shyness", "siblings", "sickness", "sidekick", "sieve", "sifting", "sighting", "silk", "simplest", "sincerely", "sipped", "siren", "situated", "sixteen", "sizes", "skater", "skew", "skirting", "skulls", "skydive", "slackens", "sleepless", "slid", "slower", "slug", "smash", "smelting", "smidgen", "smog", "smuggled", "snake", "sneeze", "sniff", "snout", "snug", "soapy", "sober", "soccer", "soda", "software", "soggy", "soil", "solved", "somewhere", "sonic", "soothe", "soprano", "sorry", "southern", "sovereign", "sowed", "soya", "space", "speedy", "sphere", "spiders", "splendid", "spout", "sprig", "spud", "spying", "square", "stacking", "stellar", "stick", "stockpile", "strained", "stunning", "stylishly", "subtly", "succeed", "suddenly", "suede", "suffice", "sugar", "suitcase", "sulking", "summon", "sunken", "superior", "surfer", "sushi", "suture", "swagger", "swept", "swiftly", "sword", "swung", "syllabus", "symptoms", "syndrome", "syringe", "system", "taboo", "tacit", "tadpoles", "tagged", "tail", "taken", "talent", "tamper", "tanks", "tapestry", "tarnished", "tasked", "tattoo", "taunts", "tavern", "tawny", "taxi", "teardrop", "technical", "tedious", "teeming", "tell", "template", "tender", "tepid", "tequila", "terminal", "testing", "tether", "textbook", "thaw", "theatrics", "thirsty", "thorn", "threaten", "thumbs", "thwart", "ticket", "tidy", "tiers", "tiger", "tilt", "timber", "tinted", "tipsy", "tirade", "tissue", "titans", "toaster", "tobacco", "today", "toenail", "toffee", "together", "toilet", "token", "tolerant", "tomorrow", "tonic", "toolbox", "topic", "torch", "tossed", "total", "touchy", "towel", "toxic", "toyed", "trash", "trendy", "tribal", "trolling", "truth", "trying", "tsunami", "tubes", "tucks", "tudor", "tuesday", "tufts", "tugs", "tuition", "tulips", "tumbling", "tunnel", "turnip", "tusks", "tutor", "tuxedo", "twang", "tweezers", "twice", "twofold", "tycoon", "typist", "tyrant", "ugly", "ulcers", "ultimate", "umbrella", "umpire", "unafraid", "unbending", "uncle", "under", "uneven", "unfit", "ungainly", "unhappy", "union", "unjustly", "unknown", "unlikely", "unmask", "unnoticed", "unopened", "unplugs", "unquoted", "unrest", "unsafe", "until", "unusual", "unveil", "unwind", "unzip", "upbeat", "upcoming", "update", "upgrade", "uphill", "upkeep", "upload", "upon", "upper", "upright", "upstairs", "uptight", "upwards", "urban", "urchins", "urgent", "usage", "useful", "usher", "using", "usual", "utensils", "utility", "utmost", "utopia", "uttered", "vacation", "vague", "vain", "value", "vampire", "vane", "vapidly", "vary", "vastness", "vats", "vaults", "vector", "veered", "vegan", "vehicle", "vein", "velvet", "venomous", "verification", "vessel", "veteran", "vexed", "vials", "vibrate", "victim", "video", "viewpoint", "vigilant", "viking", "village", "vinegar", "violin", "vipers", "virtual", "visited", "vitals", "vivid", "vixen", "vocal", "vogue", "voice", "volcano", "vortex", "voted", "voucher", "vowels", "voyage", "vulture", "wade", "waffle", "wagtail", "waist", "waking", "wallets", "wanted", "warped", "washing", "water", "waveform", "waxing", "wayside", "weavers", "website", "wedge", "weekday", "weird", "welders", "went", "wept", "were", "western", "wetsuit", "whale", "when", "whipped", "whole", "wickets", "width", "wield", "wife", "wiggle", "wildly", "winter", "wipeout", "wiring", "wise", "withdrawn", "wives", "wizard", "wobbly", "woes", "woken", "wolf", "womanly", "wonders", "woozy", "worry", "wounded", "woven", "wrap", "wrist", "wrong", "yacht", "yahoo", "yanks", "yard", "yawning", "yearbook", "yellow", "yesterday", "yeti", "yields", "yodel", "yoga", "younger", "yoyo", "zapped", "zeal", "zebra", "zero", "zesty", "zigzags", "zinger", "zippers", "zodiac", "zombie", "zones", "zoom"};
        //	}
        //}

        //namespace std
        //{
        //	public static class GlobalMembers
        //	{
        //	public static std::ostream operator << (std::ostream s, CryptoNote.CryptoNoteConnectionContext context)
        //	{
        //	  return s << "[" << Common.ipAddressToString(context.m_remote_ip) << ":" << (int)context.m_remote_port << (context.m_is_income ? " INC" : " OUT") << "] ";
        //	}
        //	}
        //}

        //namespace System.Detail
        //{
        //	public static class GlobalMembers
        //	{
        ////C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
        //	public static Future<T> async<T>(Func<T>&& operation)
        //	{
        //	  return std::async(std::launch.async, std::move(operation));
        //	}
        //	}
    //}