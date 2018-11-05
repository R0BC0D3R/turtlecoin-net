// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{

  public class BlockInfo
  {
	public uint height;
	public Crypto.Hash id = new Crypto.Hash();

	public BlockInfo()
	{
	  clear();
	}

	public void clear()
	{
	  height = 0;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: id = CryptoNote::NULL_HASH;
	  id.CopyFrom(CryptoNote.GlobalMembers.NULL_HASH);
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool empty() const
	public bool empty()
	{
	  return id == CryptoNote.GlobalMembers.NULL_HASH;
	}
  }

  public abstract class ITransactionValidator : System.IDisposable
  {
	public virtual void Dispose()
	{
	}

	public abstract bool checkTransactionInputs(CryptoNote.Transaction tx, BlockInfo maxUsedBlock);
	public abstract bool checkTransactionInputs(CryptoNote.Transaction tx, BlockInfo maxUsedBlock, BlockInfo lastFailed);
	public abstract bool haveSpentKeyImages(CryptoNote.Transaction tx);
	public abstract bool checkTransactionSize(uint blobSize);
  }

}
