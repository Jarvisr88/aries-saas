namespace Devart.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Common;
    using System.Reflection;

    public abstract class DbLoader : Component
    {
        private DbConnection a;
        private string b;
        private DbLoaderColumnCollection c;
        protected static readonly object errorEventKey = new object();
        protected static readonly object rowsCopiedEventKey = new object();
        protected bool isOpened;
        protected int loaderBufferSize;

        public DbLoader() : this(string.Empty)
        {
        }

        public DbLoader(string tableName) : this(tableName, null)
        {
        }

        public DbLoader(string tableName, DbConnection connection)
        {
            this.loaderBufferSize = 0x40000;
            this.TableName = tableName;
            this.Connection = connection;
            this.c = this.InitColumns();
        }

        internal void a()
        {
            this.Dispose(false);
        }

        private void a(DataRow[] A_0, IDataReader A_1, DataColumnCollection A_2, IColumnMappingCollection A_3)
        {
            if ((A_0 == null) || (A_0.Length != 0))
            {
                if (this.Columns.Count == 0)
                {
                    if (A_3 != null)
                    {
                        foreach (IColumnMapping mapping in A_3)
                        {
                            this.Columns.Add(this.CreateColumn(mapping.SourceColumn, A_2[mapping.DataSetColumn].DataType));
                        }
                    }
                    else if (A_2 == null)
                    {
                        if (A_1 == null)
                        {
                            throw new InvalidOperationException();
                        }
                        for (int i = 0; i < A_1.FieldCount; i++)
                        {
                            this.Columns.Add(this.CreateColumn(A_1.GetName(i), A_1.GetFieldType(i)));
                        }
                    }
                    else
                    {
                        foreach (DataColumn column in A_2)
                        {
                            this.Columns.Add(this.CreateColumn(column.ColumnName, column.DataType));
                        }
                    }
                }
                bool flag = !this.isOpened;
                if (!this.isOpened)
                {
                    this.Open();
                }
                try
                {
                    this.LoadTableInternal(A_0, A_1, A_2, A_3);
                }
                finally
                {
                    if (flag)
                    {
                        this.Close();
                    }
                }
            }
        }

        protected void CheckConnection()
        {
            if (this.a == null)
            {
                throw new InvalidOperationException(g.a("DbLoader_ConnectionWasNotInitialized"));
            }
            if (this.a.State != ConnectionState.Open)
            {
                throw new InvalidOperationException(g.a("DbLoader_ConnectionWasNotOpened"));
            }
        }

        protected void CheckOpen()
        {
            if (!this.isOpened)
            {
                throw new InvalidOperationException(g.a("DbLoader_CallOpenFirst"));
            }
        }

        protected void CheckTableName()
        {
            if (Utils.IsEmpty(this.b))
            {
                throw new InvalidOperationException(g.a("DbLoader_TableWasNotInitialized"));
            }
        }

        public abstract void Close();
        protected abstract DbLoaderColumn CreateColumn(string name, Type type);
        public abstract void CreateColumns();
        protected int GetColumnIndex(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            name = this.UnQuote(name);
            int num = -1;
            int num2 = 0;
            while (true)
            {
                if (num2 < this.c.Count)
                {
                    if (!Utils.Compare(name, this.UnQuote(this.c[num2].Name), false))
                    {
                        num2++;
                        continue;
                    }
                    num = num2;
                }
                if (num > -1)
                {
                    return num;
                }
                for (int i = 0; i < this.c.Count; i++)
                {
                    if (Utils.Compare(name, this.UnQuote(this.c[i].Name), true))
                    {
                        if (num != -1)
                        {
                            num = -2;
                            break;
                        }
                        num = i;
                    }
                }
                break;
            }
            if (num == -1)
            {
                throw new ArgumentException(string.Format(g.a("DbLoader_ColunmWithNameDoesNotExist"), name));
            }
            if (num == -2)
            {
                throw new ArgumentException(string.Format(g.a("DbLoader_TwoColunmsExist"), name));
            }
            return num;
        }

        protected abstract DbLoaderColumnCollection InitColumns();
        public void LoadTable(DataTable table)
        {
            this.LoadTable(table, (IColumnMappingCollection) null);
        }

        public void LoadTable(DataRow[] rows)
        {
            if (rows == null)
            {
                throw new ArgumentNullException("rows");
            }
            if (rows.Length != 0)
            {
                DataTable table = rows[0].Table;
                this.a(rows, null, table.Columns, null);
            }
        }

        public void LoadTable(IDataReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            this.a(null, reader, null, null);
        }

        public void LoadTable(DataTable table, DataRowState rowState)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            List<DataRow> list = new List<DataRow>(table.Rows.Count);
            foreach (DataRow row in table.Rows)
            {
                if (row.RowState == rowState)
                {
                    list.Add(row);
                }
            }
            this.LoadTable(list.ToArray());
        }

        public void LoadTable(DataTable table, IColumnMappingCollection columnMappings)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            List<DataRow> list = new List<DataRow>(table.Rows.Count);
            foreach (DataRow row in table.Rows)
            {
                list.Add(row);
            }
            this.a(list.ToArray(), null, table.Columns, columnMappings);
        }

        protected virtual void LoadTableInternal(DataRow[] rows, IDataReader reader, DataColumnCollection tableColumns, IColumnMappingCollection columnMappings)
        {
            if (columnMappings != null)
            {
                foreach (DataRow row in rows)
                {
                    foreach (IColumnMapping mapping in columnMappings)
                    {
                        this.SetValue(mapping.SourceColumn, row[mapping.DataSetColumn]);
                    }
                    this.NextRow();
                }
            }
            else if (rows == null)
            {
                if (reader == null)
                {
                    throw new InvalidOperationException();
                }
                while (reader.Read())
                {
                    int i = 0;
                    while (true)
                    {
                        if (i >= reader.FieldCount)
                        {
                            this.NextRow();
                            break;
                        }
                        this.SetValue(reader.GetName(i), reader.GetValue(i));
                        i++;
                    }
                }
            }
            else
            {
                foreach (DataRow row2 in rows)
                {
                    foreach (DataColumn column in tableColumns)
                    {
                        this.SetValue(column.ColumnName, row2[column]);
                    }
                    this.NextRow();
                }
            }
        }

        public abstract void NextRow();
        public abstract void Open();
        protected abstract string QuoteIfNeed(string name);
        public void SetNull(int i)
        {
            this.SetValue(i, null);
        }

        public void SetNull(string name)
        {
            this.SetValue(name, null);
        }

        public abstract void SetValue(int i, object value);
        public void SetValue(string name, object value)
        {
            this.SetValue(this.GetColumnIndex(name), value);
        }

        protected abstract string UnQuote(string name);

        [MergableProperty(false)]
        public DbConnection Connection
        {
            get => 
                this.a;
            set
            {
                if (!ReferenceEquals(this.a, value))
                {
                    this.a = value;
                }
            }
        }

        [y("DbLoader_BufferSize"), Category("Options"), DefaultValue(0x40000)]
        public int BufferSize
        {
            get => 
                this.loaderBufferSize;
            set => 
                this.loaderBufferSize = value;
        }

        [Category("Data"), y("DbLoader_TableName"), RefreshProperties(RefreshProperties.Repaint), MergableProperty(false)]
        public virtual string TableName
        {
            get => 
                !Utils.IsEmpty(this.b) ? this.b : string.Empty;
            set
            {
                if (this.b != value)
                {
                    this.b = value;
                }
            }
        }

        [MergableProperty(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Data"), y("DbLoader_Columns")]
        public DbLoaderColumnCollection Columns =>
            this.c;

        public object this[string columnName]
        {
            set => 
                this.SetValue(columnName, value);
        }

        public object this[int columnIndex]
        {
            set => 
                this.SetValue(columnIndex, value);
        }
    }
}

