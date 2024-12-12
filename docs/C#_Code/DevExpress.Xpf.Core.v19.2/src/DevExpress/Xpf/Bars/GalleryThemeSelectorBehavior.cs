namespace DevExpress.Xpf.Bars
{
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.ComponentModel;

    [TargetType(typeof(Gallery))]
    public class GalleryThemeSelectorBehavior : ThemeSelectorBehavior<Gallery>
    {
        protected override void Clear(Gallery item);
        protected override object CreateStyleKey();
        protected override void Initialize(Gallery item);
        protected override void OnThemesCollectionChanged(ICollectionView oldValue);
    }
}

