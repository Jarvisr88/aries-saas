namespace DevExpress.XtraPrinting
{
    using System;

    public interface ICommandHandler
    {
        bool CanHandleCommand(PrintingSystemCommand command, IPrintControl printControl);
        void HandleCommand(PrintingSystemCommand command, object[] args, IPrintControl printControl, ref bool handled);
    }
}

