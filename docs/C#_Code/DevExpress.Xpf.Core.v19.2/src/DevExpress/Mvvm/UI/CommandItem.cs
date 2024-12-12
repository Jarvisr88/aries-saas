namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class CommandItem : DependencyObjectExt
    {
        public static readonly DependencyProperty CommandProperty;
        public static readonly DependencyProperty CommandParameterProperty;
        public static readonly DependencyProperty CheckCanExecuteProperty;
        public static readonly DependencyProperty CanExecuteProperty;
        private static readonly DependencyPropertyKey CanExecutePropertyKey;
        private CanExecuteChangedEventHandler<CommandItem> commandCanExecuteChangedHandler;

        static CommandItem()
        {
            Type ownerType = typeof(CommandItem);
            CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), ownerType, new PropertyMetadata(null, (d, e) => ((CommandItem) d).OnCommandChanged(e)));
            CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), ownerType, new PropertyMetadata(null, (d, e) => ((CommandItem) d).UpdateCanExecute()));
            CheckCanExecuteProperty = DependencyProperty.Register("CheckCanExecute", typeof(bool), ownerType, new PropertyMetadata(true, (d, e) => ((CommandItem) d).UpdateCanExecute()));
            CanExecutePropertyKey = DependencyProperty.RegisterReadOnly("CanExecute", typeof(bool), ownerType, new PropertyMetadata(false));
            CanExecuteProperty = CanExecutePropertyKey.DependencyProperty;
        }

        public CommandItem()
        {
            Action<CommandItem, object, EventArgs> onEventAction = <>c.<>9__19_0;
            if (<>c.<>9__19_0 == null)
            {
                Action<CommandItem, object, EventArgs> local1 = <>c.<>9__19_0;
                onEventAction = <>c.<>9__19_0 = (owner, o, e) => owner.OnCommandCanExecuteChanged(o, e);
            }
            this.commandCanExecuteChangedHandler = new CanExecuteChangedEventHandler<CommandItem>(this, onEventAction);
        }

        public bool ExecuteCommand()
        {
            if (!this.CanExecute)
            {
                return false;
            }
            this.Command.Execute(this.CommandParameter);
            return true;
        }

        private void OnCommandCanExecuteChanged(object sender, EventArgs e)
        {
            this.UpdateCanExecute();
        }

        private void OnCommandChanged(DependencyPropertyChangedEventArgs e)
        {
            Func<object, ICommand> evaluator = <>c.<>9__21_0;
            if (<>c.<>9__21_0 == null)
            {
                Func<object, ICommand> local1 = <>c.<>9__21_0;
                evaluator = <>c.<>9__21_0 = x => x as ICommand;
            }
            e.OldValue.With<object, ICommand>(evaluator).Do<ICommand>(delegate (ICommand o) {
                o.CanExecuteChanged -= this.commandCanExecuteChangedHandler.Handler;
            });
            Func<object, ICommand> func2 = <>c.<>9__21_2;
            if (<>c.<>9__21_2 == null)
            {
                Func<object, ICommand> local2 = <>c.<>9__21_2;
                func2 = <>c.<>9__21_2 = x => x as ICommand;
            }
            e.NewValue.With<object, ICommand>(func2).Do<ICommand>(delegate (ICommand o) {
                o.CanExecuteChanged += this.commandCanExecuteChangedHandler.Handler;
            });
            this.UpdateCanExecute();
        }

        private void UpdateCanExecute()
        {
            this.CanExecute = (this.Command != null) && (!this.CheckCanExecute || this.Command.CanExecute(this.CommandParameter));
        }

        public ICommand Command
        {
            get => 
                (ICommand) base.GetValue(CommandProperty);
            set => 
                base.SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => 
                base.GetValue(CommandParameterProperty);
            set => 
                base.SetValue(CommandParameterProperty, value);
        }

        public bool CheckCanExecute
        {
            get => 
                (bool) base.GetValue(CheckCanExecuteProperty);
            set => 
                base.SetValue(CheckCanExecuteProperty, value);
        }

        public bool CanExecute
        {
            get => 
                (bool) base.GetValue(CanExecuteProperty);
            private set => 
                base.SetValue(CanExecutePropertyKey, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CommandItem.<>c <>9 = new CommandItem.<>c();
            public static Action<CommandItem, object, EventArgs> <>9__19_0;
            public static Func<object, ICommand> <>9__21_0;
            public static Func<object, ICommand> <>9__21_2;

            internal void <.cctor>b__5_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CommandItem) d).OnCommandChanged(e);
            }

            internal void <.cctor>b__5_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CommandItem) d).UpdateCanExecute();
            }

            internal void <.cctor>b__5_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CommandItem) d).UpdateCanExecute();
            }

            internal void <.ctor>b__19_0(CommandItem owner, object o, EventArgs e)
            {
                owner.OnCommandCanExecuteChanged(o, e);
            }

            internal ICommand <OnCommandChanged>b__21_0(object x) => 
                x as ICommand;

            internal ICommand <OnCommandChanged>b__21_2(object x) => 
                x as ICommand;
        }
    }
}

