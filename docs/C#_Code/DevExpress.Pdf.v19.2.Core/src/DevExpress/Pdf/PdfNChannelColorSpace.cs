namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfNChannelColorSpace : PdfDeviceNColorSpace
    {
        internal const string TypeName = "NChannel";
        private const string colorantsDictionaryKey = "Colorants";
        private const string processDictionaryKey = "Process";
        private const string colorSpaceDictionaryKey = "ColorSpace";
        private const string componentsDictionaryKey = "Components";
        private readonly PdfSeparationColorSpace[] colorants;
        private readonly string[] processComponentsNames;
        private readonly PdfColorSpace processColorSpace;

        internal PdfNChannelColorSpace(PdfObjectCollection collection, IList<object> array, PdfReaderDictionary dictionary) : base(collection, array)
        {
            List<string> list = new List<string>();
            PdfDictionary dictionary2 = dictionary.GetDictionary("Colorants");
            if (dictionary2 != null)
            {
                int count = dictionary2.Count;
                if (count == 0)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.colorants = new PdfSeparationColorSpace[count];
                int num2 = 0;
                foreach (KeyValuePair<string, object> pair in dictionary2)
                {
                    string key = pair.Key;
                    PdfSeparationColorSpace colorSpace = collection.GetColorSpace(pair.Value) as PdfSeparationColorSpace;
                    if ((colorSpace == null) || ((colorSpace.Name != key) || (colorSpace.ComponentsCount != 1)))
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.colorants[num2++] = colorSpace;
                    list.Add(key);
                }
            }
            PdfReaderDictionary dictionary3 = dictionary.GetDictionary("Process");
            if (dictionary3 != null)
            {
                IList<object> list2 = dictionary3.GetArray("Components");
                this.processColorSpace = dictionary3.GetColorSpace("ColorSpace");
                if ((list2 == null) || (this.processColorSpace == null))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                int count = list2.Count;
                if (count != this.processColorSpace.ComponentsCount)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.processComponentsNames = new string[count];
                int index = 0;
                while (true)
                {
                    if (index >= count)
                    {
                        list.AddRange(this.processComponentsNames);
                        break;
                    }
                    PdfName name = list2[index] as PdfName;
                    if (name == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.processComponentsNames[index] = name.Name;
                    index++;
                }
            }
            if ((this.colorants == null) && (this.processColorSpace == null))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            foreach (string str2 in base.Names)
            {
                if (!list.Contains(str2))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
        }

        protected override bool CheckArraySize(int actualSize) => 
            actualSize == 5;

        protected override IList<object> CreateListToWrite(PdfObjectCollection collection)
        {
            List<object> list = new List<object>(base.CreateListToWrite(collection));
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.Add("Subtype", new PdfName("NChannel"));
            if (this.colorants != null)
            {
                PdfWriterDictionary dictionary2 = new PdfWriterDictionary(collection);
                PdfSeparationColorSpace[] colorants = this.colorants;
                int index = 0;
                while (true)
                {
                    if (index >= colorants.Length)
                    {
                        dictionary.Add("Colorants", collection.AddDictionary(dictionary2));
                        break;
                    }
                    PdfSeparationColorSpace space = colorants[index];
                    dictionary2.Add(space.Name, space.Write(collection));
                    index++;
                }
            }
            if (this.processColorSpace != null)
            {
                PdfWriterDictionary dictionary3 = new PdfWriterDictionary(collection);
                dictionary3.Add("ColorSpace", this.processColorSpace.Write(collection));
                List<object> list2 = new List<object>(this.processComponentsNames.Length);
                string[] processComponentsNames = this.processComponentsNames;
                int index = 0;
                while (true)
                {
                    if (index >= processComponentsNames.Length)
                    {
                        dictionary3.Add("Components", list2);
                        dictionary.Add("Process", collection.AddDictionary(dictionary3));
                        break;
                    }
                    string name = processComponentsNames[index];
                    list2.Add(new PdfName(name));
                    index++;
                }
            }
            list.Add(collection.AddDictionary(dictionary));
            return list;
        }

        public PdfSeparationColorSpace[] Colorants =>
            this.colorants;

        public string[] ProcessComponentsNames =>
            this.processComponentsNames;

        public PdfColorSpace ProcessColorSpace =>
            this.processColorSpace;
    }
}

