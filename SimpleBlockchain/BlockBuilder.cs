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

        public IBlock BuildBlock<TTransaction>(TTransaction transaction, IBlock previousBlock) where TTransaction : ITransaction
        {
            var creationMetadata = new BlockCreationMetadata
            {
                BlockNumber = previousBlock.BlockNumber + 1,
                Created = _dateTimeProvider.Now,
                PreviousBlockHash = previousBlock.Hash,
            };
            var hash = _hashCalculator.CalculateHash(creationMetadata, transaction);
            var newBlock = new Block<TTransaction>(transaction, creationMetadata, hash);
            previousBlock.NextBlock = newBlock;
            return newBlock;
        }
    }
}