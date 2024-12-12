namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.Collections;
    using System.Data;

    public class MySqlDataReader : DbDataReaderBase, IDisposable
    {
        private MySqlConnection a;
        private Devart.Data.MySql.j b;
        private a8 c;
        private byte[] d;
        private bh[] e;
        private bool f;
        private bool g;
        private int h;
        private bool i;
        private int j;
        private int k;
        private const string l = "SELECT\r\n  TABLE_SCHEMA,\r\n  TABLE_NAME,\r\n  COLUMN_NAME,\r\n  EXTRA\r\nFROM information_schema.COLUMNS\r\nWHERE TABLE_SCHEMA = '{0}' AND\r\n      TABLE_NAME in ({1})  AND\r\n      EXTRA IN ('VIRTUAL', 'PERSISTENT')";

        internal MySqlDataReader(MySqlCommand A_0, Devart.Data.MySql.j A_1, CommandBehavior A_2);
        private bool a();
        private void a(bool A_0);
        internal MySqlType a(int A_0);
        protected void a(string A_0);
        private Hashtable a(SelectTableCollection A_0, string A_1);
        private void a(int A_0, string A_1);
        internal void b();
        private void b(int A_0, string A_1);
        public override void Close();
        internal void d();
        protected override void FillSchemaTable(DataTable dataTable);
        protected override void Finalize();
        public override bool GetBoolean(int i);
        public override byte GetByte(int i);
        public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length);
        public override char GetChar(int i);
        public override long GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length);
        public override string GetDataTypeName(int i);
        public override DateTime GetDateTime(int i);
        public override DateTimeOffset GetDateTimeOffset(int i);
        public override decimal GetDecimal(int i);
        public override double GetDouble(int i);
        public override Type GetFieldType(int i);
        public override float GetFloat(int i);
        public override Guid GetGuid(int i);
        public override short GetInt16(int i);
        public override int GetInt32(int i);
        public override long GetInt64(int i);
        public MySqlBinaryString GetMySqlBinaryString(int i);
        public MySqlBinaryString GetMySqlBinaryString(string name);
        public MySqlBlob GetMySqlBlob(int i);
        public MySqlBlob GetMySqlBlob(string name);
        public MySqlDecimal GetMySqlDecimal(int i);
        public MySqlDecimal GetMySqlDecimal(string name);
        public MySqlGuid GetMySqlGuid(int i);
        public MySqlGuid GetMySqlGuid(string name);
        public MySqlText GetMySqlText(int i);
        public MySqlText GetMySqlText(string name);
        public override string GetName(int i);
        public override Type GetProviderSpecificFieldType(int i);
        public override object GetProviderSpecificValue(int i);
        public override int GetProviderSpecificValues(object[] values);
        [CLSCompliant(false)]
        public sbyte GetSByte(int i);
        [CLSCompliant(false)]
        public sbyte GetSByte(string name);
        public override string GetString(int i);
        public TimeSpan GetTimeSpan(int i);
        public TimeSpan GetTimeSpan(string name);
        [CLSCompliant(false)]
        public ushort GetUInt16(int i);
        [CLSCompliant(false)]
        public ushort GetUInt16(string name);
        [CLSCompliant(false)]
        public uint GetUInt32(int i);
        [CLSCompliant(false)]
        public uint GetUInt32(string name);
        [CLSCompliant(false)]
        public ulong GetUInt64(int i);
        [CLSCompliant(false)]
        public ulong GetUInt64(string name);
        public override object GetValue(int i);
        public override int GetValues(object[] values);
        public override bool IsDBNull(int i);
        public override bool NextResult();
        public override bool Read();
        internal void Reset();
        public void Seek(int position);
        void IDisposable.Dispose();

        public int RecordCount { get; }

        public int CurrentRecord { get; }

        public int CurrentResult { get; set; }

        public int ResultCount { get; }

        public override int RecordsAffected { get; }

        public override bool HasRows { get; }

        public override int FieldCount { get; }

        protected override bool IsValidRow { get; }

        internal ad[] ResultBind { get; }

        public override bool EndOfData { get; }
    }
}

