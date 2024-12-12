namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    public class ThemedMessageBoxWindow : ThemedWindow
    {
        public static readonly DependencyProperty TextProperty;
        public static readonly DependencyProperty MessageContentProperty;
        public static readonly DependencyProperty TextAlignmentProperty;
        public static readonly DependencyProperty ImageProperty;
        private ThemedWindowDialogButton yesButton;
        private ThemedWindowDialogButton noButton;

        static ThemedMessageBoxWindow()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemedMessageBoxWindow), new FrameworkPropertyMetadata(typeof(ThemedMessageBoxWindow)));
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ThemedMessageBoxWindow), new FrameworkPropertyMetadata(string.Empty));
            MessageContentProperty = DependencyProperty.Register("MessageContent", typeof(UIElement), typeof(ThemedMessageBoxWindow), new FrameworkPropertyMetadata(null));
            ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(ThemedMessageBoxWindow), new FrameworkPropertyMetadata(null));
            TextAlignmentProperty = DependencyProperty.Register("TextAlignment", typeof(System.Windows.TextAlignment), typeof(ThemedMessageBoxWindow), new FrameworkPropertyMetadata(System.Windows.TextAlignment.Left));
        }

        public ThemedMessageBoxWindow()
        {
            this.SubscribeEvents();
            base.CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, new ExecutedRoutedEventHandler(this.OnMessageBoxKeyGestureCopyPressed)));
        }

        private ThemedWindowDialogButton GetDefaultDialogButton()
        {
            Func<ThemedWindowDialogButton, bool> predicate = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Func<ThemedWindowDialogButton, bool> local1 = <>c.<>9__12_0;
                predicate = <>c.<>9__12_0 = x => x.IsDefault;
            }
            return TreeHelper.GetChild<ThemedWindowDialogButton>(this, predicate);
        }

        private ThemedWindowDialogButton GetDialogButton(MessageBoxResult buttonId) => 
            TreeHelper.GetChild<ThemedWindowDialogButton>(this, x => x.DialogResult == buttonId);

        private string GetDialogButtonsNamesString(IEnumerable collection)
        {
            Func<string, UICommand, string> func = <>c.<>9__33_0;
            if (<>c.<>9__33_0 == null)
            {
                Func<string, UICommand, string> local1 = <>c.<>9__33_0;
                func = <>c.<>9__33_0 = (current, button) => $"{current}{button.Caption}	";
            }
            return collection.OfType<UICommand>().Aggregate<UICommand, string>(string.Empty, func);
        }

        private string GetTextForClipboard() => 
            $"---------------------------
{base.Title}
---------------------------
{this.Text}
---------------------------
{this.GetDialogButtonsNamesString(base.ActualDialogButtons)}";

        protected override void OnClosed(EventArgs e)
        {
            this.UnsubscribeEvents();
            base.OnClosed(e);
        }

        private void OnInitialized(object sender, EventArgs e)
        {
            WindowShowHelper.InitializeThemedWindowFromOwner<ThemedMessageBoxWindow>(base.Owner);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (WindowButtonHelper.GetIsYesNoDialog(this) && ((e.Key == Key.System) && (e.SystemKey == Key.F4)))
            {
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (base.ShowActivated)
            {
                base.Activate();
            }
            ThemedWindowDialogButton defaultDialogButton = this.GetDefaultDialogButton();
            if (defaultDialogButton == null)
            {
                ThemedWindowDialogButton local1 = defaultDialogButton;
            }
            else
            {
                defaultDialogButton.Focus();
            }
        }

        private void OnMessageBoxKeyGestureCopyPressed(object sender, ExecutedRoutedEventArgs e)
        {
            Clipboard.SetText(this.GetTextForClipboard());
        }

        private void SubscribeEvents()
        {
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.Initialized += new EventHandler(this.OnInitialized);
        }

        private void UnsubscribeEvents()
        {
            base.Loaded -= new RoutedEventHandler(this.OnLoaded);
            base.Initialized -= new EventHandler(this.OnInitialized);
        }

        protected internal ThemedWindowDialogButton YesButton
        {
            get
            {
                ThemedWindowDialogButton yesButton = this.yesButton;
                if (this.yesButton == null)
                {
                    ThemedWindowDialogButton local1 = this.yesButton;
                    yesButton = this.yesButton = this.GetDialogButton(MessageBoxResult.Yes);
                }
                return yesButton;
            }
        }

        protected internal ThemedWindowDialogButton NoButton
        {
            get
            {
                ThemedWindowDialogButton noButton = this.noButton;
                if (this.noButton == null)
                {
                    ThemedWindowDialogButton local1 = this.noButton;
                    noButton = this.noButton = this.GetDialogButton(MessageBoxResult.No);
                }
                return noButton;
            }
        }

        public string Text
        {
            get => 
                (string) base.GetValue(TextProperty);
            set => 
                base.SetValue(TextProperty, value);
        }

        public UIElement MessageContent
        {
            get => 
                (UIElement) base.GetValue(MessageContentProperty);
            set => 
                base.SetValue(MessageContentProperty, value);
        }

        public ImageSource Image
        {
            get => 
                (ImageSource) base.GetValue(ImageProperty);
            set => 
                base.SetValue(ImageProperty, value);
        }

        public System.Windows.TextAlignment TextAlignment
        {
            get => 
                (System.Windows.TextAlignment) base.GetValue(TextAlignmentProperty);
            set => 
                base.SetValue(TextAlignmentProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThemedMessageBoxWindow.<>c <>9 = new ThemedMessageBoxWindow.<>c();
            public static Func<ThemedWindowDialogButton, bool> <>9__12_0;
            public static Func<string, UICommand, string> <>9__33_0;

            internal bool <GetDefaultDialogButton>b__12_0(ThemedWindowDialogButton x) => 
                x.IsDefault;

            internal string <GetDialogButtonsNamesString>b__33_0(string current, UICommand button) => 
                $"{current}{button.Caption}	";
        }
    }
}

