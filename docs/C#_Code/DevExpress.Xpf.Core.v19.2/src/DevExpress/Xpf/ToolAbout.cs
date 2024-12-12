namespace DevExpress.Xpf
{
    using DevExpress.Data.Internal;
    using DevExpress.Data.Utils;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class ToolAbout : UserControl, IComponentConnector
    {
        internal ToolAbout Window;
        internal Button CloseButton;
        internal Grid Images;
        internal Image LicensedImage;
        internal Image TrialImage;
        internal Image ExpiredImage;
        internal StackPanel LicensedSection;
        internal Button ProductEULAButton;
        internal Button SupportCenterButton;
        internal Button ChatButton;
        internal TextBlock CopyrightText;
        private bool _contentLoaded;

        public ToolAbout(AboutToolInfo info)
        {
            base.DataContext = info;
            this.InitializeComponent();
            this.Initialize();
        }

        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {
            this.Navigate("Https://go.devexpress.com/Demo_2013_Chat.aspx");
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ((System.Windows.Window) base.Parent).Close();
        }

        protected void Initialize()
        {
            this.LicensedImage.Visibility = Visibility.Collapsed;
            this.ExpiredImage.Visibility = Visibility.Collapsed;
            this.TrialImage.Visibility = Visibility.Collapsed;
            switch (this.AboutInfo.LicenseState)
            {
                case LicenseState.Licensed:
                    this.LicensedImage.Visibility = Visibility.Visible;
                    break;

                case LicenseState.Trial:
                    this.TrialImage.Visibility = Visibility.Visible;
                    break;

                case LicenseState.TrialExpired:
                    this.ExpiredImage.Visibility = Visibility.Visible;
                    break;

                default:
                    break;
            }
            this.CopyrightText.Text = $"Copyright © 2000-{DateTime.Now.Year.ToString()} Developer Express Inc.";
            this.CloseButton.Click += new RoutedEventHandler(this.CloseButton_Click);
            this.SupportCenterButton.Click += new RoutedEventHandler(this.SupportCenterButton_Click);
            this.ChatButton.Click += new RoutedEventHandler(this.ChatButton_Click);
            this.ProductEULAButton.Click += new RoutedEventHandler(this.ProductEULAButton_Click);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Core.v19.2;component/core/about/toolabout.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void Navigate(string url)
        {
            ProcessStartTrace.Trusted(() => SafeProcess.Start(url, null, null), url);
        }

        private void ProductEULAButton_Click(object sender, RoutedEventArgs e)
        {
            this.Navigate(this.AboutInfo.ProductEULAUri);
        }

        private void SupportCenterButton_Click(object sender, RoutedEventArgs e)
        {
            this.Navigate("Https://go.devexpress.com/Demo_2013_GetSupport.aspx");
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.Window = (ToolAbout) target;
                    return;

                case 2:
                    this.CloseButton = (Button) target;
                    return;

                case 3:
                    this.Images = (Grid) target;
                    return;

                case 4:
                    this.LicensedImage = (Image) target;
                    return;

                case 5:
                    this.TrialImage = (Image) target;
                    return;

                case 6:
                    this.ExpiredImage = (Image) target;
                    return;

                case 7:
                    this.LicensedSection = (StackPanel) target;
                    return;

                case 8:
                    this.ProductEULAButton = (Button) target;
                    return;

                case 9:
                    this.SupportCenterButton = (Button) target;
                    return;

                case 10:
                    this.ChatButton = (Button) target;
                    return;

                case 11:
                    this.CopyrightText = (TextBlock) target;
                    return;
            }
            this._contentLoaded = true;
        }

        public AboutToolInfo AboutInfo =>
            base.DataContext as AboutToolInfo;
    }
}

