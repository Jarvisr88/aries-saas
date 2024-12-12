namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class BarManagerController : BarManagerControllerBase
    {
        private BarManagerActionContainer container;

        protected virtual BarManagerActionContainer CreateActionContainer();
        public override void Execute(DependencyObject context = null);
        protected virtual void SetUpActionContainer(BarManagerActionContainer container);

        [Description("Gets the container of actions that modify the bar structure of the associated BarManager.")]
        public BarManagerActionContainer ActionContainer { get; }

        protected override IEnumerator LogicalChildren { get; }
    }
}

