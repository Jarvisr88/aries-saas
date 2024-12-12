namespace DevExpress.Internal
{
    using System;

    public interface IPredefinedToastNotificationFactory
    {
        IPredefinedToastNotificationContentFactory CreateContentFactory();
        IPredefinedToastNotification CreateToastNotification(IPredefinedToastNotificationContent content);
        IPredefinedToastNotification CreateToastNotification(string bodyText);
        IPredefinedToastNotification CreateToastNotificationOneLineHeaderContent(string headlineText, string bodyText);
        IPredefinedToastNotification CreateToastNotificationOneLineHeaderContent(string headlineText, string bodyText1, string bodyText2);
        IPredefinedToastNotification CreateToastNotificationTwoLineHeader(string headlineText, string bodyText);

        double ImageSize { get; }
    }
}

