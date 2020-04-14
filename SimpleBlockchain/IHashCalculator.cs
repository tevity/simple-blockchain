using System.Collections.Generic;

namespace SimpleBlockchain
{
    public interface IHashCalculator
    {
        IReadOnlyCollection<byte> CalculateHash(IHashable blockHeader, IHashable transaction);
    }
}