namespace DevExpress.Xpf.Layout.Core.Selection
{
    using DevExpress.Xpf.Layout.Core;
    using System;

    public static class SelectionHelper
    {
        public static object GetElementKey<T>(T element) => 
            (element is ISelectionKey) ? (element as ISelectionKey).ElementKey : element;

        public static object GetItem<T>(T element) => 
            (element is ISelectionKey) ? (element as ISelectionKey).Item : element;

        public static object[] GetItems<T>(T[] elements) => 
            Array.ConvertAll<T, object>(elements, new Converter<T, object>(SelectionHelper.GetItem<T>));

        public static T[] GetSelectionRange<T>(T[] baseRange, T first, T last) where T: class
        {
            object[] items = GetItems<T>(baseRange);
            int index = Array.IndexOf<object>(items, GetItem<T>(first));
            int num2 = Array.IndexOf<object>(items, GetItem<T>(last));
            if ((index == -1) && (num2 == -1))
            {
                return new T[0];
            }
            if ((index == -1) || (num2 == -1))
            {
                return new T[] { last };
            }
            int num3 = Math.Min(index, num2);
            int num4 = Math.Max(index, num2);
            T[] localArray = new T[(num4 - num3) + 1];
            for (int i = num3; i < (num4 + 1); i++)
            {
                localArray[i - num3] = baseRange[i];
            }
            return localArray;
        }

        public static object GetViewKey<T>(T element) => 
            (element is ISelectionKey) ? (element as ISelectionKey).ViewKey : element;
    }
}

