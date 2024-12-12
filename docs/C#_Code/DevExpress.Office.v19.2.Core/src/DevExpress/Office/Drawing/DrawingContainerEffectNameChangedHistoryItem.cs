namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;

    public class DrawingContainerEffectNameChangedHistoryItem : DrawingHistoryItem<ContainerEffect, string>
    {
        public DrawingContainerEffectNameChangedHistoryItem(IDocumentModelPart documentModelPart, ContainerEffect owner, string oldValue, string newValue) : base(documentModelPart, owner, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetNameCore(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetNameCore(base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(base.OldValue);
            writer.Write(base.NewValue);
        }
    }
}

