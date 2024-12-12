namespace DevExpress.Mvvm.ModuleInjection.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.ModuleInjection;
    using System;

    public class RegionEventManager : IRegionEventManager, IRegionEventManagerImplementation
    {
        private WeakEvent<EventHandler<NavigationEventArgs>, NavigationEventArgs> navigation = new WeakEvent<EventHandler<NavigationEventArgs>, NavigationEventArgs>();
        private WeakEvent<EventHandler<ViewModelCreatedEventArgs>, ViewModelCreatedEventArgs> viewModelCreated = new WeakEvent<EventHandler<ViewModelCreatedEventArgs>, ViewModelCreatedEventArgs>();
        private WeakEvent<EventHandler<ViewModelRemovingEventArgs>, ViewModelRemovingEventArgs> viewModelRemoving = new WeakEvent<EventHandler<ViewModelRemovingEventArgs>, ViewModelRemovingEventArgs>();
        private WeakEvent<EventHandler<ViewModelRemovedEventArgs>, ViewModelRemovedEventArgs> viewModelRemoved = new WeakEvent<EventHandler<ViewModelRemovedEventArgs>, ViewModelRemovedEventArgs>();

        event EventHandler<NavigationEventArgs> IRegionEventManager.Navigation
        {
            add
            {
                this.navigation.Add(value);
            }
            remove
            {
                this.navigation.Remove(value);
            }
        }

        event EventHandler<ViewModelCreatedEventArgs> IRegionEventManager.ViewModelCreated
        {
            add
            {
                this.viewModelCreated.Add(value);
            }
            remove
            {
                this.viewModelCreated.Remove(value);
            }
        }

        event EventHandler<ViewModelRemovedEventArgs> IRegionEventManager.ViewModelRemoved
        {
            add
            {
                this.viewModelRemoved.Add(value);
            }
            remove
            {
                this.viewModelRemoved.Remove(value);
            }
        }

        event EventHandler<ViewModelRemovingEventArgs> IRegionEventManager.ViewModelRemoving
        {
            add
            {
                this.viewModelRemoving.Add(value);
            }
            remove
            {
                this.viewModelRemoving.Remove(value);
            }
        }

        void IRegionEventManagerImplementation.RaiseNavigation(object sender, NavigationEventArgs e)
        {
            this.navigation.Raise(sender, e);
        }

        void IRegionEventManagerImplementation.RaiseViewModelCreated(object sender, ViewModelCreatedEventArgs e)
        {
            this.viewModelCreated.Raise(sender, e);
        }

        void IRegionEventManagerImplementation.RaiseViewModelRemoved(object sender, ViewModelRemovedEventArgs e)
        {
            this.viewModelRemoved.Raise(sender, e);
        }

        void IRegionEventManagerImplementation.RaiseViewModelRemoving(object sender, ViewModelRemovingEventArgs e)
        {
            this.viewModelRemoving.Raise(sender, e);
        }
    }
}

