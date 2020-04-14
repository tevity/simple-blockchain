using System.Collections.Generic;

namespace SimpleBlockchain
{
    public interface ITransaction
    {
        IReadOnlyCollection<byte> GetHashBytes();
    }
}