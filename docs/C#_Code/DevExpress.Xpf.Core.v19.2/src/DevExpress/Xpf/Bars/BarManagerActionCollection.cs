namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class BarManagerActionCollection : ObservableCollection<IBarManagerControllerAction>
    {
        public BarManagerActionCollection(BarManagerActionContainer container);
        protected override void ClearItems();
        public virtual void Execute();
        public virtual void Execute(DependencyObject context);
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e);

        [Description("Gets the BarManagerActionContainer that owns the current collection.")]
        public BarManagerActionContainer Container { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarManagerActionCollection.<>c <>9;
            public static Action<DependencyObject> <>9__6_0;

            static <>c();
            internal void <Execute>b__6_0(DependencyObject x);
        }
    }
}

