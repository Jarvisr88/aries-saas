namespace DevExpress.Xpf.Editors.Flyout.Native
{
    using DevExpress.Data.Utils;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity.Internal;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Flyout;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Forms;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Threading;

    public class FlyoutBase : ContentControl
    {
        public static readonly DependencyProperty HorizontalOffsetProperty;
        public static readonly DependencyProperty IsOpenProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ActualIsOpenProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        protected static readonly DependencyProperty ShouldBeOpenedProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IndicatorDirectionProperty;
        public static readonly DependencyProperty PlacementTargetProperty;
        public static readonly DependencyProperty StaysOpenProperty;
        public static readonly DependencyProperty CloseOnInactiveWindowMouseLeaveProperty;
        public static readonly DependencyProperty VerticalOffsetProperty;
        public static readonly DependencyProperty AllowOutOfScreenProperty;
        public static readonly DependencyProperty AllowMoveAnimationProperty;
        private FlyoutPositionCalculator positionCalculator;
        private FlyoutStrategy strategy;
        public static readonly DependencyProperty TargetBoundsProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ContainerStyleProperty;
        public static readonly DependencyProperty SettingsProperty;
        public static readonly DependencyProperty FlyoutProperty;
        public static readonly DependencyProperty AnimationDurationProperty;
        public static readonly DependencyProperty AlwaysOnTopProperty;
        public static readonly DependencyProperty OpenOnHoverProperty;
        public static readonly DependencyProperty FollowTargetProperty;
        private FlyoutAnimatorBase animator;
        private FlyoutSettingsBase defaultSettings;
        private bool isInitialized;
        private WeakReference subscribedForm;
        private WeakReference subscribedRoot;
        private WeakReference subscribedWindow;
        private Size oldSize = Size.Empty;
        private Size newSize = Size.Empty;
        private Point? location;

        public event EventHandler Closed;

        public event CancelEventHandler Closing;

        public event EventHandler Opened;

        public event CancelEventHandler Opening;

        public event EventHandler PopupLoaded;

        static FlyoutBase()
        {
            System.Type ownerType = typeof(FlyoutBase);
            HorizontalOffsetProperty = DependencyPropertyManager.Register("HorizontalOffset", typeof(double), ownerType, new PropertyMetadata((d, e) => ((FlyoutBase) d).HorizontalOffsetChanged()));
            VerticalOffsetProperty = DependencyPropertyManager.Register("VerticalOffset", typeof(double), ownerType, new PropertyMetadata((d, e) => ((FlyoutBase) d).VerticalOffsetChanged()));
            IsOpenProperty = DependencyPropertyManager.Register("IsOpen", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((FlyoutBase) d).IsOpenChanged((bool) e.NewValue)));
            ActualIsOpenProperty = DependencyPropertyManager.Register("ActualIsOpen", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((FlyoutBase) d).ActualIsOpenChanged((bool) e.NewValue)));
            ShouldBeOpenedProperty = DependencyPropertyManager.Register("ShouldBeOpened", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((FlyoutBase) d).ShouldBeOpenedChanged((bool) e.NewValue)));
            PlacementTargetProperty = DependencyPropertyManager.Register("PlacementTarget", typeof(UIElement), ownerType, new PropertyMetadata((d, e) => ((FlyoutBase) d).PlacementTargetChanged(e)));
            StaysOpenProperty = DependencyPropertyManager.Register("StaysOpen", typeof(bool), ownerType, new PropertyMetadata(true));
            CloseOnInactiveWindowMouseLeaveProperty = DependencyPropertyManager.Register("CloseOnInactiveWindowMouseLeave", typeof(bool), ownerType, new PropertyMetadata(false));
            AllowOutOfScreenProperty = DependencyPropertyManager.Register("AllowOutOfScreen", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((FlyoutBase) d).AllowOutOfScreenChanged(e)));
            IndicatorDirectionProperty = DependencyPropertyManager.Register("IndicatorDirection", typeof(DevExpress.Xpf.Editors.Flyout.Native.IndicatorDirection), ownerType, new PropertyMetadata(DevExpress.Xpf.Editors.Flyout.Native.IndicatorDirection.None));
            ContainerStyleProperty = DependencyPropertyManager.Register("ContainerStyle", typeof(Style), ownerType);
            SettingsProperty = DependencyPropertyManager.Register("Settings", typeof(FlyoutSettingsBase), ownerType, new PropertyMetadata(null));
            TargetBoundsProperty = DependencyPropertyManager.Register("TargetBounds", typeof(Rect), ownerType, new PropertyMetadata(Rect.Empty, (d, e) => ((FlyoutBase) d).TargetBoundsChanged(e)));
            FlyoutProperty = DependencyProperty.RegisterAttached("Flyout", typeof(FlyoutBase), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
            AnimationDurationProperty = DependencyProperty.Register("AnimationDuration", typeof(Duration), ownerType, new FrameworkPropertyMetadata(new Duration(TimeSpan.FromMilliseconds(500.0))));
            AlwaysOnTopProperty = DependencyProperty.Register("AlwaysOnTop", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            OpenOnHoverProperty = DependencyProperty.Register("OpenOnHover", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            AllowMoveAnimationProperty = DependencyProperty.Register("AllowMoveAnimation", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            UIElement.VisibilityProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(Visibility.Collapsed, null, (CoerceValueCallback) ((d, value) => ((FlyoutBase) d).CoerceVisibility(value))));
            FollowTargetProperty = DependencyProperty.Register("FollowTarget", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((FlyoutBase) d).OnFollowTargetChanged(e)));
        }

        public FlyoutBase()
        {
            this.OpenCommand = this.CreateOpenCommand();
            this.CloseCommand = this.CreateCloseCommand();
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.Unloaded += new RoutedEventHandler(this.OnUnloaded);
            SetFlyout(this, this);
            this.UpdateAction = new PostponedAction(() => !base.IsLoaded);
        }

        protected virtual void ActualIsOpenChanged(bool newValue)
        {
            Action<DelegateCommand<object>> action = <>c.<>9__305_0;
            if (<>c.<>9__305_0 == null)
            {
                Action<DelegateCommand<object>> local1 = <>c.<>9__305_0;
                action = <>c.<>9__305_0 = x => x.RaiseCanExecuteChanged();
            }
            (this.CloseCommand as DelegateCommand<object>).Do<DelegateCommand<object>>(action);
            Action<DelegateCommand<object>> action2 = <>c.<>9__305_1;
            if (<>c.<>9__305_1 == null)
            {
                Action<DelegateCommand<object>> local2 = <>c.<>9__305_1;
                action2 = <>c.<>9__305_1 = x => x.RaiseCanExecuteChanged();
            }
            (this.OpenCommand as DelegateCommand<object>).Do<DelegateCommand<object>>(action2);
        }

        protected virtual void AllowOutOfScreenChanged(DependencyPropertyChangedEventArgs e)
        {
            this.InvalidateLocation();
        }

        private void Animate(Storyboard storyboard, Action completeAction = null, bool canSkipAnimation = false)
        {
            if (storyboard != null)
            {
                bool flag = canSkipAnimation && this.AnimationInProgress;
                if (this.AnimationInProgress)
                {
                    this.CurrentAnimation.SkipToFill(this);
                }
                this.CurrentAnimation = storyboard;
                storyboard.Completed += delegate (object d, EventArgs e) {
                    Action<Action> action = <>c.<>9__325_1;
                    if (<>c.<>9__325_1 == null)
                    {
                        Action<Action> local1 = <>c.<>9__325_1;
                        action = <>c.<>9__325_1 = x => x();
                    }
                    completeAction.Do<Action>(action);
                    if (ReferenceEquals(storyboard, this.CurrentAnimation))
                    {
                        this.CurrentAnimation = null;
                    }
                };
                storyboard.Begin(this, true);
                if (flag)
                {
                    storyboard.SkipToFill(this);
                }
            }
        }

        protected virtual void AnimateMove(Point from, Point to)
        {
            if (this.ActualIsOpen && (this.IsValid(from) && (this.IsValid(to) && ((from != to) && !this.LockMoveAnimationOnOpenAnimation))))
            {
                Storyboard storyboard = this.Animator.GetMoveAnimation(this, from, to);
                this.Animate(storyboard, null, false);
            }
        }

        protected void CalcPopupBounds()
        {
            Rect targetBounds = this.GetTargetBounds();
            targetBounds.Offset(this.HorizontalOffset, this.VerticalOffset);
            Rect rect = TranslateHelper.ToScreen(this.PlacementTarget, targetBounds);
            this.PositionCalculator.OriginScreenRect = this.PositionCalculator.GetScreenRect(rect.Center(), this.HideInTaskBar);
            this.PositionCalculator.ScreenRect = new Rect(this.PositionCalculator.OriginScreenRect.Location, this.GetTransformedRect(this.PositionCalculator.OriginScreenRect, true).Size);
            this.PositionCalculator.NormalizedScreenRect = new Rect(new Point(0.0, 0.0), this.PositionCalculator.ScreenRect.Size);
            rect = this.GetTransformedRect(this.GetNormalizedRect(rect), true);
            this.PositionCalculator.TargetBounds = rect;
            this.ActualSettings.Apply(this.PositionCalculator, this);
            this.PreparePopupSize();
            this.PositionCalculator.Initialize(base.VerticalAlignment, this.WrapForRTL(base.HorizontalAlignment), this.AllowOutOfScreen);
            if (this.IsPopupVisible)
            {
                this.PositionCalculator.PopupDesiredSize = this.MeasurePopup();
                Func<Size> fallback = <>c.<>9__255_1;
                if (<>c.<>9__255_1 == null)
                {
                    Func<Size> local1 = <>c.<>9__255_1;
                    fallback = <>c.<>9__255_1 = () => new Size();
                }
                this.PositionCalculator.IndicatorSize = this.GetIndicator(this.IndicatorDirection).Return<FrameworkElement, Size>(delegate (FrameworkElement x) {
                    if (!x.IsMeasureValid)
                    {
                        x.Measure(new Size(1000.0, 1000.0));
                    }
                    return DpiAwareHelper.SizeToMonitor(this.GetElement(x), x.DesiredSize);
                }, fallback);
                this.PositionCalculator.CalcLocation();
                this.IndicatorDirection = this.ActualSettings.GetIndicatorDirection(this.PositionCalculator.Result.Placement);
            }
        }

        protected virtual bool CanExecuteCloseCommand(object parameter) => 
            this.IsOpen;

        protected virtual bool CanExecuteOpenCommand(object parameter) => 
            !this.IsOpen;

        private void CheckMouseOver()
        {
            if (this.OpenOnHover && this.IsOpen)
            {
                Point transformedPoint = this.GetTransformedPoint(this.NormalizeToScreenOrigin(this.SubscribedRoot.PointToScreen(Mouse.GetPosition(this.SubscribedRoot))), true);
                if (!this.PositionCalculator.TargetBounds.Contains(transformedPoint + new Vector(this.HorizontalOffset, this.VerticalOffset)) && !this.PositionCalculator.Result.Bounds.Contains(transformedPoint))
                {
                    this.SetIsOpen(false, true);
                }
            }
        }

        private bool CheckWindowState(WindowState state)
        {
            Window subscribedRoot = this.SubscribedRoot as Window;
            return ((subscribedRoot != null) && (subscribedRoot.WindowState == state));
        }

        protected virtual void ChildChanged()
        {
        }

        protected virtual void ClosePopup()
        {
            if (this.FlyoutContainer != null)
            {
                Storyboard closeAnimation = this.Animator.GetCloseAnimation(this);
                this.Animate(closeAnimation, delegate {
                    if (!this.ShouldBeOpened)
                    {
                        Action<DevExpress.Xpf.Editors.Flyout.Native.FlyoutContainer> action = <>c.<>9__324_1;
                        if (<>c.<>9__324_1 == null)
                        {
                            Action<DevExpress.Xpf.Editors.Flyout.Native.FlyoutContainer> local1 = <>c.<>9__324_1;
                            action = <>c.<>9__324_1 = x => x.IsOpen = false;
                        }
                        this.FlyoutContainer.Do<DevExpress.Xpf.Editors.Flyout.Native.FlyoutContainer>(action);
                    }
                }, true);
            }
        }

        protected Visibility CoerceVisibility(object value) => 
            Visibility.Collapsed;

        protected virtual Rect CorrectPostionByRect(Rect bounds, Rect restrictRect)
        {
            double x = FlyoutPositionCalculator.RestrictByRange(bounds.Right, restrictRect.Left, restrictRect.Right);
            return new Rect(new Point(FlyoutPositionCalculator.RestrictByRange(bounds.Left, restrictRect.Left, restrictRect.Right), FlyoutPositionCalculator.RestrictByRange(bounds.Top, restrictRect.Top, restrictRect.Bottom)), new Point(x, FlyoutPositionCalculator.RestrictByRange(bounds.Bottom, restrictRect.Top, restrictRect.Bottom)));
        }

        protected virtual FlyoutAnimatorBase CreateAnimator() => 
            new FlyoutAnimator();

        protected virtual ICommand CreateCloseCommand() => 
            DelegateCommandFactory.Create<object>(delegate (object <obj>) {
                this.SetIsOpen(false, true);
            }, new Func<object, bool>(this.CanExecuteCloseCommand), false);

        protected virtual FlyoutSettings CreateDefaultSettings() => 
            new FlyoutSettings();

        private void CreateHandlers()
        {
            // Unresolved stack state at '000003D2'
        }

        protected virtual ICommand CreateOpenCommand() => 
            DelegateCommandFactory.Create<object>(delegate (object <obj>) {
                this.SetIsOpen(true, true);
            }, new Func<object, bool>(this.CanExecuteOpenCommand), false);

        protected virtual FrameworkElement GetChildContainer() => 
            (FrameworkElement) base.GetTemplateChild("PART_Container");

        private FrameworkElement GetElement(FrameworkElement element)
        {
            if (element.IsInVisualTree())
            {
                return element;
            }
            Func<FrameworkElement, bool> evaluator = <>c.<>9__251_0;
            if (<>c.<>9__251_0 == null)
            {
                Func<FrameworkElement, bool> local1 = <>c.<>9__251_0;
                evaluator = <>c.<>9__251_0 = x => x.IsInVisualTree();
            }
            Func<FrameworkElement, FrameworkElement> func2 = <>c.<>9__251_1;
            if (<>c.<>9__251_1 == null)
            {
                Func<FrameworkElement, FrameworkElement> local2 = <>c.<>9__251_1;
                func2 = <>c.<>9__251_1 = x => x;
            }
            return (this.PlacementTarget as FrameworkElement).If<FrameworkElement>(evaluator).Return<FrameworkElement, FrameworkElement>(func2, () => this);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static FlyoutBase GetFlyout(DependencyObject d) => 
            (FlyoutBase) d.GetValue(FlyoutProperty);

        protected virtual DevExpress.Xpf.Editors.Flyout.Native.FlyoutContainer GetFlyoutContainer()
        {
            this.Popup = this.GetPopup();
            if (this.Popup == null)
            {
                return null;
            }
            PopupFlyoutContainer container1 = new PopupFlyoutContainer();
            container1.Popup = this.Popup;
            return container1;
        }

        public FrameworkElement GetIndicator(DevExpress.Xpf.Editors.Flyout.Native.IndicatorDirection indicatorDirection) => 
            (indicatorDirection != DevExpress.Xpf.Editors.Flyout.Native.IndicatorDirection.None) ? LayoutHelper.FindElementByName(this.RenderGrid, indicatorDirection.ToString() + "Indicator") : null;

        private Point GetLocation()
        {
            if (this.PositionCalculator.Result.State != CalculationState.Finished)
            {
                throw new Exception();
            }
            Rect rect = new Rect(new Point(this.PositionCalculator.Result.Location.X + this.PositionCalculator.Result.Size.Width, this.PositionCalculator.Result.Location.Y), new Point(this.PositionCalculator.Result.Location.X, this.PositionCalculator.Result.Location.Y + this.PositionCalculator.Result.Size.Height));
            return DpiAwareHelper.PointToControl((base.FlowDirection == System.Windows.FlowDirection.RightToLeft) ? rect.TopRight : rect.TopLeft, this.ScreenRect.Center());
        }

        private MatrixTransform GetMatrixTransform(bool invert)
        {
            if (this.SubscribedRoot != null)
            {
                MatrixTransform transform2 = base.TransformToVisual(this.SubscribedRoot) as MatrixTransform;
                Matrix identity = Matrix.Identity;
                if (transform2 != null)
                {
                    identity = transform2.Matrix;
                }
                if (!identity.IsIdentity)
                {
                    if (invert)
                    {
                        identity.Invert();
                    }
                    return new MatrixTransform(Math.Abs(identity.M11), identity.M12, identity.M21, Math.Abs(identity.M22), 0.0, 0.0);
                }
            }
            return null;
        }

        protected virtual FrameworkElement GetMeasureElement() => 
            this.Strategy.GetMeasureElement(this);

        private Size GetMeasureSize() => 
            this.AllowOutOfScreen ? new Size(double.PositiveInfinity, double.PositiveInfinity) : this.ScreenRect.Size;

        protected internal Rect GetNormalizedRect(Rect rect) => 
            new Rect(this.NormalizeToScreenOrigin(rect.Location), rect.Size);

        protected virtual Point GetOpenAnimationOffset()
        {
            if (this.IsUpdateRequired)
            {
                this.UpdatePopupPlacement();
            }
            return this.Strategy.GetOpenAnimationOffset(this);
        }

        protected virtual System.Windows.Controls.Primitives.Popup GetPopup() => 
            base.GetTemplateChild("PART_Popup") as System.Windows.Controls.Primitives.Popup;

        protected virtual FrameworkElement GetRenderGrid() => 
            (FrameworkElement) base.GetTemplateChild("PART_RenderGrid");

        public virtual Rect GetTargetBounds() => 
            !this.TargetBounds.IsEmpty ? this.TargetBounds : this.GetTargetBounds(this.PlacementTarget, this.PlacementTarget, () => this.Strategy.GetDefaultTargetBounds(this));

        public virtual Rect GetTargetBounds(UIElement baseElement, UIElement element, Func<Rect> GetDefaultTargetBounds) => 
            ((element == null) || (baseElement == null)) ? GetDefaultTargetBounds() : this.GetVisibleRect(baseElement, element);

        protected internal Point GetTransformedPoint(Point point, bool inverted)
        {
            Point point2 = point;
            MatrixTransform matrixTransform = this.GetMatrixTransform(inverted);
            if ((matrixTransform != null) && !matrixTransform.Matrix.IsIdentity)
            {
                point2 = matrixTransform.Transform(point2);
            }
            return point2;
        }

        protected internal Rect GetTransformedRect(Rect rect, bool inverted)
        {
            Rect rect2 = rect;
            MatrixTransform matrixTransform = this.GetMatrixTransform(inverted);
            if ((matrixTransform != null) && !matrixTransform.Matrix.IsIdentity)
            {
                rect2 = matrixTransform.TransformBounds(rect2);
            }
            return rect2;
        }

        protected virtual Rect GetVisibleRect(UIElement baseElement, UIElement element)
        {
            Rect bounds = TranslateHelper.TranslateBounds(baseElement, element);
            FrameworkElement topLevelVisual = LayoutHelper.GetTopLevelVisual(baseElement);
            return ((topLevelVisual == null) ? bounds : this.CorrectPostionByRect(bounds, TranslateHelper.TranslateBounds(baseElement, topLevelVisual)));
        }

        protected virtual void HideOnWindowDeactivated()
        {
            this.UpdateShouldBeOpened(true);
        }

        protected virtual void HorizontalOffsetChanged()
        {
            this.InvalidateLocation();
        }

        private void InitializeSettings()
        {
            this.positionCalculator = null;
            this.strategy = null;
            this.animator = null;
            this.InvalidateLocation();
        }

        public void InvalidateLocation()
        {
            if (!this.IsUpdateRequired)
            {
                this.IsUpdateRequired = true;
                this.UpdateAction.PerformPostpone(() => base.Dispatcher.BeginInvoke(() => this.UpdateInternal(), DispatcherPriority.Input, new object[0]));
            }
        }

        protected bool IsConnectedToPresentationSource(UIElement element) => 
            PresentationSource.FromVisual(element) != null;

        protected virtual void IsOpenChanged(bool newValue)
        {
            this.UpdateShouldBeOpened(false);
        }

        protected bool IsValid(Point p) => 
            p.X.IsNumber() && p.Y.IsNumber();

        protected override Size MeasureOverride(Size constraint) => 
            new Size();

        private Size MeasurePopup()
        {
            if (!this.IsPopupVisible)
            {
                return Size.Empty;
            }
            FrameworkElement measureElement = this.GetMeasureElement();
            Size measureSize = this.GetMeasureSize();
            if (!measureElement.IsMeasureValid)
            {
                measureElement.Measure(measureSize);
            }
            return DpiAwareHelper.SizeToMonitor(this.GetElement(measureElement), measureElement.DesiredSize);
        }

        protected virtual Point NormalizeToScreenOrigin(Point point)
        {
            Rect screenRect = this.ScreenRect;
            point.Offset(-screenRect.Left, -screenRect.Top);
            return point;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.FlyoutContainer != null)
            {
                this.FlyoutContainer.Element.KeyDown -= new System.Windows.Input.KeyEventHandler(this.RootKeyDown);
                this.FlyoutContainer.Element.MouseDown -= new MouseButtonEventHandler(this.RootMouseDown);
                this.FlyoutContainer.Closed -= new EventHandler(this.OnPopupClosed);
                this.FlyoutContainer.Opened -= new EventHandler(this.OnPopupOpened);
            }
            this.FlyoutContainer = this.GetFlyoutContainer();
            if (this.FlyoutContainer != null)
            {
                this.FlyoutContainer.Element.KeyDown += new System.Windows.Input.KeyEventHandler(this.RootKeyDown);
                this.FlyoutContainer.Element.MouseDown += new MouseButtonEventHandler(this.RootMouseDown);
                this.FlyoutContainer.Closed += new EventHandler(this.OnPopupClosed);
                this.FlyoutContainer.Opened += new EventHandler(this.OnPopupOpened);
            }
            if (this.ChildContainer != null)
            {
                this.ChildContainer.Loaded -= new RoutedEventHandler(this.OnPopupChildContainerLoaded);
                this.ChildContainer.Unloaded -= new RoutedEventHandler(this.OnPopupChildContainerUnloaded);
                this.ChildContainer.LostFocus -= new RoutedEventHandler(this.OnPopupChildContainerLostFocus);
                this.ChildContainer.GotFocus -= new RoutedEventHandler(this.OnPopupChildContainerGotFocus);
            }
            this.ChildContainer = this.GetChildContainer();
            if (this.ChildContainer != null)
            {
                this.ChildContainer.Loaded += new RoutedEventHandler(this.OnPopupChildContainerLoaded);
                this.ChildContainer.Unloaded += new RoutedEventHandler(this.OnPopupChildContainerUnloaded);
                this.ChildContainer.LostFocus += new RoutedEventHandler(this.OnPopupChildContainerLostFocus);
                this.ChildContainer.GotFocus += new RoutedEventHandler(this.OnPopupChildContainerGotFocus);
            }
            if (this.RenderGrid != null)
            {
                this.RenderGrid.SizeChanged -= new SizeChangedEventHandler(this.RenderGrid_SizeChanged);
            }
            this.RenderGrid = this.GetRenderGrid();
            if (this.RenderGrid != null)
            {
                this.RenderGrid.SizeChanged += new SizeChangedEventHandler(this.RenderGrid_SizeChanged);
            }
            this.IsInitializedInternal = true;
        }

        protected virtual void OnClosed(EventArgs e)
        {
            this.location = null;
            this.ActualIsOpen = false;
        }

        protected void OnFollowTargetChanged(DependencyPropertyChangedEventArgs e)
        {
            if (Equals(e.NewValue, true))
            {
                this.InvalidateLocation();
            }
        }

        private void OnInitialized()
        {
            this.InitializeSettings();
            this.OpenPopup();
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.ApplyTemplate();
            ThemeManager.AddThemeChangedHandler(this, new ThemeChangedRoutedEventHandler(this.ThemeChanged));
            this.SubscribeWindowCore();
            this.SubscribeSettings(this.ActualSettings);
            this.UpdateAction.Perform();
        }

        protected virtual void OnOpened(EventArgs e)
        {
            this.ActualIsOpen = true;
            if (this.AllowRecreateContent)
            {
                this.RecreateContent();
            }
        }

        protected virtual void OnPopupChildContainerGotFocus(object sender, RoutedEventArgs e)
        {
        }

        private void OnPopupChildContainerLoaded(object sender, RoutedEventArgs e)
        {
            this.IsLoadedInternal = true;
            this.PopupLoaded.Do<EventHandler>(x => x(sender, new EventArgs()));
        }

        protected virtual void OnPopupChildContainerLostFocus(object sender, RoutedEventArgs e)
        {
        }

        private void OnPopupChildContainerUnloaded(object sender, RoutedEventArgs e)
        {
            this.IsLoadedInternal = false;
        }

        protected virtual void OnPopupClosed(object sender, object e)
        {
            this.OnClosed(EventArgs.Empty);
            this.RaiseClosed();
        }

        protected virtual void OnPopupOpened(object sender, object e)
        {
            this.OnOpened(EventArgs.Empty);
            this.RaiseOpened();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ReferenceEquals(e.Property, FrameworkElement.HorizontalAlignmentProperty) || ReferenceEquals(e.Property, FrameworkElement.VerticalAlignmentProperty))
            {
                this.InvalidateLocation();
            }
            if (ReferenceEquals(e.Property, SettingsProperty) && base.IsLoaded)
            {
                this.UnsubscribeSettings(e.OldValue as FlyoutSettingsBase);
                this.SubscribeSettings(e.NewValue as FlyoutSettingsBase);
                this.InvalidateLocation();
            }
        }

        protected virtual void OnUnloaded(object sender, RoutedEventArgs e)
        {
            ThemeManager.RemoveThemeChangedHandler(this, new ThemeChangedRoutedEventHandler(this.ThemeChanged));
            this.UnsubscribeWindowCore();
            this.UnsubscribeSettings(this.ActualSettings);
            this.SetIsOpen(false, true);
            this.ActualIsOpen = false;
        }

        protected virtual void OpenPopup()
        {
            if (this.ShouldBeOpened)
            {
                this.DoOpenPopup = true;
                this.InvalidateLocation();
            }
        }

        protected virtual void PlacementTargetChanged(DependencyPropertyChangedEventArgs e)
        {
            this.LockMoveAnimationOnOpenAnimation = false;
            this.InvalidateLocation();
            (e.OldValue as FrameworkElement).Do<FrameworkElement>(delegate (FrameworkElement x) {
                x.LayoutUpdated -= new EventHandler(this.TargetLayoutUpdated);
                x.MouseEnter -= new System.Windows.Input.MouseEventHandler(this.TargetMouseEnter);
                x.MouseMove -= new System.Windows.Input.MouseEventHandler(this.TargetMouseMove);
                x.MouseLeave -= new System.Windows.Input.MouseEventHandler(this.TargetMouseLeave);
            });
            (e.NewValue as FrameworkElement).Do<FrameworkElement>(delegate (FrameworkElement x) {
                x.LayoutUpdated += new EventHandler(this.TargetLayoutUpdated);
                x.MouseEnter += new System.Windows.Input.MouseEventHandler(this.TargetMouseEnter);
                x.MouseMove += new System.Windows.Input.MouseEventHandler(this.TargetMouseMove);
                x.MouseLeave += new System.Windows.Input.MouseEventHandler(this.TargetMouseLeave);
            });
        }

        private void PopupOpened()
        {
            Point openAnimationOffset = this.GetOpenAnimationOffset();
            if (base.FlowDirection == System.Windows.FlowDirection.RightToLeft)
            {
                openAnimationOffset = new Point(-openAnimationOffset.X, openAnimationOffset.Y);
            }
            if (this.IsValid(openAnimationOffset))
            {
                Storyboard openAnimation = this.Animator.GetOpenAnimation(this, openAnimationOffset);
                this.LockMoveAnimationOnOpenAnimation = true;
                this.Animate(openAnimation, () => this.LockMoveAnimationOnOpenAnimation = false, false);
            }
        }

        private void PreparePopupSize()
        {
            if (this.RenderGrid != null)
            {
                Point monitorPoint = this.PositionCalculator.ScreenRect.Center();
                Size size = this.PositionCalculator.ScreenRect.Size;
                if (base.VerticalAlignment != VerticalAlignment.Stretch)
                {
                    this.RenderGrid.ClearValue(FrameworkElement.HeightProperty);
                    if (!this.AllowOutOfScreen && !this.IsPropertySet(FrameworkElement.MaxHeightProperty))
                    {
                        this.RenderGrid.MaxHeight = DpiAwareHelper.SizeToControl(size, monitorPoint).Height;
                    }
                    else
                    {
                        this.RenderGrid.ClearValue(FrameworkElement.MaxHeightProperty);
                    }
                }
                if (base.HorizontalAlignment != System.Windows.HorizontalAlignment.Stretch)
                {
                    this.RenderGrid.ClearValue(FrameworkElement.WidthProperty);
                    if (!this.AllowOutOfScreen && !this.IsPropertySet(FrameworkElement.MaxWidthProperty))
                    {
                        this.RenderGrid.MaxWidth = DpiAwareHelper.SizeToControl(size, monitorPoint).Width;
                    }
                    else
                    {
                        this.RenderGrid.ClearValue(FrameworkElement.MaxWidthProperty);
                    }
                }
            }
        }

        protected void ProcessCloseOnPointerPressed()
        {
            if (!this.StaysOpen && !this.DoOpenPopup)
            {
                this.SetIsOpen(false, true);
            }
        }

        protected virtual void ProcessKeyDown(Key key)
        {
            if ((key == Key.Escape) && !this.StaysOpen)
            {
                this.SetIsOpen(false, true);
            }
        }

        protected virtual void RaiseClosed()
        {
            if (this.Closed != null)
            {
                this.Closed(this, EventArgs.Empty);
            }
        }

        protected virtual bool RaiseIsOpeningChanged(bool isOpen)
        {
            CancelEventHandler handler1 = isOpen ? this.Opening : this.Closing;
            if (<>c.<>9__240_1 == null)
            {
                CancelEventHandler local1 = isOpen ? this.Opening : this.Closing;
                handler1 = (CancelEventHandler) (<>c.<>9__240_1 = () => false);
            }
            return ((CancelEventHandler) <>c.<>9__240_1).Return<CancelEventHandler, bool>(delegate (CancelEventHandler x) {
                CancelEventArgs e = new CancelEventArgs();
                x(this, e);
                return e.Cancel;
            }, ((Func<bool>) handler1));
        }

        protected virtual void RaiseOpened()
        {
            if (this.Opened != null)
            {
                this.Opened(this, EventArgs.Empty);
            }
        }

        protected virtual void RecreateContent()
        {
            Style containerStyle = this.ContainerStyle;
            this.ContainerStyle = null;
            this.ContainerStyle = containerStyle;
            DataTemplate contentTemplate = base.ContentTemplate;
            base.ContentTemplate = null;
            base.ContentTemplate = contentTemplate;
        }

        protected virtual void RenderGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.oldSize.IsEmpty || (this.newSize.IsEmpty || ((this.oldSize != e.NewSize) || (this.newSize != e.PreviousSize))))
            {
                this.oldSize = e.PreviousSize;
                this.newSize = e.NewSize;
                this.InvalidateLocation();
            }
        }

        protected virtual void RootKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.ProcessKeyDown(e.Key);
        }

        protected virtual void RootMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.PopupChild != null)
            {
                DependencyObject originalSource = e.OriginalSource as DependencyObject;
                if ((originalSource != null) && !ReferenceEquals(LayoutHelper.FindLayoutOrVisualParentObject(originalSource, element => ReferenceEquals(element, this), true, this.GetParent()), this))
                {
                    this.ProcessCloseOnPointerPressed();
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void SetFlyout(DependencyObject d, FlyoutBase flyout)
        {
            d.SetValue(FlyoutProperty, flyout);
        }

        private void SetIsOpen(bool value, bool raiseEvent = true)
        {
            if ((value != this.IsOpen) && (!raiseEvent || !this.RaiseIsOpeningChanged(value)))
            {
                base.SetCurrentValue(IsOpenProperty, value);
            }
        }

        private void SettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.ActualSettings.Do<FlyoutSettingsBase>(x => x.OnPropertyChanged(this, e));
        }

        protected virtual void ShouldBeOpenedChanged(bool newValue)
        {
            if (newValue)
            {
                this.OpenPopup();
            }
            else
            {
                this.ClosePopup();
            }
        }

        protected virtual void ShowOnWindowActivated()
        {
            this.UpdateShouldBeOpened(true);
        }

        private void SubscribeSettings(FlyoutSettingsBase settings)
        {
            settings.Do<FlyoutSettingsBase>(delegate (FlyoutSettingsBase x) {
                x.PropertyChanged += new PropertyChangedEventHandler(this.SettingsPropertyChanged);
                this.InitializeSettings();
            });
        }

        protected virtual void SubscribeWindowCore()
        {
            this.subscribedRoot = new WeakReference(LayoutHelper.GetTopLevelVisual(this));
            if (this.SubscribedRoot != null)
            {
                if (this.ActivatedHandler == null)
                {
                    this.CreateHandlers();
                }
                this.SubscribedRoot.AddHandler(UIElement.MouseDownEvent, this.RootMouseDownHandler.Handler, true);
                this.SubscribedRoot.KeyDown += this.RootKeyDownHandler.Handler;
                Window subscribedRoot = this.SubscribedRoot as Window;
                Window target = subscribedRoot;
                if (subscribedRoot == null)
                {
                    Window local1 = subscribedRoot;
                    target = Window.GetWindow(this);
                }
                this.subscribedWindow = new WeakReference(target);
                if (this.SubscribedWindow != null)
                {
                    this.SubscribedWindow.StateChanged += this.WindowStateChangedHandler.Handler;
                    this.SubscribedWindow.LocationChanged += this.WindowLocationChangedHandler.Handler;
                    this.SubscribedWindow.SizeChanged += this.WindowSizeChangedHandler.Handler;
                    this.SubscribedWindow.Activated += this.ActivatedHandler.Handler;
                    this.SubscribedWindow.Deactivated += this.DeactivatedHandler.Handler;
                    this.SubscribedWindow.MouseLeave += this.WindowMouseLeaveHandler.Handler;
                    this.SubscribedWindow.MouseMove += this.WindowMouseMoveHandler.Handler;
                }
                else
                {
                    HwndSource source = (HwndSource) PresentationSource.FromDependencyObject(this);
                    if (source != null)
                    {
                        System.Windows.Forms.Control control = System.Windows.Forms.Control.FromChildHandle(source.Handle);
                        if (control != null)
                        {
                            this.subscribedForm = new WeakReference(control.TopLevelControl);
                            if (this.SubscribedForm != null)
                            {
                                this.SubscribedForm.LocationChanged += this.WindowLocationChangedHandler.Handler;
                                this.SubscribedForm.SizeChanged += this.FormSizeChangedHandler.Handler;
                                this.SubscribedForm.Activated += this.ActivatedHandler.Handler;
                                this.SubscribedForm.Deactivate += this.DeactivatedHandler.Handler;
                            }
                        }
                    }
                }
            }
        }

        protected virtual void TargetBoundsChanged(DependencyPropertyChangedEventArgs e)
        {
            this.InvalidateLocation();
        }

        private void TargetLayoutUpdated(object sender, EventArgs e)
        {
            if (this.FollowTarget)
            {
                this.InvalidateLocation();
            }
        }

        private void TargetMouseEnter(object sender, EventArgs e)
        {
            if (this.OpenOnHover)
            {
                this.SetIsOpen(true, true);
            }
        }

        private void TargetMouseLeave(object sender, EventArgs e)
        {
            if (this.OpenOnHover && this.IsOpen)
            {
                this.InvalidateLocation();
            }
        }

        private void TargetMouseMove(object sender, EventArgs e)
        {
            if (this.OpenOnHover)
            {
                this.SetIsOpen(true, true);
            }
        }

        protected virtual void ThemeChanged(DependencyObject sender, ThemeChangedRoutedEventArgs e)
        {
            base.ApplyTemplate();
        }

        private void UnsubscribeSettings(FlyoutSettingsBase settings)
        {
            settings.Do<FlyoutSettingsBase>(delegate (FlyoutSettingsBase x) {
                x.PropertyChanged -= new PropertyChangedEventHandler(this.SettingsPropertyChanged);
            });
        }

        protected virtual void UnsubscribeWindowCore()
        {
            if (this.SubscribedRoot != null)
            {
                this.SubscribedRoot.RemoveHandler(UIElement.MouseDownEvent, this.RootMouseDownHandler.Handler);
                this.SubscribedRoot.KeyDown -= this.RootKeyDownHandler.Handler;
                this.subscribedRoot = null;
            }
            if (this.SubscribedWindow != null)
            {
                this.SubscribedWindow.StateChanged -= this.WindowStateChangedHandler.Handler;
                this.SubscribedWindow.LocationChanged -= this.WindowLocationChangedHandler.Handler;
                this.SubscribedWindow.SizeChanged -= this.WindowSizeChangedHandler.Handler;
                this.SubscribedWindow.Activated -= this.ActivatedHandler.Handler;
                this.SubscribedWindow.Deactivated -= this.DeactivatedHandler.Handler;
                this.SubscribedWindow.MouseLeave -= this.WindowMouseLeaveHandler.Handler;
                this.SubscribedWindow.MouseMove -= this.WindowMouseMoveHandler.Handler;
                this.subscribedWindow = null;
            }
            if (this.SubscribedForm != null)
            {
                this.SubscribedForm.LocationChanged -= this.WindowLocationChangedHandler.Handler;
                this.SubscribedForm.SizeChanged -= this.FormSizeChangedHandler.Handler;
                this.SubscribedForm.Activated -= this.ActivatedHandler.Handler;
                this.SubscribedForm.Deactivate -= this.DeactivatedHandler.Handler;
                this.subscribedForm = null;
            }
        }

        private void UpdateIndicator()
        {
            FrameworkElement indicator = this.GetIndicator(this.IndicatorDirection);
            if (indicator != null)
            {
                Point point = DpiAwareHelper.PointToControl(this.PositionCalculator.Result.IndicatorOffset, this.PositionCalculator.ScreenRect.Center());
                TranslateTransform renderTransform = indicator.RenderTransform as TranslateTransform;
                if (renderTransform == null)
                {
                    renderTransform = new TranslateTransform();
                    indicator.RenderTransform = renderTransform;
                }
                renderTransform.X = point.X;
                renderTransform.Y = point.Y;
            }
        }

        private void UpdateInternal()
        {
            if (this.IsUpdateRequired)
            {
                if (this.DoOpenPopup && this.IsPopupVisible)
                {
                    this.FlyoutContainer.IsOpen = true;
                    this.DoOpenPopup = false;
                    this.PopupOpened();
                }
                this.UpdateLocation();
                this.IsUpdateRequired = false;
            }
        }

        public void UpdateLocation()
        {
            if (!this.IsPopupVisible)
            {
                this.location = null;
            }
            else
            {
                this.UpdatePopupPlacement();
                if (this.AllowMoveAnimation && (this.PositionCalculator.Result.State == CalculationState.Finished))
                {
                    Point location = this.GetLocation();
                    if (this.location == null)
                    {
                        this.location = new Point?(location);
                    }
                    else
                    {
                        Point point2 = this.location.Value;
                        this.location = new Point?(location);
                        double num = point2.X - location.X;
                        Point to = new Point();
                        this.AnimateMove(new Point((base.FlowDirection == System.Windows.FlowDirection.RightToLeft) ? -num : num, point2.Y - location.Y), to);
                    }
                }
            }
        }

        public virtual void UpdatePopupControls()
        {
            this.RenderGrid.Opacity = 0.0;
            Rect rect = new Rect(DpiAwareHelper.PointToControl(this.ScreenRect.Location, this.ScreenRect.Location), this.GetTransformedRect(DpiAwareHelper.RectToControl(this.ScreenRect, this.ScreenRect.Center()), false).Size);
            FrameworkElement popupRoot = this.PopupRoot;
            if ((popupRoot != null) && (popupRoot.GetType().FullName == "System.Windows.Controls.Primitives.PopupRoot"))
            {
                popupRoot.Width = rect.Width;
                popupRoot.Height = rect.Height;
                (PresentationSource.FromVisual(popupRoot) as HwndSource).If<HwndSource>(x => this.IsPropertySet(UIElement.IsHitTestVisibleProperty)).Do<HwndSource>(delegate (HwndSource x) {
                    if (base.IsHitTestVisible)
                    {
                        FlayoutNativeHelper.UnsetPopupTransparent(x.Handle);
                    }
                    else
                    {
                        FlayoutNativeHelper.SetPopupTransparent(x.Handle);
                    }
                });
            }
            this.FlyoutContainer.VerticalOffset = rect.Top;
            this.FlyoutContainer.HorizontalOffset = rect.Left;
        }

        protected virtual void UpdatePopupLocation(Point location)
        {
            if (this.IsPopupVisible && this.IsValid(location))
            {
                Point point = location;
                if (base.FlowDirection == System.Windows.FlowDirection.RightToLeft)
                {
                    point = new Point(DpiAwareHelper.RectToControl(this.ScreenRect, this.ScreenRect.Center()).Size.Width - location.X, location.Y);
                }
                FrameworkElement templateChild = (FrameworkElement) base.GetTemplateChild("PART_cc");
                if (templateChild != null)
                {
                    Canvas.SetLeft(templateChild, point.X);
                    Canvas.SetTop(templateChild, point.Y);
                }
            }
        }

        public virtual void UpdatePopupPlacement()
        {
            if (this.IsPopupVisible)
            {
                if ((this.PlacementTarget != null) && !this.IsConnectedToPresentationSource(this.PlacementTarget))
                {
                    this.SetIsOpen(false, true);
                }
                else
                {
                    this.CalcPopupBounds();
                    if (this.PositionCalculator.Result.State == CalculationState.Finished)
                    {
                        this.CheckMouseOver();
                        this.UpdatePopupControls();
                        this.UpdateIndicator();
                        this.UpdatePopupLocation(this.GetLocation());
                        this.UpdatePopupSize(this.PositionCalculator.Result.Size);
                    }
                }
            }
        }

        protected virtual void UpdatePopupSize(Size size)
        {
            this.Strategy.UpdatePopupSize(this, size);
        }

        private void UpdateShouldBeOpened(bool skipAssign)
        {
            if (this.AlwaysOnTop || (this.SubscribedWindow == null))
            {
                if (!skipAssign)
                {
                    this.ShouldBeOpened = this.IsOpen;
                }
            }
            else if (this.SubscribedWindow.IsActive && (this.SubscribedWindow.WindowState != WindowState.Minimized))
            {
                this.ShouldBeOpened = this.IsOpen;
            }
            else
            {
                this.ShouldBeOpened = false;
            }
        }

        protected virtual void VerticalOffsetChanged()
        {
            this.InvalidateLocation();
        }

        protected virtual void WindowLocationChanged(object sender, EventArgs e)
        {
            this.InvalidateLocation();
        }

        protected virtual void WindowMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (this.CloseOnInactiveWindowMouseLeave && ((this.SubscribedWindow != null) && !this.SubscribedWindow.IsActive))
            {
                this.SetIsOpen(false, true);
            }
        }

        protected virtual void WindowMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (this.OpenOnHover && this.IsOpen)
            {
                this.InvalidateLocation();
            }
        }

        private void WindowSizeChanged(object sender, EventArgs e)
        {
            this.InvalidateLocation();
        }

        private void WindowStateChanged(object sender, EventArgs e)
        {
            this.UpdateShouldBeOpened(true);
        }

        protected internal FlyoutPlacement WrapForRTL(FlyoutPlacement placement)
        {
            if (base.FlowDirection == System.Windows.FlowDirection.RightToLeft)
            {
                if (placement == FlyoutPlacement.Left)
                {
                    return FlyoutPlacement.Right;
                }
                if (placement == FlyoutPlacement.Right)
                {
                    return FlyoutPlacement.Left;
                }
            }
            return placement;
        }

        protected internal System.Windows.HorizontalAlignment WrapForRTL(System.Windows.HorizontalAlignment horizontalAlignment)
        {
            if (base.FlowDirection == System.Windows.FlowDirection.RightToLeft)
            {
                if (horizontalAlignment == System.Windows.HorizontalAlignment.Left)
                {
                    return System.Windows.HorizontalAlignment.Right;
                }
                if (horizontalAlignment == System.Windows.HorizontalAlignment.Right)
                {
                    return System.Windows.HorizontalAlignment.Left;
                }
            }
            return horizontalAlignment;
        }

        private PostponedAction UpdateAction { get; set; }

        protected FlyoutPositionCalculator PositionCalculator
        {
            get
            {
                FlyoutPositionCalculator positionCalculator = this.positionCalculator;
                if (this.positionCalculator == null)
                {
                    FlyoutPositionCalculator local1 = this.positionCalculator;
                    positionCalculator = this.positionCalculator = this.ActualSettings.CreatePositionCalculator();
                }
                return positionCalculator;
            }
        }

        protected internal FlyoutStrategy Strategy
        {
            get
            {
                FlyoutStrategy strategy = this.strategy;
                if (this.strategy == null)
                {
                    FlyoutStrategy local1 = this.strategy;
                    strategy = this.strategy = this.ActualSettings.CreateStrategy();
                }
                return strategy;
            }
        }

        protected UIElement PopupChild
        {
            get
            {
                Func<DevExpress.Xpf.Editors.Flyout.Native.FlyoutContainer, UIElement> evaluator = <>c.<>9__22_0;
                if (<>c.<>9__22_0 == null)
                {
                    Func<DevExpress.Xpf.Editors.Flyout.Native.FlyoutContainer, UIElement> local1 = <>c.<>9__22_0;
                    evaluator = <>c.<>9__22_0 = x => x.Child;
                }
                return this.FlyoutContainer.Return<DevExpress.Xpf.Editors.Flyout.Native.FlyoutContainer, UIElement>(evaluator, null);
            }
        }

        protected internal System.Windows.Controls.Primitives.Popup Popup { get; private set; }

        protected internal DevExpress.Xpf.Editors.Flyout.Native.FlyoutContainer FlyoutContainer { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Storyboard CurrentAnimation { get; protected set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool AnimationInProgress =>
            this.CurrentAnimation != null;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public FrameworkElement ChildContainer { get; protected set; }

        public bool IsLoadedInternal { get; protected set; }

        protected internal bool IsUpdateRequired { get; set; }

        private bool DoOpenPopup { get; set; }

        public double HorizontalOffset
        {
            get => 
                (double) base.GetValue(HorizontalOffsetProperty);
            set => 
                base.SetValue(HorizontalOffsetProperty, value);
        }

        public double VerticalOffset
        {
            get => 
                (double) base.GetValue(VerticalOffsetProperty);
            set => 
                base.SetValue(VerticalOffsetProperty, value);
        }

        public DevExpress.Xpf.Editors.Flyout.Native.IndicatorDirection IndicatorDirection
        {
            get => 
                (DevExpress.Xpf.Editors.Flyout.Native.IndicatorDirection) base.GetValue(IndicatorDirectionProperty);
            protected internal set => 
                base.SetValue(IndicatorDirectionProperty, value);
        }

        public bool ActualIsOpen
        {
            get => 
                (bool) base.GetValue(ActualIsOpenProperty);
            protected set => 
                base.SetValue(ActualIsOpenProperty, value);
        }

        protected bool ShouldBeOpened
        {
            get => 
                (bool) base.GetValue(ShouldBeOpenedProperty);
            set => 
                base.SetValue(ShouldBeOpenedProperty, value);
        }

        public bool IsOpen
        {
            get => 
                (bool) base.GetValue(IsOpenProperty);
            set => 
                base.SetValue(IsOpenProperty, value);
        }

        public UIElement PlacementTarget
        {
            get => 
                (UIElement) base.GetValue(PlacementTargetProperty);
            set => 
                base.SetValue(PlacementTargetProperty, value);
        }

        public bool StaysOpen
        {
            get => 
                (bool) base.GetValue(StaysOpenProperty);
            set => 
                base.SetValue(StaysOpenProperty, value);
        }

        public bool CloseOnInactiveWindowMouseLeave
        {
            get => 
                (bool) base.GetValue(CloseOnInactiveWindowMouseLeaveProperty);
            set => 
                base.SetValue(CloseOnInactiveWindowMouseLeaveProperty, value);
        }

        public bool AllowOutOfScreen
        {
            get => 
                (bool) base.GetValue(AllowOutOfScreenProperty);
            set => 
                base.SetValue(AllowOutOfScreenProperty, value);
        }

        public bool FollowTarget
        {
            get => 
                (bool) base.GetValue(FollowTargetProperty);
            set => 
                base.SetValue(FollowTargetProperty, value);
        }

        public FlyoutSettingsBase ActualSettings
        {
            get
            {
                FlyoutSettingsBase settings = this.Settings;
                FlyoutSettingsBase defaultSettings = settings;
                if (settings == null)
                {
                    FlyoutSettingsBase local1 = settings;
                    defaultSettings = this.defaultSettings;
                    if (this.defaultSettings == null)
                    {
                        FlyoutSettingsBase defaultSettings = this.defaultSettings;
                        defaultSettings = this.defaultSettings = this.CreateDefaultSettings();
                    }
                }
                return defaultSettings;
            }
        }

        public FlyoutSettingsBase Settings
        {
            get => 
                (FlyoutSettingsBase) base.GetValue(SettingsProperty);
            set => 
                base.SetValue(SettingsProperty, value);
        }

        public Style ContainerStyle
        {
            get => 
                (Style) base.GetValue(ContainerStyleProperty);
            set => 
                base.SetValue(ContainerStyleProperty, value);
        }

        public Duration AnimationDuration
        {
            get => 
                (Duration) base.GetValue(AnimationDurationProperty);
            set => 
                base.SetValue(AnimationDurationProperty, value);
        }

        public bool AlwaysOnTop
        {
            get => 
                (bool) base.GetValue(AlwaysOnTopProperty);
            set => 
                base.SetValue(AlwaysOnTopProperty, value);
        }

        public bool AllowMoveAnimation
        {
            get => 
                (bool) base.GetValue(AllowMoveAnimationProperty);
            set => 
                base.SetValue(AllowMoveAnimationProperty, value);
        }

        public Rect TargetBounds
        {
            get => 
                (Rect) base.GetValue(TargetBoundsProperty);
            set => 
                base.SetValue(TargetBoundsProperty, value);
        }

        public bool OpenOnHover
        {
            get => 
                (bool) base.GetValue(OpenOnHoverProperty);
            set => 
                base.SetValue(OpenOnHoverProperty, value);
        }

        public bool IsInitializedInternal
        {
            get => 
                this.isInitialized;
            protected set
            {
                this.isInitialized = value;
                if (this.isInitialized)
                {
                    this.OnInitialized();
                }
            }
        }

        public bool AllowRecreateContent { get; set; }

        public ICommand CloseCommand { get; protected set; }

        public ICommand OpenCommand { get; protected set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public FrameworkElement RenderGrid { get; protected set; }

        protected FlyoutAnimatorBase Animator
        {
            get
            {
                this.animator ??= this.ActualSettings.CreateAnimator();
                return this.animator;
            }
        }

        private Form SubscribedForm =>
            (this.subscribedForm == null) ? null : (this.subscribedForm.Target as Form);

        internal FrameworkElement SubscribedRoot =>
            (this.subscribedRoot == null) ? null : (this.subscribedRoot.Target as FrameworkElement);

        private Window SubscribedWindow =>
            (this.subscribedWindow == null) ? null : (this.subscribedWindow.Target as Window);

        private WeakEventHandler<FlyoutBase, EventArgs, EventHandler> ActivatedHandler { get; set; }

        private WeakEventHandler<FlyoutBase, EventArgs, EventHandler> DeactivatedHandler { get; set; }

        private WeakEventHandler<FlyoutBase, System.Windows.Input.MouseEventArgs, System.Windows.Input.MouseEventHandler> WindowMouseLeaveHandler { get; set; }

        private WeakEventHandler<FlyoutBase, System.Windows.Input.MouseEventArgs, System.Windows.Input.MouseEventHandler> WindowMouseMoveHandler { get; set; }

        private WeakEventHandler<FlyoutBase, EventArgs, EventHandler> WindowStateChangedHandler { get; set; }

        private WeakEventHandler<FlyoutBase, EventArgs, EventHandler> WindowLocationChangedHandler { get; set; }

        private WeakEventHandler<FlyoutBase, SizeChangedEventArgs, SizeChangedEventHandler> WindowSizeChangedHandler { get; set; }

        private WeakEventHandler<FlyoutBase, EventArgs, EventHandler> FormSizeChangedHandler { get; set; }

        private WeakEventHandler<FlyoutBase, System.Windows.Input.KeyEventArgs, System.Windows.Input.KeyEventHandler> RootKeyDownHandler { get; set; }

        private WeakEventHandler<FlyoutBase, MouseButtonEventArgs, MouseButtonEventHandler> RootMouseDownHandler { get; set; }

        protected virtual bool IsPopupVisible =>
            this.IsConnectedToPresentationSource(this) && ((!DesignerProperties.GetIsInDesignMode(this) || (InteractionHelper.GetBehaviorInDesignMode(this) != InteractionBehaviorInDesignMode.Default)) && (this.ShouldBeOpened && ((this.FlyoutContainer != null) && (this.ChildContainer != null))));

        protected internal bool HideInTaskBar =>
            (this.SubscribedWindow == null) || ((this.SubscribedWindow.WindowState != WindowState.Maximized) || (this.SubscribedWindow.WindowStyle != WindowStyle.None));

        protected internal FrameworkElement PopupRoot =>
            LayoutHelper.GetTopLevelVisual(this.ChildContainer);

        protected Rect ScreenRect =>
            this.PositionCalculator.ScreenRect;

        protected bool LockMoveAnimationOnOpenAnimation { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FlyoutBase.<>c <>9 = new FlyoutBase.<>c();
            public static Func<FlyoutContainer, UIElement> <>9__22_0;
            public static Action<FlyoutBase> <>9__189_1;
            public static Action<FlyoutBase, object, EventArgs> <>9__189_0;
            public static Action<WeakEventHandler<FlyoutBase, EventArgs, EventHandler>, object> <>9__189_2;
            public static Func<WeakEventHandler<FlyoutBase, EventArgs, EventHandler>, EventHandler> <>9__189_5;
            public static Action<FlyoutBase> <>9__189_7;
            public static Action<FlyoutBase, object, EventArgs> <>9__189_6;
            public static Action<WeakEventHandler<FlyoutBase, EventArgs, EventHandler>, object> <>9__189_8;
            public static Func<WeakEventHandler<FlyoutBase, EventArgs, EventHandler>, EventHandler> <>9__189_11;
            public static Action<FlyoutBase, object, System.Windows.Input.MouseEventArgs> <>9__189_12;
            public static Action<WeakEventHandler<FlyoutBase, System.Windows.Input.MouseEventArgs, System.Windows.Input.MouseEventHandler>, object> <>9__189_14;
            public static Func<WeakEventHandler<FlyoutBase, System.Windows.Input.MouseEventArgs, System.Windows.Input.MouseEventHandler>, System.Windows.Input.MouseEventHandler> <>9__189_16;
            public static Action<FlyoutBase, object, System.Windows.Input.MouseEventArgs> <>9__189_17;
            public static Action<WeakEventHandler<FlyoutBase, System.Windows.Input.MouseEventArgs, System.Windows.Input.MouseEventHandler>, object> <>9__189_19;
            public static Func<WeakEventHandler<FlyoutBase, System.Windows.Input.MouseEventArgs, System.Windows.Input.MouseEventHandler>, System.Windows.Input.MouseEventHandler> <>9__189_21;
            public static Action<FlyoutBase, object, EventArgs> <>9__189_22;
            public static Action<WeakEventHandler<FlyoutBase, EventArgs, EventHandler>, object> <>9__189_24;
            public static Func<WeakEventHandler<FlyoutBase, EventArgs, EventHandler>, EventHandler> <>9__189_26;
            public static Action<FlyoutBase, object, EventArgs> <>9__189_27;
            public static Action<WeakEventHandler<FlyoutBase, EventArgs, EventHandler>, object> <>9__189_29;
            public static Func<WeakEventHandler<FlyoutBase, EventArgs, EventHandler>, EventHandler> <>9__189_32;
            public static Action<FlyoutBase, object, SizeChangedEventArgs> <>9__189_33;
            public static Action<WeakEventHandler<FlyoutBase, SizeChangedEventArgs, SizeChangedEventHandler>, object> <>9__189_35;
            public static Func<WeakEventHandler<FlyoutBase, SizeChangedEventArgs, SizeChangedEventHandler>, SizeChangedEventHandler> <>9__189_37;
            public static Action<FlyoutBase, object, EventArgs> <>9__189_38;
            public static Action<WeakEventHandler<FlyoutBase, EventArgs, EventHandler>, object> <>9__189_40;
            public static Func<WeakEventHandler<FlyoutBase, EventArgs, EventHandler>, EventHandler> <>9__189_42;
            public static Action<FlyoutBase, object, System.Windows.Input.KeyEventArgs> <>9__189_43;
            public static Action<WeakEventHandler<FlyoutBase, System.Windows.Input.KeyEventArgs, System.Windows.Input.KeyEventHandler>, object> <>9__189_45;
            public static Func<WeakEventHandler<FlyoutBase, System.Windows.Input.KeyEventArgs, System.Windows.Input.KeyEventHandler>, System.Windows.Input.KeyEventHandler> <>9__189_47;
            public static Action<FlyoutBase, object, MouseButtonEventArgs> <>9__189_48;
            public static Action<WeakEventHandler<FlyoutBase, MouseButtonEventArgs, MouseButtonEventHandler>, object> <>9__189_50;
            public static Func<WeakEventHandler<FlyoutBase, MouseButtonEventArgs, MouseButtonEventHandler>, MouseButtonEventHandler> <>9__189_52;
            public static Func<bool> <>9__240_1;
            public static Func<FrameworkElement, bool> <>9__251_0;
            public static Func<FrameworkElement, FrameworkElement> <>9__251_1;
            public static Func<Size> <>9__255_1;
            public static Action<DelegateCommand<object>> <>9__305_0;
            public static Action<DelegateCommand<object>> <>9__305_1;
            public static Action<FlyoutContainer> <>9__324_1;
            public static Action<Action> <>9__325_1;

            internal void <.cctor>b__75_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FlyoutBase) d).HorizontalOffsetChanged();
            }

            internal void <.cctor>b__75_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FlyoutBase) d).VerticalOffsetChanged();
            }

            internal void <.cctor>b__75_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FlyoutBase) d).IsOpenChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__75_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FlyoutBase) d).ActualIsOpenChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__75_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FlyoutBase) d).ShouldBeOpenedChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__75_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FlyoutBase) d).PlacementTargetChanged(e);
            }

            internal void <.cctor>b__75_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FlyoutBase) d).AllowOutOfScreenChanged(e);
            }

            internal void <.cctor>b__75_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FlyoutBase) d).TargetBoundsChanged(e);
            }

            internal object <.cctor>b__75_8(DependencyObject d, object value) => 
                ((FlyoutBase) d).CoerceVisibility(value);

            internal void <.cctor>b__75_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FlyoutBase) d).OnFollowTargetChanged(e);
            }

            internal void <ActualIsOpenChanged>b__305_0(DelegateCommand<object> x)
            {
                x.RaiseCanExecuteChanged();
            }

            internal void <ActualIsOpenChanged>b__305_1(DelegateCommand<object> x)
            {
                x.RaiseCanExecuteChanged();
            }

            internal void <Animate>b__325_1(Action x)
            {
                x();
            }

            internal Size <CalcPopupBounds>b__255_1() => 
                new Size();

            internal void <ClosePopup>b__324_1(FlyoutContainer x)
            {
                x.IsOpen = false;
            }

            internal void <CreateHandlers>b__189_0(FlyoutBase flyout, object sender, EventArgs args)
            {
                Action<FlyoutBase> action = <>9__189_1;
                if (<>9__189_1 == null)
                {
                    Action<FlyoutBase> local1 = <>9__189_1;
                    action = <>9__189_1 = x => x.ShowOnWindowActivated();
                }
                flyout.Do<FlyoutBase>(action);
            }

            internal void <CreateHandlers>b__189_1(FlyoutBase x)
            {
                x.ShowOnWindowActivated();
            }

            internal EventHandler <CreateHandlers>b__189_11(WeakEventHandler<FlyoutBase, EventArgs, EventHandler> h) => 
                new EventHandler(h.OnEvent);

            internal void <CreateHandlers>b__189_12(FlyoutBase flyout, object sender, System.Windows.Input.MouseEventArgs args)
            {
                flyout.Do<FlyoutBase>(x => x.WindowMouseLeave(sender, args));
            }

            internal void <CreateHandlers>b__189_14(WeakEventHandler<FlyoutBase, System.Windows.Input.MouseEventArgs, System.Windows.Input.MouseEventHandler> h, object sender)
            {
                (sender as Window).Do<Window>(delegate (Window x) {
                    x.MouseLeave -= h.Handler;
                });
            }

            internal System.Windows.Input.MouseEventHandler <CreateHandlers>b__189_16(WeakEventHandler<FlyoutBase, System.Windows.Input.MouseEventArgs, System.Windows.Input.MouseEventHandler> h) => 
                new System.Windows.Input.MouseEventHandler(h.OnEvent);

            internal void <CreateHandlers>b__189_17(FlyoutBase flyout, object sender, System.Windows.Input.MouseEventArgs args)
            {
                flyout.Do<FlyoutBase>(x => x.WindowMouseMove(sender, args));
            }

            internal void <CreateHandlers>b__189_19(WeakEventHandler<FlyoutBase, System.Windows.Input.MouseEventArgs, System.Windows.Input.MouseEventHandler> h, object sender)
            {
                (sender as Window).Do<Window>(delegate (Window x) {
                    x.MouseMove -= h.Handler;
                });
            }

            internal void <CreateHandlers>b__189_2(WeakEventHandler<FlyoutBase, EventArgs, EventHandler> h, object sender)
            {
                (sender as Window).Do<Window>(delegate (Window x) {
                    x.Activated -= h.Handler;
                });
                (sender as Form).Do<Form>(delegate (Form x) {
                    x.Activated -= h.Handler;
                });
            }

            internal System.Windows.Input.MouseEventHandler <CreateHandlers>b__189_21(WeakEventHandler<FlyoutBase, System.Windows.Input.MouseEventArgs, System.Windows.Input.MouseEventHandler> h) => 
                new System.Windows.Input.MouseEventHandler(h.OnEvent);

            internal void <CreateHandlers>b__189_22(FlyoutBase flyout, object sender, EventArgs args)
            {
                flyout.Do<FlyoutBase>(x => x.WindowStateChanged(sender, args));
            }

            internal void <CreateHandlers>b__189_24(WeakEventHandler<FlyoutBase, EventArgs, EventHandler> h, object sender)
            {
                (sender as Window).Do<Window>(delegate (Window x) {
                    x.StateChanged -= h.Handler;
                });
            }

            internal EventHandler <CreateHandlers>b__189_26(WeakEventHandler<FlyoutBase, EventArgs, EventHandler> h) => 
                new EventHandler(h.OnEvent);

            internal void <CreateHandlers>b__189_27(FlyoutBase flyout, object sender, EventArgs args)
            {
                flyout.Do<FlyoutBase>(x => x.WindowLocationChanged(sender, args));
            }

            internal void <CreateHandlers>b__189_29(WeakEventHandler<FlyoutBase, EventArgs, EventHandler> h, object sender)
            {
                (sender as Window).Do<Window>(delegate (Window x) {
                    x.LocationChanged -= h.Handler;
                });
                (sender as Form).Do<Form>(delegate (Form x) {
                    x.LocationChanged -= h.Handler;
                });
            }

            internal EventHandler <CreateHandlers>b__189_32(WeakEventHandler<FlyoutBase, EventArgs, EventHandler> h) => 
                new EventHandler(h.OnEvent);

            internal void <CreateHandlers>b__189_33(FlyoutBase flyout, object sender, SizeChangedEventArgs args)
            {
                flyout.Do<FlyoutBase>(x => x.WindowSizeChanged(sender, args));
            }

            internal void <CreateHandlers>b__189_35(WeakEventHandler<FlyoutBase, SizeChangedEventArgs, SizeChangedEventHandler> h, object sender)
            {
                (sender as Window).Do<Window>(delegate (Window x) {
                    x.SizeChanged -= h.Handler;
                });
            }

            internal SizeChangedEventHandler <CreateHandlers>b__189_37(WeakEventHandler<FlyoutBase, SizeChangedEventArgs, SizeChangedEventHandler> h) => 
                new SizeChangedEventHandler(h.OnEvent);

            internal void <CreateHandlers>b__189_38(FlyoutBase flyout, object sender, EventArgs args)
            {
                flyout.Do<FlyoutBase>(x => x.WindowSizeChanged(sender, args));
            }

            internal void <CreateHandlers>b__189_40(WeakEventHandler<FlyoutBase, EventArgs, EventHandler> h, object sender)
            {
                (sender as Form).Do<Form>(delegate (Form x) {
                    x.SizeChanged -= h.Handler;
                });
            }

            internal EventHandler <CreateHandlers>b__189_42(WeakEventHandler<FlyoutBase, EventArgs, EventHandler> h) => 
                new EventHandler(h.OnEvent);

            internal void <CreateHandlers>b__189_43(FlyoutBase flyout, object sender, System.Windows.Input.KeyEventArgs args)
            {
                flyout.Do<FlyoutBase>(x => x.RootKeyDown(sender, args));
            }

            internal void <CreateHandlers>b__189_45(WeakEventHandler<FlyoutBase, System.Windows.Input.KeyEventArgs, System.Windows.Input.KeyEventHandler> h, object sender)
            {
                (sender as Window).Do<Window>(delegate (Window x) {
                    x.KeyDown -= h.Handler;
                });
            }

            internal System.Windows.Input.KeyEventHandler <CreateHandlers>b__189_47(WeakEventHandler<FlyoutBase, System.Windows.Input.KeyEventArgs, System.Windows.Input.KeyEventHandler> h) => 
                new System.Windows.Input.KeyEventHandler(h.OnEvent);

            internal void <CreateHandlers>b__189_48(FlyoutBase flyout, object sender, MouseButtonEventArgs args)
            {
                flyout.Do<FlyoutBase>(x => x.RootMouseDown(sender, args));
            }

            internal EventHandler <CreateHandlers>b__189_5(WeakEventHandler<FlyoutBase, EventArgs, EventHandler> h) => 
                new EventHandler(h.OnEvent);

            internal void <CreateHandlers>b__189_50(WeakEventHandler<FlyoutBase, MouseButtonEventArgs, MouseButtonEventHandler> h, object sender)
            {
                (sender as Window).Do<Window>(x => x.RemoveHandler(UIElement.MouseDownEvent, h.Handler));
            }

            internal MouseButtonEventHandler <CreateHandlers>b__189_52(WeakEventHandler<FlyoutBase, MouseButtonEventArgs, MouseButtonEventHandler> h) => 
                new MouseButtonEventHandler(h.OnEvent);

            internal void <CreateHandlers>b__189_6(FlyoutBase flyout, object sender, EventArgs args)
            {
                Action<FlyoutBase> action = <>9__189_7;
                if (<>9__189_7 == null)
                {
                    Action<FlyoutBase> local1 = <>9__189_7;
                    action = <>9__189_7 = x => x.HideOnWindowDeactivated();
                }
                flyout.Do<FlyoutBase>(action);
            }

            internal void <CreateHandlers>b__189_7(FlyoutBase x)
            {
                x.HideOnWindowDeactivated();
            }

            internal void <CreateHandlers>b__189_8(WeakEventHandler<FlyoutBase, EventArgs, EventHandler> h, object sender)
            {
                (sender as Window).Do<Window>(delegate (Window x) {
                    x.Deactivated -= h.Handler;
                });
                (sender as Form).Do<Form>(delegate (Form x) {
                    x.Deactivate -= h.Handler;
                });
            }

            internal UIElement <get_PopupChild>b__22_0(FlyoutContainer x) => 
                x.Child;

            internal bool <GetElement>b__251_0(FrameworkElement x) => 
                x.IsInVisualTree();

            internal FrameworkElement <GetElement>b__251_1(FrameworkElement x) => 
                x;

            internal bool <RaiseIsOpeningChanged>b__240_1() => 
                false;
        }

        public static class DpiAwareHelper
        {
            public static Point PointToControl(Point point, Point monitorPoint) => 
                (!Net462Detector.IsNet462() || !IsOSHigherSeven) ? ScreenHelper.GetScaledPoint(point) : ScreenHelper.GetScaledPoint(point, ScreenHelper.GetDpi(monitorPoint));

            public static Rect RectToControl(Rect rect, Point monitorPoint) => 
                (!Net462Detector.IsNet462() || !IsOSHigherSeven) ? ScreenHelper.GetScaledRect(rect) : new Rect(rect.Location, ScreenHelper.GetScaledSize(rect.Size, ScreenHelper.GetDpi(monitorPoint)));

            public static Size SizeToControl(Size size, Point monitorPoint) => 
                (!Net462Detector.IsNet462() || !IsOSHigherSeven) ? ScreenHelper.GetScaledSize(size) : ScreenHelper.GetScaledSize(size, ScreenHelper.GetDpi(monitorPoint));

            public static Size SizeToMonitor(FrameworkElement element, Size size)
            {
                if (!Net462Detector.IsNet462() || !IsOSHigherSeven)
                {
                    return ScreenHelper.GetScaledSize(size, element);
                }
                Point dpi = ScreenHelper.GetDpi(TranslateHelper.ToScreen(element, new Rect(0.0, 0.0, size.Width, size.Height)).Center());
                return new Size((size.Width * dpi.X) / 96.0, (size.Height * dpi.Y) / 96.0);
            }

            private static bool IsOSHigherSeven =>
                Environment.OSVersion.Version >= new Version(6, 3);
        }

        public class FlyinStrategy : FlyoutBase.FlyoutStrategy
        {
            public override Rect GetDefaultTargetBounds(FlyoutBase flyoutControl) => 
                flyoutControl.PositionCalculator.GetScreenRect(ScreenHelper.GetScreenRect(flyoutControl.FlyoutContainer.Element).Center(), flyoutControl.HideInTaskBar);

            public override Point GetOpenAnimationOffset(FlyoutBase flyoutControl)
            {
                double width = flyoutControl.PositionCalculator.Result.Size.Width;
                double height = flyoutControl.PositionCalculator.Result.Size.Height;
                if (flyoutControl.VerticalAlignment == VerticalAlignment.Top)
                {
                    height = -height;
                }
                if (flyoutControl.HorizontalAlignment == System.Windows.HorizontalAlignment.Left)
                {
                    width = -width;
                }
                if ((flyoutControl.VerticalAlignment == VerticalAlignment.Center) || (flyoutControl.VerticalAlignment == VerticalAlignment.Stretch))
                {
                    height = 0.0;
                }
                if ((flyoutControl.HorizontalAlignment == System.Windows.HorizontalAlignment.Center) || (flyoutControl.HorizontalAlignment == System.Windows.HorizontalAlignment.Stretch))
                {
                    width = 0.0;
                }
                return new Point(width, height);
            }
        }

        public class FlyoutStrategy
        {
            public const double MinOffset = 40.0;

            public virtual Rect GetDefaultTargetBounds(FlyoutBase flyoutControl) => 
                new Rect();

            public virtual FrameworkElement GetMeasureElement(FlyoutBase flyoutControl) => 
                flyoutControl.ChildContainer;

            public virtual Point GetOpenAnimationOffset(FlyoutBase flyoutControl)
            {
                Rect targetBounds = flyoutControl.PositionCalculator.TargetBounds;
                Rect bounds = flyoutControl.PositionCalculator.Result.Bounds;
                double number = targetBounds.CenterX() - bounds.CenterX();
                double num2 = targetBounds.CenterY() - bounds.CenterY();
                if (number.IsNotNumber() || num2.IsNotNumber())
                {
                    return new Point(double.NaN, double.NaN);
                }
                number = Math.Min(40.0, Math.Abs(number)) * Math.Sign(number);
                num2 = Math.Min(40.0, Math.Abs(num2)) * Math.Sign(num2);
                FlyoutPlacement placement = this.GetSettings(flyoutControl).Placement;
                if (((placement == FlyoutPlacement.Left) || (placement == FlyoutPlacement.Right)) && ((flyoutControl.VerticalAlignment == VerticalAlignment.Top) || (flyoutControl.VerticalAlignment == VerticalAlignment.Bottom)))
                {
                    num2 = 0.0;
                }
                if (((placement == FlyoutPlacement.Bottom) || (placement == FlyoutPlacement.Top)) && ((flyoutControl.HorizontalAlignment == System.Windows.HorizontalAlignment.Left) || (flyoutControl.HorizontalAlignment == System.Windows.HorizontalAlignment.Right)))
                {
                    number = 0.0;
                }
                return new Point(number, num2);
            }

            protected FlyoutSettings GetSettings(FlyoutBase flyoutControl) => 
                flyoutControl.ActualSettings as FlyoutSettings;

            public virtual void UpdatePopupSize(FlyoutBase flyoutControl, Size size)
            {
                if (flyoutControl.RenderGrid != null)
                {
                    Point monitorPoint = flyoutControl.PositionCalculator.ScreenRect.Center();
                    Size size2 = flyoutControl.PositionCalculator.ScreenRect.Size;
                    if (flyoutControl.VerticalAlignment == VerticalAlignment.Stretch)
                    {
                        flyoutControl.RenderGrid.Height = FlyoutBase.DpiAwareHelper.SizeToControl(size, monitorPoint).Height;
                    }
                    if (flyoutControl.HorizontalAlignment == System.Windows.HorizontalAlignment.Stretch)
                    {
                        flyoutControl.RenderGrid.Width = FlyoutBase.DpiAwareHelper.SizeToControl(size, monitorPoint).Width;
                    }
                }
            }
        }
    }
}

