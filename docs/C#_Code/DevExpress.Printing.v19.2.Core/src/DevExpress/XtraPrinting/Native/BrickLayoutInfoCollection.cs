namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class BrickLayoutInfoCollection : ICollection, IEnumerable
    {
        private Dictionary<Brick, RectangleDF> dictionary;

        public BrickLayoutInfoCollection(Dictionary<Brick, RectangleDF> dictionary);
        void ICollection.CopyTo(Array array, int index);
        IEnumerator IEnumerable.GetEnumerator();

        int ICollection.Count { get; }

        bool ICollection.IsSynchronized { get; }

        object ICollection.SyncRoot { get; }
    }
}

