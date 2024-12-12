namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Editors.Themes;
    using System;
    using System.Windows;

    public sealed class SelectAllItem : CustomItem
    {
        protected override string GetDisplayTextInternal() => 
            EditorLocalizer.GetString(EditorStringId.SelectAll);

        protected override object GetEditValueInternal() => 
            this.SelectAll;

        protected override Style GetItemStyleInternal()
        {
            FrameworkElement ownerEdit = (FrameworkElement) base.OwnerEdit;
            if (ownerEdit == null)
            {
                return null;
            }
            CustomItemThemeKeyExtension resourceKey = new CustomItemThemeKeyExtension();
            resourceKey.ResourceKey = CustomItemThemeKeys.SelectAllItemContainerStyle;
            return (Style) ownerEdit.FindResource(resourceKey);
        }

        protected internal override bool ShouldFilter =>
            true;

        private DevExpress.Xpf.Editors.Popups.SelectionViewModel SelectionViewModel
        {
            get
            {
                ISelectorEdit ownerEdit = base.OwnerEdit;
                return ((ownerEdit != null) ? ((ISelectorEditPropertyProvider) ActualPropertyProvider.GetProperties((DependencyObject) ownerEdit)).SelectionViewModel : null);
            }
        }

        public bool? SelectAll =>
            this.SelectionViewModel.SelectAll;
    }
}

