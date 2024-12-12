namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System.Windows;

    public class RemoveBandDropTarget : RemoveColumnDropTarget
    {
        protected override UIElement GetTargetElement(DataViewBase view) => 
            view.DataControl.BandsLayoutCore.GetBandsContainerControl();
    }
}

