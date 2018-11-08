// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.

namespace CryptoNote.parameters
{
    public static class CryptoNoteConfig
    {
        public static readonly uint DIFFICULTY_TARGET = 30; // seconds

        public static readonly uint CRYPTONOTE_MAX_BLOCK_NUMBER = 500000000;
        public static readonly uint CRYPTONOTE_MAX_BLOCK_BLOB_SIZE = 500000000;
        public static readonly uint CRYPTONOTE_MAX_TX_SIZE = 1000000000;
        public static readonly ulong CRYPTONOTE_PUBLIC_ADDRESS_BASE58_PREFIX = 3914525;
        public static readonly uint CRYPTONOTE_MINED_MONEY_UNLOCK_WINDOW = 40;
        public static readonly ulong CRYPTONOTE_BLOCK_FUTURE_TIME_LIMIT = 60 * 60 * 2;
        public static readonly ulong CRYPTONOTE_BLOCK_FUTURE_TIME_LIMIT_V3 = 3 * DIFFICULTY_TARGET;
        public static readonly ulong CRYPTONOTE_BLOCK_FUTURE_TIME_LIMIT_V4 = 6 * DIFFICULTY_TARGET;

        public static readonly uint BLOCKCHAIN_TIMESTAMP_CHECK_WINDOW = 60;
        public static readonly uint BLOCKCHAIN_TIMESTAMP_CHECK_WINDOW_V3 = 11;

        // MONEY_SUPPLY - total number coins to be generated
        public static readonly ulong MONEY_SUPPLY = 100000000000000;
        public static readonly uint ZAWY_DIFFICULTY_BLOCK_INDEX = 187000;
        public static readonly uint ZAWY_DIFFICULTY_V2 = 0;
        public static readonly byte ZAWY_DIFFICULTY_DIFFICULTY_BLOCK_VERSION = 3;

        public static readonly ulong LWMA_2_DIFFICULTY_BLOCK_INDEX = 620000;
        public static readonly ulong LWMA_2_DIFFICULTY_BLOCK_INDEX_V2 = 700000;
        public static readonly ulong LWMA_2_DIFFICULTY_BLOCK_INDEX_V3 = 800000;

        public static readonly ulong LWMA_3_DIFFICULTY_BLOCK_INDEX = 1000000;

        public static readonly uint EMISSION_SPEED_FACTOR = 25;
        //C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
        //static_assert(EMISSION_SPEED_FACTOR <= 8 * sizeof(ulong), "Bad EMISSION_SPEED_FACTOR");

        /* Premine amount */
        public static readonly ulong GENESIS_BLOCK_REWARD = 0;

        /* How to generate a premine:

        * Compile your code

        * Run zedwallet, ignore that it can't connect to the daemon, and generate an
          address. Save this and the keys somewhere safe.

        * Launch the daemon with these arguments:
        --print-genesis-tx --genesis-block-reward-address <premine wallet address>

        For example:
        TurtleCoind --print-genesis-tx --genesis-block-reward-address TRTLv2Fyavy8CXG8BPEbNeCHFZ1fuDCYCZ3vW5H5LXN4K2M2MHUpTENip9bbavpHvvPwb4NDkBWrNgURAd5DB38FHXWZyoBh4wW

        * Take the hash printed, and replace it with the hash below in GENESIS_COINBASE_TX_HEX

        * Recompile, setup your seed nodes, and start mining

        * You should see your premine appear in the previously generated wallet.

        */
        public const string GENESIS_COINBASE_TX_HEX = "010a01ff000188f3b501029b2e4c0281c0b02e7c53291a94d1d0cbff8883f8024f5142ee494ffbbd088071210142694232c5b04151d9e4c27d31ec7a68ea568b19488cfcb422659a07a0e44dd5";
        //C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
        //static_assert(sizeof(GENESIS_COINBASE_TX_HEX)/sizeof(*GENESIS_COINBASE_TX_HEX) != 1, "GENESIS_COINBASE_TX_HEX must not be empty.");

        /* This is the unix timestamp of the first "mined" block (technically block 2, not the genesis block)
           You can get this value by doing "print_block 2" in TurtleCoind. It is used to know what timestamp
           to import from when the block height cannot be found in the node or the node is offline. */
        public static readonly ulong GENESIS_BLOCK_TIMESTAMP = 1512800692;

