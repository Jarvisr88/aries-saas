namespace DevExpress.Xpf.Layout.Core.Base
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class BaseReadOnlyList<T> : BaseObject, DevExpress.Xpf.Layout.Core.IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, ISupportVisitor<T> where T: class
    {
        private IList<T> listCore;
        private ICollection collectionCore;

        public void Accept(IVisitor<T> visitor)
        {
            if (visitor != null)
            {
                foreach (T local in this.List)
                {
                    visitor.Visit(local);
                }
            }
        }

        public void Accept(VisitDelegate<T> visit)
        {
            if (visit != null)
            {
                foreach (T local in this.List)
                {
                    visit(local);
                }
            }
        }

        public bool Contains(T element) => 
            this.List.Contains(element);

        protected List<T> CreateListCore() => 
            new List<T>();

        protected override void OnCreate()
        {
            base.OnCreate();
            this.listCore = this.CreateListCore();
            this.collectionCore = this.listCore as ICollection;
        }

        protected override void OnDispose()
        {
            Ref.Clear<T>(ref this.listCore);
            base.OnDispose();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => 
            this.List.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            this.List.GetEnumerator();

        protected IList<T> List =>
            this.listCore;

        protected ICollection Collection =>
            this.collectionCore;

        public int Count =>
            this.List.Count;

        public T this[int index] =>
            this.List[index];
    }
}

