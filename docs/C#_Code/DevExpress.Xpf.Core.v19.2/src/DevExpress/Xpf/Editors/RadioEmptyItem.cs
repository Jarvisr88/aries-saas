namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Themes;
    using System.Windows;

    internal class RadioEmptyItem : EmptyItem
    {
        protected override Style GetItemStyleInternal()
        {
            FrameworkElement ownerEdit = (FrameworkElement) base.OwnerEdit;
            if (ownerEdit == null)
            {
                return null;
            }
            CustomItemThemeKeyExtension resourceKey = new CustomItemThemeKeyExtension();
            resourceKey.ResourceKey = CustomItemThemeKeys.RadioEmptyItemContainerStyle;
            return (Style) ownerEdit.FindResource(resourceKey);
        }
    }
}

