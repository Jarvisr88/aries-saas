namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfAnnotationAppearances : PdfObject
    {
        internal const string NormalAppearanceDictionaryKey = "N";
        private const string rolloverAppearanceDictionaryKey = "R";
        private const string downAppearanceDictionaryKey = "D";
        private PdfAnnotationAppearance normal;
        private PdfAnnotationAppearance rollover;
        private PdfAnnotationAppearance down;
        private readonly PdfForm form;

        internal PdfAnnotationAppearances()
        {
        }

        internal PdfAnnotationAppearances(PdfForm form) : base(form.ObjectNumber)
        {
            this.form = form;
        }

        internal PdfAnnotationAppearances(PdfDocumentCatalog documentCatalog, PdfRectangle bBox)
        {
            this.normal = new PdfAnnotationAppearance(documentCatalog, bBox);
        }

        internal PdfAnnotationAppearances(PdfReaderDictionary dictionary, PdfRectangle parentBBox) : base(dictionary.Number)
        {
            this.normal = PdfAnnotationAppearance.Parse(dictionary, "N");
            if (this.normal == null)
            {
                if (parentBBox == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.normal = new PdfAnnotationAppearance(dictionary.Objects.DocumentCatalog, parentBBox);
            }
            this.rollover = PdfAnnotationAppearance.Parse(dictionary, "R");
            this.down = PdfAnnotationAppearance.Parse(dictionary, "D");
        }

        internal void SetForm(PdfAnnotationAppearanceState state, string name, PdfForm form)
        {
            if (state == PdfAnnotationAppearanceState.Rollover)
            {
                this.rollover ??= new PdfAnnotationAppearance();
                this.rollover.SetForm(name, form);
            }
            else if (state == PdfAnnotationAppearanceState.Down)
            {
                this.down ??= new PdfAnnotationAppearance();
                this.down.SetForm(name, form);
            }
            else
            {
                this.normal ??= new PdfAnnotationAppearance();
                this.normal.SetForm(name, form);
            }
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            if (this.form != null)
            {
                return this.form.ToWritableObject(objects);
            }
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("N", this.normal.ToWritableObject(objects));
            if (this.rollover != null)
            {
                dictionary.Add("R", this.rollover.ToWritableObject(objects));
            }
            if (this.down != null)
            {
                dictionary.Add("D", this.down.ToWritableObject(objects));
            }
            return dictionary;
        }

        public PdfAnnotationAppearance Normal =>
            this.normal;

        public PdfAnnotationAppearance Rollover =>
            this.rollover;

        public PdfAnnotationAppearance Down =>
            this.down;

        public PdfForm Form =>
            this.form;

        internal IList<string> Names
        {
            get
            {
                List<string> names = this.normal.GetNames("N");
                if (this.rollover != null)
                {
                    names.AddRange(this.rollover.GetNames("R"));
                }
                if (this.down != null)
                {
                    names.AddRange(this.down.GetNames("D"));
                }
                return names;
            }
        }
    }
}

