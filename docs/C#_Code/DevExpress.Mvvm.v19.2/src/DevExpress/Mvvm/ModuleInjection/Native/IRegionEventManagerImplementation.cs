namespace DevExpress.Mvvm.ModuleInjection.Native
{
    using DevExpress.Mvvm.ModuleInjection;
    using System;

    public interface IRegionEventManagerImplementation : IRegionEventManager
    {
        void RaiseNavigation(object sender, NavigationEventArgs e);
        void RaiseViewModelCreated(object sender, ViewModelCreatedEventArgs e);
        void RaiseViewModelRemoved(object sender, ViewModelRemovedEventArgs e);
        void RaiseViewModelRemoving(object sender, ViewModelRemovingEventArgs e);
    }
}

