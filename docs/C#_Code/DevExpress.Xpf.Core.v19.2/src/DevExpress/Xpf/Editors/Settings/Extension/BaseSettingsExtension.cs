namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;

    public abstract class BaseSettingsExtension : MarkupExtension
    {
        protected BaseSettingsExtension()
        {
            this.VerticalContentAlignment = (VerticalAlignment) BaseEditSettings.VerticalContentAlignmentProperty.DefaultMetadata.DefaultValue;
            this.HorizontalContentAlignment = (EditSettingsHorizontalAlignment) BaseEditSettings.HorizontalContentAlignmentProperty.DefaultMetadata.DefaultValue;
            this.MaxWidth = (double) BaseEditSettings.MaxWidthProperty.DefaultMetadata.DefaultValue;
            this.AllowNullInput = (bool) BaseEditSettings.AllowNullInputProperty.DefaultMetadata.DefaultValue;
        }

        protected abstract BaseEditSettings CreateEditSettings();
        public sealed override object ProvideValue(IServiceProvider serviceProvider)
        {
            BaseEditSettings o = this.CreateEditSettings();
            o.HorizontalContentAlignment = this.HorizontalContentAlignment;
            o.VerticalContentAlignment = this.VerticalContentAlignment;
            o.MaxWidth = this.MaxWidth;
            if (!o.IsPropertyAssigned(BaseEditSettings.AllowNullInputProperty))
            {
                o.AllowNullInput = this.AllowNullInput;
            }
            return o;
        }

        public EditSettingsHorizontalAlignment HorizontalContentAlignment { get; set; }

        public VerticalAlignment VerticalContentAlignment { get; set; }

        public bool AllowNullInput { get; set; }

        public double MaxWidth { get; set; }
    }
}

