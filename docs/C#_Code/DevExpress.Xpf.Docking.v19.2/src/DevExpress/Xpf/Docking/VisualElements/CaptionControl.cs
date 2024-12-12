namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.UIAutomation;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;

    [TemplatePart(Name="PART_Image", Type=typeof(Image)), TemplatePart(Name="PART_Text", Type=typeof(FrameworkElement))]
    public class CaptionControl : AppearanceControl
    {
        public static readonly DependencyProperty TargetProperty;
        public static readonly DependencyProperty TextWrappingProperty;
        public static readonly DependencyProperty CaptionTextProperty;
        public static readonly DependencyProperty AppearanceProperty;
        private static readonly DependencyPropertyKey ActualAppearancePropertyKey;
        public static readonly DependencyProperty ActualAppearanceProperty;
        public static readonly DependencyProperty AlternateForegroundProperty;
        public static readonly DependencyProperty ShowCaptionImageProperty;
        public static readonly DependencyProperty CaptionImageProperty;
        protected FormattedText formattedText;
        private BaseLayoutItem _Item;

        static CaptionControl()
        {
            Type classType = typeof(CaptionControl);
            DependencyPropertyRegistrator<CaptionControl> registrator = new DependencyPropertyRegistrator<CaptionControl>();
            EventManager.RegisterClassHandler(classType, AccessKeyManager.AccessKeyPressedEvent, new AccessKeyPressedEventHandler(CaptionControl.OnAccessKeyPressed));
            FrameworkElement.ToolTipProperty.OverrideMetadata(classType, new FrameworkPropertyMetadata(null, (dObj, e) => ((CaptionControl) dObj).OnToolTipChanged(e.NewValue), (d, v) => ((CaptionControl) d).CoerceToolTip(v)));
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<UIElement>("Target", ref TargetProperty, null, null, null);
            registrator.Register<System.Windows.TextWrapping>("TextWrapping", ref TextWrappingProperty, System.Windows.TextWrapping.NoWrap, null, (dObj, value) => ((CaptionControl) dObj).CoerceTextWrapping((System.Windows.TextWrapping) value));
            registrator.Register<string>("CaptionText", ref CaptionTextProperty, null, (dObj, e) => ((CaptionControl) dObj).OnCaptionTextChanged((string) e.NewValue), null);
            registrator.Register<AppearanceObject>("Appearance", ref AppearanceProperty, null, (dObj, e) => ((CaptionControl) dObj).OnAppearanceChanged((AppearanceObject) e.NewValue), null);
            registrator.RegisterReadonly<AppearanceObject>("ActualAppearance", ref ActualAppearancePropertyKey, ref ActualAppearanceProperty, null, null, null);
            registrator.Register<Brush>("AlternateForeground", ref AlternateForegroundProperty, null, (dObj, e) => ((CaptionControl) dObj).OnAlternateForegroundChanged((Brush) e.NewValue), null);
            registrator.Register<bool>("ShowCaptionImage", ref ShowCaptionImageProperty, true, null, null);
            registrator.Register<ImageSource>("CaptionImage", ref CaptionImageProperty, null, null, null);
        }

        public CaptionControl()
        {
            this.EnsureAppearance();
            base.DataContextChanged += new DependencyPropertyChangedEventHandler(this.OnDataContextChanged);
        }

        private bool CanUseAlternateForeground() => 
            (!(this.Item is LayoutControlItem) || !this.Item.IsPropertyAssigned(Control.ForegroundProperty)) ? ((this.AlternateForeground != null) && (this.GetParentStyle(this.Item) == GroupBorderStyle.Tabbed)) : false;

        private bool CanUseDefaultBackground()
        {
            LayoutGroup item = this.Item as LayoutGroup;
            return ((item == null) ? (this.Item is LayoutPanel) : (item.GroupBorderStyle == GroupBorderStyle.GroupBox));
        }

        protected virtual void ClearLayoutItemBindings()
        {
            BindingHelper.ClearBinding(this, FrameworkElement.ToolTipProperty);
            BindingHelper.ClearBinding(this, TextWrappingProperty);
            BindingHelper.ClearBinding(this, CaptionTextProperty);
            BindingHelper.ClearBinding(this, AppearanceProperty);
            this.ResetToolTipService();
        }

        protected virtual object CoerceTextWrapping(System.Windows.TextWrapping wrapping) => 
            ((this.Item == null) || (!(this.Item is LayoutControlItem) && !(this.Item is FixedItem))) ? System.Windows.TextWrapping.NoWrap : wrapping;

        protected virtual object CoerceToolTip(object tooltip)
        {
            if (tooltip != null)
            {
                return tooltip;
            }
            string text = this.RecognizeAccessKey ? AccessKeyHelper.RemoveAccessKeyMarker(this.CaptionText) : this.CaptionText;
            return (!this.IsTextTrimmed(this.PartText, text) ? null : text);
        }

        private void EnsureAppearance()
        {
            if (this.ActualAppearance == null)
            {
                AppearanceObject obj1 = new AppearanceObject();
                obj1.Background = base.Background;
                obj1.Foreground = base.Foreground;
                obj1.FontWeight = new FontWeight?(base.FontWeight);
                obj1.FontFamily = base.FontFamily;
                obj1.FontSize = base.FontSize;
                obj1.FontStretch = new FontStretch?(base.FontStretch);
                obj1.FontStyle = new FontStyle?(base.FontStyle);
                this.ActualAppearance = obj1;
            }
        }

        protected virtual DependencyProperty GetCaptionTextProperty() => 
            BaseLayoutItem.ActualCaptionProperty;

        private Brush GetDefaultBackground() => 
            Control.BackgroundProperty.GetDefaultValue(typeof(CaptionControl)) as Brush;

        private FormattedText GetFormattedText(FrameworkElement textBlock, string text)
        {
            if ((this.formattedText == null) || (this.formattedText.Text != text))
            {
                Typeface typeface = new Typeface(TextBlock.GetFontFamily(textBlock), TextBlock.GetFontStyle(textBlock), TextBlock.GetFontWeight(textBlock), TextBlock.GetFontStretch(textBlock));
                this.formattedText = new FormattedText(text, CultureInfo.CurrentCulture, GetFlowDirection(textBlock), typeface, TextBlock.GetFontSize(textBlock), TextBlock.GetForeground(textBlock));
            }
            return this.formattedText;
        }

        private GroupBorderStyle GetParentStyle(BaseLayoutItem item)
        {
            if (item == null)
            {
                return GroupBorderStyle.NoBorder;
            }
            LayoutGroup group = item as LayoutGroup;
            return (((group == null) || !(((group.GroupBorderStyle != GroupBorderStyle.Tabbed) || (group.TabbedGroupDisplayModeCore == TabbedGroupDisplayMode.ContentOnly)) ? (group.GroupBorderStyle == GroupBorderStyle.GroupBox) : true)) ? this.GetParentStyle(item.Parent) : group.GroupBorderStyle);
        }

        private bool IsTextTrimmed(FrameworkElement textBlock, string text) => 
            ((textBlock != null) && !string.IsNullOrEmpty(text)) && ((this.GetFormattedText(textBlock, text).WidthIncludingTrailingWhitespace - textBlock.DesiredSize.Width) > 1.0);

        protected override Size MeasureOverride(Size constraint)
        {
            Size size = base.MeasureOverride(constraint);
            string str = this.RecognizeAccessKey ? AccessKeyHelper.RemoveAccessKeyMarker(this.CaptionText) : this.CaptionText;
            if ((this.PartText != null) && !string.IsNullOrEmpty(str))
            {
                double widthIncludingTrailingWhitespace = this.GetFormattedText(this.PartText, str).WidthIncludingTrailingWhitespace;
                double num2 = MathHelper.Round((double) (widthIncludingTrailingWhitespace + 1.0)) - widthIncludingTrailingWhitespace;
                double width = this.PartText.DesiredSize.Width;
                bool flag = false;
                if (((widthIncludingTrailingWhitespace - width) > 0.0) && ((widthIncludingTrailingWhitespace - width) < 1.0))
                {
                    size = new Size(size.Width + num2, size.Height);
                    flag = true;
                }
                double num4 = constraint.Width;
                if (((widthIncludingTrailingWhitespace - num4) > 0.0) && ((widthIncludingTrailingWhitespace - num4) < 1.0))
                {
                    size = new Size(num4 + num2, size.Height);
                    flag = true;
                }
                if (flag)
                {
                    base.MeasureOverride(new Size(num4 + num2, constraint.Height));
                }
            }
            return size;
        }

        private static void OnAccessKeyPressed(object sender, AccessKeyPressedEventArgs e)
        {
            CaptionControl objB = sender as CaptionControl;
            if (!e.Handled && ((e.Scope == null) && ((e.Target == null) || ReferenceEquals(e.Target, objB))))
            {
                e.Target = objB.Target;
            }
        }

        protected override void OnActualSizeChanged(Size value)
        {
            base.OnActualSizeChanged(value);
            base.CoerceValue(FrameworkElement.ToolTipProperty);
        }

        protected virtual void OnAlternateForegroundChanged(Brush newValue)
        {
            this.OnVisualChanged();
        }

        protected virtual void OnAppearanceChanged(AppearanceObject newValue)
        {
            this.UpdateAppearance();
            this.formattedText = null;
            base.InvalidateMeasure();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PartImage = base.GetTemplateChild("PART_Image") as Image;
            this.PartText = base.GetTemplateChild("PART_Text") as FrameworkElement;
            this.PartSpace = base.GetTemplateChild("PART_Space") as ColumnDefinition;
        }

        protected virtual void OnCaptionTextChanged(string captionText)
        {
            base.CoerceValue(FrameworkElement.ToolTipProperty);
            BaseHeadersPanel.Invalidate(this);
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new CaptionControlAutomationPeer(this);

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.Item = base.DataContext as BaseLayoutItem;
        }

        protected override void OnDispose()
        {
            this.ClearLayoutItemBindings();
            base.OnDispose();
        }

        protected override void OnGeometryChanged()
        {
            base.OnGeometryChanged();
            if (this.Item is LayoutControlItem)
            {
                this.Item.ClearValue(BaseLayoutItem.HasDesiredCaptionWidthPropertyKey);
                this.Item.ClearValue(BaseLayoutItem.DesiredCaptionWidthPropertyKey);
            }
            this.formattedText = null;
        }

        private void OnItemChanged()
        {
            if (this.Item is LayoutControlItem)
            {
                this.Target = ((LayoutControlItem) this.Item).Control;
            }
            this.SetLayoutItemBindings();
            this.UpdateAppearance();
        }

        protected virtual void OnToolTipChanged(object newValue)
        {
        }

        protected override void OnVisualChanged()
        {
            base.OnVisualChanged();
            this.UpdateAppearance();
        }

        private void ResetToolTipService()
        {
            BindingHelper.ClearBinding(this, ToolTipService.PlacementProperty);
            BindingHelper.ClearBinding(this, ToolTipService.BetweenShowDelayProperty);
            BindingHelper.ClearBinding(this, ToolTipService.HasDropShadowProperty);
            BindingHelper.ClearBinding(this, ToolTipService.HorizontalOffsetProperty);
            BindingHelper.ClearBinding(this, ToolTipService.InitialShowDelayProperty);
            BindingHelper.ClearBinding(this, ToolTipService.IsEnabledProperty);
            BindingHelper.ClearBinding(this, ToolTipService.PlacementRectangleProperty);
            BindingHelper.ClearBinding(this, ToolTipService.ShowDurationProperty);
            BindingHelper.ClearBinding(this, ToolTipService.ShowOnDisabledProperty);
            BindingHelper.ClearBinding(this, ToolTipService.VerticalOffsetProperty);
        }

        protected virtual void SetLayoutItemBindings()
        {
            this.SetupToolTipService();
            BindingHelper.SetBinding(this, FrameworkElement.ToolTipProperty, this.Item, BaseLayoutItem.ToolTipProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, TextWrappingProperty, this.Item, BaseLayoutItem.TextWrappingProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, CaptionTextProperty, this.Item, this.GetCaptionTextProperty(), BindingMode.OneWay);
            BindingHelper.SetBinding(this, AppearanceProperty, this.Item, BaseLayoutItem.ActualAppearanceObjectProperty, BindingMode.OneWay);
        }

        private void SetupToolTipService()
        {
            BindingHelper.SetBinding(this, ToolTipService.PlacementProperty, this.Item, ToolTipService.PlacementProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, ToolTipService.BetweenShowDelayProperty, this.Item, ToolTipService.BetweenShowDelayProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, ToolTipService.HasDropShadowProperty, this.Item, ToolTipService.HasDropShadowProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, ToolTipService.HorizontalOffsetProperty, this.Item, ToolTipService.HorizontalOffsetProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, ToolTipService.InitialShowDelayProperty, this.Item, ToolTipService.InitialShowDelayProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, ToolTipService.IsEnabledProperty, this.Item, ToolTipService.IsEnabledProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, ToolTipService.PlacementRectangleProperty, this.Item, ToolTipService.PlacementRectangleProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, ToolTipService.ShowDurationProperty, this.Item, ToolTipService.ShowDurationProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, ToolTipService.ShowOnDisabledProperty, this.Item, ToolTipService.ShowOnDisabledProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, ToolTipService.VerticalOffsetProperty, this.Item, ToolTipService.VerticalOffsetProperty, BindingMode.OneWay);
        }

        private object TryGetValue(DependencyProperty property, double value) => 
            !double.IsNaN(value) ? value : base.GetValue(property);

        private object TryGetValue(DependencyProperty property, object value) => 
            value ?? base.GetValue(property);

        protected void UpdateAppearance()
        {
            if (this.ItemAppearance != null)
            {
                Brush defaultBackground = this.GetDefaultBackground();
                bool flag = this.CanUseDefaultBackground();
                this.ActualAppearance.Background = (flag || (this.ItemAppearance.Background == null)) ? defaultBackground : this.ItemAppearance.Background;
                this.ActualAppearance.Foreground = (Brush) this.TryGetValue(this.CanUseAlternateForeground() ? AlternateForegroundProperty : Control.ForegroundProperty, this.ItemAppearance.Foreground);
                this.ActualAppearance.FontWeight = new FontWeight?((FontWeight) this.TryGetValue(Control.FontWeightProperty, this.ItemAppearance.FontWeight));
                this.ActualAppearance.FontFamily = (FontFamily) this.TryGetValue(Control.FontFamilyProperty, this.ItemAppearance.FontFamily);
                this.ActualAppearance.FontSize = (double) this.TryGetValue(Control.FontSizeProperty, this.ItemAppearance.FontSize);
                this.ActualAppearance.FontStretch = new FontStretch?((FontStretch) this.TryGetValue(Control.FontStretchProperty, this.ItemAppearance.FontStretch));
                this.ActualAppearance.FontStyle = new FontStyle?((FontStyle) this.TryGetValue(Control.FontStyleProperty, this.ItemAppearance.FontStyle));
            }
        }

        public AppearanceObject ActualAppearance
        {
            get => 
                (AppearanceObject) base.GetValue(ActualAppearanceProperty);
            private set => 
                base.SetValue(ActualAppearancePropertyKey, value);
        }

        public Brush AlternateForeground
        {
            get => 
                (Brush) base.GetValue(AlternateForegroundProperty);
            set => 
                base.SetValue(AlternateForegroundProperty, value);
        }

        public AppearanceObject Appearance
        {
            get => 
                (AppearanceObject) base.GetValue(AppearanceProperty);
            set => 
                base.SetValue(AppearanceProperty, value);
        }

        public ImageSource CaptionImage
        {
            get => 
                (ImageSource) base.GetValue(CaptionImageProperty);
            set => 
                base.SetValue(CaptionImageProperty, value);
        }

        public string CaptionText
        {
            get => 
                (string) base.GetValue(CaptionTextProperty);
            set => 
                base.SetValue(CaptionTextProperty, value);
        }

        public BaseLayoutItem Item
        {
            get => 
                this._Item;
            private set
            {
                if (!ReferenceEquals(this._Item, value))
                {
                    this._Item = value;
                    this.OnItemChanged();
                }
            }
        }

        public Image PartImage { get; private set; }

        public ColumnDefinition PartSpace { get; private set; }

        public FrameworkElement PartText { get; private set; }

        public bool ShowCaptionImage
        {
            get => 
                (bool) base.GetValue(ShowCaptionImageProperty);
            set => 
                base.SetValue(ShowCaptionImageProperty, value);
        }

        public UIElement Target
        {
            get => 
                (UIElement) base.GetValue(TargetProperty);
            set => 
                base.SetValue(TargetProperty, value);
        }

        public System.Windows.TextWrapping TextWrapping
        {
            get => 
                (System.Windows.TextWrapping) base.GetValue(TextWrappingProperty);
            set => 
                base.SetValue(TextWrappingProperty, value);
        }

        internal bool IsCaptionTextTrimmed =>
            this.IsTextTrimmed(this.PartText, this.CaptionText);

        protected virtual bool RecognizeAccessKey =>
            LayoutItemsHelper.CanRecognizeAccessKey(this.Item);

        private AppearanceObject ItemAppearance =>
            this.Item?.ActualAppearanceObject;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CaptionControl.<>c <>9 = new CaptionControl.<>c();

            internal void <.cctor>b__9_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((CaptionControl) dObj).OnToolTipChanged(e.NewValue);
            }

            internal object <.cctor>b__9_1(DependencyObject d, object v) => 
                ((CaptionControl) d).CoerceToolTip(v);

            internal object <.cctor>b__9_2(DependencyObject dObj, object value) => 
                ((CaptionControl) dObj).CoerceTextWrapping((TextWrapping) value);

            internal void <.cctor>b__9_3(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((CaptionControl) dObj).OnCaptionTextChanged((string) e.NewValue);
            }

            internal void <.cctor>b__9_4(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((CaptionControl) dObj).OnAppearanceChanged((AppearanceObject) e.NewValue);
            }

            internal void <.cctor>b__9_5(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((CaptionControl) dObj).OnAlternateForegroundChanged((Brush) e.NewValue);
            }
        }
    }
}

