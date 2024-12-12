namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Mvvm.UI;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ToastTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (((PredefinedToastNotificationVewModel) item).ToastTemplate)
            {
                case NotificationTemplate.LongText:
                    return this.SimpleTemplate;

                case NotificationTemplate.ShortHeaderAndLongText:
                    return this.OneLineHeaderTemplate;

                case NotificationTemplate.LongHeaderAndShortText:
                    return this.TwoLineHeaderTemplate;

                case NotificationTemplate.ShortHeaderAndTwoTextFields:
                    return this.OneLineHeaderTwoLinesBodyTemplate;
            }
            return null;
        }

        public DataTemplate SimpleTemplate { get; set; }

        public DataTemplate OneLineHeaderTemplate { get; set; }

        public DataTemplate TwoLineHeaderTemplate { get; set; }

        public DataTemplate OneLineHeaderTwoLinesBodyTemplate { get; set; }
    }
}

