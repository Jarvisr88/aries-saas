namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Design;
    using System;

    public class DXImageInfo
    {
        protected readonly IDXImageInfo info;

        public DXImageInfo(IDXImageInfo info)
        {
            this.info = info;
        }

        public Uri MakeUri() => 
            new Uri(this.info.MakeUri(), UriKind.Absolute);

        public override string ToString()
        {
            string name = this.info.Name;
            if ((this.info.Group == "Navigation") && this.info.Name.StartsWith("Next_"))
            {
                name = "Navigate" + name;
            }
            if ((this.info.Group == "Actions") && (this.info.Name.StartsWith("Delete_") && (this.info.ImageType == ImageType.GrayScaled)))
            {
                name = name.Replace("Delete_", "Remove_");
            }
            return name;
        }

        public string Group =>
            this.info.Group;
    }
}

