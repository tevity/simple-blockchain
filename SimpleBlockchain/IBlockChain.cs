namespace SimpleBlockchain
{
    public interface IBlockChain
    {
        void AddBlock(IHashable transaction);
    }
}