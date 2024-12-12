namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ThemeTreeWalker
    {
        private readonly WeakReference wRef;
        private readonly DevExpress.Xpf.Editors.Internal.InplaceResourceProvider inplaceResourceProvider;

        protected internal ThemeTreeWalker(string themeName, bool isTouch, DependencyObject owner)
        {
            this.wRef = new WeakReference(owner);
            this.ThemeName = themeName;
            this.IsTouch = isTouch;
            this.inplaceResourceProvider = new DevExpress.Xpf.Editors.Internal.InplaceResourceProvider(ThemeHelper.GetTreewalkerThemeName(this, false));
        }

        public override string ToString() => 
            this.ThemeName;

        public DependencyObject Owner =>
            this.wRef.Target as DependencyObject;

        public string ThemeName { get; protected set; }

        public DevExpress.Xpf.Editors.Internal.InplaceResourceProvider InplaceResourceProvider =>
            this.inplaceResourceProvider;

        public bool IsTouch { get; private set; }

        public bool IsDefault =>
            Theme.IsDefaultTheme(this.ThemeName);

        public bool IsRegistered =>
            Theme.FindTheme(this.ThemeName) != null;
    }
}

