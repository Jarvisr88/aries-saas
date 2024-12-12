namespace DevExpress.Xpf.Core.ServerMode
{
    using DevExpress.Data.ODataLinq;
    using DevExpress.Xpf.Core.ServerMode.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ODataInstantFeedbackDataSource : WcfInstantFeedbackDataSourceBase
    {
        public static readonly DependencyProperty KeyExpressionsProperty;
        public static readonly RoutedEvent GetSourceEvent;
        public static readonly RoutedEvent DismissSourceEvent;

        public event GetODataSourceEventHandler DismissSource
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

        public event GetODataSourceEventHandler GetSource
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

        static ODataInstantFeedbackDataSource()
        {
            Type ownerType = typeof(ODataInstantFeedbackDataSource);
            KeyExpressionsProperty = DependencyPropertyManager.Register("KeyExpressions", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(ODataInstantFeedbackDataSource.OnKeyExpressionsChanged)));
            GetSourceEvent = EventManager.RegisterRoutedEvent("GetSource", RoutingStrategy.Direct, typeof(GetODataSourceEventHandler), ownerType);
            DismissSourceEvent = EventManager.RegisterRoutedEvent("DismissSource", RoutingStrategy.Direct, typeof(GetODataSourceEventHandler), ownerType);
        }

        protected override WcfInstantFeedbackDataSourceWrapper CreateDataSourceWrapper() => 
            new WcfInstantFeedbackDataSourceWrapper(<>c.<>9__8_0 ??= () => new ODataInstantFeedbackSource(), delegate (IListSource x) {
                this.ToSource(x).Refresh();
            }, delegate (IListSource x, bool areSourceRowsThreadSafe) {
                this.ToSource(x).AreSourceRowsThreadSafe = areSourceRowsThreadSafe;
            }, delegate (IListSource x, string defaultSorting) {
                this.ToSource(x).DefaultSorting = defaultSorting;
            }, delegate (IListSource x, string keyExpression) {
                this.ToSource(x).KeyExpressions = ODataServerModeDataSource.CreateKeyExpressionArray(base.KeyExpression);
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

        protected void Data_DismissSource(object sender, GetSourceEventArgs e)
        {
            base.DisposeData(sender);
            GetODataSourceEventArgs args = new GetODataSourceEventArgs();
            this.RaiseDismissSource(args);
            if (args.Handled)
            {
                e.AreSourceRowsThreadSafe = args.AreSourceRowsThreadSafe;
                e.KeyExpressions = args.KeyExpressions;
                e.Query = args.Query;
                e.Tag = args.Tag;
            }
        }

        protected void Data_GetSource(object sender, GetSourceEventArgs e)
        {
            GetODataSourceEventArgs args = new GetODataSourceEventArgs();
            this.RaiseGetSource(args);
            if (args.Handled)
            {
                e.AreSourceRowsThreadSafe = args.AreSourceRowsThreadSafe;
                e.KeyExpressions = args.KeyExpressions;
                base.QueryField = e.Query = args.Query;
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

        private static void OnKeyExpressionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ODataInstantFeedbackDataSource source = (ODataInstantFeedbackDataSource) d;
            source.KeyExpression = source.KeyExpressions;
        }

        protected virtual void RaiseDismissSource(GetODataSourceEventArgs args)
        {
            args.RoutedEvent = DismissSourceEvent;
            base.RaiseEvent(args);
        }

        protected virtual void RaiseGetSource(GetODataSourceEventArgs args)
        {
            args.RoutedEvent = GetSourceEvent;
            base.RaiseEvent(args);
        }

        private ODataInstantFeedbackSource ToSource(IListSource source) => 
            (ODataInstantFeedbackSource) source;

        [EditorBrowsable(EditorBrowsableState.Never), Category("Data"), Browsable(false)]
        public string KeyExpressions
        {
            get => 
                (string) base.GetValue(KeyExpressionsProperty);
            set => 
                base.SetValue(KeyExpressionsProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ODataInstantFeedbackDataSource.<>c <>9 = new ODataInstantFeedbackDataSource.<>c();
            public static Func<IListSource> <>9__8_0;

            internal IListSource <CreateDataSourceWrapper>b__8_0() => 
                new ODataInstantFeedbackSource();
        }
    }
}

