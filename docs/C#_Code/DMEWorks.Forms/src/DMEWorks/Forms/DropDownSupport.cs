namespace DMEWorks.Forms
{
    using DMEWorks.Expressions;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public static class DropDownSupport
    {
        private const char quote = '\'';
        private const char slash = '\\';
        private const char lbracket = '[';
        private const char rbracket = ']';
        private const char asterisk = '*';
        private const char percent = '%';

        public static string BuildFilter(DataTable table, string searchString, StringFilterType type)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            if (searchString == null)
            {
                throw new ArgumentNullException("searchString");
            }
            List<ColumnExpression> list = new List<ColumnExpression>();
            foreach (DataColumn column in table.Columns)
            {
                list.Add(new ColumnExpression(column.ColumnName, column.DataType));
            }
            return ColumnExpressionVisitor.ExpressionToString(DMEWorks.Expressions.Utilities.BuildFilter((IEnumerable<Expression>) list, searchString), type);
        }

        internal static string QuoteLike(string value, StringFilterType type)
        {
            char ch;
            int num = (value != null) ? value.Length : 0;
            StringBuilder builder = new StringBuilder((2 + (3 * num)) + 2);
            builder.Append('\'');
            if ((type == StringFilterType.Contains) || (type == StringFilterType.EndWith))
            {
                builder.Append('%');
            }
            int num2 = 0;
            goto TR_000D;
        TR_0003:
            num2++;
        TR_000D:
            while (true)
            {
                if (num2 >= num)
                {
                    if ((type == StringFilterType.Contains) || (type == StringFilterType.StartWith))
                    {
                        builder.Append('%');
                    }
                    builder.Append('\'');
                    return builder.ToString();
                }
                ch = value[num2];
                if (ch > '\'')
                {
                    if ((ch != '*') && ((ch != '[') && (ch != ']')))
                    {
                        break;
                    }
                }
                else if (ch != '%')
                {
                    if (ch != '\'')
                    {
                        break;
                    }
                    builder.Append('\'').Append('\'');
                    goto TR_0003;
                }
                builder.Append('[').Append(ch).Append(']');
                goto TR_0003;
            }
            builder.Append(ch);
            goto TR_0003;
        }

        public static string QuoteName(string value)
        {
            int num = (value != null) ? value.Length : 0;
            StringBuilder builder = new StringBuilder((1 + (2 * num)) + 1);
            builder.Append('[');
            for (int i = 0; i < num; i++)
            {
                char ch = value[i];
                if (ch == ']')
                {
                    builder.Append('\\').Append(']');
                }
                else
                {
                    builder.Append(ch);
                }
            }
            builder.Append(']');
            return builder.ToString();
        }

        public static string QuoteString(string value)
        {
            int num = (value != null) ? value.Length : 0;
            StringBuilder builder = new StringBuilder((1 + (2 * num)) + 1);
            builder.Append('\'');
            for (int i = 0; i < num; i++)
            {
                char ch = value[i];
                if (ch == '\'')
                {
                    builder.Append('\'').Append('\'');
                }
                else
                {
                    builder.Append(ch);
                }
            }
            builder.Append('\'');
            return builder.ToString();
        }

        public enum StringFilterType
        {
            StartWith,
            EndWith,
            Contains
        }
    }
}

