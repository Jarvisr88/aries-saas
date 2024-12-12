namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XfPropBorder : XfPropColor
    {
        public XfPropBorder(short typeCode, XlColor color, XlBorderLineStyle lineStyle) : base(typeCode, color)
        {
            this.LineStyle = lineStyle;
        }

        protected override int GetSizeCore() => 
            base.GetSizeCore() + 2;

        protected override void WriteCore(BinaryWriter writer)
        {
            base.WriteCore(writer);
            writer.Write((ushort) this.LineStyle);
        }

        public XlBorderLineStyle LineStyle { get; private set; }
    }
}

