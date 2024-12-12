namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public interface IBackButtonSupport
    {
        event RoutedEventHandler BackRequested;

        bool ShowBackButton { get; set; }
    }
}

