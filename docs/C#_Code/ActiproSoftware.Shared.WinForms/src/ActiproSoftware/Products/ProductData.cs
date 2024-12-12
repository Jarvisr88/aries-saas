namespace ActiproSoftware.Products
{
    using #PAb;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class ProductData
    {
        internal #OAb ProductCode { get; set; }

        public string Description { get; set; }

        public Image ImageSource { get; set; }

        public bool IsLicensed { get; set; }

        public string Name { get; set; }

        public bool UseEmphasis { get; set; }
    }
}

