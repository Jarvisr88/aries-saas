namespace DevExpress.Xpf.Core
{
    using DevExpress.Data;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Threading;

    [SupportDXTheme(TypeInAssembly=typeof(FloatingContainer)), ContentProperty("Content")]
    public abstract class FloatingContainer : BaseFloatingContainer, IDialogOwner
    {
        public static readonly DependencyProperty IsActiveProperty;
        public static readonly DependencyProperty IsClosedProperty;
        private static readonly DependencyPropertyKey IsClosedPropertyKey;
        public static readonly DependencyProperty FloatingContainerProperty;
        public static readonly DependencyProperty ContainerStartupLocationProperty;
        public static readonly DependencyProperty ContentTemplateProperty;
        public static readonly DependencyProperty AllowMovingProperty;
        public static readonly DependencyProperty AllowSizingProperty;
        public static readonly DependencyProperty AllowShowAnimationsProperty;
        public static readonly DependencyProperty CaptionProperty;
        public static readonly DependencyProperty ContainerFocusableProperty;
        public static readonly DependencyProperty IconProperty;
        public static readonly RoutedEvent HiddenEvent;
        public static readonly RoutedEvent HidingEvent;
        public static readonly RoutedEvent FloatingContainerIsOpenChangedEvent;
        public static readonly DependencyProperty CloseOnEscapeProperty;
        public static readonly DependencyProperty ShowModalProperty;
        public static readonly DependencyProperty ShowActivatedProperty;
        public static readonly DependencyProperty IsMaximizedProperty;
        public static readonly DependencyProperty DialogResultProperty;
        public static readonly DependencyProperty DialogOwnerProperty;
        public static readonly DependencyProperty LogicalOwnerProperty;
        public static readonly DependencyProperty ShowCloseButtonProperty;
        public static readonly DependencyProperty SizeToContentProperty;
        protected int isAutoSizeUpdating;
        private int isSizing;
        protected int lockInversion;
        private bool canUpdateAutoSize = true;
        private int closing;
        private ModalAdorner modalAdorner;

        public event RoutedEventHandler Hidden
        {
            add
            {
                base.AddHandler(HiddenEvent, value);
            }
            remove
            {
                base.RemoveHandler(HiddenEvent, value);
            }
        }

        public event CancelRoutedEventHandler Hiding
        {
            add
            {
                base.AddHandler(HidingEvent, value);
            }
            remove
            {
                base.RemoveHandler(HidingEvent, value);
            }
        }

        static FloatingContainer()
        {
            Type ownerType = typeof(FloatingContainer);
            IsClosedPropertyKey = DependencyProperty.RegisterReadOnly("IsClosed", typeof(bool), ownerType, new UIPropertyMetadata(false));
            IsClosedProperty = IsClosedPropertyKey.DependencyProperty;
            IsActiveProperty = DependencyProperty.RegisterAttached("IsActive", typeof(bool), typeof(FloatingContainer), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits, null));
            FloatingContainerProperty = DependencyProperty.RegisterAttached("FloatingContainer", typeof(FloatingContainer), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
            ContainerStartupLocationProperty = DependencyProperty.Register("ContainerStartupLocation", typeof(WindowStartupLocation), ownerType, new FrameworkPropertyMetadata(WindowStartupLocation.Manual, FrameworkPropertyMetadataOptions.Inherits));
            ContentTemplateProperty = DependencyProperty.Register("ContentTemplate", typeof(DataTemplate), ownerType, new UIPropertyMetadata(null));
            AllowMovingProperty = DependencyProperty.Register("AllowMoving", typeof(bool), ownerType, new UIPropertyMetadata(true));
            AllowSizingProperty = DependencyProperty.Register("AllowSizing", typeof(bool), ownerType, new UIPropertyMetadata(true));
            AllowShowAnimationsProperty = DependencyProperty.Register("AllowShowAnimations", typeof(bool), ownerType, new UIPropertyMetadata(true));
            CaptionProperty = DependencyProperty.Register("Caption", typeof(string), ownerType, new UIPropertyMetadata(string.Empty));
            ContainerFocusableProperty = DependencyProperty.Register("ContainerFocusable", typeof(bool), ownerType, new PropertyMetadata(true));
            IconProperty = DependencyProperty.Register("Icon", typeof(ImageSource), ownerType, new UIPropertyMetadata(null));
            HiddenEvent = EventManager.RegisterRoutedEvent("Hidden", RoutingStrategy.Direct, typeof(RoutedEventHandler), ownerType);
            HidingEvent = EventManager.RegisterRoutedEvent("Hiding", RoutingStrategy.Direct, typeof(CancelRoutedEventHandler), ownerType);
            FloatingContainerIsOpenChangedEvent = EventManager.RegisterRoutedEvent("FloatingContainerIsOpenChanged", RoutingStrategy.Bubble, typeof(FloatingContainerEventHandler), ownerType);
            CloseOnEscapeProperty = DependencyProperty.Register("CloseOnEscape", typeof(bool), ownerType, new PropertyMetadata(false));
            ShowModalProperty = DependencyProperty.Register("ShowModal", typeof(bool), ownerType, new PropertyMetadata(false));
            ShowActivatedProperty = DependencyProperty.Register("ShowActivated", typeof(bool), ownerType, new PropertyMetadata(Window.ShowActivatedProperty.DefaultMetadata.DefaultValue));
            IsMaximizedProperty = DependencyProperty.RegisterAttached("IsMaximized", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, null));
            DialogResultProperty = DependencyProperty.Register("DialogResult", typeof(bool?), ownerType, new FrameworkPropertyMetadata(null));
            DialogOwnerProperty = DependencyProperty.RegisterAttached("DialogOwner", typeof(IDialogOwner), ownerType, new FrameworkPropertyMetadata(null));
            LogicalOwnerProperty = DependencyProperty.Register("LogicalOwner", typeof(FrameworkElement), ownerType, new FrameworkPropertyMetadata(null));
            ShowCloseButtonProperty = DependencyProperty.Register("ShowCloseButton", typeof(bool), ownerType, new PropertyMetadata(true, (dObj, e) => ((FloatingContainer) dObj).OnShowCloseButtonChanged()));
            SizeToContentProperty = DependencyProperty.Register("SizeToContent", typeof(System.Windows.SizeToContent), typeof(FloatingContainer), new PropertyMetadata(System.Windows.SizeToContent.Manual, new PropertyChangedCallback(FloatingContainer.OnSizeToContentChanged)));
        }

        protected FloatingContainer()
        {
        }

        public virtual void Activate()
        {
        }

        public static void AddFloatingContainerIsOpenChangedHandler(DependencyObject dObj, FloatingContainerEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(FloatingContainerIsOpenChangedEvent, handler);
            }
        }

        protected virtual void CalcBottomOffset(double absHChange, ref double dy, ref double sy)
        {
            sy = absHChange;
        }

        protected virtual void CalcLeftOffset(double absWChange, ref double dx, ref double sx)
        {
            dx = absWChange;
            sx = -absWChange;
        }

        protected void CalcOffsets(DXWindowActiveResizeParts activePart, double absWChange, double absHChange, out double dx, out double dy, out double sx, out double sy)
        {
            dx = 0.0;
            dy = 0.0;
            sx = 0.0;
            sy = 0.0;
            switch (activePart)
            {
                case DXWindowActiveResizeParts.Left:
                    this.CalcLeftOffset(absWChange, ref dx, ref sx);
                    return;

                case DXWindowActiveResizeParts.Right:
                    this.CalcRightOffset(absWChange, ref dx, ref sx);
                    return;

                case DXWindowActiveResizeParts.Top:
                    this.CalcTopOffset(absHChange, ref dy, ref sy);
                    return;

                case DXWindowActiveResizeParts.TopLeft:
                    this.CalcTopOffset(absHChange, ref dy, ref sy);
                    this.CalcLeftOffset(absWChange, ref dx, ref sx);
                    return;

                case DXWindowActiveResizeParts.TopRight:
                    this.CalcTopOffset(absHChange, ref dy, ref sy);
                    this.CalcRightOffset(absWChange, ref dx, ref sx);
                    return;

                case DXWindowActiveResizeParts.Bottom:
                    this.CalcBottomOffset(absHChange, ref dy, ref sy);
                    return;

                case DXWindowActiveResizeParts.BottomLeft:
                    this.CalcBottomOffset(absHChange, ref dy, ref sy);
                    this.CalcLeftOffset(absWChange, ref dx, ref sx);
                    return;

                case DXWindowActiveResizeParts.BottomRight:
                    this.CalcBottomOffset(absHChange, ref dy, ref sy);
                    this.CalcRightOffset(absWChange, ref dx, ref sx);
                    return;
            }
        }

        protected virtual void CalcRightOffset(double absWChange, ref double dx, ref double sx)
        {
            sx = absWChange;
        }

        protected virtual void CalcTopOffset(double absHChange, ref double dy, ref double sy)
        {
            dy = absHChange;
            sy = -absHChange;
        }

        private static double ChangeValueForRange(double val, double offset, double min, double max)
        {
            double num = val + offset;
            num = (num < min) ? Math.Min(min, Math.Max(val, num)) : num;
            return ((max < num) ? Math.Max(max, Math.Min(val, num)) : num);
        }

        protected static double CheckHChange(double y, double hChange, Rect constraints, Thickness threshold)
        {
            if ((constraints.Width * constraints.Height) == 0.0)
            {
                return hChange;
            }
            double num = y + hChange;
            if ((num + threshold.Bottom) > constraints.Bottom)
            {
                num -= (num + threshold.Bottom) - constraints.Bottom;
            }
            if ((num - threshold.Top) < constraints.Top)
            {
                num -= (num - threshold.Top) - constraints.Top;
            }
            return (num - y);
        }

        protected static Point CheckLocation(Point location, Rect constraints, Thickness threshold)
        {
            if ((constraints.Width * constraints.Height) == 0.0)
            {
                return location;
            }
            double x = location.X;
            double y = location.Y;
            if ((x + threshold.Right) > constraints.Right)
            {
                x -= (x + threshold.Right) - constraints.Right;
            }
            if ((y + threshold.Bottom) > constraints.Bottom)
            {
                y -= (y + threshold.Bottom) - constraints.Bottom;
            }
            if ((x - threshold.Left) < constraints.Left)
            {
                x -= (x - threshold.Left) - constraints.Left;
            }
            if ((y - threshold.Top) < constraints.Top)
            {
                y -= (y - threshold.Top) - constraints.Top;
            }
            return new Point(x, y);
        }

        protected static double CheckWChange(double x, double wChange, Rect constraints, Thickness threshold)
        {
            if ((constraints.Width * constraints.Height) == 0.0)
            {
                return wChange;
            }
            double num = x + wChange;
            if ((num + threshold.Right) > constraints.Right)
            {
                num -= (num + threshold.Right) - constraints.Right;
            }
            if ((num - threshold.Left) < constraints.Left)
            {
                num -= (num - threshold.Left) - constraints.Left;
            }
            return (num - x);
        }

        public void Close()
        {
            if (this.closing <= 0)
            {
                this.closing++;
                try
                {
                    this.CloseCore();
                }
                finally
                {
                    this.closing--;
                }
            }
        }

        protected virtual void CloseCore()
        {
            this.ProcessHiding();
            this.IsClosed = !base.IsOpen;
        }

        public virtual void CloseDialog(bool? dialogResult)
        {
            this.DialogResult = dialogResult;
            this.Close();
        }

        public static void CloseDialog(FrameworkElement dialogContent, bool? dialogResult)
        {
            IDialogOwner dialogOwner = GetDialogOwner(dialogContent);
            if (dialogOwner != null)
            {
                dialogOwner.CloseDialog(dialogResult);
            }
            else
            {
                Window window = Window.GetWindow(dialogContent);
                window.DialogResult = dialogResult;
                window.Close();
            }
        }

        public virtual Point CorrectRightToLeftLocation(Point location) => 
            location;

        protected override BaseFloatingContainer.ManagedContentPresenter CreateContentPresenter() => 
            new ThemedManagedContentPresenter(this);

        private void CreateModalAdorner()
        {
            if (FindModalAdorner(base.Owner) == null)
            {
                AdornerLayer layer = AdornerHelper.FindAdornerLayer(base.Owner);
                if (layer != null)
                {
                    layer.Add(new ModalAdorner(base.Owner));
                }
            }
        }

        protected void EnsureMinSize()
        {
            if (base.ReadLocalValue(FrameworkElement.MinWidthProperty) == DependencyProperty.UnsetValue)
            {
                base.MinWidth = 100.0;
            }
            if (base.ReadLocalValue(FrameworkElement.MinHeightProperty) == DependencyProperty.UnsetValue)
            {
                base.MinHeight = 100.0;
            }
        }

        internal static ModalAdorner FindModalAdorner(UIElement owner)
        {
            AdornerLayer layer = AdornerHelper.FindAdornerLayer(owner);
            Adorner[] array = new Adorner[0];
            if (layer != null)
            {
                array = layer.GetAdorners(owner);
            }
            if (array == null)
            {
                return null;
            }
            Predicate<Adorner> match = <>c.<>9__160_0;
            if (<>c.<>9__160_0 == null)
            {
                Predicate<Adorner> local1 = <>c.<>9__160_0;
                match = <>c.<>9__160_0 = adorner => adorner is ModalAdorner;
            }
            return (ModalAdorner) Array.Find<Adorner>(array, match);
        }

        public static IDialogOwner GetDialogOwner(DependencyObject obj) => 
            (IDialogOwner) obj.GetValue(DialogOwnerProperty);

        public static FloatingContainer GetFloatingContainer(DependencyObject obj) => 
            (FloatingContainer) obj.GetValue(FloatingContainerProperty);

        protected abstract FloatingMode GetFloatingMode();
        public static bool GetIsActive(DependencyObject obj) => 
            (bool) obj.GetValue(IsActiveProperty);

        public static bool GetIsMaximized(DependencyObject obj) => 
            (bool) obj.GetValue(IsMaximizedProperty);

        protected Size GetLayoutAutoSize()
        {
            UIElement presenter = base.Presenter;
            if (presenter != null)
            {
                presenter.InvalidateMeasure();
                if (this.SizeToContent == System.Windows.SizeToContent.WidthAndHeight)
                {
                    presenter.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    return presenter.DesiredSize;
                }
                Size availableSize = new Size((this.SizeToContent == System.Windows.SizeToContent.Width) ? double.PositiveInfinity : base.FloatSize.Width, (this.SizeToContent == System.Windows.SizeToContent.Height) ? double.PositiveInfinity : base.FloatSize.Height);
                presenter.Measure(availableSize);
                if (this.SizeToContent == System.Windows.SizeToContent.Width)
                {
                    return new Size(presenter.DesiredSize.Width, base.FloatSize.Height);
                }
                if (this.SizeToContent == System.Windows.SizeToContent.Height)
                {
                    return new Size(base.FloatSize.Width, presenter.DesiredSize.Height);
                }
            }
            return Size.Empty;
        }

        public Point GetPosition(MouseEventArgs e) => 
            (base.ContentContainer != null) ? e.GetPosition(base.ContentContainer) : new Point(-10000.0, -10000.0);

        private void HideModalAdorner()
        {
            if (this.modalAdorner != null)
            {
                this.modalAdorner.UnLockPreviewEvents();
                this.modalAdorner.Visibility = Visibility.Hidden;
            }
            this.modalAdorner = null;
            ModalContainer = null;
        }

        internal static Size InitDialog(FrameworkElement dialogContent, FrameworkElement element, DialogClosedDelegate closedDelegate, Size size, string title, bool allowSizing, FloatingContainer container, bool closeOnEscape) => 
            InitDialog(dialogContent, element, closedDelegate, size, title, allowSizing, container, closeOnEscape, null, true);

        internal static Size InitDialog(FrameworkElement dialogContent, FrameworkElement element, DialogClosedDelegate closedDelegate, Size size, string title, bool allowSizing, FloatingContainer container, bool closeOnEscape, ImageSource icon, bool containerFocusable) => 
            InitDialog(dialogContent, element, closedDelegate, size, title, allowSizing, container, closeOnEscape, icon, containerFocusable, true);

        internal static Size InitDialog(FrameworkElement dialogContent, FrameworkElement element, DialogClosedDelegate closedDelegate, Size size, string title, bool allowSizing, FloatingContainer container, bool closeOnEscape, ImageSource icon, bool containerFocusable, bool showModal)
        {
            SetDialogOwner(dialogContent, container);
            DialogControl control = dialogContent as DialogControl;
            if (control != null)
            {
                DependencyObject obj2 = control.DialogContent;
                if (obj2 != null)
                {
                    SetDialogOwner(obj2, container);
                }
            }
            if (element is ILogicalOwner)
            {
                ((ILogicalOwner) element).AddChild(container);
            }
            else if (LogicalTreeHelper.GetParent(container) == null)
            {
                LogicalTreeIntruder.AddLogicalChild(element, container);
            }
            container.BeginUpdate();
            if (size.IsEmpty || (double.IsNaN(size.Width) && double.IsNaN(size.Height)))
            {
                container.SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
            }
            else
            {
                if (double.IsNaN(size.Width))
                {
                    container.SizeToContent = System.Windows.SizeToContent.Width;
                }
                else
                {
                    container.MinWidth = size.Width;
                }
                if (double.IsNaN(size.Height))
                {
                    container.SizeToContent = System.Windows.SizeToContent.Height;
                }
                else
                {
                    container.MinHeight = size.Height;
                }
                container.FloatSize = new Size(Math.Max(container.FloatSize.Width, container.MinWidth), Math.Max(container.FloatSize.Height, container.MinHeight));
            }
            container.LogicalOwner = element;
            container.Owner = (FrameworkElement) container.InitDialogCorrectOwner(element);
            container.ShowModal = showModal;
            container.Icon = icon;
            container.Caption = title;
            container.AllowSizing = allowSizing;
            container.CloseOnEscape = closeOnEscape;
            container.DeactivateOnClose = true;
            container.ContainerFocusable = containerFocusable;
            FloatingContainerHiddenHandler handler = new FloatingContainerHiddenHandler(container, element, closedDelegate);
            container.Hidden += new RoutedEventHandler(handler.HiddenHandler);
            container.ContainerStartupLocation = (!IsRealWindow(container) || !IsMessageBox(dialogContent)) ? WindowStartupLocation.CenterOwner : WindowStartupLocation.CenterScreen;
            container.Content = dialogContent;
            container.EndUpdate();
            if (!container.IsUpdateLocked)
            {
                container.IsOpen = true;
            }
            return size;
        }

        protected virtual UIElement InitDialogCorrectOwner(UIElement element) => 
            element;

        private static bool IsDialogContent(object content)
        {
            FrameworkElement element = content as FrameworkElement;
            return ((element != null) && (GetDialogOwner(element) != null));
        }

        private static bool IsMessageBox(FrameworkElement dialogContent) => 
            (dialogContent != null) && (dialogContent is DXMessageBox);

        private static bool IsRealWindow(FloatingContainer container) => 
            !BrowserInteropHelper.IsBrowserHosted && (container.GetFloatingMode() == FloatingMode.Window);

        protected double MeasureAutoSize(double size, double autoSize, double realSize)
        {
            if (this.isSizing > 0)
            {
                return size;
            }
            double num = ((this.isAutoSizeUpdating > 0) || double.IsNaN(realSize)) ? double.MaxValue : realSize;
            double num2 = (this.isAutoSizeUpdating > 0) ? 0.0 : size;
            return Math.Min(num, Math.Max(autoSize, num2));
        }

        protected override void NotifyIsOpenChanged(bool isOpen)
        {
            if (this.LogicalOwner != null)
            {
                FloatingContainerEventArgs e = new FloatingContainerEventArgs(this);
                e.RoutedEvent = FloatingContainerIsOpenChangedEvent;
                this.LogicalOwner.RaiseEvent(e);
            }
            if (!isOpen)
            {
                base.RaiseEvent(new RoutedEventArgs(HiddenEvent));
            }
        }

        protected override void OnContentUpdated(object content, FrameworkElement owner)
        {
        }

        protected override void OnFloatingBoundsChanged(Rect bounds)
        {
            if (this.IsAlive)
            {
                this.UpdateFloatingBoundsCore(bounds);
            }
        }

        protected virtual void OnHided()
        {
            base.IsOpen = false;
        }

        protected override void OnHierarchyCreated()
        {
            if (this.ShowModal)
            {
                this.CreateModalAdorner();
            }
            SetFloatingContainer(base.ContentContainer, this);
            SetFloatingContainer(base.Presenter, this);
            SetIsMaximized(base.Presenter, false);
        }

        protected override void OnIsOpenChanged(bool isOpen)
        {
            FloatingContainerClosedException.CheckFloatingContainerIsNotClosed(this);
            if (this.IsAlive)
            {
                if (this.ShowModal)
                {
                    if (isOpen)
                    {
                        this.ShowModalAdorner();
                    }
                    else
                    {
                        this.HideModalAdorner();
                    }
                }
                this.UpdateIsOpenCore(isOpen);
            }
        }

        protected virtual void OnLocationChanged(Point newLocation)
        {
            base.FloatLocation = newLocation;
        }

        protected virtual void OnShowCloseButtonChanged()
        {
            if (base.Presenter != null)
            {
                base.Presenter.UpdatePresenter();
            }
        }

        protected virtual void OnSizeChanged(Size newSize)
        {
            base.FloatSize = newSize;
        }

        private static void OnSizeToContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FloatingContainer) d).OnSizeToContentChangedCore((System.Windows.SizeToContent) e.NewValue);
        }

        protected virtual void OnSizeToContentChangedCore(System.Windows.SizeToContent newVal)
        {
        }

        protected void ProcessClosing()
        {
            if (!IsDialogContent(base.Content))
            {
                this.ProcessHiding();
            }
            else
            {
                bool? dialogResult = null;
                CloseDialog((FrameworkElement) base.Content, dialogResult);
            }
        }

        protected void ProcessHiding()
        {
            if ((ModalContainer == null) || ReferenceEquals(ModalContainer, this))
            {
                this.canUpdateAutoSize = false;
                CancelRoutedEventArgs e = new CancelRoutedEventArgs(HidingEvent);
                base.RaiseEvent(e);
                this.IsClosingCanceled = e.Cancel;
                if (!e.Cancel)
                {
                    this.OnHided();
                }
            }
        }

        private void ProcessMoving(DragDeltaEventArgs e)
        {
            if (this.AllowMoving)
            {
                double horizontalChange = e.HorizontalChange;
                if ((this.lockInversion == 0) && this.InvertRTLMove)
                {
                    horizontalChange = -horizontalChange;
                }
                this.OnLocationChanged(new Point(base.FloatLocation.X + horizontalChange, base.FloatLocation.Y + e.VerticalChange));
            }
        }

        private void ProcessSizing(DragDeltaEventArgs e)
        {
            if (this.AllowMoving)
            {
                this.isSizing++;
                Point point = this.ScreenToLogical(new Point(e.HorizontalChange, e.VerticalChange));
                Size newSize = new Size(ChangeValueForRange(this.ActualSize.Width, point.X, base.MinWidth, base.MaxWidth), ChangeValueForRange(this.ActualSize.Height, point.Y, base.MinHeight, base.MaxHeight));
                this.canUpdateAutoSize = false;
                this.OnSizeChanged(newSize);
                this.isSizing--;
            }
        }

        public static void RemoveFloatingContainerIsOpenChangedHandler(DependencyObject dObj, FloatingContainerEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(FloatingContainerIsOpenChangedEvent, handler);
            }
        }

        public void ResetSizing()
        {
            this.canUpdateAutoSize = true;
            this.UpdateAutoSize();
        }

        protected virtual Point ScreenToLogical(Point point) => 
            point;

        public static void SetDialogOwner(DependencyObject obj, IDialogOwner value)
        {
            obj.SetValue(DialogOwnerProperty, value);
        }

        public static void SetFloatingContainer(DependencyObject obj, FloatingContainer value)
        {
            obj.SetValue(FloatingContainerProperty, value);
        }

        public static void SetIsActive(DependencyObject obj, bool value)
        {
            obj.SetValue(IsActiveProperty, value);
        }

        public static void SetIsMaximized(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMaximizedProperty, value);
        }

        private static void ShowDesignDialog(FrameworkElement dialogContent, FrameworkElement element, DialogClosedDelegate closedDelegate, Size size, string title, bool allowSizing, ImageSource icon)
        {
            DXWindow window1 = new DXWindow();
            window1.Title = title;
            window1.ResizeMode = allowSizing ? ResizeMode.CanResize : ResizeMode.NoResize;
            DXWindow local1 = window1;
            local1.Content = dialogContent;
            local1.ShowInTaskbar = false;
            local1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            local1.WindowStyle = WindowStyle.ToolWindow;
            Window window = local1;
            if (size != Size.Empty)
            {
                window.Width = size.Width;
                window.Height = size.Height;
                window.MinWidth = size.Width;
                window.MinHeight = size.Height;
            }
            Style style = new Style {
                TargetType = typeof(ScrollBar)
            };
            window.Resources.Add(typeof(ScrollBar), style);
            style = new Style {
                TargetType = typeof(ScrollViewer)
            };
            window.Resources.Add(typeof(ScrollViewer), style);
            style = new Style {
                TargetType = typeof(Separator)
            };
            window.Resources.Add(typeof(Separator), style);
            WindowContentHolder.SetHwndSourceOwner(window, element);
            ThemeManager.SetTheme(window, Theme.MetropolisLight);
            window.ShowDialog();
            if (closedDelegate != null)
            {
                closedDelegate(window.DialogResult);
            }
        }

        public static FloatingContainer ShowDialog(FrameworkElement dialogContent, FrameworkElement element, Size size, FloatingContainerParameters parameters) => 
            ShowDialog(dialogContent, element, size, parameters, null);

        public static FloatingContainer ShowDialog(FrameworkElement dialogContent, FrameworkElement element, Size size, FloatingContainerParameters parameters, DependencyObject owner)
        {
            if (DesignerProperties.GetIsInDesignMode(element))
            {
                ShowDesignDialog(dialogContent, element, parameters.ClosedDelegate, size, parameters.Title, parameters.AllowSizing, parameters.Icon);
                return null;
            }
            FloatingContainer container = FloatingContainerFactory.Create(FloatingContainerFactory.CheckPopupHost(element));
            if (owner != null)
            {
                SetFloatingContainer(owner, container);
            }
            size = InitDialog(dialogContent, element, parameters.ClosedDelegate, size, parameters.Title, parameters.AllowSizing, container, parameters.CloseOnEscape, parameters.Icon, parameters.ContainerFocusable, parameters.ShowModal);
            return container;
        }

        public static FloatingContainer ShowDialog(FrameworkElement dialogContent, FrameworkElement element, DialogClosedDelegate closedDelegate, Size size, string title, bool allowSizing)
        {
            FloatingContainerParameters parameters = new FloatingContainerParameters();
            parameters.ClosedDelegate = closedDelegate;
            parameters.Title = title;
            parameters.AllowSizing = allowSizing;
            parameters.CloseOnEscape = true;
            return ShowDialog(dialogContent, element, size, parameters);
        }

        public static FloatingContainer ShowDialogContent(FrameworkElement dialogContent, FrameworkElement rootElement, Size size, FloatingContainerParameters parameters) => 
            ShowDialogContent(dialogContent, rootElement, size, parameters, (DependencyObject) null);

        public static FloatingContainer ShowDialogContent(FrameworkElement dialogContent, FrameworkElement rootElement, Size size, FloatingContainerParameters parameters, bool useContentIndents) => 
            ShowDialogContent(dialogContent, rootElement, size, parameters, useContentIndents, null);

        public static FloatingContainer ShowDialogContent(FrameworkElement dialogContent, FrameworkElement rootElement, Size size, FloatingContainerParameters parameters, DependencyObject owner)
        {
            DialogControl control1 = new DialogControl();
            control1.DialogContent = dialogContent;
            control1.ShowApplyButton = parameters.ShowApplyButton;
            return ShowDialog(control1, rootElement, size, parameters, owner);
        }

        public static FloatingContainer ShowDialogContent(FrameworkElement dialogContent, FrameworkElement rootElement, Size size, FloatingContainerParameters parameters, bool useContentIndents, DependencyObject owner)
        {
            DialogControl control1 = new DialogControl();
            control1.DialogContent = dialogContent;
            control1.ShowApplyButton = parameters.ShowApplyButton;
            control1.UseContentIndents = useContentIndents;
            return ShowDialog(control1, rootElement, size, parameters, owner);
        }

        private void ShowModalAdorner()
        {
            ModalContainer = this;
            this.modalAdorner = FindModalAdorner(base.Owner);
            if (this.modalAdorner != null)
            {
                this.modalAdorner.Visibility = Visibility.Visible;
                this.modalAdorner.LockPreviewEvents(this.GetFloatingMode());
            }
        }

        public void UpdateAutoSize()
        {
            if (this.canUpdateAutoSize)
            {
                base.Dispatcher.BeginInvoke(new Action(this.UpdateAutoSizeCore), DispatcherPriority.Render, new object[0]);
            }
        }

        public void UpdateAutoSize(Action restoreSizeHandler, Action allowResizingHandler)
        {
            base.Dispatcher.BeginInvoke(delegate {
                this.UpdateAutoSizeCore();
                if (restoreSizeHandler != null)
                {
                    restoreSizeHandler();
                    this.isAutoSizeUpdating++;
                    this.UpdateFloatingBoundsCore(new Rect(this.FloatLocation, this.FloatSize));
                    this.isAutoSizeUpdating--;
                    allowResizingHandler();
                }
            }, DispatcherPriority.Render, new object[0]);
        }

        private void UpdateAutoSizeCore()
        {
            this.isAutoSizeUpdating++;
            this.UpdateFloatingBoundsCore(new Rect(base.FloatLocation, base.FloatSize));
            this.EnsureMinSize();
            this.isAutoSizeUpdating--;
        }

        public bool CloseOnEscape
        {
            get => 
                (bool) base.GetValue(CloseOnEscapeProperty);
            set => 
                base.SetValue(CloseOnEscapeProperty, value);
        }

        public bool ShowModal
        {
            get => 
                (bool) base.GetValue(ShowModalProperty);
            set => 
                base.SetValue(ShowModalProperty, value);
        }

        public WindowStartupLocation ContainerStartupLocation
        {
            get => 
                (WindowStartupLocation) base.GetValue(ContainerStartupLocationProperty);
            set => 
                base.SetValue(ContainerStartupLocationProperty, value);
        }

        public bool IsClosed
        {
            get => 
                (bool) base.GetValue(IsClosedProperty);
            private set => 
                base.SetValue(IsClosedPropertyKey, value);
        }

        public bool? DialogResult
        {
            get => 
                (bool?) base.GetValue(DialogResultProperty);
            set => 
                base.SetValue(DialogResultProperty, value);
        }

        public DataTemplate ContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContentTemplateProperty);
            set => 
                base.SetValue(ContentTemplateProperty, value);
        }

        public bool AllowMoving
        {
            get => 
                (bool) base.GetValue(AllowMovingProperty);
            set => 
                base.SetValue(AllowMovingProperty, value);
        }

        public bool AllowSizing
        {
            get => 
                (bool) base.GetValue(AllowSizingProperty);
            set => 
                base.SetValue(AllowSizingProperty, value);
        }

        public bool AllowShowAnimations
        {
            get => 
                (bool) base.GetValue(AllowShowAnimationsProperty);
            set => 
                base.SetValue(AllowShowAnimationsProperty, value);
        }

        public string Caption
        {
            get => 
                (string) base.GetValue(CaptionProperty);
            set => 
                base.SetValue(CaptionProperty, value);
        }

        public bool ContainerFocusable
        {
            get => 
                (bool) base.GetValue(ContainerFocusableProperty);
            set => 
                base.SetValue(ContainerFocusableProperty, value);
        }

        public ImageSource Icon
        {
            get => 
                (ImageSource) base.GetValue(IconProperty);
            set => 
                base.SetValue(IconProperty, value);
        }

        public bool ShowActivated
        {
            get => 
                (bool) base.GetValue(ShowActivatedProperty);
            set => 
                base.SetValue(ShowActivatedProperty, value);
        }

        public FrameworkElement LogicalOwner
        {
            get => 
                (FrameworkElement) base.GetValue(LogicalOwnerProperty);
            set => 
                base.SetValue(LogicalOwnerProperty, value);
        }

        public System.Windows.SizeToContent SizeToContent
        {
            get => 
                (System.Windows.SizeToContent) base.GetValue(SizeToContentProperty);
            set => 
                base.SetValue(SizeToContentProperty, value);
        }

        public bool ShowCloseButton
        {
            get => 
                (bool) base.GetValue(ShowCloseButtonProperty);
            set => 
                base.SetValue(ShowCloseButtonProperty, value);
        }

        protected bool InvertRTLSizeMove =>
            (base.Owner != null) && ((base.Owner.FlowDirection == base.FlowDirection) && (base.FlowDirection == FlowDirection.RightToLeft));

        protected bool InvertRTLMove =>
            (base.Owner != null) && (base.Owner.FlowDirection != base.storedFlowDirrection);

        public Size ActualSize { get; protected set; }

        protected bool IsClosingCanceled { get; private set; }

        protected static FloatingContainer ModalContainer { get; private set; }

        public static bool IsModalContainerOpened =>
            ModalContainer != null;

        public bool DeactivateOnClose { get; set; }

        internal bool UseScreenCoordinates { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FloatingContainer.<>c <>9 = new FloatingContainer.<>c();
            public static Predicate<Adorner> <>9__160_0;

            internal void <.cctor>b__39_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FloatingContainer) dObj).OnShowCloseButtonChanged();
            }

            internal bool <FindModalAdorner>b__160_0(Adorner adorner) => 
                adorner is FloatingContainer.ModalAdorner;
        }

        private class FloatingContainerHiddenHandler
        {
            private readonly FloatingContainer container;
            private readonly FrameworkElement rootElement;
            private readonly DialogClosedDelegate closedDelegate;

            public FloatingContainerHiddenHandler(FloatingContainer container, FrameworkElement rootElement, DialogClosedDelegate closedDelegate)
            {
                this.container = container;
                this.rootElement = rootElement;
                this.closedDelegate = closedDelegate;
            }

            public void HiddenHandler(object sender, RoutedEventArgs e)
            {
                if (this.closedDelegate != null)
                {
                    this.closedDelegate(this.container.DialogResult);
                }
                this.container.Hidden -= new RoutedEventHandler(this.HiddenHandler);
                if (this.rootElement is ILogicalOwner)
                {
                    ((ILogicalOwner) this.rootElement).RemoveChild(this.container);
                }
                else if (ReferenceEquals(LogicalTreeHelper.GetParent(this.container), this.rootElement))
                {
                    LogicalTreeIntruder.RemoveLogicalChild(this.container);
                }
            }
        }

        public class ModalAdorner : AdornerContainer
        {
            private readonly UIElement topContainer;
            private PreviewSubscriptions Subscriptions;

            public ModalAdorner(UIElement adornedElement) : base(adornedElement, border1)
            {
                Border border1 = new Border();
                border1.Background = Brushes.Transparent;
                WindowChrome.SetIsHitTestVisibleInChrome(base.Child, true);
                base.Visibility = Visibility.Hidden;
                this.Subscriptions = new PreviewSubscriptions();
                this.topContainer = LayoutHelper.GetTopContainerWithAdornerLayer(base.AdornedElement);
            }

            protected override Size ArrangeOverride(Size finalSize)
            {
                if (base.Visibility == Visibility.Visible)
                {
                    Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(base.AdornedElement, this.topContainer);
                    base.Child.Arrange(new Rect(new Point(-relativeElementRect.Left, -relativeElementRect.Top), this.topContainer.RenderSize));
                }
                return finalSize;
            }

            public void LockPreviewEvents(FloatingMode mode)
            {
                UIElement root = LayoutHelper.GetRoot(base.AdornedElement as FrameworkElement);
                if (!BrowserInteropHelper.IsBrowserHosted && (mode == FloatingMode.Window))
                {
                    this.Subscriptions.Add(root);
                }
                LogicalEnumerator enumerator = new LogicalEnumerator(root);
                while (enumerator.MoveNext())
                {
                    FloatingContainer current = enumerator.Current as FloatingContainer;
                    if ((current != null) && !ReferenceEquals(current, FloatingContainer.ModalContainer))
                    {
                        this.Subscriptions.Add(current.ContentContainer);
                    }
                }
            }

            public void UnLockPreviewEvents()
            {
                this.Subscriptions.Clear();
            }

            private class LogicalEnumerator : LogicalTreeEnumerator
            {
                internal LogicalEnumerator(DependencyObject root) : base(root)
                {
                }

                [IteratorStateMachine(typeof(<GetNestedObjects>d__1))]
                protected override IEnumerator GetNestedObjects(object obj)
                {
                    <GetNestedObjects>d__1 d__1 = new <GetNestedObjects>d__1(0);
                    d__1.obj = obj;
                    return d__1;
                }

                [CompilerGenerated]
                private sealed class <GetNestedObjects>d__1 : IEnumerator<object>, IDisposable, IEnumerator
                {
                    private int <>1__state;
                    private object <>2__current;
                    public object obj;
                    private IEnumerator <>7__wrap1;

                    [DebuggerHidden]
                    public <GetNestedObjects>d__1(int <>1__state)
                    {
                        this.<>1__state = <>1__state;
                    }

                    private void <>m__Finally1()
                    {
                        this.<>1__state = -1;
                        IDisposable disposable = this.<>7__wrap1 as IDisposable;
                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }
                    }

                    private bool MoveNext()
                    {
                        bool flag;
                        try
                        {
                            int num = this.<>1__state;
                            if (num == 0)
                            {
                                this.<>1__state = -1;
                                this.<>7__wrap1 = LogicalTreeHelper.GetChildren((DependencyObject) this.obj).GetEnumerator();
                                this.<>1__state = -3;
                            }
                            else if (num == 1)
                            {
                                this.<>1__state = -3;
                            }
                            else
                            {
                                return false;
                            }
                            while (true)
                            {
                                if (!this.<>7__wrap1.MoveNext())
                                {
                                    this.<>m__Finally1();
                                    this.<>7__wrap1 = null;
                                    flag = false;
                                }
                                else
                                {
                                    object current = this.<>7__wrap1.Current;
                                    if (!(current is DependencyObject))
                                    {
                                        continue;
                                    }
                                    this.<>2__current = current;
                                    this.<>1__state = 1;
                                    flag = true;
                                }
                                break;
                            }
                        }
                        fault
                        {
                            this.System.IDisposable.Dispose();
                        }
                        return flag;
                    }

                    [DebuggerHidden]
                    void IEnumerator.Reset()
                    {
                        throw new NotSupportedException();
                    }

                    [DebuggerHidden]
                    void IDisposable.Dispose()
                    {
                        int num = this.<>1__state;
                        if ((num == -3) || (num == 1))
                        {
                            try
                            {
                            }
                            finally
                            {
                                this.<>m__Finally1();
                            }
                        }
                    }

                    object IEnumerator<object>.Current =>
                        this.<>2__current;

                    object IEnumerator.Current =>
                        this.<>2__current;
                }
            }

            private class PreviewSubscriptions : IEnumerable<UIElement>, IEnumerable
            {
                private IList<UIElement> List = new List<UIElement>();

                public void Add(UIElement element)
                {
                    if ((element != null) && !this.List.Contains(element))
                    {
                        this.List.Add(element);
                        element.PreviewMouseDown += new MouseButtonEventHandler(this.OnPreviewMouseDown);
                        element.PreviewKeyDown += new KeyEventHandler(this.OnPreviewKeyDown);
                    }
                }

                public void Clear()
                {
                    foreach (UIElement element in this.List)
                    {
                        element.PreviewMouseDown -= new MouseButtonEventHandler(this.OnPreviewMouseDown);
                        element.PreviewKeyDown -= new KeyEventHandler(this.OnPreviewKeyDown);
                    }
                    this.List.Clear();
                }

                public IEnumerator<UIElement> GetEnumerator() => 
                    this.List.GetEnumerator();

                private void OnPreviewKeyDown(object sender, KeyEventArgs e)
                {
                    e.Handled = true;
                }

                private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
                {
                    e.Handled = true;
                }

                IEnumerator IEnumerable.GetEnumerator() => 
                    this.List.GetEnumerator();
            }
        }

        [TemplatePart(Name="PART_DragWidget", Type=typeof(Thumb)), TemplatePart(Name="PART_StatusPanel", Type=typeof(StackPanel)), TemplatePart(Name="PART_ContainerContent", Type=typeof(ContentPresenter)), TemplatePart(Name="PART_CloseButton", Type=typeof(Button)), TemplatePart(Name="PART_SizeGrip", Type=typeof(Thumb))]
        protected class ThemedManagedContentPresenter : BaseFloatingContainer.ManagedContentPresenter, IWindowResizeHelperClient
        {
            private Point prevAbsPos;

            public ThemedManagedContentPresenter(DevExpress.Xpf.Core.FloatingContainer container) : base(container)
            {
            }

            void IWindowResizeHelperClient.ActivePartMouseDown(object sender, MouseButtonEventArgs e)
            {
                FrameworkElement element = sender as FrameworkElement;
                if (element != null)
                {
                    element.CaptureMouse();
                    element.MouseMove += new MouseEventHandler(this.fe_MouseMove);
                    element.MouseUp += new MouseButtonEventHandler(this.fe_MouseUp);
                    this.prevAbsPos = this.GetAbsolutePosition(e.GetPosition(this));
                    e.Handled = true;
                }
            }

            FrameworkElement IWindowResizeHelperClient.GetVisualByName(string name) => 
                DXWindow.GetVisualByName(this, name) as FrameworkElement;

            private void fe_MouseMove(object sender, MouseEventArgs e)
            {
                FrameworkElement fe = sender as FrameworkElement;
                if (fe != null)
                {
                    if (e.MouseDevice.LeftButton != MouseButtonState.Pressed)
                    {
                        this.ResetSizing(fe);
                    }
                    else
                    {
                        string str = fe.Name.Substring(fe.Name.LastIndexOf("_") + 1);
                        DXWindowActiveResizeParts activePart = (DXWindowActiveResizeParts) Enum.Parse(typeof(DXWindowActiveResizeParts), str);
                        activePart = WindowResizeHelper.CorrectResizePart(base.FlowDirection, activePart);
                        Point absolutePosition = this.GetAbsolutePosition(e.GetPosition(this));
                        if (absolutePosition != this.prevAbsPos)
                        {
                            double num3;
                            double num4;
                            double num5;
                            double num6;
                            double absWChange = absolutePosition.X - this.prevAbsPos.X;
                            double absHChange = absolutePosition.Y - this.prevAbsPos.Y;
                            this.FloatingContainer.CalcOffsets(activePart, absWChange, absHChange, out num3, out num4, out num5, out num6);
                            Size floatSize = this.FloatingContainer.FloatSize;
                            this.FloatingContainer.ProcessSizing(new DragDeltaEventArgs(num5, num6));
                            Size size2 = this.FloatingContainer.FloatSize;
                            if ((floatSize.Width == 0.0) || (floatSize.Height == 0.0))
                            {
                                floatSize = size2;
                            }
                            double num7 = size2.Width - floatSize.Width;
                            double num8 = size2.Height - floatSize.Height;
                            num3 = Math.Abs(num7) * Math.Sign(num3);
                            num4 = Math.Abs(num8) * Math.Sign(num4);
                            DevExpress.Xpf.Core.FloatingContainer floatingContainer = this.FloatingContainer;
                            floatingContainer.lockInversion++;
                            if ((num3 != 0.0) || (num4 != 0.0))
                            {
                                this.FloatingContainer.ProcessMoving(new DragDeltaEventArgs(this.FloatingContainer.InvertRTLSizeMove ? -num3 : num3, num4));
                            }
                            DevExpress.Xpf.Core.FloatingContainer container2 = this.FloatingContainer;
                            container2.lockInversion--;
                            this.prevAbsPos.X = absolutePosition.X + (((num7 != 0.0) || (num3 != 0.0)) ? 0.0 : (num7 - absWChange));
                            this.prevAbsPos.Y = absolutePosition.Y + (((num8 != 0.0) || (num4 != 0.0)) ? 0.0 : (num8 - absHChange));
                            e.Handled = true;
                        }
                    }
                }
            }

            private void fe_MouseUp(object sender, MouseButtonEventArgs e)
            {
                FrameworkElement fe = sender as FrameworkElement;
                if (fe != null)
                {
                    this.ResetSizing(fe);
                }
            }

            private Point GetAbsolutePosition(Point point) => 
                ((this.FloatingContainer is FloatingAdornerContainer) || BrowserInteropHelper.IsBrowserHosted) ? this.GetRootVisualPosition(point) : this.GetScreenPosition(point);

            private Point GetRootVisualPosition(Point point)
            {
                UIElement relativeTo = LayoutHelper.FindRoot(this, false) as UIElement;
                return base.TranslatePoint(point, relativeTo);
            }

            private Point GetScreenPosition(Point point) => 
                base.PointToScreen(point);

            private void HideButton(string id)
            {
                Button visualByName = (Button) DXWindow.GetVisualByName(this, id);
                if (visualByName != null)
                {
                    visualByName.Visibility = Visibility.Collapsed;
                }
            }

            private void HideButtons()
            {
                this.HideButton("PART_Minimize");
                this.HideButton("PART_Maximize");
                this.HideButton("PART_Restore");
            }

            public override void OnApplyTemplate()
            {
                base.OnApplyTemplate();
                this.DelayedExecute(() => this.SubscribeEvents());
                base.Dispatcher.BeginInvoke(new Action(this.HideButtons), DispatcherPriority.Render, new object[0]);
            }

            private void ResetSizing(FrameworkElement fe)
            {
                fe.MouseMove -= new MouseEventHandler(this.fe_MouseMove);
                fe.MouseUp -= new MouseButtonEventHandler(this.fe_MouseUp);
                fe.ReleaseMouseCapture();
            }

            private void SubscribeEvents()
            {
                this.HideButtons();
                if (base.Container.UseActiveStateOnly)
                {
                    base.SetValue(DevExpress.Xpf.Core.FloatingContainer.IsActiveProperty, true);
                }
                else
                {
                    DependencyObject obj2 = LayoutHelper.FindRoot(this, false);
                    if ((obj2.GetType().FullName != "System.Windows.Controls.Primitives.PopupRoot") && (obj2.GetType().FullName != "DevExpress.Xpf.Core.NonLogicalDecorator"))
                    {
                        Binding binding = new Binding("IsActive");
                        binding.Source = LayoutHelper.FindRoot(this, false);
                        binding.Mode = BindingMode.OneWay;
                        base.SetBinding(DevExpress.Xpf.Core.FloatingContainer.IsActiveProperty, binding);
                    }
                }
                this.DragWidget = (Thumb) DXWindow.GetVisualByName(this, "PART_DragWidget");
                if (this.DragWidget != null)
                {
                    this.DragWidget.DragDelta += (o, e) => this.FloatingContainer.ProcessMoving(e);
                }
                this.SizeGrip = (Thumb) DXWindow.GetVisualByName(this, "PART_SizeGrip");
                if (this.SizeGrip != null)
                {
                    this.SizeGrip.DragDelta += (o, e) => this.FloatingContainer.ProcessSizing(e);
                }
                WindowResizeHelper.Subscribe(this);
                this.StatusPanel = (StackPanel) DXWindow.GetVisualByName(this, "PART_StatusPanel");
                if (this.StatusPanel != null)
                {
                    this.StatusPanel.FlowDirection = FlowDirection.LeftToRight;
                }
                this.CloseButton = (Button) DXWindow.GetVisualByName(this, "PART_CloseButton");
                if (this.CloseButton != null)
                {
                    this.CloseButton.Click += (o, e) => this.FloatingContainer.ProcessClosing();
                    this.UpdateCloseButtonVisibility(this.CloseButton);
                }
                this.ContainerContent = (FrameworkElement) DXWindow.GetVisualByName(this, "PART_ContainerContent");
                if (this.ContainerContent != null)
                {
                    Binding binding = new Binding();
                    binding.Source = base.Container;
                    binding.Path = new PropertyPath(DevExpress.Xpf.Core.FloatingContainer.ContentTemplateProperty);
                    this.ContainerContent.SetBinding(ContentPresenter.ContentTemplateProperty, binding);
                }
            }

            private void UpdateCloseButtonVisibility(Button closeButton)
            {
                if ((closeButton != null) && (this.FloatingContainer != null))
                {
                    closeButton.Visibility = this.FloatingContainer.ShowCloseButton ? Visibility.Visible : Visibility.Collapsed;
                }
            }

            protected internal override void UpdatePresenter()
            {
                base.UpdatePresenter();
                if ((this.FloatingContainer != null) && (this.CloseButton != null))
                {
                    this.UpdateCloseButtonVisibility(this.CloseButton);
                }
            }

            protected Thumb DragWidget { get; private set; }

            protected Thumb SizeGrip { get; private set; }

            protected Button CloseButton { get; private set; }

            protected StackPanel StatusPanel { get; private set; }

            protected FrameworkElement ContainerContent { get; private set; }

            public DevExpress.Xpf.Core.FloatingContainer FloatingContainer =>
                base.Container as DevExpress.Xpf.Core.FloatingContainer;
        }
    }
}

