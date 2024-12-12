namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Security;
    using System.Security.Permissions;

    internal class AssemblyPaletteInjection
    {
        private readonly Assembly _assembly;

        public AssemblyPaletteInjection(Assembly assembly);
        [SecurityCritical]
        protected virtual IAssemblyDefinition GetAssemblyDefinition();
        private byte[] GetNewBamlData(MemoryStream baml);
        private static string GetThemeAssemblyFullName(string name);
        [SecurityCritical]
        public static IEmbeddedResource GetWpfResource(IAssemblyDefinition definition);
        [SecuritySafeCritical, PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        public MemoryStream InjectPalette(ThemePaletteBase palette, string themeName, string baseThemeName);
        [SecurityCritical]
        private void InjectPaletteToBaml(ThemePaletteBase palette, string themeName, string baseThemeName, IAssemblyDefinition definition);
        [SecurityCritical]
        private void SetAssemblyName(IAssemblyDefinition def, string themeName, string baseThemeName);
    }
}

