namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Customization;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;
    using System.Windows.Media.Animation;

    public class DropBoundsHelper
    {
        public static DropType CalcDropType(BaseLayoutItem item, DragInfo info)
        {
            DropType type = info.Type;
            BaseLayoutItem target = info.Target;
            BaseLayoutItem parent = info.Item;
            if ((item == null) || LayoutItemsHelper.IsParent(target, parent))
            {
                return DropType.None;
            }
            if (ReferenceEquals(item, target) && (type == DropType.Center))
            {
                return type;
            }
            DropType none = DropType.None;
            if (((item.Parent != null) && (!ReferenceEquals(item, parent) && !ReferenceEquals(target, parent))) && ((item != null) && (target != null)))
            {
                LayoutGroup group = item.Parent;
                if (type.ToOrientation() != group.Orientation)
                {
                    return (ReferenceEquals(target, item) ? type : DropType.None);
                }
                if (ReferenceEquals(target, item))
                {
                    none = type;
                    if (LayoutItemsHelper.AreInSameGroup(parent, item))
                    {
                        bool? nullable = LayoutItemsHelper.IsNextNeighbour(parent, item);
                        if (nullable != null)
                        {
                            if (nullable.Value)
                            {
                                if ((type == DropType.Left) || (type == DropType.Top))
                                {
                                    none = DropType.None;
                                }
                            }
                            else if ((type == DropType.Right) || (type == DropType.Bottom))
                            {
                                none = DropType.None;
                            }
                        }
                    }
                }
                else if (LayoutItemsHelper.AreInSameGroup(target, item))
                {
                    bool? nullable2 = LayoutItemsHelper.IsNextNeighbour(target, item);
                    if (nullable2 != null)
                    {
                        if (nullable2.Value)
                        {
                            if (type == DropType.Right)
                            {
                                none = DropType.Left;
                            }
                            if (type == DropType.Bottom)
                            {
                                none = DropType.Top;
                            }
                        }
                        else
                        {
                            if (type == DropType.Left)
                            {
                                none = DropType.Right;
                            }
                            if (type == DropType.Top)
                            {
                                none = DropType.Bottom;
                            }
                        }
                    }
                }
            }
            return none;
        }

        public static Splitter ChooseSplitter(DropType type, Splitter prev, Splitter next)
        {
            switch (type)
            {
                case DropType.Top:
                case DropType.Left:
                    return prev;

                case DropType.Bottom:
                case DropType.Right:
                    return next;
            }
            return null;
        }

        public static DoubleAnimation CreateDoubleAnimation(DependencyObject element, DependencyProperty property, double from, double to)
        {
            DoubleAnimation animation1 = new DoubleAnimation();
            animation1.From = new double?(from);
            animation1.To = new double?(to);
            animation1.BeginTime = new TimeSpan?(TimeSpan.Zero);
            animation1.Duration = TimeSpan.FromMilliseconds(25.0);
            DoubleAnimation animation = animation1;
            Storyboard.SetTarget(animation, element);
            Storyboard.SetTargetProperty(animation, new PropertyPath(property));
            return animation;
        }

        public static GridLengthAnimation CreateGridLengthAnimation(DependencyObject element, DependencyProperty property, GridLength from, GridLength to)
        {
            GridLengthAnimation animation1 = new GridLengthAnimation();
            animation1.From = from;
            animation1.To = to;
            animation1.BeginTime = new TimeSpan?(TimeSpan.Zero);
            animation1.Duration = TimeSpan.FromMilliseconds(20.0);
            GridLengthAnimation animation = animation1;
            Storyboard.SetTarget(animation, element);
            Storyboard.SetTargetProperty(animation, new PropertyPath(property));
            return animation;
        }
    }
}

