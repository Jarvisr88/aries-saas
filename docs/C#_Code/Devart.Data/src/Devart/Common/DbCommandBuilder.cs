namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Common;
    using System.Reflection;
    using System.Text;

    public abstract class DbCommandBuilder : DbCommandBuilderBase
    {
        [Obfuscation]
        private bool a;
        protected string[] updatingFieldsList;
        private static bool throwOnInvalidQuote = true;
        internal static ArrayList b;

        protected DbCommandBuilder()
        {
        }

        protected void AfterCreatingCommand()
        {
            if (Utils.MonoDetected)
            {
                b = null;
            }
        }

        protected void BeforeCreatingCommand()
        {
            if (Utils.MonoDetected)
            {
                b = new ArrayList();
                if (base.schemaTable == null)
                {
                    base.BuildSchema(true);
                }
            }
        }

        protected void CorrectParameterPrefixes(DbCommand command)
        {
            if (command != null)
            {
                if (b != null)
                {
                    int num = -1;
                    for (int i = 0; i < b.Count; i = (i + 1) + 1)
                    {
                        if (ReferenceEquals((DbCommand) b[i], command))
                        {
                            DbParameter parameter = (DbParameter) b[i + 1];
                            int index = command.Parameters.IndexOf(parameter);
                            if (index >= 0)
                            {
                                num = index;
                            }
                            else
                            {
                                parameter.SourceColumnNullMapping = true;
                                parameter.SourceVersion = DataRowVersion.Original;
                                parameter.SourceColumn = command.Parameters[num + 1].SourceColumn;
                                command.Parameters.Insert(num + 1, parameter);
                            }
                        }
                    }
                }
                char ch = this.GetParameterPlaceholder(0)[0];
                if (ch != '@')
                {
                    string commandText = command.CommandText;
                    foreach (DbParameter parameter2 in command.Parameters)
                    {
                        string parameterName = parameter2.ParameterName;
                        string str2 = parameter2.ParameterName;
                        string str3 = str2;
                        if (!string.IsNullOrEmpty(parameter2.ParameterName))
                        {
                            if ((parameter2.ParameterName[0] == '@') && (ch != '@'))
                            {
                                str2 = parameter2.ParameterName.Substring(1);
                            }
                            else
                            {
                                str3 = "@" + parameter2.ParameterName;
                            }
                        }
                        string str4 = ch.ToString() + str2;
                        int startIndex = 0;
                        while (true)
                        {
                            int index = commandText.IndexOf(str3, startIndex);
                            if (index == -1)
                            {
                                parameter2.ParameterName = str2;
                                DataRow row = null;
                                foreach (DataRow row2 in base.schemaTable.Rows)
                                {
                                    if (((string) row2[SchemaTableColumn.BaseColumnName]) == parameter2.SourceColumn)
                                    {
                                        row = row2;
                                        break;
                                    }
                                }
                                if (row != null)
                                {
                                    this.ApplyParameterInfo(parameter2, row, StatementType.Insert, true);
                                }
                                break;
                            }
                            startIndex = index + str2.Length;
                            if ((index > 0) && (!char.IsLetterOrDigit(commandText[index - 1]) && (((index + str3.Length) >= commandText.Length) || !char.IsLetterOrDigit(commandText[index + str3.Length]))))
                            {
                                commandText = commandText.Substring(0, index) + str4 + commandText.Substring(index + str3.Length);
                            }
                        }
                    }
                    command.CommandText = commandText;
                }
            }
        }

        public DbCommand GetDeleteCommand()
        {
            DbCommand command2;
            this.BeforeCreatingCommand();
            try
            {
                DbCommand deleteCommand = base.GetDeleteCommand();
                if (Utils.MonoDetected)
                {
                    this.CorrectParameterPrefixes(deleteCommand);
                }
                command2 = deleteCommand;
            }
            finally
            {
                this.AfterCreatingCommand();
            }
            return command2;
        }

        public DbCommand GetDeleteCommand(bool useColumnsForParameterNames)
        {
            DbCommand command2;
            this.BeforeCreatingCommand();
            try
            {
                DbCommand deleteCommand = base.GetDeleteCommand(useColumnsForParameterNames);
                if (Utils.MonoDetected)
                {
                    this.CorrectParameterPrefixes(deleteCommand);
                }
                command2 = deleteCommand;
            }
            finally
            {
                this.AfterCreatingCommand();
            }
            return command2;
        }

        public DbCommand GetInsertCommand()
        {
            DbCommand insertCommand;
            this.BeforeCreatingCommand();
            try
            {
                insertCommand = base.GetInsertCommand();
                if (Utils.MonoDetected)
                {
                    this.CorrectParameterPrefixes(insertCommand);
                }
            }
            finally
            {
                this.AfterCreatingCommand();
            }
            if ((insertCommand != null) && ((base.RefreshMode & RefreshRowMode.AfterInsert) != RefreshRowMode.None))
            {
                this.AddRefreshSql(insertCommand, StatementType.Insert);
            }
            return insertCommand;
        }

        public DbCommand GetInsertCommand(bool useColumnsForParameterNames)
        {
            DbCommand insertCommand;
            this.BeforeCreatingCommand();
            try
            {
                insertCommand = base.GetInsertCommand(useColumnsForParameterNames);
                if (Utils.MonoDetected)
                {
                    this.CorrectParameterPrefixes(insertCommand);
                }
            }
            finally
            {
                this.AfterCreatingCommand();
            }
            if ((insertCommand != null) && ((base.RefreshMode & RefreshRowMode.AfterInsert) != RefreshRowMode.None))
            {
                this.AddRefreshSql(insertCommand, useColumnsForParameterNames, StatementType.Insert);
            }
            return insertCommand;
        }

        public DbCommand GetInsertCommand(string[] fields, bool useColumnsForParameterNames)
        {
            DbCommand insertCommand;
            this.updatingFieldsList = fields;
            this.RefreshSchema();
            this.BeforeCreatingCommand();
            try
            {
                insertCommand = base.GetInsertCommand(useColumnsForParameterNames);
                if (Utils.MonoDetected)
                {
                    this.CorrectParameterPrefixes(insertCommand);
                }
            }
            finally
            {
                this.AfterCreatingCommand();
            }
            if ((insertCommand != null) && ((base.RefreshMode & RefreshRowMode.AfterInsert) != RefreshRowMode.None))
            {
                this.AddRefreshSql(insertCommand, null, useColumnsForParameterNames, StatementType.Insert);
            }
            return insertCommand;
        }

        public DbCommand GetRefreshCommand() => 
            (DbCommand) base.GetRefreshCommand();

        public DbCommand GetRefreshCommand(bool useColumnsForParameterNames) => 
            (DbCommand) base.GetRefreshCommand(useColumnsForParameterNames);

        public DbCommand GetRefreshCommand(string[] fields, bool useColumnsForParameterNames)
        {
            this.updatingFieldsList = fields;
            this.RefreshSchema();
            return (DbCommand) base.GetRefreshCommand(useColumnsForParameterNames);
        }

        protected override DataTable GetSchemaTable(DbCommand srcCommand)
        {
            DataTable schemaTable;
            using (IDataReader reader = srcCommand.ExecuteReader(CommandBehavior.KeyInfo | CommandBehavior.SchemaOnly))
            {
                schemaTable = reader.GetSchemaTable();
            }
            return this.GetUpdateSchemaTable(schemaTable);
        }

        public DbCommand GetUpdateCommand()
        {
            DbCommand updateCommand;
            this.BeforeCreatingCommand();
            try
            {
                updateCommand = base.GetUpdateCommand();
                if (Utils.MonoDetected)
                {
                    this.CorrectParameterPrefixes(updateCommand);
                }
            }
            finally
            {
                this.AfterCreatingCommand();
            }
            if ((updateCommand != null) && ((base.RefreshMode & RefreshRowMode.AfterUpdate) != RefreshRowMode.None))
            {
                this.AddRefreshSql(updateCommand, StatementType.Update);
            }
            return updateCommand;
        }

        public DbCommand GetUpdateCommand(bool useColumnsForParameterNames)
        {
            DbCommand updateCommand;
            this.BeforeCreatingCommand();
            try
            {
                updateCommand = base.GetUpdateCommand(useColumnsForParameterNames);
                if (Utils.MonoDetected)
                {
                    this.CorrectParameterPrefixes(updateCommand);
                }
            }
            finally
            {
                this.AfterCreatingCommand();
            }
            if ((updateCommand != null) && ((base.RefreshMode & RefreshRowMode.AfterUpdate) != RefreshRowMode.None))
            {
                this.AddRefreshSql(updateCommand, useColumnsForParameterNames, StatementType.Update);
            }
            return updateCommand;
        }

        public DbCommand GetUpdateCommand(string[] fields, bool useColumnsForParameterNames)
        {
            DbCommand updateCommand;
            this.updatingFieldsList = fields;
            this.RefreshSchema();
            this.BeforeCreatingCommand();
            try
            {
                updateCommand = base.GetUpdateCommand(useColumnsForParameterNames);
                if (Utils.MonoDetected)
                {
                    this.CorrectParameterPrefixes(updateCommand);
                }
            }
            finally
            {
                this.AfterCreatingCommand();
            }
            if ((updateCommand != null) && ((base.RefreshMode & RefreshRowMode.AfterUpdate) != RefreshRowMode.None))
            {
                this.AddRefreshSql(updateCommand, null, useColumnsForParameterNames, StatementType.Update);
            }
            return updateCommand;
        }

        protected virtual DataTable GetUpdateSchemaTable(DataTable schemaTable)
        {
            string[] updatingFieldsList;
            int num;
            DataRow[] rowArray2;
            int num2;
            if (schemaTable == null)
            {
                return schemaTable;
            }
            DataRow[] array = new DataRow[schemaTable.Rows.Count];
            schemaTable.Rows.CopyTo(array, 0);
            DataColumn column = schemaTable.Columns[SchemaTableColumn.IsKey];
            column.ReadOnly = false;
            DataColumn column2 = schemaTable.Columns[SchemaTableColumn.IsUnique];
            column2.ReadOnly = false;
            DataColumn column3 = (schemaTable.Columns.IndexOf(SchemaTableOptionalColumn.IsAutoIncrement) == -1) ? schemaTable.Columns.Add(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool)) : schemaTable.Columns[SchemaTableOptionalColumn.IsAutoIncrement];
            column3.ReadOnly = false;
            DataColumn column4 = (schemaTable.Columns.IndexOf("AutoincrementColumnNative") == -1) ? schemaTable.Columns.Add("AutoincrementColumnNative", typeof(bool)) : schemaTable.Columns["AutoincrementColumnNative"];
            column4.ReadOnly = false;
            bool flag = false;
            bool readOnly = false;
            bool flag3 = false;
            DataColumn column5 = schemaTable.Columns[SchemaTableColumn.BaseSchemaName];
            readOnly = column5.ReadOnly;
            column5.ReadOnly = false;
            DataColumn column6 = schemaTable.Columns[SchemaTableOptionalColumn.BaseCatalogName];
            flag3 = column6.ReadOnly;
            column6.ReadOnly = false;
            if ((this.updatingFieldsList != null) && (this.updatingFieldsList.Length != 0))
            {
                updatingFieldsList = this.updatingFieldsList;
                for (num = 0; num < updatingFieldsList.Length; num++)
                {
                    string name = updatingFieldsList[num];
                    bool flag4 = false;
                    if (name.Trim() != string.Empty)
                    {
                        string strA = base.UnQuoteName(name);
                        rowArray2 = array;
                        num2 = 0;
                        while (true)
                        {
                            if (num2 >= rowArray2.Length)
                            {
                                if (!flag4)
                                {
                                    rowArray2 = array;
                                    num2 = 0;
                                    while (num2 < rowArray2.Length)
                                    {
                                        DataRow row2 = rowArray2[num2];
                                        if (string.Compare(strA, Utils.ObjectToString(row2[SchemaTableColumn.ColumnName]), true) == 0)
                                        {
                                            if (flag4)
                                            {
                                                throw new InvalidOperationException($"There is two updating field with name {strA} in schema table.");
                                            }
                                            flag4 = true;
                                        }
                                        num2++;
                                    }
                                }
                                if (flag4)
                                {
                                    break;
                                }
                                throw new InvalidOperationException($"Updating field with name {strA} does not exist in schema table.");
                            }
                            DataRow row = rowArray2[num2];
                            if (string.Compare(strA, Utils.ObjectToString(row[SchemaTableColumn.ColumnName]), false) == 0)
                            {
                                flag4 = true;
                            }
                            num2++;
                        }
                    }
                }
            }
            rowArray2 = array;
            num = 0;
            while (true)
            {
                while (true)
                {
                    if (num >= rowArray2.Length)
                    {
                        if (!flag)
                        {
                            this.SetAlternativeKey(schemaTable);
                        }
                        else if (this.a)
                        {
                            using (IEnumerator enumerator = schemaTable.Rows.GetEnumerator())
                            {
                                while (enumerator.MoveNext())
                                {
                                    ((DataRow) enumerator.Current)[SchemaTableColumn.IsUnique] = false;
                                }
                            }
                        }
                        column.ReadOnly = true;
                        column2.ReadOnly = true;
                        column3.ReadOnly = true;
                        column4.ReadOnly = true;
                        column5.ReadOnly = readOnly;
                        column6.ReadOnly = flag3;
                        return schemaTable;
                    }
                    DataRow schemaRow = rowArray2[num];
                    schemaRow["AutoincrementColumnNative"] = schemaRow[SchemaTableOptionalColumn.IsAutoIncrement];
                    if (!Utils.IsEmpty(base.UpdatingTable))
                    {
                        string source = Utils.ObjectToString(schemaRow[SchemaTableColumn.BaseTableName]);
                        if (base.UpdatingTable.IndexOf('.') != -1)
                        {
                            source = this.GetFullTableName(schemaRow);
                        }
                        string[] excludeStrings = new string[] { base.QuotePrefix, base.QuoteSuffix };
                        if (!Utils.CompareObjectNameSuffix(source, base.UpdatingTable, true, excludeStrings))
                        {
                            schemaTable.Rows.Remove(schemaRow);
                            break;
                        }
                    }
                    string str3 = Utils.ObjectToString(schemaRow[SchemaTableColumn.ColumnName]);
                    if ((this.updatingFieldsList != null) && (this.updatingFieldsList.Length != 0))
                    {
                        bool flag5 = false;
                        updatingFieldsList = this.updatingFieldsList;
                        num2 = 0;
                        while (true)
                        {
                            if (num2 < updatingFieldsList.Length)
                            {
                                string name = updatingFieldsList[num2];
                                if (!Utils.Compare(base.UnQuoteName(name), str3))
                                {
                                    num2++;
                                    continue;
                                }
                                flag5 = true;
                            }
                            schemaRow[SchemaTableOptionalColumn.IsAutoIncrement] = !flag5;
                            break;
                        }
                    }
                    if (base.keyFieldsList != null)
                    {
                        bool flag6 = false;
                        updatingFieldsList = base.keyFieldsList;
                        num2 = 0;
                        while (true)
                        {
                            if (num2 < updatingFieldsList.Length)
                            {
                                string name = updatingFieldsList[num2];
                                if (!Utils.Compare(base.UnQuoteName(name), str3))
                                {
                                    num2++;
                                    continue;
                                }
                                flag6 = true;
                            }
                            schemaRow[column] = flag6;
                            break;
                        }
                    }
                    if ((schemaRow[column] as bool) && ((bool) schemaRow[column]))
                    {
                        flag = true;
                    }
                    if (!this.UseSchema)
                    {
                        schemaRow[column5] = string.Empty;
                        schemaRow[column6] = string.Empty;
                    }
                    else if (!this.UseCatalog)
                    {
                        schemaRow[column6] = string.Empty;
                    }
                    break;
                }
                num++;
            }
        }

        protected abstract bool IsValidQuote(string quote, bool prefix);
        public override string QuoteIdentifier(string unquotedIdentifier)
        {
            Utils.CheckArgumentNull(unquotedIdentifier, "unquotedIdentifier");
            if (!this.Quoted)
            {
                return unquotedIdentifier;
            }
            StringBuilder builder1 = new StringBuilder();
            builder1.Append(this.QuotePrefix);
            builder1.Append(unquotedIdentifier);
            builder1.Append(this.QuoteSuffix);
            return builder1.ToString();
        }

        protected void RowUpdatingHandler(object sender, RowUpdatingEventArgs ruevent)
        {
            this.BeforeCreatingCommand();
            try
            {
                base.RowUpdatingHandler(ruevent);
                if (Utils.MonoDetected)
                {
                    this.CorrectParameterPrefixes((DbCommand) ruevent.Command);
                }
            }
            finally
            {
                this.AfterCreatingCommand();
            }
        }

        protected virtual void SetAlternativeKey(DataTable schemaTable)
        {
            DataColumn column = schemaTable.Columns[SchemaTableColumn.IsKey];
            foreach (DataRow row in schemaTable.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    row[column] = true;
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override string QuotePrefix
        {
            get => 
                this.Quoted ? base.QuotePrefix : string.Empty;
            set
            {
                if ((value == string.Empty) || this.IsValidQuote(value, true))
                {
                    base.QuotePrefix = value;
                }
                else if (throwOnInvalidQuote)
                {
                    throw new ArgumentException($"'{value}' is not acceptable value for the property 'QuotePrefix'.");
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override string QuoteSuffix
        {
            get => 
                this.Quoted ? base.QuoteSuffix : string.Empty;
            set
            {
                if ((value == string.Empty) || this.IsValidQuote(value, false))
                {
                    base.QuoteSuffix = value;
                }
                else if (throwOnInvalidQuote)
                {
                    throw new ArgumentException($"'{value}' is not acceptable value for the property 'QuoteSuffix'.");
                }
            }
        }

        [Category("Update"), MergableProperty(false), y("DbCommandBuilder_UpdatingFields")]
        public abstract string UpdatingFields { get; set; }
    }
}

