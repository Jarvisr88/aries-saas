namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.ModuleInjection;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public sealed class ViewInjectionService : ServiceBase, IViewInjectionService
    {
        private const string Exception = "A view model with the same key already exists in the {0} region.";
        public static readonly DependencyProperty ViewInjectionManagerProperty = DependencyProperty.Register("ViewInjectionManager", typeof(IViewInjectionManager), typeof(ViewInjectionService), new PropertyMetadata(null));
        public static readonly DependencyProperty StrategyManagerProperty = DependencyProperty.Register("StrategyManager", typeof(IStrategyManager), typeof(ViewInjectionService), new PropertyMetadata(null));
        public static readonly DependencyProperty ViewLocatorProperty = DependencyProperty.Register("ViewLocator", typeof(IViewLocator), typeof(ViewInjectionService), new PropertyMetadata(null));
        public static readonly DependencyProperty RegionNameProperty = DependencyProperty.Register("RegionName", typeof(string), typeof(ViewInjectionService), new PropertyMetadata(null));
        public static readonly DependencyProperty SelectedViewModelProperty;
        public static readonly DependencyProperty SelectedViewModelChangedCommandProperty;
        public static readonly DependencyProperty ViewModelClosingCommandProperty;
        private IStrategy strategy;
        private Dictionary<object, object> viewModels = new Dictionary<object, object>();

        static ViewInjectionService()
        {
            SelectedViewModelProperty = DependencyProperty.Register("SelectedViewModel", typeof(object), typeof(ViewInjectionService), new PropertyMetadata(null, (d, e) => ((ViewInjectionService) d).OnSelectedViewModelChanged(e)));
            SelectedViewModelChangedCommandProperty = DependencyProperty.Register("SelectedViewModelChangedCommand", typeof(ICommand), typeof(ViewInjectionService), new PropertyMetadata(null));
            ViewModelClosingCommandProperty = DependencyProperty.Register("ViewModelClosingCommand", typeof(ICommand), typeof(ViewInjectionService), new PropertyMetadata(null));
        }

        private bool CanRemoveCore(object viewModel)
        {
            if ((viewModel == null) || !this.viewModels.ContainsValue(viewModel))
            {
                return true;
            }
            ViewModelClosingEventArgs e = new ViewModelClosingEventArgs(viewModel);
            this.ViewModelClosingCommand.If<ICommand>(x => x.CanExecute(e)).Do<ICommand>(delegate (ICommand x) {
                x.Execute(e);
            });
            this.ActualViewInjectionManager.RaiseViewModelClosingEvent(e);
            return !e.Cancel;
        }

        object IViewInjectionService.GetKey(object viewModel) => 
            this.viewModels.FirstOrDefault<KeyValuePair<object, object>>(x => (x.Value == viewModel)).Key;

        void IViewInjectionService.Inject(object key, object viewModel, string viewName, Type viewType)
        {
            if ((viewModel != null) && this.Strategy.IsInitialized)
            {
                key ??= viewModel;
                if ((key != null) && this.viewModels.ContainsKey(key))
                {
                    if (!ViewModelBase.IsInDesignMode)
                    {
                        throw new InvalidOperationException($"A view model with the same key already exists in the {string.IsNullOrEmpty(this.RegionName) ? "ViewInjectionService" : this.RegionName} region.");
                    }
                }
                else if (!this.viewModels.ContainsKey(key))
                {
                    Type type1 = viewType;
                    if (viewType == null)
                    {
                        Type local1 = viewType;
                        IViewLocator viewLocator = this.ViewLocator;
                        IViewLocator locator2 = viewLocator;
                        if (viewLocator == null)
                        {
                            IViewLocator local2 = viewLocator;
                            locator2 = DevExpress.Mvvm.UI.ViewLocator.Default;
                        }
                        type1 = locator2.ResolveViewType(viewName);
                    }
                    this.Strategy.Inject(viewModel, type1);
                    this.viewModels.Add(key, viewModel);
                }
            }
        }

        bool IViewInjectionService.Remove(object viewModel)
        {
            if (!this.Strategy.IsInitialized)
            {
                return false;
            }
            if (!this.CanRemoveCore(viewModel))
            {
                return false;
            }
            this.Strategy.Remove(viewModel);
            this.RemoveCore(viewModel);
            return true;
        }

        private void OnAssociatedObjectInitialized(object sender, EventArgs e)
        {
            this.Strategy.Initialize(new StrategyOwner(this));
        }

        private void OnAssociatedObjectLoaded(object sender, EventArgs e)
        {
            if (base.AssociatedObject.IsInitialized)
            {
                this.OnAssociatedObjectInitialized(sender, EventArgs.Empty);
            }
            this.ActualViewInjectionManager.RegisterService(this);
        }

        private void OnAssociatedObjectUnloaded(object sender, EventArgs e)
        {
            this.ActualViewInjectionManager.UnregisterService(this);
            this.Strategy.Uninitialize();
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (base.AssociatedObject.IsLoaded)
            {
                this.OnAssociatedObjectLoaded(base.AssociatedObject, EventArgs.Empty);
            }
            base.AssociatedObject.Loaded += new RoutedEventHandler(this.OnAssociatedObjectLoaded);
            base.AssociatedObject.Unloaded += new RoutedEventHandler(this.OnAssociatedObjectUnloaded);
            if (base.AssociatedObject.IsInitialized)
            {
                this.OnAssociatedObjectInitialized(base.AssociatedObject, EventArgs.Empty);
            }
            base.AssociatedObject.Initialized += new EventHandler(this.OnAssociatedObjectInitialized);
            if (this.SelectedViewModel != null)
            {
                this.Strategy.Select(this.SelectedViewModel, false);
            }
        }

        protected override void OnDetaching()
        {
            base.AssociatedObject.Initialized -= new EventHandler(this.OnAssociatedObjectInitialized);
            base.AssociatedObject.Loaded -= new RoutedEventHandler(this.OnAssociatedObjectLoaded);
            base.AssociatedObject.Unloaded -= new RoutedEventHandler(this.OnAssociatedObjectUnloaded);
            this.OnAssociatedObjectUnloaded(base.AssociatedObject, EventArgs.Empty);
            base.OnDetaching();
        }

        private void OnSelectedViewModelChanged(DependencyPropertyChangedEventArgs e)
        {
            this.SelectedViewModel = e.NewValue;
            this.SelectedViewModelChangedCommand.If<ICommand>(x => x.CanExecute(e)).Do<ICommand>(x => x.Execute(e));
            if (e.OldValue != null)
            {
                this.ActualViewInjectionManager.RaiseNavigatedAwayEvent(e.OldValue);
            }
            if (e.NewValue != null)
            {
                this.ActualViewInjectionManager.RaiseNavigatedEvent(e.NewValue);
            }
            if (base.IsAttached)
            {
                this.Strategy.Select(e.NewValue, true);
            }
        }

        private void RemoveCore(object viewModel)
        {
            object key = ((IViewInjectionService) this).GetKey(viewModel);
            if (key != null)
            {
                this.viewModels.Remove(key);
            }
        }

        public IViewInjectionManager ViewInjectionManager
        {
            get => 
                (IViewInjectionManager) base.GetValue(ViewInjectionManagerProperty);
            set => 
                base.SetValue(ViewInjectionManagerProperty, value);
        }

        public IStrategyManager StrategyManager
        {
            get => 
                (IStrategyManager) base.GetValue(StrategyManagerProperty);
            set => 
                base.SetValue(StrategyManagerProperty, value);
        }

        public IViewLocator ViewLocator
        {
            get => 
                (IViewLocator) base.GetValue(ViewLocatorProperty);
            set => 
                base.SetValue(ViewLocatorProperty, value);
        }

        public string RegionName
        {
            get => 
                (string) base.GetValue(RegionNameProperty);
            set => 
                base.SetValue(RegionNameProperty, value);
        }

        public IEnumerable<object> ViewModels =>
            this.viewModels.Values;

        public object SelectedViewModel
        {
            get => 
                base.GetValue(SelectedViewModelProperty);
            set => 
                base.SetValue(SelectedViewModelProperty, value);
        }

        public ICommand SelectedViewModelChangedCommand
        {
            get => 
                (ICommand) base.GetValue(SelectedViewModelChangedCommandProperty);
            set => 
                base.SetValue(SelectedViewModelChangedCommandProperty, value);
        }

        public ICommand ViewModelClosingCommand
        {
            get => 
                (ICommand) base.GetValue(ViewModelClosingCommandProperty);
            set => 
                base.SetValue(ViewModelClosingCommandProperty, value);
        }

        protected override bool AllowAttachInDesignMode =>
            false;

        private IViewInjectionManager ActualViewInjectionManager =>
            this.ViewInjectionManager ?? DevExpress.Mvvm.ViewInjectionManager.Default;

        private IStrategyManager ActualStrategyManager =>
            this.StrategyManager ?? DevExpress.Mvvm.UI.ModuleInjection.StrategyManager.Default;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IStrategy Strategy
        {
            get
            {
                IStrategy strategy = this.strategy;
                if (this.strategy == null)
                {
                    IStrategy local1 = this.strategy;
                    strategy = this.strategy = this.ActualStrategyManager.CreateStrategy(base.AssociatedObject);
                }
                return strategy;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ViewInjectionService.<>c <>9 = new ViewInjectionService.<>c();

            internal void <.cctor>b__54_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ViewInjectionService) d).OnSelectedViewModelChanged(e);
            }
        }

        private class StrategyOwner : IStrategyOwner
        {
            private readonly ViewInjectionService owner;

            public StrategyOwner(ViewInjectionService owner)
            {
                this.owner = owner;
            }

            public bool CanRemoveViewModel(object viewModel) => 
                this.owner.CanRemoveCore(viewModel);

            public void ConfigureChild(DependencyObject child)
            {
            }

            public string GetKey(object viewModel) => 
                ((IViewInjectionService) this.owner).GetKey(viewModel) as string;

            public void RemoveViewModel(object viewModel)
            {
                this.owner.RemoveCore(viewModel);
            }

            public void SelectViewModel(object viewModel)
            {
                this.owner.SelectedViewModel = viewModel;
            }

            public string RegionName =>
                this.owner.RegionName;

            public DependencyObject Target =>
                this.owner.AssociatedObject;
        }
    }
}

