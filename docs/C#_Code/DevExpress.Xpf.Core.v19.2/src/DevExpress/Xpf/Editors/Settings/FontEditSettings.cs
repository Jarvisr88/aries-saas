namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Office.Internal;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class FontEditSettings : ComboBoxEditSettings
    {
        public static readonly DependencyProperty AllowConfirmFontUseDialogProperty;

        static FontEditSettings()
        {
            Type ownerType = typeof(FontEditSettings);
            CachedFonts = GetSystemFonts();
            AllowConfirmFontUseDialogProperty = DependencyPropertyManager.Register("AllowConfirmFontUseDialog", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            LookUpEditSettingsBase.AutoCompleteProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(true));
            TextEditSettings.ValidateOnTextInputProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(false));
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            FontEdit edit2 = edit as FontEdit;
            if (edit2 != null)
            {
                edit2.AllowConfirmFontUseDialog = this.AllowConfirmFontUseDialog;
            }
        }

        internal static List<FontFamily> GetSystemFonts()
        {
            Func<string, FontFamily> selector = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<string, FontFamily> local1 = <>c.<>9__6_0;
                selector = <>c.<>9__6_0 = fontName => new FontFamily(fontName);
            }
            List<FontFamily> list = FontManager.GetLocalizedFontNames().Select<string, FontFamily>(selector).Where<FontFamily>(new Func<FontFamily, bool>(FontManager.IsValid)).ToList<FontFamily>();
            list.Sort(new FontFamilyComparer());
            return list;
        }

        internal static List<FontFamily> CachedFonts { get; set; }

        public bool AllowConfirmFontUseDialog
        {
            get => 
                (bool) base.GetValue(AllowConfirmFontUseDialogProperty);
            set => 
                base.SetValue(AllowConfirmFontUseDialogProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FontEditSettings.<>c <>9 = new FontEditSettings.<>c();
            public static Func<string, FontFamily> <>9__6_0;

            internal FontFamily <GetSystemFonts>b__6_0(string fontName) => 
                new FontFamily(fontName);
        }

        private class FontFamilyComparer : IComparer<FontFamily>
        {
            int IComparer<FontFamily>.Compare(FontFamily x, FontFamily y) => 
                string.Compare(x.ToString(), y.ToString(), StringComparison.Ordinal);
        }
    }
}

