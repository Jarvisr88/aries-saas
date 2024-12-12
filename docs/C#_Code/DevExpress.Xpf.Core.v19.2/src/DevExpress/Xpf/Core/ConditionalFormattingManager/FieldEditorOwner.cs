namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Data;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class FieldEditorOwner : ManagerViewModelBase
    {
        private IDataColumnInfo columnInfo;

        protected FieldEditorOwner(IDialogContext context) : base(context)
        {
            this.columnInfo = base.Context.ColumnInfo;
            this.FieldNames = FieldNameWrapper.Create(context.ColumnInfo);
            this.FieldName = context.ColumnInfo.FieldName;
            this.UpdateConditionValueType();
        }

        private DataValueType CalcConditionValueType() => 
            ((this.MinValue != null) || (this.MaxValue != null)) ? ((Type.GetTypeCode((this.MinValue ?? this.MaxValue).GetType()) == TypeCode.DateTime) ? DataValueType.DateTime : DataValueType.Numeric) : ExpressionConditionalFormattingDialogViewModel.SelectValue<DataValueType>((base.Context.Find(this.FieldName) ?? base.Context).ColumnInfo.FieldType, <>c.<>9__43_0 ??= () => DataValueType.DateTime, <>c.<>9__43_1 ??= () => DataValueType.Numeric, <>c.<>9__43_2 ??= () => DataValueType.Numeric);

        protected void EditIndicator(IndicatorEditUnit unit)
        {
            unit.FieldName = this.FieldName;
            unit.Expression = this.GetValueExpression(this.Expression);
            unit.MinValue = this.GetConvertedValue(this.MinValue);
            unit.MaxValue = this.GetConvertedValue(this.MaxValue);
        }

        private object GetConvertedValue(object value) => 
            (value != null) ? ((this.ConditionValueType == DataValueType.DateTime) ? value : value.ToString()) : null;

        private string GetDisplayExpression(string expression) => 
            ManagerHelper.ConvertExpression(expression, this.columnInfo, new Func<IDataColumnInfo, string, string>(UnboundExpressionConvertHelper.ConvertToCaption));

        private string GetValueExpression(string expression) => 
            ManagerHelper.ConvertExpression(expression, this.columnInfo, new Func<IDataColumnInfo, string, string>(UnboundExpressionConvertHelper.ConvertToFields));

        protected void InitIndicator(IndicatorEditUnit unit)
        {
            this.FieldName = unit.FieldName;
            this.Expression = this.GetDisplayExpression(unit.Expression);
            this.FieldMode = string.IsNullOrEmpty(this.Expression) ? FieldEditorMode.FieldName : FieldEditorMode.Expression;
            this.MinValue = unit.MinValue;
            this.MaxValue = unit.MaxValue;
            this.UpdateConditionValueType();
        }

        protected virtual void OnConditionValueTypeChanged()
        {
            if (this.ConditionValueType != this.CalcConditionValueType())
            {
                this.MinValue = null;
                this.MaxValue = null;
            }
        }

        public void ShowCustomConditionEditor()
        {
            EditorViewModelWrapper wrapper1 = new EditorViewModelWrapper();
            wrapper1.ColumnInfo = new EditorDataColumnInfoWrapper(this.columnInfo, this.GetValueExpression(this.Expression));
            EditorViewModelWrapper viewModel = wrapper1;
            if (ManagerHelper.ShowDialog(viewModel, base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_CustomConditionEditor_Title), this.DialogService, null))
            {
                this.Expression = this.GetDisplayExpression(viewModel.Expression);
            }
        }

        protected virtual void UpdateConditionValueType()
        {
            this.ConditionValueType = this.CalcConditionValueType();
        }

        protected bool ValidateExpression()
        {
            string expression = this.Expression;
            return (!string.IsNullOrEmpty(expression) ? ConditionalFormattingDialogHelper.ValidateExpression(expression, this.MessageBoxService, this.columnInfo) : true);
        }

        public IList<FieldNameWrapper> FieldNames { get; private set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdateConditionValueType")]
        public virtual string FieldName { get; set; }

        public virtual string Expression { get; set; }

        public virtual FieldEditorMode FieldMode { get; set; }

        public virtual IDialogService DialogService =>
            null;

        public virtual IMessageBoxService MessageBoxService =>
            null;

        public virtual object MinValue { get; set; }

        public virtual object MaxValue { get; set; }

        public virtual DataValueType ConditionValueType { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FieldEditorOwner.<>c <>9 = new FieldEditorOwner.<>c();
            public static Func<DataValueType> <>9__43_0;
            public static Func<DataValueType> <>9__43_1;
            public static Func<DataValueType> <>9__43_2;

            internal DataValueType <CalcConditionValueType>b__43_0() => 
                DataValueType.DateTime;

            internal DataValueType <CalcConditionValueType>b__43_1() => 
                DataValueType.Numeric;

            internal DataValueType <CalcConditionValueType>b__43_2() => 
                DataValueType.Numeric;
        }

        private class EditorDataColumnInfoWrapper : IDataColumnInfo
        {
            private readonly IDataColumnInfo column;
            private readonly string expression;

            public EditorDataColumnInfoWrapper(IDataColumnInfo column, string expression)
            {
                this.column = column;
                this.expression = (expression == null) ? string.Empty : expression;
            }

            string IDataColumnInfo.Caption =>
                this.column.Caption;

            List<IDataColumnInfo> IDataColumnInfo.Columns =>
                this.column.Columns;

            DataControllerBase IDataColumnInfo.Controller =>
                this.column.Controller;

            string IDataColumnInfo.FieldName =>
                this.column.FieldName;

            Type IDataColumnInfo.FieldType =>
                this.column.FieldType;

            string IDataColumnInfo.Name =>
                this.column.Name;

            string IDataColumnInfo.UnboundExpression =>
                this.expression;
        }

        private class EditorViewModelWrapper
        {
            public string Expression { get; set; }

            public IDataColumnInfo ColumnInfo { get; set; }
        }
    }
}

