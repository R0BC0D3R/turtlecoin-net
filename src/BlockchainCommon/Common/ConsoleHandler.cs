// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using Color = Common.Console.Color;
using System;
using System.Collections.Generic;


#if ! _WIN32
#endif

namespace Common
{

  public class AsyncConsoleReader : System.IDisposable
  {



	/////////////////////////////////////////////////////////////////////////////
	// AsyncConsoleReader
	/////////////////////////////////////////////////////////////////////////////
	public AsyncConsoleReader()
	{
		this.m_stop = true;
	}
	public void Dispose()
	{
	  stop();
	}

	public void start()
	{
	  m_stop = false;
	  m_thread = std::thread(std::bind(this.consoleThread, this));
	}
	public bool getline(string line)
	{
	  return m_queue.pop(ref line);
	}
	public void stop()
	{

	  if (m_stop != null)
	  {
		return; // already stopping/stopped
	  }

	  m_stop = true;
	  m_queue.close();
#if _WIN32
	  global::CloseHandle(global::GetStdHandle(STD_INPUT_HANDLE));
#endif

	  if (m_thread.joinable())
	  {
		m_thread.join();
	  }

	  m_thread = std::thread();
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool stopped() const
	public bool stopped()
	{
	  return m_stop;
	}
	public void pause()
	{
	  if (m_stop != null)
	  {
		return;
	  }

	  m_stop = true;

	  if (m_thread.joinable())
	  {
		m_thread.join();
	  }

	  m_thread = std::thread();
	}
	public void unpause()
	{
	  start();
	}


	private void consoleThread()
	{

	  while (waitInput())
	  {
		string line;

		if (!line = Console.ReadLine())
		{
		  break;
		}

		if (!m_queue.push(line))
		{
		  break;
		}
	  }
	}
	private bool waitInput()
	{
#if ! _WIN32
	  int stdin_fileno = global::fileno(stdin);

	  while (m_stop == null)
	  {
		fd_set read_set = new fd_set();
		FD_ZERO(read_set);
		FD_SET(stdin_fileno, read_set);

		timeval tv = new timeval();
		tv.tv_sec = 0;
		tv.tv_usec = 100 * 1000;

		int retval = global::select(stdin_fileno + 1, read_set, null, null, tv);

		if (retval == -1 && errno == EINTR)
		{
		  continue;
		}

		if (retval < 0)
		{
		  return false;
		}

		if (retval > 0)
		{
		  return true;
		}
	  }
#endif

	  return m_stop == null;
	}

	private std::atomic<bool> m_stop = new std::atomic<bool>();
	private std::thread m_thread = new std::thread();
	private BlockingQueue<string> m_queue = new BlockingQueue<string>();
  }


  public class ConsoleHandler : System.IDisposable
  {


	  /////////////////////////////////////////////////////////////////////////////
	  // ConsoleHandler
	  /////////////////////////////////////////////////////////////////////////////
	  public void Dispose()
	  {
		stop();
	  }

	  public delegate bool ConsoleCommandHandler(List<string> UnnamedParameter);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: string getUsage() const
	  public string getUsage()
	  {

		if (m_handlers.Count == 0)
		{
		  return string();
		}

		std::stringstream ss = new std::stringstream();

		uint64_t maxlen = std::max_element(m_handlers.GetEnumerator(), m_handlers.end(), (CommandHandlersMap.const_reference a, CommandHandlersMap.const_reference b) =>
		{
			return a.first.size() < b.first.size();
		}).first.size();

		foreach (var x in m_handlers)
		{
		  ss << std::left << std::setw(maxlen + 3) << x.Item1 << x.Item2.second << std::endl;
		}

		return ss.str();
	  }
	  public void setHandler(string command, ConsoleCommandHandler handler, string usage = "")
	  {
		m_handlers[command] = Tuple.Create(handler, usage);
	  }
	  public void requestStop()
	  {
		m_consoleReader.stop();
	  }
	  public bool runCommand(List<string> cmdAndArgs)
	  {
		if (cmdAndArgs.Count == 0)
		{
		  return false;
		}

		auto cmd = cmdAndArgs[0];
		var hIter = m_handlers.find(cmd);

		if (hIter == m_handlers.end())
		{
		  Console.Write("Unknown command: ");
		  Console.Write(cmd);
		  Console.Write("\n");
		  return false;
		}

		List<string> args = new List<string>(cmdAndArgs.GetEnumerator() + 1, cmdAndArgs.end());
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
		hIter.second.first(args);
		return true;
	  }

	  public void start(bool startThread = true, string prompt = "", Console.Color promptColor = Console.Color.Default)
	  {
		m_prompt = prompt;
		m_promptColor = promptColor;
		m_consoleReader.start();

		if (startThread)
		{
		  m_thread = std::thread(std::bind(this.handlerThread, this));
		}
		else
		{
		  handlerThread();
		}
	  }
	  public void stop()
	  {
		requestStop();
		wait();
	  }
	  public void wait()
	  {

		try
		{
		  if (m_thread.joinable())
		  {
			m_thread.join();
		  }
		}
		catch (System.Exception e)
		{
		  std::cerr << "Exception in ConsoleHandler::wait - " << e.Message << std::endl;
		}
	  }
	  public void pause()
	  {
		m_consoleReader.pause();
	  }
	  public void unpause()
	  {
		m_consoleReader.unpause();
	  }



	  private virtual void handleCommand(string cmd)
	  {
		List<string> args = new List<string>();
		boost::split(args, cmd, boost::is_any_of(" "), boost::token_compress_on);
		runCommand(args);
	  }

	  private void handlerThread()
	  {
		string line;

		while (!m_consoleReader.stopped())
		{
		  try
		  {
			if (!string.IsNullOrEmpty(m_prompt))
			{
			  if (m_promptColor != Color.Default)
			  {
				Console.setTextColor(m_promptColor);
			  }

			  Console.Write(m_prompt);
			  std::cout.flush();

			  if (m_promptColor != Color.Default)
			  {
				Console.setTextColor(Color.Default);
			  }
			}

			if (!m_consoleReader.getline(line))
			{
			  break;
			}

			boost::algorithm.trim(line);
			if (!string.IsNullOrEmpty(line))
			{
			  handleCommand(line);
			}

		  }
		  catch (System.Exception)
		  {
			// ignore errors
		  }
		}
	  }

	  private std::thread m_thread = new std::thread();
	  private string m_prompt;
	  private Console.Color m_promptColor = Console.Color.Default;
	  private SortedDictionary<string, Tuple<ConsoleCommandHandler, string>> m_handlers = new SortedDictionary<string, Tuple<ConsoleCommandHandler, string>>();
	  private AsyncConsoleReader m_consoleReader = new AsyncConsoleReader();
  }
}



#if _WIN32
#else
#endif

