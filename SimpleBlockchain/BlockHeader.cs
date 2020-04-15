using System;
using System.Collections.Generic;

namespace SimpleBlockchain
{
    public class BlockHeader
    {
        public BlockHeader(
            BlockHeaderCreationMetadata creationMetadata,
            IReadOnlyCollection<byte> hash)
        {
            BlockNumber = creationMetadata.BlockNumber;
            Created = creationMetadata.Created;
            PreviousBlockHash = creationMetadata.PreviousBlockHash;
            Hash = hash;
        }

        public int BlockNumber { get; }
        public IReadOnlyCollection<byte> Hash { get; }
        public DateTimeOffset Created { get; }
        public IReadOnlyCollection<byte> PreviousBlockHash { get; }
        public IBlock NextBlock { get; set; }

        public static readonly BlockHeader InitialisationHeader = new BlockHeader(
            new BlockHeaderCreationMetadata(-1, DateTimeOffset.MinValue, new byte[0]), new byte[0]);
    }
}