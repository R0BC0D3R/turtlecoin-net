// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

namespace Tools
{

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename T>
public class ObserverManager <T>
{
  public bool add(T observer)
  {
	std::unique_lock<object> @lock = new std::unique_lock<object>(m_observersMutex);
	var it = std::find(m_observers.GetEnumerator(), m_observers.end(), observer);
	if (m_observers.end() == it)
	{
	  m_observers.Add(observer);
	  return true;
	}
	else
	{
	  return false;
	}
  }

  public bool remove(T observer)
  {
	std::unique_lock<object> @lock = new std::unique_lock<object>(m_observersMutex);

	var it = std::find(m_observers.GetEnumerator(), m_observers.end(), observer);
	if (m_observers.end() == it)
	{
	  return false;
	}
	else
	{
//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'erase' method in C#:
	  m_observers.erase(it);
	  return true;
	}
  }

  public void clear()
  {
	std::unique_lock<object> @lock = new std::unique_lock<object>(m_observersMutex);
	m_observers.Clear();
  }

#if _MSC_VER
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename F>
  public void notify<F>(F notification)
  {
	List<T> observersCopy = new List<T>();
	{
	  std::unique_lock<object> @lock = new std::unique_lock<object>(m_observersMutex);
	  observersCopy = new List<T>(m_observers);
	}

	foreach (T observer in observersCopy)
	{
	  notification();
	}
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename F, typename Arg0>
  public void notify<F, Arg0>(F notification, Arg0 arg0)
  {
	List<T> observersCopy = new List<T>();
	{
	  std::unique_lock<object> @lock = new std::unique_lock<object>(m_observersMutex);
	  observersCopy = new List<T>(m_observers);
	}

	foreach (T observer in observersCopy)
	{
	  notification(arg0);
	}
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename F, typename Arg0, typename Arg1>
  public void notify<F, Arg0, Arg1>(F notification, Arg0 arg0, Arg1 arg1)
  {
	List<T> observersCopy = new List<T>();
	{
	  std::unique_lock<object> @lock = new std::unique_lock<object>(m_observersMutex);
	  observersCopy = new List<T>(m_observers);
	}

	foreach (T observer in observersCopy)
	{
	  notification(arg0, arg1);
	}
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename F, typename Arg0, typename Arg1, typename Arg2>
  public void notify<F, Arg0, Arg1, Arg2>(F notification, Arg0 arg0, Arg1 arg1, Arg2 arg2)
  {
	List<T> observersCopy = new List<T>();
	{
	  std::unique_lock<object> @lock = new std::unique_lock<object>(m_observersMutex);
	  observersCopy = new List<T>(m_observers);
	}

	foreach (T observer in observersCopy)
	{
	  notification(arg0, arg1, arg2);
	}
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename F, typename Arg0, typename Arg1, typename Arg2, typename Arg3>
  public void notify<F, Arg0, Arg1, Arg2, Arg3>(F notification, Arg0 arg0, Arg1 arg1, Arg2 arg2, Arg3 arg3)
  {
	List<T> observersCopy = new List<T>();
	{
	  std::unique_lock<object> @lock = new std::unique_lock<object>(m_observersMutex);
	  observersCopy = new List<T>(m_observers);
	}

	foreach (T observer in observersCopy)
	{
	  notification(arg0, arg1, arg2, arg3);
	}
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename F, typename Arg0, typename Arg1, typename Arg2, typename Arg3, typename Arg4>
  public void notify<F, Arg0, Arg1, Arg2, Arg3, Arg4>(F notification, Arg0 arg0, Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4)
  {
	List<T> observersCopy = new List<T>();
	{
	  std::unique_lock<object> @lock = new std::unique_lock<object>(m_observersMutex);
	  observersCopy = new List<T>(m_observers);
	}

	foreach (T observer in observersCopy)
	{
	  notification(arg0, arg1, arg2, arg3, arg4);
	}
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename F, typename Arg0, typename Arg1, typename Arg2, typename Arg3, typename Arg4, typename Arg5>
  public void notify<F, Arg0, Arg1, Arg2, Arg3, Arg4, Arg5>(F notification, Arg0 arg0, Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4, Arg5 arg5)
  {
	List<T> observersCopy = new List<T>();
	{
	  std::unique_lock<object> @lock = new std::unique_lock<object>(m_observersMutex);
	  observersCopy = new List<T>(m_observers);
	}

	foreach (T observer in observersCopy)
	{
	  notification(arg0, arg1, arg2, arg3, arg4, arg5);
	}
  }

#else

//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to C++11 variadic templates:
  public void notify<F, typename... Args>(F notification, Args... args)
  {
	List<T> observersCopy = new List<T>();
	{
	  std::unique_lock<object> @lock = new std::unique_lock<object>(m_observersMutex);
	  observersCopy = new List<T>(m_observers);
	}

	foreach (T observer in observersCopy)
	{
	  notification(args...);
	}
  }
#endif

  private List<T> m_observers = new List<T>();
  private object m_observersMutex = new object();
}

}
