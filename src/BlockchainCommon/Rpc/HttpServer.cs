// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using Logging;
using System.Collections.Generic;

namespace CryptoNote
{

public abstract class HttpServer
{


  public HttpServer(System.Dispatcher dispatcher, Logging.ILogger log)
  {
	  this.m_dispatcher = dispatcher;
	  this.workingContextGroup = dispatcher;
	  this.logger = new Logging.LoggerRef(log, "HttpServer");

  }

  public void start(string address, uint16_t port)
  {
	m_listener = System.TcpListener(m_dispatcher, System.Ipv4Address(address), port);
	workingContextGroup.spawn(std::bind(this.acceptLoop, this));
  }
  public void stop()
  {
	workingContextGroup.interrupt();
	workingContextGroup.wait();
  }

  public abstract void processRequest(HttpRequest request, HttpResponse response);


  protected System.Dispatcher m_dispatcher;


  private void acceptLoop()
  {
	try
	{
	  System.TcpConnection connection = new System.TcpConnection();
	  bool accepted = false;

	  while (!accepted)
	  {
		try
		{
		  connection = m_listener.accept();
		  accepted = true;
		}
		catch (System.InterruptedException)
		{
		  throw;
		}
		catch (System.Exception)
		{
		  // try again
		}
	  }

	  m_connections.Add(connection);
	  BOOST_SCOPE_EXIT_ALL(this, connection)
	  {
		m_connections.erase(connection);
	  };

	  workingContextGroup.spawn(std::bind(acceptLoop, this));

	  var addr = connection.getPeerAddressAndPort();

	  logger(DEBUGGING) << "Incoming connection from " << addr.first.toDottedDecimal() << ":" << addr.second;

	  System.TcpStreambuf streambuf = new System.TcpStreambuf(connection);
	  std::iostream stream = new std::iostream(streambuf);
	  HttpParser parser = new HttpParser();

	  for (;;)
	  {
		HttpRequest req = new HttpRequest();
		HttpResponse resp = new HttpResponse();

		parser.receiveRequest(stream, req);
		processRequest(req, resp);

		stream << resp;
		stream.flush();

		if (stream.peek() == std::iostream.traits_type.eof())
		{
		  break;
		}
	  }

	  logger(DEBUGGING) << "Closing connection from " << addr.first.toDottedDecimal() << ":" << addr.second << " total=" << m_connections.Count;

	}
	catch (System.InterruptedException)
	{
	}
	catch (System.Exception e)
	{
	  logger(WARNING) << "Connection error: " << e.Message;
	}
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  void connectionHandler(System::TcpConnection&& conn);

  private System.ContextGroup workingContextGroup = new System.ContextGroup();
  private Logging.LoggerRef logger = new Logging.LoggerRef();
  private System.TcpListener m_listener = new System.TcpListener();
  private HashSet<System.TcpConnection> m_connections = new HashSet<System.TcpConnection>();
}

}

