namespace DevExpress.Utils
{
    using DevExpress.Data.Utils;
    using DevExpress.Utils.Svg;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    public static class ResourceImageHelper
    {
        public static Bitmap CreateBitmapFromResources(string name, Assembly asm)
        {
            Image image = CreateImageFromResources(name, asm);
            return ((image != null) ? ((Bitmap) image) : null);
        }

        public static Bitmap CreateBitmapFromResources(string name, System.Type type) => 
            CreateBitmapFromResources(ResourceStreamHelper.GetResourceName(type, name), type.Assembly);

        public static Cursor CreateCursorFromResources(string name, Assembly asm) => 
            new Cursor(asm.GetManifestResourceStream(name));

        public static Icon CreateIconFromResources(string name, Assembly asm) => 
            new Icon(asm.GetManifestResourceStream(name));

        public static Icon CreateIconFromResources(string name, System.Type type) => 
            CreateIconFromResources(ResourceStreamHelper.GetResourceName(type, name), type.Assembly);

        public static Icon CreateIconFromResourcesEx(string name, Assembly asm)
        {
            Stream stream = FindStream(name, asm);
            return ((stream != null) ? new Icon(stream) : null);
        }

        public static Image CreateImageFromResources(string name, Assembly asm)
        {
            Stream manifestResourceStream = asm.GetManifestResourceStream(name);
            return ((manifestResourceStream != null) ? ImageTool.ImageFromStream(manifestResourceStream) : null);
        }

        public static Image CreateImageFromResources(string name, System.Type type) => 
            CreateImageFromResources(ResourceStreamHelper.GetResourceName(type, name), type.Assembly);

        public static Image CreateImageFromResourcesEx(string name, Assembly asm)
        {
            Stream stream = FindStream(name, asm);
            return ((stream != null) ? ImageTool.ImageFromStream(stream) : null);
        }

        public static ImageList CreateImageListFromResources(string name, Assembly asm, Size size) => 
            CreateImageListFromResources(name, asm, size, Color.Magenta);

        public static ImageList CreateImageListFromResources(string name, Assembly asm, Size size, Color transparent) => 
            CreateImageListFromResources(name, asm, size, transparent, ColorDepth.Depth8Bit);

        public static ImageList CreateImageListFromResources(string name, Assembly asm, Size size, Color transparent, ColorDepth depth)
        {
            if (transparent == Color.Empty)
            {
                transparent = Color.Magenta;
            }
            ImageList images = new ImageList {
                ColorDepth = depth,
                ImageSize = size.IsEmpty ? new Size(0x10, 0x10) : size
            };
            FillImageListFromResources(images, name, asm, transparent);
            return images;
        }

        public static SvgImage CreateSvgImageFromResources(string name, Assembly asm)
        {
            Stream manifestResourceStream = asm.GetManifestResourceStream(name);
            return ((manifestResourceStream != null) ? SvgImage.FromStream(manifestResourceStream) : null);
        }

        public static SvgImage CreateSvgImageFromResources(string name, System.Type type) => 
            CreateSvgImageFromResources(ResourceStreamHelper.GetResourceName(type, name), type.Assembly);

        public static void FillImageListFromResources(ImageList images, string name, Assembly asm)
        {
            FillImageListFromResources(images, name, asm, images.TransparentColor);
        }

        public static void FillImageListFromResources(ImageList images, string name, System.Type type)
        {
            FillImageListFromResources(images, ResourceStreamHelper.GetResourceName(type, name), type.Assembly);
        }

        public static void FillImageListFromResources(ImageList images, string name, Assembly asm, Color transparent)
        {
            Bitmap bitmap = CreateBitmapFromResources(name, asm);
            bitmap.MakeTransparent(transparent);
            images.Images.AddStrip(bitmap);
        }

        public static string FindResourceName(string name, Assembly asm)
        {
            string[] manifestResourceNames = asm.GetManifestResourceNames();
            if (Array.IndexOf<string>(manifestResourceNames, name) >= 0)
            {
                return name;
            }
            char[] separator = new char[] { '.' };
            string[] strArray2 = name.Split(separator);
            if (strArray2.Length < 2)
            {
                return null;
            }
            string str = strArray2.GetValue((int) (strArray2.Length - 2)) + "." + strArray2.GetValue((int) (strArray2.Length - 1));
            return ((Array.IndexOf<string>(manifestResourceNames, str) >= 0) ? str : null);
        }

        public static Stream FindStream(string name, Assembly asm) => 
            asm.GetManifestResourceStream(FindResourceName(name, asm));
    }
}

