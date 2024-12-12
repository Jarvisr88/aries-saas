namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class EditingField : BrickPageInfo, IXtraPartlyDeserializable
    {
        private VisualBrick brick;
        private bool readOnly;

        internal event EventHandler<EventArgs> EditValueChanged;

        internal event EventHandler<EventArgs> ReadOnlyChanged;

        protected EditingField(VisualBrick brick)
        {
            this.brick = brick;
            this.readOnly = false;
            this.ID = string.Empty;
        }

        protected virtual void AfterLoadBrick()
        {
        }

        internal virtual void AssignEditValueToBrick(VisualBrick brick)
        {
            brick.EditValue = this.EditValue;
        }

        void IXtraPartlyDeserializable.Deserialize(object rootObject, IXtraPropertyCollection properties)
        {
            XtraPropertyInfo info = properties["PageIndex"];
            this.PageIndex = (int) info.ValueToObject(typeof(int));
            this.PageID = (this.PageIndex >= 0) ? ((Document) rootObject).Pages[this.PageIndex].ID : -1L;
            XtraPropertyInfo info2 = properties["Indices"];
            base.BrickIndices = BrickPagePairHelper.ParseIndices(info2.Value.ToString());
        }

        public RectangleF GetBounds() => 
            (this.Pages != null) ? base.GetBoundsCore(this.Pages) : RectangleF.Empty;

        public T GetEditValue<T>() => 
            (T) this.EditValue;

        public Page GetPage() => 
            (this.Pages != null) ? base.GetPageCore(this.Pages) : null;

        internal void InvalidatePageInfo()
        {
            base.UpdatePageInfo(BrickPagePairHelper.EmptyIndices, -1, -1L, RectangleF.Empty);
        }

        protected void RaiseEditValueChanged(EventArgs e)
        {
            if (this.EditValueChanged != null)
            {
                this.EditValueChanged(this, e);
            }
        }

        protected void RaiseReadOnlyChanged(EventArgs e)
        {
            if (this.ReadOnlyChanged != null)
            {
                this.ReadOnlyChanged(this, e);
            }
        }

        internal void UpdatePageIndex()
        {
            this.UpdatePageIndex(this.Pages);
        }

        internal void UpdatePageIndex(IPageRepository pages)
        {
            Page page;
            int num;
            if ((base.PageID >= 0L) && ((pages != null) && pages.TryGetPageByID(base.PageID, out page, out num)))
            {
                this.PageIndex = num;
            }
            else
            {
                this.PageIndex = -1;
            }
        }

        internal void UpdatePageInfo(Page page, int[] brickIndices)
        {
            base.UpdatePageInfo(brickIndices, page.Index, page.ID, RectangleF.Empty);
        }

        internal IPageRepository Pages { get; set; }

        public VisualBrick Brick
        {
            get
            {
                if (this.brick == null)
                {
                    Page page = this.GetPage();
                    if (page != null)
                    {
                        this.brick = page.GetBrickByIndices(base.BrickIndices) as VisualBrick;
                        this.AfterLoadBrick();
                    }
                }
                return this.brick;
            }
        }

        [DefaultValue(""), XtraSerializableProperty, EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DXHelpExclude(true)]
        public string Indices =>
            BrickPagePairHelper.IndicesFromArray(base.BrickIndices);

        [XtraSerializableProperty]
        public override int PageIndex
        {
            get => 
                base.PageIndex;
            protected set => 
                base.PageIndex = value;
        }

        [DefaultValue(""), XtraSerializableProperty]
        public string ID { get; set; }

        [DefaultValue(false), XtraSerializableProperty]
        public bool ReadOnly
        {
            get => 
                this.readOnly;
            set
            {
                if (this.readOnly != value)
                {
                    this.readOnly = value;
                    this.RaiseReadOnlyChanged(EventArgs.Empty);
                }
            }
        }

        public virtual object EditValue
        {
            get => 
                this.Brick.EditValue;
            set
            {
                if (!Equals(value, this.Brick.EditValue))
                {
                    this.Brick.EditValue = value;
                    this.RaiseEditValueChanged(EventArgs.Empty);
                }
            }
        }

        [XtraSerializableProperty(-1), EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DXHelpExclude(true)]
        public string EditingFieldType =>
            base.GetType().Name;
    }
}

