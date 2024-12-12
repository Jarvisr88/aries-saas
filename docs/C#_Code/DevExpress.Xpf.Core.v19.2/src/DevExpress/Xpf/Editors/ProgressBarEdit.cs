namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Automation;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Markup;
    using System.Windows.Media;

    [LicenseProvider(typeof(DX_WPFEditors_LicenseProvider)), DXToolboxBrowsable(DXToolboxItemKind.Free), ContentProperty("Content")]
    public class ProgressBarEdit : RangeBaseEdit, IProgressBarExportSettings, ITextExportSettings, IExportSettings
    {
        public static readonly DependencyProperty IsIndeterminateProperty;
        private static readonly DependencyPropertyKey IsIndeterminatePropertyKey;
        public static readonly DependencyProperty IsPercentProperty;
        public static readonly DependencyProperty AdditionalForegroundProperty;
        public static readonly DependencyProperty ContentProperty;
        public static readonly DependencyProperty ContentDisplayModeProperty;
        public static readonly DependencyProperty ContentTemplateProperty;

        static ProgressBarEdit()
        {
            Type ownerType = typeof(ProgressBarEdit);
            IsIndeterminatePropertyKey = DependencyPropertyManager.RegisterReadOnly("IsIndeterminate", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            IsIndeterminateProperty = IsIndeterminatePropertyKey.DependencyProperty;
            IsPercentProperty = DependencyPropertyManager.Register("IsPercent", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(ProgressBarEdit.IsPercentPropertyChanged)));
            AdditionalForegroundProperty = DependencyPropertyManager.Register("AdditionalForeground", typeof(SolidColorBrush), ownerType, new FrameworkPropertyMetadata(null));
            ContentProperty = DependencyPropertyManager.Register("Content", typeof(object), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));
            ContentDisplayModeProperty = DependencyPropertyManager.Register("ContentDisplayMode", typeof(DevExpress.Xpf.Editors.ContentDisplayMode), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.ContentDisplayMode.None, FrameworkPropertyMetadataOptions.None, (d, e) => ((ProgressBarEdit) d).ContentDisplayModeChanged((DevExpress.Xpf.Editors.ContentDisplayMode) e.NewValue)));
            ContentTemplateProperty = DependencyPropertyManager.Register("ContentTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));
        }

        public ProgressBarEdit()
        {
            Type type = typeof(ProgressBarEdit);
            this.SetDefaultStyleKey(type);
        }

        protected virtual void ContentDisplayModeChanged(DevExpress.Xpf.Editors.ContentDisplayMode value)
        {
            this.EditStrategy.ContentDisplayModeChanged(value);
        }

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new ProgressBarEditPropertyProvider(this);

        protected override EditStrategyBase CreateEditStrategy() => 
            new ProgressBarEditStrategy(this);

        protected internal override BaseEditStyleSettings CreateStyleSettings() => 
            new ProgressBarStyleSettings();

        protected virtual void IsPercentChanged(bool value)
        {
            this.EditStrategy.IsPercentChanged(value);
        }

        private static void IsPercentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ProgressBarEdit) d).IsPercentChanged((bool) e.NewValue);
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new ProgressBarEditAutomationPeer(this);

        public SolidColorBrush AdditionalForeground
        {
            get => 
                (SolidColorBrush) base.GetValue(AdditionalForegroundProperty);
            set => 
                base.SetValue(AdditionalForegroundProperty, value);
        }

        public object Content
        {
            get => 
                base.GetValue(ContentProperty);
            set => 
                base.SetValue(ContentProperty, value);
        }

        public DevExpress.Xpf.Editors.ContentDisplayMode ContentDisplayMode
        {
            get => 
                (DevExpress.Xpf.Editors.ContentDisplayMode) base.GetValue(ContentDisplayModeProperty);
            set => 
                base.SetValue(ContentDisplayModeProperty, value);
        }

        public DataTemplate ContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContentTemplateProperty);
            set => 
                base.SetValue(ContentTemplateProperty, value);
        }

        [Description("Gets whether the ProgressBarEdit represents a marquee progress bar.")]
        public bool IsIndeterminate
        {
            get => 
                (bool) base.GetValue(IsIndeterminateProperty);
            internal set => 
                base.SetValue(IsIndeterminatePropertyKey, value);
        }

        [Description("Gets or sets whether the value is displayed as a percentage. This is a dependency property.")]
        public bool IsPercent
        {
            get => 
                (bool) base.GetValue(IsPercentProperty);
            set => 
                base.SetValue(IsPercentProperty, value);
        }

        [Category("Behavior"), Browsable(true)]
        public BaseEditStyleSettings StyleSettings
        {
            get => 
                base.StyleSettings;
            set => 
                base.StyleSettings = value;
        }

        protected ProgressBarEditStrategy EditStrategy =>
            (ProgressBarEditStrategy) base.EditStrategy;

        protected internal override Type StyleSettingsType =>
            typeof(BaseProgressBarStyleSettings);

        int IProgressBarExportSettings.Position =>
            base.ToInt(base.Value);

        Color IExportSettings.Background =>
            Colors.White;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ProgressBarEdit.<>c <>9 = new ProgressBarEdit.<>c();

            internal void <.cctor>b__7_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ProgressBarEdit) d).ContentDisplayModeChanged((ContentDisplayMode) e.NewValue);
            }
        }
    }
}

