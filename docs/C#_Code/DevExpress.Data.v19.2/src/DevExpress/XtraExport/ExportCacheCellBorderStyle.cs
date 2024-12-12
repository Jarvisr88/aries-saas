namespace DevExpress.XtraExport
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ExportCacheCellBorderStyle
    {
        public bool IsDefault;
        public Color Color_;
        public int Width;
        public DevExpress.XtraPrinting.BorderDashStyle BorderDashStyle;
        public bool IsEqual(ExportCacheCellBorderStyle borderStyle) => 
            ((borderStyle.Width != 0) || (this.Width != 0)) ? ((borderStyle.IsDefault == this.IsDefault) && ((borderStyle.Color_ == this.Color_) && ((borderStyle.Width == this.Width) && (borderStyle.BorderDashStyle == this.BorderDashStyle)))) : true;

        public override bool Equals(object obj) => 
            (obj is ExportCacheCellBorderStyle) && this.IsEqual((ExportCacheCellBorderStyle) obj);

        public override int GetHashCode() => 
            (this.Width != 0) ? HashCodeHelper.CalculateGeneric<int, bool, Color, DevExpress.XtraPrinting.BorderDashStyle>(this.Width, this.IsDefault, this.Color_, this.BorderDashStyle) : HashCodeHelper.GetNullHash();
    }
}