        public static readonly uint CRYPTONOTE_REWARD_BLOCKS_WINDOW = 100;
        public static readonly uint CRYPTONOTE_BLOCK_GRANTED_FULL_REWARD_ZONE = 100000; //size of block (bytes) after which reward for block calculated using block size
        public static readonly uint CRYPTONOTE_BLOCK_GRANTED_FULL_REWARD_ZONE_V2 = 20000;
        public static readonly uint CRYPTONOTE_BLOCK_GRANTED_FULL_REWARD_ZONE_V1 = 10000;
        public static readonly uint CRYPTONOTE_BLOCK_GRANTED_FULL_REWARD_ZONE_CURRENT = CRYPTONOTE_BLOCK_GRANTED_FULL_REWARD_ZONE;
        public static readonly uint CRYPTONOTE_COINBASE_BLOB_RESERVED_SIZE = 600;

        public static readonly uint CRYPTONOTE_DISPLAY_DECIMAL_POINT = 2;

        public static readonly ulong MINIMUM_FEE = 10;

        /* This section defines our minimum and maximum mixin counts required for transactions */
        public static readonly ulong MINIMUM_MIXIN_V1 = 0;
        public static readonly ulong MAXIMUM_MIXIN_V1 = 100;

        public static readonly ulong MINIMUM_MIXIN_V2 = 7;
        public static readonly ulong MAXIMUM_MIXIN_V2 = 7;

        public static readonly ulong MINIMUM_MIXIN_V3 = 3;
        public static readonly ulong MAXIMUM_MIXIN_V3 = 3;

        /* The heights to activate the mixin limits at */
        public static readonly uint MIXIN_LIMITS_V1_HEIGHT = 440000;
        public static readonly uint MIXIN_LIMITS_V2_HEIGHT = 620000;
        public static readonly uint MIXIN_LIMITS_V3_HEIGHT = 800000;

        /* The mixin to use by default with zedwallet and turtle-service */
        /* DEFAULT_MIXIN_V0 is the mixin used before MIXIN_LIMITS_V1_HEIGHT is started */
        public static readonly ulong DEFAULT_MIXIN_V0 = 3;
        public static readonly ulong DEFAULT_MIXIN_V1 = MAXIMUM_MIXIN_V1;
        public static readonly ulong DEFAULT_MIXIN_V2 = MAXIMUM_MIXIN_V2;
        public static readonly ulong DEFAULT_MIXIN_V3 = MAXIMUM_MIXIN_V3;

        public static readonly ulong DEFAULT_DUST_THRESHOLD = 10;
        public static readonly ulong DEFAULT_DUST_THRESHOLD_V2 = 0;

        public static readonly uint DUST_THRESHOLD_V2_HEIGHT = MIXIN_LIMITS_V2_HEIGHT;
        public static readonly uint FUSION_DUST_THRESHOLD_HEIGHT_V2 = 800000;
        public static readonly uint EXPECTED_NUMBER_OF_BLOCKS_PER_DAY = 24 * 60 * 60 / DIFFICULTY_TARGET;

        public static readonly uint DIFFICULTY_WINDOW = 17;
        public static readonly uint DIFFICULTY_WINDOW_V1 = 2880;
        public static readonly uint DIFFICULTY_WINDOW_V2 = 2880;
        public static readonly ulong DIFFICULTY_WINDOW_V3 = 60;
        public static readonly ulong DIFFICULTY_BLOCKS_COUNT_V3 = DIFFICULTY_WINDOW_V3 + 1;

        public static readonly uint DIFFICULTY_CUT = 0; // timestamps to cut after sorting
        public static readonly uint DIFFICULTY_CUT_V1 = 60;
        public static readonly uint DIFFICULTY_CUT_V2 = 60;
        public static readonly uint DIFFICULTY_LAG = 0; // !!!
        public static readonly uint DIFFICULTY_LAG_V1 = 15;
        public static readonly uint DIFFICULTY_LAG_V2 = 15;
        //C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
        //static_assert(2 * DIFFICULTY_CUT <= DIFFICULTY_WINDOW - 2, "Bad DIFFICULTY_WINDOW or DIFFICULTY_CUT");

        public static readonly uint MAX_BLOCK_SIZE_INITIAL = 100000;
        public static readonly ulong MAX_BLOCK_SIZE_GROWTH_SPEED_NUMERATOR = 100 * 1024;
        public static readonly ulong MAX_BLOCK_SIZE_GROWTH_SPEED_DENOMINATOR = 365 * 24 * 60 * 60 / DIFFICULTY_TARGET;
        public static readonly ulong MAX_EXTRA_SIZE = 140000;

        public static readonly ulong CRYPTONOTE_LOCKED_TX_ALLOWED_DELTA_BLOCKS = 1;
        public static readonly ulong CRYPTONOTE_LOCKED_TX_ALLOWED_DELTA_SECONDS = DIFFICULTY_TARGET * CRYPTONOTE_LOCKED_TX_ALLOWED_DELTA_BLOCKS;

