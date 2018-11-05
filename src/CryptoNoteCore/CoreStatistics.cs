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
