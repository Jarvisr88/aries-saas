namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Data;
    using DevExpress.Mvvm;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class ManagerHelper
    {
        public static string ConvertExpression(string expression, IDataColumnInfo info, Func<IDataColumnInfo, string, string> convertFunc) => 
            string.IsNullOrEmpty(expression) ? null : convertFunc(info, expression);

        public static object SafeGetValue(int index, object[] values) => 
            (index < values.Length) ? values[index] : null;

        public static void SetProperty(DependencyObject dependencyObject, DependencyProperty property, object value)
        {
            ManagerHelperBase.SetProperty(dependencyObject, property, value);
        }

        public static bool ShowDialog(object viewModel, string description, IDialogService service, Action<CancelEventArgs> executeMethod = null) => 
            ManagerHelperBase.ShowDialog(viewModel, description, service, executeMethod);
    }
}

