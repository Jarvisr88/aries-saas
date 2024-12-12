namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Globalization;

    [TypeConverter(typeof(PdfPasswordSecurityOptions.PdfPasswordSecurityOptionsTypeConverter)), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfPasswordSecurityOptions"), Editor("DevExpress.XtraPrinting.Design.PdfPasswordSecurityOptionsEditor, DevExpress.XtraPrinting.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor)), Editor("DevExpress.Xpf.Printing.Native.PdfPasswordSecurityOptionsEditor, DevExpress.Xpf.Printing.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(IDXTypeEditor))]
    public class PdfPasswordSecurityOptions : ICloneable, IXtraSupportShouldSerialize
    {
        private PdfPermissionsOptions permissionsOptions = new PdfPermissionsOptions();
        private string permissionsPassword = string.Empty;
        private string openPassword = string.Empty;
        private PdfEncryptionLevel encryptionLevel;

        public void Assign(PdfPasswordSecurityOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }
            this.permissionsPassword = options.permissionsPassword;
            this.openPassword = options.openPassword;
            this.encryptionLevel = options.EncryptionLevel;
            this.permissionsOptions.Assign(options.permissionsOptions);
        }

        public object Clone()
        {
            PdfPasswordSecurityOptions options = new PdfPasswordSecurityOptions();
            options.Assign(this);
            return options;
        }

        bool IXtraSupportShouldSerialize.ShouldSerialize(string propertyName) => 
            (propertyName != "PermissionsOptions") || this.ShouldSerializePermissionsOptions();

        public override bool Equals(object obj)
        {
            PdfPasswordSecurityOptions options = obj as PdfPasswordSecurityOptions;
            return ((options != null) ? ((this.permissionsPassword == options.permissionsPassword) && ((this.openPassword == options.openPassword) && ((this.encryptionLevel == options.encryptionLevel) && this.permissionsOptions.Equals(options.PermissionsOptions)))) : false);
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        internal bool ShouldSerialize() => 
            (this.permissionsPassword != string.Empty) || ((this.openPassword != string.Empty) || ((this.encryptionLevel != PdfEncryptionLevel.AES128) || this.ShouldSerializePermissionsOptions()));

        private bool ShouldSerializePermissionsOptions() => 
            this.permissionsOptions.ShouldSerialize();

        [Description("Specifies the PDF permissions password for the document."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfPasswordSecurityOptions.PermissionsPassword"), DefaultValue(""), Localizable(true), RefreshProperties(RefreshProperties.All), XtraSerializableProperty]
        public string PermissionsPassword
        {
            get => 
                this.permissionsPassword;
            set => 
                this.permissionsPassword = value;
        }

        [Description("Specifies the password for opening the exported PDF document."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfPasswordSecurityOptions.OpenPassword"), DefaultValue(""), Localizable(true), XtraSerializableProperty]
        public string OpenPassword
        {
            get => 
                this.openPassword;
            set => 
                this.openPassword = value;
        }

        [Description("Specifies the algorithm used to encrypt PDF content."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfPasswordSecurityOptions.EncryptionLevel"), DefaultValue(0), Localizable(true), XtraSerializableProperty]
        public PdfEncryptionLevel EncryptionLevel
        {
            get => 
                this.encryptionLevel;
            set => 
                this.encryptionLevel = value;
        }

        [Description("Provides access to the PDF permission options of the document."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfPasswordSecurityOptions.PermissionsOptions"), Localizable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public PdfPermissionsOptions PermissionsOptions =>
            this.permissionsOptions;

        public class PdfPasswordSecurityOptionsTypeConverter : LocalizableExpandableObjectTypeConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if ((context != null) && (context.Instance != null))
                {
                    PdfExportOptions instance = context.Instance as PdfExportOptions;
                    if ((instance != null) && ((instance.PdfACompatibility != PdfACompatibility.None) && (destinationType == typeof(string))))
                    {
                        return "(None)";
                    }
                    PdfPasswordSecurityOptions options2 = value as PdfPasswordSecurityOptions;
                    if ((destinationType == typeof(string)) && (options2 != null))
                    {
                        if (string.IsNullOrEmpty(options2.OpenPassword) && string.IsNullOrEmpty(options2.PermissionsPassword))
                        {
                            return PreviewLocalizer.GetString(PreviewStringId.ExportOption_PdfPasswordSecurityOptions_None);
                        }
                        string str = string.IsNullOrEmpty(options2.OpenPassword) ? string.Empty : PreviewLocalizer.GetString(PreviewStringId.ExportOption_PdfPasswordSecurityOptions_DocumentOpenPassword);
                        return StringUtils.Join("; ", str, string.IsNullOrEmpty(options2.PermissionsPassword) ? string.Empty : PreviewLocalizer.GetString(PreviewStringId.ExportOption_PdfPasswordSecurityOptions_Permissions));
                    }
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }

            public override bool GetPropertiesSupported(ITypeDescriptorContext context) => 
                false;
        }
    }
}

