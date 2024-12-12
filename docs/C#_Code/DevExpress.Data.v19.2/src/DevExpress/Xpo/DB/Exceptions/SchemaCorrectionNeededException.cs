namespace DevExpress.Xpo.DB.Exceptions
{
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;

    [Serializable]
    public class SchemaCorrectionNeededException : Exception
    {
        private string sql;

        public SchemaCorrectionNeededException(Exception innerException) : this(innerException.Message, innerException)
        {
        }

        public SchemaCorrectionNeededException(string sql) : this(sql, null)
        {
        }

        protected SchemaCorrectionNeededException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public SchemaCorrectionNeededException(string sql, Exception innerException) : base(DbRes.GetString("ConnectionProvider_SchemaCorrectionNeeded", objArray1), innerException)
        {
            object[] objArray1 = new object[] { sql };
            this.sql = sql;
        }

        [Obsolete("Use Message instead.", false), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public string Sql =>
            this.sql;
    }
}

