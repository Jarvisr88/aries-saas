namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    public interface IPrintingSystemExtenderBase : IPageSettingsExtenderBase, IDisposable
    {
        void AddCommandHandler(ICommandHandler handler);
        void Clear();
        void EnableCommand(PrintingSystemCommand command, bool enabled);
        void ExecCommand(PrintingSystemCommand command, object[] args);
        void ExecCommand(PrintingSystemCommand command, object[] args, IPrintControl printControl);
        CommandVisibility GetCommandVisibility(PrintingSystemCommand command);
        void OnBeginCreateDocument();
        void Print(string printerName);
        void RemoveCommandHandler(ICommandHandler handler);
        void SetCommandVisibility(PrintingSystemCommand[] commands, CommandVisibility visibility, Priority priority);

        ProgressReflector ActiveProgressReflector { get; }
    }
}

