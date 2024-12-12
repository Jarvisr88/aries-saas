namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows.Media.Effects;

    public class RandomCircleRevealTransitionEffect : CloudyTransitionEffectBase
    {
        public RandomCircleRevealTransitionEffect()
        {
            PixelShader shader = new PixelShader {
                UriSource = ResourceUtils.MakeUri("WorkspaceManager/Effects/Shaders/RandomCircleRevealTransitionEffect.ps")
            };
            base.PixelShader = shader;
        }
    }
}

