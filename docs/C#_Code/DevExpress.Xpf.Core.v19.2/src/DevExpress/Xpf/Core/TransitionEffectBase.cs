namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Effects;

    public abstract class TransitionEffectBase : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(TransitionEffectBase), 0, SamplingMode.NearestNeighbor);
        public static readonly DependencyProperty OldInputProperty = RegisterPixelShaderSamplerProperty("OldInput", typeof(TransitionEffectBase), 1, SamplingMode.NearestNeighbor);
        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register("Progress", typeof(double), typeof(TransitionEffectBase), new PropertyMetadata(0.0, PixelShaderConstantCallback(0)));

        public TransitionEffectBase()
        {
            base.UpdateShaderValue(InputProperty);
            base.UpdateShaderValue(OldInputProperty);
            base.UpdateShaderValue(ProgressProperty);
        }

        public Brush Input
        {
            get => 
                (Brush) base.GetValue(InputProperty);
            set => 
                base.SetValue(InputProperty, value);
        }

        public Brush OldInput
        {
            get => 
                (Brush) base.GetValue(OldInputProperty);
            set => 
                base.SetValue(OldInputProperty, value);
        }

        public double Progress
        {
            get => 
                (double) base.GetValue(ProgressProperty);
            set => 
                base.SetValue(ProgressProperty, value);
        }
    }
}

