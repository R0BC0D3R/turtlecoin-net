// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Aeon Project
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2016-2018, The Karbowanec developers
// Copyright (c) 2018, The TurtleCoin Developers
// 
// Please see the included LICENSE.txt file for more information



//****************************************************************************************************
//TODO: Original file converted by C++ to C# coverter. Get what you need from it and discard the rest
//****************************************************************************************************


//#define CN_PAGE_SIZE

//using System;
//using System.Collections.Generic;


//// Standard Cryptonight Definitions

//// Standard CryptoNight Lite Definitions

//// CryptoNight Soft Shell Definitions
//// ultimately determines how big our sine wave is. A smaller value means a bigger wave
////C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
////ORIGINAL LINE: #define CN_SOFT_SHELL_ITER (CN_SOFT_SHELL_MEMORY / 2)
////C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
////ORIGINAL LINE: #define CN_SOFT_SHELL_PAD_MULTIPLIER (CN_SOFT_SHELL_WINDOW / CN_SOFT_SHELL_MULTIPLIER)
////C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
////ORIGINAL LINE: #define CN_SOFT_SHELL_ITER_MULTIPLIER (CN_SOFT_SHELL_PAD_MULTIPLIER / 2)

////C++ TO C# CONVERTER TODO TASK: C# does not allow setting or comparing #define constants:
//#if (((CN_SOFT_SHELL_WINDOW * CN_SOFT_SHELL_PAD_MULTIPLIER) + CN_SOFT_SHELL_MEMORY) > CN_PAGE_SIZE)
//#error The CryptoNight Soft Shell Parameters you supplied will exceed normal paging operations.
//#endif


//namespace std
//{
//    //C++ TO C# CONVERTER TODO TASK: C++ template specialization was removed by C++ to C# Converter:
//    //ORIGINAL LINE: struct hash<Crypto::Hash>
//    public partial class hash
//    {
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: uint operator ()(const Crypto::Hash &_v) const
//        public static uint functorMethod(Crypto.Hash _v)
//        {
//            //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//            return reinterpret_cast <const uint &> (_v);
//        }
//    }
//}


//namespace Crypto
//{

//    // Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//    //
//    // This file is part of Bytecoin.
//    //
//    // Bytecoin is free software: you can redistribute it and/or modify
//    // it under the terms of the GNU Lesser General Public License as published by
//    // the Free Software Foundation, either version 3 of the License, or
//    // (at your option) any later version.
//    //
//    // Bytecoin is distributed in the hope that it will be useful,
//    // but WITHOUT ANY WARRANTY; without even the implied warranty of
//    // MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    // GNU Lesser General Public License for more details.
//    //
//    // You should have received a copy of the GNU Lesser General Public License
//    // along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.


//#if !__cplusplus
//#endif

//    public class EllipticCurvePoint
//    {
//        public byte[] data = new byte[32];
//    }

//    public class EllipticCurveScalar
//    {
//        public byte[] data = new byte[32];
//    }

//    public class crypto_ops : System.IDisposable
//    {
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	crypto_ops();
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	crypto_ops(crypto_ops UnnamedParameter);
//        //C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
//        //ORIGINAL LINE: void operator =(const crypto_ops &);
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	void CopyFrom(crypto_ops UnnamedParameter);
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	public void Dispose();

//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static void generate_keys(PublicKey UnnamedParameter, SecretKey UnnamedParameter2);

//        /* Generate a new key pair
//         */
//        private void generate_keys(PublicKey pub, SecretKey sec)
//        {
//            this.generate_keys(pub, sec);
//        }
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static void generate_deterministic_keys(PublicKey pub, SecretKey sec, SecretKey second);
//        private void generate_deterministic_keys(PublicKey pub, SecretKey sec, SecretKey second)
//        {
//            this.generate_deterministic_keys(pub, sec, second);
//        }
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static SecretKey generate_m_keys(PublicKey pub, SecretKey sec, SecretKey recovery_key = SecretKey(), bool recover = false);
//        private SecretKey generate_m_keys(PublicKey pub, SecretKey sec, SecretKey recovery_key = SecretKey(), bool recover = false)
//        {
//            return this.generate_m_keys(pub, sec, recovery_key, recover);
//        }
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static bool check_key(PublicKey UnnamedParameter);

//        /* Check a public key. Returns true if it is valid, false otherwise.
//         */
//        private bool check_key(PublicKey key)
//        {
//            return this.check_key(key);
//        }
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static bool secret_key_to_public_key(SecretKey UnnamedParameter, PublicKey UnnamedParameter2);

//        /* Checks a private key and computes the corresponding public key.
//         */
//        private bool secret_key_to_public_key(SecretKey sec, PublicKey pub)
//        {
//            return this.secret_key_to_public_key(sec, pub);
//        }
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static bool generate_key_derivation(PublicKey UnnamedParameter, SecretKey UnnamedParameter2, KeyDerivation UnnamedParameter3);

//        /* To generate an ephemeral key used to send money to:
//         * * The sender generates a new key pair, which becomes the transaction key. The public transaction key is included in "extra" field.
//         * * Both the sender and the receiver generate key derivation from the transaction key and the receivers' "view" key.
//         * * The sender uses key derivation, the output index, and the receivers' "spend" key to derive an ephemeral public key.
//         * * The receiver can either derive the public key (to check that the transaction is addressed to him) or the private key (to spend the money).
//         */
//        private bool generate_key_derivation(PublicKey key1, SecretKey key2, KeyDerivation derivation)
//        {
//            return this.generate_key_derivation(key1, key2, derivation);
//        }
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static bool derive_public_key(KeyDerivation UnnamedParameter, uint UnnamedParameter2, PublicKey UnnamedParameter3, PublicKey UnnamedParameter4);
//        private bool derive_public_key(KeyDerivation derivation, uint output_index, PublicKey @base, PublicKey derived_key)
//        {
//            return this.derive_public_key(derivation, output_index, @base, derived_key);
//        }
//        private bool derive_public_key(KeyDerivation derivation, uint output_index, PublicKey @base, byte prefix, uint prefixLength, PublicKey derived_key)
//        {
//            return this.derive_public_key(derivation, output_index, @base, prefix, prefixLength, derived_key);
//        }
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static bool derive_public_key(KeyDerivation UnnamedParameter, uint UnnamedParameter2, PublicKey UnnamedParameter3, byte UnnamedParameter4, uint UnnamedParameter5, PublicKey UnnamedParameter6);
//        //hack for pg
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static bool underive_public_key_and_get_scalar(KeyDerivation UnnamedParameter, uint UnnamedParameter2, PublicKey UnnamedParameter3, PublicKey UnnamedParameter4, EllipticCurveScalar UnnamedParameter5);
//        private bool underive_public_key_and_get_scalar(KeyDerivation derivation, uint output_index, PublicKey derived_key, PublicKey @base, EllipticCurveScalar hashed_derivation)
//        {
//            return this.underive_public_key_and_get_scalar(derivation, output_index, derived_key, @base, hashed_derivation);
//        }
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static void generate_incomplete_key_image(PublicKey UnnamedParameter, EllipticCurvePoint UnnamedParameter2);
//        //C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' function:
//        //ORIGINAL LINE: friend void generate_incomplete_key_image(const PublicKey &, EllipticCurvePoint &);
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	void generate_incomplete_key_image(PublicKey UnnamedParameter, EllipticCurvePoint UnnamedParameter2);
//        //
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static void derive_secret_key(KeyDerivation UnnamedParameter, uint UnnamedParameter2, SecretKey UnnamedParameter3, SecretKey UnnamedParameter4);
//        private void derive_secret_key(KeyDerivation derivation, uint output_index, SecretKey @base, SecretKey derived_key)
//        {
//            this.derive_secret_key(derivation, output_index, @base, derived_key);
//        }
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static void derive_secret_key(KeyDerivation UnnamedParameter, uint UnnamedParameter2, SecretKey UnnamedParameter3, byte UnnamedParameter4, uint UnnamedParameter5, SecretKey UnnamedParameter6);
//        private void derive_secret_key(KeyDerivation derivation, uint output_index, SecretKey @base, byte prefix, uint prefixLength, SecretKey derived_key)
//        {
//            this.derive_secret_key(derivation, output_index, @base, prefix, prefixLength, derived_key);
//        }
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static bool underive_public_key(KeyDerivation UnnamedParameter, uint UnnamedParameter2, PublicKey UnnamedParameter3, PublicKey UnnamedParameter4);
//        private bool underive_public_key(KeyDerivation derivation, uint output_index, PublicKey derived_key, PublicKey @base)
//        {
//            return this.underive_public_key(derivation, output_index, derived_key, @base);
//        }
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static bool underive_public_key(KeyDerivation UnnamedParameter, uint UnnamedParameter2, PublicKey UnnamedParameter3, byte UnnamedParameter4, uint UnnamedParameter5, PublicKey UnnamedParameter6);

//        /* Inverse function of derive_public_key. It can be used by the receiver to find which "spend" key was used to generate a transaction. This may be useful if the receiver used multiple addresses which only differ in "spend" key.
//         */
//        private bool underive_public_key(KeyDerivation derivation, uint output_index, PublicKey derived_key, byte prefix, uint prefixLength, PublicKey @base)
//        {
//            return this.underive_public_key(derivation, output_index, derived_key, prefix, prefixLength, @base);
//        }
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static void generate_signature(Hash UnnamedParameter, PublicKey UnnamedParameter2, SecretKey UnnamedParameter3, Signature UnnamedParameter4);

//        /* Generation and checking of a standard signature.
//         */
//        private void generate_signature(Hash prefix_hash, PublicKey pub, SecretKey sec, Signature sig)
//        {
//            this.generate_signature(prefix_hash, pub, sec, sig);
//        }
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static bool check_signature(Hash UnnamedParameter, PublicKey UnnamedParameter2, Signature UnnamedParameter3);
//        private bool check_signature(Hash prefix_hash, PublicKey pub, Signature sig)
//        {
//            return this.check_signature(prefix_hash, pub, sig);
//        }
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static void generate_key_image(PublicKey UnnamedParameter, SecretKey UnnamedParameter2, KeyImage UnnamedParameter3);

//        /* To send money to a key:
//         * * The sender generates an ephemeral key and includes it in transaction output.
//         * * To spend the money, the receiver generates a key image from it.
//         * * Then he selects a bunch of outputs, including the one he spends, and uses them to generate a ring signature.
//         * To check the signature, it is necessary to collect all the keys that were used to generate it. To detect double spends, it is necessary to check that each key image is used at most once.
//         */
//        private void generate_key_image(PublicKey pub, SecretKey sec, KeyImage image)
//        {
//            this.generate_key_image(pub, sec, image);
//        }
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static KeyImage scalarmultKey(KeyImage P, KeyImage a);
//        private KeyImage scalarmultKey(KeyImage P, KeyImage a)
//        {
//            return this.scalarmultKey(P, a);
//        }
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static void hash_data_to_ec(byte UnnamedParameter, uint UnnamedParameter2, PublicKey UnnamedParameter3);
//        private void hash_data_to_ec(byte data, uint len, PublicKey key)
//        {
//            this.hash_data_to_ec(data, len, key);
//        }
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static void generate_ring_signature(Hash UnnamedParameter, KeyImage UnnamedParameter2, PublicKey[] UnnamedParameter3, uint UnnamedParameter4, SecretKey UnnamedParameter5, uint UnnamedParameter6, Signature UnnamedParameter7);
//        private void generate_ring_signature(Hash prefix_hash, KeyImage image, PublicKey[] pubs, uint pubs_count, SecretKey sec, uint sec_index, Signature sig)
//        {
//            this.generate_ring_signature(prefix_hash, image, pubs, pubs_count, sec, sec_index, sig);
//        }
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //	static bool check_ring_signature(Hash UnnamedParameter, KeyImage UnnamedParameter2, PublicKey[] UnnamedParameter3, uint UnnamedParameter4, Signature UnnamedParameter5, bool UnnamedParameter6);
//        private bool check_ring_signature(Hash prefix_hash, KeyImage image, PublicKey[] pubs, uint pubs_count, Signature sig, bool checkKeyImage)
//        {
//            return this.check_ring_signature(prefix_hash, image, pubs, pubs_count, sig, checkKeyImage);
//        }
//    }

