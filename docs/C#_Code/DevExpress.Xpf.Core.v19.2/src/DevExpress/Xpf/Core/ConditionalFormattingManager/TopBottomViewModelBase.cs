namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;

    public abstract class TopBottomViewModelBase : FormatEditorOwnerViewModel
    {
        protected TopBottomViewModelBase(IDialogContext context) : base(context)
        {
        }

        protected override void AddChanges(ExpressionEditUnit unit)
        {
            base.AddChanges(unit);
            TopBottomEditUnit unit2 = unit as TopBottomEditUnit;
            if (unit2 != null)
            {
                unit2.Rule = this.GetRule();
                unit2.FieldName = base.Context.ColumnInfo.FieldName;
            }
        }

        protected override ExpressionEditUnit CreateEditUnit() => 
            new TopBottomEditUnit();

        protected abstract TopBottomRule GetRule();
        protected override void InitCore(ExpressionEditUnit unit)
        {
            base.InitCore(unit);
            (unit as TopBottomEditUnit).Do<TopBottomEditUnit>(x => this.InitRule(x.Rule, x.Threshold));
        }

        protected abstract void InitRule(TopBottomRule rule, double threshold);
    }
}

