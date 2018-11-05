// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Diagnostics;

namespace Common
{

// 'StringView' is a pair of pointer to constant char and size.
// It is recommended to pass 'StringView' to procedures by value.
// 'StringView' supports 'EMPTY' and 'NIL' representations as follows:
//   'data' == 'nullptr' && 'size' == 0 - EMPTY NIL
//   'data' != 'nullptr' && 'size' == 0 - EMPTY NOTNIL
//   'data' == 'nullptr' && 'size' > 0 - Undefined
//   'data' != 'nullptr' && 'size' > 0 - NOTEMPTY NOTNIL
public class StringView : System.IDisposable
{

  public readonly uint64_t INVALID = uint64_t.MaxValue;
  public readonly StringView EMPTY = new StringView(reinterpret_cast<Object>(1), 0);
  public readonly StringView NIL = new StringView(null, 0);

  // Default constructor.
  // Leaves object uninitialized. Any usage before initializing it is undefined.
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  StringView();

  // Direct constructor.
  // The behavior is undefined unless 'stringData' != 'nullptr' || 'stringSize' == 0
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  StringView(string stringData, uint64_t stringSize);

  // Constructor from C array.
  // The behavior is undefined unless 'stringData' != 'nullptr' || 'stringSize' == 0. Input state can be malformed using poiner conversions.
//C++ TO C# CONVERTER TODO TASK: C++ 'constraints' are not converted by C++ to C# Converter:
//ORIGINAL LINE: template<Size stringSize> StringView(const Object(&stringData)[stringSize]) : data(stringData), size(stringSize - 1) {
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename stringSize>
  public StringView<stringSize>(string(stringData))
  {
	  this.data = stringData;
	  this.size = stringSize - 1;
	Debug.Assert(data != null || size == 0);
  }

  // Constructor from std::string
  public StringView(string string)
  {
	  this.data = string.data();
	  this.size = string.Length;
  }

  // Copy constructor.
  // Performs default action - bitwise copying of source object.
  // The behavior is undefined unless 'other' 'StringView' is in defined state, that is 'data' != 'nullptr' || 'size' == 0
  public StringView(StringView other)
  {
	  this.data = other.data;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.size = other.size;
	  this.size.CopyFrom(other.size);
	Debug.Assert(data != null || size == 0);
  }

  // Destructor.
  // No special action is performed.
  public void Dispose()
  {
  }

  // Copy assignment operator.
  // The behavior is undefined unless 'other' 'StringView' is in defined state, that is 'data' != 'nullptr' || 'size' == 0
//C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
//ORIGINAL LINE: StringView& operator =(const StringView& other)
  public StringView CopyFrom(StringView other)
  {
	Debug.Assert(other.data != null || other.size == 0);
	data = other.data;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: size = other.size;
	size.CopyFrom(other.size);
	return this;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: operator string() const
  public static implicit operator string(StringView ImpliedObject)
  {
	return (string)(ImpliedObject.data, ImpliedObject.size);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const char* getData() const
  public string getData()
  {
	Debug.Assert(data != null || size == 0);
	return data;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t getSize() const
  public uint64_t getSize()
  {
	Debug.Assert(data != null || size == 0);
	return size;
  }

  // Return false if 'StringView' is not EMPTY.
  // The behavior is undefined unless 'StringView' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isEmpty() const
  public bool isEmpty()
  {
	Debug.Assert(data != null || size == 0);
	return size == 0;
  }

  // Return false if 'StringView' is not NIL.
  // The behavior is undefined unless 'StringView' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isNil() const
  public bool isNil()
  {
	Debug.Assert(data != null || size == 0);
	return data == null;
  }

  // Get 'StringView' element by index.
  // The behavior is undefined unless 'StringView' was initialized and 'index' < 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const char& operator [](uint64_t index) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  char operator [](uint64_t index);

  // Get first element.
  // The behavior is undefined unless 'StringView' was initialized and 'size' > 0
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const char& first() const
  public char first()
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(size > 0);
	return *data;
  }

  // Get last element.
  // The behavior is undefined unless 'StringView' was initialized and 'size' > 0
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const char& last() const
  public char last()
  {
	Debug.Assert(data != null || size == 0);
	Debug.Assert(size > 0);
	return *(data + (size - 1));
  }

  // Return a pointer to the first element.
  // The behavior is undefined unless 'StringView' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const char* begin() const
  public string begin()
  {
	Debug.Assert(data != null || size == 0);
	return data;
  }

  // Return a pointer after the last element.
  // The behavior is undefined unless 'StringView' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const char* end() const
  public string end()
  {
	Debug.Assert(data != null || size == 0);
	return data + size;
  }

  // Compare elements of two strings, return false if there is a difference.
  // EMPTY and NIL strings are considered equal.
  // The behavior is undefined unless both strings were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator ==(StringView other) const
  public static bool operator == (StringView ImpliedObject, StringView other)
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

		if (!(*(ImpliedObject.data + i) == *(other.data + i)))
		{
		  break;
		}
	  }
	}

