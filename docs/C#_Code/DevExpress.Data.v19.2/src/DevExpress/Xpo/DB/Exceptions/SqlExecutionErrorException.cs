namespace DevExpress.Xpo.DB.Exceptions
{
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;

    [Serializable]
    public class SqlExecutionErrorException : Exception
    {
        private string sql;
        private string parameters;

        protected SqlExecutionErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public SqlExecutionErrorException(string sql, string parameters, Exception innerException) : base(DbRes.GetString("ConnectionProvider_SqlExecutionError", objArray1), innerException)
        {
            object[] objArray1 = new object[] { sql, parameters, innerException };
            this.sql = sql;
            this.parameters = parameters;
        }

        [Obsolete("Use Message instead.", false), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public string Sql =>
            this.sql;

        [Obsolete("Use Message instead.", false), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public string Parameters =>
            this.parameters;
    }
}

