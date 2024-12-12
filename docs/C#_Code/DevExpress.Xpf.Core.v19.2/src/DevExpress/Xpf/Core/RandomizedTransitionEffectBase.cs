namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public abstract class RandomizedTransitionEffectBase : TransitionEffectBase
    {
        public static readonly DependencyProperty RandomSeedProperty = DependencyProperty.Register("RandomSeed", typeof(double), typeof(RandomizedTransitionEffectBase), new PropertyMetadata(0.0, PixelShaderConstantCallback(1)));

        public RandomizedTransitionEffectBase()
        {
            base.UpdateShaderValue(RandomSeedProperty);
        }

        public double RandomSeed
        {
            get => 
                (double) base.GetValue(RandomSeedProperty);
            set => 
                base.SetValue(RandomSeedProperty, value);
        }
    }
}

