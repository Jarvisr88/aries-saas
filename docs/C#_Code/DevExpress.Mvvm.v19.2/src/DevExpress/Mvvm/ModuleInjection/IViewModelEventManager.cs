namespace DevExpress.Mvvm.ModuleInjection
{
    using DevExpress.Mvvm;
    using System;
    using System.Runtime.CompilerServices;

    public interface IViewModelEventManager
    {
        [WeakEvent]
        event EventHandler<NavigationEventArgs> Navigated;

        [WeakEvent]
        event EventHandler<NavigationEventArgs> NavigatedAway;

        [WeakEvent]
        event EventHandler<ViewModelRemovedEventArgs> ViewModelRemoved;

        [WeakEvent]
        event EventHandler<ViewModelRemovingEventArgs> ViewModelRemoving;
    }
}

