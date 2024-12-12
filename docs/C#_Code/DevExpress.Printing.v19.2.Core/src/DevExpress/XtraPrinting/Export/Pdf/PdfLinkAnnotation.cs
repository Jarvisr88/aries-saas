namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class PdfLinkAnnotation : PdfAnnotation
    {
        private static PdfArray borders = new PdfArray();
        private PdfAction action;

        static PdfLinkAnnotation()
        {
            borders.Add(0);
            borders.Add(0);
            borders.Add(0);
        }

        public PdfLinkAnnotation(PdfAction action, bool compressed) : base(compressed)
        {
            this.Initialize(action);
        }

        public PdfLinkAnnotation(PdfAction action, PdfRectangle rect, bool compressed) : base(rect, compressed)
        {
            this.Initialize(action);
        }

        public override void FillUp()
        {
            base.FillUp();
            base.Dictionary.Add("Border", borders);
            if (this.action != null)
            {
                base.Dictionary.Add("A", this.action.Dictionary);
                this.action.FillUp();
            }
            if (this.PdfACompatible)
            {
                base.Dictionary.Add("F", 4);
            }
        }

        private void Initialize(PdfAction action)
        {
            this.action = action;
        }

        protected override void RegisterContent(PdfXRef xRef)
        {
            if (this.action != null)
            {
                this.action.Register(xRef);
            }
        }

        protected override void WriteContent(StreamWriter writer)
        {
            if (this.action != null)
            {
                this.action.Write(writer);
            }
        }

        public override string Subtype =>
            "Link";

        public bool PdfACompatible { get; set; }
    }
}

