using System.Collections.Generic;

namespace SimpleBlockchain
{
    public interface IHashable
    {
        IReadOnlyCollection<byte> GetHashBytes();
    }
}