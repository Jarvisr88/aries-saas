namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;

    public class PageBreakData : IXtraSupportShouldSerialize, IPageData
    {
        public PageBreakData();
        public PageBreakData(float value);
        bool IXtraSupportShouldSerialize.ShouldSerialize(string propertyName);

        [XtraSerializableProperty(0)]
        public float Value { get; set; }

        [XtraSerializableProperty(1)]
        public Rectangle PageBounds { get; set; }

        [XtraSerializableProperty(2)]
        public Margins PageMargins { get; set; }

        [XtraSerializableProperty(3)]
        public bool Landscape { get; set; }

        Rectangle IPageData.Bounds { get; }

        MarginsF IPageData.MarginsF { get; }

        public bool HasEmptyBounds { get; }
    }
}

