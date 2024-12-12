namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    [TypeConverter(typeof(BadgesTypeConverter))]
    public class Badge : Freezable
    {
        public static readonly DependencyProperty HorizontalAlignmentProperty;
        public static readonly DependencyProperty VerticalAlignmentProperty;
        public static readonly DependencyProperty VisibilityProperty;
        public static readonly DependencyProperty OpacityProperty;
        public static readonly DependencyProperty BackgroundProperty;
        public static readonly DependencyProperty BorderBrushProperty;
        public static readonly DependencyProperty BorderThicknessProperty;
        public static readonly DependencyProperty BadgeShapeProperty;
        public static readonly DependencyProperty BadgeKindProperty;
        public static readonly DependencyProperty HorizontalAnchorProperty;
        public static readonly DependencyProperty VerticalAnchorProperty;
        public static readonly DependencyProperty ContentProperty;
        public static readonly DependencyProperty ContentTemplateProperty;
        public static readonly DependencyProperty ContentTemplateSelectorProperty;
        public static readonly DependencyProperty ContentStringFormatProperty;
        public static readonly DependencyProperty ContentFormatProviderProperty;
        public static readonly DependencyProperty TemplateProperty;
        public static readonly DependencyProperty PaddingProperty;
        public static readonly DependencyProperty CornerRadiusProperty;
        public static readonly DependencyProperty FontSizeProperty;
        public static readonly DependencyProperty FontFamilyProperty;
        public static readonly DependencyProperty FontWeightProperty;
        public static readonly DependencyProperty FontStyleProperty;
        public static readonly DependencyProperty FontStretchProperty;
        public static readonly DependencyProperty ForegroundProperty;
        public static readonly DependencyProperty HorizontalContentAlignmentProperty;
        public static readonly DependencyProperty VerticalContentAlignmentProperty;
        public static readonly DependencyProperty AnimationDurationProperty;
        public static readonly DependencyProperty MarginProperty;
        public static readonly DependencyProperty WidthProperty;
        public static readonly DependencyProperty HeightProperty;
        public static readonly DependencyProperty MinWidthProperty;
        public static readonly DependencyProperty MinHeightProperty;
        public static readonly DependencyProperty MaxWidthProperty;
        public static readonly DependencyProperty MaxHeightProperty;
        public static readonly DependencyProperty BadgeProperty;

        public event CustomBadgePlacementCallback CustomPlacement;

        static Badge()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DependencyObject), "x");
            System.Linq.Expressions.Expression[] arguments = new System.Linq.Expressions.Expression[] { expression };
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator1 = DependencyPropertyRegistrator<Badge>.New().RegisterAttached<DependencyObject, Badge>(System.Linq.Expressions.Expression.Lambda<Func<DependencyObject, Badge>>(System.Linq.Expressions.Expression.Call(null, (MethodInfo) methodof(Badge.GetBadge), arguments), parameters), out BadgeProperty, null, (x, v) => x.CoerceValue(Badges.BadgeAndShowModeProperty), frameworkOptions).Register<System.Windows.HorizontalAlignment>(System.Linq.Expressions.Expression.Lambda<Func<Badge, System.Windows.HorizontalAlignment>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_HorizontalAlignment)), expressionArray3), out HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Right, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator2 = registrator1.Register<System.Windows.VerticalAlignment>(System.Linq.Expressions.Expression.Lambda<Func<Badge, System.Windows.VerticalAlignment>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_VerticalAlignment)), expressionArray4), out VerticalAlignmentProperty, System.Windows.VerticalAlignment.Top, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator3 = registrator2.Register<System.Windows.HorizontalAlignment>(System.Linq.Expressions.Expression.Lambda<Func<Badge, System.Windows.HorizontalAlignment>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_HorizontalContentAlignment)), expressionArray5), out HorizontalContentAlignmentProperty, System.Windows.HorizontalAlignment.Center, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator4 = registrator3.Register<System.Windows.VerticalAlignment>(System.Linq.Expressions.Expression.Lambda<Func<Badge, System.Windows.VerticalAlignment>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_VerticalContentAlignment)), expressionArray6), out VerticalContentAlignmentProperty, System.Windows.VerticalAlignment.Center, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator5 = registrator4.Register<System.Windows.HorizontalAlignment>(System.Linq.Expressions.Expression.Lambda<Func<Badge, System.Windows.HorizontalAlignment>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_HorizontalAnchor)), expressionArray7), out HorizontalAnchorProperty, System.Windows.HorizontalAlignment.Center, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray8 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator6 = registrator5.Register<System.Windows.VerticalAlignment>(System.Linq.Expressions.Expression.Lambda<Func<Badge, System.Windows.VerticalAlignment>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_VerticalAnchor)), expressionArray8), out VerticalAnchorProperty, System.Windows.VerticalAlignment.Center, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray9 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator7 = registrator6.Register<Brush>(System.Linq.Expressions.Expression.Lambda<Func<Badge, Brush>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_Background)), expressionArray9), out BackgroundProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray10 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator8 = registrator7.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<Badge, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_Opacity)), expressionArray10), out OpacityProperty, 1.0, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray11 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator9 = registrator8.Register<Brush>(System.Linq.Expressions.Expression.Lambda<Func<Badge, Brush>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_BorderBrush)), expressionArray11), out BorderBrushProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray12 = new ParameterExpression[] { expression };
            Thickness? defaultValue = null;
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator10 = registrator9.Register<Thickness?>(System.Linq.Expressions.Expression.Lambda<Func<Badge, Thickness?>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_BorderThickness)), expressionArray12), out BorderThicknessProperty, defaultValue, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray13 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator11 = registrator10.Register<DevExpress.Xpf.Core.BadgeKind>(System.Linq.Expressions.Expression.Lambda<Func<Badge, DevExpress.Xpf.Core.BadgeKind>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_BadgeKind)), expressionArray13), out BadgeKindProperty, DevExpress.Xpf.Core.BadgeKind.Information, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray14 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator12 = registrator11.Register<object>(System.Linq.Expressions.Expression.Lambda<Func<Badge, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_Content)), expressionArray14), out ContentProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray15 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator13 = registrator12.Register<DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<Badge, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_ContentTemplate)), expressionArray15), out ContentTemplateProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray16 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator14 = registrator13.Register<DataTemplateSelector>(System.Linq.Expressions.Expression.Lambda<Func<Badge, DataTemplateSelector>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_ContentTemplateSelector)), expressionArray16), out ContentTemplateSelectorProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray17 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator15 = registrator14.Register<string>(System.Linq.Expressions.Expression.Lambda<Func<Badge, string>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_ContentStringFormat)), expressionArray17), out ContentStringFormatProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray18 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator16 = registrator15.Register<IFormatProvider>(System.Linq.Expressions.Expression.Lambda<Func<Badge, IFormatProvider>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_ContentFormatProvider)), expressionArray18), out ContentFormatProviderProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray19 = new ParameterExpression[] { expression };
            defaultValue = null;
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator17 = registrator16.Register<Thickness?>(System.Linq.Expressions.Expression.Lambda<Func<Badge, Thickness?>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_Padding)), expressionArray19), out PaddingProperty, defaultValue, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray20 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator18 = registrator17.Register<int>(System.Linq.Expressions.Expression.Lambda<Func<Badge, int>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_AnimationDuration)), expressionArray20), out AnimationDurationProperty, 100, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray21 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator19 = registrator18.Register<System.Windows.Visibility>(System.Linq.Expressions.Expression.Lambda<Func<Badge, System.Windows.Visibility>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_Visibility)), expressionArray21), out VisibilityProperty, System.Windows.Visibility.Visible, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray22 = new ParameterExpression[] { expression };
            DevExpress.Xpf.Core.BadgeShape? nullable3 = null;
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator20 = registrator19.Register<DevExpress.Xpf.Core.BadgeShape?>(System.Linq.Expressions.Expression.Lambda<Func<Badge, DevExpress.Xpf.Core.BadgeShape?>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_BadgeShape)), expressionArray22), out BadgeShapeProperty, nullable3, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray23 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator21 = registrator20.Register<ControlTemplate>(System.Linq.Expressions.Expression.Lambda<Func<Badge, ControlTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_Template)), expressionArray23), out TemplateProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray24 = new ParameterExpression[] { expression };
            System.Windows.CornerRadius? nullable4 = null;
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator22 = registrator21.Register<System.Windows.CornerRadius?>(System.Linq.Expressions.Expression.Lambda<Func<Badge, System.Windows.CornerRadius?>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_CornerRadius)), expressionArray24), out CornerRadiusProperty, nullable4, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray25 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator23 = registrator22.Register<System.Windows.Media.FontFamily>(System.Linq.Expressions.Expression.Lambda<Func<Badge, System.Windows.Media.FontFamily>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_FontFamily)), expressionArray25), out FontFamilyProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray26 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator24 = registrator23.Register<System.Windows.FontWeight>(System.Linq.Expressions.Expression.Lambda<Func<Badge, System.Windows.FontWeight>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_FontWeight)), expressionArray26), out FontWeightProperty, FontWeights.Normal, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray27 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator25 = registrator24.Register<System.Windows.FontStyle>(System.Linq.Expressions.Expression.Lambda<Func<Badge, System.Windows.FontStyle>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_FontStyle)), expressionArray27), out FontStyleProperty, FontStyles.Normal, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray28 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator26 = registrator25.Register<System.Windows.FontStretch>(System.Linq.Expressions.Expression.Lambda<Func<Badge, System.Windows.FontStretch>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_FontStretch)), expressionArray28), out FontStretchProperty, FontStretches.Normal, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray29 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator27 = registrator26.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<Badge, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_FontSize)), expressionArray29), out FontSizeProperty, SystemFonts.MessageFontSize, (Func<Badge, double, double>) ((x, v) => Math.Min(Math.Max(v, 0.001), 35791.0)), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray30 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator28 = registrator27.Register<Brush>(System.Linq.Expressions.Expression.Lambda<Func<Badge, Brush>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_Foreground)), expressionArray30), out ForegroundProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray31 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator29 = registrator28.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<Badge, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_Width)), expressionArray31), out WidthProperty, double.NaN, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray32 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator30 = registrator29.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<Badge, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_Height)), expressionArray32), out HeightProperty, double.NaN, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray33 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator31 = registrator30.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<Badge, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_MinWidth)), expressionArray33), out MinWidthProperty, 0.0, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray34 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator32 = registrator31.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<Badge, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_MinHeight)), expressionArray34), out MinHeightProperty, 0.0, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray35 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator33 = registrator32.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<Badge, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_MaxWidth)), expressionArray35), out MaxWidthProperty, double.PositiveInfinity, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray36 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<Badge> registrator34 = registrator33.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<Badge, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_MaxHeight)), expressionArray36), out MaxHeightProperty, double.PositiveInfinity, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Badge), "x");
            ParameterExpression[] expressionArray37 = new ParameterExpression[] { expression };
            Thickness thickness = new Thickness();
            frameworkOptions = null;
            registrator34.Register<Thickness>(System.Linq.Expressions.Expression.Lambda<Func<Badge, Thickness>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Badge.get_Margin)), expressionArray37), out MarginProperty, thickness, frameworkOptions);
        }

        protected internal virtual BadgeControl CreateControl()
        {
            BadgeControl badgeControl = new BadgeControl {
                Badge = this.TryGetCurrentValueAsFrozen<Badge>(true)
            };
            this.InitializeControl(badgeControl);
            return badgeControl;
        }

        protected override Freezable CreateInstanceCore() => 
            new Badge();

        public static Badge GetBadge(DependencyObject element) => 
            (Badge) element.GetValue(BadgeProperty);

        protected virtual void InitializeControl(BadgeControl badgeControl)
        {
            badgeControl.CoerceValue(Control.TemplateProperty);
            badgeControl.CornerRadius = this.CornerRadius;
            badgeControl.Opacity = this.Opacity;
            badgeControl.Content = this.Content;
            badgeControl.ContentTemplate = this.ContentTemplate;
            badgeControl.ContentTemplateSelector = this.ContentTemplateSelector;
            badgeControl.ContentStringFormat = this.ContentStringFormat;
            badgeControl.ContentFormatProvider = this.ContentFormatProvider;
            badgeControl.UpdatePlacement();
            badgeControl.CoerceValue(BadgeControl.KindProperty);
            badgeControl.CoerceValue(BadgeControl.ShapeProperty);
        }

        protected internal virtual Rect RaiseCustomPlacement(Size badgeSize, Rect targetBounds, Rect precomputed) => 
            (this.CustomPlacement != null) ? this.CustomPlacement(badgeSize, targetBounds, precomputed) : precomputed;

        public static void SetBadge(DependencyObject element, Badge value)
        {
            element.SetValue(BadgeProperty, value);
        }

        public Thickness Margin
        {
            get => 
                (Thickness) base.GetValue(MarginProperty);
            set => 
                base.SetValue(MarginProperty, value);
        }

        public double MaxHeight
        {
            get => 
                (double) base.GetValue(MaxHeightProperty);
            set => 
                base.SetValue(MaxHeightProperty, value);
        }

        public double MaxWidth
        {
            get => 
                (double) base.GetValue(MaxWidthProperty);
            set => 
                base.SetValue(MaxWidthProperty, value);
        }

        public double MinHeight
        {
            get => 
                (double) base.GetValue(MinHeightProperty);
            set => 
                base.SetValue(MinHeightProperty, value);
        }

        public double MinWidth
        {
            get => 
                (double) base.GetValue(MinWidthProperty);
            set => 
                base.SetValue(MinWidthProperty, value);
        }

        public double Height
        {
            get => 
                (double) base.GetValue(HeightProperty);
            set => 
                base.SetValue(HeightProperty, value);
        }

        public double Width
        {
            get => 
                (double) base.GetValue(WidthProperty);
            set => 
                base.SetValue(WidthProperty, value);
        }

        public object Content
        {
            get => 
                base.GetValue(ContentProperty);
            set => 
                base.SetValue(ContentProperty, value);
        }

        public DevExpress.Xpf.Core.BadgeKind BadgeKind
        {
            get => 
                (DevExpress.Xpf.Core.BadgeKind) base.GetValue(BadgeKindProperty);
            set => 
                base.SetValue(BadgeKindProperty, value);
        }

        public DevExpress.Xpf.Core.BadgeShape? BadgeShape
        {
            get => 
                (DevExpress.Xpf.Core.BadgeShape?) base.GetValue(BadgeShapeProperty);
            set => 
                base.SetValue(BadgeShapeProperty, value);
        }

        public int AnimationDuration
        {
            get => 
                (int) base.GetValue(AnimationDurationProperty);
            set => 
                base.SetValue(AnimationDurationProperty, value);
        }

        public System.Windows.Visibility Visibility
        {
            get => 
                (System.Windows.Visibility) base.GetValue(VisibilityProperty);
            set => 
                base.SetValue(VisibilityProperty, value);
        }

        public System.Windows.HorizontalAlignment HorizontalAlignment
        {
            get => 
                (System.Windows.HorizontalAlignment) base.GetValue(HorizontalAlignmentProperty);
            set => 
                base.SetValue(HorizontalAlignmentProperty, value);
        }

        public System.Windows.HorizontalAlignment HorizontalAnchor
        {
            get => 
                (System.Windows.HorizontalAlignment) base.GetValue(HorizontalAnchorProperty);
            set => 
                base.SetValue(HorizontalAnchorProperty, value);
        }

        public System.Windows.VerticalAlignment VerticalAlignment
        {
            get => 
                (System.Windows.VerticalAlignment) base.GetValue(VerticalAlignmentProperty);
            set => 
                base.SetValue(VerticalAlignmentProperty, value);
        }

        public System.Windows.VerticalAlignment VerticalAnchor
        {
            get => 
                (System.Windows.VerticalAlignment) base.GetValue(VerticalAnchorProperty);
            set => 
                base.SetValue(VerticalAnchorProperty, value);
        }

        public System.Windows.HorizontalAlignment HorizontalContentAlignment
        {
            get => 
                (System.Windows.HorizontalAlignment) base.GetValue(HorizontalContentAlignmentProperty);
            set => 
                base.SetValue(HorizontalContentAlignmentProperty, value);
        }

        public System.Windows.VerticalAlignment VerticalContentAlignment
        {
            get => 
                (System.Windows.VerticalAlignment) base.GetValue(VerticalContentAlignmentProperty);
            set => 
                base.SetValue(VerticalContentAlignmentProperty, value);
        }

        public Thickness? Padding
        {
            get => 
                (Thickness?) base.GetValue(PaddingProperty);
            set => 
                base.SetValue(PaddingProperty, value);
        }

        public double Opacity
        {
            get => 
                (double) base.GetValue(OpacityProperty);
            set => 
                base.SetValue(OpacityProperty, value);
        }

        public Brush Background
        {
            get => 
                (Brush) base.GetValue(BackgroundProperty);
            set => 
                base.SetValue(BackgroundProperty, value);
        }

        public Brush BorderBrush
        {
            get => 
                (Brush) base.GetValue(BorderBrushProperty);
            set => 
                base.SetValue(BorderBrushProperty, value);
        }

        public Thickness? BorderThickness
        {
            get => 
                (Thickness?) base.GetValue(BorderThicknessProperty);
            set => 
                base.SetValue(BorderThicknessProperty, value);
        }

        public System.Windows.CornerRadius? CornerRadius
        {
            get => 
                (System.Windows.CornerRadius?) base.GetValue(CornerRadiusProperty);
            set => 
                base.SetValue(CornerRadiusProperty, value);
        }

        public Brush Foreground
        {
            get => 
                (Brush) base.GetValue(ForegroundProperty);
            set => 
                base.SetValue(ForegroundProperty, value);
        }

        public double FontSize
        {
            get => 
                (double) base.GetValue(FontSizeProperty);
            set => 
                base.SetValue(FontSizeProperty, value);
        }

        public System.Windows.Media.FontFamily FontFamily
        {
            get => 
                (System.Windows.Media.FontFamily) base.GetValue(FontFamilyProperty);
            set => 
                base.SetValue(FontFamilyProperty, value);
        }

        public System.Windows.FontWeight FontWeight
        {
            get => 
                (System.Windows.FontWeight) base.GetValue(FontWeightProperty);
            set => 
                base.SetValue(FontWeightProperty, value);
        }

        public System.Windows.FontStretch FontStretch
        {
            get => 
                (System.Windows.FontStretch) base.GetValue(FontStretchProperty);
            set => 
                base.SetValue(FontStretchProperty, value);
        }

        public System.Windows.FontStyle FontStyle
        {
            get => 
                (System.Windows.FontStyle) base.GetValue(FontStyleProperty);
            set => 
                base.SetValue(FontStyleProperty, value);
        }

        public ControlTemplate Template
        {
            get => 
                (ControlTemplate) base.GetValue(TemplateProperty);
            set => 
                base.SetValue(TemplateProperty, value);
        }

        public DataTemplate ContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContentTemplateProperty);
            set => 
                base.SetValue(ContentTemplateProperty, value);
        }

        public DataTemplateSelector ContentTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ContentTemplateSelectorProperty);
            set => 
                base.SetValue(ContentTemplateSelectorProperty, value);
        }

        public string ContentStringFormat
        {
            get => 
                (string) base.GetValue(ContentStringFormatProperty);
            set => 
                base.SetValue(ContentStringFormatProperty, value);
        }

        public IFormatProvider ContentFormatProvider
        {
            get => 
                (IFormatProvider) base.GetValue(ContentFormatProviderProperty);
            set => 
                base.SetValue(ContentFormatProviderProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Badge.<>c <>9 = new Badge.<>c();

            internal void <.cctor>b__36_0(DependencyObject x, DependencyPropertyChangedEventArgs v)
            {
                x.CoerceValue(Badges.BadgeAndShowModeProperty);
            }

            internal double <.cctor>b__36_1(Badge x, double v) => 
                Math.Min(Math.Max(v, 0.001), 35791.0);
        }
    }
}

