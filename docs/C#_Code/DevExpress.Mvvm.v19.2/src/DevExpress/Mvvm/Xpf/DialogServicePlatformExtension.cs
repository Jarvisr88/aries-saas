namespace DevExpress.Mvvm.Xpf
{
    using DevExpress.Mvvm;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public static class DialogServicePlatformExtension
    {
        private static MessageBoxResult GetMessageBoxResult(UICommand result) => 
            (result != null) ? ((MessageBoxResult) result.Tag) : MessageBoxResult.None;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static MessageBoxResult ShowDialog(this IDialogService service, MessageBoxButton dialogButtons, string title, object viewModel)
        {
            DialogServiceExtensions.VerifyService(service);
            MessageBoxResult? defaultButton = null;
            defaultButton = null;
            return GetMessageBoxResult(service.ShowDialog(UICommand.GenerateFromMessageBoxButton(dialogButtons, DialogServiceExtensions.GetLocalizer(service), defaultButton, defaultButton), title, null, viewModel, null, null));
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static MessageBoxResult ShowDialog(this IDialogService service, MessageBoxButton dialogButtons, string title, string documentType, object viewModel)
        {
            DialogServiceExtensions.VerifyService(service);
            MessageBoxResult? defaultButton = null;
            defaultButton = null;
            return GetMessageBoxResult(service.ShowDialog(UICommand.GenerateFromMessageBoxButton(dialogButtons, DialogServiceExtensions.GetLocalizer(service), defaultButton, defaultButton), title, documentType, viewModel, null, null));
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static MessageBoxResult ShowDialog(this IDialogService service, MessageBoxButton dialogButtons, string title, string documentType, object parameter, object parentViewModel)
        {
            DialogServiceExtensions.VerifyService(service);
            MessageBoxResult? defaultButton = null;
            defaultButton = null;
            return GetMessageBoxResult(service.ShowDialog(UICommand.GenerateFromMessageBoxButton(dialogButtons, DialogServiceExtensions.GetLocalizer(service), defaultButton, defaultButton), title, documentType, null, parameter, parentViewModel));
        }
    }
}

