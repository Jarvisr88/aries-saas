namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public class ElementSizers : ElementPool<ElementSizer>
    {
        private double _SizingAreaWidth;

        public ElementSizers(PanelBase container) : base(container)
        {
        }

        public ElementSizer Add(FrameworkElement element, Side side)
        {
            ElementSizer sizer = base.Add();
            sizer.Element = element;
            sizer.Side = side;
            sizer.UpdateBounds();
            return sizer;
        }

        protected override ElementSizer CreateItem()
        {
            ElementSizer sizer = base.CreateItem();
            sizer.SizingAreaWidth = this.SizingAreaWidth;
            return sizer;
        }

        public double SizingAreaWidth
        {
            get => 
                this._SizingAreaWidth;
            set
            {
                value = Math.Max(0.0, value);
                if (this._SizingAreaWidth != value)
                {
                    this._SizingAreaWidth = value;
                    foreach (ElementSizer sizer in base.Items)
                    {
                        sizer.SizingAreaWidth = this.SizingAreaWidth;
                    }
                }
            }
        }
    }
}

