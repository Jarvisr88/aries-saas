namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class CustomComboBoxItemComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            CustomComboBoxItem objA = x as CustomComboBoxItem;
            CustomComboBoxItem objB = y as CustomComboBoxItem;
            return (!ReferenceEquals(objA, objB) ? ((objA != null) ? ((objB != null) ? Comparer<object>.Default.Compare(objA.DisplayValue, objB.DisplayValue) : 1) : -1) : 0);
        }
    }
}

