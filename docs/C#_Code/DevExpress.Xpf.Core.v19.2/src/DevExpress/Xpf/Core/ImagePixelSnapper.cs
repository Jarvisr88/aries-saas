namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("Image")]
    public class ImagePixelSnapper : Decorator
    {
        public static readonly DependencyProperty ImageProperty;
        private PixelSnapper snapper;

        static ImagePixelSnapper()
        {
            ImageProperty = DependencyPropertyManager.Register("Image", typeof(System.Windows.Controls.Image), typeof(ImagePixelSnapper), new PropertyMetadata((d, e) => ((ImagePixelSnapper) d).OnImageChanged()));
        }

        public ImagePixelSnapper()
        {
            RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.NearestNeighbor);
            this.IsXPMode = this.GetIsXPMode();
        }

        private bool GetIsXPMode()
        {
            bool flag3 = Environment.OSVersion.Version.Minor == 1;
            return (((Environment.OSVersion.Platform == PlatformID.Win32NT) & (Environment.OSVersion.Version.Major == 5)) & flag3);
        }

        protected virtual void OnImageChanged()
        {
            this.SetContent(this.Image);
        }

        protected virtual void SetContent(object content)
        {
            if (!this.IsXPMode)
            {
                this.Child = this.Image;
            }
            else
            {
                this.Snapper.Child = this.Image;
            }
        }

        public System.Windows.Controls.Image Image
        {
            get => 
                (System.Windows.Controls.Image) base.GetValue(ImageProperty);
            set => 
                base.SetValue(ImageProperty, value);
        }

        protected bool IsXPMode { get; private set; }

        protected PixelSnapper Snapper
        {
            get
            {
                if (this.snapper == null)
                {
                    this.snapper = new PixelSnapper();
                    this.Child = this.snapper;
                }
                return this.snapper;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ImagePixelSnapper.<>c <>9 = new ImagePixelSnapper.<>c();

            internal void <.cctor>b__15_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ImagePixelSnapper) d).OnImageChanged();
            }
        }
    }
}

