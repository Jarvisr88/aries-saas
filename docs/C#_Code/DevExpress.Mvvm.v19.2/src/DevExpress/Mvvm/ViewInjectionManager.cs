namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ViewInjectionManager : IViewInjectionManager
    {
        private const string Exception1 = "Cannot register services with the same RegionName";
        private const string Exception2 = "Cannot inject item with this key, because it already exists in this region.";
        private static IViewInjectionManager _defaultInstance = new ViewInjectionManager(ViewInjectionMode.Default);
        private static IViewInjectionManager _default;
        private static IViewInjectionManager _persistentManager = new ViewInjectionManager(ViewInjectionMode.Persistent);
        private ServiceManagerHelper ServiceManager;
        private QueueManagerHelper QueueManager;
        private MessengerEx Messenger;

        public ViewInjectionManager(ViewInjectionMode mode)
        {
            this.Mode = mode;
            this.ServiceManager = new ServiceManagerHelper();
            this.QueueManager = new QueueManagerHelper(this);
            this.Messenger = new MessengerEx();
        }

        public IViewInjectionService GetService(string regionName) => 
            this.ServiceManager.FindService(x => x.RegionName == regionName);

        public virtual void Inject(string regionName, object key, Func<object> viewModelFactory, string viewName, Type viewType)
        {
            bool flag = this.InjectCore(regionName, key, viewModelFactory, viewName, viewType);
            if (this.Mode == ViewInjectionMode.Persistent)
            {
                this.QueueManager.PutToPersistentInjectionQueue(regionName, key, viewModelFactory, viewName, viewType);
            }
            else if (!flag)
            {
                this.QueueManager.PutToInjectionQueue(regionName, key, viewModelFactory, viewName, viewType);
            }
            this.QueueManager.UpdateQueues();
        }

        protected bool InjectCore(string regionName, object key, Func<object> factory, string viewName, Type viewType)
        {
            IViewInjectionService service = this.GetService(regionName);
            if (service == null)
            {
                return false;
            }
            if ((this.Mode != ViewInjectionMode.Persistent) || (service.GetViewModel(key) == null))
            {
                service.Inject(key, factory(), viewName, viewType);
            }
            return true;
        }

        public virtual void Navigate(string regionName, object key)
        {
            if (!this.NavigateCore(regionName, key))
            {
                this.QueueManager.PutToNavigationQueue(regionName, key);
            }
        }

        protected bool NavigateCore(string regionName, object key)
        {
            IViewInjectionService service = this.GetService(regionName);
            if (service == null)
            {
                return false;
            }
            object viewModel = service.GetViewModel(key);
            if (viewModel == null)
            {
                return false;
            }
            service.SelectedViewModel = viewModel;
            return true;
        }

        public void RaiseNavigatedAwayEvent(object viewModel)
        {
            this.Messenger.Send<NavigatedAwayMessage>(new NavigatedAwayMessage(), viewModel.GetHashCode());
        }

        public void RaiseNavigatedEvent(object viewModel)
        {
            this.Messenger.Send<NavigatedMessage>(new NavigatedMessage(), viewModel.GetHashCode());
        }

        public void RaiseViewModelClosingEvent(ViewModelClosingEventArgs e)
        {
            this.Messenger.Send<ViewModelClosingEventArgs>(e, e.ViewModel.GetHashCode());
        }

        public void RegisterNavigatedAwayEventHandler(object viewModel, Action eventHandler)
        {
            this.Messenger.Register<NavigatedAwayMessage>(viewModel, viewModel.GetHashCode(), false, eventHandler);
        }

        public void RegisterNavigatedEventHandler(object viewModel, Action eventHandler)
        {
            this.Messenger.Register<NavigatedMessage>(viewModel, viewModel.GetHashCode(), false, eventHandler);
        }

        public virtual void RegisterService(IViewInjectionService service)
        {
            this.ServiceManager.Add(service);
            this.QueueManager.UpdateQueues();
        }

        public void RegisterViewModelClosingEventHandler(object viewModel, Action<ViewModelClosingEventArgs> eventHandler)
        {
            this.Messenger.Register<ViewModelClosingEventArgs>(viewModel, viewModel.GetHashCode(), false, eventHandler);
        }

        public virtual void Remove(string regionName, object key)
        {
            this.QueueManager.RemoveFromQueues(regionName, key);
            IViewInjectionService service = this.GetService(regionName);
            if (service != null)
            {
                service.Remove(service.GetViewModel(key));
            }
        }

        public void UnregisterNavigatedAwayEventHandler(object viewModel, Action eventHandler = null)
        {
            this.Messenger.Unregister<NavigatedAwayMessage>(viewModel, viewModel.GetHashCode(), eventHandler);
        }

        public void UnregisterNavigatedEventHandler(object viewModel, Action eventHandler = null)
        {
            this.Messenger.Unregister<NavigatedMessage>(viewModel, viewModel.GetHashCode(), eventHandler);
        }

        public virtual void UnregisterService(IViewInjectionService service)
        {
            this.ServiceManager.Remove(service);
            if (this.Mode == ViewInjectionMode.Persistent)
            {
                foreach (object obj2 in service.ViewModels.ToList<object>())
                {
                    service.Remove(obj2);
                }
            }
        }

        public void UnregisterViewModelClosingEventHandler(object viewModel, Action<ViewModelClosingEventArgs> eventHandler = null)
        {
            this.Messenger.Unregister<ViewModelClosingEventArgs>(viewModel, viewModel.GetHashCode(), eventHandler);
        }

        public static IViewInjectionManager Default
        {
            get => 
                _default ?? _defaultInstance;
            set => 
                _default = value;
        }

        public static IViewInjectionManager PersistentManager =>
            _persistentManager;

        protected ViewInjectionMode Mode { get; private set; }

        private class MessengerEx : Messenger
        {
            public MessengerEx() : base(false, ActionReferenceType.WeakReference)
            {
            }

            private IActionInvoker CreateActionInvoker<TMessage>(object recipient, Action action) => 
                !action.Method.IsStatic ? ((IActionInvoker) new WeakReferenceActionInvoker(recipient, action)) : ((IActionInvoker) new StrongReferenceActionInvoker(recipient, action));

            public void Register<TMessage>(object recipient, object token, bool receiveInheritedMessages, Action action)
            {
                IActionInvoker actionInvoker = this.CreateActionInvoker<TMessage>(recipient, action);
                base.RegisterCore(token, receiveInheritedMessages, typeof(TMessage), actionInvoker);
                base.RequestCleanup();
            }

            public void Unregister<TMessage>(object recipient, object token, Action action)
            {
                base.UnregisterCore(recipient, token, action, typeof(TMessage));
            }
        }

        private class NavigatedAwayMessage
        {
        }

        private class NavigatedMessage
        {
        }

        private class QueueManagerHelper
        {
            private ViewInjectionManager Owner;
            private readonly List<InjectionItem> InjectionQueue = new List<InjectionItem>();
            private readonly List<InjectionItem> PersistentInjectionQueue = new List<InjectionItem>();
            private readonly List<NavigationItem> NavigationQueue = new List<NavigationItem>();

            public QueueManagerHelper(ViewInjectionManager owner)
            {
                this.Owner = owner;
            }

            private void ProcessQueue<T>(IList<T> queue, Func<T, bool> processAction)
            {
                List<T> list = new List<T>();
                foreach (T local in queue.ToList<T>())
                {
                    if (processAction(local))
                    {
                        list.Add(local);
                    }
                }
                foreach (T local2 in list)
                {
                    queue.Remove(local2);
                }
            }

            public void PutToInjectionQueue(string regionName, object key, Func<object> factory, string viewName, Type viewType)
            {
                this.InjectionQueue.Add(new InjectionItem(regionName, key, factory, viewName, viewType));
            }

            public void PutToNavigationQueue(string regionName, object key)
            {
                this.NavigationQueue.Add(new NavigationItem(regionName, key));
            }

            public void PutToPersistentInjectionQueue(string regionName, object key, Func<object> factory, string viewName, Type viewType)
            {
                foreach (InjectionItem item in this.PersistentInjectionQueue)
                {
                    if ((item.RegionName == regionName) && Equals(item.Key, key))
                    {
                        throw new InvalidOperationException("Cannot inject item with this key, because it already exists in this region.");
                    }
                }
                this.PersistentInjectionQueue.Add(new InjectionItem(regionName, key, factory, viewName, viewType));
            }

            public void RemoveFromQueues(string regionName, object key)
            {
                this.InjectionQueue.FirstOrDefault<InjectionItem>(x => ((x.RegionName == regionName) && Equals(x.Key, key))).Do<InjectionItem>(x => this.InjectionQueue.Remove(x));
                this.PersistentInjectionQueue.FirstOrDefault<InjectionItem>(x => ((x.RegionName == regionName) && Equals(x.Key, key))).Do<InjectionItem>(x => this.PersistentInjectionQueue.Remove(x));
                this.NavigationQueue.FirstOrDefault<NavigationItem>(x => ((x.RegionName == regionName) && Equals(x.Key, key))).Do<NavigationItem>(x => this.NavigationQueue.Remove(x));
            }

            public void UpdateQueues()
            {
                this.ProcessQueue<InjectionItem>(this.InjectionQueue, x => this.Owner.InjectCore(x.RegionName, x.Key, x.Factory, x.ViewName, x.ViewType));
                this.ProcessQueue<InjectionItem>(this.PersistentInjectionQueue, delegate (InjectionItem x) {
                    this.Owner.InjectCore(x.RegionName, x.Key, x.Factory, x.ViewName, x.ViewType);
                    return false;
                });
                this.ProcessQueue<NavigationItem>(this.NavigationQueue, x => this.Owner.NavigateCore(x.RegionName, x.Key));
            }

            private class InjectionItem
            {
                public InjectionItem(string regionName, object key, Func<object> factory, string viewName, Type viewType)
                {
                    this.RegionName = regionName;
                    this.Key = key;
                    this.Factory = factory;
                    this.ViewName = viewName;
                    this.ViewType = viewType;
                }

                public string RegionName { get; private set; }

                public object Key { get; private set; }

                public Func<object> Factory { get; private set; }

                public string ViewName { get; private set; }

                public Type ViewType { get; private set; }
            }

            private class NavigationItem
            {
                public readonly string RegionName;
                public readonly object Key;

                public NavigationItem(string regionName, object key)
                {
                    this.RegionName = regionName;
                    this.Key = key;
                }
            }
        }

        private class ServiceManagerHelper
        {
            private readonly List<WeakReference> ServiceReferences = new List<WeakReference>();

            public void Add(IViewInjectionService service)
            {
                if (((service != null) && !string.IsNullOrEmpty(service.RegionName)) && (this.GetServiceReference(service) == null))
                {
                    if (this.FindService(x => x.RegionName == service.RegionName) != null)
                    {
                        throw new InvalidOperationException("Cannot register services with the same RegionName");
                    }
                    this.ServiceReferences.Add(new WeakReference(service));
                }
            }

            public IEnumerable<IViewInjectionService> FindAllServices(Func<IViewInjectionService, bool> predicate)
            {
                List<IViewInjectionService> list = new List<IViewInjectionService>();
                this.UpdateServiceReferences();
                foreach (WeakReference reference in this.ServiceReferences.FindAll(x => predicate((IViewInjectionService) x.Target)))
                {
                    list.Add((IViewInjectionService) reference.Target);
                }
                return list;
            }

            public IViewInjectionService FindService(Func<IViewInjectionService, bool> predicate)
            {
                this.UpdateServiceReferences();
                WeakReference reference = this.ServiceReferences.FirstOrDefault<WeakReference>(x => predicate((IViewInjectionService) x.Target));
                return ((reference != null) ? ((IViewInjectionService) reference.Target) : null);
            }

            private WeakReference GetServiceReference(IViewInjectionService service)
            {
                this.UpdateServiceReferences();
                return this.ServiceReferences.FirstOrDefault<WeakReference>(x => (x.Target == service));
            }

            public void Remove(IViewInjectionService service)
            {
                if (service != null)
                {
                    WeakReference serviceReference = this.GetServiceReference(service);
                    if (serviceReference != null)
                    {
                        this.ServiceReferences.Remove(serviceReference);
                    }
                }
            }

            private void UpdateServiceReferences()
            {
                List<WeakReference> list = new List<WeakReference>();
                foreach (WeakReference reference in this.ServiceReferences)
                {
                    if (!reference.IsAlive)
                    {
                        list.Add(reference);
                    }
                }
                foreach (WeakReference reference2 in list)
                {
                    this.ServiceReferences.Remove(reference2);
                }
            }
        }

        internal class ViewInjectionServiceException : Exception
        {
            private const string _viewInjectionManager_PreInjectRequiresKey = "The PreInject procedure requires key to be not null and serializable.";
            private const string _viewInjectionManager_InjectRequiresPreInject = "This Injection procedure requires the PreInjection method to be called before.";
            private const string _viewInjectionManager_KeyShouldBeSerializable = "A key should be serializable.";
            private const string _viewInjectionManager_CannotInjectNullViewModel = "Cannot inject a view model, because it is null.";
            private const string _viewInjectionManager_CannotResolveViewModel = "ViewModelLocator cannot resolve a view model (the view model name is {0}).";
            private const string _viewInjectionService_ViewModelAlreadyExists = "A view model with the same key already exists.";
            private const string _strategyManager_NoStrategy = "Cannot find an appropriate strategy for the {0} container type.";
            private const string _invalidSelectedViewModel = "Cannot set the SelectedViewModel property to a value that does not exist in the ViewModels collection. Inject the view model before selecting it.";
            private const string _contentControl_ContentAlreadySet = "It is impossible to use ViewInjectionService for the control that has the Content property set.";
            private const string _itemsControl_ItemsSourceAlreadySet = "It is impossible to use ViewInjectionService for the control that has the ItemsSource property set.";
            private const string _viewTypeIsNotSupported = "This region does not support passing viewName/viewType into the injection procedure. Customize view at the target control level.";

            public ViewInjectionServiceException(string regionName, string message) : base(message)
            {
                this.RegionName = regionName;
            }

            public static ViewInjectionManager.ViewInjectionServiceException ContentControl_ContentAlreadySet(string regionName) => 
                new ViewInjectionManager.ViewInjectionServiceException(regionName, "It is impossible to use ViewInjectionService for the control that has the Content property set.");

            public static ViewInjectionManager.ViewInjectionServiceException InvalidSelectedViewModel(string regionNamee) => 
                new ViewInjectionManager.ViewInjectionServiceException(regionNamee, "Cannot set the SelectedViewModel property to a value that does not exist in the ViewModels collection. Inject the view model before selecting it.");

            public static ViewInjectionManager.ViewInjectionServiceException ItemsControl_ItemsSourceAlreadySet(string regionName) => 
                new ViewInjectionManager.ViewInjectionServiceException(regionName, "It is impossible to use ViewInjectionService for the control that has the ItemsSource property set.");

            public static ViewInjectionManager.ViewInjectionServiceException StrategyManager_NoStrategy(object target) => 
                new ViewInjectionManager.ViewInjectionServiceException(null, $"Cannot find an appropriate strategy for the {target.GetType().Name} container type.");

            public static ViewInjectionManager.ViewInjectionServiceException ViewInjectionManager_CannotInjectNullViewModel() => 
                new ViewInjectionManager.ViewInjectionServiceException(null, "Cannot inject a view model, because it is null.");

            public static ViewInjectionManager.ViewInjectionServiceException ViewInjectionManager_CannotResolveViewModel(string viewModelName) => 
                new ViewInjectionManager.ViewInjectionServiceException(null, $"ViewModelLocator cannot resolve a view model (the view model name is {viewModelName}).");

            public static ViewInjectionManager.ViewInjectionServiceException ViewInjectionManager_InjectRequiresPreInject() => 
                new ViewInjectionManager.ViewInjectionServiceException(null, "This Injection procedure requires the PreInjection method to be called before.");

            public static ViewInjectionManager.ViewInjectionServiceException ViewInjectionManager_KeyShouldBeSerializable() => 
                new ViewInjectionManager.ViewInjectionServiceException(null, "A view model with the same key already exists.");

            public static ViewInjectionManager.ViewInjectionServiceException ViewInjectionManager_PreInjectRequiresKey() => 
                new ViewInjectionManager.ViewInjectionServiceException(null, "The PreInject procedure requires key to be not null and serializable.");

            public static ViewInjectionManager.ViewInjectionServiceException ViewInjectionService_ViewModelAlreadyExists(string regionName) => 
                new ViewInjectionManager.ViewInjectionServiceException(regionName, "A view model with the same key already exists.");

            public static ViewInjectionManager.ViewInjectionServiceException ViewTypeIsNotSupported(string regionName) => 
                new ViewInjectionManager.ViewInjectionServiceException(regionName, "This region does not support passing viewName/viewType into the injection procedure. Customize view at the target control level.");

            public string RegionName { get; private set; }
        }
    }
}

