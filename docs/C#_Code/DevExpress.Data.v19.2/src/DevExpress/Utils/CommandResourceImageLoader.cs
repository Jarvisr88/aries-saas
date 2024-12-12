namespace DevExpress.Utils
{
    using DevExpress.Data.Utils;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Reflection;

    public class CommandResourceImageLoader
    {
        public static Bitmap CreateBitmapFromResources(string name, Assembly asm) => 
            (Bitmap) ImageTool.ImageFromStream(asm.GetManifestResourceStream(name));

        public static string GetImageName(string resourcePath, string imageName, string size) => 
            string.Format(resourcePath + ".{0}_{1}.png", imageName, size);

        internal static string GetLargeImageName(string resourcePath, string imageName) => 
            GetImageName(resourcePath, imageName, "32x32");

        internal static string GetSmallImageName(string resourcePath, string imageName) => 
            GetImageName(resourcePath, imageName, "16x16");

        public static Image LoadLargeImage(string resourcePath, string imageName, Assembly asm) => 
            CreateBitmapFromResources(GetLargeImageName(resourcePath, imageName), asm);

        public static Image LoadSmallImage(string resourcePath, string imageName, Assembly asm) => 
            CreateBitmapFromResources(GetSmallImageName(resourcePath, imageName), asm);

        public static Stream LoadSmallImageStream(string resourcePath, string imageName, Assembly asm) => 
            asm.GetManifestResourceStream(GetSmallImageName(resourcePath, imageName));
    }
}

