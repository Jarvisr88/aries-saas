namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Services;
    using System;
    using System.Runtime.CompilerServices;

    public class SpinEditStrategy : RangeEditorStrategy<decimal>
    {
        public SpinEditStrategy(ButtonEdit editor) : base(editor)
        {
        }

        public decimal CoerceDecimalValue(decimal baseValue)
        {
            this.CoerceValue(SpinEdit.ValueProperty, baseValue);
            return baseValue;
        }

        public override object CoerceMaskType(MaskType maskType) => 
            MaskType.Numeric;

        public decimal? CoerceMaxValue(decimal? baseValue) => 
            baseValue;

        public decimal? CoerceMinValue(decimal? baseValue) => 
            baseValue;

        public override object Correct(object baseValue)
        {
            RangeValueConverter<decimal> converter = base.CreateValueConverter(base.Correct(baseValue));
            decimal num = converter.Value;
            decimal num2 = this.Editor.IsFloatValue ? converter.Value : Math.Round(converter.Value);
            if ((num2 < this.GetMinValue()) && (num >= this.GetMinValue()))
            {
                num2 += 1M;
            }
            if ((num2 > this.GetMaxValue()) && (num <= this.GetMaxValue()))
            {
                num2 -= 1M;
            }
            return num2;
        }

        protected override EditorSpecificValidator CreateEditorValidatorService() => 
            new SpinEditValidator(this.Editor);

        protected override MinMaxUpdateHelper CreateMinMaxHelper() => 
            new MinMaxUpdateHelper(this.Editor, SpinEdit.MinValueProperty, SpinEdit.MaxValueProperty);

        protected override RangeEditorService CreateRangeEditService() => 
            new SpinEditRangeService(this.Editor);

        protected override BaseEditingSettingsService CreateTextInputSettingsService() => 
            new ButtonEditSettingsService(this.Editor);

        protected override object GetDefaultValue() => 
            0M;

        protected internal override decimal GetMaxValue() => 
            (this.Editor.MaxValue == null) ? decimal.MaxValue : this.Editor.MaxValue.Value;

        protected internal override decimal GetMinValue() => 
            (this.Editor.MinValue == null) ? decimal.MinValue : this.Editor.MinValue.Value;

        protected internal virtual void OnIsFloatValueChanged()
        {
            this.UpdateEditMask();
            this.Editor.CoerceValue(SpinEdit.MinValueProperty);
            this.Editor.CoerceValue(SpinEdit.MaxValueProperty);
            base.SyncWithValue();
        }

        protected override void RegisterUpdateCallbacks()
        {
            base.RegisterUpdateCallbacks();
            PropertyCoercionHandler getBaseValueHandler = <>c.<>9__16_0;
            if (<>c.<>9__16_0 == null)
            {
                PropertyCoercionHandler local1 = <>c.<>9__16_0;
                getBaseValueHandler = <>c.<>9__16_0 = baseValue => baseValue;
            }
            base.PropertyUpdater.Register(SpinEdit.ValueProperty, getBaseValueHandler, new PropertyCoercionHandler(this.Correct));
        }

        protected internal void SyncWithDecimalValue(decimal oldValue, decimal value)
        {
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(SpinEdit.ValueProperty, oldValue, value);
            }
        }

        internal void UpdateEditMask()
        {
            string mask = this.Editor.Mask;
            if (this.IsFloatValue)
            {
                if (mask == "N00")
                {
                    mask = string.Empty;
                }
            }
            else if (string.IsNullOrEmpty(mask))
            {
                mask = "N00";
            }
            this.Editor.Mask = mask;
        }

        public override bool ShouldRoundToBounds =>
            this.Editor.AllowRoundOutOfRangeValue;

        protected SpinEdit Editor =>
            base.Editor as SpinEdit;

        private bool IsFloatValue =>
            this.Editor.IsFloatValue;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SpinEditStrategy.<>c <>9 = new SpinEditStrategy.<>c();
            public static PropertyCoercionHandler <>9__16_0;

            internal object <RegisterUpdateCallbacks>b__16_0(object baseValue) => 
                baseValue;
        }
    }
}

