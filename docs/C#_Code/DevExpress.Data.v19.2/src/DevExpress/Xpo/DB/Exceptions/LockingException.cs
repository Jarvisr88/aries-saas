namespace DevExpress.Xpo.DB.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class LockingException : Exception
    {
        public LockingException() : base(DbRes.GetString("ConnectionProvider_Locking"))
        {
        }

        protected LockingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

