namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class SizeSettings
    {
        private double? height;
        private double? maxHeight;
        private double? maxWidth;
        private double? minHeight;
        private double? minWidth;
        private double? width;

        public event EventHandler Changed;

        public void Apply(FrameworkElement element);
        public static void Clear(FrameworkElement element);
        protected void RaiseChanged();

        public double? Height { get; set; }

        public double? Width { get; set; }

        public double? MaxHeight { get; set; }

        public double? MaxWidth { get; set; }

        public double? MinHeight { get; set; }

        public double? MinWidth { get; set; }
    }
}

