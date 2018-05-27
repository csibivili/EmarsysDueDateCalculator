using System;
using EmarsysDueDateCalculator.Builder.BugFix;
using EmarsysDueDateCalculator.Models.Issue;
using EmarsysDueDateCalculator.Services.DueDateCalculator;
using Xunit;

namespace EmarsysDueDateCalculator.Tests
{
    public class BugFixDueDateCalculatorTest
    {
        private readonly IIssueDueDateCalculator<BugFix> _dueDateCalculator;

        public BugFixDueDateCalculatorTest()
        {
            _dueDateCalculator = new BugFixDueDateCalculator();
        }

        [Fact]
        public void CaculateDueDate_9AmSubmit4HoursDedicatedTime_1Pm()
        {
            var bugfix = BugFixBuilder.BugFix()
                .WithSubmitDateInUtc(new DateTime(2018, 5, 28, 9, 0, 0))
                .WithDedicatedTimeInHours(4)
                .AddNoMoreTime()
                .Build();

            var expected = new DateTime(2018,5,28,13,0,0);
            var actual = _dueDateCalculator.CalculateDueDate(bugfix);

            Assert.Equal(expected,actual);
        }
    }
}