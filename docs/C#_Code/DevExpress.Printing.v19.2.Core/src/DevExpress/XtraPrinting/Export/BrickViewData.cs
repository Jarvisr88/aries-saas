namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class BrickViewData : ILayoutControl
    {
        private ITableCell tableCell;
        private RectangleF bounds = RectangleF.Empty;
        private BrickStyle style;
        private int offsetYMult;

        public BrickViewData(BrickStyle style, RectangleF bounds, ITableCell tableCell)
        {
            this.tableCell = tableCell;
            this.bounds = bounds;
            this.style = style;
        }

        public virtual void ApplyClipping(RectangleF clipBounds)
        {
        }

        internal unsafe void IncreaseWidth(float delta)
        {
            RectangleF* efPtr1 = &this.bounds;
            efPtr1.Width += delta;
        }

        internal void SetOffsetY(int offsetY)
        {
            this.offsetYMult = offsetY;
        }

        private int OffsetY =>
            this.offsetYMult << 13;

        public int Left =>
            Convert.ToInt32(this.bounds.Left);

        public int Top =>
            Convert.ToInt32(this.bounds.Top) + this.OffsetY;

        [Description("Gets or sets a rectangle object specifying the height, width and location of the brick.")]
        public Rectangle Bounds
        {
            get
            {
                Rectangle rectangle = GraphicsUnitConverter.Round(this.bounds);
                rectangle.Offset(0, this.OffsetY);
                return rectangle;
            }
            set
            {
                this.bounds = value;
                this.bounds.Offset(0f, (float) -this.OffsetY);
            }
        }

        [Description("Gets or sets a rectangle object specifying the height, width and location of the brick.")]
        public RectangleF BoundsF
        {
            get
            {
                RectangleF bounds = this.bounds;
                bounds.Offset(0f, (float) this.OffsetY);
                return bounds;
            }
            protected set
            {
                this.bounds = value;
                this.bounds.Offset(0f, (float) -this.OffsetY);
            }
        }

        [Description("Gets or sets the width of the brick.")]
        public int Width =>
            this.Bounds.Width;

        [Description("Gets or sets the height of the brick.")]
        public int Height =>
            this.Bounds.Height;

        [Description("Gets or sets the BrickStyle instance used to render a brick in an appropriate format.")]
        public BrickStyle Style
        {
            get => 
                this.style;
            set => 
                this.style = value;
        }

        [Description("For internal use. Intended to support exporting bricks to some formats.")]
        public ITableCell TableCell
        {
            get => 
                (this.tableCell != null) ? this.tableCell : NullTableCell.Instance;
            set => 
                this.tableCell = value;
        }

        [Description("Gets the original bounds of the brick view data.")]
        public virtual Rectangle OriginalBounds =>
            GraphicsUnitConverter.Round(this.OriginalBoundsF);

        [Description("Gets the original bounds of the brick view data.")]
        public virtual RectangleF OriginalBoundsF =>
            this.BoundsF;
    }
}

