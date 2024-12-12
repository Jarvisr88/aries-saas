namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrintingLinks;
    using System;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public abstract class ComponentPrinterBase : ComponentExporter
    {
        protected ComponentPrinterBase(IPrintable component) : base(component)
        {
        }

        protected ComponentPrinterBase(IPrintable component, PrintingSystemBase printingSystem) : base(component, printingSystem)
        {
        }

        private static PaperSize CreateLetterPaperSize() => 
            new PaperSize("Custom", 850, 0x44c);

        protected override PrintableComponentLinkBase CreateLink() => 
            base.CreateLink();

        public static System.Drawing.Printing.PageSettings GetDefaultPageSettings()
        {
            System.Drawing.Printing.PageSettings defaultPageSettings = new PrintDocument().DefaultPageSettings;
            try
            {
                PaperSize paperSize = defaultPageSettings.PaperSize;
            }
            catch
            {
                defaultPageSettings.PaperSize = CreateLetterPaperSize();
                defaultPageSettings.Margins = XtraPageSettingsBase.DefaultMargins;
            }
            return defaultPageSettings;
        }

        public static bool IsPrintingAvailable(bool throwException) => 
            ComponentPrinterDynamic.IsPrintingAvailable_(throwException);

        public abstract void Print();
        public abstract void PrintDialog();
        public abstract Form ShowPreview(object lookAndFeel);
        public abstract Form ShowPreview(IWin32Window owner, object lookAndFeel);
        public abstract Form ShowRibbonPreview(object lookAndFeel);
        public abstract Form ShowRibbonPreview(IWin32Window owner, object lookAndFeel);

        public abstract System.Drawing.Printing.PageSettings PageSettings { get; }
    }
}

