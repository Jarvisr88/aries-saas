namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Threading;

    [ContentProperty("PopupContent")]
    public abstract class DropDownButtonBase : SimpleButton
    {
        private static readonly Action<ButtonBase, bool> setIsPressed;
        public static readonly DependencyProperty ArrowGlyphProperty = DependencyProperty.Register("ArrowGlyph", typeof(ImageSource), typeof(DropDownButtonBase), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty ArrowAlignmentProperty = DependencyProperty.Register("ArrowAlignment", typeof(Dock), typeof(DropDownButtonBase), new FrameworkPropertyMetadata(Dock.Right));
        public static readonly DependencyProperty ArrowPaddingProperty = DependencyProperty.Register("ArrowPadding", typeof(Thickness), typeof(DropDownButtonBase), new FrameworkPropertyMetadata(null));
        protected static readonly DependencyPropertyKey IsMouseOverArrowPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsMouseOverArrow", typeof(bool), typeof(DropDownButtonBase), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty IsMouseOverArrowProperty = IsMouseOverArrowPropertyKey.DependencyProperty;
        public static readonly DependencyProperty PopupContentProperty;
        public static readonly DependencyProperty PopupContentTemplateProperty;
        public static readonly DependencyProperty PopupContentTemplateSelectorProperty;
        public static readonly DependencyProperty IsPopupOpenProperty;
        public static readonly DependencyProperty PopupAutoWidthProperty;
        public static readonly DependencyProperty PopupWidthProperty;
        public static readonly DependencyProperty PopupMinWidthProperty;
        public static readonly DependencyProperty PopupMaxWidthProperty;
        public static readonly DependencyProperty PopupHeightProperty;
        public static readonly DependencyProperty PopupMinHeightProperty;
        public static readonly DependencyProperty PopupMaxHeightProperty;
        public static readonly DependencyProperty PopupDropAlignmentProperty;
        private BarPopupBase actualPopup;
        private readonly Locker clickLocker = new Locker();
        private readonly Locker popupClosingLocker = new Locker();

        static DropDownButtonBase()
        {
            PopupContentProperty = DependencyProperty.Register("PopupContent", typeof(object), typeof(DropDownButtonBase), new FrameworkPropertyMetadata(null, (d, e) => ((DropDownButtonBase) d).UpdatePopup()));
            PopupContentTemplateProperty = DependencyProperty.Register("PopupContentTemplate", typeof(DataTemplate), typeof(DropDownButtonBase), new FrameworkPropertyMetadata(null, (d, e) => ((DropDownButtonBase) d).UpdatePopup()));
            PopupContentTemplateSelectorProperty = DependencyProperty.Register("PopupContentTemplateSelector", typeof(DataTemplateSelector), typeof(DropDownButtonBase), new FrameworkPropertyMetadata(null, (d, e) => ((DropDownButtonBase) d).UpdatePopup()));
            IsPopupOpenProperty = DependencyPropertyManager.Register("IsPopupOpen", typeof(bool), typeof(DropDownButtonBase), new FrameworkPropertyMetadata(false, (d, e) => ((DropDownButtonBase) d).OnIsPopupOpenChanged((bool) e.OldValue, (bool) e.NewValue), new CoerceValueCallback(DropDownButtonBase.CoerceIsPopupOpen)));
            PopupAutoWidthProperty = DependencyProperty.Register("PopupAutoWidth", typeof(bool?), typeof(DropDownButtonBase), new FrameworkPropertyMetadata(null));
            PopupWidthProperty = DependencyProperty.Register("PopupWidth", typeof(double), typeof(DropDownButtonBase), new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0, (d, e) => ((DropDownButtonBase) d).OnPopupPropertiesChanged((double) e.OldValue, (double) e.NewValue)));
            PopupMinWidthProperty = DependencyProperty.Register("PopupMinWidth", typeof(double), typeof(DropDownButtonBase), new FrameworkPropertyMetadata(0.0, (d, e) => ((DropDownButtonBase) d).OnPopupPropertiesChanged((double) e.OldValue, (double) e.NewValue)));
            PopupMaxWidthProperty = DependencyProperty.Register("PopupMaxWidth", typeof(double), typeof(DropDownButtonBase), new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0, (d, e) => ((DropDownButtonBase) d).OnPopupPropertiesChanged((double) e.OldValue, (double) e.NewValue)));
            PopupHeightProperty = DependencyProperty.Register("PopupHeight", typeof(double), typeof(DropDownButtonBase), new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0, (d, e) => ((DropDownButtonBase) d).OnPopupPropertiesChanged((double) e.OldValue, (double) e.NewValue)));
            PopupMinHeightProperty = DependencyProperty.Register("PopupMinHeight", typeof(double), typeof(DropDownButtonBase), new FrameworkPropertyMetadata(0.0, (d, e) => ((DropDownButtonBase) d).OnPopupPropertiesChanged((double) e.OldValue, (double) e.NewValue)));
            PopupMaxHeightProperty = DependencyProperty.Register("PopupMaxHeight", typeof(double), typeof(DropDownButtonBase), new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0, (d, e) => ((DropDownButtonBase) d).OnPopupPropertiesChanged((double) e.OldValue, (double) e.NewValue)));
            PopupDropAlignmentProperty = DependencyProperty.Register("PopupDropAlignment", typeof(Dock), typeof(DropDownButtonBase), new FrameworkPropertyMetadata(Dock.Bottom));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownButtonBase), new FrameworkPropertyMetadata(typeof(DropDownButtonBase)));
            int? parametersCount = null;
            setIsPressed = DevExpress.Xpf.Core.Internal.ReflectionHelper.CreateInstanceMethodHandler<ButtonBase, Action<ButtonBase, bool>>(null, "SetIsPressed", BindingFlags.NonPublic | BindingFlags.Instance, parametersCount, null, true);
        }

        public DropDownButtonBase()
        {
            base.SetCurrentValue(IsPopupOpenProperty, false);
        }

        private void ActualPopupClosing(object sender, EventArgs e)
        {
            this.popupClosingLocker.Lock();
            base.SetCurrentValue(IsPopupOpenProperty, false);
            base.Chrome.UpdateStates();
            this.popupClosingLocker.Unlock();
        }

        private void ClickCore()
        {
            if (this.IsClickAllowed())
            {
                this.clickLocker.DoIfNotLocked(new Action(this.OnClick));
            }
        }

        private void ClosePopup()
        {
            if (this.IsPopupOpen && !this.IsPopupClosing)
            {
                this.IsPopupOpen = false;
            }
        }

        private static object CoerceIsPopupOpen(DependencyObject d, object baseValue)
        {
            bool flag = (bool) baseValue;
            if (((DropDownButtonBase) d).ActualPopup == null)
            {
                flag = false;
            }
            return flag;
        }

        private static PlacementMode ConvertDockToPlacement(Dock dropAlignment)
        {
            switch (dropAlignment)
            {
                case Dock.Left:
                    return PlacementMode.Left;

                case Dock.Top:
                    return PlacementMode.Top;

                case Dock.Right:
                    return PlacementMode.Right;

                case Dock.Bottom:
                    return PlacementMode.Bottom;
            }
            return PlacementMode.Left;
        }

        private void DoIfAutoPopupWidth()
        {
            PopupBorderControl child = this.ActualPopup.Child as PopupBorderControl;
            if (child == null)
            {
                this.ActualPopup.ClearValue(FrameworkElement.MaxWidthProperty);
                this.ActualPopup.ClearValue(FrameworkElement.WidthProperty);
                this.ActualPopup.MinWidth = base.ActualWidth;
            }
            else
            {
                child.ClearValue(PopupBorderControl.ContentMaxWidthProperty);
                child.ClearValue(PopupBorderControl.ContentWidthProperty);
                if (double.IsNaN(this.ActualPopup.Width))
                {
                    child.ContentMinWidth = base.ActualWidth;
                }
                else
                {
                    child.ClearValue(PopupBorderControl.ContentMinWidthProperty);
                }
            }
        }

        private void DoIfInPressMode()
        {
            bool flag = true;
            try
            {
                using (this.GetMouseClickLocker())
                {
                    this.OnClick();
                }
                flag = false;
            }
            finally
            {
                if (flag)
                {
                    setIsPressed(this, false);
                    base.ReleaseMouseCapture();
                }
            }
        }

        private IDisposable GetMouseClickLocker()
        {
            Func<DropDownButtonChrome, RenderButtonBorderContext> func1 = <>c.<>9__106_0;
            if (<>c.<>9__106_0 == null)
            {
                Func<DropDownButtonChrome, RenderButtonBorderContext> local1 = <>c.<>9__106_0;
                func1 = <>c.<>9__106_0 = x => x.ArrowPart;
            }
            Func<RenderButtonBorderContext, bool> func2 = <>c.<>9__106_1;
            if (<>c.<>9__106_1 == null)
            {
                Func<RenderButtonBorderContext, bool> local2 = <>c.<>9__106_1;
                func2 = <>c.<>9__106_1 = x => x.IsMouseOverCore;
            }
            return (((RenderButtonBorderContext) func2).Return<RenderButtonBorderContext, bool>(((Func<RenderButtonBorderContext, bool>) (<>c.<>9__106_2 ??= () => false)), (<>c.<>9__106_2 ??= () => false)) ? this.clickLocker.Lock() : this.clickLocker);
        }

        private bool IsClickAllowed()
        {
            Func<DropDownButtonChrome, RenderButtonBorderContext> func8;
            DropDownButtonChrome input = base.Chrome as DropDownButtonChrome;
            if (input == null)
            {
                return false;
            }
            Func<DropDownButtonChrome, RenderButtonBorderContext> evaluator = <>c.<>9__109_0;
            if (<>c.<>9__109_0 == null)
            {
                Func<DropDownButtonChrome, RenderButtonBorderContext> local1 = <>c.<>9__109_0;
                evaluator = <>c.<>9__109_0 = x => x.ContentPart;
            }
            Func<RenderButtonBorderContext, bool> func2 = <>c.<>9__109_1;
            if (<>c.<>9__109_1 == null)
            {
                Func<RenderButtonBorderContext, bool> local2 = <>c.<>9__109_1;
                func2 = <>c.<>9__109_1 = x => x.IsMouseOverCore;
            }
            if (!input.With<DropDownButtonChrome, RenderButtonBorderContext>(evaluator).Return<RenderButtonBorderContext, bool>(func2, (<>c.<>9__109_2 ??= () => false)))
            {
                Func<DropDownButtonChrome, RenderButtonBorderContext> func3 = <>c.<>9__109_3;
                if (<>c.<>9__109_3 == null)
                {
                    Func<DropDownButtonChrome, RenderButtonBorderContext> local4 = <>c.<>9__109_3;
                    func3 = <>c.<>9__109_3 = x => x.ContentAndArrowPart;
                }
                Func<RenderButtonBorderContext, bool> func4 = <>c.<>9__109_4;
                if (<>c.<>9__109_4 == null)
                {
                    Func<RenderButtonBorderContext, bool> local5 = <>c.<>9__109_4;
                    func4 = <>c.<>9__109_4 = x => x.IsMouseOverCore;
                }
                if (!input.With<DropDownButtonChrome, RenderButtonBorderContext>(func3).Return<RenderButtonBorderContext, bool>(func4, (<>c.<>9__109_5 ??= () => false)))
                {
                    Func<DropDownButtonChrome, RenderButtonBorderContext> func5 = <>c.<>9__109_6;
                    if (<>c.<>9__109_6 == null)
                    {
                        Func<DropDownButtonChrome, RenderButtonBorderContext> local7 = <>c.<>9__109_6;
                        func5 = <>c.<>9__109_6 = x => x.ContentPart;
                    }
                    Func<RenderButtonBorderContext, bool> func6 = <>c.<>9__109_7;
                    if (<>c.<>9__109_7 == null)
                    {
                        Func<RenderButtonBorderContext, bool> local8 = <>c.<>9__109_7;
                        func6 = <>c.<>9__109_7 = x => !x.IsMouseOverCore;
                    }
                    if (!input.With<DropDownButtonChrome, RenderButtonBorderContext>(func5).Return<RenderButtonBorderContext, bool>(func6, (<>c.<>9__109_8 ??= () => false)))
                    {
                        goto TR_0006;
                    }
                    else
                    {
                        Func<DropDownButtonChrome, RenderButtonBorderContext> func7 = <>c.<>9__109_9;
                        if (<>c.<>9__109_9 == null)
                        {
                            Func<DropDownButtonChrome, RenderButtonBorderContext> local10 = <>c.<>9__109_9;
                            func7 = <>c.<>9__109_9 = x => x.ArrowPart;
                        }
                        Func<RenderButtonBorderContext, bool> func9 = <>c.<>9__109_10;
                        if (<>c.<>9__109_10 == null)
                        {
                            Func<RenderButtonBorderContext, bool> local11 = <>c.<>9__109_10;
                            func9 = <>c.<>9__109_10 = x => !x.IsMouseOverCore;
                        }
                        if (!input.With<DropDownButtonChrome, RenderButtonBorderContext>(func7).Return<RenderButtonBorderContext, bool>(func9, (<>c.<>9__109_11 ??= () => false)))
                        {
                            goto TR_0006;
                        }
                    }
                }
            }
            return true;
        TR_0006:
            func8 = <>c.<>9__109_12;
            if (<>c.<>9__109_12 == null)
            {
                Func<DropDownButtonChrome, RenderButtonBorderContext> local13 = <>c.<>9__109_12;
                func8 = <>c.<>9__109_12 = x => x.ContentAndArrowPart;
            }
            Func<RenderButtonBorderContext, bool> func10 = <>c.<>9__109_13;
            if (<>c.<>9__109_13 == null)
            {
                Func<RenderButtonBorderContext, bool> local14 = <>c.<>9__109_13;
                func10 = <>c.<>9__109_13 = x => !x.IsMouseOverCore;
            }
            return (((RenderButtonBorderContext) func10).Return<RenderButtonBorderContext, bool>(((Func<RenderButtonBorderContext, bool>) (<>c.<>9__109_14 ??= () => false)), (<>c.<>9__109_14 ??= () => false)) || base.IsDefaulted);
        }

        protected virtual void OnActualPopupChanged(BarPopupBase oldValue)
        {
            if (oldValue != null)
            {
                oldValue.RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(this.OnClickInsidePopup));
                oldValue.Closed -= new EventHandler(this.ActualPopupClosing);
                base.RemoveLogicalChild(oldValue);
            }
            if (this.ActualPopup != null)
            {
                this.ActualPopup.Closed += new EventHandler(this.ActualPopupClosing);
                this.ActualPopup.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(this.OnClickInsidePopup));
                if (this.ActualPopup.Parent == null)
                {
                    base.AddLogicalChild(this.ActualPopup);
                }
                this.ActualPopup.PlacementTarget = this;
                this.ActualPopup.Placement = ConvertDockToPlacement(this.PopupDropAlignment);
                this.UpdateActualPopupSize();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.SetKeyGestures();
        }

        protected override void OnClick()
        {
            this.ClickCore();
        }

        protected virtual void OnClickInsidePopup(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        protected virtual void OnIsPopupOpenChanged(bool oldValue, bool newValue)
        {
            this.popupClosingLocker.DoIfNotLocked(() => this.ProcessIsOpenChanging(newValue));
        }

        protected override void OnIsPressedChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnIsPressedChanged(e);
            base.Chrome.UpdateStates();
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            if (!this.IsPopupOpen)
            {
                base.OnLostKeyboardFocus(e);
            }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            using (this.GetMouseClickLocker())
            {
                base.OnMouseEnter(e);
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (base.ClickMode != ClickMode.Hover)
            {
                e.Handled = true;
                base.Focus();
                if (e.ButtonState == MouseButtonState.Pressed)
                {
                    this.ProcessPressedState(e);
                }
                if (base.ClickMode == ClickMode.Press)
                {
                    this.DoIfInPressMode();
                }
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            using (this.GetMouseClickLocker())
            {
                base.OnMouseLeftButtonUp(e);
                if (base.ButtonKind == ButtonKind.Repeat)
                {
                    base.StopTimer();
                }
            }
        }

        protected virtual void OnPopupPropertiesChanged(double oldValue, double newValue)
        {
            this.UpdateActualPopupSize();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            this.UpdateActualPopupSize();
        }

        private void OpenPopup()
        {
            if (!this.IsPopupOpen && !this.IsPopupClosing)
            {
                this.IsPopupOpen = true;
            }
        }

        private void ProcessIsOpenChanging(bool newValue)
        {
            base.Chrome.UpdateStates();
            IDisposable disp = NavigationTree.DisableMouseEventsProcessing();
            if (this.ActualPopup != null)
            {
                this.ActualPopup.IsOpen = newValue;
            }
            if (disp != null)
            {
                Dispatcher dispatcher = base.Dispatcher;
                if (dispatcher == null)
                {
                    Dispatcher local1 = dispatcher;
                }
                else
                {
                    dispatcher.BeginInvoke(() => disp.Dispose(), new object[0]);
                }
            }
        }

        private void ProcessPressedState(MouseButtonEventArgs e)
        {
            Mouse.Capture(this, CaptureMode.SubTree);
            if (base.IsMouseCaptured)
            {
                if (!base.IsPressed)
                {
                    setIsPressed(this, true);
                }
                if ((base.ButtonKind == ButtonKind.Repeat) && (!this.ActAsDropDown && base.IsPressed))
                {
                    base.StartTimer();
                }
            }
        }

        private void SetKeyGesture(KeyGesture gesture, Action keyPressedCommand)
        {
            base.InputBindings.Add(new KeyBinding(new DelegateCommand(keyPressedCommand), gesture));
        }

        private void SetKeyGestures()
        {
            this.SetKeyGesture(new KeyGesture(Key.Down, ModifierKeys.Alt), new Action(this.OpenPopup));
            this.SetKeyGesture(new KeyGesture(Key.Up, ModifierKeys.Alt), new Action(this.ClosePopup));
            this.SetKeyGesture(new KeyGesture(Key.Escape), new Action(this.ClosePopup));
            this.SetKeyGesture(new KeyGesture(Key.F4), new Action(this.OpenPopup));
        }

        protected virtual void UpdateActualPopupSize()
        {
            if (this.ActualPopup != null)
            {
                bool? popupAutoWidth = this.PopupAutoWidth;
                if (popupAutoWidth != null)
                {
                    if (!popupAutoWidth.GetValueOrDefault())
                    {
                        this.UpdatePopupWidth();
                    }
                    else
                    {
                        this.DoIfAutoPopupWidth();
                    }
                    this.UpdatePopupHeight();
                }
                else
                {
                    if (double.IsNaN(this.PopupWidth) && ((this.PopupMinWidth == 0.0) && !double.IsPositiveInfinity(this.PopupMaxWidth)))
                    {
                        this.UpdatePopupWidth();
                    }
                    else
                    {
                        this.DoIfAutoPopupWidth();
                    }
                    this.UpdatePopupHeight();
                }
            }
        }

        protected virtual void UpdatePopup()
        {
            if (this.PopupContent == null)
            {
                if (this.PopupContentTemplate == null)
                {
                    this.ActualPopup = null;
                }
                else
                {
                    this.ActualPopup = new PopupControlContainer();
                    ContentControl control3 = new ContentControl();
                    control3.ContentTemplate = this.PopupContentTemplate;
                    ((PopupControlContainer) this.ActualPopup).Content = control3;
                }
            }
            else if (!(this.PopupContent is IPopupControl))
            {
                this.ActualPopup = new PopupControlContainer();
                ContentControl control2 = new ContentControl();
                control2.Content = this.PopupContent;
                control2.ContentTemplate = this.PopupContentTemplate;
                ((PopupControlContainer) this.ActualPopup).Content = control2;
            }
            else
            {
                BarPopupBase popup;
                IPopupControl popupContent = this.PopupContent as IPopupControl;
                if (popupContent != null)
                {
                    popup = popupContent.Popup;
                }
                else
                {
                    IPopupControl local1 = popupContent;
                    popup = null;
                }
                this.ActualPopup = popup;
            }
        }

        private void UpdatePopupHeight()
        {
            UIElement child = this.ActualPopup.Child;
            if (child is PopupBorderControl)
            {
                (child as PopupBorderControl).ContentHeight = !double.IsNaN(this.ActualPopup.Height) ? double.NaN : this.PopupHeight;
                (child as PopupBorderControl).ContentMinHeight = (Math.Abs(this.ActualPopup.MinHeight) < double.Epsilon) ? this.PopupMinHeight : 0.0;
                (child as PopupBorderControl).ContentMaxHeight = !double.IsPositiveInfinity(this.ActualPopup.MaxHeight) ? double.PositiveInfinity : this.PopupMaxHeight;
            }
            else
            {
                this.ActualPopup.Height = this.PopupHeight;
                this.ActualPopup.MinHeight = this.PopupMinHeight;
                this.ActualPopup.MaxHeight = this.PopupMaxHeight;
            }
        }

        private void UpdatePopupWidth()
        {
            UIElement child = this.ActualPopup.Child;
            if (child is PopupBorderControl)
            {
                (child as PopupBorderControl).ContentWidth = !double.IsNaN(this.ActualPopup.Width) ? double.NaN : this.PopupWidth;
                (child as PopupBorderControl).ContentMinWidth = Math.Max(this.ActualPopup.MinWidth, this.PopupMinWidth);
                (child as PopupBorderControl).ContentMaxWidth = !double.IsPositiveInfinity(this.ActualPopup.MaxWidth) ? double.PositiveInfinity : this.PopupMaxWidth;
            }
            else
            {
                this.ActualPopup.Width = this.PopupWidth;
                this.ActualPopup.MinWidth = this.PopupMinWidth;
                this.ActualPopup.MaxWidth = this.PopupMaxWidth;
            }
        }

        protected internal bool IsPopupClosing =>
            this.popupClosingLocker.IsLocked;

        protected internal abstract bool ActAsDropDown { get; }

        public ImageSource ArrowGlyph
        {
            get => 
                (ImageSource) base.GetValue(ArrowGlyphProperty);
            set => 
                base.SetValue(ArrowGlyphProperty, value);
        }

        public Dock ArrowAlignment
        {
            get => 
                (Dock) base.GetValue(ArrowAlignmentProperty);
            set => 
                base.SetValue(ArrowAlignmentProperty, value);
        }

        public Thickness ArrowPadding
        {
            get => 
                (Thickness) base.GetValue(ArrowPaddingProperty);
            set => 
                base.SetValue(ArrowPaddingProperty, value);
        }

        public bool IsMouseOverArrow
        {
            get => 
                (bool) base.GetValue(IsMouseOverArrowProperty);
            internal set => 
                base.SetValue(IsMouseOverArrowPropertyKey, value);
        }

        public object PopupContent
        {
            get => 
                base.GetValue(PopupContentProperty);
            set => 
                base.SetValue(PopupContentProperty, value);
        }

        public bool? PopupAutoWidth
        {
            get => 
                (bool?) base.GetValue(PopupAutoWidthProperty);
            set => 
                base.SetValue(PopupAutoWidthProperty, value);
        }

        public double PopupWidth
        {
            get => 
                (double) base.GetValue(PopupWidthProperty);
            set => 
                base.SetValue(PopupWidthProperty, value);
        }

        public double PopupMinWidth
        {
            get => 
                (double) base.GetValue(PopupMinWidthProperty);
            set => 
                base.SetValue(PopupMinWidthProperty, value);
        }

        public double PopupMaxWidth
        {
            get => 
                (double) base.GetValue(PopupMaxWidthProperty);
            set => 
                base.SetValue(PopupMaxWidthProperty, value);
        }

        public double PopupHeight
        {
            get => 
                (double) base.GetValue(PopupHeightProperty);
            set => 
                base.SetValue(PopupHeightProperty, value);
        }

        public double PopupMinHeight
        {
            get => 
                (double) base.GetValue(PopupMinHeightProperty);
            set => 
                base.SetValue(PopupMinHeightProperty, value);
        }

        public double PopupMaxHeight
        {
            get => 
                (double) base.GetValue(PopupMaxHeightProperty);
            set => 
                base.SetValue(PopupMaxHeightProperty, value);
        }

        public Dock PopupDropAlignment
        {
            get => 
                (Dock) base.GetValue(PopupDropAlignmentProperty);
            set => 
                base.SetValue(PopupDropAlignmentProperty, value);
        }

        public DataTemplate PopupContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(PopupContentTemplateProperty);
            set => 
                base.SetValue(PopupContentTemplateProperty, value);
        }

        public DataTemplateSelector PopupContentTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(PopupContentTemplateSelectorProperty);
            set => 
                base.SetValue(PopupContentTemplateSelectorProperty, value);
        }

        protected override IEnumerator LogicalChildren
        {
            get
            {
                IEnumerator[] args = new IEnumerator[] { base.LogicalChildren, new SingleObjectEnumerator(this.ActualPopup) };
                return new MergedEnumerator(args);
            }
        }

        public bool IsPopupOpen
        {
            get => 
                (bool) base.GetValue(IsPopupOpenProperty);
            set => 
                base.SetValue(IsPopupOpenProperty, value);
        }

        protected internal BarPopupBase ActualPopup
        {
            get => 
                this.actualPopup;
            set
            {
                if (!ReferenceEquals(this.actualPopup, value))
                {
                    BarPopupBase actualPopup = this.actualPopup;
                    this.actualPopup = value;
                    this.OnActualPopupChanged(actualPopup);
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DropDownButtonBase.<>c <>9 = new DropDownButtonBase.<>c();
            public static Func<DropDownButtonChrome, RenderButtonBorderContext> <>9__106_0;
            public static Func<RenderButtonBorderContext, bool> <>9__106_1;
            public static Func<bool> <>9__106_2;
            public static Func<DropDownButtonChrome, RenderButtonBorderContext> <>9__109_0;
            public static Func<RenderButtonBorderContext, bool> <>9__109_1;
            public static Func<bool> <>9__109_2;
            public static Func<DropDownButtonChrome, RenderButtonBorderContext> <>9__109_3;
            public static Func<RenderButtonBorderContext, bool> <>9__109_4;
            public static Func<bool> <>9__109_5;
            public static Func<DropDownButtonChrome, RenderButtonBorderContext> <>9__109_6;
            public static Func<RenderButtonBorderContext, bool> <>9__109_7;
            public static Func<bool> <>9__109_8;
            public static Func<DropDownButtonChrome, RenderButtonBorderContext> <>9__109_9;
            public static Func<RenderButtonBorderContext, bool> <>9__109_10;
            public static Func<bool> <>9__109_11;
            public static Func<DropDownButtonChrome, RenderButtonBorderContext> <>9__109_12;
            public static Func<RenderButtonBorderContext, bool> <>9__109_13;
            public static Func<bool> <>9__109_14;

            internal void <.cctor>b__18_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DropDownButtonBase) d).UpdatePopup();
            }

            internal void <.cctor>b__18_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DropDownButtonBase) d).UpdatePopup();
            }

            internal void <.cctor>b__18_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DropDownButtonBase) d).UpdatePopup();
            }

            internal void <.cctor>b__18_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DropDownButtonBase) d).OnIsPopupOpenChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__18_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DropDownButtonBase) d).OnPopupPropertiesChanged((double) e.OldValue, (double) e.NewValue);
            }

            internal void <.cctor>b__18_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DropDownButtonBase) d).OnPopupPropertiesChanged((double) e.OldValue, (double) e.NewValue);
            }

            internal void <.cctor>b__18_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DropDownButtonBase) d).OnPopupPropertiesChanged((double) e.OldValue, (double) e.NewValue);
            }

            internal void <.cctor>b__18_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DropDownButtonBase) d).OnPopupPropertiesChanged((double) e.OldValue, (double) e.NewValue);
            }

            internal void <.cctor>b__18_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DropDownButtonBase) d).OnPopupPropertiesChanged((double) e.OldValue, (double) e.NewValue);
            }

            internal void <.cctor>b__18_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DropDownButtonBase) d).OnPopupPropertiesChanged((double) e.OldValue, (double) e.NewValue);
            }

            internal RenderButtonBorderContext <GetMouseClickLocker>b__106_0(DropDownButtonChrome x) => 
                x.ArrowPart;

            internal bool <GetMouseClickLocker>b__106_1(RenderButtonBorderContext x) => 
                x.IsMouseOverCore;

            internal bool <GetMouseClickLocker>b__106_2() => 
                false;

            internal RenderButtonBorderContext <IsClickAllowed>b__109_0(DropDownButtonChrome x) => 
                x.ContentPart;

            internal bool <IsClickAllowed>b__109_1(RenderButtonBorderContext x) => 
                x.IsMouseOverCore;

            internal bool <IsClickAllowed>b__109_10(RenderButtonBorderContext x) => 
                !x.IsMouseOverCore;

            internal bool <IsClickAllowed>b__109_11() => 
                false;

            internal RenderButtonBorderContext <IsClickAllowed>b__109_12(DropDownButtonChrome x) => 
                x.ContentAndArrowPart;

            internal bool <IsClickAllowed>b__109_13(RenderButtonBorderContext x) => 
                !x.IsMouseOverCore;

            internal bool <IsClickAllowed>b__109_14() => 
                false;

            internal bool <IsClickAllowed>b__109_2() => 
                false;

            internal RenderButtonBorderContext <IsClickAllowed>b__109_3(DropDownButtonChrome x) => 
                x.ContentAndArrowPart;

            internal bool <IsClickAllowed>b__109_4(RenderButtonBorderContext x) => 
                x.IsMouseOverCore;

            internal bool <IsClickAllowed>b__109_5() => 
                false;

            internal RenderButtonBorderContext <IsClickAllowed>b__109_6(DropDownButtonChrome x) => 
                x.ContentPart;

            internal bool <IsClickAllowed>b__109_7(RenderButtonBorderContext x) => 
                !x.IsMouseOverCore;

            internal bool <IsClickAllowed>b__109_8() => 
                false;

            internal RenderButtonBorderContext <IsClickAllowed>b__109_9(DropDownButtonChrome x) => 
                x.ArrowPart;
        }
    }
}

