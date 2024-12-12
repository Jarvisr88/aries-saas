namespace DevExpress.XtraExport
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ExportCacheCellStyle
    {
        private Dictionary<ushort, int> types;
        public Color TextColor;
        public Font TextFont;
        public StringAlignment TextAlignment;
        public StringAlignment LineAlignment;
        public string FormatString;
        public string XlsxFormatString;
        public Color BkColor;
        public Color FgColor;
        public BrushStyle BrushStyle_;
        public ExportCacheCellBorderStyle LeftBorder;
        public ExportCacheCellBorderStyle TopBorder;
        public ExportCacheCellBorderStyle RightBorder;
        public ExportCacheCellBorderStyle BottomBorder;
        public short PreparedCellType;
        public bool WrapText;
        public bool RightToLeft;
        private Dictionary<ushort, int> Types
        {
            get
            {
                this.types ??= new Dictionary<ushort, int>();
                return this.types;
            }
        }
        public bool IsEqual(ExportCacheCellStyle style) => 
            (style.BkColor == this.BkColor) && ((style.FgColor == this.FgColor) && ((style.TextColor == this.TextColor) && (this.TextFont.Equals(style.TextFont) && ((style.BrushStyle_ == this.BrushStyle_) && (this.LeftBorder.IsEqual(style.LeftBorder) && (this.TopBorder.IsEqual(style.TopBorder) && (this.RightBorder.IsEqual(style.RightBorder) && (this.BottomBorder.IsEqual(style.BottomBorder) && ((style.TextAlignment == this.TextAlignment) && ((style.LineAlignment == this.LineAlignment) && ((style.FormatString == this.FormatString) && ((style.XlsxFormatString == this.XlsxFormatString) && ((style.PreparedCellType == this.PreparedCellType) && ((style.WrapText == this.WrapText) && (style.RightToLeft == this.RightToLeft)))))))))))))));

        public override bool Equals(object obj) => 
            (obj is ExportCacheCellStyle) && this.IsEqual((ExportCacheCellStyle) obj);

        public override int GetHashCode() => 
            HashCodeHelper.CalculateGeneric<Color, Color, Color, Font, BrushStyle, ExportCacheCellBorderStyle, ExportCacheCellBorderStyle, ExportCacheCellBorderStyle, ExportCacheCellBorderStyle, StringAlignment, StringAlignment, string, string, short, bool, bool>(this.BkColor, this.FgColor, this.TextColor, this.TextFont, this.BrushStyle_, this.LeftBorder, this.TopBorder, this.RightBorder, this.BottomBorder, this.TextAlignment, this.LineAlignment, this.FormatString, this.XlsxFormatString, this.PreparedCellType, this.WrapText, this.RightToLeft);

        internal bool WasExportedWithType(ushort type) => 
            this.Types.ContainsKey(type);

        internal void AddExportedType(ushort type, int result)
        {
            if (!this.WasExportedWithType(type))
            {
                this.Types[type] = result;
            }
        }

        internal int GetExportResult(ushort type) => 
            this.Types[type];
    }
}

