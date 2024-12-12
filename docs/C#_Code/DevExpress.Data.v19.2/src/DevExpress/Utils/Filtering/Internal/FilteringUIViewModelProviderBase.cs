namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public abstract class FilteringUIViewModelProviderBase : FilteringViewModelPropertyValuesProvider, INotifyPropertyChanged
    {
        private Lazy<Type> viewModelTypeCore;
        private Lazy<IEndUserFilteringViewModel> viewModelCore;
        private Lazy<CriteriaOperator> filterCriteriaCore;
        private readonly Action<string> filterCriteriaChangedAction;

        public event PropertyChangedEventHandler PropertyChanged;

        protected FilteringUIViewModelProviderBase(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.viewModelTypeCore = new Lazy<Type>(new Func<Type>(this.CreateAndInitializeViewModelType));
            this.viewModelCore = new Lazy<IEndUserFilteringViewModel>(new Func<IEndUserFilteringViewModel>(this.CreateAndInitializeViewModel));
            this.filterCriteriaChangedAction = new Action<string>(this.OnFilterCriteriaChanged);
            this.filterCriteriaCore = new Lazy<CriteriaOperator>(new Func<CriteriaOperator>(this.CreateFilterCriteria));
        }

        public void ClearFilterCriteria()
        {
            if (this.IsViewModelCreated)
            {
                Func<IEndUserFilteringMetricViewModel, bool> predicate = <>c.<>9__35_0;
                if (<>c.<>9__35_0 == null)
                {
                    Func<IEndUserFilteringMetricViewModel, bool> local1 = <>c.<>9__35_0;
                    predicate = <>c.<>9__35_0 = mvm => mvm.HasValue;
                }
                Func<IEndUserFilteringMetricViewModel, IValueViewModel> selector = <>c.<>9__35_1;
                if (<>c.<>9__35_1 == null)
                {
                    Func<IEndUserFilteringMetricViewModel, IValueViewModel> local2 = <>c.<>9__35_1;
                    selector = <>c.<>9__35_1 = mvm => mvm.Value;
                }
                foreach (IValueViewModel model in this.Where<IEndUserFilteringMetricViewModel>(predicate).Select<IEndUserFilteringMetricViewModel, IValueViewModel>(selector))
                {
                    model.Reset();
                }
            }
        }

        protected IEndUserFilteringViewModel CreateAndInitializeViewModel()
        {
            IEndUserFilteringViewModel @this = this.CreateViewModel();
            @this.Do<IEndUserFilteringViewModel>(delegate (IEndUserFilteringViewModel vm) {
                vm.SetParentViewModel(this.GetParentViewModel());
                foreach (IEndUserFilteringMetricViewModel model in (IEnumerable<IEndUserFilteringMetricViewModel>) this)
                {
                    model.SetParentViewModel(vm);
                }
                vm.Initialize(this);
                this.SubscribeViewModel(vm);
            });
            return @this;
        }

        protected Type CreateAndInitializeViewModelType()
        {
            Type @this = this.CreateViewModelType();
            @this.Do<Type>(delegate (Type vmType) {
                this.RaisePropertyChanged<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(FilteringUIViewModelProviderBase)), (MethodInfo) methodof(FilteringUIViewModelProviderBase.get_IsViewModelTypeCreated)), new ParameterExpression[0]));
            });
            return @this;
        }

        protected CriteriaOperator CreateFilterCriteria()
        {
            if (!this.IsViewModelCreated)
            {
                return null;
            }
            Func<IEndUserFilteringMetricViewModel, bool> predicate = <>c.<>9__34_0;
            if (<>c.<>9__34_0 == null)
            {
                Func<IEndUserFilteringMetricViewModel, bool> local1 = <>c.<>9__34_0;
                predicate = <>c.<>9__34_0 = mvm => mvm.HasValue;
            }
            Func<IEndUserFilteringMetricViewModel, CriteriaOperator> selector = <>c.<>9__34_1;
            if (<>c.<>9__34_1 == null)
            {
                Func<IEndUserFilteringMetricViewModel, CriteriaOperator> local2 = <>c.<>9__34_1;
                selector = <>c.<>9__34_1 = mvm => mvm.FilterCriteria;
            }
            return CriteriaOperator.And(this.Where<IEndUserFilteringMetricViewModel>(predicate).Select<IEndUserFilteringMetricViewModel, CriteriaOperator>(selector));
        }

        protected virtual IEndUserFilteringViewModel CreateViewModel()
        {
            IViewModelFactory service = base.GetService<IViewModelFactory>();
            Func<IViewModelBuilderResolver, IViewModelBuilder> get = <>c.<>9__25_0;
            if (<>c.<>9__25_0 == null)
            {
                Func<IViewModelBuilderResolver, IViewModelBuilder> local1 = <>c.<>9__25_0;
                get = <>c.<>9__25_0 = resolver => resolver.CreateViewModelBuilder();
            }
            IViewModelBuilder builder = base.GetService<IViewModelBuilderResolver>().Get<IViewModelBuilderResolver, IViewModelBuilder>(get, null);
            return service.Get<IViewModelFactory, IEndUserFilteringViewModel>(factory => ((IEndUserFilteringViewModel) factory.Create(this.ViewModelType, builder)), null);
        }

        protected virtual Type CreateViewModelType() => 
            base.GetService<IEndUserFilteringViewModelTypeBuilder>().Get<IEndUserFilteringViewModelTypeBuilder, Type>(builder => builder.Create(this.GetViewModelBaseType(), base.Properties, base.PropertyValues), null);

        public CriteriaOperator GetFilterCriteria(string path)
        {
            if (!this.IsViewModelCreated)
            {
                return null;
            }
            Func<IEndUserFilteringMetricViewModel, CriteriaOperator> get = <>c.<>9__36_0;
            if (<>c.<>9__36_0 == null)
            {
                Func<IEndUserFilteringMetricViewModel, CriteriaOperator> local1 = <>c.<>9__36_0;
                get = <>c.<>9__36_0 = x => x.HasValue ? x.FilterCriteria : null;
            }
            return base[path].Get<IEndUserFilteringMetricViewModel, CriteriaOperator>(get, null);
        }

        protected virtual object GetParentViewModel() => 
            null;

        protected virtual Type GetViewModelBaseType() => 
            null;

        protected virtual void OnFilterCriteriaChanged(string path)
        {
            this.ResetFilterCriteria();
            this.RaisePropertyChanged<CriteriaOperator>(Expression.Lambda<Func<CriteriaOperator>>(Expression.Property(Expression.Constant(this, typeof(FilteringUIViewModelProviderBase)), (MethodInfo) methodof(FilteringUIViewModelProviderBase.get_FilterCriteria)), new ParameterExpression[0]));
            if (this.IsViewModelCreated)
            {
                base.UpdatePropertyValuesDataBinding(path);
            }
        }

        protected override void OnViewModelCreated(IEndUserFilteringViewModel viewModel)
        {
            base.OnBindablePropertiesInitialized();
            this.RaisePropertyChanged<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(FilteringUIViewModelProviderBase)), (MethodInfo) methodof(FilteringUIViewModelProviderBase.get_IsViewModelCreated)), new ParameterExpression[0]));
            base.OnViewModelCreated(viewModel);
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected override void ResetCore()
        {
            base.ResetCore();
            this.ResetFilterCriteria();
            this.ResetViewModelType();
        }

        protected void ResetFilterCriteria()
        {
            this.ResetFilterCriteriaCore();
        }

        protected void ResetFilterCriteriaCore()
        {
            if (this.filterCriteriaCore.IsValueCreated)
            {
                this.filterCriteriaCore = new Lazy<CriteriaOperator>(new Func<CriteriaOperator>(this.CreateFilterCriteria));
            }
        }

        protected void ResetViewModelCore()
        {
            if (this.viewModelCore.IsValueCreated)
            {
                this.UnsubscribeViewModel(this.viewModelCore.Value);
                base.RaiseViewModelChanged();
                this.viewModelCore = new Lazy<IEndUserFilteringViewModel>(new Func<IEndUserFilteringViewModel>(this.CreateAndInitializeViewModel));
            }
        }

        protected void ResetViewModelType()
        {
            this.ResetViewModelTypeCore();
            this.ResetViewModelCore();
        }

        protected void ResetViewModelTypeCore()
        {
            if (this.viewModelTypeCore.IsValueCreated)
            {
                this.viewModelTypeCore = new Lazy<Type>(new Func<Type>(this.CreateAndInitializeViewModelType));
            }
        }

        protected Type RetrieveType(Type sourceType, IEnumerable<IEndUserFilteringMetricAttributes> attributes = null, Type viewModelBaseType = null) => 
            base.GetService<IEndUserFilteringViewModelTypeBuilder>().Get<IEndUserFilteringViewModelTypeBuilder, Type>(delegate (IEndUserFilteringViewModelTypeBuilder builder) {
                IEndUserFilteringSettings settings = this.CreateSettings(sourceType, attributes);
                IEndUserFilteringViewModelProperties properties = this.CreateProperties(settings);
                IStorage<IEndUserFilteringMetricViewModel> storage = this.CreateStorage(this.GetChildren(settings));
                return builder.Create(viewModelBaseType, properties, this.CreatePropertyValues(storage));
            }, null);

        private void SubscribeViewModel(object viewModel)
        {
            (viewModel as INotifyPropertyChanged).Do<INotifyPropertyChanged>(delegate (INotifyPropertyChanged npc) {
                npc.PropertyChanged += new PropertyChangedEventHandler(this.ViewModel_PropertyChanged);
            });
        }

        private void UnsubscribeViewModel(object viewModel)
        {
            (viewModel as INotifyPropertyChanged).Do<INotifyPropertyChanged>(delegate (INotifyPropertyChanged npc) {
                npc.PropertyChanged -= new PropertyChangedEventHandler(this.ViewModel_PropertyChanged);
            });
        }

        protected void UpdateMemberBindings()
        {
            if (this.IsViewModelCreated)
            {
                this.ViewModel.Do<object>(delegate (object vm) {
                    MemberReader.ResetAccessors(vm);
                    vm.SetParentViewModel(this.GetParentViewModel());
                    base.UpdateMemberBindings(vm, null);
                    this.ResetFilterCriteria();
                    this.RaisePropertyChanged<CriteriaOperator>(Expression.Lambda<Func<CriteriaOperator>>(Expression.Property(Expression.Constant(this, typeof(FilteringUIViewModelProviderBase)), (MethodInfo) methodof(FilteringUIViewModelProviderBase.get_FilterCriteria)), new ParameterExpression[0]));
                });
            }
        }

        protected void UpdateMemberBindings(string path)
        {
            if (this.IsViewModelCreated)
            {
                this.ViewModel.Do<object>(delegate (object vm) {
                    IEndUserFilteringMetricViewModel viewModel = !string.IsNullOrEmpty(path) ? this[path] : null;
                    if (viewModel != null)
                    {
                        this.UpdateMemberBindings(viewModel, null);
                    }
                    else
                    {
                        this.UpdateMemberBindings(vm, null);
                    }
                    this.ResetFilterCriteria();
                    this.RaisePropertyChanged<CriteriaOperator>(Expression.Lambda<Func<CriteriaOperator>>(Expression.Property(Expression.Constant(this, typeof(FilteringUIViewModelProviderBase)), (MethodInfo) methodof(FilteringUIViewModelProviderBase.get_FilterCriteria)), new ParameterExpression[0]));
                });
            }
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string str;
            if (!EndUserFilteringMetricViewModel.IsFilterCriteriaNotify(e, out str))
            {
                base.UpdateMemberBindings(sender, e.PropertyName);
            }
            else if (this.IsViewModelCreated)
            {
                base.QueueFilterCriteriaChange(str, this.filterCriteriaChangedAction);
            }
        }

        public Type ViewModelType =>
            this.viewModelTypeCore.Value;

        public bool IsViewModelTypeCreated =>
            this.viewModelTypeCore.IsValueCreated;

        public object ViewModel =>
            base.GetViewModelCore(this.viewModelCore);

        public bool IsViewModelCreated =>
            this.viewModelCore.IsValueCreated;

        protected sealed override Lazy<IEndUserFilteringViewModel> ViewModelCore =>
            this.viewModelCore;

        public CriteriaOperator FilterCriteria =>
            this.filterCriteriaCore.Value;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilteringUIViewModelProviderBase.<>c <>9 = new FilteringUIViewModelProviderBase.<>c();
            public static Func<IViewModelBuilderResolver, IViewModelBuilder> <>9__25_0;
            public static Func<IEndUserFilteringMetricViewModel, bool> <>9__34_0;
            public static Func<IEndUserFilteringMetricViewModel, CriteriaOperator> <>9__34_1;
            public static Func<IEndUserFilteringMetricViewModel, bool> <>9__35_0;
            public static Func<IEndUserFilteringMetricViewModel, IValueViewModel> <>9__35_1;
            public static Func<IEndUserFilteringMetricViewModel, CriteriaOperator> <>9__36_0;

            internal bool <ClearFilterCriteria>b__35_0(IEndUserFilteringMetricViewModel mvm) => 
                mvm.HasValue;

            internal IValueViewModel <ClearFilterCriteria>b__35_1(IEndUserFilteringMetricViewModel mvm) => 
                mvm.Value;

            internal bool <CreateFilterCriteria>b__34_0(IEndUserFilteringMetricViewModel mvm) => 
                mvm.HasValue;

            internal CriteriaOperator <CreateFilterCriteria>b__34_1(IEndUserFilteringMetricViewModel mvm) => 
                mvm.FilterCriteria;

            internal IViewModelBuilder <CreateViewModel>b__25_0(IViewModelBuilderResolver resolver) => 
                resolver.CreateViewModelBuilder();

            internal CriteriaOperator <GetFilterCriteria>b__36_0(IEndUserFilteringMetricViewModel x) => 
                x.HasValue ? x.FilterCriteria : null;
        }
    }
}

