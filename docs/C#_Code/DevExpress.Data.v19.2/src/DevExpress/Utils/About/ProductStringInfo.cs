namespace DevExpress.Utils.About
{
    using System;

    public class ProductStringInfo
    {
        private string name;
        private string platform;

        public ProductStringInfo(string name) : this("", name)
        {
        }

        public ProductStringInfo(string platform, string name)
        {
            this.name = name;
            this.platform = platform;
        }

        public string ProductName =>
            this.name;

        public string ProductPlatform =>
            this.platform;
    }
}

