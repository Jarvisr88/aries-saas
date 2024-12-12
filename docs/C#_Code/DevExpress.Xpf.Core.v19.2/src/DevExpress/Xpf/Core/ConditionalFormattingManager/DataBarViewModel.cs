namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class DataBarViewModel : FieldEditorOwner, IConditionEditor
    {
        private Brush zeroLineBrush;
        private double zeroLineThickness;
        private bool allowUpdateFormat;

        protected DataBarViewModel(IDialogContext context) : base(context)
        {
            this.allowUpdateFormat = true;
            this.allowUpdateFormat = false;
            try
            {
                this.ColorFill = Colors.Blue;
                this.ColorBorder = Colors.Black;
                this.BorderMode = DataBarBorderMode.NoBorder;
                this.ColorFillNegative = Colors.Red;
                this.ColorBorderNegative = Colors.Black;
                this.BorderModeNegative = DataBarBorderMode.NoBorder;
                this.UseDefaultNegativeBar = false;
                this.zeroLineBrush = (Brush) DataBarFormat.ZeroLineBrushProperty.GetMetadata(typeof(DataBarFormat)).DefaultValue;
                this.zeroLineThickness = (double) DataBarFormat.ZeroLineThicknessProperty.GetMetadata(typeof(DataBarFormat)).DefaultValue;
            }
            finally
            {
                this.allowUpdateFormat = true;
                this.UpdateFormat();
            }
        }

        private Brush CreateFillBrush(bool isPositive)
        {
            DataBarFillMode fillMode = this.FillMode;
            Color colorFill = this.ColorFill;
            if (!isPositive && !this.UseDefaultNegativeBar)
            {
                fillMode = this.FillModeNegative;
                colorFill = this.ColorFillNegative;
            }
            if (fillMode == DataBarFillMode.SolidFill)
            {
                return new SolidColorBrush(colorFill);
            }
            if (fillMode != DataBarFillMode.GradientFill)
            {
                throw new InvalidOperationException();
            }
            Color startColor = colorFill;
            Color transparent = Colors.Transparent;
            if (!isPositive)
            {
                startColor = transparent;
                transparent = startColor;
            }
            return new LinearGradientBrush(startColor, transparent, 0.0);
        }

        bool IConditionEditor.CanInit(BaseEditUnit unit) => 
            unit is DataBarEditUnit;

        BaseEditUnit IConditionEditor.Edit()
        {
            DataBarEditUnit unit = new DataBarEditUnit {
                Format = this.PreviewFormat
            };
            base.EditIndicator(unit);
            return unit;
        }

        void IConditionEditor.Init(BaseEditUnit unit)
        {
            DataBarEditUnit unit2 = unit as DataBarEditUnit;
            if (unit2 != null)
            {
                base.InitIndicator(unit2);
                DataBarFormat format = unit2.Format;
                if (format != null)
                {
                    this.zeroLineBrush = format.ZeroLineBrush;
                    this.zeroLineThickness = format.ZeroLineThickness;
                    this.ColorFill = GetColor(format.Fill, this.ColorFill, true);
                    this.ColorBorder = GetColor(format.BorderBrush, this.ColorBorder, true);
                    this.ColorFillNegative = GetColor(format.FillNegative, this.ColorFill, false);
                    this.ColorBorderNegative = GetColor(format.BorderBrushNegative, this.ColorBorder, false);
                    this.UseDefaultNegativeBar = (format.FillNegative == null) && ReferenceEquals(format.BorderBrushNegative, null);
                    this.BorderMode = this.GetBorderMode(format.BorderBrush);
                    this.BorderModeNegative = this.GetBorderMode(format.BorderBrushNegative);
                    this.FillMode = this.GetFillMode(format.Fill);
                    this.FillModeNegative = this.GetFillMode(format.FillNegative);
                }
            }
        }

        bool IConditionEditor.Validate() => 
            base.ValidateExpression();

        private DataBarBorderMode GetBorderMode(Brush brush) => 
            (brush == null) ? DataBarBorderMode.NoBorder : DataBarBorderMode.Border;

        private static Color GetColor(Brush brush, Color defalutColor, bool isPositive)
        {
            if (brush is SolidColorBrush)
            {
                return ((SolidColorBrush) brush).Color;
            }
            if (brush is GradientBrush)
            {
                GradientStop stop = ((GradientBrush) brush).GradientStops.FirstOrDefault<GradientStop>(x => x.Offset == (isPositive ? 0.0 : 1.0));
                if (stop != null)
                {
                    return stop.Color;
                }
            }
            return defalutColor;
        }

        private DataBarFillMode GetFillMode(Brush brush) => 
            (brush is GradientBrush) ? DataBarFillMode.GradientFill : DataBarFillMode.SolidFill;

        protected void UpdateFormat()
        {
            if (this.allowUpdateFormat)
            {
                DataBarFormat dependencyObject = new DataBarFormat();
                if (this.BorderMode == DataBarBorderMode.Border)
                {
                    dependencyObject.BorderBrush = new SolidColorBrush(this.ColorBorder);
                }
                if (!this.UseDefaultNegativeBar && (this.BorderModeNegative == DataBarBorderMode.Border))
                {
                    dependencyObject.BorderBrushNegative = new SolidColorBrush(this.ColorBorderNegative);
                }
                dependencyObject.Fill = this.CreateFillBrush(true);
                dependencyObject.FillNegative = this.CreateFillBrush(false);
                ManagerHelper.SetProperty(dependencyObject, DataBarFormat.ZeroLineThicknessProperty, this.zeroLineThickness);
                ManagerHelper.SetProperty(dependencyObject, DataBarFormat.ZeroLineBrushProperty, this.zeroLineBrush);
                dependencyObject.Freeze();
                this.PreviewFormat = dependencyObject;
            }
        }

        public static Func<IDialogContext, DataBarViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IDialogContext), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<IDialogContext, DataBarViewModel>(Expression.Lambda<Func<IDialogContext, DataBarViewModel>>(Expression.New((ConstructorInfo) methodof(DataBarViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        [BindableProperty(OnPropertyChangedMethodName="UpdateFormat")]
        public virtual Color ColorFill { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdateFormat")]
        public virtual Color ColorBorder { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdateFormat")]
        public virtual DataBarFillMode FillMode { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdateFormat")]
        public virtual DataBarBorderMode BorderMode { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdateFormat")]
        public virtual bool UseDefaultNegativeBar { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdateFormat")]
        public virtual Color ColorFillNegative { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdateFormat")]
        public virtual Color ColorBorderNegative { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdateFormat")]
        public virtual DataBarFillMode FillModeNegative { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdateFormat")]
        public virtual DataBarBorderMode BorderModeNegative { get; set; }

        public virtual DataBarFormat PreviewFormat { get; protected set; }

        public override string Description =>
            base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_DataBar);
    }
}

