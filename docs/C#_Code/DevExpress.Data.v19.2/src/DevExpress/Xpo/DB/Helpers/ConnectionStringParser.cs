namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text;

    public sealed class ConnectionStringParser
    {
        private const string DoubleQuotesString = "\"";
        private const char DoubleQuotesChar = '"';
        private const string DoubleQuotesInValueString = "\"\"";
        private const string SingleQuoteString = "'";
        private const char SingleQuoteChar = '\'';
        private const string SingleQuoteInValueString = "''";
        private Dictionary<string, ValuePair> propertyTable;

        public ConnectionStringParser()
        {
            this.propertyTable = new Dictionary<string, ValuePair>(StringExtensions.ComparerInvariantCultureIgnoreCase);
        }

        public ConnectionStringParser(string connectionString) : this()
        {
            foreach (string str in this.ExtractParts(connectionString))
            {
                int index = str.IndexOf("=");
                if (index != -1)
                {
                    string key = str.Substring(0, index).Trim();
                    string originalValue = str.Substring(index + 1);
                    this.propertyTable.Add(key, new ValuePair(originalValue, UnescapeArgument(originalValue.Trim())));
                }
            }
        }

        public void AddPart(string partName, string partValue)
        {
            this.propertyTable.Add(partName, new ValuePair(partValue, partValue));
        }

        public static string EscapeArgument(string value) => 
            !string.IsNullOrEmpty(value) ? ((IsStringDoubleQuoted(value) || IsStringSingleQuoted(value)) ? value : ((value.Contains(";") || (value.Contains(" ") || (value.Contains("'") || value.Contains("\"")))) ? ("\"" + value.Replace("\"", "\"\"") + "\"") : value)) : value;

        private static string EscapeArgument(string originalValue, string value) => 
            !string.IsNullOrEmpty(originalValue) ? ((originalValue == value) ? EscapeArgument(originalValue) : originalValue) : originalValue;

        private string[] ExtractParts(string connectionString)
        {
            List<string> list = new List<string>();
            int length = connectionString.Length;
            int startIndex = 0;
            InValueState none = InValueState.None;
            bool flag = false;
            for (int i = 0; i < length; i++)
            {
                char ch = connectionString[i];
                if (ch == '"')
                {
                    if (((none == InValueState.InDoubleQuotedValue) && ((i + 1) < length)) && connectionString[i + 1].Equals('"'))
                    {
                        i++;
                    }
                    else
                    {
                        if (none == InValueState.InDoubleQuotedValue)
                        {
                            flag = true;
                            none = InValueState.None;
                        }
                        if ((none == InValueState.None) && !flag)
                        {
                            none = InValueState.InDoubleQuotedValue;
                        }
                    }
                }
                else if (ch != '\'')
                {
                    if ((ch == ';') && (none == InValueState.None))
                    {
                        list.Add(connectionString.Substring(startIndex, i - startIndex));
                        startIndex = i + 1;
                        flag = false;
                    }
                }
                else if (((none == InValueState.InSingleQuotedValue) && ((i + 1) < length)) && connectionString[i + 1].Equals('\''))
                {
                    i++;
                }
                else
                {
                    if (none == InValueState.InSingleQuotedValue)
                    {
                        flag = true;
                        none = InValueState.None;
                    }
                    if ((none == InValueState.None) && !flag)
                    {
                        none = InValueState.InSingleQuotedValue;
                    }
                }
            }
            if (startIndex < length)
            {
                string str2 = connectionString.Substring(startIndex, length - startIndex);
                list.Add((none == InValueState.InDoubleQuotedValue) ? (str2 + "\"") : ((none == InValueState.InSingleQuotedValue) ? (str2 + "'") : str2));
            }
            return list.ToArray();
        }

        public string GetConnectionString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, ValuePair> pair in this.propertyTable)
            {
                object[] args = new object[] { pair.Key, EscapeArgument(pair.Value.OriginalValue, pair.Value.Value) };
                builder.AppendFormat(CultureInfo.InvariantCulture, "{0}={1}", args);
                builder.Append(";");
            }
            return builder.ToString();
        }

        public string GetOriginalPartByName(string partName)
        {
            ValuePair pair;
            return (this.propertyTable.TryGetValue(partName, out pair) ? pair.OriginalValue : string.Empty);
        }

        public string GetPartByName(string partName)
        {
            ValuePair pair;
            return (this.propertyTable.TryGetValue(partName, out pair) ? pair.Value : string.Empty);
        }

        private static bool IsStringDoubleQuoted(string value) => 
            (value.Length > 1) && (value[0].Equals('"') && value[value.Length - 1].Equals('"'));

        private static bool IsStringSingleQuoted(string value) => 
            (value.Length > 1) && (value[0].Equals('\'') && value[value.Length - 1].Equals('\''));

        public bool PartExists(string partName) => 
            this.propertyTable.ContainsKey(partName);

        public void RemovePartByName(string partName)
        {
            this.propertyTable.Remove(partName);
        }

        private static string UnescapeArgument(string value) => 
            !string.IsNullOrEmpty(value) ? (!IsStringDoubleQuoted(value) ? (!IsStringSingleQuoted(value) ? value : value.Substring(1, value.Length - 2).Replace("''", "'")) : value.Substring(1, value.Length - 2).Replace("\"\"", "\"")) : value;

        public void UpdatePartByName(string partName, string partValue)
        {
            if (this.propertyTable.ContainsKey(partName))
            {
                this.propertyTable[partName] = new ValuePair(partValue, partValue);
            }
        }

        private enum InValueState
        {
            None,
            InDoubleQuotedValue,
            InSingleQuotedValue
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct ValuePair
        {
            public readonly string OriginalValue;
            public readonly string Value;
            public ValuePair(string originalValue, string value)
            {
                this.OriginalValue = originalValue;
                this.Value = value;
            }
        }
    }
}

