using System.Collections.Generic;

namespace SimpleBlockchain
{
    public interface IHashCalculator
    {
        IReadOnlyCollection<byte> CalculateHash(BlockCreationMetadata blockHeader, ITransaction transaction);
    }
}