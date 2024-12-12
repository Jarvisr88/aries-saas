namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [DXToolboxItem(true), ToolboxBitmap(typeof(ResFinder), "Bitmaps256.UnboundDataSource.bmp"), Designer("DevExpress.Design.UnboundSourceDesigner, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"), ToolboxTabName("DX.19.2: Data & Analytics"), Description("A data source used to supply and obtain any data to data-aware controls in unbound mode."), DefaultEvent("ValueNeeded")]
    public class UnboundSource : Component, IListSource, ISupportInitialize
    {
        private UnboundSourceCore core;
        private readonly UnboundSourcePropertyCollection props;
        private bool initializing;
        private UnboundSource.InitializationState lastInitializationState;
        private IContainer components;

        public event EventHandler<UnboundSourceListChangedEventArgs> UnboundSourceListChanged;

        public event EventHandler<UnboundSourceListChangedEventArgs> UnboundSourceListChanging;

        public event EventHandler<UnboundSourceValueNeededEventArgs> ValueNeeded;

        public event EventHandler<UnboundSourceValuePushedEventArgs> ValuePushed;

        public UnboundSource();
        public UnboundSource(IContainer container);
        public int Add();
        public void Change(int row, string propertyName = null);
        public void Clear();
        protected virtual UnboundSourceCore CreateCore();
        protected override void Dispose(bool disposing);
        protected UnboundSourceCore GetCore();
        private void InitializeComponent();
        public void InsertAt(int position);
        public void Move(int from, int to);
        internal void Reconfigure(IEnumerable<UnboundSourceCore.PropertyDescriptorDescriptor> newDescriptorsDescriptors, int rowsAfterReconfigure = 0);
        public void RemoveAt(int index);
        public void Reset(int countAfterReset = 0);
        public void SetRowCount(int count);
        IList IListSource.GetList();
        void ISupportInitialize.BeginInit();
        void ISupportInitialize.EndInit();

        bool IListSource.ContainsListCollection { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Data"), Description("Gets the collection of the UnboundSource's properties."), Editor("DevExpress.Design.UnboundSourcePropertiesEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public UnboundSourcePropertyCollection Properties { get; }

        internal bool Initializing { get; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DefaultValue(0)]
        public int Count { get; set; }

        public object this[int rowIndex, string propertyName] { get; }

        private class InitializationState
        {
            public IEnumerable<UnboundSourceCore.PropertyDescriptorDescriptor> Descriptors;
            public int RowsAfterReconfigure;
        }
    }
}

