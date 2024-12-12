namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(LocalizableExpandableObjectTypeConverter)), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.DocxDocumentOptions")]
    public class DocxDocumentOptions : ICloneable
    {
        private string title = string.Empty;
        private string subject = string.Empty;
        private string keywords = string.Empty;
        private string category = string.Empty;
        private string comments = string.Empty;
        private string author = string.Empty;

        public void Assign(DocxDocumentOptions documentOptions)
        {
            if (documentOptions == null)
            {
                throw new ArgumentNullException("documentOptions");
            }
            this.title = documentOptions.title;
            this.subject = documentOptions.subject;
            this.keywords = documentOptions.keywords;
            this.category = documentOptions.category;
            this.comments = documentOptions.comments;
            this.author = documentOptions.author;
        }

        public object Clone()
        {
            DocxDocumentOptions options = new DocxDocumentOptions();
            options.Assign(this);
            return options;
        }

        internal bool ShouldSerialize() => 
            (this.title != string.Empty) || ((this.subject != string.Empty) || ((this.keywords != string.Empty) || ((this.category != string.Empty) || ((this.comments != string.Empty) || (this.author != string.Empty)))));

        [Description("Specifies the document title."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.DocxDocumentOptions.Title"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Title
        {
            get => 
                this.title;
            set => 
                this.title = value;
        }

        [Description("Specifies the document subject."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.DocxDocumentOptions.Subject"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Subject
        {
            get => 
                this.subject;
            set => 
                this.subject = value;
        }

        [Description("Specifies the document keywords."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.DocxDocumentOptions.Keywords"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Keywords
        {
            get => 
                this.keywords;
            set => 
                this.keywords = value;
        }

        [Description("Specifies the document category."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.DocxDocumentOptions.Category"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Category
        {
            get => 
                this.category;
            set => 
                this.category = value;
        }

        [Description("Specifies the document comments."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.DocxDocumentOptions.Comments"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Comments
        {
            get => 
                this.comments;
            set => 
                this.comments = value;
        }

        [Description("Specifies the document author."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.DocxDocumentOptions.Author"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Author
        {
            get => 
                this.author;
            set => 
                this.author = value;
        }
    }
}

