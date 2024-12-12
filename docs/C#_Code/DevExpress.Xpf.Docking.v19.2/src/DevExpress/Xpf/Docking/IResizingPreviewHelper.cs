namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    internal interface IResizingPreviewHelper
    {
        void EndResizing();
        void InitResizing(Point point, ILayoutElement element);
        void Resize(Point point);
    }
}

