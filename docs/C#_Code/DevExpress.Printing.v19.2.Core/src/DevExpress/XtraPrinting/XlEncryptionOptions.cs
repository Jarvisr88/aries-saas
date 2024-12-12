namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(LocalizableExpandableObjectTypeConverter)), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlEncryptionOptions")]
    public class XlEncryptionOptions
    {
        private const XlEncryptionType DefaultType = XlEncryptionType.Strong;
        private const string DefaultPassword = "";
        private XlEncryptionType type = XlEncryptionType.Strong;
        private string password = "";

        internal bool ShouldSerialize() => 
            (this.type != XlEncryptionType.Strong) || (this.password != "");

        [Description("Specifies the applied encryption mechanism."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlEncryptionOptions.Type"), DefaultValue(2), XtraSerializableProperty]
        public XlEncryptionType Type
        {
            get => 
                this.type;
            set => 
                this.type = value;
        }

        [Description("Specifies the password to open the file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlEncryptionOptions.Password"), DefaultValue(""), XtraSerializableProperty]
        public string Password
        {
            get => 
                this.password;
            set => 
                this.password = value;
        }
    }
}

