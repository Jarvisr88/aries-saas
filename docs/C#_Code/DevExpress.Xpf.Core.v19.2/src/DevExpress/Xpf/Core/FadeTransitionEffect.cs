namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows.Media.Effects;

    public class FadeTransitionEffect : TransitionEffectBase
    {
        public FadeTransitionEffect()
        {
            PixelShader shader = new PixelShader {
                UriSource = ResourceUtils.MakeUri("WorkspaceManager/Effects/Shaders/FadeTransitionEffect.ps")
            };
            base.PixelShader = shader;
        }
    }
}

