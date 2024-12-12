namespace DevExpress.Export.Xl
{
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class XlTableReference : IXlFormulaParameter
    {
        private static char[] quotedColumnSymbols = new char[] { '\'', '\\', '[', ']', '@', '#' };

        public XlTableReference(IXlTable table, XlTablePart part)
        {
            Guard.ArgumentNotNull(table, "table");
            this.Table = table;
            this.FirstColumn = string.Empty;
            this.LastColumn = string.Empty;
            this.Part = part;
        }

        public XlTableReference(IXlTable table, string columnName) : this(table, columnName, columnName)
        {
        }

        public XlTableReference(IXlTable table, string columnName, XlTablePart part) : this(table, columnName, columnName, part)
        {
        }

        public XlTableReference(IXlTable table, string firstColumnName, string lastColumnName) : this(table, firstColumnName, lastColumnName, XlTablePart.Any)
        {
        }

        public XlTableReference(IXlTable table, string firstColumnName, string lastColumnName, XlTablePart part)
        {
            this.CheckArguments(table, firstColumnName, lastColumnName, part);
            if (string.IsNullOrEmpty(firstColumnName) && !string.IsNullOrEmpty(lastColumnName))
            {
                firstColumnName = lastColumnName;
            }
            if (!string.IsNullOrEmpty(firstColumnName) && string.IsNullOrEmpty(lastColumnName))
            {
                lastColumnName = firstColumnName;
            }
            this.CheckColumnName(table, firstColumnName);
            this.CheckColumnName(table, lastColumnName);
            this.CheckColumnOrder(table, firstColumnName, lastColumnName);
            this.Table = table;
            this.FirstColumn = firstColumnName;
            this.LastColumn = lastColumnName;
            this.Part = part;
        }

        private void AppendTableColumns(StringBuilder sb, bool partDefined)
        {
            bool flag = this.FirstColumn != this.LastColumn;
            string format = (partDefined | flag) ? "[{0}]" : "{0}";
            sb.AppendFormat(format, QuoteColumnName(this.FirstColumn));
            if (flag)
            {
                sb.Append(':');
                sb.AppendFormat(format, QuoteColumnName(this.LastColumn));
            }
        }

        private void AppendTablePart(StringBuilder sb, bool needBrackets)
        {
            string format = needBrackets ? "[#{0}]" : "#{0}";
            switch (this.Part)
            {
                case XlTablePart.All:
                    sb.AppendFormat(format, "All");
                    return;

                case XlTablePart.Data:
                    sb.AppendFormat(format, "Data");
                    return;

                case XlTablePart.Headers:
                    sb.AppendFormat(format, "Headers");
                    return;

                case XlTablePart.Totals:
                    sb.AppendFormat(format, "Totals");
                    return;

                case XlTablePart.ThisRow:
                    sb.AppendFormat(format, "This Row");
                    return;
            }
        }

        private void CheckArguments(IXlTable table, string firstColumnName, string lastColumnName, XlTablePart part)
        {
            Guard.ArgumentNotNull(table, "table");
            if (string.IsNullOrEmpty(firstColumnName) && (string.IsNullOrEmpty(lastColumnName) && (part == XlTablePart.Any)))
            {
                throw new ArgumentException("Table column(s) or part is not specified.");
            }
        }

        private void CheckColumnName(IXlTable table, string columnName)
        {
            if (!string.IsNullOrEmpty(columnName) && (table.Columns[columnName] == null))
            {
                throw new ArgumentException($"Table '{table.Name} does not contains column '{columnName}'");
            }
        }

        private void CheckColumnOrder(IXlTable table, string firstColumnName, string lastColumnName)
        {
            if (table.Columns.IndexOf(lastColumnName) < table.Columns.IndexOf(firstColumnName))
            {
                throw new ArgumentException("Incorrect column order");
            }
        }

        internal static string QuoteColumnName(string s)
        {
            int num = 0;
            char[] quotedColumnSymbols = XlTableReference.quotedColumnSymbols;
            string str = s;
            while (num < str.Length)
            {
                if (Array.IndexOf<char>(quotedColumnSymbols, str[num]) >= 0)
                {
                    str = str.Insert(num++, "'");
                }
                num++;
            }
            return str;
        }

        public override string ToString() => 
            this.ToString(null);

        public string ToString(CultureInfo culture)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Table.Name);
            bool needBrackets = !string.IsNullOrEmpty(this.FirstColumn);
            bool partDefined = this.Part != XlTablePart.Any;
            if (partDefined | needBrackets)
            {
                sb.Append("[");
                if (partDefined)
                {
                    this.AppendTablePart(sb, needBrackets);
                }
                if (needBrackets)
                {
                    if (partDefined)
                    {
                        sb.Append(',');
                    }
                    this.AppendTableColumns(sb, partDefined);
                }
                sb.Append("]");
            }
            return sb.ToString();
        }

        public IXlTable Table { get; private set; }

        public string FirstColumn { get; private set; }

        public string LastColumn { get; private set; }

        public XlTablePart Part { get; private set; }
    }
}

