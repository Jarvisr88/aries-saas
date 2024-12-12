namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Effects;

    public class DissolveTransitionEffect : RandomizedTransitionEffectBase
    {
        public static readonly DependencyProperty NoiseImageProperty = RegisterPixelShaderSamplerProperty("NoiseImage", typeof(DissolveTransitionEffect), 2, SamplingMode.Bilinear);
        private static readonly ImageSource noiseBitmap = GenerateNoiseBitmap();

        public DissolveTransitionEffect()
        {
            PixelShader shader = new PixelShader {
                UriSource = ResourceUtils.MakeUri("WorkspaceManager/Effects/Shaders/DisolveTransitionEffect.ps")
            };
            base.PixelShader = shader;
            this.NoiseImage = new ImageBrush();
            ((ImageBrush) this.NoiseImage).ImageSource = noiseBitmap;
            base.UpdateShaderValue(NoiseImageProperty);
        }

        private static void GenerateNoise(int[] pixels)
        {
            Random random = new Random();
            for (int i = 0; i < pixels.Length; i++)
            {
                int r = random.Next(0, 0xff);
                int g = random.Next(0, 0xff);
                int b = random.Next(0, 0xff);
                pixels[i] = BitmapHelpers.CreateArgb32(0xff, r, g, b);
            }
        }

        private static ImageSource GenerateNoiseBitmap() => 
            BitmapHelpers.GenerateBitmap(0x200, 0x200, new Action<int[]>(DissolveTransitionEffect.GenerateNoise));

        public Brush NoiseImage
        {
            get => 
                (Brush) base.GetValue(NoiseImageProperty);
            set => 
                base.SetValue(NoiseImageProperty, value);
        }
    }
}

