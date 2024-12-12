namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class AnimationElement : DependencyObject
    {
        public static readonly DependencyProperty BackgroundProperty;
        public static readonly DependencyProperty ForegroundProperty;
        public static readonly DependencyProperty ValuePositionProperty;
        public static readonly DependencyProperty IconProperty;
        public static readonly DependencyProperty IconOpacityProperty;
        public static readonly DependencyProperty FontSizeProperty;
        public static readonly DependencyProperty FontStyleProperty;
        public static readonly DependencyProperty FontFamilyProperty;
        public static readonly DependencyProperty FontStretchProperty;
        public static readonly DependencyProperty FontWeightProperty;

        static AnimationElement()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(AnimationElement), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<AnimationElement> registrator1 = DependencyPropertyRegistrator<AnimationElement>.New().Register<Brush>(System.Linq.Expressions.Expression.Lambda<Func<AnimationElement, Brush>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AnimationElement.get_Background)), parameters), out BackgroundProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(AnimationElement), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<AnimationElement> registrator2 = registrator1.Register<Brush>(System.Linq.Expressions.Expression.Lambda<Func<AnimationElement, Brush>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AnimationElement.get_Foreground)), expressionArray2), out ForegroundProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(AnimationElement), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<AnimationElement> registrator3 = registrator2.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<AnimationElement, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AnimationElement.get_ValuePosition)), expressionArray3), out ValuePositionProperty, 0.0, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(AnimationElement), "d");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<AnimationElement> registrator4 = registrator3.Register<ImageSource>(System.Linq.Expressions.Expression.Lambda<Func<AnimationElement, ImageSource>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AnimationElement.get_Icon)), expressionArray4), out IconProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(AnimationElement), "d");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<AnimationElement> registrator5 = registrator4.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<AnimationElement, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AnimationElement.get_IconOpacity)), expressionArray5), out IconOpacityProperty, 1.0, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(AnimationElement), "d");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<AnimationElement> registrator6 = registrator5.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<AnimationElement, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AnimationElement.get_FontSize)), expressionArray6), out FontSizeProperty, 0.0, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(AnimationElement), "d");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<AnimationElement> registrator7 = registrator6.Register<System.Windows.FontStyle>(System.Linq.Expressions.Expression.Lambda<Func<AnimationElement, System.Windows.FontStyle>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AnimationElement.get_FontStyle)), expressionArray7), out FontStyleProperty, FontStyles.Normal, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(AnimationElement), "d");
            ParameterExpression[] expressionArray8 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<AnimationElement> registrator8 = registrator7.Register<System.Windows.Media.FontFamily>(System.Linq.Expressions.Expression.Lambda<Func<AnimationElement, System.Windows.Media.FontFamily>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AnimationElement.get_FontFamily)), expressionArray8), out FontFamilyProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(AnimationElement), "d");
            ParameterExpression[] expressionArray9 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<AnimationElement> registrator9 = registrator8.Register<System.Windows.FontStretch>(System.Linq.Expressions.Expression.Lambda<Func<AnimationElement, System.Windows.FontStretch>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AnimationElement.get_FontStretch)), expressionArray9), out FontStretchProperty, FontStretches.Normal, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(AnimationElement), "d");
            ParameterExpression[] expressionArray10 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator9.Register<System.Windows.FontWeight>(System.Linq.Expressions.Expression.Lambda<Func<AnimationElement, System.Windows.FontWeight>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AnimationElement.get_FontWeight)), expressionArray10), out FontWeightProperty, FontWeights.Normal, frameworkOptions);
        }

        public AnimationElement()
        {
            this.Mask = AnimationMask.All;
            this.Background = CreateTransparentBrush();
            this.Foreground = CreateTransparentBrush();
        }

        private static Brush CreateTransparentBrush() => 
            new SolidColorBrush(Colors.Transparent);

        public Brush Background
        {
            get => 
                (Brush) base.GetValue(BackgroundProperty);
            set => 
                base.SetValue(BackgroundProperty, value);
        }

        public Brush Foreground
        {
            get => 
                (Brush) base.GetValue(ForegroundProperty);
            set => 
                base.SetValue(ForegroundProperty, value);
        }

        public double ValuePosition
        {
            get => 
                (double) base.GetValue(ValuePositionProperty);
            set => 
                base.SetValue(ValuePositionProperty, value);
        }

        public ImageSource Icon
        {
            get => 
                (ImageSource) base.GetValue(IconProperty);
            set => 
                base.SetValue(IconProperty, value);
        }

        public double IconOpacity
        {
            get => 
                (double) base.GetValue(IconOpacityProperty);
            set => 
                base.SetValue(IconOpacityProperty, value);
        }

        public double FontSize
        {
            get => 
                (double) base.GetValue(FontSizeProperty);
            set => 
                base.SetValue(FontSizeProperty, value);
        }

        public System.Windows.FontStyle FontStyle
        {
            get => 
                (System.Windows.FontStyle) base.GetValue(FontStyleProperty);
            set => 
                base.SetValue(FontStyleProperty, value);
        }

        public System.Windows.Media.FontFamily FontFamily
        {
            get => 
                (System.Windows.Media.FontFamily) base.GetValue(FontFamilyProperty);
            set => 
                base.SetValue(FontFamilyProperty, value);
        }

        public System.Windows.FontStretch FontStretch
        {
            get => 
                (System.Windows.FontStretch) base.GetValue(FontStretchProperty);
            set => 
                base.SetValue(FontStretchProperty, value);
        }

        public System.Windows.FontWeight FontWeight
        {
            get => 
                (System.Windows.FontWeight) base.GetValue(FontWeightProperty);
            set => 
                base.SetValue(FontWeightProperty, value);
        }

        public AnimationMask Mask { get; set; }
    }
}

