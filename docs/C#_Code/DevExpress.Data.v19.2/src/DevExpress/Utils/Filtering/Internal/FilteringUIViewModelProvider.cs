namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.MVVM;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;

    public class FilteringUIViewModelProvider : FilteringUIViewModelProviderBase, IEndUserFilteringViewModelProvider, IEndUserFilteringViewModelPropertyValues, IEnumerable<IEndUserFilteringMetricViewModel>, IEnumerable, IEndUserFilteringCriteriaChangeAware
    {
        private Type sourceTypeCore;
        private IEnumerable<IEndUserFilteringMetricAttributes> attributesCore;
        private Type viewModelBaseTypeCore;
        private object parentViewModelCore;
        private IViewModelProvider parentViewModelProviderCore;

        public FilteringUIViewModelProvider(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            base.RegisterService<IEndUserFilteringViewModelProvider>(this);
        }

        void IEndUserFilteringViewModelProvider.RetrieveFields(Action<Type> retrieveFields, Type sourceType, IEnumerable<IEndUserFilteringMetricAttributes> attributes, Type viewModelBaseType)
        {
            if (retrieveFields != null)
            {
                base.RetrieveType(sourceType, attributes, viewModelBaseType).Do<Type>(retrieveFields);
            }
        }

        void IEndUserFilteringViewModelProvider.UpdateMemberBindings(string path)
        {
            base.UpdateMemberBindings(path);
        }

        protected override IEnumerable<IEndUserFilteringMetricAttributes> GetAttributes() => 
            this.Attributes;

        protected sealed override object GetParentViewModel() => 
            this.parentViewModelCore;

        protected override Type GetSourceType() => 
            this.SourceType;

        protected sealed override Type GetViewModelBaseType() => 
            this.viewModelBaseTypeCore;

        protected virtual void OnAttributesChanged()
        {
            base.Update();
        }

        protected override void OnModelChanged()
        {
            base.OnModelChanged();
            this.RaisePropertyChanged<Type>(Expression.Lambda<Func<Type>>(Expression.Property(Expression.Constant(this, typeof(FilteringUIViewModelProvider)), (MethodInfo) methodof(FilteringUIViewModelProviderBase.get_ViewModelType)), new ParameterExpression[0]));
            this.RaisePropertyChanged<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(FilteringUIViewModelProvider)), (MethodInfo) methodof(FilteringUIViewModelProviderBase.get_ViewModel)), new ParameterExpression[0]));
            this.RaisePropertyChanged<CriteriaOperator>(Expression.Lambda<Func<CriteriaOperator>>(Expression.Property(Expression.Constant(this, typeof(FilteringUIViewModelProvider)), (MethodInfo) methodof(FilteringUIViewModelProviderBase.get_FilterCriteria)), new ParameterExpression[0]));
        }

        protected virtual void OnParentViewModelChanged(object oldValue, object newValue)
        {
            this.UnsubscribeParentViewModel(oldValue);
            base.UpdateMemberBindings();
            this.SubscribeParentViewModel(newValue);
            this.RaisePropertyChanged<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(FilteringUIViewModelProvider)), (MethodInfo) methodof(FilteringUIViewModelProvider.get_ParentViewModel)), new ParameterExpression[0]));
        }

        protected virtual void OnParentViewModelProviderChanged(IViewModelProvider oldValue, IViewModelProvider newValue)
        {
            this.UnsubscribeParentViewModelProvider(oldValue);
            this.UpdateParentViewModel(newValue);
            this.SubscribeParentViewModelProvider(newValue);
            this.RaisePropertyChanged<IViewModelProvider>(Expression.Lambda<Func<IViewModelProvider>>(Expression.Property(Expression.Constant(this, typeof(FilteringUIViewModelProvider)), (MethodInfo) methodof(FilteringUIViewModelProvider.get_ParentViewModelProvider)), new ParameterExpression[0]));
        }

        protected virtual void OnSourceTypeChanged()
        {
            base.Update();
        }

        protected virtual void OnViewModelBaseTypeChanged()
        {
            base.ResetViewModelType();
            base.ResetFilterCriteria();
            this.RaisePropertyChanged<Type>(Expression.Lambda<Func<Type>>(Expression.Property(Expression.Constant(this, typeof(FilteringUIViewModelProvider)), (MethodInfo) methodof(FilteringUIViewModelProviderBase.get_ViewModelType)), new ParameterExpression[0]));
            this.RaisePropertyChanged<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(FilteringUIViewModelProvider)), (MethodInfo) methodof(FilteringUIViewModelProviderBase.get_ViewModel)), new ParameterExpression[0]));
            this.RaisePropertyChanged<CriteriaOperator>(Expression.Lambda<Func<CriteriaOperator>>(Expression.Property(Expression.Constant(this, typeof(FilteringUIViewModelProvider)), (MethodInfo) methodof(FilteringUIViewModelProviderBase.get_FilterCriteria)), new ParameterExpression[0]));
        }

        private void ParentViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.UpdateMemberBindings(sender, e.PropertyName);
        }

        private void ParentViewModelProvider_ViewModelChanged(object sender, EventArgs e)
        {
            this.UpdateParentViewModel(sender as IViewModelProvider);
        }

        private void SubscribeParentViewModel(object parentViewModel)
        {
            (parentViewModel as INotifyPropertyChanged).Do<INotifyPropertyChanged>(delegate (INotifyPropertyChanged npc) {
                npc.PropertyChanged += new PropertyChangedEventHandler(this.ParentViewModel_PropertyChanged);
            });
        }

        private void SubscribeParentViewModelProvider(IViewModelProvider viewModelProvider)
        {
            viewModelProvider.Do<IViewModelProvider>(delegate (IViewModelProvider vmp) {
                vmp.ViewModelChanged += new EventHandler(this.ParentViewModelProvider_ViewModelChanged);
            });
        }

        private void UnsubscribeParentViewModel(object parentViewModel)
        {
            (parentViewModel as INotifyPropertyChanged).Do<INotifyPropertyChanged>(delegate (INotifyPropertyChanged npc) {
                npc.PropertyChanged -= new PropertyChangedEventHandler(this.ParentViewModel_PropertyChanged);
            });
        }

        private void UnsubscribeParentViewModelProvider(IViewModelProvider viewModelProvider)
        {
            viewModelProvider.Do<IViewModelProvider>(delegate (IViewModelProvider vmp) {
                vmp.ViewModelChanged -= new EventHandler(this.ParentViewModelProvider_ViewModelChanged);
            });
        }

        protected void UpdateParentViewModel(IViewModelProvider provider)
        {
            provider.Do<IViewModelProvider>(vmp => this.ParentViewModel = vmp.IsViewModelCreated ? vmp.ViewModel : null);
        }

        public Type SourceType
        {
            get => 
                this.sourceTypeCore;
            set
            {
                if (value != this.sourceTypeCore)
                {
                    this.sourceTypeCore = value;
                    this.OnSourceTypeChanged();
                }
            }
        }

        public IEnumerable<IEndUserFilteringMetricAttributes> Attributes
        {
            get => 
                this.attributesCore;
            set
            {
                if (!ReferenceEquals(value, this.attributesCore))
                {
                    this.attributesCore = value;
                    this.OnAttributesChanged();
                }
            }
        }

        public Type ViewModelBaseType
        {
            get => 
                this.viewModelBaseTypeCore;
            set
            {
                if (value != this.viewModelBaseTypeCore)
                {
                    this.viewModelBaseTypeCore = value;
                    this.OnViewModelBaseTypeChanged();
                }
            }
        }

        public object ParentViewModel
        {
            get => 
                this.parentViewModelCore;
            set
            {
                if (this.parentViewModelCore != value)
                {
                    object parentViewModelCore = this.parentViewModelCore;
                    this.parentViewModelCore = value;
                    this.OnParentViewModelChanged(parentViewModelCore, value);
                }
            }
        }

        public IViewModelProvider ParentViewModelProvider
        {
            get => 
                this.parentViewModelProviderCore;
            set
            {
                if (!ReferenceEquals(this.parentViewModelProviderCore, value))
                {
                    IViewModelProvider parentViewModelProviderCore = this.parentViewModelProviderCore;
                    this.parentViewModelProviderCore = value;
                    this.OnParentViewModelProviderChanged(parentViewModelProviderCore, value);
                }
            }
        }
    }
}

