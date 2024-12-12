namespace DevExpress.Xpf.Editors.EditStrategy
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class CalcEditStrategy : PopupBaseEditStrategy
    {
        private bool isCalculatorValueChanged;

        public CalcEditStrategy(PopupCalcEdit editor) : base(editor)
        {
        }

        public virtual void AcceptCalculatorValue()
        {
            if (this.Calculator != null)
            {
                base.ValueContainer.SetEditValue(this.Calculator.Value, UpdateEditorSource.ValueChanging);
                this.SelectAll();
            }
        }

        public virtual void CalculatorAssigned(PopupCalcEditCalculator oldCalculator)
        {
            if (oldCalculator != null)
            {
                oldCalculator.ValueChanged -= new CalculatorValueChangedEventHandler(this.OnCalculatorValueChanged);
            }
            if (this.Calculator != null)
            {
                this.isCalculatorValueChanged = false;
                this.UpdateDataContext();
                this.Calculator.Value = base.ValueContainer.EditValue.TryConvertToDecimal();
                this.Calculator.ValueChanged += new CalculatorValueChangedEventHandler(this.OnCalculatorValueChanged);
            }
        }

        public decimal CoerceDecimalValue(decimal baseValue)
        {
            this.CoerceValue(PopupCalcEdit.ValueProperty, baseValue);
            return baseValue;
        }

        internal override void FlushPendingEditActions(UpdateEditorSource updateEditor)
        {
            base.FlushPendingEditActions(updateEditor);
            if ((this.Editor.EditMode != EditMode.Standalone) && this.Editor.IsPopupOpen)
            {
                this.Editor.ClosePopupOnClick();
            }
        }

        protected override string GetDisplayText() => 
            !this.IsDisplayTextFromCalculator ? base.GetDisplayText() : this.Calculator.DisplayText;

        protected virtual void OnCalculatorValueChanged(object sender, CalculatorValueChangedEventArgs e)
        {
            this.isCalculatorValueChanged = true;
        }

        protected override void RegisterUpdateCallbacks()
        {
            base.RegisterUpdateCallbacks();
            PropertyCoercionHandler getBaseValueHandler = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                PropertyCoercionHandler local1 = <>c.<>9__8_0;
                getBaseValueHandler = <>c.<>9__8_0 = baseValue => baseValue;
            }
            base.PropertyUpdater.Register(PopupCalcEdit.ValueProperty, getBaseValueHandler, <>c.<>9__8_1 ??= ((PropertyCoercionHandler) (baseValue => baseValue.TryConvertToDecimal())));
        }

        protected internal virtual void SyncWithDecimalValue(decimal oldValue, decimal value)
        {
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(PopupCalcEdit.ValueProperty, oldValue, value);
            }
        }

        protected override void SyncWithValueInternal()
        {
            base.SyncWithValueInternal();
            if (this.Calculator != null)
            {
                this.Calculator.Value = base.ValueContainer.EditValue.TryConvertToDecimal();
            }
        }

        private void UpdateDataContext()
        {
            DependencyObject dataContext = this.Calculator.DataContext as DependencyObject;
            if (dataContext == null)
            {
                dataContext = new Button();
                this.Calculator.DataContext = dataContext;
            }
            BaseEdit.SetOwnerEdit(dataContext, this.Editor);
        }

        public override void UpdateDisplayText()
        {
            base.UpdateDisplayText();
            if ((base.EditBox != null) && this.IsDisplayTextFromCalculator)
            {
                this.SelectAll();
            }
        }

        private PopupCalcEdit Editor =>
            (PopupCalcEdit) base.Editor;

        private DevExpress.Xpf.Editors.Calculator Calculator =>
            this.Editor.Calculator;

        private bool IsDisplayTextFromCalculator =>
            this.Editor.IsPopupOpen && (this.Calculator != null);

        public override bool IsValueChanged =>
            base.IsValueChanged || ((this.Editor.EditMode != EditMode.Standalone) && (this.Editor.IsPopupOpen && this.isCalculatorValueChanged));

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CalcEditStrategy.<>c <>9 = new CalcEditStrategy.<>c();
            public static PropertyCoercionHandler <>9__8_0;
            public static PropertyCoercionHandler <>9__8_1;

            internal object <RegisterUpdateCallbacks>b__8_0(object baseValue) => 
                baseValue;

            internal object <RegisterUpdateCallbacks>b__8_1(object baseValue) => 
                baseValue.TryConvertToDecimal();
        }
    }
}

