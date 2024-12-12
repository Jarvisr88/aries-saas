namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using System;
    using System.Text;

    internal class FormatStringParser
    {
        private const char OpenBrace = '{';
        private const char ClosedBrace = '}';
        private const char Zero = '0';
        private const char One = '1';
        private const char Colon = ':';
        private const char MPostfix = 'M';
        private const string DefaultFormatString = "0";
        private string prefix;
        private string valueFormat;
        private string postfix;
        private string unionString;
        private AutomatStates previousState;

        public FormatStringParser(string itemFormat, string itemFieldName)
        {
            if (!string.IsNullOrEmpty(itemFormat))
            {
                if (itemFormat.Length < 3)
                {
                    this.valueFormat = itemFormat;
                }
                else
                {
                    this.TryParse(itemFormat, itemFieldName);
                }
            }
        }

        private string CreateUnionString()
        {
            StringBuilder builder = new StringBuilder(string.Empty);
            builder.Append(this.prefix);
            builder.Append(this.valueFormat);
            builder.Append(this.postfix);
            return builder.ToString();
        }

        private bool IsCharValid(char itemFormatCh) => 
            !char.IsWhiteSpace(itemFormatCh);

        private void TryParse(string itemFormat, string itemFieldName)
        {
            AutomatStates pREFIX = AutomatStates.PREFIX;
            char ch = '\0';
            for (int i = 0; i < itemFormat.Length; i++)
            {
                char itemFormatCh = itemFormat[i];
                switch (pREFIX)
                {
                    case AutomatStates.PREFIX:
                        if (itemFormatCh > '1')
                        {
                            if (itemFormatCh == '{')
                            {
                                ch = '{';
                                break;
                            }
                            if (itemFormatCh == '}')
                            {
                                break;
                            }
                        }
                        else
                        {
                            if (itemFormatCh == '0')
                            {
                                if (ch == '{')
                                {
                                    pREFIX = AutomatStates.FORMAT;
                                }
                                else
                                {
                                    this.prefix = this.prefix + itemFormatCh.ToString();
                                }
                                break;
                            }
                            if (itemFormatCh == '1')
                            {
                                this.prefix = (ch != '{') ? (this.prefix + itemFormatCh.ToString()) : (this.prefix + itemFieldName);
                                break;
                            }
                        }
                        this.prefix = this.prefix + itemFormatCh.ToString();
                        break;

                    case AutomatStates.FORMAT:
                        if (itemFormatCh == ':')
                        {
                            if (this.previousState != AutomatStates.PREFIX)
                            {
                                this.valueFormat = this.valueFormat + itemFormatCh.ToString();
                            }
                        }
                        else if (itemFormatCh != 'M')
                        {
                            if (itemFormatCh != '}')
                            {
                                this.valueFormat = this.valueFormat + itemFormatCh.ToString();
                            }
                            else
                            {
                                pREFIX = AutomatStates.POSTFIX;
                            }
                        }
                        else if (!string.IsNullOrEmpty(this.prefix))
                        {
                            this.postfix = this.postfix + itemFormatCh.ToString();
                        }
                        else
                        {
                            this.valueFormat = this.valueFormat + itemFormatCh.ToString();
                        }
                        if (this.IsCharValid(itemFormatCh))
                        {
                            this.previousState = AutomatStates.FORMAT;
                        }
                        break;

                    case AutomatStates.POSTFIX:
                        if (itemFormatCh == ':')
                        {
                            pREFIX = AutomatStates.PREFIX;
                        }
                        else
                        {
                            this.postfix = this.postfix + itemFormatCh.ToString();
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        public string Prefix =>
            string.IsNullOrEmpty(this.prefix) ? string.Empty : this.prefix;

        public string ValueFormat =>
            string.IsNullOrEmpty(this.valueFormat) ? "0" : this.valueFormat;

        public string Postfix =>
            string.IsNullOrEmpty(this.postfix) ? string.Empty : this.postfix;

        internal string UnionString
        {
            get
            {
                this.unionString ??= this.CreateUnionString();
                return this.unionString;
            }
        }

        private enum AutomatStates
        {
            PREFIX,
            FORMAT,
            POSTFIX
        }
    }
}

