using System.Collections.Generic;

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



public class SequenceEnded: System.Exception, System.IDisposable
{
  public SequenceEnded() : base("shuffle sequence ended")
  {
  }

  public void Dispose()
  {
  }
}

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T, typename Gen>
public class ShuffleGenerator <T, Gen>
{

  public ShuffleGenerator(T n, Gen gen = Gen())
  {
	  this.N = n;
	  this.generator = gen;
	  this.count = n;
  }

  public static T functorMethod()
  {

	if (count == 0)
	{
	  throw new SequenceEnded();
	}


	std::uniform_int_distribution<T> distr = new std::uniform_int_distribution<T>();

	T value = distr(generator, param_t(0, --count));

	var rvalIt = selected.find(count);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	var rval = rvalIt != selected.end() ? rvalIt.second : count;

	var lvalIt = selected.find(value);

//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	if (lvalIt != selected.end())
	{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  value = lvalIt.second;
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  lvalIt.second = rval;
	}
	else
	{
	  selected[value] = rval;
	}

	return value;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool empty() const
  public bool empty()
  {
	return count == 0;
  }

  public void reset()
  {
	count = N;
	selected.Clear();
  }


  private Dictionary<T, T> selected = new Dictionary<T, T>();
  private T count = new T();
  private readonly T N = new T();
  private Gen generator = new Gen();
}
