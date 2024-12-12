namespace DevExpress.Images
{
    using DevExpress.Utils.Design;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal static class ImageCacheUtils
    {
        private static readonly char[] splitCharacters = new char[] { '\\', '/' };
        internal const string ColoredCategory = "images";
        internal const string DevAVCategory = "devav";
        internal const string GrayScaledCategory = "grayscaleimages";
        internal const string Office2013Category = "office2013";
        internal const string SvgCategory = "svgimages";
        public static readonly string SvgImageSuffix = ".svg";
        public static readonly string ImageSuffix = "_16x16.png";
        public static readonly string LargeImageSuffix = "_32x32.png";

        public static void ExtractPrefix(ref string key, out string prefix)
        {
            prefix = string.Empty;
            if (!string.IsNullOrEmpty(key))
            {
                string[] strArray = Split(key);
                if (strArray.Length > 1)
                {
                    prefix = strArray[0].ToLower();
                    key = strArray[strArray.Length - 1];
                }
            }
        }

        internal static string GetCategory(ImageType imageType)
        {
            switch (imageType)
            {
                case ImageType.Colored:
                    return "images";

                case ImageType.GrayScaled:
                    return "grayscaleimages";

                case ImageType.Office2013:
                    return "office2013";

                case ImageType.DevAV:
                    return "devav";

                case ImageType.Svg:
                    return "svgimages";
            }
            throw new ArgumentException("imageType");
        }

        public static string GetFileName(string id, ImageSize imageSize, ImageType imageType) => 
            (imageType != ImageType.Svg) ? ((imageSize != ImageSize.Size16x16) ? (id + LargeImageSuffix) : (id + ImageSuffix)) : (id + SvgImageSuffix);

        public static bool IsMatch(string key, string prefix, string fileName, ImageType imageType) => 
            IsMatch(key, prefix, fileName, GetCategory(imageType));

        public static bool IsMatch(string key, string prefix, string fileName, string category)
        {
            if (!key.EndsWith(fileName, StringComparison.Ordinal))
            {
                return false;
            }
            string[] strArray = Split(key);
            bool flag = strArray[0].Equals(category, StringComparison.Ordinal) && strArray[strArray.Length - 1].Equals(fileName, StringComparison.Ordinal);
            if (!string.IsNullOrEmpty(prefix))
            {
                flag &= key.IndexOf(prefix, 0, StringComparison.Ordinal) >= 0;
            }
            return flag;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string[] Split(string key) => 
            key.Split(splitCharacters);
    }
}

