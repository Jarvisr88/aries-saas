namespace DevExpress.Xpf.DocumentViewer
{
    using System;
    using System.Windows;

    public interface IPage
    {
        Size VisibleSize { get; }

        Size PageSize { get; }

        bool IsLoading { get; }

        int PageIndex { get; }

        Thickness Margin { get; }
    }
}

