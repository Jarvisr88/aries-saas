namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfSignatureBuildPropertiesAppData : PdfSignatureBuildPropertiesData
    {
        private readonly string implementationVersion;

        public PdfSignatureBuildPropertiesAppData(PdfReaderDictionary dictionary) : base(dictionary)
        {
            object obj2;
            if (dictionary.TryGetValue("REx", out obj2))
            {
                obj2 = dictionary.Objects.TryResolve(obj2, null);
                byte[] buffer = obj2 as byte[];
                if (buffer == null)
                {
                    PdfName name = obj2 as PdfName;
                    if (name == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.implementationVersion = name.Name;
                }
                else
                {
                    this.implementationVersion = PdfDocumentReader.ConvertToString(buffer);
                }
            }
        }

        public string ImplementationVersion =>
            this.implementationVersion;
    }
}

