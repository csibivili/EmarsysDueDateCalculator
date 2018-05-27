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
            //TODO: register in container
            _dueDateCalculator = new BugFixDueDateCalculator();
        }

        [Fact]
        public void CaculateDueDate_9AmSubmit4HoursDedicatedTime_SameDay1Pm()
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

        [Fact]
        public void CalculateDueDate_8PmSubmit4HoursDedicatedTime_NextWorkingDay1Pm()
        {

            var bugfix = BugFixBuilder.BugFix()
                .WithSubmitDateInLocalTimeWithTimeZone(new DateTime(2018, 5, 28, 16, 0, 0),
                    TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
                .WithDedicatedTimeInHours(4)
                .AddNoMoreTime()
                .Build();

            var expected = new DateTime(2018, 5, 29, 13, 0, 0);
            var actual = _dueDateCalculator.CalculateDueDate(bugfix);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateDueDate_9AmSubmit1DayDedicatedTime_NextWorkingDay9Am()
        {
            var bugfix = BugFixBuilder.BugFix()
                .WithSubmitDateInUtc(new DateTime(2018, 5, 28, 9, 0, 0))
                .WithDedicatedTimeInDays(1)
                .AddNoMoreTime()
                .Build();

            var expected = new DateTime(2018,5,29,9,0,0);
            var actual = _dueDateCalculator.CalculateDueDate(bugfix);

            Assert.Equal(expected,actual);
        }

        [Fact]
        public void CalculateDueDate_9AmSubmit1WeekDedicatedTime_NextWeekSameDay9Am()
        {
            var bugfix = BugFixBuilder.BugFix()
                .WithSubmitDateInUtc(new DateTime(2018, 5, 28, 9, 0, 0))
                .WithDedicatedTimeInWeeks(1)
                .AddNoMoreTime()
                .Build();

            var expected = new DateTime(2018,6,4,9,0,0);
            var actual = _dueDateCalculator.CalculateDueDate(bugfix);

            Assert.Equal(expected,actual);
        }
    }
}