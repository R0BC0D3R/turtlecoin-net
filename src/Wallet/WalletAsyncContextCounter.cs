// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


namespace CryptoNote
{

public class WalletAsyncContextCounter
{
  public WalletAsyncContextCounter()
  {
	  this.m_asyncContexts = 0;
  }

  public void addAsyncContext()
  {
	std::unique_lock<object> @lock = new std::unique_lock<object>(m_mutex);
	m_asyncContexts++;
  }
  public void delAsyncContext()
  {
	std::unique_lock<object> @lock = new std::unique_lock<object>(m_mutex);
	m_asyncContexts--;

	if (m_asyncContexts == 0)
	{
		m_cv.notify_one();
	}
  }

  //returns true if contexts are finished before timeout
  public void waitAsyncContextsFinish()
  {
	std::unique_lock<object> @lock = new std::unique_lock<object>(m_mutex);
	while (m_asyncContexts > 0)
	{
	  m_cv.wait(@lock);
	}
  }

  private uint m_asyncContexts;
  private std::condition_variable m_cv = new std::condition_variable();
  private object m_mutex = new object();
}

} //namespace CryptoNote


