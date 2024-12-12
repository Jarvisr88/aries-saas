namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public static class PdfLuminositySoftMaskBuilder
    {
        public static PdfLuminositySoftMask CreateSoftMask(PdfShading maskShading, PdfRectangle maskBoundingBox, PdfDocumentCatalog documentCatalog)
        {
            PdfGroupForm groupForm = new PdfGroupForm(documentCatalog, maskBoundingBox);
            using (PdfCommandConstructor constructor = new PdfCommandConstructor(groupForm.Resources))
            {
                constructor.DrawShading(maskShading);
                groupForm.ReplaceCommands(constructor.Commands);
            }
            return new PdfLuminositySoftMask(groupForm, documentCatalog.Objects);
        }
    }
}

