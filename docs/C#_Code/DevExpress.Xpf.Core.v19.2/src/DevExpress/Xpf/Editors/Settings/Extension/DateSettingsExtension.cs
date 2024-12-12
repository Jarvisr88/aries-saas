namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;

    public class DateSettingsExtension : PopupBaseSettingsExtension
    {
        public DateSettingsExtension()
        {
            base.ShowSizeGrip = false;
            base.MaskType = MaskType.DateTime;
            base.Mask = "d";
            base.DisplayFormat = "d";
            this.MinValue = (DateTime?) DateEditSettings.MinValueProperty.DefaultMetadata.DefaultValue;
            this.MaxValue = (DateTime?) DateEditSettings.MaxValueProperty.DefaultMetadata.DefaultValue;
            this.ShowWeekNumbers = (bool) DateEditSettings.ShowWeekNumbersProperty.DefaultMetadata.DefaultValue;
            base.AllowNullInput = (bool) BaseEditSettings.AllowNullInputProperty.GetMetadata(typeof(DateEditSettings)).DefaultValue;
        }

        protected override PopupBaseEditSettings CreatePopupBaseEditSettings() => 
            new DateEditSettings { 
                NullValue = this.NullValue,
                MinValue = this.MinValue,
                MaxValue = this.MaxValue,
                ShowWeekNumbers = this.ShowWeekNumbers
            };

        public bool ShowWeekNumbers { get; set; }

        public DateTime? MinValue { get; set; }

        public DateTime? MaxValue { get; set; }

        public object NullValue { get; set; }
    }
}

