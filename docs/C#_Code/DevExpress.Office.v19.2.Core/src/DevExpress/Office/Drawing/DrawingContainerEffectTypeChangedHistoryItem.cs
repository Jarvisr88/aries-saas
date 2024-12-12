namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;

    public class DrawingContainerEffectTypeChangedHistoryItem : DrawingHistoryItem<ContainerEffect, DrawingEffectContainerType>
    {
        public DrawingContainerEffectTypeChangedHistoryItem(IDocumentModelPart documentModelPart, ContainerEffect owner, DrawingEffectContainerType oldValue, DrawingEffectContainerType newValue) : base(documentModelPart, owner, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetTypeCore(base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetTypeCore(base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write((byte) base.OldValue);
            writer.Write((byte) base.NewValue);
        }
    }
}

