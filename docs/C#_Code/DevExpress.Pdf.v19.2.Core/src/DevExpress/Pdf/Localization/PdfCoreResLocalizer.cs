namespace DevExpress.Pdf.Localization
{
    using System;
    using System.Reflection;
    using System.Resources;

    public class PdfCoreResLocalizer : PdfCoreLocalizer
    {
        private const string baseName = "DevExpress.Pdf.LocalizationRes";
        private ResourceManager manager;

        public PdfCoreResLocalizer()
        {
            this.manager = new ResourceManager("DevExpress.Pdf.LocalizationRes", GetAssembly(base.GetType()));
        }

        private static Assembly GetAssembly(Type type) => 
            type.Assembly;

        public override string GetLocalizedString(PdfCoreStringId id) => 
            this.manager.GetString("PdfCoreStringId." + id) ?? string.Empty;
    }
}

