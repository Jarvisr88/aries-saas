namespace DevExpress.Data.ODataLinq
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.ODataLinq.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [Designer("DevExpress.Design.ODataServerModeSourceDesigner, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"), ToolboxBitmap(typeof(ResFinder), "Bitmaps256.ODataServerModeSource.bmp"), DXToolboxItem(true), ToolboxTabName("DX.19.2: Data & Analytics"), Description("A data source that binds controls to a OData v4 Data Service in Server Mode.")]
    public class ODataServerModeSource : Component, IListSource, ISupportInitialize, IODataServerModeFrontEndOwner, IDXCloneable
    {
        private ODataServerModeFrontEnd _List;
        private IQueryable query;
        private string[] keys;
        private string _DefaultSorting;
        private CriteriaOperator _FixedFilter;
        private string _Properties;
        private ODataServerModeCore internalList;
        public static bool? UseIncludeTotalCountInsteadOfCount;
        private Type elementType;
        private readonly ServerModeCoreExtender extender;
        private bool? _isDesignMode;
        private int _initCount;

        public event EventHandler<ServerModeExceptionThrownEventArgs> ExceptionThrown;

        public event EventHandler<ServerModeInconsistencyDetectedEventArgs> InconsistencyDetected;

        public ODataServerModeSource();
        public ODataServerModeSource(ServerModeCoreExtender extender);
        private void _List_ExceptionThrown(object sender, ServerModeExceptionThrownEventArgs e);
        private void _List_InconsistencyDetected(object sender, ServerModeInconsistencyDetectedEventArgs e);
        protected virtual ODataServerModeCore CreateInternalList();
        protected virtual ODataServerModeFrontEnd CreateList();
        object IDXCloneable.DXClone();
        bool IODataServerModeFrontEndOwner.IsReadyForTakeOff();
        private void DoPostponedReload(object state);
        protected virtual object DXClone();
        protected virtual ODataServerModeSource DXCloneCreate();
        private void FailUnderAspOrAnotherNonPostEnvironment();
        private void FillKeyExpression();
        private void ForceCatchUp();
        protected virtual bool IsGoodContext(SynchronizationContext context);
        private bool IsInitialized();
        protected virtual void OnExceptionThrown(ServerModeExceptionThrownEventArgs e);
        protected virtual void OnInconsistencyDetected(ServerModeInconsistencyDetectedEventArgs e);
        public void Reload();
        IList IListSource.GetList();
        void ISupportInitialize.BeginInit();
        void ISupportInitialize.EndInit();

        [Browsable(false)]
        public ServerModeCoreExtender Extender { get; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CriteriaOperator FixedFilterCriteria { get; set; }

        [DefaultValue(""), Category("Data"), Description("Specifies a string representation of an expression used to filter objects on the data store side. This filter is never affected by bound data-aware controls."), Editor("DevExpress.Design.ServerModeSourceCriteriaEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public string FixedFilterString { get; set; }

        [Localizable(false), DefaultValue(""), Category("Data"), Description("Specifies the semicolon-separated list of property names. When the list is not emply, only the listed properties and key fields will be loaded. Otherwise, all properties are loaded."), Editor("DevExpress.Design.ODataServerModeSourcePropertiesEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public string Properties { get; set; }

        [TypeConverter(typeof(ODataServerModeSourceObjectTypeConverter)), DefaultValue((string) null), Category("Data"), Description("Specifies the type of objects retrieved from a data source.")]
        public Type ElementType { get; set; }

        [RefreshProperties(RefreshProperties.All), DefaultValue((string) null), Category("Data"), Description("Specifies the query request to the OData service.")]
        public IQueryable Query { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DefaultValue(""), Category("Data"), Description("Specifies the name of the key property."), Editor("DevExpress.Design.ServerModeSourceKeyExpressionEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public string KeyExpression { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DefaultValue((string) null)]
        public string[] KeyExpressions { get; set; }

        [DefaultValue(""), Category("Data"), Description("Specifies how data source contents are sorted by default when the sort order is not specified by the bound control."), Editor("DevExpress.Design.ServerModeSourceDefaultSortingEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public string DefaultSorting { get; set; }

        private ODataServerModeFrontEnd List { get; }

        protected ODataServerModeCore InternalList { get; }

        bool IListSource.ContainsListCollection { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ODataServerModeSource.<>c <>9;
            public static Func<string, bool> <>9__65_0;

            static <>c();
            internal bool <CreateInternalList>b__65_0(string k);
        }

        private class PostState
        {
            public bool ShouldFailWithException;
        }
    }
}

