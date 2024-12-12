namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows.Media.Effects;

    public class WaveTransitionEffect : TransitionEffectBase
    {
        public WaveTransitionEffect()
        {
            PixelShader shader = new PixelShader {
                UriSource = ResourceUtils.MakeUri("WorkspaceManager/Effects/Shaders/WaveTransitionEffect.ps")
            };
            base.PixelShader = shader;
        }
    }
}

