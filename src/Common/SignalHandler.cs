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

  public class SignalHandler
  {
	public static bool install(Action t)
	{
#if WIN32
	  bool r = 1 == global::SetConsoleCtrlHandler(GlobalMembers.winHandler, 1);
	  if (r)
	  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_handler = t;
		GlobalMembers.m_handler.CopyFrom(t);
	  }
	  return r;
#else
	  sigaction newMask = new sigaction();
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
	  memset(newMask, 0, sizeof(sigaction));
	  newMask.sa_handler = GlobalMembers.posixHandler;
	  if (sigaction(SIGINT, newMask, null) != 0)
	  {
		return false;
	  }

	  if (sigaction(SIGTERM, newMask, null) != 0)
	  {
		return false;
	  }

//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
	  memset(newMask, 0, sizeof(sigaction));
	  newMask.sa_handler = SIG_IGN;
	  if (sigaction(SIGPIPE, newMask, null) != 0)
	  {
		return false;
	  }

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_handler = t;
	  GlobalMembers.m_handler.CopyFrom(t);
	  return true;
#endif
	}
  }
}



#if _WIN32
#else
#endif

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace



