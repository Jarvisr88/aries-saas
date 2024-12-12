namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class TextSplitterControl : Panel
    {
        public static readonly DependencyProperty ContentProperty;
        public static readonly DependencyProperty SplitMethodProperty;
        public static readonly DependencyProperty FirstStringHorizontalAlignmentProperty;
        public static readonly DependencyProperty SecondStringHorizontalAlignmentProperty;
        public static readonly DependencyProperty SecondStringProperty;
        public static readonly DependencyProperty FirstStringProperty;
        public static readonly DependencyProperty ContentVisibilityProperty;
        public static readonly DependencyProperty FirstStringOpacityProperty;
        public static readonly DependencyProperty SecondStringOpacityProperty;
        public static readonly DependencyProperty ContentTemplateProperty;
        public static readonly DependencyProperty IsUserContentVisibleProperty;
        protected static readonly DependencyPropertyKey IsUserContentVisiblePropertyKey;
        public static readonly DependencyProperty FirstStringMarginProperty;
        public static readonly DependencyProperty SecondStringMarginProperty;
        public static readonly DependencyProperty NormalTextStyleProperty;
        public static readonly DependencyProperty SelectedTextStyleProperty;
        public static readonly DependencyProperty ActualTextStyleProperty;
        protected static readonly DependencyPropertyKey ActualTextStylePropertyKey;
        public static readonly DependencyProperty IsRightSideArrowVisibleProperty;
        protected static readonly DependencyPropertyKey IsRightSideArrowVisiblePropertyKey;
        public static readonly DependencyProperty ActualArrowTemplateProperty;
        protected static readonly DependencyPropertyKey ActualArrowTemplatePropertyKey;
        public static readonly DependencyProperty RightSideArrowContainerStyleProperty;
        public static readonly DependencyProperty BottomSideArrowContainerStyleProperty;
        public static readonly DependencyProperty NormalArrowTemplateProperty;
        public static readonly DependencyProperty SelectedArrowTemplateProperty;
        public static readonly DependencyProperty IsArrowVisibleProperty;
        protected static readonly DependencyPropertyKey IsBottomSideArrowVisiblePropertyKey;
        public static readonly DependencyProperty IsBottomSideArrowVisibleProperty;
        public static readonly DependencyProperty IsSelectedProperty;
        public static readonly DependencyProperty DisabledTextStyleProperty;
        private DevExpress.Xpf.Bars.FontSettings fontSettings;
        private DevExpress.Xpf.Bars.BorderState borderState;
        private ContentControl firstStringControl;
        private ContentControl secondStringControl;
        private ContentControl arrowControl;

        static TextSplitterControl();
        public TextSplitterControl();
        private void ApplyFontSettings();
        protected override Size ArrangeOverride(Size finalSize);
        protected virtual void ArrangeRow(UIElement child1, UIElement child2, HorizontalAlignment horzAlignment, double top, double finalWidth);
        protected virtual ContentControl CreateArrowControl();
        protected virtual ContentControl CreateFirstStringControl();
        protected virtual ContentControl CreateSecondStringControl();
        private Size GetActualSize(UIElement element, Thickness margin);
        protected override Geometry GetLayoutClip(Size layoutSlotSize);
        protected override Size MeasureOverride(Size availableSize);
        protected virtual void OnBorderStateChanged(DevExpress.Xpf.Bars.BorderState oldValue);
        protected virtual void OnContentChanged(object oldValue);
        protected static void OnContentPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected static void OnDisabledTextStylePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected static void OnFirstStringMarginPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected virtual void OnFontSettingsChanged(DevExpress.Xpf.Bars.FontSettings oldValue);
        protected static void OnIsArrowVisiblePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected virtual void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e);
        protected static void OnIsSelectedPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected override void OnMouseEnter(MouseEventArgs e);
        protected override void OnMouseLeave(MouseEventArgs e);
        protected static void OnNormalArrowTemplatePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected static void OnNormalTextStylePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected virtual void OnPropertiesChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnRightSideArrowContainerStylePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected static void OnSecondStringMarginPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected static void OnSelectedArrowTemplatePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected static void OnSelectedTextStylePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected virtual void OnSplitMethodChanged(SplitTextMode oldValue);
        protected static void OnSplitMethodPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void SplitText(string text, ref string firstString, ref string secondString);
        protected virtual void SplitTextAutomatically(string text, ref string firstString, ref string secondString);
        protected virtual bool SplitTextBy(string text, ref string firstString, ref string secondString, params string[] splitSymbols);
        protected virtual void SplitTextByBreakLine(string text, ref string firstString, ref string secondString);
        protected virtual void SplitTextBySpace(string text, ref string firstString, ref string secondString);
        protected virtual void UpdateControls();
        protected virtual void UpdateLayoutProperties();

        public object Content { get; set; }

        public string FirstString { get; set; }

        public string SecondString { get; set; }

        public SplitTextMode SplitMethod { get; set; }

        public HorizontalAlignment FirstStringHorizontalAlignment { get; set; }

        public HorizontalAlignment SecondStringHorizontalAlignment { get; set; }

        public Visibility ContentVisibility { get; set; }

        public double FirstStringOpacity { get; set; }

        public double SecondStringOpacity { get; set; }

        public ControlTemplate ContentTemplate { get; set; }

        public bool IsUserContentVisible { get; protected set; }

        public Thickness FirstStringMargin { get; set; }

        public Thickness SecondStringMargin { get; set; }

        public Style NormalTextStyle { get; set; }

        public Style DisabledTextStyle { get; set; }

        public Style SelectedTextStyle { get; set; }

        public Style ActualTextStyle { get; protected set; }

        public Style RightSideArrowContainerStyle { get; set; }

        public Style BottomSideArrowContainerStyle { get; set; }

        public bool IsRightSideArrowVisible { get; protected set; }

        public ControlTemplate ActualArrowTemplate { get; protected set; }

        public ControlTemplate NormalArrowTemplate { get; set; }

        public ControlTemplate SelectedArrowTemplate { get; set; }

        public bool IsArrowVisible { get; set; }

        public bool IsBottomSideArrowVisible { get; protected set; }

        public bool IsSelected { get; set; }

        public DevExpress.Xpf.Bars.BorderState BorderState { get; set; }

        protected internal DevExpress.Xpf.Bars.FontSettings FontSettings { get; set; }

        protected internal ContentControl FirstStringControl { get; }

        protected internal ContentControl SecondStringControl { get; }

        protected internal ContentControl ArrowControl { get; }
    }
}

