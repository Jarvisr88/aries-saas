namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class LayoutEventList
    {
        private const int PocketCapacity = 0x99;
        private LayoutEventList.ListItem _head;
        private LayoutEventList.ListItem _pocket;
        private int _pocketSize;
        private int _count;

        internal LayoutEventList();
        internal LayoutEventList.ListItem Add(object target);
        internal LayoutEventList.ListItem[] CopyToArray();
        private LayoutEventList.ListItem getNewListItem(object target);
        internal void Remove(LayoutEventList.ListItem t);
        private void reuseListItem(LayoutEventList.ListItem t);

        internal int Count { get; }

        internal class ListItem : WeakReference
        {
            internal LayoutEventList.ListItem Next;
            internal LayoutEventList.ListItem Prev;
            internal bool InUse;

            internal ListItem();
        }
    }
}

