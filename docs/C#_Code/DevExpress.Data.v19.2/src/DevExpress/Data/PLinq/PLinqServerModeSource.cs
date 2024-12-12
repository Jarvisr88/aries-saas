namespace DevExpress.Data.PLinq
{
    using DevExpress.Data;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.PLinq.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [ToolboxBitmap(typeof(ResFinder), "Bitmaps256.PLinqServerModeSource.bmp"), DXToolboxItem(true), ToolboxTabName("DX.19.2: Data & Analytics"), Description("A data source that binds controls to any enumerable source in Server Mode."), Designer("DevExpress.Design.PLinqServerModeSourceDesigner, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a")]
    public class PLinqServerModeSource : Component, IListSource, ISupportInitialize, IPLinqServerModeFrontEndOwner, IDXCloneable
    {
        private PLinqServerModeFrontEnd _List;
        private Type _ElementType;
        private string _DefaultSorting;
        private int? _DegreeOfParallelism;
        private IEnumerable _Source;
        private int _initCount;
        private bool? _IsDesignMode;

        public event EventHandler<ServerModeExceptionThrownEventArgs> ExceptionThrown;

        public event EventHandler<ServerModeInconsistencyDetectedEventArgs> InconsistencyDetected;

        public PLinqServerModeSource();
        private void _List_ExceptionThrown(object sender, ServerModeExceptionThrownEventArgs e);
        private void _List_InconsistencyDetected(object sender, ServerModeInconsistencyDetectedEventArgs e);
        protected virtual PLinqServerModeFrontEnd CreateList();
        object IDXCloneable.DXClone();
        bool IPLinqServerModeFrontEndOwner.IsReadyForTakeOff();
        private void DoPostponedReload(object state);
        protected virtual object DXClone();
        protected virtual PLinqServerModeSource DXCloneCreate();
        private void FailUnderAspOrAnotherNonPostEnvironment();
        private void ForceCatchUp();
        protected virtual bool IsGoodContext(SynchronizationContext context);
        private bool IsInitialized();
        protected virtual void OnExceptionThrown(ServerModeExceptionThrownEventArgs e);
        protected virtual void OnInconsistencyDetected(ServerModeInconsistencyDetectedEventArgs e);
        public void Reload();
        IList IListSource.GetList();
        void ISupportInitialize.BeginInit();
        void ISupportInitialize.EndInit();

        bool IListSource.ContainsListCollection { get; }

        private PLinqServerModeFrontEnd List { get; }

        [Category("Data"), Description("Specifies the type of objects retrieved from a data source."), RefreshProperties(RefreshProperties.All), DefaultValue((string) null), TypeConverter(typeof(PLinqServerModeSourceObjectTypeConverter))]
        public Type ElementType { get; set; }

        [DefaultValue(""), Category("Data"), Description("Specifies how data source contents are sorted by default, when the sort order is not specified by the bound control."), Editor("DevExpress.Design.ServerModeSourceDefaultSortingEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public string DefaultSorting { get; set; }

        [DefaultValue((string) null), Category("Data"), Description("Specifies the maximum number of parallel threads that will be started to process a query.")]
        public int? DegreeOfParallelism { get; set; }

        [Description("Gets or sets the enumerable data source."), Category("Data"), DefaultValue((string) null), RefreshProperties(RefreshProperties.All)]
        public IEnumerable Source { get; set; }

        Type IPLinqServerModeFrontEndOwner.ElementType { get; }

        IEnumerable IPLinqServerModeFrontEndOwner.Source { get; }

        private class PostState
        {
            public bool ShouldFailWithException;
        }
    }
}

