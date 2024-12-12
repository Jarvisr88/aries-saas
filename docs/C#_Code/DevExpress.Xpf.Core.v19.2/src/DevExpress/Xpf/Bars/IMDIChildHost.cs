namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public interface IMDIChildHost
    {
        event EventHandler IsChildMenuVisibleChanged;

        bool CanResize { get; }

        System.Windows.Size Size { get; set; }

        System.Windows.Size MinSize { get; }

        System.Windows.Size MaxSize { get; }

        bool IsChildMenuVisible { get; }
    }
}

