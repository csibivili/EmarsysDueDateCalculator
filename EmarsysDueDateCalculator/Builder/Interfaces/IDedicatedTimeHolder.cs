using EmarsysDueDateCalculator.Builder.BugFix;

namespace EmarsysDueDateCalculator.Builder.Interfaces
{
    public interface IDedicatedTimeHolder
    {
        IIssueBuilder WithDedicatedTimeInHours(int hours);
        IIssueBuilder WithDedicatedTimeInDays(int days);
        IIssueBuilder WithDedicatedTimeInWeeks(int weeks);
    }
}