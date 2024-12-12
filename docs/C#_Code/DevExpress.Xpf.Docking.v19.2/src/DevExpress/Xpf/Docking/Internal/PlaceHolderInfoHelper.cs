namespace DevExpress.Xpf.Docking.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal class PlaceHolderInfoHelper
    {
        private readonly PlaceHolderCollection dockInfo = new PlaceHolderCollection();

        public void Add(PlaceHolder placeHolder)
        {
            this.dockInfo.Add(placeHolder);
        }

        internal PlaceHolderCollection AsCollection() => 
            this.dockInfo;

        internal IEnumerable<PlaceHolder> AsEnumerable() => 
            this.dockInfo;

        public void Clear()
        {
            this.dockInfo.Clear();
        }

        public bool Contains(PlaceHolder placeHolder) => 
            this.dockInfo.Contains(placeHolder);

        public PlaceHolder Find(Func<PlaceHolder, bool> predicate) => 
            this.FirstOrDefault(predicate);

        public PlaceHolder FirstOrDefault() => 
            this.dockInfo.FirstOrDefault<PlaceHolder>();

        public PlaceHolder FirstOrDefault(Func<PlaceHolder, bool> predicate) => 
            this.dockInfo.FirstOrDefault<PlaceHolder>(predicate);

        public void ForeEach(Action<PlaceHolder> action)
        {
            this.dockInfo.ForEach(action);
        }

        public bool HasItems() => 
            this.Count > 0;

        public PlaceHolder LastOrDefault() => 
            this.dockInfo.LastOrDefault<PlaceHolder>();

        public PlaceHolder LastOrDefault(Func<PlaceHolder, bool> predicate) => 
            this.dockInfo.LastOrDefault<PlaceHolder>(predicate);

        public void Remove(PlaceHolder ph)
        {
            this.dockInfo.Remove(ph);
        }

        internal List<PlaceHolder> ToList() => 
            this.dockInfo.ToList<PlaceHolder>();

        public int Count =>
            this.dockInfo.Count;

        internal PlaceHolder this[int index] =>
            this.dockInfo[index];
    }
}

