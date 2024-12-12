namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfMarkedContentCommand : PdfCommandGroup
    {
        internal const string EndToken = "EMC";
        private readonly string tag;
        private readonly PdfProperties properties;

        private PdfMarkedContentCommand(string tag)
        {
            this.tag = tag;
        }

        private PdfMarkedContentCommand(string tag, PdfProperties properties) : this(tag)
        {
            this.properties = properties;
        }

        protected override IEnumerable<object> GetPrefix(PdfResources resources)
        {
            List<object> list = new List<object> {
                new PdfName(this.tag)
            };
            if (this.properties == null)
            {
                list.Add(new PdfToken("BMC"));
            }
            else
            {
                PdfName name1 = resources.FindPropertiesName(this.properties);
                PdfName item = name1;
                if (name1 == null)
                {
                    PdfName local1 = name1;
                    item = (PdfName) this.properties.ToWritableObject(null);
                }
                list.Add(item);
                list.Add(new PdfToken("BDC"));
            }
            return list;
        }

        internal static PdfMarkedContentCommand Parse(PdfStack operands) => 
            new PdfMarkedContentCommand(ParseTag(operands));

        internal static PdfMarkedContentCommand Parse(PdfStack operands, PdfResources resources)
        {
            PdfProperties properties;
            object obj2 = operands.Pop(true);
            PdfReaderDictionary dictionary = obj2 as PdfReaderDictionary;
            if (dictionary != null)
            {
                properties = PdfProperties.ParseProperties(dictionary);
            }
            else
            {
                PdfName name = obj2 as PdfName;
                if (name == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                properties = resources.GetProperties(name.Name);
            }
            return new PdfMarkedContentCommand(ParseTag(operands), properties);
        }

        private static string ParseTag(PdfStack operands) => 
            operands.PopName(true) ?? "";

        public string Tag =>
            this.tag;

        public PdfProperties Properties =>
            this.properties;

        protected override string Suffix =>
            "EMC";
    }
}

