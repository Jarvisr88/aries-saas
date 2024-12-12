namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class PageImageBrick : ImageBrick, IPageBrick
    {
        public PageImageBrick()
        {
            base.Sides = BorderSide.None;
        }

        public PageImageBrick(BorderSide sides, float borderWidth, Color borderColor, Color backColor) : base(sides, borderWidth, borderColor, backColor)
        {
            base.Sides = BorderSide.None;
        }

        protected override void InitializeBrickProperties()
        {
            base.InitializeBrickProperties();
            this.Alignment = BrickAlignment.None;
            this.LineAlignment = BrickAlignment.None;
        }

        protected internal override void PerformLayout(IPrintingSystemContext context)
        {
            base.PerformLayout(context);
            this.InitialRect = new PageCustomBrickHelper(context).AlignRect(this.InitialRect, base.Modifier, this.Alignment, this.LineAlignment);
        }

        protected override bool ShouldSerializeCore(string propertyName) => 
            (propertyName != "Modifier") ? base.ShouldSerializeCore(propertyName) : true;

        [Description("Gets or sets the alignment of brick within the page layout rectangle."), XtraSerializableProperty]
        public BrickAlignment Alignment { get; set; }

        [Description("Gets or sets the brick alignment related to the top of the parent area."), XtraSerializableProperty]
        public BrickAlignment LineAlignment { get; set; }

        [Description("Gets the text string, containing the brick type information."), XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public override DevExpress.XtraPrinting.ImageAlignment ImageAlignment
        {
            get => 
                DevExpress.XtraPrinting.ImageAlignment.Default;
            set
            {
            }
        }

        public override string BrickType =>
            "PageImage";
    }
}

