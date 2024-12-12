namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfDeviceNColorSpace : PdfSpecialColorSpace
    {
        internal const string TypeName = "DeviceN";
        private const string noneName = "None";
        private readonly string[] names;

        internal PdfDeviceNColorSpace(PdfObjectCollection collection, IList<object> array) : base(collection, array)
        {
            PdfColorSpace alternateSpace = base.AlternateSpace;
            if (!(alternateSpace is PdfDeviceColorSpace) && (!(alternateSpace is PdfCIEBasedColorSpace) && !(alternateSpace is PdfICCBasedColorSpace)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            IList<object> list = array[1] as IList<object>;
            if (list == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int count = list.Count;
            this.names = new string[count];
            for (int i = 0; i < count; i++)
            {
                PdfName name = list[i] as PdfName;
                if (name == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                string str = name.Name;
                if (string.IsNullOrEmpty(str))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                if ((str != "None") && (i > 0))
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (str == this.names[j])
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                    }
                }
                this.names[i] = str;
            }
        }

        protected override bool CheckArraySize(int actualSize) => 
            (actualSize == 4) || (actualSize == 5);

        protected virtual IList<object> CreateListToWrite(PdfObjectCollection collection)
        {
            List<object> list = new List<object>(this.names.Length);
            foreach (string str in this.names)
            {
                list.Add(new PdfName(str));
            }
            return new object[] { new PdfName("DeviceN"), list, base.AlternateSpace.Write(collection), base.TintTransform.Write(collection) };
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection) => 
            this.CreateListToWrite(collection);

        public string[] Names =>
            this.names;

        public override int ComponentsCount =>
            this.names.Length;
    }
}

