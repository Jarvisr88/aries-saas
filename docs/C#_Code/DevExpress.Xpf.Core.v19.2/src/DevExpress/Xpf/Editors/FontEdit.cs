namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    [LicenseProvider(typeof(DX_WPFEditors_LicenseProvider)), DXToolboxBrowsable(DXToolboxItemKind.Free)]
    public class FontEdit : ComboBoxEdit
    {
        public static readonly DependencyProperty FontProperty;
        public static readonly DependencyProperty AllowConfirmFontUseDialogProperty;

        static FontEdit()
        {
            Type ownerType = typeof(FontEdit);
            FontProperty = DependencyPropertyManager.Register("Font", typeof(FontFamily), ownerType, new FrameworkPropertyMetadata(null, (obj, e) => ((FontEdit) obj).OnFontChanged((FontFamily) e.OldValue, (FontFamily) e.NewValue), (obj, baseValue) => ((FontEdit) obj).CoerceFont((FontFamily) baseValue)));
            AllowConfirmFontUseDialogProperty = DependencyPropertyManager.Register("AllowConfirmFontUseDialog", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            LookUpEditBase.AutoCompleteProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(true));
            BaseEdit.ValidateOnTextInputProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(false));
            ComboBoxEdit.ScrollUnitProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(ScrollUnit.Item));
        }

        public FontEdit()
        {
            this.SetDefaultStyleKey(typeof(FontEdit));
        }

        private object CoerceFont(FontFamily baseValue) => 
            this.EditStrategy.CoerceFont(baseValue);

        protected override EditStrategyBase CreateEditStrategy() => 
            new FontEditStrategy(this);

        protected virtual void OnFontChanged(FontFamily oldFont, FontFamily newFont)
        {
            this.EditStrategy.FontChanged(oldFont, newFont);
        }

        protected override void OnLoadedInternal()
        {
            base.OnLoadedInternal();
            this.SetItemsSource();
        }

        private void SetItemsSource()
        {
            if ((base.ItemsSource == null) && (base.GetBindingExpression(LookUpEditBase.ItemsSourceProperty) == null))
            {
                base.ItemsSource = FontEditSettings.CachedFonts;
            }
        }

        public FontFamily Font
        {
            get => 
                (FontFamily) base.GetValue(FontProperty);
            set => 
                base.SetValue(FontProperty, value);
        }

        public bool AllowConfirmFontUseDialog
        {
            get => 
                (bool) base.GetValue(AllowConfirmFontUseDialogProperty);
            set => 
                base.SetValue(AllowConfirmFontUseDialogProperty, value);
        }

        protected FontEditStrategy EditStrategy
        {
            get => 
                base.EditStrategy as FontEditStrategy;
            set => 
                base.EditStrategy = value;
        }

        protected internal FontEditSettings Settings =>
            base.Settings as FontEditSettings;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FontEdit.<>c <>9 = new FontEdit.<>c();

            internal void <.cctor>b__2_0(DependencyObject obj, DependencyPropertyChangedEventArgs e)
            {
                ((FontEdit) obj).OnFontChanged((FontFamily) e.OldValue, (FontFamily) e.NewValue);
            }

            internal object <.cctor>b__2_1(DependencyObject obj, object baseValue) => 
                ((FontEdit) obj).CoerceFont((FontFamily) baseValue);
        }
    }
}

