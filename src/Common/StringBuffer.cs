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



namespace Common
{

// 'StringBuffer' is a string of fixed maximum size.
//C++ TO C# CONVERTER TODO TASK: C++ 'constraints' are not converted by C++ to C# Converter:
//ORIGINAL LINE: template<uint64_t MAXIMUM_SIZE_VALUE> class StringBuffer {
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename MAXIMUM_SIZE_VALUE>
public class StringBuffer <MAXIMUM_SIZE_VALUE> : System.IDisposable
{
	private bool InstanceFieldsInitialized = false;

	private void InitializeInstanceFields()
	{
		data = Arrays.InitializeWithDefaultInstances<Object>(MAXIMUM_SIZE);
	}


//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public Size MAXIMUM_SIZE = MAXIMUM_SIZE_VALUE;
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public readonly Size INVALID = new Size();

//C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
//  static_assert(MAXIMUM_SIZE != 0, "StringBuffer's size must not be zero");

  // Default constructor.
  // After construction, 'StringBuffer' is empty, that is 'size' == 0
  public StringBuffer()
  {
	  if (!InstanceFieldsInitialized)
	  {
		  InitializeInstanceFields();
		  InstanceFieldsInitialized = true;
	  }
	  this.size = 0;
  }

  // Direct constructor.
  // Copies string from 'stringData' to 'StringBuffer'.
  // The behavior is undefined unless ('stringData' != 'nullptr' || 'stringSize' == 0) && 'stringSize' <= 'MAXIMUM_SIZE'.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public StringBuffer(Object stringData, Size stringSize)
  {
	  if (!InstanceFieldsInitialized)
	  {
		  InitializeInstanceFields();
		  InstanceFieldsInitialized = true;
	  }
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.size = stringSize;
	  this.size.CopyFrom(stringSize);
	Debug.Assert(stringData != null || size == 0);
	Debug.Assert(size <= MAXIMUM_SIZE);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
	memcpy(data, stringData, size);
  }

  // Constructor from C array.
  // Copies string from 'stringData' to 'StringBuffer'.
  // The behavior is undefined unless ('stringData' != 'nullptr' || 'stringSize' == 0) && 'stringSize' <= 'MAXIMUM_SIZE'. Input state can be malformed using poiner conversions.
//C++ TO C# CONVERTER TODO TASK: C++ 'constraints' are not converted by C++ to C# Converter:
//ORIGINAL LINE: template<Size stringSize> explicit StringBuffer(const Object(&stringData)[stringSize]) : size(stringSize - 1) {
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename stringSize>
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public StringBuffer<stringSize>(Object[] stringData)
  {
	  if (!InstanceFieldsInitialized)
	  {
		  InitializeInstanceFields();
		  InstanceFieldsInitialized = true;
	  }
	  this.size = stringSize - 1;
	Debug.Assert(stringData != null || size == 0);
	Debug.Assert(size <= MAXIMUM_SIZE);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
	memcpy(data, stringData, size);
  }

  // Constructor from StringView
  // Copies string from 'stringView' to 'StringBuffer'.
  // The behavior is undefined unless 'stringView.size()' <= 'MAXIMUM_SIZE'.
  public StringBuffer(StringView stringView)
  {
	  if (!InstanceFieldsInitialized)
	  {
		  InitializeInstanceFields();
		  InstanceFieldsInitialized = true;
	  }
	  this.size = stringView.getSize();
	Debug.Assert(size <= MAXIMUM_SIZE);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
	memcpy(data, stringView.getData(), size);
  }

  // Copy constructor.
  // Copies string from 'other' to 'StringBuffer'.
  public StringBuffer(StringBuffer other)
  {
	  if (!InstanceFieldsInitialized)
	  {
		  InitializeInstanceFields();
		  InstanceFieldsInitialized = true;
	  }
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.size = other.size;
	  this.size.CopyFrom(other.size);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
	memcpy(data, other.data, size);
  }

  // Destructor.
  // No special action is performed.
  public void Dispose()
  {
  }

  // Copy assignment operator.
//C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
//ORIGINAL LINE: StringBuffer& operator =(const StringBuffer& other)
  public StringBuffer CopyFrom(StringBuffer other)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: size = other.size;
	size.CopyFrom(other.size);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
	memcpy(data, other.data, size);
	return this;
  }

