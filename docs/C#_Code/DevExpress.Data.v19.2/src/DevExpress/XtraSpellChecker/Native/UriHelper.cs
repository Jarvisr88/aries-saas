namespace DevExpress.XtraSpellChecker.Native
{
    using System;
    using System.Collections.Generic;

    public static class UriHelper
    {
        private static readonly HashSet<string> knownExtensions = CreateKnownExtensions();
        private static readonly HashSet<string> subdomainPrefixes = CreateSubdomainPrefixes();
        private static readonly HashSet<string> topLevelDomains = CreateTopLevelDomains();
        private static readonly HashSet<string> schemes = CreateSchemas();
        private static HashSet<char> uriCharacters = CreateUriCharacters();

        private static HashSet<string> CreateKnownExtensions() => 
            new HashSet<string> { 
                "doc",
                "docx",
                "log",
                "msg",
                "rtf",
                "txt",
                "csv",
                "dat",
                "pps",
                "ppt",
                "pptx",
                "tar",
                "vcf",
                "xml",
                "aif",
                "mid",
                "mp3",
                "mpa",
                "wav",
                "wma",
                "avi",
                "mov",
                "mp4",
                "mpg",
                "rm",
                "swf",
                "wmv",
                "obj",
                "bmp",
                "gif",
                "jpg",
                "png",
                "tiff",
                "ai",
                "ps",
                "pct",
                "pdf",
                "xls",
                "xlsx",
                "accdb",
                "mdb",
                "pdb",
                "bat",
                "com",
                "exe",
                "jar",
                "wsf",
                "asp",
                "aspx",
                "cer",
                "css",
                "htm",
                "html",
                "js",
                "fon",
                "otf",
                "ttf",
                "cab",
                "dll",
                "drv",
                "ico",
                "sys",
                "ini",
                "hqx",
                "gz",
                "tar.gz",
                "zip",
                "cpp",
                "dtd",
                "java",
                "pl",
                "py",
                "sh",
                "sln",
                "ics",
                "msi"
            };

        private static HashSet<string> CreateSchemas() => 
            new HashSet<string> { 
                "file",
                "ftp",
                "gopher",
                "http",
                "https",
                "mailto",
                "telnet",
                "news",
                "nntp"
            };

        private static HashSet<string> CreateSubdomainPrefixes() => 
            new HashSet<string> { 
                "www",
                "ftp"
            };

        private static HashSet<string> CreateTopLevelDomains() => 
            new HashSet<string> { 
                "com",
                "org",
                "net",
                "int",
                "edu",
                "gov",
                "mil",
                "ac",
                "ad",
                "ae",
                "af",
                "ag",
                "ai",
                "al",
                "am",
                "an",
                "ao",
                "aq",
                "ar",
                "as",
                "at",
                "au",
                "aw",
                "ax",
                "az",
                "ba",
                "bb",
                "bd",
                "be",
                "bf",
                "bg",
                "bh",
                "bi",
                "bj",
                "bm",
                "bn",
                "bo",
                "bq",
                "br",
                "bs",
                "bt",
                "bv",
                "bw",
                "by",
                "bz",
                "ca",
                "cc",
                "cd",
                "cf",
                "cg",
                "ch",
                "ci",
                "ck",
                "cl",
                "cm",
                "cn",
                "co",
                "cr",
                "cs",
                "cu",
                "cv",
                "cw",
                "cx",
                "cy",
                "cz",
                "dd",
                "de",
                "dj",
                "dk",
                "dm",
                "do",
                "dz",
                "ec",
                "ee",
                "eg",
                "eh",
                "er",
                "es",
                "et",
                "eu",
                "fi",
                "fj",
                "fk",
                "fm",
                "fo",
                "fr",
                "ga",
                "gb",
                "gd",
                "ge",
                "gf",
                "gg",
                "gh",
                "gi",
                "gl",
                "gm",
                "gn",
                "gp",
                "gq",
                "gr",
                "gs",
                "gt",
                "gu",
                "gw",
                "gy",
                "hk",
                "hm",
                "hn",
                "hr",
                "ht",
                "hu",
                "id",
                "ie",
                "il",
                "im",
                "in",
                "io",
                "iq",
                "ir",
                "is",
                "it",
                "je",
                "jm",
                "jo",
                "jp",
                "ke",
                "kg",
                "kh",
                "ki",
                "km",
                "kn",
                "kp",
                "kr",
                "kw",
                "ky",
                "kz",
                "la",
                "lb",
                "lc",
                "li",
                "lk",
                "lr",
                "ls",
                "lt",
                "lu",
                "lv",
                "ly",
                "ma",
                "mc",
                "md",
                "me",
                "mg",
                "mh",
                "mk",
                "ml",
                "mm",
                "mn",
                "mo",
                "mp",
                "mq",
                "mr",
                "ms",
                "mt",
                "mu",
                "mv",
                "mw",
                "mx",
                "my",
                "mz",
                "na",
                "nc",
                "ne",
                "nf",
                "ng",
                "ni",
                "nl",
                "no",
                "np",
                "nr",
                "nu",
                "nz",
                "om",
                "pa",
                "pe",
                "pf",
                "pg",
                "ph",
                "pk",
                "pl",
                "pm",
                "pn",
                "pr",
                "ps",
                "pt",
                "pw",
                "py",
                "qa",
                "re",
                "ro",
                "rs",
                "ru",
                "rw",
                "sa",
                "sb",
                "sc",
                "sd",
                "se",
                "sg",
                "sh",
                "si",
                "sj",
                "sk",
                "sl",
                "sm",
                "sn",
                "so",
                "sr",
                "ss",
                "st",
                "su",
                "sv",
                "sx",
                "sy",
                "sz",
                "tc",
                "td",
                "tf",
                "tg",
                "th",
                "tj",
                "tk",
                "tl",
                "tm",
                "tn",
                "to",
                "tp",
                "tr",
                "tt",
                "tv",
                "tw",
                "tz",
                "ua",
                "ug",
                "uk",
                "us",
                "uy",
                "uz",
                "va",
                "vc",
                "ve",
                "vg",
                "vi",
                "vn",
                "vu",
                "wf",
                "ws",
                "ye",
                "yt",
                "yu",
                "za",
                "zm",
                "zr",
                "zw"
            };

        private static HashSet<char> CreateUriCharacters() => 
            new HashSet<char> { 
                '\\',
                '/',
                '=',
                '?',
                '#',
                '%',
                '$',
                '-',
                '_',
                '.',
                '+',
                '!',
                '*',
                '\'',
                '(',
                ')',
                ':'
            };

        public static bool IsAbsoluteFileUri(string uriString)
        {
            Uri uri;
            return (Uri.TryCreate(uriString, UriKind.Absolute, out uri) && uri.IsFile);
        }

        private static bool IsFileExtension(string value) => 
            knownExtensions.Contains(value);

        public static bool IsFileUri(string uriString)
        {
            if (IsAbsoluteFileUri(uriString))
            {
                return true;
            }
            int num = uriString.LastIndexOf('.');
            return (((num > 0) && (num != (uriString.Length - 1))) && IsFileExtension(uriString.Substring(num + 1)));
        }

        private static bool IsKnownScheme(string value) => 
            schemes.Contains(value);

        private static bool IsSubdomainPrefix(string value) => 
            subdomainPrefixes.Contains(value);

        private static bool IsTopLevelDomain(string value) => 
            topLevelDomains.Contains(value);

        public static bool IsUri(string uriString) => 
            IsFileUri(uriString) || IsWebUri(uriString);

        public static bool IsUriCharacter(char ch) => 
            uriCharacters.Contains(ch);

        public static bool IsUriString(string uriString) => 
            IsAbsoluteFileUri(uriString) || IsWebUri(uriString);

        public static bool IsWebUri(string urlString)
        {
            bool flag = false;
            bool flag2 = false;
            int startIndex = 0;
            int length = 0;
            while ((startIndex + length) < urlString.Length)
            {
                char c = urlString[startIndex + length];
                if (flag2)
                {
                    if ((length != 0) && (length != 1))
                    {
                        return true;
                    }
                    if (!(!flag ? (c == '/') : ((c == '/') || char.IsLetterOrDigit(c))))
                    {
                        return false;
                    }
                    length++;
                    continue;
                }
                if (c == ':')
                {
                    string str = urlString.Substring(startIndex, length);
                    if (!(!IsKnownScheme(str) ? Uri.CheckSchemeName(str) : true))
                    {
                        return false;
                    }
                    startIndex += length + 1;
                    length = 0;
                    continue;
                }
                if (c != '.')
                {
                    length++;
                    continue;
                }
                if ((startIndex == 0) && IsSubdomainPrefix(urlString.Substring(startIndex, length)))
                {
                    return true;
                }
                startIndex += length + 1;
                length = 0;
                if (IsTopLevelDomain(urlString.Substring(startIndex)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

