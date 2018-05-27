using System;

namespace EmarsysDueDateCalculator.Models.WorkTimeValidator
{
    public interface IWorkTimeValidator
    {
        void CheckIfOutOfWorkingHours(DateTime timestamp);
    }
}