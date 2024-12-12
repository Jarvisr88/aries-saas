namespace DevExpress.Data.Localization
{
    using DevExpress.Utils.Localization;
    using System;
    using System.ComponentModel;

    [ToolboxItem(false)]
    public class CommonLocalizer : XtraLocalizer<CommonStringId>
    {
        internal static readonly CommonLocalizer Default;

        static CommonLocalizer();
        private void AddStrings();
        public static XtraLocalizer<CommonStringId> CreateDefaultLocalizer();
        public override XtraLocalizer<CommonStringId> CreateResXLocalizer();
        public static string GetString(CommonStringId id);
        protected override void PopulateStringTable();

        public static XtraLocalizer<CommonStringId> Active { get; set; }
    }
}

