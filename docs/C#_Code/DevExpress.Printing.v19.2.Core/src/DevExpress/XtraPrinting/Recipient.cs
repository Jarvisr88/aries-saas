namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    [ToolboxItem(false), DesignTimeVisible(false)]
    public class Recipient
    {
        private const RecipientFieldType defaultFieldType = RecipientFieldType.TO;
        private const string defaultAddressPrefix = "SMTP:";
        private string address;
        private string contactName;
        private string prefix;
        private RecipientFieldType type;

        public Recipient() : this("")
        {
        }

        private Recipient(Recipient recipient) : this(recipient.Address, recipient.ContactName, recipient.Prefix, recipient.FieldType)
        {
        }

        public Recipient(string address) : this(address, "")
        {
        }

        public Recipient(string address, RecipientFieldType type) : this(address, "", "SMTP:", type)
        {
        }

        public Recipient(string address, string contactName) : this(address, contactName, "SMTP:")
        {
        }

        public Recipient(string address, string contactName, RecipientFieldType type) : this(address, contactName, "SMTP:", type)
        {
        }

        public Recipient(string address, string contactName, string prefix) : this(address, contactName, prefix, RecipientFieldType.TO)
        {
        }

        public Recipient(string address, string contactName, string prefix, RecipientFieldType type)
        {
            this.prefix = "SMTP:";
            this.type = RecipientFieldType.TO;
            this.address = address;
            this.contactName = contactName;
            this.prefix = prefix;
            this.type = type;
        }

        internal Recipient Clone() => 
            new Recipient(this);

        internal void CorrectData()
        {
            if (!string.IsNullOrEmpty(this.address) && !this.HasRecipientPrefix)
            {
                this.address = this.prefix + this.address;
            }
            if (string.IsNullOrEmpty(this.contactName))
            {
                this.contactName = this.HasRecipientPrefix ? this.address.Substring(this.prefix.Length) : this.address;
            }
        }

        public override string ToString() => 
            this.EmptyNameAndAddress ? "Recipient" : $"{(this.EmptyName ? "" : (this.contactName + " "))}{(this.EmptyAddress ? "" : ("<" + this.address + ">"))}";

        [Description("Specifies the email recipient name."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Recipient.ContactName"), Category("Data"), XtraSerializableProperty]
        public string ContactName
        {
            get => 
                this.contactName;
            set => 
                this.contactName = value;
        }

        [Description("Specifies the recipient's email address."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Recipient.Address"), Category("Data"), XtraSerializableProperty]
        public string Address
        {
            get => 
                this.address;
            set => 
                this.address = value;
        }

        [Description("Specifies the email message prefix (e.g., \"SMTP:\" or \"Fax:\")."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Recipient.Prefix"), DefaultValue("SMTP:"), Category("Data"), XtraSerializableProperty]
        public string Prefix
        {
            get => 
                this.prefix;
            set => 
                this.prefix = value;
        }

        [Description("Specifies the email recipient type."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Recipient.FieldType"), XtraSerializableProperty, Category("Data"), DefaultValue(1)]
        public RecipientFieldType FieldType
        {
            get => 
                this.type;
            set => 
                this.type = value;
        }

        internal bool EmptyNameAndAddress =>
            this.EmptyName && this.EmptyAddress;

        private bool EmptyName =>
            string.IsNullOrEmpty(this.contactName);

        private bool EmptyAddress =>
            string.IsNullOrEmpty(this.address);

        private bool HasRecipientPrefix =>
            this.address.IndexOf(this.prefix) == 0;
    }
}