        public static readonly ulong CRYPTONOTE_MEMPOOL_TX_LIVETIME = 60 * 60 * 24; //seconds, one day
        public static readonly ulong CRYPTONOTE_MEMPOOL_TX_FROM_ALT_BLOCK_LIVETIME = 60 * 60 * 24 * 7; //seconds, one week
        public static readonly ulong CRYPTONOTE_NUMBER_OF_PERIODS_TO_FORGET_TX_DELETED_FROM_POOL = 7; // CRYPTONOTE_NUMBER_OF_PERIODS_TO_FORGET_TX_DELETED_FROM_POOL * CRYPTONOTE_MEMPOOL_TX_LIVETIME = time to forget tx

        public static readonly uint FUSION_TX_MAX_SIZE = CRYPTONOTE_BLOCK_GRANTED_FULL_REWARD_ZONE_CURRENT * 30 / 100;
        public static readonly uint FUSION_TX_MIN_INPUT_COUNT = 12;
        public static readonly uint FUSION_TX_MIN_IN_OUT_COUNT_RATIO = 4;

        public static readonly uint KEY_IMAGE_CHECKING_BLOCK_INDEX = 0;

        public static readonly uint UPGRADE_HEIGHT_V2 = 1;
        public static readonly uint UPGRADE_HEIGHT_V3 = 2;
        public static readonly uint UPGRADE_HEIGHT_V4 = 350000; // Upgrade height for CN-Lite Variant 1 switch.
        public static readonly uint UPGRADE_HEIGHT_CURRENT = UPGRADE_HEIGHT_V4;

        public static readonly uint UPGRADE_VOTING_THRESHOLD = 90; // percent
        public static readonly uint UPGRADE_VOTING_WINDOW = EXPECTED_NUMBER_OF_BLOCKS_PER_DAY; // blocks
        public static readonly uint UPGRADE_WINDOW = EXPECTED_NUMBER_OF_BLOCKS_PER_DAY; // blocks

        //C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
        //static_assert(0 < UPGRADE_VOTING_THRESHOLD && UPGRADE_VOTING_THRESHOLD <= 100, "Bad UPGRADE_VOTING_THRESHOLD");
        //C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
        //static_assert(UPGRADE_VOTING_WINDOW > 1, "Bad UPGRADE_VOTING_WINDOW");

        /* Block heights we are going to have hard forks at */
        public static readonly ulong[] FORK_HEIGHTS = {
            187000,     // 0
            350000,
            440000,
            620000,
            700000,
            800000,
            1000000,
            1200000,
            1400000,
            1600000,
            1800000,
            2000000     // 11
        };

        /* MAKE SURE TO UPDATE THIS VALUE WITH EVERY MAJOR RELEASE BEFORE A FORK */
        public static readonly uint SOFTWARE_SUPPORTED_FORK_INDEX = 6;

        //TODO: Need to make sure this is correct. Original:
        //const uint64_t FORK_HEIGHTS_SIZE = sizeof(FORK_HEIGHTS) / sizeof(*FORK_HEIGHTS);
        public static readonly int FORK_HEIGHTS_SIZE = FORK_HEIGHTS.Length;

        /* The index in the FORK_HEIGHTS array that this version of the software will
           support. For example, if CURRENT_FORK_INDEX is 3, this version of the
           software will support the fork at 600,000 blocks.

           This will default to zero if the FORK_HEIGHTS array is empty, so you don't
           need to change it manually. */
        //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
        public static readonly uint CURRENT_FORK_INDEX = FORK_HEIGHTS_SIZE == 0 ? 0 : SOFTWARE_SUPPORTED_FORK_INDEX;

        //C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
        //static_assert(CURRENT_FORK_INDEX >= 0, "CURRENT FORK INDEX must be >= 0");
        /* Make sure CURRENT_FORK_INDEX is a valid index, unless FORK_HEIGHTS is empty */
        //C++ TO C# CONVERTER TODO TASK: There is no equivalent in C# to 'static_assert':
        //static_assert(FORK_HEIGHTS_SIZE == 0 || CURRENT_FORK_INDEX < FORK_HEIGHTS_SIZE, "CURRENT_FORK_INDEX out of range of FORK_HEIGHTS!");

        public const string CRYPTONOTE_BLOCKS_FILENAME = "blocks.bin";
        public const string CRYPTONOTE_BLOCKINDEXES_FILENAME = "blockindexes.bin";
        public const string CRYPTONOTE_POOLDATA_FILENAME = "poolstate.bin";
        public const string P2P_NET_DATA_FILENAME = "p2pstate.bin";
        public const string MINER_CONFIG_FILE_NAME = "miner_conf.json";


