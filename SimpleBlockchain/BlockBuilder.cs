namespace SimpleBlockchain
{
    public class BlockBuilder
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IHashCalculator _hashCalculator;

        public BlockBuilder(IDateTimeProvider dateTimeProvider, IHashCalculator hashCalculator)
        {
            _dateTimeProvider = dateTimeProvider;
            _hashCalculator = hashCalculator;
        }

        public IBlock BuildBlock<TTransaction>(BlockHeader previousBlock, TTransaction transaction) where TTransaction : IHashable
        {
            var creationMetadata = new HashableBlockHeader(
                previousBlock.BlockNumber + 1,
                _dateTimeProvider.Now,
                previousBlock.PreviousBlockHash);
            var hash = _hashCalculator.CalculateHash(creationMetadata, transaction);
            var header = new BlockHeader(creationMetadata, hash);
            var newBlock = new Block(header, transaction);
            previousBlock.NextBlock = newBlock;
            return newBlock;
        }
    }
}