namespace DevExpress.Xpf.Printing.Native.Dialogs
{
    using DevExpress.Mvvm;
    using System;
    using System.ComponentModel;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class SystemFileDialogHelper
    {
        public static void AssignImageFileFilter(this IOpenFileDialogService service)
        {
            ImageCodecInfo[] imageDecoders = ImageCodecInfo.GetImageDecoders();
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            foreach (ImageCodecInfo info in imageDecoders)
            {
                string str2 = info.FilenameExtension.ToLower();
                builder.AppendFormat($"{info.FormatDescription} ({str2})|{str2}|", new object[0]);
                if (builder2.Length > 0)
                {
                    builder2.Append(";");
                }
                builder2.Append(info.FilenameExtension);
            }
            builder.AppendFormat("SVG (*.svg)|*.svg|", new object[0]);
            if (builder2.Length > 0)
            {
                builder2.Append(";");
            }
            builder2.Append("*.svg");
            string str = builder2.ToString().ToLower();
            builder.Append($"All image files ({str})|{str}|");
            builder.Append("All files (*.*)|*.*");
            service.Filter = builder.ToString();
            service.FilterIndex = imageDecoders.Length + 2;
        }

        public static string CoerceDirectory(string directory)
        {
            string path = directory;
            if (!Directory.Exists(directory))
            {
                path = GetDefaultDirectory();
                if (string.IsNullOrEmpty(directory) || !Directory.Exists(path))
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                }
            }
            return path;
        }

        [SecuritySafeCritical]
        public static string GetDefaultDirectory()
        {
            OperatingSystem oSVersion = Environment.OSVersion;
            if ((oSVersion.Platform != PlatformID.Win32NT) || (oSVersion.Version.Major < 6))
            {
                return string.Empty;
            }
            try
            {
                IShellItem item;
                string str;
                NativeFileOpenDialog o = (NativeFileOpenDialog) new FileOpenDialogRCW();
                o.GetFolder(out item);
                item.GetDisplayName((SIGDN) (-2147123200), out str);
                Marshal.ReleaseComObject(item);
                Marshal.ReleaseComObject(o);
                return str;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}

