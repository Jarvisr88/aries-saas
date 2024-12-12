namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;

    public class AutoSuggestEditPropertyProvider : PopupBaseEditPropertyProvider
    {
        public static readonly DependencyProperty HasItemTemplateProperty;

        static AutoSuggestEditPropertyProvider()
        {
            Type ownerType = typeof(AutoSuggestEditPropertyProvider);
            HasItemTemplateProperty = DependencyPropertyManager.Register("HasItemTemplate", typeof(bool), ownerType, new PropertyMetadata(false));
        }

        public AutoSuggestEditPropertyProvider(AutoSuggestEdit editor) : base(editor)
        {
        }

        public bool HasItemTemplate
        {
            get => 
                (bool) base.GetValue(HasItemTemplateProperty);
            set => 
                base.SetValue(HasItemTemplateProperty, value);
        }

        private AutoSuggestEditStyleSettings StyleSettings =>
            base.StyleSettings as AutoSuggestEditStyleSettings;

        public bool ClosePopupOnClick =>
            this.GetPopupFooterButtons() == PopupFooterButtons.None;

        public DevExpress.Xpf.Editors.Popups.SelectionEventMode SelectionEventMode =>
            DevExpress.Xpf.Editors.Popups.SelectionEventMode.MouseUp;

        public bool SelectAllOnAcceptPopup =>
            true;
    }
}

