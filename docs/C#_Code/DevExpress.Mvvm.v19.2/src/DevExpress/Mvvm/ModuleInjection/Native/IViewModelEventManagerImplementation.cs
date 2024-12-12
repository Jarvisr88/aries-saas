namespace DevExpress.Mvvm.ModuleInjection.Native
{
    using DevExpress.Mvvm.ModuleInjection;
    using System;

    public interface IViewModelEventManagerImplementation : IViewModelEventManager
    {
        void RaiseNavigated(object sender, NavigationEventArgs e);
        void RaiseNavigatedAway(object sender, NavigationEventArgs e);
        void RaiseViewModelRemoved(object sender, ViewModelRemovedEventArgs e);
        void RaiseViewModelRemoving(object sender, ViewModelRemovingEventArgs e);
    }
}

