namespace DevExpress.Pdf
{
    using System;

    public class PdfFileAttachment
    {
        private readonly PdfFileSpecification fileSpecification;

        public PdfFileAttachment() : this(new PdfFileSpecification(string.Empty))
        {
        }

        internal PdfFileAttachment(PdfFileSpecification fileSpecification)
        {
            this.fileSpecification = fileSpecification;
            fileSpecification.Attachment = this;
        }

        internal PdfFileSpecification FileSpecification =>
            this.fileSpecification;

        public DateTimeOffset? CreationDate
        {
            get => 
                this.fileSpecification.CreationDate;
            set => 
                this.fileSpecification.CreationDate = value;
        }

        public DateTimeOffset? ModificationDate
        {
            get => 
                this.fileSpecification.ModificationDate;
            set => 
                this.fileSpecification.ModificationDate = value;
        }

        public string MimeType
        {
            get => 
                this.fileSpecification.MimeType;
            set => 
                this.fileSpecification.MimeType = value;
        }

        public byte[] Data
        {
            get => 
                this.fileSpecification.FileData;
            set => 
                this.fileSpecification.FileData = value;
        }

        public int Size =>
            this.fileSpecification.Size;

        public string FileName
        {
            get => 
                this.fileSpecification.FileName;
            set => 
                this.fileSpecification.FileName = value;
        }

        public PdfAssociatedFileRelationship Relationship
        {
            get => 
                this.fileSpecification.Relationship;
            set => 
                this.fileSpecification.Relationship = value;
        }

        public string Description
        {
            get => 
                this.fileSpecification.Description;
            set => 
                this.fileSpecification.Description = value;
        }
    }
}

