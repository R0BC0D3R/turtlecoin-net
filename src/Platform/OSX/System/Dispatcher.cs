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

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define setcontext(u) setmcontext(&(u)->uc_mcontext)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define getcontext(u) getmcontext(&(u)->uc_mcontext)

namespace System
{

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace

public class ContextMakingData
{
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
  public object uctx;
  public Dispatcher dispatcher;
}

public class MutextGuard : System.IDisposable
{
  public MutextGuard(pthread_mutex_t _mutex)
  {
	  this.mutex = _mutex;
	var ret = pthread_mutex_lock(mutex);
	if (ret != 0)
	{
	  throw new System.Exception("MutextGuard::MutextGuard, pthread_mutex_lock failed, " + errorMessage(ret));
	}
  }

  public void Dispose()
  {
	pthread_mutex_unlock(mutex);
  }

  private pthread_mutex_t mutex;
}


//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
//static_assert(Dispatcher::SIZEOF_PTHREAD_MUTEX_T == sizeof(pthread_mutex_t), "invalid pthread mutex size");





//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: NativeContext* Dispatcher::getCurrentContext() const





//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:


//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: int Dispatcher::getKqueue() const







}
