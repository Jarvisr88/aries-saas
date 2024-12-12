namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public sealed class BadgeShaderEffect : BaseShaderEffect<BadgeShaderEffect>
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty BadgeProperty = RegisterPixelShaderSamplerProperty("Badge", 1);

        protected override string GetCompiledShaderName() => 
            "badgeshader.ps";

        public void UpdatePaddings(double left, double top, double right, double bottom)
        {
            base.PaddingLeft = left;
            base.PaddingTop = top;
            base.PaddingRight = right;
            base.PaddingBottom = bottom;
        }

        public VisualBrush Badge
        {
            get => 
                (VisualBrush) base.GetValue(BadgeProperty);
            set => 
                base.SetValue(BadgeProperty, value);
        }
    }
}

