namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class BarItemImageColorizerSettings : ImageColorizerSettings<BorderState>
    {
        public override void Apply(DependencyObject target, BorderState state);

        public Color Normal { get; set; }

        public Color Hover { get; set; }

        public Color Pressed { get; set; }

        public Color Checked { get; set; }

        public Color Disabled { get; set; }
    }
}

