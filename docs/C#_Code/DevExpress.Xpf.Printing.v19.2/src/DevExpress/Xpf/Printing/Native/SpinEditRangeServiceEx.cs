namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Services;
    using System;

    public class SpinEditRangeServiceEx : SpinEditRangeService
    {
        public SpinEditRangeServiceEx(BaseEdit editor) : base(editor)
        {
        }

        private decimal GetMaxValue() => 
            (this.OwnerEdit.MaxValue == null) ? decimal.MaxValue : this.OwnerEdit.MaxValue.Value;

        private decimal GetMinValue() => 
            (this.OwnerEdit.MinValue == null) ? decimal.MinValue : this.OwnerEdit.MinValue.Value;

        public override bool SpinDown(object value, IMaskManagerProvider maskProvider)
        {
            if (!base.EditingSettings.AllowSpin)
            {
                return false;
            }
            try
            {
                decimal baseValue = this.EditStrategy.CreateValueConverter(value).Value - this.OwnerEdit.Increment;
                baseValue = this.EditStrategy.CreateValueConverter(this.EditStrategy.Correct(this.EditStrategy.ToRange(baseValue))).Value;
                System.Type editValueType = this.OwnerEdit.EditValueType;
                if (Nullable.GetUnderlyingType(this.OwnerEdit.EditValueType) != null)
                {
                    editValueType = Nullable.GetUnderlyingType(this.OwnerEdit.EditValueType);
                }
                maskProvider.SetMaskManagerValue(Convert.ChangeType(baseValue, editValueType));
                return true;
            }
            catch
            {
                maskProvider.SetMaskManagerValue(this.GetMinValue());
                return true;
            }
        }

        public override bool SpinUp(object value, IMaskManagerProvider maskProvider)
        {
            if (!base.EditingSettings.AllowSpin)
            {
                return false;
            }
            try
            {
                decimal baseValue = this.EditStrategy.CreateValueConverter(value).Value + this.OwnerEdit.Increment;
                baseValue = this.EditStrategy.CreateValueConverter(this.EditStrategy.Correct(this.EditStrategy.ToRange(baseValue))).Value;
                System.Type editValueType = this.OwnerEdit.EditValueType;
                if (Nullable.GetUnderlyingType(this.OwnerEdit.EditValueType) != null)
                {
                    editValueType = Nullable.GetUnderlyingType(this.OwnerEdit.EditValueType);
                }
                maskProvider.SetMaskManagerValue(Convert.ChangeType(baseValue, editValueType));
                return true;
            }
            catch
            {
                maskProvider.SetMaskManagerValue(this.GetMaxValue());
                return true;
            }
        }

        private SpinEdit OwnerEdit =>
            (SpinEdit) base.OwnerEdit;

        private SpinEditStrategy EditStrategy =>
            (SpinEditStrategy) base.EditStrategy;
    }
}

