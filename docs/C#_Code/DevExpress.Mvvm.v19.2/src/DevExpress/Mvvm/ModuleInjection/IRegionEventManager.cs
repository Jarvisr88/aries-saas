namespace DevExpress.Mvvm.ModuleInjection
{
    using DevExpress.Mvvm;
    using System;
    using System.Runtime.CompilerServices;

    public interface IRegionEventManager
    {
        [WeakEvent]
        event EventHandler<NavigationEventArgs> Navigation;

        [WeakEvent]
        event EventHandler<ViewModelCreatedEventArgs> ViewModelCreated;

        [WeakEvent]
        event EventHandler<ViewModelRemovedEventArgs> ViewModelRemoved;

        [WeakEvent]
        event EventHandler<ViewModelRemovingEventArgs> ViewModelRemoving;
    }
}

