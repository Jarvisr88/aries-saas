namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpo.DB;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;
    using System.Text;

    public class QueryStatementToStringFormatter : ISqlGeneratorFormatterSupportSkipTake, ISqlGeneratorFormatter
    {
        public static QueryStatementToStringFormatter Instance = new QueryStatementToStringFormatter();

        private QueryStatementToStringFormatter()
        {
        }

        string ISqlGeneratorFormatter.ComposeSafeColumnName(string columnName) => 
            columnName;

        string ISqlGeneratorFormatter.ComposeSafeSchemaName(string tableName) => 
            string.Empty;

        string ISqlGeneratorFormatter.ComposeSafeTableName(string tableName) => 
            tableName;

        string ISqlGeneratorFormatter.FormatBinary(BinaryOperatorType operatorType, string leftOperand, string rightOperand)
        {
            string[] textArray1 = new string[] { operatorType.ToString(), "(", leftOperand, ",", rightOperand, ")" };
            return string.Concat(textArray1);
        }

        string ISqlGeneratorFormatter.FormatColumn(string columnName) => 
            FormatEscape(columnName);

        string ISqlGeneratorFormatter.FormatColumn(string columnName, string tableAlias) => 
            FormatEscape(tableAlias) + "." + FormatEscape(columnName);

        string ISqlGeneratorFormatter.FormatDelete(string tableName, string whereClause)
        {
            object[] args = new object[] { tableName, whereClause };
            return string.Format(CultureInfo.InvariantCulture, "delete from {0} where {1}", args);
        }

        string ISqlGeneratorFormatter.FormatFunction(FunctionOperatorType operatorType, params string[] operands)
        {
            string str = operatorType.ToString() + "(";
            bool flag = true;
            foreach (string str2 in operands)
            {
                if (flag)
                {
                    flag = false;
                }
                else
                {
                    str = str + ",";
                }
                str = str + str2;
            }
            return str;
        }

        string ISqlGeneratorFormatter.FormatInsert(string tableName, string fields, string values)
        {
            object[] args = new object[] { tableName, fields, values };
            return string.Format(CultureInfo.InvariantCulture, "insert into {0}({1}) values({2})", args);
        }

        string ISqlGeneratorFormatter.FormatInsertDefaultValues(string tableName)
        {
            object[] args = new object[] { tableName };
            return string.Format(CultureInfo.InvariantCulture, "insert default values into {0}", args);
        }

        string ISqlGeneratorFormatter.FormatOrder(string sortProperty, SortingDirection direction)
        {
            object[] args = new object[] { sortProperty, (direction == SortingDirection.Ascending) ? "asc" : "desc" };
            return string.Format(CultureInfo.InvariantCulture, "{0} {1}", args);
        }

        string ISqlGeneratorFormatter.FormatSelect(string selectedPropertiesSql, string fromSql, string whereSql, string orderBySql, string groupBySql, string havingSql, int topSelectedRecords)
        {
            string str = string.Empty;
            if (topSelectedRecords != 0)
            {
                str = " top " + topSelectedRecords.ToString() + " ";
            }
            object[] args = new object[] { str, selectedPropertiesSql, fromSql, whereSql, orderBySql, groupBySql, havingSql };
            return string.Format(CultureInfo.InvariantCulture, "select{0}({1}) from({2}) where({3}) order({4}) group({5}) having({6})", args);
        }

        string ISqlGeneratorFormatter.FormatTable(string schema, string tableName)
        {
            string str = FormatEscape(tableName);
            return (string.IsNullOrEmpty(schema) ? str : (FormatEscape(schema) + "." + str));
        }

        string ISqlGeneratorFormatter.FormatTable(string schema, string tableName, string tableAlias)
        {
            string str = FormatEscape(tableName) + "." + FormatEscape(tableAlias);
            return (string.IsNullOrEmpty(schema) ? str : (FormatEscape(schema) + "." + str));
        }

        string ISqlGeneratorFormatter.FormatUnary(UnaryOperatorType operatorType, string operand) => 
            operatorType.ToString() + "(" + operand + ")";

        string ISqlGeneratorFormatter.FormatUpdate(string tableName, string sets, string whereClause)
        {
            object[] args = new object[] { tableName, sets, whereClause };
            return string.Format(CultureInfo.InvariantCulture, "update {0} {1} where {2}", args);
        }

        string ISqlGeneratorFormatter.GetParameterName(OperandValue parameter, int index, ref bool createPrameter) => 
            CacheParameterNames.Instance[index];

        string ISqlGeneratorFormatterSupportSkipTake.FormatSelect(string selectedPropertiesSql, string fromSql, string whereSql, string orderBySql, string groupBySql, string havingSql, int skipSelectedRecords, int topSelectedRecords)
        {
            string str = string.Empty;
            if (skipSelectedRecords != 0)
            {
                str = str + " skip " + skipSelectedRecords.ToString(CultureInfo.InvariantCulture);
            }
            if (topSelectedRecords != 0)
            {
                str = str + " top " + topSelectedRecords.ToString(CultureInfo.InvariantCulture);
            }
            object[] args = new object[] { str, selectedPropertiesSql, fromSql, whereSql, orderBySql, groupBySql, havingSql };
            return string.Format(CultureInfo.InvariantCulture, "select{0} ({1}) from({2}) where({3}) order({4}) group({5}) having({6})", args);
        }

        private static string FormatConstants(Dictionary<int, OperandValue> constantValues)
        {
            if (constantValues == null)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            List<int> list = new List<int>(constantValues.Keys);
            list.Sort();
            for (int i = 0; i < list.Count; i++)
            {
                if (builder.Length != 0)
                {
                    builder.Append(',');
                }
                OperandValue value2 = constantValues[list[i]];
                if ((value2 != null) && ((value2.Value != null) && !(value2.Value is DBNull)))
                {
                    builder.Append(FormatEscape(value2.ToString()));
                }
            }
            return builder.ToString();
        }

        private static string FormatEscape(string escaped) => 
            "'" + escaped.Replace("'", "''") + "'";

        private static string FormatParameters(QueryParameterCollection parameters)
        {
            StringBuilder builder = new StringBuilder();
            foreach (OperandValue value2 in parameters)
            {
                if (builder.Length != 0)
                {
                    builder.Append(',');
                }
                if ((value2.Value != null) && !(value2.Value is DBNull))
                {
                    builder.Append(FormatEscape(value2.Value.ToString()));
                }
            }
            return builder.ToString();
        }

        public static string GetString(SelectStatement select)
        {
            Query query = new SelectSqlGenerator(Instance).GenerateSql(select);
            string str = FormatParameters(query.Parameters);
            string str2 = FormatConstants(query.ConstantValues);
            string[] textArray1 = new string[] { query.Sql, " params(", str, ") constants(", str2, ")" };
            return string.Concat(textArray1);
        }

        public static string GetString(ModificationStatement statement, TaggedParametersHolder identities)
        {
            Query query;
            if (statement is InsertStatement)
            {
                query = new InsertSqlGenerator(Instance, identities, new Dictionary<OperandValue, string>()).GenerateSql(statement);
            }
            else if (statement is UpdateStatement)
            {
                query = new UpdateSqlGenerator(Instance, identities, new Dictionary<OperandValue, string>()).GenerateSql(statement);
            }
            else
            {
                if (!(statement is DeleteStatement))
                {
                    throw new NotImplementedException();
                }
                query = new DeleteSqlGenerator(Instance, identities, new Dictionary<OperandValue, string>()).GenerateSql(statement);
            }
            string str = FormatParameters(query.Parameters);
            string str2 = FormatConstants(query.ConstantValues);
            string[] textArray1 = new string[] { query.Sql, " params(", str, ") constants(", str2, ")" };
            return string.Concat(textArray1);
        }

        bool ISqlGeneratorFormatter.BraceJoin =>
            true;

        bool ISqlGeneratorFormatter.SupportNamedParameters =>
            false;

        bool ISqlGeneratorFormatterSupportSkipTake.NativeSkipTakeSupported =>
            true;

        private class CacheParameterNames : IList, ICollection, IEnumerable
        {
            public static readonly QueryStatementToStringFormatter.CacheParameterNames Instance = new QueryStatementToStringFormatter.CacheParameterNames();

            private CacheParameterNames()
            {
            }

            void ICollection.CopyTo(Array array, int index)
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }

            int IList.Add(object value)
            {
                throw new NotImplementedException();
            }

            void IList.Clear()
            {
                throw new NotImplementedException();
            }

            bool IList.Contains(object value)
            {
                throw new NotImplementedException();
            }

            int IList.IndexOf(object value)
            {
                throw new NotImplementedException();
            }

            void IList.Insert(int index, object value)
            {
                throw new NotImplementedException();
            }

            void IList.Remove(object value)
            {
                throw new NotImplementedException();
            }

            void IList.RemoveAt(int index)
            {
                throw new NotImplementedException();
            }

            int ICollection.Count
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            bool ICollection.IsSynchronized =>
                false;

            object ICollection.SyncRoot =>
                this;

            bool IList.IsFixedSize =>
                false;

            bool IList.IsReadOnly =>
                true;

            object IList.this[int index]
            {
                get => 
                    this[index];
                set
                {
                    throw new NotImplementedException();
                }
            }

            public string this[int index]
            {
                get => 
                    "?";
                set
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}

