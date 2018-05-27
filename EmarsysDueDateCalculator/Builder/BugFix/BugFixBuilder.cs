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
        private const int WorkStartsAt = 9;
        private const int WorkEndsAt = 17;

        private DateTime _submitDate;
        private int _dedicatedTimeInHours;

        public IDedicatedTimeHolder WithSubmitDateInUtc(DateTime timestamp)
        {
            CheckIfOutOfWorkingHours(timestamp);

            return new BugFixBuilder
            { 
                _submitDate = timestamp
            };
        }

        public IDedicatedTimeHolder WithSubmitDateInUtcAndOffset(DateTime timestamp, TimeZoneInfo timeZoneInfo)
        {
            var timestampInUtc = TimeZoneInfo.ConvertTimeToUtc(timestamp, timeZoneInfo);

            CheckIfOutOfWorkingHours(timestampInUtc);

            return new BugFixBuilder
            {
                _submitDate = timestampInUtc
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

        private static void CheckIfOutOfWorkingHours(DateTime timestamp)
        {
            if (IsOutOfWorkingHours(timestamp))
            {
                throw new OutOfWorkingHoursException();
            }
        }

        private static bool IsOutOfWorkingHours(DateTime timestamp)
        {
            return timestamp.Hour < WorkStartsAt
                   || timestamp.Hour >= WorkEndsAt
                   || timestamp.DayOfWeek == DayOfWeek.Saturday
                   || timestamp.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}