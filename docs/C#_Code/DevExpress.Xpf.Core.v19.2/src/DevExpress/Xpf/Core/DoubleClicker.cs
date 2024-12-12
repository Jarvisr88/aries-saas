namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class DoubleClicker : ContentControl
    {
        public static readonly DependencyProperty CommandParameterProperty = DependencyPropertyManager.Register("CommandParameter", typeof(object), typeof(DoubleClicker), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty CommandProperty = DependencyPropertyManager.Register("Command", typeof(ICommand), typeof(DoubleClicker), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty IsDoubleClickAreaProperty = DependencyProperty.RegisterAttached("IsDoubleClickArea", typeof(bool), typeof(DoubleClicker), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));

        public static bool GetIsDoubleClickArea(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (bool) element.GetValue(IsDoubleClickAreaProperty);
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if ((this.Command != null) && GetIsDoubleClickArea((DependencyObject) e.OriginalSource))
            {
                this.Command.Execute(this.CommandParameter);
            }
        }

        public static void SetIsDoubleClickArea(DependencyObject element, bool value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(IsDoubleClickAreaProperty, value);
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
    }
}

