namespace DevExpress.Internal
{
    using System;

    public interface IPredefinedToastNotificationContentFactory
    {
        IPredefinedToastNotificationContent CreateContent(string bodyText);
        IPredefinedToastNotificationContent CreateOneLineHeaderContent(string headlineText, string bodyText);
        IPredefinedToastNotificationContent CreateOneLineHeaderContent(string headlineText, string bodyText1, string bodyText2);
        IPredefinedToastNotificationContent CreateTwoLineHeaderContent(string headlineText, string bodyText);
    }
}

