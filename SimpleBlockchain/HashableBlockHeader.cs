using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleBlockchain
{
    public class HashableBlockHeader : IHashable
    {
        public HashableBlockHeader(
            int blockNumber, 
            DateTimeOffset created, 
            IReadOnlyCollection<byte> previousBlockHash)
        {
            BlockNumber = blockNumber;
            Created = created;
            PreviousBlockHash = previousBlockHash;
        }

        public HashableBlockHeader(BlockHeader blockHeader, BlockHeader previousBlockHeader)
        {
            BlockNumber = blockHeader.BlockNumber;
            Created = blockHeader.Created;
            PreviousBlockHash = previousBlockHeader.Hash;
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