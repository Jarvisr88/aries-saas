namespace DevExpress.Data.ODataLinq
{
    using DevExpress.Data;
    using DevExpress.Data.Async.Helpers;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.Linq.Helpers;
    using DevExpress.Data.ODataLinq.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;

    [Designer("DevExpress.Design.ODataServerModeSourceDesigner, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"), ToolboxBitmap(typeof(ResFinder), "Bitmaps256.ODataInstantFeedbackSource.bmp"), DXToolboxItem(true), ToolboxTabName("DX.19.2: Data & Analytics"), Description("A data source that binds controls to a OData V4 Service in Instant Feedback Mode."), DefaultEvent("GetSource")]
    public class ODataInstantFeedbackSource : Component, IListSource, IDXCloneable, IOdata
    {
        private AsyncListServer2DatacontrollerProxy _AsyncListServer;
        private AsyncListDesignTimeWrapper _DTWrapper;
        private IList _List;
        private CriteriaOperator _FixedFilter;
        private string _Properties;
        private string _DefaultSorting;
        private string[] _KeyExpressions;
        private bool _AreSourceRowsThreadSafe;
        private Type _ElementType;
        private bool? _isDesignMode;
        private bool IsDisposed;

        public event EventHandler<GetSourceEventArgs> DismissSource;

        public event EventHandler<GetSourceEventArgs> GetSource;

        public ODataInstantFeedbackSource();
        public ODataInstantFeedbackSource(Action<GetSourceEventArgs> getSource);
        public ODataInstantFeedbackSource(EventHandler<GetSourceEventArgs> getSource);
        public ODataInstantFeedbackSource(Action<GetSourceEventArgs> getSource, Action<GetSourceEventArgs> freeSource);
        public ODataInstantFeedbackSource(EventHandler<GetSourceEventArgs> getSource, EventHandler<GetSourceEventArgs> freeSource);
        private AsyncListDesignTimeWrapper CreateDesignTimeWrapper();
        private AsyncListServer2DatacontrollerProxy CreateRunTimeProxy();
        object IDXCloneable.DXClone();
        protected override void Dispose(bool disposing);
        protected virtual object DXClone();
        protected virtual ODataInstantFeedbackSource DXCloneCreate();
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CriteriaOperator FixedFilterCriteria { get; set; }

        [DefaultValue(""), Category("Data"), Description("Specifies a string representation of an expression used to filter objects on the data store side. This filter is never affected by bound data-aware controls."), Editor("DevExpress.Design.InstantFeedbackSourceCriteriaEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public string FixedFilterString { get; set; }

        [Localizable(false), DefaultValue(""), Category("Data"), Description("Specifies the semicolon-separated list of property names. When the list is not emply, only the listed properties and key fields will be loaded. Otherwise, all properties are loaded."), Editor("DevExpress.Design.ODataServerModeSourcePropertiesEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public string Properties { get; set; }

        [DefaultValue(""), Category("Data"), Description("Specifies how data source contents are sorted by default when the sort order is not specified by the bound control."), Editor("DevExpress.Design.InstantFeedbackSourceDefaultSortingEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public string DefaultSorting { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DefaultValue(""), Category("Data"), Description("Specifies the name of the key property."), Editor("DevExpress.Design.InstantFeedbackSourceKeyExpressionEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public string KeyExpression { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DefaultValue((string) null)]
        public string[] KeyExpressions { get; set; }

        [DefaultValue(false), Category("Data"), Description("Specifies whether elements retrieved by the ODataInstantFeedbackSource's queryable source are thread-safe.")]
        public bool AreSourceRowsThreadSafe { get; set; }

        [RefreshProperties(RefreshProperties.All), DefaultValue((string) null), TypeConverter(typeof(ODataServerModeSourceObjectTypeConverter)), Category("Design"), Description("Specifies the type of objects that will be retrieved from a data source at design time.")]
        public Type DesignTimeElementType { get; set; }

        bool IListSource.ContainsListCollection { get; }
    }
}

