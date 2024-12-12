namespace DevExpress.Data.Linq
{
    using DevExpress.Data;
    using DevExpress.Data.Async.Helpers;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.Linq.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;

    [DXToolboxItem(true), ToolboxBitmap(typeof(ResFinder), "Bitmaps256.EntityInstantFeedbackSource.bmp"), ToolboxTabName("DX.19.2: Data & Analytics"), Description("A data source that binds controls to Entity Framework model classes in Instant Feedback Mode."), DefaultEvent("GetQueryable"), Designer("DevExpress.Design.InstantFeedbackSourceDesigner, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a")]
    public class EntityInstantFeedbackSource : Component, IListSource, IDXCloneable
    {
        private Type _ElementType;
        private string _KeyExpression;
        private string _DefaultSorting;
        private bool _AreSourceRowsThreadSafe;
        private AsyncListServer2DatacontrollerProxy _AsyncListServer;
        private AsyncListDesignTimeWrapper _DTWrapper;
        private IList _List;
        private bool? _isDesignMode;
        private bool IsDisposed;

        public event EventHandler<GetQueryableEventArgs> DismissQueryable;

        public event EventHandler<GetQueryableEventArgs> GetQueryable;

        public EntityInstantFeedbackSource();
        public EntityInstantFeedbackSource(Action<GetQueryableEventArgs> getQueryable);
        public EntityInstantFeedbackSource(EventHandler<GetQueryableEventArgs> getQueryable);
        public EntityInstantFeedbackSource(Action<GetQueryableEventArgs> getQueryable, Action<GetQueryableEventArgs> freeQueryable);
        public EntityInstantFeedbackSource(EventHandler<GetQueryableEventArgs> getQueryable, EventHandler<GetQueryableEventArgs> freeQueryable);
        private AsyncListDesignTimeWrapper CreateDesignTimeWrapper();
        private AsyncListServer2DatacontrollerProxy CreateRunTimeProxy();
        object IDXCloneable.DXClone();
        protected override void Dispose(bool disposing);
        protected virtual object DXClone();
        protected virtual EntityInstantFeedbackSource DXCloneCreate();
        private void FillKeyExpression();
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

        [RefreshProperties(RefreshProperties.All), DefaultValue((string) null), TypeConverter(typeof(LinqServerModeSourceObjectTypeConverter)), Category("Design"), Description("Specifies the type of objects that will be retrieved from a data source, at design time.")]
        public Type DesignTimeElementType { get; set; }

        [DefaultValue(""), Category("Data"), Description("Specifies the name of the key property."), Editor("DevExpress.Design.InstantFeedbackSourceKeyExpressionEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public string KeyExpression { get; set; }

        [DefaultValue(""), Category("Data"), Description("Specifies how data source contents are sorted by default, when the sort order is not specified by the bound control."), Editor("DevExpress.Design.InstantFeedbackSourceDefaultSortingEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public string DefaultSorting { get; set; }

        [DefaultValue(false), Category("Data"), Description("Specifies whether elements retrieved by the EntityInstantFeedbackSource's queryable source are thread-safe.")]
        public bool AreSourceRowsThreadSafe { get; set; }

        bool IListSource.ContainsListCollection { get; }
    }
}

