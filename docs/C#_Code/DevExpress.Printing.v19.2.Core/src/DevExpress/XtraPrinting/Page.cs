namespace DevExpress.XtraPrinting
{
    using DevExpress.DocumentView;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;

    [BrickExporter(typeof(PageExporter))]
    public abstract class Page : BrickBase, IEnumerable, IXtraSerializable, IPage, IPageItem, IDisposable
    {
        private static readonly int bitLoaded = BitVector32.CreateMask();
        private static readonly int bitIsDisposed = BitVector32.CreateMask(bitLoaded);
        private static object synch = new object();
        private static long globalID;
        private PageList owner;
        internal const string DefaultPageNumberFormat = "{0}";
        private List<BrickBase> innerBricks;
        private ReadonlyPageData pageData;
        private PageWatermark watermark;
        private int originalIndex;
        private int originalPageCount;
        private float scaleFactor;
        private int index;
        private PageWatermark serializationWatermark;

        protected Page()
        {
            this.innerBricks = new List<BrickBase>();
            this.scaleFactor = 1f;
            this.index = -1;
            this.PageNumberFormat = "{0}";
            this.PageNumberOffset = 1;
            this.PageInfo = DevExpress.XtraPrinting.PageInfo.None;
        }

        protected Page(ReadonlyPageData pageData)
        {
            this.innerBricks = new List<BrickBase>();
            this.scaleFactor = 1f;
            this.index = -1;
            this.pageData = pageData;
            this.PageNumberFormat = "{0}";
            this.PageNumberOffset = 1;
            this.PageInfo = DevExpress.XtraPrinting.PageInfo.None;
            object synch = Page.synch;
            lock (synch)
            {
                this.ID = GetGlobalID();
            }
        }

        public abstract void AddBrick(VisualBrick brick);
        public void AssignWatermark(PageWatermark source)
        {
            if (source == null)
            {
                this.DisposeWatermark();
            }
            else
            {
                this.watermark ??= new PageWatermark();
                this.watermark.CopyFromInternal(source);
            }
        }

        internal void AssignWatermarkReference(PageWatermark watermark)
        {
            this.DisposeWatermark();
            this.watermark = watermark;
        }

        internal static string ConvertPageNumberValueToString(int pageNumberValue, DevExpress.XtraPrinting.PageInfo pageInfo, string pageNumberFormat)
        {
            string str = (pageInfo == DevExpress.XtraPrinting.PageInfo.RomLowNumber) ? PSConvert.ToRomanString(pageNumberValue).ToLower() : ((pageInfo != DevExpress.XtraPrinting.PageInfo.RomHiNumber) ? pageNumberValue.ToString() : PSConvert.ToRomanString(pageNumberValue));
            try
            {
                return string.Format(pageNumberFormat, str);
            }
            catch (FormatException)
            {
                return $"{str}";
            }
        }

        protected override object CreateCollectionItemCore(string propertyName, XtraItemEventArgs e) => 
            (propertyName != "InnerBricks") ? base.CreateCollectionItemCore(propertyName, e) : new CompositeBrick();

        protected internal virtual CompositeBrick CreateWrapperBrick(IList<Brick> innerBricks, PointF offset) => 
            new CompositeBrick(innerBricks, offset);

        RectangleF IPage.ApplyMargins(RectangleF pageRect) => 
            this.PageData.GetMarginRect(pageRect);

        void IPage.Draw(Graphics gr, PointF position)
        {
            this.DrawPage(gr, position);
        }

        void IXtraSerializable.OnEndDeserializing(string restoredVersion)
        {
            if (this.serializationWatermark.Equals(this.watermark))
            {
                this.AssignWatermark(null);
            }
            this.serializationWatermark.Dispose();
            this.IncProgressReflector();
            this.Loaded = true;
        }

        void IXtraSerializable.OnEndSerializing()
        {
            this.IncProgressReflector();
        }

        void IXtraSerializable.OnStartDeserializing(LayoutAllowEventArgs e)
        {
            this.serializationWatermark = new PageWatermark();
            this.AssignWatermark(this.serializationWatermark);
        }

        void IXtraSerializable.OnStartSerializing()
        {
        }

        private void DisposeWatermark()
        {
            if (this.watermark != null)
            {
                this.watermark.Dispose();
                this.watermark = null;
            }
        }

        protected virtual void DrawPage(Graphics gr, PointF position)
        {
            if ((this.Document != null) && (this.Document.PrintingSystem != null))
            {
                using (GdiGraphics graphics = new GdiGraphics(gr, this.Document.PrintingSystem))
                {
                    (this.Document.PrintingSystem.ExportersFactory.GetExporter(this) as PageExporter).DrawPage(graphics, GraphicsUnitConverter.Convert(position, GraphicsUnit.Pixel, graphics.PageUnit));
                }
            }
        }

        public RectangleF GetBrickBounds(Brick brick)
        {
            NestedBrickIterator iterator = new NestedBrickIterator(this.InnerBrickList);
            while (iterator.MoveNext())
            {
                if (ReferenceEquals(iterator.CurrentBrick, brick))
                {
                    return iterator.CurrentBrickRectangle;
                }
            }
            return RectangleF.Empty;
        }

        public BrickEnumerator GetEnumerator() => 
            new BrickEnumerator(this);

        private static long GetGlobalID()
        {
            if (globalID == 0x7fffffffffffffffL)
            {
                throw new InvalidOperationException("Page GlobalID exceeded unsigned 64-bit integer maximum value.");
            }
            globalID += 1L;
            return globalID;
        }

        protected virtual void IncProgressReflector()
        {
            if (this.Document != null)
            {
                ProgressReflector progressReflector = this.Document.ProgressReflector;
                progressReflector.RangeValue++;
            }
        }

        internal void Initialize()
        {
            this.originalIndex = this.Index;
            this.originalPageCount = this.Document.PageCount;
            this.scaleFactor = this.Document.ScaleFactor;
        }

        internal void InvalidateIndex(int newIndex)
        {
            this.index = newIndex;
        }

        internal void PerformLayout(IPrintingSystemContext context)
        {
            foreach (Brick brick in this)
            {
                brick.PerformLayout(context);
            }
        }

        protected override void SetIndexCollectionItemCore(string propertyName, XtraSetItemIndexEventArgs e)
        {
            if (propertyName == "InnerBricks")
            {
                this.innerBricks.Add((BrickBase) e.Item.Value);
            }
            base.SetIndexCollectionItemCore(propertyName, e);
        }

        public void SetOwner(PageList owner, int index)
        {
            this.owner = owner;
            this.index = index;
        }

        protected override bool ShouldSerializeCore(string propertyName) => 
            (propertyName == "OriginalIndex") ? (this.OriginalIndex != this.Index) : ((propertyName == "OriginalPageCount") ? (this.OriginalPageCount != this.Document.PageCount) : base.ShouldSerializeCore(propertyName));

        IEnumerator IEnumerable.GetEnumerator() => 
            this.InnerBricks.GetEnumerator();

        void IDisposable.Dispose()
        {
            this.AssignWatermark(null);
            this.InnerBrickList.Clear();
            this.IsDisposed = true;
        }

        int IPageItem.PageCount =>
            (this.Document != null) ? this.Document.PageCount : -1;

        RectangleF IPage.UsefulPageRectF =>
            this.UsefulPageRectF;

        internal override IList InnerBrickList =>
            this.innerBricks;

        internal bool Loaded
        {
            get => 
                base.flags[bitLoaded];
            private set => 
                base.flags[bitLoaded] = value;
        }

        internal bool IsDisposed
        {
            get => 
                base.flags[bitIsDisposed];
            private set => 
                base.flags[bitIsDisposed] = value;
        }

        internal bool IsFake { get; set; }

        public override bool RightToLeftLayout
        {
            get => 
                false;
            set
            {
                foreach (BrickBase base2 in this.innerBricks)
                {
                    base2.RightToLeftLayout = value;
                }
            }
        }

        internal bool HasRightToLeftLayoutBricks
        {
            get
            {
                bool rightToLeftLayout;
                using (List<BrickBase>.Enumerator enumerator = this.innerBricks.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        rightToLeftLayout = enumerator.Current.RightToLeftLayout;
                    }
                    else
                    {
                        return false;
                    }
                }
                return rightToLeftLayout;
            }
        }

        [Description("Provides access to the inner bricks of Page."), XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, 0, XtraSerializationFlags.None)]
        public virtual List<BrickBase> InnerBricks =>
            this.innerBricks;

        [Description("Gets the size of the current page.")]
        public SizeF PageSize =>
            this.pageData.PageSize;

        [Description("")]
        public System.Drawing.Printing.PaperKind PaperKind =>
            this.PageData.PaperKind;

        internal RectangleF UsefulPageRectF =>
            this.pageData.UsefulPageRectF;

        internal System.Drawing.Printing.Margins Margins =>
            this.pageData.Margins;

        internal ReadonlyPageData PageData
        {
            get => 
                this.pageData;
            set => 
                this.pageData = value;
        }

        [Description("Gets the margins value (measured in three hundredths of an inch) of the current page.")]
        public DevExpress.XtraPrinting.Native.MarginsF MarginsF =>
            this.pageData.MarginsF;

        internal PageList Owner
        {
            get => 
                this.owner;
            set => 
                this.owner = value;
        }

        public long ID { get; set; }

        [Description("Gets the document which contains the page.")]
        public DevExpress.XtraPrinting.Document Document =>
            this.Owner?.Document;

        [Description("Gets the page index within the PageList collection.")]
        public int Index
        {
            get => 
                (this.Owner != null) ? this.index : 0;
            set => 
                this.index = value;
        }

        internal string PageNumberFormat { get; set; }

        internal int PageNumberOffset { get; set; }

        internal DevExpress.XtraPrinting.PageInfo PageInfo { get; set; }

        internal string PageNumber =>
            ConvertPageNumberValueToString(this.Index + this.PageNumberOffset, this.PageInfo, this.PageNumberFormat);

        [EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty]
        public int OriginalIndex
        {
            get => 
                this.originalIndex;
            set => 
                this.originalIndex = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty]
        public int OriginalPageCount
        {
            get => 
                this.originalPageCount;
            set => 
                this.originalPageCount = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty, DefaultValue((float) 1f)]
        public float ScaleFactor
        {
            get => 
                this.scaleFactor;
            set => 
                this.scaleFactor = value;
        }

        [Description("Provides access to the current watermark on this page."), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public PageWatermark Watermark =>
            this.watermark;

        internal PageWatermark ActualWatermark =>
            (this.watermark == null) ? (((this.Document == null) || (this.Document.PrintingSystem == null)) ? null : this.Document.PrintingSystem.Watermark) : this.watermark;
    }
}

