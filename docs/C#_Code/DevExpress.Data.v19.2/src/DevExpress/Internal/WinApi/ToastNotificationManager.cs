namespace DevExpress.Internal.WinApi
{
    using DevExpress.Internal;
    using DevExpress.Internal.WinApi.Window.Data.Xml.Dom;
    using DevExpress.Internal.WinApi.Windows.UI.Notifications;
    using DevExpress.Utils;
    using System;
    using System.Security;

    [SecuritySafeCritical]
    public static class ToastNotificationManager
    {
        private static Version Win8Version = new Version(6, 2, 0x23f0, 0);
        private static IToastNotificationManager defaultManager;

        internal static IToastNotificationAdapter CreateToastNotificationAdapter(string appID) => 
            AreToastNotificationsSupported ? ((IToastNotificationAdapter) new ToastNotificationAdapter(appID, GetDefaultManager())) : ((IToastNotificationAdapter) new EmptyToastNotificationAdapter());

        internal static IToastNotificationManager GetDefaultManager()
        {
            defaultManager ??= ComFunctions.RoGetActivationFactory<IToastNotificationManager>();
            return defaultManager;
        }

        internal static IXmlDocument GetDocument(IPredefinedToastNotificationInfo info) => 
            GetDocument(GetDefaultManager(), info);

        internal static IXmlDocument GetDocument(IToastNotificationManager manager, IPredefinedToastNotificationInfo info) => 
            WinRTToastNotificationContent.GetDocument(manager, info);

        internal static string GetXml(IPredefinedToastNotificationInfo info) => 
            GetXml((IXmlNodeSerializer) GetDocument(info));

        internal static string GetXml(IXmlNodeSerializer content)
        {
            string str;
            content.GetXml(out str);
            return str;
        }

        internal static void LoadXml(IXmlDocumentIO content, string xml)
        {
            content.LoadXml(xml);
        }

        private static bool IsWin8OrHigher
        {
            get
            {
                OperatingSystem oSVersion = Environment.OSVersion;
                return ((oSVersion.Platform == PlatformID.Win32NT) && (oSVersion.Version >= Win8Version));
            }
        }

        public static bool IsGenericTemplateSupported =>
            WindowsVersionProvider.IsWin10AnniversaryUpdateOrHigher;

        public static bool AreToastNotificationsSupported =>
            IsWin8OrHigher;

        private class EmptyToastNotificationAdapter : IToastNotificationAdapter
        {
            IToastNotification IToastNotificationAdapter.Create(IPredefinedToastNotificationInfo info) => 
                null;

            void IToastNotificationAdapter.Hide(IToastNotification notification)
            {
            }

            void IToastNotificationAdapter.Show(IToastNotification notification)
            {
            }
        }

        private class ToastNotificationAdapter : IToastNotificationAdapter
        {
            private string appId;
            private IToastNotifier notifier;
            private IToastNotificationFactory factory;
            private IToastNotificationManager manager;

            public ToastNotificationAdapter(string appId, IToastNotificationManager manager)
            {
                this.appId = appId;
                this.manager = manager;
            }

            IToastNotification IToastNotificationAdapter.Create(IPredefinedToastNotificationInfo info)
            {
                IToastNotification notification;
                IXmlDocument content = ToastNotificationManager.GetDocument(this.manager, info);
                this.factory ??= ComFunctions.RoGetActivationFactory<IToastNotificationFactory>();
                ComFunctions.CheckHRESULT(this.factory.CreateToastNotification(content, out notification));
                return notification;
            }

            void IToastNotificationAdapter.Hide(IToastNotification notification)
            {
                if (this.notifier == null)
                {
                    ComFunctions.CheckHRESULT(this.manager.CreateToastNotifierWithId(this.appId, out this.notifier));
                }
                if ((this.notifier != null) && (notification != null))
                {
                    this.notifier.Hide(notification);
                }
            }

            void IToastNotificationAdapter.Show(IToastNotification notification)
            {
                if (this.notifier == null)
                {
                    ComFunctions.CheckHRESULT(this.manager.CreateToastNotifierWithId(this.appId, out this.notifier));
                }
                if ((this.notifier != null) && (notification != null))
                {
                    this.notifier.Show(notification);
                }
            }
        }
    }
}

