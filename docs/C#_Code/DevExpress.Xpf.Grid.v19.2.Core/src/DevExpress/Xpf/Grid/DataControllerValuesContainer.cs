namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Data;
    using System;
    using System.Runtime.CompilerServices;

    public class DataControllerValuesContainer
    {
        public DataControllerValuesContainer(DevExpress.Xpf.Data.RowHandle rowHandle, int visibleIndex, int level)
        {
            this.RowHandle = rowHandle;
            this.VisibleIndex = visibleIndex;
            this.Level = level;
        }

        public DevExpress.Xpf.Data.RowHandle RowHandle { get; private set; }

        public int VisibleIndex { get; private set; }

        public int Level { get; private set; }
    }
}

