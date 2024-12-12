namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Internal;
    using DevExpress.Mvvm.UI;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;

    public class WpfToastNotificationFactory : IPredefinedToastNotificationFactory
    {
        private CustomNotifier notifier;
        private IPredefinedToastNotificationContentFactory factoryCore;

        public WpfToastNotificationFactory(CustomNotifier notifier)
        {
            this.notifier = notifier;
        }

        public virtual IPredefinedToastNotificationContentFactory CreateContentFactory() => 
            new WpfPredefinedToastNotificationContentFactory();

        public IPredefinedToastNotification CreateToastNotification(IPredefinedToastNotificationContent content) => 
            new WpfPredefinedToastNotification((WpfPredefinedToastNotificationContent) content, this.notifier);

        public IPredefinedToastNotification CreateToastNotification(string bodyText) => 
            this.CreateToastNotification(this.DefaultFactory.CreateContent(bodyText));

        public IPredefinedToastNotification CreateToastNotificationOneLineHeaderContent(string headlineText, string bodyText) => 
            this.CreateToastNotification(this.DefaultFactory.CreateOneLineHeaderContent(headlineText, bodyText));

        public IPredefinedToastNotification CreateToastNotificationOneLineHeaderContent(string headlineText, string bodyText1, string bodyText2) => 
            this.CreateToastNotification(this.DefaultFactory.CreateOneLineHeaderContent(headlineText, bodyText1, bodyText2));

        public IPredefinedToastNotification CreateToastNotificationTwoLineHeader(string headlineText, string bodyText) => 
            this.CreateToastNotification(this.DefaultFactory.CreateTwoLineHeaderContent(headlineText, bodyText));

        private IPredefinedToastNotificationContentFactory DefaultFactory
        {
            get
            {
                this.factoryCore ??= this.CreateContentFactory();
                return this.factoryCore;
            }
        }

        public double ImageSize =>
            90.0;

        private class WpfPredefinedToastNotificationContentFactory : IPredefinedToastNotificationContentFactory
        {
            public IPredefinedToastNotificationContent CreateContent(string bodyText)
            {
                PredefinedToastNotificationVewModel viewModel = CreateDefaultViewModel();
                viewModel.ToastTemplate = NotificationTemplate.LongText;
                viewModel.Text1 = bodyText;
                return new WpfPredefinedToastNotificationContent(viewModel);
            }

            private static PredefinedToastNotificationVewModel CreateDefaultViewModel()
            {
                PredefinedToastNotificationVewModel model = new PredefinedToastNotificationVewModel();
                Icon icon = ExtractAssociatedIcon(Environment.GetCommandLineArgs()[0]);
                if (icon == null)
                {
                    model.BackgroundColor = BackgroundCalculator.DefaultGrayColor;
                }
                else
                {
                    model.Icon = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, new Int32Rect(0, 0, icon.Width, icon.Height), BitmapSizeOptions.FromEmptyOptions());
                    model.BackgroundColor = BackgroundCalculator.GetBestMatch(icon.ToBitmap());
                }
                return model;
            }

            public IPredefinedToastNotificationContent CreateOneLineHeaderContent(string headlineText, string bodyText)
            {
                PredefinedToastNotificationVewModel viewModel = CreateDefaultViewModel();
                viewModel.ToastTemplate = NotificationTemplate.ShortHeaderAndLongText;
                viewModel.Text1 = headlineText;
                viewModel.Text2 = bodyText;
                return new WpfPredefinedToastNotificationContent(viewModel);
            }

            public IPredefinedToastNotificationContent CreateOneLineHeaderContent(string headlineText, string bodyText1, string bodyText2)
            {
                PredefinedToastNotificationVewModel viewModel = CreateDefaultViewModel();
                viewModel.ToastTemplate = NotificationTemplate.ShortHeaderAndTwoTextFields;
                viewModel.Text1 = headlineText;
                viewModel.Text2 = bodyText1;
                viewModel.Text3 = bodyText2;
                return new WpfPredefinedToastNotificationContent(viewModel);
            }

            public IPredefinedToastNotificationContent CreateTwoLineHeaderContent(string headlineText, string bodyText)
            {
                PredefinedToastNotificationVewModel viewModel = CreateDefaultViewModel();
                viewModel.ToastTemplate = NotificationTemplate.LongHeaderAndShortText;
                viewModel.Text1 = headlineText;
                viewModel.Text2 = bodyText;
                return new WpfPredefinedToastNotificationContent(viewModel);
            }

            [SecuritySafeCritical]
            private static Icon ExtractAssociatedIcon(string path)
            {
                if (string.IsNullOrEmpty(path))
                {
                    return null;
                }
                if (!File.Exists(path))
                {
                    path = path + ".exe";
                    if (!File.Exists(path))
                    {
                        return null;
                    }
                }
                int index = 0;
                IntPtr handle = ExtractAssociatedIcon(IntPtr.Zero, new StringBuilder(path), ref index);
                return (!(handle != IntPtr.Zero) ? null : Icon.FromHandle(handle));
            }

            [DllImport("shell32.dll", CharSet=CharSet.Auto)]
            internal static extern IntPtr ExtractAssociatedIcon(IntPtr hInst, StringBuilder iconPath, ref int index);
        }
    }
}

