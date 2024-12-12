namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;

    public abstract class BaseComboBoxStyleSettings : BaseLookUpStyleSettings
    {
        public static readonly DependencyProperty ItemContainerStyleProperty;

        static BaseComboBoxStyleSettings()
        {
            Type ownerType = typeof(BaseComboBoxStyleSettings);
            ItemContainerStyleProperty = DependencyProperty.Register("ItemContainerStyle", typeof(Style), ownerType, new PropertyMetadata(null));
        }

        protected BaseComboBoxStyleSettings()
        {
        }

        protected internal override bool GetIncrementalFiltering() => 
            this.IsTokenStyleSettings();

        protected internal override bool ShowCustomItem(LookUpEditBase editor)
        {
            bool? showCustomItems = ((ComboBoxEdit) editor).ShowCustomItems;
            return ((showCustomItems != null) ? showCustomItems.GetValueOrDefault() : this.ShowCustomItemInternal(editor));
        }

        protected virtual bool ShowCustomItemInternal(LookUpEditBase editor) => 
            false;

        public Style ItemContainerStyle
        {
            get => 
                (Style) base.GetValue(ItemContainerStyleProperty);
            set => 
                base.SetValue(ItemContainerStyleProperty, value);
        }
    }
}

