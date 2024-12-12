namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;
    using System.Collections.Generic;

    public class GradientStopCollection : UndoableCollection<DrawingGradientStop>
    {
        private readonly InvalidateProxy innerParent;

        public GradientStopCollection(IDocumentModel documentModel) : base(documentModel.MainPart)
        {
            this.innerParent = new InvalidateProxy();
        }

        public override int AddCore(DrawingGradientStop item)
        {
            int num = base.AddCore(item);
            item.Parent = this.innerParent;
            this.innerParent.Invalidate();
            return num;
        }

        public override void AddRangeCore(IEnumerable<DrawingGradientStop> collection)
        {
            foreach (DrawingGradientStop stop in collection)
            {
                stop.Parent = this.innerParent;
            }
            base.AddRangeCore(collection);
            this.innerParent.Invalidate();
        }

        internal void AddStopsFromInfoes(IList<GradientStopInfo> infoes)
        {
            int count = infoes.Count;
            for (int i = 0; i < count; i++)
            {
                GradientStopInfo info = infoes[i];
                base.InnerList.Add(info.CreateStop(base.DocumentModel));
            }
        }

        public override void ClearCore()
        {
            if (base.Count != 0)
            {
                foreach (DrawingGradientStop stop in this)
                {
                    stop.Parent = null;
                }
                base.ClearCore();
                this.innerParent.Invalidate();
            }
        }

        public override DrawingGradientStop DeserializeItem(IHistoryReader reader) => 
            reader.GetObject(reader.ReadInt32()) as DrawingGradientStop;

        protected internal override void InsertCore(int index, DrawingGradientStop item)
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

        protected internal ISupportsInvalidate Parent
        {
            get => 
                this.innerParent.Target;
            set => 
                this.innerParent.Target = value;
        }
    }
}

