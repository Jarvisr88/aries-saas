namespace DevExpress.Xpf.Core.ServerMode
{
    using DevExpress.Data.Linq;
    using DevExpress.Xpf.Core.DataSources;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;

    public class LinqServerModeDataSource : ListSourceDataSourceBase, IQueryableServerModeDataSource, IDataSource
    {
        public static readonly DependencyProperty DefaultSortingProperty;
        public static readonly DependencyProperty ElementTypeProperty;
        public static readonly DependencyProperty KeyExpressionProperty;
        public static readonly DependencyProperty QueryableSourceProperty;
        public static readonly RoutedEvent ExceptionThrownEvent;
        public static readonly RoutedEvent InconsistencyDetectedEvent;

        public event GetEnumerableEventHandler ExceptionThrown
        {
            add
            {
                base.AddHandler(ExceptionThrownEvent, value);
            }
            remove
            {
                base.RemoveHandler(ExceptionThrownEvent, value);
            }
        }

        public event RoutedEventHandler InconsistencyDetected
        {
            add
            {
                base.AddHandler(InconsistencyDetectedEvent, value);
            }
            remove
            {
                base.RemoveHandler(InconsistencyDetectedEvent, value);
            }
        }

        static LinqServerModeDataSource()
        {
            Type ownerType = typeof(LinqServerModeDataSource);
            DefaultSortingProperty = DependencyProperty.Register("DefaultSorting", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(LinqServerModeDataSource.OnDefaultSortingChanged)));
            ElementTypeProperty = DependencyProperty.Register("ElementType", typeof(Type), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(LinqServerModeDataSource.OnElementTypeChanged)));
            KeyExpressionProperty = DependencyProperty.Register("KeyExpression", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(LinqServerModeDataSource.OnKeyExpressionChanged)));
            QueryableSourceProperty = DependencyProperty.Register("QueryableSource", typeof(IQueryable), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(LinqServerModeDataSource.OnQueryableSourceChanged)));
            ExceptionThrownEvent = EventManager.RegisterRoutedEvent("ExceptionThrown", RoutingStrategy.Direct, typeof(ServerModeExceptionThrownEventHandler), ownerType);
            InconsistencyDetectedEvent = EventManager.RegisterRoutedEvent("InconsistencyDetected", RoutingStrategy.Direct, typeof(ServerModeInconsistencyDetectedEventHandler), ownerType);
        }

        public LinqServerModeDataSource()
        {
            this.ResetLinqServerModeSource();
        }

        protected override DataSourceStrategyBase CreateDataSourceStrategy() => 
            new QueryableServerModeDataSourceStrategy(this);

        protected override string GetDesignTimeImageName() => 
            "DevExpress.Xpf.Core.Core.Images.LinqServerModeDataSource.png";

        private void linqServerModeSource_ExceptionThrown(object sender, LinqServerModeExceptionThrownEventArgs e)
        {
            ServerModeExceptionThrownEventArgs args = new ServerModeExceptionThrownEventArgs(e.Exception);
            this.RaiseExceptionThrown(args);
        }

        private void linqServerModeSource_InconsistencyDetected(object sender, LinqServerModeInconsistencyDetectedEventArgs e)
        {
            ServerModeInconsistencyDetectedEventArgs args = new ServerModeInconsistencyDetectedEventArgs();
            this.RaiseInconsistencyDetected(args);
        }

        private static void OnDefaultSortingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LinqServerModeSource data = ((LinqServerModeDataSource) d).Data as LinqServerModeSource;
            if (data != null)
            {
                data.DefaultSorting = (string) e.NewValue;
            }
        }

        private static void OnElementTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LinqServerModeSource data = ((LinqServerModeDataSource) d).Data as LinqServerModeSource;
            if (data != null)
            {
                data.ElementType = (Type) e.NewValue;
            }
        }

        private static void OnKeyExpressionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LinqServerModeSource data = ((LinqServerModeDataSource) d).Data as LinqServerModeSource;
            if (data != null)
            {
                data.KeyExpression = (string) e.NewValue;
            }
        }

        private static void OnQueryableSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LinqServerModeDataSource) d).ResetLinqServerModeSource();
        }

        protected virtual void RaiseExceptionThrown(ServerModeExceptionThrownEventArgs args)
        {
            args.RoutedEvent = ExceptionThrownEvent;
            base.RaiseEvent(args);
        }

        protected virtual void RaiseInconsistencyDetected(ServerModeInconsistencyDetectedEventArgs args)
        {
            args.RoutedEvent = InconsistencyDetectedEvent;
            base.RaiseEvent(args);
        }

        public void Reload()
        {
            if (base.Data != null)
            {
                ((LinqServerModeSource) base.Data).Reload();
            }
        }

        private void ResetLinqServerModeSource()
        {
            if (base.Data != null)
            {
                LinqServerModeSource data = (LinqServerModeSource) base.Data;
                data.Dispose();
                data.ExceptionThrown -= new LinqServerModeExceptionThrownEventHandler(this.linqServerModeSource_ExceptionThrown);
                data.InconsistencyDetected -= new LinqServerModeInconsistencyDetectedEventHandler(this.linqServerModeSource_InconsistencyDetected);
            }
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                base.Data = null;
            }
            else
            {
                LinqServerModeSource source2 = new LinqServerModeSource {
                    DefaultSorting = this.DefaultSorting,
                    ElementType = this.ElementType,
                    KeyExpression = this.KeyExpression,
                    QueryableSource = this.QueryableSource
                };
                source2.ExceptionThrown += new LinqServerModeExceptionThrownEventHandler(this.linqServerModeSource_ExceptionThrown);
                source2.InconsistencyDetected += new LinqServerModeInconsistencyDetectedEventHandler(this.linqServerModeSource_InconsistencyDetected);
                base.Data = source2;
            }
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
        public Type ElementType
        {
            get => 
                (Type) base.GetValue(ElementTypeProperty);
            set => 
                base.SetValue(ElementTypeProperty, value);
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
    }
}

