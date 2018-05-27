using System;
using EmarsysDueDateCalculator.Models.Issue;

namespace EmarsysDueDateCalculator.Services.DueDateCalculator
{
    public interface IIssueDueDateCalculator<in T> where T : IIssue
    {
        DateTime CalculateDueDate(T issue);
    }
}