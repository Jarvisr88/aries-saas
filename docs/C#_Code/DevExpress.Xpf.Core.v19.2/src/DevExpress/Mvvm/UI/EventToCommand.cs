namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class EventToCommand : EventToCommandBase
    {
        public static readonly DependencyProperty EventArgsConverterProperty = DependencyProperty.Register("EventArgsConverter", typeof(IEventArgsConverter), typeof(EventToCommand), new PropertyMetadata(null));
        public static readonly DependencyProperty PassEventArgsToCommandProperty = DependencyProperty.Register("PassEventArgsToCommand", typeof(bool?), typeof(EventToCommand), new PropertyMetadata(null));
        public static readonly DependencyProperty AllowChangingEventOwnerIsEnabledProperty;
        public static readonly DependencyProperty ModifierKeysProperty;

        static EventToCommand()
        {
            AllowChangingEventOwnerIsEnabledProperty = DependencyProperty.Register("AllowChangingEventOwnerIsEnabled", typeof(bool), typeof(EventToCommand), new PropertyMetadata(false, (d, e) => ((EventToCommand) d).UpdateIsEnabled()));
            ModifierKeysProperty = DependencyProperty.Register("ModifierKeys", typeof(System.Windows.Input.ModifierKeys?), typeof(EventToCommand), new PropertyMetadata(null));
        }

        protected override bool CanInvoke(object sender, object eventArgs)
        {
            bool flag = base.CanInvoke(sender, eventArgs);
            if (this.ModifierKeys != null)
            {
                System.Windows.Input.ModifierKeys? modifierKeys = this.ModifierKeys;
                System.Windows.Input.ModifierKeys modifiers = Keyboard.Modifiers;
                flag &= (((System.Windows.Input.ModifierKeys) modifierKeys.GetValueOrDefault()) == modifiers) ? (modifierKeys != null) : false;
            }
            return flag;
        }

        protected override void Invoke(object sender, object eventArgs)
        {
            object commandParameter = base.CommandParameter;
            if ((commandParameter == null) && this.ActualPassEventArgsToCommand)
            {
                commandParameter = (this.EventArgsConverter == null) ? eventArgs : this.EventArgsConverter.Convert(sender, eventArgs);
            }
            if (base.CommandCanExecute(commandParameter))
            {
                base.CommandExecute(commandParameter);
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.UpdateIsEnabled();
        }

        private void OnCommandCanExecuteChanged(object sender, EventArgs e)
        {
            this.UpdateIsEnabled();
        }

        protected override void OnCommandChanged(ICommand oldValue, ICommand newValue)
        {
            base.OnCommandChanged(oldValue, newValue);
            if (oldValue != null)
            {
                oldValue.CanExecuteChanged -= new EventHandler(this.OnCommandCanExecuteChanged);
            }
            if (newValue != null)
            {
                newValue.CanExecuteChanged += new EventHandler(this.OnCommandCanExecuteChanged);
            }
            this.UpdateIsEnabled();
        }

        protected override void OnCommandParameterChanged(object oldValue, object newValue)
        {
            base.OnCommandParameterChanged(oldValue, newValue);
            this.UpdateIsEnabled();
        }

        protected override void OnSourceChanged(object oldSource, object newSource)
        {
            base.OnSourceChanged(oldSource, newSource);
            this.UpdateIsEnabled();
        }

        private void UpdateIsEnabled()
        {
            if (base.Command != null)
            {
                FrameworkElement source = base.Source as FrameworkElement;
                if (this.AllowChangingEventOwnerIsEnabled && (source != null))
                {
                    source.IsEnabled = base.CommandCanExecute(base.CommandParameter);
                }
            }
        }

        public IEventArgsConverter EventArgsConverter
        {
            get => 
                (IEventArgsConverter) base.GetValue(EventArgsConverterProperty);
            set => 
                base.SetValue(EventArgsConverterProperty, value);
        }

        public bool? PassEventArgsToCommand
        {
            get => 
                (bool?) base.GetValue(PassEventArgsToCommandProperty);
            set => 
                base.SetValue(PassEventArgsToCommandProperty, value);
        }

        protected bool ActualPassEventArgsToCommand
        {
            get
            {
                bool? passEventArgsToCommand = this.PassEventArgsToCommand;
                return ((passEventArgsToCommand != null) ? passEventArgsToCommand.GetValueOrDefault() : (this.EventArgsConverter != null));
            }
        }

        public bool AllowChangingEventOwnerIsEnabled
        {
            get => 
                (bool) base.GetValue(AllowChangingEventOwnerIsEnabledProperty);
            set => 
                base.SetValue(AllowChangingEventOwnerIsEnabledProperty, value);
        }

        public System.Windows.Input.ModifierKeys? ModifierKeys
        {
            get => 
                (System.Windows.Input.ModifierKeys?) base.GetValue(ModifierKeysProperty);
            set => 
                base.SetValue(ModifierKeysProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EventToCommand.<>c <>9 = new EventToCommand.<>c();

            internal void <.cctor>b__27_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((EventToCommand) d).UpdateIsEnabled();
            }
        }
    }
}

