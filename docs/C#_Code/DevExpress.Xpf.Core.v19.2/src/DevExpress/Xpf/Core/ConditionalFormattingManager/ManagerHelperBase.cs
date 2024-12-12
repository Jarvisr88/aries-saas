namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class ManagerHelperBase
    {
        private static object ConvertDesignValue(object source)
        {
            object obj2 = source;
            TypeConverter converter = TypeDescriptor.GetConverter(source.GetType());
            if ((converter != null) && (converter.CanConvertFrom(typeof(string)) && converter.CanConvertTo(typeof(string))))
            {
                obj2 = converter.ConvertToInvariantString(source);
            }
            return obj2;
        }

        private static object ConvertEnum(Type fieldType, object source)
        {
            try
            {
                return Enum.Parse(fieldType, source.ToString());
            }
            catch
            {
                return source;
            }
        }

        public static object ConvertRuleValue(object source, Type fieldType, bool isInDesignMode) => 
            !fieldType.IsEnum ? (!isInDesignMode ? source : ConvertDesignValue(source)) : ConvertEnum(fieldType, source);

        public static void SetProperty(DependencyObject dependencyObject, DependencyProperty property, object value)
        {
            if (!Equals(dependencyObject.GetValue(property), value))
            {
                dependencyObject.SetValue(property, value);
            }
        }

        public static bool ShowDialog(object viewModel, string description, IDialogService service, Action<CancelEventArgs> executeMethod = null)
        {
            MessageBoxResult? defaultButton = null;
            defaultButton = null;
            List<UICommand> dialogCommands = UICommand.GenerateFromMessageBoxButton(MessageBoxButton.OKCancel, new DXDialogWindowMessageBoxButtonLocalizer(), defaultButton, defaultButton);
            if (executeMethod != null)
            {
                dialogCommands[0].Command = new DelegateCommand<CancelEventArgs>(executeMethod);
            }
            return (service.ShowDialog(dialogCommands, description, viewModel) == dialogCommands[0]);
        }
    }
}

