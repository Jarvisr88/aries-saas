namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;

    public class DXImageOffice2013Extension : DXImageExtension
    {
        [TypeConverter(typeof(DXImageOffice2013Converter))]
        public DXImageInfo Image
        {
            get => 
                base.Image;
            set => 
                base.Image = value;
        }
    }
}

