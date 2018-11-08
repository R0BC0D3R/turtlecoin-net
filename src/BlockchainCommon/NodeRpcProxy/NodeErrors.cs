// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information


using BlockchainCommon.NodeRpcProxy;
namespace CryptoNote
{
    namespace error
    {
        // custom error conditions enum type:
        public enum NodeErrorCodes
        {
            NOT_INITIALIZED = 1,
            ALREADY_INITIALIZED,
            NETWORK_ERROR,
            NODE_BUSY,
            INTERNAL_NODE_ERROR,
            REQUEST_ERROR,
            CONNECT_ERROR
        }

        // custom category:
        public class NodeErrorCategory : IError
        {
            public static NodeErrorCategory INSTANCE = new NodeErrorCategory();

            public string Name()
            {
                return "NodeErrorCategory";
            }

            //C++ TO C# CONVERTER WARNING: Throw clauses are not available in C#:
            //ORIGINAL LINE: virtual std::error_condition default_error_condition(int ev) const throw() override
            //public override std::error_condition default_error_condition(int ev) const
            //{
            // return std::error_condition(ev, this);
            //}


            public string Message(int ev)
            {
                switch (ev)
                {
                    case (int)NodeErrorCodes.NOT_INITIALIZED:
                        return "Object was not initialized";
                    case (int)NodeErrorCodes.ALREADY_INITIALIZED:
                        return "Object has been already initialized";
                    case (int)NodeErrorCodes.NETWORK_ERROR:
                        return "Network error";
                    case (int)NodeErrorCodes.NODE_BUSY:
                        return "Node is busy";
                    case (int)NodeErrorCodes.INTERNAL_NODE_ERROR:
                        return "Internal node error";
                    case (int)NodeErrorCodes.REQUEST_ERROR:
                        return "Error in request parameters";
                    case (int)NodeErrorCodes.CONNECT_ERROR:
                        return "Can't connect to daemon";
                    default:
                        return "Unknown error";
                }
            }

            private NodeErrorCategory()
            {
            }
        }
    }
}