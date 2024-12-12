namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;

    public class ModelShapeGuideList : ModelObjectUndoableCollection<ModelShapeGuide>, ICloneable<ModelShapeGuideList>, ISupportsCopyFrom<ModelShapeGuideList>
    {
        public ModelShapeGuideList(IDocumentModelPart documentModelPart) : base(documentModelPart)
        {
        }

        public ModelShapeGuideList Clone()
        {
            ModelShapeGuideList list = new ModelShapeGuideList(base.DocumentModelPart);
            list.CopyFrom(this);
            return list;
        }

        public void CopyFrom(ModelShapeGuideList value)
        {
            Guard.ArgumentNotNull(value, "ModelShapeGuideList");
            this.Clear();
            foreach (ModelShapeGuide guide in value)
            {
                this.AddInternal(guide.Clone());
            }
        }

        public override ModelShapeGuide DeserializeItem(IHistoryReader reader) => 
            reader.GetObject(reader.ReadInt32()) as ModelShapeGuide;
    }
}

