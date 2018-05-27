using System;

namespace EmarsysDueDateCalculator.Builder.Interfaces
{
    public interface ISubmitDateHolder
    {
        IDedicatedTimeHolder WithSubmitDateInUtc(DateTime timestamp);
        IDedicatedTimeHolder WithSubmitDateInUtcWithTimeZone(DateTime timestamp, TimeZoneInfo timeZoneInfo);
    }
}