namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.Common;
    using System.Globalization;
    using System.Reflection;

    public abstract class DbDataReaderBase : DbDataReader
    {
        private System.Data.CommandBehavior a;
        private Hashtable b;
        protected bool closed;
        protected DataTable schemaTable;

        protected DbDataReaderBase(System.Data.CommandBehavior behavior)
        {
            this.a = behavior;
        }

        internal void a()
        {
            this.Close();
        }

        protected void AssertReaderHasColumns()
        {
            if (this.FieldCount <= 0)
            {
                throw new InvalidOperationException(g.a("DataReaderNoData"));
            }
        }

        protected void AssertReaderHasData()
        {
            if (!this.IsValidRow)
            {
                throw new InvalidOperationException(g.a("DataReaderNoData"));
            }
        }

        protected void AssertReaderIsOpen(string methodName)
        {
            if (this.closed)
            {
                throw new InvalidOperationException(g.a("DataReaderClosed", methodName));
            }
        }

        public override void Close()
        {
            this.b = null;
            this.schemaTable = null;
            this.closed = true;
            GC.SuppressFinalize(this);
        }

        protected static DataTable CreateSchemaTable(int columnCount)
        {
            DataTable table = new DataTable("SchemaTable") {
                Locale = CultureInfo.InvariantCulture,
                MinimumCapacity = columnCount
            };
            DataColumn column = new DataColumn(SchemaTableColumn.ColumnOrdinal, typeof(int));
            DataColumn column9 = new DataColumn(SchemaTableColumn.IsLong, typeof(bool));
            column.DefaultValue = 0;
            column9.DefaultValue = false;
            DataColumnCollection columns = table.Columns;
            columns.Add(new DataColumn(SchemaTableColumn.ColumnName, typeof(string)));
            columns.Add(column);
            columns.Add(new DataColumn(SchemaTableColumn.ColumnSize, typeof(int)));
            columns.Add(new DataColumn(SchemaTableColumn.NumericPrecision, typeof(short)));
            columns.Add(new DataColumn(SchemaTableColumn.NumericScale, typeof(short)));
            columns.Add(new DataColumn(SchemaTableColumn.DataType, typeof(Type)));
            columns.Add(new DataColumn(SchemaTableOptionalColumn.ProviderSpecificDataType, typeof(Type)));
            columns.Add(new DataColumn(SchemaTableColumn.ProviderType, typeof(int)));
            columns.Add(column9);
            columns.Add(new DataColumn(SchemaTableColumn.AllowDBNull, typeof(bool)));
            columns.Add(new DataColumn(SchemaTableColumn.IsAliased, typeof(bool)));
            columns.Add(new DataColumn(SchemaTableColumn.IsExpression, typeof(bool)));
            columns.Add(new DataColumn(SchemaTableColumn.IsKey, typeof(bool)));
            columns.Add(new DataColumn(SchemaTableColumn.IsUnique, typeof(bool)));
            columns.Add(new DataColumn(SchemaTableColumn.BaseSchemaName, typeof(string)));
            columns.Add(new DataColumn(SchemaTableColumn.BaseTableName, typeof(string)));
            columns.Add(new DataColumn(SchemaTableColumn.BaseColumnName, typeof(string)));
            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].ReadOnly = true;
            }
            return table;
        }

        protected virtual void FillSchemaTable(DataTable dataTable)
        {
            throw new NotSupportedException("FillSchemaTable");
        }

        public bool GetBoolean(string name) => 
            this.GetBoolean(this.GetOrdinal(name));

        public byte GetByte(string name) => 
            this.GetByte(this.GetOrdinal(name));

        public override long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
        {
            this.AssertReaderIsOpen("GetBytes");
            this.AssertReaderHasData();
            if ((fieldOffset < 0L) || (fieldOffset > 0x7fffffffL))
            {
                throw new ArgumentException(g.a("InvalidSourceBufferIndex", fieldOffset), "fieldOffset");
            }
            if (bufferOffset < 0)
            {
                throw new ArgumentException(g.a("InvalidDestinationBufferIndex", bufferOffset), "bufferOffset");
            }
            if (length < 0)
            {
                throw new ArgumentException(g.a("InvalidBufferSizeOrIndex", length, ordinal));
            }
            byte[] src = (byte[]) this.GetValue(ordinal);
            if (buffer == null)
            {
                return (long) src.Length;
            }
            int count = (int) Math.Min(src.Length - fieldOffset, (long) length);
            Buffer.BlockCopy(src, (int) fieldOffset, buffer, bufferOffset, count);
            return (long) count;
        }

        public long GetBytes(string name, long fieldOffset, byte[] buffer, int bufferOffset, int length) => 
            this.GetBytes(this.GetOrdinal(name), fieldOffset, buffer, bufferOffset, length);

        public char GetChar(string name) => 
            this.GetChar(this.GetOrdinal(name));

        public override long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
        {
            this.AssertReaderIsOpen("GetChars");
            this.AssertReaderHasData();
            if ((fieldOffset < 0L) || (fieldOffset > 0x7fffffffL))
            {
                throw new ArgumentException(g.a("InvalidSourceBufferIndex", fieldOffset), "fieldOffset");
            }
            if (bufferOffset < 0)
            {
                throw new ArgumentException(g.a("InvalidDestinationBufferIndex", bufferOffset), "bufferOffset");
            }
            if (length < 0)
            {
                throw new ArgumentException(g.a("InvalidBufferSizeOrIndex", length, ordinal));
            }
            string str = this.GetString(ordinal);
            if (buffer == null)
            {
                return (long) str.Length;
            }
            int count = (int) Math.Min(str.Length - fieldOffset, (long) length);
            str.CopyTo((int) fieldOffset, buffer, bufferOffset, count);
            return (long) count;
        }

        public long GetChars(string name, long fieldOffset, char[] buffer, int bufferOffset, int length) => 
            this.GetChars(this.GetOrdinal(name), fieldOffset, buffer, bufferOffset, length);

        public override string GetDataTypeName(int ordinal)
        {
            throw new NotSupportedException("GetDataTypeName");
        }

        public string GetDataTypeName(string name) => 
            this.GetDataTypeName(this.GetOrdinal(name));

        public DateTime GetDateTime(string name) => 
            this.GetDateTime(this.GetOrdinal(name));

        public virtual DateTimeOffset GetDateTimeOffset(int ordinal) => 
            new DateTimeOffset(this.GetDateTime(ordinal));

        public DateTimeOffset GetDateTimeOffset(string name) => 
            this.GetDateTimeOffset(this.GetOrdinal(name));

        public decimal GetDecimal(string name) => 
            this.GetDecimal(this.GetOrdinal(name));

        public double GetDouble(string name) => 
            this.GetDouble(this.GetOrdinal(name));

        public override IEnumerator GetEnumerator() => 
            new DbEnumerator(this, this.IsCommandBehavior(System.Data.CommandBehavior.CloseConnection));

        public override Type GetFieldType(int ordinal)
        {
            throw new NotSupportedException("GetFieldType");
        }

        public Type GetFieldType(string name) => 
            this.GetFieldType(this.GetOrdinal(name));

        public float GetFloat(string name) => 
            this.GetFloat(this.GetOrdinal(name));

        public Guid GetGuid(string name) => 
            this.GetGuid(this.GetOrdinal(name));

        public short GetInt16(string name) => 
            this.GetInt16(this.GetOrdinal(name));

        public int GetInt32(string name) => 
            this.GetInt32(this.GetOrdinal(name));

        public long GetInt64(string name) => 
            this.GetInt64(this.GetOrdinal(name));

        public override string GetName(int ordinal)
        {
            throw new NotSupportedException("GetName");
        }

        public override int GetOrdinal(string name)
        {
            Utils.CheckArgumentNull(name, "name");
            this.AssertReaderIsOpen("GetOrdinal");
            this.AssertReaderHasColumns();
            int fieldCount = this.FieldCount;
            if (this.b == null)
            {
                this.b = Utils.CreateHashtable(true);
                for (int i = 0; i < fieldCount; i++)
                {
                    string key = this.GetName(i);
                    if (!this.b.Contains(key))
                    {
                        this.b.Add(key, i);
                    }
                }
            }
            object obj2 = this.b[name];
            if (obj2 == null)
            {
                throw new IndexOutOfRangeException(g.a("ReaderInvalidColumnName", name));
            }
            return (int) obj2;
        }

        public Type GetProviderSpecificFieldType(string name) => 
            this.GetProviderSpecificFieldType(this.GetOrdinal(name));

        public object GetProviderSpecificValue(string name) => 
            this.GetProviderSpecificValue(this.GetOrdinal(name));

        public override DataTable GetSchemaTable()
        {
            DataTable schemaTable = this.schemaTable;
            if (schemaTable == null)
            {
                this.AssertReaderIsOpen("GetSchemaTable");
                if (this.FieldCount > 0)
                {
                    schemaTable = CreateSchemaTable(this.FieldCount);
                    this.FillSchemaTable(schemaTable);
                    this.schemaTable = schemaTable;
                }
                else if (this.FieldCount <= 0)
                {
                    this.schemaTable = null;
                }
            }
            return schemaTable;
        }

        public string GetString(string name) => 
            this.GetString(this.GetOrdinal(name));

        public override object GetValue(int ordinal)
        {
            throw new NotSupportedException("GetValue");
        }

        public object GetValue(string name) => 
            this.GetValue(this.GetOrdinal(name));

        public override int GetValues(object[] values)
        {
            Utils.CheckArgumentNull(values, "values");
            this.AssertReaderIsOpen("GetValues");
            this.AssertReaderHasData();
            int num = Math.Min(values.Length, this.FieldCount);
            for (int i = 0; i < num; i++)
            {
                values[i] = this.GetValue(i);
            }
            return num;
        }

        protected bool IsCommandBehavior(System.Data.CommandBehavior condition) => 
            condition == (condition & this.a);

        public override bool IsDBNull(int ordinal)
        {
            this.AssertReaderIsOpen("IsDBNull");
            this.AssertReaderHasData();
            return Convert.IsDBNull(this.GetValue(ordinal));
        }

        public bool IsDBNull(string name) => 
            this.IsDBNull(this.GetOrdinal(name));

        public override bool NextResult()
        {
            this.AssertReaderIsOpen("NextResult");
            this.b = null;
            this.schemaTable = null;
            return false;
        }

        public override bool Read()
        {
            this.AssertReaderIsOpen("Read");
            return false;
        }

        protected internal void SetCommandBehavior(System.Data.CommandBehavior commandBehavior)
        {
            if (((commandBehavior & System.Data.CommandBehavior.KeyInfo) == System.Data.CommandBehavior.KeyInfo) && !this.IsCommandBehavior(System.Data.CommandBehavior.KeyInfo))
            {
                this.schemaTable = null;
            }
            this.a = commandBehavior;
        }

        protected internal System.Data.CommandBehavior CommandBehavior =>
            this.a;

        public override int Depth
        {
            get
            {
                this.AssertReaderIsOpen("Depth");
                return 0;
            }
        }

        public override int FieldCount
        {
            get
            {
                this.AssertReaderIsOpen("FieldCount");
                return 0;
            }
        }

        public override bool HasRows
        {
            get
            {
                this.AssertReaderIsOpen("HasRows");
                return true;
            }
        }

        public override bool IsClosed =>
            this.closed;

        public abstract bool EndOfData { get; }

        protected abstract bool IsValidRow { get; }

        public override object this[string name]
        {
            get
            {
                int ordinal = this.GetOrdinal(name);
                return this.GetValue(ordinal);
            }
        }

        public override object this[int ordinal] =>
            this.GetValue(ordinal);

        public override int RecordsAffected =>
            0;
    }
}

