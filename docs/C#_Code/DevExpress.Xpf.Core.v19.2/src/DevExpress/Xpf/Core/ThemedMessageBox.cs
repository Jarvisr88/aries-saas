namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public static class ThemedMessageBox
    {
        public static ImageSource GetMessageBoxIcon(this MessageBoxImage image, bool UsePngImages = false) => 
            (image != MessageBoxImage.None) ? (UsePngImages ? GetPngImageSource(image) : GetSvgImageSource(image)) : null;

        private static string GetMessageBoxIconUri(MessageBoxImage image, string fileExtension) => 
            "pack://application:,,,/DevExpress.Xpf.Core.v19.2;component/Core/Window/Icons/" + image.ToString() + "_48x48" + fileExtension;

        private static ImageSource GetPngImageSource(MessageBoxImage image) => 
            (ImageSource) new ImageSourceConverter().ConvertFromString(GetMessageBoxIconUri(image, ".png"));

        private static ImageSource GetSvgImageSource(MessageBoxImage image)
        {
            SvgImageSourceExtension extension1 = new SvgImageSourceExtension();
            extension1.Uri = new Uri(GetMessageBoxIconUri(image, ".svg"));
            return (ImageSource) extension1.ProvideValue(null);
        }

        private static ThemedMessageBoxWindow InitializeThemedMessageBoxWindow(string text, Window owner, string title, ImageSource image, MessageBoxOptions options, WindowStartupLocation windowStartupLocation, WindowTitleAlignment titleAlignment, bool? showActivated, UIElement messageContent)
        {
            ThemedMessageBoxWindow window1 = new ThemedMessageBoxWindow();
            window1.Text = text;
            window1.WindowStartupLocation = windowStartupLocation;
            window1.Image = image;
            window1.MessageContent = messageContent;
            window1.TextAlignment = (options == MessageBoxOptions.RightAlign) ? TextAlignment.Right : TextAlignment.Left;
            ThemedMessageBoxWindow local1 = window1;
            local1.TitleAlignment = titleAlignment;
            ThemedMessageBoxWindow window = local1;
            window.InitializeThemedWindowFromOwner(owner, title);
            window.FlowDirection = (options == MessageBoxOptions.RtlReading) ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            if (showActivated != null)
            {
                window.ShowActivated = showActivated.Value;
            }
            return window;
        }

        public static MessageBoxResult Show(string text)
        {
            MessageBoxResult? defaultButton = null;
            bool? showActivated = null;
            return ShowCore(text, null, null, MessageBoxButton.OKCancel, defaultButton, null, MessageBoxOptions.None, WindowStartupLocation.CenterOwner, WindowTitleAlignment.Left, showActivated, null);
        }

        public static MessageBoxResult Show(string title, string text)
        {
            string str = title;
            MessageBoxResult? defaultButton = null;
            bool? showActivated = null;
            return ShowCore(text, null, str, MessageBoxButton.OKCancel, defaultButton, null, MessageBoxOptions.None, WindowStartupLocation.CenterOwner, WindowTitleAlignment.Left, showActivated, null);
        }

        public static MessageBoxResult Show(string title, MessageBoxButton messageBoxButtons)
        {
            string str = title;
            MessageBoxResult? defaultButton = null;
            bool? showActivated = null;
            return ShowCore(string.Empty, null, str, messageBoxButtons, defaultButton, null, MessageBoxOptions.None, WindowStartupLocation.CenterOwner, WindowTitleAlignment.Left, showActivated, null);
        }

        public static MessageBoxResult Show(string title, string text, ImageSource image)
        {
            string str = title;
            MessageBoxResult? defaultButton = null;
            bool? showActivated = null;
            return ShowCore(text, null, str, MessageBoxButton.OKCancel, defaultButton, image, MessageBoxOptions.None, WindowStartupLocation.CenterOwner, WindowTitleAlignment.Left, showActivated, null);
        }

        public static MessageBoxResult Show(string title, string text, MessageBoxButton messageBoxButtons)
        {
            string str = title;
            MessageBoxResult? defaultButton = null;
            bool? showActivated = null;
            return ShowCore(text, null, str, messageBoxButtons, defaultButton, null, MessageBoxOptions.None, WindowStartupLocation.CenterOwner, WindowTitleAlignment.Left, showActivated, null);
        }

        public static MessageBoxResult Show(string title, string text, MessageBoxImage image)
        {
            string str = title;
            MessageBoxResult? defaultButton = null;
            bool? showActivated = null;
            return ShowCore(text, null, str, MessageBoxButton.OKCancel, defaultButton, image.GetMessageBoxIcon(false), MessageBoxOptions.None, WindowStartupLocation.CenterOwner, WindowTitleAlignment.Left, showActivated, null);
        }

        public static MessageBoxResult Show(string title, string text, MessageBoxButton messageBoxButtons, ImageSource image)
        {
            string str = title;
            MessageBoxResult? defaultButton = null;
            bool? showActivated = null;
            return ShowCore(text, null, str, messageBoxButtons, defaultButton, image, MessageBoxOptions.None, WindowStartupLocation.CenterOwner, WindowTitleAlignment.Left, showActivated, null);
        }

        public static MessageBoxResult Show(string title, string text, MessageBoxButton messageBoxButtons, MessageBoxImage image)
        {
            string str = title;
            MessageBoxResult? defaultButton = null;
            bool? showActivated = null;
            return ShowCore(text, null, str, messageBoxButtons, defaultButton, image.GetMessageBoxIcon(false), MessageBoxOptions.None, WindowStartupLocation.CenterOwner, WindowTitleAlignment.Left, showActivated, null);
        }

        public static UICommand Show(IEnumerable<UICommand> messageBoxButtons, Window owner = null, string title = null, UIElement messageContent = null, ImageSource image = null, MessageBoxOptions options = 0, WindowStartupLocation windowStartupLocation = 2, WindowTitleAlignment titleAlignment = 0, bool? showActivated = new bool?())
        {
            Window window = owner;
            UIElement element = messageContent;
            return ShowCore(string.Empty, messageBoxButtons, window, title, image, options, windowStartupLocation, titleAlignment, showActivated, element);
        }

        public static UICommand Show(string text, IEnumerable<UICommand> messageBoxButtons, Window owner = null, string title = null, ImageSource image = null, MessageBoxOptions options = 0, WindowStartupLocation windowStartupLocation = 2, WindowTitleAlignment titleAlignment = 0, bool? showActivated = new bool?())
        {
            Window window = owner;
            return ShowCore(text, messageBoxButtons, window, title, image, options, windowStartupLocation, titleAlignment, showActivated, null);
        }

        public static UICommand Show(IEnumerable<UICommand> messageBoxButtons, Window owner = null, string title = null, UIElement messageContent = null, MessageBoxImage icon = 0, bool usePngImages = false, MessageBoxOptions options = 0, WindowStartupLocation windowStartupLocation = 2, WindowTitleAlignment titleAlignment = 0, bool? showActivated = new bool?())
        {
            Window window = owner;
            UIElement element = messageContent;
            return ShowCore(string.Empty, messageBoxButtons, window, title, icon.GetMessageBoxIcon(usePngImages), options, windowStartupLocation, titleAlignment, showActivated, element);
        }

        public static UICommand Show(string text, IEnumerable<UICommand> messageBoxButtons, Window owner = null, string title = null, MessageBoxImage icon = 0, bool usePngImages = false, MessageBoxOptions options = 0, WindowStartupLocation windowStartupLocation = 2, WindowTitleAlignment titleAlignment = 0, bool? showActivated = new bool?())
        {
            Window window = owner;
            return ShowCore(text, messageBoxButtons, window, title, icon.GetMessageBoxIcon(usePngImages), options, windowStartupLocation, titleAlignment, showActivated, null);
        }

        public static MessageBoxResult Show(Window owner = null, string title = null, string text = null, MessageBoxButton messageBoxButtons = 1, MessageBoxResult? defaultButton = new MessageBoxResult?(), ImageSource image = null, MessageBoxOptions options = 0, WindowStartupLocation windowStartupLocation = 2, WindowTitleAlignment titleAlignment = 0, bool? showActivated = new bool?()) => 
            ShowCore(text, owner, title, messageBoxButtons, defaultButton, image, options, windowStartupLocation, titleAlignment, showActivated, null);

        public static MessageBoxResult Show(Window owner = null, string title = null, UIElement messageContent = null, MessageBoxButton messageBoxButtons = 1, MessageBoxResult? defaultButton = new MessageBoxResult?(), ImageSource image = null, MessageBoxOptions options = 0, WindowStartupLocation windowStartupLocation = 2, WindowTitleAlignment titleAlignment = 0, bool? showActivated = new bool?())
        {
            ImageSource source = image;
            UIElement element = messageContent;
            return ShowCore(string.Empty, owner, title, messageBoxButtons, defaultButton, source, options, windowStartupLocation, titleAlignment, showActivated, element);
        }

        public static MessageBoxResult Show(Window owner = null, string title = null, string text = null, MessageBoxButton messageBoxButtons = 1, MessageBoxResult? defaultButton = new MessageBoxResult?(), MessageBoxImage icon = 0, bool usePngImages = false, MessageBoxOptions options = 0, WindowStartupLocation windowStartupLocation = 2, WindowTitleAlignment titleAlignment = 0, bool? showActivated = new bool?()) => 
            ShowCore(text, owner, title, messageBoxButtons, defaultButton, icon.GetMessageBoxIcon(usePngImages), options, windowStartupLocation, titleAlignment, showActivated, null);

        public static MessageBoxResult Show(Window owner = null, string title = null, UIElement messageContent = null, MessageBoxButton messageBoxButtons = 1, MessageBoxResult? defaultButton = new MessageBoxResult?(), MessageBoxImage icon = 0, bool usePngImages = false, MessageBoxOptions options = 0, WindowStartupLocation windowStartupLocation = 2, WindowTitleAlignment titleAlignment = 0, bool? showActivated = new bool?())
        {
            UIElement element = messageContent;
            return ShowCore(string.Empty, owner, title, messageBoxButtons, defaultButton, icon.GetMessageBoxIcon(usePngImages), options, windowStartupLocation, titleAlignment, showActivated, element);
        }

        private static UICommand ShowCore(string text, IEnumerable<UICommand> messageBoxButtons, Window owner = null, string title = null, ImageSource image = null, MessageBoxOptions options = 0, WindowStartupLocation windowStartupLocation = 2, WindowTitleAlignment titleAlignment = 0, bool? showActivated = new bool?(), UIElement messageContent = null)
        {
            ValidStrings(ref title, ref text);
            return InitializeThemedMessageBoxWindow(text, owner, title, image, options, windowStartupLocation, titleAlignment, showActivated, messageContent).ShowDialog(messageBoxButtons);
        }

        private static MessageBoxResult ShowCore(string text, Window owner = null, string title = null, MessageBoxButton messageBoxButtons = 1, MessageBoxResult? defaultButton = new MessageBoxResult?(), ImageSource image = null, MessageBoxOptions options = 0, WindowStartupLocation windowStartupLocation = 2, WindowTitleAlignment titleAlignment = 0, bool? showActivated = new bool?(), UIElement messageContent = null)
        {
            ValidStrings(ref title, ref text);
            return InitializeThemedMessageBoxWindow(text, owner, title, image, options, windowStartupLocation, titleAlignment, showActivated, messageContent).ShowDialog(messageBoxButtons, defaultButton);
        }

        private static void ValidStrings(ref string title, ref string text)
        {
            title ??= string.Empty;
            text ??= string.Empty;
        }
    }
}

