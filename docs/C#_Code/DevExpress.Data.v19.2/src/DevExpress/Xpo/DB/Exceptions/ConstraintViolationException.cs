namespace DevExpress.Xpo.DB.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ConstraintViolationException : SqlExecutionErrorException
    {
        protected ConstraintViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ConstraintViolationException(string sql, string parameters, Exception innerException) : base(sql, parameters, innerException)
        {
        }
    }
}

