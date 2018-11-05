// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System;

namespace Tools
{

public class ScopeExit : System.IDisposable
{
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public ScopeExit(Action && handler)
  {
	  this.m_handler = std::move(handler);
	  this.m_cancelled = false;
  }
  public void Dispose()
  {
	if (!m_cancelled)
	{
	  m_handler();
	}
  }

//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  ScopeExit(const ScopeExit&) = delete;
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  ScopeExit(ScopeExit&&) = delete;
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  ScopeExit& operator =(const ScopeExit&) = delete;
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  ScopeExit& operator =(ScopeExit&&) = delete;

  public void cancel()
  {
	m_cancelled = true;
  }
  public void resume()
  {
	m_cancelled = false;
  }

  private Action m_handler;
  private bool m_cancelled;
}

}


