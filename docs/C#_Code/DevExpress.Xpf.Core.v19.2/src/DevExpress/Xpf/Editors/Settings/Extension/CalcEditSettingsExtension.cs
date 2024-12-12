namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;

    public class CalcEditSettingsExtension : PopupBaseSettingsExtension
    {
        public CalcEditSettingsExtension()
        {
            this.IsPopupAutoWidth = (bool) PopupCalcEdit.IsPopupAutoWidthProperty.DefaultMetadata.DefaultValue;
            this.Precision = (int) PopupCalcEdit.PrecisionProperty.DefaultMetadata.DefaultValue;
        }

        protected override void Assign(PopupBaseEditSettings settings)
        {
            base.Assign(settings);
            ((CalcEditSettings) settings).IsPopupAutoWidth = this.IsPopupAutoWidth;
            ((CalcEditSettings) settings).Precision = this.Precision;
        }

        protected override PopupBaseEditSettings CreatePopupBaseEditSettings()
        {
            CalcEditSettings settings = new CalcEditSettings();
            this.Assign(settings);
            return settings;
        }

        public bool IsPopupAutoWidth { get; set; }

        public int Precision { get; set; }
    }
}

