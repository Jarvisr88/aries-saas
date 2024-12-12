namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class BarManagerILogicalChildrenContainerWrapper : ILogicalChildrenContainer
    {
        private List<object> children;
        private BarManager manager;

        public BarManagerILogicalChildrenContainerWrapper(BarManager manager);
        public void AddLogicalChild(object child);
        public void RemoveLogicalChild(object child);

        public IEnumerable LogicalChildren { get; }
    }
}

