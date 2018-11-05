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


