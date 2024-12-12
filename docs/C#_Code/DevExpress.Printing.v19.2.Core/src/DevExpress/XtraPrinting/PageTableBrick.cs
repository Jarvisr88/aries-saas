namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class PageTableBrick : PanelBrick, IPageBrick
    {
        private TableRowCollection rows;

        public PageTableBrick()
        {
            base.Sides = BorderSide.None;
            base.BackColor = Color.Transparent;
        }

        protected override object CreateCollectionItemCore(string propertyName, XtraItemEventArgs e) => 
            (propertyName != "Rows") ? base.CreateCollectionItemCore(propertyName, e) : new TableRow();

        protected override void InitializeBrickProperties()
        {
            base.InitializeBrickProperties();
            this.Alignment = BrickAlignment.None;
            this.LineAlignment = BrickAlignment.None;
            this.rows = new TableRowCollection();
        }

        protected override void OnSetPrintingSystem(bool cacheStyle)
        {
            this.UpdateBricks();
            base.OnSetPrintingSystem(cacheStyle);
        }

        protected override void PerformInnerBricksLayout(IPrintingSystemContext context)
        {
            this.UpdateBricks();
            base.PerformInnerBricksLayout(context);
            this.UpdateSize();
            this.UpdateLayout();
        }

        protected internal override void PerformLayout(IPrintingSystemContext context)
        {
            base.PerformLayout(context);
            this.InitialRect = new PageCustomBrickHelper(context).AlignRect(this.InitialRect, base.Modifier, this.Alignment, this.LineAlignment);
        }

        protected override void SetIndexCollectionItemCore(string propertyName, XtraSetItemIndexEventArgs e)
        {
            if (propertyName == "Rows")
            {
                this.Rows.Add((TableRow) e.Item.Value);
            }
            base.SetIndexCollectionItemCore(propertyName, e);
        }

        protected override bool ShouldSerializeCore(string propertyName) => 
            (propertyName != "Modifier") ? base.ShouldSerializeCore(propertyName) : true;

        private void UpdateBricks()
        {
            this.Bricks.Clear();
            foreach (TableRow row in this.rows)
            {
                foreach (Brick brick in row.Bricks)
                {
                    this.Bricks.Add(brick);
                }
            }
        }

        private void UpdateLayout()
        {
            float yOffset = 0f;
            foreach (TableRow row in this.rows)
            {
                row.Align(this.Alignment, yOffset, base.Width);
                SizeF ef = row.CalcSize();
                yOffset += ef.Height;
            }
        }

        public unsafe void UpdateSize()
        {
            SizeF empty = SizeF.Empty;
            foreach (TableRow row in this.rows)
            {
                SizeF ef2 = row.CalcSize();
                SizeF* efPtr1 = &empty;
                efPtr1.Height += ef2.Height;
                empty.Width = Math.Max(empty.Width, ef2.Width);
            }
            base.Size = empty;
        }

        [Description("Gets the collection of rows owned by the PageTableBrick object."), XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false)]
        public TableRowCollection Rows =>
            this.rows;

        [Description("Gets or sets the alignment of brick within the page layout rectangle."), XtraSerializableProperty]
        public BrickAlignment Alignment { get; set; }

        [Description("Gets or sets the brick alignment related to the top of the parent area."), XtraSerializableProperty]
        public BrickAlignment LineAlignment { get; set; }

        [Description("Gets a collection of bricks which are contained in this PageTableBrick."), XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public override BrickCollectionBase Bricks =>
            base.Bricks;

        [Description("Gets the text string, containing the brick type information.")]
        public override string BrickType =>
            "PageTable";
    }
}

