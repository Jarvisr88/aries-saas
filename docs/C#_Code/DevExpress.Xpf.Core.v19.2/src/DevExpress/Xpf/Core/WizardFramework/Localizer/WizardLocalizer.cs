namespace DevExpress.Xpf.Core.WizardFramework.Localizer
{
    using DevExpress.Utils.Localization;
    using DevExpress.Utils.Localization.Internal;
    using DevExpress.Xpf.Core;
    using System;

    public class WizardLocalizer : DXLocalizer<WizardStringId>
    {
        static WizardLocalizer()
        {
            SetActiveLocalizerProvider(new DefaultActiveLocalizerProvider<WizardStringId>(CreateDefaultLocalizer()));
        }

        public static XtraLocalizer<WizardStringId> CreateDefaultLocalizer() => 
            new WizardResXLocalizer();

        public override XtraLocalizer<WizardStringId> CreateResXLocalizer() => 
            new WizardResXLocalizer();

        public static string GetString(WizardStringId id) => 
            XtraLocalizer<WizardStringId>.Active.GetLocalizedString(id);

        protected override void PopulateStringTable()
        {
            this.AddString(WizardStringId.Cancel, "Cancel");
            this.AddString(WizardStringId.Prev, "Previous");
            this.AddString(WizardStringId.Next, "Next");
            this.AddString(WizardStringId.Finish, "Finish");
        }
    }
}

