namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    internal class UnitName
    {
        internal string name;
        internal GraphicsUnit unit;
        internal static readonly DevExpress.XtraPrinting.Native.UnitName[] names = new DevExpress.XtraPrinting.Native.UnitName[] { new DevExpress.XtraPrinting.Native.UnitName("world", GraphicsUnit.World), new DevExpress.XtraPrinting.Native.UnitName("display", GraphicsUnit.Display), new DevExpress.XtraPrinting.Native.UnitName("px", GraphicsUnit.Pixel), new DevExpress.XtraPrinting.Native.UnitName("pt", GraphicsUnit.Point), new DevExpress.XtraPrinting.Native.UnitName("in", GraphicsUnit.Inch), new DevExpress.XtraPrinting.Native.UnitName("doc", GraphicsUnit.Document), new DevExpress.XtraPrinting.Native.UnitName("mm", GraphicsUnit.Millimeter) };

        internal UnitName(string name, GraphicsUnit unit)
        {
            this.name = name;
            this.unit = unit;
        }
    }
}

