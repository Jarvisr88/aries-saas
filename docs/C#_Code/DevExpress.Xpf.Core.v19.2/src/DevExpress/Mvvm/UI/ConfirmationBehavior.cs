namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Core;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class ConfirmationBehavior : Behavior<DependencyObject>
    {
        private static MessageBoxButton DefaultMessageBoxButton = MessageBoxButton.YesNo;
        public static readonly DependencyProperty EnableConfirmationMessageProperty = DependencyProperty.Register("EnableConfirmationMessage", typeof(bool), typeof(ConfirmationBehavior), new PropertyMetadata(true));
        public static readonly DependencyProperty CommandProperty;
        public static readonly DependencyProperty CommandParameterProperty;
        public static readonly DependencyProperty CommandPropertyNameProperty;
        public static readonly DependencyProperty MessageBoxServiceProperty;
        public static readonly DependencyProperty MessageTitleProperty;
        public static readonly DependencyProperty MessageTextProperty;
        public static readonly DependencyProperty MessageButtonProperty;
        public static readonly DependencyProperty MessageDefaultResultProperty;
        public static readonly DependencyProperty MessageIconProperty;
        internal DelegateCommand<object> ConfirmationCommand;

        static ConfirmationBehavior()
        {
            CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ConfirmationBehavior), new PropertyMetadata(null, (d, e) => ((ConfirmationBehavior) d).OnCommandChanged((ICommand) e.OldValue, (ICommand) e.NewValue)));
            CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(ConfirmationBehavior), new PropertyMetadata(null, (d, e) => ((ConfirmationBehavior) d).OnCommandParameterChanged()));
            CommandPropertyNameProperty = DependencyProperty.Register("CommandPropertyName", typeof(string), typeof(ConfirmationBehavior), new PropertyMetadata("Command"));
            MessageBoxServiceProperty = DependencyProperty.Register("MessageBoxService", typeof(IMessageBoxService), typeof(ConfirmationBehavior), new PropertyMetadata(null));
            MessageTitleProperty = DependencyProperty.Register("MessageTitle", typeof(string), typeof(ConfirmationBehavior), new PropertyMetadata("Confirmation"));
            MessageTextProperty = DependencyProperty.Register("MessageText", typeof(string), typeof(ConfirmationBehavior), new PropertyMetadata("Do you want to perform this action?"));
            MessageButtonProperty = DependencyProperty.Register("MessageButton", typeof(MessageBoxButton), typeof(ConfirmationBehavior), new PropertyMetadata(DefaultMessageBoxButton));
            MessageDefaultResultProperty = DependencyProperty.Register("MessageDefaultResult", typeof(MessageBoxResult), typeof(ConfirmationBehavior), new PropertyMetadata(null));
            MessageIconProperty = DependencyProperty.Register("MessageIcon", typeof(MessageBoxImage), typeof(ConfirmationBehavior), new PropertyMetadata(MessageBoxImage.None));
        }

        public ConfirmationBehavior()
        {
            this.ConfirmationCommand = new DelegateCommand<object>(new Action<object>(this.ConfirmationCommandExecute), new Func<object, bool>(this.ConfirmationCommandCanExecute), false);
        }

        private bool ConfirmationCommandCanExecute(object parameter)
        {
            if (this.Command == null)
            {
                return true;
            }
            object commandParameter = this.CommandParameter;
            object obj2 = commandParameter;
            if (commandParameter == null)
            {
                object local1 = commandParameter;
                obj2 = parameter;
            }
            return this.Command.CanExecute(obj2);
        }

        private void ConfirmationCommandExecute(object parameter)
        {
            ICommand command = this.Command;
            object obj2 = this.CommandParameter ?? parameter;
            if ((command != null) && this.ShowConfirmation())
            {
                object commandParameter = this.CommandParameter;
                object obj4 = commandParameter;
                if (commandParameter == null)
                {
                    object local2 = commandParameter;
                    obj4 = obj2;
                }
                command.Execute(obj4);
            }
        }

        internal IMessageBoxService GetActualService()
        {
            IMessageBoxService messageBoxService = this.MessageBoxService;
            if (messageBoxService != null)
            {
                return messageBoxService;
            }
            Func<DependencyObject, FrameworkElement> evaluator = <>c.<>9__52_0;
            if (<>c.<>9__52_0 == null)
            {
                Func<DependencyObject, FrameworkElement> local1 = <>c.<>9__52_0;
                evaluator = <>c.<>9__52_0 = x => x as FrameworkElement;
            }
            Func<FrameworkElement, object> func2 = <>c.<>9__52_1;
            if (<>c.<>9__52_1 == null)
            {
                Func<FrameworkElement, object> local2 = <>c.<>9__52_1;
                func2 = <>c.<>9__52_1 = x => x.DataContext;
            }
            ISupportServices services = base.AssociatedObject.With<DependencyObject, FrameworkElement>(evaluator).Return<FrameworkElement, object>(func2, null) as ISupportServices;
            if (services != null)
            {
                messageBoxService = services.ServiceContainer.GetService<IMessageBoxService>(ServiceSearchMode.PreferLocal);
            }
            return ((messageBoxService == null) ? new DXMessageBoxService() : messageBoxService);
        }

        private PropertyInfo GetCommandProperty() => 
            base.AssociatedObject.GetType().GetProperty(this.CommandPropertyName, BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance);

        protected override void OnAttached()
        {
            base.OnAttached();
            this.SetAssociatedObjectCommandProperty(this.ConfirmationCommand);
            if (this.Command != null)
            {
                this.Command.CanExecuteChanged -= new EventHandler(this.OnCommandCanExecuteChanged);
                this.Command.CanExecuteChanged += new EventHandler(this.OnCommandCanExecuteChanged);
            }
        }

        private void OnCommandCanExecuteChanged(object sender, EventArgs e)
        {
            this.ConfirmationCommand.RaiseCanExecuteChanged();
        }

        private void OnCommandChanged(ICommand oldValue, ICommand newValue)
        {
            if (oldValue != null)
            {
                oldValue.CanExecuteChanged -= new EventHandler(this.OnCommandCanExecuteChanged);
            }
            if (newValue != null)
            {
                newValue.CanExecuteChanged += new EventHandler(this.OnCommandCanExecuteChanged);
            }
            this.ConfirmationCommand.RaiseCanExecuteChanged();
        }

        private void OnCommandParameterChanged()
        {
            this.ConfirmationCommand.RaiseCanExecuteChanged();
        }

        protected override void OnDetaching()
        {
            if (this.Command != null)
            {
                this.Command.CanExecuteChanged -= new EventHandler(this.OnCommandCanExecuteChanged);
            }
            base.OnDetaching();
        }

        internal bool SetAssociatedObjectCommandProperty(object command)
        {
            PropertyInfo commandProperty = this.GetCommandProperty();
            if (commandProperty == null)
            {
                return false;
            }
            commandProperty.SetValue(base.AssociatedObject, command, null);
            return true;
        }

        private bool ShowConfirmation()
        {
            if (!this.EnableConfirmationMessage)
            {
                return true;
            }
            MessageBoxResult result = this.GetActualService().Show(this.MessageText, this.MessageTitle, this.MessageButton, this.MessageIcon, this.MessageDefaultResult);
            return ((result == MessageBoxResult.OK) || (result == MessageBoxResult.Yes));
        }

        public bool EnableConfirmationMessage
        {
            get => 
                (bool) base.GetValue(EnableConfirmationMessageProperty);
            set => 
                base.SetValue(EnableConfirmationMessageProperty, value);
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

        public string CommandPropertyName
        {
            get => 
                (string) base.GetValue(CommandPropertyNameProperty);
            set => 
                base.SetValue(CommandPropertyNameProperty, value);
        }

        public IMessageBoxService MessageBoxService
        {
            get => 
                (IMessageBoxService) base.GetValue(MessageBoxServiceProperty);
            set => 
                base.SetValue(MessageBoxServiceProperty, value);
        }

        public string MessageTitle
        {
            get => 
                (string) base.GetValue(MessageTitleProperty);
            set => 
                base.SetValue(MessageTitleProperty, value);
        }

        public string MessageText
        {
            get => 
                (string) base.GetValue(MessageTextProperty);
            set => 
                base.SetValue(MessageTextProperty, value);
        }

        public MessageBoxButton MessageButton
        {
            get => 
                (MessageBoxButton) base.GetValue(MessageButtonProperty);
            set => 
                base.SetValue(MessageButtonProperty, value);
        }

        public MessageBoxResult MessageDefaultResult
        {
            get => 
                (MessageBoxResult) base.GetValue(MessageDefaultResultProperty);
            set => 
                base.SetValue(MessageDefaultResultProperty, value);
        }

        public MessageBoxImage MessageIcon
        {
            get => 
                (MessageBoxImage) base.GetValue(MessageIconProperty);
            set => 
                base.SetValue(MessageIconProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ConfirmationBehavior.<>c <>9 = new ConfirmationBehavior.<>c();
            public static Func<DependencyObject, FrameworkElement> <>9__52_0;
            public static Func<FrameworkElement, object> <>9__52_1;

            internal void <.cctor>b__54_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ConfirmationBehavior) d).OnCommandChanged((ICommand) e.OldValue, (ICommand) e.NewValue);
            }

            internal void <.cctor>b__54_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ConfirmationBehavior) d).OnCommandParameterChanged();
            }

            internal FrameworkElement <GetActualService>b__52_0(DependencyObject x) => 
                x as FrameworkElement;

            internal object <GetActualService>b__52_1(FrameworkElement x) => 
                x.DataContext;
        }
    }
}

