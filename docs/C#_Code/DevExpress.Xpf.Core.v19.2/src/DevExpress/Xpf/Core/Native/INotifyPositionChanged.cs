namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public interface INotifyPositionChanged
    {
        void OnPositionChanged(Rect newRect);
    }
}

