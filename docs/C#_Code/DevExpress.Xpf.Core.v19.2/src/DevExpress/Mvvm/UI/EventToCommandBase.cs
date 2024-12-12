namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Threading;

    public abstract class EventToCommandBase : DevExpress.Mvvm.UI.Interactivity.EventTrigger
    {
        public static readonly DependencyProperty CommandProperty;
        public static readonly DependencyProperty CommandParameterProperty;
        public static readonly DependencyProperty ProcessEventsFromDisabledEventOwnerProperty;
        public static readonly DependencyProperty MarkRoutedEventsAsHandledProperty;
        public static readonly DependencyProperty UseDispatcherProperty;
        public static readonly DependencyProperty CommandTargetProperty;
        public static readonly DependencyProperty DispatcherPriorityProperty;

        static EventToCommandBase()
        {
            CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(EventToCommandBase), new PropertyMetadata(null, (d, e) => ((EventToCommandBase) d).OnCommandChanged((ICommand) e.OldValue, (ICommand) e.NewValue)));
            CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(EventToCommandBase), new PropertyMetadata(null, (d, e) => ((EventToCommandBase) d).OnCommandParameterChanged(e.OldValue, e.NewValue)));
            ProcessEventsFromDisabledEventOwnerProperty = DependencyProperty.Register("ProcessEventsFromDisabledEventOwner", typeof(bool), typeof(EventToCommandBase), new PropertyMetadata(true));
            MarkRoutedEventsAsHandledProperty = DependencyProperty.Register("MarkRoutedEventsAsHandled", typeof(bool), typeof(EventToCommandBase), new PropertyMetadata(false));
            UseDispatcherProperty = DependencyProperty.Register("UseDispatcher", typeof(bool?), typeof(EventToCommandBase), new PropertyMetadata(null));
            CommandTargetProperty = DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(EventToCommandBase), new PropertyMetadata(null));
            DispatcherPriorityProperty = DependencyProperty.Register("DispatcherPriority", typeof(System.Windows.Threading.DispatcherPriority?), typeof(EventToCommandBase), new PropertyMetadata(null));
        }

        protected EventToCommandBase()
        {
        }

        protected virtual bool CanInvoke(object sender, object eventArgs)
        {
            FrameworkElement source = base.Source as FrameworkElement;
            return (this.ProcessEventsFromDisabledEventOwner || ((source == null) || source.IsEnabled));
        }

        protected bool CommandCanExecute(object commandParameter) => 
            !(this.Command is RoutedCommand) ? this.Command.CanExecute(commandParameter) : ((RoutedCommand) this.Command).CanExecute(commandParameter, this.CommandTarget);

        protected void CommandExecute(object commandParameter)
        {
            if (this.Command is RoutedCommand)
            {
                ((RoutedCommand) this.Command).Execute(commandParameter, this.CommandTarget);
            }
            else
            {
                this.Command.Execute(commandParameter);
            }
        }

        protected abstract void Invoke(object sender, object eventArgs);
        protected virtual void OnCommandChanged()
        {
        }

        protected virtual void OnCommandChanged(ICommand oldValue, ICommand newValue)
        {
            this.OnCommandChanged();
        }

        protected virtual void OnCommandParameterChanged()
        {
        }

        protected virtual void OnCommandParameterChanged(object oldValue, object newValue)
        {
            this.OnCommandParameterChanged();
        }

        protected override void OnEvent(object sender, object eventArgs)
        {
            base.OnEvent(sender, eventArgs);
            if (this.Command != null)
            {
                this.OnEventCore(sender, eventArgs);
            }
            else
            {
                bool flag = BindingOperations.GetBindingExpression(this, CommandProperty) != null;
                if (ReferenceEquals(this.Command, null) & flag)
                {
                    base.Dispatcher.BeginInvoke(() => this.OnEventCore(sender, eventArgs), new object[0]);
                }
            }
        }

        private void OnEventCore(object sender, object eventArgs)
        {
            if ((this.Command != null) && this.CanInvoke(sender, eventArgs))
            {
                if (!this.ActualUseDispatcher)
                {
                    this.Invoke(sender, eventArgs);
                }
                else
                {
                    object[] args = new object[] { sender, eventArgs };
                    base.Dispatcher.BeginInvoke(new Action<object, object>(this.Invoke), this.ActualDispatcherPriority, args);
                }
                if (this.MarkRoutedEventsAsHandled && (eventArgs is RoutedEventArgs))
                {
                    ((RoutedEventArgs) eventArgs).Handled = true;
                }
            }
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

        public bool ProcessEventsFromDisabledEventOwner
        {
            get => 
                (bool) base.GetValue(ProcessEventsFromDisabledEventOwnerProperty);
            set => 
                base.SetValue(ProcessEventsFromDisabledEventOwnerProperty, value);
        }

        public bool MarkRoutedEventsAsHandled
        {
            get => 
                (bool) base.GetValue(MarkRoutedEventsAsHandledProperty);
            set => 
                base.SetValue(MarkRoutedEventsAsHandledProperty, value);
        }

        public bool? UseDispatcher
        {
            get => 
                (bool?) base.GetValue(UseDispatcherProperty);
            set => 
                base.SetValue(UseDispatcherProperty, value);
        }

        protected internal bool ActualUseDispatcher =>
            (this.UseDispatcher != null) ? this.UseDispatcher.Value : (this.DispatcherPriority != null);

        public IInputElement CommandTarget
        {
            get => 
                (IInputElement) base.GetValue(CommandTargetProperty);
            set => 
                base.SetValue(CommandTargetProperty, value);
        }

        public System.Windows.Threading.DispatcherPriority? DispatcherPriority
        {
            get => 
                (System.Windows.Threading.DispatcherPriority?) base.GetValue(DispatcherPriorityProperty);
            set => 
                base.SetValue(DispatcherPriorityProperty, value);
        }

        protected internal System.Windows.Threading.DispatcherPriority ActualDispatcherPriority
        {
            get
            {
                System.Windows.Threading.DispatcherPriority? dispatcherPriority = this.DispatcherPriority;
                return ((dispatcherPriority != null) ? dispatcherPriority.GetValueOrDefault() : System.Windows.Threading.DispatcherPriority.Normal);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EventToCommandBase.<>c <>9 = new EventToCommandBase.<>c();

            internal void <.cctor>b__43_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((EventToCommandBase) d).OnCommandChanged((ICommand) e.OldValue, (ICommand) e.NewValue);
            }

            internal void <.cctor>b__43_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((EventToCommandBase) d).OnCommandParameterChanged(e.OldValue, e.NewValue);
            }
        }
    }
}

