using System.Linq;

namespace SimpleBlockchain
{
    public class BlockVerifier
    {
        private readonly IHashCalculator _hashCalculator;

        public BlockVerifier(IHashCalculator hashCalculator)
        {
            _hashCalculator = hashCalculator;
        }

        public bool IsValid(IBlock block, IBlock previousBlock)
        {
            var creationMetadata = new HashableBlockHeader(block.Header, previousBlock.Header);
            var calculatedHash = _hashCalculator.CalculateHash(creationMetadata, block.Transaction);
            return calculatedHash.SequenceEqual(block.Header.Hash);
        }
    }
}