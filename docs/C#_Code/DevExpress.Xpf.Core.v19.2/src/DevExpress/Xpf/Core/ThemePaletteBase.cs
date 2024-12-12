namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class ThemePaletteBase
    {
        protected ThemePaletteBase(string name)
        {
            this.Name = name;
        }

        internal abstract Dictionary<string, Color> GetColors(IAssemblyDefinition themeAssembly, string themeName, string baseThemeName);
        internal virtual string GetFullName(string baseFullName) => 
            $"{this.Name} {baseFullName}";

        public string Name { get; private set; }
    }
}