	return false;
  }

  // Compare elements two strings, return false if there is no difference.
  // EMPTY and NIL strings are considered equal.
  // The behavior is undefined unless both strings were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator !=(StringView other) const
  public static bool operator != (StringView ImpliedObject, StringView other)
  {
	return !(*ImpliedObject == other);
  }

  // Compare two strings character-wise.
  // The behavior is undefined unless both strings were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator <(StringView other) const
  public static bool operator < (StringView ImpliedObject, StringView other)
  {
	Debug.Assert(ImpliedObject.data != null || ImpliedObject.size == 0);
	Debug.Assert(other.data != null || other.size == 0);
	Size count = other.size < ImpliedObject.size ? other.size : ImpliedObject.size;
	for (Size i = 0; i < count; ++i)
	{
	  Object char1 = (ImpliedObject.data + i);
	  Object char2 = (other.data + i);
	  if (char1 < char2)
	  {
		return true;
	  }

	  if (char2 < char1)
	  {
		return false;
	  }
	}

	return ImpliedObject.size < other.size;
  }

  // Compare two strings character-wise.
  // The behavior is undefined unless both strings were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator <=(StringView other) const
  public static bool operator <= (StringView ImpliedObject, StringView other)
  {
	return !(other < *ImpliedObject);
  }

  // Compare two strings character-wise.
  // The behavior is undefined unless both strings were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator >(StringView other) const
  public static bool operator > (StringView ImpliedObject, StringView other)
  {
	return other < *ImpliedObject;
  }

  // Compare two strings character-wise.
  // The behavior is undefined unless both strings were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator >=(StringView other) const
  public static bool operator >= (StringView ImpliedObject, StringView other)
  {
	return !(*ImpliedObject < other);
  }

  // Return false if 'StringView' does not contain 'object' at the beginning.
  // The behavior is undefined unless 'StringView' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool beginsWith(const char& object) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool beginsWith(char @object);

  // Return false if 'StringView' does not contain 'other' at the beginning.
  // The behavior is undefined unless both strings were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool beginsWith(StringView other) const
  public bool beginsWith(StringView other)
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

