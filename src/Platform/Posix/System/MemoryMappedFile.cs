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



namespace System
{

public class MemoryMappedFile : System.IDisposable
{
  public MemoryMappedFile()
  {
	  this.m_file = -1;
	  this.m_size = 0;
	  this.m_data = null;
  }
  public void Dispose()
  {
	std::error_code ignore = new std::error_code();
	close(ref ignore);
  }

  public void create(string path, uint64_t size, bool overwrite, ref std::error_code ec)
  {
	if (isOpened())
	{
	  close(ref ec);
	  if (ec != null)
	  {
		return;
	  }
	}

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: Tools::ScopeExit failExitHandler([this, &ec]
	Tools.ScopeExit failExitHandler(() =>
	{
	  ec = std::error_code(errno, std::system_category());
	  std::error_code ignore = new std::error_code();
	  close(ref ignore);
	});

	m_file = global::open(path, O_RDWR | O_CREAT | (overwrite ? O_TRUNC : O_EXCL), S_IRUSR | S_IWUSR);
	if (m_file == -1)
	{
	  return;
	}

	int result = global::ftruncate(m_file, (off_t)size);
	if (result == -1)
	{
	  return;
	}

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	m_data = reinterpret_cast<uint8_t>(global::mmap(null, (size_t)size, PROT_READ | PROT_WRITE, MAP_SHARED, m_file, 0));
	if (m_data == MAP_FAILED)
	{
	  return;
	}

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_size = size;
	m_size.CopyFrom(size);
	m_path = path;
	ec = std::error_code();

	failExitHandler.cancel();
  }
  public void create(string path, uint64_t size, bool overwrite)
  {
	std::error_code ec = new std::error_code();
	create(path, new uint64_t(size), overwrite, ref ec);
	if (ec != null)
	{
	  throw std::system_error(ec, "MemoryMappedFile::create");
	}
  }
  public void open(string path, ref std::error_code ec)
  {
	if (isOpened())
	{
	  close(ref ec);
	  if (ec != null)
	  {
		return;
	  }
	}

//C++ TO C# CONVERTER TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: Tools::ScopeExit failExitHandler([this, &ec]
	Tools.ScopeExit failExitHandler(() =>
	{
	  ec = std::error_code(errno, std::system_category());
	  std::error_code ignore = new std::error_code();
	  close(ref ignore);
	});

	m_file = global::open(path, O_RDWR, S_IRUSR | S_IWUSR);
	if (m_file == -1)
	{
	  return;
	}

	stat fileStat = new stat();
	int result = global::fstat(m_file, fileStat);
	if (result == -1)
	{
	  return;
	}

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_size = static_cast<uint64_t>(fileStat.st_size);
	m_size.CopyFrom((uint64_t)fileStat.st_size);

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	m_data = reinterpret_cast<uint8_t>(global::mmap(null, (size_t)m_size, PROT_READ | PROT_WRITE, MAP_SHARED, m_file, 0));
	if (m_data == MAP_FAILED)
	{
	  return;
	}

	m_path = path;
	ec = std::error_code();

	failExitHandler.cancel();
  }
  public void open(string path)
  {
	std::error_code ec = new std::error_code();
	open(path, ref ec);
	if (ec != null)
	{
	  throw std::system_error(ec, "MemoryMappedFile::open");
	}
  }
  public void close(ref std::error_code ec)
  {
	int result;
	if (m_data != null)
	{
	  flush(m_data, new uint64_t(m_size), ref ec);
	  if (ec != null)
	  {
		return;
	  }

	  result = global::munmap(m_data, (size_t)m_size);
	  if (result == 0)
	  {
		m_data = null;
	  }
	  else
	  {
		ec = std::error_code(errno, std::system_category());
		return;
	  }
	}

	if (m_file != -1)
	{
	  result = global::close(m_file);
	  if (result == 0)
	  {
		m_file = -1;
		ec = std::error_code();
	  }
	  else
	  {
		ec = std::error_code(errno, std::system_category());
		return;
	  }
	}

	ec = std::error_code();
  }
  public void close()
  {
	std::error_code ec = new std::error_code();
	close(ref ec);
	if (ec != null)
	{
	  throw std::system_error(ec, "MemoryMappedFile::close");
	}
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const string& path() const
  public string path()
  {
	Debug.Assert(isOpened());

	return m_path;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t size() const
  public uint64_t size()
  {
	Debug.Assert(isOpened());

	return m_size;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const uint8_t* data() const
  public uint8_t data()
  {
	Debug.Assert(isOpened());

	return m_data;
  }
  public uint8_t data()
  {
	Debug.Assert(isOpened());

	return m_data;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isOpened() const
  public bool isOpened()
  {
	return m_data != null;
  }

  public void rename(string newPath, ref std::error_code ec)
  {
	Debug.Assert(isOpened());

	int result = global::rename(m_path, newPath);
	if (result == 0)
	{
	  m_path = newPath;
	  ec = std::error_code();
	}
	else
	{
	  ec = std::error_code(errno, std::system_category());
	}
  }
  public void rename(string newPath)
  {
	Debug.Assert(isOpened());

	std::error_code ec = new std::error_code();
	rename(newPath, ref ec);
	if (ec != null)
	{
	  throw std::system_error(ec, "MemoryMappedFile::rename");
	}
  }

  public void flush(uint8_t data, uint64_t size, ref std::error_code ec)
  {
	Debug.Assert(isOpened());

	uintptr_t pageSize = (uintptr_t)sysconf(_SC_PAGESIZE);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	uintptr_t dataAddr = reinterpret_cast<uintptr_t>(data);
	uintptr_t pageOffset = (dataAddr / pageSize) * pageSize;

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	int result = global::msync(reinterpret_cast<object>(pageOffset), (size_t)(dataAddr % pageSize + size), MS_SYNC);
	if (result == 0)
	{
	  result = global::fsync(m_file);
	  if (result == 0)
	  {
		ec = std::error_code();
		return;
	  }
	}

	ec = std::error_code(errno, std::system_category());
  }
  public void flush(uint8_t data, uint64_t size)
  {
	Debug.Assert(isOpened());

	std::error_code ec = new std::error_code();
	flush(data, new uint64_t(size), ref ec);
	if (ec != null)
	{
	  throw std::system_error(ec, "MemoryMappedFile::flush");
	}
  }

  public void swap(MemoryMappedFile other)
  {
	std::swap(m_file, other.m_file);
	std::swap(m_path, other.m_path);
	std::swap(m_data, other.m_data);
	std::swap(m_size, other.m_size);
  }

  private int m_file;
  private string m_path;
  private uint64_t m_size = new uint64_t();
  private uint8_t[] m_data;
}

}





