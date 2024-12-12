namespace DMEWorks.Forms
{
    using System;

    public class UserNotifyException : Exception
    {
        public UserNotifyException()
        {
        }

        public UserNotifyException(string message) : base(message)
        {
        }

        public UserNotifyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

