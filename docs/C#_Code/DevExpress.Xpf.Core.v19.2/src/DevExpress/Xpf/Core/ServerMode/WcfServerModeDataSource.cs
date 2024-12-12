namespace DevExpress.Xpf.Core.ServerMode
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.WcfLinq;
    using DevExpress.Data.WcfLinq.Helpers;
    using DevExpress.Xpf.Core.DataSources;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;

    public class WcfServerModeDataSource : ListSourceDataSourceBase, IWcfServerModeDataSource, IWcfDataSource, IDataSource
    {
        public static readonly DependencyProperty DefaultSortingProperty;
        public static readonly DependencyProperty FixedFilterProperty;
        public static readonly DependencyProperty ElementTypeProperty;
        public static readonly DependencyProperty KeyExpressionProperty;
        public static readonly DependencyProperty DataServiceContextProperty;
        public static readonly DependencyProperty QueryProperty;
        public static readonly DependencyProperty ServiceRootProperty;
        public static readonly DependencyProperty UseExtendedDataQueryProperty;
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

        static WcfServerModeDataSource()
        {
            Type ownerType = typeof(WcfServerModeDataSource);
            FixedFilterProperty = DependencyProperty.Register("FixedFilter", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(WcfServerModeDataSource.OnFixedFilterChanged)));
            DefaultSortingProperty = DependencyProperty.Register("DefaultSorting", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(WcfServerModeDataSource.OnDefaultSortingChanged)));
            ElementTypeProperty = DependencyProperty.Register("ElementType", typeof(Type), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(WcfServerModeDataSource.OnElementTypeChanged)));
            KeyExpressionProperty = DependencyProperty.Register("KeyExpression", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(WcfServerModeDataSource.OnKeyExpressionChanged)));
            DataServiceContextProperty = DependencyProperty.Register("DataServiceContext", typeof(object), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(WcfServerModeDataSource.OnDataServiceContextChanged)));
            QueryProperty = DependencyProperty.Register("Query", typeof(IQueryable), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(WcfServerModeDataSource.OnQueryChanged)));
            ServiceRootProperty = DependencyProperty.Register("ServiceRoot", typeof(Uri), ownerType, new PropertyMetadata(new PropertyChangedCallback(WcfServerModeDataSource.OnServiceRootChanged)));
            UseExtendedDataQueryProperty = DependencyProperty.Register("UseExtendedDataQuery", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(WcfServerModeDataSource.OnUseExtendedDataQueryChanged)));
            ExceptionThrownEvent = EventManager.RegisterRoutedEvent("ExceptionThrown", RoutingStrategy.Direct, typeof(ServerModeExceptionThrownEventHandler), ownerType);
            InconsistencyDetectedEvent = EventManager.RegisterRoutedEvent("InconsistencyDetected", RoutingStrategy.Direct, typeof(ServerModeInconsistencyDetectedEventHandler), ownerType);
        }

        public WcfServerModeDataSource()
        {
            this.ResetWcfServerModeSource();
        }

        protected override DataSourceStrategyBase CreateDataSourceStrategy() => 
            new WcfServerModeDataSourceStrategy(this);

        private ExtendedDataResultContainer ExecuteCustomOperation(ExtendedDataParametersContainer parameters)
        {
            if ((this.DataServiceContext == null) || ((this.Query == null) || !this.UseExtendedDataQuery))
            {
                return null;
            }
            string str = ExtendedDataHelper.Serialize<ExtendedDataParametersContainer>(parameters);
            Uri uri = new Uri(WcfDataServiceQueryHelper.ContextGetBaseUri(this.DataServiceContext), $"Get{WcfInstantFeedbackDataSource.GetQueryName(WcfDataServiceQueryHelper.GetRequestUri(this.Query))}{"ExtendedData"}?{"extendedDataInfo"}='{str}'");
            string sourceXml = string.Empty;
            try
            {
                sourceXml = WcfDataServiceQueryHelper.ContextExecute(this.DataServiceContext, uri).FirstOrDefault<string>();
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("Extended data cannot be obtained. The query parameter is too long.", exception);
            }
            return ExtendedDataHelper.Deserialize<ExtendedDataResultContainer>(sourceXml);
        }

        private void Extender_CustomFetchKeys(object sender, CustomFetchKeysEventArgs e)
        {
            ExtendedDataParametersContainer parameters = new ExtendedDataParametersContainer(CriteriaOperator.ParseList(this.KeyExpression, new object[0]), e.Where, e.Order, e.Skip, e.Take);
            ExtendedDataResultContainer container2 = this.ExecuteCustomOperation(parameters);
            if (container2 != null)
            {
                e.Result = container2.GetKeys();
                e.Handled = true;
            }
        }

        private void Extender_CustomGetCount(object sender, CustomGetCountEventArgs e)
        {
            ExtendedDataParametersContainer parameters = new ExtendedDataParametersContainer(e.Where);
            ExtendedDataResultContainer container2 = this.ExecuteCustomOperation(parameters);
            if (container2 != null)
            {
                e.Result = container2.GetCount();
                e.Handled = true;
            }
        }

        private void Extender_CustomGetUniqueValues(object sender, CustomGetUniqueValuesEventArgs e)
        {
            ExtendedDataParametersContainer parameters = new ExtendedDataParametersContainer(e.Expression, e.MaxCount, e.Where);
            ExtendedDataResultContainer container2 = this.ExecuteCustomOperation(parameters);
            if (container2 != null)
            {
                e.Result = container2.GetUniqueValues();
                e.Handled = true;
            }
        }

        private void Extender_CustomPrepareChildren(object sender, CustomPrepareChildrenEventArgs e)
        {
            ExtendedDataParametersContainer parameters = new ExtendedDataParametersContainer(e.GroupWhere, e.GroupByDescriptor, e.Summaries);
            ExtendedDataResultContainer container2 = this.ExecuteCustomOperation(parameters);
            if (container2 != null)
            {
                e.Result = container2.GetChildren();
                e.Handled = true;
            }
        }

        private void Extender_CustomPrepareTopGroupInfo(object sender, CustomPrepareTopGroupInfoEventArgs e)
        {
            ExtendedDataParametersContainer parameters = new ExtendedDataParametersContainer(e.Where, e.Summaries);
            ExtendedDataResultContainer container2 = this.ExecuteCustomOperation(parameters);
            if (container2 != null)
            {
                e.Result = container2.GetTopGroupInfo();
                e.Handled = true;
            }
        }

        protected override string GetDesignTimeImageName() => 
            "DevExpress.Xpf.Core.Core.Images.WcfServerModeDataSource.png";

        private static void OnDataServiceContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WcfServerModeDataSource) d).ResetWcfServerModeSource();
        }

        private static void OnDefaultSortingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WcfServerModeSource data = ((WcfServerModeDataSource) d).Data as WcfServerModeSource;
            if (data != null)
            {
                data.DefaultSorting = (string) e.NewValue;
            }
        }

        private static void OnElementTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WcfServerModeSource data = ((WcfServerModeDataSource) d).Data as WcfServerModeSource;
            if (data != null)
            {
                data.ElementType = (Type) e.NewValue;
            }
        }

        private static void OnFixedFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WcfServerModeSource data = ((WcfServerModeDataSource) d).Data as WcfServerModeSource;
            if (data != null)
            {
                data.FixedFilterString = (string) e.NewValue;
            }
        }

        private static void OnKeyExpressionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WcfServerModeSource data = ((WcfServerModeDataSource) d).Data as WcfServerModeSource;
            if (data != null)
            {
                data.KeyExpression = (string) e.NewValue;
            }
        }

        private static void OnQueryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WcfServerModeDataSource) d).ResetWcfServerModeSource();
        }

        private static void OnServiceRootChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WcfServerModeDataSource source = (WcfServerModeDataSource) d;
            if (source.Strategy is WcfDataSourceStrategyBase)
            {
                ((WcfDataSourceStrategyBase) source.Strategy).Update(source.ServiceRoot);
            }
            source.UpdateData();
        }

        private static void OnUseExtendedDataQueryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WcfServerModeDataSource) d).ResetWcfServerModeSource();
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
                ((WcfServerModeSource) base.Data).Reload();
            }
        }

        private void ResetWcfServerModeSource()
        {
            if (base.Data != null)
            {
                WcfServerModeSource data = (WcfServerModeSource) base.Data;
                data.Dispose();
                data.Extender.CustomFetchKeys -= new EventHandler<CustomFetchKeysEventArgs>(this.Extender_CustomFetchKeys);
                data.Extender.CustomGetCount -= new EventHandler<CustomGetCountEventArgs>(this.Extender_CustomGetCount);
                data.Extender.CustomGetUniqueValues -= new EventHandler<CustomGetUniqueValuesEventArgs>(this.Extender_CustomGetUniqueValues);
                data.Extender.CustomPrepareChildren -= new EventHandler<CustomPrepareChildrenEventArgs>(this.Extender_CustomPrepareChildren);
                data.Extender.CustomPrepareTopGroupInfo -= new EventHandler<CustomPrepareTopGroupInfoEventArgs>(this.Extender_CustomPrepareTopGroupInfo);
                data.ExceptionThrown -= new EventHandler<DevExpress.Data.ServerModeExceptionThrownEventArgs>(this.wcfServerModeSource_ExceptionThrown);
                data.InconsistencyDetected -= new EventHandler<DevExpress.Data.ServerModeInconsistencyDetectedEventArgs>(this.wcfServerModeSource_InconsistencyDetected);
            }
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                base.Data = null;
            }
            else
            {
                if (string.IsNullOrEmpty(this.KeyExpression))
                {
                    this.KeyExpression = WcfDataSourceHelper.GetKeyExpressionFromQuery(this.Query);
                }
                WcfServerModeSource source2 = new WcfServerModeSource {
                    DefaultSorting = this.DefaultSorting,
                    ElementType = this.ElementType,
                    KeyExpression = this.KeyExpression,
                    Query = this.Query,
                    FixedFilterString = this.FixedFilter
                };
                source2.Extender.CustomFetchKeys += new EventHandler<CustomFetchKeysEventArgs>(this.Extender_CustomFetchKeys);
                source2.Extender.CustomGetCount += new EventHandler<CustomGetCountEventArgs>(this.Extender_CustomGetCount);
                source2.Extender.CustomGetUniqueValues += new EventHandler<CustomGetUniqueValuesEventArgs>(this.Extender_CustomGetUniqueValues);
                source2.Extender.CustomPrepareChildren += new EventHandler<CustomPrepareChildrenEventArgs>(this.Extender_CustomPrepareChildren);
                source2.Extender.CustomPrepareTopGroupInfo += new EventHandler<CustomPrepareTopGroupInfoEventArgs>(this.Extender_CustomPrepareTopGroupInfo);
                source2.ExceptionThrown += new EventHandler<DevExpress.Data.ServerModeExceptionThrownEventArgs>(this.wcfServerModeSource_ExceptionThrown);
                source2.InconsistencyDetected += new EventHandler<DevExpress.Data.ServerModeInconsistencyDetectedEventArgs>(this.wcfServerModeSource_InconsistencyDetected);
                base.Data = source2;
            }
        }

        private void wcfServerModeSource_ExceptionThrown(object sender, DevExpress.Data.ServerModeExceptionThrownEventArgs e)
        {
            DevExpress.Xpf.Core.ServerMode.ServerModeExceptionThrownEventArgs args = new DevExpress.Xpf.Core.ServerMode.ServerModeExceptionThrownEventArgs(e.Exception);
            this.RaiseExceptionThrown(args);
        }

        private void wcfServerModeSource_InconsistencyDetected(object sender, DevExpress.Data.ServerModeInconsistencyDetectedEventArgs e)
        {
            DevExpress.Xpf.Core.ServerMode.ServerModeInconsistencyDetectedEventArgs args = new DevExpress.Xpf.Core.ServerMode.ServerModeInconsistencyDetectedEventArgs();
            this.RaiseInconsistencyDetected(args);
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
        public bool UseExtendedDataQuery
        {
            get => 
                (bool) base.GetValue(UseExtendedDataQueryProperty);
            set => 
                base.SetValue(UseExtendedDataQueryProperty, value);
        }

        [Category("Data")]
        public Uri ServiceRoot
        {
            get => 
                (Uri) base.GetValue(ServiceRootProperty);
            set => 
                base.SetValue(ServiceRootProperty, value);
        }
    }
}

