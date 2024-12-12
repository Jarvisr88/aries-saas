namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native.CharacterComb;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    [BrickExporter(typeof(CharacterCombBrickExporter))]
    public class CharacterCombBrick : TextBrickBase
    {
        private const float DefaultCellWidth = 75f;
        private const float DefaultCellHeight = 75f;
        private const float DefaultCellHorizontalSpacing = 0f;
        private const float DefaultCellVerticalSpacing = 0f;
        private const SizeMode DefaultCellSizeMode = SizeMode.AutoSize;
        private float cellWidth;
        private float cellHeight;
        private float cellHorizontalSpacing;
        private float cellVerticalSpacing;
        private SizeMode cellSizeMode;

        public CharacterCombBrick() : this(NullBrickOwner.Instance)
        {
        }

        private CharacterCombBrick(CharacterCombBrick brick) : base(brick)
        {
            this.cellWidth = 75f;
            this.cellHeight = 75f;
            this.cellSizeMode = SizeMode.AutoSize;
        }

        public CharacterCombBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
            this.cellWidth = 75f;
            this.cellHeight = 75f;
            this.cellSizeMode = SizeMode.AutoSize;
        }

        public override object Clone() => 
            new CharacterCombBrick(this);

        internal CharacterCombInfo GetCellInfo() => 
            this.GetCellInfo(base.Style);

        internal CharacterCombInfo GetCellInfo(BrickStyle style) => 
            new CharacterCombInfo(this.CellWidth, this.CellHeight, this.CellHorizontalSpacing, this.CellVerticalSpacing, this.CellSizeMode, style, this.RightToLeftLayout);

        public override RectangleF GetClientRectangle(RectangleF rect, float dpi) => 
            base.GetClientRectangle(rect, dpi);

        protected internal override float ValidatePageBottomInternal(float pageBottom, RectangleF rect, IPrintingSystemContext context) => 
            new CharacterCombHelper(this.GetCellInfo()).GetBottomSplitValue(pageBottom, rect);

        protected override float ValidatePageRightInternal(float pageRight, RectangleF rect) => 
            new CharacterCombHelper(this.GetCellInfo()).GetRightSplitValue(pageRight, rect);

        [Description("Specifies the cell width a CharacterCombBrick."), XtraSerializableProperty]
        public float CellWidth
        {
            get => 
                this.cellWidth;
            set => 
                this.cellWidth = value;
        }

        [Description("Specifies the cell height in a CharacterCombBrick."), XtraSerializableProperty]
        public float CellHeight
        {
            get => 
                this.cellHeight;
            set => 
                this.cellHeight = value;
        }

        [Description("Specifies the horizontal spacing between adjacent cells in a CharacterCombBrick."), XtraSerializableProperty, DefaultValue((float) 0f)]
        public float CellHorizontalSpacing
        {
            get => 
                this.cellHorizontalSpacing;
            set => 
                this.cellHorizontalSpacing = value;
        }

        [Description("Specifies the horizontal spacing between adjacent cells in a CharacterCombBrick."), XtraSerializableProperty, DefaultValue((float) 0f)]
        public float CellVerticalSpacing
        {
            get => 
                this.cellVerticalSpacing;
            set => 
                this.cellVerticalSpacing = value;
        }

        [Description("Specifies whether the cell size should depend on the current font size."), XtraSerializableProperty, DefaultValue(3)]
        public SizeMode CellSizeMode
        {
            get => 
                this.cellSizeMode;
            set => 
                this.cellSizeMode = value;
        }

        public override string BrickType =>
            "CharacterComb";
    }
}

