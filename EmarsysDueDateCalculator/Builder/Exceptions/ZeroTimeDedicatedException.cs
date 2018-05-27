using System;
using System.Runtime.Serialization;

namespace EmarsysDueDateCalculator.Builder.Exceptions
{
    [Serializable]
    public class ZeroTimeDedicatedException : Exception
    {
        public ZeroTimeDedicatedException()
        {
        }

        public ZeroTimeDedicatedException(string message) : base(message)
        {
        }

        public ZeroTimeDedicatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ZeroTimeDedicatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}