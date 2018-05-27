using System;
using EmarsysDueDateCalculator.Builder.BugFix;
using EmarsysDueDateCalculator.Models.Issue;
using EmarsysDueDateCalculator.Models.WorkTimeValidator;

namespace EmarsysDueDateCalculator.Services.DueDateCalculator
{
    public class BugFixDueDateCalculator : IIssueDueDateCalculator<BugFix>
    {
        private const int ActualHoursPerDay = 24;

        private readonly IWorkTimeValidator _workTimeValidator;

        public BugFixDueDateCalculator()
        {
            //TODO: register validator in container
            _workTimeValidator = new DefaultWorkTimeValidator();
        }

        public DateTime CalculateDueDate(BugFix issue)
        {
            var submitDate = issue.GetSubmitDateInUtc();
            var dedicatedTimeInHours = issue.GetDedicatedTimeInHours();

            var offset = DedicatedTimeInHoursToRealTimeOffset(dedicatedTimeInHours);

            if (_workTimeValidator.IsOutOfWorkingHours(submitDate))
            {
                return GetNextWorkMorningOf(submitDate) + offset;
            }

            return submitDate + offset;
        }

        private DateTime GetNextWorkMorningOf(DateTime submitDate)
        {
            DateTime dateOfNextWorkDay;
            int days;

            switch (submitDate.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    days = IsAfterWork(submitDate) ? 3 : 2;
                    break;
                case DayOfWeek.Saturday:
                    days = IsAfterWork(submitDate) ? 2 : 1;
                    break;
                default:
                    days = IsAfterWork(submitDate) ? 1 : 0;
                    break;
            }
            dateOfNextWorkDay = OffsetDateByDays(submitDate, days);
            return SetClockToWorkStart(dateOfNextWorkDay);
        }

        private DateTime SetClockToWorkStart(DateTime timestamp)
        {
            return new DateTime(timestamp.Year,
                timestamp.Month,
                timestamp.Day,
                _workTimeValidator.WorkStartsAt, 0, 0);              
        }

        private static DateTime OffsetDateByDays(DateTime submitDate, int daysOffset)
        {
            return submitDate + new TimeSpan(ActualHoursPerDay * daysOffset, 0, 0);
        }

        private bool IsAfterWork(DateTime submitDate)
        {
            return submitDate.Hour >= _workTimeValidator.WorkEndsAt 
                   && submitDate.Hour < ActualHoursPerDay;
        }

        private static TimeSpan DedicatedTimeInHoursToRealTimeOffset(int dedicatedTimeInHours)
        {
            const int workHoursPerWeek = BugFixBuilder.WorkingDays * BugFixBuilder.WorkingHours;
            const int actualHoursPerWeek = ActualHoursPerDay * 7;

            var numberOfWeekOffset = 
                dedicatedTimeInHours / workHoursPerWeek;
            var numberOfDayOffset = 
                dedicatedTimeInHours % workHoursPerWeek / BugFixBuilder.WorkingHours;
            var numberOfHourOffset = 
                dedicatedTimeInHours % BugFixBuilder.WorkingHours;

            var offsetActualHours = numberOfWeekOffset * actualHoursPerWeek
                                    + numberOfDayOffset * ActualHoursPerDay
                                    + numberOfHourOffset;

            return new TimeSpan(offsetActualHours,0,0);
        }
    }
}