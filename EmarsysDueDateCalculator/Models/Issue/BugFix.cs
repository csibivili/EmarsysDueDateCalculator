using System;

namespace EmarsysDueDateCalculator.Models.Issue
{
    public class BugFix : IIssue
    {
        private readonly DateTime _submitDate;
        private readonly int _dedicatedTimeInHours;

        public BugFix(DateTime submitDate, int dedicatedTimeInHours)
        {
            _submitDate = submitDate;
            _dedicatedTimeInHours = dedicatedTimeInHours;
        }
        public DateTime GetSubmitDateInUtc()
        {
            return _submitDate;
        }

        public int GetDedicatedTimeInHours()
        {
            return _dedicatedTimeInHours;
        }
    }
}