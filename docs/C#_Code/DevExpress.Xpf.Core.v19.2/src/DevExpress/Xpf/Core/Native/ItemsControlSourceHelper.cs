namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public static class ItemsControlSourceHelper
    {
        public static void AddItem(object itemsSource, object item);
        public static object AddNewItem(object itemsSource);
        private static void CheckItemsSource(object itemsSource);
        public static Type GetItemsSourceItemType(object itemsSource);
        public static void InsertItem(object itemsSource, object item, int index);
        public static void MoveItem(object itemsSource, object item, int newIndex);
        public static void RemoveItem(object itemsSource, object item);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ItemsControlSourceHelper.<>c <>9;
            public static Func<Type, bool> <>9__5_0;
            public static Func<Type, bool> <>9__5_1;

            static <>c();
            internal bool <GetItemsSourceItemType>b__5_0(Type x);
            internal bool <GetItemsSourceItemType>b__5_1(Type x);
        }
    }
}

