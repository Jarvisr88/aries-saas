namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Resources;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class ResourceImageExtension : MarkupExtension
    {
        private ImageSource CreateImage(Stream stream) => 
            BitmapDecoder.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.Default).Frames.First<BitmapFrame>();

        private ResourceManager CreateResourceManager() => 
            new ResourceManager(base.GetType().Assembly.GetName().Name + ".g", base.GetType().Assembly);

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            object obj2;
            if (string.IsNullOrEmpty(this.ResourceName))
            {
                throw new InvalidOperationException("The value of the ResourceName property is not set.");
            }
            using (ResourceSet set = this.CreateResourceManager().GetResourceSet(CultureInfo.CurrentCulture, true, true))
            {
                using (UnmanagedMemoryStream stream = (UnmanagedMemoryStream) set.GetObject(this.ResourceName, true))
                {
                    obj2 = this.CreateImage(stream);
                }
            }
            return obj2;
        }

        public string ResourceName { get; set; }
    }
}

