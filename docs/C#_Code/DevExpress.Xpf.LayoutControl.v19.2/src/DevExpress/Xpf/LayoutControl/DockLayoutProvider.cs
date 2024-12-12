namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DockLayoutProvider : LayoutProviderBase
    {
        public DockLayoutProvider(IDockLayoutModel model) : base(model)
        {
        }

        protected virtual void AddItemSizers(FrameworkElements items)
        {
            foreach (FrameworkElement element in items)
            {
                DevExpress.Xpf.Core.Side? itemSizingSide = this.GetItemSizingSide(element);
                if ((itemSizingSide != null) && this.IsItemSizeable(element, itemSizingSide.Value))
                {
                    this.Parameters.ItemSizers.Add(element, itemSizingSide.Value);
                }
            }
        }

        protected virtual void AlignBounds(ref Rect bounds, Rect area, Dock dock)
        {
            if ((dock == Dock.Left) || (dock == Dock.Right))
            {
                bounds.X = (dock != Dock.Left) ? (area.Right - bounds.Width) : area.Left;
                bounds.Y = area.Y;
                bounds.Height = area.Height;
            }
            else
            {
                bounds.X = area.X;
                bounds.Width = area.Width;
                if (dock == Dock.Top)
                {
                    bounds.Y = area.Top;
                }
                else
                {
                    bounds.Y = area.Bottom - bounds.Height;
                }
            }
        }

        protected DevExpress.Xpf.Core.Side? GetItemSizingSide(FrameworkElement item)
        {
            switch (DockLayoutControl.GetDock(item))
            {
                case Dock.Left:
                    return 2;

                case Dock.Top:
                    return 4;

                case Dock.Right:
                    return 0;

                case Dock.Bottom:
                    return 1;
            }
            return null;
        }

        protected bool IsItemSizeable(FrameworkElement item, DevExpress.Xpf.Core.Side side) => 
            ((side == DevExpress.Xpf.Core.Side.Left) || (side == DevExpress.Xpf.Core.Side.LeftRight)) ? DockLayoutControl.GetAllowHorizontalSizing(item) : DockLayoutControl.GetAllowVerticalSizing(item);

        protected override Size OnArrange(FrameworkElements items, Rect bounds, Rect viewPortBounds)
        {
            Offsets offsets = new Offsets(this.Parameters.ItemSpace);
            FrameworkElements elements = new FrameworkElements();
            foreach (FrameworkElement element in items)
            {
                Dock dock = DockLayoutControl.GetDock(element);
                if (dock == Dock.None)
                {
                    element.Arrange(new Rect(new Point(element.GetLeft(), element.GetTop()), element.DesiredSize));
                    continue;
                }
                if (dock == Dock.Client)
                {
                    elements.Add(element);
                    continue;
                }
                Rect rect2 = RectHelper.New(element.DesiredSize);
                this.AlignBounds(ref rect2, offsets.GetClientBounds(bounds), dock);
                element.Arrange(rect2);
                offsets.Update(dock, rect2.Size());
            }
            Rect clientBounds = offsets.GetClientBounds(bounds);
            foreach (FrameworkElement element2 in elements)
            {
                element2.Arrange(clientBounds);
            }
            if (this.Parameters.ItemSizers != null)
            {
                this.AddItemSizers(items);
            }
            return base.OnArrange(items, bounds, viewPortBounds);
        }

        protected override Size OnMeasure(FrameworkElements items, Size maxSize)
        {
            Size size = new Size(0.0, 0.0);
            Offsets offsets = new Offsets(this.Parameters.ItemSpace);
            foreach (FrameworkElement element in items)
            {
                Dock itemDock = DockLayoutControl.GetDock(element);
                if (itemDock == Dock.None)
                {
                    element.Measure(maxSize);
                    continue;
                }
                if (itemDock != Dock.Client)
                {
                    element.Measure(offsets.GetClientSize(maxSize));
                    Size desiredSize = element.GetDesiredSize();
                    SizeHelper.UpdateMaxSize(ref size, offsets.GetFullSize(desiredSize));
                    offsets.Update(itemDock, desiredSize);
                }
            }
            Size clientSize = offsets.GetClientSize(maxSize);
            Size zero = SizeHelper.Zero;
            foreach (FrameworkElement element2 in items)
            {
                if (DockLayoutControl.GetDock(element2) == Dock.Client)
                {
                    element2.Measure(clientSize);
                    SizeHelper.UpdateMaxSize(ref zero, element2.GetDesiredSize());
                }
            }
            if (!zero.IsZero())
            {
                SizeHelper.UpdateMaxSize(ref size, offsets.GetFullSize(zero));
            }
            return size;
        }

        protected IDockLayoutModel Model =>
            (IDockLayoutModel) base.Model;

        protected DockLayoutParameters Parameters =>
            (DockLayoutParameters) base.Parameters;

        protected class Offsets : Dictionary<Dock, double>
        {
            public Offsets(double itemSpace)
            {
                this.ItemSpace = itemSpace;
                base.Add(Dock.Left, 0.0);
                base.Add(Dock.Top, 0.0);
                base.Add(Dock.Right, 0.0);
                base.Add(Dock.Bottom, 0.0);
            }

            public Rect GetClientBounds(Rect bounds)
            {
                Size clientSize = this.GetClientSize(bounds.Size());
                return new Rect(bounds.X + base[Dock.Left], bounds.Y + base[Dock.Top], clientSize.Width, clientSize.Height);
            }

            public Size GetClientSize(Size size) => 
                new Size(Math.Max((double) 0.0, (double) (size.Width - (base[Dock.Left] + base[Dock.Right]))), Math.Max((double) 0.0, (double) (size.Height - (base[Dock.Top] + base[Dock.Bottom]))));

            public Size GetFullSize(Size size) => 
                new Size((base[Dock.Left] + size.Width) + base[Dock.Right], (base[Dock.Top] + size.Height) + base[Dock.Bottom]);

            public void Update(Dock itemDock, Size itemSize)
            {
                double num = ((itemDock == Dock.Left) || (itemDock == Dock.Right)) ? itemSize.Width : itemSize.Height;
                Dock dock = itemDock;
                base[dock] += num + this.ItemSpace;
            }

            protected double ItemSpace { get; private set; }
        }
    }
}

