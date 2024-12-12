namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;

    public class DrawingContainerEffectHasEffectsListChangedHistoryItem : DrawingHistoryItem<ContainerEffect, bool>
    {
        public DrawingContainerEffectHasEffectsListChangedHistoryItem(IDocumentModelPart documentModelPart, ContainerEffect owner, bool oldValue, bool newValue) : base(documentModelPart, owner, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetHasEffectsListCore(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetHasEffectsListCore(base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(base.OldValue);
            writer.Write(base.NewValue);
        }
    }
}

