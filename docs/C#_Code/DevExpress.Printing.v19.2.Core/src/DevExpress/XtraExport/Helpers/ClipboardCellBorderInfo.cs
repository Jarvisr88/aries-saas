namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ClipboardCellBorderInfo
    {
        internal ClipboardCellBorderInfo(ClipboardCellBorderInfo other)
        {
            this.BorderStyle = other.BorderStyle;
            this.Color = other.Color;
        }

        public ClipboardCellBorderInfo(System.Drawing.Color color, DevExpress.XtraExport.Helpers.BorderStyle borderStyle)
        {
            this.Color = color;
            this.BorderStyle = borderStyle;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ClipboardCellBorderInfo))
            {
                return false;
            }
            ClipboardCellBorderInfo info = (ClipboardCellBorderInfo) obj;
            return ((info.BorderStyle == this.BorderStyle) && (info.Color == this.Color));
        }

        public override int GetHashCode() => 
            this.GetHashCode();

        public DevExpress.XtraExport.Helpers.BorderStyle BorderStyle { get; set; }
        public System.Drawing.Color Color { get; set; }
    }
}

