namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using System;

    public class ModelShapePathFillModeHistoryItem : HistoryItem
    {
        private readonly ModelShapePath shapePath;
        private readonly PathFillMode oldValue;
        private readonly PathFillMode newValue;

        public ModelShapePathFillModeHistoryItem(ModelShapePath shapePath, PathFillMode oldValue, PathFillMode newValue) : base(shapePath.DocumentModelPart)
        {
            this.shapePath = shapePath;
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        public override object GetTargetObject() => 
            this.shapePath;

        protected override void RedoCore()
        {
            this.shapePath.SetFillMode(this.newValue);
        }

        protected override void UndoCore()
        {
            this.shapePath.SetFillMode(this.oldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write((byte) this.oldValue);
            writer.Write((byte) this.newValue);
        }
    }
}

