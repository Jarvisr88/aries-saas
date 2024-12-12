namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class BarManagerLinksHolderController : BarManagerController
    {
        private ILinksHolder holderCore;

        public BarManagerLinksHolderController();
        public BarManagerLinksHolderController(ILinksHolder holder);
        public override void Execute(DependencyObject context = null);
        protected virtual void Initialize(ILinksHolder holder);
        protected virtual void OnAfterExecute();
        protected virtual void OnBeforeExecute();
        protected override void SetUpActionContainer(BarManagerActionContainer container);
        protected virtual void Uninitialize(ILinksHolder holder);

        public ILinksHolder Holder { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarManagerLinksHolderController.<>c <>9;
            public static Func<FrameworkContentElement, object> <>9__8_0;
            public static Func<FrameworkElement, object> <>9__8_1;

            static <>c();
            internal object <OnBeforeExecute>b__8_0(FrameworkContentElement x);
            internal object <OnBeforeExecute>b__8_1(FrameworkElement x);
        }
    }
}

