namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Windows;

    public static class NotificationServiceTemplatesHelper
    {
        private static ResourceDictionary resourceDictionary;
        private static DataTemplate defaultCustomToastTemplate;
        private static DataTemplate predefinedToastTemplate;

        private static ResourceDictionary GetResourceDictionary()
        {
            if (resourceDictionary == null)
            {
                resourceDictionary = new ResourceDictionary();
                string uriString = $"pack://application:,,,/{"DevExpress.Xpf.Core.v19.2"};component/Mvvm.UI/Services/NotificationService/PredefinedToastNotification.xaml";
                resourceDictionary.Source = new Uri(uriString, UriKind.Absolute);
            }
            return resourceDictionary;
        }

        public static DataTemplate DefaultCustomToastTemplate
        {
            get
            {
                defaultCustomToastTemplate ??= ((DataTemplate) GetResourceDictionary()["DefaultCustomToastTemplate"]);
                return defaultCustomToastTemplate;
            }
        }

        public static DataTemplate PredefinedToastTemplate
        {
            get
            {
                predefinedToastTemplate ??= ((DataTemplate) GetResourceDictionary()["PredefinedToastTemplate"]);
                return predefinedToastTemplate;
            }
        }
    }
}

