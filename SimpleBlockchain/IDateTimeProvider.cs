using System;

namespace SimpleBlockchain
{
    public interface IDateTimeProvider
    {
        DateTimeOffset Now { get; }
    }
}