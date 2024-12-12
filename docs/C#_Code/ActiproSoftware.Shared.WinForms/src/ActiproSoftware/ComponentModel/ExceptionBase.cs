namespace ActiproSoftware.ComponentModel
{
    using System;
    using System.Runtime.Serialization;

    public abstract class ExceptionBase : ApplicationException
    {
        public ExceptionBase();
        public ExceptionBase(string message);
        public ExceptionBase(SerializationInfo info, StreamingContext context);
        public ExceptionBase(string message, Exception innerException);
    }
}

