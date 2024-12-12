namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;

    public class ModelAdjustHandlesList : ModelObjectUndoableCollection<AdjustablePoint>, ICloneable<ModelAdjustHandlesList>, ISupportsCopyFrom<ModelAdjustHandlesList>
    {
        public ModelAdjustHandlesList(IDocumentModelPart documentModelPart) : base(documentModelPart)
        {
        }

        public ModelAdjustHandlesList Clone()
        {
            ModelAdjustHandlesList list = new ModelAdjustHandlesList(base.DocumentModelPart);
            list.CopyFrom(this);
            return list;
        }

        public void CopyFrom(ModelAdjustHandlesList value)
        {
            Guard.ArgumentNotNull(value, "ModelAdjustHandlesList");
            this.Clear();
            foreach (AdjustablePoint point in value)
            {
                this.AddInternal(point.Clone());
            }
        }

        public override AdjustablePoint DeserializeItem(IHistoryReader reader) => 
            reader.GetObject(reader.ReadInt32()) as AdjustablePoint;
    }
}

