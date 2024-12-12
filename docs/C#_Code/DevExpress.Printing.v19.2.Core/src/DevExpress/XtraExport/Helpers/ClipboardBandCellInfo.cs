namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ClipboardBandCellInfo : ClipboardCellInfo
    {
        public ClipboardBandCellInfo(int width, int height, object value, string displayValue, XlCellFormatting formatting, bool allowHtmlDraw = false) : base(value, displayValue, formatting, allowHtmlDraw)
        {
            this.Height = height;
            this.Width = width;
            this.SpaceAfter = 0;
            this.SpaceBefore = 0;
            this.Row = -1;
            this.Column = null;
        }

        public object Column { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int SpaceAfter { get; set; }

        public int SpaceBefore { get; set; }

        public int Row { get; set; }

        public int BandIndex { get; set; }
    }
}

