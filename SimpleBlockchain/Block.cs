namespace SimpleBlockchain
{
    public class Block : IBlock
    {
        public Block(BlockHeader header, IHashable transaction)
        {
            Header = header;
            Transaction = transaction;
        }

        public BlockHeader Header { get; }

        public IHashable Transaction { get; }
    }
}