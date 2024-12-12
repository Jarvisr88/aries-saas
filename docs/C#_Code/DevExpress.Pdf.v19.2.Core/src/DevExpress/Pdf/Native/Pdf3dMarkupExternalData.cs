namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class Pdf3dMarkupExternalData : PdfMarkupExternalData
    {
        private readonly Pdf3dAnnotation annotation;
        private readonly string annotationName;
        private readonly byte[] md5;
        private readonly Pdf3dView view;

        public Pdf3dMarkupExternalData(PdfPage page, PdfReaderDictionary dictionary) : base(dictionary)
        {
            object obj2;
            PdfObjectCollection objects = dictionary.Objects;
            if (dictionary.TryGetValue("3DA", out obj2))
            {
                byte[] buffer = objects.TryResolve(obj2, null) as byte[];
                if (buffer != null)
                {
                    this.annotationName = PdfDocumentReader.ConvertToString(buffer);
                }
                else
                {
                    this.annotation = objects.GetObject<Pdf3dAnnotation>(obj2, dict => new Pdf3dAnnotation(page, dict));
                }
            }
            if (dictionary.TryGetValue("3DV", out obj2))
            {
                this.view = objects.GetObject<Pdf3dView>(obj2, dict => new Pdf3dView(dict, page));
            }
            this.md5 = dictionary.GetBytes("MD5");
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            if (this.annotationName != null)
            {
                dictionary.Add("3DA", this.annotationName);
            }
            else
            {
                dictionary.Add("3DA", this.annotation);
            }
            dictionary.Add("3DV", this.view);
            dictionary.AddIfPresent("MD5", this.md5);
            return dictionary;
        }

        public override PdfMarkupExternalDataType Type =>
            PdfMarkupExternalDataType.Comment3D;
    }
}

