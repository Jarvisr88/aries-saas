namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows.Media.Effects;

    public class DropFadeTransitionEffect : CloudyTransitionEffectBase
    {
        public DropFadeTransitionEffect()
        {
            PixelShader shader = new PixelShader {
                UriSource = ResourceUtils.MakeUri("WorkspaceManager/Effects/Shaders/DropFadeTransitionEffect.ps")
            };
            base.PixelShader = shader;
        }
    }
}