//    /* Generate a value filled with random bytes.
//     */
//    //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//    //ORIGINAL LINE: template<typename T>
//    public class random_engine<T>
//    {

//        public static T min()
//        {
//            return (numeric_limits<T>.min)();
//        }

//        public static T max()
//        {
//            return (numeric_limits<T>.max)();
//        }

//        public static std::enable_if<std::is_unsigned<T>.value, T>.type functorMethod()
//        {
//            return Crypto.GlobalMembers.rand<T>();
//        }
//    }

//}

//namespace std
//{
//    //C++ TO C# CONVERTER TODO TASK: C++ template specialization was removed by C++ to C# Converter:
//    //ORIGINAL LINE: struct hash<Crypto::PublicKey>
//    public partial class hash
//    {
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: uint operator ()(const Crypto::PublicKey &_v) const
//        public static uint functorMethod(Crypto.PublicKey _v)
//        {
//            //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//            return reinterpret_cast <const uint &> (_v);
//        }
//    }
//}
//namespace std
//{
//    //C++ TO C# CONVERTER TODO TASK: C++ template specialization was removed by C++ to C# Converter:
//    //ORIGINAL LINE: struct hash<Crypto::KeyImage>
//    public partial class hash
//    {
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: uint operator ()(const Crypto::KeyImage &_v) const
//        public static uint functorMethod(Crypto.KeyImage _v)
//        {
//            //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//            return reinterpret_cast <const uint &> (_v);
//        }
//    }
//}

//// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
////
//// This file is part of Bytecoin.
////
//// Bytecoin is free software: you can redistribute it and/or modify
//// it under the terms of the GNU Lesser General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
////
//// Bytecoin is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU Lesser General Public License for more details.
////
//// You should have received a copy of the GNU Lesser General Public License
//// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.



//namespace CryptoNote
//{

//    public class ParentBlockSerializer
//    {
//        public ParentBlockSerializer(ParentBlock parentBlock, ref ulong timestamp, ref uint nonce, bool hashingSerialization, bool headerOnly)
//        {
//            this.m_parentBlock = new CryptoNote.ParentBlock(parentBlock);
//            this.m_timestamp = timestamp;
//            this.m_nonce = nonce;
//            this.m_hashingSerialization = hashingSerialization;
//            this.m_headerOnly = headerOnly;
//        }

//        public ParentBlock m_parentBlock;
//        //C++ TO C# CONVERTER TODO TASK: C# does not have an equivalent to references to value types:
//        //ORIGINAL LINE: ulong& m_timestamp;
//        public ulong m_timestamp;
//        //C++ TO C# CONVERTER TODO TASK: C# does not have an equivalent to references to value types:
//        //ORIGINAL LINE: uint& m_nonce;
//        public uint m_nonce;
//        public bool m_hashingSerialization;
//        public bool m_headerOnly;
//    }

//}

//// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
////
//// This file is part of Bytecoin.
////
//// Bytecoin is free software: you can redistribute it and/or modify
//// it under the terms of the GNU Lesser General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
////
//// Bytecoin is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU Lesser General Public License for more details.
////
//// You should have received a copy of the GNU Lesser General Public License
//// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.



//// ISerializer-based serialization
//// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
////
//// This file is part of Bytecoin.
////
//// Bytecoin is free software: you can redistribute it and/or modify
//// it under the terms of the GNU Lesser General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
////
//// Bytecoin is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU Lesser General Public License for more details.
////
//// You should have received a copy of the GNU Lesser General Public License
//// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.




//namespace CryptoNote
//{

//    public abstract class ISerializer : System.IDisposable
//    {

//        public enum SerializerType
//        {
//            INPUT,
//            OUTPUT
//        }

//        public virtual void Dispose()
//        {
//        }

//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual SerializerType type() const = 0;
//        public abstract SerializerType type();

//        public abstract bool beginObject(Common.StringView name);
//        public abstract void endObject();
//        public abstract bool beginArray(ref ulong size, Common.StringView name);
//        public abstract void endArray();

//        public static abstract bool operator ()(ref byte value, Common.StringView name);
//  public static abstract bool operator ()(ref short value, Common.StringView name);
//  public static abstract bool operator ()(ref ushort value, Common.StringView name);
//  public static abstract bool operator ()(ref int value, Common.StringView name);
//  public static abstract bool operator ()(ref uint value, Common.StringView name);
//  public static abstract bool operator ()(ref long value, Common.StringView name);
//  public static abstract bool operator ()(ref ulong value, Common.StringView name);
//  public static abstract bool operator ()(ref double value, Common.StringView name);
//  public static abstract bool operator ()(ref bool value, Common.StringView name);
//  public static abstract bool operator ()(string value, Common.StringView name);

//  // read/write binary block
//  public abstract bool binary(object value, ulong size, Common.StringView name);
//        public abstract bool binary(string value, Common.StringView name);

//        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//        //ORIGINAL LINE: template<typename T>
//        public static bool functorMethod<T>(T value, Common.StringView name)
//        {
//            return CryptoNote.GlobalMembers.serialize(value, new Common.StringView(name), this.functorMethod);
//        }
//    }

//    //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//    //ORIGINAL LINE: template<typename T>

//    //C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//    //ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);

//}

//// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
////
//// This file is part of Bytecoin.
////
//// Bytecoin is free software: you can redistribute it and/or modify
//// it under the terms of the GNU Lesser General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
////
//// Bytecoin is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU Lesser General Public License for more details.
////
//// You should have received a copy of the GNU Lesser General Public License
//// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.





//// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
////
//// This file is part of Bytecoin.
////
//// Bytecoin is free software: you can redistribute it and/or modify
//// it under the terms of the GNU Lesser General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
////
//// Bytecoin is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU Lesser General Public License for more details.
////
//// You should have received a copy of the GNU Lesser General Public License
//// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.


//// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//// Copyright (c) 2014-2018, The Monero Project
//// Copyright (c) 2018, The TurtleCoin Developers
//// 
//// Please see the included LICENSE file for more information.#pragma once



//#if __cplusplus


//namespace Crypto
//{
//#endif
//#if __cplusplus

////C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
////#pragma pack(push, 1)
//  public class chacha8_key
//  {
//	public byte[] data = new byte[DefineConstants.CHACHA8_KEY_SIZE];
//  }

//  // MS VC 2012 doesn't interpret `class chacha8_iv` as POD in spite of [9.0.10], so it is a struct
//  public class chacha8_iv
//  {
//	public byte[] data = new byte[DefineConstants.CHACHA8_IV_SIZE];
//  }
//}

//#endif



//namespace CryptoNote
//{

//    //C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//    //struct AccountKeys;
//    //C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//    //struct TransactionExtraMergeMiningTag;

//    public enum SerializationTag : byte
//    {
//        Base = 0xff,
//        Key = 0x2,
//        Transaction = 0xcc,
//        Block = 0xbb
//    }

//}


//namespace CryptoNote
//{


//    /************************************************************************/
//    /*                                                                      */
//    /************************************************************************/

//    //just to keep backward compatibility with BlockCompleteEntry serialization
//    public class RawBlockLegacy
//    {
//        public BinaryArray block = new BinaryArray();
//        public List<BinaryArray> transactions = new List<BinaryArray>();
//    }

//    public class NOTIFY_NEW_BLOCK_request
//    {
//        public RawBlockLegacy b = new RawBlockLegacy();
//        public uint current_blockchain_height;
//        public uint hop;
//    }

//    public class NOTIFY_NEW_BLOCK
//    {
//        public const int ID = DefineConstants.BC_COMMANDS_POOL_BASE + 1;
//    }

//    /************************************************************************/
//    /*                                                                      */
//    /************************************************************************/
//    public class NOTIFY_NEW_TRANSACTIONS_request
//    {
//        public List<BinaryArray> txs = new List<BinaryArray>();
//    }

//    public class NOTIFY_NEW_TRANSACTIONS
//    {
//        public const int ID = DefineConstants.BC_COMMANDS_POOL_BASE + 2;
//    }

//    /************************************************************************/
//    /*                                                                      */
//    /************************************************************************/
//    public class NOTIFY_REQUEST_GET_OBJECTS_request
//    {
//        public List<Crypto.Hash> txs = new List<Crypto.Hash>();
//        public List<Crypto.Hash> blocks = new List<Crypto.Hash>();

//        public void serialize(ISerializer s)
//        {
//            CryptoNote.GlobalMembers.serializeAsBinary(txs, "txs", s.functorMethod);
//            CryptoNote.GlobalMembers.serializeAsBinary(blocks, "blocks", s.functorMethod);
//        }
//    }

//    public class NOTIFY_REQUEST_GET_OBJECTS
//    {
//        public const int ID = DefineConstants.BC_COMMANDS_POOL_BASE + 3;
//    }

//    public class NOTIFY_RESPONSE_GET_OBJECTS_request
//    {
//        public List<string> txs = new List<string>();
//        public List<RawBlockLegacy> blocks = new List<RawBlockLegacy>();
//        public List<Crypto.Hash> missed_ids = new List<Crypto.Hash>();
//        public uint current_blockchain_height;
//    }

//    public class NOTIFY_RESPONSE_GET_OBJECTS
//    {
//        public const int ID = DefineConstants.BC_COMMANDS_POOL_BASE + 4;
//    }

//    public class NOTIFY_REQUEST_CHAIN
//    {
//        public const int ID = DefineConstants.BC_COMMANDS_POOL_BASE + 6;

//        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
//        public class request
//        {
//            public List<Crypto.Hash> block_ids = new List<Crypto.Hash>(); //IDs of the first 10 blocks are sequential, next goes with pow(2,n) offset, like 2, 4, 8, 16, 32, 64 and so on, and the last one is always genesis block

//            public void serialize(ISerializer s)
//            {
//                CryptoNote.GlobalMembers.serializeAsBinary(block_ids, "block_ids", s.functorMethod);
//            }
//        }
//    }

//    public class NOTIFY_RESPONSE_CHAIN_ENTRY_request
//    {
//        public uint start_height;
//        public uint total_height;
//        public List<Crypto.Hash> m_block_ids = new List<Crypto.Hash>();

//        public void serialize(ISerializer s)
//        {
//            s.FunctorMethod(start_height, "start_height");
//            s.FunctorMethod(total_height, "total_height");
//            CryptoNote.GlobalMembers.serializeAsBinary(m_block_ids, "m_block_ids", s.functorMethod);
//        }
//    }

