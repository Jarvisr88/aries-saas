namespace DevExpress.Data.Localization
{
    using DevExpress.Utils.Localization;
    using System;
    using System.ComponentModel;
    using System.Resources;

    [ToolboxItem(false)]
    public class CommonResLocalizer : XtraResXLocalizer<CommonStringId>
    {
        public CommonResLocalizer();
        protected override ResourceManager CreateResourceManagerCore();
    }
}

