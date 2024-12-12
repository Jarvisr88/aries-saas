namespace DevExpress.Office.Services.Implementation
{
    using DevExpress.Office.Services;
    using DevExpress.Utils;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    [ComVisible(true)]
    public class DataStringUriStreamProvider : IUriStreamProvider
    {
        private bool AreEqual(string str1, string str2) => 
            StringExtensions.CompareInvariantCultureIgnoreCase(str1, str2) == 0;

        private Stream ConvertToStream(string content)
        {
            byte[] buffer;
            try
            {
                buffer = Convert.FromBase64String(content);
            }
            catch
            {
                content = Uri.UnescapeDataString(content);
                buffer = Convert.FromBase64String(content);
            }
            return new MemoryStream(buffer);
        }

        public Stream GetStream(string uri)
        {
            if (uri.StartsWithInvariantCultureIgnoreCase("data:"))
            {
                Match match = DataStringUriPattern.regex.Match(uri);
                if (match.Success)
                {
                    if (this.AreEqual(match.Groups["capacity"].Value, "base64"))
                    {
                        return this.ConvertToStream(match.Groups["image"].Value);
                    }
                }
                else
                {
                    match = DataStringUriPattern.applicationDataStringRegex.Match(uri);
                    if (this.AreEqual(match.Groups["mediaType"].Value, "octet-stream") && this.AreEqual(match.Groups["capacity"].Value, "base64"))
                    {
                        return this.ConvertToStream(match.Groups["content"].Value);
                    }
                }
            }
            return null;
        }

        public bool IsUriSupported(string uri)
        {
            if (!uri.StartsWithInvariantCultureIgnoreCase("data:"))
            {
                return false;
            }
            Match match = DataStringUriPattern.regex.Match(uri);
            if (match.Success)
            {
                return this.AreEqual(match.Groups["capacity"].Value, "base64");
            }
            match = DataStringUriPattern.applicationDataStringRegex.Match(uri);
            return (this.AreEqual(match.Groups["mediaType"].Value, "octet-stream") && this.AreEqual(match.Groups["capacity"].Value, "base64"));
        }
    }
}

