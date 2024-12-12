namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings.Extension;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ProgressBarEditSettingsExtension : BaseSettingsExtension
    {
        public ProgressBarEditSettingsExtension()
        {
            this.IsPercent = (bool) ProgressBarEditSettings.IsPercentProperty.DefaultMetadata.DefaultValue;
            this.StyleSettings = (BaseProgressBarStyleSettings) BaseEditSettings.StyleSettingsProperty.DefaultMetadata.DefaultValue;
            this.SmallStep = (double) RangeBaseEditSettings.SmallStepProperty.DefaultMetadata.DefaultValue;
            this.LargeStep = (double) RangeBaseEditSettings.LargeStepProperty.DefaultMetadata.DefaultValue;
            this.Orientation = (System.Windows.Controls.Orientation) RangeBaseEditSettings.OrientationProperty.DefaultMetadata.DefaultValue;
            this.Minimum = (double) RangeBaseEditSettings.MinimumProperty.DefaultMetadata.DefaultValue;
            this.Maximum = (double) RangeBaseEditSettings.MaximumProperty.DefaultMetadata.DefaultValue;
            this.ContentDisplayMode = (DevExpress.Xpf.Editors.ContentDisplayMode) ProgressBarEditSettings.ContentDisplayModeProperty.DefaultMetadata.DefaultValue;
            this.ContentTemplate = (DataTemplate) ProgressBarEditSettings.ContentTemplateProperty.DefaultMetadata.DefaultValue;
        }

        protected override BaseEditSettings CreateEditSettings()
        {
            ProgressBarEditSettings settings1 = new ProgressBarEditSettings();
            settings1.IsPercent = this.IsPercent;
            settings1.StyleSettings = this.StyleSettings;
            settings1.SmallStep = this.SmallStep;
            settings1.LargeStep = this.LargeStep;
            settings1.Orientation = this.Orientation;
            settings1.Minimum = this.Minimum;
            settings1.Maximum = this.Maximum;
            settings1.ContentDisplayMode = this.ContentDisplayMode;
            settings1.ContentTemplate = this.ContentTemplate;
            return settings1;
        }

        public bool IsPercent { get; set; }

        public BaseProgressBarStyleSettings StyleSettings { get; set; }

        public double SmallStep { get; set; }

        public double LargeStep { get; set; }

        public System.Windows.Controls.Orientation Orientation { get; set; }

        public double Minimum { get; set; }

        public double Maximum { get; set; }

        public DevExpress.Xpf.Editors.ContentDisplayMode ContentDisplayMode { get; set; }

        public DataTemplate ContentTemplate { get; set; }
    }
}

