namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Localization;
    using DevExpress.Utils.Localization.Internal;
    using System;

    public class DXMessageBoxLocalizer : DXLocalizer<DXMessageBoxStringId>
    {
        static DXMessageBoxLocalizer()
        {
            SetActiveLocalizerProvider(new DefaultActiveLocalizerProvider<DXMessageBoxStringId>(CreateDefaultLocalizer()));
        }

        public static XtraLocalizer<DXMessageBoxStringId> CreateDefaultLocalizer() => 
            new DXMessageBoxResXLocalizer();

        public override XtraLocalizer<DXMessageBoxStringId> CreateResXLocalizer() => 
            new DXMessageBoxResXLocalizer();

        public static string GetString(DXMessageBoxStringId id) => 
            XtraLocalizer<DXMessageBoxStringId>.Active.GetLocalizedString(id);

        protected override void PopulateStringTable()
        {
            this.AddString(DXMessageBoxStringId.Cancel, "Cancel");
            this.AddString(DXMessageBoxStringId.Ok, "OK");
            this.AddString(DXMessageBoxStringId.Yes, "Yes");
            this.AddString(DXMessageBoxStringId.No, "No");
        }
    }
}

