namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ManagerItemViewModel
    {
        private ManagerViewModel parentVM;

        protected ManagerItemViewModel(ISupportManager condition)
        {
            if (condition == null)
            {
                this.AllowUserCustomization = true;
            }
            else
            {
                this.InitFromCondition(condition);
                this.AllowUserCustomization = condition.AllowUserCustomization || this.IsInDesignMode();
            }
        }

        private bool AreEditUnitsCompatible(BaseEditUnit firstUnit, BaseEditUnit secondUnit) => 
            (firstUnit != null) && ((secondUnit != null) && (firstUnit.GetType() == secondUnit.GetType()));

        private Freezable CreatePreviewWrapper(BaseEditUnit unit)
        {
            if (unit == null)
            {
                return null;
            }
            if (!(unit is IconSetEditUnit))
            {
                return unit.GetFormat();
            }
            IconSetFormat format = unit.GetFormat() as IconSetFormat;
            if ((format == null) || !string.IsNullOrEmpty(unit.PredefinedFormatName))
            {
                Func<ManagerViewModel, IDialogContext> evaluator = <>c.<>9__62_0;
                if (<>c.<>9__62_0 == null)
                {
                    Func<ManagerViewModel, IDialogContext> local1 = <>c.<>9__62_0;
                    evaluator = <>c.<>9__62_0 = x => x.Context;
                }
                Func<IDialogContext, IFormatsOwner> func2 = <>c.<>9__62_1;
                if (<>c.<>9__62_1 == null)
                {
                    Func<IDialogContext, IFormatsOwner> local2 = <>c.<>9__62_1;
                    func2 = <>c.<>9__62_1 = y => y.PredefinedFormatsOwner;
                }
                Func<IFormatsOwner, FormatInfoCollection> func3 = <>c.<>9__62_2;
                if (<>c.<>9__62_2 == null)
                {
                    Func<IFormatsOwner, FormatInfoCollection> local3 = <>c.<>9__62_2;
                    func3 = <>c.<>9__62_2 = z => z.PredefinedIconSetFormats;
                }
                FormatInfoCollection source = this.parentVM.With<ManagerViewModel, IDialogContext>(evaluator).With<IDialogContext, IFormatsOwner>(func2).With<IFormatsOwner, FormatInfoCollection>(func3);
                if (source == null)
                {
                    return null;
                }
                string predefinedName = unit.PredefinedFormatName;
                Func<FormatInfo, IconSetFormat> func4 = <>c.<>9__62_4;
                if (<>c.<>9__62_4 == null)
                {
                    Func<FormatInfo, IconSetFormat> local4 = <>c.<>9__62_4;
                    func4 = <>c.<>9__62_4 = x => x.Format as IconSetFormat;
                }
                format = source.FirstOrDefault<FormatInfo>(x => (x.FormatName == predefinedName)).With<FormatInfo, IconSetFormat>(func4);
            }
            return ((format != null) ? new IconFormatStyle(format, string.Empty) : null);
        }

        private static Freezable CreatePreviewWrapper(Freezable format) => 
            (format is IconSetFormat) ? new IconFormatStyle((IconSetFormat) format, string.Empty) : format;

        private void InitFromCondition(ISupportManager condition)
        {
            this.Value = condition;
            this.EditUnit = condition.CreateEditUnit();
            this.UpdateConditon();
        }

        protected void OnAppliesToChanged()
        {
            if (this.EditUnit.FieldName != this.AppliesTo)
            {
                this.EditUnit.FieldName = this.AppliesTo;
                this.RegisterUnitPropertyChange();
            }
        }

        protected void OnApplyToRowChanged()
        {
            if (this.EditUnit.ApplyToRow != this.ApplyToRow)
            {
                this.EditUnit.ApplyToRow = this.ApplyToRow;
                this.RegisterUnitPropertyChange();
            }
        }

        protected void OnColumnNameChanged()
        {
            if (this.EditUnit.ColumnName != this.ColumnName)
            {
                this.EditUnit.ColumnName = this.ColumnName;
                this.RegisterUnitPropertyChange();
            }
        }

        protected void OnIsEnabledChanged()
        {
            if (this.EditUnit.IsEnabled != this.IsEnabled)
            {
                this.EditUnit.IsEnabled = this.IsEnabled;
                this.RegisterUnitPropertyChange();
            }
        }

        protected void OnRowNameChanged()
        {
            if (this.EditUnit.RowName != this.RowName)
            {
                this.EditUnit.RowName = this.RowName;
                this.RegisterUnitPropertyChange();
            }
        }

        private void RegisterUnitPropertyChange()
        {
            if (this.parentVM != null)
            {
                this.parentVM.CanApply = true;
            }
        }

        public void SetEditUnit(BaseEditUnit unit)
        {
            if (this.AreEditUnitsCompatible(this.EditUnit, unit))
            {
                this.EditUnit.Populate(unit);
            }
            else
            {
                unit.Restore(this.EditUnit);
                this.EditUnit = unit;
                this.Value = null;
            }
            this.UpdateConditon();
        }

        internal void SetOwner(ManagerViewModel vm)
        {
            this.parentVM = vm;
            this.UpdatePreviewFormat();
            this.UpdateRule();
        }

        private void UpdateConditon()
        {
            this.UpdateRule();
            this.AppliesTo = this.EditUnit.FieldName;
            this.UpdatePreviewFormat();
            this.CanApplyToRow = this.EditUnit.CanApplyToRow;
            this.ApplyToRow = this.CanApplyToRow ? this.EditUnit.ApplyToRow : false;
            this.IsEnabled = this.EditUnit.IsEnabled;
            this.RowName = this.EditUnit.RowName;
            this.ColumnName = this.EditUnit.ColumnName;
            if ((this.PreviewFormat != null) && this.PreviewFormat.CanFreeze)
            {
                this.PreviewFormat.Freeze();
            }
        }

        private void UpdatePreviewFormat()
        {
            if (this.EditUnit != null)
            {
                this.PreviewFormat = this.CreatePreviewWrapper(this.EditUnit);
            }
        }

        private void UpdateRule()
        {
            if (this.EditUnit != null)
            {
                this.Rule = this.EditUnit.GetDescription(this.parentVM?.Context);
            }
        }

        public static Func<ISupportManager, ManagerItemViewModel> Factory
        {
            get
            {
                ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(ISupportManager), "x");
                System.Linq.Expressions.Expression[] expressionArray1 = new System.Linq.Expressions.Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<ISupportManager, ManagerItemViewModel>(System.Linq.Expressions.Expression.Lambda<Func<ISupportManager, ManagerItemViewModel>>(System.Linq.Expressions.Expression.New((ConstructorInfo) methodof(ManagerItemViewModel..ctor), (IEnumerable<System.Linq.Expressions.Expression>) expressionArray1), parameters));
            }
        }

        public ISupportManager Value { get; private set; }

        public BaseEditUnit EditUnit { get; private set; }

        public virtual string Rule { get; protected set; }

        public virtual Freezable PreviewFormat { get; protected set; }

        public virtual string AppliesTo { get; set; }

        public virtual bool ApplyToRow { get; set; }

        public virtual bool CanApplyToRow { get; protected set; }

        public virtual string ColumnName { get; set; }

        public virtual string RowName { get; set; }

        public virtual bool IsEnabled { get; set; }

        internal bool AllowUserCustomization { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ManagerItemViewModel.<>c <>9 = new ManagerItemViewModel.<>c();
            public static Func<ManagerViewModel, IDialogContext> <>9__62_0;
            public static Func<IDialogContext, IFormatsOwner> <>9__62_1;
            public static Func<IFormatsOwner, FormatInfoCollection> <>9__62_2;
            public static Func<FormatInfo, IconSetFormat> <>9__62_4;

            internal IDialogContext <CreatePreviewWrapper>b__62_0(ManagerViewModel x) => 
                x.Context;

            internal IFormatsOwner <CreatePreviewWrapper>b__62_1(IDialogContext y) => 
                y.PredefinedFormatsOwner;

            internal FormatInfoCollection <CreatePreviewWrapper>b__62_2(IFormatsOwner z) => 
                z.PredefinedIconSetFormats;

            internal IconSetFormat <CreatePreviewWrapper>b__62_4(FormatInfo x) => 
                x.Format as IconSetFormat;
        }
    }
}

