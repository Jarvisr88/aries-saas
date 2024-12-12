namespace DevExpress.Xpf.Core
{
    using DevExpress.Data.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Markup;
    using System.Windows.Media;

    [DXToolboxBrowsable(false)]
    public class DocumentPresenter : Control
    {
        private const string LocalNamespaceXaml = " xmlns:documentpresenterns=\"clr-namespace:DevExpress.Xpf.Core;assembly=DevExpress.Xpf.Core.v19.2\" ";
        private const string DefaultTemplateXaml = "<ResourceDictionary xmlns:documentpresenterns=\"clr-namespace:DevExpress.Xpf.Core;assembly=DevExpress.Xpf.Core.v19.2\" \n    xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\n    xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">\n    <ControlTemplate x:Key=\"DefaultTemplate\" TargetType=\"documentpresenterns:DocumentPresenter\">\n        <ContentPresenter x:Name=\"DocumentPresenter\" MinWidth=\"16\" />\n    </ControlTemplate>\n</ResourceDictionary>";
        private static ControlTemplate defaultTemplate;
        public static readonly DependencyProperty DocumentProperty;
        public static readonly DependencyProperty NavigateUriProperty;
        private ContentPresenter documentPresenter;

        public static event EventHandler<CancelEventArgs> HyperlinkClick;

        static DocumentPresenter()
        {
            Type ownerType = typeof(DocumentPresenter);
            DocumentProperty = DependencyProperty.Register("Document", typeof(string), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(DocumentPresenter.RaiseDocumentChanged)));
            NavigateUriProperty = DependencyProperty.RegisterAttached("NavigateUri", typeof(string), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(DocumentPresenter.RaiseNavigateUriChanged)));
            Control.ForegroundProperty.OverrideMetadata(typeof(DocumentPresenter), new FrameworkPropertyMetadata((d, e) => ((DocumentPresenter) d).UpdateDocument()));
            Control.BackgroundProperty.OverrideMetadata(typeof(DocumentPresenter), new FrameworkPropertyMetadata((d, e) => ((DocumentPresenter) d).UpdateDocument()));
        }

        public DocumentPresenter()
        {
            base.Template = DefaultTemplate;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.documentPresenter != null)
            {
                RectangleGeometry geometry1 = new RectangleGeometry();
                geometry1.Rect = new Rect(-1.0, 0.0, finalSize.Width + 2.0, finalSize.Height + 2.0);
                this.documentPresenter.Clip = geometry1;
            }
            return base.ArrangeOverride(finalSize);
        }

        public static string GetNavigateUri(Hyperlink hyperlink) => 
            (string) hyperlink.GetValue(NavigateUriProperty);

        private string GetParsedDocument()
        {
            StringBuilder builder = new StringBuilder(this.Document);
            builder.Replace(" NavigateUri=", " documentpresenterns:DocumentPresenter.NavigateUri=");
            builder.Replace('[', '<');
            builder.Replace(']', '>');
            builder.Replace("<<", "[");
            builder.Replace(">>", "]");
            StringBuilder builder2 = new StringBuilder();
            while (true)
            {
                int index = builder.ToString().IndexOf("+xmlns", StringComparison.Ordinal);
                if (index >= 0)
                {
                    int num2 = builder.ToString().IndexOf('|', index);
                    if (num2 >= 0)
                    {
                        builder2.Append(' ');
                        builder2.Append(builder.ToString().Substring(index + 1, num2 - (index + 1)));
                        builder.Remove(index, num2 + 1);
                        continue;
                    }
                }
                string solidColorBrush = this.GetSolidColorBrush(base.Foreground as SolidColorBrush);
                solidColorBrush = (solidColorBrush == null) ? string.Empty : $"Foreground="{solidColorBrush}"";
                string str2 = this.GetSolidColorBrush(base.Background as SolidColorBrush);
                str2 = (str2 == null) ? string.Empty : $"Background="{str2}"";
                string str3 = "Margin=\"-6,-1,-6,-1\"";
                string str4 = $"Cursor="Arrow" IsReadOnly="True" HorizontalScrollBarVisibility="Disabled" BorderThickness="0" BorderBrush="Transparent" {solidColorBrush} {str2} {builder2.ToString()} {str3}" + " IsDocumentEnabled=\"True\"";
                if (!base.Focusable)
                {
                    str4 = str4 + " Focusable=\"False\"";
                }
                if (!FocusHelper2.GetFocusable(this))
                {
                    str4 = str4 + " documentpresenterns:FocusHelper2.Focusable=\"False\"";
                }
                builder.Insert(0, "<FlowDocument>");
                builder.Insert(0, $"<RichTextBox xmlns:documentpresenterns="clr-namespace:DevExpress.Xpf.Core;assembly=DevExpress.Xpf.Core.v19.2" xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation" xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" {str4}>");
                builder.Append("</FlowDocument>");
                builder.Append("</RichTextBox>");
                return builder.ToString();
            }
        }

        private string GetSolidColorBrush(SolidColorBrush brush) => 
            brush?.Color.ToString();

        protected internal void InsertLineBreaks(StringBuilder sb)
        {
            if (sb.ToString().Length != 0)
            {
                List<int> list = new List<int>();
                string str = "</PARAGRAPH>";
                string str2 = "<LineBreak/>";
                string str3 = sb.ToString().ToUpperInvariant();
                int item = -1;
                while (true)
                {
                    item = str3.IndexOf(str, item + 1, StringComparison.Ordinal);
                    if (item < 0)
                    {
                        int num2 = list.Count - 1;
                        while (--num2 >= 0)
                        {
                            sb.Insert(list[num2], str2);
                        }
                        return;
                    }
                    list.Add(item);
                }
            }
        }

        private static object LoadXaml(string xaml)
        {
            try
            {
                return XamlReader.Parse(xaml);
            }
            catch
            {
                return null;
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (double.IsInfinity(constraint.Width))
            {
                constraint = new Size();
            }
            return base.MeasureOverride(constraint);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.documentPresenter = (ContentPresenter) base.GetTemplateChild("DocumentPresenter");
            this.UpdateDocument();
        }

        private void OnDocumentChanged(DependencyPropertyChangedEventArgs e)
        {
            this.UpdateDocument();
        }

        private static void OnHyperlinkClick(object sender, RoutedEventArgs e)
        {
            CancelEventArgs args = new CancelEventArgs();
            if (HyperlinkClick != null)
            {
                HyperlinkClick(sender, args);
            }
            if (!args.Cancel)
            {
                OpenLink(GetNavigateUri((Hyperlink) sender));
            }
        }

        public static void OpenLink(string link)
        {
            link = PatchLink(link);
            OpenPath(link);
        }

        public static void OpenPath(string link)
        {
            try
            {
                SafeProcess.Start(link, string.Empty, null);
            }
            catch
            {
            }
        }

        private static string PatchLink(string link)
        {
            link = link.Trim();
            string[] source = new string[] { "http://", "https://", "mailto:" };
            if (!source.Any<string>(x => link.StartsWith(x, StringComparison.OrdinalIgnoreCase)))
            {
                link = source[0] + link;
            }
            return link;
        }

        private static void RaiseDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DocumentPresenter) d).OnDocumentChanged(e);
        }

        private static void RaiseNavigateUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Hyperlink) d).Click += new RoutedEventHandler(DocumentPresenter.OnHyperlinkClick);
        }

        public static int RunProgram(string program, string args, bool waitOnReturn)
        {
            try
            {
                Process process = SafeProcess.Start(program, args, null);
                if (waitOnReturn && (process != null))
                {
                    process.WaitForExit();
                    return process.ExitCode;
                }
            }
            catch
            {
            }
            return 0;
        }

        public static void SetNavigateUri(Hyperlink hyperlink, string v)
        {
            hyperlink.SetValue(NavigateUriProperty, v);
        }

        private void UpdateDocument()
        {
            if (this.documentPresenter != null)
            {
                if (this.Document == null)
                {
                    this.documentPresenter.Content = null;
                }
                else
                {
                    this.documentPresenter.Content = LoadXaml(this.GetParsedDocument());
                }
            }
        }

        private static ControlTemplate DefaultTemplate
        {
            get
            {
                defaultTemplate ??= ((ControlTemplate) ((ResourceDictionary) LoadXaml("<ResourceDictionary xmlns:documentpresenterns=\"clr-namespace:DevExpress.Xpf.Core;assembly=DevExpress.Xpf.Core.v19.2\" \n    xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\n    xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">\n    <ControlTemplate x:Key=\"DefaultTemplate\" TargetType=\"documentpresenterns:DocumentPresenter\">\n        <ContentPresenter x:Name=\"DocumentPresenter\" MinWidth=\"16\" />\n    </ControlTemplate>\n</ResourceDictionary>"))["DefaultTemplate"]);
                return defaultTemplate;
            }
        }

        public string Document
        {
            get => 
                (string) base.GetValue(DocumentProperty);
            set => 
                base.SetValue(DocumentProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentPresenter.<>c <>9 = new DocumentPresenter.<>c();

            internal void <.cctor>b__12_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentPresenter) d).UpdateDocument();
            }

            internal void <.cctor>b__12_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentPresenter) d).UpdateDocument();
            }
        }
    }
}

