namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows.Input;

    public class ProgressBarEditStrategy : RangeBaseEditStrategyBase
    {
        public ProgressBarEditStrategy(BaseEdit editor) : base(editor)
        {
        }

        public override string CoerceDisplayText(string displayText)
        {
            this.UpdateDisplayValue();
            string str = base.IsInSupportInitialize ? string.Empty : this.GetDisplayText();
            return base.CoerceDisplayText(str);
        }

        public virtual void ContentDisplayModeChanged(ContentDisplayMode value)
        {
        }

        protected override object GetValueForDisplayText() => 
            this.Info.DisplayValue;

        public virtual void IsPercentChanged(bool value)
        {
            this.UpdateDisplayText();
        }

        protected internal override bool ShouldProcessNullInput(KeyEventArgs e) => 
            false;

        protected override void UpdateDisplayValue()
        {
            double num = ObjectToDoubleConverter.TryConvert(base.ValueContainer.EditValue);
            this.Info.DisplayValue = this.Editor.IsPercent ? ((num - this.Editor.Minimum) / base.GetRange()) : num;
        }

        private ProgressBarEdit Editor =>
            base.Editor as ProgressBarEdit;
    }
}

