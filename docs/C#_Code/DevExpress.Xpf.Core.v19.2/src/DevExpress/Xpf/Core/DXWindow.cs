namespace DevExpress.Xpf.Core
{
    using DevExpress.Data;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core.HandleDecorator;
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils.Themes;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Forms;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;

    public class DXWindow : Window, ILogicalOwner, IInputElement, IWindowResizeHelperClient, IHeaderItemsControlHost
    {
        public static readonly DependencyProperty IsDraggingOrResizingProperty;
        public static readonly DependencyProperty WindowTemplateProperty;
        public static readonly DependencyProperty AeroWindowTemplateProperty;
        protected static readonly DependencyPropertyKey ActualWindowTemplatePropertyKey;
        public static readonly DependencyProperty ActualWindowTemplateProperty;
        public static readonly DependencyProperty ShowIconProperty;
        public static readonly DependencyProperty ShowTitleProperty;
        public static readonly DependencyProperty SmallIconProperty;
        public static readonly DependencyProperty IsMaximizedProperty;
        public static readonly DependencyProperty IsActiveExProperty;
        public static readonly DependencyProperty AeroControlBoxWidthProperty;
        protected static readonly DependencyPropertyKey AeroControlBoxWidthPropertyKey;
        public static readonly DependencyProperty AeroControlBoxHeightProperty;
        protected static readonly DependencyPropertyKey AeroControlBoxHeightPropertyKey;
        public static readonly DependencyProperty AeroControlBoxMarginProperty;
        protected static readonly DependencyPropertyKey AeroControlBoxMarginPropertyKey;
        public static readonly DependencyProperty AeroBorderSizeProperty;
        public static readonly DependencyProperty IsAeroModeProperty;
        public static readonly DependencyProperty AllowApplicationIconScalingProperty;
        public static readonly DependencyProperty HeaderItemsSourceProperty;
        protected static readonly DependencyPropertyKey HeaderItemsPropertyKey;
        public static readonly DependencyProperty HeaderItemsProperty;
        public static readonly DependencyProperty HeaderItemContainerStyleProperty;
        public static readonly DependencyProperty HeaderItemContainerStyleSelectorProperty;
        public static readonly DependencyProperty HeaderItemTemplateProperty;
        public static readonly DependencyProperty HeaderItemTemplateSelectorProperty;
        private static readonly Action<Window, bool> set_updateHwndLocation;
        private static readonly Action<Window, bool> set_updateHwndSize;
        private static readonly Action<Window> updateDimensionsToRestoreBounds;
        public static readonly DependencyProperty BorderEffectLeftMarginsProperty = DependencyProperty.Register("BorderEffectLeftMargins", typeof(Thickness), typeof(DXWindow));
        public static readonly DependencyProperty BorderEffectRightMarginsProperty = DependencyProperty.Register("BorderEffectRightMargins", typeof(Thickness), typeof(DXWindow));
        public static readonly DependencyProperty BorderEffectTopMarginsProperty = DependencyProperty.Register("BorderEffectTopMargins", typeof(Thickness), typeof(DXWindow));
        public static readonly DependencyProperty BorderEffectBottomMarginsProperty = DependencyProperty.Register("BorderEffectBottomMargins", typeof(Thickness), typeof(DXWindow));
        public static readonly DependencyProperty BorderEffectImagesUriProperty = DependencyProperty.Register("BorderEffectImagesUri", typeof(TextBlock), typeof(DXWindow));
        public static readonly DependencyProperty BorderEffectOffsetProperty = DependencyProperty.Register("BorderEffectOffset", typeof(Thickness), typeof(DXWindow));
        public static readonly DependencyProperty BorderEffectActiveColorProperty = DependencyProperty.Register("BorderEffectActiveColor", typeof(SolidColorBrush), typeof(DXWindow), new FrameworkPropertyMetadata(new SolidColorBrush(), new PropertyChangedCallback(DXWindow.OnBorderEffectActiveColorPropertyChanged)));
        public static readonly DependencyProperty BorderEffectInactiveColorProperty = DependencyProperty.Register("BorderEffectInactiveColor", typeof(SolidColorBrush), typeof(DXWindow), new FrameworkPropertyMetadata(new SolidColorBrush(), new PropertyChangedCallback(DXWindow.OnBorderEffectInactiveColorPropertyChanged)));
        public static readonly DependencyProperty ResizeBorderThicknessProperty;
        public static readonly DependencyProperty ResizeBorderThicknessInAeroModeProperty;
        private Thickness actualResizeBorderThicknessCore;
        private bool allowChangeRenderModeCore = true;
        protected WindowInteropHelper interopHelperCore;
        protected System.Windows.Media.Color colorizationColorCore = System.Windows.SystemColors.ControlColor;
        private bool enableTransparencyCore;
        private WindowStyle originalWindowStyle;
        private static bool isAeroModeSupported;
        private bool isAeroModeEnabledCore;
        private System.Windows.Controls.Button closeButton;
        private static Matrix DeviceToDIPTransform;
        private static Matrix DIPToDeviceTransform;
        public static bool UpdateWindowRegionOnSourceInitialized;
        private ImageSource imageSource;
        private WindowState previousWindowState;
        private bool partFloatingContainerHeadersWasSet;
        internal Visibility restoreButtonVisibility;
        internal Visibility maximizeButtonVisibility;
        internal Visibility minimizeButtonVisibility;
        private int trueLeft;
        private int trueTop;
        private int magicNumber = -32000;
        private System.Windows.Size headerSizeCore;
        private bool isWindowSizing;
        private bool isProcessingDefWndProc;
        private bool stylePatched = true;
        private IntPtr region = IntPtr.Zero;
        private bool isClosing;
        private DispatcherTimer doubleClickTimer;
        private bool isDragMove;
        private const int MAX_PATH = 260;
        private bool dragWindowOrMaximizeWindowAsyncInvoked;
        private bool allowProcessContextMenu = true;
        public static readonly DependencyProperty AllowSystemMenuProperty;
        private DateTime lastSystemMenuShownTime = DateTime.Now;
        private EventHandlerList events;
        private DevExpress.Xpf.Core.HandleDecorator.Decorator decorator;
        private DevExpress.Xpf.Core.BorderEffect borderEffect = DevExpress.Xpf.Core.BorderEffect.None;

        event RoutedEventHandler ILogicalOwner.Loaded
        {
            add
            {
                base.Loaded += value;
            }
            remove
            {
                base.Loaded -= value;
            }
        }

        static DXWindow()
        {
            Thickness defaultValue = new Thickness();
            ResizeBorderThicknessProperty = DependencyProperty.Register("ResizeBorderThickness", typeof(Thickness), typeof(DXWindow), new FrameworkPropertyMetadata(defaultValue, new PropertyChangedCallback(DXWindow.OnResizeBorderThicknessPropertyChanged)));
            defaultValue = new Thickness();
            ResizeBorderThicknessInAeroModeProperty = DependencyProperty.Register("ResizeBorderThicknessInAeroMode", typeof(Thickness), typeof(DXWindow), new FrameworkPropertyMetadata(defaultValue, new PropertyChangedCallback(DXWindow.OnResizeBorderThicknessInAeroModePropertyChanged)));
            isAeroModeSupported = true;
            DeviceToDIPTransform = Matrix.Identity;
            DIPToDeviceTransform = Matrix.Identity;
            UpdateWindowRegionOnSourceInitialized = false;
            AllowSystemMenuProperty = DependencyProperty.Register("AllowSystemMenu", typeof(bool), typeof(DXWindow), new PropertyMetadata(true));
            isAeroModeSupported = CheckSupportAeroMode();
            UpdateTransformationMatrixes();
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DXWindow), new FrameworkPropertyMetadata(typeof(DXWindow)));
            IsDraggingOrResizingProperty = DependencyProperty.Register("IsDraggingOrResizing", typeof(bool), typeof(DXWindow), new UIPropertyMetadata(false));
            WindowTemplateProperty = DependencyProperty.Register("WindowTemplate", typeof(DataTemplate), typeof(DXWindow), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DXWindow.OnWindowTemplatePropertyChanged)));
            AeroWindowTemplateProperty = DependencyProperty.Register("AeroWindowTemplate", typeof(DataTemplate), typeof(DXWindow), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DXWindow.OnAeroWindowTemplatePropertyChanged)));
            ActualWindowTemplatePropertyKey = DependencyProperty.RegisterReadOnly("ActualWindowTemplate", typeof(DataTemplate), typeof(DXWindow), new FrameworkPropertyMetadata(null));
            ActualWindowTemplateProperty = ActualWindowTemplatePropertyKey.DependencyProperty;
            ShowIconProperty = DependencyProperty.RegisterAttached("ShowIcon", typeof(bool), typeof(DXWindow), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits, null));
            AllowApplicationIconScalingProperty = DependencyProperty.Register("AllowApplicationIconScaling", typeof(bool), typeof(DXWindow), new FrameworkPropertyMetadata(true));
            ShowTitleProperty = DependencyProperty.RegisterAttached("ShowTitle", typeof(bool), typeof(DXWindow), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsArrange, new PropertyChangedCallback(DXWindow.OnShowTitlePropertyChanged)));
            SmallIconProperty = DependencyProperty.Register("SmallIcon", typeof(ImageSource), typeof(DXWindow), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DXWindow.OnSmallIconPropertyChanged)));
            IsMaximizedProperty = FloatingContainer.IsMaximizedProperty.AddOwner(typeof(DXWindow), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(DXWindow.OnIsMaximizedChanged)));
            IsActiveExProperty = FloatingContainer.IsActiveProperty.AddOwner(typeof(DXWindow), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(DXWindow.OnIsActivePropertyChanged)));
            AeroControlBoxWidthPropertyKey = DependencyProperty.RegisterAttachedReadOnly("AeroControlBoxWidth", typeof(double), typeof(DXWindow), new PropertyMetadata((double) 1.0 / (double) 0.0));
            AeroControlBoxWidthProperty = AeroControlBoxWidthPropertyKey.DependencyProperty;
            AeroControlBoxHeightPropertyKey = DependencyProperty.RegisterAttachedReadOnly("AeroControlBoxHeight", typeof(double), typeof(DXWindow), new PropertyMetadata((double) 1.0 / (double) 0.0));
            AeroControlBoxHeightProperty = AeroControlBoxHeightPropertyKey.DependencyProperty;
            defaultValue = new Thickness();
            AeroControlBoxMarginPropertyKey = DependencyProperty.RegisterAttachedReadOnly("AeroControlBoxMargin", typeof(Thickness), typeof(DXWindow), new PropertyMetadata(defaultValue));
            AeroControlBoxMarginProperty = AeroControlBoxMarginPropertyKey.DependencyProperty;
            AeroBorderSizeProperty = DependencyProperty.Register("AeroBorderSize", typeof(double), typeof(DXWindow), new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(DXWindow.OnAeroBorderSizePropertyChanged)));
            IsAeroModeProperty = DependencyProperty.Register("IsAeroMode", typeof(bool), typeof(DXWindow), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(DXWindow.OnIsAeroModePropertyChanged), new CoerceValueCallback(DXWindow.OnIsAeroModePropertyCoerce)));
            System.Windows.Controls.Control.PaddingProperty.OverrideMetadata(typeof(DXWindow), new FrameworkPropertyMetadata(null, new CoerceValueCallback(DXWindow.CoercePaddingProperty)));
            HeaderItemsPropertyKey = DependencyProperty.RegisterReadOnly("HeaderItems", typeof(ObservableCollection<object>), typeof(DXWindow), new FrameworkPropertyMetadata(null));
            HeaderItemsProperty = HeaderItemsPropertyKey.DependencyProperty;
            HeaderItemsSourceProperty = DependencyProperty.Register("HeaderItemsSource", typeof(IEnumerable), typeof(DXWindow), new FrameworkPropertyMetadata(null));
            HeaderItemContainerStyleProperty = DependencyProperty.Register("HeaderItemContainerStyle", typeof(Style), typeof(DXWindow), new FrameworkPropertyMetadata(null));
            HeaderItemContainerStyleSelectorProperty = DependencyProperty.Register("HeaderItemContainerStyleSelector", typeof(StyleSelector), typeof(DXWindow), new FrameworkPropertyMetadata(null));
            HeaderItemTemplateProperty = DependencyProperty.Register("HeaderItemTemplate", typeof(DataTemplate), typeof(DXWindow), new FrameworkPropertyMetadata(null));
            HeaderItemTemplateSelectorProperty = DependencyProperty.Register("HeaderItemTemplateSelector", typeof(DataTemplateSelector), typeof(DXWindow), new FrameworkPropertyMetadata(null));
            UIElement.VisibilityProperty.AddOwner(typeof(DXWindow), new FrameworkPropertyMetadata(Visibility.Collapsed, new PropertyChangedCallback(DXWindow.OnVisibilityChanged)));
            FrameworkElement.UseLayoutRoundingProperty.AddOwner(typeof(DXWindow), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(DXWindow.OnUseLayoutRoundingPropertyChanged)));
            Window.WindowStateProperty.AddOwner(typeof(DXWindow), new FrameworkPropertyMetadata(WindowState.Normal, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(DXWindow.OnWindowStateChanged)));
            System.Windows.Controls.Panel.BackgroundProperty.AddOwner(typeof(DXWindow), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(DXWindow.OnWindowBackgroundChanged)));
            Window.WindowStyleProperty.AddOwner(typeof(DXWindow), new FrameworkPropertyMetadata(WindowStyle.SingleBorderWindow, new PropertyChangedCallback(DXWindow.OnWindowStyleChanged)));
            Window.ResizeModeProperty.AddOwner(typeof(DXWindow), new FrameworkPropertyMetadata(ResizeMode.CanResize, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(DXWindow.OnResizeModeChanged)));
            Window.IconProperty.AddOwner(typeof(DXWindow), new FrameworkPropertyMetadata(new PropertyChangedCallback(DXWindow.OnIconChanged)));
            EventManager.RegisterClassHandler(typeof(UIElement), FrameworkElement.ContextMenuOpeningEvent, new ContextMenuEventHandler(DXWindow.OnContextMenuOpening));
            if (!Net462Detector.IsNet462())
            {
                set_updateHwndLocation = (window, b) => ((DXWindow) window).Set_updateHwndLocation(b);
                set_updateHwndSize = (window, b) => ((DXWindow) window).Set_updateHwndSize(b);
                updateDimensionsToRestoreBounds = delegate (Window window) {
                    Rect restoreBounds = window.RestoreBounds;
                    window.SetValue(Window.LeftProperty, restoreBounds.Left);
                    window.SetValue(Window.TopProperty, restoreBounds.Top);
                    window.SetValue(FrameworkElement.WidthProperty, restoreBounds.Width);
                    window.SetValue(FrameworkElement.HeightProperty, restoreBounds.Height);
                };
            }
            else
            {
                set_updateHwndLocation = ReflectionHelper.CreateFieldSetter<Window, bool>(typeof(Window), "_updateHwndLocation", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                set_updateHwndSize = ReflectionHelper.CreateFieldSetter<Window, bool>(typeof(Window), "_updateHwndSize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                int? parametersCount = null;
                updateDimensionsToRestoreBounds = ReflectionHelper.CreateInstanceMethodHandler<Window, Action<Window>>(null, "UpdateDimensionsToRestoreBounds", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, parametersCount, null, true);
            }
        }

        public DXWindow()
        {
            DXWindowsHelper.SetWindowKind(this, "DXWindow");
            DXWindowsHelper.SetWindow(this, this);
            System.Windows.Data.Binding binding = new System.Windows.Data.Binding();
            binding.Path = new PropertyPath(System.Windows.Controls.Control.PaddingProperty);
            binding.Source = this;
            base.SetBinding(FloatingContainerControl.ContentOffsetProperty, binding);
            this.UpdateIsAeroModeEnabled();
            base.Loaded += new RoutedEventHandler(this.DXWindow_Loaded);
            this.CreateHeaderItems();
        }

        private void ActivePartMouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((base.ResizeMode != ResizeMode.NoResize) && ((base.ResizeMode != ResizeMode.CanMinimize) && (base.WindowState == WindowState.Normal)))
            {
                FrameworkElement element = sender as FrameworkElement;
                if (element != null)
                {
                    string str = element.Name.Substring(element.Name.LastIndexOf("_") + 1);
                    DXWindowActiveResizeParts activePart = (DXWindowActiveResizeParts) Enum.Parse(typeof(DXWindowActiveResizeParts), str);
                    activePart = WindowResizeHelper.CorrectResizePart(base.FlowDirection, activePart);
                    ResizeWindow(this.interopHelperCore.Handle, activePart);
                }
            }
        }

        [SecuritySafeCritical]
        private void AdjustSystemMenu(IntPtr hmenu)
        {
            if (base.ResizeMode == ResizeMode.NoResize)
            {
                DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf020, 1);
                DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf030, 1);
                DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf060, 0);
                DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf120, 1);
                DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf000, 1);
                this.InvalidateWindowCaption();
            }
            else if (base.ResizeMode == ResizeMode.CanMinimize)
            {
                DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf020, 0);
                DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf030, 1);
                DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf060, 0);
                DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf120, 1);
                DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf000, 1);
                this.InvalidateWindowCaption();
            }
            else
            {
                bool flag2 = base.WindowState != WindowState.Minimized;
                bool flag3 = base.WindowState != WindowState.Maximized;
                bool flag4 = base.WindowState != WindowState.Normal;
                bool flag5 = (((this.GetWindowStyleCore() == WindowStyle.ToolWindow) || (this.GetWindowStyleCore() != WindowStyle.None)) && (base.WindowState != WindowState.Minimized)) && (base.WindowState != WindowState.Maximized);
                if (!flag2)
                {
                    DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf020, 1);
                }
                else
                {
                    DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf020, 0);
                }
                if (!flag3)
                {
                    DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf030, 1);
                }
                else
                {
                    DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf030, 0);
                }
                DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf060, 0);
                if (!flag4)
                {
                    DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf120, 1);
                    DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf010, 0);
                }
                else
                {
                    DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf120, 0);
                    DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf010, 1);
                }
                if (!flag5)
                {
                    DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf000, 1);
                }
                else
                {
                    DevExpress.Xpf.Core.NativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 0xf000, 0);
                }
                this.InvalidateWindowCaption();
            }
        }

        protected virtual bool AllowOptimizeUpdateRootMargins() => 
            true;

        protected override System.Windows.Size ArrangeOverride(System.Windows.Size arrangeBounds)
        {
            if (this.VisualChildrenCount > 0)
            {
                FrameworkElement visualChild = this.GetVisualChild(0) as FrameworkElement;
                if (visualChild == null)
                {
                    return base.ArrangeOverride(arrangeBounds);
                }
                visualChild.Arrange(new Rect(arrangeBounds));
                visualChild.LayoutTransform = (base.FlowDirection != System.Windows.FlowDirection.RightToLeft) ? null : new MatrixTransform(-1.0, 0.0, 0.0, 1.0, arrangeBounds.Width, 0.0);
            }
            return arrangeBounds;
        }

        [SecuritySafeCritical]
        private void AttachToIcon()
        {
            this.IconImage = this.GetVisualByName(2.ToString()) as System.Windows.Controls.Image;
            if (this.IconImage != null)
            {
                System.Windows.Data.Binding binding1 = new System.Windows.Data.Binding();
                binding1.Path = new PropertyPath(SmallIconProperty);
                binding1.Source = this;
                System.Windows.Data.Binding binding = binding1;
                System.Windows.Data.Binding binding3 = new System.Windows.Data.Binding();
                binding3.Path = new PropertyPath(ShowIconProperty);
                binding3.Source = this;
                DevExpress.Mvvm.UI.BooleanToVisibilityConverter converter1 = new DevExpress.Mvvm.UI.BooleanToVisibilityConverter();
                converter1.Inverse = false;
                binding3.Converter = converter1;
                binding3.Mode = BindingMode.TwoWay;
                System.Windows.Data.Binding binding2 = binding3;
                this.IconImage.SetBinding(UIElement.VisibilityProperty, binding2);
                this.IconImage.SetBinding(System.Windows.Controls.Image.SourceProperty, binding);
                double dpiFactor = this.GetDpiFactor();
                if (this.AllowApplicationIconScaling)
                {
                    this.IconImage.Width = this.GetIconWidth() * dpiFactor;
                    this.IconImage.Height = this.GetIconHeight() * dpiFactor;
                }
                else
                {
                    this.IconImage.Width = this.GetIconWidth();
                    this.IconImage.Height = this.GetIconHeight();
                }
                this.IconImage.SnapsToDevicePixels = true;
                this.IconImage.IsHitTestVisible = true;
                this.IconImage.PreviewMouseDown += new MouseButtonEventHandler(this.IconMouseProcessing);
            }
        }

        private void AttachToText()
        {
            TextBlock visualByName = this.GetVisualByName(3.ToString()) as TextBlock;
            if (visualByName != null)
            {
                System.Windows.Data.Binding binding1 = new System.Windows.Data.Binding();
                binding1.Path = new PropertyPath(Window.TitleProperty);
                binding1.Source = this;
                System.Windows.Data.Binding binding = binding1;
                visualByName.SetBinding(TextBlock.TextProperty, binding);
                visualByName.Visibility = GetShowTitle(this) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        protected void AttachToVisualTree()
        {
            WindowResizeHelper.Subscribe(this);
            OnWindowBackgroundChanged(this, new DependencyPropertyChangedEventArgs(System.Windows.Controls.Panel.BackgroundProperty, null, base.Background));
            this.FloatingContainerHeader = this.GetVisualByName(4.ToString()) as FrameworkElement;
            this.HeaderButtons = this.GetVisualByName(5.ToString()) as ItemsControl;
            FrameworkElement visualByName = this.GetVisualByName(0.ToString()) as FrameworkElement;
            FrameworkElement element2 = this.GetVisualByName(7.ToString()) as FrameworkElement;
            if (visualByName != null)
            {
                visualByName.PreviewMouseDown += new MouseButtonEventHandler(this.DragWindowOrMaximizeWindow);
            }
            if (element2 != null)
            {
                element2.PreviewMouseDown += new MouseButtonEventHandler(this.DragWindowOrMaximizeWindow);
            }
            foreach (string str in Enum.GetNames(typeof(ButtonParts)))
            {
                System.Windows.Controls.Button button = this.GetVisualByName(str) as System.Windows.Controls.Button;
                if (((ButtonParts) Enum.Parse(typeof(ButtonParts), str)) == ButtonParts.PART_CloseButton)
                {
                    if (this.closeButton != null)
                    {
                        this.closeButton.Click -= new RoutedEventHandler(this.ButtonPartClick);
                    }
                    this.closeButton = button;
                    if (this.closeButton == null)
                    {
                        return;
                    }
                    this.closeButton.IsVisibleChanged += new DependencyPropertyChangedEventHandler(this.closeButton_IsVisibleChanged);
                }
                if (button != null)
                {
                    button.Click += new RoutedEventHandler(this.ButtonPartClick);
                }
            }
            this.DoResetVisibility();
            this.AttachToIcon();
            this.AttachToText();
            this.DoWindowStyleChangedUpdate(this.GetWindowStyleCore());
            this.UpdateRootMargins(GetIsMaximized(this));
            this.UpdateFloatPaneBorders(GetIsMaximized(this));
            if (base.Owner != null)
            {
                base.SetCurrentValue(FrameworkElement.UseLayoutRoundingProperty, base.Owner.UseLayoutRounding);
            }
        }

        public void BorderEffectReset()
        {
            base.ClearValue(BorderEffectActiveColorProperty);
            base.ClearValue(BorderEffectInactiveColorProperty);
            this.RedrawBorderWithEffectColor(this.ActiveState, false);
        }

        private void ButtonPartClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                switch (((ButtonParts) Enum.Parse(typeof(ButtonParts), button.Name)))
                {
                    case ButtonParts.PART_CloseButton:
                        if (this.isClosing)
                        {
                            break;
                        }
                        this.isClosing = true;
                        this.DoubleClickTimer.Start();
                        this.OnClose();
                        return;

                    case ButtonParts.PART_Minimize:
                        this.OnMinimize();
                        return;

                    case ButtonParts.PART_Restore:
                        this.OnRestore();
                        return;

                    case ButtonParts.PART_Maximize:
                        this.OnMaximize();
                        break;

                    default:
                        return;
                }
            }
        }

        protected Thickness CalcRootMargins()
        {
            Thickness screenMargins = this.GetScreenMargins(this.WindowRect);
            if (base.WindowStyle == WindowStyle.None)
            {
                screenMargins = this.SetTicknessWithoutTaskBar(screenMargins);
            }
            if (base.ResizeMode == ResizeMode.NoResize)
            {
                screenMargins = new Thickness(0.0);
            }
            double defaultValue = (double) FrameworkElement.MaxWidthProperty.GetMetadata(typeof(FrameworkElement)).DefaultValue;
            double num2 = (double) FrameworkElement.MaxHeightProperty.GetMetadata(typeof(FrameworkElement)).DefaultValue;
            if (base.MaxWidth != defaultValue)
            {
                screenMargins.Right = 0.0;
            }
            if (base.MaxHeight != num2)
            {
                screenMargins.Bottom = 0.0;
            }
            return screenMargins;
        }

        [SecuritySafeCritical]
        private static IntPtr CallDefWindowProcWithoutVisibleStyle(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            IntPtr ptr = DevExpress.Xpf.Core.NativeMethods.DefWindowProc(hWnd, msg, wParam, lParam);
            if (UtilityMethods.ModifyStyle(hWnd, 0x10000000, 0))
            {
                UtilityMethods.ModifyStyle(hWnd, 0, 0x10000000);
            }
            handled = true;
            return ptr;
        }

        private bool CanPatchLeftTop() => 
            (this.trueLeft != this.magicNumber) && (this.trueTop != this.magicNumber);

        protected void ChangeRenderMode(RenderMode newMode)
        {
            if (this.AllowChangeRenderMode)
            {
                HwndTarget hwndTarget = this.GetHwndTarget();
                if (hwndTarget != null)
                {
                    hwndTarget.RenderMode = newMode;
                }
            }
        }

        private static bool CheckSupportAeroMode()
        {
            if (Environment.OSVersion.Version.Major < 6)
            {
                return false;
            }
            try
            {
                DwmIsCompositionEnabled();
            }
            catch (DllNotFoundException)
            {
                return false;
            }
            return true;
        }

        [SecuritySafeCritical]
        private void CheckSystemIcon()
        {
            if (!this.SetAppIcon(this.interopHelperCore.Handle))
            {
                this.SetDefaultIcon();
            }
        }

        private void closeButton_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.DelayedExecuteEnqueue(delegate {
                if (this.NeedReAttachToVisualTree)
                {
                    this.AttachToVisualTree();
                }
            });
        }

        protected virtual bool CoerceIsAeroModeValue(bool baseValue) => 
            this.IsAeroModeEnabled & baseValue;

        private Thickness CoercePadding(Thickness thickness) => 
            (base.WindowStyle != WindowStyle.None) ? thickness : new Thickness(thickness.Left);

        private static object CoercePaddingProperty(DependencyObject d, object value) => 
            ((DXWindow) d).CoercePadding((Thickness) value);

        private DevExpress.Xpf.Core.HandleDecorator.Decorator CreateFormHandleDecorator()
        {
            StructDecoratorMargins structDecoratorMargins = new StructDecoratorMargins {
                LeftMargins = this.BorderEffectLeftMargins,
                RightMargins = this.BorderEffectRightMargins,
                TopMargins = this.BorderEffectTopMargins,
                BottomMargins = this.BorderEffectBottomMargins
            };
            return new FormHandleDecorator(this.BorderEffectActiveColor, this.BorderEffectInactiveColor, this.BorderEffectOffset, structDecoratorMargins, this.ActiveState);
        }

        private void CreateHeaderItems()
        {
            base.SetValue(HeaderItemsPropertyKey, new ObservableCollection<object>());
        }

        void ILogicalOwner.AddChild(object obj)
        {
            DependencyObject current = obj as DependencyObject;
            if ((current != null) && (LogicalTreeHelper.GetParent(current) == null))
            {
                base.AddLogicalChild(obj);
            }
        }

        void ILogicalOwner.RemoveChild(object obj)
        {
            base.RemoveLogicalChild(obj);
        }

        void IWindowResizeHelperClient.ActivePartMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ActivePartMouseDown(sender, e);
        }

        FrameworkElement IWindowResizeHelperClient.GetVisualByName(string name) => 
            this.GetVisualByName(name) as FrameworkElement;

        private IntPtr DoNCHitTest(IntPtr lParam)
        {
            int num = 1;
            if (base.WindowState == WindowState.Maximized)
            {
                return new IntPtr(1);
            }
            System.Windows.Point point = new System.Windows.Point((double) DevExpress.Xpf.Core.NativeMethods.SignedLOWORD(lParam), (double) DevExpress.Xpf.Core.NativeMethods.SignedHIWORD(lParam));
            System.Windows.Point point2 = DIPToDeviceTransform.Transform(new System.Windows.Point(base.Left, base.Top));
            System.Windows.Point point3 = DIPToDeviceTransform.Transform(new System.Windows.Point(base.ActualWidth, base.ActualHeight));
            System.Windows.Point point4 = new System.Windows.Point(point2.X + point3.X, point2.Y + point3.Y);
            Rect rect = new Rect(point2.X, point2.Y, point4.X - point2.X, point4.Y - point2.Y);
            System.Windows.Point point5 = DIPToDeviceTransform.Transform(new System.Windows.Point(this.ActualResizeBorderThickness.Left, this.ActualResizeBorderThickness.Top));
            System.Windows.Point point6 = DIPToDeviceTransform.Transform(new System.Windows.Point(this.ActualResizeBorderThickness.Right, this.ActualResizeBorderThickness.Bottom));
            Thickness thickness = new Thickness(point5.X, point5.Y, point6.X, point6.Y);
            if ((point.X >= rect.Left) && (point.X <= (rect.Left + thickness.Left)))
            {
                num = ((point.Y < rect.Top) || (point.Y >= (rect.Top + thickness.Top))) ? (((point.Y >= (rect.Top + rect.Height)) || (point.Y < ((rect.Top + rect.Height) - thickness.Bottom))) ? 10 : 0x10) : 13;
            }
            else if ((point.X <= (rect.Left + rect.Width)) && (point.X >= ((rect.Left + rect.Width) - thickness.Right)))
            {
                num = ((point.Y < rect.Top) || (point.Y >= (rect.Top + thickness.Top))) ? (((point.Y >= (rect.Top + rect.Height)) || (point.Y < ((rect.Top + rect.Height) - thickness.Bottom))) ? 11 : 0x11) : 14;
            }
            else if ((point.Y >= rect.Top) && (point.Y < (rect.Top + thickness.Top)))
            {
                num = 12;
            }
            else if ((point.Y < (rect.Top + rect.Height)) && (point.Y >= ((rect.Top + rect.Height) - thickness.Bottom)))
            {
                num = 15;
            }
            if ((num == 12) || (num == 13))
            {
                Rect empty = Rect.Empty;
                string[] names = Enum.GetNames(typeof(ButtonParts));
                int index = 0;
                while (true)
                {
                    if (index >= names.Length)
                    {
                        if (empty.Contains(point))
                        {
                            num = 1;
                        }
                        break;
                    }
                    string name = names[index];
                    FrameworkElement visualByName = this.GetVisualByName(name) as FrameworkElement;
                    if ((visualByName != null) && (visualByName.Visibility == Visibility.Visible))
                    {
                        empty = Rect.Union(empty, new Rect(visualByName.TranslatePoint(new System.Windows.Point(point2.X, point2.Y), null), new System.Windows.Size(visualByName.ActualWidth, visualByName.ActualHeight)));
                    }
                    index++;
                }
            }
            return new IntPtr(num);
        }

        [SecuritySafeCritical]
        private IntPtr DoNonClientHitTest(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            IntPtr plResult = new IntPtr(0);
            handled = DevExpress.Xpf.Core.NativeMethods.DwmDefWindowProc(hwnd, msg, wParam, lParam, ref plResult);
            if (!(plResult == IntPtr.Zero))
            {
                return plResult;
            }
            handled = true;
            return this.DoNCHitTest(lParam);
        }

        protected void DoResetVisibility()
        {
            this.ResetElementVisibility(4.ToString());
            this.ResetElementVisibility(3.ToString());
            this.ResetElementVisibility(1.ToString());
            this.ResetElementVisibility(2.ToString());
        }

        protected void DoWindowStyleChangedUpdate(WindowStyle newStyle)
        {
            base.CoerceValue(System.Windows.Controls.Control.PaddingProperty);
            this.DoResetVisibility();
            this.SetElementsVisilbility();
            switch (newStyle)
            {
                case WindowStyle.None:
                    this.SetElementVisibility(4.ToString(), Visibility.Collapsed);
                    this.UpdateNoResizeAndWSNone();
                    return;

                case WindowStyle.SingleBorderWindow:
                case WindowStyle.ThreeDBorderWindow:
                    break;

                case WindowStyle.ToolWindow:
                    this.SetElementVisibility(2.ToString(), Visibility.Collapsed);
                    this.SetElementVisibility(3.ToString(), Visibility.Collapsed, true);
                    this.SetElementVisibility(1.ToString(), Visibility.Collapsed);
                    this.SetElementVisibility(2.ToString(), Visibility.Collapsed, true);
                    break;

                default:
                    return;
            }
        }

        [SecuritySafeCritical]
        private void DragMoveInternal(IntPtr handle)
        {
            if (!this.isWindowSizing && ((base.WindowState == WindowState.Normal) || ((base.WindowState == WindowState.Maximized) && IsWinSevenOrHigher)))
            {
                this.isDragMove = true;
                Win32.SendMessage(handle, 0x112, 0xf012, IntPtr.Zero);
                Win32.SendMessage(handle, 0x202, 0, IntPtr.Zero);
            }
        }

        protected internal void DragWindowOrMaximizeWindow(object sender, MouseButtonEventArgs e)
        {
            if (!e.Handled)
            {
                if ((!this.CanResizeOrMaximize || (e.ClickCount != 2)) && (e.LeftButton == MouseButtonState.Pressed))
                {
                    this.DragMoveInternal(this.interopHelperCore.Handle);
                }
                if (!this.dragWindowOrMaximizeWindowAsyncInvoked)
                {
                    if (e.RightButton == MouseButtonState.Pressed)
                    {
                        e.Handled = true;
                    }
                    object[] args = new object[] { e };
                    base.Dispatcher.BeginInvoke(new Action<MouseButtonEventArgs>(this.DragWindowOrMaximizeWindowAsync), args);
                    this.dragWindowOrMaximizeWindowAsyncInvoked = true;
                }
            }
        }

        protected void DragWindowOrMaximizeWindowAsync(MouseButtonEventArgs e)
        {
            try
            {
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    System.Windows.Point pos = System.Windows.Point.Add(e.GetPosition(this), new Vector((double) this.trueLeft, (double) this.trueTop));
                    this.ShowSystemMenuOnRightClick(pos);
                }
                else if (this.CanResizeOrMaximize && (e.ClickCount == 2))
                {
                    base.SizeToContent = SizeToContent.Manual;
                    this.WindowState = (base.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
                }
            }
            finally
            {
                this.dragWindowOrMaximizeWindowAsyncInvoked = false;
            }
        }

        [SecuritySafeCritical]
        private static bool DwmIsCompositionEnabled() => 
            DevExpress.Xpf.Core.NativeMethods.DwmIsCompositionEnabled();

        private void DXWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.NeedReAttachToVisualTree)
            {
                this.AttachToVisualTree();
            }
            this.RedrawBorderWithEffectColor(this.ActiveState, false);
            this.UpdateRootMargins(base.WindowState == WindowState.Maximized);
            this.DelayedExecute(delegate {
                if (this.NeedReAttachToVisualTree)
                {
                    this.AttachToVisualTree();
                }
                if (base.WindowState != WindowState.Normal)
                {
                    OnWindowStateChanged(this, new DependencyPropertyChangedEventArgs(Window.WindowStateProperty, WindowState.Normal, base.WindowState));
                    if (this.NeedReAttachToVisualTree)
                    {
                        this.AttachToVisualTree();
                    }
                }
                if (base.ResizeMode == ResizeMode.NoResize)
                {
                    base.ResizeMode = ResizeMode.CanResize;
                    base.UpdateLayout();
                    base.ResizeMode = ResizeMode.NoResize;
                }
            });
        }

        private static bool DXWindowTreeStop(DependencyObject child) => 
            ((string) child.GetValue(FrameworkElement.NameProperty)) == "PART_ContainerContent";

        protected virtual Thickness GetAeroBorderThickness()
        {
            double left = -1.0;
            return new Thickness(left, (this.HeaderSize.Height != 0.0) ? this.HeaderSize.Height : this.AeroBorderSize, left, left);
        }

        public static double GetAeroControlBoxHeight(DependencyObject obj) => 
            (double) obj.GetValue(AeroControlBoxHeightProperty);

        public static Thickness GetAeroControlBoxMargin(DependencyObject obj) => 
            (Thickness) obj.GetValue(AeroControlBoxMarginProperty);

        public static double GetAeroControlBoxWidth(DependencyObject obj) => 
            (double) obj.GetValue(AeroControlBoxWidthProperty);

        [SecuritySafeCritical]
        protected int GetAppIconCore(IntPtr hwnd, int iconType, int iconHandle) => 
            (iconHandle != 0) ? iconHandle : Win32.SendMessage(hwnd, 0x7f, iconType, IntPtr.Zero);

        protected virtual Rect GetClientArea()
        {
            FrameworkElement element = LayoutHelper.FindElementByName(this, "PART_ContainerContent");
            if (element == null)
            {
                return Rect.Empty;
            }
            Rect rect = element.TransformToVisual(this).TransformBounds(LayoutInformation.GetLayoutSlot(element));
            if (base.WindowState == WindowState.Maximized)
            {
                rect = new Rect(new System.Windows.Point(rect.Left - this.ActualResizeBorderThickness.Left, rect.Top - this.ActualResizeBorderThickness.Top), rect.Size);
            }
            return rect;
        }

        private static DevExpress.Xpf.Core.NativeMethods.RECT GetClientRectRelativeToWindowRect(IntPtr hWnd)
        {
            DevExpress.Xpf.Core.NativeMethods.RECT rect;
            DevExpress.Xpf.Core.NativeMethods.RECT rect2;
            DevExpress.Xpf.Core.NativeMethods.GetWindowRect(hWnd, out rect);
            DevExpress.Xpf.Core.NativeMethods.GetClientRect(hWnd, out rect2);
            DevExpress.Xpf.Core.NativeMethods.POINT point = new DevExpress.Xpf.Core.NativeMethods.POINT {
                x = 0,
                y = 0
            };
            DevExpress.Xpf.Core.NativeMethods.ClientToScreen(hWnd, ref point);
            rect2.Offset(point.x - rect.left, point.y - rect.top);
            return rect2;
        }

        public Rect GetControlBoxRect()
        {
            if (this.interopHelperCore != null)
            {
                return GetControlBoxRect(this.interopHelperCore.Handle);
            }
            return new Rect();
        }

        [SecuritySafeCritical]
        private static Rect GetControlBoxRect(IntPtr handle)
        {
            DevExpress.Xpf.Core.NativeMethods.TITLEBARINFOEX lParam = new DevExpress.Xpf.Core.NativeMethods.TITLEBARINFOEX {
                cbSize = Marshal.SizeOf(typeof(DevExpress.Xpf.Core.NativeMethods.TITLEBARINFOEX))
            };
            DevExpress.Xpf.Core.NativeMethods.SendMessage(handle, 0x33f, IntPtr.Zero, ref lParam);
            Rect rect = new Rect();
            for (int i = 2; i < 6; i++)
            {
                if ((lParam.rgrect[i].right - lParam.rgrect[i].left) != 0)
                {
                    rect.X = (rect.X == 0.0) ? ((double) lParam.rgrect[i].left) : Math.Min((double) lParam.rgrect[i].left, rect.X);
                    rect.Y = (rect.Y == 0.0) ? ((double) lParam.rgrect[i].top) : Math.Min((double) lParam.rgrect[i].top, rect.Y);
                    rect.Width = Math.Max(lParam.rgrect[i].right - rect.X, rect.Width);
                    rect.Height = Math.Max(lParam.rgrect[i].bottom - rect.Y, rect.Height);
                }
            }
            return rect;
        }

        [DllImport("UxTheme.dll", CharSet=CharSet.Unicode)]
        private static extern int GetCurrentThemeName([MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszThemeFileName, int dwMaxNameChars, IntPtr pszColorBuff, int cchMaxColorChars, IntPtr pszSizeBuff, int cchMaxSizeChars);
        private double GetDpiFactor()
        {
            if ((this.interopHelperCore != null) && (this.interopHelperCore.Handle != IntPtr.Zero))
            {
                PresentationSource source = PresentationSource.FromVisual(this);
                if (source != null)
                {
                    return source.CompositionTarget.TransformToDevice.M11;
                }
            }
            HwndSourceParameters parameters = new HwndSourceParameters();
            using (HwndSource source2 = new HwndSource(parameters))
            {
                if (source2 != null)
                {
                    return source2.CompositionTarget.TransformToDevice.M11;
                }
            }
            return 1.0;
        }

        protected object GetField(string fieldName)
        {
            System.Type type = typeof(Window);
            return this.GetField(fieldName, type);
        }

        protected object GetField(string fieldName, System.Type type) => 
            type.GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(this);

        private Visibility GetHeaderButtonVisibility(string id)
        {
            System.Windows.Controls.Button visualByName = (System.Windows.Controls.Button) GetVisualByName(this, id);
            return ((visualByName == null) ? Visibility.Collapsed : visualByName.Visibility);
        }

        [SecuritySafeCritical]
        protected HwndTarget GetHwndTarget()
        {
            PresentationSource source = PresentationSource.FromVisual(this);
            return ((source == null) ? null : (source.CompositionTarget as HwndTarget));
        }

        protected double GetIconHeight() => 
            SystemParameters.SmallIconHeight;

        protected double GetIconWidth() => 
            SystemParameters.SmallIconWidth;

        public static bool GetIsActiveEx(DependencyObject obj) => 
            (bool) obj.GetValue(IsActiveExProperty);

        public static bool GetIsMaximized(DependencyObject obj) => 
            (bool) obj.GetValue(IsMaximizedProperty);

        [SecuritySafeCritical]
        private IntPtr GetMenuHandle()
        {
            IntPtr systemMenu = DevExpress.Xpf.Core.NativeMethods.GetSystemMenu(this.interopHelperCore.Handle, false);
            this.AdjustSystemMenu(systemMenu);
            return systemMenu;
        }

        [SecuritySafeCritical]
        private static Rect GetScreenBounds(Rect boundingBox)
        {
            DevExpress.Xpf.Core.NativeMethods.RECT rcMonitor = new DevExpress.Xpf.Core.NativeMethods.RECT(0, 0, 0, 0);
            DevExpress.Xpf.Core.NativeMethods.RECT rect2 = new DevExpress.Xpf.Core.NativeMethods.RECT(boundingBox);
            IntPtr handle = DevExpress.Xpf.Core.NativeMethods.MonitorFromRect(ref rect2, 2);
            if (handle != IntPtr.Zero)
            {
                DevExpress.Xpf.Core.NativeMethods.MONITORINFOEX info = new DevExpress.Xpf.Core.NativeMethods.MONITORINFOEX();
                DevExpress.Xpf.Core.NativeMethods.GetMonitorInfo(new HandleRef(null, handle), info);
                rcMonitor = info.rcMonitor;
            }
            return new Rect((double) rcMonitor.left, (double) rcMonitor.top, (double) (rcMonitor.right - rcMonitor.left), (double) (rcMonitor.bottom - rcMonitor.top));
        }

        [SecuritySafeCritical]
        private Thickness GetScreenMargins(Rect bounds)
        {
            DevExpress.Xpf.Core.NativeMethods.RECT rcWork = new DevExpress.Xpf.Core.NativeMethods.RECT(bounds);
            IntPtr handle = DevExpress.Xpf.Core.NativeMethods.MonitorFromRect(ref rcWork, 2);
            if (handle != IntPtr.Zero)
            {
                DevExpress.Xpf.Core.NativeMethods.MONITORINFOEX info = new DevExpress.Xpf.Core.NativeMethods.MONITORINFOEX();
                DevExpress.Xpf.Core.NativeMethods.GetMonitorInfo(new HandleRef(null, handle), info);
                rcWork = info.rcWork;
            }
            return this.PatchScreenMarginsByDpi(bounds, rcWork);
        }

        public static bool GetShowIcon(DependencyObject obj) => 
            (bool) obj.GetValue(ShowIconProperty);

        public static bool GetShowTitle(DependencyObject obj) => 
            (bool) obj.GetValue(ShowTitleProperty);

        private void GetStandartButtonsVisibility()
        {
            this.restoreButtonVisibility = this.GetHeaderButtonVisibility(2.ToString());
            this.maximizeButtonVisibility = this.GetHeaderButtonVisibility(3.ToString());
            this.minimizeButtonVisibility = this.GetHeaderButtonVisibility(1.ToString());
        }

        protected System.Windows.Point GetTruePos() => 
            new System.Windows.Point((double) this.trueLeft, (double) this.trueTop);

        protected internal object GetVisualByName(string name) => 
            GetVisualByName(this, name);

        internal static object GetVisualByName(DependencyObject root, string name)
        {
            object obj3;
            using (VisualTreeEnumeratorWithConditionalStop stop = new VisualTreeEnumeratorWithConditionalStop(root, new Predicate<DependencyObject>(DXWindow.DXWindowTreeStop)))
            {
                while (true)
                {
                    if (stop.MoveNext())
                    {
                        DependencyObject current = stop.Current;
                        if (((string) current.GetValue(FrameworkElement.NameProperty)) != name)
                        {
                            continue;
                        }
                        obj3 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return obj3;
        }

        private DevExpress.Xpf.Core.NativeMethods.WINDOWINFO GetWindowInfo(IntPtr hWnd)
        {
            DevExpress.Xpf.Core.NativeMethods.WINDOWINFO structure = new DevExpress.Xpf.Core.NativeMethods.WINDOWINFO();
            structure.cbSize = Marshal.SizeOf<DevExpress.Xpf.Core.NativeMethods.WINDOWINFO>(structure);
            DevExpress.Xpf.Core.NativeMethods.GetWindowInfo(hWnd, ref structure);
            return structure;
        }

        protected WindowMinMax GetWindowMinMax()
        {
            WindowMinMax max = new WindowMinMax();
            System.Windows.Point point = new System.Windows.Point((double) this.GetField("_trackMaxWidthDeviceUnits"), (double) this.GetField("_trackMaxHeightDeviceUnits"));
            System.Windows.Point point2 = new System.Windows.Point((double) this.GetField("_trackMinWidthDeviceUnits"), (double) this.GetField("_trackMinHeightDeviceUnits"));
            max.minWidth = Math.Max(base.MinWidth, point2.X);
            max.maxWidth = (base.MinWidth <= base.MaxWidth) ? (double.IsPositiveInfinity(base.MaxWidth) ? point.X : Math.Min(base.MaxWidth, point.X)) : Math.Min(base.MinWidth, point.X);
            max.minHeight = Math.Max(base.MinHeight, point2.Y);
            if (base.MinHeight > base.MaxHeight)
            {
                max.maxHeight = Math.Min(base.MinHeight, point.Y);
                return max;
            }
            if (!double.IsPositiveInfinity(base.MaxHeight))
            {
                max.maxHeight = Math.Min(base.MaxHeight, point.Y);
                return max;
            }
            max.maxHeight = point.Y;
            System.Windows.Point source = new System.Windows.Point(max.minWidth, max.minHeight);
            System.Windows.Point point4 = new System.Windows.Point(max.maxWidth, max.maxHeight);
            source = this.RoundPointToScreenPixels(source);
            point4 = this.RoundPointToScreenPixels(point4);
            max.maxHeight = point4.Y;
            max.maxWidth = point4.X;
            max.minHeight = source.Y;
            max.minWidth = source.X;
            return max;
        }

        [SecuritySafeCritical]
        private Rect GetWindowRect()
        {
            DevExpress.Xpf.Core.NativeMethods.RECT rect = new DevExpress.Xpf.Core.NativeMethods.RECT();
            if (this.interopHelperCore != null)
            {
                DevExpress.Xpf.Core.NativeMethods.GetWindowRect(new HandleRef(this, this.interopHelperCore.Handle), ref rect);
            }
            return new Rect((double) rect.left, (double) rect.top, (double) (rect.right - rect.left), (double) (rect.bottom - rect.top));
        }

        protected WindowStyle GetWindowStyleCore() => 
            this.EnableTransparency ? this.originalWindowStyle : base.WindowStyle;

        private void HideBorderEffect()
        {
            if (this.decorator != null)
            {
                this.decorator.Hide();
                this.ReleaseBorderEffect();
            }
        }

        [SecuritySafeCritical]
        protected virtual unsafe IntPtr HwndSourceHookHandler(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == 0x85)
            {
                if (!this.IsAeroMode)
                {
                    handled = true;
                    return IntPtr.Zero;
                }
                Rect controlBoxRect = this.GetControlBoxRect();
                Rect windowRect = this.GetWindowRect();
                SetAeroControlBoxHeight(this, controlBoxRect.Height);
                SetAeroControlBoxWidth(this, controlBoxRect.Width);
                SetAeroControlBoxMargin(this, new Thickness(0.0, 0.0, windowRect.Right - controlBoxRect.Right, 0.0));
                return IntPtr.Zero;
            }
            if (msg == 0xae)
            {
                handled = true;
                return IntPtr.Zero;
            }
            if (msg == 0x83)
            {
                if (this.IsAeroMode)
                {
                    DevExpress.Xpf.Core.NativeMethods.RECT structure = (DevExpress.Xpf.Core.NativeMethods.RECT) Marshal.PtrToStructure(lParam, typeof(DevExpress.Xpf.Core.NativeMethods.RECT));
                    int* numPtr1 = &structure.bottom;
                    numPtr1[0]++;
                    Marshal.StructureToPtr<DevExpress.Xpf.Core.NativeMethods.RECT>(structure, lParam, false);
                    handled = true;
                    return new IntPtr(0x300);
                }
                Rect windowRect = this.WindowRect;
                bool flag = ScreenHelper.IsOnPrimaryScreen(new System.Windows.Point(windowRect.CenterX(), windowRect.CenterY()));
                if (((wParam == new IntPtr(1)) && (base.WindowState == WindowState.Maximized)) & flag)
                {
                    int num;
                    DevExpress.Xpf.Core.NativeMethods.NCCALCSIZE_PARAMS structure = (DevExpress.Xpf.Core.NativeMethods.NCCALCSIZE_PARAMS) Marshal.PtrToStructure(lParam, typeof(DevExpress.Xpf.Core.NativeMethods.NCCALCSIZE_PARAMS));
                    if (TaskBarHelper.IsAutoHide(out num, this.interopHelperCore.Handle))
                    {
                        if (num == 3)
                        {
                            int* numPtr2 = &structure.rgrc0.bottom;
                            numPtr2[0] -= 10;
                        }
                        if (num == 2)
                        {
                            int* numPtr3 = &structure.rgrc0.right;
                            numPtr3[0] -= 10;
                        }
                        if (num == 1)
                        {
                            int* numPtr4 = &structure.rgrc0.bottom;
                            numPtr4[0] -= 10;
                        }
                        if (num == 0)
                        {
                            int* numPtr5 = &structure.rgrc0.right;
                            numPtr5[0] -= 10;
                        }
                    }
                    Marshal.StructureToPtr<DevExpress.Xpf.Core.NativeMethods.NCCALCSIZE_PARAMS>(structure, lParam, false);
                }
                handled = true;
                return IntPtr.Zero;
            }
            if (msg == 0x86)
            {
                if (this.IsAeroMode)
                {
                    IntPtr ptr = DevExpress.Xpf.Core.NativeMethods.DefWindowProc(hwnd, 0x86, wParam, new IntPtr(-1));
                    handled = true;
                    return ptr;
                }
                SetIsActiveEx(this, ((int) wParam) > 0);
            }
            if (msg == 0x84)
            {
                if (this.IsAeroMode)
                {
                    return this.DoNonClientHitTest(hwnd, msg, wParam, lParam, ref handled);
                }
                handled = true;
                return this.DoNCHitTest(lParam);
            }
            if (this.interopHelperCore != null)
            {
                if (hwnd != this.interopHelperCore.Handle)
                {
                    return IntPtr.Zero;
                }
                if (msg == 6)
                {
                    this.InvalidateWindowCaption();
                    return IntPtr.Zero;
                }
                if ((msg == 0x86) || ((msg == 12) || (msg == 0x80)))
                {
                    if (this.isProcessingDefWndProc)
                    {
                        return IntPtr.Zero;
                    }
                    this.InvalidateWindowCaption();
                    this.isProcessingDefWndProc = true;
                    IntPtr zero = IntPtr.Zero;
                    zero = CallDefWindowProcWithoutVisibleStyle(hwnd, msg, wParam, lParam, ref handled);
                    this.isProcessingDefWndProc = false;
                    return zero;
                }
                if (msg == 3)
                {
                    this.WmMoveChanged(this.interopHelperCore.Handle, lParam);
                    handled = true;
                    return IntPtr.Zero;
                }
                if (msg == 5)
                {
                    this.WmSizeChanged(wParam);
                }
                if ((msg == 0xa4) || ((msg == 0xa5) || (msg == 0xa6)))
                {
                    handled = true;
                    return IntPtr.Zero;
                }
                HwndTarget hwndTarget = this.GetHwndTarget();
                if (msg == 0x231)
                {
                    this.IsDraggingOrResizing = true;
                    if (hwndTarget != null)
                    {
                        if (this.isDragMove)
                        {
                            this.PatchField(true, "_windowPosChanging", typeof(HwndTarget), hwndTarget);
                        }
                        else
                        {
                            this.ChangeRenderMode(RenderMode.SoftwareOnly);
                            this.isWindowSizing = true;
                        }
                    }
                }
                if (msg == 0x232)
                {
                    if (hwndTarget != null)
                    {
                        if (this.isDragMove)
                        {
                            this.PatchField(false, "_windowPosChanging", typeof(HwndTarget), hwndTarget);
                        }
                        else
                        {
                            this.ChangeRenderMode(RenderMode.Default);
                            this.isWindowSizing = false;
                        }
                    }
                    base.InvalidateVisual();
                    this.IsDraggingOrResizing = false;
                    this.isDragMove = false;
                }
                if ((msg == 0x215) && this.isWindowSizing)
                {
                    if ((hwndTarget != null) && !this.isDragMove)
                    {
                        this.ChangeRenderMode(RenderMode.Default);
                        this.isWindowSizing = false;
                        base.InvalidateVisual();
                    }
                    this.IsDraggingOrResizing = false;
                    this.isDragMove = false;
                }
                if ((msg == 0x205) && !this.allowProcessContextMenu)
                {
                    handled = true;
                    return IntPtr.Zero;
                }
                if (msg == 0x31e)
                {
                    this.UpdateIsAeroModeEnabled();
                    if (this.IsAeroMode)
                    {
                        this.ProcessDWM();
                        base.UpdateLayout();
                        handled = true;
                        return IntPtr.Zero;
                    }
                }
            }
            return IntPtr.Zero;
        }

        private void IconMouseProcessing(object sender, MouseButtonEventArgs e)
        {
            if ((e.ClickCount == 2) && (e.ChangedButton == MouseButton.Left))
            {
                base.Close();
            }
            else if ((e.ClickCount == 1) && (((e.ChangedButton == MouseButton.Left) && (e.LeftButton == MouseButtonState.Pressed)) || ((e.ChangedButton == MouseButton.Right) && (e.RightButton == MouseButtonState.Pressed))))
            {
                System.Windows.Point pos = System.Windows.Point.Add(e.GetPosition(this), new Vector((double) (this.trueLeft + 5), (double) (this.trueTop + 5)));
                this.ShowSystemMenuOnIconClick(pos);
            }
        }

        private void InitBorderEffect()
        {
            if (this.decorator == null)
            {
                this.decorator = this.CreateFormHandleDecorator();
                this.decorator.Control = this;
                this.RedrawBorderWithEffectColor(this.ActiveState, true);
            }
        }

        [SecuritySafeCritical]
        private void InvalidateWindowCaption()
        {
            base.InvalidateVisual();
            DevExpress.Xpf.Core.NativeMethods.RedrawWindow(this.interopHelperCore.Handle, IntPtr.Zero, IntPtr.Zero, 0x501);
        }

        [SecuritySafeCritical]
        private bool IsClassicTheme() => 
            GetCurrentThemeName(new StringBuilder(260, 260), 260, IntPtr.Zero, 0, IntPtr.Zero, 0) < 0;

        protected static bool IsUncPath(string s) => 
            !string.IsNullOrEmpty(s) ? s.StartsWith(@"\\") : true;

        protected System.Windows.Point LogicalPixelsToScreen(System.Windows.Point point)
        {
            HwndTarget hwndTarget = this.GetHwndTarget();
            return ((hwndTarget == null) ? point : hwndTarget.TransformToDevice.Transform(point));
        }

        private System.Windows.Size MeasureOverrideHelper(System.Windows.Size constraint)
        {
            this.interopHelperCore ??= new WindowInteropHelper(this);
            if (this.VisualChildrenCount > 0)
            {
                UIElement visualChild = this.GetVisualChild(0) as UIElement;
                if (visualChild != null)
                {
                    System.Windows.Size size = new System.Windows.Size(0.0, 0.0);
                    if (this.AllowOptimizeUpdateRootMargins())
                    {
                        this.UpdateRootMargins(base.WindowState == WindowState.Maximized);
                    }
                    System.Windows.Size availableSize = new System.Windows.Size {
                        Width = (constraint.Width == double.PositiveInfinity) ? double.PositiveInfinity : Math.Max((double) 0.0, (double) (constraint.Width - size.Width)),
                        Height = (constraint.Height == double.PositiveInfinity) ? double.PositiveInfinity : Math.Max((double) 0.0, (double) (constraint.Height - size.Height))
                    };
                    visualChild.Measure(availableSize);
                    System.Windows.Size desiredSize = visualChild.DesiredSize;
                    System.Windows.Point source = new System.Windows.Point(desiredSize.Width + size.Width, desiredSize.Height + size.Height);
                    System.Windows.Point point2 = this.RoundPointToScreenPixels(source);
                    return new System.Windows.Size(point2.X, point2.Y);
                }
            }
            return new System.Windows.Size(0.0, 0.0);
        }

        private static DevExpress.Xpf.Core.NativeMethods.MONITORINFOEX MonitorInfoFromWindow(IntPtr hWnd)
        {
            IntPtr handle = DevExpress.Xpf.Core.NativeMethods.MonitorFromWindow(hWnd, 2);
            DevExpress.Xpf.Core.NativeMethods.MONITORINFOEX info = new DevExpress.Xpf.Core.NativeMethods.MONITORINFOEX {
                cbSize = Marshal.SizeOf(typeof(DevExpress.Xpf.Core.NativeMethods.MONITORINFOEX))
            };
            DevExpress.Xpf.Core.NativeMethods.GetMonitorInfo(new HandleRef(null, handle), info);
            return info;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            SetIsActiveEx(this, true);
        }

        protected virtual void OnActualResizeBorderThicknessChanged(Thickness oldValue)
        {
        }

        protected virtual void OnAeroBorderSizeChanged(double oldValue)
        {
        }

        protected static void OnAeroBorderSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DXWindow)
            {
                ((DXWindow) d).OnAeroBorderSizeChanged((double) e.OldValue);
            }
        }

        private static void OnAeroWindowTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DXWindow)
            {
                ((DXWindow) d).UpdateActualWindowTemplate();
            }
        }

        protected virtual void OnAllowBorderEffectChanged()
        {
            if (this.BorderEffect != DevExpress.Xpf.Core.BorderEffect.None)
            {
                this.InitBorderEffect();
            }
            else
            {
                this.ReleaseBorderEffect();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PartRootGrid = base.GetTemplateChild("Root_Grid") as Grid;
            this.PartFloatPaneBorders = base.GetTemplateChild("FloatPaneBorders") as Grid;
        }

        protected virtual void OnBorderEffectActiveColorChanged(SolidColorBrush oldValue)
        {
            if (this.decorator != null)
            {
                this.decorator.ActiveColor = this.BorderEffectActiveColor;
                this.decorator.RenderDecorator();
                this.RedrawBorderWithEffectColor(this.ActiveState, false);
            }
        }

        protected static void OnBorderEffectActiveColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DXWindow)
            {
                ((DXWindow) d).OnBorderEffectActiveColorChanged((SolidColorBrush) e.OldValue);
            }
        }

        protected virtual void OnBorderEffectInactiveColorChanged(SolidColorBrush oldValue)
        {
            if (this.decorator != null)
            {
                this.decorator.InactiveColor = this.BorderEffectInactiveColor;
                this.decorator.RenderDecorator();
                this.RedrawBorderWithEffectColor(this.ActiveState, false);
            }
        }

        protected static void OnBorderEffectInactiveColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DXWindow)
            {
                ((DXWindow) d).OnBorderEffectInactiveColorChanged((SolidColorBrush) e.OldValue);
            }
        }

        protected virtual void OnClose()
        {
            base.Close();
        }

        [SecuritySafeCritical]
        protected override void OnClosing(CancelEventArgs e)
        {
            ThemeManager.RemoveThemeChangedHandler(this, new ThemeChangedRoutedEventHandler(this.OnThemeChanged));
            base.OnClosing(e);
            if (!e.Cancel && (this.BorderEffect != DevExpress.Xpf.Core.BorderEffect.None))
            {
                this.HideBorderEffect();
            }
        }

        private static void OnContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            UIElement d = sender as UIElement;
            if (d != null)
            {
                DXWindow window = LayoutHelper.FindRoot(d, false) as DXWindow;
                if ((window != null) && !window.allowProcessContextMenu)
                {
                    e.Handled = true;
                }
            }
        }

        protected override void OnDeactivated(EventArgs e)
        {
            base.InvalidateVisual();
            base.OnDeactivated(e);
            SetIsActiveEx(this, false);
        }

        private void OnDoubleClickTimerTick(object sender, EventArgs e)
        {
            this.isClosing = false;
            this.DoubleClickTimer.Stop();
            this.doubleClickTimer = null;
        }

        protected virtual void OnHeaderSizeChanged(System.Windows.Size oldValue)
        {
            DXWindowSmartCenteringPanel child = TreeHelper.GetChild<DXWindowSmartCenteringPanel>(this, null);
            if (child != null)
            {
                child.InvalidateMeasure();
            }
        }

        internal void OnHeaderSizeChanged(DXWindowHeader windowHeader, System.Windows.Size newSize, System.Windows.Size oldSize)
        {
            this.HeaderSize = newSize;
            if (newSize.Height != oldSize.Height)
            {
                this.ProcessDWM();
                base.UpdateLayout();
            }
        }

        private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DXWindow).PatchIcon(e.NewValue);
        }

        protected virtual void OnIsActiveChanged(bool newValue)
        {
            this.ActiveState = newValue;
        }

        private static void OnIsActivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DXWindow)
            {
                ((DXWindow) d).OnIsActiveChanged((bool) e.NewValue);
            }
        }

        protected virtual void OnIsAeroModeChanged(bool oldValue)
        {
            this.UpdateActualWindowTemplate();
            this.UpdateActualResizeBorderThickness();
            base.UpdateLayout();
        }

        protected virtual void OnIsAeroModeEnabledChanged(bool oldValue)
        {
            base.CoerceValue(IsAeroModeProperty);
        }

        protected static void OnIsAeroModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DXWindow)
            {
                ((DXWindow) d).OnIsAeroModeChanged((bool) e.OldValue);
            }
        }

        protected static object OnIsAeroModePropertyCoerce(DependencyObject d, object baseValue) => 
            !(d is DXWindow) ? baseValue : ((DXWindow) d).CoerceIsAeroModeValue((bool) baseValue);

        private static void OnIsMaximizedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DXWindow window = (DXWindow) d;
            if (window != null)
            {
                window.SetElementVisibility(2.ToString(), window.restoreButtonVisibility);
                window.SetElementVisibility(3.ToString(), window.maximizeButtonVisibility);
            }
        }

        protected virtual void OnMaximize()
        {
            base.SizeToContent = SizeToContent.Manual;
            SetIsMaximized(this, true);
            base.WindowState = WindowState.Maximized;
        }

        protected virtual void OnMinimize()
        {
            base.WindowState = WindowState.Minimized;
        }

        protected override void OnPreviewMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            if (this.isWindowSizing)
            {
                e.Handled = true;
            }
            base.OnPreviewMouseMove(e);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (!base.AllowsTransparency && !this.IsAeroMode)
            {
                drawingContext.DrawRectangle(System.Windows.Media.Brushes.Transparent, new System.Windows.Media.Pen(System.Windows.Media.Brushes.Black, 1.0), new Rect(0.0, 0.0, base.Width, base.Height));
            }
        }

        protected virtual void OnResizeBorderThicknessChanged(Thickness oldValue)
        {
            this.UpdateActualResizeBorderThickness();
        }

        protected virtual void OnResizeBorderThicknessInAeroModeChanged(Thickness oldValue)
        {
            this.UpdateActualResizeBorderThickness();
        }

        protected static void OnResizeBorderThicknessInAeroModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DXWindow)
            {
                ((DXWindow) d).OnResizeBorderThicknessInAeroModeChanged((Thickness) e.OldValue);
            }
        }

        protected static void OnResizeBorderThicknessPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DXWindow)
            {
                ((DXWindow) d).OnResizeBorderThicknessChanged((Thickness) e.OldValue);
            }
        }

        private static void OnResizeModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DXWindow).SetElementsVisilbility();
        }

        protected virtual void OnRestore()
        {
            SetIsMaximized(this, false);
            base.WindowState = WindowState.Normal;
        }

        protected virtual void OnShowTitlePropertyChanged(bool oldValue)
        {
            this.AttachToText();
        }

        private static void OnShowTitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DXWindow)
            {
                ((DXWindow) d).OnShowTitlePropertyChanged((bool) e.OldValue);
            }
        }

        protected virtual void OnSmallIconChanged(ImageSource oldValue)
        {
        }

        private static void OnSmallIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DXWindow)
            {
                ((DXWindow) d).OnSmallIconChanged((ImageSource) e.OldValue);
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.interopHelperCore ??= new WindowInteropHelper(this);
            this.CheckSystemIcon();
            HwndSource.FromHwnd(this.interopHelperCore.Handle).AddHook(new HwndSourceHook(this.HwndSourceHookHandler));
            if (!this.stylePatched)
            {
                this.stylePatched = true;
                int windowLong = DevExpress.Xpf.Core.NativeMethods.GetWindowLong(this.interopHelperCore.Handle, -16);
                DevExpress.Xpf.Core.NativeMethods.SetWindowLong(this.interopHelperCore.Handle, -16, windowLong & -12582913);
                this.stylePatched = true;
            }
            if ((base.WindowState == WindowState.Normal) || UpdateWindowRegionOnSourceInitialized)
            {
                this.PatchWindowRegion((int) base.Width, (int) base.Height);
            }
            base.InvalidateVisual();
            base.InvalidateMeasure();
            this.ProcessDWM();
            ThemeManager.AddThemeChangedHandler(this, new ThemeChangedRoutedEventHandler(this.OnThemeChanged));
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
            this.UpdateHeaderWidth();
            this.SetElementsVisilbility();
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            this.DelayedExecute(delegate {
                if (this.NeedReAttachToVisualTree)
                {
                    this.AttachToVisualTree();
                }
            });
            if (this.BorderEffect != DevExpress.Xpf.Core.BorderEffect.None)
            {
                this.BorderEffect = DevExpress.Xpf.Core.BorderEffect.None;
                this.BorderEffect = DevExpress.Xpf.Core.BorderEffect.Default;
            }
        }

        private static void OnUseLayoutRoundingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DXWindow window = d as DXWindow;
            if (window != null)
            {
                foreach (Window window2 in window.OwnedWindows)
                {
                    window2.SetCurrentValue(FrameworkElement.UseLayoutRoundingProperty, window.UseLayoutRounding);
                }
            }
        }

        private static void OnVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DXWindow window = d as DXWindow;
            if ((window == null) || (window.HwndSourceWindow != null))
            {
                if (((Visibility) e.NewValue) == Visibility.Visible)
                {
                    window.DXWindow_Loaded(d, null);
                }
            }
            else if (window.EnableTransparency)
            {
                window.originalWindowStyle = window.WindowStyle;
                window.WindowStyle = WindowStyle.None;
                window.AllowsTransparency = true;
            }
        }

        private static void OnWindowBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DXWindow window = d as DXWindow;
            if (window != null)
            {
                FloatingContainerThemeKeyExtension key = new FloatingContainerThemeKeyExtension {
                    ResourceKey = FloatingContainerThemeKey.FloatingContainerBackground,
                    IsThemeIndependent = true
                };
                if (window.Resources.Contains(key))
                {
                    window.Resources.Remove(key);
                }
                if (e.NewValue != null)
                {
                    window.Resources.Add(key, e.NewValue);
                }
            }
        }

        private static void OnWindowStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DXWindow window = d as DXWindow;
            if (((window == null) || (window.HwndSourceWindow != null)) && (window.Visibility != Visibility.Hidden))
            {
                bool flag = ((WindowState) e.NewValue) == WindowState.Maximized;
                if ((window.ResizeMode == ResizeMode.CanResize) || (window.ResizeMode == ResizeMode.CanResizeWithGrip))
                {
                    SetIsMaximized(d, flag);
                }
                if (window != null)
                {
                    window.UpdateRootMargins(flag);
                    window.UpdateFloatPaneBorders(flag);
                    if (window.interopHelperCore != null)
                    {
                        window.previousWindowState = (WindowState) e.NewValue;
                    }
                }
                window.ProcessDWM();
            }
        }

        private static void OnWindowStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DXWindow).DoWindowStyleChangedUpdate((WindowStyle) e.NewValue);
        }

        private static void OnWindowTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DXWindow)
            {
                ((DXWindow) d).UpdateActualWindowTemplate();
            }
        }

        protected void PatchField(object newVal, string fieldName)
        {
            System.Type type = typeof(Window);
            this.PatchField(newVal, fieldName, type, this);
        }

        protected void PatchField(object newVal, string fieldName, System.Type type, object instance)
        {
            FieldInfo field = type.GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(instance, newVal);
            }
        }

        protected void PatchIcon(object newVal)
        {
            BitmapFrame frame = newVal as BitmapFrame;
            if (frame == null)
            {
                BitmapImage image = newVal as BitmapImage;
                if (image != null)
                {
                    try
                    {
                        frame = BitmapFrame.Create(image.UriSource);
                    }
                    catch
                    {
                        TransformedBitmap bitmap = new TransformedBitmap(image, new ScaleTransform(this.GetIconWidth() / image.Width, this.GetIconHeight() / image.Height));
                        this.SmallIcon = bitmap;
                        return;
                    }
                }
            }
            BitmapFrame source = null;
            if (frame != null)
            {
                double iWidth = this.GetIconWidth();
                double iHeight = this.GetIconHeight();
                source = (from x in frame.Decoder.Frames
                    orderby Math.Abs((double) (Math.Abs((double) (iWidth - x.Width)) + Math.Abs((double) (iHeight - x.Height))))
                    select x).FirstOrDefault<BitmapFrame>();
                if (source.Width > this.GetIconWidth())
                {
                    TransformedBitmap bitmap2 = new TransformedBitmap(source, new ScaleTransform(this.GetIconWidth() / source.Width, this.GetIconHeight() / source.Height));
                    this.SmallIcon = bitmap2;
                    return;
                }
            }
            if (source == null)
            {
                this.SmallIcon = newVal as ImageSource;
            }
            else
            {
                this.SmallIcon = source;
            }
        }

        private void PatchLeftTop()
        {
            try
            {
                System.Windows.Point point = this.ScreenToLogicalPixels(new System.Windows.Point((double) this.trueLeft, (double) this.trueTop));
                this.Set_updateHwndLocation(false);
                base.SetValue(Window.LeftProperty, point.X);
                base.SetValue(Window.TopProperty, point.Y);
            }
            finally
            {
                this.Set_updateHwndLocation(true);
            }
        }

        [SecuritySafeCritical]
        private Thickness PatchScreenMarginsByDpi(Rect bounds, DevExpress.Xpf.Core.NativeMethods.RECT rect)
        {
            double dpiFactor = this.GetDpiFactor();
            int num3 = (int) Math.Ceiling((double) ((rect.top - bounds.Top) / dpiFactor));
            int num4 = (int) Math.Ceiling((double) ((bounds.Right - rect.right) / dpiFactor));
            int num5 = (int) Math.Ceiling((double) ((bounds.Bottom - rect.bottom) / dpiFactor));
            return new Thickness((double) ((int) Math.Ceiling((double) ((rect.left - bounds.Left) / dpiFactor))), (double) num3, (double) num4, (double) num5);
        }

        [SecuritySafeCritical]
        protected virtual void PatchWindowRegion(int width, int height)
        {
            if (!this.IsAeroMode && (this.interopHelperCore != null))
            {
                if (base.WindowState != WindowState.Maximized)
                {
                    this.PatchWindowRegionCore(width, height, 0);
                }
                else if (!IsWinSevenOrHigher)
                {
                    DevExpress.Xpf.Core.NativeMethods.SetWindowRgn(this.interopHelperCore.Handle, IntPtr.Zero, true);
                }
                else if (this.IsClassicTheme())
                {
                    this.PatchWindowRegionCore(width, height, 4);
                }
                else if (!this.IsAeroModeEnabled)
                {
                    this.PatchWindowRegionCore(width, height, 8);
                }
                else
                {
                    DevExpress.Xpf.Core.NativeMethods.SetWindowRgn(this.interopHelperCore.Handle, IntPtr.Zero, true);
                }
            }
        }

        [SecuritySafeCritical]
        private void PatchWindowRegionCore(int width, int height, int happyNumber)
        {
            this.region = DevExpress.Xpf.Core.NativeMethods.CreateRectRgn(happyNumber, 0, width - happyNumber, height);
            DevExpress.Xpf.Core.NativeMethods.SetWindowRgn(this.interopHelperCore.Handle, this.region, true);
        }

        [SecuritySafeCritical]
        protected unsafe void ProcessDWM()
        {
            if (this.IsAeroMode && (this.interopHelperCore != null))
            {
                DevExpress.Xpf.Core.NativeMethods.SetWindowRgn(this.interopHelperCore.Handle, IntPtr.Zero, false);
                if (this.HwndSourceWindow != null)
                {
                    this.HwndSourceWindow.CompositionTarget.BackgroundColor = Colors.Transparent;
                    Thickness aeroBorderThickness = this.GetAeroBorderThickness();
                    if (base.WindowState == WindowState.Maximized)
                    {
                        System.Windows.Point point3 = DIPToDeviceTransform.Transform(new System.Windows.Point(0.0, this.GetWindowRect().Top));
                        Thickness* thicknessPtr1 = &aeroBorderThickness;
                        thicknessPtr1.Top -= point3.Y;
                    }
                    System.Windows.Point point = DIPToDeviceTransform.Transform(new System.Windows.Point(aeroBorderThickness.Left, aeroBorderThickness.Top));
                    System.Windows.Point point2 = DIPToDeviceTransform.Transform(new System.Windows.Point(aeroBorderThickness.Right, aeroBorderThickness.Bottom));
                    DevExpress.Xpf.Core.NativeMethods.MARGIN pMarInset = new DevExpress.Xpf.Core.NativeMethods.MARGIN {
                        left = (int) point.X,
                        right = (int) point2.X,
                        top = (int) point.Y,
                        bottom = (int) point2.Y
                    };
                    DevExpress.Xpf.Core.NativeMethods.DwmExtendFrameIntoClientArea(this.interopHelperCore.Handle, ref pMarInset);
                    DevExpress.Xpf.Core.NativeMethods.SetWindowPosOptions uFlags = DevExpress.Xpf.Core.NativeMethods.SetWindowPosOptions.NOOWNERZORDER | DevExpress.Xpf.Core.NativeMethods.SetWindowPosOptions.DRAWFRAME | DevExpress.Xpf.Core.NativeMethods.SetWindowPosOptions.NOACTIVATE | DevExpress.Xpf.Core.NativeMethods.SetWindowPosOptions.NOZORDER | DevExpress.Xpf.Core.NativeMethods.SetWindowPosOptions.NOMOVE | DevExpress.Xpf.Core.NativeMethods.SetWindowPosOptions.NOSIZE;
                    DevExpress.Xpf.Core.NativeMethods.SetWindowPos(this.interopHelperCore.Handle, IntPtr.Zero, 0, 0, 0, 0, uFlags);
                }
            }
        }

        private void RedrawBorderWithEffectColor(bool activeState, bool needLayoutUpdate)
        {
            if (this.BorderEffect != DevExpress.Xpf.Core.BorderEffect.None)
            {
                if (activeState)
                {
                    this.SetFloatingContainerBorderColor(this.BorderEffectActiveColor, needLayoutUpdate);
                }
                else
                {
                    this.SetFloatingContainerBorderColor(this.BorderEffectInactiveColor, needLayoutUpdate);
                }
            }
        }

        private void ReleaseBorderEffect()
        {
            if (this.decorator != null)
            {
                this.decorator.Dispose();
                this.decorator = null;
            }
        }

        protected void ResetElementVisibility(string elementName)
        {
            FrameworkElement visualByName = this.GetVisualByName(elementName) as FrameworkElement;
            if (visualByName != null)
            {
                visualByName.SetValue(UIElement.VisibilityProperty, DependencyProperty.UnsetValue);
            }
        }

        protected virtual void ResetFloatingContainerBodyMargin()
        {
            Border visualByName = this.GetVisualByName("FloatingContainerBody") as Border;
            if (visualByName != null)
            {
                visualByName.SetValue(FrameworkElement.MarginProperty, DependencyProperty.UnsetValue);
            }
            if (visualByName != null)
            {
                visualByName.SetValue(Border.PaddingProperty, DependencyProperty.UnsetValue);
            }
            visualByName = this.GetVisualByName("FloatingContainerBorder") as Border;
            if (visualByName != null)
            {
                visualByName.SetValue(Border.PaddingProperty, DependencyProperty.UnsetValue);
            }
        }

        private void ResetFloatingContainerBorderEffectColor(SolidColorBrush color)
        {
            if (color != null)
            {
                this.SetFloatingContainerBorderColor(color, false);
            }
        }

        protected void ResizeWindow(DXWindowActiveResizeParts resizeMode)
        {
            ResizeWindow(this.interopHelperCore.Handle, resizeMode);
        }

        [SecuritySafeCritical]
        private static void ResizeWindow(IntPtr handle, DXWindowActiveResizeParts sizingAction)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Win32.SendMessage(handle, 0x112, 0xf000 + sizingAction, IntPtr.Zero);
                Win32.SendMessage(handle, 0x202, 0, IntPtr.Zero);
            }
        }

        private double Round(double source) => 
            Math.Round((double) (source + 0.5));

        private System.Windows.Point RoundPoint(System.Windows.Point source) => 
            new System.Windows.Point(this.Round(source.X), this.Round(source.Y));

        private System.Windows.Point RoundPointToScreenPixels(System.Windows.Point source)
        {
            System.Windows.Point point = this.LogicalPixelsToScreen(source);
            point = this.RoundPoint(point);
            return this.ScreenToLogicalPixels(point);
        }

        protected System.Windows.Point ScreenToLogicalPixels(System.Windows.Point point)
        {
            HwndTarget hwndTarget = this.GetHwndTarget();
            return ((hwndTarget == null) ? point : hwndTarget.TransformFromDevice.Transform(point));
        }

        protected void Set_updateHwndLocation(bool newVal)
        {
            this.PatchField(newVal, "_updateHwndLocation");
        }

        protected void Set_updateHwndSize(bool newVal)
        {
            this.PatchField(newVal, "_updateHwndSize");
        }

        protected static void SetAeroControlBoxHeight(DependencyObject obj, double value)
        {
            obj.SetValue(AeroControlBoxHeightPropertyKey, value);
        }

        protected static void SetAeroControlBoxMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(AeroControlBoxMarginPropertyKey, value);
        }

        protected static void SetAeroControlBoxWidth(DependencyObject obj, double value)
        {
            obj.SetValue(AeroControlBoxWidthPropertyKey, value);
        }

        [SecuritySafeCritical]
        protected bool SetAppIcon(IntPtr hwnd)
        {
            if (this.SmallIcon == null)
            {
                int iconHandle = 0;
                iconHandle = this.GetAppIconCore(hwnd, 2, iconHandle);
                iconHandle = this.GetAppIconCore(hwnd, 0, iconHandle);
                iconHandle = this.GetAppIconCore(hwnd, 1, iconHandle);
                if (iconHandle == 0)
                {
                    return false;
                }
                Icon icon = Icon.FromHandle(new IntPtr(iconHandle));
                if (icon == null)
                {
                    return false;
                }
                BitmapSource newVal = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                this.PatchIcon(newVal);
            }
            return true;
        }

        [SecuritySafeCritical]
        private void SetDefaultIcon()
        {
            if (base.Icon == null)
            {
                IntPtr zero = IntPtr.Zero;
                if (this.imageSource == null)
                {
                    try
                    {
                        zero = DevExpress.Xpf.Core.NativeMethods.LoadImage(DevExpress.Xpf.Core.NativeMethods.GetModuleHandle("user32.dll"), new IntPtr(100), 1, 0x10, 0x10, 0x8000);
                        this.imageSource = Imaging.CreateBitmapSourceFromHIcon(zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        this.PatchIcon(this.imageSource);
                    }
                    catch
                    {
                    }
                    finally
                    {
                        if (zero != IntPtr.Zero)
                        {
                            DevExpress.Xpf.Core.NativeMethods.DeleteObject(zero);
                        }
                    }
                }
                if (this.imageSource != null)
                {
                    this.PatchIcon(this.imageSource);
                }
            }
        }

        protected void SetElementIsEnabled(string elementName, bool isEnabled)
        {
            this.SetElementIsEnabled(elementName, isEnabled, false);
        }

        protected void SetElementIsEnabled(string elementName, bool isEnabled, bool force)
        {
            FrameworkElement visualByName = this.GetVisualByName(elementName) as FrameworkElement;
            if (visualByName != null)
            {
                if (!force)
                {
                    visualByName.SetCurrentValue(UIElement.IsEnabledProperty, isEnabled);
                }
                else
                {
                    visualByName.SetValue(UIElement.IsEnabledProperty, isEnabled);
                }
            }
        }

        private void SetElementsVisilbility()
        {
            switch (base.ResizeMode)
            {
                case ResizeMode.NoResize:
                    this.SetElementVisibility(1.ToString(), Visibility.Collapsed);
                    this.SetElementVisibility(3.ToString(), Visibility.Collapsed);
                    this.SetElementVisibility(2.ToString(), Visibility.Collapsed);
                    this.SetElementVisibility(1.ToString(), Visibility.Collapsed);
                    this.SetElementVisibility(0.ToString(), Visibility.Visible);
                    this.SetElementVisibility("FloatPaneBorders", Visibility.Collapsed);
                    this.UpdateNoResizeAndWSNone();
                    break;

                case ResizeMode.CanMinimize:
                    this.SetElementVisibility(0.ToString(), Visibility.Visible);
                    this.SetElementVisibility(1.ToString(), Visibility.Collapsed);
                    if (base.WindowState == WindowState.Minimized)
                    {
                        this.SetElementVisibility(3.ToString(), Visibility.Visible);
                        this.SetElementIsEnabled(3.ToString(), false);
                        this.SetElementVisibility(2.ToString(), Visibility.Visible);
                        this.SetElementVisibility(1.ToString(), Visibility.Collapsed);
                    }
                    else
                    {
                        this.SetElementVisibility(3.ToString(), Visibility.Visible);
                        this.SetElementIsEnabled(3.ToString(), false);
                        this.SetElementVisibility(2.ToString(), Visibility.Collapsed);
                        this.SetElementVisibility(1.ToString(), Visibility.Visible);
                    }
                    this.SetElementVisibility("FloatPaneBorders", Visibility.Collapsed);
                    return;

                case ResizeMode.CanResize:
                case ResizeMode.CanResizeWithGrip:
                    this.SetElementVisibility(1.ToString(), Visibility.Collapsed);
                    this.SetElementVisibility(0.ToString(), Visibility.Visible);
                    if (base.WindowState == WindowState.Minimized)
                    {
                        this.SetElementVisibility(1.ToString(), Visibility.Collapsed);
                        this.SetElementVisibility(3.ToString(), Visibility.Visible);
                        this.SetElementIsEnabled(3.ToString(), true);
                        this.SetElementVisibility(2.ToString(), Visibility.Visible);
                    }
                    if (base.WindowState == WindowState.Normal)
                    {
                        if (base.WindowStyle == WindowStyle.SingleBorderWindow)
                        {
                            this.SetElementVisibility(1.ToString(), Visibility.Visible);
                        }
                        this.SetElementVisibility(3.ToString(), Visibility.Visible);
                        this.SetElementIsEnabled(3.ToString(), true);
                        this.SetElementVisibility(2.ToString(), Visibility.Collapsed);
                    }
                    if (base.WindowState == WindowState.Maximized)
                    {
                        if (base.WindowStyle == WindowStyle.SingleBorderWindow)
                        {
                            this.SetElementVisibility(1.ToString(), Visibility.Visible);
                        }
                        this.SetElementVisibility(2.ToString(), Visibility.Visible);
                        this.SetElementVisibility(3.ToString(), Visibility.Collapsed);
                    }
                    this.SetElementVisibility("FloatPaneBorders", Visibility.Visible);
                    if (base.ResizeMode == ResizeMode.CanResize)
                    {
                        break;
                    }
                    if (base.WindowStyle != WindowStyle.ToolWindow)
                    {
                        this.SetElementVisibility(1.ToString(), Visibility.Visible);
                    }
                    this.SetElementVisibility(1.ToString(), Visibility.Visible);
                    this.SetElementVisibility(0.ToString(), Visibility.Visible);
                    this.SetElementVisibility("FloatPaneBorders", Visibility.Visible);
                    return;

                default:
                    return;
            }
        }

        protected void SetElementVisibility(string elementName, Visibility visibility)
        {
            this.SetElementVisibility(elementName, visibility, false);
        }

        protected void SetElementVisibility(string elementName, Visibility visibility, bool force)
        {
            FrameworkElement visualByName = this.GetVisualByName(elementName) as FrameworkElement;
            if (visualByName != null)
            {
                if (!force)
                {
                    visualByName.SetCurrentValue(UIElement.VisibilityProperty, visibility);
                }
                else
                {
                    visualByName.SetValue(UIElement.VisibilityProperty, visibility);
                }
            }
        }

        private void SetFloatingContainerBorderColor(SolidColorBrush borderColorBrush, bool needLayoutUpdate)
        {
            if (needLayoutUpdate)
            {
                base.UpdateLayout();
            }
            Border visualByName = this.GetVisualByName("FloatingContainerBorder") as Border;
            if (visualByName != null)
            {
                visualByName.BorderBrush = borderColorBrush;
            }
        }

        public static void SetIsActiveEx(DependencyObject obj, bool value)
        {
            obj.SetValue(IsActiveExProperty, value);
        }

        public static void SetIsMaximized(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMaximizedProperty, value);
        }

        public static void SetShowIcon(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowIconProperty, value);
        }

        public static void SetShowTitle(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowTitleProperty, value);
        }

        private Thickness SetTicknessWithoutTaskBar(Thickness tickness)
        {
            if (tickness.Bottom > tickness.Top)
            {
                tickness.Bottom = tickness.Top;
            }
            if (tickness.Right > tickness.Left)
            {
                tickness.Right = tickness.Left;
            }
            if (tickness.Top > tickness.Bottom)
            {
                tickness.Top = tickness.Bottom;
            }
            if (tickness.Left > tickness.Right)
            {
                tickness.Left = tickness.Right;
            }
            return tickness;
        }

        protected bool ShowSystemMenu(System.Windows.Point point) => 
            this.ShowSystemMenu(this.interopHelperCore.Handle, point);

        [SecuritySafeCritical]
        protected bool ShowSystemMenu(IntPtr hwnd, System.Windows.Point pt)
        {
            IntPtr ptr;
            if (!this.AllowSystemMenu)
            {
                return false;
            }
            if (hwnd == IntPtr.Zero)
            {
                return false;
            }
            if ((DateTime.Now - this.lastSystemMenuShownTime).TotalMilliseconds < 100.0)
            {
                return false;
            }
            try
            {
                this.allowProcessContextMenu = false;
                ptr = DevExpress.Xpf.Core.NativeMethods.TrackPopupMenu(this.GetMenuHandle(), 0x100, (int) pt.X, (int) pt.Y, 0, hwnd, IntPtr.Zero);
            }
            finally
            {
                this.lastSystemMenuShownTime = DateTime.Now;
                this.allowProcessContextMenu = true;
            }
            Win32.SendMessage(hwnd, 0x112, ptr.ToInt32(), IntPtr.Zero);
            return true;
        }

        protected virtual void ShowSystemMenuOnIconClick(System.Windows.Point pos)
        {
            this.ShowSystemMenu(this.interopHelperCore.Handle, pos);
        }

        protected virtual void ShowSystemMenuOnRightClick(System.Windows.Point pos)
        {
            this.ShowSystemMenu(this.interopHelperCore.Handle, pos);
        }

        protected virtual void UpdateActualResizeBorderThickness()
        {
            this.ActualResizeBorderThickness = this.IsAeroMode ? this.ResizeBorderThicknessInAeroMode : this.ResizeBorderThickness;
        }

        protected virtual void UpdateActualWindowTemplate()
        {
            this.ActualWindowTemplate = this.IsAeroMode ? this.AeroWindowTemplate : this.WindowTemplate;
        }

        private void UpdateDimensionsToRestoreBounds()
        {
            set_updateHwndLocation(this, false);
            set_updateHwndSize(this, false);
            updateDimensionsToRestoreBounds(this);
            set_updateHwndLocation(this, true);
            set_updateHwndSize(this, true);
        }

        protected virtual void UpdateFloatingContainerBodyMargin(bool hideCompletly)
        {
            UpdateFloatingContainerBodyMargin(this, hideCompletly);
        }

        protected static void UpdateFloatingContainerBodyMargin(DependencyObject root, bool hideCompletly)
        {
            Border visualByName = GetVisualByName(root, "FloatingContainerBody") as Border;
            if (visualByName != null)
            {
                visualByName.Margin = new Thickness(hideCompletly ? 0.0 : visualByName.Margin.Left);
                visualByName.Padding = new Thickness(visualByName.Padding.Left);
                Border border3 = LayoutHelper.FindElementByName(visualByName, "border") as Border;
                if (border3 != null)
                {
                    border3.Margin = new Thickness(border3.Margin.Left);
                    border3.Padding = new Thickness(border3.Padding.Left);
                }
            }
            Border border2 = GetVisualByName(root, "FloatingContainerBodyBorder") as Border;
            if (border2 != null)
            {
                border2.Margin = new Thickness(border2.Margin.Left);
            }
            if (hideCompletly)
            {
                if (visualByName != null)
                {
                    visualByName.Padding = new Thickness(0.0);
                }
                visualByName = GetVisualByName(root, "FloatingContainerBorder") as Border;
                if (visualByName != null)
                {
                    visualByName.Padding = new Thickness(0.0);
                }
            }
        }

        protected void UpdateFloatPaneBorders(bool maximized)
        {
            if (this.PartFloatPaneBorders != null)
            {
                this.PartFloatPaneBorders.Visibility = maximized ? Visibility.Hidden : Visibility.Visible;
            }
        }

        protected virtual void UpdateHeaderWidth()
        {
            if (this.FloatingContainerHeader != null)
            {
                if (!base.ShowInTaskbar && (base.WindowState == WindowState.Minimized))
                {
                    this.FloatingContainerHeader.Width = base.Width;
                    this.FloatingContainerHeader.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                }
                else
                {
                    this.FloatingContainerHeader.Width = double.NaN;
                    this.FloatingContainerHeader.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                }
            }
        }

        protected void UpdateIsAeroModeEnabled()
        {
            if (isAeroModeSupported)
            {
                this.IsAeroModeEnabled = DwmIsCompositionEnabled();
            }
        }

        protected virtual void UpdateNoResizeAndWSNone()
        {
            this.ResetFloatingContainerBodyMargin();
            if (base.WindowStyle == WindowStyle.None)
            {
                this.UpdateFloatingContainerBodyMargin(base.ResizeMode == ResizeMode.NoResize);
            }
        }

        protected void UpdateRootMargins(bool maximized)
        {
            if (this.PartRootGrid != null)
            {
                if (maximized)
                {
                    this.PartRootGrid.SetValue(FrameworkElement.MarginProperty, this.CalcRootMargins());
                }
                else
                {
                    this.PartRootGrid.ClearValue(FrameworkElement.MarginProperty);
                }
            }
        }

        [SecuritySafeCritical]
        private static void UpdateTransformationMatrixes()
        {
            IntPtr dC = DevExpress.Xpf.Core.NativeMethods.GetDC(IntPtr.Zero);
            double deviceCaps = DevExpress.Xpf.Core.NativeMethods.GetDeviceCaps(dC, 0x58);
            double num2 = DevExpress.Xpf.Core.NativeMethods.GetDeviceCaps(dC, 90);
            DeviceToDIPTransform = Matrix.Identity;
            DeviceToDIPTransform.Scale(96.0 / deviceCaps, 96.0 / num2);
            DIPToDeviceTransform = Matrix.Identity;
            DIPToDeviceTransform.Scale(deviceCaps / 96.0, num2 / 96.0);
            DevExpress.Xpf.Core.NativeMethods.ReleaseDC(IntPtr.Zero, dC);
        }

        private void WindowStateProcessing(IntPtr wParam)
        {
            switch (((int) wParam))
            {
                case 0:
                    if (this.previousWindowState == WindowState.Normal)
                    {
                        break;
                    }
                    if (base.WindowState != WindowState.Normal)
                    {
                        base.WindowState = WindowState.Normal;
                    }
                    this.previousWindowState = WindowState.Normal;
                    return;

                case 1:
                    if (this.previousWindowState == WindowState.Minimized)
                    {
                        break;
                    }
                    if (base.WindowState != WindowState.Minimized)
                    {
                        base.WindowState = WindowState.Minimized;
                    }
                    this.previousWindowState = WindowState.Minimized;
                    return;

                case 2:
                    if (this.previousWindowState != WindowState.Maximized)
                    {
                        if (base.WindowState != WindowState.Maximized)
                        {
                            this.UpdateDimensionsToRestoreBounds();
                            base.WindowState = WindowState.Maximized;
                        }
                        this.previousWindowState = WindowState.Maximized;
                        this.GetStandartButtonsVisibility();
                    }
                    break;

                default:
                    return;
            }
        }

        [SecurityCritical]
        private bool WmMoveChanged(IntPtr hwnd, IntPtr lParam)
        {
            this.WmMoveChangedImpl(hwnd, lParam);
            base.InvalidateVisual();
            return false;
        }

        [SecuritySafeCritical]
        private void WmMoveChangedImpl(IntPtr hwnd, IntPtr lParam)
        {
            DevExpress.Xpf.Core.NativeMethods.RECT rect = new DevExpress.Xpf.Core.NativeMethods.RECT(0, 0, 0, 0);
            DevExpress.Xpf.Core.NativeMethods.IntGetClientRect(new HandleRef(this, hwnd), ref rect);
            this.trueLeft = DevExpress.Xpf.Core.NativeMethods.SignedLOWORD(lParam);
            this.trueTop = DevExpress.Xpf.Core.NativeMethods.SignedHIWORD(lParam);
            System.Windows.Point point = this.ScreenToLogicalPixels(new System.Windows.Point((double) this.trueLeft, (double) this.trueTop));
            this.PatchField(point.X, "_actualLeft");
            this.PatchField(point.Y, "_actualTop");
            this.PatchWindowRegion(rect.right, rect.bottom);
            int right = this.trueLeft + rect.right;
            rect = new DevExpress.Xpf.Core.NativeMethods.RECT(this.trueLeft, this.trueTop, right, this.trueTop + rect.bottom);
            System.Type type = typeof(Window);
            if ((base.WindowState == WindowState.Minimized) && this.CanPatchLeftTop())
            {
                this.PatchLeftTop();
            }
            MethodInfo method = type.GetMethod("WmMoveChangedHelper", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (method != null)
            {
                method.Invoke(this, new object[0]);
            }
        }

        [SecuritySafeCritical]
        private bool WmSizeChanged(IntPtr wParam)
        {
            this.WindowStateProcessing(wParam);
            DevExpress.Xpf.Core.NativeMethods.RECT rect = new DevExpress.Xpf.Core.NativeMethods.RECT(0, 0, 0, 0);
            DevExpress.Xpf.Core.NativeMethods.IntGetClientRect(new HandleRef(this, this.interopHelperCore.Handle), ref rect);
            if ((base.WindowState == WindowState.Minimized) && ((base.Owner != null) && (this.FloatingContainerHeader != null)))
            {
                this.FloatingContainerHeader.Width = rect.right;
                this.FloatingContainerHeader.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                this.partFloatingContainerHeadersWasSet = true;
                this.SetElementVisibility(1.ToString(), Visibility.Hidden);
                this.SetElementVisibility(2.ToString(), Visibility.Visible);
            }
            else if (this.partFloatingContainerHeadersWasSet && (this.FloatingContainerHeader != null))
            {
                this.FloatingContainerHeader.ClearValue(FrameworkElement.WidthProperty);
                this.FloatingContainerHeader.ClearValue(FrameworkElement.HorizontalAlignmentProperty);
                this.ResetElementVisibility(1.ToString());
                this.ResetElementVisibility(2.ToString());
                this.partFloatingContainerHeadersWasSet = false;
            }
            this.PatchWindowRegion(rect.right, rect.bottom);
            return false;
        }

        public bool AllowApplicationIconScaling
        {
            get => 
                (bool) base.GetValue(AllowApplicationIconScalingProperty);
            set => 
                base.SetValue(AllowApplicationIconScalingProperty, value);
        }

        [Description("Gets or sets the template that defines the visual representation of the window. This is a dependency property.")]
        public DataTemplate WindowTemplate
        {
            get => 
                (DataTemplate) base.GetValue(WindowTemplateProperty);
            set => 
                base.SetValue(WindowTemplateProperty, value);
        }

        [Description("This member supports the internal infrastructure and is not intended to be used directly from your code.")]
        public DataTemplate AeroWindowTemplate
        {
            get => 
                (DataTemplate) base.GetValue(AeroWindowTemplateProperty);
            set => 
                base.SetValue(AeroWindowTemplateProperty, value);
        }

        public DataTemplate ActualWindowTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ActualWindowTemplateProperty);
            protected set => 
                base.SetValue(ActualWindowTemplatePropertyKey, value);
        }

        [Description("Gets or sets whether a window's icon is displayed.")]
        public bool ShowIcon
        {
            get => 
                (bool) base.GetValue(ShowIconProperty);
            set => 
                base.SetValue(ShowIconProperty, value);
        }

        [Description("Gets or sets whether to show the window's title.")]
        public bool ShowTitle
        {
            get => 
                (bool) base.GetValue(ShowTitleProperty);
            set => 
                base.SetValue(ShowTitleProperty, value);
        }

        [Description("This member supports the internal infrastructure, and is not intended to be used directly from your code.")]
        public ImageSource SmallIcon
        {
            get => 
                (ImageSource) base.GetValue(SmallIconProperty);
            set => 
                base.SetValue(SmallIconProperty, value);
        }

        [Description("This member supports the internal infrastructure and is not intended to be used directly from your code.")]
        public double AeroBorderSize
        {
            get => 
                (double) base.GetValue(AeroBorderSizeProperty);
            set => 
                base.SetValue(AeroBorderSizeProperty, value);
        }

        [Description("Gets or sets whether to activate the Aero Glass effect for a window. This is a dependency property.")]
        public bool IsAeroMode
        {
            get => 
                (bool) base.GetValue(IsAeroModeProperty);
            set => 
                base.SetValue(IsAeroModeProperty, value);
        }

        public IEnumerable HeaderItemsSource
        {
            get => 
                (IEnumerable) base.GetValue(HeaderItemsSourceProperty);
            set => 
                base.SetValue(HeaderItemsSourceProperty, value);
        }

        public ObservableCollection<object> HeaderItems =>
            (ObservableCollection<object>) base.GetValue(HeaderItemsProperty);

        public Style HeaderItemContainerStyle
        {
            get => 
                (Style) base.GetValue(HeaderItemContainerStyleProperty);
            set => 
                base.SetValue(HeaderItemContainerStyleProperty, value);
        }

        public StyleSelector HeaderItemContainerStyleSelector
        {
            get => 
                (StyleSelector) base.GetValue(HeaderItemContainerStyleSelectorProperty);
            set => 
                base.SetValue(HeaderItemContainerStyleSelectorProperty, value);
        }

        public DataTemplate HeaderItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(HeaderItemTemplateProperty);
            set => 
                base.SetValue(HeaderItemTemplateProperty, value);
        }

        public DataTemplateSelector HeaderItemTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(HeaderItemTemplateSelectorProperty);
            set => 
                base.SetValue(HeaderItemTemplateSelectorProperty, value);
        }

        public Thickness ResizeBorderThickness
        {
            get => 
                (Thickness) base.GetValue(ResizeBorderThicknessProperty);
            set => 
                base.SetValue(ResizeBorderThicknessProperty, value);
        }

        public Thickness ResizeBorderThicknessInAeroMode
        {
            get => 
                (Thickness) base.GetValue(ResizeBorderThicknessInAeroModeProperty);
            set => 
                base.SetValue(ResizeBorderThicknessInAeroModeProperty, value);
        }

        public Thickness BorderEffectLeftMargins
        {
            get => 
                (Thickness) base.GetValue(BorderEffectLeftMarginsProperty);
            set
            {
            }
        }

        public Thickness BorderEffectRightMargins
        {
            get => 
                (Thickness) base.GetValue(BorderEffectRightMarginsProperty);
            set
            {
            }
        }

        public Thickness BorderEffectTopMargins
        {
            get => 
                (Thickness) base.GetValue(BorderEffectTopMarginsProperty);
            set
            {
            }
        }

        public Thickness BorderEffectBottomMargins
        {
            get => 
                (Thickness) base.GetValue(BorderEffectBottomMarginsProperty);
            set
            {
            }
        }

        public TextBlock BorderEffectImagesUri
        {
            get => 
                (TextBlock) base.GetValue(BorderEffectImagesUriProperty);
            set
            {
            }
        }

        public Thickness BorderEffectOffset
        {
            get => 
                (Thickness) base.GetValue(BorderEffectOffsetProperty);
            set
            {
            }
        }

        public SolidColorBrush BorderEffectActiveColor
        {
            get => 
                (SolidColorBrush) base.GetValue(BorderEffectActiveColorProperty);
            set => 
                base.SetValue(BorderEffectActiveColorProperty, value);
        }

        public SolidColorBrush BorderEffectInactiveColor
        {
            get => 
                (SolidColorBrush) base.GetValue(BorderEffectInactiveColorProperty);
            set => 
                base.SetValue(BorderEffectInactiveColorProperty, value);
        }

        public Thickness ActualResizeBorderThickness
        {
            get => 
                this.actualResizeBorderThicknessCore;
            private set
            {
                if (this.actualResizeBorderThicknessCore != value)
                {
                    Thickness actualResizeBorderThicknessCore = this.actualResizeBorderThicknessCore;
                    this.actualResizeBorderThicknessCore = value;
                    this.OnActualResizeBorderThicknessChanged(actualResizeBorderThicknessCore);
                }
            }
        }

        public bool AllowChangeRenderMode
        {
            get => 
                this.allowChangeRenderModeCore;
            set => 
                this.allowChangeRenderModeCore = value;
        }

        public bool EnableTransparency
        {
            get => 
                this.enableTransparencyCore;
            set
            {
                if (this.interopHelperCore != null)
                {
                    throw new ApplicationException("Can not change the EnableTransparency property after window handle created");
                }
                this.enableTransparencyCore = value;
            }
        }

        public bool IsDraggingOrResizing
        {
            get => 
                (bool) base.GetValue(IsDraggingOrResizingProperty);
            set => 
                base.SetValue(IsDraggingOrResizingProperty, value);
        }

        protected bool IsAeroModeEnabled
        {
            get => 
                this.isAeroModeEnabledCore;
            set
            {
                if ((this.isAeroModeEnabledCore != value) && isAeroModeSupported)
                {
                    bool isAeroModeEnabledCore = this.isAeroModeEnabledCore;
                    this.isAeroModeEnabledCore = value;
                    this.OnIsAeroModeEnabledChanged(isAeroModeEnabledCore);
                }
            }
        }

        protected Grid PartFloatPaneBorders { get; private set; }

        protected FrameworkElement FloatingContainerHeader { get; private set; }

        internal ItemsControl HeaderButtons { get; private set; }

        internal System.Windows.Controls.Image IconImage { get; private set; }

        protected Grid PartRootGrid { get; private set; }

        private bool ActiveState
        {
            get => 
                (bool) base.GetValue(IsActiveExProperty);
            set
            {
                if (this.decorator != null)
                {
                    this.decorator.ActiveStateChanged(value);
                    this.RedrawBorderWithEffectColor(value, false);
                }
            }
        }

        protected HwndSource HwndSourceWindow
        {
            get
            {
                PropertyInfo property = typeof(Window).GetProperty("HwndSourceWindow", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                return ((property == null) ? null : ((HwndSource) property.GetValue(this, new object[0])));
            }
        }

        public System.Windows.Size HeaderSize
        {
            get => 
                this.headerSizeCore;
            set
            {
                if (this.headerSizeCore != value)
                {
                    System.Windows.Size headerSizeCore = this.headerSizeCore;
                    this.headerSizeCore = value;
                    this.OnHeaderSizeChanged(headerSizeCore);
                }
            }
        }

        public Rect ClientArea =>
            this.GetClientArea();

        protected virtual bool NeedReAttachToVisualTree =>
            !ReferenceEquals(this.closeButton, this.GetVisualByName(0.ToString()));

        private DispatcherTimer DoubleClickTimer
        {
            get
            {
                if (this.doubleClickTimer == null)
                {
                    this.doubleClickTimer = new DispatcherTimer();
                    this.doubleClickTimer.Interval = TimeSpan.FromMilliseconds((double) SystemInformation.DoubleClickTime);
                    this.doubleClickTimer.Tick += new EventHandler(this.OnDoubleClickTimerTick);
                }
                return this.doubleClickTimer;
            }
        }

        protected virtual bool CanResizeOrMaximize =>
            (base.ResizeMode != ResizeMode.NoResize) && (base.ResizeMode != ResizeMode.CanMinimize);

        private static bool IsWinSevenOrHigher
        {
            get
            {
                Version version = Environment.OSVersion.Version;
                return (((version.Major < 6) || (version.Minor < 1)) ? (version.Major >= 7) : true);
            }
        }

        protected internal Rect WindowRect =>
            this.GetWindowRect();

        protected internal Rect WindowRectInLogicalUnits
        {
            get
            {
                Rect windowRect = this.WindowRect;
                System.Windows.Point point2 = this.ScreenToLogicalPixels(new System.Windows.Point(windowRect.Width, windowRect.Height));
                return new Rect(this.ScreenToLogicalPixels(windowRect.Location), new System.Windows.Size(point2.X, point2.Y));
            }
        }

        public bool AllowSystemMenu
        {
            get => 
                (bool) base.GetValue(AllowSystemMenuProperty);
            set => 
                base.SetValue(AllowSystemMenuProperty, value);
        }

        protected EventHandlerList Events
        {
            get
            {
                this.events ??= new EventHandlerList();
                return this.events;
            }
        }

        [DefaultValue(1)]
        public DevExpress.Xpf.Core.BorderEffect BorderEffect
        {
            get => 
                this.borderEffect;
            set
            {
                if (this.BorderEffect != value)
                {
                    this.borderEffect = value;
                    this.OnAllowBorderEffectChanged();
                }
            }
        }

        bool ILogicalOwner.IsLoaded =>
            base.IsLoaded;

        double ILogicalOwner.ActualWidth =>
            base.ActualWidth;

        double ILogicalOwner.ActualHeight =>
            base.ActualHeight;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXWindow.<>c <>9 = new DXWindow.<>c();

            internal void <.cctor>b__29_0(Window window, bool b)
            {
                ((DXWindow) window).Set_updateHwndLocation(b);
            }

            internal void <.cctor>b__29_1(Window window, bool b)
            {
                ((DXWindow) window).Set_updateHwndSize(b);
            }

            internal void <.cctor>b__29_2(Window window)
            {
                Rect restoreBounds = window.RestoreBounds;
                window.SetValue(Window.LeftProperty, restoreBounds.Left);
                window.SetValue(Window.TopProperty, restoreBounds.Top);
                window.SetValue(FrameworkElement.WidthProperty, restoreBounds.Width);
                window.SetValue(FrameworkElement.HeightProperty, restoreBounds.Height);
            }
        }

        public enum ButtonParts
        {
            PART_CloseButton,
            PART_Minimize,
            PART_Restore,
            PART_Maximize
        }

        private class ColorTranslator
        {
            public static System.Windows.Media.Color FromArgb(int argb) => 
                System.Windows.Media.Color.FromArgb(GetA(argb), GetR(argb), GetG(argb), GetB(argb));

            private static byte GetA(int argb) => 
                (byte) ((argb >> 0x18) & 0xffL);

            private static byte GetB(int argb) => 
                (byte) (argb & 0xffL);

            private static byte GetG(int argb) => 
                (byte) ((argb >> 8) & 0xffL);

            private static byte GetR(int argb) => 
                (byte) ((argb >> 0x10) & 0xffL);
        }

        public enum OtherParts
        {
            PART_DragWidget,
            PART_StatusPanel,
            PART_Icon,
            PART_Text,
            PART_Header,
            PART_HeaderButtons,
            PART_HeaderCustomItems,
            AdditionalDragWidget
        }

        public enum SysMenuItems
        {
            Restore,
            Move,
            Size,
            Minimize,
            Maximize,
            Close
        }

        [StructLayout(LayoutKind.Sequential)]
        protected struct WindowMinMax
        {
            internal double minWidth;
            internal double maxWidth;
            internal double minHeight;
            internal double maxHeight;
            internal WindowMinMax(double minSize, double maxSize)
            {
                this.minWidth = minSize;
                this.maxWidth = maxSize;
                this.minHeight = minSize;
                this.maxHeight = maxSize;
            }
        }
    }
}

