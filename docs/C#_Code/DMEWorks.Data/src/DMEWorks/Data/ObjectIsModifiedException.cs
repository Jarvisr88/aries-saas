namespace DMEWorks.Data
{
    using System;

    public class ObjectIsModifiedException : Exception
    {
        public ObjectIsModifiedException()
        {
        }

        public ObjectIsModifiedException(string message) : base(message)
        {
        }

        public ObjectIsModifiedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

