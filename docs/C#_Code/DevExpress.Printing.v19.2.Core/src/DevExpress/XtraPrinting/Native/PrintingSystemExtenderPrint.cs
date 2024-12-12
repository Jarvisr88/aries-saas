namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Printing.Native;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;

    public class PrintingSystemExtenderPrint : PrintingSystemExtenderBase
    {
        protected PrintingSystemBase printingSystem;
        private string predefinedPageRange;
        private System.Drawing.Printing.PageSettings pageSettings;

        public PrintingSystemExtenderPrint(PrintingSystemBase printingSystem);
        public override void Assign(Margins margins, PaperKind paperKind, string paperName, bool landscape);
        public override void AssignDefaultPrinterSettings(PrinterSettingsUsing settingsUsing);
        private void AssignDefaultPrinterSettingsCore(PrinterSettingsUsing settingsUsing, System.Drawing.Printing.PageSettings defaultPageSettings);
        public override void AssignPrinterSettings(string printerName, string paperName, PrinterSettingsUsing settingsUsing);
        private static void CancelPrint(System.Drawing.Printing.PrintDocument pd);
        protected virtual PSPrintDocument CreatePrintDocument(PrintingSystemBase ps);
        private PrintController GetActualPrintController();
        protected static string GetPageRange(System.Drawing.Printing.PrinterSettings printerSettings);
        private PageScope GetPageScope();
        private PaperSize GetPaperSize(PaperKind paperKind, string paperName);
        private PaperSize GetPaperSize(string printerName, PaperKind paperKind, string paperName);
        private static bool IsActualParerSize(PaperSize paperSize, PaperKind paperKind, string paperName);
        protected virtual bool PredicateMargins();
        public override void Print(string printerName);
        protected void PrintDocument(System.Drawing.Printing.PrintDocument pd);

        protected XtraPageSettingsBase XPageSettings { get; private set; }

        public override string PredefinedPageRange { get; set; }

        public override System.Drawing.Printing.PageSettings PageSettings { get; }

        public override System.Drawing.Printing.PrinterSettings PrinterSettings { get; }
    }
}

