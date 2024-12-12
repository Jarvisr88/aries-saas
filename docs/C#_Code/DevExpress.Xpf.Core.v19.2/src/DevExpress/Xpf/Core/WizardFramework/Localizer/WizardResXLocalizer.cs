namespace DevExpress.Xpf.Core.WizardFramework.Localizer
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Resources;

    public class WizardResXLocalizer : DXResXLocalizer<WizardStringId>
    {
        public WizardResXLocalizer() : base(new WizardLocalizer())
        {
        }

        protected override ResourceManager CreateResourceManagerCore() => 
            new ResourceManager("DevExpress.Xpf.Core.WizardFramework.Localizer.LocalizationRes", typeof(WizardResXLocalizer).Assembly);
    }
}

