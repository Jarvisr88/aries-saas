namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class XlHeaderFooter
    {
        public const string Left = "&L";
        public const string Center = "&C";
        public const string Right = "&R";
        public const string PageNumber = "&P";
        public const string PageTotal = "&N";
        public const string BookName = "&F";
        public const string BookPath = "&Z";
        public const string SheetName = "&A";
        public const string Date = "&D";
        public const string Time = "&T";
        public const string Bold = "&B";
        public const string Italic = "&I";
        public const string Strikethrough = "&S";
        public const string Superscript = "&X";
        public const string Subscript = "&Y";
        public const string Underline = "&U";
        public const string DoubleUnderline = "&E";

        public XlHeaderFooter()
        {
            this.Clear();
        }

        public void Clear()
        {
            this.AlignWithMargins = true;
            this.ScaleWithDoc = true;
            this.DifferentFirst = false;
            this.DifferentOddEven = false;
            this.FirstHeader = string.Empty;
            this.FirstFooter = string.Empty;
            this.EvenHeader = string.Empty;
            this.EvenFooter = string.Empty;
            this.OddHeader = string.Empty;
            this.OddFooter = string.Empty;
        }

        public static string FromLCR(string leftPart, string centerPart, string rightPart)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(leftPart))
            {
                builder.Append("&L");
                builder.Append(leftPart);
            }
            if (!string.IsNullOrEmpty(centerPart))
            {
                builder.Append("&C");
                builder.Append(centerPart);
            }
            if (!string.IsNullOrEmpty(rightPart))
            {
                builder.Append("&R");
                builder.Append(rightPart);
            }
            return builder.ToString();
        }

        internal bool IsDefault() => 
            this.AlignWithMargins && (this.ScaleWithDoc && (!this.DifferentFirst && (!this.DifferentOddEven && (string.IsNullOrEmpty(this.FirstHeader) && (string.IsNullOrEmpty(this.FirstFooter) && (string.IsNullOrEmpty(this.EvenHeader) && (string.IsNullOrEmpty(this.EvenFooter) && (string.IsNullOrEmpty(this.OddHeader) && string.IsNullOrEmpty(this.OddFooter)))))))));

        public bool AlignWithMargins { get; set; }

        public bool DifferentFirst { get; set; }

        public bool DifferentOddEven { get; set; }

        public bool ScaleWithDoc { get; set; }

        public string FirstHeader { get; set; }

        public string FirstFooter { get; set; }

        public string EvenHeader { get; set; }

        public string EvenFooter { get; set; }

        public string OddHeader { get; set; }

        public string OddFooter { get; set; }
    }
}

