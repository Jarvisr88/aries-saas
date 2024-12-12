namespace DevExpress.Xpf.Bars
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;

    public class ThemeViewModel : BindableBase
    {
        private bool init;
        private readonly Locker isSelectedUpdateLocker;

        public ThemeViewModel(DevExpress.Xpf.Core.Theme theme);
        internal bool Init();
        private void OnIsSelectedChanged();
        protected virtual void SetApplicationThemeName();
        internal void SetIsSelected(bool isSelected);

        public DevExpress.Xpf.Core.Theme Theme { get; private set; }

        public bool IsSelected { get; set; }

        public object Owner { get; set; }

        public bool UseSvgGlyphs { get; set; }

        public string DisplayName { get; set; }
    }
}

