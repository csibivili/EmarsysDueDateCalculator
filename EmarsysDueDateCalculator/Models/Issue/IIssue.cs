using System;

namespace EmarsysDueDateCalculator.Models.Issue
{
    public interface IIssue
    {
        DateTime GetSubmitDateInUtc();
        int GetDedicatedTimeInHours();
        void SetSubmitDateInUtc(DateTime timestamp);
        void SetSubmitDateInLocalTime(DateTime timestamp);
        void SetDedicatedTimeInHours(int hours);
        void SetDedicatedTimeInDays(int days);
        void SetDeicatedTimeInWeeks(int weeks);
    }
}