//    public class NOTIFY_RESPONSE_CHAIN_ENTRY
//    {
//        public const int ID = DefineConstants.BC_COMMANDS_POOL_BASE + 7;
//    }

//    /************************************************************************/
//    /*                                                                      */
//    /************************************************************************/
//    public class NOTIFY_REQUEST_TX_POOL_request
//    {
//        public List<Crypto.Hash> txs = new List<Crypto.Hash>();

//        public void serialize(ISerializer s)
//        {
//            CryptoNote.GlobalMembers.serializeAsBinary(txs, "txs", s.functorMethod);
//        }
//    }

//    public class NOTIFY_REQUEST_TX_POOL
//    {
//        public const int ID = DefineConstants.BC_COMMANDS_POOL_BASE + 8;
//    }
//}


//// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
////
//// This file is part of Bytecoin.
////
//// Bytecoin is free software: you can redistribute it and/or modify
//// it under the terms of the GNU Lesser General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
////
//// Bytecoin is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU Lesser General Public License for more details.
////
//// You should have received a copy of the GNU Lesser General Public License
//// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.



//// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
////
//// This file is part of Bytecoin.
////
//// Bytecoin is free software: you can redistribute it and/or modify
//// it under the terms of the GNU Lesser General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
////
//// Bytecoin is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU Lesser General Public License for more details.
////
//// You should have received a copy of the GNU Lesser General Public License
//// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.



//namespace Crypto
//{

//    public class Hash
//    {
//        public byte[] data = new byte[32];
//    }

//    public class PublicKey
//    {
//        public byte[] data = new byte[32];
//    }

//    public class SecretKey
//    {
//        public byte[] data = new byte[32];
//    }

//    public class KeyDerivation
//    {
//        public byte[] data = new byte[32];
//    }

//    public class KeyImage
//    {
//        public byte[] data = new byte[32];
//    }

//    public class Signature
//    {
//        public byte[] data = new byte[64];
//    }

//}

//// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
////
//// This file is part of Bytecoin.
////
//// Bytecoin is free software: you can redistribute it and/or modify
//// it under the terms of the GNU Lesser General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
////
//// Bytecoin is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU Lesser General Public License for more details.
////
//// You should have received a copy of the GNU Lesser General Public License
//// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.



//namespace CryptoNote
//{

//    public class BaseInput
//    {
//        public uint blockIndex;
//    }

//    public class KeyInput
//    {
//        public ulong amount;
//        public List<uint> outputIndexes = new List<uint>();
//        public Crypto.KeyImage keyImage = new Crypto.KeyImage();
//    }

//    public class KeyOutput
//    {
//        public Crypto.PublicKey key = new Crypto.PublicKey();
//    }



//    public class TransactionOutput
//    {
//        public ulong amount;
//        public TransactionOutputTarget target = new TransactionOutputTarget();
//    }

//    public class TransactionPrefix
//    {
//        public byte version;
//        public ulong unlockTime;
//        public List<TransactionInput> inputs = new List<TransactionInput>();
//        public List<TransactionOutput> outputs = new List<TransactionOutput>();
//        public List<byte> extra = new List<byte>();
//    }

//    public class Transaction : TransactionPrefix
//    {
//        public List<List<Crypto.Signature>> signatures = new List<List<Crypto.Signature>>();
//    }

//    public class BaseTransaction : TransactionPrefix
//    {
//    }

//    public class ParentBlock
//    {
//        public byte majorVersion;
//        public byte minorVersion;
//        public Crypto.Hash previousBlockHash = new Crypto.Hash();
//        public ushort transactionCount;
//        public List<Crypto.Hash> baseTransactionBranch = new List<Crypto.Hash>();
//        public BaseTransaction baseTransaction = new BaseTransaction();
//        public List<Crypto.Hash> blockchainBranch = new List<Crypto.Hash>();
//    }

//    public class BlockHeader
//    {
//        public byte majorVersion;
//        public byte minorVersion;
//        public uint nonce;
//        public ulong timestamp;
//        public Crypto.Hash previousBlockHash = new Crypto.Hash();
//    }

//    public class BlockTemplate : BlockHeader
//    {
//        public ParentBlock parentBlock = new ParentBlock();
//        public Transaction baseTransaction = new Transaction();
//        public List<Crypto.Hash> transactionHashes = new List<Crypto.Hash>();
//    }

//    public class AccountPublicAddress
//    {
//        public Crypto.PublicKey spendPublicKey = new Crypto.PublicKey();
//        public Crypto.PublicKey viewPublicKey = new Crypto.PublicKey();
//    }

//    public class AccountKeys
//    {
//        public AccountPublicAddress address = new AccountPublicAddress();
//        public Crypto.SecretKey spendSecretKey = new Crypto.SecretKey();
//        public Crypto.SecretKey viewSecretKey = new Crypto.SecretKey();
//    }

//    public class KeyPair
//    {
//        public Crypto.PublicKey publicKey = new Crypto.PublicKey();
//        public Crypto.SecretKey secretKey = new Crypto.SecretKey();
//    }


//    public class RawBlock
//    {
//        public BinaryArray block = new BinaryArray(); //BlockTemplate
//        public List<BinaryArray> transactions = new List<BinaryArray>();
//    }

//}



//namespace CryptoNote
//{

//    public enum TransactionRemoveReason : byte
//    {
//        INCLUDED_IN_BLOCK = 0,
//        TIMEOUT = 1
//    }

//    public class TransactionOutputDetails
//    {
//        public TransactionOutput output = new TransactionOutput();
//        public ulong globalIndex;
//    }

//    public class TransactionOutputReferenceDetails
//    {
//        public Crypto.Hash transactionHash = new Crypto.Hash();
//        public uint number;
//    }

//    public class BaseInputDetails
//    {
//        public BaseInput input = new BaseInput();
//        public ulong amount;
//    }

//    public class KeyInputDetails
//    {
//        public KeyInput input = new KeyInput();
//        public ulong mixin;
//        public TransactionOutputReferenceDetails output = new TransactionOutputReferenceDetails();
//    }



//    public class TransactionExtraDetails
//    {
//        public Crypto.PublicKey publicKey = new Crypto.PublicKey();
//        public BinaryArray nonce = new BinaryArray();
//        public BinaryArray raw = new BinaryArray();
//    }

//    public class TransactionDetails
//    {
//        public Crypto.Hash hash = new Crypto.Hash();
//        public ulong size = 0;
//        public ulong fee = 0;
//        public ulong totalInputsAmount = 0;
//        public ulong totalOutputsAmount = 0;
//        public ulong mixin = 0;
//        public ulong unlockTime = 0;
//        public ulong timestamp = 0;
//        public Crypto.Hash paymentId = new Crypto.Hash();
//        public bool hasPaymentId = false;
//        public bool inBlockchain = false;
//        public Crypto.Hash blockHash = new Crypto.Hash();
//        public uint blockIndex = 0;
//        public TransactionExtraDetails extra = new TransactionExtraDetails();
//        public List<List<Crypto.Signature>> signatures = new List<List<Crypto.Signature>>();
//        public List<TransactionInputDetails> inputs = new List<TransactionInputDetails>();
//        public List<TransactionOutputDetails> outputs = new List<TransactionOutputDetails>();
//    }

//    public class BlockDetails
//    {
//        public byte majorVersion = 0;
//        public byte minorVersion = 0;
//        public ulong timestamp = 0;
//        public Crypto.Hash prevBlockHash = new Crypto.Hash();
//        public uint nonce = 0;
//        public bool isAlternative = false;
//        public uint index = 0;
//        public Crypto.Hash hash = new Crypto.Hash();
//        public ulong difficulty = 0;
//        public ulong reward = 0;
//        public ulong baseReward = 0;
//        public ulong blockSize = 0;
//        public ulong transactionsCumulativeSize = 0;
//        public ulong alreadyGeneratedCoins = 0;
//        public ulong alreadyGeneratedTransactions = 0;
//        public ulong sizeMedian = 0;
//        public double penalty = 0.0;
//        public ulong totalFeeAmount = 0;
//        public List<TransactionDetails> transactions = new List<TransactionDetails>();
//    }

//}


//// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
////
//// This file is part of Bytecoin.
////
//// Bytecoin is free software: you can redistribute it and/or modify
//// it under the terms of the GNU Lesser General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
////
//// Bytecoin is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU Lesser General Public License for more details.
////
//// You should have received a copy of the GNU Lesser General Public License
//// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.






using BlockchainCommon.Common.CryptoNote;
using System;
using System.Collections.Generic;

namespace CryptoNote
{
    //-----------------------------------------------

    public class EMPTY_STRUCT
    {
        public void serialize(ISerializer s)
        {
        }
    }

    public class STATUS_STRUCT
    {
        public string status;

        public void serialize(ISerializer s)
        {
            s.FunctorMethod(status, "status");
        }
    }

    public class COMMAND_RPC_GET_HEIGHT
    {

