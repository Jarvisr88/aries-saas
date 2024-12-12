namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;
    using System.Collections.Generic;

    public class DrawingTextRunCollection : UndoableClonableCollection<IDrawingTextRun>
    {
        private readonly InvalidateProxy innerParent;

        public DrawingTextRunCollection(IDocumentModel documentModel) : base(documentModel.MainPart)
        {
            this.innerParent = new InvalidateProxy();
        }

        public override int AddCore(IDrawingTextRun item)
        {
            int num = base.AddCore(item);
            item.Parent = this.innerParent;
            this.innerParent.Invalidate();
            return num;
        }

        public override void AddRangeCore(IEnumerable<IDrawingTextRun> collection)
        {
            foreach (IDrawingTextRun run in collection)
            {
                run.Parent = this.innerParent;
            }
            base.AddRangeCore(collection);
            this.innerParent.Invalidate();
        }

        public override void ClearCore()
        {
            if (base.Count != 0)
            {
                foreach (IDrawingTextRun run in this)
                {
                    run.Parent = null;
                }
                base.ClearCore();
                this.innerParent.Invalidate();
            }
        }

        public override IDrawingTextRun DeserializeItem(IHistoryReader reader) => 
            reader.GetObject(reader.ReadInt32()) as IDrawingTextRun;

        public override IDrawingTextRun GetCloneItem(IDrawingTextRun item, IDocumentModelPart documentModelPart) => 
            item.CloneTo(documentModelPart.DocumentModel);

        public override UndoableClonableCollection<IDrawingTextRun> GetNewCollection(IDocumentModelPart documentModelPart) => 
            new DrawingTextRunCollection(documentModelPart.DocumentModel);

        protected internal override void InsertCore(int index, IDrawingTextRun item)
        {
            base.InsertCore(index, item);
            item.Parent = this.innerParent;
            this.innerParent.Invalidate();
        }

        public override void RemoveAtCore(int index)
        {
            IDrawingTextRun run = base[index];
            base.RemoveAtCore(index);
            run.Parent = null;
            this.innerParent.Invalidate();
        }

        protected internal ISupportsInvalidate Parent
        {
            get => 
                this.innerParent.Target;
            set => 
                this.innerParent.Target = value;
        }
    }
}

