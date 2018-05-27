using System;
using EmarsysDueDateCalculator.Builder.BugFix;
using EmarsysDueDateCalculator.Builder.Exceptions;
using EmarsysDueDateCalculator.Models.Issue;
using EmarsysDueDateCalculator.Models.WorkTimeValidator;
using Xunit;

namespace EmarsysDueDateCalculator.Tests
{
    public class BugFixTest
    {
        private readonly BugFixBuilder _bugFixBuilder;
        public BugFixTest()
        {
            _bugFixBuilder = new BugFixBuilder(new DefaultWorkTimeValidator());
        }

        [Fact]
        public void SetSubmitDateInUtc_DuringWorkingHours_OK()
        {
            var timestamp = new DateTime(2018, 5, 28, 10, 0, 0);

            var bugFix = _bugFixBuilder
                .WithSubmitDateInUtc(timestamp)
                .WithDedicatedTimeInHours(4)
                .Build();

            var expected = timestamp;
            var actual = bugFix.GetSubmitDateInUtc();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SetSubmitDateInUtc_BeforeWorkingHours_ThrowsException()
        {
            var timestamp = new DateTime(2018, 5, 28, 8, 59, 59);

            Assert.Throws<OutOfWorkingHoursException>(() => _bugFixBuilder
                .WithSubmitDateInUtc(timestamp)
                .WithDedicatedTimeInHours(4)
                .Build());
        }

        [Fact]
        public void SetSubmitDateInUtc_AfterWorkingHours_ThrowsException()
        {
            var timestamp = new DateTime(2018, 5, 28, 17, 0, 1);

            Assert.Throws<OutOfWorkingHoursException>(() => _bugFixBuilder
                .WithSubmitDateInUtc(timestamp)
                .WithDedicatedTimeInHours(4)
                .Build());
        }

        [Fact]
        public void SetSubmitDateInUtc_OnWeekend_ThrowsException()
        {
            var timestamp = new DateTime(2018, 5, 27, 10, 33, 1);

            Assert.Throws<OutOfWorkingHoursException>(() => _bugFixBuilder
                .WithSubmitDateInUtc(timestamp)
                .WithDedicatedTimeInHours(4)
                .Build());
        }
    }
}
