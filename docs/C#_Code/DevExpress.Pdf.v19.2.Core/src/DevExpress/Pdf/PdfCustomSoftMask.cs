namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfCustomSoftMask : PdfSoftMask
    {
        private const string transparencyGroupDictionaryKey = "G";
        private const string transferFunctionDictionaryKey = "TR";
        private readonly PdfGroupForm transparencyGroup;
        private readonly PdfFunction transferFunction;

        protected PdfCustomSoftMask(PdfReaderDictionary dictionary) : base(dictionary.Objects)
        {
            object obj2;
            PdfObjectCollection objects = dictionary.Objects;
            if (!dictionary.TryGetValue("G", out obj2))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.transparencyGroup = objects.GetXObject(obj2, null, "Form") as PdfGroupForm;
            if (this.transparencyGroup == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.transferFunction = dictionary.TryGetValue("TR", out obj2) ? PdfFunction.Parse(objects, obj2, false) : PdfPredefinedFunction.Identity;
        }

        protected PdfCustomSoftMask(PdfGroupForm groupForm, PdfObjectCollection collection) : base(collection)
        {
            this.transparencyGroup = groupForm;
            this.transferFunction = PdfPredefinedFunction.Identity;
        }

        protected internal override bool IsSame(PdfSoftMask softMask) => 
            false;

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("Type", new PdfName("Mask"));
            dictionary.Add("S", new PdfName(this.ActualName));
            dictionary.Add("G", objects.AddObject((PdfObject) this.transparencyGroup));
            if (!ReferenceEquals(this.transferFunction, PdfPredefinedFunction.Identity))
            {
                dictionary.Add("TR", this.transferFunction.Write(objects));
            }
            return dictionary;
        }

        protected internal override object Write(PdfObjectCollection objects) => 
            objects.AddObject((PdfObject) this);

        public PdfGroupForm TransparencyGroup =>
            this.transparencyGroup;

        public PdfFunction TransferFunction =>
            this.transferFunction;

        protected abstract string ActualName { get; }
    }
}

