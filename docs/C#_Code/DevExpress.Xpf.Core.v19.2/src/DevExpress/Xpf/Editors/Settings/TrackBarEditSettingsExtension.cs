namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings.Extension;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public class TrackBarEditSettingsExtension : BaseSettingsExtension
    {
        public TrackBarEditSettingsExtension()
        {
            this.StyleSettings = (TrackBarStyleSettings) BaseEditSettings.StyleSettingsProperty.DefaultMetadata.DefaultValue;
            this.SmallStep = (double) RangeBaseEditSettings.SmallStepProperty.DefaultMetadata.DefaultValue;
            this.LargeStep = (double) RangeBaseEditSettings.LargeStepProperty.DefaultMetadata.DefaultValue;
            this.Orientation = (System.Windows.Controls.Orientation) RangeBaseEditSettings.OrientationProperty.DefaultMetadata.DefaultValue;
            this.Minimum = (double) RangeBaseEditSettings.MinimumProperty.DefaultMetadata.DefaultValue;
            this.Maximum = (double) RangeBaseEditSettings.MaximumProperty.DefaultMetadata.DefaultValue;
            this.TickFrequency = (double) TrackBarEditSettings.TickFrequencyProperty.DefaultMetadata.DefaultValue;
            this.TickPlacement = (System.Windows.Controls.Primitives.TickPlacement) TrackBarEditSettings.TickPlacementProperty.DefaultMetadata.DefaultValue;
        }

        protected override BaseEditSettings CreateEditSettings()
        {
            TrackBarEditSettings settings1 = new TrackBarEditSettings();
            settings1.StyleSettings = this.StyleSettings;
            settings1.SmallStep = this.SmallStep;
            settings1.LargeStep = this.LargeStep;
            settings1.Orientation = this.Orientation;
            settings1.Minimum = this.Minimum;
            settings1.Maximum = this.Maximum;
            settings1.TickPlacement = this.TickPlacement;
            settings1.TickFrequency = this.TickFrequency;
            return settings1;
        }

        public TrackBarStyleSettings StyleSettings { get; set; }

        public double SmallStep { get; set; }

        public double LargeStep { get; set; }

        public System.Windows.Controls.Orientation Orientation { get; set; }

        public double Minimum { get; set; }

        public double Maximum { get; set; }

        public System.Windows.Controls.Primitives.TickPlacement TickPlacement { get; set; }

        public double TickFrequency { get; set; }
    }
}

