namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing.Printing;

    public interface IPageSettingsExtenderBase : IDisposable
    {
        void Assign(Margins margins, PaperKind paperKind, string paperName, bool landscape);
        void AssignDefaultPrinterSettings(PrinterSettingsUsing settingsUsing);
        void AssignPrinterSettings(string printerName, string paperName, PrinterSettingsUsing settingsUsing);

        string PredefinedPageRange { get; set; }

        System.Drawing.Printing.PageSettings PageSettings { get; }

        System.Drawing.Printing.PrinterSettings PrinterSettings { get; }
    }
}

