namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.DrawingML;
    using DevExpress.Office.History;
    using System;

    public class Scene3DVectorCoordinateChangeHistoryItem : DrawingHistoryItem<Scene3DVector, long>
    {
        public Scene3DVectorCoordinateChangeHistoryItem(Scene3DVector owner, int index, long oldValue, long newValue) : base(owner.DocumentModel.MainPart, owner, index, oldValue, newValue)
        {
        }

        protected override void RedoCore()
        {
            base.Owner.SetCoordinateCore(base.Index, base.NewValue);
        }

        protected override void UndoCore()
        {
            base.Owner.SetCoordinateCore(base.Index, base.OldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.WriteObject(base.OldValue);
            writer.WriteObject(base.NewValue);
        }
    }
}

