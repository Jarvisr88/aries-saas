namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows.Media.Effects;

    public class RippleTransitionEffect : TransitionEffectBase
    {
        public RippleTransitionEffect()
        {
            PixelShader shader = new PixelShader {
                UriSource = ResourceUtils.MakeUri("WorkspaceManager/Effects/Shaders/RippleTransitionEffect.ps")
            };
            base.PixelShader = shader;
        }
    }
}

