﻿using System;
using EmarsysDueDateCalculator.Builder.Exceptions;
using EmarsysDueDateCalculator.Builder.Interfaces;
using EmarsysDueDateCalculator.Models.Issue;
using EmarsysDueDateCalculator.Models.WorkTimeValidator;

namespace EmarsysDueDateCalculator.Builder.BugFix
{
    public class BugFixBuilder : 
        IIssueBuilder, ISubmitDateHolder, IDedicatedTimeHolder
    {
        private const int WorkingHours = 8;
        private const int WorkingDays = 5;

        private DateTime _submitDate;
        private int _dedicatedTimeInHours;

        private readonly IWorkTimeValidator _workTimeValidator;

        public BugFixBuilder()
        {

        }
        public BugFixBuilder(IWorkTimeValidator workTimeValidator)
        {
            _workTimeValidator = workTimeValidator ?? throw new ArgumentNullException();
        }

        public IDedicatedTimeHolder WithSubmitDateInUtc(DateTime timestamp)
        {
            _workTimeValidator.CheckIfOutOfWorkingHours(timestamp);

            return new BugFixBuilder
            { 
                _submitDate = timestamp
            };
        }

        public IDedicatedTimeHolder WithSubmitDateInUtcWithTimeZone(DateTime timestamp, TimeZoneInfo timeZoneInfo)
        {
            var timestampInUtc = TimeZoneInfo.ConvertTimeToUtc(timestamp, timeZoneInfo);

            _workTimeValidator.CheckIfOutOfWorkingHours(timestampInUtc);

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
    }
}