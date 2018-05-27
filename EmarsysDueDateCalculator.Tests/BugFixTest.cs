using System;
using EmarsysDueDateCalculator.Builder.BugFix;
using EmarsysDueDateCalculator.Builder.Exceptions;
using Xunit;

namespace EmarsysDueDateCalculator.Tests
{
    public class BugFixTest
    {
        [Fact]
        public void SetSubmitDateInUtc_DuringWorkingHours_OK()
        {
            var timestamp = new DateTime(2018, 5, 28, 10, 0, 0);

            var bugFix = BugFixBuilder.BugFix()
                .WithSubmitDateInUtc(timestamp)
                .WithDedicatedTimeInHours(4)
                .AddNoMoreTime()
                .Build();

            var expected = timestamp;
            var actual = bugFix.GetSubmitDateInUtc();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SetSubmitDateInUtc_BeforeWorkingHours_ThrowsException()
        {
            var timestamp = new DateTime(2018, 5, 28, 8, 59, 59);

            Assert.Throws<OutOfWorkingHoursException>(() => BugFixBuilder.BugFix()
                .WithSubmitDateInUtc(timestamp)
                .WithDedicatedTimeInHours(4)
                .AddNoMoreTime()
                .Build());
        }

        [Fact]
        public void SetSubmitDateInUtc_AfterWorkingHours_ThrowsException()
        {
            var timestamp = new DateTime(2018, 5, 28, 17, 0, 1);

            Assert.Throws<OutOfWorkingHoursException>(() => BugFixBuilder.BugFix()
                .WithSubmitDateInUtc(timestamp)
                .WithDedicatedTimeInHours(4)
                .AddNoMoreTime()
                .Build());
        }

        [Fact]
        public void SetSubmitDateInUtc_OnWeekend_ThrowsException()
        {
            var timestamp = new DateTime(2018, 5, 27, 10, 33, 1);

            Assert.Throws<OutOfWorkingHoursException>(() => BugFixBuilder.BugFix()
                .WithSubmitDateInUtc(timestamp)
                .WithDedicatedTimeInHours(4)
                .AddNoMoreTime()
                .Build());
        }

        [Fact]
        public void SetSubmitDateInLocalTimeWithTimeZone_DuringWorkingHours_OK()
        {
            var timestamp = new DateTime(2018, 5, 28, 16, 0, 0);
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            var bugFix = BugFixBuilder.BugFix()
                .WithSubmitDateInLocalTimeWithTimeZone(timestamp, easternZone)
                .WithDedicatedTimeInHours(4)
                .AddNoMoreTime()
                .Build();

            var expected = timestamp - easternZone.GetUtcOffset(timestamp);
            var actual = bugFix.GetSubmitDateInUtc();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SetDedicatedTime_OneWeekTwoDaysThreeHours_59()
        {
            var timestamp = new DateTime(2018, 5, 28, 16, 0, 0);

            var bugFix = BugFixBuilder.BugFix()
                .WithSubmitDateInUtc(timestamp)
                .WithDedicatedTimeInHours(3)
                .WithDedicatedTimeInDays(2)
                .WithDedicatedTimeInWeeks(1)
                .AddNoMoreTime()
                .Build();

            int expected = 59;
            int actual = bugFix.GetDedicatedTimeInHours();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SetDedicatedTime_TwoWeeksThreeDaysFourHours_108()
        {
            var timestamp = new DateTime(2018, 5, 28, 16, 0, 0);

            var bugFix = BugFixBuilder.BugFix()
                .WithSubmitDateInUtc(timestamp)
                .WithDedicatedTimeInHours(4)
                .WithDedicatedTimeInDays(3)
                .WithDedicatedTimeInWeeks(2)
                .AddNoMoreTime()
                .Build();

            int expected = 108;
            int actual = bugFix.GetDedicatedTimeInHours();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SetDedicatedTime_TimeIsZero_ThrowsException()
        {
            var timestamp = new DateTime(2018, 5, 28, 16, 0, 0);

            Assert.Throws<ZeroTimeDedicatedException>(() => BugFixBuilder.BugFix()
                .WithSubmitDateInUtc(timestamp)
                .WithDedicatedTimeInHours(0)
                .WithDedicatedTimeInDays(0)
                .WithDedicatedTimeInWeeks(0)
                .AddNoMoreTime()
                .Build());
        }
    }
}
