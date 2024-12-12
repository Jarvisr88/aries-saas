namespace DevExpress.Xpf.Bars
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [Browsable(false)]
    public abstract class ThemeSelectorBehavior<T> : Behavior<T> where T: DependencyObject
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ThemesCollectionProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ShowTouchThemesProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty UseSvgGlyphsProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty SelectedThemeProperty;
        private Theme _applicationTheme;
        private readonly ApplicationThemeChangedWeakEventHandler<ThemeSelectorBehavior<T>> onThemeManagerApplicationThemeChanged;
        private bool foreignThemesCollection;

        static ThemeSelectorBehavior();
        public ThemeSelectorBehavior();
        protected virtual void Clear(T item);
        private bool CoerceUseSvgIcons(bool useSvgIcons);
        protected virtual ICollectionView CreateCollectionView();
        protected abstract object CreateStyleKey();
        private IEnumerable<Theme> GetDXThemes();
        private IEnumerable<Theme> GetFilteredThemes();
        private IEnumerable<ThemeViewModel> GetThemes();
        protected abstract void Initialize(T item);
        protected virtual bool IsThemeSelected(string viewModelName, ThemeViewModel viewModel);
        protected virtual void OnApplicationThemeChanged();
        protected override void OnAttached();
        protected override void OnDetaching();
        protected virtual void OnShowTouchThemesChanged();
        protected abstract void OnThemesCollectionChanged(ICollectionView oldValue);
        protected virtual void OnThemesShowModeChanged();
        protected virtual void OnUseSvgIconsChanged();
        private void RaiseThemesCollectionChanged(ICollectionView oldValue);
        private void UpdateThemesCollection();
        private void UpdateThemeViewModels();

        public ICollectionView ThemesCollection { get; set; }

        public ThemeViewModel SelectedTheme { get; set; }

        protected Theme ApplicationTheme { get; set; }

        public bool ShowTouchThemes { get; set; }

        public bool UseSvgGlyphs { get; set; }

        protected override bool AllowAttachInDesignMode { get; }

        public object StyleKey { get; set; }

        protected virtual DependencyProperty StyleProperty { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThemeSelectorBehavior<T>.<>c <>9;
            public static Action<ThemeSelectorBehavior<T>, object, ThemeChangedRoutedEventArgs> <>9__31_0;
            public static Func<Theme, bool> <>9__33_0;
            public static Func<ThemeViewModel, bool> <>9__33_1;
            public static Func<ICollectionView, IEnumerable> <>9__35_0;
            public static Func<IEnumerable, IEnumerable<ThemeViewModel>> <>9__35_1;
            public static Func<ThemeViewModel, bool> <>9__43_0;
            public static Func<bool, bool> <>9__43_1;
            public static Func<Theme, ThemeViewModel> <>9__46_0;
            public static Func<Theme, bool> <>9__48_0;

            static <>c();
            internal void <.cctor>b__30_0(ThemeSelectorBehavior<T> s, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__30_1(ThemeSelectorBehavior<T> s, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__30_2(ThemeSelectorBehavior<T> s);
            internal bool <.cctor>b__30_3(ThemeSelectorBehavior<T> s, bool v);
            internal void <.ctor>b__31_0(ThemeSelectorBehavior<T> selector, object sender, ThemeChangedRoutedEventArgs e);
            internal ThemeViewModel <CreateCollectionView>b__46_0(Theme x);
            internal bool <GetFilteredThemes>b__48_0(Theme x);
            internal IEnumerable <GetThemes>b__35_0(ICollectionView x);
            internal IEnumerable<ThemeViewModel> <GetThemes>b__35_1(IEnumerable x);
            internal bool <RaiseThemesCollectionChanged>b__43_0(ThemeViewModel x);
            internal bool <RaiseThemesCollectionChanged>b__43_1(bool x);
            internal bool <UpdateThemeViewModels>b__33_0(Theme x);
            internal bool <UpdateThemeViewModels>b__33_1(ThemeViewModel x);
        }
    }
}

