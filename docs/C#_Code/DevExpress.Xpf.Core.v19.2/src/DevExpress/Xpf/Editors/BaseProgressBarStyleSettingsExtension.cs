namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public abstract class BaseProgressBarStyleSettingsExtension : MarkupExtension
    {
        protected BaseProgressBarStyleSettingsExtension()
        {
        }

        protected abstract BaseProgressBarStyleSettings CreateStyleSettings();
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            BaseProgressBarStyleSettings settings = this.CreateStyleSettings();
            settings.AccelerateRatio = this.AccelerateRatio;
            return settings;
        }

        public double AccelerateRatio { get; set; }
    }
}

