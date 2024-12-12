namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public interface IControl
    {
        FrameworkElement Control { get; }

        DevExpress.Xpf.Core.Controller Controller { get; }

        bool IsLoaded { get; }
    }
}

