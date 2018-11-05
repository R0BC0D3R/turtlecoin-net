// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

namespace CryptoNote
{

public class CoreStatistics
{
  public ulong transactionPoolSize;
  public ulong blockchainHeight;
  public ulong miningSpeed;
  public ulong alternativeBlockCount;
  public string topBlockHashString;

  public void serialize(ISerializer s)
  {
	s.functorMethod(transactionPoolSize, "tx_pool_size");
	s.functorMethod(blockchainHeight, "blockchain_height");
	s.functorMethod(miningSpeed, "mining_speed");
	s.functorMethod(alternativeBlockCount, "alternative_blocks");
	s.functorMethod(topBlockHashString, "top_block_id_str");
  }
}

}
