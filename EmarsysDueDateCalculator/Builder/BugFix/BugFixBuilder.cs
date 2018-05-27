using System;
using EmarsysDueDateCalculator.Builder.Exceptions;
using EmarsysDueDateCalculator.Builder.Interfaces;
using EmarsysDueDateCalculator.Models.Issue;

namespace EmarsysDueDateCalculator.Builder.BugFix
{
    public class BugFixBuilder : 
        IIssueBuilder, ISubmitDateHolder, IDedicatedTimeHolder
    {
        private const int WorkingHours = 8;
        private const int WorkingDays = 5;

        private DateTime _submitDate;
        private int _dedicatedTimeInHours;

        public IDedicatedTimeHolder WithSubmitDateInUtc(DateTime timestamp)
        {
            if (timestamp.Hour < 9 || timestamp.Hour > 17) //TODO: refactor to a private method
            {
                throw new OutOfWorkingHoursException();
            }

            return new BugFixBuilder
            { 
                _submitDate = timestamp
            };
        }

        public IDedicatedTimeHolder WithSubmitDateInLocalTime(DateTime timestamp)
        {
            return new BugFixBuilder
            {
                _submitDate = timestamp.ToUniversalTime()
            };
        }

        public IIssueBuilder WithDedicatedTimeInHours(int hours)
        {
            return new BugFixBuilder
            {
                _submitDate = _submitDate,
                _dedicatedTimeInHours = _dedicatedTimeInHours + hours
            };
        }

        public IIssueBuilder WithDedicatedTimeInDays(int days)
        {
            return new BugFixBuilder
            {
                _submitDate = _submitDate,
                _dedicatedTimeInHours = _dedicatedTimeInHours + (days * WorkingHours)
            };
        }

        public IIssueBuilder WithDedicatedTimeInWeeks(int weeks)
        {
            return new BugFixBuilder
            {
                _submitDate = _submitDate,
                _dedicatedTimeInHours = _dedicatedTimeInHours + (weeks* WorkingHours * WorkingDays)
            };
        }
        public IIssue Build()
        {
            return new Models.Issue.BugFix(_submitDate, _dedicatedTimeInHours);
        }
    }
}