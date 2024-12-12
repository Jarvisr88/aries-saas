namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class ThemePalette : ThemePaletteBase
    {
        public ThemePalette(string name) : base(name)
        {
            this.Colors = new Dictionary<string, Color>();
        }

        public ThemePalette(string name, ThemePaletteBase basePalette) : this(name)
        {
            this.BasePalette = basePalette;
        }

        public IEnumerable<string> GetColorNames() => 
            this.Colors.Keys.ToList<string>();

        internal override Dictionary<string, Color> GetColors(IAssemblyDefinition themeAssembly, string themeName, string baseThemeName)
        {
            if (this.BasePalette == null)
            {
                return this.Colors;
            }
            Dictionary<string, Color> dictionary = this.BasePalette.GetColors(themeAssembly, themeName, baseThemeName);
            foreach (string str in this.Colors.Keys)
            {
                if (dictionary.ContainsKey(str))
                {
                    dictionary[str] = this.Colors[str];
                    continue;
                }
                dictionary.Add(str, this.Colors[str]);
            }
            return dictionary;
        }

        public virtual void SetColor(string name, Color color)
        {
            if (this.Colors.ContainsKey(name))
            {
                this.Colors[name] = color;
            }
            else
            {
                this.Colors.Add(name, color);
            }
        }

        internal ThemePaletteBase BasePalette { get; private set; }

        public Color? this[string name]
        {
            get
            {
                Color color;
                if (!string.IsNullOrEmpty(name) && this.Colors.TryGetValue(name, out color))
                {
                    return new Color?(color);
                }
                return null;
            }
        }

        protected Dictionary<string, Color> Colors { get; set; }
    }
}

