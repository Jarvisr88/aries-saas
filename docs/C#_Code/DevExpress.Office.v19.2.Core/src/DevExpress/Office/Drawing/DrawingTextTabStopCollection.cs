namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;

    public class DrawingTextTabStopCollection : UndoableClonableCollection<DrawingTextTabStop>
    {
        public DrawingTextTabStopCollection(IDocumentModel documentModel) : base(documentModel.MainPart)
        {
        }

        public void ApplyTextTabStops()
        {
        }

        public override DrawingTextTabStop DeserializeItem(IHistoryReader reader) => 
            reader.GetObject(reader.ReadInt32()) as DrawingTextTabStop;

        public override DrawingTextTabStop GetCloneItem(DrawingTextTabStop item, IDocumentModelPart documentModelPart) => 
            item.Clone();

        public override UndoableClonableCollection<DrawingTextTabStop> GetNewCollection(IDocumentModelPart documentModelPart) => 
            new DrawingTextTabStopCollection(documentModelPart.DocumentModel);
    }
}

