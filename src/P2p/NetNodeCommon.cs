using System;

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.



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



public class NetworkAddress
{
	public uint ip;
	public uint port;
}

public class PeerlistEntry
{
	public NetworkAddress adr = new NetworkAddress();
	public ulong id;
	public ulong last_seen;
}

public class connection_entry
{
	public NetworkAddress adr = new NetworkAddress();
	public ulong id;
	public bool is_income;
}


namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//  struct CryptoNoteConnectionContext;

  public interface IP2pEndpoint
  {
	void relay_notify_to_all(int command, BinaryArray data_buff, boost::uuids.uuid excludeConnection);
	bool invoke_notify_to_peer(int command, BinaryArray req_buff, CryptoNote.CryptoNoteConnectionContext context);
	ulong get_connections_count();
	void for_each_connection(Action<CryptoNote.CryptoNoteConnectionContext , ulong> f);
	// can be called from external threads
	void externalRelayNotifyToAll(int command, BinaryArray data_buff, boost::uuids.uuid excludeConnection);
  }

  public class p2p_endpoint_stub: IP2pEndpoint
  {
	public void relay_notify_to_all(int command, BinaryArray data_buff, boost::uuids.uuid excludeConnection)
	{
	}
	public bool invoke_notify_to_peer(int command, BinaryArray req_buff, CryptoNote.CryptoNoteConnectionContext context)
	{
		return true;
	}
	public void for_each_connection(Action<CryptoNote.CryptoNoteConnectionContext , ulong> f)
	{
	}
	public ulong get_connections_count()
	{
		return 0;
	}
	public void externalRelayNotifyToAll(int command, BinaryArray data_buff, boost::uuids.uuid excludeConnection)
	{
	}
  }
}
