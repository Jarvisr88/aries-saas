namespace DevExpress.Mvvm.UI
{
    using DevExpress.Data;
    using DevExpress.Internal;
    using DevExpress.Internal.WinApi;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Mvvm.UI.Interactivity.Internal;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Threading;

    [TargetType(typeof(UserControl)), TargetType(typeof(Window))]
    public class NotificationService : ServiceBase, INotificationService
    {
        public static readonly DependencyProperty UseWin8NotificationsIfAvailableProperty;
        public static readonly DependencyProperty CustomNotificationStyleProperty;
        public static readonly DependencyProperty CustomNotificationTemplateProperty;
        public static readonly DependencyProperty CustomNotificationTemplateSelectorProperty;
        public static readonly DependencyProperty CustomNotificationDurationProperty;
        public static readonly DependencyProperty CustomNotificationPositionProperty;
        public static readonly DependencyProperty CustomNotificationVisibleMaxCountProperty;
        public static readonly DependencyProperty PredefinedNotificationTemplateProperty;
        public static readonly DependencyProperty ApplicationIdProperty;
        public static readonly DependencyProperty PredefinedNotificationDurationProperty;
        public static readonly DependencyProperty SoundProperty;
        public static readonly DependencyProperty CustomNotificationScreenProperty;
        private bool applicationShortcutWasCreated;
        private DispatcherOperation shortcutCreatedOperation;
        private IPredefinedToastNotificationFactory predefinedNotificationsFactory;
        private DevExpress.Mvvm.UI.Native.CustomNotifier customNotifier;
        private DevExpress.Mvvm.UI.Native.CustomNotifier predefinedNotifier;
        private Window rootWindow;

        static NotificationService()
        {
            UseWin8NotificationsIfAvailableProperty = DependencyProperty.Register("UseWin8NotificationsIfAvailable", typeof(bool), typeof(NotificationService), new PropertyMetadata(true, (d, e) => ((NotificationService) d).OnUseWin8NotificationsIfAvailableChanged()));
            CustomNotificationStyleProperty = DependencyProperty.Register("CustomNotificationStyle", typeof(Style), typeof(NotificationService), new PropertyMetadata(null, (d, e) => ((NotificationService) d).OnCustomNotificationStyleChanged()));
            CustomNotificationTemplateProperty = DependencyProperty.Register("CustomNotificationTemplate", typeof(DataTemplate), typeof(NotificationService), new PropertyMetadata(null, (d, e) => ((NotificationService) d).OnCustomNotificationTemplateChanged()));
            CustomNotificationTemplateSelectorProperty = DependencyProperty.Register("CustomNotificationTemplateSelector", typeof(DataTemplateSelector), typeof(NotificationService), new PropertyMetadata(null, (d, e) => ((NotificationService) d).OnCustomNotificationTemplateSelectorChanged()));
            CustomNotificationDurationProperty = DependencyProperty.Register("CustomNotificationDuration", typeof(TimeSpan), typeof(NotificationService), new PropertyMetadata(TimeSpan.FromMilliseconds(6000.0)));
            CustomNotificationPositionProperty = DependencyProperty.Register("CustomNotificationPosition", typeof(NotificationPosition), typeof(NotificationService), new PropertyMetadata(NotificationPosition.TopRight, (d, e) => ((NotificationService) d).UpdateCustomNotifierPositioner()));
            CustomNotificationVisibleMaxCountProperty = DependencyProperty.Register("CustomNotificationVisibleMaxCount", typeof(int), typeof(NotificationService), new PropertyMetadata(3, (d, e) => ((NotificationService) d).UpdateCustomNotifierPositioner()));
            PredefinedNotificationTemplateProperty = DependencyProperty.Register("PredefinedNotificationTemplate", typeof(NotificationTemplate), typeof(NotificationService), new PropertyMetadata(NotificationTemplate.LongText));
            ApplicationIdProperty = DependencyProperty.Register("ApplicationId", typeof(string), typeof(NotificationService), new PropertyMetadata(null));
            PredefinedNotificationDurationProperty = DependencyProperty.Register("PredefinedNotificationDuration", typeof(DevExpress.Mvvm.UI.PredefinedNotificationDuration), typeof(NotificationService), new PropertyMetadata(DevExpress.Mvvm.UI.PredefinedNotificationDuration.Default));
            SoundProperty = DependencyProperty.Register("Sound", typeof(DevExpress.Mvvm.UI.PredefinedSound), typeof(NotificationService), new PropertyMetadata(DevExpress.Mvvm.UI.PredefinedSound.Notification_Default));
            CustomNotificationScreenProperty = DependencyProperty.Register("CustomNotificationScreen", typeof(NotificationScreen), typeof(NotificationService), new PropertyMetadata(NotificationScreen.Primary));
        }

        public NotificationService()
        {
            this.shortcutCreatedOperation = base.Dispatcher.BeginInvoke(delegate {
                if (!this.applicationShortcutWasCreated)
                {
                    this.TryCreateApplicationShortcut();
                }
                if (this.ShouldRegisterApplicationActivatorSeparately())
                {
                    this.RegisterApplicationActivator();
                }
                if (this.CreateApplicationShortcut && (!this.RemoveApplicationShortcutOnUnloaded && this.AreWin8NotificationsAvailable))
                {
                    base.Dispatcher.ShutdownFinished += (s, e) => this.TryRemoveApplicationShortcut();
                }
            }, new object[0]);
        }

        private bool ApplicationParametersValid() => 
            (this.ApplicationActivator != null) && !string.IsNullOrEmpty(this.ApplicationId);

        private bool CanRemoveApplicationShortcutOnUnloaded() => 
            this.CreateApplicationShortcut && (this.RemoveApplicationShortcutOnUnloaded && this.AreWin8NotificationsAvailable);

        public INotification CreateCustomNotification(object viewModel) => 
            new MvvmCustomNotification(viewModel, this.CustomNotifier, ((base.AssociatedObject == null) || (this.CustomNotificationScreen != NotificationScreen.ApplicationWindow)) ? null : Window.GetWindow(base.AssociatedObject), (int) Math.Max(0.0, Math.Min(2147483647.0, this.CustomNotificationDuration.TotalMilliseconds)));

        public INotification CreatePredefinedNotification(string text1, string text2, string text3, ImageSource image = null)
        {
            this.shortcutCreatedOperation.Wait();
            IPredefinedToastNotificationContentFactory factory = this.PredefinedNotificationsFactory.CreateContentFactory();
            IPredefinedToastNotificationContent content = null;
            switch (this.PredefinedNotificationTemplate)
            {
                case NotificationTemplate.LongText:
                    content = factory.CreateContent(text1);
                    break;

                case NotificationTemplate.ShortHeaderAndLongText:
                    content = factory.CreateOneLineHeaderContent(text1, text2);
                    break;

                case NotificationTemplate.LongHeaderAndShortText:
                    content = factory.CreateTwoLineHeaderContent(text1, text2);
                    break;

                case NotificationTemplate.ShortHeaderAndTwoTextFields:
                    content = factory.CreateOneLineHeaderContent(text1, text2, text3);
                    break;

                default:
                    break;
            }
            if (image != null)
            {
                Point dpi = PrimaryScreen.GetDpi();
                double imageSize = this.PredefinedNotificationsFactory.ImageSize;
                Size size = new Size(imageSize * dpi.X, imageSize * dpi.Y);
                content.SetImage(ImageLoader2.ImageToByteArray(image, new Func<Uri>(this.GetBaseUri), new Size?(size)));
            }
            content.SetDuration((NotificationDuration) this.PredefinedNotificationDuration);
            content.SetSound((DevExpress.Internal.PredefinedSound) this.Sound);
            MvvmPredefinedNotification notification1 = new MvvmPredefinedNotification();
            notification1.Notification = this.PredefinedNotificationsFactory.CreateToastNotification(content);
            return notification1;
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.CanRemoveApplicationShortcutOnUnloaded())
            {
                base.AssociatedObject.Loaded += new RoutedEventHandler(this.OnLoaded);
                base.AssociatedObject.Unloaded += new RoutedEventHandler(this.OnUnloaded);
            }
        }

        private void OnCustomNotificationStyleChanged()
        {
            this.CustomNotifier.Style = this.CustomNotificationStyle;
        }

        private void OnCustomNotificationTemplateChanged()
        {
            this.CustomNotifier.ContentTemplate = this.CustomNotificationTemplate;
        }

        private void OnCustomNotificationTemplateSelectorChanged()
        {
            this.CustomNotifier.ContentTemplateSelector = this.CustomNotificationTemplateSelector;
        }

        protected override void OnDetaching()
        {
            if (this.CanRemoveApplicationShortcutOnUnloaded())
            {
                this.TryRemoveApplicationShortcut();
                base.AssociatedObject.Loaded -= new RoutedEventHandler(this.OnLoaded);
                base.AssociatedObject.Unloaded -= new RoutedEventHandler(this.OnUnloaded);
            }
            base.OnDetaching();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.TryCreateApplicationShortcut();
            this.rootWindow = LayoutHelper.GetRoot(base.AssociatedObject) as Window;
            this.rootWindow.Do<Window>(delegate (Window x) {
                x.Closed += new EventHandler(this.WindowClosed);
            });
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this.TryRemoveApplicationShortcut();
            this.rootWindow.Do<Window>(delegate (Window x) {
                x.Closed -= new EventHandler(this.WindowClosed);
            });
        }

        private void OnUseWin8NotificationsIfAvailableChanged()
        {
            this.predefinedNotificationsFactory = null;
            if (this.applicationShortcutWasCreated && !this.UseWin8NotificationsIfAvailable)
            {
                this.RemoveApplicationShortcut();
            }
            this.TryCreateApplicationShortcut();
        }

        public void RegisterApplicationActivator()
        {
            Action<Type> action = <>c.<>9__100_0;
            if (<>c.<>9__100_0 == null)
            {
                Action<Type> local1 = <>c.<>9__100_0;
                action = <>c.<>9__100_0 = x => ShellHelper.RegisterApplicationActivator(x);
            }
            this.ApplicationActivator.Do<Type>(action);
        }

        private void RemoveApplicationShortcut()
        {
            ShellHelper.TryRemoveShortcut(this.ApplicationName);
            this.applicationShortcutWasCreated = false;
        }

        private bool ShouldCreateApplicationShortcut() => 
            this.CreateApplicationShortcut && (!this.applicationShortcutWasCreated && (this.UseWin8NotificationsIfAvailable && (this.AreWin8NotificationsAvailable && !InteractionHelper.IsInDesignMode(this))));

        private bool ShouldRegisterApplicationActivatorSeparately() => 
            this.UseWin8NotificationsIfAvailable && (this.AreWin8NotificationsAvailable && (!this.CreateApplicationShortcut && (!string.IsNullOrEmpty(this.ApplicationId) && !InteractionHelper.IsInDesignMode(this))));

        private void TryCreateApplicationShortcut()
        {
            if (this.ShouldCreateApplicationShortcut() && (this.ApplicationParametersValid() && !this.applicationShortcutWasCreated))
            {
                string applicationIconPath = this.ApplicationIconPath;
                if (!string.IsNullOrEmpty(applicationIconPath))
                {
                    applicationIconPath = Path.GetFullPath(applicationIconPath);
                }
                this.RegisterApplicationActivator();
                ShellHelper.TryCreateShortcut(null, this.ApplicationId, this.ApplicationName, applicationIconPath, this.ApplicationActivator);
                this.applicationShortcutWasCreated = true;
            }
        }

        private void TryRemoveApplicationShortcut()
        {
            if (this.applicationShortcutWasCreated)
            {
                this.RemoveApplicationShortcut();
            }
            if (!InteractionHelper.IsInDesignMode(this))
            {
                this.UnregisterApplicationActivator();
            }
        }

        public void UnregisterApplicationActivator()
        {
            Action<Type> action = <>c.<>9__101_0;
            if (<>c.<>9__101_0 == null)
            {
                Action<Type> local1 = <>c.<>9__101_0;
                action = <>c.<>9__101_0 = x => ShellHelper.UnregisterApplicationActivator();
            }
            this.ApplicationActivator.Do<Type>(action);
        }

        private void UpdateCustomNotifierPositioner()
        {
            this.PredefinedNotifier.UpdatePositioner(this.CustomNotificationPosition, this.CustomNotificationVisibleMaxCount);
            this.CustomNotifier.UpdatePositioner(this.CustomNotificationPosition, this.CustomNotificationVisibleMaxCount);
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            this.TryRemoveApplicationShortcut();
            ((Window) sender).Closed -= new EventHandler(this.WindowClosed);
        }

        public NotificationScreen CustomNotificationScreen
        {
            get => 
                (NotificationScreen) base.GetValue(CustomNotificationScreenProperty);
            set => 
                base.SetValue(CustomNotificationScreenProperty, value);
        }

        public Type ApplicationActivator { get; set; }

        public bool CreateApplicationShortcut { get; set; }

        public bool RemoveApplicationShortcutOnUnloaded { get; set; }

        public string ApplicationName { get; set; }

        public string ApplicationIconPath { get; set; }

        private IPredefinedToastNotificationFactory PredefinedNotificationsFactory
        {
            get
            {
                if (this.predefinedNotificationsFactory == null)
                {
                    if (!this.UseWin8NotificationsIfAvailable || !this.AreWin8NotificationsAvailable)
                    {
                        this.predefinedNotificationsFactory = new WpfToastNotificationFactory(this.PredefinedNotifier);
                    }
                    else
                    {
                        if (this.ApplicationId == null)
                        {
                            throw new ArgumentNullException("ApplicationId");
                        }
                        this.predefinedNotificationsFactory = new WinRTToastNotificationFactory(this.ApplicationId);
                    }
                }
                return this.predefinedNotificationsFactory;
            }
        }

        private DevExpress.Mvvm.UI.Native.CustomNotifier CustomNotifier
        {
            get
            {
                this.customNotifier ??= new DevExpress.Mvvm.UI.Native.CustomNotifier(null);
                return this.customNotifier;
            }
        }

        private DevExpress.Mvvm.UI.Native.CustomNotifier PredefinedNotifier
        {
            get
            {
                this.predefinedNotifier ??= new DevExpress.Mvvm.UI.Native.CustomNotifier(null);
                return this.predefinedNotifier;
            }
        }

        public bool UseWin8NotificationsIfAvailable
        {
            get => 
                (bool) base.GetValue(UseWin8NotificationsIfAvailableProperty);
            set => 
                base.SetValue(UseWin8NotificationsIfAvailableProperty, value);
        }

        public Style CustomNotificationStyle
        {
            get => 
                (Style) base.GetValue(CustomNotificationStyleProperty);
            set => 
                base.SetValue(CustomNotificationStyleProperty, value);
        }

        public DataTemplate CustomNotificationTemplate
        {
            get => 
                (DataTemplate) base.GetValue(CustomNotificationTemplateProperty);
            set => 
                base.SetValue(CustomNotificationTemplateProperty, value);
        }

        public DataTemplateSelector CustomNotificationTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(CustomNotificationTemplateSelectorProperty);
            set => 
                base.SetValue(CustomNotificationTemplateSelectorProperty, value);
        }

        public TimeSpan CustomNotificationDuration
        {
            get => 
                (TimeSpan) base.GetValue(CustomNotificationDurationProperty);
            set => 
                base.SetValue(CustomNotificationDurationProperty, value);
        }

        public NotificationPosition CustomNotificationPosition
        {
            get => 
                (NotificationPosition) base.GetValue(CustomNotificationPositionProperty);
            set => 
                base.SetValue(CustomNotificationPositionProperty, value);
        }

        public int CustomNotificationVisibleMaxCount
        {
            get => 
                (int) base.GetValue(CustomNotificationVisibleMaxCountProperty);
            set => 
                base.SetValue(CustomNotificationVisibleMaxCountProperty, value);
        }

        public string ApplicationId
        {
            get => 
                (string) base.GetValue(ApplicationIdProperty);
            set => 
                base.SetValue(ApplicationIdProperty, value);
        }

        public NotificationTemplate PredefinedNotificationTemplate
        {
            get => 
                (NotificationTemplate) base.GetValue(PredefinedNotificationTemplateProperty);
            set => 
                base.SetValue(PredefinedNotificationTemplateProperty, value);
        }

        public DevExpress.Mvvm.UI.PredefinedSound Sound
        {
            get => 
                (DevExpress.Mvvm.UI.PredefinedSound) base.GetValue(SoundProperty);
            set => 
                base.SetValue(SoundProperty, value);
        }

        public DevExpress.Mvvm.UI.PredefinedNotificationDuration PredefinedNotificationDuration
        {
            get => 
                (DevExpress.Mvvm.UI.PredefinedNotificationDuration) base.GetValue(PredefinedNotificationDurationProperty);
            set => 
                base.SetValue(PredefinedNotificationDurationProperty, value);
        }

        public bool AreWin8NotificationsAvailable =>
            ToastNotificationManager.AreToastNotificationsSupported;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NotificationService.<>c <>9 = new NotificationService.<>c();
            public static Action<Type> <>9__100_0;
            public static Action<Type> <>9__101_0;

            internal void <.cctor>b__106_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((NotificationService) d).OnUseWin8NotificationsIfAvailableChanged();
            }

            internal void <.cctor>b__106_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((NotificationService) d).OnCustomNotificationStyleChanged();
            }

            internal void <.cctor>b__106_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((NotificationService) d).OnCustomNotificationTemplateChanged();
            }

            internal void <.cctor>b__106_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((NotificationService) d).OnCustomNotificationTemplateSelectorChanged();
            }

            internal void <.cctor>b__106_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((NotificationService) d).UpdateCustomNotifierPositioner();
            }

            internal void <.cctor>b__106_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((NotificationService) d).UpdateCustomNotifierPositioner();
            }

            internal void <RegisterApplicationActivator>b__100_0(Type x)
            {
                ShellHelper.RegisterApplicationActivator(x);
            }

            internal void <UnregisterApplicationActivator>b__101_0(Type x)
            {
                ShellHelper.UnregisterApplicationActivator();
            }
        }

        internal class MvvmCustomNotification : INotification
        {
            private CustomNotifier notifier;
            private CustomNotification notification;
            private Window window;
            internal int duration;

            public MvvmCustomNotification(object viewModel, CustomNotifier notifier, Window window, int duration)
            {
                this.notifier = notifier;
                this.window = window;
                this.duration = duration;
                this.notification = new CustomNotification(viewModel, notifier);
            }

            public void Hide()
            {
                this.notifier.Hide(this.notification);
            }

            public Task<NotificationResult> ShowAsync()
            {
                Point position = new Point();
                if (this.window != null)
                {
                    Point point = new Point();
                    Point point2 = this.window.PointToScreen(point);
                    position = new Point(point2.X + (this.window.Width / 2.0), point2.Y + (this.window.Height / 2.0));
                }
                this.notifier.ChangeScreen(position);
                return this.notifier.ShowAsync(this.notification, this.duration);
            }
        }

        private class MvvmPredefinedNotification : INotification
        {
            public void Hide()
            {
                this.Notification.Hide();
            }

            public Task<NotificationResult> ShowAsync()
            {
                Func<Task<ToastNotificationResultInternal>, NotificationResult> continuationFunction = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Func<Task<ToastNotificationResultInternal>, NotificationResult> local1 = <>c.<>9__4_0;
                    continuationFunction = <>c.<>9__4_0 = (Func<Task<ToastNotificationResultInternal>, NotificationResult>) (t => t.Result);
                }
                return this.Notification.ShowAsync().ContinueWith<NotificationResult>(continuationFunction);
            }

            public IPredefinedToastNotification Notification { get; set; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly NotificationService.MvvmPredefinedNotification.<>c <>9 = new NotificationService.MvvmPredefinedNotification.<>c();
                public static Func<Task<ToastNotificationResultInternal>, NotificationResult> <>9__4_0;

                internal NotificationResult <ShowAsync>b__4_0(Task<ToastNotificationResultInternal> t) => 
                    t.Result;
            }
        }
    }
}

