namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    public class StandaloneRowData : RowData
    {
        public StandaloneRowData(DataTreeBuilder treeBuilder, bool updateOnlyData = false, bool updateErrors = true) : base(treeBuilder, updateOnlyData, true, updateErrors)
        {
            base.UseInUpdateQueue = false;
        }

        internal override bool CanReuseCellData() => 
            !this.UpdateOnlyData;

        protected override FrameworkElement CreateRowElement() => 
            new ContentPresenter();

        protected override void UpdateMasterDetailInfo(bool updateRowObjectIfRowExpanded, bool updateDetailRow)
        {
        }
    }
}

