using System;
using EmarsysDueDateCalculator.Models.Issue;

namespace EmarsysDueDateCalculator.Services.DueDateCalculator
{
    public interface IIssueDueDateCalculator
    {
        DateTime CalculateDueDate(IIssue issue);
    }
}