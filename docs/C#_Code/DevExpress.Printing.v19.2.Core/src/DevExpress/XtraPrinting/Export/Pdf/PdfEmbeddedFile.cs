namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Runtime.CompilerServices;

    public class PdfEmbeddedFile : PdfDocumentStreamObject
    {
        public PdfEmbeddedFile(bool compressed) : base(compressed)
        {
        }

        public override void FillUp()
        {
            base.FillUp();
            base.Attributes.Add("Type", "EmbeddedFile");
            base.Attributes.Add("Subtype", this.Subtype);
            PdfDictionary dictionary = new PdfDictionary();
            if (this.CreationDate != null)
            {
                dictionary.Add("CreationDate", new PdfDate(this.CreationDate.Value));
            }
            if (this.ModificationDate != null)
            {
                dictionary.Add("ModDate", new PdfDate(this.ModificationDate.Value));
            }
            base.Attributes.Add("Params", dictionary);
            base.Stream.SetBytes(this.Data);
        }

        public string Subtype { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }

        public byte[] Data { get; set; }
    }
}

