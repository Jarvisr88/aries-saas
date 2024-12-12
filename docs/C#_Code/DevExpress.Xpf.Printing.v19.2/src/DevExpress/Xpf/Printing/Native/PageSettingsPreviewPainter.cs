namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.Drawing.Printing;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class PageSettingsPreviewPainter : Behavior<Grid>
    {
        public static readonly DependencyProperty PaperKindProperty;
        public static readonly DependencyProperty LandscapeProperty;
        public static readonly DependencyProperty PaperWidthProperty;
        public static readonly DependencyProperty PaperHeightProperty;
        public static readonly DependencyProperty LeftMarginProperty;
        public static readonly DependencyProperty RightMarginProperty;
        public static readonly DependencyProperty TopMarginProperty;
        public static readonly DependencyProperty BottomMarginProperty;

        static PageSettingsPreviewPainter()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PageSettingsPreviewPainter), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<PageSettingsPreviewPainter> registrator1 = DependencyPropertyRegistrator<PageSettingsPreviewPainter>.New().Register<System.Drawing.Printing.PaperKind>(System.Linq.Expressions.Expression.Lambda<Func<PageSettingsPreviewPainter, System.Drawing.Printing.PaperKind>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageSettingsPreviewPainter.get_PaperKind)), parameters), out PaperKindProperty, System.Drawing.Printing.PaperKind.A4, d => d.UpdatePreview(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PageSettingsPreviewPainter), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<PageSettingsPreviewPainter> registrator2 = registrator1.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<PageSettingsPreviewPainter, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageSettingsPreviewPainter.get_Landscape)), expressionArray2), out LandscapeProperty, false, d => d.UpdatePreview(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PageSettingsPreviewPainter), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<PageSettingsPreviewPainter> registrator3 = registrator2.Register<float>(System.Linq.Expressions.Expression.Lambda<Func<PageSettingsPreviewPainter, float>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageSettingsPreviewPainter.get_PaperWidth)), expressionArray3), out PaperWidthProperty, 0f, d => d.UpdatePreview(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PageSettingsPreviewPainter), "d");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<PageSettingsPreviewPainter> registrator4 = registrator3.Register<float>(System.Linq.Expressions.Expression.Lambda<Func<PageSettingsPreviewPainter, float>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageSettingsPreviewPainter.get_PaperHeight)), expressionArray4), out PaperHeightProperty, 0f, d => d.UpdatePreview(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PageSettingsPreviewPainter), "d");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<PageSettingsPreviewPainter> registrator5 = registrator4.Register<float>(System.Linq.Expressions.Expression.Lambda<Func<PageSettingsPreviewPainter, float>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageSettingsPreviewPainter.get_LeftMargin)), expressionArray5), out LeftMarginProperty, 0f, d => d.UpdatePreview(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PageSettingsPreviewPainter), "d");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<PageSettingsPreviewPainter> registrator6 = registrator5.Register<float>(System.Linq.Expressions.Expression.Lambda<Func<PageSettingsPreviewPainter, float>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageSettingsPreviewPainter.get_RightMargin)), expressionArray6), out RightMarginProperty, 0f, d => d.UpdatePreview(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PageSettingsPreviewPainter), "d");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<PageSettingsPreviewPainter> registrator7 = registrator6.Register<float>(System.Linq.Expressions.Expression.Lambda<Func<PageSettingsPreviewPainter, float>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageSettingsPreviewPainter.get_TopMargin)), expressionArray7), out TopMarginProperty, 0f, d => d.UpdatePreview(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PageSettingsPreviewPainter), "d");
            ParameterExpression[] expressionArray8 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator7.Register<float>(System.Linq.Expressions.Expression.Lambda<Func<PageSettingsPreviewPainter, float>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageSettingsPreviewPainter.get_BottomMargin)), expressionArray8), out BottomMarginProperty, 0f, d => d.UpdatePreview(), frameworkOptions);
        }

        private static double CalculatePreviewScale(Size areaSize, Size pageSize) => 
            Math.Min((double) (areaSize.Height / pageSize.Height), (double) (areaSize.Width / pageSize.Width));

        protected override void OnAttached()
        {
            base.OnAttached();
            base.AssociatedObject.Loaded += new RoutedEventHandler(this.OnLoaded);
        }

        protected override void OnDetaching()
        {
            base.AssociatedObject.Loaded -= new RoutedEventHandler(this.OnLoaded);
            base.OnDetaching();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdatePreview();
        }

        private void UpdatePreview()
        {
            if (base.AssociatedObject.IsLoaded && (!WrongValue((double) this.PaperWidth) && !WrongValue((double) this.PaperHeight)))
            {
                double num = CalculatePreviewScale(base.AssociatedObject.RenderSize, new Size((double) this.PaperWidth, (double) this.PaperHeight));
                Size size2 = new Size(this.PaperWidth * num, this.PaperHeight * num);
                Thickness margins = new Thickness(this.LeftMargin * num, this.TopMargin * num, this.RightMargin * num, this.BottomMargin * num);
                if (!WrongMargins(margins))
                {
                    double num3;
                    this.VerticalMargin.Width = num3 = size2.Width;
                    this.Page.Width = this.HorizontalMargin.Width = num3;
                    this.VerticalMargin.Height = num3 = size2.Height;
                    this.Page.Height = this.HorizontalMargin.Height = num3;
                    this.HorizontalMargin.Padding = new Thickness(margins.Left, 0.0, margins.Right, 0.0);
                    this.VerticalMargin.Padding = new Thickness(0.0, margins.Top, 0.0, margins.Bottom);
                }
            }
        }

        private static bool WrongMargins(Thickness margins) => 
            WrongValue(margins.Left) || (WrongValue(margins.Right) || (WrongValue(margins.Top) || WrongValue(margins.Bottom)));

        private static bool WrongValue(double value) => 
            double.IsInfinity(value) || (double.IsNaN(value) || (value < 0.0));

        private Border Page =>
            base.AssociatedObject.Children[0] as Border;

        private Border HorizontalMargin =>
            base.AssociatedObject.Children[1] as Border;

        private Border VerticalMargin =>
            base.AssociatedObject.Children[2] as Border;

        public System.Drawing.Printing.PaperKind PaperKind
        {
            get => 
                (System.Drawing.Printing.PaperKind) base.GetValue(PaperKindProperty);
            set => 
                base.SetValue(PaperKindProperty, value);
        }

        public float PaperWidth
        {
            get => 
                (float) base.GetValue(PaperWidthProperty);
            set => 
                base.SetValue(PaperWidthProperty, value);
        }

        public float PaperHeight
        {
            get => 
                (float) base.GetValue(PaperHeightProperty);
            set => 
                base.SetValue(PaperHeightProperty, value);
        }

        public float LeftMargin
        {
            get => 
                (float) base.GetValue(LeftMarginProperty);
            set => 
                base.SetValue(LeftMarginProperty, value);
        }

        public float RightMargin
        {
            get => 
                (float) base.GetValue(RightMarginProperty);
            set => 
                base.SetValue(RightMarginProperty, value);
        }

        public float TopMargin
        {
            get => 
                (float) base.GetValue(TopMarginProperty);
            set => 
                base.SetValue(TopMarginProperty, value);
        }

        public float BottomMargin
        {
            get => 
                (float) base.GetValue(BottomMarginProperty);
            set => 
                base.SetValue(BottomMarginProperty, value);
        }

        public bool Landscape
        {
            get => 
                (bool) base.GetValue(LandscapeProperty);
            set => 
                base.SetValue(LandscapeProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PageSettingsPreviewPainter.<>c <>9 = new PageSettingsPreviewPainter.<>c();

            internal void <.cctor>b__8_0(PageSettingsPreviewPainter d)
            {
                d.UpdatePreview();
            }

            internal void <.cctor>b__8_1(PageSettingsPreviewPainter d)
            {
                d.UpdatePreview();
            }

            internal void <.cctor>b__8_2(PageSettingsPreviewPainter d)
            {
                d.UpdatePreview();
            }

            internal void <.cctor>b__8_3(PageSettingsPreviewPainter d)
            {
                d.UpdatePreview();
            }

            internal void <.cctor>b__8_4(PageSettingsPreviewPainter d)
            {
                d.UpdatePreview();
            }

            internal void <.cctor>b__8_5(PageSettingsPreviewPainter d)
            {
                d.UpdatePreview();
            }

            internal void <.cctor>b__8_6(PageSettingsPreviewPainter d)
            {
                d.UpdatePreview();
            }

            internal void <.cctor>b__8_7(PageSettingsPreviewPainter d)
            {
                d.UpdatePreview();
            }
        }
    }
}

