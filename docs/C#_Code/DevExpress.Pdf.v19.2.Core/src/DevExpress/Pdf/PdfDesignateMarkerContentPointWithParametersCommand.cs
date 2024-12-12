namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfDesignateMarkerContentPointWithParametersCommand : PdfDesignateMarkedContentPointCommand
    {
        internal const string Name = "DP";
        private readonly PdfProperties properties;

        internal PdfDesignateMarkerContentPointWithParametersCommand(PdfResources resources, PdfStack operands)
        {
            object obj2 = operands.Pop(true);
            PdfReaderDictionary dictionary = obj2 as PdfReaderDictionary;
            if (dictionary != null)
            {
                this.properties = PdfProperties.ParseProperties(dictionary);
            }
            else
            {
                PdfName name = obj2 as PdfName;
                if (name == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                string str = name.Name;
                if (string.IsNullOrEmpty(str))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.properties = resources.GetProperties(str);
                if (this.properties == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            base.ParseTag(operands);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteName(new PdfName(base.Tag));
            writer.WriteSpace();
            PdfName name1 = resources.FindPropertiesName(this.properties);
            PdfName name2 = name1;
            if (name1 == null)
            {
                PdfName local1 = name1;
                name2 = (PdfName) this.properties.ToWritableObject(null);
            }
            writer.WriteObject(name2, -1);
            writer.WriteSpace();
            writer.WriteString("DP");
        }

        public PdfProperties Properties =>
            this.properties;
    }
}