        public class response
        {
            public ulong height;
            public uint network_height;
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(height, "height");
                s.FunctorMethod(network_height, "network_height");
                s.FunctorMethod(status, "status");
            }
        }
    }

    public class COMMAND_RPC_GET_BLOCKS_FAST
    {

        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public List<Crypto.Hash> block_ids = new List<Crypto.Hash>(); //*first 10 blocks id goes sequential, next goes in pow(2,n) offset, like 2, 4, 8, 16, 32, 64 and so on, and the last one is always genesis block */

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(block_ids, "block_ids");
            }
        }

        public class response
        {
            public List<RawBlock> blocks = new List<RawBlock>();
            public ulong start_height;
            public ulong current_height;
            public string status;
        }
    }
    //-----------------------------------------------
    public class COMMAND_RPC_GET_TRANSACTIONS
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public List<string> txs_hashes = new List<string>();

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(txs_hashes, "txs_hashes");
            }
        }

        public class response
        {
            public List<string> txs_as_hex = new List<string>(); //transactions blobs as hex
            public List<string> missed_tx = new List<string>(); //not found transactions
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(txs_as_hex, "txs_as_hex");
                s.FunctorMethod(missed_tx, "missed_tx");
                s.FunctorMethod(status, "status");
            }
        }
    }
    //-----------------------------------------------
    public class COMMAND_RPC_GET_POOL_CHANGES
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public Crypto.Hash tailBlockId = new Crypto.Hash();
            public List<Crypto.Hash> knownTxsIds = new List<Crypto.Hash>();

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(tailBlockId, "tailBlockId");
                s.FunctorMethod(knownTxsIds, "knownTxsIds");
            }
        }

        public class response
        {
            public bool isTailBlockActual;
            public List<BinaryArray> addedTxs = new List<BinaryArray>(); // Added transactions blobs
            public List<Crypto.Hash> deletedTxsIds = new List<Crypto.Hash>(); // IDs of not found transactions
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(isTailBlockActual, "isTailBlockActual");
                s.FunctorMethod(addedTxs, "addedTxs");
                s.FunctorMethod(deletedTxsIds, "deletedTxsIds");
                s.FunctorMethod(status, "status");
            }
        }
    }

    public class COMMAND_RPC_GET_POOL_CHANGES_LITE
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public Crypto.Hash tailBlockId = new Crypto.Hash();
            public List<Crypto.Hash> knownTxsIds = new List<Crypto.Hash>();

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(tailBlockId, "tailBlockId");
                s.FunctorMethod(knownTxsIds, "knownTxsIds");
            }
        }

        public class response
        {
            public bool isTailBlockActual;
            public List<TransactionPrefixInfo> addedTxs = new List<TransactionPrefixInfo>(); // Added transactions blobs
            public List<Crypto.Hash> deletedTxsIds = new List<Crypto.Hash>(); // IDs of not found transactions
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(isTailBlockActual, "isTailBlockActual");
                s.FunctorMethod(addedTxs, "addedTxs");
                s.FunctorMethod(deletedTxsIds, "deletedTxsIds");
                s.FunctorMethod(status, "status");
            }
        }
    }

    //-----------------------------------------------
    public class COMMAND_RPC_GET_TX_GLOBAL_OUTPUTS_INDEXES
    {

        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public Crypto.Hash txid = new Crypto.Hash();

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(txid, "txid");
            }
        }

        public class response
        {
            public List<ulong> o_indexes = new List<ulong>();
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(o_indexes, "o_indexes");
                s.FunctorMethod(status, "status");
            }
        }
    }
    //-----------------------------------------------
    public class COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_request
    {
        public List<ulong> amounts = new List<ulong>();
        public ushort outs_count;

        public void serialize(ISerializer s)
        {
            s.FunctorMethod(amounts, "amounts");
            s.FunctorMethod(outs_count, "outs_count");
        }
    }

    //C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
    //#pragma pack(push, 1)
    public class COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_out_entry
    {
        public uint global_amount_index;
        public Crypto.PublicKey out_key = new Crypto.PublicKey();

        public void serialize(ISerializer s)
        {
            s.FunctorMethod(global_amount_index, "global_amount_index");
            s.FunctorMethod(out_key, "out_key");
        }
    }
    //C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
    //#pragma pack(pop)

    public class COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount
    {
        public ulong amount;
        public List<COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_out_entry> outs = new List<COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_out_entry>();

        public void serialize(ISerializer s)
        {
            s.FunctorMethod(amount, "amount");
            s.FunctorMethod(outs, "outs");
        }
    }

    public class COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_response
    {
        public List<COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount> outs = new List<COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS_outs_for_amount>();
        public string status;

        public void serialize(ISerializer s)
        {
            s.FunctorMethod(outs, "outs");
            s.FunctorMethod(status, "status");
        }
    }

    public class COMMAND_RPC_GET_RANDOM_OUTPUTS_FOR_AMOUNTS
    {

    }

    //-----------------------------------------------
    public class COMMAND_RPC_SEND_RAW_TX
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public string tx_as_hex;

            //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
            public request()
            {
            }
            //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
            //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
            //	request(Transaction UnnamedParameter);

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(tx_as_hex, new "tx_as_hex");
            }
        }

        public class response
        {
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(status, "status");
            }
        }
    }
    //-----------------------------------------------
    public class COMMAND_RPC_START_MINING
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public string miner_address;
            public ulong threads_count;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(miner_address, "miner_address");
                s.FunctorMethod(threads_count, "threads_count");
            }
        }

        public class response
        {
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(status, "status");
            }
        }
    }
    //-----------------------------------------------
    public class COMMAND_RPC_GET_INFO
    {

        public class response
        {
            public string status;
            public ulong height;
            public ulong difficulty;
            public ulong tx_count;
            public ulong tx_pool_size;
            public ulong alt_blocks_count;
            public ulong outgoing_connections_count;
            public ulong incoming_connections_count;
            public ulong white_peerlist_size;
            public ulong grey_peerlist_size;
            public uint last_known_block_index;
            public uint network_height;
            public List<ulong> upgrade_heights = new List<ulong>();
            public ulong supported_height;
            public uint hashrate;
            public byte major_version;
            public byte minor_version;
            public string version;

            //Original:
            //public ulong start_time;
            public DateTime start_time;

            public bool synced;
            public bool testnet;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(status, "status");
                s.FunctorMethod(height, "height");
                s.FunctorMethod(difficulty, "difficulty");
                s.FunctorMethod(tx_count, "tx_count");
                s.FunctorMethod(tx_pool_size, "tx_pool_size");
                s.FunctorMethod(alt_blocks_count, "alt_blocks_count");
                s.FunctorMethod(outgoing_connections_count, "outgoing_connections_count");
                s.FunctorMethod(incoming_connections_count, "incoming_connections_count");
                s.FunctorMethod(white_peerlist_size, "white_peerlist_size");
                s.FunctorMethod(grey_peerlist_size, "grey_peerlist_size");
                s.FunctorMethod(last_known_block_index, "last_known_block_index");
                s.FunctorMethod(network_height, "network_height");
                s.FunctorMethod(upgrade_heights, "upgrade_heights");
                s.FunctorMethod(supported_height, "supported_height");
                s.FunctorMethod(hashrate, "hashrate");
                s.FunctorMethod(major_version, "major_version");
                s.FunctorMethod(minor_version, "minor_version");
                s.FunctorMethod(start_time, "start_time");
                s.FunctorMethod(synced, "synced");
                s.FunctorMethod(testnet, "testnet");
                s.FunctorMethod(version, "version");
            }
        }
    }

    //-----------------------------------------------
    public class COMMAND_RPC_STOP_MINING
    {
    }

    //-----------------------------------------------
    public class COMMAND_RPC_STOP_DAEMON
    {
    }

    //
    public class COMMAND_RPC_GETBLOCKCOUNT
    {

        //C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class response
        {
            public ulong count;
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(count, "count");
                s.FunctorMethod(status, "status");
            }
        }
    }

    public class COMMAND_RPC_GETBLOCKHASH
    {
    }

    public class COMMAND_RPC_GETBLOCKTEMPLATE
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public ulong reserve_size; //max 255 bytes
            public string wallet_address;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(reserve_size, "reserve_size");
                s.FunctorMethod(wallet_address, "wallet_address");
            }
        }

        //C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class response
        {
            public ulong difficulty;
            public uint height;
            public ulong reserved_offset;
            public string blocktemplate_blob;
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(difficulty, "difficulty");
                s.FunctorMethod(height, "height");
                s.FunctorMethod(reserved_offset, "reserved_offset");
                s.FunctorMethod(blocktemplate_blob, "blocktemplate_blob");
                s.FunctorMethod(status, "status");
            }
        }
    }

    public class COMMAND_RPC_GET_CURRENCY_ID
    {

        //C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class response
        {
            public string currency_id_blob;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(currency_id_blob, "currency_id_blob");
            }
        }
    }

    public class COMMAND_RPC_SUBMITBLOCK
    {
    }

    public class block_header_response
    {
        public byte major_version;
        public byte minor_version;
        public ulong timestamp;
        public string prev_hash;
        public uint nonce;
        public bool orphan_status;
        public uint height;
        public uint depth;
        public string hash;
        public ulong difficulty;
        public ulong reward;
        public uint num_txes;
        public ulong block_size;

        public void serialize(ISerializer s)
        {
            s.FunctorMethod(major_version, "major_version");
            s.FunctorMethod(minor_version, "minor_version");
            s.FunctorMethod(timestamp, "timestamp");
            s.FunctorMethod(prev_hash, "prev_hash");
            s.FunctorMethod(nonce, "nonce");
            s.FunctorMethod(orphan_status, "orphan_status");
            s.FunctorMethod(height, "height");
            s.FunctorMethod(depth, "depth");
            s.FunctorMethod(hash, "hash");
            s.FunctorMethod(difficulty, "difficulty");
            s.FunctorMethod(reward, "reward");
            s.FunctorMethod(num_txes, "num_txes");
            s.FunctorMethod(block_size, "block_size");
        }
    }

    public class BLOCK_HEADER_RESPONSE
    {
        public string status;
        public block_header_response block_header = new block_header_response();

        public void serialize(ISerializer s)
        {
            s.FunctorMethod(block_header, "block_header");
            s.FunctorMethod(status, "status");
        }
    }


    public class COMMAND_RPC_GET_BLOCK_HEADERS_RANGE
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public ulong start_height;
            public ulong end_height;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(start_height, "start_height");
                s.FunctorMethod(end_height, "end_height");
            }
            /*BEGIN_KV_SERIALIZE_MAP()
            KV_SERIALIZE(start_height)
            KV_SERIALIZE(end_height)
            END_KV_SERIALIZE_MAP()*/
        }

        //C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class response
        {
            public string status;
            public List<block_header_response> headers = new List<block_header_response>();
            public bool untrusted;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(status, "status");
                s.FunctorMethod(headers, "headers");
                s.FunctorMethod(untrusted, "untrusted");
            }
            /*BEGIN_KV_SERIALIZE_MAP()
            KV_SERIALIZE(status)
            KV_SERIALIZE(headers)
            KV_SERIALIZE(untrusted)
            END_KV_SERIALIZE_MAP()*/
        }
    }

    public class f_transaction_short_response
    {
        public string hash;
        public ulong fee;
        public ulong amount_out;
        public ulong size;

        public void serialize(ISerializer s)
        {
            s.FunctorMethod(hash, "hash");
            s.FunctorMethod(fee, "fee");
            s.FunctorMethod(amount_out, "amount_out");
            s.FunctorMethod(size, "size");
        }
    }

    public class f_transaction_details_response
    {
        public string hash;
        public ulong size;
        public string paymentId;
        public ulong mixin;
        public ulong fee;
        public ulong amount_out;

        public void serialize(ISerializer s)
        {
            s.FunctorMethod(hash, "hash");
            s.FunctorMethod(size, "size");
            s.FunctorMethod(paymentId, "paymentId");
            s.FunctorMethod(mixin, "mixin");
            s.FunctorMethod(fee, "fee");
            s.FunctorMethod(amount_out, "amount_out");
        }
    }

    public class f_block_short_response
    {
        public ulong difficulty;
        public ulong timestamp;
        public uint height;
        public string hash;
        public ulong tx_count;
        public ulong cumul_size;

        public void serialize(ISerializer s)
        {
            s.FunctorMethod(difficulty, "difficulty");
            s.FunctorMethod(timestamp, "timestamp");
            s.FunctorMethod(height, "height");
            s.FunctorMethod(hash, "hash");
            s.FunctorMethod(cumul_size, "cumul_size");
            s.FunctorMethod(tx_count, "tx_count");
        }
    }

    public class f_block_details_response
    {
        public byte major_version;
        public byte minor_version;
        public ulong timestamp;
        public string prev_hash;
        public uint nonce;
        public bool orphan_status;
        public uint height;
        public ulong depth;
        public string hash;
        public ulong difficulty;
        public ulong reward;
        public ulong blockSize;
        public ulong sizeMedian;
        public ulong effectiveSizeMedian;
        public ulong transactionsCumulativeSize;
        public string alreadyGeneratedCoins;
        public ulong alreadyGeneratedTransactions;
        public ulong baseReward;
        public double penalty;
        public ulong totalFeeAmount;
        public List<f_transaction_short_response> transactions = new List<f_transaction_short_response>();

        public void serialize(ISerializer s)
        {
            s.FunctorMethod(major_version, "major_version");
            s.FunctorMethod(minor_version, "minor_version");
            s.FunctorMethod(timestamp, "timestamp");
            s.FunctorMethod(prev_hash, "prev_hash");
            s.FunctorMethod(nonce, "nonce");
            s.FunctorMethod(orphan_status, "orphan_status");
            s.FunctorMethod(height, "height");
            s.FunctorMethod(depth, "depth");
            s.FunctorMethod(hash, "hash");
            s.FunctorMethod(difficulty, "difficulty");
            s.FunctorMethod(reward, "reward");
            s.FunctorMethod(blockSize, "blockSize");
            s.FunctorMethod(sizeMedian, "sizeMedian");
            s.FunctorMethod(effectiveSizeMedian, "effectiveSizeMedian");
            s.FunctorMethod(transactionsCumulativeSize, "transactionsCumulativeSize");
            s.FunctorMethod(alreadyGeneratedCoins, "alreadyGeneratedCoins");
            s.FunctorMethod(alreadyGeneratedTransactions, "alreadyGeneratedTransactions");
            s.FunctorMethod(baseReward, "baseReward");
            s.FunctorMethod(penalty, "penalty");
            s.FunctorMethod(transactions, "transactions");
            s.FunctorMethod(totalFeeAmount, "totalFeeAmount");
        }
    }
    public class COMMAND_RPC_GET_LAST_BLOCK_HEADER
    {
    }

    public class COMMAND_RPC_GET_BLOCK_HEADER_BY_HASH
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public string hash;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(hash, "hash");
            }
        }

    }

    public class COMMAND_RPC_GET_BLOCK_HEADER_BY_HEIGHT
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public ulong height;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(height, "height");
            }
        }

    }

    public class F_COMMAND_RPC_GET_BLOCKS_LIST
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public ulong height;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(height, "height");
            }
        }

        //C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class response
        {
            public List<f_block_short_response> blocks = new List<f_block_short_response>(); //transactions blobs as hex
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(blocks, "blocks");
                s.FunctorMethod(status, "status");
            }
        }
    }

    public class F_COMMAND_RPC_GET_BLOCK_DETAILS
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public string hash;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(hash, "hash");
            }
        }

        //C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class response
        {
            public f_block_details_response block = new f_block_details_response();
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(block, "block");
                s.FunctorMethod(status, "status");
            }
        }
    }

    public class F_COMMAND_RPC_GET_TRANSACTION_DETAILS
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public string hash;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(hash, "hash");
            }
        }

        //C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class response
        {
            public Transaction tx = new Transaction();
            public f_transaction_details_response txDetails = new f_transaction_details_response();
            public f_block_short_response block = new f_block_short_response();
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(tx, "tx");
                s.FunctorMethod(txDetails, "txDetails");
                s.FunctorMethod(block, "block");
                s.FunctorMethod(status, "status");
            }
        }
    }

    public class F_COMMAND_RPC_GET_POOL
    {

        //C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class response
        {
            public List<f_transaction_short_response> transactions = new List<f_transaction_short_response>(); //transactions blobs as hex
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(transactions, "transactions");
                s.FunctorMethod(status, "status");
            }
        }
    }
    public class COMMAND_RPC_QUERY_BLOCKS
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public List<Crypto.Hash> block_ids = new List<Crypto.Hash>(); //*first 10 blocks id goes sequential, next goes in pow(2,n) offset, like 2, 4, 8, 16, 32, 64 and so on, and the last one is always genesis block */
            public ulong timestamp;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(block_ids, "block_ids");
                s.FunctorMethod(timestamp, "timestamp");
            }
        }

        //C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class response
        {
            public string status;
            public ulong start_height;
            public ulong current_height;
            public ulong full_offset;
            public List<BlockFullInfo> items = new List<BlockFullInfo>();

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(status, "status");
                s.FunctorMethod(start_height, "start_height");
                s.FunctorMethod(current_height, "current_height");
                s.FunctorMethod(full_offset, "full_offset");
                s.FunctorMethod(items, "items");
            }
        }
    }

    public class COMMAND_RPC_QUERY_BLOCKS_LITE
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public List<Crypto.Hash> blockIds = new List<Crypto.Hash>();
            public ulong timestamp;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(blockIds, "blockIds");
                s.FunctorMethod(timestamp, "timestamp");
            }
        }

        //C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class response
        {
            public string status;
            public ulong startHeight;
            public ulong currentHeight;
            public ulong fullOffset;
            public List<BlockShortInfo> items = new List<BlockShortInfo>();

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(status, "status");
                s.FunctorMethod(startHeight, "startHeight");
                s.FunctorMethod(currentHeight, "currentHeight");
                s.FunctorMethod(fullOffset, "fullOffset");
                s.FunctorMethod(items, "items");
            }
        }
    }

    public class COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HEIGHTS
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public List<uint> blockHeights = new List<uint>();

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(blockHeights, "blockHeights");
            }
        }

        //C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class response
        {
            public List<BlockDetails> blocks = new List<BlockDetails>();
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(status, "status");
                s.FunctorMethod(blocks, "blocks");
            }
        }
    }

    public class COMMAND_RPC_GET_BLOCKS_DETAILS_BY_HASHES
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public List<Crypto.Hash> blockHashes = new List<Crypto.Hash>();

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(blockHashes, "blockHashes");
            }
        }

        //C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class response
        {
            public List<BlockDetails> blocks = new List<BlockDetails>();
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(status, "status");
                s.FunctorMethod(blocks, "blocks");
            }
        }
    }

    public class COMMAND_RPC_GET_BLOCK_DETAILS_BY_HEIGHT
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public uint blockHeight;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(blockHeight, "blockHeight");
            }
        }

        //C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class response
        {
            public BlockDetails block = new BlockDetails();
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(status, "status");
                s.FunctorMethod(block, "block");
            }
        }
    }

    public class COMMAND_RPC_GET_BLOCKS_HASHES_BY_TIMESTAMPS
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public ulong timestampBegin;
            public ulong secondsCount;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(timestampBegin, "timestampBegin");
                s.FunctorMethod(secondsCount, "secondsCount");
            }
        }

        //C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class response
        {
            public List<Crypto.Hash> blockHashes = new List<Crypto.Hash>();
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(status, "status");
                s.FunctorMethod(blockHashes, "blockHashes");
            }
        }
    }

    public class COMMAND_RPC_GET_TRANSACTION_HASHES_BY_PAYMENT_ID
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public Crypto.Hash paymentId = new Crypto.Hash();

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(paymentId, "paymentId");
            }
        }

        //C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class response
        {
            public List<Crypto.Hash> transactionHashes = new List<Crypto.Hash>();
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(status, "status");
                s.FunctorMethod(transactionHashes, "transactionHashes");
            }
        }
    }

    public class COMMAND_RPC_GET_TRANSACTION_DETAILS_BY_HASHES
    {
        //C++ TO C# CONVERTER TODO TASK: The typedef 'request' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class request
        {
            public List<Crypto.Hash> transactionHashes = new List<Crypto.Hash>();

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(transactionHashes, "transactionHashes");
            }
        }

        //C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class response
        {
            public List<TransactionDetails> transactions = new List<TransactionDetails>();
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(status, "status");
                s.FunctorMethod(transactions, "transactions");
            }
        }
    }

    public class COMMAND_RPC_GET_PEERS
    {
        // TODO: rename peers to white_peers - do at v1 

        //C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class response
        {
            public string status;
            public List<string> peers = new List<string>();
            public List<string> gray_peers = new List<string>();

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(status, "status");
                s.FunctorMethod(peers, "peers");
                s.FunctorMethod(gray_peers, "gray_peers");
            }
        }
    }

    public class COMMAND_RPC_GET_FEE_ADDRESS
    {

        //C++ TO C# CONVERTER TODO TASK: The typedef 'response' was defined in multiple preprocessor conditionals and cannot be replaced in-line:
        public class response
        {
            public string address;
            public uint amount;
            public string status;

            public void serialize(ISerializer s)
            {
                s.FunctorMethod(address, "address");
                s.FunctorMethod(amount, "amount");
                s.FunctorMethod(status, "status");
            }
        }
    }

}


//// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
////
//// This file is part of Bytecoin.
////
//// Bytecoin is free software: you can redistribute it and/or modify
//// it under the terms of the GNU Lesser General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
////
//// Bytecoin is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU Lesser General Public License for more details.
////
//// You should have received a copy of the GNU Lesser General Public License
//// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.




//namespace CryptoNote
//{

//    namespace TransactionTypes
//    {

//        public enum InputType : byte
//        {
//            Invalid,
//            Key,
//            Generating
//        }
//        public enum OutputType : byte
//        {
//            Invalid,
//            Key
//        }

//        public class GlobalOutput
//        {
//            public Crypto.PublicKey targetKey = new Crypto.PublicKey();
//            public uint outputIndex;
//        }


//        public class OutputKeyInfo
//        {
//            public Crypto.PublicKey transactionPublicKey = new Crypto.PublicKey();
//            public uint transactionIndex;
//            public uint outputInTransaction;
//        }

//        public class InputKeyInfo
//        {
//            public ulong amount;
//            public GlobalOutputsContainer outputs = new GlobalOutputsContainer();
//            public OutputKeyInfo realOutput = new OutputKeyInfo();
//        }
//    }

//    //
//    // ITransactionReader
//    // 
//    public abstract class ITransactionReader : System.IDisposable
//    {
//        public virtual void Dispose()
//        {
//        }

//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual Crypto::Hash getTransactionHash() const = 0;
//        public abstract Crypto.Hash getTransactionHash();
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual Crypto::Hash getTransactionPrefixHash() const = 0;
//        public abstract Crypto.Hash getTransactionPrefixHash();
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual Crypto::PublicKey getTransactionPublicKey() const = 0;
//        public abstract Crypto.PublicKey getTransactionPublicKey();
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual bool getTransactionSecretKey(Crypto::SecretKey& key) const = 0;
//        public abstract bool getTransactionSecretKey(Crypto.SecretKey key);
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual ulong getUnlockTime() const = 0;
//        public abstract ulong getUnlockTime();

//        // extra
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual bool getPaymentId(Crypto::Hash& paymentId) const = 0;
//        public abstract bool getPaymentId(Crypto.Hash paymentId);
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual bool getExtraNonce(BinaryArray& nonce) const = 0;
//        public abstract bool getExtraNonce(BinaryArray nonce);
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual BinaryArray getExtra() const = 0;
//        public abstract BinaryArray getExtra();

//        // inputs
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual uint getInputCount() const = 0;
//        public abstract uint getInputCount();
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual ulong getInputTotalAmount() const = 0;
//        public abstract ulong getInputTotalAmount();
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual TransactionTypes::InputType getInputType(uint index) const = 0;
//        public abstract TransactionTypes.InputType getInputType(uint index);
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual void getInput(uint index, KeyInput& input) const = 0;
//        public abstract void getInput(uint index, KeyInput input);

//        // outputs
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual uint getOutputCount() const = 0;
//        public abstract uint getOutputCount();
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual ulong getOutputTotalAmount() const = 0;
//        public abstract ulong getOutputTotalAmount();
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual TransactionTypes::OutputType getOutputType(uint index) const = 0;
//        public abstract TransactionTypes.OutputType getOutputType(uint index);
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual void getOutput(uint index, KeyOutput& output, ulong& amount) const = 0;
//        public abstract void getOutput(uint index, KeyOutput output, ref ulong amount);

