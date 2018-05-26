using System;

namespace EmarsysDueDateCalculator.Models.Issue
{
    public class BugFix : IIssue
    {
        private const int WorkingHours = 8;
        private const int WorkingDays = 5;

        private DateTime _submitDate;
        private int _dedicatedTimeInHours;
        public DateTime GetSubmitDateInUtc()
        {
            return _submitDate;
        }

        public int GetDedicatedTimeInHours()
        {
            return _dedicatedTimeInHours;
        }

        public void SetSubmitDateInUtc(DateTime timestamp)
        {
            if (timestamp.Hour < 9 || timestamp.Hour > 17) //TODO: refactor to a private method
            {
                throw new OutOfWorkingHoursException();
            }
            _submitDate = timestamp;
        }

        public void SetSubmitDateInLocalTime(DateTime timestamp)
        {
            _submitDate = timestamp.ToUniversalTime();
        }

        public void SetDedicatedTimeInHours(int hours)
        {
            _dedicatedTimeInHours = hours;
        }

        public void SetDedicatedTimeInDays(int days)
        {
            _dedicatedTimeInHours = days * WorkingHours;
        }

        public void SetDeicatedTimeInWeeks(int weeks)
        {
            _dedicatedTimeInHours = weeks * WorkingDays * WorkingHours;
        }
    }
}