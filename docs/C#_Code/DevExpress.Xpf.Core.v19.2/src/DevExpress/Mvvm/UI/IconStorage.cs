namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Cryptography;
    using System.Windows.Media;

    public class IconStorage : IIconStorage
    {
        private static readonly byte[] PngIconHeaderBytes = new byte[] { 
            0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0x18, 0, 0, 0,
            0, 0, 0, 0, 0, 0
        };
        private Func<Uri> baseUri;

        public IconStorage(Func<Uri> baseUri)
        {
            this.baseUri = baseUri;
        }

        [DllImport("user32.dll")]
        private static extern bool DestroyIcon(IntPtr handle);
        private static string GetImageHash(byte[] image)
        {
            using (SHA1 sha = SHA1.Create())
            {
                return Convert.ToBase64String(sha.ComputeHash(image));
            }
        }

        [SecuritySafeCritical]
        private static void SaveBmpImageAsIcon(string imagePath, byte[] imageBytes, Image image)
        {
            using (Bitmap bitmap = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.DrawImage(image, 0, 0, image.Width, image.Height);
                }
                IntPtr hicon = bitmap.GetHicon();
                try
                {
                    using (Icon icon = Icon.FromHandle(hicon))
                    {
                        WriteIconToFile(imagePath, f => icon.Save(f));
                    }
                }
                finally
                {
                    DestroyIcon(hicon);
                }
            }
        }

        private static void SaveIcon(string imagePath, byte[] imageBytes, Icon icon)
        {
            WriteIconToFile(imagePath, f => f.Write(imageBytes, 0, imageBytes.Length));
        }

        private static void SaveImageAsIcon(string imagePath, byte[] imageBytes, Image image)
        {
            if (!SavePngImageAsIcon(imagePath, imageBytes, image))
            {
                SaveBmpImageAsIcon(imagePath, imageBytes, image);
            }
        }

        private static bool SavePngImageAsIcon(string imagePath, byte[] imageBytes, Image image)
        {
            if ((image.Width > 0xff) || ((image.Height > 0xff) || ((imageBytes.Length > 0xffff) || !Equals(image.RawFormat, ImageFormat.Png))))
            {
                return false;
            }
            byte[] header = new byte[PngIconHeaderBytes.Length];
            PngIconHeaderBytes.CopyTo(header, 0);
            header[6] = (byte) image.Width;
            header[7] = (byte) image.Height;
            header[14] = (byte) (imageBytes.Length % 0x100);
            header[15] = (byte) (imageBytes.Length / 0x100);
            header[0x12] = (byte) header.Length;
            WriteIconToFile(imagePath, delegate (Stream f) {
                f.Write(header, 0, header.Length);
                f.Write(imageBytes, 0, imageBytes.Length);
            });
            return true;
        }

        private static Icon TryCreateIcon(byte[] imageBytes)
        {
            Icon icon;
            MemoryStream stream = new MemoryStream(imageBytes);
            try
            {
                icon = new Icon(stream);
            }
            catch (ArgumentException)
            {
                icon = null;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
            }
            return icon;
        }

        private static Image TryCreateImage(byte[] imageBytes)
        {
            Image image;
            MemoryStream stream = new MemoryStream(imageBytes);
            try
            {
                image = Image.FromStream(stream);
            }
            catch (ArgumentException)
            {
                image = null;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
            }
            return image;
        }

        public bool TryStoreIconToFile(ImageSource icon, string storageFolder, out string iconID, out string iconPath)
        {
            GuardHelper.ArgumentNotNull(icon, "icon");
            byte[] image = null;
            try
            {
                Size? drawingImageSize = null;
                image = ImageLoader2.ImageToByteArray(icon, this.baseUri, drawingImageSize);
            }
            catch (Exception)
            {
                iconID = null;
                iconPath = null;
                return false;
            }
            iconID = NativeResourceManager.CreateFileName(GetImageHash(image)) + ".ico";
            iconPath = UnpackIcon(Path.Combine(storageFolder, iconID), image);
            return (iconPath != null);
        }

        private static string UnpackIcon(string imagePath, byte[] imageBytes)
        {
            string str;
            if (File.Exists(imagePath))
            {
                return imagePath;
            }
            Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
            Icon icon = TryCreateIcon(imageBytes);
            if (icon != null)
            {
                using (icon)
                {
                    SaveIcon(imagePath, imageBytes, icon);
                    str = imagePath;
                }
            }
            else
            {
                Image image = TryCreateImage(imageBytes);
                if (image == null)
                {
                    return null;
                }
                using (image)
                {
                    SaveImageAsIcon(imagePath, imageBytes, image);
                    str = imagePath;
                }
            }
            return str;
        }

        private static void WriteIconToFile(string imagePath, Action<Stream> writeAction)
        {
            try
            {
                using (FileStream stream = new FileStream(imagePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    writeAction(stream);
                }
            }
            catch (IOException)
            {
            }
        }
    }
}

