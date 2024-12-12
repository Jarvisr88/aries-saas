namespace DevExpress.Data.Linq
{
    using DevExpress.Data;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.Linq.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [DXToolboxItem(true), ToolboxBitmap(typeof(ResFinder), "Bitmaps256.EntityServerModeSource.bmp"), ToolboxTabName("DX.19.2: Data & Analytics"), Description("A data source that binds controls to Entity Framework model classes in server mode."), Designer("DevExpress.Design.ServerModeSourceDesigner, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a")]
    public class EntityServerModeSource : Component, IListSource, ISupportInitialize, ILinqServerModeFrontEndOwner, IDXCloneable
    {
        private EntityServerModeFrontEnd _List;
        private Type _ElementType;
        private string _KeyExpression;
        private string _DefaultSorting;
        private IQueryable _QueryableSource;
        private int _initCount;
        private bool? _IsDesignMode;

        public event LinqServerModeExceptionThrownEventHandler ExceptionThrown;

        public event LinqServerModeInconsistencyDetectedEventHandler InconsistencyDetected;

        public EntityServerModeSource();
        private void _List_ExceptionThrown(object sender, ServerModeExceptionThrownEventArgs e);
        private void _List_InconsistencyDetected(object sender, ServerModeInconsistencyDetectedEventArgs e);
        protected virtual EntityServerModeFrontEnd CreateList();
        object IDXCloneable.DXClone();
        bool ILinqServerModeFrontEndOwner.IsReadyForTakeOff();
        private void DoPostponedReload(object state);
        protected virtual object DXClone();
        protected virtual EntityServerModeSource DXCloneCreate();
        private void FailUnderAspOrAnotherNonPostEnvironment();
        private void FillKeyExpression();
        private void ForceCatchUp();
        protected virtual bool IsGoodContext(SynchronizationContext context);
        private bool IsInitialized();
        protected virtual void OnExceptionThrown(LinqServerModeExceptionThrownEventArgs e);
        protected virtual void OnInconsistencyDetected(LinqServerModeInconsistencyDetectedEventArgs e);
        public void Reload();
        IList IListSource.GetList();
        void ISupportInitialize.BeginInit();
        void ISupportInitialize.EndInit();

        bool IListSource.ContainsListCollection { get; }

        private EntityServerModeFrontEnd List { get; }

        [Category("Data"), Description("Specifies the type of objects retrieved from a data source."), RefreshProperties(RefreshProperties.All), DefaultValue((string) null), TypeConverter(typeof(LinqServerModeSourceObjectTypeConverter))]
        public Type ElementType { get; set; }

        [Category("Data"), Description("Gets or sets the key expression."), DefaultValue(""), Editor("DevExpress.Design.ServerModeSourceKeyExpressionEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public string KeyExpression { get; set; }

        [DefaultValue(""), Category("Data"), Description("Specifies how data source contents are sorted by default, when the sort order is not specified by the bound control."), Editor("DevExpress.Design.ServerModeSourceDefaultSortingEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public string DefaultSorting { get; set; }

        [Category("Data"), Description("Gets or sets the queryable data source."), DefaultValue((string) null), RefreshProperties(RefreshProperties.All)]
        public IQueryable QueryableSource { get; set; }

        Type ILinqServerModeFrontEndOwner.ElementType { get; }

        IQueryable ILinqServerModeFrontEndOwner.QueryableSource { get; }

        string ILinqServerModeFrontEndOwner.KeyExpression { get; }

        private class PostState
        {
            public bool ShouldFailWithException;
        }
    }
}

