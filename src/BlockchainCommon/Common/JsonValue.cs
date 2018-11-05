// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

namespace Common
{

public class JsonValue : System.IDisposable
{


  public enum Type
  {
	ARRAY,
	int,
	INTEGER,
	NIL,
	OBJECT,
	REAL,
	STRING
  }

  public JsonValue()
  {
	  this.type = new Common.JsonValue.Type.NIL;
  }
  public JsonValue(JsonValue other)
  {
	switch (other.type)
	{
	case Type.ARRAY:
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  new(valueArray)Array(*reinterpret_cast<const Array>(other.valueArray));
	  break;
	case Type.int:
	  valueBool = other.valueBool;
	  break;
	case Type.INTEGER:
	  valueInteger = other.valueInteger;
	  break;
	case Type.NIL:
	  break;
	case Type.OBJECT:
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  new(valueObject)Object(*reinterpret_cast<const Object>(other.valueObject));
	  break;
	case Type.REAL:
	  valueReal = other.valueReal;
	  break;
	case Type.STRING:
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  new(valueString)String(*reinterpret_cast<const String>(other.valueString));
	  break;
	}

	type = other.type;
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public JsonValue(JsonValue && other)
  {
	switch (other.type)
	{
	case Type.ARRAY:
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  new(valueArray)Array(std::move(*reinterpret_cast<Array>(other.valueArray)));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  reinterpret_cast<Array>(other.valueArray).~Array();
	  break;
	case Type.int:
	  valueBool = other.valueBool;
	  break;
	case Type.INTEGER:
	  valueInteger = other.valueInteger;
	  break;
	case Type.NIL:
	  break;
	case Type.OBJECT:
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  new(valueObject)Object(std::move(*reinterpret_cast<Object>(other.valueObject)));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  reinterpret_cast<Object>(other.valueObject).~Object();
	  break;
	case Type.REAL:
	  valueReal = other.valueReal;
	  break;
	case Type.STRING:
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  new(valueString)String(std::move(*reinterpret_cast<String>(other.valueString)));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  reinterpret_cast<String>(other.valueString).~String();
	  break;
	}

	type = other.type;
	other.type = Type.NIL;
  }
  public JsonValue(Type valueType)
  {
	switch (valueType)
	{
	case Type.ARRAY:
	  new(valueArray)Array;
	  break;
	case Type.NIL:
	  break;
	case Type.OBJECT:
	  new(valueObject)Object;
	  break;
	case Type.STRING:
	  new(valueString)String;
	  break;
	default:
	  throw new System.Exception("Invalid JsonValue type for constructor");
	}

	type = valueType;
  }
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue(ClassicVector<JsonValue> value);
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue(ClassicVector<JsonValue>&& value);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue(bool value);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue(int64_t value);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue(std::nullptr_t value);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue(ClassicMap<string, JsonValue> value);
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue(ClassicMap<string, JsonValue>&& value);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue(double value);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue(string value);
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue(string&& value);
//C++ TO C# CONVERTER TODO TASK: C++ 'constraints' are not converted by C++ to C# Converter:
//ORIGINAL LINE: template<uint64_t size> JsonValue(const char(&value)[size]) {
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename size>
  public JsonValue<size>(string(value))
  {
	new(valueString)String(value, size - 1);
	type = Type.STRING;
  }

  public void Dispose()
  {
	destructValue();
  }

//C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
//ORIGINAL LINE: JsonValue& operator =(const JsonValue& other)
  public JsonValue CopyFrom(JsonValue other)
  {
	if (type != other.type)
	{
	  destructValue();
	  switch (other.type)
	  {
	  case Type.ARRAY:
		type = Type.NIL;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		new(valueArray)Array(*reinterpret_cast<const Array>(other.valueArray));
		break;
	  case Type.int:
		valueBool = other.valueBool;
		break;
	  case Type.INTEGER:
		valueInteger = other.valueInteger;
		break;
	  case Type.NIL:
		break;
	  case Type.OBJECT:
		type = Type.NIL;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		new(valueObject)Object(*reinterpret_cast<const Object>(other.valueObject));
		break;
	  case Type.REAL:
		valueReal = other.valueReal;
		break;
	  case Type.STRING:
		type = Type.NIL;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		new(valueString)String(*reinterpret_cast<const String>(other.valueString));
		break;
	  }

	  type = other.type;
	}
	else
	{
	  switch (type)
	  {
	  case Type.ARRAY:
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		*reinterpret_cast<Array>(valueArray) = *reinterpret_cast<const Array>(other.valueArray);
		break;
	  case Type.int:
		valueBool = other.valueBool;
		break;
	  case Type.INTEGER:
		valueInteger = other.valueInteger;
		break;
	  case Type.NIL:
		break;
	  case Type.OBJECT:
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		*reinterpret_cast<Object>(valueObject) = *reinterpret_cast<const Object>(other.valueObject);
		break;
	  case Type.REAL:
		valueReal = other.valueReal;
		break;
	  case Type.STRING:
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		*reinterpret_cast<String>(valueString) = *reinterpret_cast<const String>(other.valueString);
		break;
	  }
	}

	return this.functorMethod;
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The = operator cannot be overloaded in C#:
  public static JsonValue operator = (JsonValue && other)
  {
	if (type != other.type)
	{
	  destructValue();
	  switch (other.type)
	  {
	  case Type.ARRAY:
		type = Type.NIL;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		new(valueArray)Array(std::move(*reinterpret_cast<const Array>(other.valueArray)));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		reinterpret_cast<Array>(other.valueArray).~Array();
		break;
	  case Type.int:
		valueBool = other.valueBool;
		break;
	  case Type.INTEGER:
		valueInteger = other.valueInteger;
		break;
	  case Type.NIL:
		break;
	  case Type.OBJECT:
		type = Type.NIL;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		new(valueObject)Object(std::move(*reinterpret_cast<const Object>(other.valueObject)));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		reinterpret_cast<Object>(other.valueObject).~Object();
		break;
	  case Type.REAL:
		valueReal = other.valueReal;
		break;
	  case Type.STRING:
		type = Type.NIL;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		new(valueString)String(std::move(*reinterpret_cast<const String>(other.valueString)));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		reinterpret_cast<String>(other.valueString).~String();
		break;
	  }

	  type = other.type;
	}
	else
	{
	  switch (type)
	  {
	  case Type.ARRAY:
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		*reinterpret_cast<Array>(valueArray) = std::move(*reinterpret_cast<const Array>(other.valueArray));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		reinterpret_cast<Array>(other.valueArray).~Array();
		break;
	  case Type.int:
		valueBool = other.valueBool;
		break;
	  case Type.INTEGER:
		valueInteger = other.valueInteger;
		break;
	  case Type.NIL:
		break;
	  case Type.OBJECT:
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		*reinterpret_cast<Object>(valueObject) = std::move(*reinterpret_cast<const Object>(other.valueObject));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		reinterpret_cast<Object>(other.valueObject).~Object();
		break;
	  case Type.REAL:
		valueReal = other.valueReal;
		break;
	  case Type.STRING:
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		*reinterpret_cast<String>(valueString) = std::move(*reinterpret_cast<const String>(other.valueString));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		reinterpret_cast<String>(other.valueString).~String();
		break;
	  }
	}

	other.type = Type.NIL;
	return this.functorMethod;
  }
//C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
//ORIGINAL LINE: JsonValue& operator =(const ClassicVector<JsonValue>& value);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue CopyFrom(ClassicVector<JsonValue> value);
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue operator =(ClassicVector<JsonValue>&& value);
  //JsonValue& operator=(Bool value);
//C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
//ORIGINAL LINE: JsonValue& operator =(int64_t value);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue CopyFrom(int64_t value);
//C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
//ORIGINAL LINE: JsonValue& operator =(std::nullptr_t value);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue CopyFrom(std::nullptr_t value);
//C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
//ORIGINAL LINE: JsonValue& operator =(const ClassicMap<string, JsonValue>& value);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue CopyFrom(ClassicMap<string, JsonValue> value);
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue operator =(ClassicMap<string, JsonValue>&& value);
//C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
//ORIGINAL LINE: JsonValue& operator =(double value);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue CopyFrom(double value);
//C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
//ORIGINAL LINE: JsonValue& operator =(const string& value);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue CopyFrom(string value);
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue operator =(string&& value);
//C++ TO C# CONVERTER TODO TASK: C++ 'constraints' are not converted by C++ to C# Converter:
//ORIGINAL LINE: template<uint64_t size> JsonValue& operator=(const char(&value)[size]) {
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename size>
//C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
//ORIGINAL LINE: JsonValue& operator =(const char(&value)[size])
  public JsonValue CopyFrom<size>(string(value))
  {
	if (type != Type.STRING)
	{
	  destructValue();
	  type = Type.NIL;
	  new(valueString)String(value, size - 1);
	  type = Type.STRING;
	}
	else
	{
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  reinterpret_cast<string>(valueString).assign(value, size - 1);
	}

	return this.functorMethod;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isArray() const
  public bool isArray()
  {
	return type == Type.ARRAY;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isBool() const
  public bool isBool()
  {
	return type == Type.int;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isInteger() const
  public bool isInteger()
  {
	return type == Type.INTEGER;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isNil() const
  public bool isNil()
  {
	return type == Type.NIL;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isObject() const
  public bool isObject()
  {
	return type == Type.OBJECT;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isReal() const
  public bool isReal()
  {
	return type == Type.REAL;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isString() const
  public bool isString()
  {
	return type == Type.STRING;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: JsonValue::Type getType() const
  public JsonValue.Type getType()
  {
	return type;
  }
  public List<JsonValue> getArray()
  {
	if (type != Type.ARRAY)
	{
	  throw new System.Exception("JsonValue type is not ARRAY");
	}

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	return *reinterpret_cast<Array>(valueArray);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const ClassicVector<JsonValue>& getArray() const
  public List<JsonValue> getArray()
  {
	if (type != Type.ARRAY)
	{
	  throw new System.Exception("JsonValue type is not ARRAY");
	}

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	return *reinterpret_cast<const Array>(valueArray);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool getBool() const
  public bool getBool()
  {
	if (type != Type.int)
	{
	  throw new System.Exception("JsonValue type is not BOOL");
	}

	return valueBool;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: int64_t getInteger() const
  public int64_t getInteger()
  {
	if (type != Type.INTEGER)
	{
	  throw new System.Exception("JsonValue type is not INTEGER");
	}

	return valueInteger;
  }
  public SortedDictionary<string, JsonValue> getObject()
  {
	if (type != Type.OBJECT)
	{
	  throw new System.Exception("JsonValue type is not OBJECT");
	}

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	return *reinterpret_cast<Object>(valueObject);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const ClassicMap<string, JsonValue>& getObject() const
  public SortedDictionary<string, JsonValue> getObject()
  {
	if (type != Type.OBJECT)
	{
	  throw new System.Exception("JsonValue type is not OBJECT");
	}

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	return *reinterpret_cast<const Object>(valueObject);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: double getReal() const
  public double getReal()
  {
	if (type != Type.REAL)
	{
	  throw new System.Exception("JsonValue type is not REAL");
	}

	return valueReal;
  }
  public string getString()
  {
	if (type != Type.STRING)
	{
	  throw new System.Exception("JsonValue type is not STRING");
	}

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	return *reinterpret_cast<String>(valueString);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const string& getString() const
  public string getString()
  {
	if (type != Type.STRING)
	{
	  throw new System.Exception("JsonValue type is not STRING");
	}

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	return *reinterpret_cast<const String>(valueString);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t size() const
  public uint64_t size()
  {
	switch (type)
	{
	case Type.ARRAY:
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  return reinterpret_cast<const Array>(valueArray).size();
	case Type.OBJECT:
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  return reinterpret_cast<const Object>(valueObject).size();
	default:
	  throw new System.Exception("JsonValue type is not ARRAY or OBJECT");
	}
  }

  public JsonValue this[uint64_t index]
  {
	  get
	  {
		if (type != Type.ARRAY)
		{
		  throw new System.Exception("JsonValue type is not ARRAY");
		}
    
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		return reinterpret_cast<Array>(valueArray).at(index);
	  }
	  set
	  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		  reinterpret_cast<Array>(valueArray).at(index) = value;
	  }
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const JsonValue& operator [](uint64_t index) const
  public JsonValue this[uint64_t index]
  {
	  get
	  {
		if (type != Type.ARRAY)
		{
		  throw new System.Exception("JsonValue type is not ARRAY");
		}
    
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		return reinterpret_cast<const Array>(valueArray).at(index);
	  }
	  set
	  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		  reinterpret_cast<const Array>(valueArray).at(index) = value;
	  }
  }
  public JsonValue pushBack(JsonValue value)
  {
	if (type != Type.ARRAY)
	{
	  throw new System.Exception("JsonValue type is not ARRAY");
	}

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	reinterpret_cast<Array>(valueArray).emplace_back(value.functorMethod);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	return reinterpret_cast<Array>(valueArray).back();
  }
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public JsonValue pushBack(JsonValue && value)
  {
	if (type != Type.ARRAY)
	{
	  throw new System.Exception("JsonValue type is not ARRAY");
	}

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	reinterpret_cast<Array>(valueArray).emplace_back(std::move(value));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	return reinterpret_cast<Array>(valueArray).back();
  }

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue operator ()(string key);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const JsonValue& operator ()(const string& key) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue operator ()(string key);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool contains(const string& key) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  bool contains(string key);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue insert(string key, JsonValue value);
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue insert(string key, JsonValue&& value);

  // sets or creates value, returns reference to self
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue set(string key, JsonValue value);
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  JsonValue set(string key, JsonValue&& value);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  uint64_t erase(string key);

  public static JsonValue fromString(string source)
  {
	JsonValue jsonValue = new JsonValue();
	std::istringstream stream = new std::istringstream(source);
	stream >> jsonValue.functorMethod;
	if (stream.fail())
	{
	  throw new System.Exception("Unable to parse JsonValue");
	}

	return jsonValue.functorMethod;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: string toString() const
  public string toString()
  {
	std::ostringstream stream = new std::ostringstream();
	stream << this.functorMethod;
	return stream.str();
  }

  public static std::ostream operator << (std::ostream @out, JsonValue jsonValue)
  {
	switch (jsonValue.type)
	{
	case JsonValue.ARRAY:
	{
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  List<JsonValue> array = reinterpret_cast<const List<JsonValue>>(jsonValue.valueArray);
	  @out << '[';
	  if (array.Count > 0)
	  {
		@out << array[0].functorMethod;
		for (uint64_t i = 1; i < array.Count; ++i)
		{
		  @out << ',' << array[i].functorMethod;
		}
	  }

	  @out << ']';
	  break;
	}
	case JsonValue.BOOL:
	  @out << (jsonValue.valueBool ? "true" : "false");
	  break;
	case JsonValue.INTEGER:
	  @out << jsonValue.valueInteger;
	  break;
	case JsonValue.NIL:
	  @out << "null";
	  break;
	case JsonValue.OBJECT:
	{
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  SortedDictionary<string, JsonValue> @object = reinterpret_cast<const SortedDictionary<string, JsonValue>>(jsonValue.valueObject);
	  @out << '{';
	  var iter = @object.GetEnumerator();
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
	  if (iter != @object.end())
	  {
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
		@out << '"' << iter.first << "\":" << iter.second;
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
		++iter;
		while (iter.MoveNext())
		{
		  @out << ",\"" << iter.Current.Key << "\":" << iter.Current.Value;
		}
	  }

	  @out << '}';
	  break;
	}
	case JsonValue.REAL:
	{
	  std::ostringstream stream = new std::ostringstream();
	  stream << std::@fixed << std::setprecision(11) << jsonValue.valueReal;
	  string value = stream.str();
	  while (value.Length > 1 && value[value.Length - 2] != '.' && value[value.Length - 1] == '0')
	  {
		value.resize(value.Length - 1);
	  }

	  @out << value;
	  break;
	}
	case JsonValue.STRING:
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  @out << '"' << *reinterpret_cast<const string>(jsonValue.valueString) << '"';
	  break;
	}

	return @out;
  }
  public static std::istream operator >> (std::istream in, JsonValue jsonValue)
  {
	char c = GlobalMembers.readNonWsChar(in);

	if (c == '[')
	{
	  jsonValue.readArray(in);
	}
	else if (c == 't')
	{
	  jsonValue.readTrue(in);
	}
	else if (c == 'f')
	{
	  jsonValue.readFalse(in);
	}
	else if ((c == '-') || (c >= '0' && c <= '9'))
	{
	  jsonValue.readNumber(in, c);
	}
	else if (c == 'n')
	{
	  jsonValue.readNull(in);
	}
	else if (c == '{')
	{
	  jsonValue.readObject(in);
	}
	else if (c == '"')
	{
	  jsonValue.readString(in);
	}
	else
	{
	  throw new System.Exception("Unable to parse");
	}

	return in;
  }

  private Type type;
//C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#:
//  union
//  {
//	uint8_t valueArray[sizeof(ClassicVector<JsonValue>)];
//	bool valueBool;
//	int64_t valueInteger;
//	uint8_t valueObject[sizeof(ClassicMap<string, JsonValue>)];
//	double valueReal;
//	uint8_t valueString[sizeof(string)];
//  };

  private void destructValue()
  {
	switch (type)
	{
	case Type.ARRAY:
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  reinterpret_cast<Array>(valueArray).~Array();
	  break;
	case Type.OBJECT:
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  reinterpret_cast<Object>(valueObject).~Object();
	  break;
	case Type.STRING:
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  reinterpret_cast<String>(valueString).~String();
	  break;
	default:
	  break;
	}
  }

  private void readArray(std::istream in)
  {
	List<JsonValue> value = new List<JsonValue>();
	char c = GlobalMembers.readNonWsChar(in);

	if (c != ']')
	{
	  in.putback(c);
	  for (;;)
	  {
		value.Resize(value.Count + 1);
		in >> value[value.Count - 1].functorMethod;
		c = GlobalMembers.readNonWsChar(in);

		if (c == ']')
		{
		  break;
		}

		if (c != ',')
		{
		  throw new System.Exception("Unable to parse");
		}
	  }
	}

	if (type != JsonValue.ARRAY)
	{
	  destructValue();
	  type = JsonValue.NIL;
	  new(valueArray)List<JsonValue>;
	  type = JsonValue.ARRAY;
	}

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	reinterpret_cast<List<JsonValue>>(valueArray).swap(value);
  }
  private void readTrue(std::istream in)
  {
	string data = new string(new char[3]);
	in.read(data, 3);
	if (data[0] != 'r' || data[1] != 'u' || data[2] != 'e')
	{
	  throw new System.Exception("Unable to parse");
	}

	if (type != JsonValue.BOOL)
	{
	  destructValue();
	  type = JsonValue.BOOL;
	}

	valueBool = true;
  }
  private void readFalse(std::istream in)
  {
	string data = new string(new char[4]);
	in.read(data, 4);
	if (data[0] != 'a' || data[1] != 'l' || data[2] != 's' || data[3] != 'e')
	{
	  throw new System.Exception("Unable to parse");
	}

	if (type != JsonValue.BOOL)
	{
	  destructValue();
	  type = JsonValue.BOOL;
	}

	valueBool = false;
  }
  private void readNull(std::istream in)
  {
	string data = new string(new char[3]);
	in.read(data, 3);
	if (data[0] != 'u' || data[1] != 'l' || data[2] != 'l')
	{
	  throw new System.Exception("Unable to parse");
	}

	if (type != JsonValue.NIL)
	{
	  destructValue();
	  type = JsonValue.NIL;
	}
  }
  private void readNumber(std::istream in, char c)
  {
	string text;
	text += c;
	uint64_t dots = 0;
	for (;;)
	{
	  Type.int i = in.peek();
	  if (i >= '0' && i <= '9')
	  {
		in.read(c, 1);
		text += c;
	  }
	  else if (i == '.')
	  {
		in.read(c, 1);
		text += '.';
		++dots;
	  }
	  else
	  {
		break;
	  }
	}

	if (dots > 0)
	{
	  if (dots > 1)
	  {
		throw new System.Exception("Unable to parse");
	  }

	  Type.int i = in.peek();
	  if (in.peek() == 'e')
	  {
		in.read(c, 1);
		text += c;
		i = in.peek();
		if (i == '+')
		{
		  in.read(c, 1);
		  text += c;
		  i = in.peek();
		}
		else if (i == '-')
		{
		  in.read(c, 1);
		  text += c;
		  i = in.peek();
		}

		if (i < '0' || i > '9')
		{
		  throw new System.Exception("Unable to parse");
		}

		do
		{
		  in.read(c, 1);
		  text += c;
		  i = in.peek();
		} while (i >= '0' && i <= '9');
	  }

	  Real value = new Real();
	  std::istringstream(text) >> value;
	  if (type != Type.REAL)
	  {
		destructValue();
		type = Type.REAL;
	  }

	  valueReal = value;
	}
	else
	{
	  if (text.Length > 1 && ((text[0] == '0') || (text[0] == '-' && text[1] == '0')))
	  {
		throw new System.Exception("Unable to parse");
	  }

	  Integer value;
	  std::istringstream(text) >> value;
	  if (type != Type.INTEGER)
	  {
		destructValue();
		type = Type.INTEGER;
	  }

	  valueInteger = value;
	}
  }
  private void readObject(std::istream in)
  {
	char c = GlobalMembers.readNonWsChar(in);
	SortedDictionary<string, JsonValue> value = new SortedDictionary<string, JsonValue>();

	if (c != '}')
	{
	  string name;

	  for (;;)
	  {
		if (c != '"')
		{
		  throw new System.Exception("Unable to parse");
		}

		name = GlobalMembers.readStringToken(in);
		c = GlobalMembers.readNonWsChar(in);

		if (c != ':')
		{
		  throw new System.Exception("Unable to parse");
		}

		in >> value[name].functorMethod;
		c = GlobalMembers.readNonWsChar(in);

		if (c == '}')
		{
		  break;
		}

		if (c != ',')
		{
		  throw new System.Exception("Unable to parse");
		}

		c = GlobalMembers.readNonWsChar(in);
	  }
	}

	if (type != JsonValue.OBJECT)
	{
	  destructValue();
	  type = JsonValue.NIL;
	  new(valueObject)SortedDictionary<string, JsonValue>;
	  type = JsonValue.OBJECT;
	}

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	reinterpret_cast<SortedDictionary<string, JsonValue>>(valueObject).swap(value);
  }
  private void readString(std::istream in)
  {
	String value = GlobalMembers.readStringToken(in);

	if (type != JsonValue.STRING)
	{
	  destructValue();
	  type = JsonValue.NIL;
	  new(valueString)String;
	  type = JsonValue.STRING;
	}

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	reinterpret_cast<String>(valueString).swap(value);
  }
}

}


namespace Common
{


//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:





//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:



//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool JsonValue::contains(const Key& key) const


//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:


//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:



//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace


}
