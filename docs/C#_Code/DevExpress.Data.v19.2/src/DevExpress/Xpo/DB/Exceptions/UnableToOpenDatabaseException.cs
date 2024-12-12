namespace DevExpress.Xpo.DB.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class UnableToOpenDatabaseException : Exception
    {
        protected UnableToOpenDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UnableToOpenDatabaseException(string connectionString, Exception innerException) : base(DbRes.GetString("ConnectionProvider_UnableToOpenDatabase", objArray1), innerException)
        {
            object[] objArray1 = new object[] { connectionString, innerException };
        }
    }
}

