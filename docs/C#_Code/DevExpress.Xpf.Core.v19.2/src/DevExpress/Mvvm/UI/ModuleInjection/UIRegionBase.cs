namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.ModuleInjection;
    using DevExpress.Mvvm.ModuleInjection.Native;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class UIRegionBase : ServiceBaseGeneric<DependencyObject>, IUIRegion
    {
        public static readonly DependencyProperty InheritedServiceProperty = DependencyProperty.RegisterAttached("InheritedService", typeof(UIRegionBase), typeof(UIRegionBase), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.OverridesInheritanceBehavior | FrameworkPropertyMetadataOptions.Inherits));
        public static readonly DependencyProperty ModuleManagerProperty;
        public static readonly DependencyProperty StrategyManagerProperty;
        public static readonly DependencyProperty RegionNameProperty;
        private static readonly DependencyPropertyKey SelectedViewModelPropertyKey;
        public static readonly DependencyProperty SelectedViewModelProperty;
        public static readonly DependencyProperty SetParentViewModelProperty;
        public static readonly DependencyProperty ParentViewModelProperty;
        private bool focusOnSelectedViewModelChanged = true;
        private readonly ObservableCollection<object> viewModels = new ObservableCollection<object>();

        static UIRegionBase()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(UIRegionBase), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<UIRegionBase> registrator1 = DependencyPropertyRegistrator<UIRegionBase>.New().Register<IModuleManagerImplementation>(System.Linq.Expressions.Expression.Lambda<Func<UIRegionBase, IModuleManagerImplementation>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(UIRegionBase.get_ModuleManager)), parameters), out ModuleManagerProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(UIRegionBase), "x");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<UIRegionBase> registrator2 = registrator1.Register<IStrategyManager>(System.Linq.Expressions.Expression.Lambda<Func<UIRegionBase, IStrategyManager>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(UIRegionBase.get_StrategyManager)), expressionArray2), out StrategyManagerProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(UIRegionBase), "x");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<UIRegionBase> registrator3 = registrator2.Register<string>(System.Linq.Expressions.Expression.Lambda<Func<UIRegionBase, string>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(UIRegionBase.get_RegionName)), expressionArray3), out RegionNameProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(UIRegionBase), "x");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<UIRegionBase> registrator4 = registrator3.RegisterReadOnly<object>(System.Linq.Expressions.Expression.Lambda<Func<UIRegionBase, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(UIRegionBase.get_SelectedViewModel)), expressionArray4), out SelectedViewModelPropertyKey, out SelectedViewModelProperty, null, (x, oldValue, newValue) => x.OnSelectedViewModelPropertyChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(UIRegionBase), "x");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<UIRegionBase> registrator5 = registrator4.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<UIRegionBase, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(UIRegionBase.get_SetParentViewModel)), expressionArray5), out SetParentViewModelProperty, true, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(UIRegionBase), "x");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator5.Register<object>(System.Linq.Expressions.Expression.Lambda<Func<UIRegionBase, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(UIRegionBase.get_ParentViewModel)), expressionArray6), out ParentViewModelProperty, null, (x, oldValue, newValue) => x.OnParentViewModelChanged(oldValue, newValue), frameworkOptions);
        }

        protected virtual void ClearViewModel(object vm)
        {
            ISupportParentViewModel model = vm as ISupportParentViewModel;
            if (this.SetParentViewModel && (model != null))
            {
                model.ParentViewModel = null;
            }
        }

        void IUIRegion.Clear()
        {
            List<object> list = new List<object>(this.viewModels);
            this.DoClear();
            foreach (object obj2 in list)
            {
                this.ClearViewModel(obj2);
            }
            this.viewModels.Clear();
            this.SelectedViewModel = null;
        }

        object IUIRegion.GetView(object viewModel) => 
            this.GetView(viewModel);

        void IUIRegion.Inject(object viewModel, Type viewType)
        {
            if (viewModel != null)
            {
                this.viewModels.Add(viewModel);
                this.InitViewModel(viewModel);
                this.DoInject(viewModel, viewType);
            }
        }

        void IUIRegion.Remove(object viewModel)
        {
            if (this.viewModels.Contains(viewModel))
            {
                EnforceSaveLayout(viewModel);
                this.DoUninject(viewModel);
                this.viewModels.Remove(viewModel);
                this.ClearViewModel(viewModel);
            }
        }

        void IUIRegion.SelectViewModel(object vm, bool focus)
        {
            this.focusOnSelectedViewModelChanged = focus;
            ((IUIRegion) this).SelectedViewModel = vm;
            this.focusOnSelectedViewModelChanged = true;
        }

        protected abstract void DoClear();
        protected abstract void DoInject(object vm, Type vType);
        protected abstract void DoUninject(object vm);
        protected static void EnforceSaveLayout(object viewModel)
        {
            if (viewModel != null)
            {
                Action<IVisualStateServiceImplementation> action = <>c.<>9__11_0;
                if (<>c.<>9__11_0 == null)
                {
                    Action<IVisualStateServiceImplementation> local1 = <>c.<>9__11_0;
                    action = <>c.<>9__11_0 = x => x.EnforceSaveState();
                }
                VisualStateServiceHelper.GetServices(viewModel, false, true).ToList<IVisualStateServiceImplementation>().ForEach(action);
            }
        }

        public static UIRegionBase GetInheritedService(DependencyObject obj) => 
            (UIRegionBase) obj.GetValue(InheritedServiceProperty);

        protected abstract object GetView(object viewModel);
        protected virtual void InitViewModel(object vm)
        {
            ISupportParentViewModel objA = vm as ISupportParentViewModel;
            if (this.SetParentViewModel && (objA != null))
            {
                if (ReferenceEquals(objA, this.ActualParentViewModel))
                {
                    Trace.WriteLine("MIF: UIRegion (" + this.RegionName + ") failed to set ParentViewModel. Bind the UIRegion.ParentViewModel property manually.");
                }
                else
                {
                    objA.ParentViewModel = this.ActualParentViewModel;
                }
            }
        }

        protected virtual void InitViewModels()
        {
            foreach (object obj2 in this.viewModels)
            {
                this.InitViewModel(obj2);
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.Target = new DOTargetWrapper(base.AssociatedObject);
            if (this.Target.IsNull)
            {
                ModuleInjectionException.CannotAttach();
            }
            this.OnInitialized();
            this.Target.DataContextChanged += new DependencyPropertyChangedEventHandler(this.OnTargetDataContextChanged);
        }

        protected override void OnDetaching()
        {
            this.Target.DataContextChanged -= new DependencyPropertyChangedEventHandler(this.OnTargetDataContextChanged);
            this.OnUninitializing();
            this.Target = null;
            base.OnDetaching();
        }

        protected virtual void OnInitialized()
        {
            this.ActualModuleManager.GetRegionImplementation(this.RegionName).RegisterUIRegion(this);
        }

        private void OnParentViewModelChanged(object oldValue, object newValue)
        {
            this.InitViewModels();
        }

        protected abstract void OnSelectedViewModelChanged(object oldValue, object newValue, bool focus);
        private void OnSelectedViewModelPropertyChanged(object oldValue, object newValue)
        {
            this.OnSelectedViewModelChanged(oldValue, newValue, this.focusOnSelectedViewModelChanged);
            string key = this.ActualModuleManager.GetRegion(this.RegionName).GetKey(oldValue);
            NavigationEventArgs e = new NavigationEventArgs(this.RegionName, oldValue, newValue, key, this.ActualModuleManager.GetRegion(this.RegionName).GetKey(newValue));
            this.ActualModuleManager.OnNavigation(this.RegionName, e);
        }

        private void OnTargetDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.InitViewModels();
        }

        protected virtual void OnUninitializing()
        {
            this.ActualModuleManager.GetRegionImplementation(this.RegionName).UnregisterUIRegion(this);
        }

        public static void SetInheritedService(DependencyObject obj, UIRegionBase value)
        {
            obj.SetValue(InheritedServiceProperty, value);
        }

        public IStrategyManager StrategyManager
        {
            get => 
                (IStrategyManager) base.GetValue(StrategyManagerProperty);
            set => 
                base.SetValue(StrategyManagerProperty, value);
        }

        public IModuleManagerImplementation ModuleManager
        {
            get => 
                (IModuleManagerImplementation) base.GetValue(ModuleManagerProperty);
            set => 
                base.SetValue(ModuleManagerProperty, value);
        }

        public string RegionName
        {
            get => 
                (string) base.GetValue(RegionNameProperty);
            set => 
                base.SetValue(RegionNameProperty, value);
        }

        public object SelectedViewModel
        {
            get => 
                base.GetValue(SelectedViewModelProperty);
            protected set => 
                base.SetValue(SelectedViewModelPropertyKey, value);
        }

        public bool SetParentViewModel
        {
            get => 
                (bool) base.GetValue(SetParentViewModelProperty);
            set => 
                base.SetValue(SetParentViewModelProperty, value);
        }

        public object ParentViewModel
        {
            get => 
                base.GetValue(ParentViewModelProperty);
            set => 
                base.SetValue(ParentViewModelProperty, value);
        }

        protected DOTargetWrapper Target { get; private set; }

        protected internal IModuleManagerImplementation ActualModuleManager =>
            this.ModuleManager ?? DevExpress.Mvvm.ModuleInjection.ModuleManager.DefaultImplementation;

        protected internal IStrategyManager ActualStrategyManager =>
            this.StrategyManager ?? DevExpress.Mvvm.UI.ModuleInjection.StrategyManager.Default;

        protected virtual object ActualParentViewModel =>
            this.ParentViewModel ?? this.Target.With<DOTargetWrapper, object>((<>c.<>9__40_0 ??= x => x.DataContext));

        protected override bool AllowAttachInDesignMode =>
            false;

        IEnumerable<object> IUIRegion.ViewModels =>
            this.viewModels;

        object IUIRegion.SelectedViewModel
        {
            get => 
                this.SelectedViewModel;
            set
            {
                if (this.SelectedViewModel == value)
                {
                    this.OnSelectedViewModelChanged(this.SelectedViewModel, this.SelectedViewModel, this.focusOnSelectedViewModelChanged);
                }
                else
                {
                    this.SelectedViewModel = value;
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly UIRegionBase.<>c <>9 = new UIRegionBase.<>c();
            public static Action<IVisualStateServiceImplementation> <>9__11_0;
            public static Func<DOTargetWrapper, object> <>9__40_0;

            internal void <.cctor>b__10_0(UIRegionBase x, object oldValue, object newValue)
            {
                x.OnSelectedViewModelPropertyChanged(oldValue, newValue);
            }

            internal void <.cctor>b__10_1(UIRegionBase x, object oldValue, object newValue)
            {
                x.OnParentViewModelChanged(oldValue, newValue);
            }

            internal void <EnforceSaveLayout>b__11_0(IVisualStateServiceImplementation x)
            {
                x.EnforceSaveState();
            }

            internal object <get_ActualParentViewModel>b__40_0(DOTargetWrapper x) => 
                x.DataContext;
        }

        protected class StrategyOwnerBase : IStrategyOwner
        {
            public StrategyOwnerBase(UIRegionBase owner, DependencyObject target)
            {
                this.Owner = owner;
                this.Target = target;
            }

            public virtual bool CanRemoveViewModel(object viewModel)
            {
                if (viewModel == null)
                {
                    return true;
                }
                UIRegionBase.EnforceSaveLayout(viewModel);
                string key = this.Owner.ActualModuleManager.GetRegion(this.RegionName).GetKey(viewModel);
                ViewModelRemovingEventArgs e = new ViewModelRemovingEventArgs(this.RegionName, viewModel, key);
                this.Owner.ActualModuleManager.OnViewModelRemoving(this.RegionName, e);
                return !e.Cancel;
            }

            public virtual void ConfigureChild(DependencyObject child)
            {
                UIRegionBase.SetInheritedService(child, this.Owner);
            }

            public string GetKey(object viewModel) => 
                this.Owner.ActualModuleManager.GetRegion(this.RegionName).GetKey(viewModel);

            public virtual void RemoveViewModel(object viewModel)
            {
                string key = this.Owner.ActualModuleManager.GetRegion(this.RegionName).GetKey(viewModel);
                this.Owner.viewModels.Remove(viewModel);
                this.Owner.ClearViewModel(viewModel);
                this.Owner.ActualModuleManager.OnViewModelRemoved(this.RegionName, new ViewModelRemovedEventArgs(this.RegionName, viewModel, key));
            }

            public virtual void SelectViewModel(object viewModel)
            {
                this.Owner.SelectedViewModel = viewModel;
            }

            public UIRegionBase Owner { get; private set; }

            public DependencyObject Target { get; private set; }

            public string RegionName =>
                this.Owner.RegionName;
        }
    }
}

