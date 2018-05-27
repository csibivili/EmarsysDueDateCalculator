using System;
using EmarsysDueDateCalculator.Models.Issue;
using EmarsysDueDateCalculator.Models.WorkTimeValidator;

namespace EmarsysDueDateCalculator.Services.DueDateCalculator
{
    public class BugFixDueDateCalculator : IIssueDueDateCalculator<BugFix>
    {
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

            var offsetWorkingWeek = dedicatedTimeInHours / 40;
            var offsetWorkingDay = dedicatedTimeInHours / 8;
            var offsetWorkingHour = dedicatedTimeInHours % 8;

            var offsetActualHours = offsetWorkingWeek * 168 
                                    + offsetWorkingDay * 24 
                                    + offsetWorkingHour;
            var offset = new TimeSpan(offsetActualHours,0,0);

            if (_workTimeValidator.IsOutOfWorkingHours(submitDate))
            {
                return new DateTime(submitDate.Year, 
                    submitDate.Month, 
                    submitDate.Day + 1, 
                    9,0,0) + offset;
            }

            return submitDate + offset;
        }
    }
}