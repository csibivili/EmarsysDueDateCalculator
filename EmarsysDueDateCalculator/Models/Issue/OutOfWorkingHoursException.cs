using System;
using System.Runtime.Serialization;

namespace EmarsysDueDateCalculator.Models.Issue
{
    [Serializable]
    public class OutOfWorkingHoursException : Exception
    {
        public OutOfWorkingHoursException()
        {
        }

        public OutOfWorkingHoursException(string message) : base(message)
        {
        }

        public OutOfWorkingHoursException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OutOfWorkingHoursException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}