namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows.Media.Imaging;

    public abstract class RelativeIconSourceExtensionBase : RelativeImageSourceExtensionBase
    {
        protected RelativeIconSourceExtensionBase(string relativePath) : base(relativePath)
        {
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            BitmapFrame.Create(this.GetUri(serviceProvider));
    }
}

