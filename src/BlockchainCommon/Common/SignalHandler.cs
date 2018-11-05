// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System;

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