  // StringView assignment operator.
  // Copies string from 'stringView' to 'StringBuffer'.
  // The behavior is undefined unless 'stringView.size()' <= 'MAXIMUM_SIZE'.
//C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
//ORIGINAL LINE: StringBuffer& operator =(StringView stringView)
  public StringBuffer CopyFrom(StringView stringView)
  {
	Debug.Assert(stringView.getSize() <= MAXIMUM_SIZE);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
	memcpy(data, stringView.getData(), stringView.getSize());
	size = stringView.getSize();
	return this;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: operator StringView() const
  public static implicit operator StringView(StringBuffer ImpliedObject)
  {
	return new StringView(ImpliedObject.data, ImpliedObject.size);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: explicit operator string() const
  public static explicit operator string(StringBuffer ImpliedObject)
  {
	return (string)(ImpliedObject.data, ImpliedObject.size);
  }

//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public Object getData()
  {
	return data;
  }

//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Object* getData() const
  public Object getData()
  {
	return data;
  }

//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Size getSize() const
  public Size getSize()
  {
	return size;
  }

  // Return false if 'StringView' is not EMPTY.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isEmpty() const
  public bool isEmpty()
  {
	return size == 0;
  }

  // Get 'StringBuffer' element by index.
  // The behavior is undefined unless 'index' < 'size'.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public Object this[Size index]
  {
	  get
	  {
		Debug.Assert(index < size);
		return data[index];
	  }
	  set
	  {
		  data[index] = value;
	  }
  }

  // Get 'StringBuffer' element by index.
  // The behavior is undefined unless 'index' < 'size'.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Object& operator [](Size index) const
  public Object this[Size index]
  {
	  get
	  {
		Debug.Assert(index < size);
		return data[index];
	  }
	  set
	  {
		  data[index] = value;
	  }
  }

  // Get first element.
  // The behavior is undefined unless 'size' > 0
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public Object first()
  {
	Debug.Assert(size > 0);
	return data[0];
  }

  // Get first element.
  // The behavior is undefined unless 'size' > 0
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Object& first() const
  public Object first()
  {
	Debug.Assert(size > 0);
	return data[0];
  }

  // Get last element.
  // The behavior is undefined unless 'size' > 0
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public Object last()
  {
	Debug.Assert(size > 0);
	return data[(size - 1)];
  }

  // Get last element.
  // The behavior is undefined unless 'size' > 0
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Object& last() const
  public Object last()
  {
	Debug.Assert(size > 0);
	return data[(size - 1)];
  }

  // Return a pointer to the first element.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public Object begin()
  {
	return data;
  }

  // Return a pointer to the first element.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Object* begin() const
  public Object begin()
  {
	return data;
  }

  // Return a pointer after the last element.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public Object end()
  {
	return data + size;
  }

  // Return a pointer after the last element.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Object* end() const
  public Object end()
  {
	return data + size;
  }

  // Compare elements of two strings, return false if there is a difference.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator ==(StringView other) const
  public static bool operator == (StringBuffer ImpliedObject, StringView other)
  {
	if (ImpliedObject.size == other.getSize())
	{
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	  for (Size i = 0;; ++i)
	  {
		if (i == ImpliedObject.size)
		{
		  return true;
		}

		if (!(*(ImpliedObject.data + i) == *(other.getData() + i)))
		{
		  break;
		}
	  }
	}

	return false;
  }

  // Compare elements two strings, return false if there is no difference.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator !=(StringView other) const
  public static bool operator != (StringBuffer ImpliedObject, StringView other)
  {
	return !(*ImpliedObject == other);
  }

  // Compare two strings character-wise.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator <(StringView other) const
  public static bool operator < (StringBuffer ImpliedObject, StringView other)
  {
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	Size count = other.getSize() < ImpliedObject.size ? other.getSize() : ImpliedObject.size;
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	for (Size i = 0; i < count; ++i)
	{
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	  Object char1 = (ImpliedObject.data + i);
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	  Object char2 = (other.getData() + i);
	  if (char1 < char2)
	  {
		return true;
	  }

	  if (char2 < char1)
	  {
		return false;
	  }
	}

	return ImpliedObject.size < other.getSize();
  }

  // Compare two strings character-wise.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator <=(StringView other) const
  public static bool operator <= (StringBuffer ImpliedObject, StringView other)
  {
	return !(other < *ImpliedObject);
  }

  // Compare two strings character-wise.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator >(StringView other) const
  public static bool operator > (StringBuffer ImpliedObject, StringView other)
  {
	return other < *ImpliedObject;
  }

  // Compare two strings character-wise.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator >=(StringView other) const
  public static bool operator >= (StringBuffer ImpliedObject, StringView other)
  {
	return !(*ImpliedObject < other);
  }

  // Return false if 'StringView' does not contain 'object' at the beginning.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool beginsWith(const Object& object) const
  public bool beginsWith(Object @object)
  {
	if (size == 0)
	{
	  return false;
	}

	return data[0] == @object;
  }

  // Return false if 'StringView' does not contain 'other' at the beginning.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool beginsWith(StringView other) const
  public bool beginsWith(StringView other)
  {
	if (size >= other.getSize())
	{
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	  for (Size i = 0;; ++i)
	  {
		if (i == other.getSize())
		{
		  return true;
		}

		if (!(data[i] == *(other.getData() + i)))
		{
		  break;
		}
	  }
	}

	return false;
  }

  // Return false if 'StringView' does not contain 'object'.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool contains(const Object& object) const
  public bool contains(Object @object)
  {
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	for (Size i = 0; i < size; ++i)
	{
	  if (data[i] == @object)
	  {
		return true;
	  }
	}

	return false;
  }

  // Return false if 'StringView' does not contain 'other'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool contains(StringView other) const
  public bool contains(StringView other)
  {
	if (size >= other.getSize())
	{
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	  Size i = size - other.getSize();
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	  for (Size j = 0; !(i < j); ++j)
	  {
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
		for (Size k = 0;; ++k)
		{
		  if (k == other.getSize())
		  {
			return true;
		  }

		  if (!(data[j + k] == *(other.getData() + k)))
		  {
			break;
		  }
		}
	  }
	}

	return false;
  }

  // Return false if 'StringView' does not contain 'object' at the end.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool endsWith(const Object& object) const
  public bool endsWith(Object @object)
  {
	if (size == 0)
	{
	  return false;
	}

	return data[(size - 1)] == @object;
  }

  // Return false if 'StringView' does not contain 'other' at the end.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool endsWith(StringView other) const
  public bool endsWith(StringView other)
  {
	if (size >= other.getSize())
	{
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	  Size i = size - other.getSize();
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	  for (Size j = 0;; ++j)
	  {
		if (j == other.getSize())
		{
		  return true;
		}

		if (!(data[i + j] == *(other.getData() + j)))
		{
		  break;
		}
	  }
	}

	return false;
  }

  // Looks for the first occurence of 'object' in 'StringView',
  // returns index or INVALID if there are no occurences.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Size find(const Object& object) const
  public Size find(Object @object)
  {
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	for (Size i = 0; i < size; ++i)
	{
	  if (data[i] == @object)
	  {
		return i;
	  }
	}

	return INVALID;
  }

  // Looks for the first occurence of 'other' in 'StringView',
  // returns index or INVALID if there are no occurences.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Size find(StringView other) const
  public Size find(StringView other)
  {
	if (size >= other.getSize())
	{
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	  Size i = size - other.getSize();
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	  for (Size j = 0; !(i < j); ++j)
	  {
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
		for (Size k = 0;; ++k)
		{
		  if (k == other.getSize())
		  {
			return j;
		  }

		  if (!(data[j + k] == *(other.getData() + k)))
		  {
			break;
		  }
		}
	  }
	}

	return INVALID;
  }

  // Looks for the last occurence of 'object' in 'StringView',
  // returns index or INVALID if there are no occurences.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Size findLast(const Object& object) const
  public Size findLast(Object @object)
  {
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	for (Size i = 0; i < size; ++i)
	{
	  if (data[(size - 1 - i)] == @object)
	  {
		return size - 1 - i;
	  }
	}

	return INVALID;
  }

  // Looks for the first occurence of 'other' in 'StringView',
  // returns index or INVALID if there are no occurences.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Size findLast(StringView other) const
  public Size findLast(StringView other)
  {
	if (size >= other.getSize())
	{
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	  Size i = size - other.getSize();
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	  for (Size j = 0; !(i < j); ++j)
	  {
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
		for (Size k = 0;; ++k)
		{
		  if (k == other.getSize())
		  {
			return i - j;
		  }

		  if (!(data[(i - j + k)] == *(other.getData() + k)))
		  {
			break;
		  }
		}
	  }
	}

	return INVALID;
  }

  // Returns substring of 'headSize' first elements.
  // The behavior is undefined unless 'headSize' <= 'size'.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: StringView head(Size headSize) const
  public StringView head(Size headSize)
  {
	Debug.Assert(headSize <= size);
	return new StringView(data, new Size(headSize));
  }

  // Returns substring of 'tailSize' last elements.
  // The behavior is undefined unless 'tailSize' <= 'size'.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: StringView tail(Size tailSize) const
  public StringView tail(Size tailSize)
  {
	Debug.Assert(tailSize <= size);
	return new StringView(data + (size - tailSize), new Size(tailSize));
  }

  // Returns 'StringView' without 'headSize' first elements.
  // The behavior is undefined unless 'headSize' <= 'size'.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: StringView unhead(Size headSize) const
  public StringView unhead(Size headSize)
  {
	Debug.Assert(headSize <= size);
	return new StringView(data + headSize, size - headSize);
  }

  // Returns 'StringView' without 'tailSize' last elements.
  // The behavior is undefined unless 'tailSize' <= 'size'.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: StringView untail(Size tailSize) const
  public StringView untail(Size tailSize)
  {
	Debug.Assert(tailSize <= size);
	return new StringView(data, size - tailSize);
  }

  // Returns substring starting at 'startIndex' and contaning 'endIndex' - 'startIndex' elements.
  // The behavior is undefined unless 'startIndex' <= 'endIndex' and 'endIndex' <= 'size'.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: StringView range(Size startIndex, Size endIndex) const
  public StringView range(Size startIndex, Size endIndex)
  {
	Debug.Assert(startIndex <= endIndex != null && endIndex <= size);
	return new StringView(data + startIndex, endIndex - startIndex);
  }

  // Returns substring starting at 'startIndex' and contaning 'sliceSize' elements.
  // The behavior is undefined unless 'startIndex' <= 'size' and 'sliceSize' <= 'size' - 'startIndex'.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: StringView slice(Size startIndex, Size sliceSize) const
  public StringView slice(Size startIndex, Size sliceSize)
  {
	Debug.Assert(startIndex <= size != null && sliceSize <= size - startIndex);
	return new StringView(data + startIndex, new Size(sliceSize));
  }

  // Appends 'object' to 'StringBuffer'.
  // The behavior is undefined unless 1 <= 'MAXIMUM_SIZE' - 'size'.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public StringBuffer append(Object @object)
  {
	Debug.Assert(1 <= MAXIMUM_SIZE - size);
	data[size] = @object;
	++size;
	return this;
  }

  // Appends 'stringView' to 'StringBuffer'.
  // The behavior is undefined unless 'stringView.size()' <= 'MAXIMUM_SIZE' - 'size'.
  public StringBuffer append(StringView stringView)
  {
	Debug.Assert(stringView.getSize() <= MAXIMUM_SIZE - size);
	if (stringView.getSize() != 0)
	{
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
	  memcpy(data + size, stringView.getData(), stringView.getSize());
	  size += stringView.getSize();
	}

	return this;
  }

  // Sets 'StringBuffer' to empty state, that is 'size' == 0
  public StringBuffer clear()
  {
	size = 0;
	return this;
  }

  // Removes substring starting at 'startIndex' and contaning 'cutSize' elements.
  // The behavior is undefined unless 'startIndex' <= 'size' and 'cutSize' <= 'size' - 'startIndex'.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public StringBuffer cut(Size startIndex, Size cutSize)
  {
	Debug.Assert(startIndex <= size != null && cutSize <= size - startIndex);
	if (cutSize != 0)
	{
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
	  memcpy(data + startIndex, data + startIndex + cutSize, size - startIndex - cutSize);
	  size -= cutSize;
	}

	return this;
  }

  // Copy 'object' to each element of 'StringBuffer'.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public StringBuffer fill(Object @object)
  {
	if (size > 0)
	{
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
	  memset(data, @object, size);
	}

	return this;
  }

  // Inserts 'object' into 'StringBuffer' at 'index'.
  // The behavior is undefined unless 'index' <= 'size' and 1 <= 'MAXIMUM_SIZE' - 'size'.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public StringBuffer insert(Size index, Object @object)
  {
	Debug.Assert(index <= size);
	Debug.Assert(1 <= MAXIMUM_SIZE - size);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memmove' has no equivalent in C#:
	memmove(data + index + 1, data + index, size - index);
	data[index] = @object;
	++size;
	return this;
  }

  // Inserts 'stringView' into 'StringBuffer' at 'index'.
  // The behavior is undefined unless 'index' <= 'size' and 'stringView.size()' <= 'MAXIMUM_SIZE' - 'size'.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public StringBuffer insert(Size index, StringView stringView)
  {
	Debug.Assert(index <= size);
	Debug.Assert(stringView.getSize() <= MAXIMUM_SIZE - size);
	if (stringView.getSize() != 0)
	{
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memmove' has no equivalent in C#:
	  memmove(data + index + stringView.getSize(), data + index, size - index);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
	  memcpy(data + index, stringView.getData(), stringView.getSize());
	  size += stringView.getSize();
	}

	return this;
  }

  // Overwrites 'StringBuffer' starting at 'index' with 'stringView', possibly expanding 'StringBuffer'.
  // The behavior is undefined unless 'index' <= 'size' and 'stringView.size()' <= 'MAXIMUM_SIZE' - 'index'.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public StringBuffer overwrite(Size index, StringView stringView)
  {
	Debug.Assert(index <= size);
	Debug.Assert(stringView.getSize() <= MAXIMUM_SIZE - index);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
	memcpy(data + index, stringView.getData(), stringView.getSize());
	if (size < index + stringView.getSize())
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: size = index + stringView.getSize();
	  size.CopyFrom(index + stringView.getSize());
	}

	return this;
  }

  // Sets 'size' to 'bufferSize', assigning value '\0' to newly inserted elements.
  // The behavior is undefined unless 'bufferSize' <= 'MAXIMUM_SIZE'.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public StringBuffer resize(Size bufferSize)
  {
	Debug.Assert(bufferSize <= MAXIMUM_SIZE);
	if (bufferSize > size)
	{
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
	  memset(data + size, 0, bufferSize - size);
	}

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: size = bufferSize;
	size.CopyFrom(bufferSize);
	return this;
  }

  // Reverse 'StringBuffer' elements.
  public StringBuffer reverse()
  {
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	for (Size i = 0; i < size / 2; ++i)
	{
//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
	  Object @object = data[i];
	  data[i] = (data + (size - 1 - i));
	  data[(size - 1 - i)] = @object;
	}

	return this;
  }

  // Sets 'size' to 'bufferSize'.
  // The behavior is undefined unless 'bufferSize' <= 'size'.
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  public StringBuffer shrink(Size bufferSize)
  {
	Debug.Assert(bufferSize <= size);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: size = bufferSize;
	size.CopyFrom(bufferSize);
	return this;
  }

//C++ TO C# CONVERTER TODO TASK: The typedef 'Object' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  protected Object[] data;
//C++ TO C# CONVERTER TODO TASK: The typedef 'Size' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
  protected Size size = new Size();
}

//C++ TO C# CONVERTER TODO TASK: C++ 'constraints' are not converted by C++ to C# Converter:
//ORIGINAL LINE: template<uint64_t MAXIMUM_SIZE> const typename StringBuffer<MAXIMUM_SIZE>::Size StringBuffer<MAXIMUM_SIZE>::INVALID = std::numeric_limits<typename StringBuffer<MAXIMUM_SIZE>::Size>::max();
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename MAXIMUM_SIZE>

}
