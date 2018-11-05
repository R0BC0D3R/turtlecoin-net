// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

//C++ TO C# CONVERTER TODO TASK: C++ template specifiers containing defaults cannot be converted to C#:
//ORIGINAL LINE: template < typename T, typename Container = ClassicDeque<T>>
public class BlockingQueue < T, Container = LinkedList<T>>
{


  public BlockingQueue(ulong maxSize = 1)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.m_maxSize = maxSize;
	  this.m_maxSize.CopyFrom(maxSize);
	  this.m_closed = false;
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename TT>
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public bool push<TT>(TT && v)
  {
	std::unique_lock<object> lk = new std::unique_lock<object>(m_mutex);

	while (!m_closed && m_queue.size() >= m_maxSize)
	{
	  m_haveSpace.wait(lk);
	}

	if (m_closed)
	{
	  return false;
	}

	m_queue.push_back(std::forward<TT>(v));
	m_haveData.notify_one();
	return true;
  }

  public bool pop(ref T v)
  {
	std::unique_lock<object> lk = new std::unique_lock<object>(m_mutex);

	while (m_queue.empty())
	{
	  if (m_closed)
	  {
		// all data has been processed, queue is closed
		return false;
	  }
	  m_haveData.wait(lk);
	}

	v = std::move(m_queue.front());
	m_queue.pop_front();

	// we can have several waiting threads to unblock
	if (m_closed && m_queue.empty())
	{
	  m_haveSpace.notify_all();
	}
	else
	{
	  m_haveSpace.notify_one();
	}

	return true;
  }

  public void close(bool wait = false)
  {
	std::unique_lock<object> lk = new std::unique_lock<object>(m_mutex);
	m_closed = true;
	m_haveData.notify_all(); // wake up threads in pop()
	m_haveSpace.notify_all();

	if (wait)
	{
	  while (!m_queue.empty())
	  {
		m_haveSpace.wait(lk);
	  }
	}
  }

  public ulong size()
  {
	std::unique_lock<object> lk = new std::unique_lock<object>(m_mutex);
	return m_queue.size();
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong capacity() const
  public ulong capacity()
  {
	return m_maxSize;
  }


  private readonly ulong m_maxSize = new ulong();
  private Container m_queue = new Container();
  private bool m_closed;

  private object m_mutex = new object();
  private std::condition_variable m_haveData = new std::condition_variable();
  private std::condition_variable m_haveSpace = new std::condition_variable();
}

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename QueueT>
public class GroupClose <QueueT>
{

  public GroupClose(QueueT queue, ulong groupSize)
  {
	  this.m_queue = queue;
	  this.m_count = groupSize;
  }

  public void close()
  {
	if (m_count == 0)
	{
	  return;
	}
	if (m_count.fetch_sub(1) == 1)
	{
	  m_queue.close();
	}
  }


  private std::atomic<ulong> m_count = new std::atomic<ulong>();
  private QueueT m_queue;

}


//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace