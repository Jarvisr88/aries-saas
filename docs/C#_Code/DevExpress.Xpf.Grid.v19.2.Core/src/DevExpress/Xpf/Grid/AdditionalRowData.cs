namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class AdditionalRowData : RowData
    {
        private FrameworkElement additionalRowElement;

        public AdditionalRowData(DataTreeBuilder treeBuilder) : base(treeBuilder, false, true, true)
        {
        }

        internal void ChangeWholeElement(FrameworkElement rowElement)
        {
            this.SetWholeElement(rowElement);
        }

        protected override FrameworkElement CreateRowElement() => 
            new ContentPresenter();

        internal void SetDetailLevel(int val)
        {
            base.DetailLevel = val;
        }

        internal void SetRowElement(FrameworkElement element)
        {
            this.additionalRowElement = element;
        }

        internal override void SetRowStateClient(IRowStateClient rowStateClient)
        {
            base.SetRowStateClient(rowStateClient);
            base.UpdateContent();
        }

        internal void UpdateVisible()
        {
            base.UpdateClientRowPosition();
        }

        internal override void ValidateSetRowStateClient()
        {
        }

        internal override FrameworkElement RowElement =>
            this.additionalRowElement ?? base.RowElement;
    }
}

