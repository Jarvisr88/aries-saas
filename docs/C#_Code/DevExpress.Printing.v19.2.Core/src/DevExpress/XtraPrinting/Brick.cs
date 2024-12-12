namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [BrickExporter(typeof(BrickExporter))]
    public class Brick : BrickBase, IBaseBrick, IBrick, IDisposable, ITableCell, IEnumerable
    {
        protected PointF pageBuilderOffset;
        private AttachedPropertyValue[] fBrickData;

        public Brick()
        {
            this.pageBuilderOffset = PointF.Empty;
            this.InitializeBrickProperties();
        }

        internal Brick(Brick brick) : base(brick)
        {
            this.pageBuilderOffset = PointF.Empty;
            this.AssignBrickData(brick.fBrickData);
            this.InitializeBrickProperties();
        }

        private void AssignBrickData(AttachedPropertyValue[] source)
        {
            if ((source != null) && (source.Length != 0))
            {
                this.fBrickData = new AttachedPropertyValue[source.Length];
                Array.Copy(source, this.fBrickData, source.Length);
            }
        }

        Hashtable IBrick.GetProperties()
        {
            Hashtable ht = new Hashtable();
            Accessor.GetProperties(this, ht);
            return ht;
        }

        void IBrick.SetProperties(Hashtable properties)
        {
            Accessor.SetProperties(this, properties);
        }

        void IBrick.SetProperties(object[,] properties)
        {
            Accessor.SetProperties(this, properties);
        }

        public virtual void Dispose()
        {
            this.fBrickData = null;
        }

        internal string GetActualUrl()
        {
            string url = this.Url;
            return (!string.Equals(this.Url, "empty", StringComparison.OrdinalIgnoreCase) ? url : string.Empty);
        }

        protected int GetDataIndex(int propIndex)
        {
            if (this.fBrickData != null)
            {
                for (int i = 0; i < this.fBrickData.Length; i++)
                {
                    if (propIndex == this.fBrickData[i].PropertyIndex)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        protected T GetDataValue<T>(int index) => 
            (T) this.fBrickData[index].Value;

        public virtual IEnumerator GetEnumerator() => 
            this.Bricks.GetEnumerator();

        internal virtual Brick GetRealBrick() => 
            this;

        protected internal virtual string GetUrlByPoint(Point point) => 
            this.Url;

        protected T GetValue<T>(AttachedProperty<T> prop, T defaultValue)
        {
            int dataIndex = this.GetDataIndex(prop.Index);
            return ((dataIndex >= 0) ? this.GetDataValue<T>(dataIndex) : defaultValue);
        }

        protected internal override RectangleF GetViewRectangle() => 
            this.Rect;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Initialize(PrintingSystemBase ps, RectangleF rect)
        {
            this.Initialize(ps, rect, true);
        }

        internal void Initialize(PrintingSystemBase ps, RectangleF rect, bool cacheStyle)
        {
            this.InitialRect = rect;
            this.PrintingSystem = ps;
            this.OnSetPrintingSystem(cacheStyle);
        }

        protected virtual void InitializeBrickProperties()
        {
            this.IsVisible = true;
            this.CanAddToPage = true;
            this.CanMultiColumn = true;
        }

        protected internal virtual void OnAfterMerge()
        {
        }

        protected virtual void OnSetPrintingSystem(bool cacheStyle)
        {
        }

        protected internal virtual void PerformLayout(IPrintingSystemContext context)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void RemoveAttachedValue<T>(AttachedProperty<T> prop)
        {
            int dataIndex = this.GetDataIndex(prop.Index);
            if (dataIndex >= 0)
            {
                this.RemoveAttachedValue(dataIndex);
            }
        }

        private void RemoveAttachedValue(int index)
        {
            if (this.fBrickData.Length == 1)
            {
                this.fBrickData = null;
            }
            else
            {
                AttachedPropertyValue[] destinationArray = new AttachedPropertyValue[this.fBrickData.Length - 1];
                Array.Copy(this.fBrickData, 0, destinationArray, 0, index);
                Array.Copy(this.fBrickData, index + 1, destinationArray, index, (this.fBrickData.Length - index) - 1);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the SafeGetAttachedValue(AttachedProperty prop, object defaultValue) method instead")]
        public T SafeGetAttachedValue<T>(string propertyName)
        {
            throw new NotSupportedException();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public T SafeGetAttachedValue<T>(AttachedProperty<T> prop, T defaultValue)
        {
            int dataIndex = this.GetDataIndex(prop.Index);
            return ((dataIndex >= 0) ? this.GetDataValue<T>(dataIndex) : defaultValue);
        }

        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the SafeGetAttachedValue(AttachedProperty prop, object defaultValue) method instead")]
        public object SafeGetAttachedValue(string propertyName, object defaultValue)
        {
            throw new NotSupportedException();
        }

        protected internal virtual void Scale(double scaleFactor)
        {
            base.Rect = MathMethods.Scale(base.Rect, scaleFactor);
        }

        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public void SetAttachedValue<T>(AttachedProperty<T> prop, T value)
        {
            AttachedPropertyValue value2;
            if (this.fBrickData == null)
            {
                value2 = new AttachedPropertyValue(prop.Index) {
                    Value = value
                };
                this.fBrickData = new AttachedPropertyValue[] { value2 };
            }
            else
            {
                int dataIndex = this.GetDataIndex(prop.Index);
                if (dataIndex >= 0)
                {
                    this.fBrickData[dataIndex].Value = value;
                }
                else
                {
                    AttachedPropertyValue[] destinationArray = new AttachedPropertyValue[this.fBrickData.Length + 1];
                    Array.Copy(this.fBrickData, destinationArray, this.fBrickData.Length);
                    value2 = new AttachedPropertyValue(prop.Index) {
                        Value = value
                    };
                    destinationArray[destinationArray.Length - 1] = value2;
                    this.fBrickData = destinationArray;
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the SetAttachedValue(AttachedProperty prop, object value) method instead")]
        public void SetAttachedValue(string name, object value)
        {
            throw new NotSupportedException();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetAttachedValue<T>(AttachedProperty<T> prop, T value, T defaultValue)
        {
            if (!Equals(value, defaultValue))
            {
                this.SetAttachedValue<T>(prop, value);
            }
            else if ((this.fBrickData != null) && (this.fBrickData.Length != 0))
            {
                int dataIndex = this.GetDataIndex(prop.Index);
                if (dataIndex >= 0)
                {
                    this.RemoveAttachedValue(dataIndex);
                }
            }
        }

        private void SetSeparability(DevExpress.XtraPrinting.Native.Separability flag, bool val)
        {
            this.Separability = val ? flag : DevExpress.XtraPrinting.Native.Separability.None;
        }

        protected override bool ShouldSerializeCore(string propertyName) => 
            (propertyName != "Value") ? base.ShouldSerializeCore(propertyName) : (((this.Value is string) || ((this.Value != null) && this.Value.GetType().IsValueType)) ? base.ShouldSerializeCore(propertyName) : false);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool TryExtractAttachedValue<T>(AttachedProperty<T> prop, out T value)
        {
            int dataIndex = this.GetDataIndex(prop.Index);
            if (dataIndex < 0)
            {
                value = default(T);
                return false;
            }
            value = this.GetDataValue<T>(dataIndex);
            this.RemoveAttachedValue(dataIndex);
            return true;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool TryGetAttachedValue<T>(AttachedProperty<T> prop, out T value)
        {
            int dataIndex = this.GetDataIndex(prop.Index);
            if (dataIndex >= 0)
            {
                value = this.GetDataValue<T>(dataIndex);
                return true;
            }
            value = default(T);
            return false;
        }

        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the TryGetAttachedValue(AttachedProperty prop, out object value) method instead")]
        public bool TryGetAttachedValue(string name, out object value)
        {
            throw new NotSupportedException();
        }

        public virtual float ValidatePageBottom(RectangleF pageBounds, bool enforceSplitNonSeparable, RectangleF rect, IPrintingSystemContext context) => 
            (this.SeparableVert || ((pageBounds.Height < rect.Height) & enforceSplitNonSeparable)) ? this.ValidatePageBottomInternal(pageBounds.Bottom, rect, context) : rect.Top;

        protected internal virtual float ValidatePageBottomInternal(float pageBottom, RectangleF rect, IPrintingSystemContext context) => 
            rect.Top;

        public virtual float ValidatePageRight(float pageRight, RectangleF rect) => 
            rect.Left;

        internal PointF PageBuilderOffset
        {
            get => 
                this.pageBuilderOffset;
            set => 
                this.pageBuilderOffset = value;
        }

        internal override IList InnerBrickList =>
            this.Bricks;

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected internal virtual bool AllowHitTest =>
            true;

        [Description("Gets or sets the Printing System used to create and print this brick.")]
        public virtual PrintingSystemBase PrintingSystem
        {
            get => 
                null;
            set
            {
            }
        }

        [Description("Gets or sets an anchor name assigned to the Brick object."), XtraSerializableProperty, DefaultValue("")]
        public string AnchorName
        {
            get => 
                this.GetValue<string>(BrickAttachedProperties.AnchorName, string.Empty);
            set
            {
                string text1 = value;
                if (value == null)
                {
                    string local1 = value;
                    text1 = string.Empty;
                }
                this.SetAttachedValue<string>(BrickAttachedProperties.AnchorName, text1, string.Empty);
            }
        }

        [Description("Gets or sets the target attribute assigned to this Brick and used when a Brick.Url property is specified."), XtraSerializableProperty, DefaultValue("")]
        public string Target
        {
            get => 
                this.GetValue<string>(BrickAttachedProperties.Target, string.Empty);
            set
            {
                string text1 = value;
                if (value == null)
                {
                    string local1 = value;
                    text1 = string.Empty;
                }
                this.SetAttachedValue<string>(BrickAttachedProperties.Target, text1, string.Empty);
            }
        }

        [Description("Specifies the link to an external resource."), XtraSerializableProperty, DefaultValue("")]
        public string Url
        {
            get => 
                this.GetValue<string>(BrickAttachedProperties.Url, string.Empty);
            set
            {
                string text1 = value;
                if (value == null)
                {
                    string local1 = value;
                    text1 = string.Empty;
                }
                this.SetAttachedValue<string>(BrickAttachedProperties.Url, text1, string.Empty);
            }
        }

        public bool HasCrossReference =>
            !string.IsNullOrEmpty(this.GetActualUrl()) && (this.Target == "_self");

        [Description("Defines the text displayed as the current brick hint."), XtraSerializableProperty, DefaultValue("")]
        public virtual string Hint
        {
            get => 
                this.GetValue<string>(BrickAttachedProperties.Hint, string.Empty);
            set
            {
                string text1 = value;
                if (value == null)
                {
                    string local1 = value;
                    text1 = string.Empty;
                }
                this.SetAttachedValue<string>(BrickAttachedProperties.Hint, text1, string.Empty);
            }
        }

        [Description("Identifies the current brick."), XtraSerializableProperty, DefaultValue("")]
        public string ID
        {
            get => 
                this.GetValue<string>(BrickAttachedProperties.Id, string.Empty);
            set
            {
                string text1 = value;
                if (value == null)
                {
                    string local1 = value;
                    text1 = string.Empty;
                }
                this.SetAttachedValue<string>(BrickAttachedProperties.Id, text1, string.Empty);
            }
        }

        [Description("Gets or sets an object, containing additional information on the current brick."), XtraSerializableProperty, DefaultValue("")]
        public object Value
        {
            get => 
                this.GetValue<object>(BrickAttachedProperties.Value, string.Empty);
            set => 
                this.SetAttachedValue<object>(BrickAttachedProperties.Value, value, string.Empty);
        }

        [DXHelpExclude(true), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty, DefaultValue((string) null)]
        public object DrillDownKey
        {
            get => 
                this.GetValue<object>(BrickAttachedProperties.DrillDownKey, null);
            set => 
                this.SetAttachedValue<object>(BrickAttachedProperties.DrillDownKey, value, null);
        }

        [Description("Defines the current brick's location and size, in GraphicsUnit.Document measurement units.")]
        public override RectangleF Rect
        {
            get => 
                RectFBase.Offset(base.Rect, this.pageBuilderOffset.X, this.pageBuilderOffset.Y);
            set
            {
                if (!this.IsInitialized)
                {
                    base.Rect = RectFBase.Offset(value, -this.pageBuilderOffset.X, -this.pageBuilderOffset.Y);
                }
            }
        }

        internal virtual RectangleF DocumentBandRect =>
            this.InitialRect;

        [Description("Override this property to get or set the setting specifying whether the brick can be split horizontally on repagination.")]
        public virtual bool SeparableHorz
        {
            get => 
                (this.Separability & (DevExpress.XtraPrinting.Native.Separability.HorzForced | DevExpress.XtraPrinting.Native.Separability.Horz)) > DevExpress.XtraPrinting.Native.Separability.None;
            set
            {
                if (this.CanBeSeparabled)
                {
                    this.SetSeparability(DevExpress.XtraPrinting.Native.Separability.Horz, value);
                }
            }
        }

        [Description("Override this property to get or set the setting specifying whether the brick can be split vertically on repagination.")]
        public virtual bool SeparableVert
        {
            get => 
                (this.Separability & (DevExpress.XtraPrinting.Native.Separability.VertForced | DevExpress.XtraPrinting.Native.Separability.Vert)) > DevExpress.XtraPrinting.Native.Separability.None;
            set
            {
                if (this.CanBeSeparabled)
                {
                    this.SetSeparability(DevExpress.XtraPrinting.Native.Separability.Vert, value);
                }
            }
        }

        [Description("Override this property to specify whether the current brick can be divided into multiple parts when a document is repaginated.")]
        public virtual bool Separable
        {
            get => 
                (this.Separability & (DevExpress.XtraPrinting.Native.Separability.VertHorzForced | DevExpress.XtraPrinting.Native.Separability.VertHorz)) > DevExpress.XtraPrinting.Native.Separability.None;
            set
            {
                if (this.CanBeSeparabled)
                {
                    this.SetSeparability(DevExpress.XtraPrinting.Native.Separability.VertHorz, value);
                }
            }
        }

        protected internal bool IsInitialized =>
            this.PrintingSystem != null;

        [Description("Gets or sets the parent document band for the current brick."), Obsolete]
        public DocumentBand Parent
        {
            get => 
                null;
            set
            {
            }
        }

        [XtraSerializableProperty, DefaultValue(true)]
        public bool IsVisible
        {
            get => 
                base.flags[BrickBase.bitIsVisible];
            set => 
                base.flags[BrickBase.bitIsVisible] = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual BrickCollectionBase Bricks =>
            EmptyBrickCollection.Instance;

        bool ITableCell.ShouldApplyPadding =>
            this.ShouldApplyPaddingInternal;

        string ITableCell.FormatString =>
            null;

        string ITableCell.XlsxFormatString =>
            null;

        object ITableCell.TextValue =>
            null;

        DefaultBoolean ITableCell.XlsExportNativeFormat =>
            DefaultBoolean.Default;

        string ITableCell.Url =>
            this.GetActualUrl();

        protected internal virtual bool ShouldApplyPaddingInternal =>
            false;

        [Description("Override this property to get the text string, containing the brick type information."), XtraSerializableProperty]
        public virtual string BrickType =>
            ExceptionHelper.ThrowInvalidOperationException<string>();

        protected ImageAlignment ImageAlignmentCore
        {
            get => 
                (ImageAlignment) base.flags[BrickBase.ImageAlignmentSection];
            set => 
                base.flags[BrickBase.ImageAlignmentSection] = (int) value;
        }

        private DevExpress.XtraPrinting.Native.Separability Separability
        {
            get => 
                (DevExpress.XtraPrinting.Native.Separability) base.flags[BrickBase.SeparabilitySection];
            set => 
                base.flags[BrickBase.SeparabilitySection] = (int) value;
        }

        internal bool IsSeparable =>
            this.Separability != DevExpress.XtraPrinting.Native.Separability.None;

        private bool CanBeSeparabled =>
            (base.Modifier & (BrickModifier.Detail | BrickModifier.DetailFooter | BrickModifier.DetailHeader | BrickModifier.ReportFooter | BrickModifier.ReportHeader)) > BrickModifier.None;

        internal bool CanOverflow
        {
            get => 
                base.flags[BrickBase.bitCanOverflow];
            set => 
                base.flags[BrickBase.bitCanOverflow] = value;
        }

        internal bool CanMultiColumn
        {
            get => 
                base.flags[BrickBase.bitCanMultiColumn];
            set => 
                base.flags[BrickBase.bitCanMultiColumn] = value;
        }

        internal bool CanAddToPage
        {
            get => 
                base.flags[BrickBase.bitCanAddToPage];
            set => 
                base.flags[BrickBase.bitCanAddToPage] = value;
        }
    }
}

