namespace DevExpress.Export
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class XlFormattingObject
    {
        public void CopyFrom(XlCellFormatting value, DevExpress.Utils.FormatType columnFormatType)
        {
            if (value != null)
            {
                this.Font = CreateXlCellFont(value);
                this.FormatString = value.NetFormatString;
                this.FormatType = columnFormatType;
                this.BackColor = GetBackColor(value);
                this.Border = value.Border;
            }
        }

        private static XlCellFont CreateXlCellFont(XlCellFormatting value)
        {
            XlCellFont font1 = new XlCellFont();
            font1.Name = value.Font.Name;
            font1.Size = value.Font.Size;
            font1.Bold = value.Font.Bold;
            font1.Color = (Color) value.Font.Color;
            font1.Italic = value.Font.Italic;
            font1.Underline = value.Font.Underline;
            font1.StrikeThrough = value.Font.StrikeThrough;
            return font1;
        }

        private static Color GetBackColor(XlCellFormatting value) => 
            (value.Fill != null) ? ((Color) value.Fill.ForeColor) : Color.Empty;

        public XlCellFont Font { get; set; }

        public XlCellAlignment Alignment { get; set; }

        public Color BackColor { get; set; }

        public string FormatString { get; set; }

        public DevExpress.Utils.FormatType FormatType { get; set; }

        public XlNumberFormat NumberFormat { get; set; }

        public XlBorder Border { get; set; }
    }
}

