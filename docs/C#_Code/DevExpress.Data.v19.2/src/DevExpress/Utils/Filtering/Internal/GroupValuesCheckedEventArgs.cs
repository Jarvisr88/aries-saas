namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class GroupValuesCheckedEventArgs : EventArgs, IEnumerable<object>, IEnumerable
    {
        private readonly ICheckedGroupValues checkedValues;

        public GroupValuesCheckedEventArgs(ICheckedGroupValues checkedValues);
        IEnumerator<object> IEnumerable<object>.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator();
    }
}

