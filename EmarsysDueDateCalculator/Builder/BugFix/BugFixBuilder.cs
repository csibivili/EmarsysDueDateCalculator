using EmarsysDueDateCalculator.Builder.Exceptions;
using EmarsysDueDateCalculator.Builder.Interfaces;
using EmarsysDueDateCalculator.Models.WorkTimeValidator;
using System;

namespace EmarsysDueDateCalculator.Builder.BugFix
{
    public class BugFixBuilder : 
        IBugFixBuilder, ISubmitDateHolder, IDedicatedTimeHolder
    {
        public const int WorkingHours = 8;
        public const int WorkingDays = 5;

        private DateTime _submitDate;
        private int _dedicatedTimeInHours;

        private readonly IWorkTimeValidator _workTimeValidator;

        private BugFixBuilder()
        {

        }
        private BugFixBuilder(IWorkTimeValidator workTimeValidator)
        {
            _workTimeValidator = workTimeValidator ?? throw new ArgumentNullException();
        }

        //TODO: workTimeValidator implementation should come from DI container (singleton)
        public static ISubmitDateHolder BugFix() => new BugFixBuilder(new DefaultWorkTimeValidator());

        public IDedicatedTimeHolder WithSubmitDateInUtc(DateTime timestamp)
        {
            _workTimeValidator.CheckIfOutOfWorkingHours(timestamp);

            return new BugFixBuilder
            { 
                _submitDate = timestamp
            };
        }

        public IDedicatedTimeHolder WithSubmitDateInLocalTimeWithTimeZone(DateTime timestamp, TimeZoneInfo timeZoneInfo)
        {
            _workTimeValidator.CheckIfOutOfWorkingHours(timestamp);

            var timestampInUtc = TimeZoneInfo.ConvertTimeToUtc(timestamp, timeZoneInfo);

            return new BugFixBuilder
            {
                _submitDate = timestampInUtc
            };
        }

        public IDedicatedTimeHolder WithDedicatedTimeInHours(int hours)
        {
            return new BugFixBuilder
            {
                _submitDate = _submitDate,
                _dedicatedTimeInHours = _dedicatedTimeInHours + hours
            };
        }

        public IDedicatedTimeHolder WithDedicatedTimeInDays(int days)
        {
            return new BugFixBuilder
            {
                _submitDate = _submitDate,
                _dedicatedTimeInHours = _dedicatedTimeInHours + (days * WorkingHours)
            };
        }

        public IDedicatedTimeHolder WithDedicatedTimeInWeeks(int weeks)
        {
            return new BugFixBuilder
            {
                _submitDate = _submitDate,
                _dedicatedTimeInHours = _dedicatedTimeInHours + (weeks* WorkingHours * WorkingDays)
            };
        }

        public IBugFixBuilder AddNoMoreTime()
        {
            if (_dedicatedTimeInHours == 0)
            {
                throw new ZeroTimeDedicatedException();
            }
            return this;
        }

        public Models.Issue.BugFix Build()
        {
            return new Models.Issue.BugFix(_submitDate, _dedicatedTimeInHours);
        }
    }
}