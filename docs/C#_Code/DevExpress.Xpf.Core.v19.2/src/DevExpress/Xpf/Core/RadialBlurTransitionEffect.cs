namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows.Media.Effects;

    public class RadialBlurTransitionEffect : TransitionEffectBase
    {
        public RadialBlurTransitionEffect()
        {
            PixelShader shader = new PixelShader {
                UriSource = ResourceUtils.MakeUri("WorkspaceManager/Effects/Shaders/RadialBlurTransitionEffect.ps")
            };
            base.PixelShader = shader;
        }
    }
}

