using System;
using EmarsysDueDateCalculator.Builder.BugFix;
using EmarsysDueDateCalculator.Models.Issue;
using EmarsysDueDateCalculator.Services.DueDateCalculator;
using Xunit;

namespace EmarsysDueDateCalculator.Tests
{
    public class BugFixDueDateCalculatorTest
    {
        [Fact]
        public void CaculateDueDate_9AmSubmit4HoursDedicatedTime_1Pm()
        {
            IIssueDueDateCalculator<BugFix> dueDateCalculator = new BugFixDueDateCalculator();

            var bugfix = BugFixBuilder.BugFix()
                .WithSubmitDateInUtc(new DateTime(2018, 5, 28, 9, 0, 0))
                .WithDedicatedTimeInHours(4)
                .AddNoMoreTime()
                .Build();

            var expected = new DateTime(2018,5,28,13,0,0);
            var actual = dueDateCalculator.CalculateDueDate(bugfix);
        }
    }
}