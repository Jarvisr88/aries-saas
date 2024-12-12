namespace DMEWorks.Data
{
    using System;

    public class DeadlockException : Exception
    {
        public DeadlockException()
        {
        }

        public DeadlockException(string message) : base(message)
        {
        }

        public DeadlockException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

