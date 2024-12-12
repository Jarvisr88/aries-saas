namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;

    public abstract class VirtualItemsEnumeratorBase : NestedObjectEnumeratorBase
    {
        protected static IEnumerable EmptyEnumerable = new object[0];

        public VirtualItemsEnumeratorBase(object obj) : base(obj)
        {
        }

        protected abstract IEnumerator GetContainerEnumerator(object obj);
        protected abstract IEnumerable GetGroupEnumerable(object obj);
        protected override IEnumerator GetNestedObjects(object obj)
        {
            IEnumerator containerEnumerator = this.GetContainerEnumerator(obj);
            return ((containerEnumerator == null) ? this.GetGroupEnumerable(obj).GetEnumerator() : containerEnumerator);
        }

        public override void Reset()
        {
            base.Reset();
            this.MoveNext();
        }
    }
}

