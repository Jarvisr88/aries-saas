namespace Devart.Data.MySql
{
    using System;
    using System.Data.Common;
    using System.Runtime.Serialization;

    [Serializable]
    public class MySqlException : DbException, ISerializable
    {
        private readonly int a;
        private readonly string b;
        private readonly string c;

        internal MySqlException(int A_0);
        internal MySqlException(int A_0, Exception A_1);
        internal MySqlException(int A_0, string A_1);
        private MySqlException(SerializationInfo A_0, StreamingContext A_1);
        internal MySqlException(int A_0, string A_1, Exception A_2);
        internal MySqlException(int A_0, string A_1, string A_2);
        public override void GetObjectData(SerializationInfo info, StreamingContext context);
        public override string ToString();

        public override string Message { get; }

        public int Code { get; }

        public string SqlState { get; }
    }
}

