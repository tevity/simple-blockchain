using System.Collections.Generic;
using System.Linq;

namespace SimpleBlockchain
{
    public interface IBlockChain
    {
        
    }

    public class BlockChain : IBlockChain
    {
        private readonly BlockBuilder _blockBuilder;
        private readonly List<IBlock> _blocks;

        public BlockChain(BlockBuilder blockBuilder, List<IBlock> blocks = null)
        {
            _blockBuilder = blockBuilder;
            _blocks = blocks ?? new List<IBlock>();
        }

        public void AddBlock(IHashable transaction)
        {
            var previousBlock = _blocks.LastOrDefault();
            var previousBlockHeader = previousBlock?.Header ?? BlockHeader.InitialisationHeader;
            var block = _blockBuilder.BuildBlock(previousBlockHeader, transaction);
            _blocks.Add(block);
        }
    }
}