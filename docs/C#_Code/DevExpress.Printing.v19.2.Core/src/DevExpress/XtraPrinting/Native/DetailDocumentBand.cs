namespace DevExpress.XtraPrinting.Native
{
    using System;

    public class DetailDocumentBand : DocumentBand
    {
        public DetailDocumentBand();
        public DetailDocumentBand(int rowIndex);
        private DetailDocumentBand(DetailDocumentBand source, int rowIndex);
        public override DocumentBand GetInstance(int rowIndex);

        public override bool IsDetailBand { get; }
    }
}

