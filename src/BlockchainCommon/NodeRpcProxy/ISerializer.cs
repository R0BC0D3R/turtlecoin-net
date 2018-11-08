// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information


namespace CryptoNote
{
    public abstract class ISerializer : System.IDisposable
    {

        public enum SerializerType
        {
            INPUT,
            OUTPUT
        }

        public virtual void Dispose()
        {
        }

        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: virtual SerializerType type() const = 0;
        public abstract SerializerType type();

        public abstract bool BeginObject(Common.StringView name);
        public abstract void EndObject();
        public abstract bool BeginArray(ref ulong size, Common.StringView name);
        public abstract void EndArray();

        //public static abstract bool operator ()(ref byte value, Common.StringView name);
        //public static abstract bool operator ()(ref short value, Common.StringView name);
        //public static abstract bool operator ()(ref ushort value, Common.StringView name);
        //public static abstract bool operator ()(ref int value, Common.StringView name);
        //public static abstract bool operator ()(ref uint value, Common.StringView name);
        //public static abstract bool operator ()(ref long value, Common.StringView name);
        //public static abstract bool operator ()(ref ulong value, Common.StringView name);
        //public static abstract bool operator ()(ref double value, Common.StringView name);
        //public static abstract bool operator ()(ref bool value, Common.StringView name);
        //public static abstract bool operator ()(string value, Common.StringView name);

        // read/write binary block
        public abstract bool Binary(object value, ulong size, Common.StringView name);
        public abstract bool Binary(string value, Common.StringView name);

        public bool FunctorMethod<T>(T value, Common.StringView name)
        {
            //TODO: Revist this
            //return CryptoNote.GlobalMembers.serialize(value, new Common.StringView(name), this.functorMethod);
            return false;
        }

        public bool FunctorMethod<T>(T value, string name)
        {
            //TODO: Overwrite so I don't need to replace many calls
            //return CryptoNote.GlobalMembers.serialize(value, new Common.StringView(name), this.functorMethod);
            return false;
        }
    }
}

namespace CryptoNote
{
    public static class GlobalMembers
    {
        public static bool serialize<T>(T value, Common.StringView name, ISerializer serializer)
        {
            if (!serializer.BeginObject(new Common.StringView(name)))
            {
                return false;
            }

            //TODO: Do something with this
            //serialize(value, serializer.FunctorMethod);
            serializer.EndObject();
            return true;
        }

        /* WARNING: If you get a compiler error pointing to this line, when serializing
           a uint64_t, or other numeric type, this is due to your compiler treating some
           typedef's differently, so it does not correspond to one of the numeric
           types above. I tried using some template hackery to get around this, but
           it did not work. I resorted to just using a uint64_t instead. */
        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
        //ORIGINAL LINE: template<typename T>
        public static void serialize<T>(T value, ISerializer serializer)
        {
            //TODO: Do something with this
            //value.serialize(serializer.functorMethod);
        }
    }
}
