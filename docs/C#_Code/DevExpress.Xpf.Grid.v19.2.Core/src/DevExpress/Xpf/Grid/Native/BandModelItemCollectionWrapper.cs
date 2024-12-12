namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BandModelItemCollectionWrapper
    {
        private readonly IModelItemCollection source;
        private List<IModelItem> sourceWrapper;

        public BandModelItemCollectionWrapper(IModelItemCollection source)
        {
            this.source = source;
        }

        public void Add(IModelItem item)
        {
            this.source.Add(item);
            this.sourceWrapper = null;
        }

        public void Remove(IModelItem item)
        {
            this.source.Remove(item);
            this.sourceWrapper = null;
        }

        private List<IModelItem> SourceWrapper
        {
            get
            {
                this.sourceWrapper ??= this.source.ToList<IModelItem>();
                return this.sourceWrapper;
            }
        }

        public List<IModelItem> Collection =>
            this.SourceWrapper;

        public int Count =>
            this.SourceWrapper.Count;
    }
}

