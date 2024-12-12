namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Drawing;
    using System.Drawing.Printing;

    public class PageData : ReadonlyPageData
    {
        public PageData();
        public PageData(ReadonlyPageData pageData);
        public PageData(DevExpress.XtraPrinting.Native.MarginsF margins, System.Drawing.Printing.PaperKind paperKind, bool landscape);
        public PageData(DevExpress.XtraPrinting.Native.MarginsF margins, DevExpress.XtraPrinting.Native.MarginsF minMargins, PaperSize paperSize, bool landscape);
        public PageData(System.Drawing.Printing.Margins margins, System.Drawing.Printing.Margins minMargins, PaperSize paperSize, bool landscape);
        public PageData(DevExpress.XtraPrinting.Native.MarginsF margins, DevExpress.XtraPrinting.Native.MarginsF minMargins, System.Drawing.Printing.PaperKind paperKind, System.Drawing.Size pageSize, bool landscape);
        public PageData(System.Drawing.Printing.Margins margins, System.Drawing.Printing.Margins minMargins, System.Drawing.Printing.PaperKind paperKind, System.Drawing.Size pageSize, bool landscape);
        public static System.Drawing.Size ToSize(PaperSize paperSize);

        public override string PaperName { get; set; }

        [XtraSerializableProperty]
        public override bool Landscape { get; set; }

        [XtraSerializableProperty]
        public override System.Drawing.Printing.Margins Margins { get; set; }

        [XtraSerializableProperty]
        public override System.Drawing.Printing.Margins MinMargins { get; set; }

        [XtraSerializableProperty]
        public override DevExpress.XtraPrinting.Native.MarginsF MarginsF { get; set; }

        [XtraSerializableProperty]
        public override DevExpress.XtraPrinting.Native.MarginsF MinMarginsF { get; set; }

        [XtraSerializableProperty]
        public override System.Drawing.Printing.PaperKind PaperKind { get; set; }

        [XtraSerializableProperty]
        public override System.Drawing.Size Size { get; set; }

        [XtraSerializableProperty]
        public override System.Drawing.SizeF SizeF { get; set; }

        public override System.Drawing.SizeF PageSize { get; set; }
    }
}

