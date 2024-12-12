namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class CollectionActionWrapper : ICollectionAction
    {
        public CollectionActionWrapper(IBarManagerControllerAction action);
        public CollectionActionWrapper(IBarManagerControllerAction action, DependencyObject context);
        public CollectionActionWrapper(IBarManagerControllerAction action, IActionContainer controller, DependencyObject context);
        private void Initialize(IBarManagerControllerAction action, IActionContainer controller, DependencyObject context, bool checkCurrentContainer);

        public DependencyObject Context { get; set; }

        public CollectionActionKind Kind { get; set; }

        public object Element { get; set; }

        public IList Collection { get; set; }

        public int Index { get; set; }

        public object Container { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CollectionActionWrapper.<>c <>9;
            public static Func<IFrameworkInputElement, bool> <>9__3_0;

            static <>c();
            internal bool <Initialize>b__3_0(IFrameworkInputElement x);
        }
    }
}

