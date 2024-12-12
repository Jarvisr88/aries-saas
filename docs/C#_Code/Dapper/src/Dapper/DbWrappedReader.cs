namespace Dapper
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.Common;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class DbWrappedReader : DbDataReader, IWrappedDataReader, IDataReader, IDisposable, IDataRecord
    {
        private DbDataReader _reader;
        private IDbCommand _cmd;

        public DbWrappedReader(IDbCommand cmd, DbDataReader reader)
        {
            this._cmd = cmd;
            this._reader = reader;
        }

        public override void Close()
        {
            this._reader.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._reader.Close();
                this._reader.Dispose();
                this._reader = DisposedReader.Instance;
                if (this._cmd == null)
                {
                    IDbCommand local1 = this._cmd;
                }
                else
                {
                    this._cmd.Dispose();
                }
                this._cmd = null;
            }
        }

        public override bool GetBoolean(int i) => 
            this._reader.GetBoolean(i);

        public override byte GetByte(int i) => 
            this._reader.GetByte(i);

        public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) => 
            this._reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);

        public override char GetChar(int i) => 
            this._reader.GetChar(i);

        public override long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) => 
            this._reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);

        public override string GetDataTypeName(int i) => 
            this._reader.GetDataTypeName(i);

        public override DateTime GetDateTime(int i) => 
            this._reader.GetDateTime(i);

        protected override DbDataReader GetDbDataReader(int ordinal)
        {
            DbDataReader data = ((IDataRecord) this._reader).GetData(ordinal) as DbDataReader;
            if (data != null)
            {
                return data;
            }
            DbDataReader local1 = data;
            throw new NotSupportedException();
        }

        public override decimal GetDecimal(int i) => 
            this._reader.GetDecimal(i);

        public override double GetDouble(int i) => 
            this._reader.GetDouble(i);

        public override IEnumerator GetEnumerator() => 
            this._reader.GetEnumerator();

        public override Type GetFieldType(int i) => 
            this._reader.GetFieldType(i);

        public override T GetFieldValue<T>(int ordinal) => 
            this._reader.GetFieldValue<T>(ordinal);

        public override Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken) => 
            this._reader.GetFieldValueAsync<T>(ordinal, cancellationToken);

        public override float GetFloat(int i) => 
            this._reader.GetFloat(i);

        public override Guid GetGuid(int i) => 
            this._reader.GetGuid(i);

        public override short GetInt16(int i) => 
            this._reader.GetInt16(i);

        public override int GetInt32(int i) => 
            this._reader.GetInt32(i);

        public override long GetInt64(int i) => 
            this._reader.GetInt64(i);

        public override string GetName(int i) => 
            this._reader.GetName(i);

        public override int GetOrdinal(string name) => 
            this._reader.GetOrdinal(name);

        public override Type GetProviderSpecificFieldType(int ordinal) => 
            this._reader.GetProviderSpecificFieldType(ordinal);

        public override object GetProviderSpecificValue(int ordinal) => 
            this._reader.GetProviderSpecificValue(ordinal);

        public override int GetProviderSpecificValues(object[] values) => 
            this._reader.GetProviderSpecificValues(values);

        public override DataTable GetSchemaTable() => 
            this._reader.GetSchemaTable();

        public override Stream GetStream(int ordinal) => 
            this._reader.GetStream(ordinal);

        public override string GetString(int i) => 
            this._reader.GetString(i);

        public override TextReader GetTextReader(int ordinal) => 
            this._reader.GetTextReader(ordinal);

        public override object GetValue(int i) => 
            this._reader.GetValue(i);

        public override int GetValues(object[] values) => 
            this._reader.GetValues(values);

        public override object InitializeLifetimeService() => 
            this._reader.InitializeLifetimeService();

        public override bool IsDBNull(int i) => 
            this._reader.IsDBNull(i);

        public override Task<bool> IsDBNullAsync(int ordinal, CancellationToken cancellationToken) => 
            this._reader.IsDBNullAsync(ordinal, cancellationToken);

        public override bool NextResult() => 
            this._reader.NextResult();

        public override Task<bool> NextResultAsync(CancellationToken cancellationToken) => 
            this._reader.NextResultAsync(cancellationToken);

        public override bool Read() => 
            this._reader.Read();

        public override Task<bool> ReadAsync(CancellationToken cancellationToken) => 
            this._reader.ReadAsync(cancellationToken);

        IDataReader IWrappedDataReader.Reader =>
            this._reader;

        IDbCommand IWrappedDataReader.Command =>
            this._cmd;

        public override bool HasRows =>
            this._reader.HasRows;

        public override int Depth =>
            this._reader.Depth;

        public override bool IsClosed =>
            this._reader.IsClosed;

        public override int RecordsAffected =>
            this._reader.RecordsAffected;

        public override int FieldCount =>
            this._reader.FieldCount;

        public override object this[string name] =>
            this._reader[name];

        public override object this[int i] =>
            this._reader[i];

        public override int VisibleFieldCount =>
            this._reader.VisibleFieldCount;
    }
}

