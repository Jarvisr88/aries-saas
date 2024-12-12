namespace DevExpress.Xpf.Core
{
    using System;

    public class CarouselResourceExtension : ResourceExtensionBase
    {
        public CarouselResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Xpf.Carousel";
    }
}

