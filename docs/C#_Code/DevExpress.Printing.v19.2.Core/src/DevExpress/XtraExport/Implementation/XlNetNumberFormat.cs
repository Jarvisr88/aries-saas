namespace DevExpress.XtraExport.Implementation
{
    using System;
    using System.Runtime.CompilerServices;

    internal class XlNetNumberFormat
    {
        private string formatString = string.Empty;

        public override bool Equals(object obj)
        {
            XlNetNumberFormat format = obj as XlNetNumberFormat;
            return ((format != null) ? ((this.IsDateTimeFormat == format.IsDateTimeFormat) && string.Equals(this.FormatString, format.FormatString)) : false);
        }

        public override int GetHashCode() => 
            this.formatString.GetHashCode() ^ this.IsDateTimeFormat.GetHashCode();

        public string FormatString
        {
            get => 
                this.formatString;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.formatString = string.Empty;
                }
                else
                {
                    this.formatString = value;
                }
            }
        }

        public bool IsDateTimeFormat { get; set; }
    }
}

