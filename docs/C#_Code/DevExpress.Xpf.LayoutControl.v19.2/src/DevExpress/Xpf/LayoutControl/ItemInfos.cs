namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ItemInfos : List<DevExpress.Xpf.LayoutControl.ItemInfo>
    {
        public ItemInfos(double availableLength, double itemSpace)
        {
            this.AvailableLength = availableLength;
            this.ItemSpace = itemSpace;
        }

        public virtual void ArrangeItems(FrameworkElements items, Func<FrameworkElement, ItemAlignment> getItemAlignment)
        {
            int num = 0;
            int num2 = 0;
            int index = 0;
            while (index < (items.Count - (num + num2)))
            {
                FrameworkElement arg = items[index];
                ItemAlignment alignment = getItemAlignment(arg);
                if (alignment == ItemAlignment.Center)
                {
                    items.RemoveAt(index);
                    items.Insert(items.Count - num2, arg);
                    num++;
                    continue;
                }
                if (alignment != ItemAlignment.End)
                {
                    index++;
                    continue;
                }
                items.RemoveAt(index);
                items.Insert(items.Count, arg);
                num2++;
            }
        }

        public virtual void Calculate()
        {
            this.CalculateStretchedLengths();
            this.CalculateOffsets();
        }

        protected virtual void CalculateOffsets()
        {
            Predicate<DevExpress.Xpf.LayoutControl.ItemInfo> isItemForProcessing = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                Predicate<DevExpress.Xpf.LayoutControl.ItemInfo> local1 = <>c.<>9__11_0;
                isItemForProcessing = <>c.<>9__11_0 = itemInfo => (itemInfo.Alignment == ItemAlignment.Start) || (itemInfo.Alignment == ItemAlignment.Stretch);
            }
            double num = this.CalculateOffsets(0.0, true, isItemForProcessing);
            Predicate<DevExpress.Xpf.LayoutControl.ItemInfo> predicate2 = <>c.<>9__11_1;
            if (<>c.<>9__11_1 == null)
            {
                Predicate<DevExpress.Xpf.LayoutControl.ItemInfo> local2 = <>c.<>9__11_1;
                predicate2 = <>c.<>9__11_1 = itemInfo => itemInfo.Alignment == ItemAlignment.End;
            }
            Predicate<DevExpress.Xpf.LayoutControl.ItemInfo> predicate3 = <>c.<>9__11_2;
            if (<>c.<>9__11_2 == null)
            {
                Predicate<DevExpress.Xpf.LayoutControl.ItemInfo> local3 = <>c.<>9__11_2;
                predicate3 = <>c.<>9__11_2 = itemInfo => itemInfo.Alignment == ItemAlignment.Center;
            }
            this.CalculateOffsets(Math.Round((double) (((num + this.CalculateOffsets(Math.Max(this.Length, this.AvailableLength), false, predicate2)) - this.CenteredLength) / 2.0)), true, predicate3);
        }

        protected virtual double CalculateOffsets(double offset, bool forward, Predicate<DevExpress.Xpf.LayoutControl.ItemInfo> isItemForProcessing)
        {
            for (int i = forward ? 0 : (base.Count - 1); (forward && (i < base.Count)) || (!forward && (i >= 0)); i = !forward ? (i - 1) : (i + 1))
            {
                DevExpress.Xpf.LayoutControl.ItemInfo info = base[i];
                if (isItemForProcessing(info))
                {
                    if (!forward)
                    {
                        offset -= info.Length;
                    }
                    info.Offset = offset;
                    offset = !forward ? (offset - this.ItemSpace) : (offset + (info.Length + this.ItemSpace));
                }
            }
            return offset;
        }

        protected virtual void CalculateStretchedLengths()
        {
            List<DevExpress.Xpf.LayoutControl.ItemInfo> stretchedItems = this.GetStretchedItems();
            if (stretchedItems.Count != 0)
            {
                StretchedLengthsCalculator calculator = new StretchedLengthsCalculator(Math.Max((double) 0.0, (double) ((this.AvailableLength - this.FixedLength) - ((stretchedItems.Count - 1) * this.ItemSpace))));
                calculator.AddRange(stretchedItems);
                if (calculator.CanFitItemsUniformly())
                {
                    Action<DevExpress.Xpf.LayoutControl.ItemInfo> action = <>c.<>9__13_0;
                    if (<>c.<>9__13_0 == null)
                    {
                        Action<DevExpress.Xpf.LayoutControl.ItemInfo> local1 = <>c.<>9__13_0;
                        action = <>c.<>9__13_0 = delegate (DevExpress.Xpf.LayoutControl.ItemInfo item) {
                            item.MinLength = item.Length;
                            item.Length = 0.0;
                        };
                    }
                    calculator.ForEach(action);
                }
                calculator.Calculate();
            }
        }

        protected virtual double GetLength(ItemAlignment? alignment)
        {
            double num = 0.0;
            foreach (DevExpress.Xpf.LayoutControl.ItemInfo info in this)
            {
                if (alignment != null)
                {
                    ItemAlignment? nullable = alignment;
                    if (!((info.Alignment == ((ItemAlignment) nullable.GetValueOrDefault())) ? (nullable != null) : false))
                    {
                        continue;
                    }
                }
                num += info.Length + this.ItemSpace;
            }
            if (num != 0.0)
            {
                num -= this.ItemSpace;
            }
            return num;
        }

        protected List<DevExpress.Xpf.LayoutControl.ItemInfo> GetStretchedItems()
        {
            List<DevExpress.Xpf.LayoutControl.ItemInfo> list = new List<DevExpress.Xpf.LayoutControl.ItemInfo>();
            foreach (DevExpress.Xpf.LayoutControl.ItemInfo info in this)
            {
                if (info.Alignment == ItemAlignment.Stretch)
                {
                    list.Add(info);
                }
            }
            return list;
        }

        public double AvailableLength { get; private set; }

        public double ItemSpace { get; private set; }

        protected double CenteredLength =>
            this.GetLength(1);

        protected double FixedLength =>
            this.Length - this.GetLength(3);

        protected double Length
        {
            get
            {
                ItemAlignment? alignment = null;
                return this.GetLength(alignment);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ItemInfos.<>c <>9 = new ItemInfos.<>c();
            public static Predicate<DevExpress.Xpf.LayoutControl.ItemInfo> <>9__11_0;
            public static Predicate<DevExpress.Xpf.LayoutControl.ItemInfo> <>9__11_1;
            public static Predicate<DevExpress.Xpf.LayoutControl.ItemInfo> <>9__11_2;
            public static Action<DevExpress.Xpf.LayoutControl.ItemInfo> <>9__13_0;

            internal bool <CalculateOffsets>b__11_0(DevExpress.Xpf.LayoutControl.ItemInfo itemInfo) => 
                (itemInfo.Alignment == ItemAlignment.Start) || (itemInfo.Alignment == ItemAlignment.Stretch);

            internal bool <CalculateOffsets>b__11_1(DevExpress.Xpf.LayoutControl.ItemInfo itemInfo) => 
                itemInfo.Alignment == ItemAlignment.End;

            internal bool <CalculateOffsets>b__11_2(DevExpress.Xpf.LayoutControl.ItemInfo itemInfo) => 
                itemInfo.Alignment == ItemAlignment.Center;

            internal void <CalculateStretchedLengths>b__13_0(DevExpress.Xpf.LayoutControl.ItemInfo item)
            {
                item.MinLength = item.Length;
                item.Length = 0.0;
            }
        }
    }
}

