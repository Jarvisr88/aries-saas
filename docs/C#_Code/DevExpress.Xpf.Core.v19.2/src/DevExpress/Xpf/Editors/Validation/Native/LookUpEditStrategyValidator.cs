namespace DevExpress.Xpf.Editors.Validation.Native
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.EditStrategy;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class LookUpEditStrategyValidator : StrategyValidatorBase
    {
        public LookUpEditStrategyValidator(LookUpEditStrategyBase strategy, SelectorPropertiesCoercionHelper selectorUpdater, LookUpEditBase editor) : base(editor)
        {
            this.Strategy = strategy;
            this.SelectorUpdater = selectorUpdater;
        }

        public override string GetValidationError() => 
            "hidden error";

        private bool IndexFromItemsSource(int index) => 
            (index > -1) && !this.Strategy.IsInProcessNewValueDialog;

        protected override bool IsValidValue(object value, object convertedValue)
        {
            LookUpEditableItem item = value as LookUpEditableItem;
            return (((item == null) || (item.Completed || !item.AsyncLoading)) ? base.IsValidValue(value, convertedValue) : false);
        }

        public override object ProcessConversion(object value, UpdateEditorSource updateEditorSource) => 
            !this.Editor.IsTokenMode ? this.ProcessSingleConversion(value, updateEditorSource) : this.ProcessMultipleConversion(value, updateEditorSource);

        private object ProcessMultipleConversion(object editValue, UpdateEditorSource updateEditorSource)
        {
            IList<object> list = null;
            list = !(editValue is LookUpEditableItem) ? (editValue as IList<object>) : (((LookUpEditableItem) editValue).EditValue as IList<object>);
            if (list == null)
            {
                object item = this.ProcessSingleConversion(editValue, updateEditorSource);
                if (item == null)
                {
                    return null;
                }
                List<object> list1 = new List<object>();
                list1.Add(item);
                return list1;
            }
            List<object> list2 = new List<object>();
            foreach (object obj3 in list)
            {
                object item = this.ProcessSingleConversion(obj3, updateEditorSource);
                if ((item != null) && !string.IsNullOrEmpty(item.ToString()))
                {
                    list2.Add(item);
                }
            }
            return ((list2.Count > 0) ? list2 : null);
        }

        private object ProcessSingleConversion(object value, UpdateEditorSource updateEditorSource)
        {
            LookUpEditableItem item = value as LookUpEditableItem;
            if ((item == null) && !this.Strategy.ShouldRaiseProcessValueConversion)
            {
                return base.ProcessConversion(value, updateEditorSource);
            }
            if (item == null)
            {
                return value;
            }
            string text = Convert.ToString(item.DisplayValue);
            object editValue = item.EditValue;
            if (item.Completed)
            {
                return editValue;
            }
            if (!this.Strategy.IsInLookUpMode)
            {
                return base.ProcessConversion(editValue, updateEditorSource);
            }
            int index = item.ForbidFindIncremental ? this.SelectorUpdater.GetIndexFromEditValue(item.EditValue) : this.Strategy.FindItemIndexByText(text, !this.Strategy.ShouldRaiseProcessValueConversion && this.Editor.AutoComplete);
            if (!this.IndexFromItemsSource(index) && (this.Strategy.ShouldRaiseProcessValueConversion && !item.ProcessNewValueCompleted))
            {
                item.ProcessNewValueCompleted = true;
                this.Strategy.ProcessNewValue(text);
                index = this.Strategy.FindItemIndexByText(text, false);
                if (!this.IndexFromItemsSource(index) && this.Editor.AutoComplete)
                {
                    index = this.Strategy.FindItemIndexByText(text, true);
                }
            }
            if (!this.IndexFromItemsSource(index))
            {
                return editValue;
            }
            ChangeTextItem item1 = new ChangeTextItem();
            item1.Text = text;
            this.Strategy.UpdateAutoSearchText(item1, true);
            return this.SelectorUpdater.GetEditValueFromSelectedIndex(index);
        }

        protected LookUpEditBase Editor =>
            base.Editor as LookUpEditBase;

        protected LookUpEditStrategyBase Strategy { get; private set; }

        private SelectorPropertiesCoercionHelper SelectorUpdater { get; set; }
    }
}

