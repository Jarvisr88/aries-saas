namespace DevExpress.Xpf.Office.Internal
{
    using System;
    using System.Windows;

    public interface IToolTipControlClient
    {
        ToolTipControlInfo GetObjectInfo(Point point);

        bool HasToolTip { get; }
    }
}

