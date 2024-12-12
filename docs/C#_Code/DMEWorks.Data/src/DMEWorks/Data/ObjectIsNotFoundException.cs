namespace DMEWorks.Data
{
    using System;

    public class ObjectIsNotFoundException : Exception
    {
        public ObjectIsNotFoundException()
        {
        }

        public ObjectIsNotFoundException(string message) : base(message)
        {
        }

        public ObjectIsNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

