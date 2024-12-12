namespace DevExpress.Office.Localization
{
    using DevExpress.Utils.Localization;
    using System;
    using System.Resources;

    public class OfficeResLocalizer : XtraResXLocalizer<OfficeStringId>
    {
        public OfficeResLocalizer() : base(new OfficeLocalizer())
        {
        }

        protected override ResourceManager CreateResourceManagerCore() => 
            new ResourceManager("DevExpress.Office.LocalizationRes", typeof(OfficeResLocalizer).Assembly);
    }
}

