namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Media.Effects;

    public class SlideInTransitionEffect : TransitionEffectBase
    {
        public static readonly DependencyProperty SlideAmountProperty = DependencyProperty.Register("SlideAmount", typeof(Point), typeof(SlideInTransitionEffect), new PropertyMetadata(new Point(1.0, 0.0), PixelShaderConstantCallback(1)));

        public SlideInTransitionEffect()
        {
            PixelShader shader = new PixelShader {
                UriSource = ResourceUtils.MakeUri("WorkspaceManager/Effects/Shaders/SlideInTransitionEffect.ps")
            };
            base.PixelShader = shader;
            base.UpdateShaderValue(SlideAmountProperty);
        }

        public Point SlideAmount
        {
            get => 
                (Point) base.GetValue(SlideAmountProperty);
            set => 
                base.SetValue(SlideAmountProperty, value);
        }
    }
}

