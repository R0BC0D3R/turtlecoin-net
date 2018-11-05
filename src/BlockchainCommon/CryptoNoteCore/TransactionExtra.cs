// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

namespace CryptoNote
{

    public class TransactionExtraPadding
    {
        public uint size = new uint();
    }

    public class TransactionExtraPublicKey
    {
        public Crypto.PublicKey publicKey = new Crypto.PublicKey();
    }

    public class TransactionExtraNonce
    {
        public List<ushort> nonce = new List<ushort>();
    }

    public class TransactionExtraMergeMiningTag
    {
        public uint depth = new uint();
        public Crypto.Hash merkleRoot = new Crypto.Hash();
    }

    // tx_extra_field format, except tx_extra_padding and tx_extra_pub_key:
    //   varint tag;
    //   varint size;
    //   varint data[];



    //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
    //ORIGINAL LINE: template<typename T>
}

namespace CryptoNote
{
    //TODO: Changes from below. Not sure what it's supposed to do yet
    //public class ExtraSerializerVisitor : boost::static_visitor<bool>
    public class ExtraSerializerVisitor
    {
        public List<ushort> extra;

        public ExtraSerializerVisitor(List<ushort> tx_extra)
        {
            extra = tx_extra;
        }

        public static bool FunctorMethod(TransactionExtraPadding t)
        {
            if (t.size > DefineConstants.TX_EXTRA_PADDING_MAX_COUNT)
            {
                return false;
            }
            //C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'insert' method in C#:
            extra.insert(extra.end(), t.size, 0);
            return true;
        }

        public static bool FunctorMethod(TransactionExtraPublicKey t)
        {
            return CryptoNote.GlobalMembers.addTransactionPublicKeyToExtra(extra, t.publicKey);
        }

        public static bool FunctorMethod(TransactionExtraNonce t)
        {
            return CryptoNote.GlobalMembers.addExtraNonceToTransactionExtra(extra, t.nonce);
        }

        public static bool FunctorMethod(TransactionExtraMergeMiningTag t)
        {
            return CryptoNote.GlobalMembers.appendMergeMiningTagToExtra(extra, t);
        }
    }
}