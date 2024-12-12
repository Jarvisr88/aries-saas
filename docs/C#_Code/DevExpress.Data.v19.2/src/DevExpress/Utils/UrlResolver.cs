namespace DevExpress.Utils
{
    using DevExpress.Utils.Url;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;

    public abstract class UrlResolver
    {
        private static readonly object lockInstance = new object();
        private static UrlResolver instance;

        protected UrlResolver()
        {
        }

        [Obsolete("Use the UrlResolver.TryMapPath method instead.")]
        public virtual bool CanMapPath(string url) => 
            true;

        public static UrlResolver CreateInstance()
        {
            string str;
            return (TryGetResourceDirectory(out str) ? ((UrlResolver) new RelativeUrlResolver(str)) : (PSNativeMethods.HasHttpContext ? ((UrlResolver) new HttpUrlResolver()) : ((UrlResolver) new RelativeUrlResolver(AppDomain.CurrentDomain.BaseDirectory))));
        }

        protected bool IsAbsolutePath(string path)
        {
            PlatformID platform = Environment.OSVersion.Platform;
            return ((platform != PlatformID.Win32NT) ? (((platform == PlatformID.Unix) || (platform == PlatformID.MacOSX)) ? UnixPath.IsFullyQualified(path) : false) : (path.StartsWith(@"\\") || path.Contains<char>(Path.VolumeSeparatorChar)));
        }

        [Obsolete("Use the UrlResolver.TryMapPath method instead.")]
        public virtual string MapPath(string url) => 
            string.Empty;

        public static string TrimUrl(string url)
        {
            char[] trimChars = new char[] { '~', Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
            return url.TrimStart(trimChars);
        }

        public virtual bool TryGetRelativePath(string url, out string value)
        {
            value = string.Empty;
            return false;
        }

        protected static bool TryGetResourceDirectory(out string directory)
        {
            directory = AppDomain.CurrentDomain.GetData("DXResourceDirectory") as string;
            return !string.IsNullOrEmpty(directory);
        }

        public abstract bool TryMapPath(string path, out string value);

        public static UrlResolver Instance
        {
            get
            {
                if (instance == null)
                {
                    object lockInstance = UrlResolver.lockInstance;
                    lock (lockInstance)
                    {
                        instance ??= CreateInstance();
                    }
                }
                return instance;
            }
        }
    }
}

