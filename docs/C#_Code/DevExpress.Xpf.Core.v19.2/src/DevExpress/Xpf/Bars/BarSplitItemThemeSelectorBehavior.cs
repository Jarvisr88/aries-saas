namespace DevExpress.Xpf.Bars
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    [TargetType(typeof(BarSplitButtonItem))]
    public class BarSplitItemThemeSelectorBehavior : GalleryBarItemThemeSelectorBehavior<BarSplitButtonItem>
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ShowSelectedThemeGlyphProperty;

        static BarSplitItemThemeSelectorBehavior();
        protected override void Clear(BarSplitButtonItem item);
        protected override object CreateGalleryStyleKey();
        protected override object CreateStyleKey();
        protected override void Initialize(BarSplitButtonItem item);
        protected override void OnAttached();

        public bool ShowSelectedThemeGlyph { get; set; }
    }
}

