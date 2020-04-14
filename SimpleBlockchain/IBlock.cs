using System;
using System.Collections.Generic;

namespace SimpleBlockchain
{
    public interface IBlock
    {
        int BlockNumber { get; }
        IReadOnlyCollection<byte> Hash { get; }
        DateTimeOffset Created { get; }
        IReadOnlyCollection<byte> PreviousBlockHash { get; }
        IBlock NextBlock { get; set; }
    }
}