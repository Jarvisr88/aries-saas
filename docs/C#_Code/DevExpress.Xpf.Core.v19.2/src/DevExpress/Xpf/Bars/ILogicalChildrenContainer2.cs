namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;

    public interface ILogicalChildrenContainer2 : ILogicalChildrenContainer
    {
        IEnumerator GetEnumerator();
        void ProcessChildrenChanged(NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs);
    }
}

