namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class PdfSoftMask : PdfObject
    {
        internal const string SoftMaskTypeDictionaryKey = "S";
        internal const string DictionaryType = "Mask";

        protected PdfSoftMask(PdfObjectCollection objects)
        {
        }

        internal static PdfSoftMask Create(PdfObjectCollection objects, object value)
        {
            Func<object, PdfSoftMask> create = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<object, PdfSoftMask> local1 = <>c.<>9__2_0;
                create = <>c.<>9__2_0 = delegate (object o) {
                    PdfName name = o as PdfName;
                    if (name != null)
                    {
                        if (name.Name != "None")
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        return PdfEmptySoftMask.Instance;
                    }
                    PdfReaderDictionary dictionary = o as PdfReaderDictionary;
                    if (dictionary == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    string str = dictionary.GetName("S");
                    if (str == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    if (str == "Luminosity")
                    {
                        return new PdfLuminositySoftMask(dictionary);
                    }
                    if (str == "Alpha")
                    {
                        return new PdfAlphaSoftMask(dictionary);
                    }
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                    return null;
                };
            }
            return objects.GetObject<PdfSoftMask>(value, create);
        }

        protected internal abstract bool IsSame(PdfSoftMask softMask);
        protected internal abstract object Write(PdfObjectCollection objects);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfSoftMask.<>c <>9 = new PdfSoftMask.<>c();
            public static Func<object, PdfSoftMask> <>9__2_0;

            internal PdfSoftMask <Create>b__2_0(object o)
            {
                PdfName name = o as PdfName;
                if (name != null)
                {
                    if (name.Name != "None")
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    return PdfEmptySoftMask.Instance;
                }
                PdfReaderDictionary dictionary = o as PdfReaderDictionary;
                if (dictionary == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                string str = dictionary.GetName("S");
                if (str == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                if (str == "Luminosity")
                {
                    return new PdfLuminositySoftMask(dictionary);
                }
                if (str == "Alpha")
                {
                    return new PdfAlphaSoftMask(dictionary);
                }
                PdfDocumentStructureReader.ThrowIncorrectDataException();
                return null;
            }
        }
    }
}

