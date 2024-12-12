namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;

    public abstract class PrintingSystemExtenderBase : IPrintingSystemExtenderBase, IPageSettingsExtenderBase, IDisposable
    {
        protected PrintingSystemExtenderBase();
        public virtual void AddCommandHandler(ICommandHandler handler);
        public virtual void Assign(Margins margins, PaperKind paperKind, string paperName, bool landscape);
        public virtual void AssignDefaultPrinterSettings(PrinterSettingsUsing settingsUsing);
        public virtual void AssignPrinterSettings(string printerName, string paperName, PrinterSettingsUsing settingsUsing);
        public virtual void Clear();
        public virtual void Dispose();
        public virtual void EnableCommand(PrintingSystemCommand command, bool enabled);
        public virtual void ExecCommand(PrintingSystemCommand command, object[] args);
        public virtual void ExecCommand(PrintingSystemCommand command, object[] args, IPrintControl printControl);
        public virtual CommandVisibility GetCommandVisibility(PrintingSystemCommand command);
        public virtual void OnBeginCreateDocument();
        public virtual void Print(string printerName);
        public virtual void RemoveCommandHandler(ICommandHandler handler);
        public virtual void SetCommandVisibility(PrintingSystemCommand[] commands, CommandVisibility visibility, Priority priority);

        public virtual string PredefinedPageRange { get; set; }

        public virtual System.Drawing.Printing.PageSettings PageSettings { get; }

        public virtual System.Drawing.Printing.PrinterSettings PrinterSettings { get; }

        public virtual ProgressReflector ActiveProgressReflector { get; }
    }
}

