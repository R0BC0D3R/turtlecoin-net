// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Diagnostics;

namespace Common
{

// 'ArrayView' is a pair of pointer to constant object of parametrized type and size.
// It is recommended to pass 'ArrayView' to procedures by value.
// 'ArrayView' supports 'EMPTY' and 'NIL' representations as follows:
//   'data' == 'nullptr' && 'size' == 0 - EMPTY NIL
//   'data' != 'nullptr' && 'size' == 0 - EMPTY NOTNIL
//   'data' == 'nullptr' && 'size' > 0 - Undefined
//   'data' != 'nullptr' && 'size' > 0 - NOTEMPTY NOTNIL
// For signed integer 'Size', 'ArrayView' with 'size' < 0 is undefined.
//C++ TO C# CONVERTER TODO TASK: C++ template specifiers containing defaults cannot be converted to C#:
//ORIGINAL LINE: template<class Object = byte, class Size = ulong>
public class ArrayView <Object = byte, Size = ulong> : System.IDisposable
{

  public readonly Size INVALID = new Size();
  public readonly ArrayView EMPTY = new ArrayView();
  public readonly ArrayView NIL = new ArrayView();

  // Default constructor.
  // Leaves object uninitialized. Any usage before initializing it is undefined.
//C++ TO C# CONVERTER TODO TASK: Statements that are interrupted by preprocessor statements are not converted by C++ to C# Converter:
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
  ArrayView()
#if ! NDEBUG
//C++ TO C# CONVERTER TODO TASK: The following method format was not recognized, possibly due to an unrecognized macro:
	: data(null), size(INVALID) // In debug mode, fill in object with invalid state (undefined).
#endif
	{
	}

  // Direct constructor.
  // The behavior is undefined unless 'arrayData' != 'nullptr' || 'arraySize' == 0
  public ArrayView(Object arrayData, Size arraySize)
  {
	  this.data = arrayData;
	  this.size = arraySize;
	Debug.Assert(data != null || size == 0);
  }

  // Constructor from C array.
  // The behavior is undefined unless 'arrayData' != 'nullptr' || 'arraySize' == 0. Input state can be malformed using poiner conversions.
//C++ TO C# CONVERTER TODO TASK: C++ 'constraints' are not converted by C++ to C# Converter:
//ORIGINAL LINE: template<Size arraySize> ArrayView(const Object(&arrayData)[arraySize]) : data(arrayData), size(arraySize) {
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename arraySize>
  public ArrayView<arraySize>(Object[] arrayData)
  {
	  this.data = arrayData;
	  this.size = arraySize;
	Debug.Assert(data != null || size == 0);
  }

  // Copy constructor.
  // Performs default action - bitwise copying of source object.
  // The behavior is undefined unless 'other' 'ArrayView' is in defined state, that is 'data' != 'nullptr' || 'size' == 0
  public ArrayView(ArrayView other)
  {
	  this.data = other.data;
	  this.size = other.size;
	Debug.Assert(data != null || size == 0);
  }

  // Destructor.
  // No special action is performed.
  public void Dispose()
  {
  }

  // Copy assignment operator.
  // The behavior is undefined unless 'other' 'ArrayView' is in defined state, that is 'data' != 'nullptr' || 'size' == 0
//C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
//ORIGINAL LINE: ArrayView& operator =(const ArrayView& other)
  public ArrayView CopyFrom(ArrayView other)
  {
	Debug.Assert(other.data != null || other.size == 0);
	data = other.data;
	size = other.size;
	return this;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Object* getData() const
  public Object getData()
  {
	Debug.Assert(data != null || size == 0);
	return data;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Size getSize() const
  public Size getSize()
  {
	Debug.Assert(data != null || size == 0);
	return size;
  }

  // Return false if 'ArrayView' is not EMPTY.
  // The behavior is undefined unless 'ArrayView' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isEmpty() const
  public bool isEmpty()
  {
	Debug.Assert(data != null || size == 0);
	return size == 0;
  }

  // Return false if 'ArrayView' is not NIL.
  // The behavior is undefined unless 'ArrayView' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isNil() const
  public bool isNil()
  {
	Debug.Assert(data != null || size == 0);
	return data == null;
  }

  // Get 'ArrayView' element by index.
  // The behavior is undefined unless 'ArrayView' was initialized and 'index' < 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Object& operator [](Size index) const
  public Object this[Size index]
  {
	  get
	  {
		Debug.Assert(data != null || size == 0);
		Debug.Assert(index < size);
		return data[index];
	  }
	  set
	  {
		  data[index] = value;
	  }
  }

  // Get first element.
  // The behavior is undefined unless 'ArrayView' was initialized and 'size' > 0
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Object& first() const
  public Object first()
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(size > 0);
	return data[0];
  }

  // Get last element.
  // The behavior is undefined unless 'ArrayView' was initialized and 'size' > 0
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Object& last() const
  public Object last()
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(size > 0);
	return data[(size - 1)];
  }

