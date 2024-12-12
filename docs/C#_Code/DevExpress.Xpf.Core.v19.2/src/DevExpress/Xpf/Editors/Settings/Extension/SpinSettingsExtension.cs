namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class SpinSettingsExtension : ButtonSettingsExtension
    {
        public SpinSettingsExtension()
        {
            base.MaskType = MaskType.Numeric;
            this.MinValue = (decimal?) SpinEditSettings.MinValueProperty.DefaultMetadata.DefaultValue;
            this.MaxValue = (decimal?) SpinEditSettings.MaxValueProperty.DefaultMetadata.DefaultValue;
            this.Increment = (decimal) SpinEditSettings.IncrementProperty.DefaultMetadata.DefaultValue;
            this.IsFloatValue = (bool) SpinEditSettings.IsFloatValueProperty.DefaultMetadata.DefaultValue;
            this.AllowRoundOutOfRangeValue = (bool) SpinEditSettings.AllowRoundOutOfRangeValueProperty.GetDefaultValue();
        }

        protected sealed override ButtonEditSettings CreateButtonEditSettings()
        {
            SpinEditSettings settings = this.CreateSpinEditSettings();
            settings.MinValue = this.MinValue;
            settings.MaxValue = this.MaxValue;
            settings.Increment = this.Increment;
            settings.IsFloatValue = this.IsFloatValue;
            settings.AllowRoundOutOfRangeValue = this.AllowRoundOutOfRangeValue;
            return settings;
        }

        public virtual SpinEditSettings CreateSpinEditSettings() => 
            new SpinEditSettings();

        public decimal? MinValue { get; set; }

        public decimal? MaxValue { get; set; }

        public decimal Increment { get; set; }

        public bool IsFloatValue { get; set; }

        public bool AllowRoundOutOfRangeValue { get; set; }
    }
}

