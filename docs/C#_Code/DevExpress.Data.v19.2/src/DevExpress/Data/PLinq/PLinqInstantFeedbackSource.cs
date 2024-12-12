namespace DevExpress.Data.PLinq
{
    using DevExpress.Data;
    using DevExpress.Data.Async.Helpers;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.PLinq.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;

    [ToolboxBitmap(typeof(ResFinder), "Bitmaps256.PLinqInstantFeedbackSource.bmp"), DXToolboxItem(true), ToolboxTabName("DX.19.2: Data & Analytics"), Description("A data source that binds controls to any enumerable source in Instant Feedback Mode."), DefaultEvent("GetEnumerable"), Designer("DevExpress.Design.PLinqInstantFeedbackSourceDesigner, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a")]
    public class PLinqInstantFeedbackSource : Component, IListSource, IDXCloneable
    {
        private Type _ElementType;
        private string _DefaultSorting;
        private int? _DegreeOfParallelism;
        private AsyncListServer2DatacontrollerProxy _AsyncListServer;
        private AsyncListDesignTimeWrapper _DTWrapper;
        private IList _List;
        private bool? _isDesignMode;
        private bool IsDisposed;

        public event EventHandler<GetEnumerableEventArgs> DismissEnumerable;

        public event EventHandler<GetEnumerableEventArgs> GetEnumerable;

        public PLinqInstantFeedbackSource();
        public PLinqInstantFeedbackSource(Action<GetEnumerableEventArgs> getEnumerable);
        public PLinqInstantFeedbackSource(EventHandler<GetEnumerableEventArgs> getEnumerable);
        public PLinqInstantFeedbackSource(Action<GetEnumerableEventArgs> getEnumerable, Action<GetEnumerableEventArgs> freeEnumerable);
        public PLinqInstantFeedbackSource(EventHandler<GetEnumerableEventArgs> getEnumerable, EventHandler<GetEnumerableEventArgs> freeEnumerable);
        private AsyncListDesignTimeWrapper CreateDesignTimeWrapper();
        private AsyncListServer2DatacontrollerProxy CreateRunTimeProxy();
        object IDXCloneable.DXClone();
        protected override void Dispose(bool disposing);
        protected virtual object DXClone();
        protected virtual PLinqInstantFeedbackSource DXCloneCreate();
        private void ForceCatchUp();
        private void getPropertyDescriptors(object sender, GetPropertyDescriptorsEventArgs e);
        private void getTypeInfo(object sender, GetTypeInfoEventArgs e);
        private void getUIRow(object sender, GetUIThreadRowEventArgs e);
        private void getWorkerRowInfo(object sender, GetWorkerThreadRowInfoEventArgs e);
        private void listServerFree(object sender, ListServerGetOrFreeEventArgs e);
        private void listServerGet(object sender, ListServerGetOrFreeEventArgs e);
        public void Refresh();
        IList IListSource.GetList();
        private void TestCanChangeProperties();
        private static EventHandler<T> ToEventHandler<T>(Action<T> action) where T: EventArgs;

        [RefreshProperties(RefreshProperties.All), DefaultValue((string) null), TypeConverter(typeof(PLinqServerModeSourceObjectTypeConverter)), Category("Design"), Description("Specifies the type of objects that will be retrieved from a data source, at design time.")]
        public Type DesignTimeElementType { get; set; }

        [DefaultValue(""), Category("Data"), Description("Specifies how data source contents are sorted by default, when the sort order is not specified by the bound control."), Editor("DevExpress.Design.InstantFeedbackSourceDefaultSortingEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public string DefaultSorting { get; set; }

        [DefaultValue((string) null), Category("Data"), Description("Specifies the maximum number of parallel threads that will be started to process a query.")]
        public int? DegreeOfParallelism { get; set; }

        bool IListSource.ContainsListCollection { get; }
    }
}