  // Return a pointer to the first element.
  // The behavior is undefined unless 'ArrayView' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Object* begin() const
  public Object begin()
  {
	Debug.Assert(data != null || size == 0);
	return data;
  }

  // Return a pointer after the last element.
  // The behavior is undefined unless 'ArrayView' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Object* end() const
  public Object end()
  {
	Debug.Assert(data != null || size == 0);
	return data + size;
  }

  // Compare elements of two arrays, return false if there is a difference.
  // EMPTY and NIL arrays are considered equal.
  // The behavior is undefined unless both arrays were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator ==(ArrayView other) const
  public static bool operator == (ArrayView ImpliedObject, ArrayView other)
  {
	Debug.Assert(ImpliedObject.data != null || ImpliedObject.size == 0);
	Debug.Assert(other.data != null || other.size == 0);
	if (ImpliedObject.size == other.size)
	{
	  for (Size i = 0;; ++i)
	  {
		if (i == ImpliedObject.size)
		{
		  return true;
		}

		if (!(*(ImpliedObject.data + i) == other.data[i]))
		{
		  break;
		}
	  }
	}

	return false;
  }

  // Compare elements two arrays, return false if there is no difference.
  // EMPTY and NIL arrays are considered equal.
  // The behavior is undefined unless both arrays were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator !=(ArrayView other) const
  public static bool operator != (ArrayView ImpliedObject, ArrayView other)
  {
	Debug.Assert(ImpliedObject.data != null || ImpliedObject.size == 0);
	Debug.Assert(other.data != null || other.size == 0);
	if (ImpliedObject.size == other.size)
	{
	  for (Size i = 0;; ++i)
	  {
		if (i == ImpliedObject.size)
		{
		  return false;
		}

		if (*(ImpliedObject.data + i) != other.data[i])
		{
		  break;
		}
	  }
	}

	return true;
  }

  // Return false if 'ArrayView' does not contain 'object' at the beginning.
  // The behavior is undefined unless 'ArrayView' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool beginsWith(const Object& object) const
  public bool beginsWith(Object @object)
  {
	Debug.Assert(data != null || size == 0);
	if (size == 0)
	{
	  return false;
	}

	return data[0] == @object;
  }

  // Return false if 'ArrayView' does not contain 'other' at the beginning.
  // The behavior is undefined unless both arrays were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool beginsWith(ArrayView other) const
  public bool beginsWith(ArrayView other)
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(other.data != null || other.size == 0);
	if (size >= other.size)
	{
	  for (Size i = 0;; ++i)
	  {
		if (i == other.size)
		{
		  return true;
		}

		if (!(data[i] == other.data[i]))
		{
		  break;
		}
	  }
	}

	return false;
  }

  // Return false if 'ArrayView' does not contain 'object'.
  // The behavior is undefined unless 'ArrayView' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool contains(const Object& object) const
  public bool contains(Object @object)
  {
	Debug.Assert(data != null || size == 0);
	for (Size i = 0; i < size; ++i)
	{
	  if (data[i] == @object)
	  {
		return true;
	  }
	}

	return false;
  }

  // Return false if 'ArrayView' does not contain 'other'.
  // The behavior is undefined unless both arrays were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool contains(ArrayView other) const
  public bool contains(ArrayView other)
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(other.data != null || other.size == 0);
	if (size >= other.size)
	{
	  Size i = size - other.size;
	  for (Size j = 0; !(i < j); ++j)
	  {
		for (Size k = 0;; ++k)
		{
		  if (k == other.size)
		  {
			return true;
		  }

		  if (!(data[j + k] == other.data[k]))
		  {
			break;
		  }
		}
	  }
	}

