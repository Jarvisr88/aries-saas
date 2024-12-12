namespace DevExpress.Xpf.Core.ServerMode
{
    using DevExpress.Data;
    using DevExpress.Data.ODataLinq;
    using DevExpress.Xpf.Core.DataSources;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows;

    public class ODataServerModeDataSource : ListSourceDataSourceBase, IWcfServerModeDataSource, IWcfDataSource, IDataSource
    {
        public static readonly DependencyProperty DefaultSortingProperty;
        public static readonly DependencyProperty FixedFilterProperty;
        public static readonly DependencyProperty ElementTypeProperty;
        public static readonly DependencyProperty KeyExpressionsProperty;
        public static readonly DependencyProperty KeyExpressionProperty;
        public static readonly DependencyProperty DataServiceContextProperty;
        public static readonly DependencyProperty QueryProperty;
        public static readonly DependencyProperty ServiceRootProperty;
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

        static ODataServerModeDataSource()
        {
            Type ownerType = typeof(ODataServerModeDataSource);
            FixedFilterProperty = DependencyProperty.Register("FixedFilter", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(ODataServerModeDataSource.OnFixedFilterChanged)));
            DefaultSortingProperty = DependencyProperty.Register("DefaultSorting", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(ODataServerModeDataSource.OnDefaultSortingChanged)));
            ElementTypeProperty = DependencyProperty.Register("ElementType", typeof(Type), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ODataServerModeDataSource.OnElementTypeChanged)));
            KeyExpressionsProperty = DependencyProperty.Register("KeyExpressions", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(ODataServerModeDataSource.OnKeyExpressionsChanged)));
            KeyExpressionProperty = DependencyProperty.Register("KeyExpression", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(ODataServerModeDataSource.OnKeyExpressionChanged)));
            DataServiceContextProperty = DependencyProperty.Register("DataServiceContext", typeof(object), ownerType, new PropertyMetadata((d, e) => ((ODataServerModeDataSource) d).ResetODataServerModeSource()));
            QueryProperty = DependencyProperty.Register("Query", typeof(IQueryable), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ODataServerModeDataSource.OnQueryChanged)));
            ServiceRootProperty = DependencyProperty.Register("ServiceRoot", typeof(Uri), ownerType, new PropertyMetadata(new PropertyChangedCallback(ODataServerModeDataSource.OnServiceRootChanged)));
            ExceptionThrownEvent = EventManager.RegisterRoutedEvent("ExceptionThrown", RoutingStrategy.Direct, typeof(ServerModeExceptionThrownEventHandler), ownerType);
            InconsistencyDetectedEvent = EventManager.RegisterRoutedEvent("InconsistencyDetected", RoutingStrategy.Direct, typeof(ServerModeInconsistencyDetectedEventHandler), ownerType);
        }

        public ODataServerModeDataSource()
        {
            this.ResetODataServerModeSource();
        }

        protected override DataSourceStrategyBase CreateDataSourceStrategy() => 
            new WcfServerModeDataSourceStrategy(this);

        internal static string[] CreateKeyExpressionArray(string keyExpressions)
        {
            if (keyExpressions == null)
            {
                return null;
            }
            char[] separator = new char[] { ';' };
            return keyExpressions.Split(separator);
        }

        internal static string CreateKeyExpressionString(string[] keyExpressions)
        {
            if ((keyExpressions == null) || (keyExpressions.Length == 0))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < keyExpressions.Length; i++)
            {
                if (i != 0)
                {
                    builder.Append(';');
                }
                builder.Append(keyExpressions[i]);
            }
            return builder.ToString();
        }

        private void ODataServerModeSource_ExceptionThrown(object sender, DevExpress.Data.ServerModeExceptionThrownEventArgs e)
        {
            DevExpress.Xpf.Core.ServerMode.ServerModeExceptionThrownEventArgs args = new DevExpress.Xpf.Core.ServerMode.ServerModeExceptionThrownEventArgs(e.Exception);
            this.RaiseExceptionThrown(args);
        }

        private void ODataServerModeSource_InconsistencyDetected(object sender, DevExpress.Data.ServerModeInconsistencyDetectedEventArgs e)
        {
            DevExpress.Xpf.Core.ServerMode.ServerModeInconsistencyDetectedEventArgs args = new DevExpress.Xpf.Core.ServerMode.ServerModeInconsistencyDetectedEventArgs();
            this.RaiseInconsistencyDetected(args);
        }

        private static void OnDefaultSortingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ODataServerModeSource data = ((ODataServerModeDataSource) d).Data as ODataServerModeSource;
            if (data != null)
            {
                data.DefaultSorting = (string) e.NewValue;
            }
        }

        private static void OnElementTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ODataServerModeDataSource) d).ResetODataServerModeSource();
        }

        private static void OnFixedFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ODataServerModeSource data = ((ODataServerModeDataSource) d).Data as ODataServerModeSource;
            if (data != null)
            {
                data.FixedFilterString = (string) e.NewValue;
            }
        }

        private static void OnKeyExpressionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ODataServerModeDataSource) d).ResetODataServerModeSource();
        }

        private static void OnKeyExpressionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ODataServerModeDataSource) d).KeyExpression = e.NewValue as string;
        }

        private static void OnQueryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ODataServerModeDataSource) d).ResetODataServerModeSource();
        }

        private static void OnServiceRootChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ODataServerModeDataSource source = (ODataServerModeDataSource) d;
            if (source.Strategy is WcfDataSourceStrategyBase)
            {
                ((WcfDataSourceStrategyBase) source.Strategy).Update(source.ServiceRoot);
            }
            source.UpdateData();
        }

        protected virtual void RaiseExceptionThrown(DevExpress.Xpf.Core.ServerMode.ServerModeExceptionThrownEventArgs args)
        {
            args.RoutedEvent = ExceptionThrownEvent;
            base.RaiseEvent(args);
        }

        protected virtual void RaiseInconsistencyDetected(DevExpress.Xpf.Core.ServerMode.ServerModeInconsistencyDetectedEventArgs args)
        {
            args.RoutedEvent = InconsistencyDetectedEvent;
            base.RaiseEvent(args);
        }

        public void Reload()
        {
            if (base.Data != null)
            {
                ((ODataServerModeSource) base.Data).Reload();
            }
        }

        private void ResetODataServerModeSource()
        {
            if (base.Data != null)
            {
                ODataServerModeSource data = (ODataServerModeSource) base.Data;
                data.Dispose();
                data.ExceptionThrown -= new EventHandler<DevExpress.Data.ServerModeExceptionThrownEventArgs>(this.ODataServerModeSource_ExceptionThrown);
                data.InconsistencyDetected -= new EventHandler<DevExpress.Data.ServerModeInconsistencyDetectedEventArgs>(this.ODataServerModeSource_InconsistencyDetected);
            }
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                base.Data = null;
            }
            else
            {
                if (string.IsNullOrEmpty(this.KeyExpression))
                {
                    this.KeyExpression = CreateKeyExpressionString(ODataSourceHelper.GetKeyExpressionsFromQuery(this.Query));
                }
                ODataServerModeSource source2 = new ODataServerModeSource {
                    DefaultSorting = this.DefaultSorting,
                    ElementType = this.ElementType,
                    KeyExpressions = CreateKeyExpressionArray(this.KeyExpression),
                    Query = this.Query,
                    FixedFilterString = this.FixedFilter
                };
                source2.ExceptionThrown += new EventHandler<DevExpress.Data.ServerModeExceptionThrownEventArgs>(this.ODataServerModeSource_ExceptionThrown);
                source2.InconsistencyDetected += new EventHandler<DevExpress.Data.ServerModeInconsistencyDetectedEventArgs>(this.ODataServerModeSource_InconsistencyDetected);
                base.Data = source2;
            }
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
        public Type ElementType
        {
            get => 
                (Type) base.GetValue(ElementTypeProperty);
            set => 
                base.SetValue(ElementTypeProperty, value);
        }

        [Category("Data"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string KeyExpressions
        {
            get => 
                (string) base.GetValue(KeyExpressionsProperty);
            set => 
                base.SetValue(KeyExpressionsProperty, value);
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

        [Category("Data")]
        public object DataServiceContext
        {
            get => 
                base.GetValue(DataServiceContextProperty);
            set => 
                base.SetValue(DataServiceContextProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ODataServerModeDataSource.<>c <>9 = new ODataServerModeDataSource.<>c();

            internal void <.cctor>b__18_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ODataServerModeDataSource) d).ResetODataServerModeSource();
            }
        }
    }
}

