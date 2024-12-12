namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class TabHeaderInsertHelper
    {
        private readonly Dictionary<Rect, IDockLayoutElement> elementMap = new Dictionary<Rect, IDockLayoutElement>();
        private readonly bool isHorizontal;
        private readonly double length;
        private readonly Rect[] rects;

        public TabHeaderInsertHelper(IDockLayoutContainer container, Point point, bool canInsertAfterAll)
        {
            if ((container != null) && container.HasHeadersPanel)
            {
                this.isHorizontal = container.IsHorizontalHeaders;
                this.HeaderPanel = MathHelper.Round(container.GetHeadersPanelBounds());
                this.Tab = MathHelper.Round(container.GetSelectedPageBounds());
                this.Tab = MathHelper.Edge(this.Tab, this.HeaderPanel, !this.isHorizontal);
                this.rects = this.GetRects(container);
                Rect[] rects = this.rects;
                int index = 0;
                while (true)
                {
                    if (index >= rects.Length)
                    {
                        this.Header = this.GetInsertRect(point);
                        break;
                    }
                    Rect rect = rects[index];
                    this.length = Math.Max(this.length, this.isHorizontal ? rect.Right : rect.Bottom);
                    index++;
                }
            }
        }

        private int Compare(Rect x, Rect y)
        {
            double num = this.isHorizontal ? x.Top : x.Left;
            double num2 = this.isHorizontal ? x.Right : x.Bottom;
            double num3 = this.isHorizontal ? y.Top : y.Left;
            double num4 = this.isHorizontal ? y.Right : y.Bottom;
            int num5 = num.CompareTo(num3);
            return ((num5 == 0) ? num2.CompareTo(num4) : num5);
        }

        private Rect GetInsertRect(Point point)
        {
            Rect rect2;
            this.TabIndex = -1;
            this.Index = -1;
            Rect headerPanel = this.HeaderPanel;
            if (!headerPanel.Contains(point) || !this.rects.Any<Rect>())
            {
                return new Rect();
            }
            Point point2 = new Point(point.X, point.Y);
            for (int i = 0; i < this.rects.Length; i++)
            {
                if (this.rects[i].Contains(point2))
                {
                    IDockLayoutElement element = this.elementMap[this.rects[i]];
                    this.TabIndex = LayoutItemsHelper.GetTabIndexFromItem(element.Item);
                    this.Index = LayoutItemsHelper.GetIndexFromItem(element.Item);
                    return MathHelper.Round(this.GetRect(this.isHorizontal ? this.rects[i].Left : this.rects[i].Top, this.isHorizontal ? this.rects[i].Width : this.rects[i].Height));
                }
            }
            double start = this.isHorizontal ? point.X : point.Y;
            if (start > this.length)
            {
                rect2 = this.rects.Last<Rect>();
                this.Index = this.TabIndex = this.rects.Length;
            }
            else
            {
                if (!this.rects.Any<Rect>(x => ((this.isHorizontal ? x.Right : x.Bottom) < start)))
                {
                    this.TabIndex = 0;
                    this.Index = this.TabIndex;
                    return MathHelper.Round(this.GetRect(this.isHorizontal ? this.HeaderPanel.Left : this.HeaderPanel.Top, this.isHorizontal ? this.rects.First<Rect>().Width : this.rects.First<Rect>().Height));
                }
                rect2 = this.rects.Last<Rect>(x => (this.isHorizontal ? x.Right : x.Bottom) < start);
                this.Index = this.TabIndex = this.rects.Length;
            }
            return MathHelper.Round(this.GetRect(this.isHorizontal ? rect2.Right : rect2.Bottom, this.isHorizontal ? rect2.Width : rect2.Height));
        }

        private Rect GetRect(double start, double length) => 
            new Rect(this.isHorizontal ? start : this.HeaderPanel.Left, this.isHorizontal ? this.HeaderPanel.Top : start, this.isHorizontal ? length : this.HeaderPanel.Width, this.isHorizontal ? this.HeaderPanel.Height : length);

        private Rect[] GetRects(ILayoutContainer container)
        {
            List<Rect> list = new List<Rect>();
            for (int i = 0; i < container.Items.Count; i++)
            {
                IDockLayoutElement element = container.Items[i] as IDockLayoutElement;
                if ((element != null) && element.IsPageHeader)
                {
                    Rect item = ElementHelper.GetRect(element);
                    list.Add(item);
                    this.elementMap[item] = element;
                }
            }
            list.Sort(new Comparison<Rect>(this.Compare));
            return list.ToArray();
        }

        public Rect Header { get; private set; }

        public Rect HeaderPanel { get; private set; }

        public int Index { get; private set; }

        public Rect Tab { get; private set; }

        public int TabIndex { get; private set; }
    }
}

