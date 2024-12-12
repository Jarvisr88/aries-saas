namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.Ribbon;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Shapes;

    [ContentProperty("Content")]
    public class ThemedWindow : Window, IRibbonWindow, IHeaderItemsControlHost, IBackButtonSupport, IWindowSurrogate
    {
        public static readonly RoutedEvent BackRequestedEvent;
        public static readonly DependencyProperty NavigateBackCommandProperty;
        public static readonly DependencyProperty ShowBackButtonProperty;
        public static readonly DependencyProperty HeaderItemsSourceProperty;
        private static readonly DependencyPropertyKey HeaderItemsPropertyKey;
        public static readonly DependencyProperty HeaderItemsProperty;
        public static readonly DependencyProperty HeaderItemContainerStyleProperty;
        public static readonly DependencyProperty HeaderItemContainerStyleSelectorProperty;
        public static readonly DependencyProperty HeaderItemTemplateProperty;
        public static readonly DependencyProperty HeaderItemTemplateSelectorProperty;
        public static readonly DependencyProperty ToolbarItemsSourceProperty;
        private static readonly DependencyPropertyKey ToolbarItemsPropertyKey;
        public static readonly DependencyProperty ToolbarItemsProperty;
        public static readonly DependencyProperty ToolbarItemContainerStyleProperty;
        public static readonly DependencyProperty ToolbarItemContainerStyleSelectorProperty;
        public static readonly DependencyProperty ToolbarItemTemplateProperty;
        public static readonly DependencyProperty ToolbarItemTemplateSelectorProperty;
        public static readonly DependencyProperty ShowGlowProperty;
        public static readonly DependencyProperty ActiveGlowColorProperty;
        public static readonly DependencyProperty InactiveGlowColorProperty;
        public static readonly DependencyProperty UseGlowColorsProperty;
        private static readonly SolidColorBrush defaultGlowColor = new SolidColorBrush(Color.FromRgb(110, 110, 110));
        public static readonly DependencyProperty ShowIconProperty;
        public static readonly DependencyProperty ShowTitleProperty;
        public static readonly DependencyProperty TitleAlignmentProperty;
        private static readonly DependencyPropertyKey ActualWindowKindPropertyKey;
        public static readonly DependencyProperty ActualWindowKindProperty;
        public static readonly DependencyProperty WindowKindProperty;
        private static readonly DependencyPropertyKey ActualDialogButtonsPropertyKey;
        public static readonly DependencyProperty ActualDialogButtonsProperty;
        private static readonly DependencyPropertyKey IsDialogPropertyKey;
        public static readonly DependencyProperty IsDialogProperty;
        private static readonly Func<Window, bool> get_ShowingAsDialog;
        internal static readonly DependencyPropertyKey DialogButtonsPropertyKey;
        public static readonly DependencyProperty DialogButtonsProperty;
        public static readonly DependencyProperty DialogButtonsSourceProperty;
        private static readonly DependencyPropertyKey ShowDialogFooterPropertyKey;
        public static readonly DependencyProperty ShowDialogFooterProperty;
        public static readonly DependencyProperty EnableAcrylicProperty;
        public static readonly DependencyProperty AcrylicOpacityProperty;
        public static readonly DependencyProperty AcrylicColorProperty;
        public static readonly DependencyProperty HeaderBackgroundProperty;
        public static readonly DependencyProperty HeaderForegroundProperty;
        public static readonly DependencyProperty ControlBoxButtonSetProperty;
        public static readonly DependencyProperty ResizeBorderThicknessProperty;
        public static readonly DependencyProperty ShowStatusPanelProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ShowSearchBoxProperty;
        private double captionHeight;
        private IEnumerable<UICommand> internalDialogButtons;
        private bool recalcWindowKindAfterArrange;
        private DXTabControl tabControl;
        private TabOrientablePanel tabHeaderPanel;
        private DXTabItem tabItem;
        private Rectangle acrylicBackground;
        private bool? forceShowIcon;
        private IRibbonControl ribbonControl;
        private FrameworkElement controlBoxContainer;

        public event RoutedEventHandler BackRequested
        {
            add
            {
                base.AddHandler(BackRequestedEvent, value);
            }
            remove
            {
                base.RemoveHandler(BackRequestedEvent, value);
            }
        }

        event EventHandler IWindowSurrogate.Activated
        {
            add
            {
                base.Activated += value;
            }
            remove
            {
                base.Activated -= value;
            }
        }

        event EventHandler IWindowSurrogate.Closed
        {
            add
            {
                base.Closed += value;
            }
            remove
            {
                base.Closed -= value;
            }
        }

        event CancelEventHandler IWindowSurrogate.Closing
        {
            add
            {
                base.Closing += value;
            }
            remove
            {
                base.Closing -= value;
            }
        }

        event EventHandler IWindowSurrogate.Deactivated
        {
            add
            {
                base.Deactivated += value;
            }
            remove
            {
                base.Deactivated -= value;
            }
        }

        static ThemedWindow()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemedWindow), new FrameworkPropertyMetadata(typeof(ThemedWindow)));
            BackRequestedEvent = EventManager.RegisterRoutedEvent("BackRequested", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(ThemedWindow));
            NavigateBackCommandProperty = DependencyProperty.Register("NavigateBackCommand", typeof(ICommand), typeof(ThemedWindow));
            ShowBackButtonProperty = DependencyProperty.Register("ShowBackButton", typeof(bool), typeof(ThemedWindow), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(ThemedWindow.OnShowBackButtonPropertyChanged), new CoerceValueCallback(ThemedWindow.CoerceShowBackButtonProperty)));
            HeaderItemsPropertyKey = DependencyProperty.RegisterReadOnly("HeaderItems", typeof(ObservableCollection<object>), typeof(ThemedWindow), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ThemedWindow.OnHeaderItemsPropertyChanged)));
            HeaderItemsProperty = HeaderItemsPropertyKey.DependencyProperty;
            HeaderItemsSourceProperty = DependencyProperty.Register("HeaderItemsSource", typeof(IEnumerable), typeof(ThemedWindow), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ThemedWindow.OnHeaderItemsSourcePropertyChanged)));
            HeaderItemContainerStyleProperty = DependencyProperty.Register("HeaderItemContainerStyle", typeof(Style), typeof(ThemedWindow), new FrameworkPropertyMetadata(null));
            HeaderItemContainerStyleSelectorProperty = DependencyProperty.Register("HeaderItemContainerStyleSelector", typeof(StyleSelector), typeof(ThemedWindow), new FrameworkPropertyMetadata(null));
            HeaderItemTemplateProperty = DependencyProperty.Register("HeaderItemTemplate", typeof(DataTemplate), typeof(ThemedWindow), new FrameworkPropertyMetadata(null));
            HeaderItemTemplateSelectorProperty = DependencyProperty.Register("HeaderItemTemplateSelector", typeof(DataTemplateSelector), typeof(ThemedWindow), new FrameworkPropertyMetadata(null));
            ToolbarItemsPropertyKey = DependencyProperty.RegisterReadOnly("ToolbarItems", typeof(ObservableCollection<object>), typeof(ThemedWindow), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ThemedWindow.OnToolbarItemsPropertyChanged)));
            ToolbarItemsProperty = ToolbarItemsPropertyKey.DependencyProperty;
            ToolbarItemsSourceProperty = DependencyProperty.Register("ToolbarItemsSource", typeof(IEnumerable), typeof(ThemedWindow), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ThemedWindow.OnToolbarItemsSourcePropertyChanged)));
            ToolbarItemContainerStyleProperty = DependencyProperty.Register("ToolbarItemContainerStyle", typeof(Style), typeof(ThemedWindow), new FrameworkPropertyMetadata(null));
            ToolbarItemContainerStyleSelectorProperty = DependencyProperty.Register("ToolbarItemContainerStyleSelector", typeof(StyleSelector), typeof(ThemedWindow), new FrameworkPropertyMetadata(null));
            ToolbarItemTemplateProperty = DependencyProperty.Register("ToolbarItemTemplate", typeof(DataTemplate), typeof(ThemedWindow), new FrameworkPropertyMetadata(null));
            ToolbarItemTemplateSelectorProperty = DependencyProperty.Register("ToolbarItemTemplateSelector", typeof(DataTemplateSelector), typeof(ThemedWindow), new FrameworkPropertyMetadata(null));
            ShowIconProperty = DependencyProperty.Register("ShowIcon", typeof(bool), typeof(ThemedWindow), new FrameworkPropertyMetadata(true, null, new CoerceValueCallback(ThemedWindow.CoerceShowIconProperty)));
            ShowTitleProperty = DependencyProperty.Register("ShowTitle", typeof(bool), typeof(ThemedWindow), new FrameworkPropertyMetadata(true, new PropertyChangedCallback(ThemedWindow.OnShowTitlePropertyChanged)));
            TitleAlignmentProperty = DependencyProperty.Register("TitleAlignment", typeof(WindowTitleAlignment), typeof(ThemedWindow), new FrameworkPropertyMetadata(WindowTitleAlignment.Left, new PropertyChangedCallback(ThemedWindow.OnTitleAlignmentPropertyChanged), new CoerceValueCallback(ThemedWindow.CoerceTitleAlignmentProperty)));
            WindowChrome.CaptionHeightProperty.OverrideMetadata(typeof(ThemedWindow), new FrameworkPropertyMetadata(new PropertyChangedCallback(ThemedWindow.OnCaptionHeightPropertyChanged)));
            Window.WindowStyleProperty.OverrideMetadata(typeof(ThemedWindow), new FrameworkPropertyMetadata(new PropertyChangedCallback(ThemedWindow.OnWindowStylePropertyChanged)));
            Control.PaddingProperty.OverrideMetadata(typeof(ThemedWindow), new FrameworkPropertyMetadata(null, new CoerceValueCallback(ThemedWindow.CoercePaddingProperty)));
            WindowKindProperty = DependencyProperty.Register("WindowKind", typeof(DevExpress.Xpf.Core.WindowKind), typeof(ThemedWindow), new FrameworkPropertyMetadata(DevExpress.Xpf.Core.WindowKind.Auto, new PropertyChangedCallback(ThemedWindow.OnWindowKindPropertyChanged)));
            ActualWindowKindPropertyKey = DependencyProperty.RegisterReadOnly("ActualWindowKind", typeof(DevExpress.Xpf.Core.WindowKind), typeof(ThemedWindow), new FrameworkPropertyMetadata(DevExpress.Xpf.Core.WindowKind.Normal, new PropertyChangedCallback(ThemedWindow.OnActualWindowKindPropertyChanged), new CoerceValueCallback(ThemedWindow.CoerceActualWindowKindProperty)));
            ActualWindowKindProperty = ActualWindowKindPropertyKey.DependencyProperty;
            EnableAcrylicProperty = DependencyProperty.Register("EnableAcrylic", typeof(bool), typeof(ThemedWindow), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(ThemedWindow.OnEnableAcrylicPropertyChanged), new CoerceValueCallback(ThemedWindow.CoerceEnableAcrylicProperty)));
            AcrylicOpacityProperty = DependencyProperty.Register("AcrylicOpacity", typeof(double), typeof(ThemedWindow), new FrameworkPropertyMetadata(0.9, new PropertyChangedCallback(ThemedWindow.OnAcrylicOpacityPropertyChanged)));
            Color defaultValue = new Color();
            AcrylicColorProperty = DependencyProperty.Register("AcrylicColor", typeof(Color), typeof(ThemedWindow), new FrameworkPropertyMetadata(defaultValue, new PropertyChangedCallback(ThemedWindow.OnAcrylicColorPropertyChanged)));
            ShowGlowProperty = DependencyProperty.Register("ShowGlow", typeof(bool), typeof(ThemedWindow), new FrameworkPropertyMetadata(true, new PropertyChangedCallback(ThemedWindow.OnShowGlowPropertyChanged)));
            UseGlowColorsProperty = DependencyProperty.Register("UseGlowColors", typeof(bool), typeof(ThemedWindow), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(ThemedWindow.OnUseGlowColorsPropertyChanged)));
            ActiveGlowColorProperty = DependencyProperty.Register("ActiveGlowColor", typeof(SolidColorBrush), typeof(ThemedWindow), new FrameworkPropertyMetadata(defaultGlowColor, new PropertyChangedCallback(ThemedWindow.OnActiveGlowColorPropertyChanged), new CoerceValueCallback(ThemedWindow.CoerceGlowColorProperty)));
            InactiveGlowColorProperty = DependencyProperty.Register("InactiveGlowColor", typeof(SolidColorBrush), typeof(ThemedWindow), new FrameworkPropertyMetadata(defaultGlowColor, new PropertyChangedCallback(ThemedWindow.OnInactiveGlowColorPropertyChanged), new CoerceValueCallback(ThemedWindow.CoerceGlowColorProperty)));
            ControlBoxButtonSetProperty = DependencyProperty.Register("ControlBoxButtonSet", typeof(ControlBoxButtons), typeof(ThemedWindow), new FrameworkPropertyMetadata(ControlBoxButtons.Minimize | ControlBoxButtons.MaximizeRestore | ControlBoxButtons.Close));
            ResizeBorderThicknessProperty = DependencyProperty.Register("ResizeBorderThickness", typeof(Thickness), typeof(ThemedWindow), new FrameworkPropertyMetadata(new Thickness(5.0)));
            ShowStatusPanelProperty = DependencyProperty.Register("ShowStatusPanel", typeof(bool), typeof(ThemedWindow), new FrameworkPropertyMetadata(false));
            ActualDialogButtonsPropertyKey = DependencyProperty.RegisterReadOnly("ActualDialogButtons", typeof(IEnumerable), typeof(ThemedWindow), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ThemedWindow.OnActualDialogButtonsPropertyChanged)));
            ActualDialogButtonsProperty = ActualDialogButtonsPropertyKey.DependencyProperty;
            IsDialogPropertyKey = DependencyProperty.RegisterReadOnly("IsDialog", typeof(bool), typeof(ThemedWindow), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(ThemedWindow.OnIsDialogPropertyChanged)));
            IsDialogProperty = IsDialogPropertyKey.DependencyProperty;
            get_ShowingAsDialog = DevExpress.Xpf.Core.Internal.ReflectionHelper.CreateFieldGetter<Window, bool>(typeof(Window), "_showingAsDialog", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            DialogButtonsPropertyKey = DependencyProperty.RegisterReadOnly("DialogButtons", typeof(ObservableCollection<object>), typeof(ThemedWindow), new FrameworkPropertyMetadata(null));
            DialogButtonsProperty = DialogButtonsPropertyKey.DependencyProperty;
            DialogButtonsSourceProperty = DependencyProperty.Register("DialogButtonsSource", typeof(IEnumerable), typeof(ThemedWindow), new FrameworkPropertyMetadata(null));
            ShowDialogFooterPropertyKey = DependencyProperty.RegisterReadOnly("ShowDialogFooter", typeof(bool), typeof(ThemedWindow), new FrameworkPropertyMetadata(false));
            ShowDialogFooterProperty = ShowDialogFooterPropertyKey.DependencyProperty;
            HeaderBackgroundProperty = DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(ThemedWindow), new FrameworkPropertyMetadata(SystemParameters.WindowGlassBrush));
            HeaderForegroundProperty = DependencyProperty.Register("HeaderForeground", typeof(Brush), typeof(ThemedWindow), new FrameworkPropertyMetadata(null));
            ShowSearchBoxProperty = DependencyProperty.Register("ShowSearchBox", typeof(bool), typeof(ThemedWindow), new PropertyMetadata(false));
            DXSerializer.EnabledProperty.OverrideMetadata(typeof(ThemedWindow), new UIPropertyMetadata(false));
            DXSerializer.SerializationIDDefaultProperty.OverrideMetadata(typeof(ThemedWindow), new UIPropertyMetadata(typeof(ThemedWindow).Name));
            DXSerializer.SerializationProviderProperty.OverrideMetadata(typeof(ThemedWindow), new UIPropertyMetadata(new WindowSerializationProvider()));
        }

        public ThemedWindow()
        {
            DXWindowsHelper.SetWindowKind(this, "ThemedWindow");
            DXWindowsHelper.SetWindow(this, this);
            base.CoerceValue(Window.WindowStyleProperty);
            this.CreateHeaderItemsCollection();
            this.CreateToolbarItemsCollection();
            this.CreateDialogButtonsCollection();
            this.SubscribeEvents();
        }

        private void ApplyNormalWindowKind()
        {
        }

        private void ApplyRibbonWindowKind()
        {
            this.SetRibbonControlMargin();
        }

        private void ApplyTabbedWindowKind()
        {
            this.SetTabControlMargin();
            if (this.TabHeaderPanel != null)
            {
                this.SetTabPanelMargin();
            }
        }

        internal void ApplyWindowKind(DevExpress.Xpf.Core.WindowKind windowKind)
        {
            switch (windowKind)
            {
                case DevExpress.Xpf.Core.WindowKind.Normal:
                    this.ApplyNormalWindowKind();
                    return;

                case DevExpress.Xpf.Core.WindowKind.Tabbed:
                    if (this.TabControl != null)
                    {
                        this.ApplyTabbedWindowKind();
                    }
                    return;

                case DevExpress.Xpf.Core.WindowKind.Ribbon:
                    HeaderItemControl.SetUseExtendedTemplateProviders(this, true);
                    if (((IRibbonWindow) this).Ribbon != null)
                    {
                        this.ApplyRibbonWindowKind();
                    }
                    return;
            }
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Size size;
            try
            {
                size = base.ArrangeOverride(arrangeBounds);
            }
            finally
            {
                if (this.recalcWindowKindAfterArrange)
                {
                    this.RecalcWindowKind();
                    this.recalcWindowKindAfterArrange = false;
                }
            }
            return size;
        }

        private void CheckIfNeedToUseMvvmMessageResult(IEnumerable<UICommand> uiCommands)
        {
            if (uiCommands != null)
            {
                List<UICommand> source = uiCommands.ToList<UICommand>();
                UICommand command = source.Any<UICommand>() ? source.First<UICommand>() : null;
                if ((((command != null) ? command.Id : MessageResult.None) is MessageResult) && (command.Id == command.Tag))
                {
                    ThemedWindowsHelper.SetUseMvvmMessageResultAsDialogResult(this, true);
                }
            }
        }

        private void ClearContentPresenterMargin()
        {
            System.Windows.Controls.ContentPresenter contentPresenter = this.ContentPresenter;
            if (contentPresenter == null)
            {
                System.Windows.Controls.ContentPresenter local1 = contentPresenter;
            }
            else
            {
                contentPresenter.ClearValue(FrameworkElement.MarginProperty);
            }
        }

        public void CloseDialog(UICommand result)
        {
            if (!ThemedWindowsHelper.GetIsWindowClosingWithCustomCommand(this))
            {
                ThemedWindowsHelper.SetIsWindowClosingWithCustomCommand(this, true);
                CancelEventArgs parameter = new CancelEventArgs();
                ICommand command = result.Command;
                if ((command != null) && command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }
                if (!parameter.Cancel)
                {
                    ThemedWindowDialogButton.CalcButtonResult(this, MessageBoxResult.None, result);
                }
                ThemedWindowsHelper.SetIsWindowClosingWithCustomCommand(this, false);
            }
        }

        private DevExpress.Xpf.Core.WindowKind CoerceActualWindowKind(DevExpress.Xpf.Core.WindowKind value) => 
            ((base.WindowStyle == WindowStyle.None) || this.EnableAcrylic) ? DevExpress.Xpf.Core.WindowKind.Normal : value;

        private static object CoerceActualWindowKindProperty(DependencyObject d, object value) => 
            ((ThemedWindow) d).CoerceActualWindowKind((DevExpress.Xpf.Core.WindowKind) value);

        private bool CoerceEnableAcrylic(bool value)
        {
            if (PresentationSource.FromVisual(this) != null)
            {
                throw new InvalidOperationException("Cannot change EnableAcrylic after a Window has been shown or WindowInteropHelper.EnsureHandle has been called.");
            }
            return (WindowBlurBehindHelper.IsBlurBehindSupported & value);
        }

        private static object CoerceEnableAcrylicProperty(DependencyObject d, object value) => 
            ((ThemedWindow) d).CoerceEnableAcrylic((bool) value);

        private SolidColorBrush CoerceGlowColor(SolidColorBrush glowColor) => 
            glowColor ?? defaultGlowColor;

        private static object CoerceGlowColorProperty(DependencyObject d, object value) => 
            ((ThemedWindow) d).CoerceGlowColor((SolidColorBrush) value);

        private Size CoerceIfMeasureSpecified(Size result)
        {
            double a = (double.IsInfinity(base.MaxWidth) || (base.MaxWidth > result.Width)) ? this.WindowRoot.DesiredSize.Width : result.Width;
            double num2 = (double.IsInfinity(base.MaxHeight) || (base.MaxHeight > result.Height)) ? this.WindowRoot.DesiredSize.Height : result.Height;
            return new Size(Math.Ceiling(a), Math.Ceiling(num2));
        }

        private Thickness CoercePadding(Thickness thickness) => 
            (base.WindowStyle == WindowStyle.None) ? new Thickness(thickness.Left) : thickness;

        private static object CoercePaddingProperty(DependencyObject d, object value) => 
            ((ThemedWindow) d).CoercePadding((Thickness) value);

        private bool CoerceShowBackButton(bool value) => 
            (this.ActualWindowKind == DevExpress.Xpf.Core.WindowKind.Normal) & value;

        private static object CoerceShowBackButtonProperty(DependencyObject d, object value) => 
            ((ThemedWindow) d).CoerceShowBackButton((bool) value);

        private bool CoerceShowIcon(bool value)
        {
            bool? forceShowIcon = this.forceShowIcon;
            return ((forceShowIcon != null) ? forceShowIcon.GetValueOrDefault() : value);
        }

        private static object CoerceShowIconProperty(DependencyObject d, object value) => 
            ((ThemedWindow) d).CoerceShowIcon((bool) value);

        private WindowTitleAlignment CoerceTitleAlignment(WindowTitleAlignment titleAlignment) => 
            (this.ActualWindowKind == DevExpress.Xpf.Core.WindowKind.Tabbed) ? WindowTitleAlignment.Left : titleAlignment;

        private static object CoerceTitleAlignmentProperty(DependencyObject d, object value) => 
            ((ThemedWindow) d).CoerceTitleAlignment((WindowTitleAlignment) value);

        private static void CreateDefaultButtonsCollection(ObservableCollection<UICommand> dialogButtonsCollection)
        {
            MessageBoxResult? defaultButton = null;
            dialogButtonsCollection.Add(CreateUICommand(MessageBoxResult.OK, true, false, defaultButton));
            defaultButton = null;
            dialogButtonsCollection.Add(CreateUICommand(MessageBoxResult.Cancel, false, true, defaultButton));
        }

        private IEnumerable CreateDialogButtons() => 
            CreateMergedDialogButtonsCollections(this.GetInternalDialogButtons(this.internalDialogButtons), this.DialogButtons);

        private static IEnumerable<UICommand> CreateDialogButtons(IEnumerable<UICommand> dialogButtons)
        {
            ObservableCollection<UICommand> dialogButtonsCollection = new ObservableCollection<UICommand>();
            if (dialogButtons == null)
            {
                CreateDefaultButtonsCollection(dialogButtonsCollection);
                return dialogButtonsCollection;
            }
            foreach (UICommand command in dialogButtons)
            {
                command.Id ??= MessageBoxResult.None;
                dialogButtonsCollection.Add(command);
            }
            return dialogButtonsCollection;
        }

        private static IEnumerable<UICommand> CreateDialogButtons(MessageBoxButton? dialogButtons = new MessageBoxButton?(), MessageBoxResult? defaultButton = new MessageBoxResult?())
        {
            ObservableCollection<UICommand> dialogButtonsCollection = new ObservableCollection<UICommand>();
            MessageBoxButton? nullable = dialogButtons;
            if (nullable == null)
            {
                CreateDefaultButtonsCollection(dialogButtonsCollection);
            }
            else
            {
                switch (nullable.GetValueOrDefault())
                {
                    case MessageBoxButton.OK:
                        dialogButtonsCollection.Add(CreateUICommand(MessageBoxResult.OK, true, true, defaultButton));
                        break;

                    case MessageBoxButton.OKCancel:
                        dialogButtonsCollection.Add(CreateUICommand(MessageBoxResult.OK, true, false, defaultButton));
                        dialogButtonsCollection.Add(CreateUICommand(MessageBoxResult.Cancel, false, true, defaultButton));
                        break;

                    case MessageBoxButton.YesNoCancel:
                        dialogButtonsCollection.Add(CreateUICommand(MessageBoxResult.Yes, true, false, defaultButton));
                        dialogButtonsCollection.Add(CreateUICommand(MessageBoxResult.No, false, false, defaultButton));
                        dialogButtonsCollection.Add(CreateUICommand(MessageBoxResult.Cancel, false, true, defaultButton));
                        break;

                    case MessageBoxButton.YesNo:
                        dialogButtonsCollection.Add(CreateUICommand(MessageBoxResult.Yes, true, false, defaultButton));
                        dialogButtonsCollection.Add(CreateUICommand(MessageBoxResult.No, false, false, defaultButton));
                        break;

                    default:
                        break;
                }
            }
            return dialogButtonsCollection;
        }

        private void CreateDialogButtonsCollection()
        {
            base.SetValue(DialogButtonsPropertyKey, new ObservableCollection<object>());
        }

        private void CreateHeaderItemsCollection()
        {
            base.SetValue(HeaderItemsPropertyKey, new ObservableCollection<object>());
        }

        private static IEnumerable CreateMergedDialogButtonsCollections(IEnumerable<UICommand> codeDialogButtons, ObservableCollection<object> xamlDialogButtons)
        {
            if (codeDialogButtons == null)
            {
                return xamlDialogButtons;
            }
            List<UICommand> source = codeDialogButtons.ToList<UICommand>();
            return (xamlDialogButtons.Any<object>() ? (source.Any<UICommand>() ? xamlDialogButtons.Concat<object>(source) : xamlDialogButtons) : source);
        }

        private void CreateToolbarItemsCollection()
        {
            base.SetValue(ToolbarItemsPropertyKey, new ObservableCollection<object>());
        }

        private static UICommand CreateUICommand(MessageBoxResult name, bool defaultValueIsDefault, bool isCancel, MessageBoxResult? defaultButton = new MessageBoxResult?())
        {
            bool flag1;
            UICommand command1 = new UICommand();
            command1.Id = name;
            UICommand command2 = command1;
            if (defaultButton != null)
            {
                MessageBoxResult? nullable = defaultButton;
                MessageBoxResult none = MessageBoxResult.None;
                if ((((MessageBoxResult) nullable.GetValueOrDefault()) == none) ? (nullable == null) : true)
                {
                    nullable = defaultButton;
                    none = name;
                    flag1 = (((MessageBoxResult) nullable.GetValueOrDefault()) == none) ? (nullable != null) : false;
                    goto TR_0005;
                }
            }
            flag1 = defaultValueIsDefault;
        TR_0005:
            command2.IsDefault = flag1;
            UICommand local1 = command2;
            local1.IsCancel = isCancel;
            UICommand command = local1;
            switch (name)
            {
                case MessageBoxResult.OK:
                    command.Caption = DXMessageBoxLocalizer.GetString(DXMessageBoxStringId.Ok);
                    break;

                case MessageBoxResult.Cancel:
                    command.Caption = DXMessageBoxLocalizer.GetString(DXMessageBoxStringId.Cancel);
                    break;

                case MessageBoxResult.Yes:
                    command.Caption = DXMessageBoxLocalizer.GetString(DXMessageBoxStringId.Yes).Insert(0, "_");
                    break;

                case MessageBoxResult.No:
                    command.Caption = DXMessageBoxLocalizer.GetString(DXMessageBoxStringId.No).Insert(0, "_");
                    break;

                default:
                    break;
            }
            return command;
        }

        bool IWindowSurrogate.Activate() => 
            base.Activate();

        void IWindowSurrogate.Close()
        {
            base.Close();
        }

        void IWindowSurrogate.Hide()
        {
            base.Hide();
        }

        void IWindowSurrogate.Show()
        {
            base.Show();
        }

        bool? IWindowSurrogate.ShowDialog() => 
            this.ShowDialog();

        void IRibbonWindow.ApplyWindowKind()
        {
            this.RecalcWindowKind();
            this.ApplyWindowKind(this.ActualWindowKind);
        }

        UIElement IRibbonWindow.GetContentContainer() => 
            this.ContentPresenter;

        FrameworkElement IRibbonWindow.GetControlBoxContainer()
        {
            if (this.controlBoxContainer == null)
            {
                this.UpdateControlBoxContainer();
            }
            return this.controlBoxContainer;
        }

        Rect IRibbonWindow.GetControlBoxRect() => 
            ((ThemedWindowControlBoxBorder) ((IRibbonWindow) this).GetControlBoxContainer()).GetBounds();

        UIElement IRibbonWindow.GetWindowHeaderElement() => 
            base.GetTemplateChild(9.ToString()) as ThemedWindowHeader;

        void IRibbonWindow.HideWindowIcon()
        {
            if (this.ActualWindowKind != DevExpress.Xpf.Core.WindowKind.Normal)
            {
                this.forceShowIcon = false;
                base.CoerceValue(ShowIconProperty);
            }
        }

        void IRibbonWindow.ShowWindowIcon()
        {
            ContentControl child = null;
            if (this.ActualWindowKind == DevExpress.Xpf.Core.WindowKind.Ribbon)
            {
                Func<ContentControl, bool> predicate = <>c.<>9__341_1;
                if (<>c.<>9__341_1 == null)
                {
                    Func<ContentControl, bool> local2 = <>c.<>9__341_1;
                    predicate = <>c.<>9__341_1 = x => x.Name == "PART_ApplicationIconContainer";
                }
                child = TreeHelper.GetChild<ContentControl>(((IRibbonWindow) this).Ribbon as FrameworkElement, predicate);
                this.forceShowIcon = null;
                base.CoerceValue(ShowIconProperty);
                if (child != null)
                {
                    child.Visibility = Visibility.Hidden;
                }
            }
            else if (((IRibbonWindow) this).Ribbon != null)
            {
                Func<ContentControl, bool> predicate = <>c.<>9__341_0;
                if (<>c.<>9__341_0 == null)
                {
                    Func<ContentControl, bool> local1 = <>c.<>9__341_0;
                    predicate = <>c.<>9__341_0 = x => x.Name == "PART_ApplicationIconContainer";
                }
                child = TreeHelper.GetChild<ContentControl>(((IRibbonWindow) this).Ribbon as FrameworkElement, predicate);
                if (child != null)
                {
                    child.ClearValue(UIElement.VisibilityProperty);
                }
            }
        }

        internal static UICommand FindUICommand(IEnumerable collection, MessageBoxResult uiCommandResult) => 
            (collection != null) ? collection.OfType<UICommand>().FirstOrDefault<UICommand>(x => FindUICommandCriteria(x, uiCommandResult)) : null;

        private static bool FindUICommandCriteria(UICommand uiCommand, MessageBoxResult uiCommandResult) => 
            (uiCommand.Id != null) && (uiCommand.Id.Equals(uiCommandResult) && uiCommand.IsCancel);

        private DXTabItem GetFirstTabItem(DXTabControl dxTabControl)
        {
            DXTabItem local5;
            if (dxTabControl == null)
            {
                local5 = null;
            }
            else
            {
                Func<DXTabItem, bool> predicate = <>c.<>9__263_0;
                if (<>c.<>9__263_0 == null)
                {
                    Func<DXTabItem, bool> local1 = <>c.<>9__263_0;
                    predicate = <>c.<>9__263_0 = x => x.Visibility == Visibility.Visible;
                }
                local5 = dxTabControl.Items.OfType<DXTabItem>().FirstOrDefault<DXTabItem>(predicate);
            }
            DXTabItem item = local5;
            if (item == null)
            {
                object obj1;
                if (dxTabControl == null)
                {
                    obj1 = null;
                }
                else
                {
                    TabPanelContainer tabPanel = dxTabControl.TabPanel;
                    if (tabPanel == null)
                    {
                        TabPanelContainer local2 = tabPanel;
                        obj1 = null;
                    }
                    else
                    {
                        TabPanelBase panel = tabPanel.Panel;
                        if (panel != null)
                        {
                            obj1 = panel.Children.OfType<DXTabItem>().FirstOrDefault<DXTabItem>(<>c.<>9__263_1 ??= x => (x.Visibility == Visibility.Visible));
                        }
                        else
                        {
                            TabPanelBase local3 = panel;
                            obj1 = null;
                        }
                    }
                }
                item = (DXTabItem) obj1;
            }
            if ((item != null) && !item.Equals(this.tabItem))
            {
                if (this.tabItem != null)
                {
                    this.tabItem.Loaded -= new RoutedEventHandler(this.OnTabItemLoaded);
                }
                this.tabItem = item;
                if (this.tabItem != null)
                {
                    this.tabItem.Loaded += new RoutedEventHandler(this.OnTabItemLoaded);
                }
            }
            return this.tabItem;
        }

        private IEnumerable<UICommand> GetInternalDialogButtons(IEnumerable<UICommand> buttonsCollection)
        {
            IEnumerable<UICommand> dialogButtonsSource = (IEnumerable<UICommand>) this.DialogButtonsSource;
            return (((dialogButtonsSource == null) || (buttonsCollection != null)) ? ((dialogButtonsSource != null) ? buttonsCollection.Concat<UICommand>(dialogButtonsSource) : buttonsCollection) : dialogButtonsSource);
        }

        private Rect GetTabBorderBounds()
        {
            Func<Border, bool> predicate = <>c.<>9__266_0;
            if (<>c.<>9__266_0 == null)
            {
                Func<Border, bool> local1 = <>c.<>9__266_0;
                predicate = <>c.<>9__266_0 = x => x.Name == 3.ToString();
            }
            Border child = TreeHelper.GetChild<Border>(this, predicate);
            if (child != null)
            {
                return child.GetBounds();
            }
            return new Rect();
        }

        private DXTabControl GetTabControl()
        {
            DXTabControl child = TreeHelper.GetChild<DXTabControl>(this, null);
            if ((child != null) && !child.Equals(this.tabControl))
            {
                if (this.tabControl != null)
                {
                    this.tabControl.ItemsChanged -= new NotifyCollectionChangedEventHandler(this.OnTabControlItemsChanged);
                    this.tabControl.TabRemoved -= new TabControlTabRemovedEventHandler(this.OnTabControlTabRemoved);
                    this.tabControl.TabHidden -= new TabControlTabHiddenEventHandler(this.OnTabControlTabHidden);
                    this.tabControl.TabAdded -= new TabControlTabAddedEventHandler(this.OnTabControlTabAdded);
                }
                this.tabControl = child;
                if (this.GetFirstTabItem(this.tabControl) == null)
                {
                    this.tabControl.ItemsChanged += new NotifyCollectionChangedEventHandler(this.OnTabControlItemsChanged);
                }
                this.tabControl.TabRemoved += new TabControlTabRemovedEventHandler(this.OnTabControlTabRemoved);
                this.tabControl.TabHidden += new TabControlTabHiddenEventHandler(this.OnTabControlTabHidden);
                this.tabControl.TabAdded += new TabControlTabAddedEventHandler(this.OnTabControlTabAdded);
            }
            return this.tabControl;
        }

        private TabOrientablePanel GetTabHeaderPanel()
        {
            TabOrientablePanel panel = (this.TabControl == null) ? null : TreeHelper.GetChild<TabOrientablePanel>(this.TabControl, x => (x.Name == 2.ToString()) && x.TemplatedParent.Equals(this.TabControl));
            if ((panel != null) && !panel.Equals(this.tabHeaderPanel))
            {
                this.tabHeaderPanel = panel;
            }
            return this.tabHeaderPanel;
        }

        private FrameworkElement GetWindowContent() => 
            (from x in BarNameScope.GetService<IThemedWindowService>(this).GetElements()
                where Equals(GetWindow(x), this)
                select x).Where<FrameworkElement>(new Func<FrameworkElement, bool>(this.IsCandidateInLeftTopCorner)).FirstOrDefault<FrameworkElement>(new Func<FrameworkElement, bool>(this.IsCandidateWidthLessThanPresenter));

        private void GetWindowElements()
        {
            this.ContentPresenter = base.GetTemplateChild(0.ToString()) as System.Windows.Controls.ContentPresenter;
            this.AcrylicBackground = base.GetTemplateChild(10.ToString()) as Rectangle;
            if (base.SizeToContent == SizeToContent.WidthAndHeight)
            {
                this.WindowRoot = base.GetTemplateChild(1.ToString()) as FrameworkElement;
            }
        }

        private static void InvokeShowDialog(Func<bool?> showDialogMethod, Action showMethod)
        {
            if (showDialogMethod != null)
            {
                showDialogMethod();
            }
            else if (showMethod != null)
            {
                showMethod();
            }
        }

        private bool IsCandidateInLeftTopCorner(FrameworkElement x)
        {
            Point point2 = new Point();
            Point point = x.TranslatePoint(point2, this.ContentPresenter);
            return ((point.X <= 1.0) && (point.Y <= 1.0));
        }

        private bool IsCandidateWidthLessThanPresenter(FrameworkElement x) => 
            Math.Abs((double) (this.ContentPresenter.ActualWidth - x.ActualWidth)) >= 0.0;

        private bool IsDialogResultsDefaulted() => 
            (this.DialogButtonCommandResult == null) && (this.DialogButtonResult == MessageBoxResult.None);

        protected override Size MeasureOverride(Size availableSize)
        {
            Size result = base.MeasureOverride(availableSize);
            return (((this.WindowRoot == null) || (base.SizeToContent != SizeToContent.WidthAndHeight)) ? result : this.CoerceIfMeasureSpecified(result));
        }

        private void OnAcrylicBackgroundChanged()
        {
            if (this.AcrylicEffect != null)
            {
                this.AcrylicEffect.AcrylicBackground = this.AcrylicBackground;
            }
        }

        private void OnAcrylicColorChanged()
        {
            if (this.AcrylicEffect != null)
            {
                this.AcrylicEffect.Color = this.AcrylicColor;
            }
        }

        private static void OnAcrylicColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ThemedWindow) d).OnAcrylicColorChanged();
        }

        private void OnAcrylicOpacityChanged(double newValue)
        {
            if (this.AcrylicEffect != null)
            {
                this.AcrylicEffect.Opacity = this.AcrylicOpacity;
            }
        }

        private static void OnAcrylicOpacityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ThemedWindow) d).OnAcrylicOpacityChanged((double) e.NewValue);
        }

        private void OnActiveGlowColorChanged(SolidColorBrush newBrush, SolidColorBrush oldBrush)
        {
            WindowGlowChrome.RiseRepaint(this);
        }

        private static void OnActiveGlowColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ThemedWindow) d).OnActiveGlowColorChanged((SolidColorBrush) e.NewValue, (SolidColorBrush) e.OldValue);
        }

        private void OnActualDialogButtonsChanged(IEnumerable newValue)
        {
            if (newValue != null)
            {
                this.SetValue(ShowDialogFooterPropertyKey, newValue.IsNullOrEmpty() ? ((object) 0) : ((object) !ThemedWindowOptions.GetUseCustomDialogFooter(this)));
            }
        }

        private static void OnActualDialogButtonsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ThemedWindow) d).OnActualDialogButtonsChanged((IEnumerable) e.NewValue);
        }

        private void OnActualWindowKindChanged(DevExpress.Xpf.Core.WindowKind newValue, DevExpress.Xpf.Core.WindowKind oldValue)
        {
            this.ResetOldWindowKind(newValue, oldValue);
            this.ApplyWindowKind(newValue);
            this.SetRibbonTitleVisibility(newValue, this.ShowTitle);
        }

        private static void OnActualWindowKindPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ThemedWindow) d).OnActualWindowKindChanged((DevExpress.Xpf.Core.WindowKind) e.NewValue, (DevExpress.Xpf.Core.WindowKind) e.OldValue);
        }

        public override void OnApplyTemplate()
        {
            base.SetValue(IsDialogPropertyKey, get_ShowingAsDialog(this));
            if (!ThemedWindowOptions.GetUseCustomDialogFooter(this))
            {
                this.UpdateActualDialogButtons();
            }
            base.OnApplyTemplate();
            this.GetWindowElements();
        }

        private void OnCaptionHeightChanged(double eOldValue, double eNewValue)
        {
            this.captionHeight = WindowChrome.GetCaptionHeight(this);
        }

        private static void OnCaptionHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ThemedWindow) d).OnCaptionHeightChanged((double) e.NewValue, (double) e.OldValue);
        }

        protected override void OnClosed(EventArgs e)
        {
            this.UnsubscribeEvents();
            base.OnClosed(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (this.IsDialog)
            {
                this.OnDialogClosing(e);
            }
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new ThemedWindowAutomationPeer(this);

        private void OnDialogClosing(CancelEventArgs e)
        {
            if (e.Cancel)
            {
                this.ResetDialogResults();
            }
            else if (!WindowButtonHelper.GetIsYesNoDialog(this))
            {
                if ((this.ActualDialogButtons != null) && this.IsDialogResultsDefaulted())
                {
                    this.SetDialogResultIfClosedByCloseButton();
                }
                Window owner = base.Owner;
                if (owner == null)
                {
                    Window local1 = owner;
                }
                else
                {
                    owner.Focus();
                }
            }
        }

        private void OnEnableAcrylicChanged()
        {
            base.CoerceValue(ActualWindowKindProperty);
        }

        private static void OnEnableAcrylicPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ThemedWindow) d).OnEnableAcrylicChanged();
        }

        private static void OnHeaderItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void OnHeaderItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private void OnInactiveGlowColorChanged(SolidColorBrush newBrush, SolidColorBrush oldBrush)
        {
            WindowGlowChrome.RiseRepaint(this);
        }

        private static void OnInactiveGlowColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ThemedWindow) d).OnInactiveGlowColorChanged((SolidColorBrush) e.NewValue, (SolidColorBrush) e.OldValue);
        }

        private void OnIsDialogChanged(bool newValue)
        {
        }

        private static void OnIsDialogPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ThemedWindow) d).OnIsDialogChanged((bool) e.NewValue);
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            this.SetTabBackgroundHeight(((IRibbonWindow) this).Ribbon);
        }

        private void OnRibbonControlChanged(IRibbonControl oldValue, IRibbonControl newValue)
        {
            if (oldValue != null)
            {
                base.ClearValue(HeaderItemControl.ExtendedCloseTemplateProviderProperty);
                base.ClearValue(HeaderItemControl.ExtendedCommonTemplateProviderProperty);
            }
            if (newValue is DependencyObject)
            {
                Binding binding = new Binding();
                binding.Source = newValue;
                binding.Path = new PropertyPath(HeaderItemControl.ExtendedCloseTemplateProviderProperty);
                base.SetBinding(HeaderItemControl.ExtendedCloseTemplateProviderProperty, binding);
                Binding binding2 = new Binding();
                binding2.Source = newValue;
                binding2.Path = new PropertyPath(HeaderItemControl.ExtendedCommonTemplateProviderProperty);
                base.SetBinding(HeaderItemControl.ExtendedCommonTemplateProviderProperty, binding2);
            }
        }

        private void OnShowBackButtonChanged(bool newValue)
        {
            if (this.ActualWindowKind == DevExpress.Xpf.Core.WindowKind.Normal)
            {
                bool? nullable1;
                if (newValue)
                {
                    nullable1 = false;
                }
                else
                {
                    nullable1 = null;
                }
                this.forceShowIcon = nullable1;
                base.CoerceValue(ShowIconProperty);
            }
        }

        private static void OnShowBackButtonPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ThemedWindow) d).OnShowBackButtonChanged((bool) e.NewValue);
        }

        private void OnShowGlowChanged(bool newValue)
        {
        }

        private static void OnShowGlowPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ThemedWindow) d).OnShowGlowChanged((bool) e.NewValue);
        }

        private void OnShowTitleChanged(bool newValue, bool oldValue)
        {
            this.SetRibbonTitleVisibility(this.ActualWindowKind, newValue);
        }

        private static void OnShowTitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ThemedWindow) d).OnShowTitleChanged((bool) e.NewValue, (bool) e.OldValue);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            if (this.EnableAcrylic)
            {
                this.AcrylicEffect = new WindowAcrylicBackgroundEffect(this);
            }
        }

        private void OnTabControlItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.TabControl.HasItems)
            {
                this.tabItem ??= this.GetFirstTabItem(this.TabControl);
            }
        }

        private void OnTabControlTabAdded(object sender, TabControlTabAddedEventArgs e)
        {
            if ((this.TabControl.VisibleItemsCount == 1) && (e.TabIndex == 0))
            {
                this.ApplyTabbedWindowKind();
            }
        }

        private void OnTabControlTabHidden(object sender, TabControlTabHiddenEventArgs e)
        {
            if (this.TabControl.VisibleItemsCount == 0)
            {
                this.ResetTabControl();
            }
        }

        private void OnTabControlTabRemoved(object sender, TabControlTabRemovedEventArgs e)
        {
            if (this.TabControl.VisibleItemsCount == 0)
            {
                this.ResetTabControl();
            }
        }

        private void OnTabItemLoaded(object sender, RoutedEventArgs e)
        {
            this.ApplyWindowKind(this.ActualWindowKind);
        }

        private void OnTitleAlignmentChanged(WindowTitleAlignment newTitleAlignment, WindowTitleAlignment oldTitleAlignment)
        {
            IRibbonControl ribbon = ((IRibbonWindow) this).Ribbon;
            if (ribbon != null)
            {
                ribbon.UpdateTitleAlignment();
            }
        }

        private static void OnTitleAlignmentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ThemedWindow) d).OnTitleAlignmentChanged((WindowTitleAlignment) e.NewValue, (WindowTitleAlignment) e.OldValue);
        }

        private static void OnToolbarItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void OnToolbarItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private void OnUseGlowColorsChanged()
        {
        }

        private static void OnUseGlowColorsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ThemedWindow) d).OnUseGlowColorsChanged();
        }

        private void OnWindowKindChanged(DevExpress.Xpf.Core.WindowKind newValue, DevExpress.Xpf.Core.WindowKind oldValue)
        {
            this.RecalcWindowKind();
        }

        private static void OnWindowKindPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ThemedWindow) d).OnWindowKindChanged((DevExpress.Xpf.Core.WindowKind) e.NewValue, (DevExpress.Xpf.Core.WindowKind) e.OldValue);
        }

        private void OnWindowStyleChanged(WindowStyle newWindowStyle)
        {
            base.CoerceValue(Control.PaddingProperty);
        }

        private static void OnWindowStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ThemedWindow) d).OnWindowStyleChanged((WindowStyle) e.NewValue);
        }

        protected internal virtual void RecalcWindowKind()
        {
            FrameworkElement windowContent;
            ThemedWindowsHelper.SetIsRibbonControlAsContent(this, false);
            if (this.WindowKind != DevExpress.Xpf.Core.WindowKind.Auto)
            {
                if (this.WindowKind == DevExpress.Xpf.Core.WindowKind.Ribbon)
                {
                    windowContent = this.GetWindowContent();
                    if (windowContent == null)
                    {
                        this.ClearContentPresenterMargin();
                    }
                    if (windowContent is IRibbonControl)
                    {
                        ThemedWindowsHelper.SetIsRibbonControlAsContent(this, true);
                    }
                }
                base.SetValue(ActualWindowKindPropertyKey, this.WindowKind);
            }
            else if (this.ContentPresenter == null)
            {
                base.SetValue(ActualWindowKindPropertyKey, DevExpress.Xpf.Core.WindowKind.Normal);
            }
            else
            {
                if (!base.IsMeasureValid || !base.IsArrangeValid)
                {
                    this.recalcWindowKindAfterArrange = true;
                }
                windowContent = this.GetWindowContent();
                DevExpress.Xpf.Core.WindowKind normal = DevExpress.Xpf.Core.WindowKind.Normal;
                if (windowContent is DXTabControl)
                {
                    normal = DevExpress.Xpf.Core.WindowKind.Tabbed;
                }
                if (windowContent is IRibbonControl)
                {
                    normal = DevExpress.Xpf.Core.WindowKind.Ribbon;
                    ThemedWindowsHelper.SetIsRibbonControlAsContent(this, true);
                }
                base.SetValue(ActualWindowKindPropertyKey, normal);
            }
        }

        private void ResetDialogResults()
        {
            this.DialogButtonResult = MessageBoxResult.None;
            this.DialogButtonCommandResult = null;
        }

        internal void ResetOldWindowKind(DevExpress.Xpf.Core.WindowKind newWindowKind, DevExpress.Xpf.Core.WindowKind oldWindowKind)
        {
            switch (oldWindowKind)
            {
                case DevExpress.Xpf.Core.WindowKind.Normal:
                    break;

                case DevExpress.Xpf.Core.WindowKind.Tabbed:
                    this.ResetTabControl();
                    return;

                case DevExpress.Xpf.Core.WindowKind.Ribbon:
                    this.ResetRibbonControl();
                    break;

                default:
                    return;
            }
        }

        private void ResetRibbonControl()
        {
            base.ClearValue(HeaderItemControl.UseExtendedTemplateProvidersProperty);
            IRibbonControl ribbon = ((IRibbonWindow) this).Ribbon;
            if ((ribbon == null) && (this.ContentPresenter != null))
            {
                this.ContentPresenter.Margin = new Thickness(0.0);
            }
            else
            {
                this.ResetRibbonControlMargin(ribbon);
            }
        }

        private void ResetRibbonControlMargin(IRibbonControl ribbon)
        {
            if ((ribbon?.TabBackground != null) && (this.ContentPresenter != null))
            {
                this.ContentPresenter.Margin = new Thickness(0.0);
                ribbon.TabBackground.VerticalAlignment = VerticalAlignment.Stretch;
                ribbon.TabBackground.ClearValue(FrameworkElement.HeightProperty);
                ribbon.HeaderBorder.ClearValue(UIElement.VisibilityProperty);
                if (ribbon.BackgroundBorder != null)
                {
                    ribbon.BackgroundBorder.Visibility = Visibility.Visible;
                }
                ribbon.UpdateIsInRibbonWindow();
                ((IRibbonWindow) this).IsCaptionVisible = true;
            }
        }

        private void ResetTabControl()
        {
            if (this.TabControl != null)
            {
                this.ResetTabControlMargin();
                if (this.TabHeaderPanel != null)
                {
                    this.ResetTabPanelMargin();
                }
            }
        }

        private void ResetTabControlMargin()
        {
            this.TabControl.Margin = new Thickness(0.0);
        }

        private void ResetTabPanelMargin()
        {
            this.TabHeaderPanel.Indent = new Thickness(0.0);
        }

        public void RestoreLayoutFromStream(Stream stream)
        {
            DXSerializer.DeserializeSingleObject(this, stream, base.GetType().Name);
        }

        public void RestoreLayoutFromXml(string path)
        {
            DXSerializer.DeserializeSingleObject(this, path, base.GetType().Name);
        }

        public void SaveLayoutToStream(Stream stream)
        {
            DXSerializer.SerializeSingleObject(this, stream, base.GetType().Name);
        }

        public void SaveLayoutToXml(string path)
        {
            DXSerializer.SerializeSingleObject(this, path, base.GetType().Name);
        }

        private void SetDialogResultIfClosedByCloseButton()
        {
            UICommand command = FindUICommand(this.ActualDialogButtons, MessageBoxResult.Cancel);
            command ??= FindUICommand(this.ActualDialogButtons, MessageBoxResult.No);
            command ??= FindUICommand(this.ActualDialogButtons, MessageBoxResult.OK);
            if (command != null)
            {
                this.DialogButtonResult = (MessageBoxResult) command.Id;
            }
        }

        private void SetRibbonControlMargin()
        {
            IRibbonControl ribbon = ((IRibbonWindow) this).Ribbon;
            if ((ribbon != null) && (ribbon.RibbonHeaderVisibility == RibbonHeaderVisibility.Collapsed))
            {
                this.ResetRibbonControlMargin(ribbon);
            }
            else
            {
                ThemedWindowContentBorder templateChild = base.GetTemplateChild(4.ToString()) as ThemedWindowContentBorder;
                if ((this.ContentPresenter != null) && (templateChild != null))
                {
                    this.ContentPresenter.Margin = new Thickness(0.0, -(this.captionHeight + templateChild.BorderThickness.Top), 0.0, 0.0);
                }
                if (ribbon?.TabBackground != null)
                {
                    this.SetupRibbonForMergingInWindowHeader(ribbon);
                }
            }
        }

        private void SetRibbonTitleVisibility(DevExpress.Xpf.Core.WindowKind windowKind, bool showTitle)
        {
            if ((windowKind == DevExpress.Xpf.Core.WindowKind.Ribbon) && (((IRibbonWindow) this).Ribbon != null))
            {
                Func<Panel, bool> predicate = <>c.<>9__299_0;
                if (<>c.<>9__299_0 == null)
                {
                    Func<Panel, bool> local1 = <>c.<>9__299_0;
                    predicate = <>c.<>9__299_0 = x => x.Name == 5.ToString();
                }
                Panel child = TreeHelper.GetChild<Panel>(this, predicate);
                if (child != null)
                {
                    child.Visibility = !showTitle ? Visibility.Collapsed : Visibility.Visible;
                }
            }
        }

        private void SetTabBackgroundHeight(IRibbonControl ribbon)
        {
            if ((this.ActualWindowKind == DevExpress.Xpf.Core.WindowKind.Ribbon) && (ribbon?.TabBackground != null))
            {
                ribbon.TabBackground.Height = Math.Max((double) 0.0, (double) (ribbon.CategoriesHeight - this.captionHeight));
            }
        }

        private void SetTabControlMargin()
        {
            if ((this.TabControl.View.HeaderLocation == HeaderLocation.Top) && this.TabControl.HasItems)
            {
                double actualHeight;
                DXTabItem tabItem = this.TabItem;
                if (tabItem != null)
                {
                    actualHeight = tabItem.ActualHeight;
                }
                else
                {
                    DXTabItem local1 = tabItem;
                    actualHeight = this.captionHeight / 3.0;
                }
                double num = actualHeight;
                this.TabControl.Margin = new Thickness(0.0, -num, 0.0, 0.0);
            }
        }

        private void SetTabPanelMargin()
        {
            Rect tabBorderBounds = this.GetTabBorderBounds();
            this.TabHeaderPanel.Indent = new Thickness(tabBorderBounds.Left, 0.0, (base.Padding.Left + this.TabControl.ActualWidth) - tabBorderBounds.Right, 0.0);
        }

        private void SetupRibbonForMergingInWindowHeader(IRibbonControl ribbon)
        {
            if (ribbon != null)
            {
                ribbon.TabBackground.VerticalAlignment = VerticalAlignment.Bottom;
                this.SetTabBackgroundHeight(ribbon);
                ribbon.HeaderBorder.Visibility = Visibility.Collapsed;
                if (ribbon.BackgroundBorder != null)
                {
                    ribbon.BackgroundBorder.Visibility = Visibility.Collapsed;
                }
                ribbon.UpdateIsInRibbonWindow();
            }
        }

        public bool? ShowDialog()
        {
            base.ShowDialog();
            return base.DialogResult;
        }

        public UICommand ShowDialog(IEnumerable<UICommand> dialogButtons = null)
        {
            this.ShowDialogCore(dialogButtons, new Func<bool?>(this.ShowDialog), null);
            return this.DialogButtonCommandResult;
        }

        public MessageBoxResult ShowDialog(MessageBoxButton dialogButtons, MessageBoxResult? defaultButton = new MessageBoxResult?())
        {
            this.ShowDialogCore(dialogButtons, defaultButton, new Func<bool?>(this.ShowDialog), null);
            return this.DialogButtonResult;
        }

        private void ShowDialogCore(IEnumerable<UICommand> dialogButtons, Func<bool?> showDialogMethod, Action showMethod = null)
        {
            List<UICommand> uiCommands = dialogButtons.ToList<UICommand>();
            this.CheckIfNeedToUseMvvmMessageResult(uiCommands);
            this.internalDialogButtons = ThemedWindowOptions.GetUseCustomDialogFooter(this) ? null : CreateDialogButtons(uiCommands);
            InvokeShowDialog(showDialogMethod, showMethod);
        }

        private void ShowDialogCore(MessageBoxButton dialogButtons, MessageBoxResult? defaultButton, Func<bool?> showDialogMethod, Action showMethod = null)
        {
            object obj1;
            if (ThemedWindowOptions.GetUseCustomDialogFooter(this))
            {
                obj1 = null;
            }
            else
            {
                MessageBoxResult? nullable = null;
                obj1 = CreateDialogButtons(new MessageBoxButton?(dialogButtons), nullable);
            }
            this.internalDialogButtons = (IEnumerable<UICommand>) obj1;
            if (dialogButtons == MessageBoxButton.YesNo)
            {
                WindowButtonHelper.SetIsYesNoDialog(this, true);
            }
            InvokeShowDialog(showDialogMethod, showMethod);
        }

        private void SubscribeEvents()
        {
            base.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
        }

        private void UnsubscribeEvents()
        {
            base.LayoutUpdated -= new EventHandler(this.OnLayoutUpdated);
        }

        internal void UpdateActualDialogButtons()
        {
            base.ClearValue(ActualDialogButtonsPropertyKey);
            base.SetValue(ActualDialogButtonsPropertyKey, this.CreateDialogButtons());
        }

        protected internal virtual void UpdateControlBoxContainer()
        {
            Func<ThemedWindowControlBoxBorder, bool> predicate = <>c.<>9__334_0;
            if (<>c.<>9__334_0 == null)
            {
                Func<ThemedWindowControlBoxBorder, bool> local1 = <>c.<>9__334_0;
                predicate = <>c.<>9__334_0 = x => x.Name == 6.ToString();
            }
            this.controlBoxContainer = TreeHelper.GetChild<ThemedWindowControlBoxBorder>(this, predicate);
        }

        public ICommand NavigateBackCommand
        {
            get => 
                (ICommand) base.GetValue(NavigateBackCommandProperty);
            set => 
                base.SetValue(NavigateBackCommandProperty, value);
        }

        public bool ShowStatusPanel
        {
            get => 
                (bool) base.GetValue(ShowStatusPanelProperty);
            set => 
                base.SetValue(ShowStatusPanelProperty, value);
        }

        public bool ShowIcon
        {
            get => 
                (bool) base.GetValue(ShowIconProperty);
            set => 
                base.SetValue(ShowIconProperty, value);
        }

        public bool ShowTitle
        {
            get => 
                (bool) base.GetValue(ShowTitleProperty);
            set => 
                base.SetValue(ShowTitleProperty, value);
        }

        public bool UseGlowColors
        {
            get => 
                (bool) base.GetValue(UseGlowColorsProperty);
            set => 
                base.SetValue(UseGlowColorsProperty, value);
        }

        public bool ShowGlow
        {
            get => 
                (bool) base.GetValue(ShowGlowProperty);
            set => 
                base.SetValue(ShowGlowProperty, value);
        }

        public double AcrylicOpacity
        {
            get => 
                (double) base.GetValue(AcrylicOpacityProperty);
            set => 
                base.SetValue(AcrylicOpacityProperty, value);
        }

        public Color AcrylicColor
        {
            get => 
                (Color) base.GetValue(AcrylicColorProperty);
            set => 
                base.SetValue(AcrylicColorProperty, value);
        }

        public bool EnableAcrylic
        {
            get => 
                (bool) base.GetValue(EnableAcrylicProperty);
            set => 
                base.SetValue(EnableAcrylicProperty, value);
        }

        public bool ShowBackButton
        {
            get => 
                (bool) base.GetValue(ShowBackButtonProperty);
            set => 
                base.SetValue(ShowBackButtonProperty, value);
        }

        public WindowTitleAlignment TitleAlignment
        {
            get => 
                (WindowTitleAlignment) base.GetValue(TitleAlignmentProperty);
            set => 
                base.SetValue(TitleAlignmentProperty, value);
        }

        public DevExpress.Xpf.Core.WindowKind WindowKind
        {
            get => 
                (DevExpress.Xpf.Core.WindowKind) base.GetValue(WindowKindProperty);
            set => 
                base.SetValue(WindowKindProperty, value);
        }

        public DevExpress.Xpf.Core.WindowKind ActualWindowKind =>
            (DevExpress.Xpf.Core.WindowKind) base.GetValue(ActualWindowKindProperty);

        public bool IsDialog =>
            (bool) base.GetValue(IsDialogProperty);

        public bool ShowDialogFooter =>
            (bool) base.GetValue(ShowDialogFooterProperty);

        public ControlBoxButtons ControlBoxButtonSet
        {
            get => 
                (ControlBoxButtons) base.GetValue(ControlBoxButtonSetProperty);
            set => 
                base.SetValue(ControlBoxButtonSetProperty, value);
        }

        public Thickness ResizeBorderThickness
        {
            get => 
                (Thickness) base.GetValue(ResizeBorderThicknessProperty);
            set => 
                base.SetValue(ResizeBorderThicknessProperty, value);
        }

        public ObservableCollection<object> HeaderItems =>
            (ObservableCollection<object>) base.GetValue(HeaderItemsProperty);

        public ObservableCollection<object> ToolbarItems =>
            (ObservableCollection<object>) base.GetValue(ToolbarItemsProperty);

        public ObservableCollection<object> DialogButtons =>
            (ObservableCollection<object>) base.GetValue(DialogButtonsProperty);

        public IEnumerable ActualDialogButtons =>
            (IEnumerable) base.GetValue(ActualDialogButtonsProperty);

        public IEnumerable DialogButtonsSource
        {
            get => 
                (IEnumerable) base.GetValue(DialogButtonsSourceProperty);
            set => 
                base.SetValue(DialogButtonsSourceProperty, value);
        }

        public IEnumerable HeaderItemsSource
        {
            get => 
                (IEnumerable) base.GetValue(HeaderItemsSourceProperty);
            set => 
                base.SetValue(HeaderItemsSourceProperty, value);
        }

        public IEnumerable ToolbarItemsSource
        {
            get => 
                (IEnumerable) base.GetValue(ToolbarItemsSourceProperty);
            set => 
                base.SetValue(ToolbarItemsSourceProperty, value);
        }

        public Style HeaderItemContainerStyle
        {
            get => 
                (Style) base.GetValue(HeaderItemContainerStyleProperty);
            set => 
                base.SetValue(HeaderItemContainerStyleProperty, value);
        }

        public Style ToolbarItemContainerStyle
        {
            get => 
                (Style) base.GetValue(ToolbarItemContainerStyleProperty);
            set => 
                base.SetValue(ToolbarItemContainerStyleProperty, value);
        }

        public StyleSelector HeaderItemContainerStyleSelector
        {
            get => 
                (StyleSelector) base.GetValue(HeaderItemContainerStyleSelectorProperty);
            set => 
                base.SetValue(HeaderItemContainerStyleSelectorProperty, value);
        }

        public StyleSelector ToolbarItemContainerStyleSelector
        {
            get => 
                (StyleSelector) base.GetValue(ToolbarItemContainerStyleSelectorProperty);
            set => 
                base.SetValue(ToolbarItemContainerStyleSelectorProperty, value);
        }

        public DataTemplate HeaderItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(HeaderItemTemplateProperty);
            set => 
                base.SetValue(HeaderItemTemplateProperty, value);
        }

        public DataTemplate ToolbarItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ToolbarItemTemplateProperty);
            set => 
                base.SetValue(ToolbarItemTemplateProperty, value);
        }

        public DataTemplateSelector HeaderItemTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(HeaderItemTemplateSelectorProperty);
            set => 
                base.SetValue(HeaderItemTemplateSelectorProperty, value);
        }

        public DataTemplateSelector ToolbarItemTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ToolbarItemTemplateSelectorProperty);
            set => 
                base.SetValue(ToolbarItemTemplateSelectorProperty, value);
        }

        public SolidColorBrush ActiveGlowColor
        {
            get => 
                (SolidColorBrush) base.GetValue(ActiveGlowColorProperty);
            set => 
                base.SetValue(ActiveGlowColorProperty, value);
        }

        public SolidColorBrush InactiveGlowColor
        {
            get => 
                (SolidColorBrush) base.GetValue(InactiveGlowColorProperty);
            set => 
                base.SetValue(InactiveGlowColorProperty, value);
        }

        public Brush HeaderBackground
        {
            get => 
                (Brush) base.GetValue(HeaderBackgroundProperty);
            set => 
                base.SetValue(HeaderBackgroundProperty, value);
        }

        public Brush HeaderForeground
        {
            get => 
                (Brush) base.GetValue(HeaderForegroundProperty);
            set => 
                base.SetValue(HeaderForegroundProperty, value);
        }

        private bool ShowSearchBox
        {
            get => 
                (bool) base.GetValue(ShowSearchBoxProperty);
            set => 
                base.SetValue(ShowSearchBoxProperty, value);
        }

        public MessageBoxResult DialogButtonResult { get; internal set; }

        public UICommand DialogButtonCommandResult { get; internal set; }

        Window IWindowSurrogate.RealWindow =>
            this;

        protected internal System.Windows.Controls.ContentPresenter ContentPresenter { get; private set; }

        public FrameworkElement WindowRoot { get; private set; }

        internal DXTabControl TabControl =>
            this.GetTabControl();

        internal TabOrientablePanel TabHeaderPanel =>
            this.GetTabHeaderPanel();

        private DXTabItem TabItem =>
            this.GetFirstTabItem(this.TabControl);

        protected internal Rectangle AcrylicBackground
        {
            get => 
                this.acrylicBackground;
            set => 
                SetPropertyHelper.Set<Rectangle>(ref this.acrylicBackground, value, new Action(this.OnAcrylicBackgroundChanged));
        }

        private WindowAcrylicBackgroundEffect AcrylicEffect { get; set; }

        private bool IsInRibbonCaptionVisible { get; set; }

        IRibbonControl IRibbonWindow.Ribbon
        {
            get => 
                this.ribbonControl;
            set
            {
                if (!ReferenceEquals(this.ribbonControl, value))
                {
                    IRibbonControl ribbonControl = this.ribbonControl;
                    this.ribbonControl = value;
                    this.OnRibbonControlChanged(ribbonControl, value);
                }
            }
        }

        private bool IsCaptionVisible { get; set; }

        bool IRibbonWindow.IsCaptionVisible
        {
            get => 
                this.IsCaptionVisible;
            set => 
                this.IsCaptionVisible = value;
        }

        bool IRibbonWindow.IsRibbonCaptionVisible
        {
            get => 
                this.IsInRibbonCaptionVisible;
            set => 
                this.IsInRibbonCaptionVisible = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public WindowSerializationInfo SerializationInfo { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThemedWindow.<>c <>9 = new ThemedWindow.<>c();
            public static Func<DXTabItem, bool> <>9__263_0;
            public static Func<DXTabItem, bool> <>9__263_1;
            public static Func<Border, bool> <>9__266_0;
            public static Func<Panel, bool> <>9__299_0;
            public static Func<ThemedWindowControlBoxBorder, bool> <>9__334_0;
            public static Func<ContentControl, bool> <>9__341_0;
            public static Func<ContentControl, bool> <>9__341_1;

            internal bool <DevExpress.Xpf.Ribbon.IRibbonWindow.ShowWindowIcon>b__341_0(ContentControl x) => 
                x.Name == "PART_ApplicationIconContainer";

            internal bool <DevExpress.Xpf.Ribbon.IRibbonWindow.ShowWindowIcon>b__341_1(ContentControl x) => 
                x.Name == "PART_ApplicationIconContainer";

            internal bool <GetFirstTabItem>b__263_0(DXTabItem x) => 
                x.Visibility == Visibility.Visible;

            internal bool <GetFirstTabItem>b__263_1(DXTabItem x) => 
                x.Visibility == Visibility.Visible;

            internal bool <GetTabBorderBounds>b__266_0(Border x) => 
                x.Name == 3.ToString();

            internal bool <SetRibbonTitleVisibility>b__299_0(Panel x) => 
                x.Name == 5.ToString();

            internal bool <UpdateControlBoxContainer>b__334_0(ThemedWindowControlBoxBorder x) => 
                x.Name == 6.ToString();
        }
    }
}

