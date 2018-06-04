using System;

namespace Cure.Http.OAuth.Abstractions
{
    public interface ISystemClock
    {
        DateTime Now { get; }

        DateTimeOffset NowOffset { get; }
    }
}