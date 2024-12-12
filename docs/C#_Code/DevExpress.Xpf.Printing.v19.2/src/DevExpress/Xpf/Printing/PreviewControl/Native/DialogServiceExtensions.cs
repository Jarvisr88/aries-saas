namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Printing;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public static class DialogServiceExtensions
    {
        public static ICommand ShowDialog(this DevExpress.Xpf.Core.DialogService service, ICommand okCommand, ICommand cancelCommand, string title, ViewModelBase viewModel)
        {
            UICommand command1 = new UICommand();
            command1.Id = MessageBoxResult.OK;
            command1.Caption = PrintingLocalizer.GetString(PrintingStringId.OK);
            command1.IsCancel = false;
            command1.IsDefault = true;
            command1.Command = okCommand;
            UICommand command = command1;
            UICommand command4 = new UICommand();
            command4.Id = MessageBoxResult.Cancel;
            command4.Caption = PrintingLocalizer.GetString(PrintingStringId.Cancel);
            command4.IsCancel = true;
            command4.IsDefault = false;
            command4.Command = cancelCommand;
            UICommand command2 = command4;
            UICommand[] dialogCommands = new UICommand[] { command, command2 };
            UICommand command3 = service.ShowDialog(dialogCommands, title, viewModel);
            return command3?.Command;
        }
    }
}

