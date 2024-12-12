namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    [DXToolboxBrowsable(DXToolboxItemKind.Free), LicenseProvider(typeof(DX_WPFEditors_LicenseProvider))]
    public class RatingEdit : BaseEdit, IImageExportSettings, IExportSettings
    {
        public static readonly DependencyProperty OrientationProperty;
        public static readonly DependencyProperty PrecisionProperty;
        public static readonly DependencyProperty ItemsCountProperty;
        public static readonly DependencyProperty ItemStyleProperty;
        public static readonly DependencyProperty ValueProperty;
        public static readonly DependencyProperty MaximumProperty;
        public static readonly DependencyProperty MinimumProperty;

        static RatingEdit()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingEdit), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<RatingEdit> registrator1 = DependencyPropertyRegistrator<RatingEdit>.New().OverrideDefaultStyleKey().OverrideMetadata(BaseEdit.ShowBorderProperty, false, null, FrameworkPropertyMetadataOptions.None).OverrideMetadata(BaseEdit.DisplayFormatStringProperty, "{0:f1}", null, FrameworkPropertyMetadataOptions.None).OverrideMetadata<HorizontalAlignment>(Control.HorizontalContentAlignmentProperty, (x, oldValue, newValue) => x.OnHorizontalContentAlignmentChanged(oldValue, newValue)).OverrideMetadata<VerticalAlignment>(Control.VerticalContentAlignmentProperty, (x, oldValue, newValue) => x.OnVerticalContentAlignmentChanged(oldValue, newValue)).Register<System.Windows.Controls.Orientation>(System.Linq.Expressions.Expression.Lambda<Func<RatingEdit, System.Windows.Controls.Orientation>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingEdit.get_Orientation)), parameters), out OrientationProperty, RatingDefaultValueHelper.Orientation, (x, oldValue, newValue) => x.OnOrientationChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingEdit), "x");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingEdit> registrator2 = registrator1.Register<RatingPrecision>(System.Linq.Expressions.Expression.Lambda<Func<RatingEdit, RatingPrecision>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingEdit.get_Precision)), expressionArray2), out PrecisionProperty, RatingPrecision.Full, (x, oldValue, newValue) => x.OnPrecisionChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingEdit), "x");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingEdit> registrator3 = registrator2.Register<int>(System.Linq.Expressions.Expression.Lambda<Func<RatingEdit, int>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingEdit.get_ItemsCount)), expressionArray3), out ItemsCountProperty, 5, (x, oldValue, newValue) => x.OnItemsCountChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingEdit), "x");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingEdit> registrator4 = registrator3.Register<Style>(System.Linq.Expressions.Expression.Lambda<Func<RatingEdit, Style>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingEdit.get_ItemStyle)), expressionArray4), out ItemStyleProperty, null, (x, oldValue, newValue) => x.OnItemStyleChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingEdit), "x");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingEdit> registrator5 = registrator4.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<RatingEdit, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingEdit.get_Value)), expressionArray5), out ValueProperty, 0.0, (x, oldValue, newValue) => x.OnValueChanged(oldValue, newValue), (x, value) => x.CoerceValue(value), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingEdit), "x");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingEdit> registrator6 = registrator5.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<RatingEdit, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingEdit.get_Minimum)), expressionArray6), out MinimumProperty, 0.0, (x, oldValue, newValue) => x.OnMinimumChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingEdit), "x");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator6.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<RatingEdit, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingEdit.get_Maximum)), expressionArray7), out MaximumProperty, double.NaN, (x, oldValue, newValue) => x.OnMaximumChanged(oldValue, newValue), frameworkOptions);
            BaseEdit.EditValueProperty.OverrideMetadata(typeof(RatingEdit), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, null, true, UpdateSourceTrigger.PropertyChanged));
        }

        protected virtual double CoerceValue(object value) => 
            (double) this.EditStrategy.CoerceValue(ValueProperty, value);

        protected override EditStrategyBase CreateEditStrategy() => 
            new RatingEditStrategy(this);

        protected internal override Brush GetPrintBorderBrush() => 
            Brushes.Transparent;

        protected virtual void OnEditCoreValueChanged(object sender, ValueChangedEventArgs<double> e)
        {
            this.EditStrategy.SyncWithEditor();
        }

        protected virtual void OnHorizontalContentAlignmentChanged(HorizontalAlignment oldValue, HorizontalAlignment newValue)
        {
            this.EditStrategy.ContentAlignmentChanged(newValue, base.VerticalContentAlignment);
        }

        protected virtual void OnItemsCountChanged(int oldValue, int newValue)
        {
            this.EditStrategy.ItemsCountChanged(newValue);
        }

        protected virtual void OnItemStyleChanged(Style oldValue, Style newValue)
        {
            this.EditStrategy.ItemStyleChanged(newValue);
        }

        protected virtual void OnMaximumChanged(double oldValue, double newValue)
        {
            this.EditStrategy.MaximumChanged(newValue);
        }

        protected virtual void OnMinimumChanged(double oldValue, double newValue)
        {
            this.EditStrategy.MinimumChanged(newValue);
        }

        protected virtual void OnOrientationChanged(System.Windows.Controls.Orientation oldValue, System.Windows.Controls.Orientation newValue)
        {
            this.EditStrategy.OrientationChanged(newValue);
        }

        protected virtual void OnPrecisionChanged(RatingPrecision oldValue, RatingPrecision newValue)
        {
            this.EditStrategy.PrecisionChanged(newValue);
        }

        protected virtual void OnValueChanged(double oldValue, double newValue)
        {
            this.EditStrategy.ValuePropertyChanged(oldValue, newValue);
        }

        protected virtual void OnVerticalContentAlignmentChanged(VerticalAlignment oldValue, VerticalAlignment newValue)
        {
            this.EditStrategy.ContentAlignmentChanged(base.HorizontalContentAlignment, newValue);
        }

        protected override void SubscribeEditEventsCore()
        {
            base.SubscribeEditEventsCore();
            (base.EditCore as RatingControl).Do<RatingControl>(delegate (RatingControl x) {
                x.ValueChanged += new ValueChangedEventHandler<double>(this.OnEditCoreValueChanged);
            });
        }

        protected override void UnsubscribeEditEventsCore()
        {
            (base.EditCore as RatingControl).Do<RatingControl>(delegate (RatingControl x) {
                x.ValueChanged -= new ValueChangedEventHandler<double>(this.OnEditCoreValueChanged);
            });
            base.UnsubscribeEditEventsCore();
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        public RatingPrecision Precision
        {
            get => 
                (RatingPrecision) base.GetValue(PrecisionProperty);
            set => 
                base.SetValue(PrecisionProperty, value);
        }

        public int ItemsCount
        {
            get => 
                (int) base.GetValue(ItemsCountProperty);
            set => 
                base.SetValue(ItemsCountProperty, value);
        }

        public Style ItemStyle
        {
            get => 
                (Style) base.GetValue(ItemStyleProperty);
            set => 
                base.SetValue(ItemStyleProperty, value);
        }

        public double Value
        {
            get => 
                (double) base.GetValue(ValueProperty);
            set => 
                base.SetValue(ValueProperty, value);
        }

        public double Minimum
        {
            get => 
                (double) base.GetValue(MinimumProperty);
            set => 
                base.SetValue(MinimumProperty, value);
        }

        public double Maximum
        {
            get => 
                (double) base.GetValue(MaximumProperty);
            set => 
                base.SetValue(MaximumProperty, value);
        }

        internal RatingEditStrategy EditStrategy =>
            (RatingEditStrategy) base.EditStrategy;

        protected internal override Type StyleSettingsType =>
            typeof(EditStyleSettings);

        FrameworkElement IImageExportSettings.SourceElement =>
            this;

        ImageRenderMode IImageExportSettings.ImageRenderMode =>
            ImageRenderMode.MakeScreenshot;

        bool IImageExportSettings.ForceCenterImageMode =>
            false;

        object IImageExportSettings.ImageKey =>
            null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RatingEdit.<>c <>9 = new RatingEdit.<>c();

            internal void <.cctor>b__28_0(RatingEdit x, HorizontalAlignment oldValue, HorizontalAlignment newValue)
            {
                x.OnHorizontalContentAlignmentChanged(oldValue, newValue);
            }

            internal void <.cctor>b__28_1(RatingEdit x, VerticalAlignment oldValue, VerticalAlignment newValue)
            {
                x.OnVerticalContentAlignmentChanged(oldValue, newValue);
            }

            internal void <.cctor>b__28_2(RatingEdit x, Orientation oldValue, Orientation newValue)
            {
                x.OnOrientationChanged(oldValue, newValue);
            }

            internal void <.cctor>b__28_3(RatingEdit x, RatingPrecision oldValue, RatingPrecision newValue)
            {
                x.OnPrecisionChanged(oldValue, newValue);
            }

            internal void <.cctor>b__28_4(RatingEdit x, int oldValue, int newValue)
            {
                x.OnItemsCountChanged(oldValue, newValue);
            }

            internal void <.cctor>b__28_5(RatingEdit x, Style oldValue, Style newValue)
            {
                x.OnItemStyleChanged(oldValue, newValue);
            }

            internal void <.cctor>b__28_6(RatingEdit x, double oldValue, double newValue)
            {
                x.OnValueChanged(oldValue, newValue);
            }

            internal double <.cctor>b__28_7(RatingEdit x, double value) => 
                x.CoerceValue(value);

            internal void <.cctor>b__28_8(RatingEdit x, double oldValue, double newValue)
            {
                x.OnMinimumChanged(oldValue, newValue);
            }

            internal void <.cctor>b__28_9(RatingEdit x, double oldValue, double newValue)
            {
                x.OnMaximumChanged(oldValue, newValue);
            }
        }
    }
}

