namespace DevExpress.Data
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    [ToolboxItem(false), ToolboxBitmap(typeof(ResFinder), "Bitmaps256.RealTimeSource.bmp"), Designer("DevExpress.Utils.Design.ComparativeSourceDesigner, DevExpress.Design.v19.2"), ToolboxTabName("DX.19.2: Data & Analytics")]
    public class ComparativeSource : Component, IListSource
    {
        private ComparativeDataSourceCore comparativeDataSourceCore;
        private object dataSource;
        private bool isDisposed;
        private IList outputSource;

        public event EventHandler<CustomAttributesEventArgs> CustomAttributes;

        public event EventHandler<CustomEqualsAttributesEventArgs> CustomEquals;

        public ComparativeSource();
        private ComparativeDataSourceCore CreateComparativeDataSourceCore();
        protected override void Dispose(bool disposing);
        internal void RaiseCustomAttributes(CustomAttributesEventArgs eventArgs);
        internal void RaiseCustomEquals(CustomEqualsAttributesEventArgs eventArgs);
        IList IListSource.GetList();

        internal ComparativeDataSourceCore ComparativeDataSource { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Collection, false, false, true)]
        public CalculatedColumnCollection CalculatedColumns { get; }

        [AttributeProvider(typeof(IListSource)), DefaultValue((string) null), Category("Data"), RefreshProperties(RefreshProperties.All), Description("")]
        public object DataSource { get; set; }

        [DefaultValue(0)]
        public DevExpress.Data.ShowValues ShowValues { get; set; }

        bool IListSource.ContainsListCollection { get; }
    }
}

