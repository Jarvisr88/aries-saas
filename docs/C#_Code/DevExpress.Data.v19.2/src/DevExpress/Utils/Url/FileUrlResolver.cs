namespace DevExpress.Utils.Url
{
    using DevExpress.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class FileUrlResolver : UrlResolver
    {
        public FileUrlResolver(string directory)
        {
            this.Directory = directory;
        }

        public override bool TryMapPath(string path, out string value)
        {
            string text1;
            if (path == null)
            {
                text1 = null;
            }
            else
            {
                char[] trimChars = new char[] { ' ' };
                text1 = path.Trim(trimChars);
            }
            string local1 = text1;
            string text2 = local1;
            if (local1 == null)
            {
                string local2 = local1;
                text2 = "";
            }
            string str = text2;
            if (string.IsNullOrEmpty(str) || base.IsAbsolutePath(str))
            {
                value = string.Empty;
                return false;
            }
            if (!str.StartsWith(".."))
            {
                char[] trimChars = new char[] { '~', '.' };
                str = str.TrimStart(trimChars);
            }
            char[] separator = new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
            str = Path.Combine(str.Split(separator));
            value = Path.Combine(this.Directory, str);
            return true;
        }

        protected string Directory { virtual get; private set; }
    }
}

