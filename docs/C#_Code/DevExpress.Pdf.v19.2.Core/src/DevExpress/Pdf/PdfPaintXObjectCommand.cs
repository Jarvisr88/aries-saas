namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfPaintXObjectCommand : PdfCommand
    {
        internal const string Name = "Do";
        private readonly string xObjectName;
        private readonly PdfXObject xObject;

        internal PdfPaintXObjectCommand(PdfResources resources, PdfStack operands)
        {
            string xObjectName = operands.PopName(true);
            this.xObject = resources.GetXObject(xObjectName);
            if (this.xObject == null)
            {
                this.xObjectName = xObjectName;
            }
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            PdfXObject obj2 = this.xObject ?? interpreter.PageResources.GetXObject(this.xObjectName);
            PdfImage image = obj2 as PdfImage;
            if (image != null)
            {
                interpreter.DrawImage(image);
            }
            else
            {
                PdfForm form = obj2 as PdfForm;
                if (form != null)
                {
                    PdfGroupForm form2 = form as PdfGroupForm;
                    if (form2 == null)
                    {
                        interpreter.DrawForm(form);
                    }
                    else
                    {
                        interpreter.DrawTransparencyGroup(form2);
                    }
                }
            }
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteName(resources.FindXObjectName(this.xObject));
            writer.WriteSpace();
            writer.WriteString("Do");
        }

        public PdfXObject XObject =>
            this.xObject;
    }
}

