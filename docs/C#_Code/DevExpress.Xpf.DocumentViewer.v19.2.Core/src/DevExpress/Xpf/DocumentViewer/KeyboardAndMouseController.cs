namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class KeyboardAndMouseController
    {
        protected readonly DocumentPresenterControl presenter;

        public KeyboardAndMouseController(DocumentPresenterControl documentPresenter)
        {
            this.presenter = documentPresenter;
        }

        public virtual void ProcessKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Prior:
                {
                    Func<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand> evaluator = <>c.<>9__10_8;
                    if (<>c.<>9__10_8 == null)
                    {
                        Func<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand> local9 = <>c.<>9__10_8;
                        evaluator = <>c.<>9__10_8 = x => x.ScrollCommand;
                    }
                    Action<ICommand> action = <>c.<>9__10_9;
                    if (<>c.<>9__10_9 == null)
                    {
                        Action<ICommand> local10 = <>c.<>9__10_9;
                        action = <>c.<>9__10_9 = x => x.Execute(ScrollCommand.PageUp);
                    }
                    this.CommandProvider.With<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                    return;
                }
                case Key.Next:
                {
                    Func<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand> evaluator = <>c.<>9__10_10;
                    if (<>c.<>9__10_10 == null)
                    {
                        Func<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand> local11 = <>c.<>9__10_10;
                        evaluator = <>c.<>9__10_10 = x => x.ScrollCommand;
                    }
                    Action<ICommand> action = <>c.<>9__10_11;
                    if (<>c.<>9__10_11 == null)
                    {
                        Action<ICommand> local12 = <>c.<>9__10_11;
                        action = <>c.<>9__10_11 = x => x.Execute(ScrollCommand.PageDown);
                    }
                    this.CommandProvider.With<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                    return;
                }
                case Key.End:
                {
                    Func<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand> evaluator = <>c.<>9__10_14;
                    if (<>c.<>9__10_14 == null)
                    {
                        Func<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand> local15 = <>c.<>9__10_14;
                        evaluator = <>c.<>9__10_14 = x => x.ScrollCommand;
                    }
                    Action<ICommand> action = <>c.<>9__10_15;
                    if (<>c.<>9__10_15 == null)
                    {
                        Action<ICommand> local16 = <>c.<>9__10_15;
                        action = <>c.<>9__10_15 = x => x.Execute(ScrollCommand.End);
                    }
                    this.CommandProvider.With<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                    return;
                }
                case Key.Home:
                {
                    Func<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand> evaluator = <>c.<>9__10_12;
                    if (<>c.<>9__10_12 == null)
                    {
                        Func<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand> local13 = <>c.<>9__10_12;
                        evaluator = <>c.<>9__10_12 = x => x.ScrollCommand;
                    }
                    Action<ICommand> action = <>c.<>9__10_13;
                    if (<>c.<>9__10_13 == null)
                    {
                        Action<ICommand> local14 = <>c.<>9__10_13;
                        action = <>c.<>9__10_13 = x => x.Execute(ScrollCommand.Home);
                    }
                    this.CommandProvider.With<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                    return;
                }
                case Key.Left:
                {
                    Func<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand> evaluator = <>c.<>9__10_4;
                    if (<>c.<>9__10_4 == null)
                    {
                        Func<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand> local5 = <>c.<>9__10_4;
                        evaluator = <>c.<>9__10_4 = x => x.ScrollCommand;
                    }
                    Action<ICommand> action = <>c.<>9__10_5;
                    if (<>c.<>9__10_5 == null)
                    {
                        Action<ICommand> local6 = <>c.<>9__10_5;
                        action = <>c.<>9__10_5 = x => x.Execute(ScrollCommand.LineLeft);
                    }
                    this.CommandProvider.With<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                    return;
                }
                case Key.Up:
                {
                    Func<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand> evaluator = <>c.<>9__10_0;
                    if (<>c.<>9__10_0 == null)
                    {
                        Func<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand> local1 = <>c.<>9__10_0;
                        evaluator = <>c.<>9__10_0 = x => x.ScrollCommand;
                    }
                    Action<ICommand> action = <>c.<>9__10_1;
                    if (<>c.<>9__10_1 == null)
                    {
                        Action<ICommand> local2 = <>c.<>9__10_1;
                        action = <>c.<>9__10_1 = x => x.Execute(ScrollCommand.LineUp);
                    }
                    this.CommandProvider.With<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                    return;
                }
                case Key.Right:
                {
                    Func<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand> evaluator = <>c.<>9__10_6;
                    if (<>c.<>9__10_6 == null)
                    {
                        Func<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand> local7 = <>c.<>9__10_6;
                        evaluator = <>c.<>9__10_6 = x => x.ScrollCommand;
                    }
                    Action<ICommand> action = <>c.<>9__10_7;
                    if (<>c.<>9__10_7 == null)
                    {
                        Action<ICommand> local8 = <>c.<>9__10_7;
                        action = <>c.<>9__10_7 = x => x.Execute(ScrollCommand.LineRight);
                    }
                    this.CommandProvider.With<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                    return;
                }
                case Key.Down:
                {
                    Func<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand> evaluator = <>c.<>9__10_2;
                    if (<>c.<>9__10_2 == null)
                    {
                        Func<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand> local3 = <>c.<>9__10_2;
                        evaluator = <>c.<>9__10_2 = x => x.ScrollCommand;
                    }
                    Action<ICommand> action = <>c.<>9__10_3;
                    if (<>c.<>9__10_3 == null)
                    {
                        Action<ICommand> local4 = <>c.<>9__10_3;
                        action = <>c.<>9__10_3 = x => x.Execute(ScrollCommand.LineDown);
                    }
                    this.CommandProvider.With<DevExpress.Xpf.DocumentViewer.CommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                    return;
                }
            }
        }

        public virtual void ProcessMouseLeftButtonDown(MouseButtonEventArgs e)
        {
        }

        public virtual void ProcessMouseLeftButtonUp(MouseButtonEventArgs e)
        {
        }

        public virtual void ProcessMouseMove(MouseEventArgs e)
        {
        }

        public virtual void ProcessMouseRightButtonDown(MouseButtonEventArgs e)
        {
        }

        public virtual void ProcessMouseRightButtonUp(MouseButtonEventArgs e)
        {
        }

        public virtual void ProcessMouseWheel(MouseWheelEventArgs e)
        {
            if (KeyboardHelper.IsControlPressed)
            {
                Point position = e.GetPosition(this.ItemsPanel);
                this.NavigationStrategy.ZoomToAnchorPoint(e.Delta > 0, position);
                e.Handled = true;
            }
        }

        protected DevExpress.Xpf.DocumentViewer.CommandProvider CommandProvider =>
            this.presenter.ActualDocumentViewer.ActualCommandProvider;

        protected DevExpress.Xpf.DocumentViewer.BehaviorProvider BehaviorProvider =>
            this.presenter.BehaviorProvider;

        protected DocumentViewerPanel ItemsPanel =>
            this.presenter.ItemsPanel;

        protected DevExpress.Xpf.DocumentViewer.NavigationStrategy NavigationStrategy =>
            this.presenter.NavigationStrategy;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly KeyboardAndMouseController.<>c <>9 = new KeyboardAndMouseController.<>c();
            public static Func<CommandProvider, ICommand> <>9__10_0;
            public static Action<ICommand> <>9__10_1;
            public static Func<CommandProvider, ICommand> <>9__10_2;
            public static Action<ICommand> <>9__10_3;
            public static Func<CommandProvider, ICommand> <>9__10_4;
            public static Action<ICommand> <>9__10_5;
            public static Func<CommandProvider, ICommand> <>9__10_6;
            public static Action<ICommand> <>9__10_7;
            public static Func<CommandProvider, ICommand> <>9__10_8;
            public static Action<ICommand> <>9__10_9;
            public static Func<CommandProvider, ICommand> <>9__10_10;
            public static Action<ICommand> <>9__10_11;
            public static Func<CommandProvider, ICommand> <>9__10_12;
            public static Action<ICommand> <>9__10_13;
            public static Func<CommandProvider, ICommand> <>9__10_14;
            public static Action<ICommand> <>9__10_15;

            internal ICommand <ProcessKeyDown>b__10_0(CommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDown>b__10_1(ICommand x)
            {
                x.Execute(ScrollCommand.LineUp);
            }

            internal ICommand <ProcessKeyDown>b__10_10(CommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDown>b__10_11(ICommand x)
            {
                x.Execute(ScrollCommand.PageDown);
            }

            internal ICommand <ProcessKeyDown>b__10_12(CommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDown>b__10_13(ICommand x)
            {
                x.Execute(ScrollCommand.Home);
            }

            internal ICommand <ProcessKeyDown>b__10_14(CommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDown>b__10_15(ICommand x)
            {
                x.Execute(ScrollCommand.End);
            }

            internal ICommand <ProcessKeyDown>b__10_2(CommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDown>b__10_3(ICommand x)
            {
                x.Execute(ScrollCommand.LineDown);
            }

            internal ICommand <ProcessKeyDown>b__10_4(CommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDown>b__10_5(ICommand x)
            {
                x.Execute(ScrollCommand.LineLeft);
            }

            internal ICommand <ProcessKeyDown>b__10_6(CommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDown>b__10_7(ICommand x)
            {
                x.Execute(ScrollCommand.LineRight);
            }

            internal ICommand <ProcessKeyDown>b__10_8(CommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDown>b__10_9(ICommand x)
            {
                x.Execute(ScrollCommand.PageUp);
            }
        }
    }
}