	return false;
  }

  // Return false if 'ArrayView' does not contain 'object' at the end.
  // The behavior is undefined unless 'ArrayView' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool endsWith(const Object& object) const
  public bool endsWith(Object @object)
  {
	Debug.Assert(data != null || size == 0);
	if (size == 0)
	{
	  return false;
	}

	return data[(size - 1)] == @object;
  }

  // Return false if 'ArrayView' does not contain 'other' at the end.
  // The behavior is undefined unless both arrays were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool endsWith(ArrayView other) const
  public bool endsWith(ArrayView other)
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(other.data != null || other.size == 0);
	if (size >= other.size)
	{
	  Size i = size - other.size;
	  for (Size j = 0;; ++j)
	  {
		if (j == other.size)
		{
		  return true;
		}

		if (!(data[i + j] == other.data[j]))
		{
		  break;
		}
	  }
	}

	return false;
  }

  // Looks for the first occurence of 'object' in 'ArrayView',
  // returns index or INVALID if there are no occurences.
  // The behavior is undefined unless 'ArrayView' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Size find(const Object& object) const
  public Size find(Object @object)
  {
	Debug.Assert(data != null || size == 0);
	for (Size i = 0; i < size; ++i)
	{
	  if (data[i] == @object)
	  {
		return i;
	  }
	}

	return INVALID;
  }

  // Looks for the first occurence of 'other' in 'ArrayView',
  // returns index or INVALID if there are no occurences.
  // The behavior is undefined unless both arrays were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Size find(ArrayView other) const
  public Size find(ArrayView other)
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(other.data != null || other.size == 0);
	if (size >= other.size)
	{
	  Size i = size - other.size;
	  for (Size j = 0; !(i < j); ++j)
	  {
		for (Size k = 0;; ++k)
		{
		  if (k == other.size)
		  {
			return j;
		  }

		  if (!(data[j + k] == other.data[k]))
		  {
			break;
		  }
		}
	  }
	}

	return INVALID;
  }

  // Looks for the last occurence of 'object' in 'ArrayView',
  // returns index or INVALID if there are no occurences.
  // The behavior is undefined unless 'ArrayView' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Size findLast(const Object& object) const
  public Size findLast(Object @object)
  {
	Debug.Assert(data != null || size == 0);
	for (Size i = 0; i < size; ++i)
	{
	  if (data[(size - 1 - i)] == @object)
	  {
		return size - 1 - i;
	  }
	}

	return INVALID;
  }

  // Looks for the first occurence of 'other' in 'ArrayView',
  // returns index or INVALID if there are no occurences.
  // The behavior is undefined unless both arrays were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Size findLast(ArrayView other) const
  public Size findLast(ArrayView other)
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(other.data != null || other.size == 0);
	if (size >= other.size)
	{
	  Size i = size - other.size;
	  for (Size j = 0; !(i < j); ++j)
	  {
		for (Size k = 0;; ++k)
		{
		  if (k == other.size)
		  {
			return i - j;
		  }

		  if (!(data[(i - j + k)] == other.data[k]))
		  {
			break;
		  }
		}
	  }
	}

	return INVALID;
  }

  // Returns subarray of 'headSize' first elements.
  // The behavior is undefined unless 'ArrayView' was initialized and 'headSize' <= 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ArrayView head(Size headSize) const
  public ArrayView head(Size headSize)
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(headSize <= size);
	return new GlobalMembers.ArrayView(data, headSize);
  }

  // Returns subarray of 'tailSize' last elements.
  // The behavior is undefined unless 'ArrayView' was initialized and 'tailSize' <= 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ArrayView tail(Size tailSize) const
  public ArrayView tail(Size tailSize)
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(tailSize <= size);
	return new GlobalMembers.ArrayView(data + (size - tailSize), tailSize);
  }

  // Returns 'ArrayView' without 'headSize' first elements.
  // The behavior is undefined unless 'ArrayView' was initialized and 'headSize' <= 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ArrayView unhead(Size headSize) const
  public ArrayView unhead(Size headSize)
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(headSize <= size);
	return new GlobalMembers.ArrayView(data + headSize, size - headSize);
  }

  // Returns 'ArrayView' without 'tailSize' last elements.
  // The behavior is undefined unless 'ArrayView' was initialized and 'tailSize' <= 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ArrayView untail(Size tailSize) const
  public ArrayView untail(Size tailSize)
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(tailSize <= size);
	return new GlobalMembers.ArrayView(data, size - tailSize);
  }

  // Returns subarray starting at 'startIndex' and contaning 'endIndex' - 'startIndex' elements.
  // The behavior is undefined unless 'ArrayView' was initialized and 'startIndex' <= 'endIndex' and 'endIndex' <= 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ArrayView range(Size startIndex, Size endIndex) const
  public ArrayView range(Size startIndex, Size endIndex)
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(startIndex <= endIndex != null && endIndex <= size);
	return new GlobalMembers.ArrayView(data + startIndex, endIndex - startIndex);
  }

  // Returns subarray starting at 'startIndex' and contaning 'sliceSize' elements.
  // The behavior is undefined unless 'ArrayView' was initialized and 'startIndex' <= 'size' and 'startIndex' + 'sliceSize' <= 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ArrayView slice(Size startIndex, Size sliceSize) const
  public ArrayView slice(Size startIndex, Size sliceSize)
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(startIndex <= size != null && startIndex + sliceSize <= size);
	return new GlobalMembers.ArrayView(data + startIndex, sliceSize);
  }

  protected readonly Object[] data;
  protected Size size = new Size();
}

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class Object, class Size>

}


