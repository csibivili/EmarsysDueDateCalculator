using System;
using EmarsysDueDateCalculator.Builder.Exceptions;

namespace EmarsysDueDateCalculator.Models.WorkTimeValidator
{
    public class DefaultWorkTimeValidator : IWorkTimeValidator
    {
        public int WorkStartsAt { get; } = 9;
        public int WorkEndsAt { get; } = 17;
        public void CheckIfOutOfWorkingHours(DateTime timestamp)
        {
            if (IsOutOfWorkingHours(timestamp))
            {
                throw new OutOfWorkingHoursException();
            }
        }

        public bool IsOutOfWorkingHours(DateTime timestamp)
        {
            return timestamp.Hour < WorkStartsAt
                   || timestamp.Hour >= WorkEndsAt
                   || timestamp.DayOfWeek == DayOfWeek.Saturday
                   || timestamp.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}