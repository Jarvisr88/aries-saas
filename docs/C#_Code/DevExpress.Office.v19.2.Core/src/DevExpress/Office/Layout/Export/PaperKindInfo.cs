namespace DevExpress.Office.Layout.Export
{
    using System;
    using System.Drawing.Printing;

    public class PaperKindInfo
    {
        private static readonly PaperKindInfo defaultPaperKindInfo = new PaperKindInfo(System.Drawing.Printing.PaperKind.Custom, false);
        private System.Drawing.Printing.PaperKind paperKind;
        private bool landscape;

        public PaperKindInfo(System.Drawing.Printing.PaperKind paperKind, bool landscape)
        {
            this.paperKind = paperKind;
            this.landscape = landscape;
        }

        public static PaperKindInfo Default =>
            defaultPaperKindInfo;

        public System.Drawing.Printing.PaperKind PaperKind
        {
            get => 
                this.paperKind;
            set => 
                this.paperKind = value;
        }

        public bool Landscape
        {
            get => 
                this.landscape;
            set => 
                this.landscape = value;
        }
    }
}

