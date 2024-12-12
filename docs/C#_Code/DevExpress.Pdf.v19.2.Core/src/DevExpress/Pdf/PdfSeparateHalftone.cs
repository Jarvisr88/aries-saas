namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfSeparateHalftone : PdfHalftone
    {
        internal const int Number = 5;
        private const string defaultDictionaryKey = "Default";
        private readonly IDictionary<string, PdfHalftone> components;
        private readonly PdfHalftone defaultHalftone;

        internal PdfSeparateHalftone(PdfReaderDictionary dictionary) : base(dictionary)
        {
            PdfObjectCollection objects = dictionary.Objects;
            this.defaultHalftone = dictionary.GetHalftone("Default");
            if (this.defaultHalftone == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.components = new Dictionary<string, PdfHalftone>();
            foreach (string str in dictionary.Keys)
            {
                if ((str != "Type") && ((str != "HalftoneType") && ((str != "HalftoneName") && (str != "Default"))))
                {
                    this.components[str] = dictionary.GetHalftone(str);
                }
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.Add("HalftoneType", 5);
            dictionary.Add("Default", this.defaultHalftone.CreateWriteableObject(objects));
            foreach (KeyValuePair<string, PdfHalftone> pair in this.components)
            {
                dictionary.Add(pair.Key, pair.Value.CreateWriteableObject(objects));
            }
            return dictionary;
        }

        protected internal override bool IsSame(PdfHalftone halftone)
        {
            bool flag;
            PdfSeparateHalftone halftone2 = halftone as PdfSeparateHalftone;
            if ((halftone2 == null) || (!base.IsSame(halftone) || !this.defaultHalftone.IsSame(halftone2.defaultHalftone)))
            {
                return false;
            }
            IDictionary<string, PdfHalftone> components = halftone2.components;
            if (this.components.Count != components.Count)
            {
                return false;
            }
            using (IEnumerator<KeyValuePair<string, PdfHalftone>> enumerator = this.components.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfHalftone halftone3;
                        KeyValuePair<string, PdfHalftone> current = enumerator.Current;
                        if (components.TryGetValue(current.Key, out halftone3) && current.Value.IsSame(halftone3))
                        {
                            continue;
                        }
                        flag = false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        public IDictionary<string, PdfHalftone> Components =>
            this.components;

        public PdfHalftone Default =>
            this.defaultHalftone;
    }
}

