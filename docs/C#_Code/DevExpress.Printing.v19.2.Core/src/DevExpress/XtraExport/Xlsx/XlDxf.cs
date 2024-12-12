namespace DevExpress.XtraExport.Xlsx
{
    using DevExpress.Export.Xl;
    using DevExpress.XtraExport;
    using System;
    using System.Runtime.CompilerServices;

    internal class XlDxf
    {
        public override bool Equals(object obj)
        {
            XlDxf dxf = obj as XlDxf;
            return ((dxf != null) ? (((this.Font == null) || this.Font.Equals(dxf.Font)) ? (((this.Font != null) || (dxf.Font == null)) && (((this.NumberFormat == null) || this.NumberFormat.Equals(dxf.NumberFormat)) ? (((this.NumberFormat != null) || (dxf.NumberFormat == null)) && (((this.Fill == null) || this.Fill.Equals(dxf.Fill)) ? (((this.Fill != null) || (dxf.Fill == null)) && (((this.Alignment == null) || this.Alignment.Equals(dxf.Alignment)) ? (((this.Alignment != null) || (dxf.Alignment == null)) && (((this.Border == null) || this.Border.Equals(dxf.Border)) ? ((this.Border != null) || (dxf.Border == null)) : false)) : false)) : false)) : false)) : false) : false);
        }

        public override int GetHashCode()
        {
            int num = 0;
            if (this.Font != null)
            {
                num ^= this.Font.GetHashCode();
            }
            if (this.NumberFormat != null)
            {
                num ^= this.NumberFormat.GetHashCode();
            }
            if (this.Fill != null)
            {
                num ^= this.Fill.GetHashCode();
            }
            if (this.Alignment != null)
            {
                num ^= this.Alignment.GetHashCode();
            }
            if (this.Border != null)
            {
                num ^= this.Border.GetHashCode();
            }
            return num;
        }

        public XlFont Font { get; set; }

        public ExcelNumberFormat NumberFormat { get; set; }

        public XlFill Fill { get; set; }

        public XlCellAlignment Alignment { get; set; }

        public XlBorder Border { get; set; }

        public bool IsEmpty =>
            (this.Font == null) && ((this.NumberFormat == null) && ((this.Fill == null) && ((this.Alignment == null) && ReferenceEquals(this.Border, null))));
    }
}

