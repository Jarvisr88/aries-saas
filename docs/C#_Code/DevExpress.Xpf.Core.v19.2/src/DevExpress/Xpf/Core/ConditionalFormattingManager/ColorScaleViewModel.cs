namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class ColorScaleViewModel : FieldEditorOwner, IConditionEditor
    {
        private readonly bool allowColorMiddle;

        protected ColorScaleViewModel(IDialogContext context, bool allowColorMiddle) : base(context)
        {
            this.allowColorMiddle = allowColorMiddle;
            this.ColorMin = DefaultMin;
            this.ColorMiddle = DefaultMiddle;
            this.ColorMax = DefaultMax;
        }

        bool IConditionEditor.CanInit(BaseEditUnit unit) => 
            (unit as ColorScaleEditUnit).If<ColorScaleEditUnit>(x => ((x.Format == null) || ((x.Format.ColorMiddle != null) == this.AllowColorMiddle))) != null;

        BaseEditUnit IConditionEditor.Edit()
        {
            ColorScaleEditUnit unit = new ColorScaleEditUnit {
                Format = this.PreviewFormat
            };
            base.EditIndicator(unit);
            return unit;
        }

        void IConditionEditor.Init(BaseEditUnit unit)
        {
            ColorScaleEditUnit unit2 = unit as ColorScaleEditUnit;
            if (unit2 != null)
            {
                base.InitIndicator(unit2);
                ColorScaleFormat format = unit2.Format;
                if (format != null)
                {
                    this.ColorMin = format.ColorMin;
                    this.ColorMax = format.ColorMax;
                    if (this.AllowColorMiddle && (format.ColorMiddle != null))
                    {
                        this.ColorMiddle = format.ColorMiddle.Value;
                    }
                }
            }
        }

        bool IConditionEditor.Validate() => 
            base.ValidateExpression();

        protected void UpdateFormat()
        {
            ColorScaleFormat format1 = new ColorScaleFormat();
            format1.ColorMax = this.ColorMax;
            format1.ColorMin = this.ColorMin;
            ColorScaleFormat format = format1;
            if (this.AllowColorMiddle)
            {
                format.ColorMiddle = new Color?(this.ColorMiddle);
            }
            format.Freeze();
            this.PreviewFormat = format;
        }

        public static Func<IDialogContext, bool, ColorScaleViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IDialogContext), "x");
                ParameterExpression expression2 = Expression.Parameter(typeof(bool), "y");
                Expression[] expressionArray1 = new Expression[] { expression, expression2 };
                ParameterExpression[] parameters = new ParameterExpression[] { expression, expression2 };
                return ViewModelSource.Factory<IDialogContext, bool, ColorScaleViewModel>(Expression.Lambda<Func<IDialogContext, bool, ColorScaleViewModel>>(Expression.New((ConstructorInfo) methodof(ColorScaleViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public static Color DefaultMin =>
            Color.FromArgb(0xff, 0xf8, 0x69, 0x6b);

        public static Color DefaultMiddle =>
            Color.FromArgb(0xff, 0xff, 0xeb, 0x84);

        public static Color DefaultMax =>
            Color.FromArgb(0xff, 0x63, 190, 0x7b);

        [BindableProperty(OnPropertyChangedMethodName="UpdateFormat")]
        public virtual Color ColorMin { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdateFormat")]
        public virtual Color ColorMax { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdateFormat")]
        public virtual Color ColorMiddle { get; set; }

        public bool AllowColorMiddle =>
            this.allowColorMiddle;

        public virtual ColorScaleFormat PreviewFormat { get; protected set; }

        public override string Description =>
            this.AllowColorMiddle ? base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_3ColorScaleDescription) : base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_2ColorScaleDescription);
    }
}

