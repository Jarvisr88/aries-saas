namespace DevExpress.Xpf.Grid.Printing
{
    using DevExpress.Xpf.Grid;
    using System;

    public interface ISupportMasterDetailPrinting
    {
        IDescriptorAndDataControlBase GetDescriptorAndGridControl(DataControlDetailDescriptor descriptor);
        bool IsGeneratedControl(DataControlBase grid);
    }
}

