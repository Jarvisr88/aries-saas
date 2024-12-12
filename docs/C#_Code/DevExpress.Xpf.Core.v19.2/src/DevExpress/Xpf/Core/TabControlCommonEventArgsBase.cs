namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class TabControlCommonEventArgsBase : EventArgs
    {
        public TabControlCommonEventArgsBase(int index, object item)
        {
            this.TabIndex = index;
            this.Item = item;
        }

        public int TabIndex { get; private set; }

        public object Item { get; private set; }
    }
}

