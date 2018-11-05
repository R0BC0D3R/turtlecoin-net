// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class T>
public class SwappedVector <T> : System.IDisposable
{

  public class const_iterator
  {

	public const_iterator()
	{
	}

	public const_iterator(SwappedVector swappedVector, size_t index)
	{
		this.m_swappedVector = swappedVector;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.m_index = index;
		this.m_index.CopyFrom(index);
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
//ORIGINAL LINE: bool operator <=(const const_iterator& other) const
	public static bool operator <= (const_iterator ImpliedObject, const_iterator other)
	{
	  return ImpliedObject.m_index <= other.m_index;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator ==(const const_iterator& other) const
	public static bool operator == (const_iterator ImpliedObject, const_iterator other)
	{
	  return ImpliedObject.m_index == other.m_index;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator >(const const_iterator& other) const
	public static bool operator > (const_iterator ImpliedObject, const_iterator other)
	{
	  return ImpliedObject.m_index > other.m_index;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator >=(const const_iterator& other) const
	public static bool operator >= (const_iterator ImpliedObject, const_iterator other)
	{
	  return ImpliedObject.m_index >= other.m_index;
	}

	public static const_iterator operator ++(const_iterator ImpliedObject)
	{
	  ++ImpliedObject.m_index;
	  return *ImpliedObject;
	}

	public static const_iterator operator ++(int UnnamedParameter)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: const_iterator i = *this;
	  const_iterator i = new const_iterator(this);
	  ++m_index;
	  return i;
	}

	public static const_iterator operator --(const_iterator ImpliedObject)
	{
	  --ImpliedObject.m_index;
	  return *ImpliedObject;
	}

	public static const_iterator operator --(int UnnamedParameter)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: const_iterator i = *this;
	  const_iterator i = new const_iterator(this);
	  --m_index;
	  return i;
	}

//C++ TO C# CONVERTER TODO TASK: The += operator cannot be overloaded in C#:
	public static const_iterator operator += (ptrdiff_t n)
	{
	  m_index += n;
	  return this;
	}

//C++ TO C# CONVERTER TODO TASK: The -= operator cannot be overloaded in C#:
	public static const_iterator operator -= (ptrdiff_t n)
	{
	  m_index -= n;
	  return this;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const_iterator operator +(ptrdiff_t n) const
	public static const_iterator operator + (const_iterator ImpliedObject, ptrdiff_t n)
	{
	  return new const_iterator(ImpliedObject.m_swappedVector, ImpliedObject.m_index + n);
	}

//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' function:
//ORIGINAL LINE: friend const_iterator operator +(ptrdiff_t n, const const_iterator& i)
	public static const_iterator operator + (ptrdiff_t n, const_iterator i)
	{
	  return new const_iterator(i.m_swappedVector, n + i.m_index);
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ptrdiff_t operator -(const const_iterator& other) const
	public static ptrdiff_t operator - (const_iterator ImpliedObject, const_iterator other)
	{
	  return ImpliedObject.m_index - other.m_index;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const_iterator& operator -(ptrdiff_t n) const
	public static const_iterator operator - (const_iterator ImpliedObject, ptrdiff_t n)
	{
	  return new const_iterator(ImpliedObject.m_swappedVector, ImpliedObject.m_index - n);
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const T& operator *() const
	public T Indirection()
	{
	  return m_swappedVector[m_index];
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const T* operator ->() const
	public T Dereference()
	{
	  return (m_swappedVector)[m_index];
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const T& operator [](ptrdiff_t offset) const
	public T this[ptrdiff_t offset]
	{
		get
		{
		  return m_swappedVector[m_index + offset];
		}
		set
		{
			m_swappedVector[m_index + offset] = value;
		}
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t index() const
	public size_t index()
	{
	  return m_index;
	}

	private SwappedVector m_swappedVector;
	private size_t m_index = new size_t();
  }

  public SwappedVector()
  {
  }
  //SwappedVector(const SwappedVector&) = delete;
  public void Dispose()
  {
	close();
  }
  //SwappedVector& operator=(const SwappedVector&) = delete;

  public bool open(string itemFileName, string indexFileName, size_t poolSize)
  {
	if (poolSize == 0)
	{
	  return false;
	}

	m_itemsFile.open(itemFileName, std::ios.in | std::ios.@out | std::ios.binary);
	m_indexesFile.open(indexFileName, std::ios.in | std::ios.@out | std::ios.binary);
	if (m_itemsFile != null && m_indexesFile != null)
	{
	  ulong count = new ulong();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  m_indexesFile.read(reinterpret_cast<char>(count), sizeof (ulong));
	  if (m_indexesFile == null)
	  {
		return false;
	  }

	  List<ulong> offsets = new List<ulong>();
	  ulong itemsFileSize = 0;
	  for (ulong i = 0; i < count; ++i)
	  {
		uint itemSize = new uint();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		m_indexesFile.read(reinterpret_cast<char>(itemSize), sizeof (uint));
		if (m_indexesFile == null)
		{
		  return false;
		}

		offsets.emplace_back(itemsFileSize);
		itemsFileSize += itemSize;
	  }

	  m_offsets.Swap(offsets);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_itemsFileSize = itemsFileSize;
	  m_itemsFileSize.CopyFrom(itemsFileSize);
	}
	else
	{
	  m_itemsFile.open(itemFileName, std::ios.@out | std::ios.binary);
	  m_itemsFile.close();
	  m_itemsFile.open(itemFileName, std::ios.in | std::ios.@out | std::ios.binary);
	  m_indexesFile.open(indexFileName, std::ios.@out | std::ios.binary);
	  ulong count = 0;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  m_indexesFile.write(reinterpret_cast<char>(count), sizeof (ulong));
	  if (m_indexesFile == null)
	  {
		return false;
	  }

	  m_indexesFile.close();
	  m_indexesFile.open(indexFileName, std::ios.in | std::ios.@out | std::ios.binary);
	  m_offsets.Clear();
	  m_itemsFileSize = 0;
	}

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_poolSize = poolSize;
	m_poolSize.CopyFrom(poolSize);
	m_items.Clear();
	m_cache.Clear();
	m_cacheHits = 0;
	m_cacheMisses = 0;
	return true;
  }
  public void close()
  {
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool empty() const
  public bool empty()
  {
	return m_offsets.Count == 0;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong size() const
  public ulong size()
  {
	return m_offsets.Count;
  }
  public SwappedVector<T>.const_iterator begin()
  {
	return new const_iterator(this, 0);
  }
  public SwappedVector<T>.const_iterator end()
  {
	return new const_iterator(this, m_offsets.Count);
  }
  public T this[ulong index]
  {
	  get
	  {
		var itemIter = m_items.find(index);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
		if (itemIter != m_items.end())
		{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
		  if (itemIter.second.cacheIter != --m_cache.end())
		  {
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
			m_cache.splice(m_cache.end(), m_cache, itemIter.second.cacheIter);
		  }
    
		  ++m_cacheHits;
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
		  return itemIter.second.item;
		}
    
		if (index >= m_offsets.Count)
		{
		  throw new System.Exception("SwappedVector::operator[]");
		}
    
		if (m_itemsFile == null)
		{
		  throw new System.Exception("SwappedVector::operator[]");
		}
    
		m_itemsFile.seekg(m_offsets[index]);
		T tempItem = new default(T);
    
		Common.StdInputStream stream = new Common.StdInputStream(m_itemsFile);
		CryptoNote.BinaryInputStreamSerializer archive = new CryptoNote.BinaryInputStreamSerializer(stream);
		CryptoNote.GlobalMembers.serialize(tempItem, archive.functorMethod);
    
		T item = prepare(new ulong(index));
		std::swap(tempItem, item);
		++m_cacheMisses;
		return item;
	  }
	  set
	  {
		  *item = value;
	  }
  }
  public T front()
  {
	return operator [](0);
  }
  public T back()
  {
	return operator [](m_offsets.Count - 1);
  }
  public void clear()
  {
	if (m_indexesFile == null)
	{
	  throw new System.Exception("SwappedVector::clear");
	}

	m_indexesFile.seekp(0);
	ulong count = 0;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	m_indexesFile.write(reinterpret_cast<char>(count), sizeof (ulong));
	if (m_indexesFile == null)
	{
	  throw new System.Exception("SwappedVector::clear");
	}

	m_offsets.Clear();
	m_itemsFileSize = 0;
	m_items.Clear();
	m_cache.Clear();
  }
  public void pop_back()
  {
	if (m_indexesFile == null)
	{
	  throw new System.Exception("SwappedVector::pop_back");
	}

	m_indexesFile.seekp(0);
	ulong count = m_offsets.Count - 1;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	m_indexesFile.write(reinterpret_cast<char>(count), sizeof (ulong));
	if (m_indexesFile == null)
	{
	  throw new System.Exception("SwappedVector::pop_back");
	}

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_itemsFileSize = m_offsets.back();
	m_itemsFileSize.CopyFrom(m_offsets[m_offsets.Count - 1]);
	m_offsets.RemoveAt(m_offsets.Count - 1);
	var itemIter = m_items.find(m_offsets.Count);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	if (itemIter != m_items.end())
	{
//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL list 'erase' method in C#:
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  m_cache.erase(itemIter.second.cacheIter);
	  m_items.Remove(itemIter);
	}
  }
  public void push_back(T item)
  {
	ulong itemsFileSize = new ulong();

	{
	  if (m_itemsFile == null)
	  {
		throw new System.Exception("SwappedVector::push_back");
	  }

	  m_itemsFile.seekp(m_itemsFileSize);

	  Common.StdOutputStream stream = new Common.StdOutputStream(m_itemsFile);
	  CryptoNote.BinaryOutputStreamSerializer archive = new CryptoNote.BinaryOutputStreamSerializer(stream);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
	  CryptoNote.GlobalMembers.serialize(const_cast<T&>(item), archive.functorMethod);

	  itemsFileSize = m_itemsFile.tellp();
	}

	{
	  if (m_indexesFile == null)
	  {
		throw new System.Exception("SwappedVector::push_back");
	  }

	  m_indexesFile.seekp(sizeof(ulong) + sizeof(uint) * m_offsets.Count);
	  uint itemSize = (uint)(itemsFileSize - m_itemsFileSize);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  m_indexesFile.write(reinterpret_cast<char>(itemSize), sizeof (uint));
	  if (m_indexesFile == null)
	  {
		throw new System.Exception("SwappedVector::push_back");
	  }

	  m_indexesFile.seekp(0);
	  ulong count = m_offsets.Count + 1;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  m_indexesFile.write(reinterpret_cast<char>(count), sizeof (ulong));
	  if (m_indexesFile == null)
	  {
		throw new System.Exception("SwappedVector::push_back");
	  }
	}

	m_offsets.Add(m_itemsFileSize);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_itemsFileSize = itemsFileSize;
	m_itemsFileSize.CopyFrom(itemsFileSize);

	T newItem = prepare(m_offsets.Count - 1);
	newItem = item;
  }

//C++ TO C# CONVERTER TODO TASK: The implementation of the following type could not be found:
//  struct ItemEntry;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following type could not be found:
//  struct CacheEntry;

  private class ItemEntry
  {
	public T item = new T();
	public LinkedList<CacheEntry>.Enumerator cacheIter;
  }

  private class CacheEntry
  {
	public SortedDictionary<ulong, ItemEntry>.Enumerator itemIter;
  }

  private std::fstream m_itemsFile = new std::fstream();
  private std::fstream m_indexesFile = new std::fstream();
  private size_t m_poolSize = new size_t();
  private List<ulong> m_offsets = new List<ulong>();
  private ulong m_itemsFileSize = new ulong();
  private SortedDictionary<ulong, ItemEntry> m_items = new SortedDictionary<ulong, ItemEntry>();
  private LinkedList<CacheEntry> m_cache = new LinkedList<CacheEntry>();
  private ulong m_cacheHits = new ulong();
  private ulong m_cacheMisses = new ulong();

  private T prepare(ulong index)
  {
	if (m_items.Count == m_poolSize)
	{
	  var cacheIter = m_cache.GetEnumerator();
	  m_items.Remove(cacheIter.itemIter);
//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL list 'erase' method in C#:
	  m_cache.erase(cacheIter);
	}

	var itemIter = m_items.Add(index, new ItemEntry());
	CacheEntry cacheEntry = new CacheEntry(itemIter.first);
//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL list 'insert' method in C#:
	var cacheIter = m_cache.insert(m_cache.end(), cacheEntry);
	itemIter.first.second.cacheIter = cacheIter;
	return itemIter.first.second.item;
  }
}


//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace