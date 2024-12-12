namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(LocalizableExpandableObjectTypeConverter)), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XpsDocumentOptions")]
    public class XpsDocumentOptions : ICloneable
    {
        private string creator = string.Empty;
        private string category = string.Empty;
        private string title = string.Empty;
        private string subject = string.Empty;
        private string keywords = string.Empty;
        private string version = string.Empty;
        private string description = string.Empty;

        public void Assign(XpsDocumentOptions xpsDocumentOptions)
        {
            if (xpsDocumentOptions == null)
            {
                throw new ArgumentNullException("xpsDocumentOptions");
            }
            this.creator = xpsDocumentOptions.creator;
            this.category = xpsDocumentOptions.category;
            this.title = xpsDocumentOptions.title;
            this.subject = xpsDocumentOptions.subject;
            this.keywords = xpsDocumentOptions.keywords;
            this.version = xpsDocumentOptions.version;
            this.description = xpsDocumentOptions.description;
        }

        public object Clone()
        {
            XpsDocumentOptions options = new XpsDocumentOptions();
            options.Assign(this);
            return options;
        }

        internal bool ShouldSerialize() => 
            (this.creator != string.Empty) || ((this.category != string.Empty) || ((this.title != string.Empty) || ((this.subject != string.Empty) || ((this.keywords != string.Empty) || ((this.version != string.Empty) || (this.description != string.Empty))))));

        [Description("Gets or sets the string to be added as a Creator property of the resulting XPS file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XpsDocumentOptions.Creator"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Creator
        {
            get => 
                this.creator;
            set => 
                this.creator = value;
        }

        [Description("Gets or sets the string to be added as a Category property of the resulting XPS file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XpsDocumentOptions.Category"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Category
        {
            get => 
                this.category;
            set => 
                this.category = value;
        }

        [Description("Gets or sets the string to be added as a Title property of the resulting XPS file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XpsDocumentOptions.Title"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Title
        {
            get => 
                this.title;
            set => 
                this.title = value;
        }

        [Description("Gets or sets the string to be added as a Subject property of the resulting XPS file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XpsDocumentOptions.Subject"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Subject
        {
            get => 
                this.subject;
            set => 
                this.subject = value;
        }

        [Description("Gets or sets the string to be added as a Keywords property of the resulting XPS file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XpsDocumentOptions.Keywords"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Keywords
        {
            get => 
                this.keywords;
            set => 
                this.keywords = value;
        }

        [Description("Gets or sets the string to be added as a Version property of the resulting XPS file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XpsDocumentOptions.Version"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Version
        {
            get => 
                this.version;
            set => 
                this.version = value;
        }

        [Description("Gets or sets the string to be added as a Description property of the resulting XPS file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XpsDocumentOptions.Description"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string Description
        {
            get => 
                this.description;
            set => 
                this.description = value;
        }
    }
}

