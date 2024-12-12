namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;

    public class PdfFilespec : PdfDocumentDictionaryObject
    {
        public PdfFilespec() : base(false)
        {
        }

        public override void FillUp()
        {
            base.FillUp();
            base.Dictionary.Add("Type", "Filespec");
            base.Dictionary.Add("F", new PdfLiteralString(this.Name));
            base.Dictionary.Add("UF", new PdfTextUnicode(this.Name));
            if (this.Description != null)
            {
                base.Dictionary.Add("Desc", new PdfTextUnicode(this.Description));
            }
            base.Dictionary.Add("AFRelationship", this.Relationship.ToString());
            PdfDictionary dictionary = new PdfDictionary();
            dictionary.Add("F", this.EmbeddedFile.InnerObject);
            dictionary.Add("UF", this.EmbeddedFile.InnerObject);
            base.Dictionary.Add("EF", dictionary);
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public PdfAttachmentRelationship Relationship { get; set; }

        public PdfEmbeddedFile EmbeddedFile { get; set; }
    }
}

