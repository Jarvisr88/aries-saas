namespace DevExpress.XtraPrinting
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class PdfAttachment
    {
        internal PdfAttachment Clone()
        {
            PdfAttachment attachment1 = new PdfAttachment();
            attachment1.CreationDate = this.CreationDate;
            attachment1.ModificationDate = this.ModificationDate;
            attachment1.Type = this.Type;
            attachment1.Relationship = this.Relationship;
            attachment1.Data = this.Data;
            attachment1.FileName = this.FileName;
            attachment1.Description = this.Description;
            attachment1.FilePath = this.FilePath;
            return attachment1;
        }

        [Description("Specifies the date of the attachment file creation.")]
        public DateTime? CreationDate { get; set; }

        [Description("Specifies the date of the attachment file's last modification.")]
        public DateTime? ModificationDate { get; set; }

        [Description("Specifies the data type of the attachment file.")]
        public string Type { get; set; }

        [Description("Specifies the relation between the document and the attachment file.")]
        public PdfAttachmentRelationship Relationship { get; set; }

        [Description("Specifies the document's attachment file as a byte array.")]
        public byte[] Data { get; set; }

        [Description("Specifies the name of the attachment file.")]
        public string FileName { get; set; }

        [Description("Specifies the attachment file's description.")]
        public string Description { get; set; }

        [Description("Specifies the path to the file to be attached to the document.")]
        public string FilePath { get; set; }
    }
}

