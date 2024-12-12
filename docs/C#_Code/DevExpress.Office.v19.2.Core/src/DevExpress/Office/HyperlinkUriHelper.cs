namespace DevExpress.Office
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    public static class HyperlinkUriHelper
    {
        private const string FileNamePattern = @"^file:[\\/]*(?<path>(?<root>[A-Za-z]:[\\/]+)?[^:\*\?<>\|]+)$";
        private const string UrlPattern = @"^((?<prefix>http|https|ftp|news)\:[\\/]*)(?<url>[a-zA-Z0-9\-\.]+(\.[a-zA-Z]{2,3})?(\:\d{1,5})?([\\/]\S*)?)$";
        private const string RelativePathPattern = @"^(?:\.{1,2}[\\/])*[^:\*\?<>\|\.\\/][^:\*\?<>\|]*$";
        private const string LocalPathPattern = @"^[A-Za-z]:[\\/]+[^:\*\?<>\|]*$";
        private const string RemotePathPattern = @"^\\{2,}(?<path>[^:\*\?<>\|]+)$";
        private const string MailAddressPattern = @"^mailto:(?<email>\S+)$";
        private const string TooltipQuotesPattern = "\"";
        private const string FromHyperlinkUriPattern = @"\\\\";
        private const string SlashPattern = @"[\\/]+";
        private static Regex RemotePathRegex = new Regex(@"^\\{2,}(?<path>[^:\*\?<>\|]+)$");
        private static Regex LocalPathRegex = new Regex(@"^[A-Za-z]:[\\/]+[^:\*\?<>\|]*$");
        private static Regex RelativePathRegex = new Regex(@"^(?:\.{1,2}[\\/])*[^:\*\?<>\|\.\\/][^:\*\?<>\|]*$");
        private static Regex FileNameRegex = new Regex(@"^file:[\\/]*(?<path>(?<root>[A-Za-z]:[\\/]+)?[^:\*\?<>\|]+)$");
        private static Regex UrlRegex = new Regex(@"^((?<prefix>http|https|ftp|news)\:[\\/]*)(?<url>[a-zA-Z0-9\-\.]+(\.[a-zA-Z]{2,3})?(\:\d{1,5})?([\\/]\S*)?)$");
        private static Regex MailAddressRegex = new Regex(@"^mailto:(?<email>\S+)$");
        private static Regex TooltipQuotesRegex = new Regex("\"");
        private static Regex FromHyperlinkUriRegex = new Regex(@"\\\\");
        private static Regex BackslashRegex = new Regex(@"\\");

        public static string ConvertFromHyperlinkUri(string uri) => 
            FromHyperlinkUriRegex.Replace(uri, @"\");

        public static string ConvertRelativePathToAbsolute(string uri, string baseUri)
        {
            List<string> values = new List<string>(Split(baseUri, @"[\\/]+"));
            values.RemoveAt(values.Count - 1);
            values.AddRange(Split(uri, @"[\\/]+"));
            for (int i = 0; i < values.Count; i++)
            {
                if (values[i] == ".")
                {
                    values.RemoveAt(i);
                }
                else if (values[i] != "..")
                {
                    i++;
                }
                else if (i == 0)
                {
                    values.RemoveAt(0);
                }
                else
                {
                    values.RemoveRange(i - 1, 2);
                    i--;
                }
            }
            return EnsureLocalPathIsValid(string.Join(@"\", values));
        }

        public static string ConvertToHyperlinkUri(string uri) => 
            BackslashRegex.Replace(uri, @"\\");

        public static string ConvertToUrl(string uri) => 
            !IsLocalPath(uri) ? (!IsRemotePath(uri) ? uri : Regex.Replace($"file://{GetRemotePath(uri)}", @"\\+", "/")) : Regex.Replace($"file:///{uri}", @"\\+", "/");

        private static string EnsureFileUriIsValid(string uri)
        {
            Match match = FileNameRegex.Match(uri);
            string[] strArray = Split(match.Groups["path"].Value, @"[\\/]+");
            string str = string.Join(@"\", strArray);
            return (!match.Groups["root"].Success ? $"\\{str}" : str);
        }

        private static string EnsureLocalPathIsValid(string uri)
        {
            string[] strArray = Split(uri, @"[\\/]+");
            string str = string.Join(@"\", strArray);
            return ((strArray.Length != 1) ? str : (str + @"\"));
        }

        private static string EnsureMailAddressIsValid(string uri)
        {
            Match match = MailAddressRegex.Match(uri);
            return $"mailto:{match.Groups["email"].Value}";
        }

        private static string EnsureRelativePathIsValid(string uri)
        {
            List<string> values = new List<string>(Split(uri, @"[\\/]+"));
            Predicate<string> match = <>c.<>9__30_0;
            if (<>c.<>9__30_0 == null)
            {
                Predicate<string> local1 = <>c.<>9__30_0;
                match = <>c.<>9__30_0 = item => item.Equals(".", StringComparison.Ordinal);
            }
            values.RemoveAll(match);
            return string.Join(@"\", values);
        }

        private static string EnsureRemotePathIsValid(string uri)
        {
            string[] strArray = Split(RemotePathRegex.Match(uri).Groups["path"].Value, @"[\\/]+");
            return $"\\{string.Join(@"\", strArray)}";
        }

        public static string EnsureUriIsValid(string uri) => 
            !IsFileUri(uri) ? (!IsUrl(uri) ? (!IsLocalPath(uri) ? (!IsRemotePath(uri) ? (!IsMailAddress(uri) ? (!IsRelativePath(uri) ? uri : EnsureRelativePathIsValid(uri)) : EnsureMailAddressIsValid(uri)) : EnsureRemotePathIsValid(uri)) : EnsureLocalPathIsValid(uri)) : EnsureUrlIsValid(uri)) : EnsureFileUriIsValid(uri);

        private static string EnsureUrlIsValid(string uri)
        {
            Match match = UrlRegex.Match(uri);
            string input = match.Groups["url"].Value;
            string str2 = match.Groups["prefix"].Value;
            if (string.IsNullOrEmpty(str2))
            {
                str2 = "http";
            }
            return $"{str2}://{Regex.Replace(input, @"\\+", "/")}";
        }

        public static string EscapeHyperlinkFieldParameterString(string value) => 
            BackslashRegex.Replace(value, @"\\");

        private static string GetRemotePath(string uri) => 
            RemotePathRegex.Match(uri).Groups["path"].Value;

        internal static bool IsFileUri(string uri) => 
            FileNameRegex.IsMatch(uri);

        public static bool IsLocalPath(string uri) => 
            LocalPathRegex.IsMatch(uri);

        internal static bool IsMailAddress(string uri) => 
            MailAddressRegex.IsMatch(uri);

        public static bool IsRelativePath(string uri) => 
            RelativePathRegex.IsMatch(uri);

        internal static bool IsRemotePath(string uri) => 
            RemotePathRegex.IsMatch(uri);

        internal static bool IsUrl(string uri) => 
            UrlRegex.IsMatch(uri);

        public static string PrepareHyperlinkTooltipQuotes(string value) => 
            TooltipQuotesRegex.Replace(value, "\\\"");

        internal static string[] Split(string uri, string pattern) => 
            Regex.Split(uri, pattern);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly HyperlinkUriHelper.<>c <>9 = new HyperlinkUriHelper.<>c();
            public static Predicate<string> <>9__30_0;

            internal bool <EnsureRelativePathIsValid>b__30_0(string item) => 
                item.Equals(".", StringComparison.Ordinal);
        }
    }
}

