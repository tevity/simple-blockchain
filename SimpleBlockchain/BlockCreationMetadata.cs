using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleBlockchain
{
    public class BlockCreationMetadata
    {
        public int BlockNumber { get; set; }
        public DateTimeOffset Created { get; set; }
        public IReadOnlyCollection<byte> PreviousBlockHash { get; set; }

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