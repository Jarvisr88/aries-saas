namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class PageInfoBrick : PageInfoTextBrick, IPageBrick
    {
        protected bool fAutoWidth;

        public PageInfoBrick()
        {
        }

        public PageInfoBrick(BorderSide sides, float borderWidth, Color borderColor, Color backColor, Color foreColor) : base(sides, borderWidth, borderColor, backColor, foreColor)
        {
        }

        protected override void InitializeBrickProperties()
        {
            base.InitializeBrickProperties();
            this.Alignment = BrickAlignment.None;
            this.LineAlignment = BrickAlignment.None;
        }

        protected internal override void PerformLayout(IPrintingSystemContext context)
        {
            if (base.IsInitialized && (base.Width == 0f))
            {
                this.AutoWidth = true;
            }
            base.PerformLayout(context);
            this.InitialRect = new PageCustomBrickHelper(context).AlignRect(this.InitialRect, base.Modifier, this.Alignment, this.LineAlignment);
        }

        protected override bool ShouldSerializeCore(string propertyName) => 
            (propertyName != "Modifier") ? base.ShouldSerializeCore(propertyName) : true;

        [Description("Gets or sets the alignment of brick within the page layout rectangle."), XtraSerializableProperty]
        public BrickAlignment Alignment { get; set; }

        [Description("Gets or sets the brick alignment related to the top of the parent area."), XtraSerializableProperty]
        public BrickAlignment LineAlignment { get; set; }

        [Description("Determines whether the current brick is resized in order to display the entire text of a brick."), XtraSerializableProperty]
        public bool AutoWidth
        {
            get => 
                this.fAutoWidth;
            set => 
                this.fAutoWidth = value;
        }

        protected override bool AutoWidthCore =>
            this.AutoWidth;

        [Description("Gets the text string, containing the brick type information.")]
        public override string BrickType =>
            "PageInfo";
    }
}

