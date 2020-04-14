namespace SimpleBlockchain
{
    public interface IBlock
    {
        BlockHeader Header { get; }
        IHashable Transaction { get; }
    }
}