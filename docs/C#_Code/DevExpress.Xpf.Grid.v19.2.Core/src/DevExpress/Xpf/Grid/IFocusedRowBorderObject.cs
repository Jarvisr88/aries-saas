namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Windows;

    public interface IFocusedRowBorderObject
    {
        FrameworkElement RowDataContent { get; }

        double LeftIndent { get; }
    }
}

