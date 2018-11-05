using System;
using System.Diagnostics;

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



namespace System
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class Ipv4Address;

public class TcpConnection : System.IDisposable
{
  public TcpConnection()
  {
	  this.dispatcher = null;
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  TcpConnection(const TcpConnection&) = delete;
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public TcpConnection(TcpConnection && other)
  {
	  this.dispatcher = other.dispatcher;
	if (other.dispatcher != null)
	{
	  Debug.Assert(other.contextPair.writeContext == null);
	  Debug.Assert(other.contextPair.readContext == null);
	  connection = other.connection;
	  contextPair = other.contextPair;
	  other.dispatcher = null;
	}
  }
  public void Dispose()
  {
	if (dispatcher != null)
	{
	  Debug.Assert(contextPair.readContext == null);
	  Debug.Assert(contextPair.writeContext == null);
	  int result = close(connection);
	  if (result != 0)
	  {
	  }
	  Debug.Assert(result != -1);
	}
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  TcpConnection& operator =(const TcpConnection&) = delete;
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
  public static TcpConnection operator = (TcpConnection && other)
  {
	if (dispatcher != null)
	{
	  Debug.Assert(contextPair.readContext == null);
	  Debug.Assert(contextPair.writeContext == null);
	  if (close(connection) == -1)
	  {
		throw new System.Exception("TcpConnection::operator=, close failed, " + lastErrorMessage());
	  }
	}

	dispatcher = other.dispatcher;
	if (other.dispatcher != null)
	{
	  Debug.Assert(other.contextPair.readContext == null);
	  Debug.Assert(other.contextPair.writeContext == null);
	  connection = other.connection;
	  contextPair = other.contextPair;
	  other.dispatcher = null;
	}

	return this;
  }
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  uint read(uint8_t data, uint size);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  uint write(uint8_t data, uint size);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: System.Tuple<Ipv4Address, uint16_t> getPeerAddressAndPort() const
  public Tuple<Ipv4Address, uint16_t> getPeerAddressAndPort()
  {
	sockaddr_in addr = new sockaddr_in();
	socklen_t size = sizeof(sockaddr_in);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	if (getpeername(connection, reinterpret_cast<sockaddr>(addr), size) != 0)
	{
	  throw new System.Exception("TcpConnection::getPeerAddress, getpeername failed, " + lastErrorMessage());
	}

	Debug.Assert(size == sizeof(sockaddr_in));
	return Tuple.Create(new Ipv4Address(htonl(addr.sin_addr.s_addr)), htons(addr.sin_port));
  }

//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' class:
//  friend class TcpConnector;
//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' class:
//  friend class TcpListener;

  private Dispatcher dispatcher;
  private int connection;
  private ContextPair contextPair = new ContextPair();

  private TcpConnection(Dispatcher dispatcher, int socket)
  {
	  this.dispatcher = dispatcher;
	  this.connection = socket;
	contextPair.readContext = null;
	contextPair.writeContext = null;
	epoll_event connectionEvent = new epoll_event();
	connectionEvent.events = EPOLLONESHOT;
	connectionEvent.data.ptr = null;

	if (epoll_ctl(dispatcher.getEpoll(), EPOLL_CTL_ADD, socket, connectionEvent) == -1)
	{
	  throw new System.Exception("TcpConnection::TcpConnection, epoll_ctl failed, " + lastErrorMessage());
	}
  }
}

}




