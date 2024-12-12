namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Text.Internal;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native.MarkupText;
    using System;
    using System.Drawing;

    [BrickExporter(typeof(MarkupTextBrickExporter))]
    public class MarkupTextBrick : TextBrick
    {
        private PrintingStringInfo stringInfo;
        private DevExpress.XtraPrinting.Drawing.ImageItemCollection imageItemCollection;

        public MarkupTextBrick() : this(NullBrickOwner.Instance)
        {
        }

        public MarkupTextBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
        }

        private MarkupTextBrick(MarkupTextBrick markupTextBrick) : base(markupTextBrick)
        {
        }

        public override object Clone() => 
            new MarkupTextBrick(this);

        protected internal override string GetUrlByPoint(Point point)
        {
            StringBlock linkByPoint = this.StringInfo.GetLinkByPoint(point);
            return ((linkByPoint != null) ? linkByPoint.Link : base.GetUrlByPoint(point));
        }

        protected override void OnTextChanged()
        {
            base.OnTextChanged();
            this.stringInfo = null;
        }

        protected internal override float ValidatePageBottomInternal(float pageBottom, RectangleF brickRect, IPrintingSystemContext context) => 
            this.StringInfo.SimpleString ? base.ValidatePageBottomInternal(pageBottom, brickRect, context) : MarkupTextHelper.GetBottomSplitValue(this.StringInfo, brickRect.Top, pageBottom);

        internal DevExpress.XtraPrinting.Drawing.ImageItemCollection ImageItemCollection
        {
            get => 
                this.imageItemCollection ?? this.PrintingSystem.ImageResources;
            set => 
                this.imageItemCollection = value;
        }

        internal PrintingStringInfo StringInfo
        {
            get
            {
                this.stringInfo ??= MarkupTextPainter.Default.Calculate(this);
                return this.stringInfo;
            }
        }

        public override string BrickType =>
            "MarkupText";
    }
}