        public const string CRYPTONOTE_NAME = "TurtleCoin";

        public static readonly byte TRANSACTION_VERSION_1 = 1;
        public static readonly byte TRANSACTION_VERSION_2 = 2;
        public static readonly byte CURRENT_TRANSACTION_VERSION = TRANSACTION_VERSION_1;

        public static readonly byte BLOCK_MAJOR_VERSION_1 = 1;
        public static readonly byte BLOCK_MAJOR_VERSION_2 = 2;
        public static readonly byte BLOCK_MAJOR_VERSION_3 = 3;
        public static readonly byte BLOCK_MAJOR_VERSION_4 = 4;

        public static readonly byte BLOCK_MINOR_VERSION_0 = 0;
        public static readonly byte BLOCK_MINOR_VERSION_1 = 1;

        public static readonly uint BLOCKS_IDS_SYNCHRONIZING_DEFAULT_COUNT = 10000; //by default, blocks ids count in synchronizing
        public static readonly uint BLOCKS_SYNCHRONIZING_DEFAULT_COUNT = 100; //by default, blocks count in blocks downloading
        public static readonly uint COMMAND_RPC_GET_BLOCKS_FAST_MAX_COUNT = 1000;

        public static readonly int P2P_DEFAULT_PORT = 11897;
        public static readonly int RPC_DEFAULT_PORT = 11898;
        public static readonly int SERVICE_DEFAULT_PORT = 8070;

        public static readonly uint P2P_LOCAL_WHITE_PEERLIST_LIMIT = 1000;
        public static readonly uint P2P_LOCAL_GRAY_PEERLIST_LIMIT = 5000;

        // P2P Network Configuration Section - This defines our current P2P network version
        // and the minimum version for communication between nodes
        public static readonly byte P2P_CURRENT_VERSION = 4;
        public static readonly byte P2P_MINIMUM_VERSION = 2;
        // This defines the number of versions ahead we must see peers before we start displaying
        // warning messages that we need to upgrade our software.
        public static readonly byte P2P_UPGRADE_WINDOW = 2;

        public static readonly uint P2P_CONNECTION_MAX_WRITE_BUFFER_SIZE = 32 * 1024 * 1024; // 32 MB
        public static readonly uint P2P_DEFAULT_CONNECTIONS_COUNT = 8;
        public static readonly uint P2P_DEFAULT_WHITELIST_CONNECTIONS_PERCENT = 70;
        public static readonly uint P2P_DEFAULT_HANDSHAKE_INTERVAL = 60; // seconds
        public static readonly uint P2P_DEFAULT_PACKET_MAX_SIZE = 50000000; // 50000000 bytes maximum packet size
        public static readonly uint P2P_DEFAULT_PEERS_IN_HANDSHAKE = 250;
        public static readonly uint P2P_DEFAULT_CONNECTION_TIMEOUT = 5000; // 5 seconds
        public static readonly uint P2P_DEFAULT_PING_CONNECTION_TIMEOUT = 2000; // 2 seconds
        public static readonly ulong P2P_DEFAULT_INVOKE_TIMEOUT = 60 * 2 * 1000; // 2 minutes
        public static readonly uint P2P_DEFAULT_HANDSHAKE_INVOKE_TIMEOUT = 5000; // 5 seconds
        public const string P2P_STAT_TRUSTED_PUB_KEY = "";

        public static readonly ulong DATABASE_WRITE_BUFFER_MB_DEFAULT_SIZE = 256;
        public static readonly ulong DATABASE_READ_BUFFER_MB_DEFAULT_SIZE = 10;
        public static readonly uint DATABASE_DEFAULT_MAX_OPEN_FILES = 100;
        public static readonly ushort DATABASE_DEFAULT_BACKGROUND_THREADS_COUNT = 2;

        public const string LATEST_VERSION_URL = "http://latest.turtlecoin.lol";
        public static readonly string LICENSE_URL = "https://github.com/turtlecoin/turtlecoin/blob/master/LICENSE";

        //TODO: Figure this out
        //internal boost::uuids.uuid CRYPTONOTE_NETWORK = new boost::uuids.uuid({0xb5, 0x0c, 0x4a, 0x6c, 0xcf, 0x52, 0x57, 0x41, 0x65, 0xf9, 0x91, 0xa4, 0xb6, 0xc1, 0x43, 0xe9});

        public static readonly string[] SEED_NODES = {
            "206.189.142.142:11897",    // rock
            "145.239.88.119:11999",     // cision
            "142.44.242.106:11897",     // tom
            "165.227.252.132:11897"     //iburnmycd
        };
    }
}