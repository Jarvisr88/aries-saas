namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public abstract class PdfInteractiveFormFieldsList : PdfObject
    {
        protected PdfInteractiveFormFieldsList()
        {
        }

        protected PdfInteractiveFormFieldsList(int number) : base(number)
        {
        }

        public abstract IEnumerable<PdfInteractiveFormField> GetFormFields(PdfObjectCollection objects);
    }
}

