using EmarsysDueDateCalculator.Builder.BugFix;

namespace EmarsysDueDateCalculator.Builder.Interfaces
{
    public interface IDedicatedTimeHolder
    {
        IDedicatedTimeHolder WithDedicatedTimeInHours(int hours);
        IDedicatedTimeHolder WithDedicatedTimeInDays(int days);
        IDedicatedTimeHolder WithDedicatedTimeInWeeks(int weeks);
        IBugFixBuilder AddNoMoreTime();
    }
}