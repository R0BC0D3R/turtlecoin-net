using CryptoNote;
using System.Collections.Generic;

namespace BlockchainCommon.Common
{
    namespace CryptoNote
    {
        public enum TransactionRemoveReason : ushort
        {
            INCLUDED_IN_BLOCK = 0,
            TIMEOUT = 1
        }

        public class TransactionOutputDetails
        {
            public TransactionOutput output = new TransactionOutput();
            public ulong globalIndex = new ulong();
        }

        public class TransactionOutputReferenceDetails
        {
            public Crypto.Hash transactionHash = new Crypto.Hash();
            public uint number = new uint();
        }

        public class BaseInputDetails
        {
            public BaseInput input = new BaseInput();
            public ulong amount = new ulong();
        }

        public class KeyInputDetails
        {
            public KeyInput input = new KeyInput();
            public ulong mixin = new ulong();
            public TransactionOutputReferenceDetails output = new TransactionOutputReferenceDetails();
        }



        public class TransactionExtraDetails
        {
            public Crypto.PublicKey publicKey = new Crypto.PublicKey();
            public BinaryArray nonce = new BinaryArray();
            public BinaryArray raw = new BinaryArray();
        }

        public class TransactionInputDetails
        {
            //TODO: Added this so it compiles
            public Dictionary<BaseInputDetails, KeyInputDetails> TransactionInputDetailsDictionary = new Dictionary<BaseInputDetails, KeyInputDetails>();
        }

        public class TransactionDetails
        {
            public Crypto.Hash hash = new Crypto.Hash();
            public ulong size = 0;
            public ulong fee = 0;
            public ulong totalInputsAmount = 0;
            public ulong totalOutputsAmount = 0;
            public ulong mixin = 0;
            public ulong unlockTime = 0;
            public ulong timestamp = 0;
            public Crypto.Hash paymentId = new Crypto.Hash();
            public bool hasPaymentId = false;
            public bool inBlockchain = false;
            public Crypto.Hash blockHash = new Crypto.Hash();
            public uint blockIndex = 0;
            public TransactionExtraDetails extra = new TransactionExtraDetails();
            public List<List<Crypto.Signature>> signatures = new List<List<Crypto.Signature>>();
            public List<TransactionInputDetails> inputs = new List<TransactionInputDetails>();
            public List<TransactionOutputDetails> outputs = new List<TransactionOutputDetails>();
        }

        public class BlockDetails
        {
            public ushort majorVersion = 0;
            public ushort minorVersion = 0;
            public ulong timestamp = 0;
            public Crypto.Hash prevBlockHash = new Crypto.Hash();
            public uint nonce = 0;
            public bool isAlternative = false;
            public uint index = 0;
            public Crypto.Hash hash = new Crypto.Hash();
            public ulong difficulty = 0;
            public ulong reward = 0;
            public ulong baseReward = 0;
            public ulong blockSize = 0;
            public ulong transactionsCumulativeSize = 0;
            public ulong alreadyGeneratedCoins = 0;
            public ulong alreadyGeneratedTransactions = 0;
            public ulong sizeMedian = 0;
            public double penalty = 0.0;
            public ulong totalFeeAmount = 0;
            public List<TransactionDetails> transactions = new List<TransactionDetails>();
        }

    }
}