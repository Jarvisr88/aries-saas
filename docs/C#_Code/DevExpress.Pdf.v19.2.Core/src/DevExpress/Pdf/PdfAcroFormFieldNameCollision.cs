namespace DevExpress.Pdf
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfAcroFormFieldNameCollision
    {
        private readonly PdfAcroFormField field;
        private readonly ISet<string> forbiddenNames;
        public PdfAcroFormField Field =>
            this.field;
        public ISet<string> ForbiddenNames =>
            this.forbiddenNames;
        internal PdfAcroFormFieldNameCollision(PdfAcroFormField field, ISet<string> forbiddenNames)
        {
            this.field = field;
            this.forbiddenNames = forbiddenNames;
        }
    }
}

