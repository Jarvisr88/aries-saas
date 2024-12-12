namespace DevExpress.XtraPrinting.Export
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct TableCellLineInfo
    {
        private Color lineColor;
        private float lineWidth;
        private DashStyle lineStyle;
        public TableCellLineInfo(Color lineColor, float lineWidth, DashStyle lineStyle)
        {
            this.lineColor = lineColor;
            this.lineWidth = lineWidth;
            this.lineStyle = lineStyle;
        }

        public Color LineColor =>
            this.lineColor;
        public float LineWidth =>
            this.lineWidth;
        public DashStyle LineStyle =>
            this.lineStyle;
        public bool LineVisible =>
            this.HasColor && (this.lineWidth > 0f);
        private bool HasColor =>
            (this.lineColor != Color.Empty) && (this.lineColor != Color.Transparent);
    }
}

