namespace DevExpress.Pdf.Native
{
    using System;

    public class Pdf3dAnimationStyle : PdfObject
    {
        private readonly Pdf3dAnimationStyleKind kind;
        private readonly int playCount;
        private readonly double timeMultiplier;

        public Pdf3dAnimationStyle(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.kind = dictionary.GetEnumName<Pdf3dAnimationStyleKind>("Subtype");
            int? integer = dictionary.GetInteger("PC");
            this.playCount = (integer != null) ? integer.GetValueOrDefault() : 0;
            double? number = dictionary.GetNumber("TM");
            this.timeMultiplier = (number != null) ? number.GetValueOrDefault() : 1.0;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Type", "3DAnimationStyle");
            dictionary.AddEnumName<Pdf3dAnimationStyleKind>("Subtype", this.kind);
            dictionary.Add("PC", this.playCount, 0);
            dictionary.Add("TM", this.timeMultiplier, 1.0);
            return dictionary;
        }

        public Pdf3dAnimationStyleKind Kind =>
            this.kind;

        public int PlayCount =>
            this.playCount;

        public double TimeMultiplier =>
            this.timeMultiplier;
    }
}

