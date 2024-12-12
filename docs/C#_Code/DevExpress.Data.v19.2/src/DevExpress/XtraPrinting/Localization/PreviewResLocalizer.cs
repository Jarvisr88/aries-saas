namespace DevExpress.XtraPrinting.Localization
{
    using DevExpress.Data;
    using DevExpress.Utils.Localization;
    using System;
    using System.ComponentModel;
    using System.Resources;

    [ToolboxItem(false)]
    public class PreviewResLocalizer : XtraResXLocalizer<PreviewStringId>
    {
        public PreviewResLocalizer() : base(new PreviewLocalizer())
        {
        }

        protected override ResourceManager CreateResourceManagerCore() => 
            new ResourceManager("DevExpress.Data.Printing.LocalizationRes", typeof(ResFinder).Assembly);
    }
}

