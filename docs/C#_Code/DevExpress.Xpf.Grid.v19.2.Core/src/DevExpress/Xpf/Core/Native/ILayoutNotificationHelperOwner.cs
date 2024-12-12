namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public interface ILayoutNotificationHelperOwner
    {
        void InvalidateMeasure();

        DependencyObject NotificationManager { get; }
    }
}

