namespace DevExpress.Xpf.Core
{
    using DevExpress.Data;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.Utils;
    using System;
    using System.ComponentModel;
    using System.Media;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;

    public class DXMessageBox : Control
    {
        internal static DXMessageBoxCreator creator = new DXMessageBoxCreator();
        private Size minSize = new Size(350.0, 100.0);
        protected MessageBoxResult? messageBoxResult;
        protected MessageBoxResult defaultResult;
        protected FloatingContainer fc;
        private MessageBoxButton _messageBoxButtons;
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(DXMessageBox), new UIPropertyMetadata(string.Empty));
        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(string), typeof(DXMessageBox), new UIPropertyMetadata(string.Empty));
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(System.Windows.Media.ImageSource), typeof(DXMessageBox), new UIPropertyMetadata(null));
        private FloatingContainer storedFC;
        private string[] buttonNames = new string[] { "PART_NoButton", "PART_CancelButton", "PART_YesButton", "PART_OkButton", "PART_NoButton1", "PART_CancelButton1", "PART_YesButton1", "PART_OkButton1" };

        static DXMessageBox()
        {
            KeyboardNavigation.DirectionalNavigationProperty.OverrideMetadata(typeof(DXMessageBox), new FrameworkPropertyMetadata(KeyboardNavigationMode.Cycle));
            KeyboardNavigation.TabNavigationProperty.OverrideMetadata(typeof(DXMessageBox), new FrameworkPropertyMetadata(KeyboardNavigationMode.Cycle));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DXMessageBox), new FrameworkPropertyMetadata(typeof(DXMessageBox)));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button source = e.Source as Button;
            this.DoButtonClick(source);
        }

        protected static FrameworkElement CalcOwner()
        {
            FrameworkElement element = null;
            if ((Application.Current != null) && Application.Current.Dispatcher.CheckAccess())
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.IsActive && !ReferenceEquals(window.Background, Brushes.Transparent))
                    {
                        element = window;
                        break;
                    }
                }
                if ((element == null) && (Application.Current.Windows.Count > 0))
                {
                    Window window2 = Application.Current.Windows[0];
                    if (!ReferenceEquals(window2.Background, Brushes.Transparent))
                    {
                        element = window2;
                    }
                }
            }
            return element;
        }

        private static void CloseDXMessageBoxWindow(Window w)
        {
            if (Application.Current != null)
            {
                ShutdownMode shutdownMode = Application.Current.ShutdownMode;
                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                w.Close();
                Application.Current.ShutdownMode = shutdownMode;
            }
        }

        protected static FrameworkElement CorrectOwner(FrameworkElement owner)
        {
            DXWindow treeRoot = owner as DXWindow;
            if (treeRoot != null)
            {
                Predicate<FrameworkElement> predicate = <>c.<>9__44_0;
                if (<>c.<>9__44_0 == null)
                {
                    Predicate<FrameworkElement> local1 = <>c.<>9__44_0;
                    predicate = <>c.<>9__44_0 = element => element is BackgroundPanel;
                }
                FrameworkElement element = LayoutHelper.FindElement(treeRoot, predicate);
                if (element != null)
                {
                    return element;
                }
            }
            return owner;
        }

        protected virtual FloatingContainer CreateFloatingContainer(FloatingMode floatingMode) => 
            FloatingContainerFactory.Create(floatingMode);

        protected void DialogClosed(bool? dialogResult)
        {
            if (this.messageBoxResult == null)
            {
                switch (this.messageBoxButtons)
                {
                    case MessageBoxButton.OK:
                        this.messageBoxResult = 1;
                        return;

                    case MessageBoxButton.OKCancel:
                    case MessageBoxButton.YesNoCancel:
                        this.messageBoxResult = 2;
                        break;

                    case ((MessageBoxButton) 2):
                        break;

                    default:
                        return;
                }
            }
        }

        protected void DialogWndProc()
        {
            while (true)
            {
                DispatcherOperation operation = base.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new DispatcherOperationCallback(this.DoIdle), this);
                while (true)
                {
                    Thread.Sleep(50);
                    DispatcherOperationStatus status = operation.Wait(TimeSpan.FromSeconds(1.0));
                    if (this.fc.IsClosed && (this.messageBoxResult == null))
                    {
                        this.messageBoxResult = 0;
                    }
                    FloatingWindowContainer fc = this.fc as FloatingWindowContainer;
                    if ((fc != null) && ((this.messageBoxResult == null) && ((fc.Window == null) || !fc.Window.IsVisible)))
                    {
                        this.messageBoxResult = 0;
                    }
                    if (((this.fc.Owner == null) || !this.fc.Owner.IsInVisualTree()) && (this.fc is FloatingAdornerContainer))
                    {
                        this.messageBoxResult = 0;
                    }
                    if ((status == DispatcherOperationStatus.Completed) || (this.messageBoxResult != null))
                    {
                        if (this.messageBoxResult == null)
                        {
                            break;
                        }
                        FloatingContainer.SetFloatingContainer(this.fc.LogicalOwner, this.storedFC);
                        return;
                    }
                }
            }
        }

        protected virtual void DoButtonClick(Button button)
        {
            string name = button.Name;
            uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
            if (num > 0xa46ee43f)
            {
                if (num > 0xc556d2ce)
                {
                    if (num == 0xe891020a)
                    {
                        if (name != "PART_CancelButton1")
                        {
                            goto TR_0000;
                        }
                    }
                    else
                    {
                        if ((num != 0xf175fc18) || (name != "PART_YesButton"))
                        {
                            goto TR_0000;
                        }
                        goto TR_0003;
                    }
                }
                else
                {
                    if (num == 0xa7185a8b)
                    {
                        if (name != "PART_OkButton")
                        {
                            goto TR_0000;
                        }
                    }
                    else if ((num != 0xc556d2ce) || (name != "PART_OkButton1"))
                    {
                        goto TR_0000;
                    }
                    this.messageBoxResult = 1;
                    goto TR_0000;
                }
            }
            else if (num > 0x47620d26)
            {
                if (num == 0x765a9b35)
                {
                    if (name != "PART_NoButton1")
                    {
                        goto TR_0000;
                    }
                    goto TR_0001;
                }
                else if ((num != 0xa46ee43f) || (name != "PART_CancelButton"))
                {
                    goto TR_0000;
                }
            }
            else
            {
                if (num == 0x45bbf48b)
                {
                    if (name != "PART_YesButton1")
                    {
                        goto TR_0000;
                    }
                }
                else
                {
                    if ((num != 0x47620d26) || (name != "PART_NoButton"))
                    {
                        goto TR_0000;
                    }
                    goto TR_0001;
                }
                goto TR_0003;
            }
            this.messageBoxResult = 2;
        TR_0000:
            this.ProcessClosing();
            return;
        TR_0001:
            this.messageBoxResult = 7;
            goto TR_0000;
        TR_0003:
            this.messageBoxResult = 6;
            goto TR_0000;
        }

        protected object DoIdle(object args) => 
            args;

        private void FindAndFocusDefaultButton()
        {
            Button tempButton = null;
            switch (this.defaultResult)
            {
                case MessageBoxResult.OK:
                    tempButton = this.FindMessageBoxButton("PART_OkButton");
                    break;

                case MessageBoxResult.Cancel:
                    tempButton = this.FindMessageBoxButton("PART_CancelButton");
                    break;

                case MessageBoxResult.Yes:
                    tempButton = this.FindMessageBoxButton("PART_YesButton");
                    break;

                case MessageBoxResult.No:
                    tempButton = this.FindMessageBoxButton("PART_NoButton");
                    break;

                default:
                    break;
            }
            if (tempButton != null)
            {
                foreach (string str in this.buttonNames)
                {
                    Button button2 = LayoutHelper.FindElementByName(this, str) as Button;
                    if (button2 != null)
                    {
                        button2.Click += new RoutedEventHandler(this.button_Click);
                    }
                }
                this.FocusButton(tempButton);
            }
            else
            {
                bool flag = false;
                foreach (string str2 in this.buttonNames)
                {
                    Button button3 = LayoutHelper.FindElementByName(this, str2) as Button;
                    if (button3 != null)
                    {
                        if ((((this.messageBoxButtons == MessageBoxButton.OK) && (button3.Name == "PART_OkButton")) || (((this.messageBoxButtons == MessageBoxButton.OKCancel) && (button3.Name == "PART_OkButton1")) || ((this.messageBoxButtons == MessageBoxButton.YesNo) && (button3.Name == "PART_YesButton")))) || ((this.messageBoxButtons == MessageBoxButton.YesNoCancel) && (button3.Name == "PART_YesButton1")))
                        {
                            if (!flag)
                            {
                                this.FocusButton(button3);
                            }
                            flag = true;
                        }
                        button3.Click += new RoutedEventHandler(this.button_Click);
                    }
                }
            }
        }

        protected Button FindMessageBoxButton(string name)
        {
            Button button = LayoutHelper.FindElementByName(this, name) as Button;
            if ((button == null) || (button.ActualWidth == 0.0))
            {
                button = LayoutHelper.FindElementByName(this, name + "1") as Button;
            }
            return button;
        }

        private void FocusButton(Button tempButton)
        {
            Keyboard.Focus(tempButton);
            tempButton.Focus();
        }

        private string GetTextForClipBoard()
        {
            string str = "---------------------------";
            string newLine = Environment.NewLine;
            string str3 = string.Empty;
            switch (this.messageBoxButtons)
            {
                case MessageBoxButton.OK:
                    str3 = "OK";
                    break;

                case MessageBoxButton.OKCancel:
                    str3 = "OK Cancel";
                    break;

                case MessageBoxButton.YesNoCancel:
                    str3 = "Yes No Cancel";
                    break;

                case MessageBoxButton.YesNo:
                    str3 = "Yes No";
                    break;

                default:
                    break;
            }
            return string.Format("{0}{1}{2}{1}{0}{1}{3}{1}{0}{1}{4}{1}{0}{1}", new object[] { str, newLine, this.Caption, this.Text, str3 });
        }

        private static void InitCaption(DXMessageBox dxmb, string caption)
        {
            dxmb.Caption = caption;
        }

        private FloatingContainer InitFloatingContainer(FrameworkElement owner, string messageBoxText, string caption, MessageBoxImage icon, MessageBoxButton button, FloatingMode desiredFloatingMode, bool showCloseButton, bool closeOnEscape, bool allowShowAnimation, MessageBoxOptions options)
        {
            InitImageSource(this, icon);
            InitCaption(this, caption);
            InitText(this, messageBoxText);
            this.fc = this.CreateFloatingContainer(desiredFloatingMode);
            this.fc.AllowShowAnimations = allowShowAnimation;
            this.fc.BeginUpdate();
            if (owner != null)
            {
                this.storedFC = FloatingContainer.GetFloatingContainer(owner);
                FloatingContainer.SetFloatingContainer(owner, this.fc);
            }
            ThemedWindowHeaderItemsControlBase.SetAllowHeaderItems(this.fc, false);
            this.fc.Owner = owner;
            this.fc.AllowSizing = false;
            this.fc.SizeToContent = SizeToContent.WidthAndHeight;
            this.messageBoxButtons = button;
            if (owner != null)
            {
                this.fc.SetValue(ThemeManager.ThemeNameProperty, owner.GetValue(ThemeManager.ThemeNameProperty));
                this.fc.SetValue(FrameworkElement.FlowDirectionProperty, owner.GetValue(FrameworkElement.FlowDirectionProperty));
                this.fc.SetValue(FrameworkElement.TagProperty, this);
            }
            if ((options & MessageBoxOptions.RtlReading) == MessageBoxOptions.RtlReading)
            {
                this.fc.SetValue(FrameworkElement.FlowDirectionProperty, FlowDirection.RightToLeft);
            }
            FloatingContainer.InitDialog(this, owner, new DialogClosedDelegate(this.DialogClosed), this.minSize, caption, false, this.fc, closeOnEscape);
            this.fc.ShowCloseButton = showCloseButton;
            this.fc.DeactivateOnClose = false;
            this.fc.EndUpdate();
            this.fc.FloatLocation = new Point(double.NaN, double.NaN);
            this.InitFloatingContainerCore(owner, messageBoxText, caption, icon, button, desiredFloatingMode, showCloseButton, closeOnEscape, allowShowAnimation);
            if (this.fc is FloatingWindowContainer)
            {
                FloatingWindowContainer fc = this.fc as FloatingWindowContainer;
            }
            this.fc.IsOpen = true;
            return this.fc;
        }

        protected virtual void InitFloatingContainerCore(FrameworkElement owner, string messageBoxText, string caption, MessageBoxImage icon, MessageBoxButton button, FloatingMode desiredFloatingMode, bool showCloseButton, bool closeOnEscape, bool allowShowAnimation)
        {
        }

        protected static void InitImageSource(DXMessageBox dxmb, MessageBoxImage icon)
        {
            if (!ApplicationThemeHelper.UseDefaultSvgImages)
            {
                InitPngImageSource(dxmb, icon);
            }
            else
            {
                InitSvgImageSource(dxmb, icon);
            }
        }

        protected static void InitPngImageSource(DXMessageBox dxmb, MessageBoxImage icon)
        {
            string str = "pack://application:,,,/DevExpress.Xpf.Core.v19.2;component/Core/Window/Icons/";
            string str2 = string.Empty;
            if (icon <= MessageBoxImage.Hand)
            {
                if (icon == MessageBoxImage.None)
                {
                    return;
                }
                if (icon == MessageBoxImage.Hand)
                {
                    str2 = "Error_48x48.png";
                }
            }
            else if (icon == MessageBoxImage.Question)
            {
                str2 = "Question_48x48.png";
            }
            else if (icon == MessageBoxImage.Exclamation)
            {
                str2 = "Warning_48x48.png";
            }
            else if (icon == MessageBoxImage.Asterisk)
            {
                str2 = "Information_48x48.png";
            }
            string text = str + str2;
            dxmb.ImageSource = (System.Windows.Media.ImageSource) new ImageSourceConverter().ConvertFromString(text);
        }

        protected static void InitSvgImageSource(DXMessageBox dxmb, MessageBoxImage icon)
        {
            string str = "pack://application:,,,/DevExpress.Xpf.Core.v19.2;component/Core/Window/Icons/";
            string str2 = string.Empty;
            if (icon <= MessageBoxImage.Hand)
            {
                if (icon == MessageBoxImage.None)
                {
                    return;
                }
                if (icon == MessageBoxImage.Hand)
                {
                    str2 = "Error_48x48.svg";
                }
            }
            else if (icon == MessageBoxImage.Question)
            {
                str2 = "Question_48x48.svg";
            }
            else if (icon == MessageBoxImage.Exclamation)
            {
                str2 = "Warning_48x48.svg";
            }
            else if (icon == MessageBoxImage.Asterisk)
            {
                str2 = "Information_48x48.svg";
            }
            SvgImageSourceExtension extension = new SvgImageSourceExtension {
                Uri = new Uri(str + str2)
            };
            dxmb.ImageSource = (System.Windows.Media.ImageSource) extension.ProvideValue(null);
        }

        private static void InitText(DXMessageBox dxmb, string messageBoxText)
        {
            dxmb.Text = messageBoxText;
        }

        protected bool IsButtonVisible(string buttonName)
        {
            Button button = this.FindMessageBoxButton(buttonName);
            return ((button != null) && (button.ActualWidth > 0.0));
        }

        private static bool IsValidMessageBoxButton(MessageBoxButton value) => 
            (value == MessageBoxButton.OK) || ((value == MessageBoxButton.OKCancel) || ((value == MessageBoxButton.YesNo) || (value == MessageBoxButton.YesNoCancel)));

        private static bool IsValidMessageBoxImage(MessageBoxImage value) => 
            (value == MessageBoxImage.Asterisk) || ((value == MessageBoxImage.Hand) || ((value == MessageBoxImage.Exclamation) || ((value == MessageBoxImage.Hand) || ((value == MessageBoxImage.Asterisk) || ((value == MessageBoxImage.None) || ((value == MessageBoxImage.Question) || ((value == MessageBoxImage.Hand) || (value == MessageBoxImage.Exclamation))))))));

        private static bool IsValidMessageBoxOptions(MessageBoxOptions value)
        {
            int num = -3801089;
            return ((value & num) == MessageBoxOptions.None);
        }

        private static bool IsValidMessageBoxResult(MessageBoxResult value) => 
            (value == MessageBoxResult.Cancel) || ((value == MessageBoxResult.No) || ((value == MessageBoxResult.None) || ((value == MessageBoxResult.OK) || (value == MessageBoxResult.Yes))));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateVisualState();
            this.DelayedExecute(() => this.FindAndFocusDefaultButton());
            if (this.ImageSource == null)
            {
                Image image = LayoutHelper.FindElementByName(this, "PART_MessageBoxImage") as Image;
                if (image != null)
                {
                    image.Visibility = Visibility.Collapsed;
                }
            }
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            this.ProcessYesNoKey(e, "PART_YesButton", Key.Y, MessageBoxResult.Yes);
            this.ProcessYesNoKey(e, "PART_NoButton", Key.N, MessageBoxResult.No);
            this.ProcessEscapeKey(e);
            if ((e.Key == Key.Tab) && ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control))
            {
                e.Handled = true;
            }
        }

        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            if ((((e.Key == Key.C) && Keyboard.IsKeyDown(Key.LeftCtrl)) || (((e.Key == Key.LeftCtrl) && Keyboard.IsKeyDown(Key.C)) || (((e.Key == Key.C) && Keyboard.IsKeyDown(Key.RightCtrl)) || (((e.Key == Key.RightCtrl) && Keyboard.IsKeyDown(Key.C)) || (((e.Key == Key.LeftCtrl) && Keyboard.IsKeyDown(Key.Insert)) || (((e.Key == Key.Insert) && Keyboard.IsKeyDown(Key.LeftCtrl)) || ((e.Key == Key.RightCtrl) && Keyboard.IsKeyDown(Key.Insert)))))))) || ((e.Key == Key.Insert) && Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                Clipboard.SetText(this.GetTextForClipBoard());
            }
            base.OnPreviewKeyUp(e);
        }

        private void ProcessClosing()
        {
            if (this.fc != null)
            {
                this.fc.Close();
            }
        }

        protected void ProcessEscapeKey(KeyEventArgs e)
        {
            if ((e.Key == Key.Escape) && (this.messageBoxButtons != MessageBoxButton.YesNo))
            {
                this.messageBoxResult = (this.messageBoxButtons != MessageBoxButton.OK) ? ((MessageBoxResult?) 2) : ((MessageBoxResult?) 1);
                this.ProcessClosing();
                e.Handled = true;
            }
        }

        private static bool ProcessMouseEvent(DependencyObject originalSource) => 
            true;

        protected void ProcessYesNoKey(KeyEventArgs e, string buttonName, Key key, MessageBoxResult mbr)
        {
            if ((e.Key == key) && this.IsButtonVisible(buttonName))
            {
                this.messageBoxResult = new MessageBoxResult?(mbr);
                this.ProcessClosing();
                e.Handled = true;
            }
        }

        [SecurityCritical, DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        private static void root_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = ProcessMouseEvent(e.OriginalSource as DependencyObject);
        }

        private static void root_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = ProcessMouseEvent(e.OriginalSource as DependencyObject);
        }

        private static void root_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = ProcessMouseEvent(e.OriginalSource as DependencyObject);
        }

        private static void SetSound(MessageBoxImage icon)
        {
            SystemSound hand = null;
            if (icon <= MessageBoxImage.Question)
            {
                if (icon == MessageBoxImage.Hand)
                {
                    hand = SystemSounds.Hand;
                }
                else if (icon == MessageBoxImage.Question)
                {
                    hand = SystemSounds.Question;
                }
            }
            else if (icon == MessageBoxImage.Exclamation)
            {
                hand = SystemSounds.Exclamation;
            }
            else if (icon == MessageBoxImage.Asterisk)
            {
                hand = SystemSounds.Asterisk;
            }
            if (hand != null)
            {
                try
                {
                    hand.Play();
                }
                catch
                {
                }
            }
        }

        public static MessageBoxResult Show(string messageBoxText) => 
            Show(null, messageBoxText, string.Empty, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);

        public static MessageBoxResult Show(string messageBoxText, string caption) => 
            Show(null, messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);

        public static MessageBoxResult Show(FrameworkElement owner, string messageBoxText) => 
            Show(owner, messageBoxText, string.Empty, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button) => 
            Show(null, messageBoxText, caption, button, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);

        public static MessageBoxResult Show(FrameworkElement owner, string messageBoxText, string caption) => 
            Show(owner, messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon) => 
            Show(null, messageBoxText, caption, button, icon, MessageBoxResult.None, MessageBoxOptions.None);

        public static MessageBoxResult Show(FrameworkElement owner, string messageBoxText, string caption, MessageBoxButton button) => 
            Show(owner, messageBoxText, caption, button, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult) => 
            Show(null, messageBoxText, caption, button, icon, defaultResult, MessageBoxOptions.None);

        public static MessageBoxResult Show(FrameworkElement owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon) => 
            Show(owner, messageBoxText, caption, button, icon, MessageBoxResult.None, MessageBoxOptions.None);

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options) => 
            Show(null, messageBoxText, caption, button, icon, defaultResult, options);

        public static MessageBoxResult Show(FrameworkElement owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult) => 
            Show(owner, messageBoxText, caption, button, icon, defaultResult, MessageBoxOptions.None);

        public static MessageBoxResult Show(FrameworkElement owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options) => 
            Show(owner, messageBoxText, caption, button, icon, defaultResult, options, FloatingMode.Window);

        public static MessageBoxResult Show(FrameworkElement owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options, FloatingMode desiredFloatingMode) => 
            Show(owner, messageBoxText, caption, button, icon, defaultResult, options, desiredFloatingMode, false);

        public static MessageBoxResult Show(FrameworkElement owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options, FloatingMode desiredFloatingMode, bool allowShowAnimatoin) => 
            ShowCore(owner, messageBoxText, caption, button, icon, defaultResult, options, desiredFloatingMode, allowShowAnimatoin, 500.0);

        public static MessageBoxResult Show(FrameworkElement owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options, FloatingMode desiredFloatingMode, bool allowShowAnimatoin, double maximumWidth) => 
            ShowCore(owner, messageBoxText, caption, button, icon, defaultResult, options, desiredFloatingMode, allowShowAnimatoin, maximumWidth);

        private static MessageBoxResult ShowCore(FrameworkElement owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options, FloatingMode desiredFloatingMode, bool allowShowAnimatoin, double maximumWidth) => 
            ShowCore(creator, owner, messageBoxText, caption, button, icon, defaultResult, options, desiredFloatingMode, allowShowAnimatoin, maximumWidth);

        protected static MessageBoxResult ShowCore(DXMessageBoxCreator creator, FrameworkElement owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options, FloatingMode desiredFloatingMode, bool allowShowAnimatoin, double maximumWidth)
        {
            MessageBoxResult result;
            if (!IsValidMessageBoxButton(button))
            {
                throw new InvalidEnumArgumentException("button", (int) button, typeof(MessageBoxButton));
            }
            if (!IsValidMessageBoxImage(icon))
            {
                throw new InvalidEnumArgumentException("icon", (int) icon, typeof(MessageBoxImage));
            }
            if (!IsValidMessageBoxResult(defaultResult))
            {
                throw new InvalidEnumArgumentException("defaultResult", (int) defaultResult, typeof(MessageBoxResult));
            }
            if (!IsValidMessageBoxOptions(options))
            {
                throw new InvalidEnumArgumentException("options", (int) options, typeof(MessageBoxOptions));
            }
            if ((options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) == MessageBoxOptions.None)
            {
                owner ??= CalcOwner();
            }
            else if (owner != null)
            {
                throw new ArgumentException("CantShowMBServiceWithOwner");
            }
            if (VerifyIsAlive())
            {
                return MessageBox.Show(messageBoxText, caption, button, icon, defaultResult, options);
            }
            if ((owner != null) && owner.IsVisible)
            {
                return ((owner != null) ? ShowCoreValidOwner(creator, CorrectOwner(owner), messageBoxText, caption, button, icon, defaultResult, options, desiredFloatingMode, allowShowAnimatoin, maximumWidth) : MessageBoxResult.None);
            }
            Window window = creator.CreateWindow();
            try
            {
                window.ShowActivated = false;
                window.ShowInTaskbar = true;
                window.WindowState = WindowState.Minimized;
                window.AllowsTransparency = true;
                window.WindowStyle = WindowStyle.None;
                window.Background = Brushes.Transparent;
                if (ApplicationThemeHelper.ApplicationThemeName != string.Empty)
                {
                    window.SetValue(ThemeManager.ThemeNameProperty, ApplicationThemeHelper.ApplicationThemeName);
                }
                window.Show();
                result = ShowCoreValidOwner(creator, window, messageBoxText, caption, button, icon, defaultResult, options, FloatingMode.Window, allowShowAnimatoin, maximumWidth);
            }
            finally
            {
                if ((Application.Current != null) && (Application.Current.CheckAccess() && (Application.Current.Windows.Count <= 1)))
                {
                    CloseDXMessageBoxWindow(window);
                }
                else
                {
                    window.Close();
                }
            }
            return result;
        }

        [SecuritySafeCritical]
        private static MessageBoxResult ShowCoreValidOwner(DXMessageBoxCreator creator, FrameworkElement owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options, FloatingMode desiredFloatingMode, bool allowShowAnimation, double maximumWidth)
        {
            ReleaseCapture();
            FrameworkElement topLevelVisual = LayoutHelper.GetTopLevelVisual(owner);
            DXMessageBox dxmb = creator.Create();
            bool showCloseButton = button != MessageBoxButton.YesNo;
            dxmb.defaultResult = defaultResult;
            dxmb.MaxWidth = maximumWidth;
            dxmb.InitFloatingContainer(owner, messageBoxText, caption, icon, button, desiredFloatingMode, showCloseButton, showCloseButton, allowShowAnimation, options);
            if (!NetVersionDetector.IsNetCore3())
            {
                SetSound(icon);
            }
            if (button == MessageBoxButton.YesNo)
            {
                FloatingWindowContainer fc = dxmb.fc as FloatingWindowContainer;
                if ((fc != null) && (fc.Window != null))
                {
                    fc.allowProcessClosing = false;
                    fc.Window.Closing += delegate (object s, CancelEventArgs e) {
                        if (dxmb.messageBoxResult == null)
                        {
                            e.Cancel = true;
                        }
                    };
                }
            }
            dxmb.DialogWndProc();
            Window window = LayoutHelper.FindRoot(owner, false) as Window;
            if (window != null)
            {
                window.Activate();
            }
            return ((dxmb.messageBoxResult != null) ? dxmb.messageBoxResult.Value : MessageBoxResult.None);
        }

        private void UpdateVisualState()
        {
            VisualStateManager.GoToState(this, this.messageBoxButtons.ToString(), true);
        }

        private static bool VerifyIsAlive()
        {
            PropertyInfo property = typeof(Application).GetProperty("IsShuttingDown", BindingFlags.NonPublic | BindingFlags.Static);
            if (property == null)
            {
                return false;
            }
            MethodInfo getMethod = property.GetGetMethod(true);
            return ((getMethod != null) ? ((bool) getMethod.Invoke(null, new object[0])) : false);
        }

        protected MessageBoxButton messageBoxButtons
        {
            get => 
                this._messageBoxButtons;
            set
            {
                if (value != this._messageBoxButtons)
                {
                    this._messageBoxButtons = value;
                    this.UpdateVisualState();
                }
            }
        }

        public string Text
        {
            get => 
                (string) base.GetValue(TextProperty);
            set => 
                base.SetValue(TextProperty, value);
        }

        public string Caption
        {
            get => 
                (string) base.GetValue(CaptionProperty);
            set => 
                base.SetValue(CaptionProperty, value);
        }

        public System.Windows.Media.ImageSource ImageSource
        {
            get => 
                (System.Windows.Media.ImageSource) base.GetValue(ImageSourceProperty);
            set => 
                base.SetValue(ImageSourceProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXMessageBox.<>c <>9 = new DXMessageBox.<>c();
            public static Predicate<FrameworkElement> <>9__44_0;

            internal bool <CorrectOwner>b__44_0(FrameworkElement element) => 
                element is BackgroundPanel;
        }
    }
}

