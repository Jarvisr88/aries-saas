namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Settings.Extension;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public class RatingEditSettingsExtension : BaseSettingsExtension
    {
        public RatingEditSettingsExtension()
        {
            this.Orientation = RatingDefaultValueHelper.Orientation;
            this.Precision = RatingPrecision.Full;
            this.ItemsCount = 5;
            this.Minimum = 0.0;
            this.Maximum = double.NaN;
        }

        protected override BaseEditSettings CreateEditSettings()
        {
            RatingEditSettings settings1 = new RatingEditSettings();
            settings1.Orientation = this.Orientation;
            settings1.Precision = this.Precision;
            settings1.ItemsCount = this.ItemsCount;
            settings1.Minimum = this.Minimum;
            settings1.Maximum = this.Maximum;
            return settings1;
        }

        public System.Windows.Controls.Orientation Orientation { get; set; }

        public RatingPrecision Precision { get; set; }

        public int ItemsCount { get; set; }

        public double Minimum { get; set; }

        public double Maximum { get; set; }
    }
}

