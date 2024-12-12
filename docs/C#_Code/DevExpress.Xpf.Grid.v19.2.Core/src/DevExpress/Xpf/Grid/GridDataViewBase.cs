namespace DevExpress.Xpf.Grid
{
    using System;

    public abstract class GridDataViewBase : DataViewBase
    {
        internal GridDataViewBase(MasterNodeContainer masterRootNode, MasterRowsContainer masterRootDataItem, DataControlDetailDescriptor detailDescriptor) : base(masterRootNode, masterRootDataItem, detailDescriptor)
        {
        }
    }
}

