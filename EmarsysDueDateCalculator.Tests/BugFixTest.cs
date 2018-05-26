using System;
using EmarsysDueDateCalculator.Models.Issue;
using Xunit;

namespace EmarsysDueDateCalculator.Tests
{
    public class BugFixTest
    {
        [Fact]
        public void SetSubmitDateInUtc_DuringWorkingHours_OK()
        {
            var bugFix = new BugFix();

            var timestamp = new DateTime(2018,5,28,10,0,0);

            bugFix.SetSubmitDateInUtc(timestamp);

            var expected = timestamp;
            var actual = bugFix.GetSubmitDateInUtc();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SetSubmitDateInUtc_BeforeWorkingHours_ThrowsException()
        {
            var bugFix = new BugFix();

            var timestamp = new DateTime(2018, 5, 28, 8, 0, 0);

            Assert.Throws<OutOfWorkingHoursException>(() => bugFix.SetSubmitDateInUtc(timestamp));
        }
    }
}
