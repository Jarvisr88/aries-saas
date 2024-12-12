namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfLuminositySoftMask : PdfCustomSoftMask
    {
        internal const string Name = "Luminosity";
        private const string backdropColorDictionaryKey = "BC";
        private readonly PdfColor backdropColor;

        internal PdfLuminositySoftMask(PdfReaderDictionary dictionary) : base(dictionary)
        {
            IList<object> array = dictionary.GetArray("BC");
            if (array != null)
            {
                int count = array.Count;
                double[] components = new double[count];
                int index = 0;
                while (true)
                {
                    if (index >= count)
                    {
                        this.backdropColor = new PdfColor(components);
                        break;
                    }
                    components[index] = PdfDocumentReader.ConvertToDouble(array[index]);
                    index++;
                }
            }
        }

        internal PdfLuminositySoftMask(PdfGroupForm groupForm, PdfObjectCollection collection) : base(groupForm, collection)
        {
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = (PdfWriterDictionary) base.ToWritableObject(objects);
            dictionary.Add("BC", this.backdropColor);
            return dictionary;
        }

        public PdfColor BackdropColor =>
            this.backdropColor;

        protected override string ActualName =>
            "Luminosity";
    }
}

