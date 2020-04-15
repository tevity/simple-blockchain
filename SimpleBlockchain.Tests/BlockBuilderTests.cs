using System;
using NSubstitute;
using Shouldly;
using Xunit;

namespace SimpleBlockchain.Tests
{
    public class BlockBuilderTests
    {
        private static BlockBuilder CreateTarget(
            IDateTimeProvider dateTimeProvider = null,
            IHashCalculator hashCalculator = null)
        {
            dateTimeProvider ??= Substitute.For<IDateTimeProvider>();
            hashCalculator ??= Substitute.For<IHashCalculator>();

            return new BlockBuilder(dateTimeProvider, hashCalculator);
        }

        [Fact]
        public void BuildGenesisBlock_GivenAnyTransaction_CreatedBlockHasBlockNumber0()
        {
            var target = CreateTarget();

            var transaction = Substitute.For<IHashable>();
            var result = target.BuildGenesisBlock(transaction);

            result.Header.BlockNumber.ShouldBe(0);
        }

        [Fact]
        public void BuildGenesisBlock_GivenAnyTransaction_CreatedBlockHasCreatedAsNow()
        {
            var dateTimeProvider = Substitute.For<IDateTimeProvider>();
            var now = new DateTimeOffset(new DateTime(2000, 1, 1));
            dateTimeProvider.Now.Returns(now);
            var target = CreateTarget(dateTimeProvider);

            var transaction = Substitute.For<IHashable>();
            var result = target.BuildGenesisBlock(transaction);

            result.Header.Created.ShouldBe(now);
        }

        [Fact]
        public void BuildGenesisBlock_GivenAnyTransaction_CreatedBlockHasHashFromHashCalculator()
        {
            var hashCalculator = Substitute.For<IHashCalculator>();
            var calculatedHash = new byte[0];
            hashCalculator.CalculateHash(null, null).ReturnsForAnyArgs(calculatedHash);
            var target = CreateTarget(hashCalculator: hashCalculator);

            var transaction = Substitute.For<IHashable>();
            var result = target.BuildGenesisBlock(transaction);

            result.Header.Hash.ShouldBe(calculatedHash);
        }

        [Fact]
        public void BuildGenesisBlock_GivenAnyTransaction_CreatedBlockHasGivenTransaction()
        {
            var target = CreateTarget();

            var transaction = Substitute.For<IHashable>();
            var result = target.BuildGenesisBlock(transaction);

            result.Transaction.ShouldBe(transaction);
        }

        [Fact]
        public void BuildGenesisBlock_HashCalculator_ShouldBeGivenEmptyHashableBlockHeader()
        {
            var hashCalculator = Substitute.For<IHashCalculator>();
            var dateTimeProvider = Substitute.For<IDateTimeProvider>();
            var now = new DateTimeOffset(new DateTime(2000, 1, 1));
            dateTimeProvider.Now.Returns(now);
            var target = CreateTarget(dateTimeProvider, hashCalculator);

            var transaction = Substitute.For<IHashable>();
            target.BuildGenesisBlock(transaction);

            var expectedBlockHeader = new HashableBlockHeader(0, now, new byte[0]);
            hashCalculator.Received().CalculateHash(expectedBlockHeader, transaction);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        public void BuildBlock_GivenPreviousBlock_CreatedBlockHasIncrementedBlockNumberFromPreviousBlock(
            int previousBlockNumber,
            int expectedNewBlockNumber)
        {
            var target = CreateTarget();

            var metadata = new HashableBlockHeader(previousBlockNumber, new DateTimeOffset(), new byte[0]);
            var previousBlockHeader = new BlockHeader(metadata, new byte[0]);
            var transaction = Substitute.For<IHashable>();
            var result = target.BuildBlock(previousBlockHeader, transaction);

            result.Header.BlockNumber.ShouldBe(expectedNewBlockNumber);
        }

        [Fact]
        public void BuildBlock_GivenAnyPreviousBlock_CreatedBlockHasCreatedAsNow()
        {
            var dateTimeProvider = Substitute.For<IDateTimeProvider>();
            var now = new DateTimeOffset(new DateTime(2000, 1, 1));
            dateTimeProvider.Now.Returns(now);
            var target = CreateTarget(dateTimeProvider);

            var metadata = new HashableBlockHeader(0, new DateTimeOffset(), new byte[0]);
            var previousBlockHeader = new BlockHeader(metadata, new byte[0]);
            var transaction = Substitute.For<IHashable>();
            var result = target.BuildBlock(previousBlockHeader, transaction);

            result.Header.Created.ShouldBe(now);
        }

        [Fact]
        public void BuildBlock_GivenAnyPreviousBlock_CreatedBlockHasHashFromHashCalculator()
        {
            var hashCalculator = Substitute.For<IHashCalculator>();
            var calculatedHash = new byte[0];
            hashCalculator.CalculateHash(null, null).ReturnsForAnyArgs(calculatedHash);
            var target = CreateTarget(hashCalculator: hashCalculator);

            var metadata = new HashableBlockHeader(0, new DateTimeOffset(), new byte[0]);
            var previousBlockHeader = new BlockHeader(metadata, new byte[0]);
            var transaction = Substitute.For<IHashable>();
            var result = target.BuildBlock(previousBlockHeader, transaction);

            result.Header.Hash.ShouldBe(calculatedHash);
        }

        [Fact]
        public void BuildBlock_GivenAnyTransaction_CreatedBlockHasGivenTransaction()
        {
            var target = CreateTarget();

            var metadata = new HashableBlockHeader(0, new DateTimeOffset(), new byte[0]);
            var previousBlockHeader = new BlockHeader(metadata, new byte[0]);
            var transaction = Substitute.For<IHashable>();
            var result = target.BuildBlock(previousBlockHeader, transaction);

            result.Transaction.ShouldBe(transaction);
        }

        [Fact]
        public void BuildBlock_GivenAnyPreviousBlock_PreviousBlockHasCreatedBlockAsNextBlock()
        {
            var target = CreateTarget();

            var metadata = new HashableBlockHeader(0, new DateTimeOffset(), new byte[0]);
            var previousBlockHeader = new BlockHeader(metadata, new byte[0]);
            var transaction = Substitute.For<IHashable>();
            var createdBlock = target.BuildBlock(previousBlockHeader, transaction);

            previousBlockHeader.NextBlock.ShouldBe(createdBlock);
        }

        [Fact]
        public void BuildBlock_HashCalculator_ShouldBeGivenNewHashableBlockHeader()
        {
            var hashCalculator = Substitute.For<IHashCalculator>();
            var dateTimeProvider = Substitute.For<IDateTimeProvider>();
            var now = new DateTimeOffset(new DateTime(2000, 1, 1));
            dateTimeProvider.Now.Returns(now);
            var target = CreateTarget(dateTimeProvider, hashCalculator);

            var previousBlockHash = new[] {new byte()};
            var metadata = new HashableBlockHeader(0, new DateTimeOffset(), new byte[0]);
            var previousBlockHeader = new BlockHeader(metadata, previousBlockHash);
            var transaction = Substitute.For<IHashable>();
            target.BuildBlock(previousBlockHeader, transaction);

            var expectedBlockHeader = new HashableBlockHeader(1, now, previousBlockHash);
            hashCalculator.Received().CalculateHash(expectedBlockHeader, transaction);
        }
    }
}