using System;

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


