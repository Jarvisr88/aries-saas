namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(LocalizableExpandableObjectTypeConverter)), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlDocumentOptions")]
    public class XlDocumentOptions : ICloneable
    {
        private string title = string.Empty;
        private string subject = string.Empty;
        private string tags = string.Empty;
        private string category = string.Empty;
        private string comments = string.Empty;
        private string author = string.Empty;
        private string application = string.Empty;
        private string company = string.Empty;

        public void Assign(XlDocumentOptions documentOptions)
        {
            if (documentOptions == null)
            {
                throw new ArgumentNullException("documentOptions");
            }
            this.title = documentOptions.title;
            this.subject = documentOptions.subject;
            this.tags = documentOptions.tags;
            this.category = documentOptions.category;
            this.comments = documentOptions.comments;
            this.author = documentOptions.author;
            this.application = documentOptions.application;
            this.company = documentOptions.company;
        }

        public object Clone()
        {
            XlDocumentOptions options = new XlDocumentOptions();
            options.Assign(this);
            return options;
        }

        internal bool ShouldSerialize() => 
            (this.title != string.Empty) || ((this.subject != string.Empty) || ((this.tags != string.Empty) || ((this.category != string.Empty) || ((this.comments != string.Empty) || ((this.author != string.Empty) || ((this.application != string.Empty) || (this.company != string.Empty)))))));

        [Description("Specifies a string to be added as the Title property of the resulting XLS file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlDocumentOptions.Title"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Title
        {
            get => 
                this.title;
            set => 
                this.title = value;
        }

        [Description("Specifies a string to be added as the Subject property of the resulting XLS file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlDocumentOptions.Subject"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Subject
        {
            get => 
                this.subject;
            set => 
                this.subject = value;
        }

        [Description("Specifies a string to be added as the Tags property of the resulting XLS file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlDocumentOptions.Tags"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Tags
        {
            get => 
                this.tags;
            set => 
                this.tags = value;
        }

        [Description("Specifies a string to be added as the Categories property of the resulting XLS file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlDocumentOptions.Category"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Category
        {
            get => 
                this.category;
            set => 
                this.category = value;
        }

        [Description("Specifies a string to be added as the Comments property of the resulting XLS file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlDocumentOptions.Comments"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Comments
        {
            get => 
                this.comments;
            set => 
                this.comments = value;
        }

        [Description("Specifies a string to be added as the Authors property of the resulting XLS file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlDocumentOptions.Author"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Author
        {
            get => 
                this.author;
            set => 
                this.author = value;
        }

        [Description("Specifies a string to be added as the Program name property of the resulting XLS file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlDocumentOptions.Application"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Application
        {
            get => 
                this.application;
            set => 
                this.application = value;
        }

        [Description("Specifies a string to be added as the Company property of the resulting XLS file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlDocumentOptions.Company"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Company
        {
            get => 
                this.company;
            set => 
                this.company = value;
        }
    }
}

