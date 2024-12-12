namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public abstract class GridDropTargetFactoryBase : IDropTargetFactory
    {
        protected GridDropTargetFactoryBase()
        {
        }

        protected abstract IDropTarget CreateDropTarget(UIElement dropTargetElement);
        IDropTarget IDropTargetFactory.CreateDropTarget(UIElement dropTargetElement) => 
            this.CreateDropTarget(dropTargetElement);

        protected virtual bool IsCompatibleDataControl(DataControlBase sourceDataControl, DataControlBase targetDataControl) => 
            ReferenceEquals(sourceDataControl.GetOriginationDataControl(), targetDataControl.GetOriginationDataControl());

        public virtual bool IsCompatibleDropTargetFactory(UIElement dropTargetElement, BaseGridHeader sourceHeader) => 
            this.IsCompatibleDataControl(DataControlBase.FindCurrentView(dropTargetElement).DataControl, sourceHeader.GridView.DataControl);
    }
}

