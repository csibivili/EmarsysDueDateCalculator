using System;

namespace EmarsysDueDateCalculator.Builder.Interfaces
{
    public interface ISubmitDateHolder
    {
        IDedicatedTimeHolder WithSubmitDateInUtc(DateTime timestamp);
        IDedicatedTimeHolder WithSubmitDateInLocalTime(DateTime timestamp);
    }
}