namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class RadioTokenComboBoxStyleSettings : RadioComboBoxStyleSettings, ITokenStyleSettings
    {
        public static readonly DependencyProperty ShowTokenButtonsProperty;
        public static readonly DependencyProperty TokenBorderTemplateProperty;
        public static readonly DependencyProperty EnableTokenWrappingProperty;
        public static readonly DependencyProperty NewTokenPositionProperty;
        public static readonly DependencyProperty TokenTextTrimmingProperty;
        public static readonly DependencyProperty TokenMaxWidthProperty;
        public static readonly DependencyProperty AllowEditTokensProperty;
        public static readonly DependencyProperty TokenStyleProperty;
        public static readonly DependencyProperty NewTokenTextProperty;

        static RadioTokenComboBoxStyleSettings()
        {
            Type ownerType = typeof(RadioTokenComboBoxStyleSettings);
            EnableTokenWrappingProperty = DependencyProperty.Register("EnableTokenWrapping", typeof(bool?), ownerType);
            TokenBorderTemplateProperty = DependencyProperty.Register("TokenBorderTemplate", typeof(ControlTemplate), ownerType);
            ShowTokenButtonsProperty = DependencyProperty.Register("ShowTokenButtons", typeof(bool?), ownerType, new FrameworkPropertyMetadata(true));
            NewTokenPositionProperty = DependencyProperty.Register("NewTokenPosition", typeof(DevExpress.Xpf.Editors.NewTokenPosition?), ownerType, new FrameworkPropertyMetadata(null));
            TokenTextTrimmingProperty = DependencyProperty.Register("TokenTextTrimming", typeof(TextTrimming?), ownerType, new FrameworkPropertyMetadata(null));
            TokenMaxWidthProperty = DependencyProperty.Register("TokenMaxWidth", typeof(double?), ownerType, new FrameworkPropertyMetadata(null));
            AllowEditTokensProperty = DependencyProperty.Register("AllowEditTokens", typeof(bool?), ownerType, new FrameworkPropertyMetadata(null));
            TokenStyleProperty = DependencyProperty.Register("TokenStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null));
            NewTokenTextProperty = DependencyProperty.Register("NewTokenText", typeof(string), ownerType, new FrameworkPropertyMetadata(null));
        }

        protected internal override bool GetActualAllowDefaultButton(ButtonEdit editor) => 
            !((LookUpEditBasePropertyProvider) editor.PropertyProvider).EnableTokenWrapping;

        public override bool IsTokenStyleSettings() => 
            true;

        public string NewTokenText
        {
            get => 
                (string) base.GetValue(NewTokenTextProperty);
            set => 
                base.SetValue(NewTokenTextProperty, value);
        }

        public Style TokenStyle
        {
            get => 
                (Style) base.GetValue(TokenStyleProperty);
            set => 
                base.SetValue(TokenStyleProperty, value);
        }

        public bool? AllowEditTokens
        {
            get => 
                (bool?) base.GetValue(AllowEditTokensProperty);
            set => 
                base.SetValue(AllowEditTokensProperty, value);
        }

        public double? TokenMaxWidth
        {
            get => 
                (double?) base.GetValue(TokenMaxWidthProperty);
            set => 
                base.SetValue(TokenMaxWidthProperty, value);
        }

        public TextTrimming? TokenTextTrimming
        {
            get => 
                (TextTrimming?) base.GetValue(TokenTextTrimmingProperty);
            set => 
                base.SetValue(TokenTextTrimmingProperty, value);
        }

        public DevExpress.Xpf.Editors.NewTokenPosition? NewTokenPosition
        {
            get => 
                (DevExpress.Xpf.Editors.NewTokenPosition?) base.GetValue(NewTokenPositionProperty);
            set => 
                base.SetValue(NewTokenPositionProperty, value);
        }

        public bool? EnableTokenWrapping
        {
            get => 
                (bool?) base.GetValue(EnableTokenWrappingProperty);
            set => 
                base.SetValue(EnableTokenWrappingProperty, value);
        }

        public ControlTemplate TokenBorderTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(TokenBorderTemplateProperty);
            set => 
                base.SetValue(TokenBorderTemplateProperty, value);
        }

        public bool? ShowTokenButtons
        {
            get => 
                (bool?) base.GetValue(ShowTokenButtonsProperty);
            set => 
                base.SetValue(ShowTokenButtonsProperty, value);
        }
    }
}

