namespace DevExpress.Images
{
    using DevExpress.Utils;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Svg;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security;

    public class ImageResourceCache
    {
        private readonly Dictionary<string, Stream> resources = new Dictionary<string, Stream>(0x1400);
        private readonly IDictionary<string, Stream> resourcesByFileName = new Dictionary<string, Stream>(0x1400, StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, Dictionary<string, Stream>> categoryResources = new Dictionary<string, Dictionary<string, Stream>>(0x40, StringComparer.OrdinalIgnoreCase);
        private SvgImageCache svgImageCache = new SvgImageCache();
        private SvgRenderedImageCache svgRenderedImageCache = new SvgRenderedImageCache();
        private IList<string> svgImages = new List<string>(640);
        private Dictionary<ImageResourceKey, string> imageResourceKeyCache = new Dictionary<ImageResourceKey, string>();
        private static ImageResourceCache defaultCore;

        protected ImageResourceCache()
        {
        }

        private static Size ConvertToSize(ImageSize imageSize) => 
            (imageSize == ImageSize.Size16x16) ? new Size(0x10, 0x10) : ((imageSize == ImageSize.Size32x32) ? new Size(0x20, 0x20) : new Size(0x20, 0x20));

        [SecuritySafeCritical]
        private static ImageResourceCache DoLoad()
        {
            ImageResourceCache cache = new ImageResourceCache();
            using (IAssemblyImageReader reader = AssemblyImageReaderFactory.Create(null))
            {
                IDictionaryEnumerator streamEnumerator = reader.GetStreamEnumerator();
                while (streamEnumerator.MoveNext())
                {
                    string[] strArray;
                    string key = streamEnumerator.Key as string;
                    if (key.EndsWith(".png", StringComparison.Ordinal))
                    {
                        cache.resources.Add(key, (Stream) streamEnumerator.Value);
                        strArray = ImageCacheUtils.Split(key);
                        key = strArray[0] + @"\" + strArray[strArray.Length - 1];
                        if (cache.resourcesByFileName.ContainsKey(key))
                        {
                            continue;
                        }
                        cache.resourcesByFileName.Add(key, (Stream) streamEnumerator.Value);
                        continue;
                    }
                    if (key.EndsWith(".svg", StringComparison.Ordinal))
                    {
                        Dictionary<string, Stream> dictionary;
                        cache.resources.Add(key, (Stream) streamEnumerator.Value);
                        strArray = ImageCacheUtils.Split(key);
                        string str2 = strArray[strArray.Length - 1];
                        string str3 = strArray[1];
                        if (strArray[0] != "svgimages")
                        {
                            str3 = strArray[0] + @"\" + str3;
                        }
                        if (!cache.categoryResources.TryGetValue(str3, out dictionary))
                        {
                            dictionary = new Dictionary<string, Stream>(0x100);
                            cache.categoryResources.Add(str3, dictionary);
                        }
                        if (!dictionary.ContainsKey(str2))
                        {
                            dictionary.Add(str2, (Stream) streamEnumerator.Value);
                        }
                        cache.svgImages.Add(str2.Substring(0, str2.Length - 4));
                        key = strArray[0] + @"\" + str2;
                        if (!cache.resourcesByFileName.ContainsKey(key))
                        {
                            cache.resourcesByFileName.Add(key, (Stream) streamEnumerator.Value);
                        }
                    }
                }
            }
            return cache;
        }

        private string FindResourceKey(string fileName, string category, string prefix)
        {
            string str = string.Empty;
            ImageResourceKey key = new ImageResourceKey(fileName, prefix, category);
            if (!this.imageResourceKeyCache.TryGetValue(key, out str))
            {
                str = (from key in this.resources.Keys
                    where ImageCacheUtils.IsMatch(key, prefix, fileName, category)
                    select key).FirstOrDefault<string>();
                this.imageResourceKeyCache.Add(key, str);
            }
            return str;
        }

        public string[] GetAllResourceKeys() => 
            (this.resources.Keys.Count == 0) ? null : this.resources.Keys.ToArray<string>();

        private string GetFullNameImage(string id)
        {
            string prefix = string.Empty;
            ImageCacheUtils.ExtractPrefix(ref id, out prefix);
            string fileName = ImageCacheUtils.GetFileName(id.ToLower(), ImageSize.Any, ImageType.Svg);
            return this.FindResourceKey(fileName, "svgimages", prefix);
        }

        public Image GetImage(string resourceName) => 
            this.resources.ContainsKey(resourceName) ? Image.FromStream(this.resources[resourceName]) : null;

        public Image GetImageById(string id, ImageSize imageSize, ImageType imageType)
        {
            string str;
            ImageCacheUtils.ExtractPrefix(ref id, out str);
            string fileName = ImageCacheUtils.GetFileName(id.ToLower(), imageSize, imageType);
            string category = ImageCacheUtils.GetCategory(imageType);
            string str4 = this.FindResourceKey(fileName, category, str);
            if (string.IsNullOrEmpty(str4) && !string.IsNullOrEmpty(str))
            {
                str4 = this.FindResourceKey(fileName, category, string.Empty);
            }
            return (string.IsNullOrEmpty(str4) ? null : ((imageType != ImageType.Svg) ? this.GetImage(str4) : this.GetSvgImage(str4, null, ConvertToSize(imageSize))));
        }

        internal ICollection GetKeys() => 
            this.resources.Keys;

        public Stream GetResource(string resourceName)
        {
            Stream stream = null;
            return (((resourceName == null) || !this.resources.TryGetValue(resourceName, out stream)) ? null : stream);
        }

        public Stream GetResourceByFileName(string fileName, ImageType imageType = 1)
        {
            Stream stream = null;
            return (((fileName == null) || !this.resourcesByFileName.TryGetValue(ImageCacheUtils.GetCategory(imageType) + @"\" + fileName, out stream)) ? null : stream);
        }

        public SvgImage GetSvgImage(string resourceName)
        {
            Stream stream;
            return (((resourceName == null) || !this.resources.TryGetValue(resourceName, out stream)) ? null : SvgImage.FromStream(stream));
        }

        public Image GetSvgImage(string id, ISvgPaletteProvider paletteProvider, Size imageSize)
        {
            SvgBitmap bitmap;
            Image image;
            if (!this.svgImageCache.TryGetValue(id, out bitmap))
            {
                bitmap = new SvgBitmap(SvgLoader.LoadFromStream(this.resources[id]));
                this.svgImageCache.Add(id, bitmap);
            }
            if (!this.svgRenderedImageCache.TryGetValue(id, imageSize, paletteProvider, out image))
            {
                image = bitmap.Render(imageSize.IsEmpty ? new Size((int) bitmap.SvgImage.Width, (int) bitmap.SvgImage.Height) : imageSize, paletteProvider, DefaultBoolean.Default, DefaultBoolean.Default);
                int? paletteHashCode = null;
                if (paletteProvider != null)
                {
                    paletteHashCode = new int?(paletteProvider.GetHashCode());
                }
                this.svgRenderedImageCache.Add(new SvgImageKey(id, imageSize, paletteHashCode), image);
            }
            return image;
        }

        public Stream GetSvgImageByCategory(string category, string fileName)
        {
            Dictionary<string, Stream> dictionary;
            ImageType svg = ImageType.Svg;
            Stream stream = null;
            return ((!this.categoryResources.TryGetValue(category.ToLowerInvariant(), out dictionary) || !dictionary.TryGetValue(fileName.ToLowerInvariant(), out stream)) ? (((fileName == null) || !this.resourcesByFileName.TryGetValue(ImageCacheUtils.GetCategory(svg) + @"\" + fileName, out stream)) ? null : stream) : stream);
        }

        public SvgImage GetSvgImageById(string id)
        {
            SvgBitmap bitmap;
            string fullNameImage = this.GetFullNameImage(id);
            if (fullNameImage == null)
            {
                return null;
            }
            if (!this.svgImageCache.TryGetValue(fullNameImage, out bitmap))
            {
                bitmap = new SvgBitmap(SvgLoader.LoadFromStream(this.resources[fullNameImage]));
                this.svgImageCache.Add(fullNameImage, bitmap);
            }
            return bitmap.SvgImage;
        }

        public Image GetSvgImageById(string id, ISvgPaletteProvider paletteProvider, int width, int height)
        {
            string fullNameImage = this.GetFullNameImage(id);
            return (!string.IsNullOrEmpty(fullNameImage) ? this.GetSvgImage(fullNameImage, paletteProvider, new Size(width, height)) : null);
        }

        public void ResetSvgCache()
        {
            this.svgRenderedImageCache.Clear();
        }

        public static ImageResourceCache Default
        {
            get
            {
                defaultCore ??= DoLoad();
                return defaultCore;
            }
        }

        protected internal IList<string> SvgImages =>
            this.svgImages;
    }
}

