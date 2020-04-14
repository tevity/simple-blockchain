using System;

namespace SimpleBlockchain
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}