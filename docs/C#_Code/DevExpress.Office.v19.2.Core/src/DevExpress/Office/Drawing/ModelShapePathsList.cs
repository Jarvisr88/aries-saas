namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;

    public class ModelShapePathsList : UndoableCollection<ModelShapePath>, ICloneable<ModelShapePathsList>, ISupportsCopyFrom<ModelShapePathsList>
    {
        public ModelShapePathsList(IDocumentModelPart documentModelPart) : base(documentModelPart)
        {
        }

        public ModelShapePathsList Clone()
        {
            ModelShapePathsList list = new ModelShapePathsList(base.DocumentModelPart);
            list.CopyFrom(this);
            return list;
        }

        public void CopyFrom(ModelShapePathsList value)
        {
            Guard.ArgumentNotNull(value, "ModelShapePathsList");
            this.Clear();
            foreach (ModelShapePath path in value)
            {
                this.AddInternal(path.Clone());
            }
        }

        public override ModelShapePath DeserializeItem(IHistoryReader reader) => 
            reader.GetObject(reader.ReadInt32()) as ModelShapePath;
    }
}

