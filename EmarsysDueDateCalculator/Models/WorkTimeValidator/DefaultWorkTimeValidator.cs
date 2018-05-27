using System;
using EmarsysDueDateCalculator.Builder.Exceptions;

namespace EmarsysDueDateCalculator.Models.WorkTimeValidator
{
    public class DefaultWorkTimeValidator : IWorkTimeValidator
    {
        private const int WorkStartsAt = 9;
        private const int WorkEndsAt = 17;
        public void CheckIfOutOfWorkingHours(DateTime timestamp)
        {
            if (IsOutOfWorkingHours(timestamp))
            {
                throw new OutOfWorkingHoursException();
            }
        }

        private static bool IsOutOfWorkingHours(DateTime timestamp)
        {
            return timestamp.Hour < WorkStartsAt
                   || timestamp.Hour >= WorkEndsAt
                   || timestamp.DayOfWeek == DayOfWeek.Saturday
                   || timestamp.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}