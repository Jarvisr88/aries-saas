namespace DevExpress.Mvvm.ModuleInjection.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.ModuleInjection;
    using System;

    public class ViewModelEventManager : IViewModelEventManager, IViewModelEventManagerImplementation
    {
        private WeakEvent<EventHandler<NavigationEventArgs>, NavigationEventArgs> navigated = new WeakEvent<EventHandler<NavigationEventArgs>, NavigationEventArgs>();
        private WeakEvent<EventHandler<NavigationEventArgs>, NavigationEventArgs> navigatedAway = new WeakEvent<EventHandler<NavigationEventArgs>, NavigationEventArgs>();
        private WeakEvent<EventHandler<ViewModelRemovingEventArgs>, ViewModelRemovingEventArgs> viewModelRemoving = new WeakEvent<EventHandler<ViewModelRemovingEventArgs>, ViewModelRemovingEventArgs>();
        private WeakEvent<EventHandler<ViewModelRemovedEventArgs>, ViewModelRemovedEventArgs> viewModelRemoved = new WeakEvent<EventHandler<ViewModelRemovedEventArgs>, ViewModelRemovedEventArgs>();
        private readonly WeakReference viewModel;

        event EventHandler<NavigationEventArgs> IViewModelEventManager.Navigated
        {
            add
            {
                this.Add<NavigationEventArgs>(this.navigated, value);
            }
            remove
            {
                this.Remove<NavigationEventArgs>(this.navigated, value);
            }
        }

        event EventHandler<NavigationEventArgs> IViewModelEventManager.NavigatedAway
        {
            add
            {
                this.Add<NavigationEventArgs>(this.navigatedAway, value);
            }
            remove
            {
                this.Remove<NavigationEventArgs>(this.navigatedAway, value);
            }
        }

        event EventHandler<ViewModelRemovedEventArgs> IViewModelEventManager.ViewModelRemoved
        {
            add
            {
                this.Add<ViewModelRemovedEventArgs>(this.viewModelRemoved, value);
            }
            remove
            {
                this.Remove<ViewModelRemovedEventArgs>(this.viewModelRemoved, value);
            }
        }

        event EventHandler<ViewModelRemovingEventArgs> IViewModelEventManager.ViewModelRemoving
        {
            add
            {
                this.Add<ViewModelRemovingEventArgs>(this.viewModelRemoving, value);
            }
            remove
            {
                this.Remove<ViewModelRemovingEventArgs>(this.viewModelRemoving, value);
            }
        }

        public ViewModelEventManager(object viewModel)
        {
            this.viewModel = new WeakReference(viewModel);
        }

        private void Add<TEventArgs>(WeakEvent<EventHandler<TEventArgs>, TEventArgs> weakEvent, EventHandler<TEventArgs> eventHandler) where TEventArgs: EventArgs
        {
            if (this.viewModel.IsAlive)
            {
                weakEvent.Add(eventHandler);
            }
        }

        void IViewModelEventManagerImplementation.RaiseNavigated(object sender, NavigationEventArgs e)
        {
            this.Raise<NavigationEventArgs>(this.navigated, sender, e);
        }

        void IViewModelEventManagerImplementation.RaiseNavigatedAway(object sender, NavigationEventArgs e)
        {
            this.Raise<NavigationEventArgs>(this.navigatedAway, sender, e);
        }

        void IViewModelEventManagerImplementation.RaiseViewModelRemoved(object sender, ViewModelRemovedEventArgs e)
        {
            this.Raise<ViewModelRemovedEventArgs>(this.viewModelRemoved, sender, e);
        }

        void IViewModelEventManagerImplementation.RaiseViewModelRemoving(object sender, ViewModelRemovingEventArgs e)
        {
            this.Raise<ViewModelRemovingEventArgs>(this.viewModelRemoving, sender, e);
        }

        private void Raise<TEventArgs>(WeakEvent<EventHandler<TEventArgs>, TEventArgs> weakEvent, object sender, TEventArgs e) where TEventArgs: EventArgs
        {
            weakEvent.Raise(sender, e);
        }

        private void Remove<TEventArgs>(WeakEvent<EventHandler<TEventArgs>, TEventArgs> weakEvent, EventHandler<TEventArgs> eventHandler) where TEventArgs: EventArgs
        {
            weakEvent.Remove(eventHandler);
        }
    }
}

