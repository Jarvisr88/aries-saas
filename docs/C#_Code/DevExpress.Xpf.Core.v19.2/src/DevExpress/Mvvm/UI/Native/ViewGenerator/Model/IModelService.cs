namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using System;

    public interface IModelService
    {
        void RaiseModelChanged();
        IModelSubscribedEvent SubscribeToModelChanged(EventHandler handler);
        void UnsubscribeFromModelChanged(IModelSubscribedEvent e);

        IModelItem Root { get; }
    }
}

