namespace DevExpress.Data.Mask
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class TimeSpanMaskHiddenGroupCollection : IEnumerable<TimeSpanMaskHiddenGroup>, IEnumerable
    {
        private readonly List<TimeSpanMaskHiddenGroup> groupList;

        public TimeSpanMaskHiddenGroupCollection();
        public void Add(int startIndex, int endIndex);
        public bool Get(int index, out int startIndex, out int endIndex);
        IEnumerator<TimeSpanMaskHiddenGroup> IEnumerable<TimeSpanMaskHiddenGroup>.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator();
    }
}

