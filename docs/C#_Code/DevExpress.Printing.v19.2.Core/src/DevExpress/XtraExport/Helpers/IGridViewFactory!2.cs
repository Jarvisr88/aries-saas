namespace DevExpress.XtraExport.Helpers
{
    using System;

    public interface IGridViewFactory<TCol, TRow> : IGridViewFactoryBase where TCol: IColumn where TRow: IRowBase
    {
        IGridView<TCol, TRow> GetIViewImplementerInstance();
        void ReleaseIViewImplementerInstance(IGridView<TCol, TRow> instance);
    }
}

