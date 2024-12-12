namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Windows;

    internal static class MessageBoxHelper
    {
        public static MessageBoxResult Show(MessageBoxButton buttons, MessageBoxImage icon, PreviewStringId formatId, params object[] args)
        {
            string str = PreviewLocalizer.GetString(formatId);
            return (!string.IsNullOrEmpty(str) ? DXMessageBox.Show(string.Format(str, args), null, buttons, icon) : MessageBoxResult.None);
        }
    }
}

