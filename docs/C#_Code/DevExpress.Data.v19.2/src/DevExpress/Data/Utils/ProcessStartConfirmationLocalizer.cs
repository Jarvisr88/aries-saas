namespace DevExpress.Data.Utils
{
    using DevExpress.Utils.Localization;
    using System;

    public class ProcessStartConfirmationLocalizer : XtraLocalizer<ProcessStartConfirmationStringId>
    {
        static ProcessStartConfirmationLocalizer();
        public static XtraLocalizer<ProcessStartConfirmationStringId> CreateDefaultLocalizer();
        public override XtraLocalizer<ProcessStartConfirmationStringId> CreateResXLocalizer();
        public static string GetString(ProcessStartConfirmationStringId id);
        protected override void PopulateStringTable();

        public static XtraLocalizer<ProcessStartConfirmationStringId> Active { get; set; }
    }
}

