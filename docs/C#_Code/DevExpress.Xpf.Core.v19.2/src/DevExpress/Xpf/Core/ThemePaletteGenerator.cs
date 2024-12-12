namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    internal static class ThemePaletteGenerator
    {
        private static readonly Regex validateThemeNameRegex = new Regex("^[_0-9a-zA-Z]*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        internal static readonly string DefaultCacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"DevExpress\PaletteThemeCache");

        [SecurityCritical]
        private static void CacheAssembly(string newThemeName, MemoryStream stream, string hash)
        {
            string paletteThemeCacheDirectory = Theme.PaletteThemeCacheDirectory;
            if (!Directory.Exists(paletteThemeCacheDirectory))
            {
                Directory.CreateDirectory(paletteThemeCacheDirectory);
            }
            string cacheAssemblyPath = GetCacheAssemblyPath(newThemeName);
            IAssemblyDefinition definition = MonoCecilHelper.ReadAssembly(stream);
            if (File.Exists(cacheAssemblyPath))
            {
                File.Delete(cacheAssemblyPath);
            }
            definition.Write(cacheAssemblyPath);
            File.WriteAllText(GetHashFilePath(cacheAssemblyPath), hash);
        }

        [SecuritySafeCritical]
        internal static string CalcHash(ThemePaletteBase palette, Theme baseTheme, string newThemeName)
        {
            string str;
            byte[] buffer = File.ReadAllBytes(baseTheme.Assembly.Location);
            using (MemoryStream stream = new MemoryStream(buffer))
            {
                IAssemblyDefinition themeAssembly = MonoCecilHelper.ReadAssembly(stream);
                Dictionary<string, Color> source = palette.GetColors(themeAssembly, newThemeName, baseTheme.Name);
                using (SHA1 sha1 = SHA1.Create())
                {
                    IEnumerable<byte> colorsHash = Enumerable.Empty<byte>();
                    source.ForEach<KeyValuePair<string, Color>>(delegate (KeyValuePair<string, Color> x) {
                        colorsHash = colorsHash.Concat<byte>(sha1.ComputeHash(GetBytes(x.Key)));
                        colorsHash = colorsHash.Concat<byte>(sha1.ComputeHash(GetBytes(x.Value.ToString())));
                    });
                    byte[] second = sha1.ComputeHash(buffer);
                    str = Convert.ToBase64String(sha1.ComputeHash(GetBytes(newThemeName)).Concat<byte>(colorsHash).Concat<byte>(second).ToArray<byte>());
                }
            }
            return str;
        }

        public static void ClearPaletteThemeCache()
        {
            if (Directory.Exists(Theme.PaletteThemeCacheDirectory))
            {
                Directory.Delete(Theme.PaletteThemeCacheDirectory, true);
            }
        }

        private static bool CompareHash(string cachedAssemblyPath, string newHash)
        {
            string hashFilePath = GetHashFilePath(cachedAssemblyPath);
            if (!File.Exists(hashFilePath))
            {
                return false;
            }
            string b = File.ReadAllText(hashFilePath);
            return string.Equals(newHash, b);
        }

        [SecuritySafeCritical]
        private static MemoryStream GenerateAssembly(ThemePaletteBase palette, Theme baseTheme, string newThemeName, string hash)
        {
            using (MemoryStream stream = new AssemblyPaletteInjection(baseTheme.Assembly).InjectPalette(palette, newThemeName, baseTheme.Name))
            {
                if (Theme.CachePaletteThemes)
                {
                    CacheAssembly(newThemeName, stream, hash);
                    stream.Seek(0L, SeekOrigin.Begin);
                }
                return stream;
            }
        }

        public static Theme GenerateTheme(ThemePaletteBase palette, Theme baseTheme, string themeName = null, string fullThemeName = null, string category = null, Uri smallGlyph = null, Uri largeGlyph = null, Uri svgGlyph = null)
        {
            string newThemeName = !string.IsNullOrEmpty(themeName) ? themeName : GenerateThemeName(baseTheme, palette.Name);
            if (!ValidateThemeName(newThemeName))
            {
                throw new ArgumentException($"The {newThemeName} theme name is not valid. You can only use the [_0-9a-zA-Z] symbols for a theme name.");
            }
            string fullName = !string.IsNullOrEmpty(fullThemeName) ? fullThemeName : palette.GetFullName(baseTheme.FullName);
            string str3 = !string.IsNullOrEmpty(category) ? category : baseTheme.Category;
            Uri smallGlyphUri = (smallGlyph != null) ? smallGlyph : baseTheme.SmallGlyph;
            Uri largeGlyphUri = (largeGlyph != null) ? largeGlyph : baseTheme.LargeGlyph;
            ThemePaletteSvgPaletteProvider.AddSvgPalette(newThemeName, baseTheme.Name, palette);
            return new Theme(newThemeName, fullName, str3, smallGlyphUri, largeGlyphUri, (svgGlyph != null) ? svgGlyph : GetSvgIcon(), GetAssembly(palette, baseTheme, newThemeName), baseTheme, string.IsNullOrEmpty(fullThemeName) ? palette.Name : null);
        }

        public static string GenerateThemeName(Theme baseTheme, string paletteName)
        {
            if (!IsTouchTheme(baseTheme.Name))
            {
                return $"{paletteName}{baseTheme.Name}";
            }
            string str = baseTheme.Name.Substring(0, baseTheme.Name.IndexOf(";"));
            return $"{paletteName}{str}{";touch"}";
        }

        private static Func<MemoryStream> GetAssembly(ThemePaletteBase palette, Theme baseTheme, string newThemeName) => 
            delegate {
                string newHash = "";
                if (Theme.CachePaletteThemes)
                {
                    string cacheAssemblyPath = GetCacheAssemblyPath(newThemeName);
                    newHash = CalcHash(palette, baseTheme, newThemeName);
                    if (File.Exists(cacheAssemblyPath) && CompareHash(cacheAssemblyPath, newHash))
                    {
                        return ReadAssembly(cacheAssemblyPath);
                    }
                }
                return GenerateAssembly(palette, baseTheme, newThemeName, newHash);
            };

        private static byte[] GetBytes(string newThemeName) => 
            Encoding.UTF8.GetBytes(newThemeName);

        internal static string GetCacheAssemblyPath(string themeName) => 
            Path.Combine(Theme.PaletteThemeCacheDirectory, $"{GetThemeFullName(themeName)}.dll");

        private static string GetHashFilePath(string cachedAssemblyPath) => 
            $"{Path.Combine(Path.GetDirectoryName(cachedAssemblyPath), Path.GetFileNameWithoutExtension(cachedAssemblyPath))}.hash";

        private static Uri GetSvgIcon() => 
            Theme.GetPaletteThemeSvgGlyphUri();

        public static string GetThemeFullName(string themeName) => 
            BamlHelper.ThemeFullNamePrefix + themeName + ".v19.2";

        private static bool IsTouchTheme(string themeName) => 
            themeName.EndsWith(";touch", StringComparison.InvariantCultureIgnoreCase);

        private static MemoryStream ReadAssembly(string assembly)
        {
            using (FileStream stream = new FileStream(assembly, FileMode.Open))
            {
                return new MemoryStream(stream.CopyAllBytes());
            }
        }

        private static bool ValidateThemeName(string newThemeName)
        {
            string input = IsTouchTheme(newThemeName) ? newThemeName.Substring(0, newThemeName.IndexOf(";")) : newThemeName;
            return validateThemeNameRegex.IsMatch(input);
        }
    }
}

