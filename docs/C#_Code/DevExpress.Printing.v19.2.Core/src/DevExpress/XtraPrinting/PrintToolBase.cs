namespace DevExpress.XtraPrinting
{
    using DevExpress.ReportServer.Printing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing.Printing;

    public class PrintToolBase
    {
        private PrintingSystemBase printingSystem;

        public PrintToolBase(PrintingSystemBase printingSystem)
        {
            this.PrintingSystem = printingSystem;
        }

        protected virtual void BeforePrint()
        {
        }

        protected virtual void ExtendPrintingSystem(PrintingSystemBase printingSystem)
        {
            if (!(printingSystem.Extender is PrintingSystemExtenderPrint))
            {
                printingSystem.Extender = new PrintingSystemExtenderPrint(printingSystem);
            }
        }

        public void Print()
        {
            this.Print(string.Empty);
        }

        public void Print(string printerName)
        {
            if (this.printingSystem != null)
            {
                this.BeforePrint();
                if (this.printingSystem is RemotePrintingSystem)
                {
                    this.printingSystem.GetService<IRemotePrintService>().PrintDirect(0, this.printingSystem.PageCount - 1, delegate (string defaultPrinterName) {
                        string text1 = printerName;
                        if (printerName == null)
                        {
                            string local1 = printerName;
                            text1 = defaultPrinterName;
                        }
                        this.printingSystem.Extender.Print(text1);
                    });
                }
                else
                {
                    this.printingSystem.Extender.Print(printerName);
                }
            }
        }

        public System.Drawing.Printing.PrinterSettings PrinterSettings =>
            this.PrintingSystem.Extender.PrinterSettings;

        public PrintingSystemBase PrintingSystem
        {
            get => 
                this.printingSystem;
            protected set
            {
                this.printingSystem = value;
                this.ExtendPrintingSystem(this.printingSystem);
            }
        }
    }
}

