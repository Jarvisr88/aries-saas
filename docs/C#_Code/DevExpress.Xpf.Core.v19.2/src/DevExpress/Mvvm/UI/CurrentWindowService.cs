namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    [TargetType(typeof(UserControl)), TargetType(typeof(Window))]
    public class CurrentWindowService : WindowAwareServiceBase, ICurrentWindowService
    {
        public static readonly DependencyProperty ClosingCommandProperty = DependencyProperty.Register("ClosingCommand", typeof(ICommand), typeof(CurrentWindowService), new PropertyMetadata(null));

        void ICurrentWindowService.Activate()
        {
            Action<Window> action = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Action<Window> local1 = <>c.<>9__6_0;
                action = <>c.<>9__6_0 = x => x.Activate();
            }
            this.GetActualWindow().Do<Window>(action);
        }

        void ICurrentWindowService.Close()
        {
            Action<Window> action = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Action<Window> local1 = <>c.<>9__5_0;
                action = <>c.<>9__5_0 = x => x.Close();
            }
            this.GetActualWindow().Do<Window>(action);
        }

        void ICurrentWindowService.Hide()
        {
            Action<Window> action = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Action<Window> local1 = <>c.<>9__7_0;
                action = <>c.<>9__7_0 = x => x.Hide();
            }
            this.GetActualWindow().Do<Window>(action);
        }

        void ICurrentWindowService.SetWindowState(WindowState state)
        {
            this.GetActualWindow().Do<Window>(x => x.WindowState = state);
        }

        void ICurrentWindowService.Show()
        {
            Action<Window> action = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Action<Window> local1 = <>c.<>9__9_0;
                action = <>c.<>9__9_0 = x => x.Show();
            }
            this.GetActualWindow().Do<Window>(action);
        }

        protected Window GetActualWindow()
        {
            if (base.ActualWindow == null)
            {
                base.UpdateActualWindow();
            }
            return base.ActualWindow;
        }

        protected override void OnActualWindowChanged(Window oldWindow)
        {
            oldWindow.Do<Window>(delegate (Window x) {
                x.Closing -= new CancelEventHandler(this.OnClosing);
            });
            base.ActualWindow.Do<Window>(delegate (Window x) {
                x.Closing += new CancelEventHandler(this.OnClosing);
            });
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            this.ClosingCommand.Do<ICommand>(x => x.Execute(e));
        }

        public ICommand ClosingCommand
        {
            get => 
                (ICommand) base.GetValue(ClosingCommandProperty);
            set => 
                base.SetValue(ClosingCommandProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CurrentWindowService.<>c <>9 = new CurrentWindowService.<>c();
            public static Action<Window> <>9__5_0;
            public static Action<Window> <>9__6_0;
            public static Action<Window> <>9__7_0;
            public static Action<Window> <>9__9_0;

            internal void <DevExpress.Mvvm.ICurrentWindowService.Activate>b__6_0(Window x)
            {
                x.Activate();
            }

            internal void <DevExpress.Mvvm.ICurrentWindowService.Close>b__5_0(Window x)
            {
                x.Close();
            }

            internal void <DevExpress.Mvvm.ICurrentWindowService.Hide>b__7_0(Window x)
            {
                x.Hide();
            }

            internal void <DevExpress.Mvvm.ICurrentWindowService.Show>b__9_0(Window x)
            {
                x.Show();
            }
        }
    }
}

