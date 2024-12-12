namespace DevExpress.Xpf.Core.ServerMode
{
    using DevExpress.Data.Linq;
    using DevExpress.Xpf.Core.DataSources;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;

    public class EntityServerModeDataSource : ListSourceDataSourceBase, IQueryableServerModeDataSource, IDataSource
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

        static EntityServerModeDataSource()
        {
            Type ownerType = typeof(EntityServerModeDataSource);
            DefaultSortingProperty = DependencyProperty.Register("DefaultSorting", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(EntityServerModeDataSource.OnDefaultSortingChanged)));
            ElementTypeProperty = DependencyProperty.Register("ElementType", typeof(Type), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(EntityServerModeDataSource.OnElementTypeChanged)));
            KeyExpressionProperty = DependencyProperty.Register("KeyExpression", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(EntityServerModeDataSource.OnKeyExpressionChanged)));
            QueryableSourceProperty = DependencyProperty.Register("QueryableSource", typeof(IQueryable), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(EntityServerModeDataSource.OnQueryableSourceChanged)));
            ExceptionThrownEvent = EventManager.RegisterRoutedEvent("ExceptionThrown", RoutingStrategy.Direct, typeof(ServerModeExceptionThrownEventHandler), ownerType);
            InconsistencyDetectedEvent = EventManager.RegisterRoutedEvent("InconsistencyDetected", RoutingStrategy.Direct, typeof(ServerModeInconsistencyDetectedEventHandler), ownerType);
        }

        public EntityServerModeDataSource()
        {
            this.ResetEntityServerModeSource();
        }

        protected override DataSourceStrategyBase CreateDataSourceStrategy() => 
            new QueryableServerModeDataSourceStrategy(this);

        private void entityServerModeSource_ExceptionThrown(object sender, LinqServerModeExceptionThrownEventArgs e)
        {
            ServerModeExceptionThrownEventArgs args = new ServerModeExceptionThrownEventArgs(e.Exception);
            this.RaiseExceptionThrown(args);
        }

        private void entityServerModeSource_InconsistencyDetected(object sender, LinqServerModeInconsistencyDetectedEventArgs e)
        {
            ServerModeInconsistencyDetectedEventArgs args = new ServerModeInconsistencyDetectedEventArgs();
            this.RaiseInconsistencyDetected(args);
        }

        protected override string GetDesignTimeImageName() => 
            "DevExpress.Xpf.Core.Core.Images.EntityServerModeDataSource.png";

        private static void OnDefaultSortingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EntityServerModeSource data = ((EntityServerModeDataSource) d).Data as EntityServerModeSource;
            if (data != null)
            {
                data.DefaultSorting = (string) e.NewValue;
            }
        }

        private static void OnElementTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EntityServerModeSource data = ((EntityServerModeDataSource) d).Data as EntityServerModeSource;
            if (data != null)
            {
                data.ElementType = (Type) e.NewValue;
            }
        }

        private static void OnKeyExpressionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EntityServerModeSource data = ((EntityServerModeDataSource) d).Data as EntityServerModeSource;
            if (data != null)
            {
                data.KeyExpression = (string) e.NewValue;
            }
        }

        private static void OnQueryableSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EntityServerModeDataSource) d).ResetEntityServerModeSource();
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
                ((EntityServerModeSource) base.Data).Reload();
            }
        }

        private void ResetEntityServerModeSource()
        {
            if (base.Data != null)
            {
                EntityServerModeSource data = (EntityServerModeSource) base.Data;
                data.Dispose();
                data.ExceptionThrown -= new LinqServerModeExceptionThrownEventHandler(this.entityServerModeSource_ExceptionThrown);
                data.InconsistencyDetected -= new LinqServerModeInconsistencyDetectedEventHandler(this.entityServerModeSource_InconsistencyDetected);
            }
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                base.Data = null;
            }
            else
            {
                EntityServerModeSource source2 = new EntityServerModeSource {
                    DefaultSorting = this.DefaultSorting,
                    ElementType = this.ElementType,
                    KeyExpression = this.KeyExpression,
                    QueryableSource = this.QueryableSource
                };
                source2.ExceptionThrown += new LinqServerModeExceptionThrownEventHandler(this.entityServerModeSource_ExceptionThrown);
                source2.InconsistencyDetected += new LinqServerModeInconsistencyDetectedEventHandler(this.entityServerModeSource_InconsistencyDetected);
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

