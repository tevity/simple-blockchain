using System;
using System.Collections.Generic;

namespace SimpleBlockchain
{
    public class Block<TTransaction> : IBlock
    {
        public Block(
            TTransaction transaction,
            BlockCreationMetadata creationMetadata,
            IReadOnlyCollection<byte> hash)
        {
            Transaction = transaction;
            BlockNumber = creationMetadata.BlockNumber;
            Created = creationMetadata.Created;
            PreviousBlockHash = creationMetadata.PreviousBlockHash;
            Hash = hash;
        }

        public TTransaction Transaction { get; }
        public int BlockNumber { get; }
        public IReadOnlyCollection<byte> Hash { get; }
        public DateTimeOffset Created { get; }
        public IReadOnlyCollection<byte> PreviousBlockHash { get; }
        public IBlock NextBlock { get; set; }
    }
}