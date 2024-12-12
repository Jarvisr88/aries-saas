namespace DevExpress.Internal
{
    using System;

    public interface IPredefinedToastNotificationContentFactoryGeneric
    {
        IPredefinedToastNotificationContent CreateToastGeneric(string headlineText, string bodyText1, string bodyText2);
    }
}

