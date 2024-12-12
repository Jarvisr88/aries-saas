namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;

    public class ReadonlyPageData : IXtraSupportShouldSerialize, IPageData
    {
        protected bool fLandscape;
        protected DevExpress.XtraPrinting.Native.MarginsF fMinMargins;
        protected DevExpress.XtraPrinting.Native.MarginsF fMargins;
        protected System.Drawing.Printing.PaperKind fPaperKind;
        protected System.Drawing.SizeF fSize;
        protected System.Drawing.SizeF fPageSize;
        protected string fPaperName;

        public ReadonlyPageData(ReadonlyPageData source);
        internal ReadonlyPageData(ReadonlyPageData source, IPageDataService serv);
        public ReadonlyPageData(System.Drawing.Printing.Margins margins, System.Drawing.Printing.Margins minMargins, System.Drawing.Printing.PaperKind paperKind, bool landscape);
        public ReadonlyPageData(DevExpress.XtraPrinting.Native.MarginsF margins, DevExpress.XtraPrinting.Native.MarginsF minMargins, System.Drawing.Printing.PaperKind paperKind, System.Drawing.SizeF pageSize, bool landscape);
        public ReadonlyPageData(System.Drawing.Printing.Margins margins, System.Drawing.Printing.Margins minMargins, System.Drawing.Printing.PaperKind paperKind, System.Drawing.SizeF pageSize, bool landscape);
        internal static int ConvertSizeValue(float value);
        bool IXtraSupportShouldSerialize.ShouldSerialize(string propertyName);
        public override bool Equals(object obj);
        public override int GetHashCode();
        public RectangleF GetMarginRect(RectangleF pageRect);
        private static DevExpress.XtraPrinting.Native.MarginsF GetMaxValue(DevExpress.XtraPrinting.Native.MarginsF m1, DevExpress.XtraPrinting.Native.MarginsF m2);

        public virtual string PaperName { get; set; }

        [XtraSerializableProperty, DefaultValue(false)]
        public virtual bool Landscape { get; set; }

        public virtual System.Drawing.Printing.Margins MinMargins { get; set; }

        public virtual System.Drawing.Printing.Margins Margins { get; set; }

        [XtraSerializableProperty]
        public virtual DevExpress.XtraPrinting.Native.MarginsF MinMarginsF { get; set; }

        [XtraSerializableProperty]
        public virtual DevExpress.XtraPrinting.Native.MarginsF MarginsF { get; set; }

        [XtraSerializableProperty, DefaultValue(1)]
        public virtual System.Drawing.Printing.PaperKind PaperKind { get; set; }

        [XtraSerializableProperty]
        public virtual System.Drawing.Size Size { get; set; }

        [XtraSerializableProperty]
        public virtual System.Drawing.SizeF SizeF { get; set; }

        public Rectangle Bounds { get; }

        private System.Drawing.SizeF ActualSizeF { get; }

        public virtual System.Drawing.SizeF PageSize { get; set; }

        public RectangleF UsefulPageRectF { get; }

        public float UsefulPageWidth { get; }

        public float UsefulPageHeight { get; }

        public RectangleF PageHeaderRect { get; }

        public RectangleF PageFooterRect { get; }
    }
}

