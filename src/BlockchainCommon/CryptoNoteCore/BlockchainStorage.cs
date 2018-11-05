// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

using CryptoNote;

namespace CryptoNote
{

//TODO: rename this class since it's not persistent blockchain storage!
public class BlockchainStorage : System.IDisposable
{

  public abstract class IBlockchainStorageInternal : System.IDisposable
  {
	public virtual void Dispose()
	{
	}

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
	public abstract void pushBlock(RawBlock && rawBlock);

	//Returns IBlockchainStorageInternal with elements from [splitIndex, blocks.size() - 1].
	//Original IBlockchainStorageInternal will contain elements from [0, splitIndex - 1].
	public abstract std::unique_ptr<IBlockchainStorageInternal> splitStorage(uint32_t splitIndex);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual RawBlock getBlockByIndex(uint32_t index) const = 0;
	public abstract RawBlock getBlockByIndex(uint32_t index);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint32_t getBlockCount() const = 0;
	public abstract uint32_t getBlockCount();
  }

  public BlockchainStorage(uint32_t reserveSize)
  {
	  this.internalStorage = new MemoryBlockchainStorage(new uint32_t(reserveSize));
  }
  public BlockchainStorage(string indexFileName, string dataFileName)
  {
	  this.internalStorage = new SwappedBlockchainStorage(indexFileName, dataFileName);
  }
  public virtual void Dispose()
  {
  }

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public virtual void pushBlock(RawBlock && rawBlock)
  {
	internalStorage.pushBlock(std::move(rawBlock));
  }

  //Returns BlockchainStorage with elements from [splitIndex, blocks.size() - 1].
  //Original BlockchainStorage will contain elements from [0, splitIndex - 1].

  //Returns MemoryBlockchainStorage with elements from [splitIndex, blocks.size() - 1].
  //Original MemoryBlockchainStorage will contain elements from [0, splitIndex - 1].
  public virtual std::unique_ptr<BlockchainStorage> splitStorage(uint32_t splitIndex)
  {
	std::unique_ptr<BlockchainStorage> newStorage = new std::unique_ptr<BlockchainStorage>(new BlockchainStorage(internalStorage.splitStorage(splitIndex)));
	return newStorage;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual RawBlock getBlockByIndex(uint32_t index) const
  public virtual RawBlock getBlockByIndex(uint32_t index)
  {
	return internalStorage.getBlockByIndex(index);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint32_t getBlockCount() const
  public virtual uint32_t getBlockCount()
  {
	return internalStorage.getBlockCount();
  }

  private std::unique_ptr<IBlockchainStorageInternal> internalStorage = new std::unique_ptr<IBlockchainStorageInternal>();

  private BlockchainStorage(std::unique_ptr<IBlockchainStorageInternal> storage)
  {
	  this.internalStorage = std::move(storage);
  }
}

}
