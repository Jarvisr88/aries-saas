namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public abstract class CloudyTransitionEffectBase : RandomizedTransitionEffectBase
    {
        private const int Width = 0x200;
        private const int Height = 0x200;
        private const int Gamma = 510;
        public static readonly DependencyProperty CloudImageProperty = RegisterPixelShaderSamplerProperty("CloudImage", typeof(CloudyTransitionEffectBase), 2, SamplingMode.Bilinear);
        private static readonly ImageSource cloudsBitmap = GenerateCloudsBitmap();
        private static Random random;

        public CloudyTransitionEffectBase()
        {
            this.CloudImage = new ImageBrush();
            ((ImageBrush) this.CloudImage).ImageSource = cloudsBitmap;
            base.UpdateShaderValue(CloudImageProperty);
        }

        private static double Displace(double num)
        {
            double num2 = (num / 1024.0) * 10.0;
            return ((random.NextDouble() - 0.5) * num2);
        }

        private static void GenerateClouds(int[] pixels)
        {
            random = new Random();
            Plasma(pixels, 0.0, 0.0, 512.0, 512.0, random.NextDouble(), random.NextDouble(), random.NextDouble(), random.NextDouble());
        }

        private static ImageSource GenerateCloudsBitmap() => 
            BitmapHelpers.GenerateBitmap(0x200, 0x200, new Action<int[]>(CloudyTransitionEffectBase.GenerateClouds));

        private static void Plasma(int[] pixels, double x, double y, double width, double height, double c1, double c2, double c3, double c4)
        {
            double num6 = width / 2.0;
            double num7 = height / 2.0;
            if ((width <= 1.0) && (height <= 1.0))
            {
                double num8 = (((c1 + c2) + c3) + c4) / 4.0;
                double num9 = 0.0;
                double num10 = 0.0;
                double num11 = 0.0;
                num9 = (num8 >= 0.5) ? ((1.0 - num8) * 510.0) : (num8 * 510.0);
                num10 = ((num8 < 0.3) || (num8 >= 0.8)) ? ((num8 >= 0.3) ? ((1.3 - num8) * 510.0) : ((0.3 - num8) * 510.0)) : ((num8 - 0.3) * 510.0);
                num11 = (num8 < 0.5) ? ((0.5 - num8) * 510.0) : ((num8 - 0.5) * 510.0);
                pixels[(((int) y) * 0x200) + ((int) x)] = BitmapHelpers.CreateArgb32(0xff, (int) num9, (int) num10, (int) num11);
            }
            else
            {
                double num5 = ((((c1 + c2) + c3) + c4) / 4.0) + Displace(num6 + num7);
                double num = (c1 + c2) / 2.0;
                double num2 = (c2 + c3) / 2.0;
                double num3 = (c3 + c4) / 2.0;
                double num4 = (c4 + c1) / 2.0;
                if (num5 < 0.0)
                {
                    num5 = 0.0;
                }
                else if (num5 > 1.0)
                {
                    num5 = 1.0;
                }
                Plasma(pixels, x, y, num6, num7, c1, num, num5, num4);
                Plasma(pixels, x + num6, y, num6, num7, num, c2, num2, num5);
                Plasma(pixels, x + num6, y + num7, num6, num7, num5, num2, c3, num3);
                Plasma(pixels, x, y + num7, num6, num7, num4, num5, num3, c4);
            }
        }

        public Brush CloudImage
        {
            get => 
                (Brush) base.GetValue(CloudImageProperty);
            set => 
                base.SetValue(CloudImageProperty, value);
        }
    }
}

