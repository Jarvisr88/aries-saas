namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    [DXToolboxItem(true), ToolboxBitmap(typeof(ResFinder), "Bitmaps256.RealTimeSource.bmp"), Designer("DevExpress.Design.RealtimeSourceDesigner, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"), ToolboxTabName("DX.19.2: Data & Analytics"), Description("A data source that acts as an asynchronous bridge between a data-aware control and rapidly changing data.")]
    public class RealTimeSource : Component, IListSource, IDXCloneable
    {
        private bool ignoreItemEvents;
        private object source;
        private object dataSource;
        private bool _IsDisposed;
        private bool isSuspended;
        private object suspendDataSource;
        private RealTimeSourceCore _RealTimeCore;
        private bool useWeakEventHandler;
        private RealTimeSourceDesignTimeWrapper _DTWrapper;
        private string _DisplayableProperties;
        public static int SendQueueTimeout;
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static bool? FireListChangedResetAfterPropertyDescriptorChanged;

        static RealTimeSource();
        public RealTimeSource();
        public void CatchUp();
        private RealTimeSourceDesignTimeWrapper CreateDesignTimeWrapper();
        private RealTimeSourceCore CreateRuntimeCore();
        object IDXCloneable.DXClone();
        protected override void Dispose(bool disposing);
        protected virtual object DXClone();
        protected virtual RealTimeSource DXCloneCreate();
        private void ForceCatchUp();
        private string GetDefaultDisplayableProperties(object dataSource);
        [IteratorStateMachine(typeof(RealTimeSource.<GetDefProps>d__38))]
        private static IEnumerable<string> GetDefProps(int depthLeft, PropertyDescriptorCollection pdc);
        public static IEnumerable<string> GetDisplayableProperties(object source);
        private static IEnumerable<string> GetDisplayableProperties(object source, int depthOfReferences);
        public IList GetList();
        public TimeSpan GetQueueDelay();
        public void Resume();
        public void Suspend();

        internal RealTimeSourceCore RealTimeCore { get; set; }

        internal RealTimeSourceDesignTimeWrapper DTWrapper { get; set; }

        [AttributeProvider(typeof(IListSource)), DefaultValue((string) null), Category("Data"), RefreshProperties(RefreshProperties.All), Description("Specifies the data source from which the RealTimeSource component retrieves its data.")]
        public object DataSource { get; set; }

        [Editor("DevExpress.Design.RealTimeSourcePropertiesEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", "System.Drawing.Design.UITypeEditor, System.Drawing"), RefreshProperties(RefreshProperties.All), DefaultValue(""), Category("Data"), Description("Specifies a semicolon-separated list of displayable property names.")]
        public string DisplayableProperties { get; set; }

        [DefaultValue(false), Category("Behavior"), Description("Specifies whether to ignore INotifyPropertyChanged.PropertyChanged events of the data source items.")]
        public bool IgnoreItemEvents { get; set; }

        [Browsable(false), DefaultValue(true)]
        public bool UseWeakEventHandler { get; set; }

        bool IListSource.ContainsListCollection { get; }

    }
}

