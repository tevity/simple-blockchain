namespace SimpleBlockchain
{
    public class Block : IBlock
    {
        public BlockHeader Header { get; }
        public IHashable Transaction { get; }

        public Block(BlockHeader header, IHashable transaction)
        {
            Header = header;
            Transaction = transaction;
        }
    }
}