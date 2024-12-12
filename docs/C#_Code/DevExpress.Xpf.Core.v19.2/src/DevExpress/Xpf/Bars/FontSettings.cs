namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class FontSettings
    {
        private Brush _checked;
        private Brush disabled;
        private Brush hover;
        private Brush hoverChecked;
        private Brush normal;
        private Brush pressed;

        public void Apply(Control element, BorderState actualState);
        public static void Clear(Control element);
        private Brush CorrectBrush(Brush brush);
        private Brush FreezeValue(Brush value);

        public Brush Normal { get; set; }

        public Brush Hover { get; set; }

        public Brush Pressed { get; set; }

        public Brush Disabled { get; set; }

        public Brush HoverChecked { get; set; }

        public Brush Checked { get; set; }

        public FontFamily Family { get; set; }

        public FontWeight? Weight { get; set; }

        public double? Size { get; set; }
    }
}

