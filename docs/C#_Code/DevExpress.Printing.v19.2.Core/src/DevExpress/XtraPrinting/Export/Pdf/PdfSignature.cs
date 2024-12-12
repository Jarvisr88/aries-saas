namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    public class PdfSignature : PdfDocumentDictionaryObject
    {
        private PdfSignatureContentString content;
        private PdfSignatureByteRange byteRange;

        public PdfSignature() : base(false)
        {
        }

        private PdfObject CreatePdfString(string s) => 
            (Encoding.UTF8.GetByteCount(s) == s.Length) ? ((PdfObject) new PdfLiteralString(s)) : ((PdfObject) new PdfTextUnicode(s));

        public override void FillUp()
        {
            if ((this.content != null) && (this.byteRange != null))
            {
                base.Dictionary.Add("Type", "Sig");
                base.Dictionary.Add("Filter", "Adobe.PPKMS");
                base.Dictionary.Add("SubFilter", "adbe.pkcs7.sha1");
                if (!string.IsNullOrEmpty(this.Reason))
                {
                    base.Dictionary.Add("Reason", this.CreatePdfString(this.Reason));
                }
                if (!string.IsNullOrEmpty(this.Location))
                {
                    base.Dictionary.Add("Location", this.CreatePdfString(this.Location));
                }
                if (!string.IsNullOrEmpty(this.ContactInfo))
                {
                    base.Dictionary.Add("ContactInfo", this.CreatePdfString(this.ContactInfo));
                }
                base.Dictionary.Add("M", new PdfDate(DateTime.Now));
                base.Dictionary.Add("Contents", this.content);
                base.Dictionary.Add("ByteRange", this.byteRange);
            }
        }

        public void Finish(StreamWriter writer)
        {
            if ((this.content != null) && (this.byteRange != null))
            {
                this.byteRange.Patch(writer, this.content.Offset, this.content.Length);
                this.content.Patch(writer);
            }
        }

        public string Reason { get; set; }

        public string Location { get; set; }

        public string ContactInfo { get; set; }

        public X509Certificate2 Certificate
        {
            set
            {
                if (value != null)
                {
                    this.content = new PdfSignatureContentString(value);
                    this.byteRange = new PdfSignatureByteRange();
                }
            }
        }

        public bool Active =>
            this.content != null;
    }
}

