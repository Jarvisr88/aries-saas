namespace DevExpress.Xpf.Grid.Printing
{
    using DevExpress.Xpf.Grid;

    public interface IDescriptorAndDataControlBase
    {
        DataControlBase GetDetailGridControl(PrintingDataTreeBuilderBase treeBuilder);

        DataControlBase Grid { get; }

        DetailDescriptorBase Descriptor { get; }
    }
}

