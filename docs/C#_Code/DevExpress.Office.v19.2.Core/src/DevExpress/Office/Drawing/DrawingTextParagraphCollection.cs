namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;
    using System.Collections.Generic;

    public class DrawingTextParagraphCollection : UndoableClonableCollection<DrawingTextParagraph>
    {
        private readonly InvalidateProxy innerParent;

        public DrawingTextParagraphCollection(IDocumentModel documentModel) : base(documentModel.MainPart)
        {
            this.innerParent = new InvalidateProxy();
        }

        public override int AddCore(DrawingTextParagraph item)
        {
            int num = base.AddCore(item);
            item.Parent = this.innerParent;
            this.innerParent.Invalidate();
            return num;
        }

        public override void AddRangeCore(IEnumerable<DrawingTextParagraph> collection)
        {
            foreach (DrawingTextParagraph paragraph in this)
            {
                paragraph.Parent = this.innerParent;
            }
            base.AddRangeCore(collection);
            this.innerParent.Invalidate();
        }

        public override void ClearCore()
        {
            if (base.Count != 0)
            {
                foreach (DrawingTextParagraph paragraph in this)
                {
                    paragraph.Parent = null;
                }
                base.ClearCore();
                this.innerParent.Invalidate();
            }
        }

        public override DrawingTextParagraph DeserializeItem(IHistoryReader reader) => 
            reader.GetObject(reader.ReadInt32()) as DrawingTextParagraph;

        public override DrawingTextParagraph GetCloneItem(DrawingTextParagraph item, IDocumentModelPart documentModelPart) => 
            item.CloneTo(documentModelPart.DocumentModel);

        public override UndoableClonableCollection<DrawingTextParagraph> GetNewCollection(IDocumentModelPart documentModelPart) => 
            new DrawingTextParagraphCollection(documentModelPart.DocumentModel);

        protected internal override void InsertCore(int index, DrawingTextParagraph item)
        {
            base.InsertCore(index, item);
            item.Parent = this.innerParent;
            this.innerParent.Invalidate();
        }

        public override void RemoveAtCore(int index)
        {
            base[index].Parent = null;
            base.RemoveAtCore(index);
            this.innerParent.Invalidate();
        }

        public ISupportsInvalidate Parent
        {
            get => 
                this.innerParent.Target;
            set => 
                this.innerParent.Target = value;
        }
    }
}

