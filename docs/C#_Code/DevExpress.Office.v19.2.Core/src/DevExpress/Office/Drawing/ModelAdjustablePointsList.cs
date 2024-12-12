namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;

    public class ModelAdjustablePointsList : UndoableCollection<AdjustablePoint>, ICloneable<ModelAdjustablePointsList>, ISupportsCopyFrom<ModelAdjustablePointsList>
    {
        public ModelAdjustablePointsList(IDocumentModelPart documentModelPart) : base(documentModelPart)
        {
        }

        public ModelAdjustablePointsList Clone()
        {
            ModelAdjustablePointsList list = new ModelAdjustablePointsList(base.DocumentModelPart);
            list.CopyFrom(this);
            return list;
        }

        public void CopyFrom(ModelAdjustablePointsList value)
        {
            Guard.ArgumentNotNull(value, "AdjustablePointsList");
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

