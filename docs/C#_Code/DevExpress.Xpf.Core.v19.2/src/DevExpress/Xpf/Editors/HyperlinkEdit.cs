namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Automation;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("EditValue"), DXToolboxBrowsable(DXToolboxItemKind.Free)]
    public class HyperlinkEdit : BaseEdit, ICommandSource, ITextExportSettings, IExportSettings, ISupportTextHighlighting
    {
        public static readonly DependencyProperty NavigationUrlMemberProperty;
        public static readonly DependencyProperty NavigationUrlFormatProperty;
        public static readonly DependencyProperty NavigationUrlProperty;
        public static readonly DependencyProperty ActualNavigationUrlProperty;
        private static readonly DependencyPropertyKey ActualNavigationUrlPropertyKey;
        public static readonly DependencyProperty TextProperty;
        public static readonly DependencyProperty DisplayMemberProperty;
        private static readonly DependencyPropertyKey HighlightedTextPropertyKey;
        public static readonly DependencyProperty HighlightedTextProperty;
        private static readonly DependencyPropertyKey HighlightedTextCriteriaPropertyKey;
        public static readonly DependencyProperty HighlightedTextCriteriaProperty;
        public static readonly DependencyProperty AllowAutoNavigateProperty;
        public static readonly DependencyProperty CommandProperty;
        public static readonly DependencyProperty CommandParameterProperty;
        public static readonly DependencyProperty CommandTargetProperty;
        public static readonly DependencyProperty IsHyperlinkPressedProperty;
        private static readonly DependencyPropertyKey IsHyperlinkPressedPropertyKey;
        public static readonly DependencyProperty ShowNavigationUrlToolTipProperty;
        public static readonly RoutedEvent RequestNavigationEvent;

        public event HyperlinkEditRequestNavigationEventHandler RequestNavigation
        {
            add
            {
                base.AddHandler(RequestNavigationEvent, value);
            }
            remove
            {
                base.RemoveHandler(RequestNavigationEvent, value);
            }
        }

        static HyperlinkEdit()
        {
            Type ownerType = typeof(HyperlinkEdit);
            RequestNavigationEvent = EventManager.RegisterRoutedEvent("RequestNavigation", RoutingStrategy.Direct, typeof(HyperlinkEditRequestNavigationEventHandler), ownerType);
            NavigationUrlMemberProperty = DependencyPropertyManager.Register("NavigationUrlMember", typeof(string), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (o, args) => ((HyperlinkEdit) o).NavigationUrlMemberChanged((string) args.OldValue, (string) args.NewValue)));
            NavigationUrlProperty = DependencyPropertyManager.Register("NavigationUrl", typeof(string), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (o, args) => ((HyperlinkEdit) o).NavigateUrlChanged((string) args.OldValue, (string) args.NewValue)));
            NavigationUrlFormatProperty = DependencyPropertyManager.Register("NavigationUrlFormat", typeof(string), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (o, args) => ((HyperlinkEdit) o).NavigateUrlFormatChanged((string) args.OldValue, (string) args.NewValue)));
            TextProperty = DependencyPropertyManager.Register("Text", typeof(string), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (o, args) => ((HyperlinkEdit) o).TextChanged((string) args.OldValue, (string) args.NewValue)));
            ActualNavigationUrlPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualNavigationUrl", typeof(string), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));
            ActualNavigationUrlProperty = ActualNavigationUrlPropertyKey.DependencyProperty;
            DisplayMemberProperty = DependencyProperty.Register("DisplayMember", typeof(string), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (o, args) => ((HyperlinkEdit) o).DisplayMemberChanged((string) args.OldValue, (string) args.NewValue)));
            HighlightedTextCriteriaPropertyKey = DependencyPropertyManager.RegisterReadOnly("HighlightedTextCriteria", typeof(DevExpress.Xpf.Editors.HighlightedTextCriteria), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.HighlightedTextCriteria.StartsWith, (d, e) => ((HyperlinkEdit) d).HighlightedTextCriteriaChanged((DevExpress.Xpf.Editors.HighlightedTextCriteria) e.NewValue)));
            HighlightedTextCriteriaProperty = HighlightedTextCriteriaPropertyKey.DependencyProperty;
            HighlightedTextPropertyKey = DependencyPropertyManager.RegisterReadOnly("HighlightedText", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, (d, e) => ((HyperlinkEdit) d).HighlightedTextChanged((string) e.NewValue)));
            HighlightedTextProperty = HighlightedTextPropertyKey.DependencyProperty;
            AllowAutoNavigateProperty = DependencyProperty.Register("AllowAutoNavigate", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            CommandProperty = DependencyPropertyManager.Register("Command", typeof(ICommand), ownerType, new PropertyMetadata(null, (o, args) => ((HyperlinkEdit) o).CommandChanged((ICommand) args.NewValue)));
            CommandParameterProperty = DependencyPropertyManager.Register("CommandParameter", typeof(object), ownerType);
            CommandTargetProperty = DependencyPropertyManager.Register("CommandTarget", typeof(IInputElement), ownerType, new FrameworkPropertyMetadata(null));
            IsHyperlinkPressedPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsHyperlinkPressed", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            IsHyperlinkPressedProperty = IsHyperlinkPressedPropertyKey.DependencyProperty;
            ShowNavigationUrlToolTipProperty = DependencyProperty.Register("ShowNavigationUrlToolTip", typeof(bool), ownerType, new PropertyMetadata(true));
            Control.VerticalContentAlignmentProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(VerticalAlignment.Stretch, (d, e) => ((HyperlinkEdit) d).VerticalContentAlignmentChanged((VerticalAlignment) e.OldValue, (VerticalAlignment) e.NewValue)));
            Control.PaddingProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata((d, e) => ((HyperlinkEdit) d).PaddingChanged((Thickness) e.OldValue, (Thickness) e.NewValue)));
        }

        public HyperlinkEdit()
        {
            this.SetDefaultStyleKey(typeof(HyperlinkEdit));
            this.<EditBox>k__BackingField = this.CreateEditBoxWrapper();
            this.<CommandSourceHelper>k__BackingField = this.CreateCommandSourceHelper();
            this.CommandSourceHelper.CanExecuteChanged += (sender, args) => base.CoerceValue(UIElement.IsEnabledProperty);
            this.CommandSourceHelper.Attach();
        }

        protected virtual void CommandChanged(ICommand newValue)
        {
            this.CommandSourceHelper.Update(newValue, false);
        }

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new HyperlinkEditPropertyProvider(this);

        protected virtual DevExpress.Xpf.Bars.Native.CommandSourceHelper CreateCommandSourceHelper() => 
            new DevExpress.Xpf.Bars.Native.CommandSourceHelper(this);

        protected virtual EditBoxWrapper CreateEditBoxWrapper() => 
            new HyperlinkEditBoxWrapper(this);

        protected override EditStrategyBase CreateEditStrategy() => 
            new HyperlinkEditStrategy(this);

        void ISupportTextHighlighting.UpdateHighlightedText(string highlightedText, DevExpress.Xpf.Editors.HighlightedTextCriteria criteria)
        {
            this.HighlightedText = highlightedText;
            this.HighlightedTextCriteria = criteria;
        }

        protected virtual void DisplayMemberChanged(string oldValue, string newValue)
        {
            this.EditStrategy.DisplayMemberChanged(oldValue, newValue);
        }

        public virtual void DoNavigate()
        {
            this.EditStrategy.PerformNavigate();
        }

        private void EditCoreOnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((base.EditCore != null) && (e.ButtonState == MouseButtonState.Pressed))
            {
                Mouse.Capture(base.EditCore);
                if (base.EditCore.IsMouseCaptured)
                {
                    if ((e.ButtonState == MouseButtonState.Pressed) && (e.ChangedButton == MouseButton.Left))
                    {
                        e.Handled = true;
                        this.IsHyperlinkPressed = true;
                    }
                    else
                    {
                        base.ReleaseMouseCapture();
                    }
                }
            }
        }

        private void EditCoreOnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (base.EditCore != null)
            {
                if (base.EditCore.IsMouseCaptured)
                {
                    base.EditCore.ReleaseMouseCapture();
                }
                if (this.IsHyperlinkPressed && (base.EditCore.IsMouseOver && (e.ChangedButton == MouseButton.Left)))
                {
                    this.DoNavigate();
                    e.Handled = true;
                }
                this.IsHyperlinkPressed = false;
            }
        }

        protected virtual string GetExportText() => 
            base.PropertyProvider.DisplayText;

        protected virtual TextDecorationCollection GetPrintTextDecorations() => 
            ExportSettingDefaultValue.TextDecorations;

        protected virtual object GetTextValueInternal() => 
            this.EditStrategy.IsNullValue(base.EditValue) ? null : base.EditValue;

        protected virtual bool? GetXlsExportNativeFormatInternal() => 
            ExportSettingDefaultValue.XlsExportNativeFormat;

        protected virtual void HighlightedTextChanged(string newValue)
        {
            this.EditStrategy.HighlightedTextChanged(newValue);
        }

        protected virtual void HighlightedTextCriteriaChanged(DevExpress.Xpf.Editors.HighlightedTextCriteria newValue)
        {
            this.EditStrategy.HighlightedTextCriteriaChanged(newValue);
        }

        internal bool IsInactiveModeWithTextBlock() => 
            this.IsInplaceMode && (base.EditCore is TextBlock);

        protected virtual void NavigateUrlChanged(string oldValue, string newValue)
        {
            this.EditStrategy.NavigateUrlChanged(oldValue, newValue);
        }

        protected virtual void NavigateUrlFormatChanged(string oldValue, string newValue)
        {
            this.EditStrategy.NavigateUrlFormatChanged(oldValue, newValue);
        }

        protected virtual void NavigationUrlMemberChanged(string oldValue, string newValue)
        {
            this.EditStrategy.NavigateUrlMemberChanged(oldValue, newValue);
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new HyperlinkEditAutomationPeer(this);

        protected override void OnEditCoreAssigned()
        {
            base.OnEditCoreAssigned();
            this.SyncTextBlockPadding();
            this.SyncTextBlockProperty(Control.VerticalContentAlignmentProperty, FrameworkElement.VerticalAlignmentProperty);
        }

        protected virtual void PaddingChanged(Thickness oldValue, Thickness newValue)
        {
            this.SyncTextBlockPadding();
        }

        protected override void SubscribeEditEventsCore()
        {
            base.SubscribeEditEventsCore();
            if (base.EditCore != null)
            {
                base.EditCore.PreviewMouseDown += new MouseButtonEventHandler(this.EditCoreOnPreviewMouseDown);
                base.EditCore.PreviewMouseUp += new MouseButtonEventHandler(this.EditCoreOnPreviewMouseUp);
            }
        }

        private void SyncTextBlockPadding()
        {
            this.SyncTextBlockProperty(Control.PaddingProperty, TextBlock.PaddingProperty);
        }

        protected void SyncTextBlockProperty(object newValue, DependencyProperty textBlockProperty)
        {
            if (this.IsInactiveModeWithTextBlock())
            {
                ValueSource valueSource = DependencyPropertyHelper.GetValueSource(base.EditCore, textBlockProperty);
                if ((valueSource.BaseValueSource <= BaseValueSource.DefaultStyleTrigger) || (valueSource.BaseValueSource == BaseValueSource.Local))
                {
                    base.EditCore.SetValue(textBlockProperty, newValue);
                }
            }
        }

        private void SyncTextBlockProperty(DependencyProperty editorProperty, DependencyProperty textBlockProperty)
        {
            this.SyncTextBlockProperty(base.GetValue(editorProperty), textBlockProperty);
        }

        protected virtual void TextChanged(string oldValue, string newValue)
        {
            this.EditStrategy.TextChanged(oldValue, newValue);
        }

        protected override void UnsubscribeEditEventsCore()
        {
            base.UnsubscribeEditEventsCore();
            if (base.EditCore != null)
            {
                base.EditCore.PreviewMouseDown -= new MouseButtonEventHandler(this.EditCoreOnPreviewMouseDown);
                base.EditCore.PreviewMouseUp -= new MouseButtonEventHandler(this.EditCoreOnPreviewMouseUp);
            }
        }

        protected virtual void VerticalContentAlignmentChanged(VerticalAlignment oldValue, VerticalAlignment newValue)
        {
            this.SyncTextBlockProperty(Control.VerticalContentAlignmentProperty, FrameworkElement.VerticalAlignmentProperty);
        }

        public bool ShowNavigationUrlToolTip
        {
            get => 
                (bool) base.GetValue(ShowNavigationUrlToolTipProperty);
            set => 
                base.SetValue(ShowNavigationUrlToolTipProperty, value);
        }

        public bool IsHyperlinkPressed
        {
            get => 
                (bool) base.GetValue(IsHyperlinkPressedProperty);
            internal set => 
                base.SetValue(IsHyperlinkPressedPropertyKey, value);
        }

        public bool AllowAutoNavigate
        {
            get => 
                (bool) base.GetValue(AllowAutoNavigateProperty);
            set => 
                base.SetValue(AllowAutoNavigateProperty, value);
        }

        public string NavigationUrl
        {
            get => 
                (string) base.GetValue(NavigationUrlProperty);
            set => 
                base.SetValue(NavigationUrlProperty, value);
        }

        public string ActualNavigationUrl
        {
            get => 
                (string) base.GetValue(ActualNavigationUrlProperty);
            protected internal set => 
                base.SetValue(ActualNavigationUrlPropertyKey, value);
        }

        public string NavigationUrlMember
        {
            get => 
                (string) base.GetValue(NavigationUrlMemberProperty);
            set => 
                base.SetValue(NavigationUrlMemberProperty, value);
        }

        public string NavigationUrlFormat
        {
            get => 
                (string) base.GetValue(NavigationUrlFormatProperty);
            set => 
                base.SetValue(NavigationUrlFormatProperty, value);
        }

        public string Text
        {
            get => 
                (string) base.GetValue(TextProperty);
            set => 
                base.SetValue(TextProperty, value);
        }

        public string DisplayMember
        {
            get => 
                (string) base.GetValue(DisplayMemberProperty);
            set => 
                base.SetValue(DisplayMemberProperty, value);
        }

        public string HighlightedText
        {
            get => 
                (string) base.GetValue(HighlightedTextProperty);
            internal set => 
                base.SetValue(HighlightedTextPropertyKey, value);
        }

        public DevExpress.Xpf.Editors.HighlightedTextCriteria HighlightedTextCriteria
        {
            get => 
                (DevExpress.Xpf.Editors.HighlightedTextCriteria) base.GetValue(HighlightedTextCriteriaProperty);
            internal set => 
                base.SetValue(HighlightedTextCriteriaPropertyKey, value);
        }

        public ICommand Command
        {
            get => 
                (ICommand) base.GetValue(CommandProperty);
            set => 
                base.SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => 
                base.GetValue(CommandParameterProperty);
            set => 
                base.SetValue(CommandParameterProperty, value);
        }

        public IInputElement CommandTarget
        {
            get => 
                (IInputElement) base.GetValue(CommandTargetProperty);
            set => 
                base.SetValue(CommandTargetProperty, value);
        }

        private HyperlinkEditStrategy EditStrategy =>
            (HyperlinkEditStrategy) base.EditStrategy;

        protected internal EditBoxWrapper EditBox { get; }

        protected DevExpress.Xpf.Bars.Native.CommandSourceHelper CommandSourceHelper { get; }

        protected override bool IsEnabledCore =>
            base.IsEnabledCore && this.CommandSourceHelper.CanExecute;

        string IExportSettings.Url =>
            this.ActualNavigationUrl;

        HorizontalAlignment ITextExportSettings.HorizontalAlignment =>
            HorizontalAlignment.Left;

        VerticalAlignment ITextExportSettings.VerticalAlignment =>
            (base.VerticalContentAlignment != VerticalAlignment.Stretch) ? base.VerticalContentAlignment : ExportSettingDefaultValue.VerticalAlignment;

        string ITextExportSettings.Text =>
            this.GetExportText();

        object ITextExportSettings.TextValue =>
            this.GetTextValueInternal();

        string ITextExportSettings.TextValueFormatString =>
            base.DisplayFormatString;

        TextWrapping ITextExportSettings.TextWrapping =>
            TextWrapping.NoWrap;

        bool ITextExportSettings.NoTextExport =>
            ExportSettingDefaultValue.NoTextExport;

        bool? ITextExportSettings.XlsExportNativeFormat =>
            this.GetXlsExportNativeFormatInternal();

        TextTrimming ITextExportSettings.TextTrimming =>
            TextTrimming.CharacterEllipsis;

        string ITextExportSettings.XlsxFormatString =>
            ExportSettingDefaultValue.XlsxFormatString;

        TextDecorationCollection ITextExportSettings.TextDecorations =>
            this.GetPrintTextDecorations();

        FontFamily ITextExportSettings.FontFamily =>
            base.FontFamily;

        FontStyle ITextExportSettings.FontStyle =>
            base.FontStyle;

        FontWeight ITextExportSettings.FontWeight =>
            base.FontWeight;

        double ITextExportSettings.FontSize =>
            base.FontSize;

        Thickness ITextExportSettings.Padding =>
            base.Padding;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly HyperlinkEdit.<>c <>9 = new HyperlinkEdit.<>c();

            internal void <.cctor>b__19_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((HyperlinkEdit) o).NavigationUrlMemberChanged((string) args.OldValue, (string) args.NewValue);
            }

            internal void <.cctor>b__19_1(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((HyperlinkEdit) o).NavigateUrlChanged((string) args.OldValue, (string) args.NewValue);
            }

            internal void <.cctor>b__19_2(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((HyperlinkEdit) o).NavigateUrlFormatChanged((string) args.OldValue, (string) args.NewValue);
            }

            internal void <.cctor>b__19_3(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((HyperlinkEdit) o).TextChanged((string) args.OldValue, (string) args.NewValue);
            }

            internal void <.cctor>b__19_4(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((HyperlinkEdit) o).DisplayMemberChanged((string) args.OldValue, (string) args.NewValue);
            }

            internal void <.cctor>b__19_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((HyperlinkEdit) d).HighlightedTextCriteriaChanged((HighlightedTextCriteria) e.NewValue);
            }

            internal void <.cctor>b__19_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((HyperlinkEdit) d).HighlightedTextChanged((string) e.NewValue);
            }

            internal void <.cctor>b__19_7(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((HyperlinkEdit) o).CommandChanged((ICommand) args.NewValue);
            }

            internal void <.cctor>b__19_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((HyperlinkEdit) d).VerticalContentAlignmentChanged((VerticalAlignment) e.OldValue, (VerticalAlignment) e.NewValue);
            }

            internal void <.cctor>b__19_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((HyperlinkEdit) d).PaddingChanged((Thickness) e.OldValue, (Thickness) e.NewValue);
            }
        }
    }
}

