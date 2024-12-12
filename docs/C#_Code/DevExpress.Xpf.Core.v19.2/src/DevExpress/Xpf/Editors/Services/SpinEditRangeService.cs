namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using System;

    public class SpinEditRangeService : RangeEditorService
    {
        public SpinEditRangeService(BaseEdit editor) : base(editor)
        {
        }

        public override object CorrectToBounds(object maskValue) => 
            this.EditStrategy.Correct(maskValue);

        public override bool InRange(object maskValue) => 
            this.EditStrategy.InRange(maskValue);

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
                maskProvider.SetMaskManagerValue(baseValue);
                return true;
            }
            catch
            {
                maskProvider.SetMaskManagerValue(this.EditStrategy.GetMinValue());
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
                maskProvider.SetMaskManagerValue(baseValue);
                return true;
            }
            catch
            {
                maskProvider.SetMaskManagerValue(this.EditStrategy.GetMaxValue());
                return true;
            }
        }

        public override bool ShouldRoundToBounds =>
            this.EditStrategy.ShouldRoundToBounds;

        private SpinEditStrategy EditStrategy =>
            (SpinEditStrategy) base.EditStrategy;

        private SpinEdit OwnerEdit =>
            (SpinEdit) base.OwnerEdit;
    }
}

