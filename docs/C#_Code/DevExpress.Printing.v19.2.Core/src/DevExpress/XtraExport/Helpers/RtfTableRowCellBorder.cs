namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal class RtfTableRowCellBorder
    {
        private System.Drawing.Color color = System.Drawing.Color.Black;
        private RtfCellBorderStyle style = RtfCellBorderStyle.Thin;
        private int width;

        public RtfTableRowCellBorder(RtfDocument document)
        {
            this.<Document>k__BackingField = document;
            this.UpdateBorderWidth();
        }

        public void AssignStyle(RtfTableRowCellBorder source)
        {
            this.Color = source.Color;
            this.Style = source.Style;
            this.width = source.width;
            this.WidthScale = source.WidthScale;
        }

        private void UpdateBorderWidth()
        {
            switch (this.style)
            {
                case RtfCellBorderStyle.Thin:
                    this.width = 0;
                    return;

                case RtfCellBorderStyle.Medium:
                case RtfCellBorderStyle.MediumDashed:
                case RtfCellBorderStyle.MediumDashDot:
                case RtfCellBorderStyle.MediumDashDotDot:
                    this.width = this.WidthScale * 30;
                    return;

                case RtfCellBorderStyle.Thick:
                    this.width = this.WidthScale * 0x2d;
                    return;

                case RtfCellBorderStyle.Hair:
                    this.width = this.WidthScale * 10;
                    return;
            }
            this.width = 0;
        }

        public System.Drawing.Color Color
        {
            get => 
                this.color;
            set
            {
                if (this.color != value)
                {
                    this.color = value;
                    this.Document.ColorTable.CheckColor(this.color);
                }
            }
        }

        public RtfDocument Document { get; }

        public RtfCellBorderStyle Style
        {
            get => 
                this.style;
            set
            {
                if (this.style != value)
                {
                    this.style = value;
                    this.UpdateBorderWidth();
                }
            }
        }

        public int Width =>
            this.width * this.WidthScale;

        public int WidthScale { get; set; }
    }
}

