namespace DevExpress.Xpf.Office.UI
{
    using DevExpress.Office.Internal;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class FontComboBoxEditSettings : ComboBoxEditSettings, IFontBasedElement
    {
        public static readonly DependencyProperty ItemFontFamilyProperty = DependencyPropertyManager.RegisterAttached("ItemFontFamily", typeof(string), typeof(FontComboBoxEditSettings), new FrameworkPropertyMetadata(new PropertyChangedCallback(FontComboBoxEditSettings.OnItemFontFamilyChanged)));

        static FontComboBoxEditSettings()
        {
            RegisterEditor();
        }

        public FontComboBoxEditSettings()
        {
            FontManager.RegisterFontBasedElement(this);
            base.DisplayMember = "DisplayText";
            base.ValueMember = "EditValue";
            this.Populate();
        }

        public static string GetItemFontFamily(DependencyObject d) => 
            (string) d.GetValue(ItemFontFamilyProperty);

        public void OnFontsChanged()
        {
            this.Populate();
        }

        protected static void OnItemFontFamilyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBlock textBlock = d as TextBlock;
            string newValue = e.NewValue as string;
            if ((textBlock != null) && !string.IsNullOrEmpty(newValue))
            {
                SetTextBlockFontFamilty(textBlock, newValue);
            }
        }

        private void Populate()
        {
            base.Items.Clear();
            List<FontComboBoxEditItem> list = new List<FontComboBoxEditItem>();
            foreach (string str in FontManager.GetLocalizedFontNames())
            {
                FontFamily fontFamily = FontManager.GetFontFamily(str);
                list.Add(new FontComboBoxEditItem(fontFamily.Source, str, fontFamily));
            }
            Comparison<FontComboBoxEditItem> comparison = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Comparison<FontComboBoxEditItem> local1 = <>c.<>9__7_0;
                comparison = <>c.<>9__7_0 = (x, y) => x.DisplayText.CompareTo(y.DisplayText);
            }
            list.Sort(comparison);
            base.Items.AddRange(list.ToArray());
        }

        internal static void RegisterEditor()
        {
            CreateEditorMethod createEditorMethod = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                CreateEditorMethod local1 = <>c.<>9__5_0;
                createEditorMethod = <>c.<>9__5_0 = (CreateEditorMethod) (() => new FontComboBoxEdit());
            }
            EditorSettingsProvider.Default.RegisterUserEditor(typeof(FontComboBoxEdit), typeof(FontComboBoxEditSettings), createEditorMethod, <>c.<>9__5_1 ??= ((CreateEditorSettingsMethod) (() => new FontComboBoxEditSettings())));
        }

        public static void SetItemFontFamily(DependencyObject d, string value)
        {
            d.SetValue(ItemFontFamilyProperty, value);
        }

        public static void SetTextBlockFontFamilty(TextBlock textBlock, string name)
        {
            textBlock.FontFamily = FontManager.GetFontFamily(name);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FontComboBoxEditSettings.<>c <>9 = new FontComboBoxEditSettings.<>c();
            public static CreateEditorMethod <>9__5_0;
            public static CreateEditorSettingsMethod <>9__5_1;
            public static Comparison<FontComboBoxEditItem> <>9__7_0;

            internal int <Populate>b__7_0(FontComboBoxEditItem x, FontComboBoxEditItem y) => 
                x.DisplayText.CompareTo(y.DisplayText);

            internal IBaseEdit <RegisterEditor>b__5_0() => 
                new FontComboBoxEdit();

            internal BaseEditSettings <RegisterEditor>b__5_1() => 
                new FontComboBoxEditSettings();
        }
    }
}

