using System;

namespace EmarsysDueDateCalculator.Models.WorkTimeValidator
{
    public interface IWorkTimeValidator
    {
        int WorkStartsAt { get; }
        int WorkEndsAt { get; }
        void CheckIfOutOfWorkingHours(DateTime timestamp);
        bool IsOutOfWorkingHours(DateTime timestamp);
    }
}