namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfPrintFieldLogicalStructureElementAttribute : PdfLogicalStructureElementAttribute
    {
        internal const string Owner = "PrintField";
        private const string roleKey = "Role";
        private const string checkedKed = "checked";
        private const string nameKey = "Desc";
        private readonly PdfPrintFieldLogicalStructureElementAttributeRole? role;
        private readonly PdfPrintFieldLogicalStructureElementAttributeRadioButtonState radioButtonSate;
        private readonly string name;

        internal PdfPrintFieldLogicalStructureElementAttribute(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.radioButtonSate = PdfPrintFieldLogicalStructureElementAttributeRadioButtonState.Off;
            this.role = new PdfPrintFieldLogicalStructureElementAttributeRole?(PdfEnumToStringConverter.Parse<PdfPrintFieldLogicalStructureElementAttributeRole>(dictionary.GetName("Role"), true));
            this.radioButtonSate = PdfEnumToStringConverter.Parse<PdfPrintFieldLogicalStructureElementAttributeRadioButtonState>(dictionary.GetName("checked"), true);
            this.name = dictionary.GetString("Desc");
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.AddName("O", "PrintField");
            if (this.role != null)
            {
                dictionary.AddEnumName<PdfPrintFieldLogicalStructureElementAttributeRole>("Role", this.role.Value);
            }
            dictionary.AddEnumName<PdfPrintFieldLogicalStructureElementAttributeRadioButtonState>("checked", this.radioButtonSate);
            dictionary.AddNotNullOrEmptyString("Desc", this.name);
            return dictionary;
        }
    }
}