//        // signatures
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual uint getRequiredSignaturesCount(uint inputIndex) const = 0;
//        public abstract uint getRequiredSignaturesCount(uint inputIndex);
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual bool findOutputsToAccount(const AccountPublicAddress& addr, const Crypto::SecretKey& viewSecretKey, ClassicVector<uint>& outs, ulong& outputAmount) const = 0;
//        public abstract bool findOutputsToAccount(AccountPublicAddress addr, Crypto.SecretKey viewSecretKey, List<uint> outs, ref ulong outputAmount);

//        // various checks
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual bool validateInputs() const = 0;
//        public abstract bool validateInputs();
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual bool validateOutputs() const = 0;
//        public abstract bool validateOutputs();
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual bool validateSignatures() const = 0;
//        public abstract bool validateSignatures();

//        // serialized transaction
//        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//        //ORIGINAL LINE: virtual BinaryArray getTransactionData() const = 0;
//        public abstract BinaryArray getTransactionData();
//    }

//    //
//    // ITransactionWriter
//    // 
//    public abstract class ITransactionWriter : System.IDisposable
//    {

//        public virtual void Dispose()
//        {
//        }

//        // transaction parameters
//        public abstract void setUnlockTime(ulong unlockTime);

//        // extra
//        public abstract void setPaymentId(Crypto.Hash paymentId);
//        public abstract void setExtraNonce(BinaryArray nonce);
//        public abstract void appendExtra(BinaryArray extraData);

//        // Inputs/Outputs 
//        public abstract uint addInput(KeyInput input);
//        public abstract uint addInput(AccountKeys senderKeys, TransactionTypes.InputKeyInfo info, KeyPair ephKeys);

//        public abstract uint addOutput(ulong amount, AccountPublicAddress to);
//        public abstract uint addOutput(ulong amount, KeyOutput @out);

//        // transaction info
//        public abstract void setTransactionSecretKey(Crypto.SecretKey key);

//        // signing
//        public abstract void signInputKey(uint input, TransactionTypes.InputKeyInfo info, KeyPair ephKeys);
//    }

//    //C++ TO C# CONVERTER TODO TASK: Multiple inheritance is not available in C#:
//    public abstract class ITransaction : ITransactionReader, ITransactionWriter
//    {
//        public override void Dispose()
//        {
//            base.Dispose();
//        }

//    }

//}


namespace CryptoNote
{
    public class OutEntry
    {
        public uint outGlobalIndex;
        public Crypto.PublicKey outKey = new Crypto.PublicKey();
    }

    public class OutsForAmount
    {
        public ulong amount;
        public List<OutEntry> outs = new List<OutEntry>();
    }

    public class TransactionShortInfo
    {
        public Crypto.Hash txId = new Crypto.Hash();
        public TransactionPrefix txPrefix = new TransactionPrefix();
    }

    public class BlockShortEntry
    {
        public Crypto.Hash blockHash = new Crypto.Hash();
        public bool hasBlock;
        public CryptoNote.BlockTemplate block = new CryptoNote.BlockTemplate();
        public List<TransactionShortInfo> txsShortInfo = new List<TransactionShortInfo>();
    }

    public class BlockHeaderInfo
    {
        public uint index;
        public byte majorVersion;
        public byte minorVersion;
        public ulong timestamp;
        public Crypto.Hash hash = new Crypto.Hash();
        public Crypto.Hash prevHash = new Crypto.Hash();
        public uint nonce;
        public bool isAlternative;
        public uint depth; // last block index = current block index + depth
        public ulong difficulty;
        public ulong reward;
    }
}

//namespace Crypto
//{
//    public static class GlobalMembers
//    {
//        /*
//          Cryptonight hash functions
//        */

//        public static void cn_fast_hash(object data, uint length, Hash hash)
//        {
//            //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//            cn_fast_hash(data, length, reinterpret_cast<char>(hash));
//        }

//        public static Hash cn_fast_hash(object data, uint length)
//        {
//            Hash h = new Hash();
//            //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//            cn_fast_hash(data, length, reinterpret_cast<char>(h));
//            return h;
//        }

//        // Standard CryptoNight
//        public static void cn_slow_hash_v0(object data, uint length, Hash hash)
//        {
//            //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//            cn_slow_hash(data, length, reinterpret_cast<char>(hash), 0, 0, 0, DefineConstants.CN_PAGE_SIZE, DefineConstants.CN_SCRATCHPAD, DefineConstants.CN_ITERATIONS);
//        }

//        public static void cn_slow_hash_v1(object data, uint length, Hash hash)
//        {
//            //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//            cn_slow_hash(data, length, reinterpret_cast<char>(hash), 0, 1, 0, DefineConstants.CN_PAGE_SIZE, DefineConstants.CN_SCRATCHPAD, DefineConstants.CN_ITERATIONS);
//        }

//        public static void cn_slow_hash_v2(object data, uint length, Hash hash)
//        {
//            //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//            cn_slow_hash(data, length, reinterpret_cast<char>(hash), 0, 2, 0, DefineConstants.CN_PAGE_SIZE, DefineConstants.CN_SCRATCHPAD, DefineConstants.CN_ITERATIONS);
//        }

//        // Standard CryptoNight Lite
//        public static void cn_lite_slow_hash_v0(object data, uint length, Hash hash)
//        {
//            //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//            cn_slow_hash(data, length, reinterpret_cast<char>(hash), 1, 0, 0, DefineConstants.CN_LITE_PAGE_SIZE, DefineConstants.CN_LITE_SCRATCHPAD, DefineConstants.CN_LITE_ITERATIONS);
//        }

//        public static void cn_lite_slow_hash_v1(object data, uint length, Hash hash)
//        {
//            //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//            cn_slow_hash(data, length, reinterpret_cast<char>(hash), 1, 1, 0, DefineConstants.CN_LITE_PAGE_SIZE, DefineConstants.CN_LITE_SCRATCHPAD, DefineConstants.CN_LITE_ITERATIONS);
//        }

//        public static void cn_lite_slow_hash_v2(object data, uint length, Hash hash)
//        {
//            //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//            cn_slow_hash(data, length, reinterpret_cast<char>(hash), 1, 2, 0, DefineConstants.CN_LITE_PAGE_SIZE, DefineConstants.CN_LITE_SCRATCHPAD, DefineConstants.CN_LITE_ITERATIONS);
//        }

//        // CryptoNight Soft Shell
//        public static void cn_soft_shell_slow_hash_v0(object data, uint length, Hash hash, uint height)
//        {
//            uint base_offset = (height % DefineConstants.CN_SOFT_SHELL_WINDOW);
//            int offset = (height % (DefineConstants.CN_SOFT_SHELL_WINDOW * 2)) - (base_offset * 2);
//            if (offset < 0)
//            {
//                offset = base_offset;
//            }

//            uint scratchpad = DefineConstants.CN_SOFT_SHELL_MEMORY + ((uint)offset * (DefineConstants.CN_SOFT_SHELL_WINDOW / DefineConstants.CN_SOFT_SHELL_MULTIPLIER));
//            uint iterations = (DefineConstants.CN_SOFT_SHELL_MEMORY / 2) + ((uint)offset * ((DefineConstants.CN_SOFT_SHELL_WINDOW / DefineConstants.CN_SOFT_SHELL_MULTIPLIER) / 2));
//            uint pagesize = scratchpad;

//            //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//            cn_slow_hash(data, length, reinterpret_cast<char>(hash), 1, 0, 0, pagesize, scratchpad, iterations);
//        }

//        public static void cn_soft_shell_slow_hash_v1(object data, uint length, Hash hash, uint height)
//        {
//            uint base_offset = (height % DefineConstants.CN_SOFT_SHELL_WINDOW);
//            int offset = (height % (DefineConstants.CN_SOFT_SHELL_WINDOW * 2)) - (base_offset * 2);
//            if (offset < 0)
//            {
//                offset = base_offset;
//            }

//            uint scratchpad = DefineConstants.CN_SOFT_SHELL_MEMORY + ((uint)offset * (DefineConstants.CN_SOFT_SHELL_WINDOW / DefineConstants.CN_SOFT_SHELL_MULTIPLIER));
//            uint iterations = (DefineConstants.CN_SOFT_SHELL_MEMORY / 2) + ((uint)offset * ((DefineConstants.CN_SOFT_SHELL_WINDOW / DefineConstants.CN_SOFT_SHELL_MULTIPLIER) / 2));
//            uint pagesize = scratchpad;

//            //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//            cn_slow_hash(data, length, reinterpret_cast<char>(hash), 1, 1, 0, pagesize, scratchpad, iterations);
//        }

//        public static void cn_soft_shell_slow_hash_v2(object data, uint length, Hash hash, uint height)
//        {
//            uint base_offset = (height % DefineConstants.CN_SOFT_SHELL_WINDOW);
//            int offset = (height % (DefineConstants.CN_SOFT_SHELL_WINDOW * 2)) - (base_offset * 2);
//            if (offset < 0)
//            {
//                offset = base_offset;
//            }

//            uint scratchpad = DefineConstants.CN_SOFT_SHELL_MEMORY + ((uint)offset * (DefineConstants.CN_SOFT_SHELL_WINDOW / DefineConstants.CN_SOFT_SHELL_MULTIPLIER));
//            uint iterations = (DefineConstants.CN_SOFT_SHELL_MEMORY / 2) + ((uint)offset * ((DefineConstants.CN_SOFT_SHELL_WINDOW / DefineConstants.CN_SOFT_SHELL_MULTIPLIER) / 2));
//            uint pagesize = scratchpad;

//            //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//            cn_slow_hash(data, length, reinterpret_cast<char>(hash), 1, 2, 0, pagesize, scratchpad, iterations);
//        }

//        public static void tree_hash(Hash hashes, uint count, Hash root_hash)
//        {
//            //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//            tree_hash(reinterpret_cast <const char(*)[HASH_SIZE]>(hashes), count, reinterpret_cast<char>(root_hash));
//	  }

//    public static void tree_branch(Hash hashes, uint count, Hash branch)
//    {
//        //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//        tree_branch(reinterpret_cast <const char(*)[HASH_SIZE]>(hashes), count, reinterpret_cast<char(*)[HASH_SIZE]>(branch));
//	  }

//public static void tree_hash_from_branch(Hash branch, uint depth, Hash leaf, object path, Hash root_hash)
//{
//    //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//    tree_hash_from_branch(reinterpret_cast <const char(*)[HASH_SIZE]>(branch), depth, reinterpret_cast<const char>(leaf), path, reinterpret_cast<char>(root_hash));
//	  }
//		public static bool operator ==(Hash _v1, Hash _v2)
//{
//    //C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
//    return memcmp(_v1, _v2, sizeof(Hash)) == 0;
//}
//public static bool operator !=(Hash _v1, Hash _v2)
//{
//    //C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
//    return memcmp(_v1, _v2, sizeof(Hash)) != 0;
//}
////C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
////	static_assert(sizeof(uint) <= sizeof(Hash), "Size of " "Hash" " must be at least that of size_t");
//public static uint hash_value(Hash _v)
//{
//    //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//    return reinterpret_cast <const uint &> (_v);
//}

////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
////void generate_random_bytes(uint n, object result);


////C++ TO C# CONVERTER NOTE: 'extern' variable declarations are not required in C#:
////  extern object random_lock;
//public static std::enable_if<std::is_pod<T>.value, T>.type rand<T>()
//{
//    std::remove_cv<T>.type res = new std::remove_cv<T>.type();
//    lock (random_lock)
//    {
//        generate_random_bytes(sizeof(T), res);
//    }
//    return res;
//}

