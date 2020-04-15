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

        #region Equality

        protected bool Equals(HashableBlockHeader other)
        {
            return BlockNumber == other.BlockNumber 
                   && Created.Equals(other.Created) 
                   && PreviousBlockHash.SequenceEqual(other.PreviousBlockHash);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is HashableBlockHeader other))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BlockNumber, Created, PreviousBlockHash);
        }

        #endregion
    }
}