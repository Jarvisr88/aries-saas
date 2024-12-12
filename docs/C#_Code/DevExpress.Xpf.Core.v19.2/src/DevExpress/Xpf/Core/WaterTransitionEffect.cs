namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows.Media.Effects;

    public class WaterTransitionEffect : CloudyTransitionEffectBase
    {
        public WaterTransitionEffect()
        {
            PixelShader shader = new PixelShader {
                UriSource = ResourceUtils.MakeUri("WorkspaceManager/Effects/Shaders/WaterTransitionEffect.ps")
            };
            base.PixelShader = shader;
        }
    }
}

