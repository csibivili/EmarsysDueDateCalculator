using System;

namespace EmarsysDueDateCalculator.Builder.Interfaces
{
    public interface ISubmitDateHolder
    {
        IDedicatedTimeHolder WithSubmitDateInUtc(DateTime timestamp);
        IDedicatedTimeHolder WithSubmitDateInLocalTimeWithTimeZone(DateTime timestamp, TimeZoneInfo timeZoneInfo);
    }
}