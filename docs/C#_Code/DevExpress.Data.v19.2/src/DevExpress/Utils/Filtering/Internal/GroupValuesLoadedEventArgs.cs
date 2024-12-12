namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    public class GroupValuesLoadedEventArgs : EventArgs
    {
        public GroupValuesLoadedEventArgs(int index, int count);

        public int GroupIndex { get; private set; }

        public int Count { get; private set; }
    }
}

