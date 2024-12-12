namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using System;

    public class DateEditRangeService : RangeEditorService
    {
        public DateEditRangeService(BaseEdit editor) : base(editor)
        {
        }

        public override object CorrectToBounds(object maskValue) => 
            this.EditStrategy.Correct(maskValue);

        public override bool InRange(object maskValue) => 
            this.EditStrategy.InRange(maskValue);

        public override bool ShouldRoundToBounds =>
            this.EditStrategy.ShouldRoundToBounds;

        private DateEditStrategy EditStrategy =>
            (DateEditStrategy) base.EditStrategy;
    }
}

