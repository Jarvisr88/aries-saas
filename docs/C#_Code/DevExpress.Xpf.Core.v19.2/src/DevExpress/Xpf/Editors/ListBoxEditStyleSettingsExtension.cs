namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Popups;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class ListBoxEditStyleSettingsExtension : MarkupExtension
    {
        public ListBoxEditStyleSettingsExtension()
        {
            this.SelectionEventMode = (DevExpress.Xpf.Editors.Popups.SelectionEventMode) BaseListBoxEditStyleSettings.SelectionEventModeProperty.DefaultMetadata.DefaultValue;
        }

        public sealed override object ProvideValue(IServiceProvider serviceProvider) => 
            this.ProvideValueCore(serviceProvider);

        protected virtual BaseListBoxEditStyleSettings ProvideValueCore(IServiceProvider serviceProvider)
        {
            ListBoxEditStyleSettings settings1 = new ListBoxEditStyleSettings();
            settings1.SelectionEventMode = this.SelectionEventMode;
            return settings1;
        }

        public DevExpress.Xpf.Editors.Popups.SelectionEventMode SelectionEventMode { get; set; }
    }
}

