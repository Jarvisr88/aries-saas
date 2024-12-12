namespace DevExpress.Xpf.Core.ServerMode
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.DataSources;
    using DevExpress.Xpf.Core.ServerMode.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    [DefaultEvent("GetSource")]
    public abstract class WcfInstantFeedbackDataSourceBase : ListSourceDataSourceBase, IWcfServerModeDataSource, IWcfDataSource, IDataSource, IDisposable
    {
        public static readonly DependencyProperty AreSourceRowsThreadSafeProperty;
        public static readonly DependencyProperty DefaultSortingProperty;
        public static readonly DependencyProperty FixedFilterProperty;
        public static readonly DependencyProperty KeyExpressionProperty;
        public static readonly DependencyProperty DataServiceContextProperty;
        public static readonly DependencyProperty QueryProperty;
        public static readonly DependencyProperty ServiceRootProperty;
        private readonly WcfInstantFeedbackDataSourceWrapper instantFeedbackDataSourceWrapper;
        private volatile bool isDisposed;

        static WcfInstantFeedbackDataSourceBase()
        {
            Type ownerType = typeof(WcfInstantFeedbackDataSourceBase);
            AreSourceRowsThreadSafeProperty = DependencyPropertyManager.Register("AreSourceRowsThreadSafe", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(WcfInstantFeedbackDataSourceBase.OnAreSourceRowsThreadSafeChanged)));
            FixedFilterProperty = DependencyPropertyManager.Register("FixedFilter", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(WcfInstantFeedbackDataSourceBase.OnFixedFilterChanged)));
            DefaultSortingProperty = DependencyPropertyManager.Register("DefaultSorting", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(WcfInstantFeedbackDataSourceBase.OnDefaultSortingChanged)));
            KeyExpressionProperty = DependencyPropertyManager.Register("KeyExpression", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(WcfInstantFeedbackDataSourceBase.OnKeyExpressionChanged)));
            DataServiceContextProperty = DependencyPropertyManager.Register("DataServiceContext", typeof(object), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(WcfInstantFeedbackDataSourceBase.OnDataServiceContextChanged)));
            QueryProperty = DependencyPropertyManager.Register("Query", typeof(IQueryable), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(WcfInstantFeedbackDataSourceBase.OnQueryChanged)));
            ServiceRootProperty = DependencyPropertyManager.Register("ServiceRoot", typeof(Uri), ownerType, new PropertyMetadata(new PropertyChangedCallback(WcfInstantFeedbackDataSourceBase.OnServiceRootChanged)));
        }

        public WcfInstantFeedbackDataSourceBase()
        {
            this.instantFeedbackDataSourceWrapper = this.CreateDataSourceWrapper();
            this.ResetInstantFeedbackSource();
            Func<object, bool> canExecuteMethod = <>c.<>9__17_1;
            if (<>c.<>9__17_1 == null)
            {
                Func<object, bool> local1 = <>c.<>9__17_1;
                canExecuteMethod = <>c.<>9__17_1 = parameter => true;
            }
            this.DisposeCommand = DelegateCommandFactory.Create<object>(parameter => this.Dispose(), canExecuteMethod, false);
        }

        protected override DataSourceStrategyBase CreateDataSourceStrategy() => 
            new WcfInstantFeedackModeDataSourceStrategy(this);

        protected abstract WcfInstantFeedbackDataSourceWrapper CreateDataSourceWrapper();
        public void Dispose()
        {
            this.isDisposed = true;
            if (base.Data is IDisposable)
            {
                ((IDisposable) base.Data).Dispose();
            }
        }

        protected void DisposeData(object sender)
        {
            if (this.isDisposed)
            {
                IListSource source = (IListSource) sender;
                this.instantFeedbackDataSourceWrapper.UnsubscribeGetQueryable(source);
                this.instantFeedbackDataSourceWrapper.UnsubscribeDismissQueryable(source);
            }
        }

        protected IQueryable GetActualQueryableSource()
        {
            if (this.QueryField != null)
            {
                return this.QueryField;
            }
            object contextInstance = base.Strategy.CreateContextIstance();
            return (base.Strategy.GetDataMemberValue(contextInstance) as IQueryable);
        }

        private static void OnAreSourceRowsThreadSafeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WcfInstantFeedbackDataSourceBase) d).ResetInstantFeedbackSource();
        }

        private static void OnDataServiceContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WcfInstantFeedbackDataSourceBase base2 = (WcfInstantFeedbackDataSourceBase) d;
            base2.ContextField = base2.DataServiceContext;
            base2.ResetInstantFeedbackSource();
        }

        private static void OnDefaultSortingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WcfInstantFeedbackDataSourceBase) d).ResetInstantFeedbackSource();
        }

        private static void OnFixedFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WcfInstantFeedbackDataSourceBase) d).ResetInstantFeedbackSource();
        }

        private static void OnKeyExpressionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WcfInstantFeedbackDataSourceBase base2 = (WcfInstantFeedbackDataSourceBase) d;
            base2.KeyExpressionField = base2.KeyExpression;
            base2.ResetInstantFeedbackSource();
        }

        private static void OnQueryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WcfInstantFeedbackDataSourceBase base2 = (WcfInstantFeedbackDataSourceBase) d;
            base2.QueryField = base2.Query;
            base2.ResetInstantFeedbackSource();
        }

        private static void OnServiceRootChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WcfInstantFeedbackDataSourceBase base2 = (WcfInstantFeedbackDataSourceBase) d;
            if (base2.Strategy is WcfDataSourceStrategyBase)
            {
                ((WcfDataSourceStrategyBase) base2.Strategy).Update(base2.ServiceRoot);
            }
            base2.UpdateData();
        }

        public void Refresh()
        {
            if (base.Data != null)
            {
                this.instantFeedbackDataSourceWrapper.Refresh(base.Data);
            }
        }

        internal void ResetInstantFeedbackSource()
        {
            if (base.Data is IDisposable)
            {
                ((IDisposable) base.Data).Dispose();
            }
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                base.Data = null;
            }
            else
            {
                IListSource source = this.instantFeedbackDataSourceWrapper.Create();
                this.instantFeedbackDataSourceWrapper.SetAreSourceRowsThreadSafe(source, this.AreSourceRowsThreadSafe);
                this.instantFeedbackDataSourceWrapper.SetDefaultSorting(source, this.DefaultSorting);
                this.instantFeedbackDataSourceWrapper.SetKeyExpression(source, this.KeyExpression);
                this.instantFeedbackDataSourceWrapper.SetFixedFilter(source, this.FixedFilter);
                this.instantFeedbackDataSourceWrapper.SubscribeGetQueryable(source);
                this.instantFeedbackDataSourceWrapper.SubscribeDismissQueryable(source);
                base.Data = source;
            }
        }

        protected IQueryable QueryField { get; set; }

        protected string KeyExpressionField { get; set; }

        protected object ContextField { get; set; }

        [Category("Data")]
        public bool AreSourceRowsThreadSafe
        {
            get => 
                (bool) base.GetValue(AreSourceRowsThreadSafeProperty);
            set => 
                base.SetValue(AreSourceRowsThreadSafeProperty, value);
        }

        [Category("Data")]
        public string FixedFilter
        {
            get => 
                (string) base.GetValue(FixedFilterProperty);
            set => 
                base.SetValue(FixedFilterProperty, value);
        }

        [Category("Data")]
        public string DefaultSorting
        {
            get => 
                (string) base.GetValue(DefaultSortingProperty);
            set => 
                base.SetValue(DefaultSortingProperty, value);
        }

        [Category("Data")]
        public string KeyExpression
        {
            get => 
                (string) base.GetValue(KeyExpressionProperty);
            set => 
                base.SetValue(KeyExpressionProperty, value);
        }

        [Category("Data")]
        public object DataServiceContext
        {
            get => 
                base.GetValue(DataServiceContextProperty);
            set => 
                base.SetValue(DataServiceContextProperty, value);
        }

        [Category("Data")]
        public IQueryable Query
        {
            get => 
                (IQueryable) base.GetValue(QueryProperty);
            set => 
                base.SetValue(QueryProperty, value);
        }

        [Category("Data")]
        public Uri ServiceRoot
        {
            get => 
                (Uri) base.GetValue(ServiceRootProperty);
            set => 
                base.SetValue(ServiceRootProperty, value);
        }

        public ICommand DisposeCommand { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WcfInstantFeedbackDataSourceBase.<>c <>9 = new WcfInstantFeedbackDataSourceBase.<>c();
            public static Func<object, bool> <>9__17_1;

            internal bool <.ctor>b__17_1(object parameter) => 
                true;
        }
    }
}

