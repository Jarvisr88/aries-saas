namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Media;

    public static class ViewLocatorExtensions
    {
        internal static object CreateFallbackView(string errorText)
        {
            FallbackView view1 = new FallbackView();
            view1.Text = errorText;
            return view1;
        }

        internal static DataTemplate CreateFallbackViewTemplate(string errorText)
        {
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(FallbackView));
            factory.SetValue(FallbackView.TextProperty, errorText);
            DataTemplate template1 = new DataTemplate();
            template1.VisualTree = factory;
            DataTemplate template = template1;
            template.Seal();
            return template;
        }

        public static DataTemplate CreateViewTemplate(Type viewType)
        {
            if (viewType == null)
            {
                throw new ArgumentNullException("viewType");
            }
            DataTemplate template = null;
            if ((!typeof(FrameworkElement).IsAssignableFrom(viewType) && !typeof(FrameworkContentElement).IsAssignableFrom(viewType)) || viewType.IsNested)
            {
                template = CreateFallbackViewTemplate(GetErrorMessage_CannotCreateDataTemplateFromViewType(viewType.Name));
            }
            else
            {
                template = CreateViewTemplateCore(viewType);
                template.Seal();
            }
            return template;
        }

        public static DataTemplate CreateViewTemplate(this IViewLocator viewLocator, string viewName)
        {
            Verify(viewLocator);
            Type viewType = viewLocator.ResolveViewType(viewName);
            return ((viewType != null) ? viewLocator.CreateViewTemplate(viewType) : CreateFallbackViewTemplate(GetErrorMessage_CannotResolveViewType(viewName)));
        }

        public static DataTemplate CreateViewTemplate(this IViewLocator viewLocator, Type viewType)
        {
            Verify(viewLocator);
            return CreateViewTemplate(viewType);
        }

        private static DataTemplate CreateViewTemplateCore(Type viewType)
        {
            ParserContext parserContext = new ParserContext {
                XmlnsDictionary = { 
                    { 
                        "",
                        "http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    },
                    { 
                        "x",
                        "http://schemas.microsoft.com/winfx/2006/xaml"
                    },
                    { 
                        "v",
                        $"clr-namespace:{viewType.Namespace};assembly={AssemblyHelper.GetPartialName(viewType.Assembly)}"
                    }
                }
            };
            return (DataTemplate) XamlReader.Parse($"<DataTemplate><v:{viewType.Name}/></DataTemplate>", parserContext);
        }

        internal static string GetErrorMessage_CannotCreateDataTemplateFromViewType(string name) => 
            !ViewModelBase.IsInDesignMode ? $"Cannot create DataTemplate from the "{name}" view type." : $"[{name}]";

        internal static string GetErrorMessage_CannotResolveViewType(string name) => 
            !string.IsNullOrEmpty(name) ? (!ViewModelBase.IsInDesignMode ? $""{name}" type not found." : $"[{name}]") : "ViewType is not specified.";

        private static void Verify(IViewLocator viewLocator)
        {
            if (viewLocator == null)
            {
                throw new ArgumentNullException("viewLocator");
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public class FallbackView : Panel
        {
            public static readonly DependencyProperty TextProperty;
            private TextBlock tb = new TextBlock();

            static FallbackView()
            {
                TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ViewLocatorExtensions.FallbackView), new PropertyMetadata(null, (d, e) => ((ViewLocatorExtensions.FallbackView) d).OnTextChanged()));
            }

            public FallbackView()
            {
                if (ViewModelBase.IsInDesignMode)
                {
                    this.tb.FontSize = 25.0;
                    this.tb.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    this.tb.FontSize = 18.0;
                    this.tb.Foreground = new SolidColorBrush(Colors.Gray);
                }
                base.HorizontalAlignment = HorizontalAlignment.Center;
                base.VerticalAlignment = VerticalAlignment.Center;
                base.Children.Add(this.tb);
            }

            protected override Size ArrangeOverride(Size finalSize)
            {
                Point location = new Point();
                this.tb.Arrange(new Rect(location, finalSize));
                return finalSize;
            }

            protected override Size MeasureOverride(Size availableSize)
            {
                this.tb.Measure(availableSize);
                return this.tb.DesiredSize;
            }

            private void OnTextChanged()
            {
                this.tb.Text = this.Text;
            }

            public string Text
            {
                get => 
                    (string) base.GetValue(TextProperty);
                set => 
                    base.SetValue(TextProperty, value);
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly ViewLocatorExtensions.FallbackView.<>c <>9 = new ViewLocatorExtensions.FallbackView.<>c();

                internal void <.cctor>b__9_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
                {
                    ((ViewLocatorExtensions.FallbackView) d).OnTextChanged();
                }
            }
        }
    }
}

