using System.Collections.Generic;
using System.Linq;

namespace SimpleBlockchain
{
    public class BlockChain : IBlockChain
    {
        private readonly BlockBuilder _blockBuilder;
        private readonly BlockVerifier _verifier;
        private readonly List<IBlock> _blocks;

        public BlockChain(BlockBuilder blockBuilder, BlockVerifier verifier, List<IBlock> blocks = null)
        {
            _blockBuilder = blockBuilder;
            _verifier = verifier;
            _blocks = blocks ?? new List<IBlock>();
        }

        public void AddBlock(IHashable transaction)
        {
            var previousBlock = _blocks.LastOrDefault();
            var previousBlockHeader = previousBlock?.Header ?? BlockHeader.InitialisationHeader;
            var block = _blockBuilder.BuildBlock(previousBlockHeader, transaction);
            _blocks.Add(block);
        }

        public bool Validate()
        {
            IBlock previousBlock = null;
            var valid = true;
            foreach (var block in _blocks)
            {
                valid &= _verifier.IsValid(block, previousBlock);
                previousBlock = block;
            }

            return valid;
        }
    }
}