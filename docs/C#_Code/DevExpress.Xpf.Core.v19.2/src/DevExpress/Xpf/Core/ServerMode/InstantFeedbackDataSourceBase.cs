namespace DevExpress.Xpf.Core.ServerMode
{
    using DevExpress.Data.Linq;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.DataSources;
    using DevExpress.Xpf.Core.ServerMode.Native;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    [DefaultEvent("GetQueryable")]
    public abstract class InstantFeedbackDataSourceBase : ListSourceDataSourceBase, IQueryableServerModeDataSource, IDataSource, IDisposable
    {
        public static readonly DependencyProperty AreSourceRowsThreadSafeProperty;
        public static readonly DependencyProperty DefaultSortingProperty;
        public static readonly DependencyProperty KeyExpressionProperty;
        public static readonly DependencyProperty QueryableSourceProperty;
        public static readonly RoutedEvent GetQueryableEvent;
        public static readonly RoutedEvent DismissQueryableEvent;
        private readonly InstantFeedbackDataSourceWrapper instantFeedbackDataSourceWrapper;
        private volatile bool isDisposed;

        public event GetQueryableEventHandler DismissQueryable
        {
            add
            {
                base.AddHandler(DismissQueryableEvent, value);
            }
            remove
            {
                base.RemoveHandler(DismissQueryableEvent, value);
            }
        }

        public event GetQueryableEventHandler GetQueryable
        {
            add
            {
                base.AddHandler(GetQueryableEvent, value);
            }
            remove
            {
                base.RemoveHandler(GetQueryableEvent, value);
            }
        }

        static InstantFeedbackDataSourceBase()
        {
            Type ownerType = typeof(InstantFeedbackDataSourceBase);
            AreSourceRowsThreadSafeProperty = DependencyProperty.Register("AreSourceRowsThreadSafe", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(InstantFeedbackDataSourceBase.OnAreSourceRowsThreadSafeChanged)));
            DefaultSortingProperty = DependencyProperty.Register("DefaultSorting", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(InstantFeedbackDataSourceBase.OnDefaultSortingChanged)));
            KeyExpressionProperty = DependencyProperty.Register("KeyExpression", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(InstantFeedbackDataSourceBase.OnKeyExpressionChanged)));
            QueryableSourceProperty = DependencyProperty.Register("QueryableSource", typeof(IQueryable), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(InstantFeedbackDataSourceBase.OnQueryableSourceChanged)));
            GetQueryableEvent = EventManager.RegisterRoutedEvent("GetQueryable", RoutingStrategy.Direct, typeof(GetQueryableEventHandler), ownerType);
            DismissQueryableEvent = EventManager.RegisterRoutedEvent("DismissQueryable", RoutingStrategy.Direct, typeof(GetQueryableEventHandler), ownerType);
        }

        public InstantFeedbackDataSourceBase()
        {
            this.instantFeedbackDataSourceWrapper = this.CreateDataSourceWrapper();
            this.ResetInstantFeedbackSource();
            Func<object, bool> canExecuteMethod = <>c.<>9__39_1;
            if (<>c.<>9__39_1 == null)
            {
                Func<object, bool> local1 = <>c.<>9__39_1;
                canExecuteMethod = <>c.<>9__39_1 = parameter => true;
            }
            this.DisposeCommand = DelegateCommandFactory.Create<object>(parameter => this.Dispose(), canExecuteMethod, false);
        }

        protected override DataSourceStrategyBase CreateDataSourceStrategy() => 
            new InstantFeedbackDataSourceStrategy(this);

        protected abstract InstantFeedbackDataSourceWrapper CreateDataSourceWrapper();
        protected void Data_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            if (this.isDisposed)
            {
                IListSource source = (IListSource) sender;
                this.instantFeedbackDataSourceWrapper.UnsubscribeGetQueryable(source);
                this.instantFeedbackDataSourceWrapper.UnsubscribeDismissQueryable(source);
            }
            DevExpress.Xpf.Core.ServerMode.GetQueryableEventArgs args = new DevExpress.Xpf.Core.ServerMode.GetQueryableEventArgs();
            this.RaiseDismissQueryable(args);
            if (args.Handled)
            {
                e.AreSourceRowsThreadSafe = args.AreSourceRowsThreadSafe;
                e.KeyExpression = args.KeyExpression;
                e.QueryableSource = args.QueryableSource;
                e.Tag = args.Tag;
            }
        }

        protected void Data_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            DevExpress.Xpf.Core.ServerMode.GetQueryableEventArgs args = new DevExpress.Xpf.Core.ServerMode.GetQueryableEventArgs();
            this.RaiseGetQueryable(args);
            if (args.Handled)
            {
                e.AreSourceRowsThreadSafe = args.AreSourceRowsThreadSafe;
                e.KeyExpression = args.KeyExpression;
                e.QueryableSource = args.QueryableSource;
                e.Tag = args.Tag;
            }
            else
            {
                IQueryable actualQueryableSource = this.GetActualQueryableSource();
                if (actualQueryableSource != null)
                {
                    e.QueryableSource = actualQueryableSource;
                }
            }
        }

        public void Dispose()
        {
            this.isDisposed = true;
            if (base.Data is IDisposable)
            {
                ((IDisposable) base.Data).Dispose();
            }
        }

        private IQueryable GetActualQueryableSource()
        {
            if (this.QueryField != null)
            {
                return this.QueryField;
            }
            object contextInstance = base.Strategy.CreateContextIstance();
            return ((contextInstance != null) ? (base.Strategy.GetDataMemberValue(contextInstance) as IQueryable) : null);
        }

        private static void OnAreSourceRowsThreadSafeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InstantFeedbackDataSourceBase base2 = (InstantFeedbackDataSourceBase) d;
            if (base2.Data != null)
            {
                base2.instantFeedbackDataSourceWrapper.SetAreSourceRowsThreadSafe(base2.Data, (bool) e.NewValue);
            }
        }

        private static void OnDefaultSortingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InstantFeedbackDataSourceBase base2 = (InstantFeedbackDataSourceBase) d;
            if (base2.Data != null)
            {
                base2.instantFeedbackDataSourceWrapper.SetDefaultSorting(base2.Data, (string) e.NewValue);
            }
        }

        private static void OnKeyExpressionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InstantFeedbackDataSourceBase base2 = (InstantFeedbackDataSourceBase) d;
            IListSource data = ((InstantFeedbackDataSourceBase) d).Data;
            if (base2.Data != null)
            {
                base2.instantFeedbackDataSourceWrapper.SetKeyExpression(base2.Data, (string) e.NewValue);
            }
        }

        private static void OnQueryableSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InstantFeedbackDataSourceBase base2 = (InstantFeedbackDataSourceBase) d;
            base2.QueryField = base2.QueryableSource;
            base2.ResetInstantFeedbackSource();
        }

        protected virtual void RaiseDismissQueryable(DevExpress.Xpf.Core.ServerMode.GetQueryableEventArgs args)
        {
            args.RoutedEvent = DismissQueryableEvent;
            base.RaiseEvent(args);
        }

        protected virtual void RaiseGetQueryable(DevExpress.Xpf.Core.ServerMode.GetQueryableEventArgs args)
        {
            args.RoutedEvent = GetQueryableEvent;
            base.RaiseEvent(args);
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
                this.instantFeedbackDataSourceWrapper.SubscribeGetQueryable(source);
                this.instantFeedbackDataSourceWrapper.SubscribeDismissQueryable(source);
                base.Data = source;
            }
        }

        protected IQueryable QueryField { get; set; }

        [Category("Data")]
        public bool AreSourceRowsThreadSafe
        {
            get => 
                (bool) base.GetValue(AreSourceRowsThreadSafeProperty);
            set => 
                base.SetValue(AreSourceRowsThreadSafeProperty, value);
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
        public IQueryable QueryableSource
        {
            get => 
                (IQueryable) base.GetValue(QueryableSourceProperty);
            set => 
                base.SetValue(QueryableSourceProperty, value);
        }

        public ICommand DisposeCommand { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly InstantFeedbackDataSourceBase.<>c <>9 = new InstantFeedbackDataSourceBase.<>c();
            public static Func<object, bool> <>9__39_1;

            internal bool <.ctor>b__39_1(object parameter) => 
                true;
        }
    }
}

