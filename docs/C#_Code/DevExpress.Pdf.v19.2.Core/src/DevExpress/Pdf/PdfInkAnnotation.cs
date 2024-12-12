namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfInkAnnotation : PdfMarkupAnnotation
    {
        internal const string Type = "Ink";
        private const string inksDictionaryKey = "InkList";
        private readonly IList<PdfPoint[]> inks;
        private readonly PdfAnnotationBorderStyle borderStyle;

        internal PdfInkAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            this.inks = new List<PdfPoint[]>();
            IList<object> array = dictionary.GetArray("InkList");
            if (array != null)
            {
                foreach (object obj2 in array)
                {
                    IList<object> list2 = obj2 as IList<object>;
                    if (list2 == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.inks.Add(PdfDocumentReader.CreatePointArray(list2));
                }
            }
            this.borderStyle = PdfAnnotationBorderStyle.Parse(dictionary);
        }

        protected internal override void Accept(IPdfAnnotationVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected internal override IPdfAnnotationAppearanceBuilder CreateAppearanceBuilder(IPdfExportFontProvider fontSearch) => 
            new PdfInkAnnotationAppearanceBuilder(this);

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            Func<PdfPoint[], object> converter = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                Func<PdfPoint[], object> local1 = <>c.<>9__11_0;
                converter = <>c.<>9__11_0 = value => new PdfWritablePointsArray(value);
            }
            dictionary.AddList<PdfPoint[]>("InkList", this.inks, converter);
            dictionary.Add("BS", this.borderStyle);
            return dictionary;
        }

        public IList<PdfPoint[]> Inks =>
            this.inks;

        public PdfAnnotationBorderStyle BorderStyle =>
            this.borderStyle;

        protected override string AnnotationType =>
            "Ink";

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfInkAnnotation.<>c <>9 = new PdfInkAnnotation.<>c();
            public static Func<PdfPoint[], object> <>9__11_0;

            internal object <CreateDictionary>b__11_0(PdfPoint[] value) => 
                new PdfWritablePointsArray(value);
        }
    }
}

