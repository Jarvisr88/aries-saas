namespace DevExpress.Xpf.Layout.Core
{
    using DevExpress.Xpf.Layout.Core.Platform;
    using System;
    using System.Windows.Input;

    public interface IUIService : IDisposable
    {
        bool ProcessKey(IView view, KeyEventType eventype, Key key);
        bool ProcessMouse(IView view, MouseEventType eventType, DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs ea);
    }
}

