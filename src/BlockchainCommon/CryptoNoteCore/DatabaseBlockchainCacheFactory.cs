// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class IDataBase;

public class DatabaseBlockchainCacheFactory: IBlockchainCacheFactory
{
  public DatabaseBlockchainCacheFactory(IDataBase database, Logging.ILogger logger)
  {
	  this.database = new CryptoNote.IDataBase(database);
	  this.logger = new Logging.ILogger(logger);

  }
  public override void Dispose()
  {

	  base.Dispose();
  }

  public override std::unique_ptr<IBlockchainCache> createRootBlockchainCache(Currency currency)
  {
	return std::unique_ptr<IBlockchainCache> (new DatabaseBlockchainCache(currency, database, this, logger));
  }
  public override std::unique_ptr<IBlockchainCache> createBlockchainCache(Currency currency, IBlockchainCache parent, uint32_t startIndex = 0)
  {
	return std::unique_ptr<IBlockchainCache> (new BlockchainCache("", currency, logger, parent, new uint32_t(startIndex)));
  }

  private IDataBase database;
  private Logging.ILogger logger;
}

} //namespace CryptoNote



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

