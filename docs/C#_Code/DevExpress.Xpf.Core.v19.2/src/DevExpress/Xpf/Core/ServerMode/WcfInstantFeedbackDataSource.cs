namespace DevExpress.Xpf.Core.ServerMode
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.WcfLinq;
    using DevExpress.Data.WcfLinq.Helpers;
    using DevExpress.Xpf.Core.ServerMode.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class WcfInstantFeedbackDataSource : WcfInstantFeedbackDataSourceBase
    {
        public static readonly DependencyProperty UseExtendedDataQueryProperty;
        public static readonly RoutedEvent GetSourceEvent;
        public static readonly RoutedEvent DismissSourceEvent;
        private volatile bool useExtendedDataQueryField;

        public event GetWcfSourceEventHandler DismissSource
        {
            add
            {
                base.AddHandler(DismissSourceEvent, value);
            }
            remove
            {
                base.RemoveHandler(DismissSourceEvent, value);
            }
        }

        public event GetWcfSourceEventHandler GetSource
        {
            add
            {
                base.AddHandler(GetSourceEvent, value);
            }
            remove
            {
                base.RemoveHandler(GetSourceEvent, value);
            }
        }

        static WcfInstantFeedbackDataSource()
        {
            Type ownerType = typeof(WcfInstantFeedbackDataSource);
            UseExtendedDataQueryProperty = DependencyPropertyManager.Register("UseExtendedDataQuery", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(WcfInstantFeedbackDataSource.OnUseExtendedDataQueryChanged)));
            GetSourceEvent = EventManager.RegisterRoutedEvent("GetSource", RoutingStrategy.Direct, typeof(GetWcfSourceEventHandler), ownerType);
            DismissSourceEvent = EventManager.RegisterRoutedEvent("DismissSource", RoutingStrategy.Direct, typeof(GetWcfSourceEventHandler), ownerType);
        }

        protected override WcfInstantFeedbackDataSourceWrapper CreateDataSourceWrapper() => 
            new WcfInstantFeedbackDataSourceWrapper(delegate {
                if (string.IsNullOrEmpty(base.KeyExpression))
                {
                    base.KeyExpression = WcfDataSourceHelper.GetKeyExpressionFromQuery(base.QueryField);
                }
                return new WcfInstantFeedbackSource();
            }, delegate (IListSource x) {
                this.ToSource(x).Refresh();
            }, delegate (IListSource x, bool areSourceRowsThreadSafe) {
                this.ToSource(x).AreSourceRowsThreadSafe = areSourceRowsThreadSafe;
            }, delegate (IListSource x, string defaultSorting) {
                this.ToSource(x).DefaultSorting = defaultSorting;
            }, delegate (IListSource x, string keyExpression) {
                this.ToSource(x).KeyExpression = base.KeyExpression;
            }, delegate (IListSource x) {
                this.ToSource(x).GetSource += new EventHandler<GetSourceEventArgs>(this.Data_GetSource);
            }, delegate (IListSource x) {
                this.ToSource(x).DismissSource += new EventHandler<GetSourceEventArgs>(this.Data_DismissSource);
            }, delegate (IListSource x) {
                this.ToSource(x).GetSource -= new EventHandler<GetSourceEventArgs>(this.Data_GetSource);
            }, delegate (IListSource x) {
                this.ToSource(x).DismissSource -= new EventHandler<GetSourceEventArgs>(this.Data_DismissSource);
            }, delegate (IListSource x, string fixedFilter) {
                this.ToSource(x).FixedFilterString = fixedFilter;
            });

        private void Data_DismissSource(object sender, GetSourceEventArgs e)
        {
            base.DisposeData(sender);
            GetWcfSourceEventArgs args = new GetWcfSourceEventArgs();
            e.Extender.CustomFetchKeys -= new EventHandler<CustomFetchKeysEventArgs>(this.Extender_CustomFetchKeys);
            e.Extender.CustomGetCount -= new EventHandler<CustomGetCountEventArgs>(this.Extender_CustomGetCount);
            e.Extender.CustomGetUniqueValues -= new EventHandler<CustomGetUniqueValuesEventArgs>(this.Extender_CustomGetUniqueValues);
            e.Extender.CustomPrepareChildren -= new EventHandler<CustomPrepareChildrenEventArgs>(this.Extender_CustomPrepareChildren);
            e.Extender.CustomPrepareTopGroupInfo -= new EventHandler<CustomPrepareTopGroupInfoEventArgs>(this.Extender_CustomPrepareTopGroupInfo);
            this.RaiseDismissSource(args);
            if (args.Handled)
            {
                e.AreSourceRowsThreadSafe = args.AreSourceRowsThreadSafe;
                e.KeyExpression = args.KeyExpression;
                e.Query = args.Query;
                e.Tag = args.Tag;
            }
        }

        private void Data_GetSource(object sender, GetSourceEventArgs e)
        {
            GetWcfSourceEventArgs args = new GetWcfSourceEventArgs();
            e.Extender.CustomFetchKeys += new EventHandler<CustomFetchKeysEventArgs>(this.Extender_CustomFetchKeys);
            e.Extender.CustomGetCount += new EventHandler<CustomGetCountEventArgs>(this.Extender_CustomGetCount);
            e.Extender.CustomGetUniqueValues += new EventHandler<CustomGetUniqueValuesEventArgs>(this.Extender_CustomGetUniqueValues);
            e.Extender.CustomPrepareChildren += new EventHandler<CustomPrepareChildrenEventArgs>(this.Extender_CustomPrepareChildren);
            e.Extender.CustomPrepareTopGroupInfo += new EventHandler<CustomPrepareTopGroupInfoEventArgs>(this.Extender_CustomPrepareTopGroupInfo);
            this.RaiseGetSource(args);
            if (args.Handled)
            {
                e.AreSourceRowsThreadSafe = args.AreSourceRowsThreadSafe;
                e.KeyExpression = args.KeyExpression;
                base.KeyExpressionField = args.KeyExpression;
                e.Query = args.Query;
                base.QueryField = args.Query;
                e.Tag = args.Tag;
            }
            else
            {
                IQueryable actualQueryableSource = base.GetActualQueryableSource();
                if (actualQueryableSource != null)
                {
                    e.Query = actualQueryableSource;
                }
            }
        }

        private ExtendedDataResultContainer ExecuteCustomOperation(ExtendedDataParametersContainer parameters)
        {
            if (!this.useExtendedDataQueryField)
            {
                return null;
            }
            if ((base.ContextField == null) || (base.QueryField == null))
            {
                throw new InvalidOperationException("DataServiceContext and Query must be specified to use extended data queries.");
            }
            string absoluteUri = WcfDataServiceQueryHelper.ContextGetBaseUri(base.ContextField).AbsoluteUri;
            if (!absoluteUri.EndsWith("/"))
            {
                absoluteUri = absoluteUri + "/";
            }
            string str2 = ExtendedDataHelper.Serialize<ExtendedDataParametersContainer>(parameters);
            Uri uri = new Uri($"{absoluteUri}Get{GetQueryName(WcfDataServiceQueryHelper.GetRequestUri(base.QueryField))}{"ExtendedData"}?{"extendedDataInfo"}='{str2}'");
            return ExtendedDataHelper.Deserialize<ExtendedDataResultContainer>(WcfDataServiceQueryHelper.ContextExecute(base.ContextField, uri).FirstOrDefault<string>());
        }

        private void Extender_CustomFetchKeys(object sender, CustomFetchKeysEventArgs e)
        {
            ExtendedDataParametersContainer parameters = new ExtendedDataParametersContainer(CriteriaOperator.ParseList(base.KeyExpressionField, new object[0]), e.Where, e.Order, e.Skip, e.Take);
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
            "DevExpress.Xpf.Core.Core.Images.WcfInstantFeedbackDataSource.png";

        internal static string GetQueryName(Uri requestUri)
        {
            char[] separator = new char[] { '/' };
            string str = requestUri.AbsolutePath.Split(separator).Last<string>();
            if (str.EndsWith("()"))
            {
                str = str.Substring(0, str.Length - "()".Length);
            }
            return str;
        }

        private static void OnUseExtendedDataQueryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WcfInstantFeedbackDataSource source = (WcfInstantFeedbackDataSource) d;
            source.useExtendedDataQueryField = source.UseExtendedDataQuery;
            source.ResetInstantFeedbackSource();
        }

        protected virtual void RaiseDismissSource(GetWcfSourceEventArgs args)
        {
            args.RoutedEvent = DismissSourceEvent;
            base.RaiseEvent(args);
        }

        protected virtual void RaiseGetSource(GetWcfSourceEventArgs args)
        {
            args.RoutedEvent = GetSourceEvent;
            base.RaiseEvent(args);
        }

        private WcfInstantFeedbackSource ToSource(IListSource source) => 
            (WcfInstantFeedbackSource) source;

        [Category("Data")]
        public bool UseExtendedDataQuery
        {
            get => 
                (bool) base.GetValue(UseExtendedDataQueryProperty);
            set => 
                base.SetValue(UseExtendedDataQueryProperty, value);
        }
    }
}

