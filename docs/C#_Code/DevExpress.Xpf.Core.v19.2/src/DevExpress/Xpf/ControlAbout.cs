namespace DevExpress.Xpf
{
    using DevExpress.Data.Internal;
    using DevExpress.Data.Utils;
    using DevExpress.Utils.About;
    using Microsoft.Win32;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Shapes;

    public class ControlAbout : UserControl, IComponentConnector
    {
        internal ControlAbout Window;
        internal Button CloseButton;
        internal Grid Images;
        internal Image LicensedImage;
        internal Image TrialImage;
        internal Image ExpiredImage;
        internal StackPanel TrialSection;
        internal TextBlock trialOrLicensed;
        internal StackPanel TrialDays;
        internal TextBlock TrialDaysCount;
        internal Button RegisterButton;
        internal Grid buttonsPanel;
        internal Button HelpButton;
        internal TextBlock blockToUnderline;
        internal Rectangle SubscribeButtonLine;
        internal Button BuyButton;
        internal TextBlock blockToUnderline1;
        internal Rectangle RegisterButtonLine;
        internal Button DiscountsButton;
        internal TextBlock blockToUnderline2;
        internal Rectangle DiscountsButtonLine;
        internal StackPanel LicensedSection;
        internal StackPanel questions;
        internal Button SupportCenterButton;
        internal Button ChatButton;
        internal TextBlock CopyrightText;
        private bool _contentLoaded;

        public ControlAbout(DevExpress.Xpf.AboutInfo info)
        {
            base.DataContext = info;
            this.InitializeComponent();
            this.Initialize();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private void buttonsPanel_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (this.FindZIndexPropertyElement(this.HelpButton) != null)
            {
                this.SubscribeButtonLine.Visibility = (((int) this.FindZIndexPropertyElement(this.HelpButton).GetValue(Panel.ZIndexProperty)) <= 0) ? Visibility.Collapsed : Visibility.Visible;
                this.RegisterButtonLine.Visibility = (((int) this.FindZIndexPropertyElement(this.BuyButton).GetValue(Panel.ZIndexProperty)) <= 0) ? Visibility.Collapsed : Visibility.Visible;
                if (((int) this.FindZIndexPropertyElement(this.DiscountsButton).GetValue(Panel.ZIndexProperty)) > 0)
                {
                    this.DiscountsButtonLine.Visibility = Visibility.Visible;
                }
                else
                {
                    this.DiscountsButtonLine.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void BuyButtonClick(object sender, RoutedEventArgs e)
        {
            this.Navigate("Https://go.devexpress.com/Demo_2013_BuyNow.aspx");
        }

        private string CalcSetupFilePath(string keyPath, string keyName)
        {
            string setupFilePath = this.GetSetupFilePath(keyPath, keyName);
            if (setupFilePath == string.Empty)
            {
                setupFilePath = this.GetSetupFilePath(keyPath.Replace("SOFTWARE", @"SOFTWARE\Wow6432Node"), keyName);
            }
            return setupFilePath;
        }

        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {
            this.Navigate("Https://go.devexpress.com/Demo_2013_Chat.aspx");
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Window parent = base.Parent as System.Windows.Window;
            if (parent != null)
            {
                parent.Close();
            }
        }

        private void DiscountsButtonClick(object sender, RoutedEventArgs e)
        {
            this.Navigate("https://go.devexpress.com/Demo_2013_Competitive_Discounts.aspx");
        }

        private DependencyObject FindZIndexPropertyElement(Button button) => 
            button.Template.FindName("rootButtonElement", button) as DependencyObject;

        private int GetDays() => 
            (this.AboutInfo.LicenseState == LicenseState.Licensed) ? 0 : Utility.DaysLeft();

        private string GetSetupFilePath(string keyPath, string keyName)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath);
            if (key != null)
            {
                string path = $"{key.GetValue(keyName)}";
                if (File.Exists(path))
                {
                    return path;
                }
            }
            return string.Empty;
        }

        private void HelpButtonClick(object sender, RoutedEventArgs e)
        {
            this.Navigate("Https://go.devexpress.com/Demo_2013_GetSupport.aspx");
        }

        protected void Initialize()
        {
            this.LicensedImage.Visibility = Visibility.Collapsed;
            this.ExpiredImage.Visibility = Visibility.Collapsed;
            this.TrialImage.Visibility = Visibility.Collapsed;
            this.TrialDays.Visibility = Visibility.Collapsed;
            switch (this.AboutInfo.LicenseState)
            {
                case LicenseState.Licensed:
                    this.LicensedImage.Visibility = Visibility.Visible;
                    this.TrialSection.Visibility = Visibility.Collapsed;
                    this.LicensedSection.Visibility = Visibility.Visible;
                    break;

                case LicenseState.Trial:
                    this.questions.Visibility = Visibility.Collapsed;
                    this.TrialImage.Visibility = Visibility.Visible;
                    this.trialOrLicensed.Text = "an eval version";
                    this.TrialSection.Visibility = Visibility.Visible;
                    this.LicensedSection.Visibility = Visibility.Collapsed;
                    this.TrialDays.Visibility = (this.GetDays() > 0) ? Visibility.Visible : Visibility.Collapsed;
                    this.TrialDaysCount.Text = this.GetDays().ToString();
                    break;

                case LicenseState.TrialExpired:
                    this.questions.Visibility = Visibility.Collapsed;
                    this.TrialSection.Visibility = Visibility.Visible;
                    this.LicensedSection.Visibility = Visibility.Collapsed;
                    this.ExpiredImage.Visibility = Visibility.Visible;
                    this.trialOrLicensed.Text = "an expired version";
                    break;

                default:
                    break;
            }
            this.CopyrightText.Text = $"Copyright © 2000-{DateTime.Now.Year.ToString()} Developer Express Inc.";
            this.CloseButton.Click += new RoutedEventHandler(this.CloseButtonClick);
            this.HelpButton.Click += new RoutedEventHandler(this.HelpButtonClick);
            this.BuyButton.Click += new RoutedEventHandler(this.BuyButtonClick);
            this.RegisterButton.Click += new RoutedEventHandler(this.RegisterButtonClick);
            this.DiscountsButton.Click += new RoutedEventHandler(this.DiscountsButtonClick);
            base.PreviewMouseMove += new MouseEventHandler(this.buttonsPanel_PreviewMouseMove);
            this.SupportCenterButton.Click += new RoutedEventHandler(this.SupportCenterButton_Click);
            this.ChatButton.Click += new RoutedEventHandler(this.ChatButton_Click);
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Core.v19.2;component/core/about/controlabout.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void Navigate(string url)
        {
            ProcessStartTrace.Trusted(() => SafeProcess.Start(url, null, null), url);
            this.CloseButtonClick(null, null);
        }

        private void RegisterButtonClick(object sender, RoutedEventArgs e)
        {
            string setupFilePath = this.CalcSetupFilePath(@"SOFTWARE\DevExpress\Components\v19.2", "SetupFilePath");
            bool showSite = false;
            if (setupFilePath == string.Empty)
            {
                showSite = true;
            }
            else
            {
                string arguments = "/Register";
                Action<ProcessStartInfo> action1 = <>c.<>9__11_0;
                if (<>c.<>9__11_0 == null)
                {
                    Action<ProcessStartInfo> local1 = <>c.<>9__11_0;
                    action1 = <>c.<>9__11_0 = delegate (ProcessStartInfo x) {
                        x.Verb = "Open";
                        x.WindowStyle = ProcessWindowStyle.Normal;
                    };
                }
                Action<ProcessStartInfo> setupProcessStartInfoAction = action1;
                ProcessStartTrace.Trusted(delegate {
                    if (SafeProcess.Start(setupFilePath, arguments, setupProcessStartInfoAction) == null)
                    {
                        showSite = true;
                    }
                }, setupFilePath, arguments);
            }
            if (showSite)
            {
                this.Navigate("https://go.devexpress.com/Demo_2013_RegisterTrial.aspx");
            }
            this.CloseButtonClick(null, null);
        }

        private void SupportCenterButton_Click(object sender, RoutedEventArgs e)
        {
            this.Navigate("Https://go.devexpress.com/Demo_2013_GetSupport.aspx");
        }

        [EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.Window = (ControlAbout) target;
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
                    this.TrialSection = (StackPanel) target;
                    return;

                case 8:
                    this.trialOrLicensed = (TextBlock) target;
                    return;

                case 9:
                    this.TrialDays = (StackPanel) target;
                    return;

                case 10:
                    this.TrialDaysCount = (TextBlock) target;
                    return;

                case 11:
                    this.RegisterButton = (Button) target;
                    return;

                case 12:
                    this.buttonsPanel = (Grid) target;
                    return;

                case 13:
                    this.HelpButton = (Button) target;
                    return;

                case 14:
                    this.blockToUnderline = (TextBlock) target;
                    return;

                case 15:
                    this.SubscribeButtonLine = (Rectangle) target;
                    return;

                case 0x10:
                    this.BuyButton = (Button) target;
                    return;

                case 0x11:
                    this.blockToUnderline1 = (TextBlock) target;
                    return;

                case 0x12:
                    this.RegisterButtonLine = (Rectangle) target;
                    return;

                case 0x13:
                    this.DiscountsButton = (Button) target;
                    return;

                case 20:
                    this.blockToUnderline2 = (TextBlock) target;
                    return;

                case 0x15:
                    this.DiscountsButtonLine = (Rectangle) target;
                    return;

                case 0x16:
                    this.LicensedSection = (StackPanel) target;
                    return;

                case 0x17:
                    this.questions = (StackPanel) target;
                    return;

                case 0x18:
                    this.SupportCenterButton = (Button) target;
                    return;

                case 0x19:
                    this.ChatButton = (Button) target;
                    return;

                case 0x1a:
                    this.CopyrightText = (TextBlock) target;
                    return;
            }
            this._contentLoaded = true;
        }

        public DevExpress.Xpf.AboutInfo AboutInfo =>
            base.DataContext as DevExpress.Xpf.AboutInfo;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ControlAbout.<>c <>9 = new ControlAbout.<>c();
            public static Action<ProcessStartInfo> <>9__11_0;

            internal void <RegisterButtonClick>b__11_0(ProcessStartInfo x)
            {
                x.Verb = "Open";
                x.WindowStyle = ProcessWindowStyle.Normal;
            }
        }
    }
}

