namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class DocumentBandContainer : DocumentBand
    {
        private DevExpress.XtraPrinting.Native.MultiColumn multiColumn;
        private bool containsDetailBands;

        public DocumentBandContainer();
        public DocumentBandContainer(bool containsDetailBands);
        private DocumentBandContainer(DocumentBandContainer source, int rowIndex);
        public override bool ContainsDetailBands();
        public override DocumentBand GetInstance(int rowIndex);

        public override object GroupKey { get; set; }

        public override DevExpress.XtraPrinting.Native.MultiColumn MultiColumn { get; set; }
    }
}

