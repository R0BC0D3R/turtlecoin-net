// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// This file is part of Bytecoin.
//
// Bytecoin is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Bytecoin is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.



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
