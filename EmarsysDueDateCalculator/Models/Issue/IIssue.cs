using System;

namespace EmarsysDueDateCalculator.Models.Issue
{
    public interface IIssue
    {
        DateTime GetSubmitDateInUtc();
        int GetDedicatedTimeInHours();
    }
}