namespace DevExpress.Xpf.Office.UI
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class FontSizeComboBoxEditSettings : ComboBoxEditSettings
    {
        public static readonly DependencyProperty OfficeFontSizeProviderProperty = DependencyPropertyManager.Register("OfficeFontSizeProvider", typeof(IOfficeFontSizeProvider), typeof(FontSizeComboBoxEditSettings), new FrameworkPropertyMetadata(new PropertyChangedCallback(FontSizeComboBoxEditSettings.OnOfficeFontSizeProviderChanged)));

        static FontSizeComboBoxEditSettings()
        {
            RegisterEditor();
        }

        protected internal virtual void OnOfficeFontSizeProviderChanged()
        {
            this.Populate();
        }

        protected static void OnOfficeFontSizeProviderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FontSizeComboBoxEditSettings settings = d as FontSizeComboBoxEditSettings;
            if (settings != null)
            {
                settings.OnOfficeFontSizeProviderChanged();
            }
        }

        private void Populate()
        {
            base.Items.Clear();
            if (this.OfficeFontSizeProvider != null)
            {
                foreach (int num in this.OfficeFontSizeProvider.GetPredefinedFontSizes())
                {
                    base.Items.Add(num);
                }
            }
        }

        internal static void RegisterEditor()
        {
            CreateEditorMethod createEditorMethod = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                CreateEditorMethod local1 = <>c.<>9__7_0;
                createEditorMethod = <>c.<>9__7_0 = (CreateEditorMethod) (() => new FontSizeComboBoxEdit());
            }
            EditorSettingsProvider.Default.RegisterUserEditor(typeof(FontSizeComboBoxEdit), typeof(FontSizeComboBoxEditSettings), createEditorMethod, <>c.<>9__7_1 ??= ((CreateEditorSettingsMethod) (() => new FontSizeComboBoxEditSettings())));
        }

        public IOfficeFontSizeProvider OfficeFontSizeProvider
        {
            get => 
                (IOfficeFontSizeProvider) base.GetValue(OfficeFontSizeProviderProperty);
            set => 
                base.SetValue(OfficeFontSizeProviderProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FontSizeComboBoxEditSettings.<>c <>9 = new FontSizeComboBoxEditSettings.<>c();
            public static CreateEditorMethod <>9__7_0;
            public static CreateEditorSettingsMethod <>9__7_1;

            internal IBaseEdit <RegisterEditor>b__7_0() => 
                new FontSizeComboBoxEdit();

            internal BaseEditSettings <RegisterEditor>b__7_1() => 
                new FontSizeComboBoxEditSettings();
        }
    }
}

