namespace DevExpress.Xpo.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public class SimpleSqlParser
    {
        private const string AsDelimiterString = " as ";
        private string sql;
        private List<string> result = new List<string>();
        private StringBuilder curColumn = new StringBuilder();
        private int inBrackets;
        private bool inQuotes;
        private bool inDoubleQuotes;
        private bool firstQuote;
        private bool quoteEscaped;
        private char chr;
        private char? nextChr;
        private char? prevChr;

        private SimpleSqlParser(string sql)
        {
            this.sql = sql;
        }

        private void Appender(bool append)
        {
            if (append)
            {
                this.curColumn.Append(this.chr);
            }
            else
            {
                if (this.chr != ',')
                {
                    this.curColumn.Append(this.chr);
                }
                this.result.Add(this.curColumn.ToString().Trim());
                this.curColumn.Remove(0, this.curColumn.Length);
            }
        }

        private bool ClosingBracket()
        {
            if (this.inDoubleQuotes)
            {
                if (this.nextChr == null)
                {
                    throw new FormatException("Statement not finished.");
                }
                return true;
            }
            if (this.inQuotes)
            {
                if (this.nextChr == null)
                {
                    throw new FormatException("Statement not finished.");
                }
                return true;
            }
            if (this.inBrackets < 1)
            {
                throw new FormatException("Statement not finished.");
            }
            this.inBrackets--;
            if (this.nextChr != null)
            {
                return true;
            }
            if (this.inBrackets != 0)
            {
                throw new FormatException("Statement not finished.");
            }
            return false;
        }

        private bool Comma()
        {
            if (this.nextChr == null)
            {
                throw new FormatException("Statement not finished.");
            }
            return ((this.inBrackets == 0) ? (!this.inQuotes ? this.inDoubleQuotes : true) : true);
        }

        private bool Default()
        {
            if (this.firstQuote)
            {
                this.firstQuote = false;
            }
            return (this.nextChr != null);
        }

        private bool DoubleQuotes()
        {
            if (this.inQuotes)
            {
                if (this.nextChr == null)
                {
                    throw new FormatException("Statement not finished.");
                }
                return true;
            }
            if (!this.inDoubleQuotes)
            {
                this.inDoubleQuotes = true;
                this.firstQuote = true;
            }
            else
            {
                this.quoteEscaped = !this.quoteEscaped;
                if (this.firstQuote)
                {
                    this.firstQuote = false;
                    if ((this.nextChr == null) || (this.nextChr.Value != '"'))
                    {
                        this.inDoubleQuotes = false;
                        this.quoteEscaped = false;
                    }
                }
                else if (this.quoteEscaped)
                {
                    int? nullable1;
                    char? nextChr = this.nextChr;
                    if (nextChr != null)
                    {
                        nullable1 = new int?(nextChr.GetValueOrDefault());
                    }
                    else
                    {
                        nullable1 = null;
                    }
                    int? nullable = nullable1;
                    int num = 0x22;
                    if ((nullable.GetValueOrDefault() == num) ? (nullable == null) : true)
                    {
                        this.inDoubleQuotes = false;
                        this.quoteEscaped = false;
                    }
                }
            }
            if (this.inDoubleQuotes && (this.nextChr == null))
            {
                throw new FormatException("Statement not finished.");
            }
            return (this.nextChr != null);
        }

        private string[] GetColumns()
        {
            for (int i = 0; i < this.sql.Length; i++)
            {
                this.chr = this.sql[i];
                this.nextChr = null;
                this.prevChr = null;
                if ((i + 1) < this.sql.Length)
                {
                    this.nextChr = new char?(this.sql[i + 1]);
                }
                if ((i - 1) > -1)
                {
                    this.prevChr = new char?(this.sql[i - 1]);
                }
                bool append = false;
                char chr = this.chr;
                if (chr == '"')
                {
                    append = this.DoubleQuotes();
                }
                else
                {
                    switch (chr)
                    {
                        case '\'':
                            append = this.Quote();
                            break;

                        case '(':
                            append = this.OpeningBracket();
                            break;

                        case ')':
                            append = this.ClosingBracket();
                            break;

                        case ',':
                            append = this.Comma();
                            break;

                        default:
                            append = this.Default();
                            break;
                    }
                }
                this.Appender(append);
            }
            return this.result.ToArray();
        }

        public static string[] GetColumns(string sql) => 
            new SimpleSqlParser(sql).GetColumns();

        public static StringBuilder GetExpandedProperties(string[] properties, string expandingAlias)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < properties.Length; i++)
            {
                if (!string.IsNullOrEmpty(properties[i].Trim()))
                {
                    string str = properties[i];
                    string str2 = null;
                    int startIndex = str.LastIndexOf(" as ");
                    if (startIndex > 0)
                    {
                        string str3 = str.Substring(startIndex + " as ".Length).Trim();
                        if (!str3.Contains(" "))
                        {
                            str = str.Remove(startIndex);
                            str2 = str3;
                        }
                    }
                    if (!string.IsNullOrEmpty(str2))
                    {
                        if (i > 0)
                        {
                            builder.Append(", ");
                        }
                        builder.Append(expandingAlias);
                        builder.Append(".");
                        builder.Append(str2);
                    }
                    else
                    {
                        object[] args = new object[] { str, i };
                        properties[i] = string.Format(CultureInfo.InvariantCulture, "{0} as F{1}", args);
                        if (i > 0)
                        {
                            builder.Append(", ");
                        }
                        builder.Append(expandingAlias);
                        builder.Append(".F");
                        builder.Append(i.ToString(CultureInfo.InvariantCulture));
                    }
                }
            }
            return builder;
        }

        private bool OpeningBracket()
        {
            if (this.nextChr == null)
            {
                throw new FormatException("Statement not finished.");
            }
            if (this.inDoubleQuotes)
            {
                if (this.nextChr == null)
                {
                    throw new FormatException("Statement not finished.");
                }
                return true;
            }
            if (!this.inQuotes)
            {
                this.inBrackets++;
                return true;
            }
            if (this.nextChr == null)
            {
                throw new FormatException("Statement not finished.");
            }
            return true;
        }

        private bool Quote()
        {
            if (this.inDoubleQuotes)
            {
                if (this.nextChr == null)
                {
                    throw new FormatException("Statement not finished.");
                }
                return true;
            }
            if (!this.inQuotes)
            {
                this.inQuotes = true;
                this.firstQuote = true;
            }
            else
            {
                this.quoteEscaped = !this.quoteEscaped;
                if (this.firstQuote)
                {
                    this.firstQuote = false;
                    if ((this.nextChr == null) || (this.nextChr.Value != '\''))
                    {
                        this.inQuotes = false;
                        this.quoteEscaped = false;
                    }
                }
                else if (this.quoteEscaped)
                {
                    int? nullable1;
                    char? nextChr = this.nextChr;
                    if (nextChr != null)
                    {
                        nullable1 = new int?(nextChr.GetValueOrDefault());
                    }
                    else
                    {
                        nullable1 = null;
                    }
                    int? nullable = nullable1;
                    int num = 0x27;
                    if ((nullable.GetValueOrDefault() == num) ? (nullable == null) : true)
                    {
                        this.inQuotes = false;
                        this.quoteEscaped = false;
                    }
                }
            }
            if (this.inQuotes && (this.nextChr == null))
            {
                throw new FormatException("Statement not finished.");
            }
            return (this.nextChr != null);
        }
    }
}

