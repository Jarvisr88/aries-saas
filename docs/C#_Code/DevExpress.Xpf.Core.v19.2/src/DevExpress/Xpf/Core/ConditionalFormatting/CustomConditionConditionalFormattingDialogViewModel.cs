namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Data;
    using DevExpress.Data.ExpressionEditor;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Exceptions;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Editors.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class CustomConditionConditionalFormattingDialogViewModel : ExpressionConditionalFormattingDialogViewModel
    {
        public const string ConditionServiceKey = "customConditionService";
        private string _valueText;

        protected CustomConditionConditionalFormattingDialogViewModel(IFormatsOwner owner) : base(owner, ConditionalFormattingStringId.ConditionalFormatting_CustomConditionDialog_Title, ConditionalFormattingStringId.ConditionalFormatting_CustomConditionDialog_Description, ConditionalFormattingStringId.ConditionalFormatting_CustomConditionDialog_Connector)
        {
        }

        private CriteriaOperator GetCiteriaForFilterEditor()
        {
            try
            {
                return CriteriaOperator.Parse(this.GetCriteriaStringCore(true), new object[0]);
            }
            catch (CriteriaParserException)
            {
                return null;
            }
        }

        protected override CriteriaOperator GetCriteria(string fieldName)
        {
            throw new InvalidOperationException();
        }

        protected override string GetCriteriaString(string fieldName) => 
            this.GetCriteriaStringCore(true);

        private string GetCriteriaStringCore(bool convertToFields = true)
        {
            string expression = ((string) this.Value) ?? string.Empty;
            if (convertToFields)
            {
                expression = UnboundExpressionConvertHelper.ConvertToFields(this.DataColumnInfo, expression);
            }
            return expression;
        }

        private string GetDisplayExpression(CriteriaOperator criteria)
        {
            Func<string> fallback = <>c.<>9__23_1;
            if (<>c.<>9__23_1 == null)
            {
                Func<string> local1 = <>c.<>9__23_1;
                fallback = <>c.<>9__23_1 = () => string.Empty;
            }
            return criteria.Return<CriteriaOperator, string>(x => UnboundExpressionConvertHelper.ConvertToCaption(this.DataColumnInfo, x.ToString()), fallback);
        }

        internal override object GetInitialValue() => 
            !this.IsReadOnly ? this.GetDisplayExpression(new BinaryOperator(base.Context.ColumnInfo.FieldName, null, BinaryOperatorType.Equal)) : new BinaryOperator(base.Context.ColumnInfo.FieldName, null, BinaryOperatorType.Equal).ToString();

        protected override void OnValueChanged()
        {
            base.OnValueChanged();
            Func<string> fallback = <>c.<>9__13_1;
            if (<>c.<>9__13_1 == null)
            {
                Func<string> local1 = <>c.<>9__13_1;
                fallback = <>c.<>9__13_1 = () => string.Empty;
            }
            this.ValueText = this.Value.Return<object, string>(x => UnboundExpressionConvertHelper.ConvertToCaption(this.DataColumnInfo, x.ToString()), fallback);
        }

        public void ShowConditionEditor()
        {
            CustomConditionEditorViewModel model1 = new CustomConditionEditorViewModel(base.Context.FilterColumn, base.Context.FilteredComponent);
            model1.Criteria = this.GetCiteriaForFilterEditor();
            CustomConditionEditorViewModel viewModel = model1;
            if (this.DialogService.ShowDialog(MessageButton.OKCancel, ConditionalFormattingLocalizer.GetString(ConditionalFormattingStringId.ConditionalFormatting_CustomConditionEditor_Title), viewModel) == MessageResult.OK)
            {
                if (this.IsReadOnly)
                {
                    if (Equals(viewModel.Criteria, null))
                    {
                        this.Value = null;
                    }
                    else
                    {
                        this.Value = viewModel.Criteria.ToString();
                    }
                }
                else
                {
                    this.Value = this.GetDisplayExpression(viewModel.Criteria);
                }
            }
        }

        public override bool TryClose()
        {
            string criteriaStringCore = this.GetCriteriaStringCore(false);
            return (!string.IsNullOrEmpty(criteriaStringCore) ? (ConditionalFormattingDialogHelper.ValidateExpression(criteriaStringCore, this.MessageBox, this.DataColumnInfo) && base.TryClose()) : ConditionalFormattingDialogHelper.ShowInvalidExpressionError(this.MessageBox));
        }

        public static Func<IFormatsOwner, CustomConditionConditionalFormattingDialogViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IFormatsOwner), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<IFormatsOwner, CustomConditionConditionalFormattingDialogViewModel>(Expression.Lambda<Func<IFormatsOwner, CustomConditionConditionalFormattingDialogViewModel>>(Expression.New((ConstructorInfo) methodof(CustomConditionConditionalFormattingDialogViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public override DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType ConditionValueType =>
            DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.SingleWithElipsisButton;

        [ServiceProperty(Key="customConditionService")]
        protected virtual IDialogService DialogService =>
            null;

        private IDataColumnInfo DataColumnInfo =>
            base.Context.ColumnInfo;

        public string ValueText
        {
            get => 
                this._valueText;
            set
            {
                this._valueText = value;
                this.RaisePropertiesChanged();
            }
        }

        public bool IsReadOnly =>
            !CompatibilitySettings.AllowEditTextExpressionInFormatRule;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomConditionConditionalFormattingDialogViewModel.<>c <>9 = new CustomConditionConditionalFormattingDialogViewModel.<>c();
            public static Func<string> <>9__13_1;
            public static Func<string> <>9__23_1;

            internal string <GetDisplayExpression>b__23_1() => 
                string.Empty;

            internal string <OnValueChanged>b__13_1() => 
                string.Empty;
        }

        public class CustomConditionEditorViewModel
        {
            public CustomConditionEditorViewModel(FilterColumn defaultFilterColumn, IFilteredComponent filteredComponent)
            {
                this.DefaultFilterColumn = defaultFilterColumn;
                this.FilteredComponent = filteredComponent;
            }

            public CriteriaOperator Criteria { get; set; }

            public FilterColumn DefaultFilterColumn { get; private set; }

            public IFilteredComponent FilteredComponent { get; private set; }
        }
    }
}

