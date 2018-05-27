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
                return GetNextWorkDayOf(submitDate) + offset;
            }

            return submitDate + offset;
        }

        private static DateTime GetNextWorkDayOf(DateTime submitDate)
        {
            switch (submitDate.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    var threeDaysLater = submitDate + new TimeSpan(ActualHoursPerDay * 3, 0, 0);
                    return new DateTime(threeDaysLater.Year,
                        threeDaysLater.Month,
                        threeDaysLater.Day,
                        9,0,0);
                case DayOfWeek.Saturday:
                    var twoDaysLater = submitDate + new TimeSpan(ActualHoursPerDay * 2, 0, 0);
                    return new DateTime(twoDaysLater.Year,
                        twoDaysLater.Month,
                        twoDaysLater.Day,
                        9,0,0);
                default:
                    var oneDayLater = submitDate + new TimeSpan(ActualHoursPerDay, 0, 0);
                    return new DateTime(oneDayLater.Year,
                        oneDayLater.Month,
                        oneDayLater.Day,
                        9,0,0);
            }
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