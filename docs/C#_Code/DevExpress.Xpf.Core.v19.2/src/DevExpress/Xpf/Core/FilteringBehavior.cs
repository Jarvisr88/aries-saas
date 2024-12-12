namespace DevExpress.Xpf.Core
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Utils.Filtering.Internal;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TargetType(typeof(UserControl)), TargetType(typeof(Window))]
    public class FilteringBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty SourceTypeProperty;
        private static readonly DependencyPropertyKey FilteringViewModelPropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty FilteringViewModelProperty;
        public static readonly DependencyProperty FilterCriteriaProperty;
        public static readonly DependencyProperty FilterStringProperty;
        private IEndUserFilteringViewModelProvider filteringViewModelProvider;

        static FilteringBehavior()
        {
            SourceTypeProperty = DependencyProperty.Register("SourceType", typeof(Type), typeof(FilteringBehavior), new PropertyMetadata(null, (d, e) => ((FilteringBehavior) d).OnSourceTypeChanged()));
            FilteringViewModelPropertyKey = DependencyProperty.RegisterReadOnly("FilteringViewModel", typeof(object), typeof(FilteringBehavior), new PropertyMetadata(null));
            FilteringViewModelProperty = FilteringViewModelPropertyKey.DependencyProperty;
            FilterCriteriaProperty = DependencyProperty.Register("FilterCriteria", typeof(CriteriaOperator), typeof(FilteringBehavior), new PropertyMetadata(null, (d, e) => ((FilteringBehavior) d).OnFilterCriteriaChanged()));
            FilterStringProperty = DependencyProperty.Register("FilterString", typeof(string), typeof(FilteringBehavior), new PropertyMetadata(null, (d, e) => ((FilteringBehavior) d).OnFilterStringChanged()));
        }

        internal static IEndUserFilteringViewModelProvider GetFilteringViewModelProvider(Type sourceType, object parentVM)
        {
            IEndUserFilteringViewModelProvider provider2 = new FilteringBehaviorServiceProvider().CreateViewModelProvider();
            provider2.ParentViewModel = parentVM;
            provider2.SourceType = sourceType;
            return provider2;
        }

        private void Initialize()
        {
            this.filteringViewModelProvider = GetFilteringViewModelProvider(this.SourceType, base.AssociatedObject.DataContext);
            ((INotifyPropertyChanged) this.filteringViewModelProvider).PropertyChanged += new PropertyChangedEventHandler(this.OnFilteringViewModelProviderPropertyChanged);
            this.FilteringViewModel = this.filteringViewModelProvider.ViewModel;
            this.filteringViewModelProvider.ApplyFilterCriteria(() => this.FilteringViewModel, this.FilterCriteria);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.Initialize();
            base.AssociatedObject.DataContextChanged += new DependencyPropertyChangedEventHandler(this.OnDataContextChanged);
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.filteringViewModelProvider.Do<IEndUserFilteringViewModelProvider>(x => x.ParentViewModel = ((FrameworkElement) sender).DataContext);
        }

        protected override void OnDetaching()
        {
            base.AssociatedObject.DataContextChanged -= new DependencyPropertyChangedEventHandler(this.OnDataContextChanged);
            this.Uninitialize();
            base.OnDetaching();
        }

        private void OnFilterCriteriaChanged()
        {
            string str = (this.FilterCriteria != null) ? this.FilterCriteria.ToString() : string.Empty;
            base.SetCurrentValue(FilterStringProperty, str);
            if ((this.filteringViewModelProvider != null) && ((this.FilteringViewModel != null) && !ReferenceEquals(this.FilterCriteria, this.filteringViewModelProvider.FilterCriteria)))
            {
                this.filteringViewModelProvider.ApplyFilterCriteria(() => this.FilteringViewModel, this.FilterCriteria);
            }
        }

        private void OnFilteringViewModelProviderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == BindableBase.GetPropertyName<CriteriaOperator>(System.Linq.Expressions.Expression.Lambda<Func<CriteriaOperator>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Field(System.Linq.Expressions.Expression.Constant(this, typeof(FilteringBehavior)), fieldof(FilteringBehavior.filteringViewModelProvider)), (MethodInfo) methodof(IEndUserFilteringViewModelProvider.get_FilterCriteria)), new ParameterExpression[0])))
            {
                this.FilterCriteria = this.filteringViewModelProvider.FilterCriteria;
            }
            if (e.PropertyName == BindableBase.GetPropertyName<object>(System.Linq.Expressions.Expression.Lambda<Func<object>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Field(System.Linq.Expressions.Expression.Constant(this, typeof(FilteringBehavior)), fieldof(FilteringBehavior.filteringViewModelProvider)), (MethodInfo) methodof(IEndUserFilteringViewModelProvider.get_ViewModel)), new ParameterExpression[0])))
            {
                this.FilteringViewModel = this.filteringViewModelProvider.ViewModel;
            }
        }

        private void OnFilterStringChanged()
        {
            if (this.FilterCriteria == null)
            {
                if (this.FilterString == string.Empty)
                {
                    return;
                }
            }
            else if (this.FilterCriteria.ToString() == this.FilterString)
            {
                return;
            }
            base.SetCurrentValue(FilterCriteriaProperty, CriteriaOperator.TryParse(this.FilterString, new object[0]));
        }

        private void OnSourceTypeChanged()
        {
            this.filteringViewModelProvider.Do<IEndUserFilteringViewModelProvider>(x => x.SourceType = this.SourceType);
        }

        private void Uninitialize()
        {
            ((INotifyPropertyChanged) this.filteringViewModelProvider).PropertyChanged -= new PropertyChangedEventHandler(this.OnFilteringViewModelProviderPropertyChanged);
            this.FilterCriteria = null;
            this.FilteringViewModel = null;
            this.filteringViewModelProvider.SourceType = null;
            this.filteringViewModelProvider.ParentViewModel = null;
            this.filteringViewModelProvider.Reset();
            this.filteringViewModelProvider = null;
        }

        public object FilteringViewModel
        {
            get => 
                base.GetValue(FilteringViewModelProperty);
            private set => 
                base.SetValue(FilteringViewModelPropertyKey, value);
        }

        public Type SourceType
        {
            get => 
                (Type) base.GetValue(SourceTypeProperty);
            set => 
                base.SetValue(SourceTypeProperty, value);
        }

        public CriteriaOperator FilterCriteria
        {
            get => 
                (CriteriaOperator) base.GetValue(FilterCriteriaProperty);
            set => 
                base.SetValue(FilterCriteriaProperty, value);
        }

        public string FilterString
        {
            get => 
                (string) base.GetValue(FilterStringProperty);
            set => 
                base.SetValue(FilterStringProperty, value);
        }

        protected override bool AllowAttachInDesignMode =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilteringBehavior.<>c <>9 = new FilteringBehavior.<>c();

            internal void <.cctor>b__34_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilteringBehavior) d).OnSourceTypeChanged();
            }

            internal void <.cctor>b__34_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilteringBehavior) d).OnFilterCriteriaChanged();
            }

            internal void <.cctor>b__34_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilteringBehavior) d).OnFilterStringChanged();
            }
        }

        private class FilteringBehaviorServiceProvider : FilteringUIServiceProvider
        {
            protected override IViewModelFactory GetViewModelFactory() => 
                new FilteringBehavior.ViewModelFactory();
        }

        private class ViewModelFactory : IViewModelFactory
        {
            public object Create(Type viewModelType, IViewModelBuilder builder) => 
                Activator.CreateInstance(ViewModelSource.GetPOCOType(viewModelType, new FilteringBehavior.ViewModelSourceBuilder(builder)));
        }

        private class ViewModelSourceBuilder : ViewModelSourceBuilderBase
        {
            private readonly IViewModelBuilder underlyingBuilder;

            public ViewModelSourceBuilder(IViewModelBuilder underlyingBuilder)
            {
                this.underlyingBuilder = underlyingBuilder;
            }

            protected override void BuildBindablePropertyAttributes(PropertyInfo property, PropertyBuilder builder)
            {
                base.BuildBindablePropertyAttributes(property, builder);
                if (this.underlyingBuilder != null)
                {
                    this.underlyingBuilder.BuildBindablePropertyAttributes(property, builder);
                }
            }

            protected override bool ForceOverrideProperty(PropertyInfo property) => 
                this.underlyingBuilder.ForceBindableProperty(property);
        }
    }
}

