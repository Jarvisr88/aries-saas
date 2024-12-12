namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography.X509Certificates;

    [TypeConverter(typeof(PdfSignatureOptions.PdfSignatureOptionsTypeConverter)), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfSignatureOptions"), Editor("DevExpress.XtraPrinting.Design.PdfSignatureOptionsEditor, DevExpress.XtraPrinting.v19.2", typeof(UITypeEditor)), Editor("DevExpress.Xpf.Printing.Native.PdfSignatureOptionsEditor, DevExpress.Xpf.Printing.v19.2", typeof(IDXTypeEditor))]
    public class PdfSignatureOptions : ICloneable
    {
        public void Assign(PdfSignatureOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }
            this.Certificate = options.Certificate;
            this.Reason = options.Reason;
            this.Location = options.Location;
            this.ContactInfo = options.ContactInfo;
        }

        public object Clone()
        {
            PdfSignatureOptions options = new PdfSignatureOptions();
            options.Assign(this);
            return options;
        }

        public override bool Equals(object obj)
        {
            PdfSignatureOptions options = obj as PdfSignatureOptions;
            return ((options != null) ? ((this.Reason == options.Reason) && ((this.Location == options.Location) && ((this.ContactInfo == options.ContactInfo) && Equals(this.Certificate, options.Certificate)))) : false);
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        internal bool ShouldSerialize() => 
            !string.IsNullOrEmpty(this.Reason) || (!string.IsNullOrEmpty(this.Location) || !string.IsNullOrEmpty(this.ContactInfo));

        [DefaultValue(""), XtraSerializableProperty]
        public string Reason { get; set; }

        [DefaultValue(""), XtraSerializableProperty]
        public string Location { get; set; }

        [DefaultValue(""), XtraSerializableProperty]
        public string ContactInfo { get; set; }

        [Browsable(false), Description("Specifies an X.509 certificate of PdfSignatureOptions.")]
        public X509Certificate2 Certificate { get; set; }

        public class PdfSignatureOptionsTypeConverter : LocalizableExpandableObjectTypeConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                PdfSignatureOptions options = value as PdfSignatureOptions;
                if (!(destinationType == typeof(string)) || (options == null))
                {
                    return base.ConvertTo(context, culture, value, destinationType);
                }
                List<string> list = new List<string>();
                if (options.Certificate != null)
                {
                    list.Add(PreviewLocalizer.GetString(PreviewStringId.ExportOption_PdfSignatureOptions_Certificate));
                }
                if (!string.IsNullOrEmpty(options.Reason))
                {
                    list.Add(PreviewLocalizer.GetString(PreviewStringId.ExportOption_PdfSignatureOptions_Reason));
                }
                if (!string.IsNullOrEmpty(options.Location))
                {
                    list.Add(PreviewLocalizer.GetString(PreviewStringId.ExportOption_PdfSignatureOptions_Location));
                }
                if (!string.IsNullOrEmpty(options.ContactInfo))
                {
                    list.Add(PreviewLocalizer.GetString(PreviewStringId.ExportOption_PdfSignatureOptions_ContactInfo));
                }
                if (list.Count == 0)
                {
                    list.Add(PreviewLocalizer.GetString(PreviewStringId.ExportOption_PdfSignatureOptions_None));
                }
                return StringUtils.Join("; ", list);
            }

            public override bool GetPropertiesSupported(ITypeDescriptorContext context) => 
                false;
        }
    }
}

