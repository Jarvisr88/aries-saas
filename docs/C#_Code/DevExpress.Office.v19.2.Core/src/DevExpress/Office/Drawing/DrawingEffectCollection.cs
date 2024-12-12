namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;

    public class DrawingEffectCollection : UndoableClonableCollection<IDrawingEffect>
    {
        public DrawingEffectCollection(IDocumentModel documentModel) : base(documentModel.MainPart)
        {
        }

        public void ApplyEffects(IDrawingEffectVisitor visitor)
        {
            int count = base.Count;
            for (int i = 0; i < count; i++)
            {
                base.InnerList[i].Visit(visitor);
            }
        }

        public override IDrawingEffect DeserializeItem(IHistoryReader reader) => 
            reader.GetObject(reader.ReadInt32()) as IDrawingEffect;

        public override IDrawingEffect GetCloneItem(IDrawingEffect item, IDocumentModelPart documentModelPart) => 
            item.CloneTo(documentModelPart.DocumentModel);

        public override UndoableClonableCollection<IDrawingEffect> GetNewCollection(IDocumentModelPart documentModelPart) => 
            new DrawingEffectCollection(documentModelPart.DocumentModel);
    }
}

