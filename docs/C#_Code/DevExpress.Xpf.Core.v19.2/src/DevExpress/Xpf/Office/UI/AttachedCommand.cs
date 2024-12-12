namespace DevExpress.Xpf.Office.UI
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class AttachedCommand : DependencyObject
    {
        public static readonly DependencyProperty CommandProperty;

        static AttachedCommand()
        {
            Type ownerType = typeof(AttachedCommand);
            CommandProperty = DependencyPropertyManager.RegisterAttached("Command", typeof(ICommand), ownerType, new FrameworkPropertyMetadata());
        }

        public static ICommand GetCommand(DependencyObject element) => 
            (element != null) ? (element.GetValue(CommandProperty) as ICommand) : null;

        public static void SetCommand(DependencyObject element, ICommand value)
        {
            if (element != null)
            {
                element.SetValue(CommandProperty, value);
            }
        }
    }
}

