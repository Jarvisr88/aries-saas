namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public abstract class DbCommandBuilderBase : System.Data.Common.DbCommandBuilder
    {
        private MissingMappingAction a;
        protected DataRow[] schemaRows;
        private bool b;
        protected DataRow[] keyRows;
        protected DataTable schemaTable;
        protected string tableName;
        private IDbCommand c;
        protected RefreshRowMode refreshRowMode;
        protected string[] refreshingFieldsList;
        private bool d = true;
        private bool e = true;
        private string f;
        protected string[] keyFieldsList;
        private bool g;

        protected DbCommandBuilderBase()
        {
        }

        private void a()
        {
            DataRow[] rowArray = null;
            bool flag = true;
            if (this.schemaTable != null)
            {
                string str;
                string str2;
                int num2;
                rowArray = new DataRow[this.schemaTable.Rows.Count];
                int index = 0;
                while (true)
                {
                    if (index >= this.schemaTable.Rows.Count)
                    {
                        if (rowArray.Length == 0)
                        {
                            throw new InvalidOperationException(Devart.Common.g.a("DynamicSQLNoTableInfo"));
                        }
                        str = "";
                        str2 = "";
                        this.b = false;
                        num2 = 0;
                        break;
                    }
                    rowArray[index] = this.schemaTable.Rows[index];
                    index++;
                }
                while (true)
                {
                    while (true)
                    {
                        if (num2 >= rowArray.Length)
                        {
                            if (flag)
                            {
                                this.b = false;
                            }
                            if (str2 == "")
                            {
                                throw new InvalidOperationException(Devart.Common.g.a("DynamicSQLNoTableInfo"));
                            }
                            this.schemaRows = rowArray;
                            return;
                        }
                        DataRow schemaRow = rowArray[num2];
                        string strB = Convert.ToString(schemaRow[SchemaTableColumn.BaseTableName]);
                        if (strB == "")
                        {
                            rowArray[num2] = null;
                        }
                        else
                        {
                            string b = Convert.ToString(schemaRow[SchemaTableColumn.BaseSchemaName]);
                            if (this.UpdatingTable != string.Empty)
                            {
                                string source = strB;
                                if (this.UpdatingTable.IndexOf('.') != -1)
                                {
                                    source = this.GetFullTableName(schemaRow);
                                }
                                string[] excludeStrings = new string[] { this.QuotePrefix, this.QuoteSuffix };
                                if (!Utils.CompareObjectNameSuffix(source, this.UpdatingTable, true, excludeStrings) || (!Utils.IsEmpty(str2) && ((string.Compare(str2, strB, true) == 0) && (str2 != strB))))
                                {
                                    rowArray[num2] = null;
                                    break;
                                }
                            }
                            if (!Convert.ToBoolean(schemaRow[SchemaTableColumn.IsKey]) && !Convert.ToBoolean(schemaRow[SchemaTableColumn.IsUnique]))
                            {
                                flag = false;
                            }
                            else
                            {
                                this.b = true;
                            }
                            if (str2 == "")
                            {
                                str = b;
                                str2 = strB;
                            }
                            else if ((str2 != strB) || !string.Equals(str, b, StringComparison.OrdinalIgnoreCase))
                            {
                                throw new InvalidOperationException(Devart.Common.g.a("DynamicSQLJoinUnsupported"));
                            }
                        }
                        break;
                    }
                    num2++;
                }
            }
        }

        private void a(bool A_0)
        {
            IDbCommand command;
            if (base.DataAdapter == null)
            {
                throw new InvalidOperationException(Devart.Common.g.a("MissingSourceCommand"));
            }
            IDbCommand selectCommand = base.DataAdapter.SelectCommand;
            if (selectCommand == null)
            {
                throw new InvalidOperationException(Devart.Common.g.a("MissingSourceCommand"));
            }
            if (selectCommand.Connection == null)
            {
                throw new InvalidOperationException(Devart.Common.g.a("MissingSourceCommandConnection"));
            }
            if (Utils.IsEmpty(this.UpdatingTable) || (selectCommand.CommandType != CommandType.StoredProcedure))
            {
                command = selectCommand;
            }
            else
            {
                command = selectCommand.Connection.CreateCommand();
                command.CommandText = this.UpdatingTable;
                command.CommandType = CommandType.TableDirect;
            }
            IDbConnection connection = command.Connection;
            if (this.schemaTable == null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                else
                {
                    A_0 = false;
                }
                try
                {
                    this.schemaTable = this.GetSchemaTable((DbCommand) command);
                }
                finally
                {
                    if (A_0 && (connection.State != ConnectionState.Closed))
                    {
                        connection.Close();
                    }
                }
            }
        }

        private static bool a(DataRowVersion A_0, DataRowVersion A_1) => 
            (A_0 == DataRowVersion.Original) ? ((A_1 != DataRowVersion.Current) && (A_1 != DataRowVersion.Default)) : ((A_0 == DataRowVersion.Current) || ((A_0 == DataRowVersion.Default) ? (A_1 != DataRowVersion.Current) : (A_1 == DataRowVersion.Proposed)));

        private void a(IDbCommand A_0, IDbCommand A_1)
        {
            if (A_1 != null)
            {
                A_0.CommandText = A_0.CommandText + ";\n" + A_1.CommandText;
                while (A_1.Parameters.Count > 0)
                {
                    object obj2 = A_1.Parameters[0];
                    A_1.Parameters.RemoveAt(0);
                    A_0.Parameters.Add(obj2);
                }
            }
        }

        private static void a(IDbCommand A_0, int A_1)
        {
            while (A_0.Parameters.Count > A_1)
            {
                A_0.Parameters.RemoveAt(A_1);
            }
        }

        private static string a(string A_0, IDataParameterCollection A_1)
        {
            DbParameter parameter = null;
            foreach (DbParameter parameter2 in A_1)
            {
                if ((parameter2.SourceColumn == A_0) && !parameter2.SourceColumnNullMapping)
                {
                    if (parameter == null)
                    {
                        parameter = parameter2;
                        continue;
                    }
                    if (a(parameter2.SourceVersion, parameter.SourceVersion))
                    {
                        parameter = parameter2;
                    }
                }
            }
            return parameter?.ParameterName;
        }

        internal IDbCommand a(DataTableMapping A_0, DataRow A_1, bool A_2)
        {
            this.BuildSchema(true);
            if ((base.DataAdapter == null) || (base.DataAdapter.SelectCommand == null))
            {
                return null;
            }
            if (this.tableName == "")
            {
                return null;
            }
            IDbCommand command = this.BuildNewCommand(null);
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ");
            int num = 0;
            DataColumnCollection columns = A_1.Table.Columns;
            int num2 = 0;
            while (num2 < columns.Count)
            {
                DataColumn column = columns[num2];
                string unquotedIdentifier = this.a(column, A_0, A_1);
                bool flag = false;
                int index = 0;
                while (true)
                {
                    if (index < this.schemaRows.Length)
                    {
                        DataRow row = this.schemaRows[index];
                        if ((row == null) || (unquotedIdentifier != ((string) row[SchemaTableColumn.BaseColumnName])))
                        {
                            index++;
                            continue;
                        }
                        flag = true;
                    }
                    if (!flag)
                    {
                        if (num > 0)
                        {
                            builder.Append(", ");
                        }
                        builder.Append(this.QuoteIdentifier(unquotedIdentifier));
                        num++;
                    }
                    num2++;
                    break;
                }
            }
            if (num == 0)
            {
                return null;
            }
            builder.Append(" FROM ");
            builder.Append(this.tableName);
            this.a(builder, command, 0, A_0, A_1, StatementType.Update, A_2, null);
            command.CommandText = builder.ToString();
            return command;
        }

        private string a(DataColumn A_0, DataTableMapping A_1, DataRow A_2)
        {
            DataColumnMappingCollection columnMappings = A_1.ColumnMappings;
            string columnName = A_0.ColumnName;
            for (int i = 0; i < columnMappings.Count; i++)
            {
                if (columnMappings[i].DataSetColumn == columnName)
                {
                    return A_1.SourceTable;
                }
            }
            return columnName;
        }

        private DataColumn a(string A_0, DataTableMapping A_1, DataRow A_2)
        {
            if (A_0 != "")
            {
                DataColumnMapping columnMappingBySchemaAction = A_1.GetColumnMappingBySchemaAction(A_0, this.a);
                if (columnMappingBySchemaAction != null)
                {
                    return columnMappingBySchemaAction.GetDataColumnBySchemaAction(A_2.Table, null, MissingSchemaAction.Error);
                }
            }
            return null;
        }

        internal IDbCommand a(string[] A_0, DataTableMapping A_1, DataRow A_2, bool A_3, IDataParameterCollection A_4)
        {
            this.BuildSchema(true);
            this.schemaRows = null;
            return this.BuildRefreshSqlCommand(A_1, A_2, A_3, A_4);
        }

        private void a(StringBuilder A_0, IDbCommand A_1, int A_2, DataTableMapping A_3, DataRow A_4, StatementType A_5, bool A_6, string[] A_7)
        {
            this.a(A_0, A_1, A_2, A_3, A_4, A_5, A_6, null, A_7);
        }

        private void a(StringBuilder A_0, IDbCommand A_1, int A_2, DataTableMapping A_3, DataRow A_4, StatementType A_5, bool A_6, IDataParameterCollection A_7, string[] A_8)
        {
            DataRow schemaRow = null;
            int num = 0;
            A_0.Append(" WHERE ");
            DataRow[] rowArray = (this.keyRows == null) ? this.schemaRows : this.keyRows;
            int index = 0;
            while (true)
            {
                while (true)
                {
                    if (index >= rowArray.Length)
                    {
                        if (num != 0)
                        {
                            a(A_1, A_2);
                            return;
                        }
                        if (A_7 == null)
                        {
                            throw new InvalidOperationException(Devart.Common.g.a("DynamicSqlGenerationNotSupp"));
                        }
                        throw new InvalidOperationException(Devart.Common.g.a("DbCommandBuilder_AllRefreshNotSupported"));
                    }
                    schemaRow = rowArray[index];
                    if ((schemaRow != null) && (Convert.ToString(schemaRow[SchemaTableColumn.BaseColumnName]) != ""))
                    {
                        if ((A_8 != null) && ((A_8.Length != 0) && ((this.keyRows == null) && !this.b)))
                        {
                            bool flag = false;
                            string[] strArray = A_8;
                            int num3 = 0;
                            while (true)
                            {
                                if (num3 < strArray.Length)
                                {
                                    string str4 = strArray[num3];
                                    if ((str4 == null) || ((str4 == string.Empty) || (str4 != Convert.ToString(schemaRow[SchemaTableColumn.BaseColumnName]))))
                                    {
                                        num3++;
                                        continue;
                                    }
                                    flag = (((schemaRow.Table.Columns.Contains("AutoincrementColumnNative") ? ((bool) schemaRow["AutoincrementColumnNative"]) : ((bool) schemaRow[SchemaTableOptionalColumn.IsAutoIncrement])) || (schemaRow.Table.Columns.Contains("IsIdentity") && ((bool) schemaRow["IsIdentity"]))) || !this.b) || (!Convert.ToBoolean(schemaRow[SchemaTableColumn.IsKey]) && !Convert.ToBoolean(schemaRow[SchemaTableColumn.IsUnique]));
                                }
                                if (!flag)
                                {
                                    break;
                                }
                                break;
                            }
                        }
                        if ((this.keyRows != null) || (!this.ExcludeFromWhere(schemaRow) && (!this.b || (Convert.ToBoolean(schemaRow[SchemaTableColumn.IsKey]) || Convert.ToBoolean(schemaRow[SchemaTableColumn.IsUnique])))))
                        {
                            if (num > 0)
                            {
                                A_0.Append(" AND ");
                            }
                            string str = Convert.ToString(schemaRow[SchemaTableColumn.ColumnName]);
                            object obj2 = null;
                            if ((A_3 != null) && (A_4 != null))
                            {
                                DataColumn column = this.a(str, A_3, A_4);
                                if (column != null)
                                {
                                    obj2 = A_4[column, DataRowVersion.Original];
                                }
                            }
                            string parameterName = Convert.ToString(schemaRow[SchemaTableColumn.BaseColumnName]);
                            bool flag2 = true;
                            string str3 = null;
                            if (A_7 != null)
                            {
                                str3 = a(str, A_7);
                                if (str3 != null)
                                {
                                    flag2 = false;
                                }
                                else
                                {
                                    if (A_2 < A_7.Count)
                                    {
                                        A_2 = A_7.Count;
                                    }
                                    flag2 = true;
                                }
                            }
                            if (flag2)
                            {
                                str3 = !A_6 ? this.GetParameterName((int) (A_2 + 1)) : this.GetParameterName(parameterName, A_1.Parameters);
                            }
                            parameterName = this.QuoteIdentifier(parameterName);
                            if (Convert.IsDBNull(obj2))
                            {
                                A_0.Append($"{parameterName} IS NULL");
                            }
                            else
                            {
                                A_0.Append(parameterName);
                                A_0.Append(" = ");
                                A_0.Append(this.GetParameterPlaceholder(str3));
                            }
                            if (!Convert.IsDBNull(obj2))
                            {
                                if (flag2)
                                {
                                    IDbDataParameter parameter = b(A_1, A_2);
                                    parameter.ParameterName = str3;
                                    this.ApplyParameterInfo((DbParameter) parameter, schemaRow, A_5, true);
                                    parameter.Direction = ParameterDirection.Input;
                                    parameter.Value = obj2;
                                    parameter.SourceColumn = str;
                                    parameter.SourceVersion = DataRowVersion.Original;
                                    A_1.Parameters.Add(parameter);
                                }
                                A_2++;
                            }
                            num++;
                        }
                    }
                    break;
                }
                index++;
            }
        }

        protected virtual void AddRefreshSql(IDbCommand command, StatementType statementType)
        {
            this.a(command, this.a(null, null, null, true, command.Parameters));
        }

        protected virtual void AddRefreshSql(IDbCommand command, bool useColumnsForParameterNames, StatementType statementType)
        {
            this.a(command, this.a(null, null, null, useColumnsForParameterNames, command.Parameters));
        }

        protected virtual void AddRefreshSql(IDbCommand command, string[] fields, bool useColumnsForParameterNames, StatementType statementType)
        {
            string[] refreshingFieldsList = this.refreshingFieldsList;
            try
            {
                if (fields != null)
                {
                    this.refreshingFieldsList = fields;
                }
                this.a(command, this.a(fields, null, null, useColumnsForParameterNames, command.Parameters));
            }
            finally
            {
                if (fields != null)
                {
                    this.refreshingFieldsList = refreshingFieldsList;
                }
            }
        }

        private void b()
        {
            this.schemaTable = null;
            this.schemaRows = null;
            this.tableName = null;
            this.keyRows = null;
        }

        private static IDbDataParameter b(IDbCommand A_0, int A_1) => 
            (A_1 >= A_0.Parameters.Count) ? A_0.CreateParameter() : ((IDbDataParameter) A_0.Parameters[A_1]);

        protected virtual string BuildAutoIncrementRefreshSql(DataTableMapping mappings, DataRow dataRow, bool useColumnsForParametersNames)
        {
            DataRow row;
            if ((this.GetLastInsertIdKeyword() == string.Empty) || (this.GetLastInsertIdKeyword() == null))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            DataRow row2 = null;
            ArrayList list = new ArrayList();
            if ((this.tableName == null) || (this.tableName == string.Empty))
            {
                return string.Empty;
            }
            if (this.schemaRows == null)
            {
                return string.Empty;
            }
            bool flag = (this.schemaRows.Length != 0) ? this.schemaRows[0].Table.Columns.Contains(SchemaTableColumn.IsExpression) : false;
            int index = 0;
            while (index < this.schemaRows.Length)
            {
                row = this.schemaRows[index];
                if (((row != null) && ((Convert.ToString(row[SchemaTableColumn.BaseColumnName]) != "") && (!flag || !((bool) row[SchemaTableColumn.IsExpression])))) && !((bool) row[SchemaTableOptionalColumn.IsRowVersion]))
                {
                    if (((row.Table.Columns.Contains("AutoincrementColumnNative") ? ((bool) row["AutoincrementColumnNative"]) : ((bool) row[SchemaTableOptionalColumn.IsAutoIncrement])) || (row.Table.Columns.Contains("IsIdentity") && ((bool) row["IsIdentity"]))) && ((bool) row[SchemaTableColumn.IsKey]))
                    {
                        row2 = row;
                    }
                    list.Add(row);
                }
                index++;
            }
            if (row2 == null)
            {
                return string.Empty;
            }
            builder.Append("SELECT ");
            if ((this.refreshingFieldsList != null) && (this.refreshingFieldsList.Length != 0))
            {
                index = 0;
                while (index < this.refreshingFieldsList.Length)
                {
                    string st = this.refreshingFieldsList[index];
                    if (!Utils.IsEmpty(st))
                    {
                        if (index > 0)
                        {
                            builder.Append(", ");
                        }
                        builder.Append(this.QuoteIdentifier(st));
                    }
                    index++;
                }
            }
            else if (list.Count == 0)
            {
                builder.Append("*");
            }
            else
            {
                for (index = 0; index < list.Count; index++)
                {
                    row = (DataRow) list[index];
                    string unquotedIdentifier = (string) row[SchemaTableColumn.ColumnName];
                    if (index > 0)
                    {
                        builder.Append(", ");
                    }
                    builder.Append(this.QuoteIdentifier(unquotedIdentifier));
                }
            }
            builder.Append(" FROM ");
            builder.Append(this.tableName);
            builder.Append(" WHERE ");
            builder.Append(this.QuoteIdentifier((string) row2[SchemaTableColumn.ColumnName]));
            builder.Append(" = " + this.GetLastInsertIdKeyword());
            return builder.ToString();
        }

        protected virtual IDbCommand BuildNewCommand(IDbCommand command)
        {
            IDbCommand selectCommand = null;
            if (base.DataAdapter != null)
            {
                selectCommand = base.DataAdapter.SelectCommand;
            }
            if (command != null)
            {
                command.Parameters.Clear();
            }
            else
            {
                command = selectCommand.Connection.CreateCommand();
                command.CommandTimeout = selectCommand.CommandTimeout;
                command.Transaction = selectCommand.Transaction;
            }
            command.CommandType = CommandType.Text;
            command.UpdatedRowSource = (this.RefreshMode == RefreshRowMode.None) ? UpdateRowSource.None : UpdateRowSource.Both;
            return command;
        }

        protected virtual IDbCommand BuildRefreshSqlCommand(DataTableMapping mappings, DataRow dataRow, bool useColumnsForParametersNames, IDataParameterCollection mainCommandParameters)
        {
            DataRow row;
            if (this.tableName == string.Empty)
            {
                return null;
            }
            bool flag = false;
            IDbCommand command = this.BuildNewCommand(this.c);
            StringBuilder builder = new StringBuilder("SELECT ");
            ArrayList list = new ArrayList();
            bool flag2 = (this.schemaRows.Length != 0) ? this.schemaRows[0].Table.Columns.Contains(SchemaTableColumn.IsExpression) : false;
            int index = 0;
            while (index < this.schemaRows.Length)
            {
                row = this.schemaRows[index];
                if (((row != null) && ((Convert.ToString(row[SchemaTableColumn.BaseColumnName]) != "") && (!((bool) row[SchemaTableOptionalColumn.IsReadOnly]) && (!flag2 || !((bool) row[SchemaTableColumn.IsExpression]))))) && !((bool) row[SchemaTableOptionalColumn.IsRowVersion]))
                {
                    list.Add(row);
                }
                index++;
            }
            if ((this.refreshingFieldsList != null) && (this.refreshingFieldsList.Length != 0))
            {
                index = 0;
                while (index < this.refreshingFieldsList.Length)
                {
                    if (this.refreshingFieldsList[index].Trim() != string.Empty)
                    {
                        if (index > 0)
                        {
                            builder.Append(", ");
                        }
                        if (this.Quoted)
                        {
                            builder.Append(this.QuoteIdentifier(this.refreshingFieldsList[index]));
                        }
                        else
                        {
                            builder.Append(this.refreshingFieldsList[index]);
                        }
                    }
                    index++;
                }
            }
            else if (list.Count == 0)
            {
                builder.Append("*");
            }
            else
            {
                for (index = 0; index < list.Count; index++)
                {
                    row = (DataRow) list[index];
                    string unquotedIdentifier = (string) row[SchemaTableColumn.BaseColumnName];
                    string str2 = (string) row[SchemaTableColumn.ColumnName];
                    flag = !string.IsNullOrEmpty(str2) && (str2 != unquotedIdentifier);
                    if (unquotedIdentifier.Trim() != string.Empty)
                    {
                        if (index > 0)
                        {
                            builder.Append(", ");
                        }
                        if (this.Quoted)
                        {
                            builder.Append(this.QuoteIdentifier(unquotedIdentifier));
                            if (flag)
                            {
                                builder.Append(" AS " + this.QuoteIdentifier(str2));
                            }
                        }
                        else
                        {
                            builder.Append(unquotedIdentifier);
                            if (flag)
                            {
                                builder.Append(" AS " + str2);
                            }
                        }
                    }
                }
            }
            builder.Append(" FROM ");
            builder.Append(this.tableName);
            this.a(builder, command, 0, mappings, dataRow, StatementType.Delete, useColumnsForParametersNames, mainCommandParameters, this.refreshingFieldsList);
            command.CommandText = builder.ToString();
            this.c = command;
            return command;
        }

        protected void BuildSchema(bool closeConnection)
        {
            this.a(closeConnection);
            this.ChangeSchema();
        }

        protected void ChangeSchema()
        {
            if (base.DataAdapter != null)
            {
                this.a = base.DataAdapter.MissingMappingAction;
                if (this.a != MissingMappingAction.Passthrough)
                {
                    this.a = MissingMappingAction.Error;
                }
            }
            if (this.schemaRows == null)
            {
                this.a();
            }
            if (this.schemaRows != null)
            {
                for (int i = 0; i < this.schemaRows.Length; i++)
                {
                    DataRow schemaRow = this.schemaRows[i];
                    if (schemaRow != null)
                    {
                        this.tableName = this.GetFullTableName(schemaRow);
                        break;
                    }
                }
            }
            if ((this.keyRows == null) && !Utils.IsEmpty(this.keyFieldsList))
            {
                this.keyRows = new DataRow[this.keyFieldsList.Length];
                for (int i = 0; i < this.keyFieldsList.Length; i++)
                {
                    foreach (DataRow row2 in this.schemaTable.Rows)
                    {
                        if (((string) row2[SchemaTableColumn.BaseColumnName]) == this.UnQuoteName(this.keyFieldsList[i]))
                        {
                            this.keyRows[i] = row2;
                            break;
                        }
                    }
                    if (this.keyRows[i] == null)
                    {
                        this.keyRows[i] = this.schemaTable.NewRow();
                        this.InitSchemaRow(this.keyRows[i]);
                        this.keyRows[i][SchemaTableColumn.BaseColumnName] = this.UnQuoteName(this.keyFieldsList[i]);
                    }
                }
            }
        }

        protected virtual bool ExcludeFromWhere(DataRow schemaRow) => 
            Convert.ToBoolean(schemaRow[SchemaTableColumn.IsLong]);

        protected virtual string GetFullTableName(DataRow schemaRow) => 
            this.GetFullTableName(schemaRow[SchemaTableOptionalColumn.BaseCatalogName] as string, schemaRow[SchemaTableColumn.BaseSchemaName] as string, schemaRow[SchemaTableColumn.BaseTableName] as string);

        protected string GetFullTableName(string catalogName, string schemaName, string tableName)
        {
            StringBuilder builder = new StringBuilder();
            if (!Utils.IsEmpty(tableName))
            {
                if (!Utils.IsEmpty(catalogName))
                {
                    builder.Append(this.QuoteIdentifier(catalogName));
                    builder.Append(".");
                }
                if (!Utils.IsEmpty(schemaName))
                {
                    builder.Append(this.QuoteIdentifier(schemaName));
                    builder.Append(".");
                }
                builder.Append(this.QuoteIdentifier(tableName));
            }
            return builder.ToString();
        }

        protected virtual string GetLastInsertIdKeyword() => 
            string.Empty;

        protected virtual string GetParameterName(string parameterName, IList parameters) => 
            this.GetParameterName((int) (parameters.Count + 1));

        protected virtual string GetParameterPlaceholder(string parameterName) => 
            ":" + parameterName;

        public IDbCommand GetRefreshCommand() => 
            this.GetRefreshCommand(null, false);

        public IDbCommand GetRefreshCommand(bool useColumnsForParameterNames) => 
            this.GetRefreshCommand(null, useColumnsForParameterNames);

        public IDbCommand GetRefreshCommand(string[] fields, bool useColumnsForParameterNames) => 
            this.a(fields, null, null, useColumnsForParameterNames, null);

        protected override DbCommand InitializeCommand(DbCommand command)
        {
            DbCommand command2 = base.InitializeCommand(command);
            if (this.RefreshMode != RefreshRowMode.None)
            {
                if ((base.DataAdapter != null) && (base.DataAdapter.UpdateBatchSize > 1))
                {
                    throw new InvalidOperationException("When batching, the RefreshMode property value of RefreshRowMode.AfterInsert, RefreshRowMode.AfterUpdate or RefreshRowMode.Both is invalid.");
                }
                command2.UpdatedRowSource = UpdateRowSource.Both;
            }
            return command2;
        }

        protected virtual void InitSchemaRow(DataRow schemaRow)
        {
            schemaRow[SchemaTableColumn.ColumnName] = string.Empty;
            schemaRow[SchemaTableColumn.ColumnOrdinal] = 0;
            schemaRow[SchemaTableColumn.ColumnSize] = 0;
            schemaRow[SchemaTableColumn.NumericPrecision] = 0;
            schemaRow[SchemaTableColumn.NumericScale] = 0;
            schemaRow[SchemaTableColumn.DataType] = typeof(object);
            schemaRow[SchemaTableOptionalColumn.ProviderSpecificDataType] = typeof(object);
            schemaRow[SchemaTableColumn.ProviderType] = 0;
            schemaRow[SchemaTableColumn.IsLong] = false;
            schemaRow[SchemaTableColumn.AllowDBNull] = true;
            schemaRow[SchemaTableOptionalColumn.IsReadOnly] = false;
            schemaRow[SchemaTableOptionalColumn.IsRowVersion] = false;
            schemaRow[SchemaTableColumn.IsUnique] = false;
            schemaRow[SchemaTableColumn.IsKey] = false;
            schemaRow[SchemaTableOptionalColumn.IsAutoIncrement] = false;
            schemaRow[SchemaTableColumn.BaseSchemaName] = string.Empty;
            schemaRow[SchemaTableOptionalColumn.BaseCatalogName] = string.Empty;
            schemaRow[SchemaTableColumn.BaseTableName] = string.Empty;
            schemaRow[SchemaTableColumn.BaseColumnName] = string.Empty;
            if (schemaRow.Table.Columns.Contains(SchemaTableColumn.IsAliased))
            {
                schemaRow[SchemaTableColumn.IsAliased] = false;
            }
            if (schemaRow.Table.Columns.Contains(SchemaTableColumn.IsExpression))
            {
                schemaRow[SchemaTableColumn.IsExpression] = false;
            }
        }

        public override void RefreshSchema()
        {
            this.b();
            base.RefreshSchema();
        }

        protected string UnQuoteName(string name) => 
            ((name == null) || ((name.Length <= 1) || (!name.StartsWith(this.QuotePrefix) || !name.EndsWith(this.QuoteSuffix)))) ? name : name.Substring(this.QuotePrefix.Length, (name.Length - this.QuotePrefix.Length) - this.QuoteSuffix.Length).Replace("\"\"", "\"");

        [DefaultValue(""), y("DbCommandBuilder_UpdatingTable")]
        public string UpdatingTable
        {
            get => 
                (this.f != null) ? this.f : string.Empty;
            set
            {
                if (this.f != value)
                {
                    this.f = value;
                    this.RefreshSchema();
                }
            }
        }

        [y("DbCommandBuilder_KeyFields"), DefaultValue("")]
        public string KeyFields
        {
            get => 
                (this.keyFieldsList != null) ? string.Join(";", this.keyFieldsList) : string.Empty;
            set
            {
                if (this.KeyFields != value)
                {
                    if (Utils.IsEmpty(value))
                    {
                        this.keyFieldsList = null;
                    }
                    else
                    {
                        char[] separator = new char[] { ';' };
                        this.keyFieldsList = value.Split(separator);
                    }
                    this.RefreshSchema();
                }
            }
        }

        [y("DbCommandBuilder_Quoted"), Category("Behavior"), DefaultValue(false)]
        public virtual bool Quoted
        {
            get => 
                this.g;
            set => 
                this.g = value;
        }

        [y("DbCommandBuilder_RefreshMode"), DefaultValue(0), Category("Update")]
        public RefreshRowMode RefreshMode
        {
            get => 
                this.refreshRowMode;
            set => 
                this.refreshRowMode = value;
        }

        protected abstract char[] QuoteSymbols { get; }

        [DefaultValue(""), y("DbCommandBuilder_RefreshingFields"), Category("Update")]
        public virtual string RefreshingFields
        {
            get => 
                (this.refreshingFieldsList != null) ? string.Join(";", this.refreshingFieldsList) : string.Empty;
            set
            {
                if (this.RefreshingFields != value)
                {
                    this.refreshingFieldsList = !Utils.IsEmpty(value) ? Utils.SplitItems(value, this.QuoteSymbols) : null;
                    this.RefreshSchema();
                }
            }
        }

        [y("DbCommandBuilder_UseSchema"), Category("Schema"), DefaultValue(true)]
        public virtual bool UseSchema
        {
            get => 
                this.d;
            set
            {
                if (this.d != value)
                {
                    this.d = value;
                    this.RefreshSchema();
                }
            }
        }

        [DefaultValue(true), y("DbCommandBuilder_UseCatalog"), Category("Schema")]
        public virtual bool UseCatalog
        {
            get => 
                this.e;
            set
            {
                if (this.e != value)
                {
                    this.e = value;
                    this.RefreshSchema();
                }
            }
        }
    }
}

