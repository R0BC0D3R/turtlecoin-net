// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


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

using CryptoNote;

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
//ORIGINAL LINE: ushort getBackgroundThreadsCount() const
  public ushort getBackgroundThreadsCount()
  {
	return backgroundThreadsCount;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint getMaxOpenFiles() const
  public uint getMaxOpenFiles()
  {
	return maxOpenFiles;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong getWriteBufferSize() const
  public ulong getWriteBufferSize()
  {
	return writeBufferSize;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong getReadCacheSize() const
  public ulong getReadCacheSize()
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
  public void setBackgroundThreadsCount(ushort backgroundThreadsCount)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this->backgroundThreadsCount = backgroundThreadsCount;
	this.backgroundThreadsCount.CopyFrom(backgroundThreadsCount);
  }
  public void setMaxOpenFiles(uint maxOpenFiles)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this->maxOpenFiles = maxOpenFiles;
	this.maxOpenFiles.CopyFrom(maxOpenFiles);
  }
  public void setWriteBufferSize(ulong writeBufferSize)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this->writeBufferSize = writeBufferSize;
	this.writeBufferSize.CopyFrom(writeBufferSize);
  }
  public void setReadCacheSize(ulong readCacheSize)
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
  private ushort backgroundThreadsCount = new ushort();
  private uint maxOpenFiles = new uint();
  private ulong writeBufferSize = new ulong();
  private ulong readCacheSize = new ulong();
  private bool testnet;
}
} //namespace CryptoNote

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace