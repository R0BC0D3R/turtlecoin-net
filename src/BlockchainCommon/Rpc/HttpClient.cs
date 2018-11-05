// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

namespace CryptoNote
{

public class ConnectException : System.Exception
{
  public ConnectException(string whatArg) : base(whatArg)
  {
  }
}

public class HttpClient : System.IDisposable
{

  public HttpClient(System.Dispatcher dispatcher, string address, ushort port)
  {
	  this.m_dispatcher = dispatcher;
	  this.m_address = address;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.m_port = port;
	  this.m_port.CopyFrom(port);
  }
  public void Dispose()
  {
	if (m_connected)
	{
	  disconnect();
	}
  }
  public void request(HttpRequest req, HttpResponse res)
  {
	if (!m_connected)
	{
	  connect();
	}

	try
	{
	  std::iostream stream = new std::iostream(m_streamBuf.get());
	  HttpParser parser = new HttpParser();
	  stream << req;
	  stream.flush();
	  parser.receiveResponse(stream, res);
	}
	catch (System.Exception)
	{
	  disconnect();
	  throw;
	}
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isConnected() const
  public bool isConnected()
  {
	return m_connected;
  }

  private void connect()
  {
	try
	{
	  var ipAddr = System.Ipv4Resolver(m_dispatcher).resolve(m_address);
	  m_connection = System.TcpConnector(m_dispatcher).connect(ipAddr, m_port);
	  m_streamBuf.reset(new System.TcpStreambuf(m_connection));
	  m_connected = true;
	}
	catch (System.Exception e)
	{
	  throw new ConnectException(e.Message);
	}
  }
  private void disconnect()
  {
	m_streamBuf.reset();
	try
	{
	  m_connection.write(null, 0); //Socket shutdown.
	}
	catch (System.Exception)
	{
	  //Ignoring possible exception.
	}

	try
	{
	  m_connection = System.TcpConnection();
	}
	catch (System.Exception)
	{
	  //Ignoring possible exception.
	}

	m_connected = false;
  }

  private readonly string m_address;
  private readonly ushort m_port = new ushort();

  private bool m_connected = false;
  private System.Dispatcher m_dispatcher;
  private System.TcpConnection m_connection = new System.TcpConnection();
  private std::unique_ptr<System.TcpStreambuf> m_streamBuf = new std::unique_ptr<System.TcpStreambuf>();
}

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename Request, typename Response>

}