namespace Common
{

// 'ArrayRef' is a pair of pointer to object of parametrized type and size.
// It is recommended to pass 'ArrayRef' to procedures by value.
// 'ArrayRef' supports 'EMPTY' and 'NIL' representations as follows:
//   'data' == 'nullptr' && 'size' == 0 - EMPTY NIL
//   'data' != 'nullptr' && 'size' == 0 - EMPTY NOTNIL
//   'data' == 'nullptr' && 'size' > 0 - Undefined
//   'data' != 'nullptr' && 'size' > 0 - NOTEMPTY NOTNIL
// For signed integer 'Size', 'ArrayRef' with 'size' < 0 is undefined.
//C++ TO C# CONVERTER TODO TASK: C++ template specifiers containing defaults cannot be converted to C#:
//ORIGINAL LINE: template<class ObjectType = byte, class SizeType = ulong>
public class ArrayRef <ObjectType = byte, SizeType = ulong> : System.IDisposable
{

  public readonly SizeType INVALID = new SizeType();
  public readonly ArrayRef EMPTY = new ArrayRef();
  public readonly ArrayRef NIL = new ArrayRef();

  // Default constructor.
  // Leaves object uninitialized. Any usage before initializing it is undefined.
//C++ TO C# CONVERTER TODO TASK: Statements that are interrupted by preprocessor statements are not converted by C++ to C# Converter:
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
  ArrayRef()
#if ! NDEBUG
//C++ TO C# CONVERTER TODO TASK: The following method format was not recognized, possibly due to an unrecognized macro:
	: data(null), size(INVALID) // In debug mode, fill in object with invalid state (undefined).
#endif
	{
	}

  // Direct constructor.
  // The behavior is undefined unless 'arrayData' != 'nullptr' || 'arraySize' == 0
  public ArrayRef(ObjectType arrayData, SizeType arraySize)
  {
	  this.data = arrayData;
	  this.size = arraySize;
	Debug.Assert(data != null || size == 0);
  }

  // Constructor from C array.
  // The behavior is undefined unless 'arrayData' != 'nullptr' || 'arraySize' == 0. Input state can be malformed using poiner conversions.
//C++ TO C# CONVERTER TODO TASK: C++ 'constraints' are not converted by C++ to C# Converter:
//ORIGINAL LINE: template<Size arraySize> ArrayRef(Object(&arrayData)[arraySize]) : data(arrayData), size(arraySize) {
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename arraySize>
  public ArrayRef<arraySize>(Object[] arrayData)
  {
	  this.data = arrayData;
	  this.size = arraySize;
	Debug.Assert(data != null || size == 0);
  }

