namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Mvvm.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class NotificationPositioner<T> where T: class
    {
        internal NotificationPosition position;
        internal int maxCount;
        private Rect screen;
        private const double verticalMargin = 10.0;
        private const double verticalScreenMargin = 20.0;
        private readonly List<ItemInfo<T>> items;
        private double itemWidth;
        private double itemHeight;

        public NotificationPositioner()
        {
            this.items = new List<ItemInfo<T>>();
        }

        public Point Add(T item, double width, double height)
        {
            this.itemWidth = width;
            this.itemHeight = height;
            ItemInfo<T> newInfo = new ItemInfo<T>();
            newInfo.value = item;
            newInfo.size = new Size(width, height);
            this.ReplaceSlotValue(null, newInfo);
            return this.GetItemPosition(item);
        }

        private List<ItemInfo<T>> CloneItemsCollection() => 
            new List<ItemInfo<T>>(this.items);

        public Point GetItemPosition(T item)
        {
            ItemInfo<T> info = this.CloneItemsCollection().FirstOrDefault<ItemInfo<T>>(i => (i != null) && (i.value == item));
            if (info == null)
            {
                return new Point(-1.0, -1.0);
            }
            int index = this.items.IndexOf(info);
            return new Point((this.screen.X + this.screen.Width) - info.size.Width, (this.position != NotificationPosition.TopRight) ? ((((this.screen.Height + this.screen.Y) - info.size.Height) - 20.0) - (index * (info.size.Height + 10.0))) : ((this.screen.Y + 20.0) + (index * (info.size.Height + 10.0))));
        }

        public bool HasEmptySlot()
        {
            bool flag1;
            List<ItemInfo<T>> source = this.CloneItemsCollection();
            if (source.Count < this.maxCount)
            {
                flag1 = true;
            }
            else
            {
                Func<ItemInfo<T>, bool> predicate = <>c<T>.<>9__16_0;
                if (<>c<T>.<>9__16_0 == null)
                {
                    Func<ItemInfo<T>, bool> local1 = <>c<T>.<>9__16_0;
                    predicate = <>c<T>.<>9__16_0 = i => ReferenceEquals(i, null);
                }
                flag1 = source.Any<ItemInfo<T>>(predicate);
            }
            bool flag = flag1;
            int num = source.Where<ItemInfo<T>>((<>c<T>.<>9__16_1 ??= i => (i != null))).Count<ItemInfo<T>>();
            double num2 = 40.0 + ((num <= 1) ? 0.0 : ((num - 1) * 10.0));
            return (flag & ((((1 + num) * this.itemHeight) + num2) <= this.screen.Height));
        }

        public void Remove(T item)
        {
            ItemInfo<T> oldInfo = this.CloneItemsCollection().First<ItemInfo<T>>(i => (i != null) && (i.value == item));
            this.ReplaceSlotValue(oldInfo, null);
        }

        private void ReplaceSlotValue(ItemInfo<T> oldInfo, ItemInfo<T> newInfo)
        {
            for (int i = 0; i < this.items.Count; i++)
            {
                if (this.items[i] == oldInfo)
                {
                    this.items[i] = newInfo;
                    return;
                }
            }
            this.items.Add(newInfo);
        }

        public void Update(Rect screen)
        {
            this.Update(screen, this.position, this.maxCount);
        }

        public void Update(Rect screen, NotificationPosition position, int maxCount)
        {
            if ((this.screen != screen) || ((this.position != position) || (this.maxCount != maxCount)))
            {
                this.screen = screen;
                this.position = position;
                this.maxCount = maxCount;
                Func<ItemInfo<T>, bool> predicate = <>c<T>.<>9__12_0;
                if (<>c<T>.<>9__12_0 == null)
                {
                    Func<ItemInfo<T>, bool> local1 = <>c<T>.<>9__12_0;
                    predicate = <>c<T>.<>9__12_0 = i => i != null;
                }
                List<ItemInfo<T>> list = this.CloneItemsCollection().Where<ItemInfo<T>>(predicate).ToList<ItemInfo<T>>();
                this.items.Clear();
                foreach (ItemInfo<T> info in list)
                {
                    this.Add(info.value, info.size.Width, info.size.Height);
                }
            }
        }

        public List<T> Items
        {
            get
            {
                Func<ItemInfo<T>, T> selector = <>c<T>.<>9__8_0;
                if (<>c<T>.<>9__8_0 == null)
                {
                    Func<ItemInfo<T>, T> local1 = <>c<T>.<>9__8_0;
                    selector = <>c<T>.<>9__8_0 = delegate (ItemInfo<T> i) {
                        if (i != null)
                        {
                            return i.value;
                        }
                        return default(T);
                    };
                }
                return this.CloneItemsCollection().Select<ItemInfo<T>, T>(selector).ToList<T>();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NotificationPositioner<T>.<>c <>9;
            public static Func<NotificationPositioner<T>.ItemInfo, T> <>9__8_0;
            public static Func<NotificationPositioner<T>.ItemInfo, bool> <>9__12_0;
            public static Func<NotificationPositioner<T>.ItemInfo, bool> <>9__16_0;
            public static Func<NotificationPositioner<T>.ItemInfo, bool> <>9__16_1;

            static <>c()
            {
                NotificationPositioner<T>.<>c.<>9 = new NotificationPositioner<T>.<>c();
            }

            internal T <get_Items>b__8_0(NotificationPositioner<T>.ItemInfo i)
            {
                if (i != null)
                {
                    return i.value;
                }
                return default(T);
            }

            internal bool <HasEmptySlot>b__16_0(NotificationPositioner<T>.ItemInfo i) => 
                ReferenceEquals(i, null);

            internal bool <HasEmptySlot>b__16_1(NotificationPositioner<T>.ItemInfo i) => 
                i != null;

            internal bool <Update>b__12_0(NotificationPositioner<T>.ItemInfo i) => 
                i != null;
        }

        private class ItemInfo
        {
            public T value;
            public Size size;
        }
    }
}

