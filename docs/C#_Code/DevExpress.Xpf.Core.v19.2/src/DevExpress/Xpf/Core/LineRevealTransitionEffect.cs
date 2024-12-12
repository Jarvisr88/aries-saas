namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Media.Effects;

    public class LineRevealTransitionEffect : TransitionEffectBase
    {
        public static readonly DependencyProperty LineOriginProperty = DependencyProperty.Register("LineOrigin", typeof(Point), typeof(LineRevealTransitionEffect), new PropertyMetadata(new Point(-0.2, -0.2), PixelShaderConstantCallback(1)));
        public static readonly DependencyProperty LineNormalProperty = DependencyProperty.Register("LineNormal", typeof(Point), typeof(LineRevealTransitionEffect), new PropertyMetadata(new Point(1.0, 0.0), PixelShaderConstantCallback(2)));
        public static readonly DependencyProperty LineOffsetProperty = DependencyProperty.Register("LineOffset", typeof(Point), typeof(LineRevealTransitionEffect), new PropertyMetadata(new Point(1.4, 1.4), PixelShaderConstantCallback(3)));
        public static readonly DependencyProperty FuzzyAmountProperty = DependencyProperty.Register("FuzzyAmount", typeof(double), typeof(LineRevealTransitionEffect), new PropertyMetadata(0.2, PixelShaderConstantCallback(4)));

        public LineRevealTransitionEffect()
        {
            base.UpdateShaderValue(LineOriginProperty);
            base.UpdateShaderValue(LineNormalProperty);
            base.UpdateShaderValue(LineOffsetProperty);
            base.UpdateShaderValue(FuzzyAmountProperty);
            PixelShader shader = new PixelShader {
                UriSource = ResourceUtils.MakeUri("WorkspaceManager/Effects/Shaders/LineRevealTransitionEffect.ps")
            };
            base.PixelShader = shader;
        }

        public Point LineOrigin
        {
            get => 
                (Point) base.GetValue(LineOriginProperty);
            set => 
                base.SetValue(LineOriginProperty, value);
        }

        public Point LineNormal
        {
            get => 
                (Point) base.GetValue(LineNormalProperty);
            set => 
                base.SetValue(LineNormalProperty, value);
        }

        public Point LineOffset
        {
            get => 
                (Point) base.GetValue(LineOffsetProperty);
            set => 
                base.SetValue(LineOffsetProperty, value);
        }

        public double FuzzyAmount
        {
            get => 
                (double) base.GetValue(FuzzyAmountProperty);
            set => 
                base.SetValue(FuzzyAmountProperty, value);
        }
    }
}

