namespace DevExpress.Printing
{
    using DevExpress.Printing.Native.PrintEditor;
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Printing;

    public class PrinterItem
    {
        private SafeGetter<string> fullName;
        private SafeGetter<string> location;
        private SafeGetter<string> comment;
        private SafeGetter<string> printerDocumentsInQueue;
        private SafeGetter<string> displayName;
        private SafeGetter<PrinterStatus> printerStatus;
        private DevExpress.Printing.Native.PrintEditor.PrinterType printerType;

        public PrinterItem(PrintQueue printQueue, bool isFax, bool isNetwork, bool isDefault, bool isOffline)
        {
            this.fullName = new SafeGetter<string>(() => printQueue.FullName, "Undefined Printer");
            this.location = new SafeGetter<string>(() => printQueue.Location, "Undefined Location");
            this.comment = new SafeGetter<string>(() => printQueue.Comment, string.Empty);
            this.printerDocumentsInQueue = new SafeGetter<string>(() => printQueue.NumberOfJobs.ToString(), "0");
            this.displayName = !isNetwork ? new SafeGetter<string>(() => printQueue.Name, "Undefined Printer") : new SafeGetter<string>(() => string.Format(PreviewStringId.NetworkPrinterFormat.GetString(), printQueue.Name, printQueue.HostingPrintServer.Name.Replace(@"\", string.Empty)), "Undefined Printer");
            this.printerStatus = new SafeGetter<PrinterStatus>(() => (PrinterStatus) printQueue.QueueStatus, PrinterStatus.NotAvailable);
            this.InitPrinterTypeFlags(isFax, isNetwork, isDefault, isOffline);
        }

        public PrinterItem(string fullName, string displayName, string location, string comment, string printerDocumentsInQueue, PrinterStatus status)
        {
            this.fullName = new SafeGetter<string>(() => fullName, "Undefined Printer");
            this.displayName = new SafeGetter<string>(() => displayName, "Undefined Printer");
            this.location = new SafeGetter<string>(() => location, "Undefined Location");
            this.comment = new SafeGetter<string>(() => comment, string.Empty);
            this.printerDocumentsInQueue = new SafeGetter<string>(() => printerDocumentsInQueue, "0");
            this.printerStatus = new SafeGetter<PrinterStatus>(() => status, PrinterStatus.NotAvailable);
        }

        internal void InitPrinterTypeFlags(bool isFax, bool isNetwork, bool isDefault, bool isOffline)
        {
            if (isFax)
            {
                this.printerType |= DevExpress.Printing.Native.PrintEditor.PrinterType.Fax;
            }
            if (isNetwork)
            {
                this.printerType |= DevExpress.Printing.Native.PrintEditor.PrinterType.Network;
            }
            if (isDefault)
            {
                this.printerType |= DevExpress.Printing.Native.PrintEditor.PrinterType.Default;
            }
            if (isOffline)
            {
                this.printerType |= DevExpress.Printing.Native.PrintEditor.PrinterType.Offline;
            }
        }

        public string Location =>
            this.location.Value;

        public string Comment =>
            this.comment.Value;

        public string PrinterDocumentsInQueue =>
            this.printerDocumentsInQueue.Value;

        public string Status =>
            this.printerStatus.Value.GetString();

        public string DisplayName =>
            this.displayName.Value;

        public string FullName =>
            this.fullName.Value;

        public DevExpress.Printing.Native.PrintEditor.PrinterType PrinterType
        {
            get
            {
                if (this.printerStatus.Value.HasFlag(PrinterStatus.Offline) || this.printerStatus.Value.HasFlag(PrinterStatus.ServerOffline))
                {
                    this.printerType |= DevExpress.Printing.Native.PrintEditor.PrinterType.Offline;
                }
                return this.printerType;
            }
        }
    }
}

