namespace DevExpress.XtraPrinting.Export.Text
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Reflection;

    internal class TextLayoutGroup
    {
        private ArrayList list = new ArrayList();
        private bool decomposed;
        private float minCenterX = float.MaxValue;
        private float minCenterY = float.MaxValue;
        private int startIndex;

        public TextLayoutGroup(bool decomposed)
        {
            this.decomposed = decomposed;
        }

        public void Add(TextLayoutItem item)
        {
            this.RecalcMinCenters(item);
            this.list.Add(item);
        }

        public void Insert(int index, TextLayoutItem item)
        {
            this.RecalcMinCenters(item);
            if (index < this.Count)
            {
                this.list.Insert(index, item);
            }
            else
            {
                this.list.Add(item);
            }
        }

        private void RecalcMinCenters(TextLayoutItem item)
        {
            PointF tf = RectHelper.CenterOf((RectangleF) item.Owner.Bounds);
            this.minCenterX = Math.Min(this.minCenterX, tf.X);
            this.minCenterY = Math.Min(this.minCenterY, tf.Y);
        }

        public void RemoveAt(int index)
        {
            this.startIndex = ((index != this.startIndex) || (this.startIndex == -1)) ? -1 : (this.startIndex + 1);
            this.list[index] = null;
        }

        public void Sort(IComparer comparer)
        {
            this.list.Sort(comparer);
        }

        public int StartIndex =>
            (this.startIndex >= 0) ? this.startIndex : 0;

        public int Count =>
            this.list.Count;

        public TextLayoutItem this[int index] =>
            (TextLayoutItem) this.list[index];

        public bool Decomposed =>
            this.decomposed;

        public float MinCenterX =>
            this.minCenterX;

        public float MinCenterY =>
            this.minCenterY;
    }
}

