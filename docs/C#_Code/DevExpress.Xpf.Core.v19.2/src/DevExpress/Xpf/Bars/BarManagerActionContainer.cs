namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("Actions")]
    public class BarManagerActionContainer : FrameworkElement, ILogicalChildrenContainer
    {
        private BarManagerActionCollection actions;
        private BarManagerController barManagerController;

        public BarManagerActionContainer();
        public BarManagerActionContainer(BarManagerController barManagerController);
        protected virtual BarManagerActionCollection CreateActions();
        void ILogicalChildrenContainer.AddLogicalChild(object child);
        void ILogicalChildrenContainer.RemoveLogicalChild(object child);
        public virtual void Execute(DependencyObject context = null);
        protected virtual void OnControllerChanged();

        [Description("Gets the BarManagerController object that owns the current object.")]
        public BarManagerController Controller { get; set; }

        [Description("Provides access to the collection of actions.")]
        public BarManagerActionCollection Actions { get; }

        protected override IEnumerator LogicalChildren { get; }
    }
}

