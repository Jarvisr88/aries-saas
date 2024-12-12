namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Themes;
    using System;
    using System.ComponentModel;
    using System.Windows;

    public abstract class BaseProgressBarStyleSettings : BaseEditStyleSettings
    {
        protected ProgressBarEdit editor;
        public static readonly DependencyProperty AccelerateRatioProperty = DependencyProperty.Register("AccelerateRatio", typeof(double), typeof(BaseProgressBarStyleSettings), new PropertyMetadata(1.0, null));

        protected BaseProgressBarStyleSettings()
        {
        }

        private void ApplyPanelStyle(ProgressBarEdit editor)
        {
            this.editor = editor;
            editor.Panel.Style = editor.TryFindResource(this.StyleThemeKey) as Style;
        }

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            ProgressBarEdit edit = editor as ProgressBarEdit;
            if ((edit != null) && (edit.Panel != null))
            {
                this.ApplyPanelStyle(edit);
                edit.IsIndeterminate = this.IsIndeterminate;
            }
        }

        protected abstract ProgressBarEditStyleThemeKeyExtension StyleThemeKey { get; }

        [Description("Gets or sets the acceleration ratio of the progress animation. This is a dependency property.")]
        public double AccelerateRatio
        {
            get => 
                (double) base.GetValue(AccelerateRatioProperty);
            set => 
                base.SetValue(AccelerateRatioProperty, value);
        }

        protected abstract bool IsIndeterminate { get; }
    }
}

