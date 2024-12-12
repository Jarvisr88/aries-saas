namespace DevExpress.Mvvm
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public static class MessageBoxServicePlatformExtensions
    {
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static MessageBoxResult Show(this IMessageBoxService service, string messageBoxText) => 
            service.Show(messageBoxText, string.Empty);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static MessageBoxResult Show(this IMessageBoxService service, string messageBoxText, string caption) => 
            service.ShowMessage(messageBoxText, caption, MessageButton.OK).ToMessageBoxResult();

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static MessageBoxResult Show(this IMessageBoxService service, string messageBoxText, string caption, MessageBoxButton button) => 
            service.Show(messageBoxText, caption, button, MessageBoxImage.None);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static MessageBoxResult Show(this IMessageBoxService service, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon) => 
            service.Show(messageBoxText, caption, button, icon, MessageBoxResult.None);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static MessageBoxResult Show(this IMessageBoxService service, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            VerifyService(service);
            return service.Show(messageBoxText, caption, button.ToMessageButton(), icon.ToMessageIcon(), defaultResult.ToMessageResult()).ToMessageBoxResult();
        }

        private static void VerifyService(IMessageBoxService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
        }
    }
}

