namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [TypeConverter(typeof(LocalizableExpandableObjectTypeConverter)), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfDocumentOptions")]
    public class PdfDocumentOptions : ICloneable
    {
        private const string productName = "Developer Express Inc. DXperience (tm)";
        private string producer = string.Empty;
        private string author = string.Empty;
        private string application = string.Empty;
        private string title = string.Empty;
        private string subject = string.Empty;
        private string keywords = string.Empty;

        static PdfDocumentOptions()
        {
            string str = "19.2.9.0";
            str = "v" + str.Remove(str.Length - 2, 2);
            DefaultProducer = "Developer Express Inc. DXperience (tm) " + str;
        }

        public void Assign(PdfDocumentOptions documentOptions)
        {
            if (documentOptions == null)
            {
                throw new ArgumentNullException("documentOptions");
            }
            this.producer = documentOptions.producer;
            this.author = documentOptions.author;
            this.application = documentOptions.application;
            this.title = documentOptions.title;
            this.subject = documentOptions.subject;
            this.keywords = documentOptions.keywords;
        }

        public object Clone()
        {
            PdfDocumentOptions options = new PdfDocumentOptions();
            options.Assign(this);
            return options;
        }

        internal bool ShouldSerialize() => 
            (this.author != string.Empty) || ((this.application != string.Empty) || ((this.title != string.Empty) || ((this.subject != string.Empty) || (this.keywords != string.Empty))));

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static string DefaultProducer { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public string Producer
        {
            get => 
                this.producer;
            set => 
                this.producer = value;
        }

        internal string ActualProducer =>
            string.IsNullOrEmpty(this.producer) ? DefaultProducer : this.producer;

        [Description("Gets or sets the string to be added as an Author property of the resulting PDF file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfDocumentOptions.Author"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Author
        {
            get => 
                this.author;
            set => 
                this.author = value;
        }

        [Description("Gets or sets the string to be added as an Application property of the resulting PDF file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfDocumentOptions.Application"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Application
        {
            get => 
                this.application;
            set => 
                this.application = value;
        }

        [Description("Gets or sets the string to be added as a Title property of the resulting PDF file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfDocumentOptions.Title"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Title
        {
            get => 
                this.title;
            set => 
                this.title = value;
        }

        [Description("Gets or sets the string to be added as a Subject property of the resulting PDF file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfDocumentOptions.Subject"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Subject
        {
            get => 
                this.subject;
            set => 
                this.subject = value;
        }

        [Description("Gets or sets the string to be added as a Keywords property of the resulting PDF file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfDocumentOptions.Keywords"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Keywords
        {
            get => 
                this.keywords;
            set => 
                this.keywords = value;
        }
    }
}

