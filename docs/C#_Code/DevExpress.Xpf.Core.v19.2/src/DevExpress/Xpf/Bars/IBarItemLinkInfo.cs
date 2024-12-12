namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;

    public interface IBarItemLinkInfo : IAltitudeSupport
    {
        event ValueChangedEventHandler<bool> ActualIsVisibleChanged;

        void Clear(bool allowRecycling);
        void Destroy(bool force);

        bool ActualIsVisible { get; }
    }
}

