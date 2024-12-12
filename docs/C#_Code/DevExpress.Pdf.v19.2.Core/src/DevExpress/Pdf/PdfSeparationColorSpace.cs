namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfSeparationColorSpace : PdfSpecialColorSpace
    {
        internal const string TypeName = "Separation";
        private readonly string name;

        internal PdfSeparationColorSpace(PdfObjectCollection collection, IList<object> array) : base(collection, array)
        {
            PdfName name = collection.TryResolve(array[1], null) as PdfName;
            if ((name == null) || (base.TintTransform.Domain.Count != 1))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.name = name.Name;
        }

        protected override bool CheckArraySize(int actualSize) => 
            actualSize == 4;

        protected internal override object ToWritableObject(PdfObjectCollection collection) => 
            new object[] { new PdfName("Separation"), new PdfName(this.name), base.AlternateSpace.Write(collection), base.TintTransform.Write(collection) };

        public string Name =>
            this.name;

        public override int ComponentsCount =>
            1;
    }
}

