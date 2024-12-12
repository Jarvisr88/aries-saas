namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Windows;

    public interface ITabHeaderLayoutOptions
    {
        bool IsAutoFill { get; }

        bool IsHorizontal { get; }

        bool FixedRows { get; }

        int ScrollIndex { get; }

        bool SelectedRowFirst { get; }

        System.Windows.Size Size { get; }

        double Offset { get; }
    }
}

