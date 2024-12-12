namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using System;

    public abstract class TopBottomConditionalFormattingDialogViewModel : ConditionalFormattingDialogViewModel
    {
        protected TopBottomConditionalFormattingDialogViewModel(IFormatsOwner owner, ConditionalFormattingStringId titleId, ConditionalFormattingStringId descriptionId, ConditionalFormattingStringId connectorId) : base(owner, titleId, descriptionId, connectorId)
        {
        }

        protected override BaseEditUnit CreateEditUnit(string fieldName)
        {
            TopBottomEditUnit unit = new TopBottomEditUnit {
                Rule = this.RuleKind,
                Threshold = Convert.ToDouble(this.Value)
            };
            if (base.ApplyFormatToWholeRow)
            {
                unit.ApplyToRow = true;
            }
            return unit;
        }

        internal override object GetInitialValue() => 
            10M;

        protected abstract TopBottomRule RuleKind { get; }
    }
}

