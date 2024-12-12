namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export.Rtf;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    [BrickExporter(typeof(RichTextBoxBrickExporter))]
    public class RichTextBoxBrick : VisualBrick
    {
        private string rtfText;
        private bool detectUrls;

        public RichTextBoxBrick(IRichTextBoxBrickOwner container) : base(container)
        {
            this.rtfText = string.Empty;
        }

        protected internal override float ValidatePageBottomInternal(float pageBottom, RectangleF rect, IPrintingSystemContext context)
        {
            float top;
            string rtf = this.RichTextBoxBrickContainer.RichTextBox.Rtf;
            try
            {
                int num;
                this.RichTextBoxBrickContainer.RichTextBox.Rtf = this.RtfText;
                RectangleF clientRectangle = this.GetClientRectangle(rect, 300f);
                pageBottom -= clientRectangle.Top;
                pageBottom = MathMethods.Scale(pageBottom, (double) (1f / base.GetScaleFactor(context)));
                RectangleF bounds = new RectangleF(clientRectangle.X, 0f, clientRectangle.Width, pageBottom);
                RectangleF ef3 = RichEditHelper.CorrectRtfLineBounds(300f, this.RichTextBoxBrickContainer.RichTextBox, bounds, 0, out num);
                if (ef3.Height > bounds.Height)
                {
                    top = rect.Top;
                }
                else
                {
                    if (num < this.RichTextBoxBrickContainer.RichTextBox.Text.Length)
                    {
                        pageBottom = ef3.Bottom;
                    }
                    top = MathMethods.Scale(pageBottom, (double) base.GetScaleFactor(context)) + clientRectangle.Top;
                }
            }
            finally
            {
                this.RichTextBoxBrickContainer.RichTextBox.Rtf = rtf;
            }
            return top;
        }

        [XtraSerializableProperty]
        public string RtfText
        {
            get => 
                this.rtfText;
            set
            {
                if (RtfTags.IsRtfContent(value))
                {
                    this.rtfText = value;
                }
                else
                {
                    this.rtfText = RtfTags.WrapTextInRtf(value);
                }
            }
        }

        public bool DetectUrls
        {
            get => 
                this.detectUrls;
            set => 
                this.detectUrls = value;
        }

        internal IRichTextBoxBrickOwner RichTextBoxBrickContainer =>
            (IRichTextBoxBrickOwner) base.BrickOwner;

        public override string BrickType =>
            "RichText";
    }
}

