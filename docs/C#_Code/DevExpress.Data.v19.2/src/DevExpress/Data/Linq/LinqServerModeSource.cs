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

    [ToolboxBitmap(typeof(ResFinder), "Bitmaps256.LinqServerModeSource.bmp"), DXToolboxItem(true), ToolboxTabName("DX.19.2: Data & Analytics"), Description("A data source that binds controls to any IQueryable query provider in server mode."), Designer("DevExpress.Design.ServerModeSourceDesigner, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a")]
    public class LinqServerModeSource : Component, IListSource, ISupportInitialize, ILinqServerModeFrontEndOwner, IDXCloneable
    {
        private LinqServerModeFrontEnd _List;
        private Type _ElementType;
        private string _KeyExpression;
        private string _DefaultSorting;
        private IQueryable _QueryableSource;
        private int _initCount;
        private bool? _isDesignMode;

        public event LinqServerModeExceptionThrownEventHandler ExceptionThrown;

        public event LinqServerModeInconsistencyDetectedEventHandler InconsistencyDetected;

        public LinqServerModeSource();
        private void _List_ExceptionThrown(object sender, ServerModeExceptionThrownEventArgs e);
        private void _List_InconsistencyDetected(object sender, ServerModeInconsistencyDetectedEventArgs e);
        protected virtual LinqServerModeFrontEnd CreateList();
        object IDXCloneable.DXClone();
        bool ILinqServerModeFrontEndOwner.IsReadyForTakeOff();
        protected virtual object DXClone();
        protected virtual LinqServerModeSource DXCloneCreate();
        private void FillKeyExpression();
        private void ForceCatchUp();
        private bool IsInitialized();
        protected virtual void OnExceptionThrown(LinqServerModeExceptionThrownEventArgs e);
        protected virtual void OnInconsistencyDetected(LinqServerModeInconsistencyDetectedEventArgs e);
        public void Reload();
        IList IListSource.GetList();
        void ISupportInitialize.BeginInit();
        void ISupportInitialize.EndInit();

        bool IListSource.ContainsListCollection { get; }

        private LinqServerModeFrontEnd List { get; }

        [Category("Data"), Description("Specifies the type of objects retrieved from a data source."), RefreshProperties(RefreshProperties.All), DefaultValue((string) null), TypeConverter(typeof(LinqServerModeSourceObjectTypeConverter))]
        public Type ElementType { get; set; }

        [Description("Gets or sets the key expression."), Category("Data"), DefaultValue(""), Editor("DevExpress.Design.ServerModeSourceKeyExpressionEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public string KeyExpression { get; set; }

        [DefaultValue(""), Category("Data"), Description("Specifies how data source contents are sorted by default, when sort order is not specified by the bound control."), Editor("DevExpress.Design.ServerModeSourceDefaultSortingEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public string DefaultSorting { get; set; }

        [Description("Gets or sets the queryable data source."), Category("Data"), DefaultValue((string) null), RefreshProperties(RefreshProperties.All)]
        public IQueryable QueryableSource { get; set; }

        Type ILinqServerModeFrontEndOwner.ElementType { get; }

        IQueryable ILinqServerModeFrontEndOwner.QueryableSource { get; }

        string ILinqServerModeFrontEndOwner.KeyExpression { get; }
    }
}