  // Copy constructor.
  // Performs default action - bitwise copying of source object.
  // The behavior is undefined unless 'other' 'ArrayRef' is in defined state, that is 'data' != 'nullptr' || 'size' == 0
  public ArrayRef(ArrayRef other)
  {
	  this.data = other.data;
	  this.size = other.size;
	Debug.Assert(data != null || size == 0);
  }

  // Destructor.
  // No special action is performed.
  public void Dispose()
  {
  }

  // Copy assignment operator.
  // The behavior is undefined unless 'other' 'ArrayRef' is in defined state, that is 'data' != 'nullptr' || 'size' == 0
//C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
//ORIGINAL LINE: ArrayRef& operator =(const ArrayRef& other)
  public ArrayRef CopyFrom(ArrayRef other)
  {
	Debug.Assert(other.data != null || other.size == 0);
	data = other.data;
	size = other.size;
	return this;
  }

//C++ TO C# CONVERTER TODO TASK: C++ template specialization was removed by C++ to C# Converter:
//ORIGINAL LINE: operator ArrayView<ObjectType, SizeType>() const
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
  public static implicit operator ArrayView(ArrayRef ImpliedObject)
  {
	return new GlobalMembers.ArrayView<ObjectType, SizeType>(ImpliedObject.data, ImpliedObject.size);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ObjectType* getData() const
  public ObjectType getData()
  {
	Debug.Assert(data != null || size == 0);
	return data;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: SizeType getSize() const
  public SizeType getSize()
  {
	Debug.Assert(data != null || size == 0);
	return size;
  }

  // Return false if 'ArrayRef' is not EMPTY.
  // The behavior is undefined unless 'ArrayRef' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isEmpty() const
  public bool isEmpty()
  {
	Debug.Assert(data != null || size == 0);
	return size == 0;
  }

  // Return false if 'ArrayRef' is not NIL.
  // The behavior is undefined unless 'ArrayRef' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isNil() const
  public bool isNil()
  {
	Debug.Assert(data != null || size == 0);
	return data == null;
  }

  // Get 'ArrayRef' element by index.
  // The behavior is undefined unless 'ArrayRef' was initialized and 'index' < 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ObjectType& operator [](SizeType index) const
  public ObjectType this[SizeType index]
  {
	  get
	  {
		Debug.Assert(data != null || size == 0);
		Debug.Assert(index < size);
		return data[index];
	  }
	  set
	  {
		  data[index] = value;
	  }
  }

  // Get first element.
  // The behavior is undefined unless 'ArrayRef' was initialized and 'size' > 0
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ObjectType& first() const
  public ObjectType first()
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(size > 0);
	return data[0];
  }

  // Get last element.
  // The behavior is undefined unless 'ArrayRef' was initialized and 'size' > 0
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ObjectType& last() const
  public ObjectType last()
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(size > 0);
	return data[(size - 1)];
  }

  // Return a pointer to the first element.
  // The behavior is undefined unless 'ArrayRef' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ObjectType* begin() const
  public ObjectType begin()
  {
	Debug.Assert(data != null || size == 0);
	return data;
  }

  // Return a pointer after the last element.
  // The behavior is undefined unless 'ArrayRef' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ObjectType* end() const
  public ObjectType end()
  {
	Debug.Assert(data != null || size == 0);
	return data + size;
  }

