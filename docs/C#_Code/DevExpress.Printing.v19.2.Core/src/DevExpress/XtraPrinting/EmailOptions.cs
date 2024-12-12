namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Linq;

    [TypeConverter(typeof(LocalizableExpandableObjectTypeConverter)), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.EmailOptions")]
    public class EmailOptions : IXtraSupportShouldSerialize, IXtraSupportDeserializeCollectionItem
    {
        private const string defaultAddressPrefix = "SMTP:";
        private string recipientName = string.Empty;
        private string recipientAddress = string.Empty;
        private string recipientAddressPrefix = "SMTP:";
        private string subject = string.Empty;
        private string body = string.Empty;
        private RecipientCollection recipients = new RecipientCollection();

        public void AddRecipient(Recipient recipient)
        {
            if (!this.recipients.Contains(recipient))
            {
                this.recipients.Add(recipient);
            }
        }

        public void Assign(EmailOptions source)
        {
            this.AdditionalRecipients.CopyFrom(source.AdditionalRecipients);
            this.recipientAddressPrefix = source.recipientAddressPrefix;
            this.subject = source.Subject;
            this.body = source.Body;
            this.recipientName = source.RecipientName;
            this.recipientAddress = source.RecipientAddress;
        }

        object IXtraSupportDeserializeCollectionItem.CreateCollectionItem(string propertyName, XtraItemEventArgs e) => 
            (propertyName != "AdditionalRecipients") ? null : new Recipient();

        void IXtraSupportDeserializeCollectionItem.SetIndexCollectionItem(string propertyName, XtraSetItemIndexEventArgs e)
        {
            if (propertyName == "AdditionalRecipients")
            {
                this.AdditionalRecipients.Add((Recipient) e.Item.Value);
            }
        }

        bool IXtraSupportShouldSerialize.ShouldSerialize(string propertyName) => 
            (propertyName != "AdditionalRecipients") || this.ShouldSerializeAdditionalRecipients();

        internal RecipientCollection GetAllRecipients()
        {
            Recipient item = new Recipient(this.recipientAddress, this.recipientName, this.recipientAddressPrefix, RecipientFieldType.TO);
            RecipientCollection collection1 = new RecipientCollection();
            collection1.Add(item);
            RecipientCollection recipients = collection1;
            recipients.AddRange(this.recipients.ToArray<Recipient>());
            return recipients;
        }

        public void InsertRecipient(int index, Recipient recipient)
        {
            if (!this.recipients.Contains(recipient))
            {
                this.recipients.Insert(index, recipient);
            }
        }

        internal bool ShouldSerialize() => 
            (this.recipientName != "") || ((this.recipientAddress != "") || ((this.recipientAddressPrefix != "SMTP:") || ((this.subject != "") || ((this.body != "") || this.ShouldSerializeAdditionalRecipients()))));

        private bool ShouldSerializeAdditionalRecipients() => 
            this.recipients.Count > 0;

        [Description("Gets or sets the text which is used as an e-mail's recipient name, when a document is exported and sent via e-mail from its Print Preview."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.EmailOptions.RecipientName"), DefaultValue(""), XtraSerializableProperty]
        public string RecipientName
        {
            get => 
                this.recipientName;
            set => 
                this.recipientName = value;
        }

        [Description("Gets or sets the text which is used as an e-mail's recipient address, when a document is exported and sent via e-mail from its Print Preview."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.EmailOptions.RecipientAddress"), DefaultValue(""), XtraSerializableProperty]
        public string RecipientAddress
        {
            get => 
                this.recipientAddress;
            set => 
                this.recipientAddress = value;
        }

        [Description("Gets or sets the prefix appended to the EmailOptions.RecipientAddress property value."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.EmailOptions.RecipientAddressPrefix"), DefaultValue("SMTP:"), XtraSerializableProperty]
        public string RecipientAddressPrefix
        {
            get => 
                this.recipientAddressPrefix;
            set => 
                this.recipientAddressPrefix = value;
        }

        [Description("Gets or sets the text which is used as an e-mail's subject, when a document is exported and sent via e-mail from its Print Preview."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.EmailOptions.Subject"), DefaultValue(""), XtraSerializableProperty]
        public string Subject
        {
            get => 
                this.subject;
            set => 
                this.subject = value;
        }

        [Description("Gets or sets the text which is appended to an e-mail as its body, when a document is exported and sent via e-mail from its Print Preview."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.EmailOptions.Body"), DefaultValue(""), XtraSerializableProperty, Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string Body
        {
            get => 
                this.body;
            set => 
                this.body = value;
        }

        [Description("Provides access to the collection of email recipients."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.EmailOptions.AdditionalRecipients"), XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, -1, XtraSerializationFlags.Cached), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Editor("DevExpress.XtraReports.Design.AdditionalRecipientsCollectionEditor, DevExpress.XtraReports.v19.2.Extensions, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public RecipientCollection AdditionalRecipients =>
            this.recipients;
    }
}

