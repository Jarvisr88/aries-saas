namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal class HtmlTableCellBorder
    {
        private System.Drawing.Color color;
        private HtmlCellBorderStyle style = HtmlCellBorderStyle.Thin;
        private int width;

        public HtmlTableCellBorder()
        {
            this.UpdateBorderWidth();
        }

        public void AssignStyle(HtmlTableCellBorder source)
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
                case HtmlCellBorderStyle.Thin:
                    this.width = this.WidthScale;
                    return;

                case HtmlCellBorderStyle.Medium:
                case HtmlCellBorderStyle.MediumDashed:
                case HtmlCellBorderStyle.MediumDashDot:
                case HtmlCellBorderStyle.MediumDashDotDot:
                    this.width = this.WidthScale * 2;
                    return;

                case HtmlCellBorderStyle.Thick:
                    this.width = 3;
                    return;

                case HtmlCellBorderStyle.Hair:
                    this.width = this.WidthScale;
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
                }
            }
        }

        public HtmlCellBorderStyle Style
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

        public float Width =>
            (float) (this.width * this.WidthScale);

        public int WidthScale { get; set; }
    }
}

