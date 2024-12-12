namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Windows;

    public interface ITabHeaderLayoutResult
    {
        System.Windows.Size Size { get; }

        bool HasScroll { get; }

        Rect[] Headers { get; }

        IScrollResult ScrollResult { get; }

        bool IsEmpty { get; }
    }
}

