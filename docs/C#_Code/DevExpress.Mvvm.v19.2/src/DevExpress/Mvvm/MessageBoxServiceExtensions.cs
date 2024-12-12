namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.CompilerServices;

    public static class MessageBoxServiceExtensions
    {
        public static bool? ShowMessage(this IMessageBoxService service, string messageBoxText) => 
            service.ShowMessage(messageBoxText, string.Empty);

        public static bool? ShowMessage(this IMessageBoxService service, string messageBoxText, string caption) => 
            service.ShowMessage(messageBoxText, caption, MessageButton.OK).ToBool();

        public static MessageResult ShowMessage(this IMessageBoxService service, string messageBoxText, string caption, MessageButton button) => 
            service.ShowMessage(messageBoxText, caption, button, MessageIcon.None);

        public static MessageResult ShowMessage(this IMessageBoxService service, string messageBoxText, string caption, MessageButton button, MessageIcon icon)
        {
            VerifyService(service);
            return service.Show(messageBoxText, caption, button, icon, MessageResult.None);
        }

        public static MessageResult ShowMessage(this IMessageBoxService service, string messageBoxText, string caption, MessageButton button, MessageIcon icon, MessageResult defaultResult)
        {
            VerifyService(service);
            return service.Show(messageBoxText, caption, button, icon, defaultResult);
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

