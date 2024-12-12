namespace DevExpress.Xpf.PdfViewer.Extensions
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.PdfViewer;
    using DevExpress.Xpf.PdfViewer.Internal;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public static class PdfDialogServiceExtensions
    {
        public static void ShowDialog(this IDialogService service, ICommand okCommand, string okCommandCaption, ICommand cancelCommand, string cancelCommandCaption, string title, Func<object, object> convertViewModel, Func<object> getDefaultContent)
        {
            Func<PdfDialogService, object> evaluator = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<PdfDialogService, object> local1 = <>c.<>9__0_0;
                evaluator = <>c.<>9__0_0 = x => x.Content;
            }
            object local2 = (service as PdfDialogService).With<PdfDialogService, object>(evaluator);
            object local4 = local2;
            if (local2 == null)
            {
                object local3 = local2;
                local4 = getDefaultContent();
            }
            object arg = local4;
            PrintDialogViewModel printDialogViewModel = convertViewModel(arg) as PrintDialogViewModel;
            UICommand command1 = new UICommand();
            command1.Id = MessageBoxResult.OK;
            command1.Caption = okCommandCaption;
            command1.IsCancel = false;
            command1.IsDefault = true;
            bool? useCommandManager = null;
            command1.Command = new DelegateCommand(delegate {
                Action<ICommand> <>9__2;
                Action<ICommand> action = <>9__2;
                if (<>9__2 == null)
                {
                    Action<ICommand> local1 = <>9__2;
                    action = <>9__2 = x => x.TryExecute(printDialogViewModel.PdfViewModel);
                }
                okCommand.Do<ICommand>(action);
            }, delegate {
                Func<ICommand, bool> <>9__4;
                Func<ICommand, bool> func2 = <>9__4;
                if (<>9__4 == null)
                {
                    Func<ICommand, bool> local1 = <>9__4;
                    func2 = <>9__4 = x => x.CanExecute(printDialogViewModel.PdfViewModel);
                }
                return okCommand.Return<ICommand, bool>(func2, <>c.<>9__0_5 ??= () => true);
            }, useCommandManager);
            UICommand command = command1;
            UICommand command3 = new UICommand();
            command3.Id = MessageBoxResult.Cancel;
            command3.Caption = cancelCommandCaption;
            command3.IsCancel = true;
            command3.IsDefault = false;
            useCommandManager = null;
            command3.Command = new DelegateCommand(delegate {
                Action<ICommand> <>9__7;
                Action<ICommand> action = <>9__7;
                if (<>9__7 == null)
                {
                    Action<ICommand> local1 = <>9__7;
                    action = <>9__7 = x => x.TryExecute(printDialogViewModel.PdfViewModel);
                }
                cancelCommand.Do<ICommand>(action);
            }, delegate {
                Func<ICommand, bool> <>9__9;
                Func<ICommand, bool> func2 = <>9__9;
                if (<>9__9 == null)
                {
                    Func<ICommand, bool> local1 = <>9__9;
                    func2 = <>9__9 = x => x.CanExecute(printDialogViewModel.PdfViewModel);
                }
                return cancelCommand.Return<ICommand, bool>(func2, <>c.<>9__0_10 ??= () => true);
            }, useCommandManager);
            UICommand command2 = command3;
            UICommand[] dialogCommands = new UICommand[] { command, command2 };
            service.ShowDialog(dialogCommands, title, printDialogViewModel);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfDialogServiceExtensions.<>c <>9 = new PdfDialogServiceExtensions.<>c();
            public static Func<PdfDialogService, object> <>9__0_0;
            public static Func<bool> <>9__0_5;
            public static Func<bool> <>9__0_10;

            internal object <ShowDialog>b__0_0(PdfDialogService x) => 
                x.Content;

            internal bool <ShowDialog>b__0_10() => 
                true;

            internal bool <ShowDialog>b__0_5() => 
                true;
        }
    }
}

