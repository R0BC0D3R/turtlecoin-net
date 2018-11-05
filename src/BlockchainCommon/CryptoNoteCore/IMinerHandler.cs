// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{
  public abstract class IMinerHandler : System.IDisposable
  {
	public abstract bool handle_block_found(BlockTemplate b);
	public abstract bool get_block_template(BlockTemplate b, AccountPublicAddress adr, ref ulong diffic, ref uint height, BinaryArray ex_nonce);

	public void Dispose()
	{
	}
  }
}
