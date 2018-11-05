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

using CryptoNote;

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.



namespace CryptoNote
{

public class DataBaseConfig
{
  public DataBaseConfig()
  {
	  this.dataDir = Tools.getDefaultDataDirectory();
	  this.backgroundThreadsCount = DATABASE_DEFAULT_BACKGROUND_THREADS_COUNT;
	  this.maxOpenFiles = DATABASE_DEFAULT_MAX_OPEN_FILES;
	  this.writeBufferSize = DATABASE_WRITE_BUFFER_MB_DEFAULT_SIZE * GlobalMembers.MEGABYTE;
	  this.readCacheSize = DATABASE_READ_BUFFER_MB_DEFAULT_SIZE * GlobalMembers.MEGABYTE;
	  this.testnet = false;
  }
  public bool init(string dataDirectory, int backgroundThreads, int openFiles, int writeBuffer, int readCache)
  {
	dataDir = dataDirectory;
	backgroundThreadsCount = backgroundThreads;
	maxOpenFiles = openFiles;
	writeBufferSize = writeBuffer;
	readCacheSize = readCache;

	if (dataDir == Tools.getDefaultDataDirectory())
	{
	  configFolderDefaulted = true;
	}

	return true;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isConfigFolderDefaulted() const
  public bool isConfigFolderDefaulted()
  {
	return configFolderDefaulted;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: string getDataDir() const
  public string getDataDir()
  {
	return dataDir;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint16_t getBackgroundThreadsCount() const
  public uint16_t getBackgroundThreadsCount()
  {
	return backgroundThreadsCount;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint32_t getMaxOpenFiles() const
  public uint32_t getMaxOpenFiles()
  {
	return maxOpenFiles;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t getWriteBufferSize() const
  public uint64_t getWriteBufferSize()
  {
	return writeBufferSize;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t getReadCacheSize() const
  public uint64_t getReadCacheSize()
  {
	return readCacheSize;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool getTestnet() const
  public bool getTestnet()
  {
	return testnet;
  }

  public void setConfigFolderDefaulted(bool defaulted)
  {
	configFolderDefaulted = defaulted;
  }
  public void setDataDir(string dataDir)
  {
	this.dataDir = dataDir;
  }
  public void setBackgroundThreadsCount(uint16_t backgroundThreadsCount)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this->backgroundThreadsCount = backgroundThreadsCount;
	this.backgroundThreadsCount.CopyFrom(backgroundThreadsCount);
  }
  public void setMaxOpenFiles(uint32_t maxOpenFiles)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this->maxOpenFiles = maxOpenFiles;
	this.maxOpenFiles.CopyFrom(maxOpenFiles);
  }
  public void setWriteBufferSize(uint64_t writeBufferSize)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this->writeBufferSize = writeBufferSize;
	this.writeBufferSize.CopyFrom(writeBufferSize);
  }
  public void setReadCacheSize(uint64_t readCacheSize)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this->readCacheSize = readCacheSize;
	this.readCacheSize.CopyFrom(readCacheSize);
  }
  public void setTestnet(bool testnet)
  {
	this.testnet = testnet;
  }

  private bool configFolderDefaulted;
  private string dataDir;
  private uint16_t backgroundThreadsCount = new uint16_t();
  private uint32_t maxOpenFiles = new uint32_t();
  private uint64_t writeBufferSize = new uint64_t();
  private uint64_t readCacheSize = new uint64_t();
  private bool testnet;
}
} //namespace CryptoNote

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace