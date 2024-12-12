namespace DevExpress.XtraPrinting.Native.Presentation
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Effects;

    public class TileEffect : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty;
        public static readonly DependencyProperty TileCountProperty;
        public static readonly DependencyProperty OpacityProperty;

        static TileEffect();
        public TileEffect();

        public Brush Input { get; set; }

        public Point TileCount { get; set; }

        public double Opacity { get; set; }
    }
}

