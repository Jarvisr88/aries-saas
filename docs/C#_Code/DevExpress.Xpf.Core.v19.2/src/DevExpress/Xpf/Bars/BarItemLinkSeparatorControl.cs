namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Themes;
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public class BarItemLinkSeparatorControl : BarItemLinkControl, IBarItemLinkSeparatorControl, IBarItemLinkControl, IFrameworkElementAPISupport, IUIElementAPI, IAnimatable, IFrameworkInputElement, IInputElement, ISupportInitialize, IQueryAmbient
    {
        private Orientation layoutOrientation;
        public static readonly DependencyProperty InMenuContentOffsetProperty;
        protected static readonly DependencyPropertyKey InMenuOrientationPropertyKey;
        public static readonly DependencyProperty InMenuOrientationProperty;

        event ContextMenuEventHandler IFrameworkElementAPISupport.ContextMenuClosing;

        event ContextMenuEventHandler IFrameworkElementAPISupport.ContextMenuOpening;

        event DependencyPropertyChangedEventHandler IFrameworkElementAPISupport.DataContextChanged;

        event EventHandler IFrameworkElementAPISupport.Initialized;

        event RoutedEventHandler IFrameworkElementAPISupport.Loaded;

        event RequestBringIntoViewEventHandler IFrameworkElementAPISupport.RequestBringIntoView;

        event SizeChangedEventHandler IFrameworkElementAPISupport.SizeChanged;

        event EventHandler<DataTransferEventArgs> IFrameworkElementAPISupport.SourceUpdated;

        event EventHandler<DataTransferEventArgs> IFrameworkElementAPISupport.TargetUpdated;

        event ToolTipEventHandler IFrameworkElementAPISupport.ToolTipClosing;

        event ToolTipEventHandler IFrameworkElementAPISupport.ToolTipOpening;

        event RoutedEventHandler IFrameworkElementAPISupport.Unloaded;

        event DragEventHandler IUIElementAPI.DragEnter;

        event DragEventHandler IUIElementAPI.DragLeave;

        event DragEventHandler IUIElementAPI.DragOver;

        event DragEventHandler IUIElementAPI.Drop;

        event GiveFeedbackEventHandler IUIElementAPI.GiveFeedback;

        event EventHandler<TouchEventArgs> IUIElementAPI.GotTouchCapture;

        event DependencyPropertyChangedEventHandler IUIElementAPI.IsKeyboardFocusedChanged;

        event DependencyPropertyChangedEventHandler IUIElementAPI.IsKeyboardFocusWithinChanged;

        event DependencyPropertyChangedEventHandler IUIElementAPI.IsMouseCapturedChanged;

        event DependencyPropertyChangedEventHandler IUIElementAPI.IsMouseDirectlyOverChanged;

        event DependencyPropertyChangedEventHandler IUIElementAPI.IsStylusCapturedChanged;

        event DependencyPropertyChangedEventHandler IUIElementAPI.IsStylusCaptureWithinChanged;

        event DependencyPropertyChangedEventHandler IUIElementAPI.IsStylusDirectlyOverChanged;

        event EventHandler<TouchEventArgs> IUIElementAPI.LostTouchCapture;

        event DragEventHandler IUIElementAPI.PreviewDragEnter;

        event DragEventHandler IUIElementAPI.PreviewDragLeave;

        event DragEventHandler IUIElementAPI.PreviewDragOver;

        event DragEventHandler IUIElementAPI.PreviewDrop;

        event GiveFeedbackEventHandler IUIElementAPI.PreviewGiveFeedback;

        event QueryContinueDragEventHandler IUIElementAPI.PreviewQueryContinueDrag;

        event EventHandler<TouchEventArgs> IUIElementAPI.PreviewTouchDown;

        event EventHandler<TouchEventArgs> IUIElementAPI.PreviewTouchMove;

        event EventHandler<TouchEventArgs> IUIElementAPI.PreviewTouchUp;

        event QueryContinueDragEventHandler IUIElementAPI.QueryContinueDrag;

        event QueryCursorEventHandler IUIElementAPI.QueryCursor;

        event EventHandler<TouchEventArgs> IUIElementAPI.TouchDown;

        event EventHandler<TouchEventArgs> IUIElementAPI.TouchEnter;

        event EventHandler<TouchEventArgs> IUIElementAPI.TouchLeave;

        event EventHandler<TouchEventArgs> IUIElementAPI.TouchMove;

        event EventHandler<TouchEventArgs> IUIElementAPI.TouchUp;

        static BarItemLinkSeparatorControl();
        public BarItemLinkSeparatorControl(BarItemLinkSeparator separator);
        protected override Size ArrangeOverride(Size arrangeBounds);
        void IFrameworkElementAPISupport.BeginStoryboard(Storyboard storyboard);
        void IFrameworkElementAPISupport.BeginStoryboard(Storyboard storyboard, HandoffBehavior handoffBehavior);
        void IFrameworkElementAPISupport.BeginStoryboard(Storyboard storyboard, HandoffBehavior handoffBehavior, bool isControllable);
        void IFrameworkElementAPISupport.BringIntoView();
        void IFrameworkElementAPISupport.BringIntoView(Rect targetRectangle);
        object IFrameworkElementAPISupport.FindResource(object resourceKey);
        BindingExpression IFrameworkElementAPISupport.GetBindingExpression(DependencyProperty dp);
        BindingExpression IFrameworkElementAPISupport.SetBinding(DependencyProperty dp, string path);
        BindingExpressionBase IFrameworkElementAPISupport.SetBinding(DependencyProperty dp, BindingBase binding);
        void IFrameworkElementAPISupport.SetResourceReference(DependencyProperty dp, object name);
        bool IFrameworkElementAPISupport.ShouldSerializeResources();
        object IFrameworkElementAPISupport.TryFindResource(object resourceKey);
        void IUIElementAPI.AddHandler(RoutedEvent routedEvent, Delegate handler, bool handledEventsToo);
        void IUIElementAPI.AddToEventRoute(EventRoute route, RoutedEventArgs e);
        bool IUIElementAPI.ShouldSerializeCommandBindings();
        bool IUIElementAPI.ShouldSerializeInputBindings();
        public override bool GetCanShowKeyTip();
        private Size GetCorrectSize(Size size);
        protected internal override bool GetIsSelectable();
        protected object GetTemplateFromProvider(DependencyProperty prop, BarItemSeparatorThemeKeys themeKeys);
        protected override Size MeasureOverride(Size availableSize);
        protected virtual void OnInMenuOrientationChanged(Orientation oldValue);
        protected static void OnInMenuOrientationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected override void OnLoaded(object sender, RoutedEventArgs e);
        public override void OnMaxGlyphSizeChanged(Size oldValue, Size newValue);
        protected virtual void UpdateInMenuContentOffset();
        protected internal override void UpdateStyleByContainerType(LinkContainerType type);

        public BarItemLinkSeparator LinkSeparator { get; }

        public double InMenuContentOffset { get; set; }

        protected internal Orientation LayoutOrientation { get; set; }

        public Orientation InMenuOrientation { get; protected internal set; }

        Style IFrameworkElementAPISupport.Style { get; set; }

        bool IFrameworkElementAPISupport.OverridesDefaultStyle { get; set; }

        bool IFrameworkElementAPISupport.UseLayoutRounding { get; set; }

        ResourceDictionary IFrameworkElementAPISupport.Resources { get; set; }

        [Localizability(LocalizationCategory.NeverLocalize), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        object IFrameworkElementAPISupport.DataContext { get; set; }

        BindingGroup IFrameworkElementAPISupport.BindingGroup { get; set; }

        XmlLanguage IFrameworkElementAPISupport.Language { get; set; }

        [Localizability(LocalizationCategory.NeverLocalize)]
        object IFrameworkElementAPISupport.Tag { get; set; }

        InputScope IFrameworkElementAPISupport.InputScope { get; set; }

        Transform IFrameworkElementAPISupport.LayoutTransform { get; set; }

        double IFrameworkElementAPISupport.MinWidth { get; set; }

        double IFrameworkElementAPISupport.MaxWidth { get; set; }

        double IFrameworkElementAPISupport.MinHeight { get; set; }

        double IFrameworkElementAPISupport.MaxHeight { get; set; }

        FlowDirection IFrameworkElementAPISupport.FlowDirection { get; set; }

        Thickness IFrameworkElementAPISupport.Margin { get; set; }

        HorizontalAlignment IFrameworkElementAPISupport.HorizontalAlignment { get; set; }

        VerticalAlignment IFrameworkElementAPISupport.VerticalAlignment { get; set; }

        Style IFrameworkElementAPISupport.FocusVisualStyle { get; set; }

        TriggerCollection IFrameworkElementAPISupport.Triggers { get; }

        DependencyObject IFrameworkElementAPISupport.TemplatedParent { get; }

        bool IFrameworkElementAPISupport.IsInitialized { get; }

        bool IFrameworkElementAPISupport.ForceCursor { get; set; }

        double IFrameworkElementAPISupport.ActualWidth { get; }

        double IFrameworkElementAPISupport.ActualHeight { get; }

        Cursor IFrameworkElementAPISupport.Cursor { get; set; }

        bool IFrameworkElementAPISupport.IsLoaded { get; }

        ContextMenu IFrameworkElementAPISupport.ContextMenu { get; set; }

        bool IUIElementAPI.IsVisible { get; }

        InputBindingCollection IUIElementAPI.InputBindings { get; }

        CommandBindingCollection IUIElementAPI.CommandBindings { get; }
    }
}

