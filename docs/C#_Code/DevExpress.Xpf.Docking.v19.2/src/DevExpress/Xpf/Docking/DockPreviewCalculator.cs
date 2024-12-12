namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public static class DockPreviewCalculator
    {
        private static Space[] CreateGroupSpaces(LayoutGroup group, bool fHorz)
        {
            Space[] spaceArray = new Space[group.Items.Count];
            for (int i = 0; i < spaceArray.Length; i++)
            {
                BaseLayoutItem item = group[i];
                spaceArray[i] = new Space(fHorz ? item.ItemWidth : item.ItemHeight);
            }
            return spaceArray;
        }

        private static Space[] CreateItemSpaces(BaseLayoutItem item)
        {
            if (LayoutItemsHelper.IsEmptyLayoutGroup(item))
            {
                return new Space[0];
            }
            return new Space[] { CreateSpace(item) };
        }

        private static Space CreateSpace(BaseLayoutItem item)
        {
            double starDockSize = 1.0;
            if (item is LayoutGroup)
            {
                starDockSize = GetStarDockSize(item as LayoutGroup);
            }
            return new Space(new GridLength(starDockSize, GridUnitType.Star));
        }

        private static Space CreateSpace(BaseLayoutItem item, bool horz)
        {
            GridLength length = horz ? item.ItemWidth : item.ItemHeight;
            DockSituation lastDockSituation = item.GetLastDockSituation();
            if (lastDockSituation != null)
            {
                GridLength length2 = horz ? lastDockSituation.Width : lastDockSituation.Height;
                if (length2.IsAbsolute)
                {
                    length = length2;
                }
            }
            return new Space(length);
        }

        public static Size DockPreviewGroup(Rect bounds, BaseLayoutItem dockItem, LayoutGroup targetGroup, DockType type)
        {
            Space[] spaceArray2;
            bool fHorz = type.ToOrientation() == Orientation.Horizontal;
            if (!targetGroup.IgnoreOrientation && (type.ToOrientation() == targetGroup.Orientation))
            {
                spaceArray2 = CreateGroupSpaces(targetGroup, fHorz);
            }
            else
            {
                spaceArray2 = new Space[] { CreateSpace(targetGroup, fHorz) };
            }
            Space[] panels = spaceArray2;
            return MeasureSpaces(bounds, panels, CreateSpace(dockItem, fHorz), type);
        }

        public static Size DockPreviewItem(Rect bounds, BaseLayoutItem dockItem, BaseLayoutItem targetItem, DockType type)
        {
            bool horz = type.ToOrientation() == Orientation.Horizontal;
            Space[] panels = CreateItemSpaces(targetItem);
            return MeasureSpaces(bounds, panels, CreateSpace(dockItem, horz), type);
        }

        public static double GetStarDockSize(LayoutGroup group)
        {
            if (group.Items.Count == 0)
            {
                return 1.0;
            }
            bool flag = group.Orientation == Orientation.Horizontal;
            double count = 0.0;
            int num2 = 0;
            while (true)
            {
                if (num2 < group.Items.Count)
                {
                    GridLength length = flag ? group[num2].ItemWidth : group[num2].ItemHeight;
                    if (length.IsStar)
                    {
                        count += length.Value;
                        num2++;
                        continue;
                    }
                    count = group.Items.Count;
                }
                return (count / ((double) group.Items.Count));
            }
        }

        private static Size MeasureSpaces(Rect bounds, Space[] panels, Space measuredPanel, DockType type)
        {
            Grid grid = new Grid();
            int index = (type.ToInsertType() == InsertType.Before) ? 0 : panels.Length;
            if (type.ToOrientation() == Orientation.Horizontal)
            {
                int num2 = (index == 0) ? 1 : 0;
                int num3 = 0;
                while (true)
                {
                    if (num3 >= panels.Length)
                    {
                        ColumnDefinition definition2 = new ColumnDefinition();
                        definition2.Width = measuredPanel.Length;
                        grid.ColumnDefinitions.Insert(index, definition2);
                        Grid.SetColumn(measuredPanel, index);
                        break;
                    }
                    ColumnDefinition definition1 = new ColumnDefinition();
                    definition1.Width = panels[num3].Length;
                    grid.ColumnDefinitions.Add(definition1);
                    Grid.SetColumn(panels[num3], num2++);
                    grid.Children.Add(panels[num3]);
                    num3++;
                }
            }
            else
            {
                int num4 = (index == 0) ? 1 : 0;
                int num5 = 0;
                while (true)
                {
                    if (num5 >= panels.Length)
                    {
                        RowDefinition definition4 = new RowDefinition();
                        definition4.Height = measuredPanel.Length;
                        grid.RowDefinitions.Insert(index, definition4);
                        Grid.SetRow(measuredPanel, index);
                        break;
                    }
                    RowDefinition definition3 = new RowDefinition();
                    definition3.Height = panels[num5].Length;
                    grid.RowDefinitions.Add(definition3);
                    Grid.SetRow(panels[num5], num4++);
                    grid.Children.Add(panels[num5]);
                    num5++;
                }
            }
            grid.Children.Add(measuredPanel);
            grid.Arrange(bounds);
            return new Size(measuredPanel.ActualWidth, measuredPanel.ActualHeight);
        }

        private class Space : FrameworkElement
        {
            public readonly GridLength Length;

            public Space(GridLength length)
            {
                this.Length = length;
            }
        }
    }
}