		if (!(*(data + i) == *(other.data + i)))
		{
		  break;
		}
	  }
	}

	return false;
  }

  // Return false if 'StringView' does not contain 'object'.
  // The behavior is undefined unless 'StringView' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool contains(const char& object) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool contains(char @object);

  // Return false if 'StringView' does not contain 'other'.
  // The behavior is undefined unless both strings were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool contains(StringView other) const
  public bool contains(StringView other)
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

		  if (!(*(data + j + k) == *(other.data + k)))
		  {
			break;
		  }
		}
	  }
	}

	return false;
  }

  // Return false if 'StringView' does not contain 'object' at the end.
  // The behavior is undefined unless 'StringView' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool endsWith(const char& object) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool endsWith(char @object);

  // Return false if 'StringView' does not contain 'other' at the end.
  // The behavior is undefined unless both strings were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool endsWith(StringView other) const
  public bool endsWith(StringView other)
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

		if (!(*(data + i + j) == *(other.data + j)))
		{
		  break;
		}
	  }
	}

	return false;
  }

  // Looks for the first occurence of 'object' in 'StringView',
  // returns index or INVALID if there are no occurences.
  // The behavior is undefined unless 'StringView' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t find(const char& object) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  uint64_t find(char @object);

  // Looks for the first occurence of 'other' in 'StringView',
  // returns index or INVALID if there are no occurences.
  // The behavior is undefined unless both strings were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t find(StringView other) const
  public uint64_t find(StringView other)
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

		  if (!(*(data + j + k) == *(other.data + k)))
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
  // The behavior is undefined unless 'StringView' was initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t findLast(const char& object) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  uint64_t findLast(char @object);

  // Looks for the first occurence of 'other' in 'StringView',
  // returns index or INVALID if there are no occurences.
  // The behavior is undefined unless both strings were initialized.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t findLast(StringView other) const
  public uint64_t findLast(StringView other)
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

		  if (!(*(data + (i - j + k)) == *(other.data + k)))
		  {
			break;
		  }
		}
	  }
	}

	return INVALID;
  }

  // Returns substring of 'headSize' first elements.
  // The behavior is undefined unless 'StringView' was initialized and 'headSize' <= 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: StringView head(uint64_t headSize) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  StringView head(uint64_t headSize);

  // Returns substring of 'tailSize' last elements.
  // The behavior is undefined unless 'StringView' was initialized and 'tailSize' <= 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: StringView tail(uint64_t tailSize) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  StringView tail(uint64_t tailSize);

  // Returns 'StringView' without 'headSize' first elements.
  // The behavior is undefined unless 'StringView' was initialized and 'headSize' <= 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: StringView unhead(uint64_t headSize) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  StringView unhead(uint64_t headSize);

  // Returns 'StringView' without 'tailSize' last elements.
  // The behavior is undefined unless 'StringView' was initialized and 'tailSize' <= 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: StringView untail(uint64_t tailSize) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  StringView untail(uint64_t tailSize);

  // Returns substring starting at 'startIndex' and contaning 'endIndex' - 'startIndex' elements.
  // The behavior is undefined unless 'StringView' was initialized and 'startIndex' <= 'endIndex' and 'endIndex' <= 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: StringView range(uint64_t startIndex, uint64_t endIndex) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  StringView range(uint64_t startIndex, uint64_t endIndex);

  // Returns substring starting at 'startIndex' and contaning 'sliceSize' elements.
  // The behavior is undefined unless 'StringView' was initialized and 'startIndex' <= 'size' and 'startIndex' + 'sliceSize' <= 'size'.
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: StringView slice(uint64_t startIndex, uint64_t sliceSize) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  StringView slice(uint64_t startIndex, uint64_t sliceSize);

  protected readonly string data;
  protected uint64_t size = new uint64_t();
}

}


namespace Common
{


//C++ TO C# CONVERTER TODO TASK: Statements that are interrupted by preprocessor statements are not converted by C++ to C# Converter:
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
StringView.StringView()
#if ! NDEBUG
//C++ TO C# CONVERTER TODO TASK: The following method format was not recognized, possibly due to an unrecognized macro:
  : data(null), size(INVALID) // In debug mode, fill in object with invalid state (undefined).
#endif
  {
  }


//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool StringView::beginsWith(const Object& object) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool StringView::contains(const Object& object) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool StringView::endsWith(const Object& object) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t StringView::find(const Object& object) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t StringView::findLast(const Object& object) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: StringView StringView::head(Size headSize) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: StringView StringView::tail(Size tailSize) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: StringView StringView::unhead(Size headSize) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: StringView StringView::untail(Size tailSize) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: StringView StringView::range(Size startIndex, Size endIndex) const

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: StringView StringView::slice(Size startIndex, Size sliceSize) const

}
