namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using DevExpress.Data.PLinq.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    [DXToolboxItem(true), ToolboxBitmap(typeof(ResFinder), "Bitmaps256.VirtualServerModeSource.bmp"), ToolboxTabName("DX.19.2: Data & Analytics"), Description("A data source that features event-based data operations: async data load, sorting, filtering and infinite scrolling through records (in a bound Windows Forms GridControl)."), DefaultEvent("ConfigurationChanged"), DefaultProperty("RowType"), Designer("DevExpress.Design.VirtualServerModeSourceDesigner, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a")]
    public class VirtualServerModeSource : Component, IListSource, ISupportInitialize
    {
        private VirtualServerModeCore _Core;
        private Type _rowtype;
        private bool forceRefresh;
        private VirtualServerModeConfigurationInfo ConfigurationInfo;
        private object CurrentUserData;
        private CancellationTokenSource RowsCancellation;
        private CancellationTokenSource TotalsCancellation;
        private CancellationTokenSource UniqueValuesCancellation;
        private VirtualServerModeAcquireInnerListEventArgs InnerListData;
        private bool _IsDisposed;
        private int _IsInit;

        private event EventHandler<VirtualServerModeGetUniqueValuesEventArgs> _GetUniqueValues;

        private event EventHandler<VirtualServerModeTotalSummaryEventArgs> _TotalSummary;

        public event EventHandler<VirtualServerModeAcquireInnerListEventArgs> AcquireInnerList;

        public event EventHandler<VirtualServerModeCanPerformColumnServerActionEventArgs> CanPerformColumnServerAction;

        public event EventHandler<VirtualServerModeRowsEventArgs> ConfigurationChanged;

        public event EventHandler<VirtualServerModeGetUniqueValuesEventArgs> GetUniqueValues;

        public event EventHandler<VirtualServerModeRowsEventArgs> MoreRows;

        public event EventHandler<VirtualServerModeTotalSummaryEventArgs> TotalSummary;

        public VirtualServerModeSource();
        public VirtualServerModeSource(IContainer container);
        public VirtualServerModeSource(Type rowType);
        private void core_CancelGetUniqueValuesRequested(object sender, EventArgs e);
        private void core_ConfigurationChanged(object sender, VirtualServerModeConfigurationChangedEventArgs e);
        private void core_GetUniqueValuesRequested(object sender, GetUniqueValuesEventArgs e);
        private void core_MoreRowsRequested(object sender, EventArgs e);
        protected virtual VirtualServerModeCore CreateCore();
        private IBindingList CreateDefaultInnerList();
        private VirtualServerModeAcquireInnerListEventArgs CreateInitialSourceData();
        protected override void Dispose(bool disposing);
        private VirtualServerModeSource.SameConfigAsBeforeDegree IsSameConfigAsBefore(VirtualServerModeConfigurationInfo e);
        private bool IsSameCoreConfigAsBefore(VirtualServerModeConfigurationInfo e);
        private bool IsSameSummaryConfigAsBefore(VirtualServerModeConfigurationInfo e);
        private void ProcessGUVCompleted(Task<object[]> task, CancellationTokenSource closuredCancellation);
        private void ProcessRowsTaskCompleted(Task<VirtualServerModeRowsTaskResult> task, CancellationTokenSource closuredCancellationSource, bool isInit);
        private void ProcessSummaryReadyCore(IDictionary<ServerModeSummaryDescriptor, object> summary, CancellationTokenSource closured);
        private void ProcessTotalTaskCompleted(Task<IDictionary<ServerModeSummaryDescriptor, object>> task, CancellationTokenSource closured);
        protected virtual void RaiseAcquireInnerList(VirtualServerModeAcquireInnerListEventArgs e);
        protected virtual void RaiseCanPerformColumnServerAction(VirtualServerModeCanPerformColumnServerActionEventArgs e);
        protected virtual void RaiseConfigurationChanged(VirtualServerModeRowsEventArgs e);
        protected virtual void RaiseGetUniqueValues(VirtualServerModeGetUniqueValuesEventArgs e);
        protected virtual void RaiseMoreRows(VirtualServerModeRowsEventArgs e);
        protected virtual void RaiseRefreshRequested(EventArgs eventArgs);
        protected virtual void RaiseTotalSummary(VirtualServerModeTotalSummaryEventArgs e);
        public void Refresh();
        private bool RunMainConfigChangedTask();
        private void RunTotalsTask();
        IList IListSource.GetList();
        void ISupportInitialize.BeginInit();
        void ISupportInitialize.EndInit();
        private void ThrowIfDisposed();
        private void UpdateGetUniqueValuesSupported();
        private void UpdateTotalsSupported();

        [RefreshProperties(RefreshProperties.All), DefaultValue((string) null), TypeConverter(typeof(PLinqServerModeSourceObjectTypeConverter)), Category("Data"), Description("Gets or sets the type of object whose public properties identify data-aware control columns.")]
        public Type RowType { get; set; }

        bool IListSource.ContainsListCollection { get; }

        protected VirtualServerModeCore Core { get; }

        protected bool IsInit { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly VirtualServerModeSource.<>c <>9;
            public static Func<object[]> <>9__32_0;
            public static Action<IList> <>9__37_0;
            public static Func<IList, IEnumerable, IList> <>9__37_1;
            public static Func<IList, IEnumerable, IList> <>9__37_2;
            public static Action<IList> <>9__37_3;
            public static Func<IList, IEnumerable, IList> <>9__37_4;

            static <>c();
            internal object[] <core_GetUniqueValuesRequested>b__32_0();
            internal void <CreateInitialSourceData>b__37_0(IList list);
            internal IList <CreateInitialSourceData>b__37_1(IList list, IEnumerable newRows);
            internal IList <CreateInitialSourceData>b__37_2(IList list, IEnumerable newRows);
            internal void <CreateInitialSourceData>b__37_3(IList list);
            internal IList <CreateInitialSourceData>b__37_4(IList list, IEnumerable newRows);
        }

        private abstract class ReadOnlyBindingListCreator : GenericInvoker<Func<IBindingList>, VirtualServerModeSource.ReadOnlyBindingListCreator.Impl<object>>
        {
            protected ReadOnlyBindingListCreator();

            public class Impl<T> : VirtualServerModeSource.ReadOnlyBindingListCreator
            {
                protected override Func<IBindingList> CreateInvoker();

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly VirtualServerModeSource.ReadOnlyBindingListCreator.Impl<T>.<>c <>9;
                    public static Func<IBindingList> <>9__0_0;

                    static <>c();
                    internal IBindingList <CreateInvoker>b__0_0();
                }
            }
        }

        private enum SameConfigAsBeforeDegree
        {
            public const VirtualServerModeSource.SameConfigAsBeforeDegree Same = VirtualServerModeSource.SameConfigAsBeforeDegree.Same;,
            public const VirtualServerModeSource.SameConfigAsBeforeDegree CompletelyDifferent = VirtualServerModeSource.SameConfigAsBeforeDegree.CompletelyDifferent;,
            public const VirtualServerModeSource.SameConfigAsBeforeDegree JustSummaryDifferent = VirtualServerModeSource.SameConfigAsBeforeDegree.JustSummaryDifferent;
        }
    }
}

