namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class PdfInfo : PdfDocumentDictionaryObject
    {
        private string producer;
        private string author;
        private string application;
        private string title;
        private string subject;
        private string keywords;
        private DateTime creationDate;

        public PdfInfo(bool compressed) : base(compressed)
        {
            this.producer = string.Empty;
            this.author = string.Empty;
            this.application = string.Empty;
            this.title = string.Empty;
            this.subject = string.Empty;
            this.keywords = string.Empty;
        }

        public override void FillUp()
        {
            this.FillUpValue("Producer", this.producer);
            this.FillUpValue("Author", this.author);
            this.FillUpValue("Creator", this.application);
            this.FillUpValue("Title", this.title);
            this.FillUpValue("Subject", this.subject);
            this.FillUpValue("Keywords", this.keywords);
            base.Dictionary.Add("CreationDate", new PdfDate(this.creationDate));
        }

        private void FillUpValue(string name, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                base.Dictionary.Add(name, new PdfTextUnicode(value));
            }
        }

        public string Producer
        {
            get => 
                this.producer;
            set => 
                this.producer = value;
        }

        public string Author
        {
            get => 
                this.author;
            set => 
                this.author = value;
        }

        public string Application
        {
            get => 
                this.application;
            set => 
                this.application = value;
        }

        public string Title
        {
            get => 
                this.title;
            set => 
                this.title = value;
        }

        public string Subject
        {
            get => 
                this.subject;
            set => 
                this.subject = value;
        }

        public string Keywords
        {
            get => 
                this.keywords;
            set => 
                this.keywords = value;
        }

        public DateTime CreationDate
        {
            get => 
                this.creationDate;
            set => 
                this.creationDate = value;
        }
    }
}

