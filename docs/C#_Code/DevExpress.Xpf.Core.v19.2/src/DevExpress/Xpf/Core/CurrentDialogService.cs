namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Bars.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class CurrentDialogService : CurrentWindowService, ICurrentDialogService, ICurrentWindowService
    {
        private void Close(UICommand dialogResult)
        {
            if (base.ActualWindow is ThemedWindow)
            {
                this.ThemedWindowClose(dialogResult);
            }
            else
            {
                this.DXDialogWindowClose(dialogResult);
            }
        }

        void ICurrentDialogService.Close(MessageResult dialogResult)
        {
            if (!(base.ActualWindow is ThemedWindow))
            {
                this.Close(this.GetUICommands().With<IEnumerable<UICommand>, UICommand>(x => DialogButton.GetUICommandByDialogResult(x, dialogResult)));
            }
            else
            {
                Func<ThemedWindowDialogButtonsControl, bool> predicate = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<ThemedWindowDialogButtonsControl, bool> local1 = <>c.<>9__2_0;
                    predicate = <>c.<>9__2_0 = x => ((UIElement) x.Parent).Visibility == Visibility.Visible;
                }
                ThemedWindowDialogButtonsControl child = TreeHelper.GetChild<ThemedWindowDialogButtonsControl>((ThemedWindow) base.ActualWindow, predicate);
                if (child == null)
                {
                    ((ICurrentWindowService) this).Close();
                }
                else
                {
                    UICommand command;
                    UICommand command2 = child.FindUICommand(dialogResult.ToMessageBoxResult());
                    if (command == null)
                    {
                        UICommand local2 = child.FindUICommand(dialogResult.ToMessageBoxResult());
                        UICommand command1 = new UICommand();
                        command1.Id = dialogResult.ToMessageBoxResult();
                        command1.Tag = dialogResult.ToMessageBoxResult();
                        command2 = command1;
                    }
                    command = command2;
                    this.Close(command);
                }
            }
        }

        void ICurrentDialogService.Close(UICommand dialogResult)
        {
            this.Close(dialogResult);
        }

        private void DXDialogWindowClose(UICommand dialogResult)
        {
            ICommand command = new DialogButton.UICommandWrapper(dialogResult, null, () => ((ICurrentWindowService) this).Close());
            CancelEventArgs parameter = new CancelEventArgs();
            if ((command != null) && command.CanExecute(parameter))
            {
                command.Execute(parameter);
            }
        }

        private IEnumerable<UICommand> GetUICommands()
        {
            if (!(base.ActualWindow is ThemedWindow))
            {
                if (!(base.ActualWindow is DXDialogWindow))
                {
                    return null;
                }
                Func<IEnumerable, IEnumerable<UICommand>> evaluator = <>c.<>9__7_0;
                if (<>c.<>9__7_0 == null)
                {
                    Func<IEnumerable, IEnumerable<UICommand>> local3 = <>c.<>9__7_0;
                    evaluator = <>c.<>9__7_0 = x => x.Cast<object>().Select<object, UICommand>(new Func<object, UICommand>(UICommandContainer.GetUICommand));
                }
                return DialogButtonsControl.GetCommandsSource(base.GetActualWindow()).With<IEnumerable, IEnumerable<UICommand>>(evaluator);
            }
            ThemedWindow actualWindow = base.ActualWindow as ThemedWindow;
            if (actualWindow == null)
            {
                ThemedWindow local1 = actualWindow;
                return null;
            }
            IEnumerable actualDialogButtons = actualWindow.ActualDialogButtons;
            if (actualDialogButtons != null)
            {
                return actualDialogButtons.OfType<UICommand>();
            }
            IEnumerable local2 = actualDialogButtons;
            return null;
        }

        private void ThemedWindowClose(UICommand dialogResult)
        {
            ((ThemedWindow) base.ActualWindow).CloseDialog(dialogResult);
        }

        IEnumerable<UICommand> ICurrentDialogService.UICommands =>
            this.GetUICommands();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CurrentDialogService.<>c <>9 = new CurrentDialogService.<>c();
            public static Func<ThemedWindowDialogButtonsControl, bool> <>9__2_0;
            public static Func<IEnumerable, IEnumerable<UICommand>> <>9__7_0;

            internal bool <DevExpress.Mvvm.ICurrentDialogService.Close>b__2_0(ThemedWindowDialogButtonsControl x) => 
                ((UIElement) x.Parent).Visibility == Visibility.Visible;

            internal IEnumerable<UICommand> <GetUICommands>b__7_0(IEnumerable x) => 
                x.Cast<object>().Select<object, UICommand>(new Func<object, UICommand>(UICommandContainer.GetUICommand));
        }
    }
}

