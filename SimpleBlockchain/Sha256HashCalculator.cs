using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace SimpleBlockchain
{
    public class Sha256HashCalculator : IHashCalculator
    {
        public IReadOnlyCollection<byte> CalculateHash(IHashable blockHeader, IHashable transaction)
        {
            using var sha256 = SHA256.Create();
            var metadataHash = blockHeader.GetHashBytes();
            var transactionHash = transaction.GetHashBytes();
            var allHashBytes = metadataHash.Concat(transactionHash);
            return sha256.ComputeHash(allHashBytes.ToArray());
        }
    }
}