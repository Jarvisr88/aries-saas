namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DockSituation
    {
        private BaseLayoutItem dockTargetCore;
        private GridLength[] Lengths;
        private Orientation? LengthsOrientation;
        private readonly WeakReference rootReference;

        public DockSituation(DockType type, BaseLayoutItem target) : this(type, target, Dock.Left)
        {
        }

        public DockSituation(DockType type, BaseLayoutItem target, Dock desiredDock)
        {
            this.Type = type;
            this.DesiredDock = desiredDock;
            this.DockTarget = target;
            this.rootReference = new WeakReference(target.GetRoot());
            GridLength length = new GridLength(1.0, GridUnitType.Star);
            this.Height = length;
            this.Width = length;
        }

        private static Dock Calc(Rect r, Point p, Dock lastDockType)
        {
            Dock top = lastDockType;
            if (r.Contains(p))
            {
                if ((p.X == p.Y) || (p.X == (1.0 - p.Y)))
                {
                    return lastDockType;
                }
                bool flag = (p.Y - p.X) < (r.Top - r.Left);
                bool flag2 = (p.Y + p.X) < (r.Top + r.Right);
                bool flag3 = (p.Y - p.X) > (r.Bottom - r.Right);
                bool flag4 = (p.Y + p.X) > (r.Bottom + r.Left);
                if ((flag & flag2) && (p.Y < (r.Top + (r.Height * 0.5))))
                {
                    top = Dock.Top;
                }
                if ((flag3 & flag4) && (p.Y > (r.Top + (r.Height * 0.5))))
                {
                    top = Dock.Bottom;
                }
                if (!flag && (!flag4 && (p.X < (r.Left + (r.Width * 0.5)))))
                {
                    top = Dock.Left;
                }
                if (!flag2 && (!flag3 && (p.X > (r.Left + (r.Width * 0.5)))))
                {
                    top = Dock.Right;
                }
            }
            return top;
        }

        internal static Dock GetDesiredDock(BaseLayoutItem item)
        {
            Dock? autoHideCenterDock = LayoutItemsHelper.GetAutoHideCenterDock(item);
            if (autoHideCenterDock != null)
            {
                return autoHideCenterDock.Value;
            }
            Rect rect = new Rect(0.0, 0.0, 1.0, 1.0);
            Stack<Dock> stack = new Stack<Dock>(4);
            while (item.Parent != null)
            {
                LayoutGroup parent = item.Parent;
                if (!parent.IgnoreOrientation && (parent.GetVisibleItemsCount() > 1))
                {
                    stack.Push(GetDockInGroup(item, parent));
                }
                item = item.Parent;
            }
            Dock left = Dock.Left;
            while (stack.Count > 0)
            {
                left = stack.Pop();
                switch (left)
                {
                    case Dock.Left:
                    {
                        rect = new Rect(rect.Left, rect.Top, rect.Width * 0.5, rect.Height);
                        continue;
                    }
                    case Dock.Top:
                    {
                        rect = new Rect(rect.Left, rect.Top, rect.Width, rect.Height * 0.5);
                        continue;
                    }
                    case Dock.Right:
                    {
                        rect = new Rect(rect.Left + (rect.Width * 0.5), rect.Top, rect.Width * 0.5, rect.Height);
                        continue;
                    }
                    case Dock.Bottom:
                    {
                        rect = new Rect(rect.Left, rect.Top + (rect.Height * 0.5), rect.Width, rect.Height * 0.5);
                        continue;
                    }
                }
            }
            double x = rect.Left + (rect.Width * 0.5);
            return Calc(new Rect(0.0, 0.0, 1.0, 1.0), new Point(x, rect.Top + (rect.Height * 0.5)), left);
        }

        private static Dock GetDockInGroup(BaseLayoutItem item, LayoutGroup group)
        {
            bool flag2 = group.Orientation == Orientation.Horizontal;
            return (IsOneOfFirstItems(item, group) ? (flag2 ? Dock.Left : Dock.Top) : (flag2 ? Dock.Right : Dock.Bottom));
        }

        public static DockSituation GetDockSituation(BaseLayoutItem item) => 
            GetDockSituation(item, GetDesiredDock(item));

        public static DockSituation GetDockSituation(BaseLayoutItem item, Dock dock) => 
            new DockSituation(GetDockType(item), item.Parent, dock) { 
                Width = item.ItemWidth,
                Height = item.ItemHeight
            };

        private static DockType GetDockType(BaseLayoutItem item)
        {
            LayoutGroup parent = item.Parent;
            if (parent is TabbedGroup)
            {
                return DockType.Fill;
            }
            bool flag = IsOneOfFirstItems(item, parent);
            bool flag2 = parent.IgnoreOrientation || (parent.Orientation == Orientation.Horizontal);
            return (flag ? (flag2 ? DockType.Left : DockType.Top) : (flag2 ? DockType.Right : DockType.Bottom));
        }

        private GridLength[] GetLengths(LayoutGroup group)
        {
            GridLength[] lengthArray = null;
            if ((group != null) && (group.ItemType == LayoutItemType.Group))
            {
                lengthArray = new GridLength[group.Items.Count];
                bool flag = group.Orientation == Orientation.Horizontal;
                for (int i = 0; i < lengthArray.Length; i++)
                {
                    lengthArray[i] = flag ? group[i].ItemWidth : group[i].ItemHeight;
                }
            }
            return lengthArray;
        }

        private static bool IsOneOfFirstItems(BaseLayoutItem item, LayoutGroup group)
        {
            BaseLayoutItem[] visibleItems = group.GetVisibleItems();
            return (Array.IndexOf<BaseLayoutItem>(visibleItems, item) < (visibleItems.Length * 0.5));
        }

        public bool TheSameLengths(LayoutGroup group)
        {
            GridLength[] lengths = this.GetLengths(group);
            if ((lengths == null) || (this.Lengths == null))
            {
                return false;
            }
            if (lengths.Length != this.Lengths.Length)
            {
                return false;
            }
            if ((this.LengthsOrientation != null) && (((Orientation) this.LengthsOrientation.Value) != group.Orientation))
            {
                return false;
            }
            for (int i = 0; i < this.Lengths.Length; i++)
            {
                if (this.Lengths[i] != lengths[i])
                {
                    return false;
                }
            }
            return true;
        }

        public Point FloatLocation { get; set; }

        public GridLength Width { get; set; }

        public GridLength Height { get; set; }

        public Dock DesiredDock { get; private set; }

        public DevExpress.Xpf.Docking.AutoHideType AutoHideType { get; internal set; }

        public DockType Type { get; internal set; }

        public BaseLayoutItem DockTarget
        {
            get => 
                this.dockTargetCore;
            internal set
            {
                Orientation? nullable1;
                this.dockTargetCore = value;
                LayoutGroup group = value as LayoutGroup;
                this.Lengths = this.GetLengths(group);
                if (this.Lengths != null)
                {
                    nullable1 = new Orientation?(group.Orientation);
                }
                else
                {
                    nullable1 = null;
                }
                this.LengthsOrientation = nullable1;
            }
        }

        public LayoutGroup Root =>
            this.rootReference.IsAlive ? (this.rootReference.Target as LayoutGroup) : null;
    }
}

