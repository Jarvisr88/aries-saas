namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfDeferredInteractiveFormFieldsList : PdfInteractiveFormFieldsList
    {
        private readonly List<PdfInteractiveFormField> fields;

        public PdfDeferredInteractiveFormFieldsList(List<PdfInteractiveFormField> fields)
        {
            this.fields = fields;
        }

        protected internal override PdfObject GetDeferredSavedObject(PdfObjectCollection objects, bool isCloning) => 
            new PdfClonedInteractiveFormFieldsList(this.fields, objects);

        public override IEnumerable<PdfInteractiveFormField> GetFormFields(PdfObjectCollection objects) => 
            (this.fields.Count != 0) ? this.fields : null;

        protected internal override bool IsDeferredObject(bool isCloning) => 
            isCloning;

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            new PdfWritableObjectArray((IEnumerable<PdfObject>) this.fields, objects);
    }
}

