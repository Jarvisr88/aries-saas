namespace DevExpress.Data.WcfLinq
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.WcfLinq.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [ToolboxBitmap(typeof(ResFinder), "Bitmaps256.WcfServerModeSource.bmp"), DXToolboxItem(true), ToolboxTabName("DX.19.2: Data & Analytics"), Description("A data source that binds controls to a WCF Data Service in Server Mode."), Designer("DevExpress.Design.ServerModeSourceDesigner, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a")]
    public class WcfServerModeSource : Component, IListSource, ISupportInitialize, IWcfServerModeFrontEndOwner, IDXCloneable
    {
        private WcfServerModeFrontEnd _List;
        private IQueryable query;
        private string key;
        private string _DefaultSorting;
        private CriteriaOperator _FixedFilter;
        private WcfServerModeCore internalList;
        public static bool? UseCountInsteadOfIncludeTotalCount;
        private Type elementType;
        private readonly ServerModeCoreExtender extender;
        private bool? _isDesignMode;
        private int _initCount;

        public event EventHandler<ServerModeExceptionThrownEventArgs> ExceptionThrown;

        public event EventHandler<ServerModeInconsistencyDetectedEventArgs> InconsistencyDetected;

        public WcfServerModeSource();
        public WcfServerModeSource(ServerModeCoreExtender extender);
        private void _List_ExceptionThrown(object sender, ServerModeExceptionThrownEventArgs e);
        private void _List_InconsistencyDetected(object sender, ServerModeInconsistencyDetectedEventArgs e);
        protected virtual WcfServerModeCore CreateInternalList();
        protected virtual WcfServerModeFrontEnd CreateList();
        object IDXCloneable.DXClone();
        bool IWcfServerModeFrontEndOwner.IsReadyForTakeOff();
        private void DoPostponedReload(object state);
        protected virtual object DXClone();
        protected virtual WcfServerModeSource DXCloneCreate();
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

        [TypeConverter(typeof(WcfServerModeSourceObjectTypeConverter)), DefaultValue((string) null), Category("Data"), Description("Specifies the type of objects retrieved from a data source.")]
        public Type ElementType { get; set; }

        [DefaultValue((string) null), Category("Data"), Description("Specifies the query request to the WCF data service.")]
        public IQueryable Query { get; set; }

        [DefaultValue(""), Category("Data"), Description("Specifies the key expression."), Editor("DevExpress.Design.ServerModeSourceKeyExpressionEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public string KeyExpression { get; set; }

        [DefaultValue(""), Category("Data"), Description("Specifies how data source contents are sorted by default, when the sort order is not specified by the bound control."), Editor("DevExpress.Design.ServerModeSourceDefaultSortingEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public string DefaultSorting { get; set; }

        private WcfServerModeFrontEnd List { get; }

        protected WcfServerModeCore InternalList { get; }

        bool IListSource.ContainsListCollection { get; }

        private class PostState
        {
            public bool ShouldFailWithException;
        }
    }
}

