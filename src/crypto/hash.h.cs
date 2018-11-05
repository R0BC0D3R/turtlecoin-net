#define CN_PAGE_SIZE

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2014-2018, The Aeon Project
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.




// Standard Cryptonight Definitions

// Standard CryptoNight Lite Definitions

// CryptoNight Soft Shell Definitions
										  // ultimately determines how big our sine wave is. A smaller value means a bigger wave
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_ITER (CN_SOFT_SHELL_MEMORY / 2)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_PAD_MULTIPLIER (CN_SOFT_SHELL_WINDOW / CN_SOFT_SHELL_MULTIPLIER)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_ITER_MULTIPLIER (CN_SOFT_SHELL_PAD_MULTIPLIER / 2)

//C++ TO C# CONVERTER TODO TASK: C# does not allow setting or comparing #define constants:
#if (((CN_SOFT_SHELL_WINDOW * CN_SOFT_SHELL_PAD_MULTIPLIER) + CN_SOFT_SHELL_MEMORY) > CN_PAGE_SIZE)
#error The CryptoNight Soft Shell Parameters you supplied will exceed normal paging operations.
#endif


//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
CRYPTO_MAKE_HASHABLE(Hash)
