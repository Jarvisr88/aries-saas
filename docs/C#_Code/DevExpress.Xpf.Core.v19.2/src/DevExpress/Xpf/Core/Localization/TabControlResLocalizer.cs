namespace DevExpress.Xpf.Core.Localization
{
    using DevExpress.Utils.Localization;
    using System;
    using System.Resources;

    public class TabControlResLocalizer : XtraResXLocalizer<TabControlStringId>
    {
        public TabControlResLocalizer() : base(new TabControlLocalizer())
        {
        }

        protected override ResourceManager CreateResourceManagerCore() => 
            new ResourceManager("DevExpress.Xpf.Core.LocalizationRes", typeof(TabControlResLocalizer).Assembly);
    }
}

