namespace DevExpress.XtraPrinting.Export
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class RectangleDivider
    {
        private Rectangle bounds;
        private List<int> xCoords;
        private List<int> yCoords;
        private List<Rectangle> innerAreas;
        private bool finalized;

        public RectangleDivider(Rectangle bounds)
        {
            this.bounds = bounds;
            this.xCoords = new List<int>();
            this.yCoords = new List<int>();
            this.innerAreas = new List<Rectangle>();
            this.AddPoint(bounds.Left, bounds.Top);
            this.AddPoint(bounds.Right, bounds.Bottom);
        }

        public void AddInnerArea(Rectangle area, bool onlyTopLeft)
        {
            this.innerAreas.Add(area);
            this.AddPoint(area.Left, area.Top);
            if (!onlyTopLeft)
            {
                this.AddPoint(area.Right, area.Bottom);
            }
        }

        public void AddInnerAreas(Rectangle[] areas, bool onlyTopLeft)
        {
            foreach (Rectangle rectangle in areas)
            {
                this.AddInnerArea(rectangle, onlyTopLeft);
            }
        }

        private void AddPoint(int xCoord, int yCoord)
        {
            this.xCoords.Add(xCoord);
            this.yCoords.Add(yCoord);
            this.finalized = false;
        }

        public List<Rectangle> GenerateEmptyAreas()
        {
            List<Rectangle> list = new List<Rectangle>();
            this.ProcessData();
            int count = this.yCoords.Count;
            int num2 = 1;
            while (num2 < count)
            {
                int height = this.yCoords[num2] - this.yCoords[num2 - 1];
                int num4 = this.xCoords.Count;
                int num5 = 1;
                while (true)
                {
                    if (num5 >= num4)
                    {
                        num2++;
                        break;
                    }
                    int width = this.xCoords[num5] - this.xCoords[num5 - 1];
                    Rectangle rect = new Rectangle(this.xCoords[num5 - 1], this.yCoords[num2 - 1], width, height);
                    bool flag = false;
                    int num7 = this.innerAreas.Count;
                    int num8 = 0;
                    while (true)
                    {
                        if (num8 < num7)
                        {
                            Rectangle rectangle2 = this.innerAreas[num8];
                            if (!rectangle2.Contains(rect))
                            {
                                num8++;
                                continue;
                            }
                            flag = true;
                        }
                        if (!flag)
                        {
                            list.Add(rect);
                        }
                        num5++;
                        break;
                    }
                }
            }
            return list;
        }

        private static List<int> GetIntervals(List<int> bounds)
        {
            List<int> list = new List<int>();
            int num = bounds.Count - 1;
            for (int i = 0; i < num; i++)
            {
                list.Add(bounds[i + 1] - bounds[i]);
            }
            return list;
        }

        private static void MakeArrayListUnique(List<int> arrayList)
        {
            if (arrayList.Count != 0)
            {
                int index = 0;
                int count = arrayList.Count;
                for (int i = 1; i < count; i++)
                {
                    if (arrayList[i] != arrayList[index])
                    {
                        arrayList[++index] = arrayList[i];
                    }
                }
                index++;
                int num3 = count - index;
                if (num3 != 0)
                {
                    arrayList.RemoveRange(index, num3);
                }
            }
        }

        private void ProcessData()
        {
            if (!this.finalized)
            {
                this.xCoords.Sort();
                this.yCoords.Sort();
                MakeArrayListUnique(this.xCoords);
                MakeArrayListUnique(this.yCoords);
                this.finalized = true;
            }
        }

        public List<int> XCoords
        {
            get
            {
                this.ProcessData();
                return this.xCoords;
            }
        }

        public List<int> YCoords
        {
            get
            {
                this.ProcessData();
                return this.yCoords;
            }
        }

        public List<int> ColWidths =>
            GetIntervals(this.XCoords);

        public List<int> RowHeights =>
            GetIntervals(this.YCoords);
    }
}

