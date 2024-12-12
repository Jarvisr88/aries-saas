namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Windows;

    [Browsable(false)]
    public abstract class GalleryBarItemThemeSelectorBehavior<T> : BarItemThemeSelectorBehavior<T> where T: BarItem
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty GalleryProperty;
        private GalleryThemeSelectorBehavior galleryBehavior;
        private object galleryStyleKey;

        static GalleryBarItemThemeSelectorBehavior();
        public GalleryBarItemThemeSelectorBehavior();
        protected override void Clear(T item);
        protected virtual object CreateGalleryStyleKey();
        protected virtual void InitializeGallery(DevExpress.Xpf.Bars.Gallery gallery, GalleryThemeSelectorBehavior behavior);
        protected virtual void OnGalleryChanged(DevExpress.Xpf.Bars.Gallery oldValue);
        private static void OnGalleryPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void OnGalleryStyleKeyChanged();
        protected override void OnThemesCollectionChanged(ICollectionView oldValue);
        protected virtual void UninitializeGallery(DevExpress.Xpf.Bars.Gallery gallery, GalleryThemeSelectorBehavior behavior);

        public DevExpress.Xpf.Bars.Gallery Gallery { get; set; }

        protected GalleryThemeSelectorBehavior GalleryBehavior { get; }

        public object GalleryStyleKey { get; set; }
    }
}

