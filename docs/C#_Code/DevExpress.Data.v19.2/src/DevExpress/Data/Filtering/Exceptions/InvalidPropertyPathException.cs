namespace DevExpress.Data.Filtering.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class InvalidPropertyPathException : Exception
    {
        public InvalidPropertyPathException(string messageText);
        protected InvalidPropertyPathException(SerializationInfo info, StreamingContext context);
    }
}

