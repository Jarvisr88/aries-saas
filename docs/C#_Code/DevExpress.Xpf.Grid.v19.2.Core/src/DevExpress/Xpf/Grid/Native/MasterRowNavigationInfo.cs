namespace DevExpress.Xpf.Grid.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class MasterRowNavigationInfo
    {
        public MasterRowNavigationInfo(int startVisibleIndex, int detailStartVisibleIndex, bool isDetail)
        {
            this.StartVisibleIndex = startVisibleIndex;
            this.DetailStartVisibleIndex = detailStartVisibleIndex;
            this.IsDetail = isDetail;
        }

        public int StartVisibleIndex { get; private set; }

        public int DetailStartVisibleIndex { get; private set; }

        public bool IsDetail { get; private set; }
    }
}

