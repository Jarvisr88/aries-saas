namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    internal interface IGroupValuesSource
    {
        string GetText(int key, int index);
        object GetValue(int index, out int group);
        object GetValue(int key, int index);
        IEnumerator<IGrouping<int, object>> Groups();
        ICheckedValuesEnumerator Values(Func<int, bool> isChecked);
        IEnumerator<object> Values(int key, int level);

        int Count { get; }

        object this[int index] { get; }
    }
}