///* Random number engine based on Crypto::rand()
// */
////C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
////ORIGINAL LINE: template <typename T>

///* Variants with vector<const PublicKey *> parameters.
// */
//public static void generate_ring_signature(Hash prefix_hash, KeyImage image, List<const PublicKey> pubs, SecretKey sec, uint sec_index, Signature sig)
//{
//    generate_ring_signature(prefix_hash, image, pubs.data(), pubs.Count, sec, sec_index, sig);
//}
//public static bool check_ring_signature(Hash prefix_hash, KeyImage image, List<const PublicKey> pubs, Signature sig, bool checkKeyImage)
//{
//    return check_ring_signature(prefix_hash, image, pubs.data(), pubs.Count, sig, checkKeyImage);
//}
//public static bool operator ==(PublicKey _v1, PublicKey _v2)
//{
//    //C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
//    return memcmp(_v1, _v2, sizeof(PublicKey)) == 0;
//}
//public static bool operator !=(PublicKey _v1, PublicKey _v2)
//{
//    //C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
//    return memcmp(_v1, _v2, sizeof(PublicKey)) != 0;
//}
////C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
////	static_assert(sizeof(uint) <= sizeof(PublicKey), "Size of " "PublicKey" " must be at least that of size_t");
//public static uint hash_value(PublicKey _v)
//{
//    //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//    return reinterpret_cast <const uint &> (_v);
//}
//public static bool operator ==(KeyImage _v1, KeyImage _v2)
//{
//    //C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
//    return memcmp(_v1, _v2, sizeof(KeyImage)) == 0;
//}
//public static bool operator !=(KeyImage _v1, KeyImage _v2)
//{
//    //C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
//    return memcmp(_v1, _v2, sizeof(KeyImage)) != 0;
//}
////C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
////	static_assert(sizeof(uint) <= sizeof(KeyImage), "Size of " "KeyImage" " must be at least that of size_t");
//public static uint hash_value(KeyImage _v)
//{
//    //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//    return reinterpret_cast <const uint &> (_v);
//}
//public static bool operator ==(Signature _v1, Signature _v2)
//{
//    //C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
//    return memcmp(_v1, _v2, sizeof(Signature)) == 0;
//}
//public static bool operator !=(Signature _v1, Signature _v2)
//{
//    //C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
//    return memcmp(_v1, _v2, sizeof(Signature)) != 0;
//}
//public static bool operator ==(SecretKey _v1, SecretKey _v2)
//{
//    //C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
//    return memcmp(_v1, _v2, sizeof(SecretKey)) == 0;
//}
//public static bool operator !=(SecretKey _v1, SecretKey _v2)
//{
//    //C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
//    return memcmp(_v1, _v2, sizeof(SecretKey)) != 0;
//}
////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
////	void chacha8(object[] data, uint length, byte key, byte iv, ref string cipher);
////C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
////#pragma pack(pop)

////C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
////  static_assert(sizeof(chacha8_key) == DefineConstants.CHACHA8_KEY_SIZE && sizeof(chacha8_iv) == DefineConstants.CHACHA8_IV_SIZE, "Invalid structure size");

//public static void chacha8(object data, uint length, chacha8_key key, chacha8_iv iv, ref string cipher)
//{
//    //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//    chacha8(data, length, reinterpret_cast <const byte> (key), reinterpret_cast <const byte> (iv), ref cipher);
//}

//public static void generate_chacha8_key(string password, chacha8_key key)
//{
//    //C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
//    //	static_assert(sizeof(chacha8_key) <= sizeof(Hash), "Size of hash must be at least that of chacha8_key");
//    Hash pwd_hash = new Hash();
//    cn_slow_hash_v0(password.data(), password.Length, pwd_hash);
//    //C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
//    memcpy(key, pwd_hash, sizeof(Crypto.chacha8_key));
//    //C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
//    memset(pwd_hash, 0, sizeof(Crypto.Hash));
//}

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//bool serialize(PublicKey pubKey, Common::StringView name, CryptoNote::ISerializer serializer);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//bool serialize(SecretKey secKey, Common::StringView name, CryptoNote::ISerializer serializer);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//bool serialize(Hash h, Common::StringView name, CryptoNote::ISerializer serializer);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//bool serialize(chacha8_iv chacha, Common::StringView name, CryptoNote::ISerializer serializer);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//bool serialize(KeyImage keyImage, Common::StringView name, CryptoNote::ISerializer serializer);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//bool serialize(Signature sig, Common::StringView name, CryptoNote::ISerializer serializer);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//bool serialize(EllipticCurveScalar ecScalar, Common::StringView name, CryptoNote::ISerializer serializer);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//bool serialize(EllipticCurvePoint ecPoint, Common::StringView name, CryptoNote::ISerializer serializer);
//	}
//}

//namespace CryptoNote
//{
//    public static class GlobalMembers
//    {
//        public static readonly Crypto.Hash NULL_HASH = boost::value_initialized<Crypto.Hash>();
//        public static readonly Crypto.PublicKey NULL_PUBLIC_KEY = boost::value_initialized<Crypto.PublicKey>();
//        public static readonly Crypto.SecretKey NULL_SECRET_KEY = boost::value_initialized<Crypto.SecretKey>();

//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //  KeyPair generateKeyPair();

//        public static ParentBlockSerializer makeParentBlockSerializer(BlockTemplate b, bool hashingSerialization, bool headerOnly)
//        {
//            //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
//            BlockTemplate blockRef = const_cast < BlockTemplate &> (b);
//            return new ParentBlockSerializer(blockRef.parentBlock, ref blockRef.timestamp, ref blockRef.nonce, hashingSerialization, headerOnly);
//        }
//        public static bool serialize<T>(T value, Common.StringView name, ISerializer serializer)
//        {
//            if (!serializer.beginObject(new Common.StringView(name)))
//            {
//                return false;
//            }

//            serialize(value, serializer.functorMethod);
//            serializer.endObject();
//            return true;
//        }

//        /* WARNING: If you get a compiler error pointing to this line, when serializing
//           a uint64_t, or other numeric type, this is due to your compiler treating some
//           typedef's differently, so it does not correspond to one of the numeric
//           types above. I tried using some template hackery to get around this, but
//           it did not work. I resorted to just using a uint64_t instead. */
//        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//        //ORIGINAL LINE: template<typename T>
//        public static void serialize<T>(T value, ISerializer serializer)
//        {
//            value.serialize(serializer.functorMethod);
//        }
//        public static std::enable_if<std::is_pod<T>.value>.type serializeAsBinary<T>(List<T> value, Common.StringView name, CryptoNote.ISerializer serializer)
//        {
//            string blob;
//            if (serializer.type() == ISerializer.INPUT)
//            {
//                serializer.binary(blob, new Common.StringView(name));
//                value.Resize(blob.Length / sizeof(T));
//                if (blob.Length != 0)
//                {
//                    //C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
//                    memcpy(value[0], blob.data(), blob.Length);
//                }
//            }
//            else
//            {
//                if (value.Count > 0)
//                {
//                    //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//                    blob.assign(reinterpret_cast <const char> (value[0]), value.Count * sizeof(T));
//                }
//                serializer.binary(blob, new Common.StringView(name));
//            }
//        }

//        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//        //ORIGINAL LINE: template<typename T>
//        public static std::enable_if<std::is_pod<T>.value>.type serializeAsBinary<T>(LinkedList<T> value, Common.StringView name, CryptoNote.ISerializer serializer)
//        {
//            string blob;
//            if (serializer.type() == ISerializer.INPUT)
//            {
//                serializer.binary(blob, new Common.StringView(name));

//                ulong count = blob.Length / sizeof(T);
//                //C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on this variable, so pointers on this variable are left unchanged:
//                //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//                T* ptr = reinterpret_cast <const T> (blob.data());

//                while (count-- != 0)
//                {
//                    value.AddLast(*ptr++);
//                }
//            }
//            else
//            {
//                if (value.Count > 0)
//                {
//                    blob.resize(value.Count * sizeof(T));
//                    //C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on this variable, so pointers on this variable are left unchanged:
//                    //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//                    T* ptr = reinterpret_cast<T>(blob[0]);

//                    foreach (var item in value)
//                    {
//                        *ptr++ = item;
//                    }
//                }
//                serializer.binary(blob, new Common.StringView(name));
//            }
//        }

//        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//        //ORIGINAL LINE: template <typename Cont>
//        public static bool serializeContainer<Cont>(Cont value, Common.StringView name, CryptoNote.ISerializer serializer)
//        {
//            ulong size = value.size();
//            if (!serializer.beginArray(ref size, new Common.StringView(name)))
//            {
//                if (serializer.type() == ISerializer.INPUT)
//                {
//                    value.clear();
//                }

//                return false;
//            }

//            value.resize(size);

//            foreach (var item in value)
//            {
//                //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
//                serializer.FunctorMethod(const_cast < typename Cont.value_type &> (item), "");
//            }

//            serializer.endArray();
//            return true;
//        }

//        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//        //ORIGINAL LINE: template <typename E>
//        public static bool serializeEnumClass<E>(ref E value, Common.StringView name, CryptoNote.ISerializer serializer)
//        {
//            //C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
//            //  static_assert(std::is_enum<E>::value, "E must be an enum class");


//            if (serializer.type() == CryptoNote.ISerializer.INPUT)
//            {
//                std::underlying_type<E>.type numericValue = new std::underlying_type<E>.type();
//                serializer.FunctorMethod(numericValue, name);
//                value = (E)numericValue;
//            }
//            else
//            {
//                var numericValue = (typename std::underlying_type<E>.type)value;
//                serializer.FunctorMethod(numericValue, name);
//            }

//            return true;
//        }

//        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//        //ORIGINAL LINE: template<typename T>
//        public static bool serialize<T>(List<T> value, Common.StringView name, CryptoNote.ISerializer serializer)
//        {
//            return serializeContainer(value, new Common.StringView(name), serializer.functorMethod);
//        }

//        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//        //ORIGINAL LINE: template<typename T>
//        public static bool serialize<T>(LinkedList<T> value, Common.StringView name, CryptoNote.ISerializer serializer)
//        {
//            return serializeContainer(value, new Common.StringView(name), serializer.functorMethod);
//        }

//        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//        //ORIGINAL LINE: template<typename MapT, typename ReserveOp>
//        public static bool serializeMap<MapT, ReserveOp>(MapT value, Common.StringView name, CryptoNote.ISerializer serializer, ReserveOp reserve)
//        {
//            ulong size = value.size();

//            if (!serializer.beginArray(ref size, new Common.StringView(name)))
//            {
//                if (serializer.type() == ISerializer.INPUT)
//                {
//                    value.clear();
//                }

//                return false;
//            }

//            if (serializer.type() == CryptoNote.ISerializer.INPUT)
//            {
//                reserve(size);

//                for (ulong i = 0; i < size; ++i)
//                {
//                    MapT.key_type key = new MapT.key_type();
//                    MapT.mapped_type v = new MapT.mapped_type();

//                    serializer.beginObject("");
//                    serializer.FunctorMethod(key, "key");
//                    serializer.FunctorMethod(v, "value");
//                    serializer.endObject();

