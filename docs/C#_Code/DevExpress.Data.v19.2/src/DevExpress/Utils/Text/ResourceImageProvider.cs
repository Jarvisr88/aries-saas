namespace DevExpress.Utils.Text
{
    using DevExpress.Utils;
    using DevExpress.Utils.Svg;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;
    using System.Resources;

    public class ResourceImageProvider
    {
        [ThreadStatic]
        private static ResourceImageProvider current;
        private Dictionary<string, Image> images;
        private System.Resources.ResourceManager resourceManager;

        protected System.Resources.ResourceManager GetDefaultResourceManager()
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                foreach (string str in entryAssembly.GetManifestResourceNames())
                {
                    string str2 = str.ToLowerInvariant();
                    if (str2.EndsWith(".resources.resources") || str2.Equals("resources.resources"))
                    {
                        return new System.Resources.ResourceManager(str.Substring(0, str.Length - ".resources".Length), entryAssembly);
                    }
                }
            }
            return null;
        }

        public Image GetImage(object context, string id) => 
            this.GetImage(context, id, 1f, Size.Empty);

        public Image GetImage(object context, string id, float dpiScaleFactor, Size imageSize)
        {
            Image image;
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            if (id[0] == '#')
            {
                id = id.Substring(1);
            }
            this.Images.TryGetValue(id, out image);
            if (image == null)
            {
                try
                {
                    image = this.GetResourceImage(context, id, dpiScaleFactor, imageSize);
                }
                catch
                {
                }
                if (image == null)
                {
                    return null;
                }
                this.Images[id] = image;
            }
            return image;
        }

        protected Image GetResourceImage(object context, string id, float dpiScaleFactor, Size imageSize)
        {
            System.Resources.ResourceManager resourceManager = this.ResourceManager;
            if (resourceManager != null)
            {
                object obj2 = resourceManager.GetObject(id);
                if (obj2 is Image)
                {
                    return (Image) obj2;
                }
                SvgImage svgImage = obj2 as SvgImage;
                if (svgImage != null)
                {
                    if (imageSize.IsEmpty)
                    {
                        return new SvgBitmap(svgImage).Render(null, (double) dpiScaleFactor, DefaultBoolean.Default, DefaultBoolean.Default);
                    }
                    imageSize = new Size((int) (imageSize.Width * dpiScaleFactor), (int) (imageSize.Height * dpiScaleFactor));
                    return new SvgBitmap(svgImage).Render(imageSize, null, DefaultBoolean.Default, DefaultBoolean.Default);
                }
            }
            return null;
        }

        public static ResourceImageProvider Current
        {
            get
            {
                current ??= new ResourceImageProvider();
                return current;
            }
        }

        protected Dictionary<string, Image> Images
        {
            get
            {
                this.images ??= new Dictionary<string, Image>();
                return this.images;
            }
        }

        public System.Resources.ResourceManager ResourceManager
        {
            get
            {
                this.resourceManager ??= this.GetDefaultResourceManager();
                return this.resourceManager;
            }
            set => 
                this.resourceManager = value;
        }
    }
}

