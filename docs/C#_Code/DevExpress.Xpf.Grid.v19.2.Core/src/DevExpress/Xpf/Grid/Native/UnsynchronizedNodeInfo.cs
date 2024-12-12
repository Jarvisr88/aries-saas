namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;

    public class UnsynchronizedNodeInfo
    {
        public UnsynchronizedNodeInfo(DevExpress.Xpf.Grid.RowsContainer dataContainer, DevExpress.Xpf.Grid.NodeContainer nodeContainer, int nodeIndex)
        {
            this.NodeContainer = nodeContainer;
            this.NodeIndex = nodeIndex;
            this.RowsContainer = dataContainer;
        }

        public DevExpress.Xpf.Grid.NodeContainer NodeContainer { get; private set; }

        public int NodeIndex { get; private set; }

        public DevExpress.Xpf.Grid.RowsContainer RowsContainer { get; private set; }
    }
}