  // Compare elements of two arrays, return false if there is a difference.
  // EMPTY and NIL arrays are considered equal.
  // The behavior is undefined unless both arrays were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator ==(ArrayView<ObjectType, SizeType> other) const
  public static bool operator == (ArrayRef ImpliedObject, ArrayView<ObjectType, SizeType> other)
  {
	Debug.Assert(ImpliedObject.data != null || ImpliedObject.size == 0);
	if (ImpliedObject.size == other.getSize())
	{
	  for (SizeType i = 0;; ++i)
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

  // Compare elements two arrays, return false if there is no difference.
  // EMPTY and NIL arrays are considered equal.
  // The behavior is undefined unless both arrays were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator !=(ArrayView<ObjectType, SizeType> other) const
  public static bool operator != (ArrayRef ImpliedObject, ArrayView<ObjectType, SizeType> other)
  {
	Debug.Assert(ImpliedObject.data != null || ImpliedObject.size == 0);
	if (ImpliedObject.size == other.getSize())
	{
	  for (SizeType i = 0;; ++i)
	  {
		if (i == ImpliedObject.size)
		{
		  return false;
		}

		if (*(ImpliedObject.data + i) != *(other.getData() + i))
		{
		  break;
		}
	  }
	}

	return true;
  }

  // Return false if 'ArrayRef' does not contain 'object' at the beginning.
  // The behavior is undefined unless 'ArrayRef' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool beginsWith(const ObjectType& object) const
  public bool beginsWith(ObjectType @object)
  {
	Debug.Assert(data != null || size == 0);
	if (size == 0)
	{
	  return false;
	}

	return data[0] == @object;
  }

  // Return false if 'ArrayRef' does not contain 'other' at the beginning.
  // The behavior is undefined unless both arrays were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool beginsWith(ArrayView<ObjectType, SizeType> other) const
  public bool beginsWith(ArrayView<ObjectType, SizeType> other)
  {
	Debug.Assert(data != null || size == 0);
	if (size >= other.getSize())
	{
	  for (SizeType i = 0;; ++i)
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

  // Return false if 'ArrayRef' does not contain 'object'.
  // The behavior is undefined unless 'ArrayRef' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool contains(const ObjectType& object) const
  public bool contains(ObjectType @object)
  {
	Debug.Assert(data != null || size == 0);
	for (SizeType i = 0; i < size; ++i)
	{
	  if (data[i] == @object)
	  {
		return true;
	  }
	}

	return false;
  }

  // Return false if 'ArrayRef' does not contain 'other'.
  // The behavior is undefined unless both arrays were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool contains(ArrayView<ObjectType, SizeType> other) const
  public bool contains(ArrayView<ObjectType, SizeType> other)
  {
	Debug.Assert(data != null || size == 0);
	if (size >= other.getSize())
	{
	  SizeType i = size - other.getSize();
	  for (SizeType j = 0; !(i < j); ++j)
	  {
		for (SizeType k = 0;; ++k)
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

  // Return false if 'ArrayRef' does not contain 'object' at the end.
  // The behavior is undefined unless 'ArrayRef' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool endsWith(const ObjectType& object) const
  public bool endsWith(ObjectType @object)
  {
	Debug.Assert(data != null || size == 0);
	if (size == 0)
	{
	  return false;
	}

	return data[(size - 1)] == @object;
  }

  // Return false if 'ArrayRef' does not contain 'other' at the end.
  // The behavior is undefined unless both arrays were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool endsWith(ArrayView<ObjectType, SizeType> other) const
  public bool endsWith(ArrayView<ObjectType, SizeType> other)
  {
	Debug.Assert(data != null || size == 0);
	if (size >= other.getSize())
	{
	  SizeType i = size - other.getSize();
	  for (SizeType j = 0;; ++j)
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

  // Looks for the first occurence of 'object' in 'ArrayRef',
  // returns index or INVALID if there are no occurences.
  // The behavior is undefined unless 'ArrayRef' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: SizeType find(const ObjectType& object) const
  public SizeType find(ObjectType @object)
  {
	Debug.Assert(data != null || size == 0);
	for (SizeType i = 0; i < size; ++i)
	{
	  if (data[i] == @object)
	  {
		return i;
	  }
	}

	return INVALID;
  }

  // Looks for the first occurence of 'other' in 'ArrayRef',
  // returns index or INVALID if there are no occurences.
  // The behavior is undefined unless both arrays were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: SizeType find(ArrayView<ObjectType, SizeType> other) const
  public SizeType find(ArrayView<ObjectType, SizeType> other)
  {
	Debug.Assert(data != null || size == 0);
	if (size >= other.getSize())
	{
	  SizeType i = size - other.getSize();
	  for (SizeType j = 0; !(i < j); ++j)
	  {
		for (SizeType k = 0;; ++k)
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

  // Looks for the last occurence of 'object' in 'ArrayRef',
  // returns index or INVALID if there are no occurences.
  // The behavior is undefined unless 'ArrayRef' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: SizeType findLast(const ObjectType& object) const
  public SizeType findLast(ObjectType @object)
  {
	Debug.Assert(data != null || size == 0);
	for (SizeType i = 0; i < size; ++i)
	{
	  if (data[(size - 1 - i)] == @object)
	  {
		return size - 1 - i;
	  }
	}

	return INVALID;
  }

  // Looks for the first occurence of 'other' in 'ArrayRef',
  // returns index or INVALID if there are no occurences.
  // The behavior is undefined unless both arrays were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: SizeType findLast(ArrayView<ObjectType, SizeType> other) const
  public SizeType findLast(ArrayView<ObjectType, SizeType> other)
  {
	Debug.Assert(data != null || size == 0);
	if (size >= other.getSize())
	{
	  SizeType i = size - other.getSize();
	  for (SizeType j = 0; !(i < j); ++j)
	  {
		for (SizeType k = 0;; ++k)
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

  // Returns subarray of 'headSize' first elements.
  // The behavior is undefined unless 'ArrayRef' was initialized and 'headSize' <= 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ArrayRef head(SizeType headSize) const
  public ArrayRef head(SizeType headSize)
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(headSize <= size);
	return new GlobalMembers.ArrayRef(data, headSize);
  }

  // Returns subarray of 'tailSize' last elements.
  // The behavior is undefined unless 'ArrayRef' was initialized and 'tailSize' <= 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ArrayRef tail(SizeType tailSize) const
  public ArrayRef tail(SizeType tailSize)
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(tailSize <= size);
	return new GlobalMembers.ArrayRef(data + (size - tailSize), tailSize);
  }

  // Returns 'ArrayRef' without 'headSize' first elements.
  // The behavior is undefined unless 'ArrayRef' was initialized and 'headSize' <= 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ArrayRef unhead(SizeType headSize) const
  public ArrayRef unhead(SizeType headSize)
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(headSize <= size);
	return new GlobalMembers.ArrayRef(data + headSize, size - headSize);
  }

  // Returns 'ArrayRef' without 'tailSize' last elements.
  // The behavior is undefined unless 'ArrayRef' was initialized and 'tailSize' <= 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ArrayRef untail(SizeType tailSize) const
  public ArrayRef untail(SizeType tailSize)
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(tailSize <= size);
	return new GlobalMembers.ArrayRef(data, size - tailSize);
  }

  // Returns subarray starting at 'startIndex' and contaning 'endIndex' - 'startIndex' elements.
  // The behavior is undefined unless 'ArrayRef' was initialized and 'startIndex' <= 'endIndex' and 'endIndex' <= 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ArrayRef range(SizeType startIndex, SizeType endIndex) const
  public ArrayRef range(SizeType startIndex, SizeType endIndex)
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(startIndex <= endIndex != null && endIndex <= size);
	return new GlobalMembers.ArrayRef(data + startIndex, endIndex - startIndex);
  }

  // Returns subarray starting at 'startIndex' and contaning 'sliceSize' elements.
  // The behavior is undefined unless 'ArrayRef' was initialized and 'startIndex' <= 'size' and 'startIndex' + 'sliceSize' <= 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ArrayRef slice(SizeType startIndex, SizeType sliceSize) const
  public ArrayRef slice(SizeType startIndex, SizeType sliceSize)
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(startIndex <= size != null && startIndex + sliceSize <= size);
	return new GlobalMembers.ArrayRef(data + startIndex, sliceSize);
  }

  // Copy 'object' to each element of 'ArrayRef'.
  // The behavior is undefined unless 'ArrayRef' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const ArrayRef& fill(const ObjectType& object) const
  public ArrayRef fill(ObjectType @object)
  {
	Debug.Assert(data != null || size == 0);
	for (SizeType i = 0; i < size; ++i)
	{
	  data[i] = @object;
	}

	return this;
  }

  // Reverse 'ArrayRef' elements.
  // The behavior is undefined unless 'ArrayRef' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const ArrayRef& reverse() const
  public ArrayRef reverse()
  {
	Debug.Assert(data != null || size == 0);
	for (SizeType i = 0; i < size / 2; ++i)
	{
	  ObjectType @object = data[i];
	  data[i] = (data + (size - 1 - i));
	  data[(size - 1 - i)] = @object;
	}

	return this;
  }

  protected ObjectType[] data;
  protected SizeType size = new SizeType();
}

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class Object, class Size>

}
