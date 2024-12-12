namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Effects;

    public class ColorizerEffect : ShaderEffect
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty InputProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ColorProperty;
        [ThreadStatic]
        internal static PixelShader _pixelShader;

        static ColorizerEffect();
        public ColorizerEffect();
        private PixelShader GetPixelShader();

        public System.Windows.Media.Color Color { get; set; }
    }
}

