namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;

    public class ModelShapeConnectionList : ModelObjectUndoableCollection<ModelShapeConnection>, ICloneable<ModelShapeConnectionList>, ISupportsCopyFrom<ModelShapeConnectionList>
    {
        public ModelShapeConnectionList(IDocumentModelPart documentModelPart) : base(documentModelPart)
        {
        }

        public ModelShapeConnectionList Clone()
        {
            ModelShapeConnectionList list = new ModelShapeConnectionList(base.DocumentModelPart);
            list.CopyFrom(this);
            return list;
        }

        public void CopyFrom(ModelShapeConnectionList value)
        {
            Guard.ArgumentNotNull(value, "ModelShapeConnectionList");
            this.Clear();
            foreach (ModelShapeConnection connection in value)
            {
                this.AddInternal(connection.Clone());
            }
        }

        public override ModelShapeConnection DeserializeItem(IHistoryReader reader) => 
            reader.GetObject(reader.ReadInt32()) as ModelShapeConnection;
    }
}

