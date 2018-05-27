﻿using System;
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

            return submitDate + offset;
        }
    }
}