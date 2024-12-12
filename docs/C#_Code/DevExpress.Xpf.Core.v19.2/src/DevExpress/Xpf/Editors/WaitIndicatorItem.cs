namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Themes;
    using System;
    using System.Windows;

    public class WaitIndicatorItem : CustomItem
    {
        public static readonly DependencyProperty IsHitTestVisibleProperty = DependencyPropertyManager.Register("IsHitTestVisible", typeof(bool), typeof(WaitIndicatorItem), new PropertyMetadata(true));

        protected override ICustomItem GetCustomItem()
        {
            EditorCustomItem item1 = new EditorCustomItem();
            item1.DisplayValue = string.Empty;
            item1.EditValue = null;
            return item1;
        }

        protected override Style GetItemStyleInternal()
        {
            FrameworkElement ownerEdit = (FrameworkElement) base.OwnerEdit;
            if (ownerEdit == null)
            {
                return null;
            }
            CustomItemThemeKeyExtension resourceKey = new CustomItemThemeKeyExtension();
            resourceKey.ResourceKey = CustomItemThemeKeys.WaitIndicatorItemContainerStyle;
            return (Style) ownerEdit.FindResource(resourceKey);
        }

        public bool IsHitTestVisible
        {
            get => 
                (bool) base.GetValue(IsHitTestVisibleProperty);
            set => 
                base.SetValue(IsHitTestVisibleProperty, value);
        }
    }
}

