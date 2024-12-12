namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class IndexCalculator
    {
        private readonly DocumentViewerPanel documentViewerPanel;

        public IndexCalculator(DocumentViewerPanel panel)
        {
            this.ItemSizes = new Dictionary<int, Size>();
            this.documentViewerPanel = panel;
        }

        public Size GetItemSize(int index) => 
            this.HasItemSize(index) ? ((!this.Panel.ActualShowSingleItem || !this.Panel.ViewportHeight.GreaterThanOrClose(this.ItemSizes[index].Height)) ? this.ItemSizes[index] : new Size(this.ItemSizes[index].Width, this.Panel.ViewportHeight)) : Size.Empty;

        public Size GetRealItemSize(int index) => 
            this.HasItemSize(index) ? this.ItemSizes[index] : Size.Empty;

        public bool HasItemSize(int index) => 
            this.ItemSizes.ContainsKey(index);

        public double IndexToVerticalOffset(int index, bool showSingleItem)
        {
            if (showSingleItem)
            {
                double num2 = this.Panel.ExtentHeight / (this.Panel.ExtentHeight - this.Panel.ViewportHeight);
                return (!this.GetRealItemSize(index).Height.LessThanOrClose(this.Panel.ViewportHeight) ? (this.IndexToVerticalOffset(index, false) / num2) : this.IndexToVerticalOffset(index, false));
            }
            double num = 0.0;
            for (int i = 0; i < index; i++)
            {
                if (!this.HasItemSize(i))
                {
                    return double.PositiveInfinity;
                }
                num += this.GetItemSize(i).Height;
            }
            return num;
        }

        public void SetItemSize(int index, Size size)
        {
            if (this.ItemSizes.ContainsKey(index))
            {
                this.ItemSizes[index] = size;
            }
            else
            {
                this.ItemSizes.Add(index, size);
            }
        }

        public int VerticalOffsetToIndex(double offset)
        {
            double num = offset;
            int index = 0;
            while (this.HasItemSize(index) && num.GreaterThanOrClose(0.0))
            {
                num -= this.GetItemSize(index++).Height;
            }
            return (!num.AreClose(0.0) ? (!num.GreaterThan(0.0) ? (index - 1) : -1) : index);
        }

        protected IDictionary<int, Size> ItemSizes { get; set; }

        protected internal DocumentViewerPanel Panel =>
            this.documentViewerPanel;
    }
}

