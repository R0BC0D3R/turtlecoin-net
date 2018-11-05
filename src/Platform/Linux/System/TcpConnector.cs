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
//class Dispatcher;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class Ipv4Address;
//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class TcpConnection;

public class TcpConnector : System.IDisposable
{
  public TcpConnector()
  {
	  this.dispatcher = null;
  }
  public TcpConnector(Dispatcher dispatcher)
  {
	  this.dispatcher = dispatcher;
	  this.context = null;
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  TcpConnector(const TcpConnector&) = delete;
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public TcpConnector(TcpConnector && other)
  {
	  this.dispatcher = other.dispatcher;
	if (other.dispatcher != null)
	{
	  Debug.Assert(other.context == null);
	  context = null;
	  other.dispatcher = null;
	}
  }
  public void Dispose()
  {
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  TcpConnector& operator =(const TcpConnector&) = delete;
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
  public static TcpConnector operator = (TcpConnector && other)
  {
	dispatcher = other.dispatcher;
	if (other.dispatcher != null)
	{
	  Debug.Assert(other.context == null);
	  context = null;
	  other.dispatcher = null;
	}

	return this;
  }
  public TcpConnection connect(Ipv4Address address, uint16_t port)
  {
	Debug.Assert(dispatcher != null);
	Debug.Assert(context == null);
	if (dispatcher.interrupted())
	{
	  throw InterruptedException();
	}

	string message;
	int connection = global::socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (connection == -1)
	{
	  message = "socket failed, " + lastErrorMessage();
	}
	else
	{
	  sockaddr_in bindAddress = new sockaddr_in();
	  bindAddress.sin_family = AF_INET;
	  bindAddress.sin_port = 0;
	  bindAddress.sin_addr.s_addr = INADDR_ANY;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  if (bind(connection, reinterpret_cast<sockaddr>(bindAddress), sizeof (sockaddr_in)) != 0)
	  {
		message = "bind failed, " + lastErrorMessage();
	  }
	  else
	  {
		int flags = fcntl(connection, F_GETFL, 0);
		if (flags == -1 || fcntl(connection, F_SETFL, flags | O_NONBLOCK) == -1)
		{
		  message = "fcntl failed, " + lastErrorMessage();
		}
		else
		{
		  sockaddr_in addressData = new sockaddr_in();
		  addressData.sin_family = AF_INET;
		  addressData.sin_port = htons(port);
		  addressData.sin_addr.s_addr = htonl(address.getValue());
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		  int result = global::connect(connection, reinterpret_cast<sockaddr>(addressData), sizeof (sockaddr_in));
		  if (result == -1)
		  {
			if (errno == EINPROGRESS)
			{

			  ContextPair contextPair = new ContextPair();
			  TcpConnectorContextExt connectorContext = new TcpConnectorContextExt();
			  connectorContext.interrupted = false;
			  connectorContext.context = dispatcher.getCurrentContext();
			  connectorContext.connection = connection;

			  contextPair.readContext = null;
			  contextPair.writeContext = connectorContext;

			  epoll_event connectEvent = new epoll_event();
			  connectEvent.events = EPOLLOUT | EPOLLRDHUP | EPOLLERR | EPOLLONESHOT;
			  connectEvent.data.ptr = contextPair;
			  if (epoll_ctl(dispatcher.getEpoll(), EPOLL_CTL_ADD, connection, connectEvent) == -1)
			  {
				message = "epoll_ctl failed, " + lastErrorMessage();
			  }
			  else
			  {
				context = connectorContext;
				dispatcher.getCurrentContext().interruptProcedure = () =>
				{
				  TcpConnectorContextExt connectorContext1 = (TcpConnectorContextExt)context;
				  if (!connectorContext1.interrupted)
				  {
					if (close(connectorContext1.connection) == -1)
					{
					  throw new System.Exception("TcpListener::stop, close failed, " + lastErrorMessage());
					}

					connectorContext1.interrupted = true;
					dispatcher.pushContext(connectorContext1.context);
				  }
				};

				dispatcher.dispatch();
				dispatcher.getCurrentContext().interruptProcedure = null;
				Debug.Assert(dispatcher != null);
				Debug.Assert(connectorContext.context == dispatcher.getCurrentContext());
				Debug.Assert(contextPair.readContext == null);
				Debug.Assert(context == connectorContext);
				context = null;
				connectorContext.context = null;
				if (connectorContext.interrupted)
				{
				  throw InterruptedException();
				}

				if (epoll_ctl(dispatcher.getEpoll(), EPOLL_CTL_DEL, connection, null) == -1)
				{
				  message = "epoll_ctl failed, " + lastErrorMessage();
				}
				else
				{
				  if ((connectorContext.events & (EPOLLERR | EPOLLHUP)) != 0)
				  {
					int result = close(connection);
					if (result != 0)
					{
					}
					Debug.Assert(result != -1);

					throw new System.Exception("TcpConnector::connect, connection failed");
				  }

				  int retval = -1;
				  socklen_t retValLen = sizeof(int);
				  int s = getsockopt(connection, SOL_SOCKET, SO_ERROR, retval, retValLen);
				  if (s == -1)
				  {
					message = "getsockopt failed, " + lastErrorMessage();
				  }
				  else
				  {
					if (retval != 0)
					{
					  message = "getsockopt failed, " + lastErrorMessage();
					}
					else
					{
					  return new TcpConnection(dispatcher, connection);
					}
				  }
				}
			  }
			}
		  }
		  else
		  {
			return new TcpConnection(dispatcher, connection);
		  }
		}
	  }

	  int result = close(connection);
	  if (result != 0)
	  {
	  }
	  Debug.Assert(result != -1);
	}


	throw new System.Exception("TcpConnector::connect, " + message);
  }

  private object context;
  private Dispatcher dispatcher;
}

}




namespace System
{

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

public class TcpConnectorContextExt : OperationContext
{
  public int connection;
}


}
