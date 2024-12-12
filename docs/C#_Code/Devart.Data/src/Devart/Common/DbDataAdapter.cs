namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public abstract class DbDataAdapter : System.Data.Common.DbDataAdapter, IDbDataAdapter
    {
        protected DbDataAdapter()
        {
        }

        private static bool a(ICollection<string> A_0, string A_1)
        {
            bool flag;
            using (IEnumerator<string> enumerator = A_0.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        string current = enumerator.Current;
                        if (string.Compare(A_1, current, StringComparison.InvariantCultureIgnoreCase) != 0)
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        internal int a(DataTable A_0, IDataReader A_1) => 
            this.LoadTable(A_0, (DbDataReader) A_1);

        protected static bool CheckMissingSchemaAction(string fieldName, string tableName, MissingSchemaAction missingSchemaAction)
        {
            switch (missingSchemaAction)
            {
                case MissingSchemaAction.Add:
                case MissingSchemaAction.AddWithKey:
                    return false;

                case MissingSchemaAction.Ignore:
                    return true;

                case MissingSchemaAction.Error:
                    throw new InvalidOperationException($"Missing the DataColumn {fieldName} in the DataTable {tableName}.");
            }
            return false;
        }

        protected virtual void CustomizeDataTableColumns(DataTable[] tables)
        {
            DataTable[] tableArray = tables;
            for (int i = 0; i < tableArray.Length; i++)
            {
                using (IEnumerator enumerator = tableArray[i].Columns.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        ((DataColumn) enumerator.Current).MaxLength = -1;
                    }
                }
            }
        }

        public int FillPage(DataTable dataTable, int startRecord, int maxRecords)
        {
            int num;
            CommandBehavior closeConnection = CommandBehavior.Default;
            DbCommandBase selectCommand = (DbCommandBase) base.SelectCommand;
            if ((selectCommand.Connection != null) && (selectCommand.Connection.State == ConnectionState.Closed))
            {
                selectCommand.Connection.Open();
                closeConnection = CommandBehavior.CloseConnection;
            }
            DbDataReader dataReader = selectCommand.ExecutePageReader(closeConnection, startRecord, maxRecords);
            try
            {
                num = this.LoadTable(dataTable, dataReader);
            }
            finally
            {
                dataReader.Close();
            }
            return num;
        }

        protected override DataTable FillSchema(DataTable dataTable, SchemaType schemaType, IDataReader dataReader)
        {
            DataTable table = base.FillSchema(dataTable, schemaType, dataReader);
            DataTable[] tables = new DataTable[] { dataTable };
            this.CustomizeDataTableColumns(tables);
            return table;
        }

        protected override DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable, IDataReader dataReader)
        {
            DataTable[] tables = base.FillSchema(dataSet, schemaType, srcTable, dataReader);
            this.CustomizeDataTableColumns(tables);
            return tables;
        }

        protected static string[] GetIndexedFieldNames(ICollection<string> fieldNames) => 
            GetIndexedFieldNames(fieldNames, true);

        protected static string[] GetIndexedFieldNames(ICollection<string> fieldNames, bool firstColumnBugCompatibleMode)
        {
            int count = fieldNames.Count;
            List<string> list = new List<string>(fieldNames);
            string[] strArray = new string[count];
            List<KeyValuePair<string, int>> list2 = new List<KeyValuePair<string, int>>(count / 2);
            for (int i = 0; i < count; i++)
            {
                string st = list[i];
                strArray[i] = st;
                if (Utils.IsEmpty(st))
                {
                    list2.Add(new KeyValuePair<string, int>(st, i));
                }
                else
                {
                    for (int j = i + 1; j < count; j++)
                    {
                        if (string.Compare(st, list[j], StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            list2.Add(new KeyValuePair<string, int>(firstColumnBugCompatibleMode ? st : list[j], j));
                            break;
                        }
                    }
                }
            }
            foreach (KeyValuePair<string, int> pair in list2)
            {
                string key = pair.Key;
                string str4 = key;
                int num4 = 1;
                if (Utils.IsEmpty(key))
                {
                    key = "Column";
                    str4 = "Column1";
                    num4 = 2;
                }
                int index = pair.Value;
                while (true)
                {
                    if (!a(list, str4))
                    {
                        strArray[index] = str4;
                        list.Add(str4);
                        break;
                    }
                    str4 = key + num4;
                    num4++;
                }
            }
            return strArray;
        }

        protected virtual int LoadTable(DataTable table, DbDataReader dataReader) => 
            this.Fill(table, dataReader);
    }
}

