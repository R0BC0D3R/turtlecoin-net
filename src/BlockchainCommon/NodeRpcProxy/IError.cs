using CryptoNote.error;

namespace BlockchainCommon.NodeRpcProxy
{
    interface IError
    {
        string Name();
        string Message(int ev);
    }
}