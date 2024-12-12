namespace DevExpress.Utils.Url
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    public class RelativeUrlResolver : FileUrlResolver
    {
        public RelativeUrlResolver(string directory) : base(directory)
        {
        }

        public override bool TryGetRelativePath(string url, out string value)
        {
            string text3;
            string directory = this.Directory;
            if (directory == null)
            {
                string local1 = directory;
                text3 = null;
            }
            else
            {
                char[] trimChars = new char[] { Path.DirectorySeparatorChar };
                text3 = directory.TrimEnd(trimChars);
            }
            string local2 = text3;
            string text4 = local2;
            if (local2 == null)
            {
                string local3 = local2;
                string text2 = Path.DirectorySeparatorChar.ToString();
                text4 = text2;
                if (text2 == null)
                {
                    string local4 = text2;
                    text4 = "";
                }
            }
            string str = text4;
            if (!string.IsNullOrEmpty(str) && (!string.IsNullOrEmpty(url) && url.StartsWith(str)))
            {
                value = url.Remove(0, str.Length + 1);
                return true;
            }
            value = string.Empty;
            return false;
        }
    }
}

