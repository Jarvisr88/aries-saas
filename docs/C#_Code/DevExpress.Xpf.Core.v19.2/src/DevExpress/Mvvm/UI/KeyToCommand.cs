namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class KeyToCommand : EventToCommandBase
    {
        public static readonly DependencyProperty KeyGestureProperty = DependencyProperty.Register("KeyGesture", typeof(System.Windows.Input.KeyGesture), typeof(KeyToCommand), new PropertyMetadata(null));

        static KeyToCommand()
        {
            EventTriggerBase<DependencyObject>.EventNameProperty.OverrideMetadata(typeof(KeyToCommand), new PropertyMetadata("KeyUp"));
        }

        protected override bool CanInvoke(object sender, object eventArgs)
        {
            bool flag = base.CanInvoke(sender, eventArgs);
            if ((this.KeyGesture == null) || !(eventArgs is InputEventArgs))
            {
                return flag;
            }
            InputEventArgs inputEventArgs = (InputEventArgs) eventArgs;
            return (flag && this.KeyGesture.Matches(base.Source, inputEventArgs));
        }

        protected override void Invoke(object sender, object eventArgs)
        {
            if (base.CommandCanExecute(base.CommandParameter))
            {
                base.CommandExecute(base.CommandParameter);
            }
        }

        public System.Windows.Input.KeyGesture KeyGesture
        {
            get => 
                (System.Windows.Input.KeyGesture) base.GetValue(KeyGestureProperty);
            set => 
                base.SetValue(KeyGestureProperty, value);
        }
    }
}

