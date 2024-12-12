namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;
    using System.Drawing;

    public class ColorTransformCollection : UndoableClonableCollection<ColorTransformBase>
    {
        private readonly InvalidateProxy innerParent;

        public ColorTransformCollection(IDocumentModel documentModel) : base(documentModel.MainPart)
        {
            this.innerParent = new InvalidateProxy();
        }

        public Color ApplyTransform(Color color)
        {
            for (int i = 0; i < base.Count; i++)
            {
                color = base.InnerList[i].ApplyTransform(color);
            }
            return color;
        }

        public override ColorTransformBase DeserializeItem(IHistoryReader reader) => 
            reader.GetObject(reader.ReadInt32()) as ColorTransformBase;

        public override ColorTransformBase GetCloneItem(ColorTransformBase item, IDocumentModelPart documentModelPart) => 
            item.Clone();

        public override UndoableClonableCollection<ColorTransformBase> GetNewCollection(IDocumentModelPart documentModelPart) => 
            new ColorTransformCollection(documentModelPart.DocumentModel);

        public ISupportsInvalidate Parent
        {
            get => 
                this.innerParent.Target;
            set => 
                this.innerParent.Target = value;
        }
    }
}

