namespace Dapper
{
    using System;
    using System.Data;

    internal class BasicWrappedReader : IWrappedDataReader, IDataReader, IDisposable, IDataRecord
    {
        private IDataReader _reader;
        private IDbCommand _cmd;

        public BasicWrappedReader(IDbCommand cmd, IDataReader reader)
        {
            this._cmd = cmd;
            this._reader = reader;
        }

        void IDataReader.Close()
        {
            this._reader.Close();
        }

        DataTable IDataReader.GetSchemaTable() => 
            this._reader.GetSchemaTable();

        bool IDataReader.NextResult() => 
            this._reader.NextResult();

        bool IDataReader.Read() => 
            this._reader.Read();

        bool IDataRecord.GetBoolean(int i) => 
            this._reader.GetBoolean(i);

        byte IDataRecord.GetByte(int i) => 
            this._reader.GetByte(i);

        long IDataRecord.GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) => 
            this._reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);

        char IDataRecord.GetChar(int i) => 
            this._reader.GetChar(i);

        long IDataRecord.GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) => 
            this._reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);

        IDataReader IDataRecord.GetData(int i) => 
            this._reader.GetData(i);

        string IDataRecord.GetDataTypeName(int i) => 
            this._reader.GetDataTypeName(i);

        DateTime IDataRecord.GetDateTime(int i) => 
            this._reader.GetDateTime(i);

        decimal IDataRecord.GetDecimal(int i) => 
            this._reader.GetDecimal(i);

        double IDataRecord.GetDouble(int i) => 
            this._reader.GetDouble(i);

        Type IDataRecord.GetFieldType(int i) => 
            this._reader.GetFieldType(i);

        float IDataRecord.GetFloat(int i) => 
            this._reader.GetFloat(i);

        Guid IDataRecord.GetGuid(int i) => 
            this._reader.GetGuid(i);

        short IDataRecord.GetInt16(int i) => 
            this._reader.GetInt16(i);

        int IDataRecord.GetInt32(int i) => 
            this._reader.GetInt32(i);

        long IDataRecord.GetInt64(int i) => 
            this._reader.GetInt64(i);

        string IDataRecord.GetName(int i) => 
            this._reader.GetName(i);

        int IDataRecord.GetOrdinal(string name) => 
            this._reader.GetOrdinal(name);

        string IDataRecord.GetString(int i) => 
            this._reader.GetString(i);

        object IDataRecord.GetValue(int i) => 
            this._reader.GetValue(i);

        int IDataRecord.GetValues(object[] values) => 
            this._reader.GetValues(values);

        bool IDataRecord.IsDBNull(int i) => 
            this._reader.IsDBNull(i);

        void IDisposable.Dispose()
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

        IDataReader IWrappedDataReader.Reader =>
            this._reader;

        IDbCommand IWrappedDataReader.Command =>
            this._cmd;

        int IDataReader.Depth =>
            this._reader.Depth;

        bool IDataReader.IsClosed =>
            this._reader.IsClosed;

        int IDataReader.RecordsAffected =>
            this._reader.RecordsAffected;

        int IDataRecord.FieldCount =>
            this._reader.FieldCount;

        object IDataRecord.this[string name] =>
            this._reader[name];

        object IDataRecord.this[int i] =>
            this._reader[i];
    }
}