//                    value.insert(Tuple.Create(std::move(key), std::move(v)));
//                }
//            }
//            else
//            {
//                foreach (var kv in value)
//                {
//                    serializer.beginObject("");
//                    //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
//                    serializer.FunctorMethod(const_cast < typename MapT.key_type &> (kv.first), "key");
//                    serializer.FunctorMethod(kv.second, "value");
//                    serializer.endObject();
//                }
//            }

//            serializer.endArray();
//            return true;
//        }

//        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//        //ORIGINAL LINE: template<typename SetT>
//        public static bool serializeSet<SetT>(SetT value, Common.StringView name, CryptoNote.ISerializer serializer)
//        {
//            ulong size = value.size();

//            if (!serializer.beginArray(ref size, new Common.StringView(name)))
//            {
//                if (serializer.type() == ISerializer.INPUT)
//                {
//                    value.clear();
//                }

//                return false;
//            }

//            if (serializer.type() == CryptoNote.ISerializer.INPUT)
//            {
//                for (ulong i = 0; i < size; ++i)
//                {
//                    SetT.value_type key = new SetT.value_type();
//                    serializer.FunctorMethod(key, "");
//                    value.insert(std::move(key));
//                }
//            }
//            else
//            {
//                foreach (var key in value)
//                {
//                    //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
//                    serializer.FunctorMethod(const_cast < typename SetT.value_type &> (key), "");
//                }
//            }

//            serializer.endArray();
//            return true;
//        }

//        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//        //ORIGINAL LINE: template<typename K, typename Hash>
//        public static bool serialize<K, Hash>(HashSet<K, Hash> value, Common.StringView name, CryptoNote.ISerializer serializer)
//        {
//            return serializeSet(value, new Common.StringView(name), serializer.functorMethod);
//        }

//        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//        //ORIGINAL LINE: template<typename K, typename Cmp>
//        public static bool serialize<K, Cmp>(SortedSet<K, Cmp> value, Common.StringView name, CryptoNote.ISerializer serializer)
//        {
//            return serializeSet(value, new Common.StringView(name), serializer.functorMethod);
//        }

//        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//        //ORIGINAL LINE: template<typename K, typename V, typename Hash>
//        public static bool serialize<K, V, Hash>(Dictionary<K, V, Hash> value, Common.StringView name, CryptoNote.ISerializer serializer)
//        {
//            return serializeMap(value, new Common.StringView(name), serializer.functorMethod, (ulong size) =>
//            {
//                value.reserve(size);
//            });
//        }

//        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//        //ORIGINAL LINE: template<typename K, typename V, typename Hash>
//        public static bool serialize<K, V, Hash>(std::unordered_multimap<K, V, Hash> value, Common.StringView name, CryptoNote.ISerializer serializer)
//        {
//            return serializeMap(value, new Common.StringView(name), serializer.functorMethod, (ulong size) =>
//            {
//                value.reserve(size);
//            });
//        }

//        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//        //ORIGINAL LINE: template<typename K, typename V, typename Hash>
//        public static bool serialize<K, V, Hash>(SortedDictionary<K, V, Hash> value, Common.StringView name, CryptoNote.ISerializer serializer)
//        {
//            return serializeMap(value, new Common.StringView(name), serializer.functorMethod, (ulong size) =>
//            {
//            });
//        }

//        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//        //ORIGINAL LINE: template<typename K, typename V, typename Hash>
//        public static bool serialize<K, V, Hash>(std::multimap<K, V, Hash> value, Common.StringView name, CryptoNote.ISerializer serializer)
//        {
//            return serializeMap(value, new Common.StringView(name), serializer.functorMethod, (ulong size) =>
//            {
//            });
//        }

//        //C++ TO C# CONVERTER TODO TASK: C++ 'constraints' are not converted by C++ to C# Converter:
//        //ORIGINAL LINE: template<uint64_t size>
//        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//        //ORIGINAL LINE: template<typename size>
//        public static bool serialize<size>(List<byte> value, Common.StringView name, CryptoNote.ISerializer s)
//        {
//            return s.binary(value.data(), value.Count, new Common.StringView(name));
//        }

//        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//        //ORIGINAL LINE: template <typename T1, typename T2>
//        public static void serialize<T1, T2>(Tuple<T1, T2> value, ISerializer s)
//        {
//            s.FunctorMethod(value.Item1, "first");
//            s.FunctorMethod(value.Item2, "second");
//        }

//        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//        //ORIGINAL LINE: template <typename Element, typename Iterator>
//        public static void writeSequence<Element, Iterator>(Iterator begin, Iterator end, Common.StringView name, ISerializer s)
//        {
//            ulong size = std::distance(begin, end);
//            s.beginArray(ref size, new Common.StringView(name));
//            for (Iterator i = begin; i != end; ++i)
//            {
//                //C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'const_cast' in C#:
//                s.FunctorMethod(const_cast < Element &> (*i), "");
//            }
//            s.endArray();
//        }

//        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//        //ORIGINAL LINE: template <typename Element, typename Iterator>
//        public static void readSequence<Element, Iterator>(Iterator outputIterator, Common.StringView name, ISerializer s)
//        {
//            ulong size = 0;
//            // array of zero size is not written in KVBinaryOutputStreamSerializer
//            if (!s.beginArray(ref size, new Common.StringView(name)))
//            {
//                return;
//            }

//            while (size-- != 0)
//            {
//                Element e = new default(Element);
//                s.FunctorMethod(e, "");
//                *outputIterator++ = std::move(e);
//            }

//            s.endArray();
//        }

//        //convinience function since we change block height type
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serializeBlockHeight(ISerializer s, ref uint blockHeight, Common::StringView name);

//        //convinience function since we change global output index type
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serializeGlobalOutputIndex(ISerializer s, ref uint globalOutputIndex, Common::StringView name);

//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(TransactionPrefix txP, ISerializer serializer);
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(Transaction tx, ISerializer serializer);
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(BaseTransaction tx, ISerializer serializer);
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(TransactionInput in, ISerializer serializer);
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(TransactionOutput in, ISerializer serializer);

//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(BaseInput gen, ISerializer serializer);
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(KeyInput key, ISerializer serializer);

//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(TransactionOutput output, ISerializer serializer);
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(TransactionOutputTarget output, ISerializer serializer);
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(KeyOutput key, ISerializer serializer);

//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(BlockHeader header, ISerializer serializer);
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(BlockTemplate block, ISerializer serializer);
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(ParentBlockSerializer pbs, ISerializer serializer);
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(TransactionExtraMergeMiningTag tag, ISerializer serializer);

//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(AccountPublicAddress address, ISerializer serializer);
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(AccountKeys keys, ISerializer s);

//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(KeyPair keyPair, ISerializer serializer);
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(RawBlock rawBlock, ISerializer serializer);

//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(TransactionOutputDetails output, ISerializer serializer);
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(TransactionOutputReferenceDetails outputReference, ISerializer serializer);

//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(BaseInputDetails inputBase, ISerializer serializer);
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(KeyInputDetails inputToKey, ISerializer serializer);
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(boost::variant<BaseInputDetails, KeyInputDetails> input, ISerializer serializer);

//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(TransactionExtraDetails extra, ISerializer serializer);
//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(TransactionDetails transaction, ISerializer serializer);

//        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//        //void serialize(BlockDetails block, ISerializer serializer);
//    }
//}

//public static class GlobalMembers
//{
//    // Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//    // Copyright (c) 2018, The TurtleCoin Developers
//    // Copyright (c) 2018, The Karai Developers
//    // 
//    // Please see the included LICENSE file for more information.


//    // Copyright (c) 2018, The TurtleCoin Developers
//    // 
//    // Please see the included LICENSE file for more information.


//    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//    //ulong nextDifficultyV6(ClassicVector<ulong> timestamps, ClassicVector<ulong> cumulativeDifficulties);

//    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//    //ulong nextDifficultyV5(ClassicVector<ulong> timestamps, ClassicVector<ulong> cumulativeDifficulties);

//    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//    //ulong nextDifficultyV4(ClassicVector<ulong> timestamps, ClassicVector<ulong> cumulativeDifficulties);

//    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//    //ulong nextDifficultyV3(ClassicVector<ulong> timestamps, ClassicVector<ulong> cumulativeDifficulties);

//    /* TODO: This has been added in the stdlib in c++17 */
//    //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//    //ORIGINAL LINE: template <typename T>
//    public static T clamp<T>(T n, T lower, T upper)
//    {
//        return Math.Max(lower, Math.Min(n, upper));
//    }
//}

//internal static class DefineConstants
//{
//    public const int CN_PAGE_SIZE = 2097152;
//    public const int CN_SCRATCHPAD = 2097152;
//    public const int CN_ITERATIONS = 1048576;
//    public const int CN_LITE_PAGE_SIZE = 2097152;
//    public const int CN_LITE_SCRATCHPAD = 1048576;
//    public const int CN_LITE_ITERATIONS = 524288;
//    public const int CN_SOFT_SHELL_MEMORY = 262144; // This defines the lowest memory utilization for our curve
//    public const int CN_SOFT_SHELL_WINDOW = 2048; // This defines how many blocks we cycle through as part of our algo sine wave
//    public const int CN_SOFT_SHELL_MULTIPLIER = 3; // This defines how big our steps are for each block and
//    public const int CHACHA8_KEY_SIZE = 32;
//    public const int CHACHA8_IV_SIZE = 8;
//    public const int BC_COMMANDS_POOL_BASE = 2000;
//    public const string CORE_RPC_STATUS_OK = "OK";
//    public const string CORE_RPC_STATUS_BUSY = "BUSY";
//}

////----------------------------------------------------------------------------------------
////	Copyright © 2006 - 2018 Tangible Software Solutions, Inc.
////	This class can be used by anyone provided that the copyright notice remains intact.
////
////	This class is used to convert some of the C++ std::vector methods to C#.
////----------------------------------------------------------------------------------------

//using System.Collections.Generic;

//internal static class VectorHelper
//{
//    public static void Resize<T>(this List<T> list, int newSize, T value = default(T))
//    {
//        if (list.Count > newSize)
//            list.RemoveRange(newSize, list.Count - newSize);
//        else if (list.Count < newSize)
//        {
//            for (int i = list.Count; i < newSize; i++)
//            {
//                list.Add(value);
//            }
//        }
//    }

//    public static void Swap<T>(this List<T> list1, List<T> list2)
//    {
//        List<T> temp = new List<T>(list1);
//        list1.Clear();
//        list1.AddRange(list2);
//        list2.Clear();
//        list2.AddRange(temp);
//    }

//    public static List<T> InitializedList<T>(int size, T value)
//    {
//        List<T> temp = new List<T>();
//        for (int count = 1; count <= size; count++)
//        {
//            temp.Add(value);
//        }

//        return temp;
//    }

//    public static List<List<T>> NestedList<T>(int outerSize, int innerSize)
//    {
//        List<List<T>> temp = new List<List<T>>();
//        for (int count = 1; count <= outerSize; count++)
//        {
//            temp.Add(new List<T>(innerSize));
//        }

//        return temp;
//    }

//    public static List<List<T>> NestedList<T>(int outerSize, int innerSize, T value)
//    {
//        List<List<T>> temp = new List<List<T>>();
//        for (int count = 1; count <= outerSize; count++)
//        {
//            temp.Add(InitializedList(innerSize, value));
//        }

//        return temp;
//    }
//}