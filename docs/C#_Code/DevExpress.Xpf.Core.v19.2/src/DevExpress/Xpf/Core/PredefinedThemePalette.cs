namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Resources;
    using System.Security;

    public sealed class PredefinedThemePalette : ThemePaletteBase
    {
        internal PredefinedThemePalette(string name) : base(name)
        {
            PredefinedThemePalettes.RegisterPredefinedThemePalette(this);
        }

        internal override Dictionary<string, Color> GetColors(IAssemblyDefinition themeAssembly, string themeName, string baseThemeName) => 
            (themeAssembly != null) ? this.GetColorsFromAssembly(baseThemeName, themeAssembly) : new Dictionary<string, Color>();

        [SecuritySafeCritical]
        private Dictionary<string, Color> GetColorsFromAssembly(string baseThemeName, IAssemblyDefinition themeAssembly)
        {
            Dictionary<string, Color> dXPaletteColors;
            IEmbeddedResource wpfResource = AssemblyPaletteInjection.GetWpfResource(themeAssembly);
            if (wpfResource == null)
            {
                return new Dictionary<string, Color>();
            }
            using (ResourceReader reader = new ResourceReader(wpfResource.GetResourceStream()))
            {
                using (IDictionaryEnumerator enumerator = reader.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            string str2;
                            byte[] buffer;
                            DictionaryEntry current = (DictionaryEntry) enumerator.Current;
                            string key = (string) current.Key;
                            if (!this.IsPaletteResource(key, baseThemeName))
                            {
                                continue;
                            }
                            reader.GetResourceData(key, out str2, out buffer);
                            using (MemoryStream stream = new MemoryStream(buffer))
                            {
                                stream.Position = 4L;
                                dXPaletteColors = new BamlPaletteInjection(stream).GetDXPaletteColors(base.Name);
                            }
                        }
                        else
                        {
                            return new Dictionary<string, Color>();
                        }
                        break;
                    }
                }
            }
            return dXPaletteColors;
        }

        private bool IsPaletteResource(string resourceName, string themeName) => 
            resourceName.EndsWith($"core/core/themes/{themeName.ToLowerInvariant()}/palettes.baml");
    }
}

