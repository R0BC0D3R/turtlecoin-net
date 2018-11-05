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






namespace Common
{

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class T>
public class EnableIfPod <T>
{
}

public enum FileMappedVectorOpenMode
{
  OPEN,
  CREATE,
  OPEN_OR_CREATE
}

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class T>
public class FileMappedVector <T>: EnableIfPod<T>.type
{

  public uint64_t metadataSize = static_cast<uint64_t>(2 * sizeof(uint64_t));
  public uint64_t valueSize = static_cast<uint64_t>(sizeof(T));

  public class const_iterator
  {

	public const_iterator()
	{
		this.m_fileMappedVector = new FileMappedVector(null);
	}

	public const_iterator(FileMappedVector fileMappedVector, uint64_t index)
	{
		this.m_fileMappedVector = new FileMappedVector(fileMappedVector);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.m_index = index;
		this.m_index.CopyFrom(index);
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const T& operator *() const
	public T Indirection()
	{
	  return m_fileMappedVector[m_index];
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const T* operator ->() const
	public T Dereference()
	{
	  return m_fileMappedVector[m_index];
	}

	public static const_iterator operator ++(const_iterator ImpliedObject)
	{
	  ++ImpliedObject.m_index;
	  return *ImpliedObject;
	}

	public static const_iterator operator ++(int UnnamedParameter)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: const_iterator tmp = *this;
	  const_iterator tmp = new const_iterator(this);
	  ++m_index;
	  return tmp;
	}

	public static const_iterator operator --(const_iterator ImpliedObject)
	{
	  --ImpliedObject.m_index;
	  return *ImpliedObject;
	}

	public static const_iterator operator --(int UnnamedParameter)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: const_iterator tmp = *this;
	  const_iterator tmp = new const_iterator(this);
	  --m_index;
	  return tmp;
	}

//C++ TO C# CONVERTER TODO TASK: The += operator cannot be overloaded in C#:
	public static const_iterator operator += (ptrdiff_t n)
	{
	  m_index += n;
	  return this;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const_iterator operator +(ptrdiff_t n) const
	public static const_iterator operator + (const_iterator ImpliedObject, ptrdiff_t n)
	{
	  return new const_iterator(ImpliedObject.m_fileMappedVector, ImpliedObject.m_index + n);
	}

//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' function:
//ORIGINAL LINE: friend const_iterator operator +(ptrdiff_t n, const const_iterator& i)
	public static const_iterator operator + (ptrdiff_t n, const_iterator i)
	{
	  return new const_iterator(i.m_fileMappedVector, n + i.m_index);
	}

//C++ TO C# CONVERTER TODO TASK: The -= operator cannot be overloaded in C#:
	public static const_iterator operator -= (ptrdiff_t n)
	{
	  m_index -= n;
	  return this;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const_iterator operator -(ptrdiff_t n) const
	public static const_iterator operator - (const_iterator ImpliedObject, ptrdiff_t n)
	{
	  return new const_iterator(ImpliedObject.m_fileMappedVector, ImpliedObject.m_index - n);
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ptrdiff_t operator -(const const_iterator& other) const
	public static ptrdiff_t operator - (const_iterator ImpliedObject, const_iterator other)
	{
	  return ImpliedObject.m_index - other.m_index;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const T& operator [](ptrdiff_t offset) const
	public T this[ptrdiff_t offset]
	{
		get
		{
		  return m_fileMappedVector[m_index + offset];
		}
		set
		{
			m_fileMappedVector[m_index + offset] = value;
		}
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator ==(const const_iterator& other) const
	public static bool operator == (const_iterator ImpliedObject, const_iterator other)
	{
	  return ImpliedObject.m_index == other.m_index;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator !=(const const_iterator& other) const
	public static bool operator != (const_iterator ImpliedObject, const_iterator other)
	{
	  return ImpliedObject.m_index != other.m_index;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator <(const const_iterator& other) const
	public static bool operator < (const_iterator ImpliedObject, const_iterator other)
	{
	  return ImpliedObject.m_index < other.m_index;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator >(const const_iterator& other) const
	public static bool operator > (const_iterator ImpliedObject, const_iterator other)
	{
	  return ImpliedObject.m_index > other.m_index;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator <=(const const_iterator& other) const
	public static bool operator <= (const_iterator ImpliedObject, const_iterator other)
	{
	  return ImpliedObject.m_index <= other.m_index;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator >=(const const_iterator& other) const
	public static bool operator >= (const_iterator ImpliedObject, const_iterator other)
	{
	  return ImpliedObject.m_index >= other.m_index;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t index() const
	public uint64_t index()
	{
	  return m_index;
	}

	protected readonly FileMappedVector[] m_fileMappedVector;
	protected uint64_t m_index = new uint64_t();
  }

  public class iterator : const_iterator
  {

	public iterator() : base()
	{
	}

	public iterator(FileMappedVector fileMappedVector, uint64_t index) : base(fileMappedVector, new uint64_t(index))
	{
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: T& operator *() const
	public new T Indirection()
	{
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
	  return const_cast<T&>(base.m_fileMappedVector[base.m_index]);
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: T* operator ->() const
	public new T Dereference()
	{
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
	  return const_cast<T>(base.m_fileMappedVector[base.m_index]);
	}

	public static new iterator operator ++(iterator ImpliedObject)
	{
	  ++base.m_index;
	  return *ImpliedObject;
	}

	public static new iterator operator ++(int UnnamedParameter)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: iterator tmp = *this;
	  iterator tmp = new iterator(this);
	  ++base.m_index;
	  return tmp;
	}

	public static new iterator operator --(iterator ImpliedObject)
	{
	  --base.m_index;
	  return *ImpliedObject;
	}

	public static new iterator operator --(int UnnamedParameter)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: iterator tmp = *this;
	  iterator tmp = new iterator(this);
	  --base.m_index;
	  return tmp;
	}

//C++ TO C# CONVERTER TODO TASK: The typedef 'difference_type' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The += operator cannot be overloaded in C#:
	public static iterator operator += (difference_type n)
	{
	  base.m_index += n;
	  return this;
	}

//C++ TO C# CONVERTER TODO TASK: The typedef 'difference_type' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: iterator operator +(difference_type n) const
	public static iterator operator + (iterator ImpliedObject, difference_type n)
	{
	  return new iterator(base.m_fileMappedVector, base.m_index + n);
	}

//C++ TO C# CONVERTER TODO TASK: The typedef 'difference_type' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' function:
//ORIGINAL LINE: friend iterator operator +(difference_type n, const iterator& i)
	public static iterator operator + (difference_type n, iterator i)
	{
	  return new iterator(i.m_fileMappedVector, n + i.m_index);
	}

//C++ TO C# CONVERTER TODO TASK: The typedef 'difference_type' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The -= operator cannot be overloaded in C#:
	public static iterator operator -= (difference_type n)
	{
	  base.m_index -= n;
	  return this;
	}

//C++ TO C# CONVERTER TODO TASK: The typedef 'difference_type' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: iterator operator -(difference_type n) const
	public static iterator operator - (iterator ImpliedObject, difference_type n)
	{
	  return new iterator(base.m_fileMappedVector, base.m_index - n);
	}

//C++ TO C# CONVERTER TODO TASK: The typedef 'difference_type' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: difference_type operator -(const iterator& other) const
	public static difference_type operator - (iterator ImpliedObject, iterator other)
	{
	  return base.m_index - other.m_index;
	}

//C++ TO C# CONVERTER TODO TASK: The typedef 'difference_type' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: T& operator [](difference_type offset) const
	public T this[difference_type offset]
	{
		get
		{
		  return base.m_fileMappedVector[base.m_index + offset];
		}
		set
		{
			base.m_fileMappedVector[base.m_index + offset] = value;
		}
	}
  }

  public FileMappedVector()
  {
	  this.m_autoFlush = true;
  }
  public FileMappedVector(string path, FileMappedVectorOpenMode mode = FileMappedVectorOpenMode.OPEN_OR_CREATE, uint64_t prefixSize = 0)
  {
	  this.m_autoFlush = true;
	open(path, mode, new uint64_t(prefixSize));
  }
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  FileMappedVector(const FileMappedVector&) = delete;
//C++ TO C# CONVERTER TODO TASK: C# has no equivalent to ' = delete':
//  FileMappedVector& operator =(const FileMappedVector&) = delete;

  public void open(string path, FileMappedVectorOpenMode mode = FileMappedVectorOpenMode.OPEN_OR_CREATE, uint64_t prefixSize = 0)
  {
	Debug.Assert(!isOpened());

	const uint64_t initialCapacity = 10;

	boost::filesystem.path filePath = path;
	boost::filesystem.path bakPath = path + ".bak";
	bool fileExists;
	if (boost::filesystem.exists(filePath))
	{
	  if (boost::filesystem.exists(bakPath))
	  {
		boost::filesystem.remove(bakPath);
	  }

	  fileExists = true;
	}
	else if (boost::filesystem.exists(bakPath))
	{
	  boost::filesystem.rename(bakPath, filePath);
	  fileExists = true;
	}
	else
	{
	  fileExists = false;
	}

	if (mode == FileMappedVectorOpenMode.OPEN)
	{
	  open(path, new uint64_t(prefixSize));
	}
	else if (mode == FileMappedVectorOpenMode.CREATE)
	{
	  create(path, new uint64_t(initialCapacity), new uint64_t(prefixSize), 0);
	}
	else if (mode == FileMappedVectorOpenMode.OPEN_OR_CREATE)
	{
	  if (fileExists)
	  {
		open(path, new uint64_t(prefixSize));
	  }
	  else
	  {
		create(path, new uint64_t(initialCapacity), new uint64_t(prefixSize), 0);
	  }
	}
	else
	{
	  throw new System.Exception("FileMappedVector: Unsupported open mode: " + Convert.ToString((int)mode));
	}
  }
  public void close()
  {
	std::error_code ec = new std::error_code();
	close(ec);
	if (ec != null)
	{
	  throw std::system_error(ec, "FileMappedVector::close");
	}
  }
  public void close(std::error_code ec)
  {
	m_file.close(ec);
	if (ec == null)
	{
	  m_prefixSize = 0;
	  m_suffixSize = 0;
	  m_path = "";
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isOpened() const
  public bool isOpened()
  {
	return m_file.isOpened();
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool empty() const
  public bool empty()
  {
	Debug.Assert(isOpened());

	return size() == 0;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t capacity() const
  public uint64_t capacity()
  {
	Debug.Assert(isOpened());

	return capacityPtr();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t size() const
  public uint64_t size()
  {
	Debug.Assert(isOpened());

	return sizePtr();
  }
  public void reserve(uint64_t n)
  {
	Debug.Assert(isOpened());

	if (n > capacity())
	{
  //C++ TO C# CONVERTER TODO TASK: The typedef 'value_type' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: atomicUpdate(size(), n, prefixSize(), suffixSize(), [this](value_type* target)
	  atomicUpdate(size(), new uint64_t(n), prefixSize(), suffixSize(), (value_type target) =>
	  {
		std::copy(cbegin(), cend(), target);
	  });
	}
  }
  public void shrink_to_fit()
  {
	Debug.Assert(isOpened());

	if (size() < capacity())
	{
  //C++ TO C# CONVERTER TODO TASK: The typedef 'value_type' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: atomicUpdate(size(), size(), prefixSize(), suffixSize(), [this](value_type* target)
	  atomicUpdate(size(), size(), prefixSize(), suffixSize(), (value_type target) =>
	  {
		std::copy(cbegin(), cend(), target);
	  });
	}
  }

  public FileMappedVector<T>.iterator begin()
  {
	Debug.Assert(isOpened());

	return new iterator(this, 0);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: FileMappedVector<T>::const_iterator begin() const
  public FileMappedVector<T>.const_iterator begin()
  {
	Debug.Assert(isOpened());

	return new const_iterator(this, 0);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: FileMappedVector<T>::const_iterator cbegin() const
  public FileMappedVector<T>.const_iterator cbegin()
  {
	Debug.Assert(isOpened());

	return new const_iterator(this, 0);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: FileMappedVector<T>::const_iterator end() const
  public FileMappedVector<T>.const_iterator end()
  {
	Debug.Assert(isOpened());

	return new const_iterator(this, size());
  }
  public FileMappedVector<T>.iterator end()
  {
	Debug.Assert(isOpened());

	return new iterator(this, size());
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: FileMappedVector<T>::const_iterator cend() const
  public FileMappedVector<T>.const_iterator cend()
  {
	Debug.Assert(isOpened());

	return new const_iterator(this, size());
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const T& operator [](uint64_t index) const
  public T this[uint64_t index]
  {
	  get
	  {
		Debug.Assert(isOpened());
    
		return vectorDataPtr()[index];
	  }
	  set
	  {
		  vectorDataPtr()[index] = value;
	  }
  }
  public T this[uint64_t index]
  {
	  get
	  {
		Debug.Assert(isOpened());
    
		return vectorDataPtr()[index];
	  }
	  set
	  {
		  vectorDataPtr()[index] = value;
	  }
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const T& at(uint64_t index) const
  public T at(uint64_t index)
  {
	Debug.Assert(isOpened());

	if (index >= size())
	{
	  throw new System.IndexOutOfRangeException("FileMappedVector::at " + Convert.ToString(index));
	}

	return vectorDataPtr()[index];
  }
  public T at(uint64_t index)
  {
	Debug.Assert(isOpened());

	if (index >= size())
	{
	  throw new System.IndexOutOfRangeException("FileMappedVector::at " + Convert.ToString(index));
	}

	return vectorDataPtr()[index];
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const T& front() const
  public T front()
  {
	Debug.Assert(isOpened());

	return vectorDataPtr()[0];
  }
  public T front()
  {
	Debug.Assert(isOpened());

	return vectorDataPtr()[0];
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const T& back() const
  public T back()
  {
	Debug.Assert(isOpened());

	return vectorDataPtr()[size() - 1];
  }
  public T back()
  {
	Debug.Assert(isOpened());

	return vectorDataPtr()[size() - 1];
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const T* data() const
  public T data()
  {
	Debug.Assert(isOpened());

	return vectorDataPtr();
  }
  public T data()
  {
	Debug.Assert(isOpened());

	return vectorDataPtr();
  }

  public void clear()
  {
	Debug.Assert(isOpened());

	sizePtr() = null;
	flushSize();
  }
  public FileMappedVector<T>.iterator erase(const_iterator position)
  {
	Debug.Assert(isOpened());

//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: return erase(position, std::next(position));
	return erase(new Common.FileMappedVector.const_iterator(position), std::next(position));
  }
  public FileMappedVector<T>.iterator erase(const_iterator first, const_iterator last)
  {
	Debug.Assert(isOpened());

	uint64_t newSize = size() - std::distance(first, last);

  //C++ TO C# CONVERTER TODO TASK: The typedef 'value_type' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: atomicUpdate(newSize, capacity(), prefixSize(), suffixSize(), [this, first, last](value_type* target)
	atomicUpdate(new uint64_t(newSize), capacity(), prefixSize(), suffixSize(), (value_type target) =>
	{
	  std::copy(cbegin(), first, target);
	  std::copy(last, cend(), target + std::distance(cbegin(), first));
	});

	return new iterator(this, first.index());
  }
  public FileMappedVector<T>.iterator insert(const_iterator position, T val)
  {
	Debug.Assert(isOpened());

//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: return insert(position, &val, &val + 1);
	return insert(new Common.FileMappedVector.const_iterator(position), val, val + 1);
  }
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class InputIterator>
  public FileMappedVector<T>.iterator insert<InputIterator>(const_iterator position, InputIterator first, InputIterator last)
  {
	Debug.Assert(isOpened());

	uint64_t newSize = size() + (uint64_t)std::distance(first, last);
	uint64_t newCapacity = new uint64_t();
	if (newSize > capacity())
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: newCapacity = nextCapacity();
	  newCapacity.CopyFrom(nextCapacity());
	  if (newSize > newCapacity)
	  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: newCapacity = newSize;
		newCapacity.CopyFrom(newSize);
	  }
	}
	else
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: newCapacity = capacity();
	  newCapacity.CopyFrom(capacity());
	}

  //C++ TO C# CONVERTER TODO TASK: The typedef 'value_type' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: atomicUpdate(newSize, newCapacity, prefixSize(), suffixSize(), [this, position, first, last](value_type* target)
	atomicUpdate(new uint64_t(newSize), new uint64_t(newCapacity), prefixSize(), suffixSize(), (value_type target) =>
	{
	  std::copy(cbegin(), position, target);
	  std::copy(first, last, target + position.index());
	  std::copy(position, cend(), target + position.index() + std::distance(first, last));
	});

	return new iterator(this, position.index());
  }
  public void pop_back()
  {
	Debug.Assert(isOpened());

	--sizePtr();
	flushSize();
  }
  public void push_back(T val)
  {
	Debug.Assert(isOpened());

	if (capacity() == size())
	{
	  reserve(nextCapacity());
	}

	vectorDataPtr()[size()] = val;
	flushElement(size());

	++sizePtr();
	flushSize();
  }
  public void swap(FileMappedVector other)
  {
	m_path.swap(other.m_path);
	m_file.swap(other.m_file);
	std::swap(m_prefixSize, other.m_prefixSize);
	std::swap(m_suffixSize, other.m_suffixSize);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool getAutoFlush() const
  public bool getAutoFlush()
  {
	return m_autoFlush;
  }
  public void setAutoFlush(bool autoFlush)
  {
	m_autoFlush = autoFlush;
  }

  public void flush()
  {
	Debug.Assert(isOpened());

	m_file.flush(m_file.data(), m_file.size());
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const uint8_t* prefix() const
  public uint8_t prefix()
  {
	Debug.Assert(isOpened());

	return prefixPtr();
  }
  public uint8_t prefix()
  {
	Debug.Assert(isOpened());

	return prefixPtr();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t prefixSize() const
  public uint64_t prefixSize()
  {
	Debug.Assert(isOpened());

	return m_prefixSize;
  }
  public void resizePrefix(uint64_t newPrefixSize)
  {
	Debug.Assert(isOpened());

	if (prefixSize() != newPrefixSize)
	{
  //C++ TO C# CONVERTER TODO TASK: The typedef 'value_type' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: atomicUpdate(size(), capacity(), newPrefixSize, suffixSize(), [this](value_type* target)
	  atomicUpdate(size(), capacity(), new uint64_t(newPrefixSize), suffixSize(), (value_type target) =>
	  {
		std::copy(cbegin(), cend(), target);
	  });
	}
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const uint8_t* suffix() const
  public uint8_t suffix()
  {
	Debug.Assert(isOpened());

	return suffixPtr();
  }
  public uint8_t suffix()
  {
	Debug.Assert(isOpened());

	return suffixPtr();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t suffixSize() const
  public uint64_t suffixSize()
  {
	Debug.Assert(isOpened());

	return m_suffixSize;
  }
  public void resizeSuffix(uint64_t newSuffixSize)
  {
	Debug.Assert(isOpened());

	if (suffixSize() != newSuffixSize)
	{
  //C++ TO C# CONVERTER TODO TASK: The typedef 'value_type' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: atomicUpdate(size(), capacity(), prefixSize(), newSuffixSize, [this](value_type* target)
	  atomicUpdate(size(), capacity(), prefixSize(), new uint64_t(newSuffixSize), (value_type target) =>
	  {
		std::copy(cbegin(), cend(), target);
	  });
	}
  }

  public void rename(string newPath, std::error_code ec)
  {
	m_file.rename(newPath, ec);
	if (ec == null)
	{
	  m_path = newPath;
	}
  }
  public void rename(string newPath)
  {
	m_file.rename(newPath);
	m_path = newPath;
  }

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class F>
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public void atomicUpdate<F>(F && func)
  {
	atomicUpdate0(capacity(), prefixSize(), suffixSize(), std::move(func));
  }

  private string m_path;
  private System.MemoryMappedFile m_file = new System.MemoryMappedFile();
  private uint64_t m_prefixSize = new uint64_t();
  private uint64_t m_suffixSize = new uint64_t();
  private bool m_autoFlush;

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class F>
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  private void atomicUpdate<F>(uint64_t newSize, uint64_t newCapacity, uint64_t newPrefixSize, uint64_t newSuffixSize, F && func)
  {
	Debug.Assert(newSize <= newCapacity);

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: atomicUpdate0(newCapacity, newPrefixSize, newSuffixSize, [this, newSize, &func](FileMappedVector<T>& newVector)
	atomicUpdate0(new uint64_t(newCapacity), new uint64_t(newPrefixSize), new uint64_t(newSuffixSize), (FileMappedVector<T> newVector) =>
	{
	  if (prefixSize() != 0 && newVector.prefixSize() != 0)
	  {
		std::copy(prefixPtr(), prefixPtr() + Math.Min(prefixSize(), newVector.prefixSize()), newVector.prefix());
	  }

	  newVector.sizePtr().CopyFrom(newSize);
	  func(newVector.data());

	  if (suffixSize() != 0 && newVector.suffixSize() != 0)
	  {
		std::copy(suffixPtr(), suffixPtr() + Math.Min(suffixSize(), newVector.suffixSize()), newVector.suffix());
	  }
	});
  }
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class F>
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  private void atomicUpdate0<F>(uint64_t newCapacity, uint64_t newPrefixSize, uint64_t newSuffixSize, F && func)
  {
	if (m_file.path() != m_path)
	{
	  throw new System.Exception("Vector is mapped to a .bak file due to earlier errors");
	}

	boost::filesystem.path bakPath = m_path + ".bak";
	boost::filesystem.path tmpPath = boost::filesystem.unique_path(m_path + ".tmp.%%%%-%%%%");

	if (boost::filesystem.exists(bakPath))
	{
	  boost::filesystem.remove(bakPath);
	}

	Tools.ScopeExit tmpFileDeleter(() =>
	{
	  boost::system.error_code ignore = new boost::system.error_code();
	  boost::filesystem.remove(tmpPath, ignore);
	});

	// Copy file. It is slow but atomic operation
	FileMappedVector<T> tmpVector = new FileMappedVector<T>();
	tmpVector.create(tmpPath.string(), new uint64_t(newCapacity), new uint64_t(newPrefixSize), new uint64_t(newSuffixSize));
	func(tmpVector);
	tmpVector.flush();

	// Swap files
	std::error_code ec = new std::error_code();
	std::error_code ignore = new std::error_code();
	m_file.rename(bakPath.string());
	tmpVector.rename(m_path, ec);
	if (ec != null)
	{
	  // Try to restore and ignore errors
	  m_file.rename(m_path, ignore);
	  throw std::system_error(ec, "Failed to swap temporary and vector files");
	}

	m_path = bakPath.string();
	swap(tmpVector);
	tmpFileDeleter.cancel();

	// Remove .bak file and ignore errors
	tmpVector.close(ignore);
	boost::system.error_code boostError = new boost::system.error_code();
	boost::filesystem.remove(bakPath, boostError);
  }

  private void open(string path, uint64_t prefixSize)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_prefixSize = prefixSize;
	m_prefixSize.CopyFrom(prefixSize);
	m_file.open(path);
	m_path = path;

	if (m_file.size() < prefixSize + metadataSize)
	{
	  throw new System.Exception("FileMappedVector::open() file is too small");
	}

	if (size() > capacity())
	{
	  throw new System.Exception("FileMappedVector::open() vector size is greater than capacity");
	}

	var minRequiredFileSize = m_prefixSize + metadataSize + vectorDataSize();
	if (m_file.size() < minRequiredFileSize)
	{
	  throw new System.Exception("FileMappedVector::open() invalid file size");
	}

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_suffixSize = m_file.size() - minRequiredFileSize;
	m_suffixSize.CopyFrom(m_file.size() - minRequiredFileSize);
  }
  private void create(string path, uint64_t initialCapacity, uint64_t prefixSize, uint64_t suffixSize)
  {
	m_file.create(path, prefixSize + metadataSize + initialCapacity * valueSize + suffixSize, false);
	m_path = path;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_prefixSize = prefixSize;
	m_prefixSize.CopyFrom(prefixSize);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_suffixSize = suffixSize;
	m_suffixSize.CopyFrom(suffixSize);
	sizePtr() = null;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: *capacityPtr() = initialCapacity;
	capacityPtr().CopyFrom(initialCapacity);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	m_file.flush(reinterpret_cast<uint8_t>(sizePtr()), new uint64_t(metadataSize));
  }

  private uint8_t prefixPtr()
  {
	return m_file.data();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const uint8_t* prefixPtr() const
  private uint8_t prefixPtr()
  {
	return m_file.data();
  }
  private uint64_t capacityPtr()
  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	return reinterpret_cast<uint64_t>(prefixPtr() + m_prefixSize);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const uint64_t* capacityPtr() const
  private uint64_t capacityPtr()
  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	return reinterpret_cast<const uint64_t>(prefixPtr() + m_prefixSize);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const uint64_t* sizePtr() const
  private uint64_t sizePtr()
  {
	return capacityPtr() + 1;
  }
  private uint64_t sizePtr()
  {
	return capacityPtr() + 1;
  }
  private T vectorDataPtr()
  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	return reinterpret_cast<T>(sizePtr() + 1);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const T* vectorDataPtr() const
  private T vectorDataPtr()
  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	return reinterpret_cast<const T>(sizePtr() + 1);
  }
  private uint8_t suffixPtr()
  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	return reinterpret_cast<uint8_t>(vectorDataPtr() + capacity());
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const uint8_t* suffixPtr() const
  private uint8_t suffixPtr()
  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	return reinterpret_cast<const uint8_t>(vectorDataPtr() + capacity());
  }

  private uint64_t vectorDataSize()
  {
	return capacity() * valueSize;
  }

  private uint64_t nextCapacity()
  {
	return capacity() + capacity() / 2 + 1;
  }

  private void flushElement(uint64_t index)
  {
	if (m_autoFlush)
	{
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  m_file.flush(reinterpret_cast<uint8_t>(vectorDataPtr() + index), new uint64_t(valueSize));
	}
  }
  private void flushSize()
  {
	if (m_autoFlush)
	{
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  m_file.flush(reinterpret_cast<uint8_t>(sizePtr()), sizeof(uint64_t));
	}
  }
}

}


//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace