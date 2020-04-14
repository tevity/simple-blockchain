using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleBlockchain
{
    public class BlockHeaderCreationMetadata : IHashable
    {
        public BlockHeaderCreationMetadata(
            int blockNumber, 
            DateTimeOffset created, 
            IReadOnlyCollection<byte> previousBlockHash)
        {
            BlockNumber = blockNumber;
            Created = created;
            PreviousBlockHash = previousBlockHash;
        }

        public int BlockNumber { get; }

        public DateTimeOffset Created { get; }

        public IReadOnlyCollection<byte> PreviousBlockHash { get; }

        public IReadOnlyCollection<byte> GetHashBytes()
        {
            return new[]
            {
                Encoding.UTF8.GetBytes(BlockNumber.ToString()),
                Encoding.UTF8.GetBytes(Created.ToString()),
                PreviousBlockHash.ToArray()
            }.SelectMany(x => x).ToArray();
        }
    }
}