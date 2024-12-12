namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TargetType(typeof(UserControl)), TargetType(typeof(Window))]
    public class WindowService : ViewServiceBase, DevExpress.Mvvm.IWindowService, IDocumentOwner
    {
        private const string WindowTypeException = "WindowType show be derived from the Window type";
        public static readonly DependencyProperty WindowStartupLocationProperty = DependencyProperty.Register("WindowStartupLocation", typeof(System.Windows.WindowStartupLocation), typeof(WindowService), new PropertyMetadata(System.Windows.WindowStartupLocation.CenterScreen));
        public static readonly DependencyProperty AllowSetWindowOwnerProperty = DependencyProperty.Register("AllowSetWindowOwner", typeof(bool), typeof(WindowService), new PropertyMetadata(true));
        public static readonly DependencyProperty WindowStyleProperty = DependencyProperty.Register("WindowStyle", typeof(Style), typeof(WindowService), new PropertyMetadata(null));
        public static readonly DependencyProperty WindowTypeProperty;
        public static readonly DependencyProperty TitleProperty;
        public static readonly DependencyProperty WindowShowModeProperty;
        private IWindowSurrogate window;

        static WindowService()
        {
            WindowTypeProperty = DependencyProperty.Register("WindowType", typeof(Type), typeof(WindowService), new PropertyMetadata(null, (d, e) => ((WindowService) d).OnWindowTypeChanged()));
            TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(WindowService), new PropertyMetadata(string.Empty, (d, e) => ((WindowService) d).OnTitleChanged()));
            WindowShowModeProperty = DependencyProperty.Register("WindowShowMode", typeof(DevExpress.Mvvm.UI.WindowShowMode), typeof(WindowService), new PropertyMetadata(DevExpress.Mvvm.UI.WindowShowMode.Default));
        }

        protected virtual IWindowSurrogate CreateWindow(object view)
        {
            IWindowSurrogate windowSurrogate = WindowProxy.GetWindowSurrogate(Activator.CreateInstance(this.ActualWindowType));
            base.UpdateThemeName(windowSurrogate.RealWindow);
            windowSurrogate.RealWindow.Content = view;
            base.InitializeDocumentContainer(windowSurrogate.RealWindow, ContentControl.ContentProperty, this.WindowStyle);
            windowSurrogate.RealWindow.WindowStartupLocation = this.WindowStartupLocation;
            if (this.AllowSetWindowOwner && (base.AssociatedObject != null))
            {
                windowSurrogate.RealWindow.Owner = Window.GetWindow(base.AssociatedObject);
            }
            return windowSurrogate;
        }

        void IDocumentOwner.Close(IDocumentContent documentContent, bool force)
        {
            if ((this.window != null) && (this.GetViewModel(this.window.RealWindow) == documentContent))
            {
                if (force)
                {
                    this.window.Closing -= new CancelEventHandler(this.OnWindowClosing);
                }
                this.window.Close();
            }
        }

        void DevExpress.Mvvm.IWindowService.Activate()
        {
            if (this.window != null)
            {
                this.window.Activate();
            }
        }

        void DevExpress.Mvvm.IWindowService.Close()
        {
            if (this.window != null)
            {
                this.window.Close();
            }
        }

        void DevExpress.Mvvm.IWindowService.Hide()
        {
            if (this.window != null)
            {
                this.window.Hide();
            }
        }

        void DevExpress.Mvvm.IWindowService.Restore()
        {
            if (this.window != null)
            {
                this.window.Show();
            }
        }

        void DevExpress.Mvvm.IWindowService.SetWindowState(WindowState state)
        {
            if (this.window != null)
            {
                this.window.RealWindow.WindowState = state;
            }
        }

        void DevExpress.Mvvm.IWindowService.Show(string documentType, object viewModel, object parameter, object parentViewModel)
        {
            if (this.window != null)
            {
                this.window.Show();
            }
            else
            {
                object view = base.CreateAndInitializeView(documentType, viewModel, parameter, parentViewModel, this);
                this.window = this.CreateWindow(view);
                this.SetTitleBinding();
                this.window.Closing += new CancelEventHandler(this.OnWindowClosing);
                this.window.Closed += new EventHandler(this.OnWindowClosed);
                if (this.WindowShowMode == DevExpress.Mvvm.UI.WindowShowMode.Dialog)
                {
                    this.window.ShowDialog();
                }
                else
                {
                    this.window.Show();
                }
            }
        }

        internal static Type GetDefaultDialogWindowType(Style windowStyle) => 
            !CompatibilitySettings.UseThemedWindowInServices ? (GetWindowType(windowStyle, typeof(DXDialogWindow)) ?? GetWindowType(windowStyle, typeof(ThemedWindow))) : (GetWindowType(windowStyle, typeof(ThemedWindow)) ?? GetWindowType(windowStyle, typeof(DXDialogWindow)));

        internal static Type GetDefaultTabbedWindowType(Style windowStyle) => 
            !CompatibilitySettings.UseThemedWindowInServices ? (GetWindowType(windowStyle, typeof(DXTabbedWindow)) ?? GetWindowType(windowStyle, typeof(ThemedWindow))) : (GetWindowType(windowStyle, typeof(ThemedWindow)) ?? GetWindowType(windowStyle, typeof(DXTabbedWindow)));

        internal static Type GetDefaultWindowType(Style windowStyle) => 
            !CompatibilitySettings.UseThemedWindowInServices ? (GetWindowType(windowStyle, typeof(DXWindow)) ?? GetWindowType(windowStyle, typeof(ThemedWindow))) : (GetWindowType(windowStyle, typeof(ThemedWindow)) ?? GetWindowType(windowStyle, typeof(DXWindow)));

        private object GetViewModel(Window window) => 
            ViewHelper.GetViewModelFromView(window.Content);

        private static Type GetWindowType(Style windowStyle, Type expectedType) => 
            ((windowStyle == null) || (windowStyle.TargetType == expectedType)) ? expectedType : (!expectedType.IsAssignableFrom(windowStyle.TargetType) ? (!windowStyle.TargetType.IsAssignableFrom(expectedType) ? null : expectedType) : windowStyle.TargetType);

        private void OnTitleChanged()
        {
            if (this.window != null)
            {
                string title = this.Title;
                string text2 = title;
                if (title == null)
                {
                    string local1 = title;
                    text2 = string.Empty;
                }
                this.window.RealWindow.Title = text2;
            }
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            this.window.Closing -= new CancelEventHandler(this.OnWindowClosing);
            this.window.Closed -= new EventHandler(this.OnWindowClosed);
            DocumentViewModelHelper.OnDestroy(this.GetViewModel(this.window.RealWindow));
            this.window = null;
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            DocumentViewModelHelper.OnClose(this.GetViewModel(this.window.RealWindow), e);
        }

        private void OnWindowTypeChanged()
        {
            if ((this.WindowType != null) && !typeof(Window).IsAssignableFrom(this.WindowType))
            {
                throw new ArgumentException("WindowType show be derived from the Window type");
            }
        }

        private void SetTitleBinding()
        {
            if (string.IsNullOrEmpty(this.Title))
            {
                DocumentUIServiceBase.SetTitleBinding(this.window.RealWindow.Content, Window.TitleProperty, this.window.RealWindow, true);
            }
            else
            {
                this.window.RealWindow.Title = this.Title;
            }
        }

        public System.Windows.WindowStartupLocation WindowStartupLocation
        {
            get => 
                (System.Windows.WindowStartupLocation) base.GetValue(WindowStartupLocationProperty);
            set => 
                base.SetValue(WindowStartupLocationProperty, value);
        }

        public bool AllowSetWindowOwner
        {
            get => 
                (bool) base.GetValue(AllowSetWindowOwnerProperty);
            set => 
                base.SetValue(AllowSetWindowOwnerProperty, value);
        }

        public Type WindowType
        {
            get => 
                (Type) base.GetValue(WindowTypeProperty);
            set => 
                base.SetValue(WindowTypeProperty, value);
        }

        public Style WindowStyle
        {
            get => 
                (Style) base.GetValue(WindowStyleProperty);
            set => 
                base.SetValue(WindowStyleProperty, value);
        }

        public string Title
        {
            get => 
                (string) base.GetValue(TitleProperty);
            set => 
                base.SetValue(TitleProperty, value);
        }

        public DevExpress.Mvvm.UI.WindowShowMode WindowShowMode
        {
            get => 
                (DevExpress.Mvvm.UI.WindowShowMode) base.GetValue(WindowShowModeProperty);
            set => 
                base.SetValue(WindowShowModeProperty, value);
        }

        private Type ActualWindowType =>
            this.WindowType ?? GetDefaultWindowType(this.WindowStyle);

        bool DevExpress.Mvvm.IWindowService.IsWindowAlive =>
            this.window != null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WindowService.<>c <>9 = new WindowService.<>c();

            internal void <.cctor>b__49_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((WindowService) d).OnWindowTypeChanged();
            }

            internal void <.cctor>b__49_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((WindowService) d).OnTitleChanged();
            }
        }
    }
}

