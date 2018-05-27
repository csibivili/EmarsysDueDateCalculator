using System;
using EmarsysDueDateCalculator.Models.Issue;

namespace EmarsysDueDateCalculator.Services.DueDateCalculator
{
    public class BugFixDueDateCalculator : IIssueDueDateCalculator<BugFix>
    {
        public DateTime CalculateDueDate(BugFix issue)
        {
            var submitDate = issue.GetSubmitDateInUtc();
            var dedicatedTimeInHours = issue.GetDedicatedTimeInHours();

            var offset = new TimeSpan(dedicatedTimeInHours,0,0);

            var calculated = submitDate + offset;

            if (calculated.Hour >= 17 || calculated.Day > submitDate.Day)
            {
                return new DateTime(calculated.Year, 
                    calculated.Month, 
                    submitDate.Day + 1,
                    9 + offset.Hours,
                    0 + calculated.Minute,
                    0 + calculated.Second);
            }
            else
            {
                return submitDate + offset;
            }
        }
    }
}