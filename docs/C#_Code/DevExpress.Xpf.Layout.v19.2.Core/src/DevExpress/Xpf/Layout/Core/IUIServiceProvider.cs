namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public interface IUIServiceProvider
    {
        ServiceListener GetUIServiceListener<ServiceListener>() where ServiceListener: class, IUIServiceListener;
        ServiceListener GetUIServiceListener<ServiceListener>(object key) where ServiceListener: class, IUIServiceListener;
        void RegisterUIServiceListener(IUIServiceListener listener);
    }
}

