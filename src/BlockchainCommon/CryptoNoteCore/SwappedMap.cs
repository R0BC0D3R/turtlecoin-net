// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System;
using System.Collections.Generic;

#if __APPLE__
#endif
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class Key, class T>
public class SwappedMap <Key, T> : System.IDisposable
{
  private class Descriptor
  {
	public ulong offset = new ulong();
	public ulong index = new ulong();
  }


  public class const_iterator
  {
	//typedef ptrdiff_t difference_type;
	//typedef std::bidirectional_iterator_tag iterator_category;
	//typedef std::pair<const Key, T>* pointer;
	//typedef std::pair<const Key, T>& reference;
	//typedef std::pair<const Key, T> value_type;

	public const_iterator(SwappedMap swappedMap, Dictionary<Key, Descriptor>.Enumerator descriptorsIterator)
	{
		this.m_swappedMap = swappedMap;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.m_descriptorsIterator = descriptorsIterator;
		this.m_descriptorsIterator.CopyFrom(descriptorsIterator);
	}

	public static const_iterator operator ++(const_iterator ImpliedObject)
	{
	  ++ImpliedObject.m_descriptorsIterator;
	  return *ImpliedObject;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator !=(const_iterator other) const
	public static bool operator != (const_iterator ImpliedObject, const_iterator other)
	{
	  return ImpliedObject.m_descriptorsIterator != other.m_descriptorsIterator;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator ==(const_iterator other) const
	public static bool operator == (const_iterator ImpliedObject, const_iterator other)
	{
	  return ImpliedObject.m_descriptorsIterator == other.m_descriptorsIterator;
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const System.Tuple<const Key, T>& operator *() const
	public Tuple< Key, T> Indirection()
	{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  return *m_swappedMap.load(m_descriptorsIterator.first, m_descriptorsIterator.second.offset);
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const System.Tuple<const Key, T>* operator ->() const
	public Tuple< Key, T> Dereference()
	{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  return m_swappedMap.load(m_descriptorsIterator.first, m_descriptorsIterator.second.offset);
	}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: typename ClassicUnorderedMapIterator<Key, Descriptor> innerIterator() const
	public Dictionary<Key, Descriptor>.Enumerator innerIterator()
	{
	  return m_descriptorsIterator;
	}

	private SwappedMap m_swappedMap;
	private Dictionary<Key, Descriptor>.Enumerator m_descriptorsIterator;
  }


  public SwappedMap()
  {
  }
  //SwappedMap(const SwappedMap&) = delete;
  public void Dispose()
  {
	close();
  }
  //SwappedMap& operator=(const SwappedMap&) = delete;

  public bool open(string itemFileName, string indexFileName, uint poolSize)
  {
	if (poolSize == 0)
	{
	  return false;
	}
	descriptorsCounter = 0;

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

	  Dictionary<Key, Descriptor> descriptors = new Dictionary<Key, Descriptor>();
	  ulong itemsFileSize = 0;
	  for (ulong i = 0; i < count; ++i)
	  {
		bool valid;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		m_indexesFile.read(reinterpret_cast<char>(valid), sizeof (bool));
		if (m_indexesFile == null)
		{
		  return false;
		}

		Key key = new default(Key);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		m_indexesFile.read(reinterpret_cast<char>(key), sizeof (Key));
		if (m_indexesFile == null)
		{
		  return false;
		}

		uint itemSize = new uint();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		m_indexesFile.read(reinterpret_cast<char>(itemSize), sizeof (uint));
		if (m_indexesFile == null)
		{
		  return false;
		}

		if (valid)
		{
		  Descriptor descriptor = new Descriptor(itemsFileSize, i);
		  descriptors.Add(key, descriptor);
		}
		descriptorsCounter++;
		itemsFileSize += itemSize;
	  }

	  m_descriptors.swap(descriptors);
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
	  m_descriptors.Clear();
	  m_itemsFileSize = 0;
	}

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_poolSize = poolSize;
	m_poolSize.CopyFrom(poolSize);
	m_items.Clear();
	m_cache.Clear();
	m_cacheIterators.Clear();
	m_cacheHits = 0;
	m_cacheMisses = 0;
	return true;
  }
  public void close()
  {
	Console.Write("SwappedMap cache hits: ");
	Console.Write(m_cacheHits);
	Console.Write(", misses: ");
	Console.Write(m_cacheMisses);
	Console.Write(" (");
	Console.Write(std::@fixed);
	Console.Write("{0:2}", (double)m_cacheMisses / (m_cacheHits + m_cacheMisses) * 100);
	Console.Write("{0:2}", "%)");
	Console.Write("{0:2}", "\n");
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong size() const
  public ulong size()
  {
	return m_descriptors.Count;
  }
  public SwappedMap<Key, T>.const_iterator begin()
  {
	return new const_iterator(this, m_descriptors.cbegin());
  }
  public SwappedMap<Key, T>.const_iterator end()
  {
	return new const_iterator(this, m_descriptors.cend());
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint count(const Key& key) const
  public uint count(Key key)
  {
	return m_descriptors.count(key);
  }
  public SwappedMap<Key, T>.const_iterator find(Key key)
  {
	return new const_iterator(this, m_descriptors.find(key));
  }

  public void clear()
  {
	if (m_indexesFile == null)
	{
	  throw new System.Exception("SwappedMap::clear");
	}

	m_indexesFile.seekp(0);
	ulong count = 0;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	m_indexesFile.write(reinterpret_cast<char>(count), sizeof (ulong));
	if (m_indexesFile == null)
	{
	  throw new System.Exception("SwappedMap::clear");
	}

	m_descriptors.Clear();
	m_itemsFileSize = 0;
	m_items.Clear();
	m_cache.Clear();
	m_cacheIterators.Clear();
	descriptorsCounter = 0;
  }
  public void erase(const_iterator iterator)
  {
	if (m_indexesFile == null)
	{
	  throw new System.Exception("SwappedMap::erase");
	}

	Dictionary<Key, Descriptor>.Enumerator descriptorsIterator = iterator.innerIterator();
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	m_indexesFile.seekp(sizeof(ulong) + (sizeof(bool) + sizeof(Key) + sizeof(uint)) * descriptorsIterator.second.index);
	bool valid = false;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	m_indexesFile.write(reinterpret_cast<char>(valid), sizeof (bool));
	if (m_indexesFile == null)
	{
	  throw new System.Exception("SwappedMap::erase");
	}

	m_descriptors.Remove(descriptorsIterator);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	var cacheIteratorsIterator = m_cacheIterators.find(descriptorsIterator.first);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	if (cacheIteratorsIterator != m_cacheIterators.end())
	{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  m_items.Remove(descriptorsIterator.first);
//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL list 'erase' method in C#:
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  m_cache.erase(cacheIteratorsIterator.second);
	  m_cacheIterators.Remove(cacheIteratorsIterator);
	}
  }
  public Tuple<typename SwappedMap<Key, T>.const_iterator, bool> insert(Tuple<const Key, T> value)
  {
	ulong itemsFileSize = new ulong();

	{
	  if (m_itemsFile == null)
	  {
		throw new System.Exception("SwappedMap::insert");
	  }

	  m_itemsFile.seekp(m_itemsFileSize);
	  try
	  {
		boost::archive.binary_oarchive archive = new boost::archive.binary_oarchive(m_itemsFile);
		archive & value.Item2;
	  }
	  catch (System.Exception)
	  {
		throw new System.Exception("SwappedMap::insert");
	  }

	  itemsFileSize = m_itemsFile.tellp();
	}

	{
	  if (m_indexesFile == null)
	  {
		throw new System.Exception("SwappedMap::insert");
	  }

	  m_indexesFile.seekp(sizeof(ulong) + (sizeof(bool) + sizeof(Key) + sizeof(uint)) * descriptorsCounter);
	  bool valid = true;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  m_indexesFile.write(reinterpret_cast<char>(valid), sizeof (bool));
	  if (m_indexesFile == null)
	  {
		throw new System.Exception("SwappedMap::insert");
	  }

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  m_indexesFile.write(reinterpret_cast<const char>(value.Item1), sizeof (value.Item1));
	  if (m_indexesFile == null)
	  {
		throw new System.Exception("SwappedMap::insert");
	  }

	  uint itemSize = (uint)(itemsFileSize - m_itemsFileSize);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  m_indexesFile.write(reinterpret_cast<char>(itemSize), sizeof (uint));
	  if (m_indexesFile == null)
	  {
		throw new System.Exception("SwappedMap::insert");
	  }

	  m_indexesFile.seekp(0);
	  ulong count = descriptorsCounter + 1;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  m_indexesFile.write(reinterpret_cast<char>(count), sizeof (ulong));
	  if (m_indexesFile == null)
	  {
		throw new System.Exception("SwappedMap::insert");
	  }

	}

	Descriptor descriptor = new Descriptor(m_itemsFileSize, descriptorsCounter);
	var descriptorsInsert = m_descriptors.Add(value.Item1, descriptor);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_itemsFileSize = itemsFileSize;
	m_itemsFileSize.CopyFrom(itemsFileSize);

	descriptorsCounter++;

	T newItem = prepare(value.Item1).Item2;
	newItem = value.Item2;
	return Tuple.Create(new const_iterator(this, descriptorsInsert.first), true);
  }

  private std::fstream m_itemsFile = new std::fstream();
  private std::fstream m_indexesFile = new std::fstream();
  private uint m_poolSize = new uint();
  private Dictionary<Key, Descriptor> m_descriptors = new Dictionary<Key, Descriptor>();
  private ulong m_itemsFileSize = new ulong();
  private Dictionary<Key, T> m_items = new Dictionary<Key, T>();
  private LinkedList<Key> m_cache = new LinkedList<Key>();
  private Dictionary<Key, typename LinkedList<Key>.Enumerator> m_cacheIterators = new Dictionary<Key, typename LinkedList<Key>.Enumerator>();
  private ulong m_cacheHits = new ulong();
  private ulong m_cacheMisses = new ulong();
  private ulong descriptorsCounter = new ulong();

  private Tuple< Key, T> prepare(Key key)
  {
	if (m_items.Count == m_poolSize)
	{
	  LinkedList<Key>.Enumerator cacheIter = m_cache.GetEnumerator();
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  m_items.Remove(cacheIter);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  m_cacheIterators.Remove(cacheIter);
//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL list 'erase' method in C#:
	  m_cache.erase(cacheIter);
	}

	Tuple<typename Dictionary<Key, T>.Enumerator, bool> itemInsert = m_items.Add(key, default(T));
//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL list 'insert' method in C#:
	LinkedList<Key>.Enumerator cacheIter = m_cache.insert(m_cache.end(), key);
	m_cacheIterators.Add(key, cacheIter);
	return *itemInsert.Item1;
  }
  private Tuple< Key, T> load(Key key, ulong offset)
  {
	var itemIterator = m_items.find(key);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	if (itemIterator != m_items.end())
	{
	  var cacheIteratorsIterator = m_cacheIterators.find(key);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  if (cacheIteratorsIterator.second != --m_cache.end())
	  {
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
		m_cache.splice(m_cache.end(), m_cache, cacheIteratorsIterator.second);
	  }

	  ++m_cacheHits;
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  return itemIterator;
	}

	Dictionary<Key, Descriptor>.Enumerator descriptorsIterator = m_descriptors.find(key);
	if (descriptorsIterator == m_descriptors.end())
	{
	  throw new System.Exception("SwappedMap::load");
	}

	if (m_itemsFile == null)
	{
	  throw new System.Exception("SwappedMap::load");
	}

//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	m_itemsFile.seekg(descriptorsIterator.second.offset);
	T tempItem = new default(T);
	try
	{
	  boost::archive.binary_iarchive archive = new boost::archive.binary_iarchive(m_itemsFile);
	  archive tempItem;
	}
	catch (System.Exception)
	{
	  throw new System.Exception("SwappedMap::load");
	}

	Tuple<Key, T> item = prepare(key);
	std::swap(tempItem, item.Item2);
	++m_cacheMisses;
	return item;
  }
}


//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace