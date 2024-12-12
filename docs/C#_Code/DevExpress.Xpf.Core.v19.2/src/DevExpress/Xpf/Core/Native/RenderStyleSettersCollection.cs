namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    public class RenderStyleSettersCollection : ObservableCollection<RenderStyleSetter>
    {
        private static readonly object Locker;
        private readonly RenderStyle style;
        private bool isSealed;

        static RenderStyleSettersCollection();
        public RenderStyleSettersCollection(RenderStyle style);
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e);
        public void Seal();
    }
}

