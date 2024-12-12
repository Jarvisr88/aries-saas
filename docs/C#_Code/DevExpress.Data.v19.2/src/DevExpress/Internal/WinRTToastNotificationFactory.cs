namespace DevExpress.Internal
{
    using DevExpress.Data.Controls.WinrtToastNotifier.WinApi;
    using DevExpress.Internal.WinApi;
    using System;

    public class WinRTToastNotificationFactory : IPredefinedToastNotificationFactory
    {
        private readonly string appId;
        private IPredefinedToastNotificationContentFactory factoryCore;

        public WinRTToastNotificationFactory(string appId)
        {
            this.appId = appId;
        }

        public virtual IPredefinedToastNotificationContentFactory CreateContentFactory() => 
            new WinRTToastNotificationContentFactory();

        public IPredefinedToastNotification CreateToastNotification(IPredefinedToastNotificationContent content) => 
            new WinRTToastNotification(content, () => ToastNotificationManager.CreateToastNotificationAdapter(this.appId));

        public IPredefinedToastNotification CreateToastNotification(string bodyText) => 
            this.CreateToastNotification(this.DefaultFactory.CreateContent(bodyText));

        public IPredefinedToastNotification CreateToastNotificationOneLineHeaderContent(string headlineText, string bodyText) => 
            this.CreateToastNotification(this.DefaultFactory.CreateOneLineHeaderContent(headlineText, bodyText));

        public IPredefinedToastNotification CreateToastNotificationOneLineHeaderContent(string headlineText, string bodyText1, string bodyText2) => 
            this.CreateToastNotification(this.DefaultFactory.CreateOneLineHeaderContent(headlineText, bodyText1, bodyText2));

        public IPredefinedToastNotification CreateToastNotificationTwoLineHeader(string headlineText, string bodyText) => 
            this.CreateToastNotification(this.DefaultFactory.CreateTwoLineHeaderContent(headlineText, bodyText));

        private IPredefinedToastNotificationContentFactory DefaultFactory
        {
            get
            {
                this.factoryCore ??= this.CreateContentFactory();
                return this.factoryCore;
            }
        }

        public double ImageSize =>
            WindowsVersion.IsWin10AnniversaryOrNewer ? ((double) 0x30) : ((double) 90);

        private class WinRTToastNotificationContentFactory : IPredefinedToastNotificationContentFactory, IPredefinedToastNotificationContentFactoryGeneric
        {
            public IPredefinedToastNotificationContent CreateContent(string bodyText) => 
                WinRTToastNotificationContent.Create(bodyText);

            public IPredefinedToastNotificationContent CreateOneLineHeaderContent(string headlineText, string bodyText) => 
                WinRTToastNotificationContent.CreateOneLineHeader(headlineText, bodyText);

            public IPredefinedToastNotificationContent CreateOneLineHeaderContent(string headlineText, string bodyText1, string bodyText2) => 
                WinRTToastNotificationContent.CreateOneLineHeader(headlineText, bodyText1, bodyText2);

            public IPredefinedToastNotificationContent CreateToastGeneric(string headlineText, string bodyText1, string bodyText2) => 
                WinRTToastNotificationContent.CreateToastGeneric(headlineText, bodyText1, bodyText2);

            public IPredefinedToastNotificationContent CreateTwoLineHeaderContent(string headlineText, string bodyText) => 
                WinRTToastNotificationContent.CreateTwoLineHeader(headlineText, bodyText);
        }
    }
}

