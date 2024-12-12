namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;

    public class DXImageGrayscaleExtension : DXImageExtension
    {
        [TypeConverter(typeof(DXImageGrayscaleConverter))]
        public DXImageInfo Image
        {
            get => 
                base.Image;
            set => 
                base.Image = value;
        }
    }
}

